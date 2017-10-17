using System;
using System.Collections.Generic;
using NewsReader.Model;
using Xamarin.Forms;

namespace NewsReader.Pages
{
    public partial class WebContentPage : ContentPage
    {
		string link;

		public WebContentPage(Item feedItem)
		{
			InitializeComponent();

            this.link = feedItem.Link;
            this.Title = feedItem.Title;
		}

        public WebContentPage()
        {
            InitializeComponent();
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();
			WebView1.Source = link;
		}
    }
}
