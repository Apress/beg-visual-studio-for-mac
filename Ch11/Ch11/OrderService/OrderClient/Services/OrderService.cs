using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OrderClient.Model;

namespace OrderClient.Services
{
	public class OrderService
	{
		public async Task<List<Order>> GetOrdersAsync()
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(ServiceUrl);
				string jsonResult = await client.GetStringAsync("/api/order");
				var orders = JsonConvert.DeserializeObject<List<Order>>(jsonResult);
				return orders;
			}
		}

		public async Task CreateOrderAsync(Order newOrder)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(ServiceUrl);
				var content = new StringContent(JsonConvert.SerializeObject(newOrder),
												Encoding.UTF8, "application/json");

				await client.PostAsync("/api/order", content);
			}
		}

		public async Task UpdateOrderAsync(Order order)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(ServiceUrl);
				string stringData = JsonConvert.SerializeObject(order);
				var content = new StringContent(stringData,
												Encoding.UTF8, "application/json");

				await client.PutAsync($"/api/order/{order.Id}", content);
			}
		}
		public async Task DeleteOrderAsync(Order order)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(ServiceUrl);
				await client.DeleteAsync($"/api/order/{order.Id}");
			}
		}

		public OrderService(string serviceUrl)
		{
			this.ServiceUrl = serviceUrl;
		}

		public string ServiceUrl { get; }
	}
}
