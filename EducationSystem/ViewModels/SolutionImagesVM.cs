namespace EducationSystem.ViewModels
{
    public class SolutionImagesVM
    {
        public long ImageCode { get; set; }
        public long SolutionCode { get; set; }
        public byte[] SolutionImage { get; set; }
        public int SolutionImageNumber { get; set; }

        //public virtual SolutionVM Solution { get; set; }
    }
}