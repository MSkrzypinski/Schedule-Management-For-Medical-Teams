'use server'

import { cookies } from 'next/headers';
import { setCookie, deleteCookie, hasCookie, getCookie, getCookies } from 'cookies-next';
import { error } from 'console';

export async function getScheduleByMonthAndYearAndMedicalTeamId(medicalTeamId: string,year:number,month:number)    
{
    const token = getCookie('token', { cookies });

    const res = await fetch(`${process.env.BACKEND_SERVER}/user/Schedule/GetScheduleByMonthAndYearAndMedicalTeamId?MedicalTeamId=${medicalTeamId}&Year=${year}&Month=${month}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      },
      //cache:'force-cache'
    })
    if(res.status == 204)
    {
      return 204;
    }
    return res.json();

  }