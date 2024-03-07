'use client'

import { createUser } from "@/requests/POST/createUser"
import { useMutation } from "@tanstack/react-query";
import { FormEvent, useState } from "react"

export default async function Page() {

    const mutation = useMutation({
        mutationFn: async ({firstName,lastName,password,phoneNumber,email}) => {
            return await createUser(firstName,lastName,password,phoneNumber,email);
        },
    })

    async function handleRegister(event: FormEvent<HTMLFormElement>) 
    {
        event.preventDefault()
        const formData = new FormData(event?.currentTarget)
        
        const firstName = formData.get('firstName')
        const lastName = formData.get('lastName')
        const password = formData.get('password')
        const confirmPassword = formData.get('confirmPassword')
        const phoneNumber = formData.get('phoneNumber')
        const email = formData.get('email')

        if (password !== confirmPassword) 
        {
            alert("Hasła muszą być takie same");
            return;
        }
 
        mutation.mutateAsync({firstName:firstName,lastName:lastName,password:password,phoneNumber:phoneNumber,email:email});
        
        alert(mutation.data?.message);

    }

    return (
        <div className="flex justify-center items-center h-screen bg-gray-100">
          <div className="bg-white p-8 rounded shadow-md w-90">
            <h2 className="text-2xl font-bold mb-6">Rejestracja nowego użytkownika</h2>
            <form onSubmit={handleRegister} className="max-w-md mx-auto">
                <div className="mb-4">
                    <label className="block mb-1">Imię:</label>
                    <input minLength={2} maxLength={20} title='firstName' type="text" id="firstName" name="firstName" className="w-full border-gray-300 rounded-md px-3 py-2" required />
                </div>
                <div className="mb-4">
                    <label className="block mb-1">Nazwisko:</label>
                    <input minLength={5} maxLength={20} title="lastName" type="text" id="lastName" name="lastName" className="w-full border-gray-300 rounded-md px-3 py-2" required />
                </div>
                <div className="mb-4">
                    <label className="block mb-1">Hasło:</label>
                    <input minLength={5} maxLength={20} title="password" type="password" id="password" name="password" className="w-full border-gray-300 rounded-md px-3 py-2" required />
                </div>
                <div className="mb-4">
                    <label className="block mb-1">Potwierdź hasło:</label>
                    <input minLength={5} maxLength={20} title="confirmPassword"  type="password" id="confirmPassword" name="confirmPassword" className="w-full border-gray-300 rounded-md px-3 py-2" required />
                </div>
                <div className="mb-4">
                    <label className="block mb-1">Numer telefonu:</label>
                    <input maxLength={9} minLength={9} title="phoneNumber" type="tel" id="phoneNumber" name="phoneNumber" className="w-full border-gray-300 rounded-md px-3 py-2" required />
                </div>
                <div className="mb-4">
                    <label className="block mb-1">Email:</label>
                    <input title="email" type="email" id="email" name="email" className="w-full border-gray-300 rounded-md px-3 py-2" required />
                </div>
                <button type="submit" className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">Rejestruj</button>
            </form>
          </div>
        </div>
      )
}
   

   
