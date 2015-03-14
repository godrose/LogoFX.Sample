using System.IO;

namespace Groopfie.Storage
{
    public interface IStorageStreamProvider
    {
        Stream GetImage(string imageName);
    }
}
