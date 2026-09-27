import { Canvas } from '@react-three/fiber';
import { useNavigate } from "react-router";
import type { VoxelInfoResponse } from '../api/ApiResponses/VoxelInfoResponse';
import ElementRotator from './three/ElementRotator';
import VoxScene from './three/VoxScene';
import SceneLighting from './three/SceneLighting';
import { useEffect, useState } from 'react';
import { PerspectiveCamera } from '@react-three/drei';
import CanvasSetup from './three/CanvasSetup';
import { VOXLoader, type VOXLoaderResult } from 'three/examples/jsm/Addons.js';
import {VoxelBlobApi} from "../api/VoxelBlobApi.ts";
import UserName from './UserName.tsx';
import { useHeaderWithBearer } from '../hooks/useHeaderWithBearer.tsx';
import { useDarkMode } from '../hooks/useDarkMode.tsx';
type Props = {
    voxelResponse: VoxelInfoResponse;
};

export const Voxpreview = ({ voxelResponse }: Props) => {
    const navigate = useNavigate();

    const [onHover, setHover] = useState(false);
    const [IsSetupDone, setIsCameraDone] = useState(false);
    const [ImgData, setImgData] = useState<string|null>(null);
    const [voxData, setVoxData] = useState<VOXLoaderResult|null>(null);
    const bearer = useHeaderWithBearer()
    useEffect(()=>{
        if(ImgData){
        URL.revokeObjectURL(ImgData);
        }
        setVoxData(null)
   setImgData(null)
                 VoxelBlobApi.get<Blob>(`api/PreviewImages/${voxelResponse.id}`,{...bearer,responseType: "blob",}).then(
                   (response)=>{ 
                    var imgurl = URL.createObjectURL(response.data);
                    setImgData(imgurl)
                    // console.log("img")
                    //  console.log(imgurl)
                   
                   }
                )
                
        
    
},[voxelResponse])

   useEffect(()=>{
        if(onHover&&voxData==null){
        const load = () =>{
    
                  VoxelBlobApi.get<Blob>(`api/VoxFiles/${voxelResponse.id}`,{...bearer,responseType: "blob"}).then(
                       (response)=>{ 
                     console.log(response)
                     response.data.arrayBuffer().then(
                        (value)=>{
                            const loader = new VOXLoader();
                        setVoxData(loader.parse(value))
                    })
                   }
                )
                
                 
            }
        
        load()
    }
},[onHover,voxelResponse])

  const date =  new Date(voxelResponse.uploadTime);
  const dateReadable = date && date.toLocaleString("en-GB",{dateStyle:"short"});
       const{highlight,field} = useDarkMode()
    return (
        < div className={`flex flex-col mx-2 my-2 items-center w-[220px] ${field}`}>
             <div className={`w-full ${highlight}`}>
            <p className="mx-2 font-semibold text"> {voxelResponse.name}</p>
             </div>
            < div className="my-1 relative canvasbg
        w-[200px] max-w-50 shrink-0 aspect-square"
                onClick={() => {
                    navigate(`models/${voxelResponse.id}`);
                }}
                onMouseEnter={() => setHover(true)}
                onMouseLeave={() => {setHover(false);setIsCameraDone(false)}}
            >
                {voxData&&onHover ?
                    (
                    <Canvas  className={`absolute inset-0 ${IsSetupDone ? "opacity-100" : "opacity-0"}`}>
                        <PerspectiveCamera makeDefault={true} />
                        <CanvasSetup position={voxelResponse.cameraPosition} lookAt={voxelResponse.cameraLookAt} setIsSetupDone={setIsCameraDone} />
                        
                       <ElementRotator rotationSpeed={1} shouldRotate={IsSetupDone}>
                            <VoxScene vox={voxData} />
                       </ElementRotator>

                        <SceneLighting />
                    </Canvas>
                    ) :null
                   
                }
                <img hidden={IsSetupDone} className="absolute inset-0 w-full h-full object-cover"
                src={ImgData?ImgData:""}
                        alt={`Preview image for ${voxelResponse.name}`} />
            </div>
            <div className='w-[200px] h-18 overflow-y-auto'>
                   <div className='flex text-[14px]'> Uploaded by <UserName user={voxelResponse.poster}/></div>
                   <div><p className='text-[12px]'> on {dateReadable}</p></div>
                    <div><p className='text-[14px]'>{voxelResponse.likeCount} {voxelResponse.likeCount==1?"Like":"Likes"}</p></div>
                   
                
            </div>
        </div>
    )
}
