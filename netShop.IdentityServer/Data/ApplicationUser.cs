using Microsoft.AspNetCore.Identity;
namespace netShop.IdentityServer.Data;

public class ApplicationUser: IdentityUser
{
    public string? FirstName { get; set; } = String.Empty;
    public string? LastName { get; set; } = string.Empty;


}
