using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hereglish.Core;
using Hereglish.Core.Models;
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

        public async Task<Word> GetWord(int id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Words.FindAsync(id);
            }

            return await context.Words
                .Include(w => w.PartOfSpeech)
                .Include(w => w.Features)
                    .ThenInclude(wf => wf.Feature)
                .Include(w => w.Subcategory)
                    .ThenInclude(m => m.Category)
                .SingleOrDefaultAsync(w => w.Id == id);
        }

        public async Task<IEnumerable<Word>> GetWords(Filter filter)
        {
            var query = context.Words
            .Include(w => w.PartOfSpeech)
            .Include(w => w.Features)
                .ThenInclude(wf => wf.Feature)
            .Include(w => w.Subcategory)
                .ThenInclude(m => m.Category)
                .AsQueryable();

            if (filter.CategoryId.HasValue)
            {
                query = query.Where(w => w.Subcategory.CategoryId == filter.CategoryId.Value);
            }

            if (filter.SubcategoryId.HasValue)
            {
                query = query.Where(w => w.SubcategoryId == filter.SubcategoryId.Value);
            }

            if (filter.PartOfSpeechId.HasValue)
            {
                query = query.Where(w => w.PartOfSpeechId == filter.PartOfSpeechId.Value);
            }

            if (!String.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(w => w.Name.Contains(filter.Name));
            }

            if (!String.IsNullOrEmpty(filter.Meaning))
            {
                query = query.Where(w => w.Meaning.Contains(filter.Meaning));
            }

            if (!String.IsNullOrEmpty(filter.Example))
            {
                query = query.Where(w => w.Example.Contains(filter.Example));
            }

            return await query.ToListAsync();
        }

        public void Add(Word word)
        {
            context.Words.Add(word);
        }

        public void Remove(Word word)
        {
            context.Words.Remove(word);
        }

    }
}