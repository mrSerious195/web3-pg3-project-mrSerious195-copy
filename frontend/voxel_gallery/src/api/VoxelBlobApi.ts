import Axios from "axios";

export const VoxelBlobApi = Axios.create({
    baseURL: "https://vox-blob-service.greensand-9e6ef676.germanywestcentral.azurecontainerapps.io",
})