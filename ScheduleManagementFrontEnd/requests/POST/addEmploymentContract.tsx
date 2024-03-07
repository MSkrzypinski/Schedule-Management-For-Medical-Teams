'use server'

import { cookies } from 'next/headers';
import { setCookie, deleteCookie, hasCookie, getCookie, getCookies } from 'cookies-next';

export async function addEmploymentContract(medicalWorkerId:string,medicalTeamId:string,medicRole :string,contractType:string,medicalWorkerProfession:string)    
{
    const token = getCookie('token', { cookies });
    const res = await fetch(`${process.env.BACKEND_SERVER}/user/MedicalWorkers/CreateNewEmploymentContract`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      },
      body: JSON.stringify({
        "medicalWorkerId": medicalWorkerId,
        "medicalTeamId": medicalTeamId,
        "medicRole": medicRole,
        "contractType": contractType,
        "medicalWorkerProfession": medicalWorkerProfession
      }),
    })
    console.log(JSON.stringify({
      "medicalWorkerId": medicalWorkerId,
      "medicalTeamId": medicalTeamId,
      "medicRole": medicRole,
      "contractType": contractType,
      "medicalWorkerProfession": medicalWorkerProfession
    }))
    return res.json();
  }