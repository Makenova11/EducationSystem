using System.ComponentModel.DataAnnotations;
using System.Web;

namespace EducationSystem.ViewModels
{
    /// <summary>
    ///     Задание.
    /// </summary>
    public class TaskVM
    {
        /// <summary>
        ///     Код задания.
        /// </summary>
        public long TaskCode { get; set; }

        /// <summary>
        ///     Год использования задания.
        /// </summary>
        [Required]
        [Range(1700, 2400, ErrorMessage = "Недопустимый год")]
        public int Year { get; set; }

        /// <summary>
        ///     Код мероприятия.
        /// </summary>
        public int EventCode { get; set; }

        /// <summary>
        ///     Код задания в предмете.
        /// </summary>
        public int SubjectTaskCode { get; set; }

        /// <summary>
        ///     Наименование задания.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Файл критерия.
        /// </summary>
        public HttpPostedFileBase CriterionFileImage { get; set; }

        /// <summary>
        ///     Файл задания.
        /// </summary>
        public HttpPostedFileBase TaskImage { get; set; }
    }
}