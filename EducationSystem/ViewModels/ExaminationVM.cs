using System;
using System.Collections.Generic;

namespace EducationSystem.ViewModels
{
    public class ExaminationVM
    {
        public ExaminationVM()
        {
            Variant = new HashSet<VariantVM>();
        }

        public long ExamCode { get; set; }
        public DateTime ExamDate { get; set; }
        public int SubjectCode { get; set; }
        public virtual SubjectVM Subject { get; set; }
        public virtual ICollection<VariantVM> Variant { get; set; }
    }
}