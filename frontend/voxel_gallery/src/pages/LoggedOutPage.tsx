import { type PropsWithChildren } from 'react'
import { useAuth } from 'react-oidc-context'

const LoggedOutPage = ({children}:PropsWithChildren) => {
   const {isAuthenticated,signinRedirect}= useAuth()
  return (
    <>
    {isAuthenticated?children:
     <div className='flex min-h-screen'>  
    <div className='flex-1 flex-col bg-gray-600 text-white justify-center flex text-center'>   
    <h1 className='font-bold text-[36px]'>You're logged out</h1>
     <p>you need to login to use this site</p>
     </div> 
     <div className='flex flex-col justify-center flex-1 text-black bg-gray-50 text-center'>
<button onClick={()=>{signinRedirect()}}
      className="no-underline cursor-pointer self-center py-3 px-5 text-white  w-fit font-bold text-[36px] bg-gray-600 hover:bg-gray-500 shadow-xl/30"
          >
        Login
      </button>
     </div>
    
    </div> 
    }
    </>
  )
}

export default LoggedOutPage