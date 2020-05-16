using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoldenLeafMobile.Data
{
    public interface ISQLite: IDisposable
    {
        SQLiteConnection GetConnection();
    }
}
