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
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
    DmlOperation dml = new DmlOperation();
    FieldHide fd = new FieldHide();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            Fiscal_Error.Visible = false;
            Findbox.Visible = false;
            btnUpdate.Visible = false;
            btnSave.Visible = false;
            userid = Request.QueryString["UserID"];
            UserGrpID = Request.QueryString["UsergrpID"];
            FormID = Request.QueryString["FormID"];
            Deletebox.Visible = false;
            Editbox.Visible = false;
            btnDelete_after.Visible = false;
            Showall_Dml();


            dml.daterangeforfiscal(CalendarExtender1, Request.Cookies["fiscalYear"].Value);
            dml.dropdownsql(lblUserName, "SET_User_Manager", "user_name", "Sno");
            dml.dropdownsql(ddlComp, "SET_Company", "CompName", "CompId", "GOCId", gocid().ToString());
            dml.dropdownsql(ddlBranch, "SET_Branch", "BranchName", "BranchId", "CompId", compid().ToString());
            //dml.dropdownsql(ddlFiscalYear, "SET_Fiscal_Year", "Description", "FiscalYearID");
            dml.DropDownForFiscalYear(ddlFiscalYear, 0, 0);

            // select UserId, user_name from SET_User_Manager where Record_Deleted = '0'
            txtEntryDate.Attributes.Add("readonly", "readonly");
            dml.dropdownsql(ddlFind_UserName, "SET_User_Manager", "user_name", "UserId");
            dml.dropdownsql(ddlDel_UserName, "SET_User_Manager", "user_name", "UserId");
            dml.dropdownsql(ddlEdit_UserName, "SET_User_Manager", "user_name", "UserId");
            textClear();


            string user = "";
            DataSet dsusrGrp = dml.Find("select * from SET_UserGrp where Record_Deleted = '0' and IsActive = '1'  and UserGrpId = '" + UserGrpID + "'");

            if (dsusrGrp.Tables[0].Rows.Count > 0)
            {
                user = dsusrGrp.Tables[0].Rows[0]["Description"].ToString();
            }

            Hide_lblusergrpname.Text = user;

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            userid = Request.QueryString["UserID"];
            int chk = 0;

            if (chkActive.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
            string comall = "";
            string ddlvalue_comp = "";
            if (chkCompAll.Checked == true)
            {
                comall = "Y";
                ddlvalue_comp = "1";
            }
            else
            {
                comall = "N";
                ddlvalue_comp = ddlComp.SelectedItem.Value;
            }
            string BranchAll = "";
            string ddlvalue_Branch = "";
            if (chkBranchAll.Checked == true)
            {
                BranchAll = "Y";
                //ddlBranch.Items.FindByValue("1").Selected = true;
                ddlvalue_Branch = "1";
            }
            else
            {
                BranchAll = "N";

                ddlvalue_Branch = ddlBranch.SelectedItem.Value;

            }
            string Fiscaly = "";
            string ddlvalue_Fiscaly = "";
            if (chkFYearAll.Checked == true)
            {
                Fiscaly = "Y";
                ddlvalue_Fiscaly = "1";
            }
            else
            {
                Fiscaly = "N";
                ddlvalue_Fiscaly = ddlFiscalYear.SelectedItem.Value;
            }

            DataSet ds_usergrpname = dml.Find("select UserId from SET_User_Manager where Sno= " + lblUserName.SelectedItem.Value + "");
            string usergrpid = ds_usergrpname.Tables[0].Rows[0]["UserId"].ToString();

            if (chkCompAll.Checked == false && ddlComp.SelectedIndex == 0)
            {
                lblcomp.Enabled = true;
                lblcomp.Text = "Please select the Company";
                Fiscal_Error.Visible = false;
            }
            else if (chkBranchAll.Checked == false && ddlBranch.SelectedIndex == 0)
            {
                lblbranch.Enabled = true;
                lblbranch.Text = "Please select the Branch";
                Fiscal_Error.Visible = false;
            }
            else if (chkFYearAll.Checked == false && ddlFiscalYear.SelectedIndex == 0)
            {
                lblfyear.Enabled = true;
                lblfyear.Text = "Please select the Fiscal Year";
                Fiscal_Error.Visible = true;
            }
            else
            {
                Fiscal_Error.Visible = false;
                string Query = "INSERT INTO SET_User_Permission_CompBrYear ([UserId], [GocId], [CompId], [CompAll], [BranchId], [BranchAll], [FiscalYearsID], [FiscalYearAll], [SortOrder], [SysDate], [remarks], [active], [EntryDate], [EntryUserId], [Record_Deleted],[MLD]) VALUES ('" + usergrpid + "'," + gocid() + ", '" + ddlvalue_comp + "', '" + comall + "', '" + ddlvalue_Branch + "', '" + BranchAll + "', '" + ddlvalue_Fiscaly + "', '" + Fiscaly + "', '" + txtSortORder.Text + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '" + txtRemarks.Text + "', '" + chk + "', '" + txtEntryDate.Text + "', '" + userid + "' , 0,'" + dml.Encrypt("h") + "')";
                dml.Insert(Query, "");
                if (ddlFiscalYear.SelectedItem.Value != "Select Goc And Company") { 
                dml.Update("update SET_Fiscal_Year set MLD = '" + dml.Encrypt("q") + "' where FiscalyearID = '" + ddlFiscalYear.SelectedItem.Value + "'", "");
                }

                if (ddlBranch.SelectedItem.Value != "Please select...") { 
                dml.Update("update set_Branch set MLD = '" + dml.Encrypt("q") + "' where BranchID = '" + ddlBranch.SelectedItem.Value + "'", "");
                }
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "alertme()", true);

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
        FormID = Request.QueryString["FormID"];
        Showall_Dml();
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        btnSave.Visible = true;
        btnEdit.Visible = false;
        btnFind.Visible = false;
        btnCancel.Visible = true;
        btnDelete.Visible = false;
        btnInsert.Visible = false;
        btnUpdate.Visible = false;

        lblUserName.Enabled = true;
        lblGocName.Enabled = true;
        ddlComp.Enabled = true;
        chkCompAll.Enabled = true;
        ddlBranch.Enabled = true;
        chkBranchAll.Enabled = true;
        ddlFiscalYear.Enabled = true;
        chkFYearAll.Enabled = true;
        txtSortORder.Enabled = true;
        lblSystemDate.Enabled = true;
        txtRemarks.Enabled = true;
        chkActive.Enabled = true;
        txtEntryDate.Enabled = true;
        lblEntryUserName.Enabled = true;
        chkActive.Checked = true;
        chkCompAll.Checked = true;
        chkBranchAll.Checked = true;
        chkFYearAll.Checked = true;
        imgPopup.Enabled = true;
        chkCompAll_CheckedChanged(sender, e);
        chkBranchAll_CheckedChanged(sender, e);
        chkFYearAll_CheckedChanged(sender, e);



        lblGocName.Text = Show_GocName();
        lblEntryUserName.Text = show_username();
        lblEntryUserName.Enabled = false;
        lblSystemDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {



            string ed = txtEntryDate.Text;
            userid = Request.QueryString["UserID"];
            int chk = 0;

            if (chkActive.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
            string comall = "";
            string ddlvalue_comp = "";
            if (chkCompAll.Checked == true)
            {
                comall = "Y";
                ddlvalue_comp = "1";
            }
            else
            {
                comall = "N";
                ddlvalue_comp = ddlComp.SelectedItem.Value;
            }
            string BranchAll = "";
            string ddlvalue_Branch = "";
            if (chkBranchAll.Checked == true)
            {
                BranchAll = "Y";
                ddlvalue_Branch = "1";
            }
            else
            {
                BranchAll = "N";
                ddlvalue_Branch = ddlBranch.SelectedItem.Value;
            }
            string Fiscaly = "";
            string ddlvalue_Fiscaly = "";
            if (chkFYearAll.Checked == true)
            {
                Fiscaly = "Y";
                ddlvalue_Fiscaly = "1";
            }
            else
            {
                Fiscaly = "N";
                ddlvalue_Fiscaly = ddlFiscalYear.SelectedItem.Value;
            }
            DataSet ds_usergrpname = dml.Find("select UserId from SET_User_Manager where Sno= " + lblUserName.SelectedItem.Value + "");
            string usergrpid = ds_usergrpname.Tables[0].Rows[0]["UserId"].ToString();

            string emp, d, edp;
            if (ed == "")
            {
                ed = "EntryDate = NULL";
                edp = "EntryDate IS NULL";
            }
            else
            {
                ed = "EntryDate = '" + txtEntryDate.Text + "'";
                edp = "([EntryDate] = '" + txtEntryDate.Text + "')";
            }
            string sorder, remarks;
            if (txtSortORder.Text == "")
            {
                sorder = "([SortOrder] IS NULL)";
            }
            else
            {
                sorder = "([SortOrder] = '" + txtSortORder.Text + "')";
            }
            if (txtRemarks.Text == "")
            {
                remarks = "([remarks] IS NULL)";
            }
            else
            {
                remarks = "([remarks] = '" + txtRemarks.Text + "')";
            }

            DataSet ds_up = dml.Find("select * from SET_User_Permission_CompBrYear WHERE ([Sno]='" + ViewState["Sno"].ToString() + "') AND ([UserId]='" + usergrpid + "') AND ([CompId]='" + ddlvalue_comp + "') AND ([CompAll]='" + comall + "') AND ([BranchId]='" + ddlvalue_Branch + "') AND ([BranchAll]='" + BranchAll + "') AND ([FiscalYearsID]='" + ddlvalue_Fiscaly + "') AND ([FiscalYearAll]='" + Fiscaly + "') AND " + sorder + " ANd " + remarks + " AND ([active]='" + chk + "') AND " + edp + "");

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



                dml.Update("UPDATE SET_User_Permission_CompBrYear SET [UserId]='" + usergrpid + "', [GocId]=" + gocid() + ", [CompId]='" + ddlvalue_comp + "', [CompAll]='" + comall + "', [BranchId]='" + ddlvalue_Branch + "', [BranchAll]='" + BranchAll + "', [FiscalYearsID]='" + ddlvalue_Fiscaly + "', [FiscalYearAll]='" + Fiscaly + "', [SortOrder]='" + txtSortORder.Text + "', [SysDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', [remarks]='" + txtRemarks.Text + "', [active]='" + chk + "'," + ed + " , [EntryUserId]='" + userid + "' WHERE ([Sno]='" + ViewState["Sno"].ToString() + "');", "");
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
        FormID = Request.QueryString["FormID"];
        Showall_Dml();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            dml.Delete("update SET_User_Permission_CompBrYear set Record_Deleted = 1 where Sno =  " + ViewState["Sno"].ToString() + "", "");
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
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
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
        FormID = Request.QueryString["FormID"];
        Fiscal_Error.Visible = false;
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
            UserGrpID = Request.QueryString["UsergrpID"];

            string squer = "Select * from V_UserPermission";

            if (ddlEdit_UserName.SelectedIndex != 0)
            {
                swhere = "UserId='" + ddlEdit_UserName.SelectedItem.Value + "'";
            }
            else
            {
                swhere = "UserId is not null";
            }
            if (ChkEdit_Active.Checked == true)
            {
                swhere = swhere + " and active = '1'";
            }
            else if (ChkEdit_Active.Checked == false)
            {
                swhere = swhere + " and active = '0'";
            }
            else
            {
                swhere = swhere + " and active is not null";
            }

            string fiscal = Request.QueryString["fiscaly"];
            string stdate = dml.daterangefiscal_start(fiscal);
            string enddate = dml.daterangefiscal_end(fiscal);


            squer = squer + " where " + swhere + " and Record_Deleted = 0 and GocId = '" + gocid() + "'";
            //squer = squer + " where " + swhere + " and Record_Deleted = 0 and GocId = '" + gocid() + "' and CompId= '" + compid() + "' and BranchId = '" + branchId() + "' and FiscalYearsID = '" + FiscalYear() + "'";

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

            string swhere;
            UserGrpID = Request.QueryString["UsergrpID"];

            string squer = "Select * from V_UserPermission";

            if (ddlFind_UserName.SelectedIndex != 0)
            {
                swhere = "userId = '" + ddlFind_UserName.SelectedItem.Value + "'";
            }
            else
            {
                swhere = "userId is not null";
            }
            if (ChkFInd_Active.Checked == true)
            {
                swhere = swhere + " and active = '1'";
            }
            else if (ChkFInd_Active.Checked == false)
            {
                swhere = swhere + " and active = '0'";
            }
            else
            {
                swhere = swhere + " and active is not null";
            }
            string fiscal = Request.QueryString["fiscaly"];
            string stdate = dml.daterangefiscal_start(fiscal);
            string enddate = dml.daterangefiscal_end(fiscal);

                squer = squer + " where " + swhere + " and Record_Deleted = 0 and GocId = " + gocid() ;

            //if (Hide_lblusergrpname.Text.Contains("Super"))
            //{
            //    squer = squer + " where " + swhere + " and Record_Deleted = 0 and GocId = " + gocid() ;
            //}
            //else if (Hide_lblusergrpname.Text.Contains("Company"))
            //{
            //    squer = squer + " where " + swhere + " and Record_Deleted = 0 and GocId = " + gocid() + "  and CompId= " + compid() + " and FiscalYearsID = " + FiscalYear() ;
            //}
            //else
            //{
            //    squer = squer + " where " + swhere + " and Record_Deleted = 0 and GocId = '" + gocid() + "'  and CompId= '" + compid() + "' and BranchId= '" + branchId() + "'  and FiscalYearsID = '" + FiscalYear() + "' and (EntryDate >= '" + stdate + "' and EntryDate < '" + enddate + "') ORDER BY user_name";
            //}




            //swhere = "UserGrpId = '" + UserGrpID + "'";
            //squer = squer + " where " + swhere;
            btnCancel.Visible = true;
            btnFind.Visible = true;
            btnDelete.Visible = false;
            btnEdit.Visible = false;
            btnInsert.Visible = false;

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

            string squer = "Select * from V_UserPermission";

            if (ddlDel_UserName.SelectedIndex != 0)
            {
                swhere = "user_name like '" + ddlDel_UserName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "user_name is not null";
            }
            if (chkDelete_Active.Checked == true)
            {
                swhere = swhere + " and active = '1'";
            }
            else if (chkDelete_Active.Checked == false)
            {
                swhere = swhere + " and active = '0'";
            }
            else
            {
                swhere = swhere + " and active is not null";
            }
            string fiscal = Request.QueryString["fiscaly"];
            string stdate = dml.daterangefiscal_start(fiscal);
            string enddate = dml.daterangefiscal_end(fiscal);

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and GocId = '" + gocid() + "' and CompId= '" + compid() + "' and BranchId = '" + branchId() + "' and FiscalYearsID = '" + FiscalYear() + "' and (EntryDate >= '" + stdate + "' and EntryDate < '" + enddate + "') ORDER BY user_name";


            Findbox.Visible = false;
            Deletebox.Visible = true;
            Editbox.Visible = false;
            fieldbox.Visible = false;

            DataSet dgrid = dml.grid(squer);
            GridView2.DataSource = dgrid;
            GridView2.DataBind();

        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
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
    public void textClear()
    {
        lblUserName.Enabled = false;
        lblGocName.Enabled = false;
        ddlComp.Enabled = false;
        chkCompAll.Enabled = false;
        ddlBranch.Enabled = false;
        chkBranchAll.Enabled = false;
        ddlFiscalYear.Enabled = false;
        chkFYearAll.Enabled = false;
        txtSortORder.Enabled = false;
        lblSystemDate.Enabled = false;
        txtRemarks.Enabled = false;
        chkActive.Enabled = false;
        txtEntryDate.Enabled = false;
        lblEntryUserName.Enabled = false;
        imgPopup.Enabled = false;

        lblUserName.SelectedIndex = 0;
        lblGocName.Text = "";
        ddlComp.SelectedIndex = 0;
        chkCompAll.Checked = false;
        ddlBranch.SelectedIndex = 0;
        chkBranchAll.Checked = false;
        ddlFiscalYear.SelectedIndex = 0;
        chkFYearAll.Checked = false;
        txtSortORder.Text = "";
        lblSystemDate.Text = "";
        txtRemarks.Text = "";
        chkActive.Checked = false;
        txtEntryDate.Text = "";
        lblEntryUserName.Text = "";
        Label1.Text = "";

        lblcomp.Enabled = false;
        lblbranch.Enabled = false;
        lblfyear.Enabled = false;
        lblcomp.Text = "";
        lblbranch.Text = "";
        lblfyear.Text = "";
    }

    public string show_username()
    {
        DataSet ds= dml.Find("select User_name From SET_User_Manager Where UserId = '" + Request.QueryString["UserID"] + "'");
        userid = ds.Tables[0].Rows[0]["User_Name"].ToString(); 
        return userid;

    }
    public string Show_GocName()
    {
        int id = gocid();
        DataSet ds_autoUserName = dml.Find("select GOCName from SET_GOC where GocId ='" + id + "'");
        return ds_autoUserName.Tables[0].Rows[0]["GOCName"].ToString();

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
            ViewState["Sno"] = serialno;
            DataSet ds = dml.Find("select * from V_UserPermission where Sno = " + serialno);

            if (ds.Tables[0].Rows.Count > 0)

            {
                DataSet dds = dml.Find("select * from SET_User_Permission_CompBrYear where Sno = " + serialno);

                lblUserName.ClearSelection();
                ddlComp.ClearSelection();
                ddlBranch.ClearSelection();
                ddlFiscalYear.ClearSelection();

                lblGocName.Text = ds.Tables[0].Rows[0]["GOCName"].ToString();
                lblUserName.Items.FindByText(ds.Tables[0].Rows[0]["user_name"].ToString()).Selected = true;

                txtEntryDate.Text = dds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtSortORder.Text = dds.Tables[0].Rows[0]["SortOrder"].ToString();
                txtRemarks.Text = dds.Tables[0].Rows[0]["remarks"].ToString();
                dml.dateConvert(txtEntryDate);
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["active"].ToString());


                string compall = ds.Tables[0].Rows[0]["CompAll"].ToString();
                string Branchall = ds.Tables[0].Rows[0]["BranchAll"].ToString();
                string FiscalAll = ds.Tables[0].Rows[0]["FiscalYearAll"].ToString();


                DateTime sys = DateTime.Parse(dds.Tables[0].Rows[0]["SysDate"].ToString());
                lblSystemDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");


                string entruserid = dds.Tables[0].Rows[0]["EntryUserId"].ToString();
                DataSet ddss = dml.Find("Select user_name from set_user_manager where userid= '" + entruserid + "'");
                lblEntryUserName.Text = ddss.Tables[0].Rows[0]["user_name"].ToString();

                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (compall == "Y")
                {
                    chkCompAll.Checked = true;
                }
                else
                {
                    ddlComp.Items.FindByText(ds.Tables[0].Rows[0]["CompName"].ToString()).Selected = true;
                    chkCompAll.Checked = false;
                }
                if (Branchall == "Y")
                {
                    chkBranchAll.Checked = true;
                }
                else
                {
                    dml.dropdownsql(ddlBranch, "SET_Branch", "BranchName", "BranchId", "CompId", ddlComp.SelectedItem.Value);
                    ddlBranch.ClearSelection();
                    string bran = ds.Tables[0].Rows[0]["BranchName"].ToString();
                    ddlBranch.Items.FindByText(bran).Selected = true;
                    chkBranchAll.Checked = false;
                }
                if (FiscalAll == "Y")
                {
                    chkFYearAll.Checked = true;
                }
                else
                {
                    ddlFiscalYear.Items.FindByText(ds.Tables[0].Rows[0]["Description"].ToString()).Selected = true;
                    chkFYearAll.Checked = false;
                }


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
            ViewState["Sno"] = serialno;
            DataSet ds = dml.Find("select * from V_UserPermission where Sno = " + serialno);

            if (ds.Tables[0].Rows.Count > 0)

            {
                DataSet dds = dml.Find("select * from SET_User_Permission_CompBrYear where Sno = " + serialno);

                lblUserName.ClearSelection();
                ddlComp.ClearSelection();
                ddlBranch.ClearSelection();
                ddlFiscalYear.ClearSelection();

                lblGocName.Text = ds.Tables[0].Rows[0]["GOCName"].ToString();
                lblUserName.Items.FindByText(ds.Tables[0].Rows[0]["user_name"].ToString()).Selected = true;

                txtEntryDate.Text = dds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtSortORder.Text = dds.Tables[0].Rows[0]["SortOrder"].ToString();
                txtRemarks.Text = dds.Tables[0].Rows[0]["remarks"].ToString();
                dml.dateConvert(txtEntryDate);
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["active"].ToString());


                string compall = ds.Tables[0].Rows[0]["CompAll"].ToString();
                string Branchall = ds.Tables[0].Rows[0]["BranchAll"].ToString();
                string FiscalAll = ds.Tables[0].Rows[0]["FiscalYearAll"].ToString();


                DateTime sys = DateTime.Parse(dds.Tables[0].Rows[0]["SysDate"].ToString());
                lblSystemDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");


                string entruserid = dds.Tables[0].Rows[0]["EntryUserId"].ToString();
                DataSet ddss = dml.Find("Select user_name from set_user_manager where userid= '" + entruserid + "'");
                lblEntryUserName.Text = ddss.Tables[0].Rows[0]["user_name"].ToString();

                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (compall == "Y")
                {
                    chkCompAll.Checked = true;
                }
                else
                {
                    ddlComp.Items.FindByText(ds.Tables[0].Rows[0]["CompName"].ToString()).Selected = true;
                    chkCompAll.Checked = false;
                }
                if (Branchall == "Y")
                {
                    chkBranchAll.Checked = true;
                }
                else
                {
                    dml.dropdownsql(ddlBranch, "SET_Branch", "BranchName", "BranchId", "CompId", ddlComp.SelectedItem.Value);
                    ddlBranch.ClearSelection();
                    string bran = ds.Tables[0].Rows[0]["BranchName"].ToString();
                    ddlBranch.Items.FindByText(bran).Selected = true;
                    chkBranchAll.Checked = false;
                }
                if (FiscalAll == "Y")
                {
                    chkFYearAll.Checked = true;
                }
                else
                {
                    ddlFiscalYear.Items.FindByText(ds.Tables[0].Rows[0]["Description"].ToString()).Selected = true;
                    chkFYearAll.Checked = false;
                }


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

        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {
            string serialno = GridView3.SelectedRow.Cells[1].Text;
            ViewState["Sno"] = serialno;
            DataSet ds = dml.Find("select * from V_UserPermission where Sno = " + serialno);

            if (ds.Tables[0].Rows.Count > 0)

            {

                DataSet dds = dml.Find("select * from SET_User_Permission_CompBrYear where Sno = " + serialno);

                lblUserName.ClearSelection();
                ddlComp.ClearSelection();
                ddlBranch.ClearSelection();
                ddlFiscalYear.ClearSelection();

                lblGocName.Text = ds.Tables[0].Rows[0]["GOCName"].ToString();
                lblUserName.Items.FindByText(ds.Tables[0].Rows[0]["user_name"].ToString()).Selected = true;

                txtEntryDate.Text = dds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtSortORder.Text = dds.Tables[0].Rows[0]["SortOrder"].ToString();
                txtRemarks.Text = dds.Tables[0].Rows[0]["remarks"].ToString();
                dml.dateConvert(txtEntryDate);
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["active"].ToString());


                string compall = ds.Tables[0].Rows[0]["CompAll"].ToString();
                string Branchall = ds.Tables[0].Rows[0]["BranchAll"].ToString();
                string FiscalAll = ds.Tables[0].Rows[0]["FiscalYearAll"].ToString();


                DateTime sys = DateTime.Parse(dds.Tables[0].Rows[0]["SysDate"].ToString());
                lblSystemDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");


                string entruserid = dds.Tables[0].Rows[0]["EntryUserId"].ToString();
                DataSet ddss = dml.Find("Select user_name from set_user_manager where userid= '" + entruserid + "'");
                lblEntryUserName.Text = ddss.Tables[0].Rows[0]["user_name"].ToString();

                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (compall == "Y")
                {
                    chkCompAll.Checked = true;
                }
                else
                {
                    ddlComp.Items.FindByText(ds.Tables[0].Rows[0]["CompName"].ToString()).Selected = true;

                    chkCompAll.Checked = false;
                }
                if (Branchall == "Y")
                {
                    chkBranchAll.Checked = true;
                }
                else
                {

                    dml.dropdownsql(ddlBranch, "SET_Branch", "BranchName", "BranchId", "CompId", ddlComp.SelectedItem.Value);
                    ddlBranch.ClearSelection();
                    string bran = ds.Tables[0].Rows[0]["BranchId"].ToString();
                   
                    ddlBranch.Items.FindByValue(bran).Selected = true;
                    chkBranchAll.Checked = false;
                }
                if (FiscalAll == "Y")
                {
                    chkFYearAll.Checked = true;
                }
                else
                {
                    dml.DropDownFiscal(ddlFiscalYear, gocid());
                    var count = ddlFiscalYear.Items.Count;
                    //ddlFiscalYear.Items.FindByValue(ds.Tables[0].Rows[0]["FiscalYearsId"].ToString()).Selected = true;
                    chkFYearAll.Checked = false;
                }
                lblUserName.Enabled = true;
                lblGocName.Enabled = true;
                ddlComp.Enabled = true;
                chkCompAll.Enabled = true;
                ddlBranch.Enabled = true;
                chkBranchAll.Enabled = true;
                ddlFiscalYear.Enabled = true;
                chkFYearAll.Enabled = true;
                txtSortORder.Enabled = true;
                lblSystemDate.Enabled = true;
                txtRemarks.Enabled = true;
                chkActive.Enabled = true;
                txtEntryDate.Enabled = true;
                lblEntryUserName.Enabled = true;
                imgPopup.Enabled = true;

            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    public int gocid()
    {
        string GocId = Request.Cookies["GocId"].Value;
        return Convert.ToInt32(GocId);
    }
    public int compid()
    {
        string CompId = Request.Cookies["CompId"].Value;
        return Convert.ToInt32(CompId);
    }
    public int branchId()
    {
        string BranchId = Request.Cookies["BranchId"].Value;
        return Convert.ToInt32(BranchId);
    }
    public int FiscalYear()
    {
        string FiscalYearId = Request.Cookies["FiscalYearId"].Value;
        return Convert.ToInt32(FiscalYearId);
    }
    public int Fyear()
    {
        try
        {

            string fy = Request.QueryString["fiscaly"];
            string compid = "";
            string gocid1 = "";
            DataSet ds = dml.Find("select FiscalYearID from SET_Fiscal_Year where FiscalYearId=" + FiscalYear() + " and GOCid = '" + gocid().ToString() + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                compid = ds.Tables[0].Rows[0]["FiscalYearID"].ToString();
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
    protected void chkCompAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCompAll.Checked == true)
        {
            ddlComp.Enabled = false;
            ddlComp.ClearSelection();

        }
        if (chkCompAll.Checked == false)
        {
            ddlComp.Enabled = true;
            ddlComp.ClearSelection();

        }
    }
    protected void chkBranchAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkBranchAll.Checked == true)
        {
            ddlBranch.Enabled = false;
            ddlBranch.ClearSelection();

        }
        if (chkBranchAll.Checked == false)
        {
            ddlBranch.Enabled = true;
            ddlBranch.ClearSelection();

        }
    }
    protected void chkFYearAll_CheckedChanged(object sender, EventArgs e)
    {

        if (chkFYearAll.Checked == true)
        {
            ddlFiscalYear.Enabled = false;
            ddlFiscalYear.ClearSelection();

        }
        if (chkFYearAll.Checked == false)
        {
            ddlFiscalYear.Enabled = true;
            ddlFiscalYear.ClearSelection();

        }

    }
    protected void ddlComp_SelectedIndexChanged(object sender, EventArgs e)
    {
        dml.dropdownsql(ddlBranch, "SET_Branch", "BranchName", "BranchId", "CompId", ddlComp.SelectedItem.Value);
        if (ddlComp.SelectedIndex != 0)
        {
            lblcomp.Text = "";
            lblcomp.Enabled = false;
        }

    }



    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex != 0)
        {
            lblbranch.Text = "";
            lblbranch.Enabled = false;
            dml.DropDownForFiscalYear(ddlFiscalYear, gocid(), Convert.ToInt32(ddlBranch.SelectedItem.Value));
        }
    }

    protected void ddlFiscalYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFiscalYear.SelectedIndex != 0)
        {
            lblfyear.Text = "";
            lblfyear.Enabled = false;
        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //string user = "";
        //DataSet dsusrGrp = dml.Find("select * from SET_UserGrp where Record_Deleted = '0' and IsActive = '1'  and UserGrpId = '"+UserGrpID+"'");

        //if(dsusrGrp.Tables[0].Rows.Count > 0)
        //{
        //    user = dsusrGrp.Tables[0].Rows[0]["Description"].ToString();
        //}


        string id = e.Row.Cells[1].Text;
        bool flag = dml.isNumber(id);

        if (flag == true)
        {

            DataSet ds = dml.Find("select Sno,InUse from SET_User_Permission_CompBrYear where Sno = '" + id + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string value = ds.Tables[0].Rows[i]["InUse"].ToString();

                    if (value == "Y")
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
        //string user = "";
        //DataSet dsusrGrp = dml.Find("select * from SET_UserGrp where Record_Deleted = '0' and IsActive = '1'  and UserGrpId = '" + UserGrpID + "'");

        //if (dsusrGrp.Tables[0].Rows.Count > 0)
        //{
        //    user = dsusrGrp.Tables[0].Rows[0]["Description"].ToString();
        //}


        string id = e.Row.Cells[1].Text;
        bool flag = dml.isNumber(id);


        if (flag == true)
        {

            DataSet ds = dml.Find("select Sno,InUse from SET_User_Permission_CompBrYear where Sno = '" + id + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string value = ds.Tables[0].Rows[i]["InUse"].ToString();
                    //if (user != "Super Admin")
                    //{
                    if (value == "Y")
                    {
                        e.Row.Enabled = false;
                        e.Row.Cells[0].ToolTip = "Cannot be editable, work in find mode only";
                        e.Row.BackColor = System.Drawing.Color.LightGray;
                    }
                    //}
                }
            }
        }
    }

}