using AutoMapper;
using Hereglish.Controllers.Resources;
using Hereglish.Models;

namespace Hereglish.Mapping
{
    public class MappingProfile : Profile
    {
        public  MappingProfile()
        {
            CreateMap<Category, CategoryResource>();
            CreateMap<Subcategory, SubcategoryResource>();
        }
    }
}