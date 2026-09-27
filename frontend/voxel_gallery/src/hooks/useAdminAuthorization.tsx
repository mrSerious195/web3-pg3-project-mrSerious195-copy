
import { useAuth } from 'react-oidc-context';


const useAdminAuthorization = () => {
const {user,isAuthenticated}=useAuth()
    if(!isAuthenticated||!user){
            return
          }
          if(!user.profile){
           return
          }
          console.log(`userToken=${user?.access_token}`)
          return(user.profile.role==="admin")
}

export default useAdminAuthorization