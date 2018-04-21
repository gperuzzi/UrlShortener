using System.Data.Linq.Mapping;

namespace UrlShortener.Domain.Models
{
    [Table(Name = "Urls")]
    public class Url
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int Id { get; set; }

        [Column(Name = "Url")]
        public string Value { get; set; }

        [Column]
        public int HitCount { get; set; }
    }
}
