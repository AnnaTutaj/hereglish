namespace Hereglish.Core.Models
{
    public class Filter
    {
        public int? CategoryId { get; set; }
        public int? SubcategoryId { get; set; }
        public int? PartOfSpeechId { get; set; }
        public string Name { get; set; }
        public string Meaning { get; set; }
        public string Example { get; set; }
    }
}