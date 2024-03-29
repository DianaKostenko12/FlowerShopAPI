﻿using FlowerShop.Data;
using FlowerShop.Interfaces;
using FlowerShop.Models;

namespace FlowerShop.Repository
{
    public class FlowerRepository : IFlowerRepository
    {
        private readonly DataContext _context;
        public FlowerRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateFlower(int bouquetId, Flower flower)
        {
            var flowerBouquet = _context.Bouquets.Where(b => b.BouquetId== bouquetId).FirstOrDefault();

            var bouquetFlower = new BouquetFlower()
            {
                Bouquet = flowerBouquet,
                Flower = flower,
            };

            _context.Add(bouquetFlower);

            _context.Add(flower);

            return Save();
        }

        public bool FlowerExists(int Id)
        {
            return _context.Flowers.Any(f => f.FlowerId == Id);
        }

        public Flower GetFlowerById(int id)
        {
            return _context.Flowers.Where(f => f.FlowerId == id).FirstOrDefault();
        }

        public Flower GetFlowerByName(string name)
        {
            return _context.Flowers.Where(f => f.FlowerName == name).FirstOrDefault();
        }

        public ICollection<Flower> GetFlowers() 
        {
            return _context.Flowers.OrderBy(f => f.FlowerId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
