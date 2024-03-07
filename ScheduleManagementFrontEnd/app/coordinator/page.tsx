import { getUserById } from '@/requests/POST/loginRequest';
import { useState } from 'react';
import { cookies } from 'next/headers'
import { jwtDecode } from 'jwt-decode';
import { List } from 'postcss/lib/list';
import { NextRequest, NextResponse } from 'next/server';
import Link from 'next/link';
import { getAllMedicalTeamsAssignedToCoordinator } from '@/requests/GET/getAllMedicalTeamsAssignedToCoordinator';
import MedicalTeamsTable from '@/components/medicalTeamsTable';
import { MedicalTeam } from '@/models/medicalTeam';
import { useQuery } from '@tanstack/react-query';
import MedicalWorkersTable from '@/components/medicalWorkersTable';


export default async function Page() {

   
}