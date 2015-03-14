using System;
using Microsoft.WindowsAzure.Storage.Blob;
using NLog;

namespace Groopfie.Storage.Azure
{       
    //TODO: inject in runtime
    public class StorageInitializer : IStorageInitializer
    {
        async public void InitAsync()
        {
            var logger = LogManager.GetCurrentClassLogger();
            try
            {
                var container = ContainerFactory.CreateCloudBlobContainer();

                if (await container.CreateIfNotExistsAsync())
                {
                    await container.SetPermissionsAsync(
                        new BlobContainerPermissions
                        {
                            PublicAccess =
                                BlobContainerPublicAccessType.Blob
                        });
                    logger.Info("Successfully created Blob Storage Images Container and made it public");
            
                }
            }
            catch (Exception ex)
            {
                 logger.Error("Failure to Create or Configure images container in Blob Storage Service",ex);
            }
        }        
    }    
}
