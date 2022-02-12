using EducationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EducationSystem.Controllers
{
    /// <summary>
    /// Предметы.
    /// </summary>
    public class SubjectsController : Controller
    {
        /// <summary>
        /// База данных.
        /// </summary>
        private EducationSystemDB db = new EducationSystemDB();

        /// <summary>
        /// Список предметов, отфильтрованный по классу.
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
        /// Инициирование добавления нового предмета
        /// </summary>
        /// <param name="numClass"> Номер класса. </param>
        /// <param name="numTask"> Номера заданий. </param>
        /// <returns> ActionResult. </returns>
        public async Task<ActionResult> Create(int numClass, List<int> numTask)
        {
            var subjects = await db.Subject.Where(c => c.Class == numClass).ToListAsync();
            ViewBag.classNum = subjects.Select(c => c.Class).FirstOrDefault();
            return View();
        }

        // POST: Subjects/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SubjectCode,Name")] Subject subject, int numClass
        , List<int> numTask)
        {
            numTask.Distinct();
            if (ModelState.IsValid)
            {
                //Этап добавления Subject
                subject.Class = numClass;
                db.Subject.Add(subject);
                await db.SaveChangesAsync();
                var subjCode = await db.Subject.Where(x => x.SubjectCode == subject.SubjectCode).Select(c => c.SubjectCode)
                    .FirstOrDefaultAsync();
                //Этап добавления SubjectTask по созданному Subject
                foreach (var item in numTask)
                {
                    if (item > 0)//todo добавить проверку на уникальные значения
                    {
                        db.SubjectTask.Add(new SubjectTask()
                        {
                            Number = item,
                            SubjectCode = subjCode
                        });
                    }
                    
                }
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { numClass = numClass});
            }

            return View(subject);
        }

        // GET: Subjects/Edit/5
        public ActionResult Edit(int? SubjectCode)
        {
            if (SubjectCode == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subject.Find(SubjectCode);
            ViewBag.SubTaskList = db.SubjectTask.Where(x => x.SubjectCode == SubjectCode).Select(x =>x.Number).ToList();
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubjectCode,Name,Class")] Subject subject, List<int> numTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subject);
        }

        // GET: Subjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subject.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subject subject = db.Subject.Find(id);
            db.Subject.Remove(subject);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
