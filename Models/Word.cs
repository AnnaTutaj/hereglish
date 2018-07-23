using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hereglish.Models
{
    [Table("Words")]
    public class Word
    {
        public int Id { get; set; }

        public int SubcategoryId { get; set; }

        public Subcategory Subcategory { get; set; }

        public int PartOfSpeechId { get; set; }

        public PartOfSpeech PartOfSpeech { get; set; }

        public bool IsLearned { get; set; }

        public string Example { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Meaning { get; set; }

        public string PronunciationUK { get; set; }

        public string PronunciationUS { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<WordFeature> Features { get; set; }

        public Word()
        {
            Features = new Collection<WordFeature>();
        }
    }
}