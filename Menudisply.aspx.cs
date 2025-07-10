using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Menudisply : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=db_menu;User ID=sa;Password=apple");   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetMenuData();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
       
    }

    private void GetMenuData()
    {
        var UserId = Request.Cookies["UserId"].Value;
        DataTable table = new DataTable();
        string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString;

        SqlConnection conn = new SqlConnection(strCon);
        //string sql = "select menuId, Menu_title, MainMenuId from SET_Menu where menuId in (select MenuId from SET_UserGrp_Menu where UserGrpId = '862767BE-CB6E-4DF3-8181-03C3DF1D1906');";
        string sql = "SELECT MENUID,Menu_title FROM SET_MENU Where MenuId IN (SELECT MenuId FROM SET_UserGrp_Menu WHERE UserGrpId In (SELECT UserGrpId FROM SET_Assign_UserGrp WHERE UserId='"+UserId+"') AND Record_Deleted='0') And Menu_SubMenu='M' And Hide='N' And Record_Deleted='0'";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(table);
        DataView view = new DataView(table);
        view.RowFilter = "MainMenuId is NULL";
        foreach (DataRowView row in view)
        {
            MenuItem menuItem = new MenuItem(row["Menu_title"].ToString(),
            row["menuId"].ToString());
            //menuItem.NavigateUrl = row["menu_url"].ToString();
            menuBar.Items.Add(menuItem);
            AddChildItems(table, menuItem);
        }
    }

    private void AddChildItems(DataTable table, MenuItem menuItem)
    {
        DataView viewItem = new DataView(table);
        viewItem.RowFilter = "MainMenuId=" +"'"+ menuItem.Value+"'";
        foreach (DataRowView childView in viewItem)
        {
            MenuItem childItem = new MenuItem(childView["Menu_title"].ToString(),
            childView["menuId"].ToString());
            //childItem.NavigateUrl = childView["menu_url"].ToString();
            menuItem.ChildItems.Add(childItem);
            AddChildItems(table, childItem);
        }
    }

}