using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hereglish.Controllers.Resources
{
    public class CategoryResource : KeyValuePairResource
    {
        public ICollection<KeyValuePairResource> Subcategories { get; set; }

        public CategoryResource()
        {
            Subcategories = new Collection<KeyValuePairResource>();
        }

    }
}