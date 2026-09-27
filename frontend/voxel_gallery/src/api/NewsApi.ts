import Axios from "axios";

export const NewsApi = Axios.create({
    baseURL: "https://vox-news-server.greensand-9e6ef676.germanywestcentral.azurecontainerapps.io",
})