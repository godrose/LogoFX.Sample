using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sample.Client.Shell.Model
{
    public interface IDataService
    {
        IEnumerable<ImageModel> ImageModels { get; }
        void AddImage(string path);
    }

    public class DataService : IDataService
    {
        private ObservableCollection<ImageModel> _imageModels;
        private ObservableCollection<ImageModel> ImageModels
        {
            get { return _imageModels ?? (_imageModels = new ObservableCollection<ImageModel>()); }
        }

        public void AddImage(string path)
        {
            ImageModels.Add(new ImageModel {ImagePath = path});
        }

        IEnumerable<ImageModel> IDataService.ImageModels
        {
            get { return ImageModels; }
        }
    }    
}
