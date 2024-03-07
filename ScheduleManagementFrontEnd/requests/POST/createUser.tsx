'use server'

import { cookies } from 'next/headers';
import { setCookie, deleteCookie, hasCookie, getCookie, getCookies } from 'cookies-next';

export async function createUser(firstName:string,lastName:string,password:string,phoneNumber:number,email:string)    
{
    const token = getCookie('token', { cookies });

    const res = await fetch(`${process.env.BACKEND_SERVER}/api/User/Register`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      },
      body: JSON.stringify({
        "name": {
            "firstName": firstName,
            "lastName": lastName
          },
          "password": {
            "value": password
          },
          "phoneNumber": {
            "number": phoneNumber
          },
          "email": {
            "value": email
          }
      }),
    })
    
    return res.json()
  }