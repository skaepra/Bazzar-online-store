using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Online_Store.DTOs.Auth
{
    public class CreateEmployeeDto
    {
        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        [EmailAddress(ErrorMessage = "صيغة البريد الإلكتروني غير صحيحة")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "اسم الموظف مطلوب")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "كلمة السر مطلوبة")]
        [MinLength(6, ErrorMessage = "كلمة السر يجب أن تكون 6 خانات على الأقل")]
        public string Password { get; set; } = string.Empty;

    }
}
