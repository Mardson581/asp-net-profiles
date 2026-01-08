using AspProfile.Models.DTO;
using AspProfile.Models;

namespace AspProfile.Data.Repository;

public interface IUserRepository
{
    public Task<Result<bool>> CreateUserAsync(UserCreateDTO user);
    public Task<User> FindByEmailAsync(string email);
    public Task<UserResponseDTO> FindByNameAsync(string name);
    public Task<Result<bool>> DeleteUserAsync(UserRequestDTO user);
    public Task<Result<bool>> LoginUserAsync(UserRequestDTO user);
    public Task SignOutAsync();
}