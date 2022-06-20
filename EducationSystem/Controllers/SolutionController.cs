using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationSystem.Models;
using EducationSystem.ViewModels;

namespace EducationSystem.Controllers
{
    public class SolutionController : Controller
    {
        /// <summary>
        ///     База данных.
        /// </summary>
        private readonly EducationSystemDB db = new EducationSystemDB();

        /// <summary>
        ///     Представление вариантов решения одной задачи.
        /// </summary>
        /// <param name="TaskCode"> Код задания выбранного предмета. </param>
        /// <returns> ActionResult. </returns>
        public ActionResult Index(int TaskCode)
        {
            var result = db.Solution.Where(c => c.TaskCode == TaskCode).ToList();
            var nums = new int[result.Count];
            for (var i = 0; i < result.Count; i++) nums[i] = i + 1;
            ViewBag.solutionNumber = nums; //Массив с нумерацией имеющихся решений выбранного задания
            ViewBag.taskCode = TaskCode;
            ViewBag.subjTaskCode = db.Task.Where(c => c.TaskCode == TaskCode).Select(c => c.SubjectTaskCode).FirstOrDefault();
            ViewBag.TaskImage = db.Task.Where(c => c.TaskCode == TaskCode).Select(c => c.TaskImage).FirstOrDefault();
            ViewBag.CriterionFile = db.Task.Where(c => c.TaskCode == TaskCode).Select(c => c.CriterionFile).FirstOrDefault();
            return View(result);
        }

        [HttpGet]
        public ActionResult Create(int TaskCode)
        {
            ViewBag.TaskCode = TaskCode;
            ViewBag.description = db.Task.Where(x => x.TaskCode == TaskCode).Select(c => c.Name).FirstOrDefault();
            ViewBag.Number = db.Task.Join(db.SubjectTask,
                    task => task.SubjectTaskCode,
                    subTask => subTask.SubjectTaskCode,
                    (task, subTask) => new { subTask.SubjectCode, subTask.Number })
                .Select(c => c.Number).FirstOrDefault();
            ViewBag.Class = db.Task.Join(db.SubjectTask,
                    task => task.SubjectTaskCode,
                    subTask => subTask.SubjectTaskCode,
                    (task, subTask) => new { subTask.SubjectCode, subTask.Number })
                .Join(db.Subject,
                    subTask2 => subTask2.SubjectCode,
                    sub => sub.SubjectCode,
                    (subTask2, sub) => new { sub.Name, sub.Class, subTask2.Number })
                .Select(c => c.Class).FirstOrDefault();
            ViewBag.Subject = db.Task.Join(db.SubjectTask,
                    task => task.SubjectTaskCode,
                    subTask => subTask.SubjectTaskCode,
                    (task, subTask) => new { subTask.SubjectCode, subTask.Number })
                .Join(db.Subject,
                    subTask2 => subTask2.SubjectCode,
                    sub => sub.SubjectCode,
                    (subTask2, sub) => new { sub.Name, sub.Class, subTask2.Number })
                .Select(c => c.Name).FirstOrDefault();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskCode")] Solution solution,
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

                //Этап добавления Solution
                solution.SolutionImage = imageData;
                SolutionImageList.RemoveAt(0);
                db.Solution.Add(solution);
                db.SaveChanges();
                var solutionCode = db.Solution.Where(x => x.SolutionCode == solution.SolutionCode)
                    .Select(c => c.SolutionCode).FirstOrDefault();
                //Этап добавления SolutionImages по созданному Solution
                var number = 2;
                foreach (var item in SolutionImageList)
                {
                    imageData = null;
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
                    number++;
                    db.SaveChanges();
                }
                //Этап добавления SolutionCriterionScore


                return RedirectToAction("Index", new { solution.TaskCode });
            }

            return View(solution);
        }

