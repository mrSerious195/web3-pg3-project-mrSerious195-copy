import { useDarkMode } from "../hooks/useDarkMode"


const SubmitButton = () => {
  const{highlight,hoverhighlight} = useDarkMode()
  return (
    <button type="submit" className={` ${highlight} ${hoverhighlight} my-2 px-2 py-1`}>Submit</button>
  )
}

export default SubmitButton