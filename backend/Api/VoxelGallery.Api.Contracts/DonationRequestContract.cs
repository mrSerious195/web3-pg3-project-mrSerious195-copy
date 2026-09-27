using System;
using System.ComponentModel.DataAnnotations;

namespace VoxelGallery.Api.Contracts;

public class DonationRequestContract
{ 
        [Required]
    public required string CustomerId{get;set;}

}
