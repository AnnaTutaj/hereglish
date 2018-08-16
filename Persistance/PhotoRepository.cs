using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Hereglish.Core;
using Hereglish.Core.Models;

namespace Hereglish.Persistance
{
  public class PhotoRepository : IPhotoRepository
  {
    private readonly HereglishDbContext context;
    public PhotoRepository(HereglishDbContext context)
    {
      this.context = context;
    }
    public async Task<IEnumerable<Photo>> GetPhotos(int wordId)
    {
      return await context.Photos
        .Where(p => p.WordId == wordId)
        .ToListAsync();
    }
  }
}