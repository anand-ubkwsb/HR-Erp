using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //bind the gridview data
            gd12.DataSource = SqlDataSource1;
            gd12.DataBind();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    //add the thead and tbody section programatically
        //    e.Row.TableSection = TableRowSection.TableHeader;

            
        //}
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    //add the thead and tbody section programatically
        //    e.Row.TableSection = TableRowSection.TableFooter;
        //}
        


    }

    protected void gd12_PreRender(object sender, EventArgs e)
    {
       
        gd12.HeaderRow.TableSection = TableRowSection.TableHeader;
        
        gd12.FooterRow.TableSection = TableRowSection.TableFooter;

    }

    [System.Web.Services.WebMethod]
    public static string DeleteClick()
    {
        //Do your Stuff Here.
        return "ButtonClicked";
    }

    protected void btnSweetAlert_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "functionConfirm()", true);
    }
}