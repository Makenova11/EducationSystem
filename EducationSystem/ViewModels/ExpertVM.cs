using System.Collections.Generic;

namespace EducationSystem.ViewModels
{
    public class ExpertVM
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
        public virtual ICollection<ExpertSubjectVM> ExpertSubject { get; set; }
    }
}