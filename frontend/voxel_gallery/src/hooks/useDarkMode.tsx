import { useContext } from "react";
import { DarkmodeContext } from "../contexts/DarkModeContext";

export const useDarkMode = () => {
  const darkmodeContext = useContext(DarkmodeContext);

  if (!darkmodeContext) {
    throw new Error("DarkmodeContext must be wrapped in a DarkmodeProvider");
  }

  return darkmodeContext;
};
