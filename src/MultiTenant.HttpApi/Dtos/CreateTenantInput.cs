using System.ComponentModel.DataAnnotations;

namespace MultiTenant.HttpApi.Dtos;

public class CreateTenantInput
{
    [Required] 
    [MaxLength(128)]
    public string Name { get; set; }

    [MaxLength(256)]
    public string Description { get; set; } = "";

    [MaxLength(128)]
    public string Domain { get; set; } = "";
}