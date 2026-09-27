import { useNavigate, useParams } from "react-router";
import { useEffect, useState } from 'react'
import type { VoxelInfoResponse } from "../api/ApiResponses/VoxelInfoResponse";
import { VoxelDataApi } from "../api/VoxelDataApi";
import { VoxelBlobApi } from "../api/VoxelBlobApi";

import { Canvas } from "@react-three/fiber";
import { OrbitControls } from "@react-three/drei";
import VoxScene from "../components/three/VoxScene";
import type { FileResponseContract } from "../api/ApiResponses/FileResponseContract";
import { VOXLoader, type VOXLoaderResult } from "three/examples/jsm/Addons.js";
import SceneLighting from "../components/three/SceneLighting";
import CanvasSetup from "../components/three/CanvasSetup";
import { useAuth } from "react-oidc-context";
import type { LikeRequestContract } from "../api/ApiRequest/LikeRequestContract"
import type { LikeResponseContract } from "../api/ApiResponses/LikeResponseContract"
import CommentSection from "../components/CommentSection";
import UserName from "../components/UserName";
import { Menu, MenuButton, MenuItem, MenuItems } from "@headlessui/react";
import { useHeaderWithBearer } from "../hooks/useHeaderWithBearer";
import { useFormik } from "formik";
import * as Yup from 'yup';
import SubmitButton from "../components/SubmitButton";
import CancelButton from "../components/CancelButton";
import type { VoxelInfoRequest } from "../api/ApiRequest/VoxelInfoRequest";
import DownloadMenu from "../components/DownloadMenu";
import type { FavoriteRequestContract } from "../api/ApiRequest/FavoriteRequestContract";
import type { FavoritesResponseContract } from "../api/ApiResponses/FavoritesResponseContract";
import { useDarkMode } from "../hooks/useDarkMode";





