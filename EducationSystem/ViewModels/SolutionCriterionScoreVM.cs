namespace EducationSystem.ViewModels
{
    public class SolutionCriterionScoreVM
    {
        public long SolutionCriterionCode { get; set; }
        public string Comment { get; set; }
        public bool isValid { get; set; }
        public int PossibleScore { get; set; }
        public long SolutionCode { get; set; }
        public long CriterionCode { get; set; }

        //public virtual CriterionVM Criterion { get; set; }
        //public virtual SolutionVM Solution { get; set; }
    }
}