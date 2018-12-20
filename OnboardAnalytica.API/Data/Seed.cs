using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using OnboardAnalytica.API.Models;

namespace OnboardAnalytica.API.Data {
    public class Seed {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly DataContext _context;

        public Seed(DataContext context, UserManager<User> userManager, RoleManager<Role> roleManager) {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedUsers() {
            if(!_userManager.Users.Any()) {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);

                var roles = new List<Role> {
                    new Role { Name = "Member" },
                    new Role { Name = "Admin" },
                    new Role { Name = "Moderator" },
                    new Role { Name = "VIP" },
                };

                foreach(var role in roles) {
                    _roleManager.CreateAsync(role).Wait();
                }

                foreach(var user in users) {
                    _userManager.CreateAsync(user, "password").Wait();
                    _userManager.AddToRoleAsync(user, "Member").Wait();
                }

                var adminUser = new User {
                    UserName = "Admin",
                };

                IdentityResult result = _userManager.CreateAsync(adminUser, "password").Result;

                if(result.Succeeded) {
                    var admin = _userManager.FindByNameAsync("Admin").Result;
                    _userManager.AddToRolesAsync(admin, new [] { "Admin", "Moderator" }).Wait();
                }

                var values = new List<Value> {
                    new Value { Name = "Value 1" },
                    new Value { Name = "Value 2" },
                    new Value { Name = "Value 3" },
                    new Value { Name = "Value 4" },
                };

                 _context.AddRangeAsync(values);
                 _context.SaveChangesAsync();
            }
        }
    }
}