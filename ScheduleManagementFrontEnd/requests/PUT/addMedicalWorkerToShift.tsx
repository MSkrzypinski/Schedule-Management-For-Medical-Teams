'use server'

import { getCookie } from "cookies-next";
import { cookies } from "next/headers";

export async function AddMedicalWorkerToShift(medicalWorkerId:string,shiftId:string,medicRole:string)    
{
    const token = getCookie('token', { cookies });

    const res = await fetch(`${process.env.BACKEND_SERVER}/user/Shift/AddMedicalWorkerToShift`, {
      
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      },
      body: JSON.stringify({
        medicalWorkerId,
        shiftId,
        medicRole
      }),
     
    })
     
    return res.json()
  }
  