'use server'

import { getCookie } from "cookies-next";
import { cookies } from "next/headers";

export async function AddDaysOffToMedicalWorker(medicalWorkerId:string,start:Date,end:Date)    
{
    const token = getCookie('token', { cookies });

    const res = await fetch(`${process.env.BACKEND_SERVER}/user/MedicalWorkers/AddDayOff`, {
      
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      },
      body: JSON.stringify({
        medicalWorkerId,
        start,
        end
      }),
     
    })
     console.log(JSON.stringify({
        medicalWorkerId,
        start,
        end
      }))
    return res.json()
  }
  