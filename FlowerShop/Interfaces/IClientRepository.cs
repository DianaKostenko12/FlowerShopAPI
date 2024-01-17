using FlowerShop.Models;

namespace FlowerShop.Interfaces
{
    public interface IClientRepository
    {
        ICollection<Client> GetClients();
        Client GetClient(int clientId);
        bool ClientExists(int clientId);
        ICollection<Client> GetClientBySurname(string surname);
        ICollection<Order> GetOrdersByClient(int clientId);
        bool CreateClient(Client client);
        bool Save();
    }
}
