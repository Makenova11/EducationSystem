using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationSystem.Models;

namespace EducationSystem.Controllers
{
    /// <summary>
    /// Задание.
    /// </summary>
    public class TaskController : Controller
    {
        /// <summary>
        /// База данных.
        /// </summary>
        private readonly EducationSystemDB db = new EducationSystemDB();

        /// <summary>
        /// Индекс SubjectTaskCode.
        /// Используется для передачи значения в RedirectToAction после добавления файла.
        /// </summary>
        private int subjTaskCodeIndex = 1;
        /// <summary>
        /// Получение общей информации о задании.
        /// </summary>
        /// <returns> ActionResult. </returns>
        [HttpGet]
        public ActionResult Index(int subjTaskCode)
        {
            var result = db.Task.Where(c => c.SubjectTaskCode == subjTaskCode).ToList();
            subjTaskCodeIndex = subjTaskCode;
            int[] nums = new int[result.Count];
            for (int i = 0; i < result.Count; i++)
            {
                nums[i] = i+1;
            }
            ViewBag.arrayNumber = nums;//Массив с нумерацией имеющихся заданий
            ViewBag.criterionFile = db.Task.Where(c => c.SubjectTaskCode == subjTaskCode).Select(c => c.CriterionFile)
                .FirstOrDefault();
            ViewBag.subjTaskCode = subjTaskCode;
            ViewBag.numClass = (from item in db.SubjectTask//класс
                where item.SubjectTaskCode == subjTaskCode
                select item.Subject.Class).FirstOrDefault();
            ViewBag.nameSubj = (from item in db.SubjectTask//предмет
                where item.SubjectTaskCode == subjTaskCode
                select item.Subject.Name).FirstOrDefault();
            ViewBag.numTask = db.SubjectTask.Where(x => x.SubjectTaskCode == subjTaskCode)//номер задания
                .Select(c => c.Number).FirstOrDefault();
            return View(result);
        }

        /// <summary>
        /// Добавление нового варианта задания(Инициирование)
        /// </summary>
        /// <returns> ActionResult. </returns>
        [HttpGet]
        public ActionResult Create(int SubjectTaskCode)
        {
            ViewBag.EventCode = new SelectList(db.Event, "EventCode", "EventName");
            ViewBag.numClass = (from item in db.SubjectTask
                               where item.SubjectTaskCode == SubjectTaskCode
                               select item.Subject.Class).FirstOrDefault();
            ViewBag.nameSubj = (from item in db.SubjectTask
                                where item.SubjectTaskCode == SubjectTaskCode
                                select item.Subject.Name).FirstOrDefault();
            ViewBag.numTask = db.SubjectTask.Where(x => x.SubjectTaskCode == SubjectTaskCode).Select(c => c.Number).FirstOrDefault();
            ViewBag.subjTaskCode = SubjectTaskCode;
            return View();
        }
        /// <summary>
        /// Добавление нового варианта задания(Реализация)
        /// </summary>
        /// <param name="task"> Данные модели. </param>
        /// <param name="TaskImage"> Изображение задания. </param>
        /// <param name="CriterionFileImage"> Изображение критерия задания. </param>
        /// <returns> ActionResult. </returns>
        [HttpPost]
        public ActionResult Create([Bind(Include = "TaskCode,Year,EventCode,SubjectTaskCode")] Task task,
            HttpPostedFileBase TaskImage, HttpPostedFileBase CriterionFileImage)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;
                byte[] fileData = null;

                using (var binaryReader = new BinaryReader(TaskImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(TaskImage.ContentLength);
                }
                using (var binaryReader = new BinaryReader(CriterionFileImage.InputStream))
                {
                    fileData = binaryReader.ReadBytes(CriterionFileImage.ContentLength);
                }
                task.CriterionFileName = CriterionFileImage.FileName;
                task.Name = TaskImage.FileName;
                task.TaskImage = imageData;
                task.CriterionFile = fileData;
                db.Task.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index", new { subjTaskCode = subjTaskCodeIndex});
            }
            ViewBag.EventCode = new SelectList(db.Event, "EventCode", "EventName", task.EventCode);
            return View(task);
        }
    }
}