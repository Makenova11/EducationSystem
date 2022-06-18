using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EducationSystem.ViewModels
{
    public class SubjectVM
    {

        /// <summary>
        ///     Наименование нового предмета
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Наименование не может быть больше 50 символов")]
        public string Name { get; set; }

        /// <summary>
        ///     Класс
        /// </summary>
        public int Class { get; set; }

    }
}