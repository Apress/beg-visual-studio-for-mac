using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using NewsReader.Model;
using Xamarin.Forms;

namespace NewsReader.Services
{
	public static class ItemService
	{
		public static string FeedUri = "https://s.ch9.ms/Feeds/RSS";
		private static XNamespace mediaNS = XNamespace.Get("http://search.yahoo.com/mrss/");

		private static string GetThumbnail(XElement node)
		{
			var images = node.Descendants(mediaNS + "thumbnail");

			string imageUrl;

			switch (Device.Idiom)
			{
				case TargetIdiom.Phone:
                    // Take the first image (smaller)
					imageUrl = images.FirstOrDefault().Attribute("url").Value;
					break;
				default:
                    // Take a larger image if available
					if (images.Count() > 1)
					{
						imageUrl = images.Skip(1).FirstOrDefault().Attribute("url").Value;
					}
					else
					{
						imageUrl = images.FirstOrDefault().Attribute("url").Value;
					}
					break;
			}
			return imageUrl;
		}

		// Query the RSS feed with LINQ and return an IEnumerable of Item
		public static async Task<IEnumerable<Item>> QueryRssAsync()
		{
			var client = new HttpClient();

            var data = await client.GetStringAsync(FeedUri);

			var doc = XDocument.Parse(data);
			var dcNS = XNamespace.Get("http://purl.org/dc/elements/1.1/");

			var query = (from video in doc.Descendants("item")
						 select new Item
						 {
							 Title = video.Element("title").Value,
							 Author = video.Element(dcNS + "creator").Value,
							 Link = video.Element("link").Value,
							 Thumbnail = GetThumbnail(video),
							 PublicationDate = DateTime.Parse(video.Element("pubDate").Value,
								 System.Globalization.CultureInfo.InvariantCulture)
						 });

            client.Dispose();
			return query;
		}
	}
}
