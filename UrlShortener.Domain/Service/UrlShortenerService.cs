using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using UrlShortener.Domain.Models;
using UrlShortener.Domain.SQLRepository;

namespace UrlShortener.Domain.Service
{
    public class UrlShortenerService:IUrlShortenerService
    {
        readonly ISQLRepository repository;

        public UrlShortenerService(ISQLRepository repository)
        {
            this.repository = repository;
        }


        public string CreateUrlId(string url)
        {
            var urlEntity = (from u in repository.Urls
                             where u.Value == url
                             select u
                                ).FirstOrDefault();

            if (urlEntity == null)
            {
                urlEntity = new Url() { Value = url };
                repository.InsertUrl(urlEntity);
            }

            return EncoderDecoder.Encode(urlEntity.Id);

        }

        public string ResolveUrl(string urlShort)
        {
            var id = EncoderDecoder.Decode(urlShort);
            var urlEntity = (from u in repository.Urls
                             where u.Id == id
                             select u
                    ).FirstOrDefault();

            if (urlEntity == null)
            {
                throw new Exception("Id not found");
            }
            return urlEntity.Value;
        }


        public void UpdateUrlStats(string encodedId, NameValueCollection nameValueCollection)
        {
            repository.IncreaseUrlHitCount(EncoderDecoder.Decode(encodedId));


        }

    }

    public static class EncoderDecoder
    {
        static char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz01234567890".ToCharArray();

        static public string Encode(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id must be greater than 0");
            }

            StringBuilder result = new StringBuilder();
            int workValue = id;
            while (workValue > 0)
            {
                int remainder = workValue % alphabet.Length;
                workValue = workValue / alphabet.Length;
                result.Append(alphabet[remainder]);
            }

            return result.ToString();
        }

        static public int Decode(string value)
        {
            long result = 0;
            foreach (var c in value.Reverse())
            {
                result = result * alphabet.Length;
                result += Array.IndexOf(alphabet, c);
            }
            return (int)result;
        }

    }
}
