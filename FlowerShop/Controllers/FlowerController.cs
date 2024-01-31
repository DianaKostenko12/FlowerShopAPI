using AutoMapper;
using FlowerShop.DTO;
using FlowerShop.Interfaces;
using FlowerShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowerController : Controller
    {
        private readonly IFlowerRepository _flowerRepository;
        private readonly IMapper _mapper;

        public FlowerController(IFlowerRepository flowerRepository, IMapper mapper)
        {
            _flowerRepository = flowerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetFlowers()
        {
            var flowers = _mapper.Map<List<FlowerDto>>(_flowerRepository.GetFlowers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(flowers);
        }

        [HttpGet("{id}")]
        public IActionResult GetFlowerById(int id)
        {
            if (!_flowerRepository.FlowerExists(id))
                return NotFound();

            var flower = _mapper.Map<FlowerDto>(_flowerRepository.GetFlowerById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(flower);
        }

        [HttpGet("flower")]
        public IActionResult GetFlowerByName([FromQuery] string name) 
        {
            var flower = _mapper.Map<FlowerDto>(_flowerRepository.GetFlowerByName(name));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(flower);
        }
    }
}
