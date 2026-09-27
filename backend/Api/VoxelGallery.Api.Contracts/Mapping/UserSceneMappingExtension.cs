using Vox.IdentityServer.Domein;

namespace VoxelGallery.Api.Contracts.Mapping;

public static class UserSceneMappingExtension
{
  

    public static UserModel AsModel(this UserequestContract contract)
    {
     return new UserModel
     {
         Name=contract.Name,
         IdentityServerId = contract.IdentityServerId,
         RegistrationTime = DateTime.Now,
         Country = contract.Country
      
     } ;
    }


public static UserResponseContract AsContract(this UserModel model,string? currentUserID)
    {
        return new UserResponseContract(){
            RegistrationTime=model.RegistrationTime,
            CountryName=model.Country,
            CountryFlag=model.Countryflag,
            Name=model.Name,
            IsUser=currentUserID!=null&&(model.IdentityServerId==currentUserID),
            IsDonor=model.IsDonor
        };
        
    }
}
