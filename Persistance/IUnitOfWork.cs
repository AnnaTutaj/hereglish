using System.Threading.Tasks;

namespace Hereglish.Persistance
{

    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}