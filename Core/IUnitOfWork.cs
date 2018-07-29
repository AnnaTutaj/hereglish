using System.Threading.Tasks;

namespace Hereglish.Core
{

    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}