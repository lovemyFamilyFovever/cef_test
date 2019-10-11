using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace CEF_test
{
    public class Tools
    {
        /// <summary>
        /// 获取本地ip地址，多个ip
        /// </summary>
        public String[] getLocalIpAddress()
        {
            string hostName = Dns.GetHostName();                    //获取主机名称  
            IPAddress[] addresses = Dns.GetHostAddresses(hostName); //解析主机IP地址  

            string[] IP = new string[addresses.Length];             //转换为字符串形式  
            for (int i = 0; i < addresses.Length; i++)
                IP[i] = addresses[i].ToString();

            return IP;
        }

        /// <summary>
        /// 获取appconfig的数据库连接字符串
        /// </summary>
        /// <param name="connectionName">appName</param>
        /// <returns></returns>
        public string getConnectionStringsConfig(string connectionName)
        {
            string connectionString= ConfigurationManager.ConnectionStrings[connectionName].ToString();
            return connectionString;
        }

        /// <summary>
        /// 测试数据库连接字符串
        /// </summary>
        /// <param name="connectionStrings">连接字符串</param>
        /// <returns></returns>
        public bool testConnectionStrings(string connectionStrings)
        {
            //Data Source=192.168.186.133:1521/orcl;Persist Security Info=True;User ID=jyspjmis;Password=jyspjmis;Unicode=True
            bool IsCanConnectioned = false;
            OracleConnection orcl = new OracleConnection(connectionStrings);          
            try
            {
                orcl.Open();
                IsCanConnectioned = true;
            }
            catch
            {
                IsCanConnectioned = false;
            }
            finally
            {                
                orcl.Close();
            }
            if (orcl.State == ConnectionState.Closed || orcl.State == ConnectionState.Broken)
                return IsCanConnectioned;
            else
                return IsCanConnectioned;
        }

        /// <summary>
        /// 更新appconfig的数据库连接字符串
        /// </summary>
        /// <param name="name">节点名</param>
        /// <param name="connectionStrings">更新的内容</param>
        public string updateConnectionStrings(string name,string connectionStrings)
        {
            try
            {
                string file = System.Windows.Forms.Application.ExecutablePath;
                Configuration config = ConfigurationManager.OpenExeConfiguration(file);

                config.ConnectionStrings.ConnectionStrings.Remove(name);

                ConnectionStringSettings mySettings =new ConnectionStringSettings(name, connectionStrings, "System.Data.OracleClient");

                config.ConnectionStrings.ConnectionStrings.Add(mySettings);
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("ConnectionStrings");

                return "success";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }            
        }

        /// <summary>
        /// Datatable转换为Json
        /// </summary>
        /// <param name="dt">内存表</param>
        /// <returns></returns>
        public string dataToJson(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return "[{}]";
            StringBuilder jsonString = new StringBuilder();

            jsonString.Append("[");
            DataRowCollection drc = dt.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                jsonString.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string strKey = dt.Columns[j].ColumnName;
                    string strValue = drc[i][j].ToString();
                    Type type = dt.Columns[j].DataType;
                    jsonString.Append("\"" + strKey + "\":");
                    strValue = String.Format(strValue, type);
                    if (j < dt.Columns.Count - 1)
                        jsonString.Append(strValue + ",");
                    else
                        jsonString.Append(strValue);
                }
                jsonString.Append("},");
            }
            jsonString.Remove(jsonString.Length - 1, 1);
            jsonString.Append("]");
            return jsonString.ToString();
        }
    }
}
