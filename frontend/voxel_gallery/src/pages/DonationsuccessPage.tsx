import { HandThumbUpIcon } from '@heroicons/react/20/solid'

const DonationsuccessPage = () => {
  return (
       <div className='flex min-h-screen'>  
    <div className='flex-1 flex-col text-white bg-gray-600 justify-center flex text-center'>   
    <h1 className='font-bold text-white text-[36px]'>Payment went through</h1>
     <p>Thanks for donating, here have a green name</p>
     </div> 
     <div className='flex flex-col justify-center flex-1 text-black bg-gray-300 text-center'>
        <HandThumbUpIcon className='text-gray-600'/>
     </div>
    
    </div> 
  )
}

export default DonationsuccessPage