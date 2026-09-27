import { useEffect, useState } from 'react'
import { StripeApi } from '../api/StripeApi'
import { useHeaderWithBearer } from '../hooks/useHeaderWithBearer'
import { useAuth } from 'react-oidc-context'
interface stripePriceData{

    products:{
        data:{
    default_price:{
        id:string
    }
}[]
    }

}



const DonationButton = () => {
    const bearer = useHeaderWithBearer()
    const {user,isAuthenticated} = useAuth()
   const [priceid, setPriceId] = useState<string|null>(null)
   const [loadfailed, setloadfailed] = useState<boolean>(false)
    useEffect(()=>{
       tryload()
    
    },[user,isAuthenticated])

const tryload = ()=>{
    setloadfailed(false)
    if(isAuthenticated){
           StripeApi.get<stripePriceData>("stripe/products",bearer).then(
            (Values)=>{ setPriceId(Values.data.products.data[0].default_price.id)}
        ).catch(
          () =>  setloadfailed(true)
        )
}
}

    const donate = async()=>{
        const content ={
            priceId:priceid
        }

StripeApi.post<string>("stripe",content,bearer).then(
    (ret)=>{ window.location.href =ret.data;}
)
 }

  return (
    <>
    {priceid?
     <button onClick={donate}
      className=
             "no-underline cursor-pointer "
          >
        donate 5 euro
      </button>:
            loadfailed? <button onClick={tryload}
      className= "no-underline cursor-pointer text-red-500 "
          >
        failed to load
      </button>:
      <span className="loading loading-spinner text-primary"/>}
      
      </>
  )

}

export default DonationButton