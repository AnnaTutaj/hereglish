using System.Collections.Generic;
using System.Threading.Tasks;
using Hereglish.Core.Models;

namespace Hereglish.Core
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetPhotos(int wordId);
    }
}