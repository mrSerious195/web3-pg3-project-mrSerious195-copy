using System.ComponentModel.DataAnnotations;

namespace VoxIdentityServer.Pages.Register;

public class RegisterModel
{
[Required]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }

     [Required]
    public string? Email { get; set; }
    public string? ReturnUrl { get; set; }

    public string? Country{get;set;}

    public string? Button { get; set; }

}
