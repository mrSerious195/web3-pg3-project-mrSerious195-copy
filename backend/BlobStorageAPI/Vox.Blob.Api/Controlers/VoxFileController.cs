using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vox.Blob.Api.Contracts;
using Vox.Blob.Domein.Interfaces;

namespace Vox.Blob.Api.Controlers
{
    [Route("api/VoxFiles")]
    [ApiController]
     
    public class VoxFileController(IVoxFileService voxService) : ControllerBase
    {
        [HttpGet("{id}")]
        [Authorize(Policy ="User")]
         public async Task<ActionResult> GetById([FromRoute]long id)
        {
               
                   byte[]  voxfile=await voxService.GetVoxFile(id);
                  
                     return File(
            voxfile,
            "application/octet-stream",
            $"Vox-{id}.vox"
        );
        }
          [HttpPost]
          [Authorize(Policy ="m2m")]
        public async Task<ActionResult> UploadVoxelFiles([FromBody]FileRequestContract voxelContract)
        {
        await voxService.SaveVoxFile(Convert.FromBase64String(voxelContract.Base64),voxelContract.Id);

        return NoContent();
        }
    }

    public class VoxandImgRequestContract
    {
    }
}
