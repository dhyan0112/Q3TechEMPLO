using System.ComponentModel.DataAnnotations;

namespace EmployeeManagmentSystem.Models
{
    public class UserLogin
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
