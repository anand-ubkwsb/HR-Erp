
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class frm_Period : System.Web.UI.Page
{
    int DateFrom, AddDays, EditDays, DeleteDays;
    string userid, UserGrpID, FormID, fiscal;
    DmlOperation dml = new DmlOperation();
    radcomboxclass cmb = new radcomboxclass();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
    FieldHide fd = new FieldHide();

    string itemtype, itemhead, itemsubhead;
    float i;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            btnUpdate.Visible = false;
            btnSave.Visible = false;
            btnDelete_after.Visible = false;
            userid = Request.QueryString["UserID"];
            UserGrpID = Request.QueryString["UsergrpID"];
            FormID = Request.QueryString["FormID"];
            fiscal = Request.QueryString["fiscaly"];
            Showall_Dml();


            dml.dropdownsql(ddlMain_Menu, "SET_Menu", "Menu_title", "MenuId");
            dml.dropdownsql(ddlModule, "SET_Module", "ModuleDescription", "ModuleId");

            dml.dropdownsql(ddlEdit_MenuTitle, "SET_Menu", "Menu_title", "MenuId");
            dml.dropdownsql(ddlEdit_Module, "SET_Module", "ModuleDescription", "ModuleId");

            dml.dropdownsql(ddlFind_MenuName, "SET_Menu", "Menu_title", "MenuId");
            dml.dropdownsql(ddlFind_Module, "SET_Module", "ModuleDescription", "ModuleId");

            dml.dropdownsql(ddlDel_MenuName, "SET_Menu", "Menu_title", "MenuId");
            dml.dropdownsql(ddlDel_Module, "SET_Module", "ModuleDescription", "ModuleId");
            

            textClear();
            updatecol.Visible = false;

            Findbox.Visible = false;
            DeleteBox.Visible = false;
            Editbox.Visible = false;
        }
    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        textClear();
        btnSave.Visible = true;
        btnEdit.Visible = false;
        btnFind.Visible = false;
        btnCancel.Visible = true;
        btnDelete.Visible = false;
        btnInsert.Visible = false;
        btnUpdate.Visible = false;
        updatecol.Visible = false;

        txtMenuNAme.Enabled = true;
        txtRemarks.Enabled = true;
        chkActive.Enabled = true;
        rdbHide_N.Enabled = true;
        rdbHide_Y.Enabled = true;
        rdbMainMenu.Enabled = true;
        rdbSubMenu.Enabled = true;
        ddlMain_Menu.Enabled = true;
        ddlModule.Enabled = true;
        txtSortOrder.Enabled = true;
        txtMenuPath.Enabled = true;
        txtCreatedby.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdateBy.Enabled = true;
        txtUpdateDate.Enabled = true;
        rdbMainMenu.Checked = true;
        rdbHide_N.Checked = true;
        
        chkActive.Checked = true;
       

        txtCreatedDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
        rdbMainMenu_CheckedChanged(sender, e);
        rdbSubMenu_CheckedChanged(sender, e);
        userid = Request.QueryString["UserID"];
        DataSet ds_autoUserName = dml.Find("select user_name from SET_User_Manager where UserId ='" + userid + "'");
        txtCreatedby.Text = ds_autoUserName.Tables[0].Rows[0]["user_name"].ToString();

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        try {
            DataSet uniqueg_B_C = dml.Find("select * from SET_Menu where Menu_title = '" + txtMenuNAme.Text + "' and Record_Deleted = '0'");
            if (uniqueg_B_C.Tables[0].Rows.Count > 0)
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "Duplicated entry not allowed";
            }

            else {


                int chk = 0;
                if (chkActive.Checked == true)
                {
                    chk = 1;
                }
                else
                {
                    chk = 0;
                }
                string hide = "", main_sub = "";
                if (rdbHide_N.Checked == true)
                {
                    hide = "N";
                }
                if (rdbHide_Y.Checked == true)
                {
                    hide = "Y";
                }
                if (rdbMainMenu.Checked == true)
                {
                    main_sub = "M";
                }
                if (rdbSubMenu.Checked == true)
                {
                    main_sub = "S";
                }
                string m;
                if (ddlMain_Menu.SelectedIndex == 0)
                {
                    m = "NULL";
                }
                else
                {
                    m =  ddlMain_Menu.SelectedItem.Value ;
                }

                string Query = "";
                if (m.Equals("NULL"))
                {
                    Query = "INSERT INTO SET_Menu ( [Menu_title], [remarks], [IsActive], [Hide], [Menu_SubMenu],  [ModuleId],[SortOrder], [MenuPath], [Record_Deleted], [CreatedBy], [CreateDate]) VALUES ('" + txtMenuNAme.Text + "', '" + txtRemarks.Text + "', '" + chk + "', '" + hide + "', '" + main_sub + "', '" + ddlModule.SelectedItem.Value + "', '" + txtSortOrder.Text + "', '" + txtMenuPath.Text + "','0', '" + Request.QueryString["UserID"] + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "')";
                    dml.Insert(Query, "");

                }
                else {
                    Query = "INSERT INTO SET_Menu ( [Menu_title], [remarks], [IsActive], [Hide], [Menu_SubMenu], [MainMenuId], [ModuleId],[SortOrder], [MenuPath], [Record_Deleted], [CreatedBy], [CreateDate]) VALUES ('" + txtMenuNAme.Text + "', '" + txtRemarks.Text + "', '" + chk + "', '" + hide + "', '" + main_sub + "','" +m+ "', '" + ddlModule.SelectedItem.Value + "', " + txtSortOrder.Text + ", '" + txtMenuPath.Text + "','0', '" + Request.QueryString["UserID"] + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "')";
                    dml.Insert(Query, "");

                }
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);

                Label1.Text = "";
                txtMenuNAme.Text = "";
                txtRemarks.Text = "";
                chkActive.Checked = false;
                rdbHide_N.Checked = false;
                rdbHide_Y.Checked = false;
                rdbMainMenu.Checked = false;
                rdbSubMenu.Checked = false;
                ddlMain_Menu.SelectedIndex = 0;
                ddlModule.SelectedIndex = 0;
                txtSortOrder.Text = "";
                txtMenuPath.Text = "";
                txtCreatedby.Text = "";
                txtCreatedDate.Text = "";
                txtUpdateBy.Text = "";
                txtUpdateDate.Text = "";

                chkActive.Checked = true;
                FormID = Request.QueryString["FormID"];
                Showall_Dml();
            }
        }
        catch(Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try {
            string mainmenuupdate, moduleupdate;
            int chk = 0;
            if (chkActive.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
            string hide = "", main_sub = "";
            if (rdbHide_N.Checked == true)
            {
                hide = "N";
            }
            if (rdbHide_Y.Checked == true)
            {
                hide = "Y";
            }
            if (rdbMainMenu.Checked == true)
            {
                main_sub = "M";
            }
            if (rdbSubMenu.Checked == true)
            {
                main_sub = "S";
            }
            string sortorder = "([ModuleId] IS NULL) AND ([SortOrder] IS NULL)";
            string Module, mainmenu;
            if (txtSortOrder.Text == "")
            {
                sortorder = "([SortOrder] IS NULL)";
            }
            else
            {
                sortorder = "([SortOrder] = '" + txtSortOrder + "')";
            }
            if (ddlModule.SelectedIndex == 0)
            {
                Module = "([ModuleId] IS NULL)";
                moduleupdate = "[ModuleId] = NULL";
            }
            else
            {
                Module = "([ModuleId] = '" + ddlModule.SelectedItem.Value + "')";
                moduleupdate = "[ModuleId] = '" + ddlModule.SelectedItem.Value + "'";
            }
            if (ddlMain_Menu.SelectedIndex == 0)
            {

                mainmenu = "([MainMenuId] IS NULL)";
                mainmenuupdate = "[MainMenuId] = NULL";
            }
            else
            {
                mainmenu = "([MainMenuId] = '" + ddlMain_Menu.SelectedItem.Value + "')";
                mainmenuupdate = "[MainMenuId] = '" + ddlMain_Menu.SelectedItem.Value + "'";
            }
            DataSet ds_up = dml.Find("select * from SET_Menu WHERE ([Sno]='" + ViewState["SNO"].ToString() + "') AND ([Menu_title]='" + txtMenuNAme.Text + "') AND ([remarks]='" + txtRemarks.Text + "') AND ([IsActive]='" + chk + "') AND ([Hide]='" + hide + "') AND ([Menu_SubMenu]='" + main_sub + "') AND " + mainmenu + " AND " + Module + " AND ([MenuPath]='" + txtMenuPath.Text + "')");

            if (ds_up.Tables[0].Rows.Count > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", " noupdate()", true);

                textClear();
                btnInsert.Visible = true;
                btnDelete.Visible = true;
                btnUpdate.Visible = false;
                btnEdit.Visible = true;
                btnFind.Visible = true;
                btnSave.Visible = false;
                btnDelete_after.Visible = false;
                FormID = Request.QueryString["FormID"];
                Showall_Dml();
            }
            else {


                string Query = "UPDATE [SET_Menu] SET [Menu_title]='" + txtMenuNAme.Text + "', [remarks]='" + txtRemarks.Text + "', [IsActive]='" + chk + "', [Hide]='" + hide + "', [Menu_SubMenu]='" + main_sub + "', " + mainmenuupdate + "," + moduleupdate + ", [SortOrder]='" + txtSortOrder.Text + "', [MenuPath]='" + txtMenuPath.Text + "', [UpdatedBy]='" + Request.QueryString["UserID"] + "', [UpdateDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' WHERE ([Sno]='" + ViewState["SNO"].ToString() + "')";
                dml.Update(Query, "");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", " Editalert()", true);
                textClear();
                btnInsert.Visible = true;
                btnDelete.Visible = true;
                btnUpdate.Visible = false;
                btnEdit.Visible = true;
                btnFind.Visible = true;
                btnSave.Visible = false;
                btnDelete_after.Visible = false;
                FormID = Request.QueryString["FormID"];
                Showall_Dml();
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnInsert.Visible = true;
        updatecol.Visible = false;
        btnDelete.Visible = true;
        btnUpdate.Visible = false;
        btnEdit.Visible = true;
        btnFind.Visible = true;
        btnSave.Visible = false;
        btnDelete_after.Visible = false;
        textClear();
        FormID = Request.QueryString["FormID"];
        Showall_Dml();

    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        DeleteBox.Visible = true;
        btnFind.Visible = false;
        btnUpdate.Visible = false;
        btnSave.Visible = false;
        btnDelete.Visible = true;
        btnInsert.Visible = false;
        btnCancel.Visible = true;
        btnEdit.Visible = false;
        btnDelete_after.Visible = false;
        try
        {
            GridView2.DataBind();
            string swhere = "";
            string squer = "select * from SET_Menu";


            if (ddlDel_MenuName.SelectedIndex != 0)
            {
                swhere = "Menu_title = '" + ddlDel_MenuName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "Menu_title is not null";
            }
            if (ddlDel_Module.SelectedIndex != 0)
            {
                swhere = swhere + " and ModuleId = '" + ddlDel_Module.SelectedItem.Value + "'";
            }


            if (rdbDel_M.Checked == true)
            {
                swhere = swhere + " and Menu_SubMenu = 'M'";
            }
            else if (rdbDel_S.Checked == true)
            {
                swhere = swhere + " and Menu_SubMenu = 'S'";
            }
            else if (rdbDel_B.Checked == true)
            {

            }
            if (chkDel_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkDel_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0  ORDER BY Menu_title";

            Findbox.Visible = false;
            fieldbox.Visible = false;
            Editbox.Visible = false;

            DataSet dgrid = dml.grid(squer);
            GridView2.DataSource = dgrid;
            GridView2.DataBind();
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }

    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        btnCancel.Visible = true;
        btnFind.Visible = true;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnInsert.Visible = false;

        try
        {
            //SELECT DepartmentId, BPartnerId,BillNo,IsActive FROM SET_ItemMasterOpening WHERE GocID = '1' AND CompId = '1' AND BranchId='5' AND IsActive = '1' AND Record_Deleted = '0'
            GridView1.DataBind();
            string swhere = "";
            string squer = "select * from SET_Menu";


            if (ddlFind_MenuName.SelectedIndex != 0)
            {
                swhere = "Menu_title = '" + ddlFind_MenuName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "Menu_title is not null";
            }
            if (ddlFind_Module.SelectedIndex != 0)
            {
                swhere = swhere + " and ModuleId = '" + ddlFind_Module.SelectedItem.Value + "'";
            }


            if (rdbFind_M.Checked == true)
            {
                swhere = swhere + " and Menu_SubMenu = 'M'";
            }
            else if (rdbFind_S.Checked == true)
            {
                swhere = swhere + " and Menu_SubMenu = 'S'";
            }
            else if (rdbFind_B.Checked == true)
            {

            }
            if (chkFind_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkFind_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0  ORDER BY Menu_title";

            Findbox.Visible = true;
            fieldbox.Visible = false;

            DataSet dgrid = dml.grid(squer);
            GridView1.DataSource = dgrid;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }


    }
    protected void btnSearchEdit_Click(object sender, EventArgs e)
    {
        Editbox.Visible = true;
        btnFind.Visible = false;
        btnUpdate.Visible = false;
        btnSave.Visible = false;
        btnDelete.Visible = false;
        btnInsert.Visible = false;
        btnCancel.Visible = true;
        btnEdit.Visible = true;
        btnDelete_after.Visible = false;
        try
        {
            
            //SELECT Menu_Title, ModuleId, Menu_SubMenu, IsActive from SET_Menu
            GridView3.DataBind();
            string swhere = "";
            string squer = "select * from SET_Menu";
            

            if (ddlEdit_MenuTitle.SelectedIndex != 0)
            {
                swhere = "Menu_title = '" + ddlEdit_MenuTitle.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "Menu_title is not null";
            }
            if (ddlEdit_Module.SelectedIndex != 0)
            {
                swhere = swhere + " and ModuleId = '"+ ddlEdit_Module.SelectedItem.Value+"'";
            }
           

            if (rdbEditMain.Checked == true)
            {
                swhere = swhere + " and Menu_SubMenu = 'M'";
            }
            else if (rdbEditSub.Checked == true)
            {
                swhere = swhere + " and Menu_SubMenu = 'S'";
            }
            else if (rdbEditAll.Checked == true)
            {
               
            }
            if (chkEdit_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkEdit_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0  ORDER BY Menu_title";

            Findbox.Visible = false;
            fieldbox.Visible = false;
            DeleteBox.Visible = false;

            DataSet dgrid = dml.grid(squer);
            GridView3.DataSource = dgrid;
            GridView3.DataBind();
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    public void textClear()
    {
        DeleteBox.Visible = false;
        Editbox.Visible = false;
        Findbox.Visible = false;
        fieldbox.Visible = true;
       

        txtMenuNAme.Text = "";
        txtRemarks.Text = "";
        chkActive.Checked = false;
        rdbHide_N.Checked = false;
        rdbHide_Y.Checked = false;
        rdbMainMenu.Checked = false;
        rdbSubMenu.Checked = false;
        ddlMain_Menu.SelectedIndex = 0;
        ddlModule.SelectedIndex = 0;
        txtSortOrder.Text="";
        txtMenuPath.Text = "";
        txtCreatedby.Text = "";
        txtCreatedDate.Text = "";
        txtUpdateBy.Text = "";
        txtUpdateDate.Text = "";
        Label1.Text = "";

        txtMenuNAme.Enabled = false;
        txtRemarks.Enabled = false;
        chkActive.Enabled = false;
        rdbHide_N.Enabled = false;
        rdbHide_Y.Enabled = false;
        rdbMainMenu.Enabled = false;
        rdbSubMenu.Enabled = false;
        ddlMain_Menu.Enabled = false;
        ddlModule.Enabled = false;
        txtSortOrder.Enabled = false;
        txtMenuPath.Enabled = false;
        txtCreatedby.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdateBy.Enabled = false;
        txtUpdateDate.Enabled = false;


    }
    public void Showall_Dml()
    {
        userid = Request.QueryString["UserID"];
        FormID = Request.QueryString["FormID"];
        DataSet FiscalStatus;
        con.Open();
        string Query = "SELECT * FROM SET_FISCAL_YEAR WHERE FISCALYEARID=" + FiscalYear();
        DataSet Fiscal = dml.Find(Query);
        string FiscalStatusQuery = "SELECT * FROM SET_FISCAL_YEAR_STATUS WHERE FISCALYEARSTATUSID=(SELECT FISCALYEARSTATUSID FROM SET_FISCAL_YEAR WHERE FISCALYEARID=" + FiscalYear() + ") AND RECORD_DELETED='0'";
        FiscalStatus = dml.Find(FiscalStatusQuery);

        var FiscalId = Request.Cookies["FiscalYearId"].Value;
        SqlDataAdapter da = new SqlDataAdapter("select * from SET_UserGrp_Form where FormId='" + FormID + "' and DmlAllowed = 'Y' AND UserGrpId in (Select UserGrpId from SET_Assign_UserGrp where UserId='" + userid + "' AND Record_Deleted='0') ", con);
        DataSet UserGroupFormDataSet = new DataSet();
        da.Fill(UserGroupFormDataSet);
        int FiscalStatusId = Convert.ToInt32(FiscalStatus.Tables[0].Rows[0]["FiscalYearStatusId"].ToString());
        if (FiscalStatusId == 1)
        {
            var count = UserGroupFormDataSet.Tables[0].Rows.Count;
            bool[] UserGroupBool = new bool[count];
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (UserGroupFormDataSet.Tables[0].Rows[i]["Add"].ToString() == "N")
                    {
                        UserGroupBool[i] = false;
                    }
                    else {
                        UserGroupBool[i] = true;
                    }
                }
                if (UserGroupBool.Contains(true))
                {
                    btnInsert.Visible = true;
                }
                else {
                    btnInsert.Visible = false;
                }



                for (int i = 0; i < count; i++)
                {
                    if (UserGroupFormDataSet.Tables[0].Rows[i]["Edit"].ToString() == "N")
                    {
                        UserGroupBool[i] = false;
                    }
                    else {
                        UserGroupBool[i] = true;
                    }
                }
                if (UserGroupBool.Contains(true))
                {
                    btnEdit.Visible = true;
                }
                else {
                    btnEdit.Visible = false;
                }

                for (int i = 0; i < count; i++)
                {
                    if (UserGroupFormDataSet.Tables[0].Rows[i]["DELETE"].ToString() == "N")
                    {
                        UserGroupBool[i] = false;
                    }
                    else {
                        UserGroupBool[i] = true;
                    }
                }
                if (UserGroupBool.Contains(true))
                {
                    btnDelete.Visible = true;
                }
                else {
                    btnDelete.Visible = false;
                }

            }
            else
            {
                btnInsert.Visible = false;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                btnFind.Visible = true;
                btnCancel.Visible = true;
            }




            for (int i = 0; i < count; i++)
            {
                if (i != 0)
                {
                    if (UserGroupFormDataSet.Tables[0].Rows[i]["DmlAllowed"].ToString() == "Y")
                    {
                        if (Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i]["DateFrom"].ToString()) > Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i - 1]["DateFrom"].ToString()))
                        {
                            DateFrom = Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i]["DateFrom"].ToString());
                        }
                    }
                }
                else {
                    if (UserGroupFormDataSet.Tables[0].Rows[i]["DmlAllowed"].ToString() == "Y")
                    {
                        DateFrom = Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i]["DateFrom"].ToString());
                    }
                }
            }


            for (int i = 0; i < count; i++)
            {
                if (i != 0)
                {
                    if (UserGroupFormDataSet.Tables[0].Rows[i]["Add"].ToString() == "Y")
                    {
                        if (Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i]["AddDays"].ToString()) > Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i - 1]["AddDays"].ToString()))
                        {
                            AddDays = Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i]["AddDays"].ToString());
                        }
                    }
                }
                else {
                    if (UserGroupFormDataSet.Tables[0].Rows[i]["Add"].ToString() == "Y")
                    {
                        AddDays = Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i]["AddDays"].ToString());
                    }
                }
            }


            for (int i = 0; i < count; i++)
            {
                if (i != 0)
                {
                    if (UserGroupFormDataSet.Tables[0].Rows[i]["Edit"].ToString() == "Y")
                    {
                        if (Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i]["EditDays"].ToString()) > Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i - 1]["EditDays"].ToString()))
                        {
                            EditDays = Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i]["EditDays"].ToString());
                        }
                    }
                }
                else {
                    if (UserGroupFormDataSet.Tables[0].Rows[i]["Edit"].ToString() == "Y")
                    {
                        EditDays = Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i]["EditDays"].ToString());
                    }
                }
            }


            for (int i = 0; i < count; i++)
            {
                if (i != 0)
                {
                    if (UserGroupFormDataSet.Tables[0].Rows[i]["Delete"].ToString() == "Y")
                    {
                        if (Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i]["DeleteDays"].ToString()) > Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i - 1]["EditDays"].ToString()))
                        {
                            DeleteDays = Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i]["DeleteDays"].ToString());
                        }
                    }
                }
                else {
                    if (UserGroupFormDataSet.Tables[0].Rows[i]["Delete"].ToString() == "Y")
                    {
                        DeleteDays = Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i]["DeleteDays"].ToString());
                    }
                }
            }
        }
        else if (FiscalStatusId == 2)
        {
            btnInsert.Visible = false;
            btnEdit.Visible = false;
            btnFind.Visible = true;
            btnDelete.Visible = false;
            btnCancel.Visible = false;

        }
        con.Close();

        //con.Open();
        //SqlDataAdapter da = new SqlDataAdapter("select * from SET_UserGrp_Form where FormId='" + FormID + "' and DmlAllowed = 'Y'", con);
        //DataSet ds = new DataSet();
        //da.Fill(ds);
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    if (ds.Tables[0].Rows[0]["Add"].ToString() == "N")
        //    {
        //        btnInsert.Visible = false;
        //    }
        //    if (ds.Tables[0].Rows[0]["Edit"].ToString() == "N")
        //    {
        //        btnEdit.Visible = false;
        //    }
        //    if (ds.Tables[0].Rows[0]["Delete"].ToString() == "N")
        //    {
        //        btnDelete.Visible = false;
        //    }
        //    if (ds.Tables[0].Rows[0]["Find"].ToString() == "N")
        //    {
        //        btnFind.Visible = false;
        //    }
        //}
        //else
        //{
        //    btnInsert.Visible = false;
        //    btnEdit.Visible = false;
        //    btnDelete.Visible = false;
        //    btnFind.Visible = true;
        //    btnCancel.Visible = true;
        //}
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dsformid = dml.Find("select MenuId, Menu_title from SET_Menu where Sno= '" + ViewState["SNO"].ToString() + "' and Record_Deleted='0'");
            if (dsformid.Tables[0].Rows.Count > 0)
            {
                string formid = dsformid.Tables[0].Rows[0]["MenuId"].ToString();
                DataSet ds = dml.Find("select  * from SET_UserGrp_Menu where MenuId = '" + formid + "' and Record_Deleted = '0'");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Label1.Text = "'" + dsformid.Tables[0].Rows[0]["Menu_title"].ToString().ToUpper() + "' Already in used. First delete from User Group Menu";
                }
                else
                {
                    dml.Delete("update SET_Menu set Record_Deleted = 1 where Sno = " + ViewState["SNO"].ToString() + "", "");
                    textClear();
                    ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "alertDelete()", true);
                    btnInsert.Visible = true;
                    btnDelete.Visible = true;
                    btnUpdate.Visible = false;
                    btnEdit.Visible = true;
                    btnFind.Visible = true;
                    btnSave.Visible = false;
                    btnDelete_after.Visible = false;
                    FormID = Request.QueryString["FormID"];
                    Showall_Dml();
                }
            }
            
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        btnUpdate.Visible = false;
        btnInsert.Visible = false;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        Label1.Text = "";

        updatecol.Visible = true;
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {
            string serial_id;
            serial_id = GridView1.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;



            DataSet ds = dml.Find("select * from SET_Menu WHERE ([Sno]='"+serial_id+"') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlMain_Menu.ClearSelection();
                ddlModule.ClearSelection();

                txtMenuNAme.Text = ds.Tables[0].Rows[0]["Menu_title"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["remarks"].ToString();

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                
                string hideYN = ds.Tables[0].Rows[0]["Hide"].ToString();
                
                string MAinSub = ds.Tables[0].Rows[0]["Menu_SubMenu"].ToString();

                if (ds.Tables[0].Rows[0]["MainMenuId"].ToString() == "")
                {
                    ddlMain_Menu.SelectedIndex = 0;
                }
                else {
                    ddlMain_Menu.Items.FindByValue(ds.Tables[0].Rows[0]["MainMenuId"].ToString()).Selected = true;
                }

                if (ds.Tables[0].Rows[0]["ModuleId"].ToString() == "")
                {
                    ddlModule.SelectedIndex = 0;
                }
                else {
                    ddlModule.Items.FindByValue(ds.Tables[0].Rows[0]["ModuleId"].ToString()).Selected = true;
                }
                txtSortOrder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();
                txtMenuPath.Text = ds.Tables[0].Rows[0]["MenuPath"].ToString();
                txtCreatedby.Text = ds.Tables[0].Rows[0]["CreatedBy"].ToString();
                txtUpdateBy.Text = ds.Tables[0].Rows[0]["UpdatedBy"].ToString();

               if(chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                  }
                if (hideYN == "Y")
                {
                    rdbHide_Y.Checked = true;
                    rdbHide_N.Checked = false;
                }
                else
                {
                    rdbHide_N.Checked = true;
                    rdbHide_Y.Checked = false;
                }
                if (MAinSub == "M")
                {
                    rdbMainMenu.Checked = true;
                    rdbSubMenu.Checked = false;
                }
                else
                {
                    rdbMainMenu.Checked = false;
                    rdbSubMenu.Checked = true;
                }
                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() == "")
                {
                    txtCreatedDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                    txtCreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdateDate"].ToString() == "")
                {
                    txtUpdateDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateDate"].ToString());
                    txtUpdateDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                
                updatecol.Visible = true;


            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        btnInsert.Visible = false;
        btnCancel.Visible = true;
        btnUpdate.Visible = false;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnFind.Visible = false;
        btnDelete_after.Visible = true;
        txtUpdateDate.Enabled = false;
        txtUpdateBy.Enabled = false;
        textClear();

        updatecol.Visible = true;
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        DeleteBox.Visible = false;

        try
        {
            string serial_id;
            serial_id = GridView2.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select * from SET_Menu WHERE ([Sno]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlMain_Menu.ClearSelection();
                ddlModule.ClearSelection();
             

                txtMenuNAme.Text = ds.Tables[0].Rows[0]["Menu_title"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["remarks"].ToString();

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());

                string hideYN = ds.Tables[0].Rows[0]["Hide"].ToString();

                string MAinSub = ds.Tables[0].Rows[0]["Menu_SubMenu"].ToString();


                if (ds.Tables[0].Rows[0]["MainMenuId"].ToString() == "")
                {
                    ddlMain_Menu.SelectedIndex = 0;
                }
                else {
                    ddlMain_Menu.Items.FindByValue(ds.Tables[0].Rows[0]["MainMenuId"].ToString()).Selected = true;
                }

                if (ds.Tables[0].Rows[0]["ModuleId"].ToString() == "")
                {
                    ddlModule.SelectedIndex = 0;
                }
                else {
                    ddlModule.Items.FindByValue(ds.Tables[0].Rows[0]["ModuleId"].ToString()).Selected = true;
                }

                txtSortOrder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();
                txtMenuPath.Text = ds.Tables[0].Rows[0]["MenuPath"].ToString();
                  string Id  = ds.Tables[0].Rows[0]["CreatedBy"].ToString();
                DataSet d = dml.Find("Select * From Set_User_Manager where userid='" + Id + "'");
                txtCreatedby.Text = d.Tables[0].Rows[0]["user_name"].ToString();

                //txtCreatedDate.Text = ds.Tables[0].Rows[0]["CreateDate"].ToString();
                //txtUpdateBy.Text = ds.Tables[0].Rows[0]["UpdatedBy"].ToString();
                // txtUpdateDate.Text = ds.Tables[0].Rows[0]["UpdateDate"].ToString();

                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }

                if (MAinSub == "M")
                {
                    rdbMainMenu.Checked = true;
                    rdbSubMenu.Checked = false;
                }
                else
                {
                    rdbMainMenu.Checked = false;
                    rdbSubMenu.Checked = true;
                }
                if (hideYN == "Y")
                {
                    rdbHide_Y.Checked = true;
                    rdbHide_N.Checked = false;
                }
                else
                {
                    rdbHide_N.Checked = true;
                    rdbHide_Y.Checked = false;
                }

                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() == "")
                {
                    txtCreatedDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                    txtCreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdateDate"].ToString() == "")
                {
                    txtUpdateDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateDate"].ToString());
                    txtUpdateDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }

                updatecol.Visible = true;


            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }

    }
    protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
    {
        textClear();
        
        btnInsert.Visible = false;
        btnCancel.Visible = true;
        btnUpdate.Visible = true;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnFind.Visible = false;
        Label1.Text = "";

        txtMenuNAme.Enabled = true;
        txtRemarks.Enabled = true;
        chkActive.Enabled = true;
        rdbHide_N.Enabled = true;
        rdbHide_Y.Enabled = true;
        rdbMainMenu.Enabled = true;
        rdbSubMenu.Enabled = true;
        ddlMain_Menu.Enabled = true;
        ddlModule.Enabled = true;
        txtSortOrder.Enabled = true;
        txtMenuPath.Enabled = true;
        txtCreatedby.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdateBy.Enabled = false;
        txtUpdateDate.Enabled = false;

       
        updatecol.Visible = true;
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {
            string serial_id;
            serial_id = GridView3.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select * from SET_Menu WHERE ([Sno]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlMain_Menu.ClearSelection();
                ddlModule.ClearSelection();
               

                txtMenuNAme.Text = ds.Tables[0].Rows[0]["Menu_title"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["remarks"].ToString();

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());

                string hideYN = ds.Tables[0].Rows[0]["Hide"].ToString();

                string MAinSub = ds.Tables[0].Rows[0]["Menu_SubMenu"].ToString();


                if (ds.Tables[0].Rows[0]["MainMenuId"].ToString() == "")
                {
                    ddlMain_Menu.SelectedIndex = 0;
                }
                else {
                    ddlMain_Menu.Items.FindByValue(ds.Tables[0].Rows[0]["MainMenuId"].ToString()).Selected = true;
                }

                if (ds.Tables[0].Rows[0]["ModuleId"].ToString() == "")
                {
                    ddlModule.SelectedIndex = 0;
                }
                else {
                    ddlModule.Items.FindByValue(ds.Tables[0].Rows[0]["ModuleId"].ToString()).Selected = true;
                }

                txtSortOrder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();
                txtMenuPath.Text = ds.Tables[0].Rows[0]["MenuPath"].ToString();
                txtCreatedby.Text = ds.Tables[0].Rows[0]["CreatedBy"].ToString();
                //txtCreatedDate.Text = ds.Tables[0].Rows[0]["CreateDate"].ToString();
                txtUpdateBy.Text = ds.Tables[0].Rows[0]["UpdatedBy"].ToString();
                // txtUpdateDate.Text = ds.Tables[0].Rows[0]["UpdateDate"].ToString();

                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }

                if (MAinSub == "M")
                {
                    rdbMainMenu.Checked = true;
                    rdbSubMenu.Checked = false;
                }
                else
                {
                    rdbMainMenu.Checked = false;
                    rdbSubMenu.Checked = true;
                }


                if (hideYN == "Y")
                {
                    rdbHide_Y.Checked = true;
                    rdbHide_N.Checked = false;
                }
                else
                {
                    rdbHide_N.Checked = true;
                    rdbHide_Y.Checked = false;
                }
                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() == "")
                {
                    txtCreatedDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                    txtCreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdateDate"].ToString() == "")
                {
                    txtUpdateDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateDate"].ToString());
                    txtUpdateDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }

                updatecol.Visible = true;

                rdbMainMenu_CheckedChanged(sender, e);
                rdbSubMenu_CheckedChanged(sender, e);
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }

    }
    public string show_username()
    {
        userid = Request.QueryString["UserID"];
        DataSet ds_autoUserName = dml.Find("select user_name from SET_User_Manager where UserId='" + userid + "'");
        return ds_autoUserName.Tables[0].Rows[0]["user_name"].ToString();
    }
    public int gocid()
    {
    return Convert.ToInt32(Request.Cookies["GocId"].Value);
    }
    public int compid()
    {
        return Convert.ToInt32(Request.Cookies["CompId"].Value);
    }
    public int branchId()
    {
        return Convert.ToInt32(Request.Cookies["BranchId"].Value);
    }
    public int FiscalYear()
    {
        return Convert.ToInt32(Request.Cookies["FiscalYearId"].Value);
    }






    protected void rdbMainMenu_CheckedChanged(object sender, EventArgs e)
    {
        if(rdbMainMenu.Checked == true)
        {
            ddlMain_Menu.SelectedIndex = 0;
            ddlMain_Menu.Enabled = false;
        }
    }

    protected void rdbSubMenu_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbSubMenu.Checked == true)
        {
            ddlMain_Menu.SelectedIndex = 0;
            ddlMain_Menu.Enabled = true;
        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string id = e.Row.Cells[1].Text;
        bool flag = dml.isNumber(id);


        if (flag == true)
        {

            DataSet ds = dml.Find("select Sno ,MLD from SET_Menu where Sno = '" + id + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string value = ds.Tables[0].Rows[i]["MLD"].ToString();
                    if (dml.Decrypt(value) == "q")
                    {
                        e.Row.Enabled = false;
                        e.Row.Cells[0].ToolTip = "Cannot be deleteable, work in find mode only";
                        e.Row.BackColor = System.Drawing.Color.LightGray;
                    }

                }
            }
        }
    }

    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string id = e.Row.Cells[1].Text;
        bool flag = dml.isNumber(id);


        if (flag == true)
        {

            DataSet ds = dml.Find("select Sno,MLD from SET_Menu where Sno = '" + id + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string value = ds.Tables[0].Rows[i]["MLD"].ToString();
                    if (dml.Decrypt(value) == "q")
                    {
                        e.Row.Enabled = false;
                        e.Row.Cells[0].ToolTip = "Cannot be editable, work in find mode only";
                        e.Row.BackColor = System.Drawing.Color.LightGray;
                    }

                }
            }
        }
    }
}