using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EducationSystem.Models;

namespace EducationSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly EducationSystemDB db = new EducationSystemDB();
        private List<Subject> subjects = new List<Subject>();

        public ActionResult Index()
        {
            subjects = db.Subject.ToList();
            return View(subjects);
        }

        // GET: Subjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //Subject sub = subjects.FirstOrDefault(c => c.SubjectCode == id);
            ViewBag.subTask = db.SubjectTask.Where(c => c.SubjectCode == id).ToList();
            if (ViewBag.subTask == null) return HttpNotFound();
            return PartialView(ViewBag.subTask);
        }
    }
}