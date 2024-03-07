import { NextResponse } from 'next/server'
import type { NextRequest } from 'next/server'
import Cookies from 'js-cookie';
import { getUserById } from './requests/POST/loginRequest';
import router from 'next/router';
import { getActualToken, getUserIdFromCookies } from './app/lib/utils';


export async function middleware(request: NextRequest) {

  if (request.nextUrl.href == `${process.env.FRONTEND_SERVER}/login` || request.nextUrl.href == `${process.env.FRONTEND_SERVER}/` || request.nextUrl.pathname.startsWith('/coordinator') || request.nextUrl.pathname.startsWith('/medicalWorker'))
  {
      if(getActualToken() == null)
      {
        return NextResponse.rewrite(new URL('/login', request.url))  
      }
      else
      {
        const userId = getUserIdFromCookies();
        const user = await getUserById(userId);

        const userRoles:string[] = user.userRoles

        if(userRoles.find(x=> x.value == "Coordinator") && request.nextUrl.href !== `${process.env.FRONTEND_SERVER}/coordinator/changeToMedicalWorker`)
        {
          if(request.nextUrl.pathname.startsWith('/coordinator'))
          {
            return NextResponse.rewrite(new URL(request.nextUrl, request.url))
          }
          else
          {
            return NextResponse.rewrite(new URL('/coordinator', request.url)) 
          } 
        }
        else if(userRoles.find(x=>x.value == "MedicalWorker"))
        {
          if(request.nextUrl.pathname.startsWith('/medicalWorker'))
          {
            return NextResponse.rewrite(new URL(request.nextUrl, request.url))
          }
          else
          {
            return NextResponse.rewrite(new URL('/medicalWorker', request.url)) 
          } 
        }
      }
  }
  
}