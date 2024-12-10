using System.Data;
using System.Data.SqlClient;

namespace Practice.Data
{
    internal class Msdblayer
    {
        internal bool ExecuteSP(string v, SqlParameter[] paramArr1, ref SqlParameter[] paramArr2)
        {
            throw new NotImplementedException();
        }

        internal bool ExecuteSP(string v1, SqlParameter[] paramArr, DataSet ds, string v2)
        {
            throw new NotImplementedException();
        }
    }
}