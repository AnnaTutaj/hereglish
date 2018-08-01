using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<IEnumerable<Word>> GetWords(WordQuery queryObj)
        {
            var query = context.Words
            .Include(w => w.PartOfSpeech)
            .Include(w => w.Features)
                .ThenInclude(wf => wf.Feature)
            .Include(w => w.Subcategory)
                .ThenInclude(m => m.Category)
                .AsQueryable();

            if (queryObj.CategoryId.HasValue)
            {
                query = query.Where(w => w.Subcategory.CategoryId == queryObj.CategoryId.Value);
            }

            if (queryObj.SubcategoryId.HasValue)
            {
                query = query.Where(w => w.SubcategoryId == queryObj.SubcategoryId.Value);
            }

            if (queryObj.PartOfSpeechId.HasValue)
            {
                query = query.Where(w => w.PartOfSpeechId == queryObj.PartOfSpeechId.Value);
            }

            if (!String.IsNullOrEmpty(queryObj.Name))
            {
                query = query.Where(w => w.Name.Contains(queryObj.Name));
            }

            if (!String.IsNullOrEmpty(queryObj.Meaning))
            {
                query = query.Where(w => w.Meaning.Contains(queryObj.Meaning));
            }

            if (!String.IsNullOrEmpty(queryObj.Example))
            {
                query = query.Where(w => w.Example.Contains(queryObj.Example));
            }

            var columnsMap = new Dictionary<string, Expression<Func<Word, object>>>()
            {
                ["category"] = w => w.Subcategory.Category.Name,
                ["subcategory"] = v => v.Subcategory.Name,
                ["name"] = v => v.Name,
                ["meaning"] = v => v.Meaning
            };

            query = ApplyOrdering(queryObj, query, columnsMap);

            return await query.ToListAsync();
        }

        private IQueryable<Word> ApplyOrdering(WordQuery queryObj, IQueryable<Word> query, Dictionary<string, Expression<Func<Word, object>>> columnsMap)
        {
            if (queryObj.IsSortAscending)
            {
                return query = query.OrderBy(columnsMap[queryObj.SortBy]);
            }
            else
            {
                return query = query.OrderByDescending(columnsMap[queryObj.SortBy]);
            }
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