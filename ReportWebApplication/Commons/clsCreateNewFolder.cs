using System.IO;

namespace ReportWebApplication.Commons
{
    public class clsCreateNewFolder
    {
        public string CreatNewFolder(string path)
        {
            var folder = Directory.CreateDirectory(path);
            return folder.Name;
        }
    }
}