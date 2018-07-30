using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hereglish.Controllers.Resources;
using Hereglish.Models;
using Hereglish.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hereglish.Controllers
{
    public class PartsOfSpeechController : Controller
    {
        private readonly HereglishDbContext context;
        private readonly IMapper mapper;
        public PartsOfSpeechController(HereglishDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet("api/parts-of-speech")]
        public async Task<IEnumerable<KeyValuePairResource>> GetFeature()
        {
            var partsOfSpeech = await context.PartsOfSpeech.ToListAsync();

            return mapper.Map<List<PartOfSpeech>, List<KeyValuePairResource>>(partsOfSpeech);
        }
    }
}