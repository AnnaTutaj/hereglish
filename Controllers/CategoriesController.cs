using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hereglish.Controllers.Resources;
using Hereglish.Core.Models;
using Hereglish.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hereglish.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly HereglishDbContext context;
        private readonly IMapper mapper;
        public CategoriesController(HereglishDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet("api/categories")]
        public async Task<IEnumerable<CategoryResource>> GetCategories()
        {
            var categories = await context.Categories.Include(c => c.Subcategories).ToListAsync();

            return mapper.Map<List<Category>, List<CategoryResource>>(categories);
        }
    }
}