//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EducationSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SolutionImages
    {
        public long ImageCode { get; set; }
        public long SolutionCode { get; set; }
        public byte[] SolutionImage { get; set; }
        public int SolutionImageNumber { get; set; }
    
        public virtual Solution Solution { get; set; }
    }
}
