using System;
using System.IO;
using NewsReader;
using NewsReader.iOS;
using SQLite.Net;
using Xamarin.Forms;

[assembly: Dependency(typeof(DataAccess))]
namespace NewsReader.iOS
{
    public class DataAccess: IDataAccess
    {
        public SQLiteConnection DbConnection()
        {
            string dbName = "newreader.db3";
            string personalFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            string libraryFolder = Path.Combine(personalFolder, "..", "Library");
            string dbPath = Path.Combine(libraryFolder, dbName);

            return 
                new SQLiteConnection(new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS(), 
                                     dbPath);
        }
    }
}
