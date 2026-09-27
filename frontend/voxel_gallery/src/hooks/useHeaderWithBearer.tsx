
import { useAuth } from "react-oidc-context";

export const useHeaderWithBearer = () => {
    const {user} =useAuth()
  const headers = {
            Authorization: `Bearer ${user?.access_token}`,
          }

  return {headers};
};