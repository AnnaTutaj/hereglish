using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hereglish.Controllers.Resources;
using Hereglish.Core;
using Hereglish.Core.Models;
using Hereglish.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hereglish.Controllers
{
    public class SubcategoriesController : Controller
    {
        private readonly IMapper mapper;
        private readonly ISubcategoryRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public SubcategoriesController(IMapper mapper, ISubcategoryRepository repository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost("api/subcategories")]
        public async Task<IActionResult> CreateSubcategory([FromBody] SaveSubcategoryResource subcategoryResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subcategory = mapper.Map<SaveSubcategoryResource, Subcategory>(subcategoryResource);

            repository.Add(subcategory);

            await unitOfWork.CompleteAsync();

            subcategory = await repository.GetSubcategoryAsync(subcategory.Id);

            var result = mapper.Map<Subcategory, SubcategoryResource>(subcategory);

            return Ok(result);
        }
    }
}