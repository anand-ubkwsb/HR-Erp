using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Site : System.Web.UI.MasterPage
{
    string Usergrpid, id, st, FiscalId, GocId, CompId, BranchId;
    DmlOperation dml = new DmlOperation();
    string userid;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            id = Request.QueryString.Get("UserID");
            Usergrpid = Request.QueryString["UsergrpID"];
            //Usergrpid = Request.QueryString.Get("UsergrpID");
            st = Request.QueryString.Get("fiscaly");
            FiscalId = Request.QueryString.Get("Fiscal");
            GocId = Request.QueryString.Get("GocId");
            CompId = Request.QueryString.Get("CompId");
            BranchId = Request.QueryString.Get("BranchId");

            //id = Request.Cookies["userid"].Value;
            //Usergrpid = Request.Cookies["UsergrpId"].Value;
            //st = Request.Cookies["fiscalYear"].Value;
            //FiscalId = Request.Cookies["fiscalYear"].Value;
            //GocId = Request.Cookies["GocId"].Value;
            //CompId = Request.Cookies["CompId"].Value;
            //BranchId = Request.Cookies["BranchId"].Value;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("select USER_NAME from SET_User_Manager where UserId='" + id + "'", con);
            DataSet ds = new DataSet();

            da.Fill(ds);
            string username = ds.Tables[0].Rows[0]["USER_NAME"].ToString();
            ViewState["fiyr"] = st;
            Response.Cookies["fiscalYear"].Value = st;
            Response.Cookies["fiscalYear"].Expires = DateTime.Now.AddDays(1);

            //Response.Cookies["FiscalYearId"].Value = FiscalId;
            //Response.Cookies["FiscalYearId"].Expires = DateTime.Now.AddDays(1);

            //Response.Cookies["GocId"].Value = GocId;
            //Response.Cookies["GocId"].Expires = DateTime.Now.AddDays(1);

            //Response.Cookies["CompId"].Value = CompId;
            //Response.Cookies["CompId"].Expires = DateTime.Now.AddDays(1);

            //Response.Cookies["BranchId"].Value = BranchId;
            //Response.Cookies["BranchId"].Expires = DateTime.Now.AddDays(1);

            lbl_USer_fiscal.Text = "Company: &nbsp;&nbsp;&nbsp;" + Request.Cookies["compNAme"].Value + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp Branch: &nbsp;&nbsp;&nbsp;" + Request.Cookies["branchNAme"].Value + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp";
            lbl2.Text = "Fiscal Year :(" + st + " )&nbsp;&nbsp;&nbsp;&nbsp;&nbsp";
            //lbl2.Text = "Fiscal Year :(" + st + " )&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; db Period :&nbsp;&nbsp;&nbsp;" + dbb_description(st);

            lblLoginNameHEAD.Text = " Login Name :  &nbsp;&nbsp;&nbsp;" + loginanaem_userid(id);

            lbl_userName.Text = loginanaem_userid(id);
            string str = Usergrpid;

            GetMenuData();
            GetMenuData_V();
        }
     }
    private void GetMenuData()
    {
        var UserId = Request.Cookies["UserId"].Value;
        DataTable table = new DataTable();
        string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString;
        SqlConnection conn = new SqlConnection(strCon);
        string sql = "SELECT * FROM SET_MENU Where MenuId IN (SELECT MenuId FROM SET_UserGrp_Menu WHERE UserGrpId In (SELECT UserGrpId FROM SET_Assign_UserGrp WHERE UserId='" + UserId + "') AND Record_Deleted='0') And Menu_SubMenu='M' And Hide='N' And Record_Deleted='0'";
        //string sql = "select SET_UserGrp_Menu.MenuId, SET_Menu.Menu_title, SET_Menu.MainMenuId,SET_Menu.MenuPath ,SET_UserGrp_Menu.Hide as userhide , case when SET_UserGrp_Menu.SortOrder is null THEN SET_Menu.SortOrder else SET_UserGrp_Menu.SortOrder end as OrignalOrder from SET_UserGrp_Menu left join SET_Menu on SET_UserGrp_Menu.MenuId = SET_Menu.menuId WHERE UserGrpId in (select UserGrpId from SET_Assign_UserGrp where userID='" + id + "' AND FromDate <= '" + DateTime.Now + "' AND (ToDate is null or ToDate >= '" + DateTime.Now + "')) and  SET_Menu.Hide = 'N'  and SET_UserGrp_Menu.Hide = 'N' AND SET_UserGrp_Menu.IsActive = '1' AND set_menu.IsActive = '1' and SET_UserGrp_Menu.Record_Deleted = '0' ORDER BY 6 ";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(table);
        DataView view = new DataView(table);
        view.RowFilter = "MainMenuId is NULL";
        foreach (DataRowView row in view)
        {

            RadMenuItem menuItem = new RadMenuItem();
            menuItem.Text = row["Menu_title"].ToString().ToUpper();
            menuItem.Value = row["menuId"].ToString();
            //menuItem.NavigateUrl = row["MenuPath"].ToString() + "?UserID=" + id + "&UsergrpID=" + Usergrpid + "&fiscaly=" + st;


            RadMenu1.Items.Add(menuItem);
            AddChildItems(menuItem);
            AddChildItemsform(menuItem);
            // AddChildItemsdoc(menuItem);
        }


    }

    private void AddChildItems(RadMenuItem menuItem)
    {
       
        DataTable table = new DataTable();
        string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString;
        SqlConnection conn = new SqlConnection(strCon);
        string sql = "Select * From SET_Menu Where MenuId In(Select MenuId From SET_UserGrp_Menu Where UserGrpId in (Select UserGrpId From SET_Assign_UserGrp Where UserId= '6B9C1166-0F4B-41DC-99E8-B47BE96C8157')) And  MainMenuId = '"+menuItem.Value+"'  And Menu_SubMenu = 'S' And Record_Deleted='0'";
        //string sqlas = "select SET_UserGrp_Menu.MenuId, SET_Menu.Menu_title, SET_Menu.MainMenuId,SET_Menu.MenuPath ,SET_UserGrp_Menu.Hide as userhide , case when SET_UserGrp_Menu.SortOrder is null THEN SET_Menu.SortOrder else SET_UserGrp_Menu.SortOrder end as OrignalOrder from SET_UserGrp_Menu left join SET_Menu on SET_UserGrp_Menu.MenuId = SET_Menu.menuId WHERE SET_UserGrp_Menu.UserGrpId in (select UserGrpId from SET_Assign_UserGrp where userID='" + id + "' AND FromDate <= '" + DateTime.Now + "' and (ToDate is null or ToDate >= '" + DateTime.Now + "')) and  SET_Menu.Hide = 'N'  and SET_UserGrp_Menu.Hide = 'N' AND SET_UserGrp_Menu.IsActive = '1' AND SET_Menu.IsActive = '1' and SET_UserGrp_Menu.Record_Deleted = '0' ORDER BY 6 ";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(table);


        DataView view = new DataView(table);
        view.RowFilter = "MainMenuId = " + "'" + menuItem.Value + "'";
        foreach (DataRowView childView in view)
        {
            RadMenuItem childItem = new RadMenuItem();
            childItem.Text = childView["Menu_title"].ToString().ToUpper();
            childItem.Value = childView["menuId"].ToString();

            // childItem.NavigateUrl = childView["MenuPath"].ToString() + "?UserID=" + id + "&UsergrpID=" + Usergrpid + "&fiscaly=" + st;
            menuItem.Items.Add(childItem);
            AddChildItems(childItem);
            AddChildItemsform(childItem);
            //AddChildItemsdoc(childItem);
        }



    }
    private void AddChildItemsform(RadMenuItem menuItem)
    {
        st = Request.QueryString.Get("fiscaly");
        string sdate = startdate(st);
        string enddate = Enddate(st);

        DataTable table = new DataTable();
        string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString;
        SqlConnection conn = new SqlConnection(strCon);

        //string sql = " SELECT * FROM set_form WHERE FormId IN (SELECT * FROM SET_UserGrp_Form WHERE UserGrpId IN ( SELECT UserGrpId FROM SET_Assign_UserGrp WHERE UserId = '" + Request.Cookies["userid"].Value + "')) AND MenuId = '" + menuItem.Value + "' AND IsActive = '1' AND Hide = 'N' AND Record_Deleted = '0' ORDER BY SortOrder";
        string sql = "SELECT * FROM set_form WHERE FormId IN(SELECT FormId FROM SET_UserGrp_Form WHERE UserGrpId IN(SELECT UserGrpId FROM SET_Assign_UserGrp WHERE UserId = '" + Request.Cookies["userid"].Value + "')) AND MenuId = '" + menuItem.Value + "' AND IsActive = '1' AND Hide = 'N' AND Record_Deleted = '0' ORDER BY SortOrder";

        //string sql = "Select* From SET_UserGrp_Form where UserGrpId in (Select UserGrpId From SET_Assign_UserGrp where UserId = '" + Request.Cookies["userid"].Value + "') And MenuId = '" + menuItem.Value + "' And Record_Deleted = '0'";
        //string sql = "select SET_UserGrp_Form.FormId, SET_Form.FormTitle, SET_Form.Sno,SET_UserGrp_Form.MenuId,SET_Form.FormPath ,SET_UserGrp_Form.Hide as userhide , case when SET_UserGrp_Form.SortOrder is null THEN SET_Form.SortOrder ELSE SET_UserGrp_Form.SortOrder end as OrginalOrder from SET_UserGrp_Form left join SET_Form on SET_UserGrp_Form.FormId = SET_Form.FormId WHERE SET_UserGrp_Form.UserGrpId in (select UserGrpId from SET_Assign_UserGrp where userID='" + id + "' AND FromDate <= '" + DateTime.Now + "' and (ToDate is null or ToDate >= '" + DateTime.Now + "')) AND SET_UserGrp_Form.FromDate<='" + DateTime.Now.ToString("dd-MMM-yyyy") + "' AND ((SET_UserGrp_Form.ToDate >= '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' OR ToDate is null) OR (FromDate<='" + sdate + "' AND ToDate>='" + enddate + "')) and SET_Form.Hide = 'N' and SET_UserGrp_Form.Hide = 'N' and SET_UserGrp_Form.Record_Deleted = '0' order by 7 ";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(table);
        DataView view = new DataView(table);




        view.RowFilter = "MenuId = " + "'" + menuItem.Value + "'";
        foreach (DataRowView childView in view)
        {
            RadMenuItem childItem = new RadMenuItem();
            childItem.Text = childView["FormTitle"].ToString().ToUpper();
            childItem.Value = childView["FormId"].ToString();

            childItem.NavigateUrl = childView["FormPath"].ToString() + "?UserID=" + id + "&UsergrpID=" + Usergrpid + "&fiscaly=" + st + "&FormID=" + childView["FormId"].ToString() + "&Menuid=" + menuItem.Value + "&Fiscal=" + FiscalId;
            menuItem.Items.Add(childItem);
            AddChildItemsform(childItem);
        }

    }

    //private void AddChildItemsdoc(RadMenuItem menuItem)
    //{
    //    DataTable table = new DataTable();
    //    string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString;
    //    SqlConnection conn = new SqlConnection(strCon);
    //    string sql = "select SET_UserGrp_Documents.Sno,SET_UserGrp_Documents.DocID, SET_Documents.DocDescription,SET_Documents.MenuId_Sno,SET_Documents.doc_Path, SET_UserGrp_Documents.IsHide as userhide, case when SET_UserGrp_Documents.SortOrder is null THEN SET_Documents.SortOrder else SET_UserGrp_Documents.SortOrder end as OrignalOrder from SET_UserGrp_Documents left join SET_Documents on SET_UserGrp_Documents.DocID = SET_Documents.DocID where SET_UserGrp_Documents.UserGrpId='ff43b221-f9e1-4423-aa61-f12880a9e13d' and  SET_Documents.IsHide = '0'  and SET_UserGrp_Documents.IsHide = '0' and SET_UserGrp_Documents.Record_Deleted = '0' ORDER BY 1";
    //    SqlCommand cmd = new SqlCommand(sql, conn);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    da.Fill(table);
    //    DataView view = new DataView(table);


    //    view.RowFilter = "MenuId_Sno = " + "'" + menuItem.Value + "'";
    //    foreach (DataRowView childView in view)
    //    {
    //        RadMenuItem childItem = new RadMenuItem();
    //        childItem.Text = childView["DocDescription"].ToString();
    //        childItem.Value = childView["DocID"].ToString();

    //        childItem.NavigateUrl = childView["doc_Path"].ToString() + "?UserID=" + id + "&UsergrpID=" + Usergrpid + "&fiscaly=" + st + "&FormID=" + childView["DocID"].ToString();
    //        menuItem.Items.Add(childItem);
    //        AddChildItemsdoc(childItem);
    //    }

    //}


    private void GetMenuData_V()
    {
        DataTable table = new DataTable();
        string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString;
        SqlConnection conn = new SqlConnection(strCon);
        string sql = "SELECT * FROM SET_MENU Where MenuId IN (SELECT MenuId FROM SET_UserGrp_Menu WHERE UserGrpId In (SELECT UserGrpId FROM SET_Assign_UserGrp WHERE UserId='" + Request.QueryString["UserID"] + "') AND Record_Deleted='0') And Menu_SubMenu='M' And Hide='N' And Record_Deleted='0'";
        //string sql = "select SET_UserGrp_Menu.MenuId, SET_Menu.Menu_title, SET_Menu.MainMenuId,SET_Menu.MenuPath ,SET_UserGrp_Menu.Hide as userhide , case when SET_UserGrp_Menu.SortOrder is null THEN SET_Menu.SortOrder else SET_UserGrp_Menu.SortOrder end as OrignalOrder from SET_UserGrp_Menu left join SET_Menu on SET_UserGrp_Menu.MenuId = SET_Menu.menuId WHERE UserGrpId in (select UserGrpId from SET_Assign_UserGrp where userID='" + id + "' AND FromDate <= '" + DateTime.Now + "' AND (ToDate is null or ToDate >= '" + DateTime.Now + "')) and  SET_Menu.Hide = 'N'  and SET_UserGrp_Menu.Hide = 'N' AND SET_UserGrp_Menu.IsActive = '1' AND set_menu.IsActive = '1' and SET_UserGrp_Menu.Record_Deleted = '0' ORDER BY 6 ";
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

            RadMenu2.Items.Add(menuItem);
            AddChildItems(menuItem);
            AddChildItemsform(menuItem);
        }

    }

    private void AddChildItems_V(RadMenuItem menuItem)
    {
        DataTable table = new DataTable();
        string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString;
        SqlConnection conn = new SqlConnection(strCon);
        string sql = "select SET_UserGrp_Menu.MenuId, SET_Menu.Menu_title, SET_Menu.MainMenuId,SET_Menu.MenuPath ,SET_UserGrp_Menu.Hide as userhide , case when SET_UserGrp_Menu.SortOrder is null THEN SET_Menu.SortOrder else SET_UserGrp_Menu.SortOrder end as OrignalOrder from SET_UserGrp_Menu left join SET_Menu on SET_UserGrp_Menu.MenuId = SET_Menu.menuId WHERE SET_UserGrp_Menu.UserGrpId in (select UserGrpId from SET_Assign_UserGrp where userID='" + id + "' AND FromDate <= '" + DateTime.Now + "' and (ToDate is null or ToDate >= '" + DateTime.Now + "')) and  SET_Menu.Hide = 'N'  and SET_UserGrp_Menu.Hide = 'N' AND SET_UserGrp_Menu.IsActive = '1' AND SET_Menu.IsActive = '1' and SET_UserGrp_Menu.Record_Deleted = '0' ORDER BY 6 ";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(table);


        DataView view = new DataView(table);
        view.RowFilter = "MainMenuId = " + "'" + menuItem.Value + "'";
        foreach (DataRowView childView in view)
        {
            RadMenuItem childItem = new RadMenuItem();
            childItem.Text = childView["Menu_title"].ToString();
            childItem.Value = childView["menuId"].ToString();

            // childItem.NavigateUrl = childView["MenuPath"].ToString() + "?UserID=" + id + "&UsergrpID=" + Usergrpid + "&fiscaly=" + st;
            menuItem.Items.Add(childItem);
            AddChildItems(childItem);
            AddChildItemsform(childItem);
        }
    }
    private void AddChildItemsform_V(RadMenuItem menuItem)
    {
        st = Request.QueryString.Get("fiscaly");
        string sdate = startdate(st);
        string enddate = Enddate(st);
        DataTable table = new DataTable();
        string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString;
        SqlConnection conn = new SqlConnection(strCon);
        string sql = "select SET_UserGrp_Form.FormId, SET_Form.FormTitle,SET_UserGrp_Form.MenuId,SET_Form.FormPath ,SET_UserGrp_Form.Hide as userhide , case when SET_UserGrp_Form.SortOrder is null THEN SET_Form.SortOrder ELSE SET_UserGrp_Form.SortOrder end as OrginalOrder from SET_UserGrp_Form left join SET_Form on SET_UserGrp_Form.FormId = SET_Form.FormId where SET_UserGrp_Form.UserGrpId = '" + Usergrpid + "' AND SET_UserGrp_Form.FromDate<='" + DateTime.Now.ToString("dd-MMM-yyyy") + "' AND ((SET_UserGrp_Form.ToDate >= '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' OR ToDate is null)  OR (FromDate<='" + sdate + "' AND ToDate>='" + enddate + "')) and  SET_Form.Hide = 'N' and SET_UserGrp_Form.Hide = 'N' and SET_UserGrp_Form.Record_Deleted = '0' order by 6 ";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(table);
        DataView view = new DataView(table);




        view.RowFilter = "MenuId = " + "'" + menuItem.Value + "'";
        foreach (DataRowView childView in view)
        {
            RadMenuItem childItem = new RadMenuItem();
            childItem.Text = childView["FormTitle"].ToString();
            childItem.Value = childView["FormId"].ToString();

            childItem.NavigateUrl = childView["FormPath"].ToString() + "?UserID=" + id + "&UsergrpID=" + Usergrpid + "&fiscaly=" + st + "&FormID=" + childView["FormId"].ToString() + "&Fiscal=" + FiscalId;
            menuItem.Items.Add(childItem);
            AddChildItemsform(childItem);
        }

    }


    protected void login_ServerClick(object sender, EventArgs e)
    {
        Session.Contents.Clear();
        userid = Request.QueryString["UserID"];
        DataSet ds = dml.Find("select * from SET_User_Permission_CompBrYear where gocid = '" + gocid() + "' and CompId = '" + compid() + "' and CompAll = 'Y' and Record_Deleted = '0' and UserId= '" + userid + "'");
        if (ds.Tables[0].Rows.Count > 0)
        {
            dml.Update("update SET_User_Permission_CompBrYear set InUse = Null where GocId='" + gocid() + "' AND CompId='" + compid() + "' and Record_Deleted = '0' and UserId= '" + userid + "'", "");
        }
        else
        {
            dml.Update("update SET_User_Permission_CompBrYear set InUse = Null where GocId='" + gocid() + "' AND CompId='" + compid() + "' AND BranchId='" + branchId() + "' AND FiscalYearsID='" + FiscalYear() + "'", "");
        }
        Response.Redirect("frmlogin.aspx");
    }

    public string dbb_description(string fiscal_year)
    {
        string Query = "SELECT Description from SET_Period where PeriodID = (SELECT PeriodID from SET_Fiscal_Year WHERE FiscalYearId = " + FiscalId + ") and EntryUserId = '"+id+"'";
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
        SqlDataAdapter da = new SqlDataAdapter(Query, conn);
        DataSet ds = new DataSet();
        da.Fill(ds);

        string db_period = ds.Tables[0].Rows[0]["Description"].ToString();
        return db_period;
    }
    public string loginanaem_userid(string Login_userid)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
        SqlDataAdapter da = new SqlDataAdapter("select loginname from SET_User_Manager where userid='" + Login_userid + "'", conn);
        DataSet ds = new DataSet();
        da.Fill(ds);

        string db_period = ds.Tables[0].Rows[0]["loginname"].ToString();
        return db_period;
    }

    public string usergrpname(string usergrpid)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
        SqlDataAdapter da = new SqlDataAdapter("select Description from SET_UserGrp where UserGrpId = '" + usergrpid + "'", conn);
        DataSet ds = new DataSet();
        da.Fill(ds);

        string db_period = ds.Tables[0].Rows[0]["Description"].ToString();
        return db_period;
    }
    protected void chkslide_CheckedChanged(object sender, EventArgs e)
    {
        //Label1.Text = "slide check";

        // chkslide.Attributes.Add("data-toggle", "push-menu");
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        offbutton.Visible = true;
        myDIV.Visible = false;
    }


    protected void RadMenu1_ItemClick(object sender, Telerik.Web.UI.RadMenuEventArgs e)
    {


    }


    public string startdate(string fy)
    {
        string sdate = "";


        DataSet ds = dml.Find("select StartDate,EndDate from SET_Fiscal_Year where Description = '" + fy + "'");
        if (ds.Tables[0].Rows.Count > 0)
        {
            sdate = ds.Tables[0].Rows[0]["StartDate"].ToString();

        }

        return sdate;

    }
    public string Enddate(string fy)
    {

        string Edate = "";


        DataSet ds = dml.Find("select StartDate,EndDate from SET_Fiscal_Year where Description = '" + fy + "'");
        if (ds.Tables[0].Rows.Count > 0)
        {

            Edate = ds.Tables[0].Rows[0]["EndDate"].ToString();
        }

        return Edate;

    }

    public int gocid()
    {
        string gocid = "";
        try
        {
            DataSet ds = dml.Find("select GOCId from SET_Company where CompId=" + Convert.ToInt32(Request.Cookies["CompId"].Value));
            if (ds.Tables[0].Rows.Count > 0)
            {
                string id = gocid = ds.Tables[0].Rows[0]["GOCId"].ToString();
                return int.Parse(id);
            }
            else
            {
                return 0;
            }
        }
        catch (Exception ex)
        {
            ex.GetBaseException();
            return 0;

        }
    }
    public int compid()
    {
        try
        {
            userid = Request.QueryString["UserID"];
            string compid = "";
            //string gocid = "";
            DataSet ds = dml.Find("select compid, GOCId,UseCompanyCOA from SET_Company where CompId='" + Convert.ToInt32(Request.Cookies["CompId"].Value));
            if (ds.Tables[0].Rows.Count > 0)
            {
                compid = ds.Tables[0].Rows[0]["compid"].ToString();
                return int.Parse(compid);
            }
            else
            {
                return 0;
            }
        }
        catch (Exception ex)
        {
            ex.GetBaseException();
            return 0;

        }
    }
    public int branchId()
    {
        try
        {
            userid = Request.QueryString["UserID"];
            string branchid = "";
            DataSet ds = dml.Find("select BranchId from SET_Branch where BranchId=" + Request.Cookies["BranchId"].Value);
            if (ds.Tables[0].Rows.Count > 0)
            {
                branchid = ds.Tables[0].Rows[0]["BranchId"].ToString();
                return int.Parse(branchid);
            }
            else
            {
                return 0;
            }
        }
        catch (Exception ex)
        {
            ex.GetBaseException();
            return 0;

        }
    }
    public int FiscalYear()
    {
        try
        {
            userid = Request.QueryString["UserID"];
            string fy = "";
            DataSet ds = dml.Find("select FiscalYearID from SET_Fiscal_Year where FiscalYearId = " + Convert.ToInt32(Request.Cookies["FiscalYearId"].Value));
            if (ds.Tables[0].Rows.Count > 0)
            {
                fy = ds.Tables[0].Rows[0]["FiscalYearID"].ToString();
                return int.Parse(fy);
            }
            else
            {
                return 0;
            }
        }
        catch (Exception ex)
        {
            ex.GetBaseException();
            return 0;

        }
    }

}
