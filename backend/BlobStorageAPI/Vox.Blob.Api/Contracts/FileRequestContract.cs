using System;
using System.ComponentModel.DataAnnotations;

namespace Vox.Blob.Api.Contracts;

public class FileRequestContract
{
   [Required]
    public required string Base64{get;set;}

    public required long Id{get;set;}
}
