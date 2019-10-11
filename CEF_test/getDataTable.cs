using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEF_test
{
    public class GetDataTable
    {
        public string returnGisTable()
        {
            Tools tool = new Tools();
            string connectionString = tool.getConnectionStringsConfig("gisConnectionString");
            OracleConnection orcl = new OracleConnection(connectionString);
            try
            {
                orcl.Open();

                string selectSql = "select * from STATISTICS_CONFIG";
                OracleDataAdapter da = new OracleDataAdapter(selectSql, orcl);
                DataTable dt = new DataTable();
                da.Fill(dt);
                //string dataTableJson =tool.dataToJson(dt);
                string dataTableJson= JsonConvert.SerializeObject(dt);
                return dataTableJson;
            }
            catch(Exception ex)
            {
                return "";
            }
            finally
            {
                orcl.Close();
            }
        }

    }
}
