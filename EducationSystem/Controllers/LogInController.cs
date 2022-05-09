using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EducationSystem.Helpers;
using EducationSystem.Models;
using EducationSystem.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EducationSystem.Controllers
{
    /// <summary>
    ///     Контроллер авторизации в системе.
    /// </summary>
    public class LogInController : Controller
    {
        /// <summary>
        ///     База данных.
        /// </summary>
        private readonly EducationSystemDB db = new EducationSystemDB();

        /// <summary>
        ///     Get запрос на авторизацию.
        /// </summary>
        /// <returns> LogIn.cshtml </returns>
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        /// <summary>
        ///     Процесс авторизации.
        /// </summary>
        /// <param name="model"> Данные для проверки. </param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult LogIn(LogInVM model)
        {
            if (ModelState.IsValid)
            {
                using (EducationSystemDB context = new EducationSystemDB())
                {
                    var user = context.Expert.FirstOrDefault(x => x.Login == model.Login);
                    
                    if (user != null)
                    {
                        var hash = FormsAuthentication.HashPasswordForStoringInConfigFile($"{model.Password}{user.Salt}",
                            "SHA1").ToLower();
                        if (user.Password == hash)
                        {
                            var role = context.Role.Where(x => x.RoleCode == user.RoleCode).FirstOrDefault().Name;

                            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                                (1, user.Login, DateTime.Now, DateTime.Now.AddDays(1), false, role);

                            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                            HttpContext.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket));

                            return RedirectToAction("Index", "Home");
                        }

                        ModelState.AddModelError("", "Неправильный логин или пароль");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Неправильный логин или пароль");
                    }
                }
            }

            return View(model);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}