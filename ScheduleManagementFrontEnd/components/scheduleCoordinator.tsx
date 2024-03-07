'use client'

import Loading from "@/app/loading"
import { getAllMedicalTeamsAssignedToCoordinator } from "@/requests/GET/getAllMedicalTeamsAssignedToCoordinator"
import FullCalendar from "@fullcalendar/react"
import { useMutation, useQuery } from "@tanstack/react-query"
import dayGridPlugin from '@fullcalendar/daygrid'
import interactionPlugin from '@fullcalendar/interaction'
import timeGridPlugin from '@fullcalendar/timegrid'
import { useState } from "react"
import { getScheduleByMonthAndYearAndMedicalTeamId } from "@/requests/GET/getScheduleByMonthAndYearAndMedicalTeamId"
import { DeleteEmploymentContract } from "@/requests/PUT/deleteEmploymentContract"
import { createSchedule } from "@/requests/POST/createSchedule"
import Modal from 'react-modal';
import { customStyles } from "@/styles/modalStyle"
import { getMedcialWorkersByMedcalTeamIdAndMedicRole } from "@/requests/GET/getMedcialWorkersByMedcalTeamIdAndMedicRole"
import { AddMedicalWorkerToShift } from "@/requests/PUT/addMedicalWorkerToShift"

/*interface Shift
{
  id:         string;
  scheduleId: string;
  driver:     MedicalWorkerShift | undefined;
  manager:    MedicalWorkerShift | undefined;
  crewMember: MedicalWorkerShift | undefined;
  shiftType:  string;
}
interface MedicalWorkerShift
{
  id:   string;
  name: string;
}*/


