using System.Collections.Generic;

namespace EducationSystem.ViewModels
{
    public class SolutionVM
    {
        public SolutionVM()
        {
            VariantSolution = new HashSet<VariantSolutionVM>();
            SolutionCriterionScore = new HashSet<SolutionCriterionScoreVM>();
            SolutionImages = new HashSet<SolutionImagesVM>();
        }

        public long SolutionCode { get; set; }
        public byte[] SolutionImage { get; set; }
        public long TaskCode { get; set; }
        public virtual ICollection<VariantSolutionVM> VariantSolution { get; set; }
        public virtual TaskVM Task { get; set; }
        public virtual ICollection<SolutionCriterionScoreVM> SolutionCriterionScore { get; set; }
        public virtual ICollection<SolutionImagesVM> SolutionImages { get; set; }
    }
}