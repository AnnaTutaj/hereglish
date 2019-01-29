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

        [HttpGet("api/flat-features")]
        public async Task<IEnumerable<KeyValuePairResource>> GetFlatFeatures()
        {
            var features = await context.Features.ToListAsync();

            return mapper.Map<List<Feature>, List<KeyValuePairResource>>(features);
        }

        [HttpGet("api/features")]
        public async Task<IEnumerable<FeatureResource>> GetFeatures()
        {
            var features = await context.Features.Include(s => s.Subfeatures).ToListAsync();
            var featuresTree = mapper.Map<List<Feature>, List<FeatureResource>>(features);
            featuresTree.RemoveAll(item => item.ParentId != null);
            return featuresTree;
        }
    }
}