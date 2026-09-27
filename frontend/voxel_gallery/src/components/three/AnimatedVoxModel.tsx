import React from 'react'
import { type PropsWithChildren } from 'react';
import * as THREE from "three";
interface Props extends PropsWithChildren {
  frames: THREE.Object3D[];
};

const AnimatedVoxModel = ({ frames, children }: Props) => {
  const myMesh = React.useRef<THREE.Mesh | null>(null);
var currentFrame = 0;


// animation attempt 1
/*
//const timeBetweenFrames = 1
//var timeTillNextFrame = timeBetweenFrames
useFrame((_,delta) => {
  timeTillNextFrame-=delta;
if(timeTillNextFrame<=0){
  currentFrame=(currentFrame+1)%frames.length;
  const currentmesh = frames[currentFrame]
  if(myMesh.current instanceof THREE.Mesh&& currentmesh instanceof THREE.Mesh){
  myMesh.current.geometry = currentmesh.geometry
  }else{
    console.log(`instanceof failed  myMesh.type = ${myMesh.current?.type}, currentmesh.type = ${currentmesh.type}`)
  }
  timeTillNextFrame=timeBetweenFrames}
})*/
// animation attempt 2
 setInterval(()=>{
 currentFrame=(currentFrame+1)%frames.length;
  const currentmesh = frames[currentFrame]
  if(myMesh.current instanceof THREE.Mesh&& currentmesh instanceof THREE.Mesh){
  myMesh.current.geometry = currentmesh.geometry
  }else{
    console.log(`instanceof failed  myMesh.type = ${myMesh.current?.type}, currentmesh.type = ${currentmesh.type}`)
  }
},1000)

// there is no meaningfull difference between animation attempt 1 and 2
// TODO if I have problems, maybe I can just add all the Meshes, but change their Scale according to frame


  const currentVox = frames[0];
  if(currentVox instanceof THREE.Mesh)
  return (
    
      
        <mesh ref={myMesh}
            geometry={currentVox.geometry}
            material={currentVox.material}
            position={currentVox.position}
          >
            {children}
          </mesh>
        

  );
return "";

}

export default AnimatedVoxModel