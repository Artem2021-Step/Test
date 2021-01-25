using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace Dovidnuk
{
    public static class dov
    {

        public static OleDbConnection connection;
        public static OleDbDataAdapter adapter;
        public static DataTable table;
        public static DataView dataView;
        public static int currentRow = -1;
    }
}
