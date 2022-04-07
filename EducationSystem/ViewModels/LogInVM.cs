using System.ComponentModel.DataAnnotations;

namespace EducationSystem.ViewModels
{
    /// <summary>
    /// ViewModel Авторизации в системе.
    /// </summary>
    public class LogInVM
    {
        /// <summary>
        /// Логин.
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Логин не может быть больше 50 символов")]
        public string Login { get; set; }
        
        /// <summary>
        /// Пароль.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "Пароль не может быть больше 50 символов")]
        public string Password { get; set; }

        //[CompareAttribute("Password", ErrorMessage = "Пароли не совпадают.")]
        //public string ConfirmPassword { get; set; }
    }
}