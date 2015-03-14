using Caliburn.Micro;
using Sample.Client.Shell.Model;

namespace Sample.Client.Shell.ViewModels
{
    public class UploadFileViewModel : PropertyChangedBase
    {
        public UploadFileViewModel(ImageModel model)
        {
            ImageViewModel = new ImageViewModel(model);
        }

        private bool _isUploaded;
        public bool IsUploaded
        {
            get { return _isUploaded; }
            set
            {
                if (value == _isUploaded)
                {
                    return;
                }
                _isUploaded = value;
                NotifyOfPropertyChange(() => IsUploaded);
            }
        }

        public ImageViewModel ImageViewModel { get; private set; }
    }
}