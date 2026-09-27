using System;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using Vox.Blob.Persistence.Interfaces;

namespace Vox.Blob.Persistence;

public class VoxFileRepo(IOptions<VoxBlobOptions> _options) : IVoxFileRepo
{
    public async Task<byte[]> GetVoxFile(long id)
    {
        return await ReadAsync(GetVoxFileName(id));
    }

    public async Task SaveVoxFile(byte[] data, long id)
    {
        await WriteAsync(data,GetVoxFileName(id));
    }

  private string GetVoxFileName(long id)
    {
        return $"VoxFile-{id}.vox";
    }
        private async Task<byte[]> ReadAsync(string fileName)
    {
        var blob = await GetBlobClient(fileName).DownloadContentAsync();
        return blob.Value.Content.ToArray();
    }

    private Task WriteAsync(byte[] content, string fileName)
    {
        return GetBlobClient(fileName).UploadAsync(new BinaryData(content));
    }

     private BlobClient GetBlobClient(string fileName)
{
    var client = new BlobServiceClient(_options.Value.ConnectionString); 
    var containerClient = client.GetBlobContainerClient(_options.Value.ContainerName); 
    return containerClient.GetBlobClient(fileName); 
}
}
