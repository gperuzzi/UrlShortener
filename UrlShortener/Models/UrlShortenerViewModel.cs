using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models
{
    public class UrlShortenerViewModel
    {
        [Required(ErrorMessage = "Enter url")]
        public string Url { get; set; }

        public string ShortenedUrl { get; set; }
    }
}