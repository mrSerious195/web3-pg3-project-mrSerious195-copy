import Axios from "axios";

export const StripeApi = Axios.create({
    baseURL: "https://vox-payment-server.greensand-9e6ef676.germanywestcentral.azurecontainerapps.io",
})