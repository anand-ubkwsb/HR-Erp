using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frm_SET_COA_Master : System.Web.UI.Page
{
    int DateFrom, AddDays, EditDays, DeleteDays;
    public string compidinsert;
    public string gocidinsert;
    string userid, UserGrpID, FormID;
    DmlOperation dml = new DmlOperation();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
    FieldHide fd = new FieldHide();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {

            Findbox.Visible = false;
            btnUpdate.Visible = false;
            btnSave.Visible = false;
            userid = Request.QueryString["UserID"];
            UserGrpID = Request.QueryString["UsergrpID"];
            FormID = Request.QueryString["FormID"];

            Deletebox.Visible = false;
            Editbox.Visible = false;
            btnDelete_after.Visible = false;
            dml.daterangeforfiscal(CalendarExtender1, Request.Cookies["fiscalyear"].Value);
            txtEntryDate.Attributes.Add("readonly", "readonly");
            CalendarExtender1.EndDate = DateTime.Now;
            Showall_Dml();

            dml.dropdownsql(lblUserGrpName, "SET_UserGrp", "UserGrpName", "UserGrpId", "Record_Deleted", "0");
            dml.dropdownsql(lblmenu, "View_UserGrpMenu", "Menu_title", "MenuId", "UserGrpName", lblUserGrpName.SelectedItem.Text);
            dml.dropdownsql(lblReport, "SET_Report", "Report_Title", "ReportId", "Record_Deleted", "0");


            dml.dropdownsql(txtEdit_UserGrpname, "SET_UserGrp", "UserGrpName", "UserGrpId", "Record_Deleted", "0");
            dml.dropdownsql(txtEdit_menutitle, "View_UserGrpMenu", "Menu_title", "MenuId");
            dml.dropdownsql(txtEdit_Report, "SET_Report", "Report_Title", "ReportId", "Record_Deleted", "0");

            dml.dropdownsql(txtFind_UserGrpName, "SET_UserGrp", "UserGrpName", "UserGrpId", "Record_Deleted", "0");
            dml.dropdownsql(txtFind_MenuTitle, "View_UserGrpMenu", "Menu_title", "MenuId");
            dml.dropdownsql(txtFind_ReportTitle, "SET_Report", "Report_Title", "ReportId", "Record_Deleted", "0");

            dml.dropdownsql(txtDel_UserGrpName, "SET_UserGrp", "UserGrpName", "UserGrpId", "Record_Deleted", "0");
            dml.dropdownsql(txtDel_MenuTitle, "View_UserGrpMenu", "Menu_title", "MenuId");
            dml.dropdownsql(txtDel_reportName, "SET_Report", "Report_Title", "ReportId", "Record_Deleted", "0");

            datamenu_view();
            selectcheck();
            textClear();


        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string Print = "N";
            string view = "N";

            if (chk_R_print.Checked == true)
            {
                Print = "Y";
            }
            else
            {
                Print = "N";
            }
            if (chk_R_View.Checked == true)
            {
                view = "Y";
            }
            else
            {
                view = "N";
            }
            userid = Request.QueryString["UserID"];
            int chk = 0;
            if (chkActive_Status.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
            string hide = "";
            if (rdb_Hide_Y.Checked == true)
            {
                hide = "Y";
            }
            if (rdb_Hide_N.Checked == true)
            {
                hide = "N";
            }
            //DataSet ds_usergrpname = dml.Find("select UserGrpId from SET_UserGrp where Sno= "+lblUserGrpName.SelectedItem.Value+"");
            //string usergrpid = ds_usergrpname.Tables[0].Rows[0]["UserGrpId"].ToString();
            DataSet ds_usermenuid = dml.Find("select MenuId from SET_Menu where Sno = " + lblmenu.SelectedItem.Value + "");
            string menuid = ds_usermenuid.Tables[0].Rows[0]["MenuId"].ToString();
            DataSet ds_userReportid = dml.Find("select ReportId from SET_Report where Sno = " + lblReport.SelectedItem.Value + "");
            string reportid = ds_userReportid.Tables[0].Rows[0]["ReportId"].ToString();
            //DataSet ds_FEndDate = dml.Find("");
            dml.Insert("INSERT INTO SET_UserGrp_Report ([UserGrpId], [EntryDate], [MenuId], [ReportId], [View], [Print], [IsActive], [Hide], [SortOrder], [SysDate], [EnterUserId],[BranchId],[CompId]) VALUES ('" + lblUserGrpName.SelectedItem.Value + "', '" + txtEntryDate.Text + "', '" + menuid + "', '" + lblReport.SelectedItem.Value + "', '" + view + "', '" + Print + "', '" + chk + "', '" + hide + "', '" + txtSortOrder.Text + "', '" + DateTime.Now + "', '" + userid + "'," + branchId() + "," + compid() + ");", "");
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "alertme()", true);

            textClear();
            btnInsert.Visible = true;
            btnDelete.Visible = true;
            btnUpdate.Visible = false;
            btnEdit.Visible = true;
            btnFind.Visible = true;
            btnSave.Visible = false;
            btnDelete_after.Visible = false;
            btnInsert_Click(sender, e);

        }
        catch (Exception ex)
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = ex.Message;
        }
        UserGrpID = Request.QueryString["UsergrpID"];
        FormID = Request.QueryString["FormID"];
        Showall_Dml();
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        textClear();
        Div1.Visible = true;
        btnSave.Visible = true;
        btnEdit.Visible = false;
        btnFind.Visible = false;
        btnCancel.Visible = true;
        btnDelete.Visible = false;
        btnInsert.Visible = false;
        btnUpdate.Visible = false;

        chkActive_Status.Checked = true;
        txtSortOrder.Enabled = true;
        rdb_Hide_N.Enabled = true;
        rdb_Hide_Y.Enabled = true;
        lblmenu.Enabled = true;
        lblUserGrpName.Enabled = true;
        lblReport.Enabled = true;
        lblSystemDate.Enabled = true;
        lblEntryUSer_Name.Enabled = true;
        txtEntryDate.Enabled = true;
        chkActive_Status.Enabled = true;
        rdb_Hide_N.Checked = true;
        chk_R_print.Checked = true;
        chk_R_View.Checked = true;
        chk_R_print.Enabled = true;
        chk_R_View.Enabled = true;
        imgPopup.Enabled = true;
        lblSystemDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff ");
        lblEntryUSer_Name.Text = show_username();

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string Print = "N";
            string view = "N";

            if (chk_R_print.Checked == true)
            {
                Print = "Y";
            }
            else
            {
                Print = "N";
            }
            if (chk_R_View.Checked == true)
            {
                view = "Y";
            }
            else
            {
                view = "N";
            }
            userid = Request.QueryString["UserID"];
            int chk = 0;
            if (chkActive_Status.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
            string hide = "";
            if (rdb_Hide_Y.Checked == true)
            {
                hide = "Y";
            }
            if (rdb_Hide_N.Checked == true)
            {
                hide = "N";
            }
            //DataSet ds_usergrpname = dml.Find("select UserGrpId from SET_UserGrp where Sno= " + lblUserGrpName.SelectedItem.Value + "");
            //string usergrpid = ds_usergrpname.Tables[0].Rows[0]["UserGrpId"].ToString();
            //DataSet ds_usermenuid = dml.Find("select MenuId from SET_Menu where Sno = " + lblmenu.SelectedItem.Value + "");
            //string menuid = ds_usermenuid.Tables[0].Rows[0]["MenuId"].ToString();
            //DataSet ds_userReportid = dml.Find("select ReportId from SET_Report where Sno = " + lblReport.SelectedItem.Value + "");
            //string reportid = ds_userReportid.Tables[0].Rows[0]["ReportId"].ToString();

            string en_d = dml.dateconvertforinsert(txtEntryDate);
            DataSet ds_up = dml.Find("select * from SET_UserGrp_Report WHERE ([Sno]='" + ViewState["sno"].ToString() + "') AND ([UserGrpId]='" + lblUserGrpName.SelectedItem.Value + "') AND ([EntryDate]='" + en_d + "') AND ([MenuId]='" + lblmenu.SelectedItem.Value + "') AND ([ReportId]='" + lblReport.SelectedItem.Value + "') AND ([View]='" + view + "') AND ([Print]='" + Print + "') AND ([IsActive]='" + chk + "') AND ([Hide]='" + hide + "') AND ([SortOrder]='" + txtSortOrder.Text + "')");

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
            }
            else {


                dml.Update("UPDATE SET_UserGrp_Report SET [UserGrpId]='" + lblUserGrpName.SelectedItem.Value + "', [EntryDate]='" + en_d + "', [MenuId]='" + lblmenu.SelectedItem.Value + "', [ReportId]='" + lblReport.SelectedItem.Value + "', [View]='" + view + "', [Print]='" + Print + "', [IsActive]='" + chk + "', [Hide]='" + hide + "', [SortOrder]='" + txtSortOrder.Text + "', [SysDate]='" + DateTime.Now + "', [EnterUserId]='" + userid + "' where Sno = " + ViewState["sno"].ToString() + " ", "");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "Editalert()", true);

                textClear();
                btnInsert.Visible = true;
                btnDelete.Visible = true;
                btnUpdate.Visible = false;
                btnEdit.Visible = true;
                btnFind.Visible = true;
                btnSave.Visible = false;
                btnDelete_after.Visible = false;

            }
        }
        catch (Exception ex)
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = ex.Message;
        }
        UserGrpID = Request.QueryString["UsergrpID"];
        FormID = Request.QueryString["FormID"];
        Showall_Dml();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            dml.Delete("update SET_UserGrp_Report set Record_Deleted = 1 where Sno =  " + ViewState["sno"].ToString() + "", "");
            textClear();
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "alertDelete()", true);


            btnInsert.Visible = true;
            btnDelete.Visible = true;
            btnUpdate.Visible = false;
            btnEdit.Visible = true;
            btnFind.Visible = true;
            btnSave.Visible = false;
            btnDelete_after.Visible = false;

        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
        UserGrpID = Request.QueryString["UsergrpID"];
        FormID = Request.QueryString["FormID"];
        Showall_Dml();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        dml.dropdownsql(lblmenu, "SET_Menu", "Menu_title", "Sno");
        dml.dropdownsql(lblUserGrpName, "SET_UserGrp", "UserGrpName", "UserGrpId");
        dml.dropdownsql(lblReport, "SET_Report", "Report_Title", "Sno");
        textClear();
        btnInsert.Visible = true;
        btnDelete.Visible = true;
        btnUpdate.Visible = false;
        btnEdit.Visible = true;
        btnFind.Visible = true;
        btnSave.Visible = false;

        btnDelete_after.Visible = false;

        Deletebox.Visible = false;
        Editbox.Visible = false;
        Findbox.Visible = false;
        fieldbox.Visible = true;



        UserGrpID = Request.QueryString["UsergrpID"];
        FormID = Request.QueryString["FormID"];
        Showall_Dml();
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

            string swhere;
            string squer = "select * from View_UserGrpReport ";

            if (txtEdit_Report.SelectedIndex != 0)
            {
                swhere = "Report_Title = '" + txtEdit_Report.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "Report_Title is not null";
            }
            if (txtEdit_menutitle.SelectedIndex != 0)
            {
                swhere = swhere + " and Menu_title = '" + txtEdit_menutitle.SelectedItem.Text + "'";
            }
            //else
            //{
            //    swhere = swhere + " and Menu_title is not null";
            //}
            if (txtEdit_UserGrpname.SelectedIndex != 0)
            {
                swhere = swhere + " and UserGrpName = '" + txtEdit_UserGrpname.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and UserGrpName is not null";
            }

            if (ChkEdit_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (ChkEdit_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }
            else
            {
                swhere = swhere + " and IsActive is not null";
            }


            string fiscal = Request.QueryString["fiscaly"];
            string stdate = dml.daterangefiscal_start(fiscal);
            string enddate = dml.daterangefiscal_end(fiscal);
            squer = squer + " where " + swhere + " and Record_Deleted = 0  and (EntryDate > '" + stdate + "' and EntryDate < '" + enddate + "')  and CompId='" + compid() + "' and branchId='" + branchId() + "' ORDER BY Report_Title";

            Findbox.Visible = false;
            Deletebox.Visible = false;
            Editbox.Visible = true;
            fieldbox.Visible = false;


            DataSet dgrid = dml.grid(squer);
            GridView3.DataSource = dgrid;
            GridView3.DataBind();
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }

    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        try
        {
            btnCancel.Visible = true;
            btnFind.Visible = true;
            btnDelete.Visible = false;
            btnEdit.Visible = false;
            btnInsert.Visible = false;

            string swhere;
            UserGrpID = Request.QueryString["UsergrpID"];
            //SELECT Report_Title,UserGrpName,Menu_title,IsActive from View_UserGrpReport
            //string squer = "select * from View_UserGrpReport where Record_Deleted = 0";


            string squer = "select * from View_UserGrpReport ";

            if (txtFind_ReportTitle.SelectedIndex != 0)
            {
                swhere = "Report_Title = '" + txtFind_ReportTitle.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "Report_Title is not null";
            }
            if (txtFind_MenuTitle.SelectedIndex != 0)
            {
                swhere = swhere + " and Menu_title = '" + txtFind_MenuTitle.SelectedItem.Text + "'";
            }
            //else
            //{
            //    swhere = swhere + " and Menu_title is not null";
            //}
            if (txtFind_UserGrpName.SelectedIndex != 0)
            {
                swhere = swhere + " and UserGrpName = '" + txtFind_UserGrpName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and UserGrpName is not null";
            }

            if (chkFind_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkFind_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }
            else
            {
                swhere = swhere + " and IsActive is not null";
            }
            string fiscal = Request.QueryString["fiscaly"];
            string stdate = dml.daterangefiscal_start(fiscal);
            string enddate = dml.daterangefiscal_end(fiscal);
            squer = squer + " where " + swhere + " and Record_Deleted = 0  and (EntryDate > '" + stdate + "' and EntryDate < '" + enddate + "')  and CompId='" + compid() + "' and branchId='" + branchId() + "' ORDER BY Report_Title";


            Findbox.Visible = true;
            Deletebox.Visible = false;
            Editbox.Visible = false;
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

    protected void btn_delete_Click(object sender, EventArgs e)
    {
        Deletebox.Visible = true;
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
            string swhere;
            UserGrpID = Request.QueryString["UsergrpID"];
            //SELECT Report_Title,UserGrpName,Menu_title,IsActive from View_UserGrpReport
            //string squer = "select * from View_UserGrpReport where Record_Deleted = 0";


            string squer = "select * from View_UserGrpReport ";

            if (txtDel_reportName.SelectedIndex != 0)
            {
                swhere = "Report_Title = '" + txtDel_reportName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "Report_Title is not null";
            }
            if (txtDel_MenuTitle.SelectedIndex != 0)
            {
                swhere = swhere + " and Menu_title = '" + txtDel_MenuTitle.SelectedItem.Text + "'";
            }
            //else
            //{
            //    swhere = swhere + " and Menu_title is not null";
            //}
            if (txtDel_UserGrpName.SelectedIndex != 0)
            {
                swhere = swhere + " and UserGrpName = '" + txtDel_UserGrpName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and UserGrpName is not null";
            }

            if (chkDel_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkDel_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }
            else
            {
                swhere = swhere + " and IsActive is not null";
            }
            string fiscal = Request.QueryString["fiscaly"];
            string stdate = dml.daterangefiscal_start(fiscal);
            string enddate = dml.daterangefiscal_end(fiscal);
            squer = squer + " where " + swhere + " and Record_Deleted = 0  and (EntryDate > '" + stdate + "' and EntryDate < '" + enddate + "')  and CompId='" + compid() + "' and branchId='" + branchId() + "' ORDER BY Report_Title";


            Findbox.Visible = false;
            fieldbox.Visible = false;
            Editbox.Visible = false;
            Deletebox.Visible = true;

            DataSet dgrid = dml.grid(squer);
            GridView2.DataSource = dgrid;
            GridView2.DataBind();
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }

    }

    public void textClear()
    {
        Div1.Visible = false;
        lblUserGrpName.SelectedIndex = 0;
        lblReport.SelectedIndex = 0;
        lblmenu.SelectedIndex = 0;
        chkActive_Status.Checked = false;
        rdb_Hide_Y.Checked = false;
        rdb_Hide_N.Checked = false;
        Label1.Text = "";
        txtSortOrder.Text = "";
        lblSystemDate.Text = "";
        lblEntryUSer_Name.Text = "";
        txtEntryDate.Text = "";

        chkActive_Status.Checked = false;
        txtSortOrder.Enabled = false;
        rdb_Hide_N.Enabled = false;
        rdb_Hide_Y.Enabled = false;
        lblmenu.Enabled = false;
        lblUserGrpName.Enabled = false;
        lblReport.Enabled = false;
        lblSystemDate.Enabled = false;
        lblEntryUSer_Name.Enabled = false;
        chkActive_Status.Enabled = false;
        txtEntryDate.Enabled = false;
        imgPopup.Enabled = false;

        chk_R_View.Enabled = false;
        chk_R_print.Enabled = false;
        chk_R_View.Checked = false;
        chk_R_print.Checked = false;


    }
    public void Showall_Dml()
    {
        userid = Request.QueryString["UserID"];
        FormID = Request.QueryString["FormID"];
        DataSet FiscalStatus;
        con.Open();
        string Query = "SELECT * FROM SET_FISCAL_YEAR WHERE FISCALYEARID=" + Convert.ToInt32(Request.Cookies["FiscalYearId"].Value);
        DataSet Fiscal = dml.Find(Query);
        string FiscalStatusQuery = "SELECT * FROM SET_FISCAL_YEAR_STATUS WHERE FISCALYEARSTATUSID=(SELECT FISCALYEARSTATUSID FROM SET_FISCAL_YEAR WHERE FISCALYEARID=" + Convert.ToInt32(Request.Cookies["FiscalYearId"].Value)+ ") AND RECORD_DELETED='0'";
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


        //DataSet ds = dml.Find("select * from SET_UserGrp_Form where FormId='" + FormID + "' and DmlAllowed = 'Y' and UserGrpId = '" + UserGrpID + "' and Record_Deleted = '0'");
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
        //    btnCancel.Visible = false;
        //}
    }

    public string show_username()
    {
        userid = Request.QueryString["UserID"];
        return userid;
    }


    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

        btnInsert.Visible = false;
        btnCancel.Visible = true;
        btnUpdate.Visible = false;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnFind.Visible = true;
        textClear();


        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {

            string serialno = GridView1.SelectedRow.Cells[1].Text;
            ViewState["sno"] = serialno;

            DataSet ds = dml.Find("Select * from SET_UserGrp_Report where Sno = " + serialno);
            //
            if (ds.Tables[0].Rows.Count > 0)

            {
                DataSet dds = dml.Find("select * from View_UserGrpReport where Sno = " + serialno);


                lblUserGrpName.ClearSelection();

                lblReport.ClearSelection();

                lblUserGrpName.Items.FindByText(dds.Tables[0].Rows[0]["UserGrpName"].ToString()).Selected = true;
                ddl_menu();
                lblmenu.ClearSelection();
                if (dds.Tables[0].Rows[0]["Menu_title"].ToString() != "")
                {
                    lblmenu.Items.FindByText(dds.Tables[0].Rows[0]["Menu_title"].ToString()).Selected = true;
                }
                else
                {
                    lblmenu.SelectedIndex = 0;
                }
                lblReport.Items.FindByText(dds.Tables[0].Rows[0]["Report_Title"].ToString()).Selected = true;



                // lblUserGrpName.SelectedItem.Text= dds.Tables[0].Rows[0]["UserGrpName"].ToString();
                //  lblmenu.SelectedItem.Text = dds.Tables[0].Rows[0]["Menu_title"].ToString();
                // lblReport.SelectedItem.Text = dds.Tables[0].Rows[0]["Report_Title"].ToString();
                DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["SysDate"].ToString());
                txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                dml.dateConvert(txtEntryDate);
                string print = ds.Tables[0].Rows[0]["Print"].ToString();
                string view = ds.Tables[0].Rows[0]["View"].ToString();

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                string hide = ds.Tables[0].Rows[0]["Hide"].ToString();

                lblSystemDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

                if (chk == true)
                {
                    chkActive_Status.Checked = true;
                }
                else
                {
                    chkActive_Status.Checked = false;
                }
                if (hide == "N")
                {
                    rdb_Hide_N.Checked = true;
                }
                else
                {
                    rdb_Hide_Y.Checked = true;
                }

                if (view == "Y")
                {
                    chk_R_View.Checked = true;
                }
                else
                {
                    chk_R_View.Checked = false;
                }
                if (print == "Y")
                {
                    chk_R_print.Checked = true;
                }
                else
                {
                    chk_R_print.Checked = false;
                }


                txtSortOrder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();
                string entruserid = ds.Tables[0].Rows[0]["EnterUserId"].ToString();
                DataSet ddss = dml.Find("Select user_name from set_user_manager where userid= '" + entruserid + "'");
                lblEntryUSer_Name.Text = ddss.Tables[0].Rows[0]["user_name"].ToString();


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
        textClear();

        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        Deletebox.Visible = false;
        try
        {

            string serialno = GridView2.SelectedRow.Cells[1].Text;
            ViewState["sno"] = serialno;

            DataSet ds = dml.Find("Select * from SET_UserGrp_Report where Sno = " + serialno);
            //
            if (ds.Tables[0].Rows.Count > 0)

            {
                DataSet dds = dml.Find("select * from View_UserGrpReport where Sno = " + serialno);
                lblUserGrpName.ClearSelection();

                lblReport.ClearSelection();

                lblUserGrpName.Items.FindByText(dds.Tables[0].Rows[0]["UserGrpName"].ToString()).Selected = true;
                ddl_menu();
                lblmenu.ClearSelection();
                if (dds.Tables[0].Rows[0]["Menu_title"].ToString() != "")
                {
                    lblmenu.Items.FindByText(dds.Tables[0].Rows[0]["Menu_title"].ToString()).Selected = true;
                }
                else
                {
                    lblmenu.SelectedIndex = 0;
                }
                lblReport.Items.FindByText(dds.Tables[0].Rows[0]["Report_Title"].ToString()).Selected = true;

                // lblUserGrpName.SelectedItem.Text = dds.Tables[0].Rows[0]["UserGrpName"].ToString();
                //   lblmenu.SelectedItem.Text = dds.Tables[0].Rows[0]["Menu_title"].ToString();
                //  lblReport.SelectedItem.Text = dds.Tables[0].Rows[0]["Report_Title"].ToString();
                DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["SysDate"].ToString());
                txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                dml.dateConvert(txtEntryDate);
                string print = ds.Tables[0].Rows[0]["Print"].ToString();
                string view = ds.Tables[0].Rows[0]["View"].ToString();

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                string hide = ds.Tables[0].Rows[0]["Hide"].ToString();

                lblSystemDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

                if (chk == true)
                {
                    chkActive_Status.Checked = true;
                }
                else
                {
                    chkActive_Status.Checked = false;
                }
                if (hide == "N")
                {
                    rdb_Hide_N.Checked = true;
                }
                else
                {
                    rdb_Hide_Y.Checked = true;
                }

                if (view == "Y")
                {
                    chk_R_View.Checked = true;
                }
                else
                {
                    chk_R_View.Checked = false;
                }
                if (print == "Y")
                {
                    chk_R_print.Checked = true;
                }
                else
                {
                    chk_R_print.Checked = false;
                }


                txtSortOrder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();
                string entruserid = ds.Tables[0].Rows[0]["EnterUserId"].ToString();
                DataSet ddss = dml.Find("Select user_name from set_user_manager where userid= '" + entruserid + "'");
                lblEntryUSer_Name.Text = ddss.Tables[0].Rows[0]["user_name"].ToString();


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

        chkActive_Status.Checked = true;
        txtSortOrder.Enabled = true;
        rdb_Hide_N.Enabled = true;
        rdb_Hide_Y.Enabled = true;
        lblmenu.Enabled = true;
        lblUserGrpName.Enabled = true;
        lblReport.Enabled = true;
        lblSystemDate.Enabled = true;
        lblEntryUSer_Name.Enabled = true;
        txtEntryDate.Enabled = true;
        chk_R_print.Enabled = true;
        chk_R_View.Enabled = true;
        chkActive_Status.Enabled = true;
        lblSystemDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff ");
        lblEntryUSer_Name.Text = show_username();
        imgPopup.Enabled = true;

        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;

        try
        {

            string serialno = GridView3.SelectedRow.Cells[1].Text;

            ViewState["sno"] = serialno;

            DataSet ds = dml.Find("Select * from SET_UserGrp_Report where Sno = " + serialno);
            //
            if (ds.Tables[0].Rows.Count > 0)

            {
                DataSet dds = dml.Find("select * from View_UserGrpReport where Sno = " + serialno);
                lblUserGrpName.ClearSelection();

                lblReport.ClearSelection();

                lblUserGrpName.Items.FindByText(dds.Tables[0].Rows[0]["UserGrpName"].ToString()).Selected = true;
                ddl_menu();
                lblmenu.ClearSelection();
                if (dds.Tables[0].Rows[0]["Menu_title"].ToString() != "")
                {
                    lblmenu.Items.FindByText(dds.Tables[0].Rows[0]["Menu_title"].ToString()).Selected = true;
                }
                else
                {
                    lblmenu.SelectedIndex = 0;
                }

                lblReport.Items.FindByText(dds.Tables[0].Rows[0]["Report_Title"].ToString()).Selected = true;

                // lblUserGrpName.SelectedItem.Text = dds.Tables[0].Rows[0]["UserGrpName"].ToString();
                //  lblmenu.SelectedItem.Text = dds.Tables[0].Rows[0]["Menu_title"].ToString();
                //  lblReport.SelectedItem.Text = dds.Tables[0].Rows[0]["Report_Title"].ToString();
                DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["SysDate"].ToString());
                txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                dml.dateConvert(txtEntryDate);
                string print = ds.Tables[0].Rows[0]["Print"].ToString();
                string view = ds.Tables[0].Rows[0]["View"].ToString();

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                string hide = ds.Tables[0].Rows[0]["Hide"].ToString();

                lblSystemDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

                if (chk == true)
                {
                    chkActive_Status.Checked = true;
                }
                else
                {
                    chkActive_Status.Checked = false;
                }
                if (hide == "N")
                {
                    rdb_Hide_N.Checked = true;
                }
                else
                {
                    rdb_Hide_Y.Checked = true;
                }

                if (view == "Y")
                {
                    chk_R_View.Checked = true;
                }
                else
                {
                    chk_R_View.Checked = false;
                }
                if (print == "Y")
                {
                    chk_R_print.Checked = true;
                }
                else
                {
                    chk_R_print.Checked = false;
                }


                txtSortOrder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();
                string entruserid = ds.Tables[0].Rows[0]["EnterUserId"].ToString();
                DataSet ddss = dml.Find("Select user_name from set_user_manager where userid= '" + entruserid + "'");
                lblEntryUSer_Name.Text = ddss.Tables[0].Rows[0]["user_name"].ToString();


            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

    public int gocid()
    {
        return Convert.ToInt32(Request.Cookies["GocId"].Value);
    }

    public int compid()
    {
        string CompId = Request.Cookies["CompId"].Value;
        return Convert.ToInt32(CompId);
    }


    protected void lblUserGrpName_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmenu.ClearSelection();
        dml.dropdownsql(lblmenu, "View_UserGrpMenu", "Menu_title", "Sno", "UserGrpName", lblUserGrpName.SelectedItem.Text);

        Label1.Text = "";
        datamenu_view();
        selectcheck();
    }
    public void ddl_menu()
    {

        dml.dropdownsql(lblmenu, "View_UserGrpMenu", "Menu_title", "MenuId", "UserGrpName", lblUserGrpName.SelectedItem.Text);
    }


    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtdate = ((TextBox)e.Row.FindControl("txtAppleDate123"));
            txtdate.Attributes.Add("readonly", "readonly");
            e.Row.Attributes.Add("ondblclick", "__doPostBack('GridView4','Select$" + e.Row.RowIndex + "');");
        }
    }

    protected void GridView4_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSave.Visible = false;
        btnInsert.Visible = false;
        btnCancel.Visible = true;
        btnUpdate.Visible = true;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnFind.Visible = false;
        Label1.Text = "";
        chkActive_Status.Checked = true;
        txtSortOrder.Enabled = true;
        rdb_Hide_N.Enabled = true;
        rdb_Hide_Y.Enabled = true;
        lblmenu.Enabled = true;
        lblUserGrpName.Enabled = true;
        lblReport.Enabled = true;
        lblSystemDate.Enabled = true;
        lblEntryUSer_Name.Enabled = true;
        txtEntryDate.Enabled = true;
        chkActive_Status.Enabled = true;
        rdb_Hide_N.Checked = true;
        chk_R_print.Checked = true;
        chk_R_View.Checked = true;
        chk_R_print.Enabled = true;
        chk_R_View.Enabled = true;
        imgPopup.Enabled = true;

        try
        {

            dml.dropdownsql(lblUserGrpName, "SET_UserGrp", "UserGrpName", "UserGrpId", "Record_Deleted", "0");
            dml.dropdownsql(lblmenu, "View_UserGrpMenu", "Menu_title", "MenuId", "UserGrpName", lblUserGrpName.SelectedItem.Text);
            dml.dropdownsql(lblReport, "SET_Report", "Report_Title", "ReportId", "Record_Deleted", "0");

            Label serial_id = (Label)GridView4.SelectedRow.FindControl("lblNatureID");
            //  serial_id = GridView4.SelectedRow.Cells[2].Text;
            ViewState["sno"] = serial_id.Text;
            DataSet ds = dml.Find("select * from ViewReportUSER where ReportId = '" + serial_id.Text + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {

                lblUserGrpName.ClearSelection();

                lblReport.ClearSelection();

                lblUserGrpName.Items.FindByValue(ds.Tables[0].Rows[0]["UserGrpId"].ToString()).Selected = true;
                ddl_menu();
                lblmenu.ClearSelection();
                lblmenu.Items.FindByValue(ds.Tables[0].Rows[0]["MenuId"].ToString()).Selected = true;

                lblReport.Items.FindByValue(ds.Tables[0].Rows[0]["ReportId"].ToString()).Selected = true;

                // lblUserGrpName.SelectedItem.Text = dds.Tables[0].Rows[0]["UserGrpName"].ToString();
                //  lblmenu.SelectedItem.Text = dds.Tables[0].Rows[0]["Menu_title"].ToString();
                //  lblReport.SelectedItem.Text = dds.Tables[0].Rows[0]["Report_Title"].ToString();
                DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["SysDate"].ToString());
                txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                dml.dateConvert(txtEntryDate);
                string print = ds.Tables[0].Rows[0]["Print"].ToString();
                string view = ds.Tables[0].Rows[0]["View"].ToString();

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                string hide = ds.Tables[0].Rows[0]["Hide"].ToString();

                lblSystemDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

                if (chk == true)
                {
                    chkActive_Status.Checked = true;
                }
                else
                {
                    chkActive_Status.Checked = false;
                }
                if (hide == "N")
                {
                    rdb_Hide_N.Checked = true;
                }
                else
                {
                    rdb_Hide_Y.Checked = true;
                }

                if (view == "Y")
                {
                    chk_R_View.Checked = true;
                }
                else
                {
                    chk_R_View.Checked = false;
                }
                if (print == "Y")
                {
                    chk_R_print.Checked = true;
                }
                else
                {
                    chk_R_print.Checked = false;
                }


                txtSortOrder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();
                string entruserid = ds.Tables[0].Rows[0]["EnterUserId"].ToString();
                DataSet ddss = dml.Find("Select user_name from set_user_manager where userid = '" + entruserid + "'");
                lblEntryUSer_Name.Text = ddss.Tables[0].Rows[0]["user_name"].ToString();

            }
            else
            {
                Div1.Visible = true;
                Label1.Text = "Entry Not Inserted";
                lblUserGrpName.SelectedIndex = 0;
                lblReport.SelectedIndex = 0;
                lblmenu.SelectedIndex = 0;
                chkActive_Status.Checked = false;
                rdb_Hide_Y.Checked = false;
                rdb_Hide_N.Checked = false;
                txtSortOrder.Text = "";
                lblSystemDate.Text = "";
                lblEntryUSer_Name.Text = "";
                txtEntryDate.Text = "";

            }
        }
        catch (Exception ex)
        {
            Label1.Enabled = true;
            Label1.Text = ex.Message;
        }


    }

    public void selectcheck()
    {
        string view, print, apply, rdbHide;
        try
        {
            UserGrpID = Request.QueryString["UsergrpID"];
            DataSet ds;

            int aa = lblUserGrpName.SelectedIndex;

            if (lblUserGrpName.SelectedIndex != 0)
            {
                ds = dml.Find("select  ReportId,Hide, [View], [Print] ,EntryDate from ViewReportUSER where UserGrpId = '" + lblUserGrpName.SelectedItem.Value + "' and Record_deleted = 0 order by ReportId asc");
            }
            else
            {
                ds = dml.Find("select  ReportId,Hide, [View], [Print] ,EntryDate from ViewReportUSER where UserGrpId = '" + UserGrpID + "' and Record_deleted = 0 order by ReportId asc");
            }
            int countrow = ds.Tables[0].Rows.Count;
            if (countrow > 0)
            {

                for (int i = 0; i <= countrow - 1; i++)
                {
                    string val = ds.Tables[0].Rows[i]["ReportId"].ToString();
                    rdbHide = ds.Tables[0].Rows[i]["Hide"].ToString();
                    view = ds.Tables[0].Rows[i]["View"].ToString();
                    print = ds.Tables[0].Rows[i]["Print"].ToString();
                    apply = ds.Tables[0].Rows[i]["EntryDate"].ToString();
                    foreach (GridViewRow grow in GridView4.Rows)
                    {
                        CheckBox chk_del = (CheckBox)grow.FindControl("chkSelect");
                        RadioButton rdbyes = (RadioButton)grow.FindControl("rdb_PY");
                        RadioButton rdbNo = (RadioButton)grow.FindControl("rdb_PN");

                        RadioButton rdbMain = (RadioButton)grow.FindControl("rdb_VY");
                        RadioButton rdbSite = (RadioButton)grow.FindControl("rdb_VN");

                        RadioButton rdbH_Yes = (RadioButton)grow.FindControl("rdb_HYes");
                        RadioButton rdbH_No = (RadioButton)grow.FindControl("rdb_NYes");

                        TextBox txtapply = (TextBox)grow.FindControl("txtAppleDate123");
                        Label lblID = (Label)grow.FindControl("lblNatureID");

                        if (lblID.Text == val)
                        {

                            if (print == "True")
                            {
                                rdbMain.Checked = true;
                                rdbSite.Checked = false;
                            }
                            if (print == "False")
                            {
                                rdbMain.Checked = false;
                                rdbSite.Checked = true;
                            }

                            if (view == "True")
                            {
                                rdbyes.Checked = true;
                                rdbNo.Checked = false;
                            }
                            if (view == "False")
                            {
                                rdbyes.Checked = false;
                                rdbNo.Checked = true;
                            }
                            if (rdbHide == "True")
                            {
                                rdbH_Yes.Checked = true;
                                rdbH_No.Checked = false;
                            }
                            if (rdbHide == "False")
                            {
                                rdbH_Yes.Checked = false;
                                rdbH_No.Checked = true;
                            }

                            txtapply.Text = dml.dateConvert(apply);
                            // dml.Insert("INSERT INTO [SET_BPartnerType] ([GocID], [BPartnerID], [BPNatureID], [SysDate]) VALUES  ('1', '" + ViewState["BPID"].ToString() + "', '" + lblID.Text + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "');", "");
                            chk_del.Checked = true;

                        }
                    }
                }


            }
            else {

                bool a = ((CheckBox)GridView4.HeaderRow.FindControl("chkall")).Checked;
                for (int i = 0; i < GridView4.Rows.Count; i++)
                {
                    TextBox txtdate = ((TextBox)GridView4.Rows[i].FindControl("txtAppleDate123"));
                    txtdate.Attributes.Add("readonly", "readonly");

                    ((CheckBox)GridView4.Rows[i].FindControl("chkSelect")).Checked = false;
                    txtdate.Text = "";
                    // selectcheck();

                }

            }


        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

    protected void chkall_CheckedChanged(object sender, EventArgs e)
    {
        int co = 0;
        int ab = 0;
        CheckBox chkall = (CheckBox)GridView4.HeaderRow.FindControl("chkall");
        bool a = ((CheckBox)GridView4.HeaderRow.FindControl("chkall")).Checked;
        for (int i = 0; i < GridView4.Rows.Count; i++)
        {
            TextBox txtdate = ((TextBox)GridView4.Rows[i].FindControl("txtAppleDate123"));
            txtdate.Attributes.Add("readonly", "readonly");
            if (a == true)
            {
                ((CheckBox)GridView4.Rows[i].FindControl("chkSelect")).Checked = true;
                co = co + 1;

            }
            if (a == false)
            {
                ((CheckBox)GridView4.Rows[i].FindControl("chkSelect")).Checked = false;
                ab = ab + 1;
                txtdate.Text = "";
                selectcheck();

            }


        }
        if (GridView4.Rows.Count == co)
        {
            chkall.Checked = true;
        }

        else if (GridView4.Rows.Count == ab)
        {
            for (int q = 0; q < ab; q++)
            {
                ((CheckBox)GridView4.Rows[q].FindControl("chkSelect")).Checked = false;
                selectcheck();
            }
        }
        else
        {
            selectcheck();

        }

    }


    protected void Button1_Click(object sender, EventArgs e)
    {


        //  selectcount();
        int count = 0;
        int uncount = 0;

        int ca = 0;
        int ca1 = 0;
        int un = 0;
        bool flag = true;

        UserGrpID = Request.QueryString["UsergrpID"];
        DataSet ds, ds1, ds2;
        if (lblUserGrpName.SelectedIndex != 0)
        {
            ds = dml.Find("select * from SET_UserGrp_Report where UserGrpId = '" + lblUserGrpName.SelectedItem.Value + "' and Record_deleted = 0 ");
            ds1 = dml.Find("select  * from SET_UserGrp_Report where UserGrpId = '" + lblUserGrpName.SelectedItem.Value + "' and Record_deleted = 1 ");
        }
        else
        {
            ds = dml.Find("select  * from SET_UserGrp_Report where UserGrpId = '" + UserGrpID + "' and Record_deleted = 0 ");
            ds1 = dml.Find("select  * from SET_UserGrp_Report where UserGrpId = '" + UserGrpID + "' and Record_deleted = 1 ");
        }


        int countrow = ds.Tables[0].Rows.Count;
        int countdel = ds1.Tables[0].Rows.Count;
        if (countrow > 0)
        {
            string chkname;
            foreach (GridViewRow g in GridView4.Rows)
            {
                CheckBox chk_del = (CheckBox)g.FindControl("chkSelect");
                chkname = chk_del.Checked.ToString();
                chkname = chk_del.Text;

                Label lblID = (Label)g.FindControl("lblNatureID");
                if (chk_del.Checked == true)
                {
                    if (countdel > 0)
                    {
                        for (int i = 0; i <= countdel - 1; i++)
                        {
                            string val = ds1.Tables[0].Rows[i]["ReportId"].ToString();
                            if (val == lblID.Text && chk_del.Checked == true)
                            {
                                ca1 = ca1 + 1;
                                if (lblUserGrpName.SelectedIndex != 0)
                                {
                                    dml.Update("update SET_UserGrp_Report set Record_Deleted = '0'  where ReportId = '" + lblID.Text + "' and UserGrpId = '" + lblUserGrpName.SelectedItem.Value + "';", "");
                                }
                                else
                                {
                                    dml.Update("update SET_UserGrp_Report set Record_Deleted = '0'  where ReportId = '" + lblID.Text + "' and UserGrpId = '" + UserGrpID + "';", "");
                                }
                                flag = false;

                            }
                        }
                    }
                    else
                    {
                        if (chk_del.Checked == true)
                        {

                            if (lblUserGrpName.SelectedIndex != 0)
                            {
                                ds2 = dml.Find("select ReportId, [View], [Print], EntryDate,Hide,SortOrder from SET_UserGrp_Report  where UserGrpId = '" + lblUserGrpName.SelectedItem.Value + "' and ReportId = '" + lblID.Text + "' and Record_deleted = 0 order by ReportId");
                            }
                            else
                            {
                                ds2 = dml.Find("select ReportId, [View], [Print], EntryDate,Hide,SortOrder from SET_UserGrp_Report  where UserGrpId = '" + UserGrpID + "' and ReportId = '" + lblID.Text + "' and Record_deleted = 0 order by ReportId");
                            }

                            if (ds2.Tables[0].Rows.Count > 0)
                            {


                            }
                            else {
                                //Insert
                                userid = Request.QueryString["UserID"];
                                UserGrpID = Request.QueryString["UsergrpID"];
                                string view = "N", hideyn = "N", print = "N";
                                Label lbdocdes = (Label)g.FindControl("lbldoc");
                                Label lbdocname = (Label)g.FindControl("lbldocname");
                                TextBox applydate = (TextBox)g.FindControl("txtAppleDate123");
                                RadioButton rdbview = (RadioButton)g.FindControl("rdb_VY");
                                RadioButton rdbviewN = (RadioButton)g.FindControl("rdb_VN");

                                RadioButton rdbPrint = (RadioButton)g.FindControl("rdb_PY");
                                RadioButton rdbPrintN = (RadioButton)g.FindControl("rdb_PN");

                                RadioButton rdbHideY = (RadioButton)g.FindControl("rdb_HYes");
                                RadioButton rdbHideN = (RadioButton)g.FindControl("rdb_NYes");
                                if (rdbview.Checked == true)
                                {
                                    view = "Y";
                                }
                                if (rdbviewN.Checked == true)
                                {
                                    view = "N";
                                }
                                if (rdbHideY.Checked == true)
                                {
                                    hideyn = "Y";
                                }
                                if (rdbHideN.Checked == true)
                                {
                                    hideyn = "N";
                                }
                                if (rdbPrint.Checked == true)
                                {
                                    print = "Y";
                                }
                                if (rdbPrintN.Checked == true)
                                {
                                    print = "N";
                                }

                                string date = DateTime.Now.ToString("yyyy-MMM-dd"); ;//dml.dateconvertString(txtAppleDate.Text);

                                if (lblUserGrpName.SelectedIndex != 0)
                                {
                                    dml.Insert("INSERT INTO SET_UserGrp_Report ([UserGrpId], [EntryDate],  [View], [Print], [IsActive], [Hide], [SortOrder], [SysDate], [EnterUserId], [Record_Deleted], [CompId], [BranchId], [ReportId]) VALUES ('" + lblUserGrpName.SelectedItem.Value + "', '" + date + "', '" + view + "', '" + print + "', '1', '" + hideyn + "', '0', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '" + userid + "', '0'," + compid() + ", " + branchId() + ", '" + lblID.Text + "');", "");
                                    //dml.Insert("INSERT INTO SET_UserGrp_Documents([UserGrpId], [DocID], [DocDescription], [Main_Site], [ApplyDate], [IsActive], [IsHide], [SortOrder], [Record_Deleted], [CreatedBy], [CreateDate]) VALUES ('" + lblUserGrpName.SelectedItem.Value + "', '" + lblID.Text + "', '" + lbdocdes.Text + "', '" + mainsite + "', '" + date + "', '1', '" + hideyn + "', '0', '0', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "')", "");
                                }
                                else
                                {
                                    dml.Insert("INSERT INTO SET_UserGrp_Report ([UserGrpId], [EntryDate],  [View], [Print], [IsActive], [Hide], [SortOrder], [SysDate], [EnterUserId], [Record_Deleted], [CompId], [BranchId], [ReportId]) VALUES ('" + UserGrpID + "', '" + date + "', '" + view + "', '" + print + "', '1', '" + hideyn + "', '0', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '" + userid + "', '0'," + compid() + "," + branchId() + ", '" + lblID.Text + "');", "");
                                }
                                Label1.Text = "data Inserted";
                                //insert

                            }

                        }
                    }
                }
                else
                {

                    for (int i = 0; i <= countrow - 1; i++)
                    {
                        string val = ds.Tables[0].Rows[i]["ReportId"].ToString();
                        if (val == lblID.Text && chk_del.Checked == false)
                        {


                            if (lblUserGrpName.SelectedIndex != 0)
                            {
                                dml.Update("update SET_UserGrp_Report set Record_Deleted = '0'  where ReportId = '" + lblID.Text + "' and UserGrpId = '" + lblUserGrpName.SelectedItem.Value + "';", "");
                            }
                            else
                            {
                                dml.Update("update SET_UserGrp_Report set Record_Deleted = '0'  where ReportId = '" + lblID.Text + "' and UserGrpId = '" + UserGrpID + "';", "");
                            }
                            flag = false;
                        }


                    }
                    if (flag == false)
                    {
                        Label1.Text = "Updated Success";
                        GridView4.DataBind();
                        selectcheck();


                    }

                }


            }


        }
        else
        {

            foreach (GridViewRow g in GridView4.Rows)
            {
                CheckBox chk_del = (CheckBox)g.FindControl("chkSelect");
                Label lblID = (Label)g.FindControl("lblNatureID");
                userid = Request.QueryString["UserID"];
                UserGrpID = Request.QueryString["UsergrpID"];
                if (chk_del.Checked == true)
                {


                    if (lblUserGrpName.SelectedIndex != 0)
                    {
                        ds2 = dml.Find("select * from SET_UserGrp_Report where UserGrpId = '" + lblUserGrpName.SelectedItem.Value + "' and ReportId = '" + lblID.Text + "' and Record_deleted = 1");
                    }
                    else
                    {
                        ds2 = dml.Find("select * from SET_UserGrp_Report where UserGrpId = '" + UserGrpID + "' and ReportId = '" + lblID.Text + "' and Record_deleted = 1");
                    }

                    if (ds2.Tables[0].Rows.Count > 0)
                    {


                        if (lblUserGrpName.SelectedIndex != 0)
                        {
                            dml.Update("update SET_UserGrp_Report set Record_Deleted = '0'  where ReportId = '" + lblID.Text + "' and UserGrpId = '" + lblUserGrpName.SelectedItem.Value + "';", "");
                        }
                        else
                        {
                            dml.Update("update SET_UserGrp_Report set Record_Deleted = '0'  where ReportId = '" + lblID.Text + "' and UserGrpId = '" + UserGrpID + "';", "");
                        }
                        Label1.Text = "Updated Success";


                    }
                    else
                    {

                        UserGrpID = Request.QueryString["UsergrpID"];
                        string view = "N", hideyn = "N", print = "N";
                        Label lbdocdes = (Label)g.FindControl("lbldoc");
                        Label lbdocname = (Label)g.FindControl("lbldocname");
                        TextBox applydate = (TextBox)g.FindControl("txtAppleDate123");
                        RadioButton rdbview = (RadioButton)g.FindControl("rdb_VY");
                        RadioButton rdbviewN = (RadioButton)g.FindControl("rdb_VN");

                        RadioButton rdbPrint = (RadioButton)g.FindControl("rdb_PY");
                        RadioButton rdbPrintN = (RadioButton)g.FindControl("rdb_PN");

                        RadioButton rdbHideY = (RadioButton)g.FindControl("rdb_HYes");
                        RadioButton rdbHideN = (RadioButton)g.FindControl("rdb_NYes");
                        if (rdbview.Checked == true)
                        {
                            view = "Y";
                        }
                        if (rdbviewN.Checked == true)
                        {
                            view = "N";
                        }
                        if (rdbHideY.Checked == true)
                        {
                            hideyn = "Y";
                        }
                        if (rdbHideN.Checked == true)
                        {
                            hideyn = "N";
                        }
                        if (rdbPrint.Checked == true)
                        {
                            print = "Y";
                        }
                        if (rdbPrintN.Checked == true)
                        {
                            print = "N";
                        }
                        userid = Request.QueryString["UserID"];
                        string date = DateTime.Now.ToString("yyyy-MMM-dd"); ;//dml.dateconvertString(txtAppleDate.Text);

                        if (lblUserGrpName.SelectedIndex != 0)
                        {
                            dml.Insert("INSERT INTO SET_UserGrp_Report ([UserGrpId], [EntryDate],  [View], [Print], [IsActive], [Hide], [SortOrder], [SysDate], [EnterUserId], [Record_Deleted], [CompId], [BranchId], [ReportId]) VALUES ('" + lblUserGrpName.SelectedItem.Value + "', '" + date + "', '" + view + "', '" + print + "', '1', '" + hideyn + "', '0', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '" + userid + "', '0', '" + compid() + "', '" + branchId() + "', '" + lblID.Text + "');", "");
                        }
                        else
                        {
                            dml.Insert("INSERT INTO SET_UserGrp_Report ([UserGrpId], [EntryDate],  [View], [Print], [IsActive], [Hide], [SortOrder], [SysDate], [EnterUserId], [Record_Deleted], [CompId], [BranchId], [ReportId]) VALUES ('" + UserGrpID + "', '" + date + "', '" + view + "', '" + print + "', '1', '" + hideyn + "', '0', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '" + userid + "', '0', '" + compid() + "', '" + branchId() + "', '" + lblID.Text + "');", "");
                        }
                        Label1.Text = "data Inserted";
                    }
                }
            }
        }

        datamenu_view();
        GridView4.DataBind();
        selectcheck();
    }

    public void selectcount()
    {

        int count = 0;
        int uncount = 0;
        int ca = 0;
        int s = 0;


        foreach (GridViewRow grow in GridView4.Rows)
        {
            CheckBox chk_del = (CheckBox)grow.FindControl("chkSelect");
            Label lblID = (Label)grow.FindControl("lblNatureID");
            if (chk_del.Checked)
            {

                count = count + 1;
            }
            else
                uncount = uncount + 1;

        }

        string[] chkarray = new string[count];
        int c = 0;
        int e = 0;
        foreach (GridViewRow grow in GridView4.Rows)
        {
            CheckBox chk_del = (CheckBox)grow.FindControl("chkSelect");
            Label lblID = (Label)grow.FindControl("lblNatureID");
            if (chk_del.Checked)
            {
                chkarray[c] = lblID.Text;
                c = c + 1;

            }
            else
            {

            }


        }
        DataSet ds;
        UserGrpID = Request.QueryString["UsergrpID"];
        userid = Request.QueryString["UserID"];
        foreach (GridViewRow grow in GridView4.Rows)
        {
            UserGrpID = Request.QueryString["UsergrpID"];
            CheckBox chk_del = (CheckBox)grow.FindControl("chkSelect");
            Label lblID = (Label)grow.FindControl("lblNatureID");
            if (chk_del.Checked)
            {
                for (int a = 0; a < chkarray.Length; a++)
                {
                    if (chkarray[a].ToString() == lblID.Text)
                    {

                        if (lblUserGrpName.SelectedIndex != 0)
                        {
                            ds = dml.Find("SELECT * from SET_UserGrp_Report where DocID= '" + lblID.Text + "' and UserGrpId='" + lblUserGrpName.SelectedItem.Value + "' and Record_Deleted = '0'");
                        }
                        else
                        {
                            ds = dml.Find("SELECT * from SET_UserGrp_Report where DocID= '" + lblID.Text + "' and UserGrpId='" + UserGrpID + "' and Record_Deleted = '0'");
                        }



                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            e = e + 1;
                        }
                        else
                        {
                            userid = Request.QueryString["UserID"];
                            UserGrpID = Request.QueryString["UsergrpID"];
                            string view = "N", hideyn = "N", print = "N";
                            Label lbdocdes = (Label)grow.FindControl("lbldoc");
                            Label lbdocname = (Label)grow.FindControl("lbldocname");
                            TextBox applydate = (TextBox)grow.FindControl("txtAppleDate123");
                            RadioButton rdbview = (RadioButton)grow.FindControl("rdb_VY");
                            RadioButton rdbviewN = (RadioButton)grow.FindControl("rdb_VN");

                            RadioButton rdbPrint = (RadioButton)grow.FindControl("rdb_PY");
                            RadioButton rdbPrintN = (RadioButton)grow.FindControl("rdb_PN");

                            RadioButton rdbHideY = (RadioButton)grow.FindControl("rdb_HYes");
                            RadioButton rdbHideN = (RadioButton)grow.FindControl("rdb_NYes");
                            if (rdbview.Checked == true)
                            {
                                view = "Y";
                            }
                            if (rdbviewN.Checked == true)
                            {
                                view = "N";
                            }
                            if (rdbHideY.Checked == true)
                            {
                                hideyn = "Y";
                            }
                            if (rdbHideN.Checked == true)
                            {
                                hideyn = "N";
                            }
                            if (rdbPrint.Checked == true)
                            {
                                print = "Y";
                            }
                            if (rdbPrintN.Checked == true)
                            {
                                print = "N";
                            }

                            string date = DateTime.Now.ToString("yyyy-MMM-dd"); ;//dml.dateconvertString(txtAppleDate.Text);

                            if (lblUserGrpName.SelectedIndex != 0)
                            {
                                dml.Insert("INSERT INTO SET_UserGrp_Report ([UserGrpId], [EntryDate],  [View], [Print], [IsActive], [Hide], [SortOrder], [SysDate], [EnterUserId], [Record_Deleted], [CompId], [BranchId], [ReportId]) VALUES ('" + lblUserGrpName.SelectedItem.Value + "', '" + date + "', '" + view + "', '" + print + "', '1', '" + hideyn + "', '0', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '" + userid + ", '0', " + compid() + "," + branchId() + ", '" + lblID.Text + "');", "");
                            }
                            else
                            {
                                dml.Insert("INSERT INTO SET_UserGrp_Report ([UserGrpId], [EntryDate],  [View], [Print], [IsActive], [Hide], [SortOrder], [SysDate], [EnterUserId], [Record_Deleted], [CompId], [BranchId], [ReportId]) VALUES ('" + UserGrpID + "', '" + date + "', '" + view + "', '" + print + "', '1', '" + hideyn + "', '0', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '" + userid + "', '0'," + compid() + "," + branchId() + ", '" + lblID.Text + "');", "");
                            }
                            Label1.Text = "data Inserted";
                            datamenu_view();
                            GridView4.DataBind();
                            selectcheck();

                        }

                    }

                }
            }
        }

        datamenu_view();
        GridView4.DataBind();


    }
    protected void ddlUsergrp_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Text = "";
        datamenu_view();
        selectcheck();
    }
    public void datamenu_view()
    {
        UserGrpID = Request.QueryString["UsergrpID"];

        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        try
        {
            cmd = new SqlCommand("sp_USERREPORT", con);

            if (lblUserGrpName.SelectedIndex != 0)
            {
                string text = lblUserGrpName.SelectedItem.Text;
                string id = lblUserGrpName.SelectedItem.Value;
                cmd.Parameters.Add(new SqlParameter("@id", lblUserGrpName.SelectedItem.Value));
            }
            else
            {
                cmd.Parameters.Add(new SqlParameter("@id", UserGrpID));
            }


            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            da.Fill(dt);
            GridView4.DataSource = dt;
            GridView4.DataBind();
        }
        catch (Exception x)
        {

        }
        finally
        {
            cmd.Dispose();
            con.Close();
        }


    }

    public void ccc()
    {
        foreach (GridViewRow g in GridView4.Rows)
        {
            CheckBox chk = (CheckBox)g.FindControl("chkSelect");
            string text = chk.Checked.ToString();
        }
    }


    protected void Button2_Click(object sender, EventArgs e)
    {

        ccc();
    }

    public int branchId()
    {
        string BranchId = Request.Cookies["BranchId"].Value;
        return Convert.ToInt32(BranchId);
    }



    protected void GridView4_PreRender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CheckBox chkall = (CheckBox)GridView4.HeaderRow.FindControl("chkall");

            GridView4.DataSource = datamenu_table();
            GridView4.DataBind();
            if (chkall.Checked == true)
            {
                chkall_CheckedChanged(sender, e);
            }
        }

        if (GridView4.Rows.Count > 0)
        {
            //This replaces <td> with <th> and adds the scope attribute
            GridView4.UseAccessibleHeader = true;

            //This will add the <thead> and <tbody> elements
            GridView4.HeaderRow.TableSection = TableRowSection.TableHeader;

            //This adds the <tfoot> element. 
            //Remove if you don't have a footer row
            //gvTheGrid.FooterRow.TableSection = TableRowSection.TableFooter;
        }


    }
    public DataTable datamenu_table()
    {
        UserGrpID = Request.QueryString["UsergrpID"];

        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        try
        {
            cmd = new SqlCommand("sp_USERREPORT", con);

            if (lblUserGrpName.SelectedIndex != 0)
            {
                string text = lblUserGrpName.SelectedItem.Text;
                string id = lblUserGrpName.SelectedItem.Value;
                cmd.Parameters.Add(new SqlParameter("@id", lblUserGrpName.SelectedItem.Value));
            }
            else
            {
                cmd.Parameters.Add(new SqlParameter("@id", UserGrpID));
            }


            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            da.Fill(dt);

            return dt;
        }
        catch (Exception x)
        {

        }
        finally
        {

            cmd.Dispose();
            con.Close();
        }
        return dt;

    }
}