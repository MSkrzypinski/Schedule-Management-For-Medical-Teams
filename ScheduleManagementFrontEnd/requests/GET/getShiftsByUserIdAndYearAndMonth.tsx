'use server'

import { cookies } from 'next/headers';
import { setCookie, deleteCookie, hasCookie, getCookie, getCookies } from 'cookies-next';


export async function getShiftsByUserIdAndYearAndMonth(userId : string,year:number,month:number)    
{
    const token = getCookie('token', { cookies });
    const res = await fetch(`${process.env.BACKEND_SERVER}/user/Shift/GetShiftsByMonthAndYearAndUserId?UserId=${userId}&Year=${year}&Month=${month}`, 
    {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      },
    })

    return res.json()
  }