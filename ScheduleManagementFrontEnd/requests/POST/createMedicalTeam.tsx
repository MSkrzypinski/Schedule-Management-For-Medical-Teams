'use server'

import { cookies } from 'next/headers';
import { setCookie, deleteCookie, hasCookie, getCookie, getCookies } from 'cookies-next';
import { User } from '@/components/medicalWorkersTable';

export async function createMedicalTeam(coordinatorId:string,code:string,city:string,sizeOfTeam:number,medicalTeamType:string)    
{
    const token = getCookie('token', { cookies });

    const res = await fetch(`${process.env.BACKEND_SERVER}/MedicalTeam/create`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      },
      body: JSON.stringify({
        "coordinatorId": coordinatorId,
        "code": code,
        "city": city,
        "sizeOfTeam": sizeOfTeam,
        "medicalTeamType": medicalTeamType
      }),
    })
    
    return res.json()
  }