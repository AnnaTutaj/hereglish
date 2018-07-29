using System.ComponentModel.DataAnnotations.Schema;

namespace Hereglish.Models
{
    [Table("WordFeatures")]
    public class WordFeature
    {
        public int WordId { get; set; }

        public int FeatureId { get; set; }

        public Word Word { get; set; }

        public Feature Feature { get; set; }

    }
}