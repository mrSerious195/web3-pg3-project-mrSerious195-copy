import { useAuth } from "react-oidc-context";
import { NavLink } from "react-router";
import DonationButton from "./DonationButton";
import { useDarkMode } from "../hooks/useDarkMode";

const Header = () => {
  const {isAuthenticated,signinRedirect,signoutRedirect} = useAuth();
const {highlight, setDarkMode}=useDarkMode()

  return (
     <header className={` shadow-xl/20 items-center w-full grid grid-cols-3 font-semibold  h-[30px] ${highlight} px-4"`}>
      
      <div className="flex gap-4 items-center">
        <NavLink
          className={({ isActive }) =>
            isActive ? "underline underline-offset-8" : "no-underline"
          }
          to="/">
          Home
        </NavLink>
        <NavLink
          className={({ isActive }) =>
            isActive ? "underline underline-offset-8" : "no-underline"
          }
          to="/upload">
          Upload
        </NavLink>
     
      </div>
     <div className="text-center">
      <DonationButton/>
      </div>
<div className="justify-self-end gap-5 flex">
   <div className="flex items-center gap-2">
        <p className= "mx-2px">light/dark</p>
      <input type="checkbox" onClick={(e) => {
    setDarkMode(!(e.target as HTMLInputElement).checked)}}
     defaultChecked
      className="toggle" />
      </div>
      {isAuthenticated?<button onClick={()=>{signoutRedirect()}}
      className={
             "no-underline text-right w-auto cursor-pointer "
          }>LogOut</button>:
     
      <button onClick={()=>{signinRedirect()}}
      className={
             "no-underline cursor-pointer"
          }>Login</button>}
      </div>
    </header>
  )
}

export default Header