import { Outlet } from "react-router";
import Header from "../components/Header";
import { useDarkMode } from "../hooks/useDarkMode";


const DefaultLayout = () => {
  const {bg} = useDarkMode()
  return (
  
    <div className={`"flex flex-col ${bg} min-h-screen "`}>
     
      <Header/>
      <div className="flex flex-col flex-1 mx-2 mb-2 mt-4">
       <Outlet />
    </div>
     
    </div>
   
  );
};
 
export default DefaultLayout;
