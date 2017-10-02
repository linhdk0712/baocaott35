using System.IO;

namespace ReportWebApplication.Commons
{
    public class clsDeleteFileTemp
    {
        public bool DeleteFileOnTemp(string fileName, string path)
        {
            var status = false;
            var dir = new DirectoryInfo(path);
            var files = dir.GetFiles();
            foreach (var item in files)
            {
                if (item.Name.StartsWith(fileName))
                    item.Delete();
                status = true;
            }
            return status;
        }
    }
}