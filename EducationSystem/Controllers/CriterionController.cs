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
        public async Task<ActionResult> Index()
        {
            //int numClass = 0;
            //var result = numClass != 0
            //   ? await db.Criterion
            //       .Join(db.SubjectTask,
            //           crit => crit.SubjectTaskCode,
            //           subTask => subTask.SubjectTaskCode,
            //           (crit, subTask) => new
            //           {
            //               crit.Name,
            //               crit.MaxScore,
            //               subTask.Number,
            //               subTask.SubjectCode,
            //               subTask.SubjectTaskCode
            //           })
            //       .Join(db.Subject, subTask2 => subTask2.SubjectCode,
            //           sub => sub.SubjectCode,
            //           (subTask2, sub) => new
            //           {
            //               Criterion = subTask2.Name,
            //               subTask2.MaxScore,
            //               subTask2.Number,
            //               sub.Class,
            //               SubjectName = sub.Name,
            //               subTask2.SubjectTaskCode
            //           })
            //       .Join(db.Task,
            //           subTask3 => subTask3.SubjectTaskCode,
            //           task => task.SubjectTaskCode,
            //           (subTask3, task) => new
            //           {
            //               subTask3.Class,
            //               subTask3.Criterion,
            //               subTask3.MaxScore,
            //               subTask3.Number,
            //               task.CriterionFile,
            //               subTask3.SubjectName,
            //               task.Year
            //           })
            //       .Where(x => x.Class == numClass).ToListAsync()
            //   : await db.Criterion
            //       .Join(db.SubjectTask,
            //           crit => crit.SubjectTaskCode,
            //           subTask => subTask.SubjectTaskCode,
            //           (crit, subTask) => new
            //           {
            //               crit.Name,
            //               crit.MaxScore,
            //               subTask.Number,
            //               subTask.SubjectCode,
            //               subTask.SubjectTaskCode
            //           })
            //       .Join(db.Subject, subTask2 => subTask2.SubjectCode,
            //           sub => sub.SubjectCode,
            //           (subTask2, sub) => new
            //           {
            //               Criterion = subTask2.Name,
            //               subTask2.MaxScore,
            //               subTask2.Number,
            //               sub.Class,
            //               SubjectName = sub.Name,
            //               subTask2.SubjectTaskCode
            //           })
            //       .ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(int numClass)
        {
            //Информация о критериях
            var result = numClass != 0
                ? await db.Criterion
                    .Join(db.SubjectTask,
                        crit => crit.SubjectTaskCode,
                        subTask => subTask.SubjectTaskCode,
                        (crit, subTask) => new
                        {
                            crit.Name, crit.MaxScore, subTask.Number,
                            subTask.SubjectCode, subTask.SubjectTaskCode
                        })
                    .Join(db.Subject, subTask2 => subTask2.SubjectCode,
                        sub => sub.SubjectCode,
                        (subTask2, sub) => new
                        {
                            Criterion = subTask2.Name,
                            subTask2.MaxScore,
                            subTask2.Number,
                            sub.Class,
                            SubjectName = sub.Name,
                            subTask2.SubjectTaskCode
                        })
                    .Join(db.Task,
                        subTask3 => subTask3.SubjectTaskCode,
                        task => task.SubjectTaskCode,
                        (subTask3, task) => new
                        {
                            subTask3.Class, subTask3.Criterion, subTask3.MaxScore, subTask3.Number,
                            task.CriterionFile, subTask3.SubjectName
                        })
                    .Where(x => x.Class == numClass).ToListAsync()
                : await db.Criterion
                    .Join(db.SubjectTask,
                        crit => crit.SubjectTaskCode,
                        subTask => subTask.SubjectTaskCode,
                        (crit, subTask) => new
                        {
                            crit.Name,
                            crit.MaxScore,
                            subTask.Number,
                            subTask.SubjectCode,
                            subTask.SubjectTaskCode
                        })
                    .Join(db.Subject, subTask2 => subTask2.SubjectCode,
                        sub => sub.SubjectCode,
                        (subTask2, sub) => new
                        {
                            Criterion = subTask2.Name,
                            subTask2.MaxScore,
                            subTask2.Number,
                            sub.Class,
                            SubjectName = sub.Name,
                            subTask2.SubjectTaskCode
                        })
                    .Join(db.Task,
                        subTask3 => subTask3.SubjectTaskCode,
                        task => task.SubjectTaskCode,
                        (subTask3, task) => new
                        {
                            subTask3.Class,
                            subTask3.Criterion,
                            subTask3.MaxScore,
                            subTask3.Number,
                            task.CriterionFile,
                            subTask3.SubjectName
                        })
                    .ToListAsync();
            //ViewBag.result = result;
            return View(ViewBag.result);
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
        ///     Добавление критерия (Post метод)
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
                return RedirectToAction("Index", "Subjects");
            }

            return View(criterion);
        }
    }
}