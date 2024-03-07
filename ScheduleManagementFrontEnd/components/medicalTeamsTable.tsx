'use client'

import { FormEvent, useMemo, useState } from 'react';
import { updateMedicalTeam } from '@/requests/PUT/updateMedialTeam';
import Modal from 'react-modal';
import { createMedicalTeam } from '@/requests/POST/createMedicalTeam';
import { getCoordinatorByUserId } from '@/requests/GET/getCoordinatorByUserId';
import { customStyles } from '@/styles/modalStyle';
import Loading from '@/app/loading';
import { getAllMedicalTeamsAssignedToCoordinator } from '@/requests/GET/getAllMedicalTeamsAssignedToCoordinator';
import { useQuery } from '@tanstack/react-query';
import { translateMedicalTeamType } from '@/app/lib/mapEnum';

const MedicalTeamsTable = ({userId}) => 
{

const [data, setData] = useState([]);
const [isNew, setIsNew] = useState(false);
const [code, setCode] = useState('');
const [city, setCity] = useState('');
const [sizeOfTeam, setSizeOfTeam] = useState(Number);
const [medicalTeamType, setMedicalTeamType] = useState('');

const [modalIsOpen, setIsOpen] = useState(false);



const medicalTeams = useQuery({
  queryKey: ['GetDataMedicalTeamTable'],
  queryFn: async () => await getAllMedicalTeamsAssignedToCoordinator(userId),
})
if(medicalTeams.isFetching || medicalTeams.isLoading)
{
  return (Loading())
}

  function openModal(medicalTeam) {
    setData(medicalTeam);
    setIsOpen(true);
  }
  function addMedicalTeam() {
    setData([]);
    setIsOpen(true);
    setIsNew(true);
  }
  function closeModal() {
    setIsOpen(false);
  }
  async function saveModal(event: FormEvent<HTMLFormElement>)
  {
    event.preventDefault()
    const formData = new FormData(event?.currentTarget)
    
    const code = formData.get('code')
    const city = formData.get('city')
    const sizeOfTeam = Number(formData.get('sizeOfTeam'))
    const medicalTeamType = formData.get('medicalTeamType')
    
    if(isNew)
    {
      const coordinator = await getCoordinatorByUserId(userId)
      await createMedicalTeam(coordinator.id,code,city,sizeOfTeam,medicalTeamType);
    }
    else
    {
      await updateMedicalTeam(data.id,data.coordinator.id,city,sizeOfTeam,medicalTeamType);
    }

    alert("Zapisano!!!")
    setIsOpen(false)
  }

console.log(medicalTeams.data)
  return(
    <div className='container'>
      <button onClick={addMedicalTeam} className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 mr-2 mb-4 rounded">
        Dodaj nowy zespół
      </button>
      <table className="min-w-full bg-white border border-gray-200">
          <thead className="bg-gray-100">
          <tr>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Kod zespołu</th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Miasto</th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Rozmiar zespołu</th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Typ zespołu</th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Status</th>
          </tr>
          </thead>
          <tbody className="divide-y divide-gray-200">
          {medicalTeams.data.map((medicalTeam) => (
            
          <tr key={medicalTeam.id}>
             
              <td className="px-6 py-4 whitespace-nowrap">
              {medicalTeam.informationAboutTeam.code}
              </td>

             <td className="px-6 py-4 whitespace-nowrap">
               {medicalTeam.informationAboutTeam.city}
             </td>
             <td className="px-6 py-4 whitespace-nowrap">
               {medicalTeam.informationAboutTeam.sizeOfTeam}
             </td>
             <td className="px-6 py-4 whitespace-nowrap">
              {translateMedicalTeamType(medicalTeam.informationAboutTeam.medicalTeamType)}
             </td>
             <td className="px-6 py-4 whitespace-nowrap">
              {medicalTeam.isActive == true ? 'Aktywowany':'Dezaktywowany'}
             </td>
              <td>
              <button className="bg-blue-500 justify-center hover:bg-blue-700 text-white font-bold py-2 px-4 ml-4 rounded" onClick={() => openModal(medicalTeam)}>Edytuj</button>
              {medicalTeam.isActive == true ? (
              <button className="bg-red-500 justify-center hover:bg-red-700 text-white font-bold py-2 px-4 ml-4 rounded">Dezaktywuj</button>
              ) : <button className="bg-blue-500 justify-center hover:bg-blue-700 text-white font-bold py-2 px-4 ml-4 rounded" >Aktywuj</button>}
              </td>            
          </tr>
          ))}
          </tbody>
      </table>
        <div className="text-center mt-8">
          <Modal
            isOpen={modalIsOpen}
            onRequestClose={() => setIsOpen(false)}
            style={customStyles}
            data={data}
            >
            <h2 className="text-lg font-bold mb-4">Zespół medyczny</h2>
          <form onSubmit={saveModal} className="space-y-4">
            <label className="block mb-4">
              Kod Zespołu:
              <input type="text" name='code' id="code" defaultValue={data.informationAboutTeam?.code} onChange={(e) => setCode(e.target.value)} className="block w-full border border-gray-300 rounded px-3 py-2" />
            </label>
            <label className="block mb-4">
              Miasto:
              <input type="text" name='city' id="city" defaultValue={data.informationAboutTeam?.city} className="block w-full border border-gray-300 rounded px-3 py-2" />
            </label>
            <label className="block mb-4">
              Rozmiar Zespołu:
              <input type="number" name='sizeOfTeam' id="sizeOfTeam" defaultValue={data.informationAboutTeam?.sizeOfTeam} onChange={(e) => setSizeOfTeam(parseInt(e.target.value))} className="block w-full border border-gray-300 rounded px-3 py-2" />
            </label>
            <label className="block mb-4">
              Typ Zespołu:
              <select defaultValue={data.informationAboutTeam?.medicalTeamType} name='medicalTeamType' id="medicalTeamType" onChange={(e) => setMedicalTeamType(e.target.value)} className="block w-full border border-gray-300 rounded px-3 py-2">
                <option value="S">S</option>
                <option value="P">P</option>
                <option value="N">N</option>
              </select>
            </label>
            <div className="flex justify-end">
              <button type='submit' className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 mr-2 rounded">
                Edytuj
              </button>
              <button onClick={closeModal} className="bg-gray-300 hover:bg-gray-400 text-gray-800 font-bold py-2 px-4 rounded">
                Anuluj
              </button>
            </div>
          </form>
        </Modal>
      </div>
    </div>
      
  )

    }
    export default MedicalTeamsTable