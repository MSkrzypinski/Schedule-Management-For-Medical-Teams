import React from 'react'
import { title } from 'process'
import { QueryClient, useQuery } from '@tanstack/react-query'
import { getAllMedicalTeamsAssignedToCoordinator } from '@/requests/GET/getAllMedicalTeamsAssignedToCoordinator'
import Loading from '@/app/loading'
import { cookies } from 'next/headers'
import { getCoordinatorByUserId } from '@/requests/GET/getCoordinatorByUserId'
import { jwtDecode } from 'jwt-decode'
import ScheduleCoordinator from '@/components/scheduleCoordinator'


export default async function Page() {

  const cookieStore = cookies()

  const token = cookieStore.get('token') ?? '';

  const decoded = jwtDecode(token.value);

  const coordinator = await getCoordinatorByUserId(decoded.UserID)

  return(
    <ScheduleCoordinator userId={decoded.UserID}></ScheduleCoordinator>
  )
}