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
            CreateMap<Word, WordResource>()
            .ForMember(wr => wr.Pronunciation, opt => opt.MapFrom(w => new PronunciationResource { Uk = w.PronunciationUK, Us = w.PronunciationUS }))
            .ForMember(wr => wr.Features, opt => opt.MapFrom(w => w.Features.Select(wf => wf.FeatureId)));

            //API Resource to Domain
            CreateMap<WordResource, Word>()
            .ForMember(w => w.PronunciationUK, opt => opt.MapFrom(wr => wr.Pronunciation.Uk))
            .ForMember(w => w.PronunciationUS, opt => opt.MapFrom(wr => wr.Pronunciation.Us))
            .ForMember(w => w.Features, opt => opt.MapFrom(wr => wr.Features.Select(id => new WordFeature { FeatureId = id })));
        }
    }
}