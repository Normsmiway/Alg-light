using System.Collections.Generic;
using System.Data;


namespace Alg.Domain
{
   public static class AlgContext
    {
        static Db db = new AlgDb();

        public static Reports Reports { get { return new Reports(); } }
        public static Agents Agents { get { return new Agents(); } }
        public static Groups Groups { get { return new Groups(); } }
        public static Schedules Schedules { get { return new Schedules(); } }

        // general purpose operations

        public static void Execute(string sql, params object[] parms) { db.Execute(sql, parms); }
        public static IEnumerable<dynamic> Query(string sql, params object[] parms) { return db.Query(sql, parms); }
        public static object Scalar(string sql, params object[] parms) { return db.Scalar(sql, parms); }

        public static DataSet GetDataSet(string sql, params object[] parms) { return db.GetDataSet(sql, parms); }
        public static DataTable GetDataTable(string sql, params object[] parms) { return db.GetDataTable(sql, parms); }
        public static DataRow GetDataRow(string sql, params object[] parms) { return db.GetDataRow(sql, parms); }
    }
}
