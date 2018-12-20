using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace OnboardAnalytica.API.Models
{
    public class User: IdentityUser<int>
    {
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}