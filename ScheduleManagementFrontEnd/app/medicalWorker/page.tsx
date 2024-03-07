import { getUserById } from '@/requests/POST/loginRequest';
import { useState } from 'react';
import { cookies } from 'next/headers'
import { jwtDecode } from 'jwt-decode';
import { List } from 'postcss/lib/list';
import { NextRequest, NextResponse } from 'next/server';
import { getAllMedicalTeamsAssignedToCoordinator } from '@/requests/GET/getAllMedicalTeamsAssignedToCoordinator';


export default async function Page() {


  }

  
  