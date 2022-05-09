using System.Collections.Generic;

namespace EducationSystem.ViewModels
{
    /// <summary>
    /// ViewModel для создания записи решения.
    /// </summary>
    public class SolutionVMgetCreateQuery
    {
        /// <summary>
        /// Код задания.
        /// </summary>
        public int TaskCode { get; set; }
        /// <summary>
        /// Наименование задания.
        /// </summary>
        public string TaskName { get; set; }
        /// <summary>
        /// Код номера предмета.
        /// </summary>
        public int SubjectCodeNumber { get; set; }
        /// <summary>
        /// Номер класса задания.
        /// </summary>
        public int Class { get; set; }
        /// <summary>
        /// Наименование предмета.
        /// </summary>
        public string NameSubject { get; set; }

    }
}