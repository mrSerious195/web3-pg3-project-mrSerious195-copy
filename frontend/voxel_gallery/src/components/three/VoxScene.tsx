import { type PropsWithChildren } from 'react'
import type { VOXLoaderResult } from 'three/examples/jsm/Addons.js';
import AnimatedVoxModel from './AnimatedVoxModel';
import StationaryVoxModel from './StationaryVoxModel';
interface Props extends PropsWithChildren  {
  vox : VOXLoaderResult
};

const VoxScene = ({vox}: Props) => {
      
  return (
    <group>
        {vox.scene.children.map((voxObject,index)=>{
            return voxObject.children[0]?
            <AnimatedVoxModel key={index} frames={voxObject.children[0].children}></AnimatedVoxModel>:
            <StationaryVoxModel key={index} voxmesh={voxObject}></StationaryVoxModel>
        })}
    </group>
  )
}

export default VoxScene