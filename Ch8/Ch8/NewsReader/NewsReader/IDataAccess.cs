using SQLite.Net;
namespace NewsReader
{
    public interface IDataAccess
    {
        SQLiteConnection DbConnection();
    }
}
