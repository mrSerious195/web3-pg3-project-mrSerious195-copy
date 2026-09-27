export interface VoxelInfoRequest {
Id?:Number
Name: string
CameraPosition?: {X : number,Y:number,Z:number} 
CameraLookAt?: {X : number,Y:number,Z:number} 
VoxelDataAsBase64? : string
ImgAsBase64?:string
Description:string
}