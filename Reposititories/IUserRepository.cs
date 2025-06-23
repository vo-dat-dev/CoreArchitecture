using AuthenticationApi.Models;

namespace AuthenticationApi.Reposititories
{
    public interface IUserRepository
    {
        Task AddAsync(User product);
    }
};