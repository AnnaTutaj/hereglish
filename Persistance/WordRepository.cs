using System.Threading.Tasks;
using Hereglish.Models;
using Microsoft.EntityFrameworkCore;

namespace Hereglish.Persistance
{
    public class WordRepository : IWordRepository
    {
        private readonly HereglishDbContext context;

        public WordRepository(HereglishDbContext context)
        {
            this.context = context;
        }

        public async Task<Word> GetWord (int id){
        return await context.Words
            .Include(w => w.Features)
                .ThenInclude(wf => wf.Feature)
            .Include(w => w.Subcategory)
                .ThenInclude(m => m.Category)
            .SingleOrDefaultAsync(w => w.Id == id);
        }
        
    }
}