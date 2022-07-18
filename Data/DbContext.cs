using ManagementAssistantTester.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementAssistantTester.Data
{
    public class DbContext
    {
        private String _connectionString = ConfigurationManager.AppSettings.Get("connectionString");

        public event EventHandler<EventArgs> RowValueExist;
        protected virtual void OnRowValueExist(EventArgs e)
        {
            if (RowValueExist != null)
            {
                RowValueExist(this, e);
            }
        }
        public event EventHandler<EventArgs> AddRowValueSuccess;
        protected virtual void OnAddRowValueSuccess(EventArgs e)
        {
            if (AddRowValueSuccess != null)
            {
                AddRowValueSuccess(this, e);
            }
        }
        public event EventHandler<EventArgs> AddRowValueFail;
        protected virtual void OnAddRowValueFail(EventArgs e)
        {
            if (AddRowValueFail != null)
            {
                AddRowValueFail(this, e);
            }
        }
        public event EventHandler<EventArgs> ViewChartFail;
        protected virtual void OnViewChartFail(EventArgs e)
        {
            if (ViewChartFail != null)
            {
                ViewChartFail(this, e);
            }
        }

        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------

        public Boolean AddRowValue(DataRowModel dataRow)
        {
            Int32 id = 0;
            List<Int32> ids = new List<Int32>();
            SqlCommand cmd;
            SqlConnection cnn = new SqlConnection(_connectionString);
            SqlDataReader reader;
            try
            {
                String sql = $"SELECT Id, Xvalue FROM [dbo].[_Tester]" +
                    $" WHERE Symbol = \'{dataRow.Symbol}\'";
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                    ids.Add(id);
                    if (reader.GetInt32(1) == dataRow.Xvalue)
                    {
                        OnRowValueExist(new EventArgs());
                        cnn.Close();
                        cnn.Dispose();
                        return false;
                    }
                }
                cnn.Close();
                id = 0;
                foreach(Int32 _id in ids)
                {
                    if (id == _id || ids.Contains(id))
                        id++;
                    else 
                        break;
                }
            }
            catch 
            {
                OnAddRowValueFail(new EventArgs());
                return false; 
            }
            try
            {
                String sql = $"INSERT [dbo].[_Tester] (Id, Xvalue, [Open], [High], [Low], [Close], [Symbol], [Name])" +
                    $" VALUES ({id}, \'{dataRow.Xvalue}\', \'{dataRow.Open}\'," +
                    $" \'{dataRow.High}\', \'{dataRow.Low}\', \'{dataRow.Close}\'," +
                    $" \'{dataRow.Symbol}\', \'{dataRow.Name}\')";
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                Int32 row = cmd.ExecuteNonQuery();
                if(row > 0)
                {
                    OnAddRowValueSuccess(new EventArgs());
                    cnn.Close();
                    cnn.Dispose();
                    return true;
                }
                else
                {
                    OnAddRowValueFail(new EventArgs());
                    cnn.Close();
                    cnn.Dispose();
                    return false;
                }
            }
            catch
            {
                OnAddRowValueFail(new EventArgs());
                return false;
            }
        }
        public Boolean RemoveRowValue(DataRowModel dataRow)
        {
            SqlCommand cmd;
            SqlConnection cnn = new SqlConnection(_connectionString);
            SqlDataReader reader;
            try
            {
                String sql = $"DELETE [dbo].[_Tester] WHERE Id = \'{dataRow.Id}\' AND Symbol = \'{dataRow.Symbol}\'";
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                Int32 row = cmd.ExecuteNonQuery();
                if(row > 0) { return true; }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public DataTable GetDataTable(String symbol)
        {
            DataTable dt = new DataTable();
            SqlConnection cnn = new SqlConnection(_connectionString);
            SqlDataReader reader;
            try
            {
                String sql = $"SELECT * FROM [dbo].[_Tester] WHERE" +
                    $" Symbol = \'{symbol}\'";
                SqlDataAdapter da = new SqlDataAdapter(sql, cnn);
                da.Fill(dt);
                return dt;
            }
            catch
            {
                return dt;
            }
        }

        public List<DataRowModel> GetRawsList(String symbol)
        {
            List<DataRowModel> raws = new List<DataRowModel>();
            SqlCommand cmd;
            SqlConnection cnn = new SqlConnection(_connectionString);
            SqlDataReader reader;
            try
            {
                String sql = $"SELECT * FROM [dbo].[_Tester] WHERE Symbol = \'{symbol}\'";
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        DataRowModel dataRow = new DataRowModel();
                        dataRow.Id = reader.GetInt32(0);
                        dataRow.Symbol = reader.GetString(1);
                        dataRow.Xvalue = reader.GetInt32(2);
                        dataRow.Open = reader.GetString(3);
                        dataRow.High = reader.GetString(4);
                        dataRow.Low = reader.GetString(5);
                        dataRow.Close = reader.GetString(6);
                        try
                        {
                            dataRow.Volume = reader.GetString(7);
                            dataRow.Name = reader.GetString(8);
                        }
                        catch
                        {
                            dataRow.Volume = String.Empty;
                            dataRow.Name = String.Empty;
                        }
                        raws.Add(dataRow);
                    }
                }
                cnn.Close();
                cnn.Dispose();
                if(raws.Count == 0) { OnViewChartFail(new EventArgs()); }
                return raws;
            }
            catch
            {
                OnViewChartFail(new EventArgs());
                return raws;
            }
        }
        public List<String> GetSymbols()
        {
            List<String> symbols = new List<String>();
            SqlCommand cmd;
            SqlConnection cnn = new SqlConnection(_connectionString);
            SqlDataReader reader;
            try
            {
                String sql = $"SELECT Symbol FROM [dbo].[_Tester]";
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        String symbol = reader.GetString(0);
                        if(!symbols.Contains(symbol))
                            symbols.Add(symbol);
                    }
                }
                return symbols;
            }
            catch
            {
                return symbols;
            }
        }

        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------
    }
}
