using System.Collections.Generic;
using System.Threading.Tasks;
using Hereglish.Models;

namespace Hereglish.Core
{
    public interface IWordRepository
    {
        Task<Word> GetWord(int id, bool includeRelated = true);
        Task<IEnumerable<Word>> GetWords();
        void Add(Word word);
        void Remove(Word word);

    }
}