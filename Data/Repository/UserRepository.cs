using Microsoft.AspNetCore.Identity;
using AspProfile.Models.DTO;
using AspProfile.Models;

namespace AspProfile.Data.Repository;

public class UserRepository(UserManager<User> manager, SignInManager<User> signManager) : IUserRepository
{
    private readonly UserManager<User> _manager = manager;
    private readonly SignInManager<User> _signManager = signManager;

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

    public async Task<User> FindByEmailAsync(string email)
    {
        return await _manager.FindByEmailAsync(email);
    }

    public async Task<UserResponseDTO> FindByNameAsync(string name)
    {
        User user = await _manager.FindByNameAsync(name);
        if (user == null)
            return null;

        return new UserResponseDTO
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Description = user.Description,
            Email = user.Email
        };
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

    public async Task<Result<bool>> LoginUserAsync(UserRequestDTO user)
    {
        if (user.Password == null)
            return Result<bool>.Error("Senha não informada");

        User? userToLogin = await FindByEmailAsync(user.Email);
        if (userToLogin == null)
            return Result<bool>.Error("Usuário não encontrado");

        var result = await _signManager.PasswordSignInAsync(userToLogin, user.Password, false, false);
        
        if (result.Succeeded)
            return Result<bool>.Success("Usuário logado com sucesso", true);
        else
            return Result<bool>.Error("Email ou senha estão errados");
    }

    public async Task SignOutAsync()
    {
        await _signManager.SignOutAsync();
    }
}