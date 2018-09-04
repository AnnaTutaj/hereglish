using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hereglish.Core;
using Hereglish.Core.Models;
using Microsoft.EntityFrameworkCore;
using Hereglish.Extensions;

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

        public async Task<QueryResult<Word>> GetWords(WordQuery queryObj)
        {
            var result = new QueryResult<Word>();

            var query = context.Words
            .Include(w => w.PartOfSpeech)
            .Include(w => w.Features)
                .ThenInclude(wf => wf.Feature)
            .Include(w => w.Subcategory)
                .ThenInclude(m => m.Category)
                .AsQueryable();

            query = query.ApplyFiltering(queryObj);

            var columnsMap = new Dictionary<string, Expression<Func<Word, object>>>()
            {
                ["category"] = w => w.Subcategory.Category.Name,
                ["subcategory"] = w => w.Subcategory.Name,
                ["name"] = w => w.Name,
                ["meaning"] = w => w.Meaning,
                ["createdAt"] = w => w.CreatedAt
            };
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
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