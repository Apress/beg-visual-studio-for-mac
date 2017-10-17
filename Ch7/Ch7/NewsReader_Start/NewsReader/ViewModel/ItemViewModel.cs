using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NewsReader.Model;
using NewsReader.Services;

namespace NewsReader.ViewModel
{
	public class ItemViewModel : INotifyPropertyChanged
	{

		private bool isBusy;
		public bool IsBusy
		{
			get
			{
				return isBusy;
			}
			set
			{
				isBusy = value;
				OnPropertyChanged();
			}
		}

        private ObservableCollection<Item> items;
		public ObservableCollection<Item> Items 
        { 
            get
            { 
                return items; 
            }
            set
            {
                items = value;
                OnPropertyChanged();
            }
        }

		public ItemViewModel()
		{
			this.Items = new ObservableCollection<Item>();
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged([CallerMemberName]string name = "") =>
			   PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

		public async Task InitializeAsync()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				var result = await ItemService.QueryRssAsync();
				if (result != null)
				{
					this.Items = null;
					this.Items = new ObservableCollection<Item>(result);
				}
			}
			catch (Exception)
			{

                this.Items = null;
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
