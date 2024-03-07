'use server'

import { cookies } from 'next/headers';
import { setCookie, deleteCookie, hasCookie, getCookie, getCookies } from 'cookies-next';


export async function getCoordinatorByUserId(userId: string)    
{
    const token = getCookie('token', { cookies });

    const res = await fetch(`${process.env.BACKEND_SERVER}/Coordinator/user/${userId}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      }
    })

    return res.json()
  }