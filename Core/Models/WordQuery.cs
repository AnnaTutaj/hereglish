using Hereglish.Extensions;

namespace Hereglish.Core.Models
{
    public class WordQuery : IQueryObject
    {
        public string Query { get; set; }
        public int? CategoryId { get; set; }
        public int? SubcategoryId { get; set; }
        public int? PartOfSpeechId { get; set; }
        public string Name { get; set; }
        public string Meaning { get; set; }
        public string Example { get; set; }
        public bool? IsLearned { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
        public bool WithoutPagination { get; set; }
    }
}