using System;
namespace OrderService.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderDescription { get; set; }
        public string CustomerName { get; set; }
    }
}
