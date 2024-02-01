using AutoMapper;
using FlowerShop.DTO;
using FlowerShop.Interfaces;
using FlowerShop.Models;
using FlowerShop.Repository;
using FlowerShop.Views;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IGiftRepository _giftRepository;
        private readonly IOrderGiftRepository _orderGiftRepository;
        private readonly IBouquetRepository _bouquetRepository;
        private readonly IOrderBouquetRepository _orderBouquetRepository;
        private readonly IMapper _mapper;
        public OrderController(
            IOrderRepository orderRepository, 
            IClientRepository clientRepository, 
            IGiftRepository giftRepository,
            IOrderGiftRepository orderGiftRepository,
            IBouquetRepository bouquetRepository,
            IOrderBouquetRepository orderBouquetRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
            _giftRepository = giftRepository;
            _orderGiftRepository = orderGiftRepository;
            _bouquetRepository = bouquetRepository;
            _orderBouquetRepository = orderBouquetRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = _mapper.Map<List<OrderDto>>(_orderRepository.GetOrders());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            if (!_orderRepository.OrderExists(id))
                return NotFound();

            var order = _mapper.Map<OrderDto>(_orderRepository.GetOrder(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(order);
        }

        [HttpGet("gift")]
        public IActionResult GetOrderGiftsWithCount([FromQuery] int orderId)
        {
            if (!_orderRepository.OrderExists(orderId))
                return NotFound();

            var orders = _orderRepository.GetOrderGiftsWithCount(orderId); 

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orders);
        }

        [HttpGet("bouquet")]
        public IActionResult GetOrderBouquetsWithCount([FromQuery] int orderId)
        {
            if (!_orderRepository.OrderExists(orderId))
                return NotFound();

            var orders =  _orderRepository.GetOrderBouquetsWithCount(orderId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orders);
        }

        [HttpGet("client")]
        public IActionResult GetClientByOrder([FromQuery] int orderId)
        {
            if (!_orderRepository.OrderExists(orderId))
                return NotFound();

            var clients = _mapper.Map<List<ClientDto>>(
                _orderRepository.GetClientByOrder(orderId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(clients);
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] AddOrderModel model)
        {
            if (model == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var gifts = _giftRepository.GetGifts()
                .Where(g => model.Gifts.Any(gift => gift.Id == g.GiftId));
            var bouquets = _bouquetRepository.GetBouquets()
               .Where(b => model.Bouquets.Any(bouquet => bouquet.Id == b.BouquetId));

            var order = new Order() { OrderDate = DateTime.Now };
            _orderRepository.CreateOrder(order);
            _orderRepository.Save();

            var orderGifts = gifts.Select(gift => new OrderGift() 
            {
                Order = order, 
                Gift = gift,
                GiftCount = model.Gifts.First(g => g.Id == gift.GiftId).Count
            });
            _orderGiftRepository.CreateRange(orderGifts);
            _orderGiftRepository.Save();

            var orderBouquets = bouquets.Select(bouquet => new OrderBouquet()
            {
                Order = order,
                Bouquet = bouquet,
                BouquetCount = model.Bouquets.First(b => b.Id == bouquet.BouquetId).Count
            });
            _orderBouquetRepository.CreateRange(orderBouquets);
            _orderBouquetRepository.Save();

            return Ok("Successfully created");
        }
    }
}
