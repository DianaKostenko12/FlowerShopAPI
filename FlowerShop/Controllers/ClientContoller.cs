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
    public class ClientContoller : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientContoller(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetClients()
        {
            var clients = _mapper.Map<List<ClientDto>>(_clientRepository.GetClients());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(clients);
        }

        [HttpGet("{id}")]
        public IActionResult GetClient(int id)
        {
            if (!_clientRepository.ClientExists(id))
                return NotFound();

            var client = _mapper.Map<ClientDto>(_clientRepository.GetClient(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(client);
        }

        [HttpGet("client")]
        public IActionResult GetClientsBySurname([FromQuery] string surname)
        {
            var clients = _mapper.Map<List<ClientDto>>(_clientRepository.GetClientBySurname(surname));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(clients);
        }

        [HttpGet("order")]
        public IActionResult GetOrdersByClient([FromQuery] int clientId)
        {
            if (!_clientRepository.ClientExists(clientId))
                return NotFound();

            var orders = _mapper.Map<List<OrderDto>>(
                _clientRepository.GetOrdersByClient(clientId));

            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            return Ok(orders);
        }

        [HttpPost]
        public IActionResult CreateClient([FromBody] ClientDto clientCreate)
        {
            if (clientCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var clientMap = _mapper.Map<Client>(clientCreate);

            if (!_clientRepository.CreateClient(clientMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}
