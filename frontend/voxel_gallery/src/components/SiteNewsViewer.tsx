import { useState } from 'react'
import type { NewsResponseContract } from '../api/ApiResponses/NewsResponseContract'
interface props{
    data:NewsResponseContract[]
}

const SiteNewsViewer = ({data}:props) => {
  const [index, setIndex]=useState(0);

  const currentData = data[index];
  return(
    <div>
      <h2>SiteNews</h2>
      <div className='flex items-center'>
            <button onClick={()=>{setIndex((data.length+index-1)%data.length)}}>⇐</button>
          <button onClick={()=>{setIndex((index+1)%data.length)}}>⇒</button>
         </div>
        <div className='flex items-center justify-between'>
        <div>{currentData.title}</div>
        <div>{currentData.time}</div>
        </div>
        <div><p className="whitespace-pre-line">{currentData.body}</p></div>
    </div>
  )
}


export default SiteNewsViewer