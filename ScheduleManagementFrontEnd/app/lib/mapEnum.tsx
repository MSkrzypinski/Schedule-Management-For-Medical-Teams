'use client'

export const roleRedirect = (roles:string[]) =>
{
    if(roles.find(x=>x == "Coordinator"))
    {
        return "coordinator"
    }
    else if(roles.find(x=>x == "MedicalWorker"))
    {
        return "MedicalWorker"
    }
}
export function translateEmploymentContract(employmentContract) {
    switch (employmentContract) {
      case 2:
        return 'Umowa zlecenie'
   
      case 0:
        return 'B2B'
  
      case 1:
        return 'Umowa o prace'
    }
  }
  
 export function translateProfession(profession) {
    switch (profession) {
      case 1:
        return 'Lekarz'
   
      case 2:
        return 'Ratownik medyczny'

      case 0:
        return 'Pielęgniarz/-arka'

      case 3:
        return 'Ratownik'

    }
  }
 export function translateMedicRole(role) {
    switch (role) {
      case 0:
        return 'Kierowca'
   
      case 2:
        return 'Członek zespołu'

      case 1:
        return 'Kierownik zespołu'
    }
  }
 export function translateMedicalTeamType(role) {
    switch (role) {
      case 0:
        return 'P'
   
      case 1:
        return 'S'
      case 2:
        return 'N'
      case 3:
        return 'T'
    }
  }



  export function translateEmploymentContractToUpdate(employmentContract) {
    switch (employmentContract) {
      case 2:
        return 'ContractOfService'
   
      case 0:
        return 'BusinessToBusiness'
  
      case 1:
        return 'IndefiniteContract'
    }
  }
  
 export function translateProfessionToUpdate(profession) {
    switch (profession) {
      case 1:
        return 'Doctor'
   
      case 2:
        return 'Paramedic'

      case 0:
        return 'Nurse'

      case 3:
        return 'Paramedic'

    }
  }
 export function translateMedicRoleToUpdate(role) {
    switch (role) {
      case 0:
        return 'Driver'
   
      case 2:
        return 'RegularMedic'

      case 1:
        return 'Manager'
    }
  }
 export function translateMedicalTeamTypeToUpdate(role) {
    switch (role) {
      case 0:
        return 'P'
      case 1:
        return 'S'
      case 2:
        return 'N'
      case 3:
        return 'T'
    }
  }