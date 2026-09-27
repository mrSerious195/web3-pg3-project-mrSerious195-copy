import { type PropsWithChildren } from 'react'

const Errormsg = ({children}:PropsWithChildren) => {
  return (
    <p className='w-full text-[12px] text-center text-red-800'>{children}</p>
  )
}

export default Errormsg