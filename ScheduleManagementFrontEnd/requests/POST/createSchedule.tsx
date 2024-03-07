'use server'

import { cookies } from 'next/headers';
import { setCookie, deleteCookie, hasCookie, getCookie, getCookies } from 'cookies-next';

export async function createSchedule(medicalTeamId : string,monthSchedule:number,yearSchedule:number)    
{
    const token = getCookie('token', { cookies });

    const res = await fetch(`${process.env.BACKEND_SERVER}/user/Schedule/AddNewSchedule`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      },
      body: JSON.stringify({
        "medicalTeamId": medicalTeamId,
        "monthOfSchedule": monthSchedule,
        "yearOfSchedule": yearSchedule
      }),
    })
    
    return res.json()
  }