using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EducationSystem.ViewModels
{
    public class SolutionCriterionCodeScoreVM
    {
        [StringLength(500, ErrorMessage = "Комментарий должен быть не больше 500 символов")]
        public string Comment { get; set; }
        public string isValid { get; set; }

        [Range(0, 20, ErrorMessage = "Недопустимый возможный балл")]
        public int possibleScore { get; set; }

        public long SolutionCode { get; set; }
        public long CriterionCode { get; set; }
    }
}