'use client'

import Loading from "@/app/loading"
import { getAllMedicalTeamsAssignedToCoordinator } from "@/requests/GET/getAllMedicalTeamsAssignedToCoordinator"
import FullCalendar from "@fullcalendar/react"
import { useMutation, useQuery } from "@tanstack/react-query"
import dayGridPlugin from '@fullcalendar/daygrid'
import interactionPlugin from '@fullcalendar/interaction'
import timeGridPlugin from '@fullcalendar/timegrid'
import { FormEvent, useState } from "react"
import { getScheduleByMonthAndYearAndMedicalTeamId } from "@/requests/GET/getScheduleByMonthAndYearAndMedicalTeamId"
import { DeleteEmploymentContract } from "@/requests/PUT/deleteEmploymentContract"
import { createSchedule } from "@/requests/POST/createSchedule"
import Modal from 'react-modal';
import { customStyles } from "@/styles/modalStyle"
import { getMedcialWorkersByMedcalTeamIdAndMedicRole } from "@/requests/GET/getMedcialWorkersByMedcalTeamIdAndMedicRole"
import { AddMedicalWorkerToShift } from "@/requests/PUT/addMedicalWorkerToShift"
import { getShiftsByUserIdAndYearAndMonth } from "@/requests/GET/getShiftsByUserIdAndYearAndMonth"
import { getMedicalWorkerByUserId } from "@/requests/GET/getMedicalWorkerByUserId"
import { AddDaysOffToMedicalWorker } from "@/requests/PUT/addDaysOffToMedicalWorker"



