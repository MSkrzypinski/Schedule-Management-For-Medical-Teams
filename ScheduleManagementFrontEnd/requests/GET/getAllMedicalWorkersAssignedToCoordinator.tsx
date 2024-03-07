'use server'

import { cookies } from 'next/headers';
import { setCookie, deleteCookie, hasCookie, getCookie, getCookies } from 'cookies-next';


export async function getAllMedicalWorkersAssignedToCoordinator(coordinatorId: string)    
{
    const token = getCookie('token', { cookies });

    const res = await fetch(`${process.env.BACKEND_SERVER}/user/MedicalWorkers/get/${coordinatorId}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      },
      cache:"no-cache"
    })
    return res.json()
  }