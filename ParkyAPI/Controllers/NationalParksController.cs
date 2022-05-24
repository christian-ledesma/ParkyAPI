using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Models;
using ParkyAPI.Models.DTOs.NationalPark;
using ParkyAPI.Repository.Interfaces;
using System.Collections.Generic;

namespace ParkyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalParksController : ControllerBase
    {
        private readonly INationalParkRepository _repository;
        private readonly IMapper _mapper;

        public NationalParksController(INationalParkRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetNationalParks()
        {
            var response = _repository.GetNationalParks();
            var responseDto = new List<NationalParkDto>();
            foreach (var park in response)
            {
                responseDto.Add(_mapper.Map<NationalParkDto>(park));
            }
            return Ok(responseDto);
        }

        [HttpGet("{id:int}", Name = "GetNationalPark")]
        public IActionResult GetNationalPark(int id)
        {
            var res = _repository.GetNationalPark(id);
            if (res == null) return NotFound();
            return Ok(_mapper.Map<NationalParkDto>(res));
        }

        [HttpPost]
        public IActionResult CreateNationalPark([FromBody]NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null) return BadRequest(ModelState);

            if (_repository.NationalParkExists(nationalParkDto.Name))
            {
                ModelState.AddModelError("", $"{nationalParkDto.Name} exists!");
                return StatusCode(404, ModelState);
            }

            var nationalPark = _mapper.Map<NationalPark>(nationalParkDto);

            if (!_repository.CreateNationalPark(nationalPark))
            {
                ModelState.AddModelError("", $"Error saving record {nationalPark.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetNationalPark", new { id = nationalPark.Id }, nationalPark);
        }

        [HttpPatch("{id}", Name = "UpdateNationalPark")]
        public IActionResult UpdateNationalPark(int id, [FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null || id != nationalParkDto.Id)
                return BadRequest(ModelState);

            var nationalPark = _mapper.Map<NationalPark>(nationalParkDto);

            if (!_repository.UpdateNationalPark(nationalPark))
            {
                ModelState.AddModelError("", $"Error updating record {nationalPark.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteNationalPark")]
        public IActionResult DeleteNationalPark(int id)
        {
            if (!_repository.NationalParkExists(id))
                return NotFound();

            var nationalPark = _repository.GetNationalPark(id);

            if (!_repository.DeleteNationalPark(nationalPark))
            {
                ModelState.AddModelError("", $"Error deleting record {nationalPark.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
