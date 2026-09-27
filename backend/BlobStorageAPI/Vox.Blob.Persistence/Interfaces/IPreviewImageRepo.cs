using System;

namespace Vox.Blob.Persistence.Interfaces;

public interface IPreviewImageRepo
{

    public Task<byte[]> GetImg(long id);

     public Task SaveImg(byte[] data,long id);

}
