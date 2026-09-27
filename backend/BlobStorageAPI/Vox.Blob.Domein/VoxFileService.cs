using System;
using Vox.Blob.Domein.Interfaces;
using Vox.Blob.Persistence.Interfaces;

namespace Vox.Blob.Domain;

public class VoxFileService(IVoxFileRepo repo) : IVoxFileService
{
    public async Task<byte[]> GetVoxFile(long id)
    {
        return await repo.GetVoxFile(id);
    }

    public async Task SaveVoxFile(byte[] data, long id)
    {
         await repo.SaveVoxFile(data,id);
    }
}
