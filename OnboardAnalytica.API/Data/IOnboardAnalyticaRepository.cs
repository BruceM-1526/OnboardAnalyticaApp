using System.Threading.Tasks;
using OnboardAnalytica.API.Helpers;
using OnboardAnalytica.API.Models;

namespace OnboardAnalytica.API.Data
{
    public interface IOnboardAnalyticaRepository
    {
        void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<PagedList<User>> GetUsers(UserParams userParams);
         Task<User> GetUser(int id, bool isCurrentUser);
         Task<Photo> GetPhoto(int id);
         Task<Photo> GetMainPhotoForUser(int userId);
       
    }
}