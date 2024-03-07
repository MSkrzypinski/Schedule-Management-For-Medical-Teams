'use server'
import { getCookie } from "cookies-next";
import { cookies } from "next/headers";

export async function updateMedicalWorker(MedicalWorkerId:string,city:string,zipCode:string,street:string,houseNumber:number,apartmentNumber:number,dateOfBirth:Date)    
{
    const token = getCookie('token', { cookies });

    const res = await fetch(`${process.env.BACKEND_SERVER}/user/MedicalWorkers/update`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      },
      body: JSON.stringify({
        "id": MedicalWorkerId,
        "city": city,
        "zipCode": zipCode,
        "street": street,
        "houseNumber": houseNumber,
        "apartmentNumber": apartmentNumber,
        "dateOfBirth": dateOfBirth
      }),
    })
     
    return res.json()
  }
  