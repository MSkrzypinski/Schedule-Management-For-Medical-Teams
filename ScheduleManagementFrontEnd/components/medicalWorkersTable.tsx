'use client'

import { FormEvent, useMemo, useState } from 'react';
import Modal from 'react-modal';
import { createMedicalWorker } from '@/requests/POST/createMedicalWorker';
import { updateMedicalWorker } from '@/requests/PUT/updateMedicalWorker';
import { customStyles } from '@/styles/modalStyle';
import { AddMedicalWorkerProfession } from '@/requests/POST/addMedicalWorkerProfession';
import { DeleteMedicalWorkerProfession } from '@/requests/PUT/deleteMedicalWorkerProfession';
import { getMedicRoleToMedicalTeam } from '@/requests/GET/getMedicRoleToMedicalTeam';
import { addEmploymentContract } from '@/requests/POST/addEmploymentContract';
import Loading from '@/app/loading';
import { getAllMedicalWorkersAssignedToCoordinator } from '@/requests/GET/getAllMedicalWorkersAssignedToCoordinator';
import { useMutation, useQuery } from '@tanstack/react-query';
import { getAllMedicalTeamsAssignedToCoordinator } from '@/requests/GET/getAllMedicalTeamsAssignedToCoordinator';
import { translateMedicRole, translateProfession } from '@/app/lib/mapEnum';
import { getAllUnassignedUsersToSelectedRole } from '@/requests/GET/GetAllUnassignedUsersToSelectedRole';


