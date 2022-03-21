using System.Collections.Generic;

namespace EducationSystem.ViewModels
{
    public class SubjectTaskVM
    {
        public SubjectTaskVM()
        {
            Criterion = new HashSet<CriterionVM>();
            Task = new HashSet<TaskVM>();
        }

        public int SubjectTaskCode { get; set; }
        public int Number { get; set; }
        public int SubjectCode { get; set; }
        public virtual ICollection<CriterionVM> Criterion { get; set; }
        public virtual SubjectVM Subject { get; set; }
        public virtual ICollection<TaskVM> Task { get; set; }
    }
}