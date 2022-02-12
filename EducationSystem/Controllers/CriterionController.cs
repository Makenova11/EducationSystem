using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using EducationSystem.Models;

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
        public async Task<ActionResult> Index(int numClass)
        {
            var subjects = await db.Subject.Where(c => c.Class == numClass).ToListAsync();
            ViewBag.classNum = subjects.Select(c => c.Class).FirstOrDefault();
            return View(subjects);
        }

        /// <summary>
        ///     Добавление критерия
        /// </summary>
        /// <param name="subjectTaskCode"> Код задания предмета. </param>
        /// <returns> ActionResult. </returns>
        public ActionResult Create(int subjectTaskCode)
        {
            ViewBag.numClass = db.SubjectTask.Where(x => x.SubjectTaskCode == subjectTaskCode)
                .Join(db.Subject,
                    subTask => subTask.SubjectCode,
                    sub => sub.SubjectCode,
                    (subTask, sub) => new { sub.Class }).FirstOrDefault();
            ViewBag.subName = db.SubjectTask.Where(x => x.SubjectTaskCode == subjectTaskCode)
                .Join(db.Subject,
                    subTask => subTask.SubjectCode,
                    sub => sub.SubjectCode,
                    (subTask, sub) => new { sub.Name }).FirstOrDefault();
            ViewBag.numSubTask = db.SubjectTask.Where(x => x.SubjectTaskCode == subjectTaskCode)
                .Select(x => x.Number).FirstOrDefault();
            ViewBag.subjectTaskCode = subjectTaskCode;
            return View();
        }

        /// <summary>
        /// Добавление критерия (Post метод)
        /// </summary>
        /// <param name="criterion"> Сущность criterion</param>
        /// <returns> ActionResult. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CriterionCode,SubjectTaskCode")] Criterion criterion)
        {
            if (ModelState.IsValid)
            {
                db.Criterion.Add(criterion);
                db.SaveChanges();
                var subCode = db.SubjectTask.Where(x => x.SubjectTaskCode == criterion.SubjectTaskCode)
                    .Select(x => x.SubjectCode).FirstOrDefault();
                var numClass = db.Subject.Where(x => x.SubjectCode == subCode).Select(c => c.Class)
                    .FirstOrDefault();
                return RedirectToAction("Index","Subjects", new { numClass});
            }
            return View(criterion);
        }
    }
}
