import { useFrame } from '@react-three/fiber'
import { useRef, type PropsWithChildren } from 'react'
import * as THREE from 'three';
interface spinnerprops
extends PropsWithChildren
{
    rotationSpeed: number 
    shouldRotate?:boolean
}


const ElementRotator = ({rotationSpeed,shouldRotate, children} :spinnerprops) => {
    const groupref = useRef<THREE.Group>(null!);
      useFrame((_state,delta) => {if(shouldRotate==null || shouldRotate==true){
        groupref.current.rotation.y += rotationSpeed* delta}
})
  return (
    <group ref={groupref}>{children}</group>
  )
}

export default ElementRotator