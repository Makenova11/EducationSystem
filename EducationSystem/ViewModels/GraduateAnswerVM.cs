namespace EducationSystem.ViewModels
{
    public class GraduateAnswerVM
    {
        public int Score { get; set; }
        public long GraduateCode { get; set; }
        public long VariantTaskCode { get; set; }
        public long CriterionCode { get; set; }

        public virtual CriterionVM Criterion { get; set; }
        public virtual GraduateVM Graduate { get; set; }
        public virtual VariantSolutionVM VariantSolution { get; set; }
    }
}