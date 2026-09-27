export interface NewsResponseContract {
  id: string;
  // TODO check if this is the one used in our lessons, could also be Uint8Array, Blob, ArrayBuffer or number[] (turned out that it arrives as a string)
title:string;
body:string;
  time: string;
}