using AutoMapper;
using FlowerShop.DTO;
using FlowerShop.Interfaces;
using FlowerShop.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : Controller
    {
        private readonly IGiftRepository _giftRepository;
        private readonly IMapper _mapper;
        public GiftController(IGiftRepository giftRepository, IMapper mapper)
        {
            _giftRepository = giftRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetGifts()
        {
            var gifts = _mapper.Map<List<GiftDto>>(_giftRepository.GetGifts());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(gifts);
        }

        [HttpGet("{id}")]
        public IActionResult GetGift(int id)
        {
            if (!_giftRepository.GiftExists(id))
                return NotFound();

            var gift = _mapper.Map<GiftDto>(_giftRepository.GetGift(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(gift);
        }

        [HttpGet("gift")]
        public IActionResult GetBouquetsByCost([FromQuery] float minCost, float maxCost)
        {
            var gifts = _mapper.Map<GiftDto>(_giftRepository.GetGiftsByCost(minCost, maxCost));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(gifts);
        }

        [HttpGet("order")]
        public IActionResult GetGiftsByOrder([FromQuery] int orderId)
        {
            var gifts = _mapper.Map<List<GiftDto>>(
                _giftRepository.GetGiftsByOrder(orderId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(gifts);
        }
    }
}
