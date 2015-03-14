using System.Linq;
using System.Windows.Input;
using LogoFX.Infra;
using LogoFX.ViewModels;
using Sample.Client.Shell.Model;
using Sample.Client.Shell.ViewModels;

namespace Sample.Client.Shell
{
    public class ShellViewModel
    {
        private readonly IDataService _dataService;
        private readonly IUserInteractionService _userInteractionService;
        private readonly IServerAgent _serverAgent;

        public ShellViewModel(IDataService dataService, IUserInteractionService userInteractionService, IServerAgent serverAgent)
        {
            _dataService = dataService;
            _userInteractionService = userInteractionService;
            _serverAgent = serverAgent;
        }

        private ActionCommand _addFileCommand;

        public ICommand AddFileCommand
        {
            get
            {
                return _addFileCommand ?? (_addFileCommand = ActionCommand.When(() => true).Do(() =>
                {
                    var path = _userInteractionService.BrowseForFile("open image", "Image files|*.bmp;*.jpg;*.png");
                    _dataService.AddImage(path);
                }));
            }
        }

        private ActionCommand _uploadCommand;
        public ICommand UploadCommand
        {
            get
            {
                return _uploadCommand ?? (_uploadCommand = ActionCommand.When(() => true).Do(() =>
                {
                    foreach (var fileItem in FileItems.OfType<UploadFileViewModel>())
                    {
                        var item = fileItem;
                        _serverAgent.UploadImage(fileItem.ImageViewModel.Model.ImagePath, () =>
                        {
                            item.IsUploaded = true;
                        },
                            e => { });
                    }
                }));
            }
        }

        private WrappingCollection _fileItems;
        public WrappingCollection FileItems
        {
            get { return _fileItems ?? (_fileItems = CreateFileItems()); }
        }

        private WrappingCollection CreateFileItems()
        {
            var wc = new WrappingCollection {FactoryMethod = r => new UploadFileViewModel((ImageModel)r) };
            wc.AddSource(_dataService.ImageModels);
            return wc;
        }
    }

    
}
