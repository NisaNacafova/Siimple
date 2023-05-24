using Siimple.Models;
using System.ComponentModel;

namespace Siimple.Extensions
{
    public static class FileExtension
    {
        public static bool CheckType(this IFormFile file, string type)
        {
            return file.ContentType.Contains(type);
        }
        public static bool CheckSize(this IFormFile file, double kb)
        {
            return file.ContentType.Length / 1024 > kb;
        }
        public static async Task<string> Upload(this IFormFile file, params string[] folders)
        {
            string FileName = Guid.NewGuid().ToString() + file.FileName;
            string pathFolders = Path.Combine(folders);
            string path = Path.Combine(pathFolders,FileName);
            using (FileStream filestream = new FileStream(path, FileMode.CreateNew))
            {
                await file.CopyToAsync(filestream);
            }
            return FileName;
        }
    }
}
