using VillaMVCProj.Models;
using VillaMVCProj.Models.Dto;
using VillaMVCProj.Services.IServices;
using VillaUtility;

namespace VillaMVCProj.Services
{
    public class VillaService : BaseService, IVillaService
    {
        private readonly IHttpClientFactory _ClientFactory;
        private string villaUrl;
        public VillaService(IHttpClientFactory ClientFactory, IConfiguration configuration) : base(ClientFactory)
        {
            this._ClientFactory = ClientFactory;
            this.villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }
        public Task<T> CreateAsync<T>(VillaCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = villaUrl + "/api/v1/home",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            var result = base.SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = villaUrl + "/api/v1/home/" + id,
                Token = token
            });
            return result;
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            var result = base.SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "/api/v1/home",
                Token = token
            });
            return result;
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "/api/v1/home/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(VillaUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = villaUrl + "/api/v1/home/" + dto.Id,
                Token = token
            });
        }
    }
}
