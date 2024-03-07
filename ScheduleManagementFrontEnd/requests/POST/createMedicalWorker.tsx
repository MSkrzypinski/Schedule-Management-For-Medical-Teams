'use server'

import { cookies } from 'next/headers';
import { setCookie, deleteCookie, hasCookie, getCookie, getCookies } from 'cookies-next';

export async function createMedicalWorker(userId:string,city:string,zipCode:string,street:string,houseNumber:number,apartmentNumber:number,dateOfBirth:Date)    
{
    const token = getCookie('token', { cookies });

    const res = await fetch(`${process.env.BACKEND_SERVER}/user/MedicalWorkers`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      },
      body: JSON.stringify({
        "address": {
            "city": city,
            "zipCode": zipCode,
            "street": street,
            "houseNumber": houseNumber,
            "apartmentNumber": apartmentNumber
          },
          "dateOfBirth": dateOfBirth,
          "userId": userId
      }),
    })
    
    return res.json()
  }