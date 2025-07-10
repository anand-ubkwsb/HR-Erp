using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [System.Web.Services.WebMethod]
    public static string DeleteClick(int id)
    {
        string conString = ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString;
        string query = "delete from tbl_Delete where id = '"+id+"'";
        SqlCommand cmd = new SqlCommand(query);
        SqlConnection con = new SqlConnection(conString);
        cmd.Connection = con;
        con.Open();
        int i = cmd.ExecuteNonQuery();
        con.Close();
        return i > 0 ? "Deleted" : "Failed";

    }
}