using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationSystem.ViewModels
{
    public class SolutionVM
    {
        public long SolutionCode { get; set; }
        public byte[] SolutionImage { get; set; }
        public long TaskCode { get; set; }

    }
}