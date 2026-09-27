using System;

namespace Vox.Blob.Persistence.Interfaces;

public interface IVoxFileRepo
{

        public Task<byte[]> GetVoxFile(long id);

     public Task SaveVoxFile(byte[] data,long id);

}
