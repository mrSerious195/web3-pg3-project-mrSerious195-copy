using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Vox.IdentityServer.Domein.InterFaces;
using VoxelGallery.Api.Contracts;

namespace VoxelGallery.Api.Controllers
{
    [Route("api/donations")]
    [ApiController]
    public class DonationControler(IUserService userService) : ControllerBase
    {
        [HttpPost]
        [Authorize("m2m")]
        public async Task<ActionResult> Donate([FromBody]DonationRequestContract contract)
        {
           var model = await userService.SetDonation(contract.CustomerId,true);
            if (model == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
