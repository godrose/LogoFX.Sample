using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Groopfie.Storage;

namespace Groopfie.Server.TestModule
{
    public class StorageMultipartStreamProvider : MultipartStreamProvider
    {
        private readonly IStorageStreamProvider _storageStreamProvider;

        public StorageMultipartStreamProvider(IStorageStreamProvider storageStreamProvider)
        {
            _storageStreamProvider = storageStreamProvider;
        }

        public string ImageName { get; private set; } 

        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            Stream stream = null;
            
            var contentDisposition = headers.ContentDisposition;
            if (contentDisposition != null && String.IsNullOrWhiteSpace(contentDisposition.FileName) == false)
            {
                ImageName = contentDisposition.FileName;
                stream = _storageStreamProvider.GetImage(ImageName);
            }
            return stream;
        }

    }
}
