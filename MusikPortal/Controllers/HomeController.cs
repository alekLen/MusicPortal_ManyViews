using Microsoft.AspNetCore.Mvc;
using MusikPortal.Models;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using System.Diagnostics;
using MusicPortal.Filters;
using MusicPortal.Models;

namespace MusikPortal.Controllers
{
    [Culture]
    public class HomeController : Controller
    {
        private readonly ISongService songService;
        public HomeController(  ISongService song)
        {
            songService = song;          
        }
        public async Task<IActionResult> Index(SortState sortOrder= SortState.YearDesc)
        {
            HttpContext.Session.SetString("path", Request.Path);
            IEnumerable<SongDTO> s = await songService.GetAllSongs();
            ViewData["ArtistSort"] = sortOrder == SortState.ArtAsc ? SortState.ArtDesc : SortState.ArtAsc;
            ViewData["StyleSort"] = sortOrder == SortState.StyleAsc ? SortState.StyleDesc : SortState.StyleAsc;
            ViewData["YearSort"] = sortOrder == SortState.YearAsc ? SortState.YearDesc : SortState.YearAsc;          

           s = sortOrder switch
            {
                SortState.ArtDesc => s.OrderByDescending(m => m.artist),
                SortState.ArtAsc => s.OrderBy(m => m.artist),
                SortState.StyleDesc => s.OrderByDescending(m => m.style),
                SortState.StyleAsc => s.OrderBy(m => m.style),             
                SortState.YearAsc => s.OrderBy(m => m.Year),
                _ =>  s.OrderByDescending(m => m.Year),
            };
            ViewBag.Songs = s;
            return View();
        }
        public async Task<IActionResult> Find(string str)
        {
            HttpContext.Session.SetString("path", Request.Path);
            IEnumerable<SongDTO> s = await songService.FindSongs(str);
            ViewBag.Songs = s;
            return View("Index");
        }
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
    }
}