using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Default4 : System.Web.UI.Page
{
    string Usergrpid, id, st;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //RadMenuItem item1 = new RadMenuItem();
            //item1.Text = "Item 1";
            //RadMenuItem item11 = new RadMenuItem();
            //item11.Text = "Child 1";
            //item1.Items.Add(item11);
            //RadMenuItem item2 = new RadMenuItem();
            //item2.Text = "Item 2";
            //RadMenuItem item3 = new RadMenuItem();
            //item3.Text = "Go to the Telerik site";
            //item3.NavigateUrl = " http://www.telerik.com";
            //RadMenu1.Items.Add(item1);
            //RadMenu1.Items.Add(item2);
            //RadMenu1.Items.Add(item3);




            id = "6b9c1166-0f4b-41dc-99e8-b47be96c8157";
            Usergrpid = "ff43b221-f9e1-4423-aa61-f12880a9e13d";
            //Usergrpid = Request.QueryString.Get("UsergrpID");
            st = "2019-2020";


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("select USER_NAME from SET_User_Manager where UserId='" + id + "'", con);
            DataSet ds = new DataSet();

            da.Fill(ds);
            string username = ds.Tables[0].Rows[0]["USER_NAME"].ToString();
            ViewState["fiyr"] = st;
            Response.Cookies["fiscalYear"].Value = st;
            Response.Cookies["fiscalYear"].Expires = DateTime.Now.AddDays(1);
            string str = Usergrpid;
            GetMenuData();



        }
    }

        private void GetMenuData()
    {

        DataTable table = new DataTable();
        string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString;
        SqlConnection conn = new SqlConnection(strCon);
        string sql = "select SET_UserGrp_Menu.MenuId, SET_Menu.Menu_title, SET_Menu.MainMenuId,SET_Menu.MenuPath ,SET_UserGrp_Menu.Hide as userhide , case when SET_UserGrp_Menu.SortOrder is null THEN SET_Menu.SortOrder else SET_UserGrp_Menu.SortOrder end as OrignalOrder from SET_UserGrp_Menu left join SET_Menu on SET_UserGrp_Menu.MenuId = SET_Menu.menuId where SET_UserGrp_Menu.UserGrpId='" + Usergrpid + "' and  SET_Menu.Hide = 'N'  and SET_UserGrp_Menu.Hide = 'N' and SET_UserGrp_Menu.Record_Deleted = '0' ORDER BY 6 ";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(table);
        DataView view = new DataView(table);
        view.RowFilter = "MainMenuId is NULL";
        foreach (DataRowView row in view)
        {
            
            RadMenuItem menuItem = new RadMenuItem();
            menuItem.Text = row["Menu_title"].ToString();
            menuItem.Value = row["menuId"].ToString();
            menuItem.NavigateUrl = row["MenuPath"].ToString() + "?UserID=" + id + "&UsergrpID=" + Usergrpid + "&fiscaly=" + st;

            RadMenu1.Items.Add(menuItem);
            AddChildItems(menuItem);
            AddChildItemsform(menuItem);
        }


    }

    private void AddChildItems(RadMenuItem menuItem)
    {
        DataTable table = new DataTable();
        string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString;
        SqlConnection conn = new SqlConnection(strCon);
        string sql = "select SET_UserGrp_Menu.MenuId, SET_Menu.Menu_title, SET_Menu.MainMenuId,SET_Menu.MenuPath ,SET_UserGrp_Menu.Hide as userhide , case when SET_UserGrp_Menu.SortOrder is null THEN SET_Menu.SortOrder else SET_UserGrp_Menu.SortOrder end as OrignalOrder from SET_UserGrp_Menu left join SET_Menu on SET_UserGrp_Menu.MenuId = SET_Menu.menuId where SET_UserGrp_Menu.UserGrpId='" + Usergrpid + "' and  SET_Menu.Hide = 'N'  and SET_UserGrp_Menu.Hide = 'N' and SET_UserGrp_Menu.Record_Deleted = '0' ORDER BY 6 ";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(table);
        
       
        DataView view = new DataView(table);
        view.RowFilter = "MainMenuId = " + "'" + menuItem.Value + "'";
        foreach (DataRowView childView in view)
        {
            RadMenuItem childItem = new RadMenuItem();
            childItem.Text = childView["Menu_title"].ToString();
            childItem.Value =  childView["menuId"].ToString();

            // childItem.NavigateUrl = childView["MenuPath"].ToString() + "?UserID=" + id + "&UsergrpID=" + Usergrpid + "&fiscaly=" + st;
            menuItem.Items.Add(childItem);
            AddChildItems(childItem);
            AddChildItemsform(childItem);
        }



    }
    private void AddChildItemsform(RadMenuItem menuItem)
    {
        DataTable table = new DataTable();
        string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString;
        SqlConnection conn = new SqlConnection(strCon);
        string sql = "select SET_UserGrp_Form.FormId, SET_Form.FormTitle,SET_UserGrp_Form.MenuId,SET_Form.FormPath ,SET_UserGrp_Form.Hide as userhide , case when SET_UserGrp_Form.SortOrder is null THEN SET_Form.SortOrder ELSE SET_UserGrp_Form.SortOrder end as OrginalOrder from SET_UserGrp_Form left join SET_Form on SET_UserGrp_Form.FormId = SET_Form.FormId where SET_UserGrp_Form.UserGrpId = '" + Usergrpid + "' and  SET_Form.Hide = 'N' and SET_UserGrp_Form.Hide = 'N' and SET_UserGrp_Form.Record_Deleted = '0' order by 6 ";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(table);
        DataView view = new DataView(table);

        
      

        view.RowFilter = "MenuId = " + "'" + menuItem.Value + "'";
        foreach (DataRowView childView in view)
        {
            RadMenuItem childItem = new RadMenuItem();
            childItem.Text = childView["FormTitle"].ToString();
            childItem.Value =  childView["FormId"].ToString();

            childItem.NavigateUrl = childView["FormPath"].ToString() + "?UserID=" + id + "&UsergrpID=" + Usergrpid + "&fiscaly=" + st + "&FormID=" + childView["FormId"].ToString();
            menuItem.Items.Add(childItem);
            AddChildItemsform(childItem);
        }

    }













    protected void Button1_Click(object sender, EventArgs e)
    {
        
    }
   

   

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        //Label1.Text = DropDownList1.SelectedItem.Value;
        
    }


    protected void Button1_Click1(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(5000);
    }

    protected void RadMenu1_ItemClick(object sender, RadMenuEventArgs e)
    {
        //switch (e.Item.Value)
        //{
        //    case "R":
        //        // add a new root item  
        //        RadMenuItem RootItem = new RadMenuItem();
        //        RootItem.Text = "New Root Item";
        //        RadMenu1.Items.Add(RootItem);
        //        break;
        //    case "C":
        //        // add a new child item        
        //        RadMenuItem ChildItem = new RadMenuItem();
        //        RadMenuItem ParentItem = e.Item;
        //        ChildItem.Text = "New Child Item";
        //        ParentItem.Items.Add(ChildItem);
        //        break;
        //}
    }
}