using System.Threading.Tasks;
using Hereglish.Models;

namespace Hereglish.Persistance
{
    public interface IWordRepository
    {
        Task<Word> GetWord(int id);

    }
}