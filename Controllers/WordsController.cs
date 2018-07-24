using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hereglish.Controllers.Resources;
using Hereglish.Models;
using Hereglish.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commasCount = wordResource.Meaning.TakeWhile(c => c == ',').Count();
            var limitOfCommas = 3;

            if (commasCount > limitOfCommas)
            {
                ModelState.AddModelError("error", "Cannot add a word with more than three commas in meaning");
                return BadRequest(ModelState);
            }

            var word = mapper.Map<WordResource, Word>(wordResource);

            word.CreatedAt = DateTime.Now;
            word.UpdatedAt = null;

            context.Words.Add(word);
            await context.SaveChangesAsync();

            var result = mapper.Map<Word, WordResource>(word);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWord(int id, [FromBody] WordResource wordResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commasCount = wordResource.Meaning.TakeWhile(c => c == ',').Count();
            var limitOfCommas = 3;

            if (commasCount > limitOfCommas)
            {
                ModelState.AddModelError("error", "Cannot add a word with more than three commas in meaning");
                return BadRequest(ModelState);
            }

            var word = await context.Words.Include(w => w.Features).SingleOrDefaultAsync(v => v.Id == id);
            mapper.Map<WordResource, Word>(wordResource, word);

            word.UpdatedAt = DateTime.Now; ;

            await context.SaveChangesAsync();

            var result = mapper.Map<Word, WordResource>(word);

            return Ok(result);
        }


    }
}