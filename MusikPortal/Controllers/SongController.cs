using Microsoft.AspNetCore.Mvc;
using MusikPortal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Services;
using MusicPortal.Filters;


namespace MusikPortal.Controllers
{
    [Culture]
    public class SongController : Controller
    {
        IWebHostEnvironment _appEnvironment;
        private readonly ISongService songService;
        private readonly IArtistService artistService;
        private readonly IStyleService styleService;
        public SongController(ISongService s, IArtistService a, IStyleService st, IWebHostEnvironment appEnvironment)
        {
            songService = s;
            artistService = a;
            styleService = st;
            _appEnvironment = appEnvironment;
        }
        public async Task<IActionResult> Create()
        {
            await putStylesArtists();
            return View("AddSong");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( AddSong song, IFormFile uploadedFile)
        {            
            if (uploadedFile == null)
                ModelState.AddModelError("", "put the file");
            DateTime today = DateTime.Today;
            int currentYear = today.Year;
            try
            {
                if (Convert.ToInt32(song.Year) < 0 || Convert.ToInt32(song.Year) > currentYear)
                    ModelState.AddModelError("", "uncorrectly year");
            }
            catch { ModelState.AddModelError("", "uncorrectly year"); }
            if (uploadedFile != null)
            {
                string str= uploadedFile.FileName.Replace(" ", "_");
                string str1 = str.Replace("-", "_");
                // Путь к папке Files
                string path = "/MusicFiles/" + str1; // имя файла

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream); // копируем файл в поток
                }
                SongDTO s = new();
                StyleDTO sStyle = await styleService.GetStyle(song.StyleId);
                ArtistDTO aArtist = await artistService.GetArtist(song.ArtistId);
                s.Name = song.Name;
                s.style =sStyle.Name;
                s.styleId = sStyle.Id;
                s.artist = aArtist.Name;
                s.artistId = aArtist.Id;
                s.Year = song.Year;
                s.text = song.text;
                s.Album=song.Album; 
                s.file = path;
                if (ModelState.IsValid)
                {
                    try
                    {
                        await songService.AddSong(s);
                        
                        await songService.AddSongToArtist(song.ArtistId, s);
                     
                        return RedirectToAction("Index", "Home");
                    }
                    catch
                    {
                        await putStylesArtists();
                        return View("AddSong", song);
                    }
                }
                else
                {
                    await putStylesArtists();
                    return View("AddSong", song);
                }
            }
            await putStylesArtists();
            return View("AddSong", song);
        }
        public async Task putStylesArtists()
        {
            IEnumerable<StyleDTO> s = await styleService.GetAllStyles();
            IEnumerable<ArtistDTO> a = await artistService.GetAllArtists();
            ViewData["StyleId"] = new SelectList(s, "Id", "Name");
            ViewData["ArtistId"] = new SelectList(a, "Id", "Name");
        }
       
    }
}
