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
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly HereglishDbContext context;

        public SubcategoryRepository(HereglishDbContext context)
        {
            this.context = context;
        }

        public void Add(Subcategory subcategory)
        {
            context.Subcategories.Add(subcategory);
        }

        public async Task<Subcategory> GetSubcategoryAsync(int id)
        {
            return await context.Subcategories.FindAsync(id);
        }
    }
}