using Microsoft.AspNetCore.Mvc;
using MusikPortal.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.DTO;
using MusicPortal.Filters;

namespace MusikPortal.Controllers
{
    [Culture]
    public class LoginController : Controller
    {
        private readonly IUserService userService;
        public LoginController(IUserService u)
        {
            userService = u;           
        }
        public IActionResult Registration()
        {
            HttpContext.Session.SetString("path", Request.Path);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegisterModel user)
        {
            HttpContext.Session.SetString("path", Request.Path);
            try
            {
                if (Convert.ToInt32(user.age) < 0 || Convert.ToInt32(user.age) > 99)
                    ModelState.AddModelError("age", "uncorrectly age");
            }
            catch { ModelState.AddModelError("age", "uncorrectly age"); }
                if (ModelState.IsValid)
                {
                    if (await userService.GetUser(user.Login) != null)
                    {
                        ModelState.AddModelError("login", "this login already exists");
                        return View(user);
                    }
                    if (await userService.GetEmail(user.email) != null)
                    {
                        ModelState.AddModelError("email", "this email is already registred");
                        return View(user);
                    }
                UserDTO u = new();
                    u.Name = user.Login;
                    u.Age = user.age;
                    u.email = user.email;          
                 u.Password = user.Password;
                    try
                    {
                        await userService.CreateUser(u);                      
                    }
                    catch { }
                    return RedirectToAction("Login");
                }
            return View(user);
        }
        public IActionResult Login()
        {
            HttpContext.Session.SetString("path", Request.Path);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel user)
        {
            HttpContext.Session.SetString("path", Request.Path);

            if (ModelState.IsValid)
            {
                var u = await userService.GetUser(user.Login);
                {
                   
                    if (u != null )
                    {
                        if(await userService.CheckPassword(u,user.Password))
                        {
                            HttpContext.Session.SetString("login", user.Login);
                            if (u.Level == 1)
                                HttpContext.Session.SetString("level", "level");
                            if (u.Level==2)
                              HttpContext.Session.SetString("admin", "admin");
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "login/password  not correct");
                            return View(user);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "login/password  not correct");
                        return View(user);
                    }
                }
            }
            return View(user);
        }
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            bool isEmailInUse =await userService.CheckEmail(email);
            return Json(!isEmailInUse);
        }
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsLoginInUse( string login)
        {

            bool isUnique = await userService.GetLogins(login);
            return Json(isUnique);
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear(); // очищается сессия
            return RedirectToAction("Index", "Home");
        }
     
    }
}
