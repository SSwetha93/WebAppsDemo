using VillaMVCProj.Models;
using VillaMVCProj.Models.Dto;
using VillaMVCProj.Services.IServices;
using VillaUtility;

namespace VillaMVCProj.Services
{
    public class VillaNumberService : BaseService, IVillaNumberService
    {
        private readonly IHttpClientFactory _ClientFactory;
        private string villaUrl;
        public VillaNumberService(IHttpClientFactory ClientFactory, IConfiguration configuration) : base(ClientFactory)
        {
            this._ClientFactory = ClientFactory;
            this.villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }
        public Task<T> CreateAsync<T>(VillaNumberCreateDTO dto, string token)
        {
			var response = base.SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = villaUrl + "/api/v1/VillaNumber",
                Token = token
            });
			return response;
		}

        public Task<T> DeleteAsync<T>(int id, string token)
        {
			var response = base.SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = villaUrl + "/api/v1/VillaNumber/" + id,
                Token = token
            });
			return response;
		}

        public Task<T> GetAllAsync<T>(string token)
        {
            //Calling base class SendAsync method to fetch data from API using httpclient
            var response = base.SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = villaUrl + "/api/v1/VillaNumber",
                Token = token
			});
            //Returning response from Base class to controller.
            return response;
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
			var response = base.SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "/api/v1/VillaNumber/" + id,
                Token = token
            });
			return response;
		}

        public Task<T> UpdateAsync<T>(VillaNumberUpdateDTO dto, string token)
        {
			var response = base.SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = villaUrl + "/api/v1/VillaNumber/" + dto.VillaNo,
                Token = token
            });
			return response;
		}
    }
}
