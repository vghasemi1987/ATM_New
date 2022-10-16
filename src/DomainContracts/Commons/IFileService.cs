using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DomainContracts.Commons
{
    public interface IFileService
    {
        Task SaveFileToServerAsync(string pathe, IFormFile postedFile);
        void DeleteFileFromServer(string filePathe);
        void DeleteFileListFromServer(List<string> fileList, string location);
        bool FileExist(string filePathe);
    }
}