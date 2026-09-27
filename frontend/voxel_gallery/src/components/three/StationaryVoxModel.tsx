
import type { PropsWithChildren } from 'react';
import * as THREE from "three";
interface Props extends PropsWithChildren  {
  voxmesh: THREE.Object3D;
};

const StationaryVoxModel = ({voxmesh,children}: Props ) => {

   
     if ((voxmesh instanceof THREE.Mesh)){
    return(
        <mesh
          geometry={voxmesh.geometry}
          material={voxmesh.material}
          position={voxmesh.position}
        >
          {children}
        </mesh>
      );
    }
  
console.error("faled to cast")
  return (
     <group>
  </group>
  )
}

export default StationaryVoxModel