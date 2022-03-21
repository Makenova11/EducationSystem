using System.Collections.Generic;

namespace EducationSystem.ViewModels
{
    public class GraduateVM
    {
        public GraduateVM()
        {
            GraduateAnswer = new HashSet<GraduateAnswerVM>();
        }

        public long GraduateCode { get; set; }
        public string Password { get; set; }
        public long CodeSubjectExpert { get; set; }
        public long VariantCode { get; set; }
        public long? ExamCode { get; set; }

        public virtual ExpertSubjectVM ExpertSubject { get; set; }
        public virtual ICollection<GraduateAnswerVM> GraduateAnswer { get; set; }
        public virtual VariantVM Variant { get; set; }
    }
}