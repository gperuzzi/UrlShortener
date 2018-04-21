using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Domain.Service
{
    public interface IUrlShortenerService
    {
        string CreateUrlId(string url);
        string ResolveUrl(string ulrShort);

        void UpdateUrlStats(string encodedId, System.Collections.Specialized.NameValueCollection nameValueCollection);
    }
}
