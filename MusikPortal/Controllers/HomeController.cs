using Microsoft.AspNetCore.Mvc;
using MusikPortal.Models;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using System.Diagnostics;
using MusicPortal.Filters;
using MusicPortal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MusicPortal.BLL.Services;
using Microsoft.Data.SqlClient;

namespace MusikPortal.Controllers
{
    [Culture]
    public class HomeController : Controller
    {
        private readonly ISongService songService;
        private readonly IArtistService artistService;
        private readonly IStyleService styleService;
        public HomeController(  ISongService song, IArtistService a, IStyleService st)
        {
            songService = song;
            artistService = a;
            styleService = st;
        }
        public async Task<IActionResult> Index(string? str ,SortState sortOrder= SortState.YearDesc, int page = 1, int art=0,int st = 0 )
        {
            HttpContext.Session.SetString("path", Request.Path);
            IEnumerable<SongDTO> s;
           if (str!=null)
                s = await songService.FindSongs(str);
            else
                s = await songService.GetAllSongs();

            if (art != 0)
            {
                s = s.Where(p => p.artistId == art);
            }
            if (st != 0)
            {
                s = s.Where(p => p.styleId == st);
            }       
           s = sortOrder switch
            {
                SortState.ArtDesc => s.OrderByDescending(m => m.artist),
                SortState.ArtAsc => s.OrderBy(m => m.artist),
                SortState.StyleDesc => s.OrderByDescending(m => m.style),
                SortState.StyleAsc => s.OrderBy(m => m.style),             
                SortState.YearAsc => s.OrderBy(m => m.Year),
                _ =>  s.OrderByDescending(m => m.Year),
            };
            int pageSize = 3;   // количество элементов на странице           
            var count = s.Count();
            var items = s.Skip((page - 1) * pageSize).Take(pageSize);
            IEnumerable<ArtistDTO> a = await artistService.GetAllArtists();
            IEnumerable<StyleDTO> stt = await styleService.GetAllStyles();
            IndexViewModel viewModel = new IndexViewModel(
               items,
               new PageViewModel(count, page, pageSize),
               new FilterViewModel(a.ToList(), art, stt.ToList(), st),
               new SortViewModel(sortOrder),
               str
           );
            return View(viewModel);
        }
      /* public async Task<IActionResult> Find(string str, SortState sortOrder = SortState.YearDesc, int page = 1, int art = 0, int st = 0)
        {
            HttpContext.Session.SetString("path", Request.Path);
            IEnumerable<SongDTO> s = await songService.FindSongs(str);
            // ViewBag.Songs = s;
            IndexViewModel viewModel = new IndexViewModel(
              s,
              new PageViewModel(count, page, pageSize),
              new FilterViewModel(a.ToList(), art, stt.ToList(), st),
              new SortViewModel(sortOrder)
          );
            return View("Index", viewModel);
        }*/
        public ActionResult ChangeCulture(string lang)
        {
            string? returnUrl = HttpContext.Session.GetString("path") ?? "/Home/Index";

            // Список культур
            List<string> cultures = new List<string>() { "ru", "en", "uk", "de", "fr" };
            if (!cultures.Contains(lang))
            {
                lang = "ru";
            }

            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(10); // срок хранения куки - 10 дней
            Response.Cookies.Append("lang", lang, option); // создание куки
            return Redirect(returnUrl);
            //return Redirect("/Home/Index");
        }
        public ActionResult ChangeCultureMain(string lang, string? str, SortState sortOrder = SortState.YearDesc, int page = 1, int art = 0, int st = 0)
        {           
            // Список культур
            List<string> cultures = new List<string>() { "ru", "en", "uk", "de", "fr" };
            if (!cultures.Contains(lang))
            {
                lang = "ru";
            }

            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(10); // срок хранения куки - 10 дней
            Response.Cookies.Append("lang", lang, option); // создание куки
            string url = "/Home/Index/?" + "str=" + str + "&sortOrder=" + sortOrder + "&page=" + page + "&art=" + art + "&st=" + st;
            return Redirect(url);
        }      
    }
}