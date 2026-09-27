import Axios from "axios";

export const VoxelDataApi = Axios.create({
    baseURL: "https://vox-info-service.greensand-9e6ef676.germanywestcentral.azurecontainerapps.io",
})