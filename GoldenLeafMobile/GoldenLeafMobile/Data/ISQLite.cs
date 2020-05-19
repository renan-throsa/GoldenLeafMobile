using SQLite;
namespace GoldenLeafMobile.Data
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
