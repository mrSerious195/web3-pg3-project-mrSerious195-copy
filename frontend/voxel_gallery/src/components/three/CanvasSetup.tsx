import { useThree } from '@react-three/fiber';
import { useEffect } from 'react'
import { Vector3 } from 'three';

interface props{
   position:{x:number, y:number, z:number},
   lookAt:{x:number, y:number, z:number}
   setIsSetupDone?:(iscamdone:boolean)=>void
}

const CanvasSetup = ({ position, lookAt,setIsSetupDone}:props) => {

  const { camera } = useThree();

  useEffect(() => {
    camera.position.set(position.x,position.y,position.z);
    camera.lookAt(new Vector3(lookAt.x,lookAt.y,lookAt.z));
    camera.updateProjectionMatrix();
    if(setIsSetupDone)
      setIsSetupDone(true)
  });


  return null
}

export default CanvasSetup