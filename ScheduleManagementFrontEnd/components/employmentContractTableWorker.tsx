'use client'
import { translateEmploymentContract, translateProfession, translateMedicRole } from "@/app/lib/mapEnum";
import Loading from "@/app/loading";
import { getMedicalWorkerByUserId } from "@/requests/GET/getMedicalWorkerByUserId";
import { useQuery } from "@tanstack/react-query";


const EmploymentContractTableWorker = ({userId}) =>{

const medicalWorker = useQuery({
    queryKey: ['GetDataEmploymentContractTable'],
    queryFn: async () => await getMedicalWorkerByUserId(userId),
  })


if(medicalWorker.isFetching)
{
return (Loading())
}
if(medicalWorker.isFetched)
{
return(
<div className='container overflow-y-hidden overflow-x-hidden'>
  <table className="min-w-full bg-white border border-gray-200">
      <thead className="bg-gray-100">
      <tr>
          <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Imię i nazwisko pracownika</th>
          <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Adres email</th>
          <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Zespół medyczny</th>
          <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Rodzaj umowy</th>
          <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Wykonywany zawód</th>
          <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Wykonywana rola</th>
      </tr>
      </thead>
      <tbody className="divide-y divide-gray-200">
      {medicalWorker.employmentContracts?.map((employmentContract) => (
      <tr> 
        <td className="px-6 py-4 whitespace-nowrap">
        {medicalWorker.user.name.firstName} {medicalWorker.user.name.lastName}
        </td>
        <td className="px-6 py-4 whitespace-nowrap">
          {medicalWorker.user.email.value}
        </td>
        <td className="px-6 py-4 whitespace-nowrap">
          {employmentContract.medicalTeam.informationAboutTeam.code}
        </td>
        <td className="px-6 py-4 whitespace-nowrap">
          {translateEmploymentContract(employmentContract.contractType)}
        </td>
        <td className="px-6 py-4 whitespace-nowrap">
          {translateProfession(employmentContract.medicalWorkerProfession)}
        </td>
        <td className="px-6 py-4 whitespace-nowrap">
          {translateMedicRole(employmentContract.medicRole)}
        </td>
         <td>
         <button className="bg-red-500 justify-center hover:bg-red-700 text-white font-bold py-2 px-4 rounded" onClick={() => mutation.mutate({medicalWorkerId:medicalWorker.id,employmentContract:employmentContract})}>Usuń</button>
        </td>                        
      </tr>
      ))}
      </tbody>
  </table>
  
</div>
  
)
}
    }
export default EmploymentContractTableWorker;
