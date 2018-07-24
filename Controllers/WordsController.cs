using AutoMapper;
using Hereglish.Controllers.Resources;
using Hereglish.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hereglish.Controllers
{
    [Route("api/words")]
    public class WordsController : Controller
    {
        private readonly IMapper mapper;
        public WordsController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateWord([FromBody] WordResource wordResource)
        {
            var word = mapper.Map<WordResource, Word>(wordResource);

            return Ok(word);
        }

    }
}