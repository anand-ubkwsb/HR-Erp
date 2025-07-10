using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default6 : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=HRERPSys;User ID=sa;Password=@pple123");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("select  CompId,CompName from SET_Company", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                DropDownList1.DataSource = dt;
                DropDownList1.DataTextField = "CompName";
                DropDownList1.DataValueField = "CompId";
                DropDownList1.DataBind();
            }

            con.Close();
        }
    }

    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        //GridView1.UseAccessibleHeader = true;
       // GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

   public  void change()
    {
       Label1.Text = TextBox1.Text;
    }





    protected void TextBox2_TextChanged(object sender, EventArgs e)
   {
        ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "alertme()", true);
      //  Label1.Text = TextBox2.Text;
        
    }

    
}