        /// <summary>
        ///     Определяет часть для обучения.
        /// </summary>
        /// <param name="SolutionCode"> Код решения. </param>
        /// <returns> ActionResult. /returns>
        public ActionResult SolutionTest(int SolutionCode)
        {
            var solutionTaskCode = db.Solution.Where(c => c.SolutionCode == SolutionCode).Select(c => c.TaskCode).FirstOrDefault();
            ViewBag.solutionTaskCode = solutionTaskCode;
            ViewBag.TaskImage = db.Task.Where(c => c.TaskCode == solutionTaskCode).Select(c => c.TaskImage).FirstOrDefault();
            //Первое изображение решения(нужно для отображения впервые загруженной страницы)
            ViewBag.FirstImage = db.Solution.Where(x => x.SolutionCode == SolutionCode).Select(x => x.SolutionImage)
                .FirstOrDefault();
            //Изображения из solutionImages
            var Images = db.SolutionImages.Where(x => x.SolutionCode == SolutionCode)
                .OrderBy(c => c.SolutionImageNumber).Select(c => c.SolutionImage).ToList();
            //Первое изображение, но уже в виде листа.
            var FirstImage = db.Solution.Where(x => x.SolutionCode == SolutionCode).Select(x => x.SolutionImage)
                .ToList();
            //Объединяем все изображения, чтобы получить общий массив решений.
            ViewBag.Images = FirstImage.Union(Images);
            //Для второго варианта
            ViewBag.AllImages = db.Solution.Where(x => x.SolutionCode == SolutionCode).Select(x => x.SolutionImage).ToList();
            var taskCode = db.Solution.Where(c => c.SolutionCode == SolutionCode).Select(c => c.TaskCode).FirstOrDefault();
            ViewBag.SolutionCode = SolutionCode;
            var subjectTaskcode = db.Task.Where(c => c.TaskCode == solutionTaskCode).Select(c => c.SubjectTaskCode).FirstOrDefault();
            ViewBag.CriterionCode = db.Criterion.Where(c => c.SubjectTaskCode == subjectTaskcode).Select(c => c.CriterionCode).FirstOrDefault();
            ViewBag.Test = db.SolutionCriterionScore.Where(c => c.SolutionCode == SolutionCode).ToList();
            return View();
        }

        /// <summary>
        /// Добавить комментарий к решению
        /// </summary>
        /// <param name="SolutionCode"></param>
        /// <param name="CriterionCode"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateComment(int SolutionCode, int CriterionCode)
        {
            if (CriterionCode == null)
            {
                return RedirectToAction("NotFound");
            }
            List<SelectListItem> dropdownItems = new List<SelectListItem>();
            dropdownItems.AddRange(new[]{
                            new SelectListItem() { Selected = true, Text = "Верно", Value= "true"},
                            new SelectListItem() { Selected = false, Text = "Неверно", Value= "false"}});
            ViewData.Add("DropDownItems", dropdownItems);
            ViewBag.SolutionCode = SolutionCode;
            ViewBag.CriterionCode = CriterionCode;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment([Bind(Include = "Comment, isValid, possibleScore, SolutionCode, CriterionCode")] 
        SolutionCriterionCodeScoreVM solution)
        {
            if (ModelState.IsValid)
            {
                SolutionCriterionScore data = new SolutionCriterionScore
                {
                    PossibleScore = solution.possibleScore,
                    Comment = solution.Comment,
                    SolutionCode = solution.SolutionCode,
                    CriterionCode = solution.CriterionCode,
                    isValid = solution.isValid == "true"? true : false,
                };
                db.SolutionCriterionScore.Add(data);
                db.SaveChanges();
                return RedirectToAction("SolutionTest","Solution", new { SolutionCode = solution.SolutionCode });
            }

            return View(solution);
        }

        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        /// <summary>
        /// Посмотреть комментарии к решению
        /// </summary>
        /// <param name="SolutionCode"></param>
        /// <returns></returns>
        public ActionResult ViewComment(int SolutionCode)
        {
            List<SolutionCriterionScore> data = db.SolutionCriterionScore.Where(c => c.SolutionCode == SolutionCode).ToList();
            return View(data);
        }
    }
}