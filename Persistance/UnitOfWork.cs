using System.Threading.Tasks;
using Hereglish.Core;

namespace Hereglish.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HereglishDbContext context;

        public UnitOfWork(HereglishDbContext context)
        {
            this.context = context;
        }
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}