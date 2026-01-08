using AspProfile.Models;

namespace AspProfile.Data.Repository;

public interface IRepository
{
    public Task<bool> CreateUser(User user);
    public Task<bool> FindUser(User user);
    public Task<bool> DeleteUser(User user);
}