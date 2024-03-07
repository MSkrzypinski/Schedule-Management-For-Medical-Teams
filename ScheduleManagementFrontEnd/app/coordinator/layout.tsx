'use client'
import Link from "next/link"
import { deleteCookie,} from 'cookies-next';

export default function CoordinatorLayout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <div className='flex w-screen h-screen'>
    <div className="bg-gray-800 text-white w-1/6 py-6">
      <h2 className="block w-full text-center px-4 pb-5 font-bold ">Panel koordynatora</h2>
      <Link href="/coordinator/medicalTeam" className="block w-full text-left px-4 py-8 hover:bg-gray-700 focus:bg-blue-800 focus:outline-none focus-visible:ring-2 focus-visible:ring-blue-500 ">Zespoły medyczne</Link>
      <Link href="/coordinator/medicalWorker" className="block w-full text-left px-4 py-8 hover:bg-gray-700 focus:bg-blue-800 focus:outline-none focus-visible:ring-2 focus-visible:ring-blue-500">Pracownicy</Link>
      <Link href="/coordinator/employmentContract" className="block w-full text-left px-4 py-8 hover:bg-gray-700 focus:bg-blue-800 focus:outline-none focus-visible:ring-2 focus-visible:ring-blue-500">Lista umów</Link>
      <Link href="/coordinator/schedule" className="block w-full text-left px-4 py-8 hover:bg-gray-700 focus:bg-blue-800 focus:outline-none focus-visible:ring-2 focus-visible:ring-blue-500">Grafik</Link>
      <Link href="/coordinator/createUser" className="block w-full text-left px-4 py-8 hover:bg-gray-700 focus:bg-blue-800 focus:outline-none focus-visible:ring-2 focus-visible:ring-blue-500">Stwórz nowego użytkownika</Link>
      <Link href="/coordinator/changeToMedicalWorker" className="block w-full text-left px-4 py-8 hover:bg-gray-700 focus:bg-blue-800 focus:outline-none focus-visible:ring-2 focus-visible:ring-blue-500">Przełącz na panel pracownika</Link>
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