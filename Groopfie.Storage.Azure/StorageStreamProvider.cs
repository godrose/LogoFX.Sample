using System.IO;

namespace Groopfie.Storage.Azure
{    
    public class StorageStreamProvider : IStorageStreamProvider
    {
        public Stream GetImage(string imageName)
        {            
            var blobContainer = ContainerFactory.CreateCloudBlobContainer();
            var blob = blobContainer.GetBlockBlobReference(imageName);
            return blob.OpenWrite();              
        }
    }
}