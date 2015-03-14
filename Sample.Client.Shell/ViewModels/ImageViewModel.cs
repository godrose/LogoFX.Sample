using Sample.Client.Shell.Model;

namespace Sample.Client.Shell.ViewModels
{
    public class ImageViewModel
    {        
        public ImageViewModel(ImageModel model)
        {
            Model = model;
        }

        public ImageModel Model { get; set; }
    }
}
