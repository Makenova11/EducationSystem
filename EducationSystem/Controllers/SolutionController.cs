using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationSystem.Models;

namespace EducationSystem.Controllers
{
    public class SolutionController : Controller
    {
        /// <summary>
        ///     База данных.
        /// </summary>
        private readonly EducationSystemDB db = new EducationSystemDB();

        /// <summary>
        ///     Индекс TaskCode.
        ///     Используется для передачи значения в RedirectToAction после добавления файла.
        /// </summary>
        private int TaskCodeIndex = 1;

        /// <summary>
        ///     Представление вариантов решения одной задачи.
        /// </summary>
        /// <param name="TaskCode"> Код задания выбранного предмета. </param>
        /// <returns> ActionResult. </returns>
        public ActionResult Index(int TaskCode)
        {
            var result = db.Solution.Where(c => c.TaskCode == TaskCode).ToList();
            TaskCodeIndex = TaskCode;
            var nums = new int[result.Count];
            for (var i = 0; i < result.Count; i++) nums[i] = i + 1;
            ViewBag.solutionNumber = nums; //Массив с нумерацией имеющихся решений выбранного задания
            ViewBag.taskCode = TaskCode;
            ViewBag.description = db.Task.Where(x => x.TaskCode == TaskCode).Select(c => c.Name).FirstOrDefault();
            ViewBag.TaskImage = db.Task.Where(c => c.TaskCode == TaskCode).Select(c => c.TaskImage).FirstOrDefault();
            return View(result);

        }

        public ActionResult Create(int TaskCode)
        {
            ViewBag.TaskCode = TaskCode;
            ViewBag.description = db.Task.Where(x => x.TaskCode == TaskCode).Select(c => c.Name).FirstOrDefault();
            ViewBag.Number = db.Task.Join(db.SubjectTask,
                    task => task.SubjectTaskCode,
                    subTask => subTask.SubjectTaskCode,
                    (task, subTask) => new { SubjectCode = subTask.SubjectCode, Number = subTask.Number })
                .Select(c => c.Number).FirstOrDefault();
            ViewBag.Class= db.Task.Join(db.SubjectTask,
                    task => task.SubjectTaskCode,
                    subTask => subTask.SubjectTaskCode,
                    (task, subTask) => new { SubjectCode = subTask.SubjectCode, Number = subTask.Number })
                .Join(db.Subject,
                    subTask2 => subTask2.SubjectCode,
                    sub => sub.SubjectCode,
                    (subTask2, sub) => new { Name = sub.Name, Class = sub.Class, Number = subTask2.Number })
                .Select(c => c.Class).FirstOrDefault();
            ViewBag.Subject = db.Task.Join(db.SubjectTask,
                    task => task.SubjectTaskCode,
                    subTask => subTask.SubjectTaskCode,
                    (task, subTask) => new { SubjectCode = subTask.SubjectCode, Number = subTask.Number })
                .Join(db.Subject,
                    subTask2 => subTask2.SubjectCode,
                    sub => sub.SubjectCode,
                    (subTask2, sub) => new { Name = sub.Name, Class = sub.Class, Number = subTask2.Number })
                .Select(c => c.Name).FirstOrDefault();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SolutionCode,TaskCode")] Solution solution,
            List<HttpPostedFileBase> SolutionImageList)
        {
            if (ModelState.IsValid)
            {
                SolutionImageList.RemoveAll(c => c == null);
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(SolutionImageList[0].InputStream))
                {
                    imageData = binaryReader.ReadBytes(SolutionImageList[0].ContentLength);
                }

                solution.SolutionImage = imageData;
                SolutionImageList.RemoveAt(0);
                db.Solution.Add(solution); //добавляем solution
                db.SaveChanges();
                var solutionCode = db.Solution.Where(x => x.SolutionCode == solution.SolutionCode)
                    .Select(c => c.SolutionCode).FirstOrDefault();
                foreach (var item in SolutionImageList)
                {
                    imageData = null;
                    var number = 2;
                    using (var binaryReader = new BinaryReader(item.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(item.ContentLength);
                    }

                    db.SolutionImages.Add(new SolutionImages() //добавляем solutionImages
                    {
                        SolutionCode = solutionCode,
                        SolutionImage = imageData,
                        SolutionImageNumber = number
                    });
                    db.SaveChanges();
                }

                
                return RedirectToAction("Index");
            }

            ViewBag.TaskCode = new SelectList(db.Task, "TaskCode", "CriterionFileName", solution.TaskCode);
            return View(solution);
        }

        //public ActionResult TestSolution()
        //{

        //}
    }
}