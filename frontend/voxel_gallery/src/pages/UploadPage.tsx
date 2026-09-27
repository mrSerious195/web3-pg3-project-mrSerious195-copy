import React, { useEffect, useRef, useState } from 'react'
import { useFormik } from "formik";
import * as THREE from "three";
import { PerspectiveCamera } from "@react-three/drei";
import { Canvas } from '@react-three/fiber';
import VoxScene from '../components/three/VoxScene';

import PictureCamera, { type PictureCameraHandle } from '../components/three/PictureCamera';
import SceneLighting from '../components/three/SceneLighting';
import { VoxelDataApi } from '../api/VoxelDataApi';
import Base64ToVox from '../functions/Base64ToVox';
import { useAuth } from 'react-oidc-context';
import type { VOXLoaderResult } from 'three/examples/jsm/Addons.js';
import type { VoxelInfoRequest } from '../api/ApiRequest/VoxelInfoRequest';
import type { VoxelInfoResponse } from '../api/ApiResponses/VoxelInfoResponse';
import { useNavigate } from 'react-router-dom';
import SubmitButton from '../components/SubmitButton';
import * as Yup from 'yup';
import Errormsg from '../components/Errormsg';
import { useDarkMode } from '../hooks/useDarkMode';

const UplaodPage = () => {
  const { user } = useAuth();
  const navigate = useNavigate();
  console.log(user)
  const pictureref = useRef<PictureCameraHandle>(null);
  const cameraRef = useRef<THREE.PerspectiveCamera>(null);
  const [FileErrormsg, setErrormsg] = useState<string | null>(null);
  const [vox, setVox] = useState<VOXLoaderResult>();

  const schema = Yup.object().shape({
    Name: Yup.string().required(),
    Description: Yup.string().required(),
    Camera: Yup.object().shape({
      CameraPosition: Yup.object().shape({
        x: Yup.number().required(),
        y: Yup.number().required(),
        z: Yup.number().required()
      }),
      CameraLookAt: Yup.object().shape({
        x: Yup.number().required(),
        y: Yup.number().required(),
        z: Yup.number().required()
      })
    }),
    UploadFile:Yup.string().required()
  })



  const { handleChange, handleBlur, handleSubmit,setFieldValue, values,errors } = useFormik({
    validationSchema: schema,
    initialValues: {
      Name: "",
      Description: "",
      Camera: {
        CameraPosition: {
          x: 5,
          y: 0,
          z: 0
        },
        CameraLookAt: {
          x: 0,
          y: 0,
          z: 0
        }
      },
    UploadFile:""
    },
    onSubmit: values => {
      var myImg: string | null = null
      if (pictureref.current)
        myImg = pictureref.current.TakePicture().replace("data:image/png;base64,", "");
      if (myImg) {
        const requestContract: VoxelInfoRequest = {
          Name: values.Name,
          CameraPosition: { X: values.Camera.CameraPosition.x, Y: values.Camera.CameraPosition.y, Z: values.Camera.CameraPosition.z },
          CameraLookAt: { X: values.Camera.CameraLookAt.x, Y: values.Camera.CameraLookAt.y, Z: values.Camera.CameraLookAt.z },
          VoxelDataAsBase64: values.UploadFile,
          ImgAsBase64: myImg,
          Description: values.Description
        }
        const headers = {
          Authorization: `Bearer ${user?.access_token}`,
        }
        console.log(headers)
        console.log(requestContract)
        VoxelDataApi.post<VoxelInfoResponse>("api/voxels", requestContract, { headers }).then(
          (values) => { navigate(`/models/${values.data.id}`) }
        ).catch(
          () => { setErrormsg("error during posting") }
        )
      } else {
        console.log("something went wrong with img generation")
      }
    },
  })
  // we use a useEffect for the camera change because of a few reasons
  /*
  1 we don't have access to the lookat function if we don't have a ref
  2 the position change can be done in the xml, it has to happen before the lookat (turns out it didn't if I tried that)
  3 you can probably do it without the lookat by computing the rotation yourself, but why re-invent the wheel.
  */
  const handleCamChange = () => {

    if (cameraRef.current) {

      cameraRef.current.position.set(
        Number(values.Camera.CameraPosition.x),
        Number(values.Camera.CameraPosition.y),
        Number(values.Camera.CameraPosition.z)
      );

      if (cameraRef.current) {
        cameraRef.current.lookAt(new THREE.Vector3(values.Camera.CameraLookAt.x, values.Camera.CameraLookAt.y, values.Camera.CameraLookAt.z))
      }

    }
  }
  
  useEffect(handleCamChange,[
  values.Camera.CameraPosition.x,
  values.Camera.CameraPosition.y,
  values.Camera.CameraPosition.z,
  values.Camera.CameraLookAt.x,
  values.Camera.CameraLookAt.y,
  values.Camera.CameraLookAt.z
])

  const handleFileChange = async (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (!file) return;
    console.log("try 2 read")

    const buffer = await file.arrayBuffer();
    const bytes = new Uint8Array(buffer);

    let binary = "";
    for (let i = 0; i < bytes.length; i++) {
      binary += String.fromCharCode(bytes[i]);
    }
    const base64 = btoa(binary);
    setFieldValue("UploadFile",base64)
    setVox(Base64ToVox(base64));
  }
const {highlight} = useDarkMode()
  return (
    <div className="flex  flex-1 h-auto flex gap-5">
      <div className='flex flex-2 flex-col aspect-square'>
        <Canvas gl={{ preserveDrawingBuffer: true }} className='flex canvasbg'>
          <PictureCamera ref={pictureref} />
          <PerspectiveCamera ref={cameraRef} makeDefault={true} />
          {vox ? <VoxScene vox={vox}></VoxScene> : ""}
          <SceneLighting />
        </Canvas>
      </div>
      <form onSubmit={handleSubmit} className=" justify-between flex-1 flex-col flex  ">
        <div >
          
          <div className={`px-5 py-2 w-full shadow-xl/20 ${highlight} font-semibold`}>
            <label htmlFor='Name' className="">Name</label>
          </div>
          <div className="my-2">
                    {errors.Name && < Errormsg>{errors.Name}</Errormsg>}
          <div className='flex px-5 '>
            <input id="Name"
              className="border flex-1 "
              name="Name"
              value={values.Name}
              onChange={handleChange}
              onBlur={handleBlur}
            />
          </div>
          </div>
          <div className={`px-5 py-2 mb-5 w-full shadow-xl/20 ${highlight} font-semibold`}>
            <label >CameraPosition</label>
          </div>
            {errors.Camera?.CameraPosition?.x && 
            < Errormsg>{errors.Camera?.CameraPosition?.x}</Errormsg>}
            {errors.Camera?.CameraPosition?.y && 
            < Errormsg>{errors.Camera?.CameraPosition?.y}</Errormsg>}
            {errors.Camera?.CameraPosition?.z && 
            < Errormsg>{errors.Camera?.CameraPosition?.z}</Errormsg>}
          <div className="justify-center px-5 py-2 gap-8 flex flex-1   items-center">
            <div>
              <label >x :</label>
              <input
                className="mx-2  w-20 border [appearance:textfield]"
                id="Camera.CameraPosition.x"
                name="Camera.CameraPosition.x"
                value={values.Camera.CameraPosition.x}
                type='number'
                onChange={handleChange}
                onBlur={handleBlur}
              />
            </div>
            <div>
              <label >y :</label>
              <input
                className=" mx-2  w-20 border [appearance:textfield]"
                id="Camera.CameraPosition.y"
                name="Camera.CameraPosition.y"
                value={values.Camera.CameraPosition.y}
                type='number'
                onChange={handleChange}
                onBlur={handleBlur}
              />
            </div>
            <div>
              <label >z :</label>
              <input
                className="mx-2  w-20 border [appearance:textfield]"
                id="Camera.CameraPosition.z"
                name="Camera.CameraPosition.z"
                value={values.Camera.CameraPosition.z}
                type='number'
                onChange={handleChange}
                onBlur={handleBlur}
              />
            </div>
          </div>
          <div className={`px-5 py-2 mb-5 w-full shadow-xl/20 ${highlight} font-semibold`}>

            <label >CameraLookAt</label>
              </div>
            {errors.Camera?.CameraLookAt?.x && 
            < Errormsg>{errors.Camera?.CameraLookAt?.x}</Errormsg>}
            {errors.Camera?.CameraLookAt?.y && 
            < Errormsg>{errors.Camera?.CameraLookAt?.y}</Errormsg>}
            {errors.Camera?.CameraLookAt?.z && 
            < Errormsg>{errors.Camera?.CameraLookAt?.z}</Errormsg>}
        
          <div className="justify-center gap-8 px-5 py-2 flex   items-center">
            <div><label >x :</label>
              <input
                className="mx-2 w-20 border [appearance:textfield]"
                id="Camera.CameraLookAt.x"
                name="Camera.CameraLookAt.x"
                value={values.Camera.CameraLookAt.x}
                type='number'
                onChange={handleChange}
                onBlur={handleBlur}
              />
            </div>
            <div> <label >y :</label>
              <input
                className=" mx-2  w-20 border [appearance:textfield]"
                id="Camera.CameraLookAt.y"
                name="Camera.CameraLookAt.y"
                value={values.Camera.CameraLookAt.y}
                type='number'
                onChange={handleChange}
                onBlur={handleBlur}
              /></div>
            <div>  <label >z :</label>
              <input
                className="mx-2  w-20 border [appearance:textfield]"
                id="Camera.CameraLookAt.z"
                name="Camera.CameraLookAt.z"
                value={values.Camera.CameraLookAt.z}
                type='number'
                onChange={handleChange}
                onBlur={handleBlur}
              /></div>
          </div>
          <div className={`px-5 py-2 mb-5 w-full shadow-xl/20 ${highlight} font-semibold`}>
            <label >Description</label>
          </div>
          {errors.Description && <Errormsg>{errors.Description}</Errormsg>}

          <div className="px-5 py-2 flex   items-center">
            <textarea
              className="flex-1 min-w-0 border "
              id="Description"
              name="Description"
              value={values.Description}
              onChange={handleChange}
              onBlur={handleBlur}
            />
          </div>
        </div>
        <div className='mx-10 my-5'>
          {FileErrormsg && < Errormsg>{FileErrormsg}</Errormsg>}
          <div className='justify-between flex center'>
            <input
              className="border px-2 py-2"
              type="file"
              accept=".vox"
              name="voxFile"
              onChange={
                handleFileChange
              }
            />

            <SubmitButton />
          </div>
        </div>
      </form>
    </div>
  )
}

export default UplaodPage;


