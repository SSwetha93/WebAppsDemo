using VillaMVCProj.Models;
using VillaMVCProj.Models.Dto;
using VillaMVCProj.Services.IServices;
using VillaUtility;

namespace VillaMVCProj.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _ClientFactory;
        private string villaUrl;
        public AuthService(IHttpClientFactory ClientFactory, IConfiguration configuration) : base(ClientFactory)
        {
            this._ClientFactory = ClientFactory;
            this.villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> LoginAsync<T>(LoginRequestDTO obj)
        {
            return SendAsync<T>(new APIRequest() { 
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = villaUrl + "/api/v1/UsersAuth/login/"
            });
        }

        public Task<T> RegisterAsync<T>(RegisterationRequestDTO obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = villaUrl + "/api/v1/UsersAuth/register/"
            });
        }
    }
}
