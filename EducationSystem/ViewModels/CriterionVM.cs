namespace EducationSystem.ViewModels
{
    /// <summary>
    /// ViewModel Criterion
    /// </summary>
    public class CriterionVM
    {
        /// <summary>
        /// CriterionCode
        /// </summary>
        public long CriterionCode { get; set; }
        /// <summary>
        /// Наименование критерия (название загружаемого файла)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Максимальный балл за задание
        /// </summary>
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