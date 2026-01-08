using AspProfile.Models.DTO;
using AspProfile.Models;

namespace AspProfile.Data.Repository;

public interface IUserRepository
{
    public Task<Result<bool>> CreateUserAsync(UserCreateDTO user);
    public Task<User> FindUserAsync(UserRequestDTO user);
    public Task<Result<bool>> DeleteUserAsync(UserRequestDTO user);
}