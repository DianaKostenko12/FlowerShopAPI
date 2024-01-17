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
            var gifts = _mapper.Map<List<GiftDto>>(_giftRepository.GetGiftsByCost(minCost, maxCost));

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

        [HttpPost]
        public IActionResult CreateGift([FromBody] GiftDto giftCreate)
        {
            if (giftCreate == null)
                return BadRequest(ModelState);

            var gift = _giftRepository.GetGifts().Where(b => b.GiftName.Trim().ToUpper() == giftCreate.GiftName.TrimEnd()
            .ToUpper()).FirstOrDefault();

            if (gift != null)
            {
                ModelState.AddModelError("", "Client already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var giftMap = _mapper.Map<Gift>(giftCreate);

            if (!_giftRepository.CreateGift(giftMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}
