import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'

import DefaultLayout from "./layouts/DefaultLayout.tsx";
import { createBrowserRouter, RouterProvider } from "react-router";

import FrontPage from "./pages/FrontPage.tsx";
import ModelViewPage from "./pages/ModelViewPage.tsx";
import UploadPage from './pages/UploadPage.tsx';
import { AuthProvider } from 'react-oidc-context';
import type { UserManagerSettings } from 'oidc-client-ts';
import DonationsuccessPage from './pages/DonationsuccessPage.tsx';
import LoggedOutPage from './pages/LoggedOutPage.tsx';
import DarkmodeProvider from './contexts/DarkModeContext.tsx';
const browserRouter = createBrowserRouter([
  {
    element: <DefaultLayout />,
    children: [
      {
        path: "/",
        element: <FrontPage />
      },
      {
        path: "/models/:modelId",
        element: <ModelViewPage />
      },
      {
        path: "/Donationsuccess",
        element: <DonationsuccessPage />
      },
      {
        path: "/upload",
        element: <UploadPage />
      }

    ]
  }
]);
//  authority: "https://voxidentityservice.greensand-9e6ef676.germanywestcentral.azurecontainerapps.io/",
//
const settings: UserManagerSettings = {
  authority: "https://voxidentityservice.greensand-9e6ef676.germanywestcentral.azurecontainerapps.io",
  client_id: "webapp-client",
  client_secret: "my-webapp-secret",
  redirect_uri: "https://hogenttaskvoxfilestorage.z1.web.core.windows.net",
  post_logout_redirect_uri:"https://hogenttaskvoxfilestorage.z1.web.core.windows.net",
  response_type: "code",
  scope: "openid profile roles VoxelGallery.Api",
  monitorSession: true,
  loadUserInfo: true,
};

const onSigninCallback = (): void => {
  window.history.replaceState({}, document.title, window.location.pathname);
};


createRoot(document.getElementById('root')!).render(
  <StrictMode>

    <AuthProvider {...settings} onSigninCallback={onSigninCallback}>
      <LoggedOutPage>
          <DarkmodeProvider>
            <RouterProvider router={browserRouter} />
          </DarkmodeProvider>
      </LoggedOutPage>
    </AuthProvider>
  </StrictMode>,
)
