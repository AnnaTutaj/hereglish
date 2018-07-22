using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hereglish.Controllers.Resources
{
    public class CategoryResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<SubcategoryResource> Subcategories { get; set; }

        public CategoryResource()
        {
            Subcategories = new Collection<SubcategoryResource>();
        }

    }
}