using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using EducationSystem.Models;
using EducationSystem.ViewModels;

namespace EducationSystem.Controllers
{
    /// <summary>
    ///     Предметы.
    /// </summary>
    public class SubjectsController : Controller
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
        [AllowAnonymous]
        public async Task<ActionResult> Index(int numClass)
        {
            var subjects = await db.Subject.Where(c => c.Class == numClass).ToListAsync();
            ViewBag.classNum = subjects.Select(c => c.Class).FirstOrDefault();
            return View(subjects);
        }



        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create2(int numClass, List<int> numTask)
        {
            return View(new SubjectVM
            {
                Class = numClass,
                Name = ""
            });
        }

        /// <summary>
        /// Добавление предметов, их заданий и критерия
        /// </summary>
        /// <param name="subjectVM"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create2([Bind(Include = "SubjectCode,Name, Class")] 
            SubjectVM subjectVM, List<int> numTask)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    //Add Subject
                    var subject = new Subject
                    {
                        Name = subjectVM.Name,
                        Class = subjectVM.Class
                    };
                    db.Subject.Add(subject); 
                    db.SaveChanges();
                    var subjCode = db.Subject.Where(x => x.SubjectCode == subject.SubjectCode)
                        .Select(c => c.SubjectCode)
                        .FirstOrDefault();

                    //Этап добавления SubjectTask по созданному Subject
                    foreach (var data in numTask)
                    {
                        var subjectTask = new SubjectTask
                        {
                            Number = data,
                            SubjectCode = subjCode
                        };
                        db.SubjectTask.Add(subjectTask);
                        db.SaveChanges();

                    }
                    
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index", new {numClass = subjectVM.Class });
                }

                return View();
            }
            catch
            {
                return RedirectToAction("Index", new { numClass = subjectVM.Class });
            }
        }

        /// <summary>
        ///     Редактирование SubjectTask.
        ///     Добавление Criterion.
        /// </summary>
        /// <param name="SubjectCode"></param>
        /// <returns></returns>
        public ActionResult Edit(int? SubjectCode)
        {
            if (SubjectCode == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var subject = db.Subject.Find(SubjectCode);
            ViewBag.SubTaskList =
                db.SubjectTask.Where(x => x.SubjectCode == SubjectCode).Select(x => x.Number).ToList();
            if (subject == null) return HttpNotFound();
            return View(subject);
        }

        /// <summary>
        ///     Редактирование SubjectTask.
        ///     Добавление Criterion.
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="numTask"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubjectCode,Name,Class")] Subject subject, List<int> numTask
            , string criterionName)
        {
            if (ModelState.IsValid)
            {
                //Изменяем значения Subject
                db.Entry(subject).State = EntityState.Modified;
                db.SaveChanges();
                //Вводим проверку изменил ли пользователь значения или нет
                var subjectTaskList = db.SubjectTask.Where(x => x.SubjectCode == subject.SubjectCode)
                    .Select(c => c.Number).ToList();
                var isModified = subjectTaskList.SequenceEqual(numTask);
                //Изменяем значения SubjectTask 
                if (!isModified)
                    foreach (var item in numTask)
                    {
                        db.Entry(new SubjectTask
                        {
                            SubjectCode = subject.SubjectCode,
                            Number = item
                        }).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                ////Изменяем/Добавляем Criterion
                //db.Entry(new Criterion()
                //{
                //    SubjectTaskCode = db.SubjectTask.Where(x => x.SubjectCode == subject.SubjectCode)
                //        .Select(c => c.SubjectTaskCode).FirstOrDefault(),
                //    Name = criterionName,
                //    MaxScore = maxScore
                //}).State = EntityState.Modified; ;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subject);
        }

        // GET: Subjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var subject = db.Subject.Find(id);
            if (subject == null) return HttpNotFound();
            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var subject = db.Subject.Find(id);
            db.Subject.Remove(subject);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}