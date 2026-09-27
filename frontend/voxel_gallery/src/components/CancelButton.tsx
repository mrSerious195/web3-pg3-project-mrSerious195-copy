import { useDarkMode } from "../hooks/useDarkMode"

interface props{
    setCurrentMode:(newState:boolean)=>void
}
const CancelButton = ({setCurrentMode}:props) => {
  const {highlight: highlightcol, hoverhighlight: hoverhighlightcol} =useDarkMode()
  const classname = `${highlightcol} ${hoverhighlightcol} my-2 px-2 py-1`
  return (
       <button 
       type="button"
       onClick={()=>{setCurrentMode(false)}}
                    className={classname}>Cancel</button>

  )
}

export default CancelButton