using System.Collections.Generic;

namespace EducationSystem.ViewModels
{
    public class VariantSolutionVM
    {
        public VariantSolutionVM()
        {
            //GraduateAnswer = new HashSet<GraduateAnswerVM>();
        }

        public long VariantTaskCode { get; set; }
        public long Number { get; set; }
        public long SolutionCode { get; set; }
        public long VariantCode { get; set; }
        public long ExamCode { get; set; }

        //public virtual ICollection<GraduateAnswerVM> GraduateAnswer { get; set; }
        //public virtual SolutionVM Solution { get; set; }
        //public virtual VariantVM Variant { get; set; }
    }
}