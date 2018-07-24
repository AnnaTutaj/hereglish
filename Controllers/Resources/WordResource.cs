using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Hereglish.Models;

namespace Hereglish.Controllers.Resources
{
    public class WordResource
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

        public PronunciationResource Pronunciation { get; set; }
        
        public ICollection<int> Features { get; set; }

        public WordResource()
        {
            Features = new Collection<int>();
        }
    }
}