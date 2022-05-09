using System.ComponentModel.DataAnnotations;

namespace EducationSystem.ViewModels
{
    public class SubjectVM
    {
        public int SubjectCode { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Наименование не может быть больше 50 символов")]
        public string Name { get; set; }

        public int Class { get; set; }
    }
}