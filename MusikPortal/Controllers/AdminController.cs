using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusikPortal.Models;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.Infrastructure;
using System.IO;
using MusicPortal.BLL.Services;
using MusicPortal.Filters;

namespace MusikPortal.Controllers
{
    [Culture]
    public class AdminController : Controller
    {
        IWebHostEnvironment _appEnvironment;
        private readonly ISongService songService;
        private readonly IArtistService artistService;
        private readonly IStyleService styleService;
        private readonly IUserService userService;
        private readonly ISaltService saltService;
        public AdminController(ISongService s, IArtistService a, IStyleService st, IUserService u, ISaltService t, IWebHostEnvironment appEnvironment)
        {
            songService = s;
            artistService = a;
            styleService = st;
            userService = u;
            saltService = t;
            _appEnvironment = appEnvironment;
        }
        public async Task<IActionResult> Styles()
        {
            HttpContext.Session.SetString("path", Request.Path);
            IEnumerable<StyleDTO> s = await styleService.GetAllStyles();
            ViewData["StyleId"] = new SelectList(s, "Id", "Name");
            return View("Styles");
        }
        public async Task<IActionResult> Artists()
        {
            HttpContext.Session.SetString("path", Request.Path);
            IEnumerable<ArtistDTO> a = await artistService.GetAllArtists();
            ViewData["ArtistId"] = new SelectList(a, "Id", "Name");
            return View("Artists");
        }
        public async Task<IActionResult> EditArtist(int id)
        {
            HttpContext.Session.SetString("path", Request.Path);
            ArtistDTO a = await artistService.GetArtist(id);
            return View("EditArtist",a);
        }       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStyle(StyleDTO s)
        {
            HttpContext.Session.SetString("path", Request.Path);
            StyleDTO style = new StyleDTO();
            style.Name = s.Name;
            if (ModelState.IsValid)
            {
                try
                {
                    await styleService.AddStyle(style);                
                    return RedirectToAction("Index", "Home");                   
                }
                catch
                {
                    await putStyles(); 
                    return View("Styles", s);
                }

            }
            await putStyles();
            return View("Styles", s);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateArtist(ArtistDTO s, IFormFile p)
        {
            HttpContext.Session.SetString("path", Request.Path);
            if (p != null)
            {
                string str = p.FileName.Replace(" ", "_");
                string str1 = str.Replace("-", "_");
                // Путь к папке Files
                string path = "/Photos/" + str1; // имя файла

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await p.CopyToAsync(fileStream); // копируем файл в поток
                }
                ArtistDTO art = new ArtistDTO();
                art.Name = s.Name;
                art.photo = path;
               
                    try
                    {
                        await artistService.AddArtist(art);                    
                        return RedirectToAction("Index", "Home");
                    }
                    catch
                    {
                        await putArtists();
                        return View("Artists", s);
                    }               
            }
            ModelState.AddModelError("", "enter the photo");
            await putArtists();
            return View("Artists", s);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStyle(StyleDTO s)
        {
            HttpContext.Session.SetString("path", Request.Path);
            try
            {
                StyleDTO a = await styleService.GetStyle(s.Id);
                return View(a);
            }
            catch
            {
                await putStyles();
                return View("Styles");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDeleteStyle(StyleDTO s)
        {
            HttpContext.Session.SetString("path", Request.Path);
            try
                {
                    await styleService.DeleteStyle(s.Id);
                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                await putStyles(); 
                return View("Styles");
                }           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteArtist(ArtistDTO s)
        {
            HttpContext.Session.SetString("path", Request.Path);
            try
            {
                ArtistDTO a = await artistService.GetArtist(s.Id);
                return View(a);
            }
            catch
            {
                await putArtists();
                return View("Artists");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDeleteArtist(ArtistDTO s)
        {
            HttpContext.Session.SetString("path", Request.Path);
            try
            {
                await artistService.DeleteArtist(s.Id);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                await putArtists();
                return View("Artists");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelDeleteArtist()
        {
            HttpContext.Session.SetString("path", Request.Path);
            await putArtists();
            return View("Artists");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelDeleteStyle()
        {
            HttpContext.Session.SetString("path", Request.Path);
            await putStyles();
            return View("Styles");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStyle(StyleDTO s)
        {
            HttpContext.Session.SetString("path", Request.Path);
            if (ModelState.IsValid)
                {
                    try
                    {
                        await styleService.UpdateStyle(s.Id,s.Name);
                        return RedirectToAction("Index", "Home");
                    }
                    catch
                    {
                        await putStyles();
                        return View("Styles");
                    }

                }
            await putStyles();
            return View("Styles");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditArtist(ArtistDTO s, IFormFile? p)
        {
            HttpContext.Session.SetString("path", Request.Path);
            if (ModelState.IsValid)
            {
                try
                {
                    if (p != null)
                    {
                        string str = p.FileName.Replace(" ", "_");
                        string str1 = str.Replace("-", "_");
                        // Путь к папке Files
                        string path = "/Photos/" +str1; // имя файла

                        using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                        {
                            await p.CopyToAsync(fileStream); // копируем файл в поток
                        }
                        await artistService.UpdateArtist(s.Id, s.Name, path);
                    }
                    else
                        await artistService.UpdateArtist(s.Id, s.Name,s.photo);                  
                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    await putArtists();
                    return View("EditArtist");
                }

            }
            await putArtists();
            return View("Artists");
        }
        public async Task putStyles()
        {
            HttpContext.Session.SetString("path", Request.Path);
            IEnumerable<StyleDTO> s = await styleService.GetAllStyles();
            ViewData["StyleId"] = new SelectList(s, "Id", "Name");          
        }
        public async Task putArtists()
        {
            HttpContext.Session.SetString("path", Request.Path);
            IEnumerable<ArtistDTO> a = await artistService.GetAllArtists();
            ViewData["ArtistId"] = new SelectList(a, "Id", "Name");
        }
        public async Task putUsers()
        {
            HttpContext.Session.SetString("path", Request.Path);
            IEnumerable<UserDTO> s = await userService.GetUsers(HttpContext.Session.GetString("login"));
            ViewBag.Users = s;
        }
        public async Task<IActionResult> Users()
        {
            HttpContext.Session.SetString("path", Request.Path);
            IEnumerable<UserDTO> s = await userService.GetUsers(HttpContext.Session.GetString("login"));
            ViewBag.Users = s;
            return View();
        }
        public async Task<IActionResult> EditUser(UserDTO u)
        {
            HttpContext.Session.SetString("path", Request.Path);
            try
                {
                IEnumerable<UserDTO> s = await userService.GetUsers(HttpContext.Session.GetString("login"));
                ViewBag.Users = s;
                await userService.UpdateUser(u.Id, u.Level.Value);

                IEnumerable<UserDTO> s1 = await userService.GetUsers(HttpContext.Session.GetString("login"));
                ViewBag.Users = s1;
                await putUsers();
                return View("Users");
            }
                catch
                {
                IEnumerable<UserDTO> s = await userService.GetUsers(HttpContext.Session.GetString("login"));
                ViewBag.Users = s;
                await putUsers();
                    return View("Users");
                }
           
        }
        public async Task<IActionResult> DeleteSong(int id)
        {
            HttpContext.Session.SetString("path", Request.Path);
            if (id == null)
            {
                return NotFound();
            }

            SongDTO s = await songService.GetSong(id);
            if (s == null)
            {
                return NotFound();
            }

            return View(s);
        }
        [HttpPost, ActionName("DeleteSong")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSongConfirmd(int id)
        {            
            try
            {
                await songService.DeleteSong(id);
             
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
       [HttpPost, ActionName("CancelDeleteSong")]
        [ValidateAntiForgeryToken]
        public IActionResult CancelDeleteSong()
        {           
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> EditSong(int id)
        {
            HttpContext.Session.SetString("path", Request.Path);
            try
            {
                SongDTO s=await songService.GetSong(id);              
                AddSong s1 = new();
                s1.SongId = id;
                s1.Name= s.Name;
                s1.Year= s.Year;
                s1.Album= s.Album;
                s1.text = s.text;
                int i = await artistService.GetArtistId(s);
                int i1 = await styleService.GetStyleId(s);
                s1.ArtistId = i;
                s1.StyleId = i1;
                s1.file = s.file;
                await putStyles();
                await putArtists();
                return View("EditSong",s1);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSong(AddSong s, IFormFile? p)
        {
            try
            {
                DateTime today = DateTime.Today;
                int currentYear = today.Year;
                try
                {
                    if (Convert.ToInt32(s.Year) < 0 || Convert.ToInt32(s.Year) > currentYear)
                        ModelState.AddModelError("", "uncorrectly year");
                }
                catch { ModelState.AddModelError("", "uncorrectly year"); }
                if (ModelState.IsValid)
                {
                    try
                    {
                        if (p != null)
                        {
                            // Путь к папке Files
                            string path = "/MusicFiles/" + p.FileName; // имя файла

                            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                            {
                                await p.CopyToAsync(fileStream); // копируем файл в поток
                            }
                            s.file = path;
                        }
                    SongDTO song = new(); //await songService.GetSong(s.SongId.Value);
                    song.Id = s.SongId.Value;
                    song.Name = s.Name;
                    song.Year = s.Year;
                    song.Album = s.Album;
                    ArtistDTO a= await artistService.GetArtist(s.ArtistId);
                    song.artist = a.Name;
                    song.artistId = a.Id;
                    song.artistPhoto = a.photo;
                    StyleDTO st = await styleService.GetStyle(s.StyleId);
                    song.style = st.Name;
                     song.styleId = st.Id;
                    song.file = s.file;
                    song.text = s.text;
                    await songService.UpdateSong(song);
               
                    return RedirectToAction("Index", "Home");
                    }
                    catch
                    {
                        await putStyles();
                        await putArtists();
                        return View("EditSong", s);
                    }
                }
                else
                {
                    await putStyles();
                    await putArtists();
                    return View("EditSong", s);
                }
            }
            catch
            {
                await putStyles();
                await putArtists();
                return View("EditSong", s);
            }
        }
    }
}

