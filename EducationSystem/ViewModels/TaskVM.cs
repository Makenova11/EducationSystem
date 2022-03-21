using System.Collections.Generic;

namespace EducationSystem.ViewModels
{
    public class TaskVM
    {
        public TaskVM()
        {
            Solution = new HashSet<SolutionVM>();
        }

        public long TaskCode { get; set; }
        public byte[] TaskImage { get; set; }
        public int Year { get; set; }
        public byte[] CriterionFile { get; set; }
        public string CriterionFileName { get; set; }
        public int EventCode { get; set; }
        public int SubjectTaskCode { get; set; }
        public string Name { get; set; }

        public virtual EventVM Event { get; set; }
        public virtual ICollection<SolutionVM> Solution { get; set; }
        public virtual SubjectTaskVM SubjectTask { get; set; }
    }
}