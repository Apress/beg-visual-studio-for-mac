using System;
using NewsReader;
using Xamarin.Forms;
using NewsReader.Droid;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using System.IO;

[assembly: Dependency(typeof(DataAccess))]
namespace NewsReader.Droid
{
    public class DataAccess : IDataAccess
    {
        public SQLiteConnection DbConnection()
        {
            string dbName = "newsreader.db3";
            string dbPath = 
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), 
                             dbName);

            return 
                new SQLiteConnection(new SQLitePlatformAndroid(), 
                                     dbPath);
        }
    }
}
