using System;

namespace VoxelGallery.Api.Contracts;

public class UserResponseContract
{
     public string Name {get;set;}

      public string CountryName {get;set;}

       public string CountryFlag {get;set;}
 public required DateTime RegistrationTime { get; set; }
      public bool IsUser{get;set;}

        public bool IsDonor{get;set;}

}
