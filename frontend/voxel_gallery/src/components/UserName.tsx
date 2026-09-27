
import type { UserResponseContract } from '../api/ApiResponses/UserResponseContract'
interface props{
    user?:UserResponseContract
}
const UserName = ({user}:props) => {
  return (
            <p className={user&&user.isDonor?'mx-2 text-green-800':'mx-2'}>{user&&user.name} {user&&user.countryFlag}</p>
       
  )
}

export default UserName