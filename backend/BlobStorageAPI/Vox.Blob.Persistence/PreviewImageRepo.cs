using System;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using Vox.Blob.Persistence.Interfaces;

namespace Vox.Blob.Persistence;

public class PreviewImageRepo(IOptions<VoxBlobOptions> _options) : IPreviewImageRepo
{
    public async Task<byte[]> GetImg(long id)
    {
        return await ReadAsync(GetImgFileName(id));
    }

    public async Task SaveImg(byte[] data, long id)
    {
               await WriteAsync(data,GetImgFileName(id));

    }


      private string GetImgFileName(long id)
    {
        return $"preview-image-{id}.png";
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