const MedicalWorkersTable = ({coordinatorId,userId}) => 
{

  const unassignedUsers = useQuery({
    queryKey: ['GetUnassignedUsersForMedicalWorkerTable'],
    queryFn: async () => await getAllUnassignedUsersToSelectedRole("MedicalWorker"),
  })

  const medicalTeams = useQuery({
    queryKey: ['GetDataMedicalTeamForMedicalWorkerTable'],
    queryFn: async () => await getAllMedicalTeamsAssignedToCoordinator(userId),
  })
  
  const medicalWorkers = useQuery({
    queryKey: ['GetDataMedicalWorkerTable'],
    queryFn: async () => await getAllMedicalWorkersAssignedToCoordinator(coordinatorId),
  })

  const handleDelete = useMutation({
    mutationFn: async ({item}) => {
      await DeleteMedicalWorkerProfession(data.id,item.medicalWorkerProfessionEnum)
    },
    onSuccess: () => {
      medicalWorkers.refetch()
    },
  })
  const handleAdd = useMutation({
    mutationFn: async () => {
      await AddMedicalWorkerProfession(data.id,selectedProfession);
    },
    onSuccess: () => {
      medicalWorkers.refetch()
    },
  })


const [data, setData] = useState([]);
const [isNew, setIsNew] = useState(false);
const [code, setCode] = useState('');
const [city, setCity] = useState('');
const [selectedProfession, setSelectedProfession] = useState('');
const [sizeOfTeam, setSizeOfTeam] = useState(Number);
const [medicalTeamType, setMedicalTeamType] = useState('');
const [openProfessionModal, setOpenProfessionModal] = useState(false);
const [openEmploymentContractModal,setOpenEmploymentContractModal] = useState(false);
const [selectedMedicalTeam,setSelectedMedicalTeam] = useState([])
const [medicRoles,setMedicRoles] = useState([])
const [selectedRole,setSelectedRole] = useState('')
const [selectedContractType,setSelectedContractType] = useState('')
const [modalIsOpen, setIsOpen] = useState(false);
    

  async function handleAddEmploymentContract() {

    await addEmploymentContract(data.id,selectedMedicalTeam,selectedRole,selectedContractType,selectedProfession)
  }
  function openModal(medicalWorker) {
    setIsNew(false);
    setData(medicalWorker);
    setIsOpen(true);
  }
  async function getMedicRolesByProfessionAndMedicalTeam(medicalTeamId,profession)
  {
    console.log(medicalTeamId,profession)
    setMedicRoles(await getMedicRoleToMedicalTeam(medicalTeamId,profession))
    console.log(medicRoles)
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
    
    const city = formData.get('city')
    const userId = formData.get('user')
    const street = formData.get('street')
    const houseNumber = formData.get('houseNumber')
    const apartmentNumber = formData.get('apartmentNumber')
    const dateOfBirth = formData.get('dateOfBirth')
    const zipCode = formData.get('zipCode')

    if(isNew)
    {
      await createMedicalWorker(userId,city,zipCode,street,Number(houseNumber),Number(apartmentNumber),dateOfBirth)  
      //await AddMedicalWorkerProfession(medicalWorkerId,selectedProfession);  
    }
    else
    {
      await updateMedicalWorker(data.id,city,zipCode,street,Number(houseNumber),Number(apartmentNumber),dateOfBirth);
    }

    alert("Zapisano!!!")
    setIsOpen(false)
  }

  if(medicalWorkers.isFetching || medicalTeams.isFetching  || unassignedUsers.isFetching )
  {
    return (Loading())
  }


  return(
    <div className="container">
      <button onClick={addMedicalTeam} className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 mr-2 mb-4 rounded">
        Dodaj nowego pracownika medycznego
      </button>
      <table className="min-w-full bg-white border border-gray-200">
          <thead className="bg-gray-100">
          <tr>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Imię i nazwisko</th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Miasto</th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Kod pocztowy</th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Ulica i numer domu</th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Data urodzenia</th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Zawody</th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Status</th>

          </tr>
          </thead>
          <tbody className="divide-y divide-gray-200">
          {medicalWorkers.data.map((medicalWorker) => (
            
          <tr key={medicalWorker.id}>
              <td className="px-6 py-4 whitespace-nowrap">
              {medicalWorker.user.name.firstName} {medicalWorker.user.name.lastName}
              </td>

             <td className="px-6 py-4 whitespace-nowrap">
               {medicalWorker.address.city}
             </td>
             <td className="px-6 py-4 whitespace-nowrap">
               {medicalWorker.address.zipCode}
             </td>
             <td className="px-6 py-4 whitespace-nowrap">
               {medicalWorker.address.street} {medicalWorker.address.houseNumber} {(medicalWorker.address.apartmentNumber == '') ? '' : (`/ ${medicalWorker.address.apartmentNumber}`) }
             </td>
             <td suppressHydrationWarning={true} className="px-6 py-4 whitespace-nowrap">
              {medicalWorker.dateOfBirth.split('T')[0]}
             </td>
             <td suppressHydrationWarning={true} className="px-6 py-4 whitespace-nowrap">
              {medicalWorker.medicalWorkerProfessions.map(profession => translateProfession(profession.medicalWorkerProfessionEnum)).join(', ')}
             </td>
             <td className="px-6 py-4 whitespace-nowrap">
               {medicalWorker.isActive == true ? 'Aktywowany' : 'Dezaktywowany'}
             </td>
              <td className="whitespace-nowrap">
              <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 ml-4 rounded" onClick={() => openModal(medicalWorker)}>Edytuj</button>
              <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 ml-4 rounded" onClick={()=> {setOpenProfessionModal(true); setData(medicalWorker)}}>Dodaj/Usuń zawód</button>
              <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 ml-4 rounded" onClick={()=> {setSelectedMedicalTeam([]);setSelectedProfession('');setOpenEmploymentContractModal(true); setData(medicalWorker)}}>Dodaj umowę</button>
              {medicalWorker.isActive == true ? (
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
            <h2 className="text-lg font-bold mb-4">Pracownik</h2>
          <form onSubmit={saveModal} className="space-y-4">
            {isNew == true ? (
            <label className="block mb-4">
              Użytkownik:
              <select name='user' id="user" className="block w-full border border-gray-300 rounded px-3 py-2">
                (
                  {unassignedUsers.data?.map((user) => (
                  <option value={user.id}>{user.name.firstName} {user.name.lastName} ({user.email.value})</option>
                  ))}
                )
              </select>
            </label>
            ) : '' }
            <label className="block mb-4">
              Miasto:
              <input type="text" name='city' id="city" defaultValue={data.address?.city} className="block w-full border border-gray-300 rounded px-3 py-2" />
            </label>
            <label className="block mb-4">
              Kod pocztowy:
              <input type="text" name='zipCode' id="zipCode" defaultValue={data.address?.zipCode} className="block w-full border border-gray-300 rounded px-3 py-2" />
            </label>
            <label className="block mb-4">
              Ulica:
              <input type="text" name='street' id="street" defaultValue={data.address?.street} className="block w-full border border-gray-300 rounded px-3 py-2" />
            </label>
            <label className="block mb-4">
              Numer domu:
              <input type="number" name='houseNumber' id="houseNumber" defaultValue={data.address?.houseNumber} className="block w-full border border-gray-300 rounded px-3 py-2" />
            </label>
            <label className="block mb-4">
              Numer mieszkania:
              <input type="number" name='apartmentNumber' id="apartmentNumber" defaultValue={data.address?.apartmentNumber} className="block w-full border border-gray-300 rounded px-3 py-2" />
            </label>
            <label className="block mb-4">
               Data urodzenia:
              <input type="date" name='dateOfBirth' id="dateOfBirth" defaultValue={data.dateOfBirth?.split('T')[0]} className="block w-full border border-gray-300 rounded px-3 py-2" />
            </label>
           
            <div className="flex justify-end">
              <button type='submit' className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 mr-2 rounded">
                {isNew ? 'Dodaj':'Edytuj'}
              </button>
              <button onClick={closeModal} className="bg-gray-300 hover:bg-gray-400 text-gray-800 font-bold py-2 px-4 rounded">
                Anuluj
              </button>
            </div>
          </form>
        </Modal>
        <Modal
      isOpen={openProfessionModal}
      onRequestClose={() => setOpenProfessionModal(false)}
      contentLabel="Dodaj lub usuń zawód"
      style={customStyles}
    >
      <h2 className="text-lg font-bold mb-4">Dodaj lub usuń zawód</h2>
      <div className="mb-4 border rounded-md p-2 bg-gray-100">
        <select onChange={(e) => setSelectedProfession(e.target.value)} className="border rounded-md px-2 py-1 mb-4 w-full"> 
          <option value=""></option>
          <option value="Nurse">Pielęgniarz/-arka</option>
          <option value="Doctor">Lekarz</option>
          <option value="Paramedic">Ratownik medyczny</option>
          <option value="BasicMedic">Ratownik</option>
        </select>
        <button className="ml-2 bg-blue-500 hover:bg-red-700 text-white font-bold py-1 px-2 rounded" onClick={()=>handleAdd.mutate()}>Dodaj</button>
      </div>
      <div className="mb-4 border rounded-md p-2 bg-gray-100">
        <ul className="list-disc pl-5 mt-4">
          {data.medicalWorkerProfessions?.map((item) => (
            <li className="flex items-center">
              {translateProfession(item.medicalWorkerProfessionEnum)}
              <button className="ml-2 bg-red-500 hover:bg-red-700 text-white font-bold py-1 px-2 rounded" 
                onClick={()=>{handleDelete.mutate({item:item})}}>
                Usuń
              </button>
            </li>
          ))}
        </ul>
      </div>
      <div className="flex justify-end">
        <button onClick={()=>setOpenProfessionModal(false)} className="bg-gray-300 hover:bg-gray-400 text-gray-800 font-bold py-2 px-4 rounded">
          Cancel
        </button>
      </div>
    </Modal> 
    <Modal
      isOpen={openEmploymentContractModal}
      onRequestClose={() => setOpenEmploymentContractModal(false)}
      contentLabel="Add or Remove Items Modal"
      style={customStyles}
    >
      <h2 className="text-lg font-bold mb-4">Dodaj umowę</h2>
        Wybierz zespół medyczny:
        <select onChange={(e) => {setSelectedMedicalTeam(e.target.value);selectedMedicalTeam.length != 0 && selectedProfession != '' ? getMedicRolesByProfessionAndMedicalTeam(e.target.value,selectedProfession) : []}} className="border rounded-md px-2 py-1 mb-4 w-full"> 
          <option value=""></option>
          (
            {medicalTeams.data.map((medicalTeam) => (
            <option value={medicalTeam.id}>{medicalTeam.informationAboutTeam.code}</option>
            ))}
          )
        </select>
        {selectedMedicalTeam.length != 0 ? (     
        <>
        Wybierz wykonywany zawód:
        <select onChange={(e) => {setSelectedProfession(e.target.value);getMedicRolesByProfessionAndMedicalTeam(selectedMedicalTeam,e.target.value)}} className="border rounded-md px-2 py-1 mb-4 w-full"> 
          <option value=""></option>
          {data.medicalWorkerProfessions?.map((item) => (
            <option value={item.medicalWorkerProfessionEnum}>{translateProfession(item.medicalWorkerProfessionEnum)}</option>
          ))}
        </select>
        </>
        ) : ''}
        { selectedProfession != '' ? (
        <>
        Wybierz wykonywaną role:
        <select onChange={(e) => setSelectedRole(e.target.value)} className="border rounded-md px-2 py-1 mb-4 w-full"> 
          <option value=""></option>
          {medicRoles?.map((medicRole) => (
            <option value={medicRole.medicRole}>{translateMedicRole(medicRole.medicRole)}</option>
          ))}
        </select>
        </>
        ) : ''}
        { selectedRole != '' && selectedProfession != '' ? (
        <>
        Wybierz rodzaj umowy:
        <select onChange={(e) => setSelectedContractType(e.target.value)} className="border rounded-md px-2 py-1 mb-4 w-full"> 
          <option value=""></option>
          <option value='BusinessToBusiness'>B2B</option>
          <option value='IndefiniteContract'>Umowa o pracę</option>
          <option value='ContractOfService'>Umowa zlecenie</option>
        </select>
        </>
        ) : ''}
      <div className="flex justify-end">
        { selectedProfession != '' && selectedMedicalTeam.length != 0 && selectedRole.length != 0 && selectedContractType != '' ? (
        <button type='submit' onClick={handleAddEmploymentContract} className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 mr-2 rounded">
          Dodaj
        </button>
        ): ''
        }
        <button onClick={()=>setOpenEmploymentContractModal(false)} className="bg-gray-300 hover:bg-gray-400 text-gray-800 font-bold py-2 px-4 rounded">
          Anuluj
        </button>
      </div>
    </Modal>         
    </div>
    </div>
    
  )

}
export default MedicalWorkersTable