using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Models;
using ParkyAPI.Models.DTOs.NationalPark;
using ParkyAPI.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ParkyAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    //[Route("api/[controller]")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "ParkyOpenAPISpecNationalParks")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class NationalParksV2Controller : ControllerBase
    {
        private readonly INationalParkRepository _repository;
        private readonly IMapper _mapper;

        public NationalParksV2Controller(INationalParkRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of national parks.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<NationalParkDto>))]
        public IActionResult GetNationalParks()
        {
            var response = _repository.GetNationalParks().FirstOrDefault();
            var responseDto = new List<NationalParkDto>();
            //foreach (var park in response)
            //{
            //    responseDto.Add(_mapper.Map<NationalParkDto>(park));
            //}
            return Ok(_mapper.Map<NationalParkDto>(response));
        }
    }
}
