using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspProfile.Data.Repository;
using AspProfile.Models.DTO;

namespace AspProfile.Controllers;

[Route("/{action=Index}")]
public class HomeController(IUserRepository repository) : Controller
{
    private readonly IUserRepository _repository = repository;

    public async Task<IActionResult> Index()
    {
        if (User.Identity.IsAuthenticated)
        {
            UserResponseDTO user = await _repository.FindByNameAsync(User.Identity.Name);
            return Ok(User.Identity.Name);
        }
        return NotFound("You're not logged in");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserRequestDTO user)
    {
        if (!ModelState.IsValid)
        {
            TempData["Message"] = ModelState.Values
                                .SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage)
                                .ToList()[0];
            return View();
        }
        var result = await _repository.LoginUserAsync(user);
        if (!result.IsSuccess)
        {
            TempData["Message"] = result.Message;
            return View();
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
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

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _repository.SignOutAsync();
        return RedirectToAction("Index");
    }
}
