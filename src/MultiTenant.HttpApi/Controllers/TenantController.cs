using Core.Dal.Repositories;
using Core.Models.Tenants;
using Microsoft.AspNetCore.Mvc;
using MultiTenant.HttpApi.Dtos;

namespace MultiTenant.HttpApi.Controllers;

[Route("api/[controller]")]
public class TenantController : Controller
{
    private readonly ITenantRepository _tenantRepository;

    public TenantController(ITenantRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var tenants = await _tenantRepository.GetAllAsync();
        return Ok(tenants);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTenantInput input)
    {
        var tenant = new Tenant
        {
            Domain = input.Domain,
            Name = input.Name,
            Description = input.Description
        };
        await _tenantRepository.AddAsync(tenant);
        await _tenantRepository.SaveChangeAsync();
        return Created();
    }
}