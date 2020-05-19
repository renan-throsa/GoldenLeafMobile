using GoldenLeafMobile.Data;
using GoldenLeafMobile.Droid;
using SQLite;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteAndroid))]
namespace GoldenLeafMobile.Droid
{

    class SQLiteAndroid : ISQLite
    {
        private const string fileNameDB = "GoldenLeafMobile.db3";
        
        public SQLiteConnection GetConnection()
        {
            var pathDB = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.Path, fileNameDB);
            return new SQLiteConnection(pathDB);
        }
    }
}