using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationSystem.Models;

namespace EducationSystem.Controllers
{
    public class SolutionController : Controller
    {
        /// <summary>
        /// База данных.
        /// </summary>
        private readonly EducationSystemDB db = new EducationSystemDB();
        /// <summary>
        /// Индекс TaskCode.
        /// Используется для передачи значения в RedirectToAction после добавления файла.
        /// </summary>
        private int TaskCodeIndex = 1;

        /// <summary>
        /// Представление вариантов решения одной задачи.
        /// </summary>
        /// <param name="TaskCode"> Код задания выбранного предмета. </param>
        /// <returns> ActionResult. </returns>
        public ActionResult Index(int TaskCode)
        {
            var result = db.Solution.Where(c => c.TaskCode == TaskCode).ToList();
            TaskCodeIndex = TaskCode;
            int[] nums = new int[result.Count];
            for (int i = 0; i < result.Count; i++)
            {
                nums[i] = i + 1;
            }
            ViewBag.arrayNumber = nums;//Массив с нумерацией имеющихся решений выбранного задания
            //ViewBag.criterionFile = db.Task.Where(c => c.SubjectTaskCode == subjTaskCode).Select(c => c.CriterionFile)
            //    .FirstOrDefault();
            return View(result); ;
        }
        public ActionResult Create()
        {
            ViewBag.TaskCode = new SelectList(db.Task, "TaskCode", "CriterionFileName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SolutionCode,TaskCode")] Solution solution)
        {
            if (ModelState.IsValid)
            {
                db.Solution.Add(solution);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TaskCode = new SelectList(db.Task, "TaskCode", "CriterionFileName", solution.TaskCode);
            return View(solution);
        }
    }
}