const ScheduleMedicalWorker = ({userId}) => {

    const [selectedMonthAndYearCalendar,setSelectedMonthAndYearCalendar] = useState(new Date())
    const [selectedMedicalTeamId,setSelectedMedicalTeamId] = useState('')
    const [isScheduleCreated,setIsScheduleCreated] = useState(true)
    const [isClicked, setIsClicked] = useState(false);
    const [events, setEvents] = useState([]);
    const [selectedshiftId,setSelectedshiftId] = useState('')
    const [selectedShiftData,setSelectedShiftData] = useState(Object)
    const [openDaysOff,setOpenDaysOff] = useState(false)
    const [selectedDriverId,setSelectedDriverId] = useState('')
    const [selectedManagerId,setSelectedManagerId] = useState('')
    const [selectedCrewMemberId,setSelectedCrewMemberId] = useState('')

    const schedule = useQuery({
      queryKey: ['GetMedicalWorkerSchedule'],
      queryFn: async () => await getShiftsByUserIdAndYearAndMonth(userId,selectedMonthAndYearCalendar.getFullYear(),(Number(selectedMonthAndYearCalendar.getMonth())+1)),
      enabled:false,
      retry:1,
      refetchOnWindowFocus:false
    })
    const medicalWorker = useQuery({
      queryKey: ['GetMedicalWorker'],
      queryFn: async () => await getMedicalWorkerByUserId(userId),
      enabled:false,
      retry:1,
      refetchOnWindowFocus:false
    })
    const addDayOffToMedicalWorker = useMutation({
      mutationFn: async ({medicalWorkerId,start,end}) => {
        await AddDaysOffToMedicalWorker(medicalWorkerId,start,end);
      },
      onSuccess: async()=>{schedule.refetch()}
    })
  async function saveModal(event: FormEvent<HTMLFormElement>)
  {
    event.preventDefault()
    const formData = new FormData(event?.currentTarget)
    
    const start = formData.get('start')
    const end = formData.get('end')
    
    await addDayOffToMedicalWorker.mutate({medicalWorkerId:medicalWorker.data.id,start:start,end:end})

    alert("Zapisano!!!")
    setOpenDaysOff(false);
  }
    function handleClickEvent(info) 
    {
      alert('Clicked on: ' + info.event.start);
    }
    if(schedule.isFetching)
    {
        return (Loading())
    }

let e = [];

if (schedule.data && schedule.data.length > 0) {

  const shiftsEvents = schedule.data.map((shift) => ({
    shiftId: shift.id,
    start: shift.dateRange.start,
    end: shift.dateRange.start, 
    title: (new Date(shift.dateRange.start).getHours() === 7 && new Date(shift.dateRange.end).getHours() === 19 ? "Dyżur dzienny" : "Dyżur nocny"),
    medicalTeamDisplayText: shift.medicalTeam.informationAboutTeam.code + ' ' + shift.medicalTeam.informationAboutTeam.city,
    managerDisplayText: 'Kierownik: ' + (shift.manager == undefined ? '' : shift.manager?.user?.name?.firstName + ' ' + shift.manager?.user?.name?.lastName),
    driverDisplayText: 'Kierowca: ' + (shift.driver == undefined ? '' : shift.driver?.user?.name?.firstName + ' ' + shift.driver?.user?.name?.lastName),
    crewMemberDisplayText: 'Członek: ' + (shift.crewMember == undefined ? '' : shift.crewMember?.user?.name?.firstName + ' ' + shift.crewMember?.user?.name?.lastName),
    managerId: shift.manager?.id,
    driverId: shift.driver?.id,
    crewMemberId: shift.crewMember?.id,
    managerName:  (shift.manager == undefined ? '' : shift.manager?.user?.name?.firstName + ' ' + shift.manager?.user?.name?.lastName),
    driverName: (shift.driver == undefined ? '' : shift.driver?.user?.name?.firstName + ' ' + shift.driver?.user?.name?.lastName),
    crewMemberName:  (shift.crewMember == undefined ? '' : shift.crewMember?.user?.name?.firstName + ' ' + shift.crewMember?.user?.name?.lastName),
  }));

  e = e.concat(shiftsEvents);

}

if (medicalWorker.data?.daysOff && medicalWorker.data.daysOff.length > 0) {
  const daysOffEvents = medicalWorker.data.daysOff.map((dayOff) => ({
    start: dayOff.start,
    end: new Date(dayOff.end).setHours(new Date(dayOff.end).getHours() + 1),
    title: `<div style="display: flex; justify-content: center; align-items: center;">
    <div style="text-align: center; font-size: 20px; overflow: hidden; max-width: 100%;">
        Urlop
    </div>
</div>`,
    medicalTeamDisplayText:'<br>',
    managerDisplayText:'<br>',
    driverDisplayText: '<br>',
    crewMemberDisplayText:'<br><br><br>'
  }));
  console.log(daysOffEvents)
  e = e.concat(daysOffEvents);
}


    return (
      <div className="container">
      <div >
      <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold ml-2 py-2 px-4 rounded" onClick={()=>{schedule.refetch();medicalWorker.refetch()}}>Wczytaj</button>
      <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold ml-2 py-2 px-4 rounded" onClick={() => setOpenDaysOff(true)}>Dodaj dni wolne</button>
      <div className='overflow-hidden overflow-y-hidden'>
              
      {openDaysOff == false ?
      <FullCalendar
        plugins={[ dayGridPlugin,interactionPlugin,timeGridPlugin ]}
        aspectRatio={1.9}
        showNonCurrentDates={false}
        editable={false}
        locale={'pl'}
        timeZone='local'
        initialDate={selectedMonthAndYearCalendar}
        datesSet={(arg) => {
            setSelectedMonthAndYearCalendar(arg.view.currentStart) 
          }}
        displayEventTime={true}
        displayEventEnd={true}
        eventDisplay="block"
        eventContent={(eventInfo) => 
          { return { html: eventInfo.event.extendedProps.medicalTeamDisplayText + "<br> " + 
          eventInfo.event.title + "<br> " + eventInfo.event.extendedProps.driverDisplayText  + "<br> " + 
          eventInfo.event.extendedProps.managerDisplayText  + "<br> " + 
          eventInfo.event.extendedProps.crewMemberDisplayText }
          }}
        events={
        e
      } 
      />
      : '' }
      </div>
      </div>
      <Modal
      isOpen={openDaysOff}
      onRequestClose={() => {setOpenDaysOff(false);setSelectedDriverId('');setSelectedManagerId('');setSelectedCrewMemberId('')}}
      contentLabel="Add or Remove Items Modal"
      style={customStyles}
      >
      <h2 className="text-lg font-bold mb-4">Dni urlopowe</h2>
      <form onSubmit={saveModal}>
        <label className="block mb-4">
          Data rozpoczynająca urlop:
          <input type="date" name="start" id="start" className="block w-full border border-gray-300 rounded px-3 py-2" />
        </label>

        <label className="block mb-4">
          Data kończąca urlop:
          <input type="date" name="end" id="end" className="block w-full border border-gray-300 rounded px-3 py-2" />
        </label>

        <div className="flex justify-end">
          <button type="submit" 
        className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 mr-2 rounded">
            Dodaj
          </button>
        
          <button onClick={()=>setOpenDaysOff(false)} className="bg-gray-300 hover:bg-gray-400 text-gray-800 font-bold py-2 px-4 rounded">
            Anuluj
          </button>
          </div>
        </form>
    </Modal> 
     
      </div>
    )
}
export default ScheduleMedicalWorker;