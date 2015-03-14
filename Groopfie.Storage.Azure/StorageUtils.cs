using System;
using Microsoft.WindowsAzure.Storage;

namespace Groopfie.Storage.Azure
{
    internal class StorageUtils
    {
        private const string AccountName = "fivewes";
        private const string AccountKey = "7mlc+u36yg1CR6YAp3WZNG/lKnsuq0J7so/5fug3q2WeZV8Nrb40wOvlPx3wsCfPABgZrtAsLfNQZAjBaS0cZw==";
        public static CloudStorageAccount GetStorageAccount()
        {
            var connectionString = String.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", AccountName, AccountKey);
            return CloudStorageAccount.Parse(connectionString);
        }
    }
}
