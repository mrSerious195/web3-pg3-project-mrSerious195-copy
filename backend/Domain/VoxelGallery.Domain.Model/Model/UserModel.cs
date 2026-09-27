namespace Vox.IdentityServer.Domein;

    public class UserModel
    {
        public long? Id { get; set; }

        public required string IdentityServerId { get; set; } = null!;

        public required string Name { get; set; } = null!;

        public required DateTime RegistrationTime { get; set; }

        public string? Countryflag{get;set;}

        public string? Country{get;set;}

          public bool IsDonor{get;set;}
    }

    
