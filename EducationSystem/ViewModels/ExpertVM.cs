using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EducationSystem.ViewModels
{
    public class ExpertVM : IdentityUser
    {
        public ExpertVM()
        {
            ExpertSubject = new HashSet<ExpertSubjectVM>();
        }

        public int ExpertCode { get; set; }
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual ICollection<ExpertSubjectVM> ExpertSubject { get; set; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}