using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnboardAnalytica.API.Helpers;
using OnboardAnalytica.API.Models;

namespace OnboardAnalytica.API.Data
{
    public class OnboardAnalyticaRepository : IOnboardAnalyticaRepository
    {
        private readonly DataContext _context;
        
         public OnboardAnalyticaRepository(DataContext context)
         {
            _context = context;
         }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<User> GetUser(int id, bool isCurrentUser)
        {
            var query = _context.Users.Include(p => p.Photos).AsQueryable();

            if (isCurrentUser)
                query = query.IgnoreQueryFilters();

            var user =  await query.FirstOrDefaultAsync(u => u.Id == id);
            
            return user;
        }

         public async Task<PagedList<User>> GetUsers(UserParams userParams) {
            var users = _context.Users.Include(p => p.Photos).OrderByDescending(u => u.LastActive).AsQueryable();
            users = users.Where(u => u.Id != userParams.UserId);

            if(!string.IsNullOrEmpty(userParams.OrderBy)) {
                switch(userParams.OrderBy) {
                    case "created":
                        users = users.OrderByDescending(u => u.Created);
                        break;
                    default:
                        users = users.OrderByDescending(u => u.LastActive);
                        break;
                }
            }
            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.Id == id);
            return photo;
        }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            return await _context.Photos.Where(u => u.UserId == userId).FirstOrDefaultAsync(p => p.IsMain);
        }
         public async Task<bool> SaveAll() {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}