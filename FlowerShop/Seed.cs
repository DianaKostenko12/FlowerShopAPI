using FlowerShop.Data;
using FlowerShop.Models;

namespace FlowerShop
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void SeedDataContext()
        {
            var flowers = new List<Flower>()
            {
                new Flower() {}
            };
        }
    }
}
