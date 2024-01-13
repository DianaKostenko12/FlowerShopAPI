using AutoMapper;
using FlowerShop.DTO;
using FlowerShop.Interfaces;
using FlowerShop.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BouquetController : Controller
    {
        private readonly IBouquetRepository _bouquetRepository;
        private readonly IMapper _mapper;

        public BouquetController(IBouquetRepository bouquetRepository, IMapper mapper)
        {
            _bouquetRepository = bouquetRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBouquets()
        {
            var bouquets = _mapper.Map<List<BouquetDto>>(_bouquetRepository.GetBouquets());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bouquets);
        }

        [HttpGet("{id}")]
        public IActionResult GetBouquet(int id)
        {
            if (!_bouquetRepository.BouquetExists(id))
                return NotFound();

            var bouquet = _mapper.Map<BouquetDto>(_bouquetRepository.GetBouquet(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bouquet);
        }

        [HttpGet("bouquet")]
        public IActionResult GetBouquetsByCost([FromQuery] int cost)
        {
            var bouquets = _mapper.Map<BouquetDto>(_bouquetRepository.GetBouquetsByCost(cost));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bouquets);
        }

        [HttpGet("flower")]
        public IActionResult GetBouquetsByFlower([FromQuery] int flowerId)
        {
            var bouquets = _mapper.Map<List<BouquetDto>>(
                _bouquetRepository.GetBouquetsByFlower(flowerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bouquets);
        }
    }
}
