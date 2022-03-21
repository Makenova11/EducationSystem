using System.Collections.Generic;

namespace EducationSystem.ViewModels
{
    public class VariantVM
    {
        public VariantVM()
        {
            Graduate = new HashSet<GraduateVM>();
            VariantSolution = new HashSet<VariantSolutionVM>();
        }

        public long VariantCode { get; set; }
        public long ExamCode { get; set; }

        public virtual ExaminationVM Examination { get; set; }
        public virtual ICollection<GraduateVM> Graduate { get; set; }
        public virtual ICollection<VariantSolutionVM> VariantSolution { get; set; }
    }
}