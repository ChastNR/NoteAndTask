

using ProjectModels;

namespace Repository.Interface
{
    public interface IUserRepository
    {
        User GetById(int id);
        User AuthUser(string login, string password);
        bool UserExist(string email, string phoneNumber);
        void Add(User user);
    }
}