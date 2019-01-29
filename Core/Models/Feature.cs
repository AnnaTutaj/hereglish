using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Hereglish.Core.Models
{
    public class Feature
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public virtual Feature Parent { get; set; }
        public ICollection<Feature> Subfeatures { get; set; }

         public Feature()
        {
            Subfeatures = new Collection<Feature>();
        }

    }
}