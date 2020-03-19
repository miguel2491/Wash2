using System;
using System.Collections.Generic;
using System.Text;

namespace Wash2.SQLiteDB
{
    public interface ISQLite
    {
        SQLite.SQLiteConnection GetConnection();
    }
}
