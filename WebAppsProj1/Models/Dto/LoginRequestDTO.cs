using System.ComponentModel.DataAnnotations;

namespace WebAppsProj1.Models.Dto
{
    public class LoginRequestDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
