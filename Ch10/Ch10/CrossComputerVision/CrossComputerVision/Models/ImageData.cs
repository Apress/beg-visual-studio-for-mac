using System;
using Microsoft.EntityFrameworkCore;

namespace CrossComputerVision.Models
{
	public class ImageData
	{
		public int Id { get; set; }
		public string ImageName { get; set; }
		public string DetectionResult { get; set; }
		public DateTime TimeStamp { get; set; }
	}

	public class ImageContext : DbContext
	{
		public DbSet<ImageData> ImageList { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=ComputerVision.db");
		}
	}
}
