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
        private readonly IMapper _mapper;
        public OrderController(IOrderRepository orderRepository, IClientRepository clientRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _clientRepository = clientRepository;
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
        public IActionResult CreateOrder([FromBody] OrderDto orderCreate)
        {
            if (orderCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderMap = _mapper.Map<Order>(orderCreate);

            if (!_orderRepository.CreateOrder(orderMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}
