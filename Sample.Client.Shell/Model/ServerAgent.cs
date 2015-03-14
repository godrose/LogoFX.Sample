using System;

namespace Sample.Client.Shell.Model
{
    public interface IServerAgent
    {
        void UploadImage(string imagePath, Action onComplete, Action<Exception> onError);
    }

    public class ServerAgent : IServerAgent
    {
        private readonly IFileUploader _fileUploader;

        public ServerAgent(IFileUploader fileUploader)
        {
            _fileUploader = fileUploader;
        }

        public void UploadImage(string imagePath, Action onComplete, Action<Exception> onError)
        {            
            _fileUploader.UploadFile(imagePath, "http://localhost:5002/api/Upload/UploadImage");
            onComplete();
        }
    }    
}
