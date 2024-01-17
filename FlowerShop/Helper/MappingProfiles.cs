using AutoMapper;
using FlowerShop.DTO;
using FlowerShop.Models;

namespace FlowerShop.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Flower, FlowerDto>();
            CreateMap<Bouquet, BouquetDto>();
            CreateMap<Gift, GiftDto>();
            CreateMap<Order, OrderDto>();   
            CreateMap<Client, ClientDto>();
            CreateMap<BouquetDto, Bouquet>();
            CreateMap<ClientDto, Client>();
            CreateMap<GiftDto, Gift>();
            CreateMap<OrderDto, Order>();
        }
    }
}
