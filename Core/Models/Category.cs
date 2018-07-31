using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Hereglish.Core.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public ICollection<Subcategory> Subcategories { get; set; }

        public Category()
        {
            Subcategories = new Collection<Subcategory>();
        }

    }
}