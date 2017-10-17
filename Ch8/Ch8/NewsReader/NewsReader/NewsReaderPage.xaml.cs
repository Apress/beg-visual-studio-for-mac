using NewsReader.Model;
using NewsReader.Pages;
using NewsReader.ViewModel;
using Xamarin.Forms;
using System;
using System.Threading.Tasks;

namespace NewsReader
{
    public partial class NewsReaderPage : ContentPage
    {
        private ItemViewModel viewModel;

        public NewsReaderPage()
        {
            InitializeComponent();

            this.viewModel = new ItemViewModel();
        }

        private async Task LoadDataAsync(bool forceReload)
        {
			try
            {
                this.NewsActivity.IsVisible = true;
                this.NewsActivity.IsRunning = true;
                await this.viewModel.InitializeAsync(forceReload);
                this.RssView.ItemsSource = this.viewModel.Items;
                this.NewsActivity.IsVisible = false;
                this.NewsActivity.IsRunning = false;
			} 
            catch (InvalidOperationException)
            {
                await DisplayAlert("Error", "Check your Internet connection", "OK");
                return;
            }
            catch (Exception ex) 
            {
                await DisplayAlert("Error", ex.Message, "OK");
                return;
			}
        }

		protected override async void OnAppearing()
		{
			base.OnAppearing();
            await LoadDataAsync(false);
		}

		private async void Handle_Refreshing(object sender, System.EventArgs e)
		{
			await LoadDataAsync(true);
		}

		private async void RssView_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			var selected = e.Item as Item;

			if (selected != null)
            {
                WebContentPage webPage = new WebContentPage(selected);
				await Navigation.PushAsync(webPage);                
            }
		}


    }
}
