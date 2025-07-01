using CoreArchitecture.Models;

namespace CoreArchitecture.Reposititories
{
    public interface IUserRepository
    {
        Task AddAsync(User product);
    }
};