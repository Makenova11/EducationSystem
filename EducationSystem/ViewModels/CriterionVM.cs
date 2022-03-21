using System.Collections.Generic;

namespace EducationSystem.ViewModels
{
    public class CriterionVM
    {
        public CriterionVM()
        {
            GraduateAnswer = new HashSet<GraduateAnswerVM>();
            SolutionCriterionScore = new HashSet<SolutionCriterionScoreVM>();
        }

        public long CriterionCode { get; set; }
        public string Name { get; set; }
        public int MaxScore { get; set; }
        public int SubjectTaskCode { get; set; }
        public virtual ICollection<GraduateAnswerVM> GraduateAnswer { get; set; }
        public virtual ICollection<SolutionCriterionScoreVM> SolutionCriterionScore { get; set; }
        public virtual SubjectTaskVM SubjectTask { get; set; }
    }
}