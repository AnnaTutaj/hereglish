namespace Hereglish.Core.Models
{
    public class WordQuery
    {
        public int? CategoryId { get; set; }
        public int? SubcategoryId { get; set; }
        public int? PartOfSpeechId { get; set; }
        public string Name { get; set; }
        public string Meaning { get; set; }
        public string Example { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
    }
}