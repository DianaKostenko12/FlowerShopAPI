using FlowerShop.Models;

namespace FlowerShop.Interfaces
{
    public interface IFlowerRepository
    {
        ICollection<Flower> GetFlowers();
        Flower GetFlowerById(int id);
        Flower GetFlowerByName(string name);
        bool FlowerExists(int Id);
    }
}
