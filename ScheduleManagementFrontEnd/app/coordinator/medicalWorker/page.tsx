import { getUserById } from '@/requests/POST/loginRequest';
import { useState } from 'react';
import { cookies } from 'next/headers'
import { jwtDecode } from 'jwt-decode';
import { List } from 'postcss/lib/list';
import { NextRequest, NextResponse } from 'next/server';
import Link from 'next/link';
import { getAllMedicalTeamsAssignedToCoordinator } from '@/requests/GET/getAllMedicalTeamsAssignedToCoordinator';
import MedicalTeamsTable from '@/components/medicalWorkersTable';
import { MedicalTeam } from '@/models/medicalTeam';
import { useQuery } from '@tanstack/react-query';
import { getAllMedicalWorkersAssignedToCoordinator } from '@/requests/GET/getAllMedicalWorkersAssignedToCoordinator';
import { getCoordinatorByUserId } from '@/requests/GET/getCoordinatorByUserId';
import MedicalWorkersTable from '@/components/medicalWorkersTable';
import { getAllUnassignedUsersToSelectedRole } from '@/requests/GET/GetAllUnassignedUsersToSelectedRole';


export default async function Page() {
  
  const cookieStore = cookies()

  const token = cookieStore.get('token') ?? '';

  const decoded = jwtDecode(token.value);

  const coordinator = await getCoordinatorByUserId(decoded.UserID)
  
  return (
    <MedicalWorkersTable coordinatorId={coordinator.id} userId={decoded.UserID}></MedicalWorkersTable>
  );
}