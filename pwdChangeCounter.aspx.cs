using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pwdChangeCounter : System.Web.UI.Page
{
   SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        
        counterdays();
    }
    public void counterdays()
    {
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("select PwdChangeCounter from SET_User_Manager where  LoginName = 'fahad'", con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            int counter = int.Parse(ds.Tables[0].Rows[0]["PwdChangeCounter"].ToString());

            int res = 30 - counter;
            if (res > 0 && res <= 30)
            {
                lblcounterdays.Text = res.ToString();
            }
            else
            {
                lblcounterdays.Text = "0";
            }
        }
    }
}