import type { UserResponseContract } from "./UserResponseContract";

export interface VoxelInfoResponse {
  id: number;
  // TODO check if this is the one used in our lessons, could also be Uint8Array, Blob, ArrayBuffer or number[] (turned out that it arrives as a string)
uploadTime:string;
poster?:UserResponseContract
  name: string;
  cameraPosition: {x:number,y:number,z:number}
  cameraLookAt: {x:number,y:number,z:number}
  description:string,
  likeCount:number,
  isLiked:boolean,
  isFavorite:boolean
}