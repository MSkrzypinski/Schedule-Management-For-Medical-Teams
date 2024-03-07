'use client'
import {FormEvent, Suspense, useState } from 'react';
import LoginPanel from '../components/loginComponent';
import { QueryClient, QueryClientProvider, useMutation, useQuery } from '@tanstack/react-query';
import { getToken, getUserById } from '@/requests/POST/loginRequest';
import { useRouter } from 'next/navigation';
import Cookies from 'js-cookie';
import { authorization, getActualToken, getUserIdFromCookies, roleRedirect } from './lib/utils';

process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0';

export default function Page() {
/*
  const router = useRouter();
  if(getActualToken() == null)
  {
    router.push('/login');  
  }
  else
  {
    const userId = getUserIdFromCookies();
    const user= getUserById(userId.toString());
    router.push('/coordinator');  
    //roleRedirect(user.userRoles)
  }*/
  return (
    <div></div>
  );
};




