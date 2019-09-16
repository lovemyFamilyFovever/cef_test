using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEF_test
{
    public class CefConnectAccess
    {
        public string getTableData(string password)
        {    
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source = " + System.Environment.CurrentDirectory+ "\\possword.accdb;Jet OLEDB:DataBase Password="+ password + "";
            string state = TestConnection(connectionString);
            if (state == "success")
            {
                string sql = "SELECT   title, account, [password], program_name, url FROM original";
                OleDbDataAdapter dbDataAdapter = new OleDbDataAdapter(sql, connectionString); //创建适配对象
                DataTable dt = new DataTable(); //新建表对象
                dbDataAdapter.Fill(dt); //用适配对象填充表对象
                var result = DataTableToJsonWithJsonNet(dt);
                return result;
            }
            else{
                return state;
            }           
        }
        
        /// <summary>
        /// 格式转换
        /// </summary>
        /// <param name="table">缓存表</param>
        /// <returns>返回json格式的表数据</returns>
        public string DataTableToJsonWithJsonNet(DataTable table)
        {
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(table);
            return JsonString;
        }


        public string TestConnection(string connectionString)
        {
            OleDbConnection accConnection = new OleDbConnection(connectionString);
            try
            {
                accConnection.Open();
            }
            catch (OleDbException ex)
            {
                return ex.ToString();
            }
            if (accConnection.State != ConnectionState.Open)
            {
                return "无法打开文件";
            }
            else
            {
                return "success";
            }
        }
    }
}
