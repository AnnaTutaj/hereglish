using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hereglish.Models
{
    [Table("Subcategories")]
    public class Subcategory
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public Category Category { get; set; }

        public int CategoryId { get; set; }
    }
}