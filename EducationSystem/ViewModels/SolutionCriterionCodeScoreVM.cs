using System.ComponentModel.DataAnnotations;

namespace EducationSystem.ViewModels
{
    public class SolutionCriterionCodeScoreVM
    {
        [StringLength(50, ErrorMessage = "Наименование не может быть больше 50 символов")]
        public string Comment { get; set; }
        public bool isValid { get; set; }
        public int possibleScore { get; set; }

        public long SolutionCode { get; set; }
        public long CriterionCode { get; set; }
    }
}