using System.Linq;
using AutoMapper;
using Hereglish.Controllers.Resources;
using Hereglish.Models;

namespace Hereglish.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain to API Resource
            CreateMap<Category, CategoryResource>();
            CreateMap<Subcategory, SubcategoryResource>();
            CreateMap<Feature, FeatureResource>();
            CreateMap<PartOfSpeech, PartOfSpeechResource>();

            //API Resource to Domain
            CreateMap<WordResource, Word>()
            .ForMember(w => w.PronunciationUK, opt => opt.MapFrom(wr => wr.PronunciationResource.Uk))
            .ForMember(w => w.PronunciationUS, opt => opt.MapFrom(wr => wr.PronunciationResource.Us))
            .ForMember(w => w.Features, opt => opt.MapFrom(wr => wr.Features.Select(id => new WordFeature { FeatureId = id })));
        }
    }
}