using System.Collections.Generic;
using System.Threading.Tasks;
using Hereglish.Core.Models;

namespace Hereglish.Core
{
    public interface ISubcategoryRepository
    {
        Task<Subcategory> GetSubcategoryAsync(int id);
        void Add(Subcategory subcategory);
    }
}