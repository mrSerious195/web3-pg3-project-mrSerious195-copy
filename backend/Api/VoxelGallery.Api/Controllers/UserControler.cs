using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vox.IdentityServer.Domein.InterFaces;
using VoxelGallery.Api.Contracts;
using VoxelGallery.Api.Contracts.Mapping;

namespace VoxelGallery.Api.Controllers;

[ApiController]
[Route("api/Users")]
public class UserControler(IUserService userService) : ControllerBase
{
     [HttpPost]
     [Authorize("m2m")]
      public async Task<ActionResult> MakeUser([FromBody]UserequestContract voxelContract)
    {

     var user =  await userService.MakeUser(voxelContract.AsModel());
        return Ok(user.AsContract(null));

    }
}
