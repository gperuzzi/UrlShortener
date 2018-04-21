using System.Data.Linq;
using System.Linq;
using UrlShortener.Domain.Models;

namespace UrlShortener.Domain.SQLRepository
{
    public class SQLRepository:ISQLRepository
    {
        private Table<Url> urlsTable;
        private DataContext dc;

        public SQLRepository(string connectionString)
        {
            dc = new DataContext(connectionString);
            urlsTable = dc.GetTable<Url>();
        }

        public IQueryable<Url> Urls
        {
            get { return urlsTable; }
        }

        public Url InsertUrl(Url url)
        {
            urlsTable.InsertOnSubmit(url);
            urlsTable.Context.SubmitChanges();
            return url;
        }


        public void IncreaseUrlHitCount(int urlId)
        {
            dc.ExecuteCommand("UPDATE urls SET hitcount = hitcount + 1 WHERE id = {0} ", urlId);
        }
    }
}
