using FlowerShop.Data;
using FlowerShop.Interfaces;
using FlowerShop.Models;

namespace FlowerShop.Repository
{
    public class ClientRepository : IClientRepository
    {
        private DataContext _context;
        public ClientRepository(DataContext context) 
        {
            _context = context;
        }
        public bool ClientExists(int clientId)
        {
            return _context.Clients.Any(c => c.ClientId == clientId);
        }

        public bool CreateClient(Client client)
        {
            _context.Add(client);
            return Save();
        }

        public Client GetClient(int clientId)
        {
            return _context.Clients.Where(c => c.ClientId == clientId).FirstOrDefault();
        }

        public ICollection<Client> GetClientBySurname(string surname)
        {
            return _context.Clients.Where(c => c.ClientSurname == surname).ToList();
        }

        public ICollection<Client> GetClients()
        {
            return _context.Clients.ToList();
        }

        public ICollection<Order> GetOrdersByClient(int clientId)
        {
            return _context.Clients.Where(c => c.ClientId == clientId).SelectMany(o => o.Orders).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false; 
        }
    }
}
