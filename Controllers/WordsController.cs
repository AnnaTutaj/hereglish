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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWord(int id)
        {
            var word = await context.Words
            .Include(w => w.Features)
                .ThenInclude(wf => wf.Feature)
            .Include(w => w.Subcategory)
                .ThenInclude(m => m.Category)
            .SingleOrDefaultAsync(w => w.Id == id);

            if (word == null)
            {
                return NotFound();
            }

            var wordResource = mapper.Map<Word, WordResource>(word);

            return Ok(wordResource);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWord([FromBody] SaveWordResource wordResource)
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

            var word = mapper.Map<SaveWordResource, Word>(wordResource);

            word.CreatedAt = DateTime.Now;
            word.UpdatedAt = null;

            context.Words.Add(word);
            await context.SaveChangesAsync();

            var result = mapper.Map<Word, SaveWordResource>(word);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWord(int id, [FromBody] SaveWordResource wordResource)
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

            if (word == null)
            {
                return NotFound();
            }

            mapper.Map<SaveWordResource, Word>(wordResource, word);

            word.UpdatedAt = DateTime.Now; ;

            await context.SaveChangesAsync();

            var result = mapper.Map<Word, SaveWordResource>(word);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWord(int id)
        {
            var word = await context.Words.FindAsync(id);

            if (word == null)
            {
                return NotFound();
            }

            context.Remove(word);
            await context.SaveChangesAsync();

            return Ok(id);
        }
    }
}