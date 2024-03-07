import { cookies } from 'next/headers'
import { jwtDecode } from 'jwt-decode';
import { getAllMedicalTeamsAssignedToCoordinator } from '@/requests/GET/getAllMedicalTeamsAssignedToCoordinator';
import MedicalTeamsTable from '@/components/medicalTeamsTable';


export default async function Page() {
  
  const cookieStore = cookies()

  const token = cookieStore.get('token') ?? '';

  const decoded = jwtDecode(token.value);

  return (
    <MedicalTeamsTable userId={decoded.UserID}></MedicalTeamsTable>
  );
}



