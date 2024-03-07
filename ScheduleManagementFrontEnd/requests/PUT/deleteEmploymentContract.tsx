
'use server'

import { cookies } from 'next/headers';
import { setCookie, deleteCookie, hasCookie, getCookie, getCookies } from 'cookies-next';

export async function DeleteEmploymentContract(medicalWorkerId: string,medicalTeamId :string,contractType:string,medicalWorkerProfession:string,medicRole:string)    
{
    const token = getCookie('token', { cookies });
    
    const res = await fetch(`${process.env.BACKEND_SERVER}/user/MedicalWorkers/delete/employmentContract`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      },
      body: JSON.stringify({
        medicalWorkerId,
        medicalTeamId,
        contractType,
        medicalWorkerProfession,
        medicRole
      }),
    })
    console.log(JSON.stringify({
      medicalWorkerId,
      medicalTeamId,
      contractType,
      medicalWorkerProfession,
      medicRole
    }))
    return res.json()
  }