using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebAppsProj1.Data;
using WebAppsProj1.Models;
using WebAppsProj1.Models.Dto;
using WebAppsProj1.Repository;
using WebAppsProj1.Repository.IRepository;
//using System.Web.Http;

namespace WebAppsProj1.Controllers.v1
{
    [Route("api/v{version:apiVersion}/VillaNumber")]
    [ApiController]
    [ApiVersion("1.0"/*, Deprecated = true*/)]
    //[ApiVersion("2.0")]
    public class VillaNumberController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ILogger<HomeController> logger;
        //private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IVillaNumberRepository _villaNumberRepository;
        private readonly IVillaRepository _villaRepository;
        public VillaNumberController(ILogger<HomeController> _logger, IMapper mapper, IVillaNumberRepository villaNumberRepository, IVillaRepository villaRepository)
        {
            logger = _logger;
            //this._db = db;
            _mapper = mapper;
            _villaNumberRepository = villaNumberRepository;
            _response = new();
            _villaRepository = villaRepository;
        }

        [HttpGet]
        //[MapToApiVersion("1.0")]
        public async Task<ActionResult<APIResponse>> GetVillaNumbers() //ActionResult is used to return any type of data, not specifically to something in particular
        {
            try
            {
                logger.LogInformation("Villas are retrieved");
                IEnumerable<VillaNumber> villaList = await _villaNumberRepository.GetAllAsync(includeProperties: "Villa");
                _response.Result = _mapper.Map<List<VillaNumberDTO>>(villaList);
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

        [HttpGet("GetString")]
        public IEnumerable<string> Get()
        {
            return new string[] { "Swetha", "in Old Version" };
        }

        [HttpGet("{id:int}", Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)] //Status Code more readable form
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetVillaNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    logger.LogError("Id is wrong");
                    return BadRequest();
                }
                var villaNumber = await _villaNumberRepository.GetAsync(u => u.VillaNo == id);
                if (villaNumber == null)
                {
                    return NotFound();
                }
                //End Validations
                _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] //Status Code more readable form
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDTO createDTO)
        {
            try
            {
                //Start Validations
                if (await _villaNumberRepository.GetAsync(x => x.VillaNo == createDTO.VillaNo) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa Number already exists!");
                    return BadRequest(ModelState);
                }
                if (await _villaRepository.GetAsync(x => x.Id == createDTO.VillaId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa ID is INVALID!");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest(createDTO); //400
                }

                VillaNumber villaNumber = _mapper.Map<VillaNumber>(createDTO);

                //Villa model = new()
                //{
                //    Amenity = createDTO.Amenity,
                //    Details = createDTO.Details,
                //    //Id = createDTO.Id,
                //    ImageUrl = createDTO.ImageUrl,
                //    Name = createDTO.Name,
                //    Occupancy = createDTO.Occupancy,
                //    Rate = createDTO.Rate,
                //    Sqft = createDTO.Sqft
                //};
                await _villaNumberRepository.CreateAsync(villaNumber);
                await _villaNumberRepository.SaveAsync();

                _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
                _response.StatusCode = HttpStatusCode.Created;
                //return Ok(villaDTO); //200
                return CreatedAtRoute("GetVillaNumber", new { id = villaNumber.VillaNo }, _response); // GetVilla is the name used in the attribute of HttpGet Action method - GetVilla
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteVillaNumber")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int id)
        {
            try
            {
                //Start Validations
                if (id == 0)
                {
                    return BadRequest();
                }
                var villaNumber = await _villaNumberRepository.GetAsync(u => u.VillaNo == id);
                if (villaNumber == null)
                {
                    return NotFound();
                }
                //End Validations
                await _villaNumberRepository.RemoveAsync(villaNumber);
                await _villaNumberRepository.SaveAsync();
                _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("{id:int}", Name = "UpdateVillaNumber")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int id, [FromBody] VillaNumberUpdateDTO updateDTO)
        {
            try
            {
                if (id == 0 || id != updateDTO.VillaNo)
                {
                    return BadRequest();
                }

                if (await _villaRepository.GetAsync(x => x.Id == updateDTO.VillaId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa ID is INVALID!");
                    return BadRequest(ModelState);
                }
                if (updateDTO == null)
                {
                    return NotFound();
                }
                ////Updated Model Properties and assigning values from UI to these updates Model properties to store in Data Store 
                //villa.Name = villaDTO.Name;
                //villa.Sqft = villaDTO.Sqft;
                //villa.Occupancy = villaDTO.Occupancy;

                VillaNumber villaNumber = _mapper.Map<VillaNumber>(updateDTO);

                //Villa model = new()
                //{
                //    Amenity = updateDTO.Amenity,
                //    Details = updateDTO.Details,
                //    Id = updateDTO.Id,
                //    ImageUrl = updateDTO.ImageUrl,
                //    Name = updateDTO.Name,
                //    Occupancy = updateDTO.Occupancy,
                //    Rate = updateDTO.Rate,
                //    Sqft = updateDTO.Sqft
                //};
                await _villaNumberRepository.UpdateAsync(villaNumber);
                await _villaNumberRepository.SaveAsync();
                _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        //[HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<VillaDTO>> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
        //{
        //    if (id == 0 || patchDTO == null)
        //    {
        //        return BadRequest();
        //    }
        //    var villa = await _villaRepository.GetAsync(u => u.Id == id, tracked:false);

        //    //converting villa model to villaDto using Automapper
        //    VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(villa);

        //    if (villa == null)
        //    {
        //        return BadRequest();
        //    }
        //    patchDTO.ApplyTo(villaDTO, ModelState);

        //    //converting villaDto back to Villa Model after entering data in UI
        //    Villa model = _mapper.Map<Villa>(villaDTO);

        //    await _villaRepository.UpdateAsync(model);
        //    await _villaRepository.SaveAsync();

        //    if (!ModelState.IsValid) 
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    return NoContent();
        //}
    }
}
