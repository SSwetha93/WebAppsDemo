using Microsoft.AspNetCore.Identity;

namespace WebAppsProj1.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }
    }
}
