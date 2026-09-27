using System;

namespace Vox.Blob.Domein.Interfaces;

public interface IVoxFileService
{
    public Task<byte[]> GetVoxFile(long id);

    public Task SaveVoxFile(byte[] data,long id);
}
