export async function updateMedicalTeam(id:string,coordinatorId:string,city:string,sizeOfTeam:number,medicalTeamType:string)    
{
    const res = await fetch(`${process.env.BACKEND_SERVER}/MedicalTeam/update`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        "id": id,
        "coordinatorId": coordinatorId,
        "city": city,
        "sizeOfTeam": sizeOfTeam,
        "medicalTeamType": medicalTeamType
      }),
    })
     
    return res.json()
  }
  