using System.ComponentModel.DataAnnotations;
using Hereglish.Core.Models;

namespace Hereglish.Controllers.Resources
{
    public class SaveSubcategoryResource
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public Category Category { get; set; }

        public int CategoryId { get; set; }
    }
}