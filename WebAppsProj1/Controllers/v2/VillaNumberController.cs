using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebAppsProj1.Controllers.v1;
using WebAppsProj1.Data;
using WebAppsProj1.Models;
using WebAppsProj1.Models.Dto;
using WebAppsProj1.Repository;
using WebAppsProj1.Repository.IRepository;
//using System.Web.Http;

namespace WebAppsProj1.Controllers.v2
{
    [Route("api/v{version:apiVersion}/VillaNumber")]
    [ApiController]
    [ApiVersion("2.0")]
    public class VillaNumberController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ILogger<HomeController> logger;
        private readonly IMapper _mapper;
        private readonly IVillaNumberRepository _villaNumberRepository;
        private readonly IVillaRepository _villaRepository;
        public VillaNumberController(ILogger<HomeController> _logger, IMapper mapper, IVillaNumberRepository villaNumberRepository, IVillaRepository villaRepository)
        {
            logger = _logger;
            _mapper = mapper;
            _villaNumberRepository = villaNumberRepository;
            _response = new();
            _villaRepository = villaRepository;
        }

        [HttpGet("GetString")]
        public IEnumerable<string> Get()
        {
            return new string[] { "Swetha", "in new Version" };
        }

    }
}
