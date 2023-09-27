using Microsoft.AspNetCore.Mvc;
using MusikPortal.Models;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using System.Diagnostics;
using MusicPortal.Filters;

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
        public async Task<IActionResult> Index()
        {
            IEnumerable<SongDTO> s = await songService.GetAllSongs();
            ViewBag.Songs = s;
            return View();
        }
        public async Task<IActionResult> Find(string str)
        {
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