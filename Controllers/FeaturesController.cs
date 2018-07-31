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
    public class FeaturesController : Controller
    {
        private readonly HereglishDbContext context;
        private readonly IMapper mapper;
        public FeaturesController(HereglishDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet("api/features")]
        public async Task<IEnumerable<KeyValuePairResource>> GetFeature()
        {
            var features = await context.Features.ToListAsync();

            return mapper.Map<List<Feature>, List<KeyValuePairResource>>(features);
        }
    }
}