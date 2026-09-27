// Stap 1: Context aanmaken

import { createContext, useState, type PropsWithChildren } from "react";

interface DarkmodeContextType {
  setDarkMode: (isDarkMode: boolean) => void;
  isDarkmode:boolean
  bg:string
  field:string
  highlight:string
  hoverhighlight:string
  menu:string

}

// eslint-disable-next-line react-refresh/only-export-components
export const DarkmodeContext = createContext<DarkmodeContextType | null>(
  null,
);
// Stap 2: Provider aanmaken
const DarkmodeProvider = (props: PropsWithChildren) => {
      
  const [isDarkmode, setDarkMode] = useState<boolean>(false);
  
 const bg:string = isDarkmode?"bg-gray-500 text-white":"bg-gray-50 text-black"
 const field:string = isDarkmode?"bg-gray-700 text-white":"bg-gray-300 text-black"
 const highlight:string = isDarkmode?"bg-gray-800 text-white":"bg-gray-600 text-white"
 const hoverhighlight:string = isDarkmode?"hover:bg-gray-700 hover:text-white":"hover:bg-gray-500 hover:text-white"
  const menu:string = isDarkmode?"bg-gray-800 text-white":"bg-gray-800 text-white"
 





  return (
    <DarkmodeContext.Provider
      value={{isDarkmode:isDarkmode, setDarkMode:setDarkMode,bg:bg,field:field,highlight:highlight
        ,hoverhighlight:hoverhighlight,menu:menu
       }}>
      {props.children}
    </DarkmodeContext.Provider>
  );
};

export default DarkmodeProvider;
