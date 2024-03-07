'use server'

import { cookies } from 'next/headers';
import { setCookie, deleteCookie, hasCookie, getCookie, getCookies } from 'cookies-next';

export async function AddMedicalWorkerProfession(medicalWorkerId:string,profession:string)    
{
    const token = getCookie('token', { cookies });
    const res = await fetch(`${process.env.BACKEND_SERVER}/user/MedicalWorkers/AddMedicalWorkerProfession`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      },
      body: JSON.stringify({
        "medicalWorkerId": medicalWorkerId,
        "medicalWorkerProfessionEnum": profession
      }),
    })

    return res.json();
  }