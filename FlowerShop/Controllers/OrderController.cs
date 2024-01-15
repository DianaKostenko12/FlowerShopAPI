using AutoMapper;
using FlowerShop.DTO;
using FlowerShop.Interfaces;
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
        private readonly IMapper _mapper;
        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
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
            var orders = _mapper.Map<List<OrderGiftWithCount>>(
                _orderRepository.GetOrderGiftsWithCount(orderId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orders);
        }

        [HttpGet("bouquet")]
        public IActionResult GetOrderBouquetsWithCount([FromQuery] int orderId)
        {
            var orders = _mapper.Map<List<OrderBouquetWithCount>>(
                _orderRepository.GetOrderBouquetsWithCount(orderId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orders);
        }

        //[HttpGet("client")]
        //public IActionResult GetClientByOrder([FromQuery] int orderId)
        //{
        //    var clients = _mapper.Map<List<ClientDto>>(
        //        _clientRepository.GetClientByOrder(orderId));

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    return Ok(clients);
        //}
    }
}