const ModelViewPage = () => {
  const bearer = useHeaderWithBearer()
  const { isAuthenticated, user } = useAuth()
  // get modelId from parameters (should normally be something like /models/{modelId})
  const { modelId } = useParams();

  const [voxMeshes, setVoxMeshes] = useState<VOXLoaderResult | null>(null);
  const [voxData, setData] = useState<VoxelInfoResponse | null>(null);
  useEffect(() => {

    
      
        
        VoxelDataApi.get<VoxelInfoResponse>(`api/voxels/${modelId}`,bearer).then((dataResponse)=>{
        setData(dataResponse.data);
        setFieldValue("Name",dataResponse.data.name)
         setFieldValue("Description",dataResponse.data.description)
        console.log("full response:", dataResponse);
        })

       VoxelBlobApi.get<Blob>(`api/VoxFiles/${modelId}`,{...bearer,responseType: "blob"})
                  .then(
                    (response)=>{ 
                     console.log(response)
                     response.data.arrayBuffer().then(
                        (value)=>{
                            const loader = new VOXLoader();
                        setVoxMeshes(loader.parse(value))
                       }
                      )
                    }
                  )
      
    
   

  }, [isAuthenticated]);


  const ChangeLike = async (shouldBeLiked: boolean) => {
    const data: LikeRequestContract = {
      SceneId: Number(modelId),
      IsLiked: shouldBeLiked
    }
    const headers = {
      Authorization: `Bearer ${user?.access_token}`,
    }
    VoxelDataApi.put<LikeResponseContract>("api/Likes", data, { headers }).then(
      (value) => { console.log(value.data)
        if(voxData){
        const newdata:VoxelInfoResponse = {...voxData,isLiked:value.data.isLiked,likeCount:value.data.currentLikeCound}
        setData(newdata)
        }
       }
    )
  }
const ChangeFavorite = async (shouldBeFavorite: boolean) => {
    const data:FavoriteRequestContract ={
          FavoriteId: Number(modelId),
          IsFavorite: shouldBeFavorite
      }
     
      VoxelDataApi.put<FavoritesResponseContract>("api/Favorites",data,bearer).then(
          (value)=>{
            if(voxData){
             const newfavedata:VoxelInfoResponse = {...voxData,isFavorite:value.data.isFavorite}
        setData(newfavedata)
            }
          }
      )
    }
  const date = voxData && new Date(voxData.uploadTime);
  const dateReadable = date && date.toLocaleString("en-GB");

const navigate = useNavigate();
  const deleteThis=async()=>{
    if(voxData)
        await VoxelDataApi.delete<FileResponseContract>(`api/voxels/${voxData.id}`,bearer).then(
      ()=>{navigate(`/`)}
      )

  }

  const schema = Yup.object().shape({
      Name: Yup.string().required(),
      Description: Yup.string().required(),
    })
  
  const [edditMode,setEddit]=useState(false);
  
    const { handleChange, handleBlur, handleSubmit,setFieldValue, values } = useFormik({
      validationSchema: schema,
      initialValues: {
        Name:"",
        Description:""
      },
      onSubmit: async(values) => {
        if(voxData&&voxData.id){
        const dataToSend:VoxelInfoRequest = {
          Id:voxData.id,
          Name:values.Name,
          Description:values.Description,
        }
        
          await VoxelDataApi.put<VoxelInfoResponse>(`api/voxels`,dataToSend,bearer).then(
      (newdata)=>{
        
        setData(newdata.data);
        setFieldValue("Name",newdata.data.name)
         setFieldValue("Description",newdata.data.description)
      }
      ).finally(
        ()=>{setEddit(false)}
      )
      }
      }
    })
const {menu, highlight} = useDarkMode()
  return (
    <>
      <title>{voxData?.name}</title>
      < div className="flex  gap-4 h-[750px] ">

        <Canvas className=' flex-2 canvasbg'>
          <OrbitControls />
          {voxMeshes ? <VoxScene vox={voxMeshes} /> : ""}
          <SceneLighting />
          {voxData ? <CanvasSetup position={voxData.cameraPosition} lookAt={voxData.cameraLookAt} /> : null}
          <directionalLight position={[-1, 0, -3]} intensity={0.15} />
          <directionalLight position={[1, 5, 3]} intensity={0.5} />
        </Canvas>
        {voxData && < div className="flex flex-col justify-between flex-initial" >
          <form className="w-100 shrink-0" onSubmit={handleSubmit}>
            {edditMode?
             <input id="Name"
              className="border flex-1 "
              name="Name"
              value={values.Name}
              onChange={handleChange}
              onBlur={handleBlur}
            />:
            <h1 className="text-[24px] font-bold">{voxData?.name}</h1>
            }
            <hr/>
             <div className="flex">
            <div className="font-bold flex">Uploaded by :<UserName user={voxData.poster}/>  
              {voxData.poster&&voxData.poster.isUser?( 
                        <Menu as="div" className={`${menu} flex my-1`}>
                    <MenuButton className="text-[10px] flex-1 bg-white/20 px-2 font-semibold text-white hover:bg-white/40">
                      X
                     
                    </MenuButton>
              
                    <MenuItems
                      transition
                      className={`absolute right-0 z-10 mt-2 w-56 origin-top-right ${highlight} outline-1 
                      -outline-offset-1 outline-white/10 transition data-closed:scale-95 data-closed:transform 
                      data-closed:opacity-0 data-enter:duration-100 data-enter:ease-out data-leave:duration-75 data-leave:ease-in`}
                    >
                      <div className="py-1">
                       
                          <MenuItem>
                            <button
                            type="button"
                              onClick={deleteThis}
                              className="block w-full px-4 py-2 text-left text-sm data-focus:bg-white/5 data-focus:text-white data-focus:outline-hidden"
                            >
                              Delete
                            </button>
                          </MenuItem>
                           <MenuItem>
                            <button
                            type="button"
                              onClick={()=>{setEddit(true)}}
                              className="block w-full px-4 py-2 text-left text-sm data-focus:bg-white/5 data-focus:text-white data-focus:outline-hidden"
                            >
                              Edit Description
                            </button>
                          </MenuItem>
                      </div>
                    </MenuItems>
                  </Menu>)
                  :null}
             </div>
            </div>
            <p>Uploaded on {dateReadable}</p>
             <p>Likes: {voxData.likeCount}</p>
             {edditMode?
             ( <div className="flex flex-col"><textarea
              className="flex-1 min-w-0 border "
              id="Description"
              name="Description"
              value={values.Description}
              onChange={handleChange}
              onBlur={handleBlur}
            />
          <SubmitButton/>
           <CancelButton setCurrentMode={setEddit} />
          </div>
          ):
            <div className=" shadow-xl/20 rounded-none collapse my-5">
              <input type="checkbox" />
              <div className={` ${highlight}  collapse-title font-semibold`}>Description</div>
              <div className="collapse-content text-sm">
                <p className="">{voxData?.description}</p>
              </div>
            </div>
            }
          </form>
          <div className="flex flex-col">
          {voxData&&<DownloadMenu sceneId={voxData.id} name={voxData.name}/>}
          </div>
        </div>}
      </div>
       <div hidden={!isAuthenticated} 
          className="flex  gap-10 py-5">
            <button className ={voxData&&voxData.isLiked?`font-semibold shadow-xl/20 px-5 py-3 bg-green-400 text-white hover:text-black  hover:bg-red-600`:
            `font-semibold shadow-xl/20 px-5 py-3 ${highlight}  hover:text-black  hover:bg-green-400`}
            onClick={() => ChangeLike(!(voxData&&voxData.isLiked))}>Like</button>
            <button className = {(voxData)&&voxData.isFavorite?
            `font-semibold shadow-xl/20 px-5 py-3 bg-yellow-500 text-white hover:text-black  hover:bg-red-600`:
            `font-semibold shadow-xl/20 px-5 py-3 ${highlight}  hover:text-black hover:bg-yellow-500`}
            onClick={() => ChangeFavorite(!(voxData&&voxData.isFavorite))}>favorite</button>
          </div>
      <CommentSection sceneID={Number(modelId)} />
    </>
  )
}



export default ModelViewPage