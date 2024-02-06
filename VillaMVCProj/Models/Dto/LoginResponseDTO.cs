using System.ComponentModel.DataAnnotations;

namespace VillaMVCProj.Models.Dto
{
    public class LoginResponseDTO
    {
        public UserDTO User { get; set; }
        public string Token { get; set; }
    }
}
