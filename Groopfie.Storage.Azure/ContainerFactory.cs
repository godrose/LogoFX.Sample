using Microsoft.WindowsAzure.Storage.Blob;

namespace Groopfie.Storage.Azure
{
    internal static class ContainerFactory
    {
        internal static CloudBlobContainer CreateCloudBlobContainer()
        {
            var storageAccount = StorageUtils.GetStorageAccount();
            var blobClient = storageAccount.CreateCloudBlobClient();
            return blobClient.GetContainerReference("images");
        }
    }
}
