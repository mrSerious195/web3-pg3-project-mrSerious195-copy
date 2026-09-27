using System;

namespace Vox.Blob.Persistence;

public class VoxBlobOptions
{
  public required string ConnectionString { get; set; }
    public required string ContainerName { get; set; }
}
