
import React from 'react'
import { title } from 'process'
import { QueryClient, useQuery } from '@tanstack/react-query'
import { getAllMedicalTeamsAssignedToCoordinator } from '@/requests/GET/getAllMedicalTeamsAssignedToCoordinator'
import Loading from '@/app/loading'
import { cookies } from 'next/headers'
import { getCoordinatorByUserId } from '@/requests/GET/getCoordinatorByUserId'
import { jwtDecode } from 'jwt-decode'
import ScheduleMedicalWorker from '@/components/scheduleMedicalWorker'
import { getMedicalWorkerByUserId } from '@/requests/GET/getMedicalWorkerByUserId'
import { translateEmploymentContract, translateMedicRole, translateProfession } from '@/app/lib/mapEnum'
import EmploymentContractTableWorker from '@/components/employmentContractTableWorker'


export default async function Page() {

  const cookieStore = cookies()

  const token = cookieStore.get('token') ?? '';

  const decoded = jwtDecode(token.value);

  return(
    <EmploymentContractTableWorker userId={decoded.UserID}></EmploymentContractTableWorker>
  )
}

