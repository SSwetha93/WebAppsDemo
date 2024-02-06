using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using VillaMVCProj.Services;
using WebAppsProj1.Models;

namespace WebAppsProj1.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        protected APIResponse _response;
        private StudentDataService _StudentDataService;

        public TestController(StudentDataService studentDataService)
        {
            _response = new();
            _StudentDataService = studentDataService;
        }


        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAll()
        {
            try
            {
                _response.Result = _StudentDataService.GetSudentData();
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        //Reading input param data from Route
        [HttpGet("{id:int}")]
        public async Task<ActionResult<APIResponse>> GetById(int id)
        {
            try
            {
                _response.Result = _StudentDataService.GetSudentData().Where(x => x.Id == id);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        //Reading input param data from Route
        //URL: https://localhost:7262/api/test/1/name/John
        [HttpGet("{id}/name/{name}")]
        public async Task<ActionResult<APIResponse>> GetById(int id, string name)
        {
            try
            {
                _response.Result = _StudentDataService.GetSudentData().Where(x => x.Id == id && x.Name == name);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        //Reading Data from Query String
        //URL: https://localhost:7262/api/test/GetIdName?Id=1&Name=John
        [HttpGet("GetIdName")]
        public async Task<ActionResult<APIResponse>> GetIdName([FromQuery(Name = "Id")] int id, [FromQuery(Name = "Name")] string name)
        {
            try
            {
                _response.Result = _StudentDataService.GetSudentData().Where(x => x.Id == id && x.Name == name);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
