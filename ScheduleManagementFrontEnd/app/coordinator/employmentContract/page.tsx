import { cookies } from 'next/headers'
import { jwtDecode } from 'jwt-decode';
import { getAllMedicalWorkersAssignedToCoordinator } from '@/requests/GET/getAllMedicalWorkersAssignedToCoordinator';
import { getCoordinatorByUserId } from '@/requests/GET/getCoordinatorByUserId';
import EmploymentContractTable from '@/components/employmentContractTable';
import useSWR from 'swr';
import React, { useState } from 'react'


export default async function Page() {
  
  const cookieStore = cookies()

  const token = cookieStore.get('token') ?? '';
 
  const decoded = jwtDecode(token.value);

  const coordinator = await getCoordinatorByUserId(decoded.UserID)

  //const medicalWorkers = await getAllMedicalWorkersAssignedToCoordinator(coordinator.id) 

  return (
    <EmploymentContractTable coordinatorId={coordinator.id}></EmploymentContractTable>
  );
}