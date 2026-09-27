import { ArrowLeftIcon, ArrowRightIcon } from '@heroicons/react/20/solid';
import { useEffect, useState } from 'react'
import { Voxpreview } from './Voxpreview';
import { VoxelDataApi } from '../api/VoxelDataApi';
import type { VoxelInfoResponse } from '../api/ApiResponses/VoxelInfoResponse';
import { useAuth } from 'react-oidc-context';
import { useHeaderWithBearer } from '../hooks/useHeaderWithBearer';
import { useDarkMode } from '../hooks/useDarkMode';

interface props{
    className?:string
}
const FavoriteGrid = ({className}:props) => {
  const {isAuthenticated} = useAuth();
       const [voxData,setVoxData] = useState<VoxelInfoResponse[]|null>(null);
        const [FailedToLoad,setFailedToLoad] = useState<boolean>(false);
     const bearer = useHeaderWithBearer()
 
     const pageSize = 4
 
     const [currentPage,setCurrentPage]=useState(1);

 
     const search=()=>{
        
      setFailedToLoad(false)
          VoxelDataApi.get<VoxelInfoResponse[]>(`api/voxels/favorites/?page=${currentPage}&pageSize=${pageSize}`,bearer).then(
         (response)=>{
              setVoxData(response.data);
         }
      ).catch (()=>{
        setFailedToLoad(true)
        
       })
     }
         
      useEffect(()=>{
         if(!isAuthenticated)
             return       
         search()
      }, [ isAuthenticated,currentPage]);
      const {highlight: highlightcol,hoverhighlight: hoverhighlightcol} =useDarkMode()
     return (
         <div className={className}>
             <div>
                    <div className="flex items-center">
          
            
                   <h2 className={`py-1 px-2 ${highlightcol} font-semibold  ml-2`}>Favorites</h2>
         
         <div className={` ${highlightcol} h-[10px]  flex-3 `} />
         
          <div className={` ${highlightcol} flex items-center justify-center`}>

         <button onClick={()=>{if(currentPage-1>=1)setCurrentPage(currentPage-1)}}>
         <ArrowLeftIcon className={`w-8 ${hoverhighlightcol} `}/>
         </button>
         <p className=" text-white text-center">{currentPage}</p>
         <button onClick={()=>setCurrentPage(currentPage+1)}>
         <ArrowRightIcon className={`w-8 ${hoverhighlightcol} `}/>
         </button >
         </div>
       </div>
                
             </div>
             <div className='flex justify-center'>
             <div className='flex flex-wrap max-w-[600px] justify-center'>
              {FailedToLoad&&!voxData&&<button className={`p-3 ${highlightcol} ${hoverhighlightcol}`} onClick={search}>Retry Loading</button>}
              {(!FailedToLoad)&&!voxData&&<span className="loading loading-spinner text-primary"></span>}
                 {voxData&&voxData.map((value, idx) => {
                       return (<Voxpreview key={idx} voxelResponse = {value}/>);
                     })}
             </div>
             </div>
         </div>)
}

export default FavoriteGrid