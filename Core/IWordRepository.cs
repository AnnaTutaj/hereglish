using System.Collections.Generic;
using System.Threading.Tasks;
using Hereglish.Core.Models;

namespace Hereglish.Core
{
    public interface IWordRepository
    {
        Task<Word> GetWord(int id, bool includeRelated = true);
        Task<QueryResult<Word>> GetWords(WordQuery filter);
        void Add(Word word);
        void Remove(Word word);

    }
}