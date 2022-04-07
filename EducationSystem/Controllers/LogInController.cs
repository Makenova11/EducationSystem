using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using EducationSystem.Models;
using EducationSystem.ViewModels;

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
        public ActionResult LogIn(LogInVM model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Expert.Where(x => x.Login == model.Login).FirstOrDefault();

                if (user != null)
                {
                    var hash = FormsAuthentication.HashPasswordForStoringInConfigFile($"{model.Password}{user.Salt}",
                        "SHA1").ToLower();
                    if (user.Password == hash)
                    {
                        FormsAuthentication.SetAuthCookie(model.Login, true);
                        if (User.Identity.IsAuthenticated)
                        {
                            var r = "da";
                            var f = User.Identity.Name;
                        }
                        return RedirectToAction("Index", "Home");
                    }

                    ModelState.AddModelError("", "Неправильный логин или пароль");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин или пароль");
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