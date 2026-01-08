using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspProfile.Data.Repository;
using AspProfile.Models.DTO;

namespace AspProfile.Controllers;

public class HomeController(IUserRepository repository) : Controller
{
    private readonly IUserRepository _repository = repository;

    public async Task<IActionResult> Index()
    {
        if (User.Identity.IsAuthenticated)
        {
            UserResponseDTO user = await _repository.FindByNameAsync(User.Identity.Name);
            return View(user);
        }
        return Ok(User.Identity.Name);
    }

    public async Task<IActionResult> Login(UserRequestDTO user)
    {
        var result = await _repository.LoginUserAsync(user);
        return Ok(result.Message);
    }

    public async Task<IActionResult> Register(UserCreateDTO user)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var result = await _repository.CreateUserAsync(user);
        if (result.IsSuccess)
        {
            await _repository.LoginUserAsync(
                new UserRequestDTO { Email = user.Email, Password = user.Password });
        }
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Logout()
    {
        await _repository.SignOutAsync();
        return RedirectToAction("Index");
    }
}
