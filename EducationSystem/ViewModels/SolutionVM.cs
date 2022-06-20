using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationSystem.ViewModels
{
    public class SolutionVM
    {
        public byte[] SolutionImage { get; set; }
        public long TaskCode { get; set; }

        public List<SolutionVM> inputElemGroup { get; set; }
    }

    public class SolutionCriterionVM
    {
        public int Number { get; set; }
        public bool rightAnswer { get; set; }
        public string Comment { get; set; }

    }
}