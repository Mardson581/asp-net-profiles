using Microsoft.AspNetCore.Identity;
using AspProfile.Models.DTO;
using AspProfile.Models;

namespace AspProfile.Data.Repository;

public class UserRepository(UserManager<User> manager) : IUserRepository
{
    private readonly UserManager<User> _manager = manager;

    public async Task<Result<bool>> CreateUserAsync(UserCreateDTO user)
    {
        User newUser = new User
        {
            UserName = user.FirstName + user.LastName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Description = user.Description,
            Email = user.Email,
            NormalizedEmail = user.Email.ToUpper()
        };
        newUser.PasswordHash = _manager.PasswordHasher.HashPassword(newUser, user.Password);

        var result = await _manager.CreateAsync(newUser);
        
        if (result.Succeeded)
            return Result<bool>.Success("Usuário criado com sucesso", true);

        return Result<bool>.Error(result.ToString());
    }

    public async Task<User> FindUserAsync(UserRequestDTO user)
    {
        return await _manager.FindByIdAsync(user.Id);
    }

    public async Task<Result<bool>> DeleteUserAsync(UserRequestDTO user)
    {
        var userToDelete = await _manager.FindByIdAsync(user.Id);

        if (userToDelete != null)
            return Result<bool>.Error("Usuário não encontrado");

        var result = await _manager.DeleteAsync(userToDelete);

        if (result.Succeeded)
            return Result<bool>.Success("Usuário deletado com sucesso", true);

        return Result<bool>.Error(result.ToString());
    }
}