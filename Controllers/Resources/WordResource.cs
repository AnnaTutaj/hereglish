using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hereglish.Controllers.Resources
{
    public class WordResource
    {
        public int Id { get; set; }

        public KeyValuePairResource Subcategory { get; set; }
       
        public KeyValuePairResource Category { get; set; }

        public KeyValuePairResource PartOfSpeech { get; set; }

        public bool IsLearned { get; set; }

        public string Example { get; set; }

        public string Name { get; set; }

        public string Meaning { get; set; }
       
        public string Link { get; set; }

        public PronunciationResource Pronunciation { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public ICollection<KeyValuePairResource> Features { get; set; }

        public WordResource()
        {
            Features = new Collection<KeyValuePairResource>();
        }
    }
}