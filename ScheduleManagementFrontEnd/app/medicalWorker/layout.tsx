'use client'
import { getUserById } from '@/requests/POST/loginRequest';
import { cookies } from 'next/headers'
import { jwtDecode } from 'jwt-decode';
import { deleteCookie } from 'cookies-next';
import Link from 'next/link';

export default function MedicalWorkerLayout({
    children,
  }: {
    children: React.ReactNode
  }) {

    return (
        <div className='flex w-screen h-screen'>
        <div className="bg-gray-800 text-white w-1/6 py-6">
          <h2 className="block w-full text-center px-4 pb-5 font-bold ">Panel pracownika medycznego</h2>
          <Link href="/medicalWorker/schedule" className="block w-full text-left px-4 py-8 hover:bg-gray-700 focus:bg-blue-800 focus:outline-none focus-visible:ring-2 focus-visible:ring-blue-500">Grafik</Link>
          <Link href="/coordinator/changeToCoordinator" className="block w-full text-left px-4 py-8 hover:bg-gray-700">Przełącz na panel koordynatora</Link>
          <Link href="/login" onClick={()=>deleteCookie('token')} className="block w-full text-left px-4 py-8 hover:bg-gray-700">Wyloguj</Link>
        </div>
        <div className="w-5/6 p-6">
          <div className="overflow-x-auto">
            {children}
          </div>
        </div>
      </div>
      )
  }

  
  