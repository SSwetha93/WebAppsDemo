using System.ComponentModel.DataAnnotations;

namespace WebAppsProj1.Models.Dto
{
    public class LoginResponseDTO
    {
        public UserDTO User { get; set; }  //changed from local user to Application User - UserDTO
        //public string Role { get; set; } //Adding property to store the role
        public string Token { get; set; }
    }
}
