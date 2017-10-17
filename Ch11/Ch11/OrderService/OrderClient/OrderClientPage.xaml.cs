using OrderClient.Model;
using OrderClient.ViewModel;
using Xamarin.Forms;

namespace OrderClient
{
	public partial class OrderClientPage : ContentPage
	{
		private Order currentOrder;
		private OrderViewModel viewModel;

		public OrderClientPage()
		{
			InitializeComponent();
			// Create an instance of the ViewModel
			this.viewModel = new OrderViewModel();
		}

		// Load and bind the collection of orders
		protected async override void OnAppearing()
		{
			this.IsBusy = true;
			await this.viewModel.InitAsync();
			this.OrdersListView.ItemsSource = this.viewModel.Orders;
			this.IsBusy = false;
		}

		// Add a new order to the data-bound collection
		private void AddButton_Clicked(object sender, System.EventArgs e)
		{
			this.viewModel.Orders.Add(new Order());
		}

		// Save all items in the collection. The SaveOrderAsync in 
		// the ViewModel knows where to create or update an item
		private async void SaveButton_Clicked(object sender, System.EventArgs e)
		{
			this.IsBusy = true;
			foreach (Order order in this.viewModel.Orders)
			{
				await this.viewModel.SaveOrderAsync(order);
			}
			this.IsBusy = false;
		}

		// Remove the current order (if not null)
		private async void DeleteButton_Clicked(object sender, System.EventArgs e)
		{
			this.IsBusy = true;
			if (this.currentOrder != null)
				await this.viewModel.DeleteOrderAsync(this.currentOrder);
			this.IsBusy = false;
		}

		// Take note of the selected order
		private void OrdersListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var order = e.SelectedItem as Order;
			if (order != null) this.currentOrder = order;
		}
	}
}

