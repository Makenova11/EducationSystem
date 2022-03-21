using System.Collections.Generic;

namespace EducationSystem.ViewModels
{
    public class SubjectVM
    {
        public SubjectVM()
        {
            Examination = new HashSet<ExaminationVM>();
            ExpertSubject = new HashSet<ExpertSubjectVM>();
            SubjectTask = new HashSet<SubjectTaskVM>();
        }

        public int SubjectCode { get; set; }
        public string Name { get; set; }
        public int Class { get; set; }
        public virtual ICollection<ExaminationVM> Examination { get; set; }
        public virtual ICollection<ExpertSubjectVM> ExpertSubject { get; set; }
        public virtual ICollection<SubjectTaskVM> SubjectTask { get; set; }
    }
}