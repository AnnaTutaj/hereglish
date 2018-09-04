using System.Threading.Tasks;
using Hereglish.Core.Models;
using Microsoft.AspNetCore.Http;

namespace Hereglish.Core
{
    public interface IPhotoService
    {
        Task<Photo> UploadPhoto(Word word, IFormFile file, string uploadsFolderPath);
    }
}