const ScheduleCoordinator = ({userId}) => {

    const [selectedMonthAndYearCalendar,setSelectedMonthAndYearCalendar] = useState(new Date())
    const [selectedMedicalTeamId,setSelectedMedicalTeamId] = useState('')
    const [isScheduleCreated,setIsScheduleCreated] = useState(true)
    const [isClicked, setIsClicked] = useState(false);
    const [events, setEvents] = useState([]);
    const [selectedshiftId,setSelectedshiftId] = useState('')
    const [selectedShiftData,setSelectedShiftData] = useState(Object)
    const [openShiftModal,setOpenShiftModal] = useState(false)
    const [selectedDriverId,setSelectedDriverId] = useState('')
    const [selectedManagerId,setSelectedManagerId] = useState('')
    const [selectedCrewMemberId,setSelectedCrewMemberId] = useState('')



    function handleClickEvent(info) {
      setOpenShiftModal(true)

      setSelectedshiftId(info.event.extendedProps.shiftId);
  
      setSelectedShiftData(
        {
          shiftId: info.event.extendedProps.shiftId,
          start:info.event.start,
          end:info.event.end,
          title: info.event.title,
          manager: info.event.extendedProps.manager,
          driver: info.event.extendedProps.driver,
          crewMemberId: info.event.extendedProps.crewMemberId,
          managerId: info.event.extendedProps.managerId,
          driverId: info.event.extendedProps.driverId,
          managerName:  info.event.extendedProps.managerName,
          driverName: info.event.extendedProps.driverName,
          crewMemberName: info.event.extendedProps.crewMemberName
    },
      )
      
    }
  
    const addMedicalWorkerToShift = useMutation({
      mutationFn: async ({managerId,managerMedicRole,shiftId,driverId,driverMedicRole,crewMemberId,crewMemberMedicRole}) => {
        await AddMedicalWorkerToShift(managerId,shiftId,managerMedicRole);
        await AddMedicalWorkerToShift(driverId,shiftId,driverMedicRole);
        await AddMedicalWorkerToShift(crewMemberId,shiftId,crewMemberMedicRole);
      },
      onSuccess: () => {
        schedule.refetch()
      },
    })
    const addScheduleMutation = useMutation({
      mutationFn: async ({medicalTeamId,monthSchedule,yearSchedule}) => {
        await createSchedule(medicalTeamId,monthSchedule,yearSchedule);
      },
      onSuccess: async () => {
        await schedule.refetch();
      },
    })

    async function getSchedule()
    {
      await schedule.refetch();
      setIsClicked(false);
      //console.log(schedule.data)
      if(schedule.data === 204)
      {
        console.log("TAblica nie ma elementów");
        setIsScheduleCreated(false);
      }
      else{
        console.log(events)
        setIsScheduleCreated(true);
      }
    }
    const medicalTeams = useQuery({
        queryKey: ['GetDataMedicalTeamTable'],
        queryFn: async () => await getAllMedicalTeamsAssignedToCoordinator(userId),
        refetchOnWindowFocus:false
    })
    const schedule = useQuery({
      queryKey: ['GetScheduleByMonthAndYearAndMedicalTeamCodee'],
      queryFn: async () => await getScheduleByMonthAndYearAndMedicalTeamId(selectedMedicalTeamId,selectedMonthAndYearCalendar.getFullYear(),(Number(selectedMonthAndYearCalendar.getMonth())+1)),
      enabled:isClicked == true,
      retry:1,
      refetchOnWindowFocus:false
  })
  const medicalWorkersDrivers = useQuery({
    queryKey: ['GetMedcialDriversByMedcalTeamIdAndMedicRole'],
    queryFn: async () => await getMedcialWorkersByMedcalTeamIdAndMedicRole(selectedMedicalTeamId,'Driver',selectedshiftId),
    enabled:openShiftModal,
    retry:1,
    refetchOnWindowFocus:false
})
const medicalWorkersManagers = useQuery({
  queryKey: ['GetMedcialManagersByMedcalTeamIdAndMedicRole'],
  queryFn: async () => await getMedcialWorkersByMedcalTeamIdAndMedicRole(selectedMedicalTeamId,'Manager',selectedshiftId),
  enabled:openShiftModal,
  retry:1,
  refetchOnWindowFocus:false
})
const medicalWorkersCrewMembers = useQuery({
  queryKey: ['GetMedcialCrewMemberByMedcalTeamIdAndMedicRole'],
  queryFn: async () => await getMedcialWorkersByMedcalTeamIdAndMedicRole(selectedMedicalTeamId,'RegularMedic',selectedshiftId),
  enabled:openShiftModal,
  retry:1,
  refetchOnWindowFocus:false
})
    if(medicalTeams.isFetching 
      || schedule.isFetching || medicalWorkersCrewMembers.isFetching
      || medicalWorkersDrivers.isFetching || medicalWorkersManagers.isFetching)
    {
        return (Loading())
    }
    if(medicalTeams.error)
    {
        return (<div>Error</div>)
    }
  
    return (
      <div className="container">
      <div >
        Wybierz zespół:
        <select title="MedicalTeamSelector" defaultValue={selectedMedicalTeamId} onChange={(e) => setSelectedMedicalTeamId(e.target.value)} className="w-40 border border-gray-300 rounded ml-1 px-3 py-2">
          <option value=""></option>
          {
            medicalTeams.data.map((medicalTeam) => 
            <option value={medicalTeam.id}>
                {medicalTeam.informationAboutTeam.code} {medicalTeam.informationAboutTeam.city}
            </option>
            )
          }
        </select>
      <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold ml-2 py-2 px-4 rounded" onClick={()=>{getSchedule();setIsScheduleCreated(schedule.data);setIsClicked(true)}}>Wczytaj</button>
      {!schedule.data?.medicalTeam?.informationAboutTeam.code ? 
        (
          <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold ml-2 py-2 px-4 rounded" onClick={()=>addScheduleMutation.mutateAsync(

            {
              medicalTeamId:selectedMedicalTeamId,
              yearSchedule: selectedMonthAndYearCalendar.getFullYear(),
              monthSchedule:(Number(selectedMonthAndYearCalendar.getMonth())+1)
            }
            
            )}>Utwórz nowy grafik</button>
        ) : ''
        }
      <div className='overflow-hidden overflow-y-hidden'>
      
      <h1 className="flex items-center justify-center text-4xl font-bold">{schedule.data?.medicalTeam?.informationAboutTeam.code} {schedule.data?.medicalTeam?.informationAboutTeam.city}</h1>
        
      {openShiftModal == false?
      <FullCalendar
        plugins={[ dayGridPlugin,interactionPlugin,timeGridPlugin ]}
        aspectRatio={1.9}
        showNonCurrentDates={false}
        editable={false}
        locale={'pl'}
        timeZone='local'
        initialDate={selectedMonthAndYearCalendar}
        eventDisplay="block"
        datesSet={(arg) => {
            setSelectedMonthAndYearCalendar(arg.view.currentStart) 
          }}
        eventClick={(info) => handleClickEvent(info)}
        eventContent={(eventInfo) => { return { html: eventInfo.event.title + "<br> " + eventInfo.event.extendedProps.managerDisplayText
        + "<br> " + eventInfo.event.extendedProps.driverDisplayText + "<br> " + eventInfo.event.extendedProps.crewMemberDisplayText}}}
       events={
          schedule.data?.shifts?.map((shift) => 
        ({
          shiftId:shift.id,
          start:shift.dateRange.start,
          end:shift.dateRange.start,
          title: (new Date(shift.dateRange.start).getHours() == 7 && new Date(shift.dateRange.end).getHours() == 19 ? "Dyżur dzienny" : "Dyżur nocny"),
          managerDisplayText: 'Kierownik: ' + (shift.manager == undefined ? '' : shift.manager?.user?.name?.firstName + ' ' + shift.manager?.user?.name?.lastName),
          driverDisplayText: 'Kierowca: ' + (shift.driver == undefined ? '' : shift.driver?.user?.name?.firstName + ' ' + shift.driver?.user?.name?.lastName),
          crewMemberDisplayText: 'Członek: ' + (shift.crewMember == undefined ? '' : shift.crewMember?.user?.name?.firstName + ' ' + shift.crewMember?.user?.name?.lastName),
          managerId: shift.manager?.id,
          driverId: shift.driver?.id,
          crewMemberId: shift.crewMember?.id,
          managerName:  (shift.manager == undefined ? '' : shift.manager?.user?.name?.firstName + ' ' + shift.manager?.user?.name?.lastName),
          driverName: (shift.driver == undefined ? '' : shift.driver?.user?.name?.firstName + ' ' + shift.driver?.user?.name?.lastName),
          crewMemberName:  (shift.crewMember == undefined ? '' : shift.crewMember?.user?.name?.firstName + ' ' + shift.crewMember?.user?.name?.lastName),
        }))
       }
      />
      : ''}
      </div>
      </div>
      <Modal
      isOpen={openShiftModal}
      onRequestClose={() => {setOpenShiftModal(false);setSelectedDriverId('');setSelectedManagerId('');setSelectedCrewMemberId('')}}
      contentLabel="Add or Remove Items Modal"
      style={customStyles}
      >
      <h2 className="text-lg font-bold mb-4">Edytuj dyżur</h2>
        Wybierz kierownika:
        <select defaultValue={selectedShiftData.managerId} onChange={(e)=>setSelectedManagerId(e.target.value)} className="border rounded-md px-2 py-1 mb-4 w-full"> 
          (
            <option value="none" selected disabled hidden>
              {selectedShiftData.managerDisplayText}
            </option>
            {medicalWorkersManagers.data?.map((manager) => (
            <option value={manager.id}>
            {manager.user.name.firstName} {manager.user.name.lastName}
          </option>
            ))}
          )
        </select>
        Wybierz kierowce:
        <select defaultValue={selectedShiftData.driverId} onChange={(e)=>setSelectedDriverId(e.target.value)} className="border rounded-md px-2 py-1 mb-4 w-full"> 
          (
            <option value="none" selected disabled hidden>
              {selectedShiftData.driverDisplayText}
            </option>
            {medicalWorkersDrivers.data?.map((driver) => (
            <option value={driver.id}>
            {driver.user.name.firstName} {driver.user.name.lastName}
          </option>
            ))}
          )
        </select>

        Wybierz członka:
        <select defaultValue={selectedShiftData.crewMemberId} onChange={(e)=>setSelectedCrewMemberId(e.target.value)} className="border rounded-md px-2 py-1 mb-4 w-full"> 
        (
            <option value={selectedShiftData.crewMemberId} selected disabled hidden>
              {selectedShiftData.crewMemberDisplayText}
            </option>
            {medicalWorkersCrewMembers.data?.map((crewMember) => (
            <option value={crewMember.id}>
            {crewMember.user.name.firstName} {crewMember.user.name.lastName}
          </option>
            ))}
          )
          
        </select>
  
      <div className="flex justify-end">
        <button onClick={
          ()=>{
            addMedicalWorkerToShift.mutate({managerId:selectedManagerId,shiftId:selectedshiftId,managerMedicRole:'Manager',driverId:selectedDriverId,driverMedicRole:'Driver',crewMemberId:selectedCrewMemberId,crewMemberMedicRole:'RegularMedic'});
            setOpenShiftModal(false)
          }} 
      className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 mr-2 rounded">
          Dodaj
        </button>
      
        <button onClick={()=>setOpenShiftModal(false)} className="bg-gray-300 hover:bg-gray-400 text-gray-800 font-bold py-2 px-4 rounded">
          Anuluj
        </button>
      </div>
    </Modal> 
      </div>
    )
}
export default ScheduleCoordinator;