
import { VOXLoader } from "three/examples/jsm/Addons.js";

const Base64ToVox = (base64String : string) => {
    // transform the base64 string into the raw text you'd find in a .vox file
  
 const rawString = atob(base64String);
     
      const bytes = new Uint8Array(rawString.length);
     for(var i =0;i<bytes.length;i++){
            bytes[i]=((Number)(rawString.charCodeAt(i)));
     }
     //make a scene and a loader
    const loader = new VOXLoader();
    // try to parse the array buffer using the loader
    return loader.parse(bytes.buffer);
}

export default Base64ToVox