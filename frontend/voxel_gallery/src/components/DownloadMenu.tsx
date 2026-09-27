import { Menu, MenuButton,MenuItems,MenuItem } from '@headlessui/react'

import { useHeaderWithBearer } from '../hooks/useHeaderWithBearer'
import { VoxelBlobApi } from '../api/VoxelBlobApi'
import { VoxelDataApi } from '../api/VoxelDataApi'
import { useDarkMode } from '../hooks/useDarkMode'
interface props{
    sceneId:number
    name:string
}
const DownloadMenu = ({sceneId,name}:props) => {
    const bearer = useHeaderWithBearer()
  
    const downloadVox=()=>{
     VoxelBlobApi.get<Blob>(`api/VoxFiles/${sceneId}`,{...bearer,responseType: "blob"}).then(
                       (response)=>{ 
                    const url = URL.createObjectURL(response.data)
                    const link = document.createElement('a')
                    link.href = url
                    link.download = `${name}.vox`
                    link.click()
                    URL.revokeObjectURL(url)
                    })
                   }
                
    

    

    const handlePDFDownload = async() => {
    const retdata = await VoxelDataApi.get(`api/voxels/${sceneId}/pdf`,{...bearer,
        responseType: "blob",})
    const url = URL.createObjectURL(retdata.data)
    const link = document.createElement('a')
    link.href = url
    link.download = `${name}.pdf`
    link.click()
    URL.revokeObjectURL(url)
  }

  const {menu} = useDarkMode()
  return (
      <Menu as="div" className={`flex flex-col relative inline-block text-white ${menu}`}>
         <MenuButton className=" flex-1 w-full px-2 font-semibold  hover:bg-white/40 ">
           Download File...
          
         </MenuButton>
   
         <MenuItems
           transition
           className={`${menu} absolute right-0 z-10 mt-2 w-56 origin-top-right outline-1 -outline-offset-1 outline-white/10 transition data-closed:scale-95 data-closed:transform data-closed:opacity-0 data-enter:duration-100 data-enter:ease-out data-leave:duration-75 data-leave:ease-in`}
         >
           <div className="py-1">
            
               <MenuItem>
                 <button
                   onClick={downloadVox}
                   className="block w-full px-4 py-2 text-left text-sm data-focus:bg-white/5 data-focus:text-white data-focus:outline-hidden"
                 >
                   VoxFile
                 </button>
               </MenuItem>
                <MenuItem>
                 <button
                   onClick={handlePDFDownload}
                   className="block w-full px-4 py-2 text-left text-sm data-focus:bg-white/5 data-focus:text-white data-focus:outline-hidden"
                 >
                   PDF
                 </button>
               </MenuItem>
           </div>
         </MenuItems>
       </Menu>
  )
}

export default DownloadMenu