import { useThree } from '@react-three/fiber'
import { forwardRef, useImperativeHandle } from 'react'
import * as THREE from 'three'

export type PictureCameraHandle = {
  TakePicture: () => string;
 // cameraRef :React.RefObject<THREE.PerspectiveCamera | null>
};




const PictureCamera =  forwardRef<PictureCameraHandle>((_Props,ref) => {
  const { gl,camera,scene } = useThree()


// this is to export the taken picture
useImperativeHandle(ref,()=>({
   TakePicture :()=>{
    const width = 400;
    const height = 400; 

    gl.setSize(width, height, false);
    if (camera instanceof THREE.PerspectiveCamera) 
    camera.aspect = width / height;
    camera.updateProjectionMatrix();
    gl.render(scene, camera);
    var ret:string = gl.domElement.toDataURL()
    return ret
  },
}))

// <PerspectiveCamera  makeDefault={true} />

  return (
    null
  )
})

export default PictureCamera
