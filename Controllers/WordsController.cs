using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hereglish.Controllers.Resources;
using Hereglish.Core.Models;
using Hereglish.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Hereglish.Controllers
{
    [Route("api/words")]
    public class WordsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IWordRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public WordsController(IMapper mapper, IWordRepository repository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWord(int id)
        {
            var word = await repository.GetWord(id);

            if (word == null)
            {
                return NotFound();
            }

            var wordResource = mapper.Map<Word, WordResource>(word);

            return Ok(wordResource);
        }

        
        [HttpGet]
        public async Task<IEnumerable<WordResource>> GetWords()
        {
            var words = await repository.GetWords();
            return mapper.Map<IEnumerable<Word>, IEnumerable<WordResource>>(words);
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

            repository.Add(word);

            await unitOfWork.CompleteAsync();

            word = await repository.GetWord(word.Id);

            var result = mapper.Map<Word, WordResource>(word);

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

            var word = await repository.GetWord(id);

            if (word == null)
            {
                return NotFound();
            }

            mapper.Map<SaveWordResource, Word>(wordResource, word);

            word.UpdatedAt = DateTime.Now; ;

            await unitOfWork.CompleteAsync();

            word = await repository.GetWord(word.Id);

            var result = mapper.Map<Word, WordResource>(word);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWord(int id)
        {
            var word = await repository.GetWord(id, includeRelated: false);

            if (word == null)
            {
                return NotFound();
            }

            repository.Remove(word);
            await unitOfWork.CompleteAsync();

            return Ok(id);
        }
    }
}