using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Models;
using ParkyAPI.Models.DTOs.Trail;
using ParkyAPI.Repository.Interfaces;
using System.Collections.Generic;

namespace ParkyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class TrailsController : ControllerBase
    {
        private readonly ITrailRepository _repository;
        private readonly IMapper _mapper;

        public TrailsController(ITrailRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of trails.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TrailDto>))]
        public IActionResult GetTrails()
        {
            var response = _repository.GetTrails();
            var responseDto = new List<TrailDto>();
            foreach (var park in response)
            {
                responseDto.Add(_mapper.Map<TrailDto>(park));
            }
            return Ok(responseDto);
        }

        /// <summary>
        /// Get individual trail
        /// </summary>
        /// <param name="id">Id of the trail</param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetTrail")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TrailDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetTrail(int id)
        {
            var res = _repository.GetTrail(id);
            if (res == null) return NotFound();
            return Ok(_mapper.Map<TrailDto>(res));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TrailDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult CreateTrail([FromBody]TrailCreateDto trailDto)
        {
            if (trailDto == null) return BadRequest(ModelState);

            if (_repository.TrailExists(trailDto.Name))
            {
                ModelState.AddModelError("", $"{trailDto.Name} exists!");
                return StatusCode(404, ModelState);
            }

            var trail = _mapper.Map<Trail>(trailDto);

            if (!_repository.CreateTrail(trail))
            {
                ModelState.AddModelError("", $"Error saving record {trail.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetTrail", new { id = trail.Id }, trail);
        }

        [HttpPatch("{id}", Name = "UpdateTrail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult UpdateTrail(int id, [FromBody] TrailUpdateDto trailDto)
        {
            if (trailDto == null || id != trailDto.Id)
                return BadRequest(ModelState);

            var trail = _mapper.Map<Trail>(trailDto);

            if (!_repository.UpdateTrail(trail))
            {
                ModelState.AddModelError("", $"Error updating record {trail.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteTrail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTrail(int id)
        {
            if (!_repository.TrailExists(id))
                return NotFound();

            var trail = _repository.GetTrail(id);

            if (!_repository.DeleteTrail(trail))
            {
                ModelState.AddModelError("", $"Error deleting record {trail.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
