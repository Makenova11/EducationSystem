using System.Linq;
using System.Web.Mvc;
using EducationSystem.Models;

namespace EducationSystem.Controllers
{
    public class ExpertsController : Controller
    {
        private readonly EducationSystemDB db = new EducationSystemDB();

        // GET: Experts
        public ActionResult Index()
        {
            return View(db.Expert.ToList());
        }
    }
}