'use server'
import { getToken, getUserById } from "@/requests/POST/loginRequest";
import { jwtDecode } from "jwt-decode";
import { cookies } from 'next/headers';
import { setCookie, deleteCookie, hasCookie, getCookie, getCookies } from 'cookies-next';

export async function authorization()
{
    try{
        if(getActualToken() != null)
        {
            return true;
        }
        /*
        const auth = await getToken(email,password)

        if(auth.token == null)
            return false
        
        const decoded = jwtDecode(auth.token);

        const userIdFromToken = decoded.UserID

        setCookie('token',  auth.token, { cookies });
        setCookie('userId', userIdFromToken, { cookies });
        */
        //Cookies.set('token', auth.token)
        //Cookies.set('userId', userIdFromToken)
        
        return true;
        
    }
    catch(err)
    {
        return false;
    }
}
export const getActualToken = () =>
{
    const token = getCookie('token', { cookies });
    
    if(token == null)
    {
        return null;
    }
    return token;

}
export const getUserIdFromCookies = () =>
{
    //const cookieStore = cookies()

    //const token = Cookies.get('token') ;

    const token = getCookie('token', { cookies });

    if(token == null)
        return false

    const decoded = jwtDecode(token);

    const userIdFromToken = decoded.UserID

    return userIdFromToken

}
