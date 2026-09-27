using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vox.Blob.Api.Contracts;
using Vox.Blob.Domein.Interfaces;

namespace Vox.Blob.Api.Controlers
{
    [Route("api/PreviewImages")]
    [ApiController]
    public class VoxPreviewImageController(IPreviewImageService ImgService) : ControllerBase
    {
          [HttpGet("{id}")]
           [Authorize(Policy ="User")]
         public async Task<ActionResult> GetById([FromRoute]long id)
        {
                  
                    var img= await ImgService.GetImg(id);
                   
               return File(
            img,
            "img/png",
            $"Vox-preview-{id}.png"
        );
        }
          [HttpPost]
        [Authorize(Policy ="m2m")]
        public async Task<ActionResult> UploadVoxelFiles([FromBody]FileRequestContract voxelContract)
        {

        await ImgService.SaveImg(Convert.FromBase64String(voxelContract.Base64),voxelContract.Id);

        return NoContent();
        }
    }
}
