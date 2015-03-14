using Microsoft.Win32;

namespace Sample.Client.Shell.Model
{
    public interface IUserInteractionService
    {
        string BrowseForFile(string title, string filter);
    }

    public class UserInteractionService : IUserInteractionService
    {
        public string BrowseForFile(string title, string filter)
        {
            var openFileDialog = new OpenFileDialog {Filter = filter, Title = title};
            var result = openFileDialog.ShowDialog();
            return result == true ? openFileDialog.FileName : null;
        }
    }
}
