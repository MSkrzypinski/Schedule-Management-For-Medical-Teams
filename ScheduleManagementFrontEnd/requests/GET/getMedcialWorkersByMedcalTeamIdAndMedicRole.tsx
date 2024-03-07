'use server'

import { cookies } from 'next/headers';
import { setCookie, deleteCookie, hasCookie, getCookie, getCookies } from 'cookies-next';


export async function getMedcialWorkersByMedcalTeamIdAndMedicRole(medicalTeamId: string,medicRole:string,shiftId:string)    
{
    const token = getCookie('token', { cookies });
    console.log(medicalTeamId,medicRole,shiftId)
    const res = await fetch(`${process.env.BACKEND_SERVER}/user/MedicalWorkers/GetMedicalWorkersAssignedToMedcialTeam?MedicalTeamId=${medicalTeamId}&MedicRole=${medicRole}&ShiftId=${shiftId}
    `, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      }
    })

    return res.json()
  }