using System.Web.Mvc;
using UrlShortener.Domain.Service;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    public class UrlShortenerController : Controller
    {
        IUrlShortenerService service;

        public UrlShortenerController(IUrlShortenerService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View(new UrlShortenerViewModel() { Url = "http://" });
        }

        [HttpPost]
        public ViewResult Index(UrlShortenerViewModel request)
        {

            if (!request.Url.Contains(":"))
                request.Url = @"http://" + request.Url;

            var urlId = service.CreateUrlId(request.Url);
            var shortUrl = Request.Url.Scheme + @"://" + Request.Url.Authority + @"/" + urlId;

            request.ShortenedUrl = shortUrl;
            return View(request);
        }

        public ActionResult RedirectUrl(string encodedId)
        {
            var url = service.ResolveUrl(encodedId);

            service.UpdateUrlStats(encodedId, Request.Headers);

            return Redirect(url);
        }
    }
}