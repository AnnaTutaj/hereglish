using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Hereglish.Core.Models;

namespace Hereglish.Controllers.Resources
{
    public class SaveWordResource
    {
        public int Id { get; set; }

        public int SubcategoryId { get; set; }

        public int PartOfSpeechId { get; set; }

        public bool IsLearned { get; set; }

        public string Example { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Meaning { get; set; }
        
        [StringLength(255)]
        public string Link { get; set; }

        public PronunciationResource Pronunciation { get; set; }
        
        public ICollection<int> Features { get; set; }

        public SaveWordResource()
        {
            Features = new Collection<int>();
        }
    }
}