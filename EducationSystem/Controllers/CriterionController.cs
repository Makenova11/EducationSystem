using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EducationSystem.Models;

namespace EducationSystem.Controllers
{
    public class CriterionController : Controller
    {
        /// <summary>
        /// База данных.
        /// </summary>
        private readonly EducationSystemDB db = new EducationSystemDB();

        /// <summary>
        /// Список предметов, отфильтрованный по классу.
        /// </summary>
        /// <param name="numClass"> Класс. </param>
        /// <returns> Task<ActionResult> </returns>
        [HttpGet]
        public async Task<ActionResult> Index(int numClass)
        {
            var subjects = await db.Subject.Where(c => c.Class == numClass).ToListAsync();
            ViewBag.classNum = subjects.Select(c => c.Class).FirstOrDefault();
            return View(subjects);
        }
    }
}