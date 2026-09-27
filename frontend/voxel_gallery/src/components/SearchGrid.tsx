import { useEffect, useState } from 'react'
import { useHeaderWithBearer } from '../hooks/useHeaderWithBearer';
import { useAuth } from 'react-oidc-context';
import type { VoxelInfoResponse } from '../api/ApiResponses/VoxelInfoResponse';
import { VoxelDataApi } from '../api/VoxelDataApi';
import { Voxpreview } from './Voxpreview';

import { ArrowLeftIcon, ArrowRightIcon } from '@heroicons/react/20/solid';
import { useFormik } from 'formik';
import { useDarkMode } from '../hooks/useDarkMode';

const SearchGrid = () => {

     const {isAuthenticated} = useAuth();
      const [voxData,setVoxData] = useState<VoxelInfoResponse[]|null>(null);
       const [FailedToLoad,setFailedToLoad] = useState<boolean>(false);
    const bearer = useHeaderWithBearer()

    const pageSize = 4

    const [currentPage,setCurrentPage]=useState(1);

     const { handleChange, handleBlur, handleSubmit, values } = useFormik({
         
            initialValues: {
                searchfield:""
        
            },
            onSubmit: () => {
               search();
               setCurrentPage(1)
                } 
})

    const search=()=>{
        
        setFailedToLoad(false)
         VoxelDataApi.get<VoxelInfoResponse[]>(values.searchfield?`api/voxels?page=${currentPage}&pageSize=${pageSize}&name=${values.searchfield}`:
            `api/voxels?page=${currentPage}&pageSize=${pageSize}`,bearer).then(
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

   const {highlight, hoverhighlight}=  useDarkMode()
    return (
        <div className="">
            <div>
                   <div className="flex items-center">
         
           
                <form onSubmit={handleSubmit} className='flex flex-1 '>
                    <input  type="search" className={`${highlight} ml-2 border-2 w-full`} 
                    placeholder='search...'
                    id="searchfield"
                    name="searchfield"
                    value={values.searchfield}
                    onChange={handleChange}
                    onBlur={handleBlur}
                    />
                    <button className={`font-semibold ${highlight} ${hoverhighlight} py-1 px-2`}>Search</button>
                </form>
        
        <div className={`h-[10px]  flex-3 ${highlight} `} />
        
         <div className={`flex items-center ${highlight} justify-center `}>
        <button onClick={()=>{if(currentPage-1>=1)setCurrentPage(currentPage-1)}}>
        <ArrowLeftIcon className={`w-8  ${hoverhighlight}`}/>
        </button>
        <p className=" text-white text-center">{currentPage}</p>
        <button onClick={()=>setCurrentPage(currentPage+1)}>
        <ArrowRightIcon className={`w-8  ${hoverhighlight}`}/>
        </button >
        </div>
      </div>
               
            </div>
            
            <div className='flex flex-wrap justify-center'>
                 {FailedToLoad&&!voxData&&<button className={`p-3 ${highlight} ${hoverhighlight}`} onClick={search}>Retry Loading</button>}
                 {(!FailedToLoad)&&!voxData&&<span className="loading loading-spinner text-primary"></span>}
                {voxData&&voxData.map((value, idx) => {
                      return (<Voxpreview key={idx} voxelResponse = {value}/>);
                    })}
            </div>
        </div>
    )
}

export default SearchGrid