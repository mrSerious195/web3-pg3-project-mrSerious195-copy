import { type PropsWithChildren } from 'react'

const LineHeader = ({children}:PropsWithChildren) => {
  return (
          <div className="flex items-center">
       
          <h2 className=" text-lg font-semibold">{children}</h2>
        <div className="h-px flex-1" />
      </div>
  )
}

export default LineHeader