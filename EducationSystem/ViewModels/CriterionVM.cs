namespace EducationSystem.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// ViewModel Criterion
    /// </summary>
    public class CriterionVM
    {
        /// <summary>
        /// Наименование критерия (название загружаемого файла)
        /// </summary>
        [StringLength(50, ErrorMessage = "Наименование не может быть больше 50 символов")]
        public string Name { get; set; }
        /// <summary>
        /// Максимальный балл за задание
        /// </summary>
        [Range(0, 20, ErrorMessage = "Недопустимое количество баллов")]
        public int MaxScore { get; set; }
        /// <summary>
        /// Код номер задания
        /// </summary>
        public int SubjectTaskCode { get; set; }
        /// <summary>
        /// Номер критерия
        /// </summary>
        public int Number { get; set; }
    }
}