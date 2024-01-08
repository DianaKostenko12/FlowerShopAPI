namespace FlowerShop.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
