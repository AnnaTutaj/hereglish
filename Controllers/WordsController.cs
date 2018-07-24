using System;
using System.Threading.Tasks;
using AutoMapper;
using Hereglish.Controllers.Resources;
using Hereglish.Models;
using Hereglish.Persistance;
using Microsoft.AspNetCore.Mvc;

namespace Hereglish.Controllers
{
    [Route("api/words")]
    public class WordsController : Controller
    {
        private readonly IMapper mapper;
        private readonly HereglishDbContext context;

        public WordsController(IMapper mapper, HereglishDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWord([FromBody] WordResource wordResource)
        {
            var word = mapper.Map<WordResource, Word>(wordResource);

            word.CreatedAt = DateTime.Now;
            word.UpdatedAt = null;

            context.Words.Add(word);
            await context.SaveChangesAsync();

            var result = mapper.Map<Word, WordResource>(word);

            return Ok(result);
        }

    }
}