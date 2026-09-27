using System;

namespace Vox.Blob.Domein.Interfaces;

public interface IPreviewImageService
{
    public Task<byte[]> GetImg(long id);

     public Task SaveImg(byte[] data,long id);
}
