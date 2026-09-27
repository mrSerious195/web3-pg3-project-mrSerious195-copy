using System;
using Vox.Blob.Domein.Interfaces;
using Vox.Blob.Persistence.Interfaces;

namespace Vox.Blob.Domain;

public class PreviewImageService(IPreviewImageRepo repo) : IPreviewImageService
{
    public async Task<byte[]> GetImg(long id)
    {
        return await repo.GetImg(id);
    }

    public async Task SaveImg(byte[] data, long id)
    {
         await repo.SaveImg(data,id);
    }
}
