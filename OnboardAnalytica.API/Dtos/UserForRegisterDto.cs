using System;
using System.ComponentModel.DataAnnotations;

namespace OnboardAnalytica.API.Dtos
{
   public class UserForRegisterDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(25, MinimumLength = 6, ErrorMessage = "You must specify password between 6 and 25 characters")]
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }

        public UserForRegisterDto()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }
    }
}