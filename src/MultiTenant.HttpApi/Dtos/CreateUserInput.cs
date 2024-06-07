using System.ComponentModel.DataAnnotations;

namespace MultiTenant.HttpApi.Dtos;

public class CreateUserInput
{
    [Range(1, int.MaxValue)]
    public int TenantId { get; set; }
    
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}