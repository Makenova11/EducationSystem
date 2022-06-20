using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using EducationSystem.Models;
using EducationSystem.ViewModels;

namespace EducationSystem.Controllers
{
    /// <summary>
    ///     Критерий (Criterion)
    /// </summary>
    public class CriterionController : Controller
    {
        /// <summary>
        ///     База данных.
        /// </summary>
        private readonly EducationSystemDB db = new EducationSystemDB();

        /// <summary>
        ///     Список предметов, отфильтрованный по классу.
        /// </summary>
        /// <param name="numClass"> Класс. </param>
        /// <returns> Task<ActionResult> </returns>
        [HttpGet]
        public ActionResult Index(int subjectTaskCode)
        {
            var data = db.Criterion.Where(c => c.SubjectTaskCode == subjectTaskCode).ToList();
            return View(data);
        }

        //[HttpPost]
        //public async Task<ActionResult> Index(int numClass)
        //{
        //    return View();
        //}

        /// <summary>
        ///     Добавление критерия
        /// </summary>
        /// <param name="subjectTaskCode"> Код задания предмета. </param>
        /// <returns> ActionResult. </returns>
        [HttpGet]
        public ActionResult Create(int subjectTaskCode)
        {
             var subj = db.SubjectTask.Where(x => x.SubjectTaskCode == subjectTaskCode)
                .Select(c => c.SubjectCode).FirstOrDefault();
            ViewBag.numClass = db.Subject.Where(c => c.SubjectCode == subj).Select(c => c.Class).FirstOrDefault();
            ViewBag.subName = db.Subject.Where(c => c.SubjectCode == subj).Select(c => c.Name).FirstOrDefault();
            ViewBag.numSubTask = db.SubjectTask.Where(x => x.SubjectTaskCode == subjectTaskCode)
                .Select(x => x.Number).FirstOrDefault();
            ViewBag.subjectTaskCode = subjectTaskCode;
            return View();
        }

        /// <summary>
        ///     Добавление критерия (Post метод)
        /// </summary>
        /// <param name="criterion"> Сущность criterion</param>
        /// <returns> ActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubjectTaskCode, MaxScore, Number, Name")] CriterionVM criterion)
        {
            try
            {
                var data = new Criterion
                {
                    Name = criterion.Name,
                    MaxScore = criterion.MaxScore,
                    SubjectTaskCode = criterion.SubjectTaskCode,
                    Number = criterion.Number,
                };
                db.Criterion.Add(data);
                db.SaveChanges();
                var subCode = db.SubjectTask.Where(x => x.SubjectTaskCode == criterion.SubjectTaskCode)
                    .Select(x => x.SubjectCode).FirstOrDefault();
                var numClass = db.Subject.Where(x => x.SubjectCode == subCode).Select(c => c.Class)
                    .FirstOrDefault();
                return RedirectToAction("Index", "Task", new { subjTaskCode = criterion.SubjectTaskCode });
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}