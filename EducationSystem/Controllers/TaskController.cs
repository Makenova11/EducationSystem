using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationSystem.Models;
using EducationSystem.ViewModels;

namespace EducationSystem.Controllers
{
    /// <summary>
    ///     Задание.
    /// </summary>
    public class TaskController : Controller
    {
        /// <summary>
        ///     База данных.
        /// </summary>
        private readonly EducationSystemDB db = new EducationSystemDB();


        /// <summary>
        ///     Получение общей информации о задании.
        /// </summary>
        /// <returns> ActionResult. </returns>
        [HttpGet]
        public ActionResult Index(int subjTaskCode)
        {
            var result = db.Task.Where(c => c.SubjectTaskCode == subjTaskCode).ToList();
            var nums = new int[result.Count];
            for (var i = 0; i < result.Count; i++) nums[i] = i + 1;
            ViewBag.arrayNumber = nums; //Массив с нумерацией имеющихся заданий
            ViewBag.criterionFile = db.Task.Where(c => c.SubjectTaskCode == subjTaskCode).Select(c => c.CriterionFile)
                .FirstOrDefault();
            ViewBag.subjTaskCode = subjTaskCode;
            ViewBag.numClass = (from item in db.SubjectTask //класс
                where item.SubjectTaskCode == subjTaskCode
                select item.Subject.Class).FirstOrDefault();
            ViewBag.nameSubj = (from item in db.SubjectTask //предмет
                where item.SubjectTaskCode == subjTaskCode
                select item.Subject.Name).FirstOrDefault();
            ViewBag.numTask = db.SubjectTask.Where(x => x.SubjectTaskCode == subjTaskCode) //номер задания
                .Select(c => c.Number).FirstOrDefault();
            return View(result);
        }

        /// <summary>
        ///     Добавление нового варианта задания(Инициирование)
        /// </summary>
        /// <returns> ActionResult. </returns>
        [HttpGet]
        public ActionResult Create2(int SubjectTaskCode)
        {
            ViewBag.EventCode = new SelectList(db.Event, "EventCode", "EventName");
            ViewBag.numClass = (from item in db.SubjectTask
                                where item.SubjectTaskCode == SubjectTaskCode
                                select item.Subject.Class).FirstOrDefault();
            ViewBag.nameSubj = (from item in db.SubjectTask
                                where item.SubjectTaskCode == SubjectTaskCode
                                select item.Subject.Name).FirstOrDefault();
            ViewBag.numTask = db.SubjectTask.Where(x => x.SubjectTaskCode == SubjectTaskCode).Select(c => c.Number)
                .FirstOrDefault();
            ViewBag.subjTaskCode = SubjectTaskCode;
            return View();
        }

        /// <summary>
        ///     Добавление нового варианта задания(Реализация)
        /// </summary>
        /// <param name="task"> Данные модели. </param>
        /// <param name="TaskImage"> Изображение задания. </param>
        /// <param name="CriterionFileImage"> Изображение критерия задания. </param>
        /// <returns> ActionResult. </returns>
        [HttpPost]
        public ActionResult Create2(TaskVM task)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;
                byte[] fileData = null;
                var newTask = new Task();

                using (var binaryReader = new BinaryReader(task.TaskImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(task.TaskImage.ContentLength);
                }

                using (var binaryReader = new BinaryReader(task.CriterionFileImage.InputStream))
                {
                    fileData = binaryReader.ReadBytes(task.CriterionFileImage.ContentLength);
                }

                newTask.TaskCode = task.TaskCode;
                newTask.Year = task.Year;
                newTask.EventCode = task.EventCode;
                newTask.SubjectTaskCode = task.SubjectTaskCode;
                newTask.Name = task.TaskImage.FileName;
                newTask.CriterionFile = fileData;
                newTask.CriterionFileName = task.CriterionFileImage.FileName;
                newTask.TaskImage = imageData;
                db.Task.Add(newTask);
                db.SaveChanges();
                return RedirectToAction("Index", new { subjTaskCode = task.SubjectTaskCode });
            }

            ViewBag.EventCode = new SelectList(db.Event, "EventCode", "EventName", task.EventCode);
            return View(task);
        }
    }
}