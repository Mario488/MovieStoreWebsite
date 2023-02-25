using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace move_store_app.Models
{
    public class Registration_Login
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

    }
}
