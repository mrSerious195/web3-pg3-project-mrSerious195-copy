using System.ComponentModel.DataAnnotations;

namespace VoxelGallery.Api.Contracts;

public class UserequestContract
{
   [Required]
     public  string IdentityServerId {get;set;}
     [Required]
     [MaxLength(256)]
      public required string Name {get;set;}

  [Required]
   [MaxLength(256)]
      public required string Country {get;set;}
    
}
