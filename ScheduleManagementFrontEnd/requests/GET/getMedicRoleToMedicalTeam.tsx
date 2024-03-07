'use server'

import { cookies } from 'next/headers';
import { setCookie, deleteCookie, hasCookie, getCookie, getCookies } from 'cookies-next';


export async function getMedicRoleToMedicalTeam(medicalTeamId: string,profession:string)    
{
    const token = getCookie('token', { cookies });

    const res = await fetch(`${process.env.BACKEND_SERVER}/user/MedicalWorkers/GetMedicRoleToMedicalTeam?MedicalTeamId=${medicalTeamId}&MedicalWorkerProfessionEnum=${profession}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      }
    })

    return res.json()
  }