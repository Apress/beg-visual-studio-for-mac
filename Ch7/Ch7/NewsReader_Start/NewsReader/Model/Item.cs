using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NewsReader.Model
{
	public class Item : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string name = "") =>
			   PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

		private string title;
		public string Title
		{
			get
			{
				return title;
			}

			set
			{
				title = value;
				OnPropertyChanged();
			}
		}

		private string link;
		public string Link
		{
			get
			{
				return link;
			}

			set
			{
				link = value;
				OnPropertyChanged();
			}
		}

		private string author;
		public string Author
		{
			get
			{
				return author;
			}

			set
			{
				author = value;
				OnPropertyChanged();
			}
		}

        private DateTime publicationDate;
		public DateTime PublicationDate
		{
			get
			{
				return publicationDate;
			}

			set
			{
				publicationDate = value;
				OnPropertyChanged();
			}
		}

		private string thumbnail;
		public string Thumbnail
		{
			get { return thumbnail; }
			set
			{
				thumbnail = value;
				OnPropertyChanged();
			}
		}
	}
}
