using System.Collections.Generic;

namespace EducationSystem.ViewModels
{
    public class ExpertSubjectVM
    {
        public ExpertSubjectVM()
        {
            Graduate = new HashSet<GraduateVM>();
        }

        public long CodeSubjectExpert { get; set; }
        public int SubjectCode { get; set; }
        public int ExpertCode { get; set; }
        public virtual ExpertVM Expert { get; set; }
        public virtual SubjectVM Subject { get; set; }
        public virtual ICollection<GraduateVM> Graduate { get; set; }
    }
}