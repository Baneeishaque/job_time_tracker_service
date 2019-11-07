using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public class DBhandler
{
    SqlConnection con;

    public DBhandler()
    {
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["ConnectionString"];
        if (settings != null)
        {
            con = new SqlConnection(settings.ConnectionString);
        }
    }

    public string SingleInsert(String query)
    {
        if (con != null)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                int result = cmd.ExecuteNonQuery();
                con.Close();
                if (result == 1)
                {
                    return "1";
                }
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }
        return "0";
    }
}





