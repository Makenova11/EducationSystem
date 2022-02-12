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
            return View(result);

        }

        public ActionResult Create(int TaskCode)
        {
            ViewBag.taskCode = TaskCode;
            ////ViewBag.numClass = db.Solution.Join(db.Task,
            ////    item => item.TaskCode,
            ////    meta => meta.TaskCode)
            //var d = from item in db.Solution
            //                   join meta in db.Task on item.TaskCode equals meta.TaskCode
            //                   join subjTask in db.SubjectTask on meta.SubjectTaskCode equals subjTask.SubjectTaskCode
            //                   join sub in db.Subject on subjTask.SubjectCode equals sub.SubjectCode
            //                   where meta.TaskCode == TaskCode
            //                   select sub.Class;

            //var в = from item in db.Solution
            //    join meta in db.Task on item.TaskCode equals meta.TaskCode
            //    join subjTask in db.SubjectTask on meta.SubjectTaskCode equals subjTask.SubjectTaskCode
            //    where meta.TaskCode == TaskCode
            //    select subjTask.Number;

            //ViewBag.nameSubj = from item in db.Solution
            //    join meta in db.Task on item.TaskCode equals meta.TaskCode
            //    join subjTask in db.SubjectTask on meta.SubjectTaskCode equals subjTask.SubjectTaskCode
            //    join sub in db.Subject on subjTask.SubjectCode equals sub.SubjectCode
            //    where meta.TaskCode == TaskCode
            //    select sub.Name;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SolutionCode,TaskCode")] Solution solution,
            List<HttpPostedFileBase> SolutionImageList)
        {
            if (ModelState.IsValid)
            {
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
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TaskCode = new SelectList(db.Task, "TaskCode", "CriterionFileName", solution.TaskCode);
            return View(solution);
        }
    }
}