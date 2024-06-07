using Core.Dal.Repositories;
using Core.Models.Users;
using Microsoft.AspNetCore.Mvc;
using MultiTenant.HttpApi.Dtos;

namespace MultiTenant.HttpApi.Controllers;

[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly IAppUserRepository _appUserRepository;

    public UserController(IAppUserRepository appUserRepository)
    {
        _appUserRepository = appUserRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var users = await _appUserRepository.GetAllAsync();
        return Ok(users);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUserInput input)
    {
        var user = new AppUser
        {
            Id = Guid.NewGuid(),
            TenantId = input.TenantId,
            UserName = input.Username,
            PasswordHash = input.Password
        };
        await _appUserRepository.AddAsync(user);
        await _appUserRepository.SaveChangeAsync();
        return Created();
    }
}