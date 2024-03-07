'use client'

import {FormEvent, Suspense, useState } from 'react';
import { QueryClient, QueryClientProvider, useMutation, useQuery } from '@tanstack/react-query';
import { getToken, getUserById } from '@/requests/POST/loginRequest';
import { useRouter } from 'next/navigation';
import Cookies from 'js-cookie';
import LoginPanel from '@/components/loginComponent';
import { jwtDecode } from 'jwt-decode';
import { getCookies, setCookie, deleteCookie, getCookie } from 'cookies-next';

export default function Page() {

  const [errorMessage,setErrorMessage] = useState("")
  const router = useRouter();  
  
  async function handleLogin(event: FormEvent<HTMLFormElement>) 
  {
    event.preventDefault()
    const formData = new FormData(event?.currentTarget)
    
    const email = formData.get('email')
    const password = formData.get('password')
    
    const res = await getToken(email,password)

    if(res.token == null)
    {
      setErrorMessage('Niepoprawne dane logowania');
      return;
    }
    
    const decoded = jwtDecode(res.token);

    const userIdFromToken = decoded.UserID
    
    setCookie('token',  res.token);
    setCookie('userId', userIdFromToken);

    router.push('/coordinator');

    }
  
  return (
    <LoginPanel errorMessage={errorMessage} handleLogin={handleLogin}></LoginPanel>
  );
};




