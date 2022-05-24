using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Repository.Interfaces;

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
    }
}
