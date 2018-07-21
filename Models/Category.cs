using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hereglish.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Subcategory> Subcategories { get; set; }

        public Category()
        {
            Subcategories = new Collection<Subcategory>();
        }

    }
}