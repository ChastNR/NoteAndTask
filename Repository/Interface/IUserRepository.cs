using Repository.Models;

namespace Repository.Interface
{
    public interface IUserRepository
    {
        User AuthUser(string login);

        User UserExist(string email, string phoneNumber);
    }
}