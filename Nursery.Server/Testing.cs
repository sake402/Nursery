
namespace Nursery.Server
{
    public partial class Testing : IFileManager
    {
        public void SaveFile(string name, string path, string message)
        {
            File.WriteAllText(path, message);
        }
    }

    public class FileManagerUser
    {
        IFileManager fileio;

        public FileManagerUser(IFileManager fileio)
        {
            this.fileio = fileio;
            fileio.SaveFile();
        }
    }
}
