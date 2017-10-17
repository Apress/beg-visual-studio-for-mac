using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private OrderContext dataContext;

        public OrderController(OrderContext context)
        {
            this.dataContext = context;
            this.dataContext.Database.Migrate();
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return this.dataContext.Orders;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Order Get(int id)
        {
            return this.dataContext.Orders.Where(o => o.Id == id).FirstOrDefault();
        }

        // POST api/order
        [HttpPost]
        public async void Post([FromBody]Order order)
        {
            if(order!=null)
            {
                this.dataContext.Orders.Add(order);
                await this.dataContext.SaveChangesAsync();
            }
        }

        // PUT api/order/5
        [HttpPut("{id}")]
        public async void Put(int id, [FromBody]Order order)
        {
            if(id!=0)
            {
				var oldOrder = this.dataContext.Orders.Where(o => o.Id == id).FirstOrDefault();
                if(oldOrder!=null)
                {
                    oldOrder.OrderDate = order.OrderDate;
                    oldOrder.OrderDescription = order.OrderDescription;
                    oldOrder.CustomerName = order.CustomerName;

                    await this.dataContext.SaveChangesAsync();
                }
            }

        }

        // DELETE api/order/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            var order = this.dataContext.Orders.Where(o => o.Id == id).FirstOrDefault();
            if(order!=null)
            {
                this.dataContext.Orders.Remove(order);
            }

            await this.dataContext.SaveChangesAsync();
        }
    }
}
