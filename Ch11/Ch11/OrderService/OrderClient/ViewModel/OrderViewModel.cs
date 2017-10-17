using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using OrderClient.Model;
using OrderClient.Services;

namespace OrderClient.ViewModel
{
	public class OrderViewModel
	{
		private OrderService service;
		public OrderViewModel()
		{
			this.Orders = new ObservableCollection<Order>();
			this.service = new OrderService("http://orderservice.azurewebsites.net");
		}

		public ObservableCollection<Order> Orders { get; set; }

		public async Task InitAsync()
		{
			var orders = await service.GetOrdersAsync();

			if (orders != null)
				this.Orders = new ObservableCollection<Order>(orders);
		}

		public async Task SaveOrderAsync(Order order)
		{
			if (order.Id == 0)
				await service.CreateOrderAsync(order);
			else
				await service.UpdateOrderAsync(order);
		}

		public async Task DeleteOrderAsync(Order order)
		{
			if (order.Id == 0)
				this.Orders.Remove(order);
			else
				await service.DeleteOrderAsync(order);
		}
	}
}
