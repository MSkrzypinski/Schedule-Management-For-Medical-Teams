'use client'

import { getToken } from '@/requests/POST/loginRequest';
import { useRouter } from 'next/navigation';
import React, { FormEvent, FormEventHandler, useState } from 'react';
import Cookies from 'js-cookie';
import { useMutation, useQuery } from '@tanstack/react-query';
import { useFormState } from 'react-dom';

const LoginPanel = ({ errorMessage, handleLogin }: { errorMessage: string, handleLogin: FormEventHandler<HTMLFormElement> }) => {
  
  return (

    <div className="flex justify-center items-center h-screen bg-gray-100">
      <div className="bg-white p-8 rounded shadow-md w-80">
        <h2 className="text-2xl font-bold mb-6">Logowanie</h2>
        <form onSubmit={handleLogin} className="space-y-4">
          <span className="text-red-400">{errorMessage}</span>
          <div>
            <label className="block mb-1 text-sm font-medium">Email</label>
            <input type="email" name='email' id="email" className="w-full border-gray-300 hover:bg-stone-200 transition duration-300 bg-stone-100 rounded-md p-2" required />
          </div>
          <div>
            <label className="block mb-1 text-sm font-medium">Hasło</label>
            <input type="password" name='password' id="password" className="w-full border-gray-300 hover:bg-stone-200 transition duration-300 bg-stone-100 rounded-md p-2 mb-3" required />
          </div>
          <button type="submit" className="w-full bg-blue-500 text-white py-2 rounded-md hover:bg-blue-600 transition duration-300">Zaloguj się</button>
        </form>
      </div>
    </div>
  );
};

export default LoginPanel;