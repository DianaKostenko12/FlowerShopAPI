using AutoMapper;
using FlowerShop.DTO;
using FlowerShop.Interfaces;
using FlowerShop.Models;
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
        public IActionResult GetBouquetsByCost([FromQuery] float minCost, float maxCost)
        {
            var bouquets = _mapper.Map<BouquetDto>(_bouquetRepository.GetBouquetsByCost(minCost, maxCost));

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

        [HttpGet("order")]
        public IActionResult GetBouquetsByOrder([FromQuery] int orderId)
        {
            var bouquets = _mapper.Map<List<BouquetDto>>(
                _bouquetRepository.GetBouquetsByOrder(orderId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bouquets);
        }
    }
}
