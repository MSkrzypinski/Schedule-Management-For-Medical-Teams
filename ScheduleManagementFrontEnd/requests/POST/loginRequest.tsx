'use server'
export async function getToken(email:string | undefined,password:string | undefined)    
{
    const res = await fetch(`${process.env.BACKEND_SERVER}/api/User/Login`, {
      method: 'POST',
      cache: 'no-cache',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        "email": email,
        "password": password
      }),
    })
     
    return res.json()
  }
  
  export async function getUserById(id:string | undefined)    
{
    const res = await fetch(`${process.env.BACKEND_SERVER}/api/User/id/${id}`, {
      method: 'GET',
    })
     
    return res.json()
  }
