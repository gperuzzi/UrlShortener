using System.Linq;
using UrlShortener.Domain.Models;

namespace UrlShortener.Domain.SQLRepository
{
   public interface ISQLRepository
    {
        Url InsertUrl(Url url);
        IQueryable<Url> Urls { get; }

        void IncreaseUrlHitCount(int p);
    }
}
