using System;
using System.Collections.Generic;

namespace OnboardAnalytica.API.Dtos
{
   public class UserForDetailedDto
    {
        public int Id { get; set; } 
        public string Email { get; set; }
        public string Username { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<PhotosForDetailedDto> Photos { get; set; }
    }
}