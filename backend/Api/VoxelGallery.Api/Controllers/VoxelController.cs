using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VoxelGallery.Api.Contracts;
using VoxelGallery.Api.Contracts.Mapping;
using VoxelGallery.Domain.Interfaces;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Extensions.Primitives;
using VoxelGallery.Persistence.DataModel.Entities;
namespace VoxelGallery.Api.Controllers;

    [Route("api/voxels")]
    [ApiController]
    public class VoxelController(IVoxelService Voxelservice)  : ControllerBase
    {
        [Authorize(Policy ="User")]
        [HttpGet]
    public async Task<ActionResult<List<VoxelResponseContract>>> GetPage([FromQuery] int page = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] string? name = null)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
        var allVoxelScenesAsModels = await Voxelservice.GetAllVoxelScenes(page,pageSize,name,userId);
        var allVoxelContracts = allVoxelScenesAsModels.Select(x=>x.AsContract(userId)).ToList();

        StringValues authHeaders = string.Empty;

        return Ok(allVoxelContracts);
    }

        [Authorize(Policy ="User")]
        [HttpGet("favorites")]
    public async Task<ActionResult<List<VoxelResponseContract>>> GetFavorites([FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
        var allVoxelScenesAsModels = await Voxelservice.GetFavorites(page,pageSize,userId);
        var allVoxelContracts = allVoxelScenesAsModels.Select(x=>x.AsContract(userId)).ToList();

        StringValues authHeaders = string.Empty;

        return Ok(allVoxelContracts);
    }



    [Authorize(Policy ="User")]
         [HttpGet("{id}")]
    public async Task<ActionResult<VoxelResponseContract>> GetById([FromRoute]long id)
    {   
         String userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
        
        var voxelmodel = await Voxelservice.GetVoxelSceneById(id,userId);
         if (voxelmodel is null)
            return NotFound();
  var voxelContract = voxelmodel.AsContract(userId);
  
   
   
        return Ok(voxelContract);
    }

      [Authorize(Policy ="User")]
         [HttpGet("{id}/pdf")]
    public async Task<IActionResult> Getpdf([FromRoute]long id)
    {
        
        var pdf = await Voxelservice.GetVoxelPdfById(id);
         if (pdf is null)
            return NotFound();

    
   
        return  File(
            pdf,
            "application/pdf",
            $"Vox-{id}.pdf"
        );
    }




      [Authorize(Policy ="User")]
      [HttpPost]
      public async Task<ActionResult<VoxelResponseContract>> UploadVoxelScene([FromBody]VoxelRequestContract voxelContract)
    {
        //TODO CHECK if the img and voxeldata are valid
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
        var model = voxelContract.AsModel();
        model.UserDatabaseId=userId;
       var result =await Voxelservice.AddVoxelScene(model);
        return Ok(result.AsContract(userId));

    }

          [Authorize(Policy ="User")]
      [HttpDelete("{SceneId}")]
           public async Task<ActionResult> DeleteVoxelScene([FromRoute]long SceneId)
        {
           var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
           if(!await Voxelservice.IsThisSceneOwner(SceneId, userId))
        {
            return Forbid();
        }
         await Voxelservice.DeleteVoxelScene(SceneId);
        return Ok();    
        }


          [Authorize(Policy ="User")]
      [HttpPut]
           public async Task<ActionResult<VoxelResponseContract>> EdditVoxelScene([FromBody]VoxelRequestContract Scene)
        {
           var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
           if(!await Voxelservice.IsThisSceneOwner(Scene.Id.Value, userId))
        {
            return Forbid();
        }
        var ret = await Voxelservice.EdditScene(Scene.AsModel(),userId);
        var voxelContract = ret.AsContract(userId);
  
        return Ok(voxelContract);    
        }
}

