using WebAppsProj1.Models;
using WebAppsProj1.Models.Dto;

namespace WebAppsProj1.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username); //Method - Checks whether the UserName is unique
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO); //Used for Login  
        Task<UserDTO> Register(RegisterationRequestDTO registerationRequestDTO); //Used for Registeration
    }
}
