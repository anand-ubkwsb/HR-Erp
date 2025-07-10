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
    int DateFrom, EditDays, DeleteDays, AddDays;
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

            Findbox.Visible = false;
            btnUpdate.Visible = false;
            btnSave.Visible = false;
            userid = Request.QueryString["UserID"];
            UserGrpID = Request.QueryString["UsergrpID"];
            FormID = Request.QueryString["FormID"];

            Deletebox.Visible = false;
            Editbox.Visible = false;
            btnDelete_after.Visible = false;
            updatecol.Visible = false;
            // fd.HideUSerGrpField(Page.Controls, UserGrpID, FormID, "SET_Fiscal_Year");
            // Showall_Dml();

            dml.dropdownsql(ddlEdit_Menu, "SET_Menu", "Menu_title", "MenuId");
            dml.dropdownsql(ddlEdit_RptTitle, "SET_Report", "Report_Title", "Sno");

            dml.dropdownsql(ddlDel_Menu, "SET_Menu", "Menu_title", "MenuId");
            dml.dropdownsql(ddlDel_RptTitle, "SET_Report", "Report_Title", "Sno");

            dml.dropdownsql(ddlFind_MenuTitle, "SET_Menu", "Menu_title", "MenuId");
            dml.dropdownsql(ddlFind_RptTitle, "SET_Report", "Report_Title", "Sno");

            dml.dropdownsql(lblmenu, "SET_Menu", "Menu_title", "MenuId");
           
            textClear();


        }


    }

    protected void showall_Dml() {
        userid = Request.QueryString["UserID"];
        FormID = Request.QueryString["FormID"];

        DataSet FiscalStatus;
        con.Open();
        string Query = "SELECT * FROM SET_FISCAL_YEAR WHERE FISCALYEARID=" + Convert.ToInt32(Request.Cookies["FiscalYearId"].Value);
        DataSet Fiscal = dml.Find(Query);
        string FiscalStatusQuery = "SELECT * FROM SET_FISCAL_YEAR_STATUS WHERE FISCALYEARSTATUSID=(SELECT FISCALYEARSTATUSID FROM SET_FISCAL_YEAR WHERE FISCALYEARID=" + Convert.ToInt32(Request.Cookies["FiscalYearId"].Value) + ") AND RECORD_DELETED='0'";
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
                else
                {
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

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet uniqueg_B_C = dml.Find("select * from SET_Report where Report_Title = '" + txtReportName.Text + "' and Record_Deleted = '0'");
            if (uniqueg_B_C.Tables[0].Rows.Count > 0)
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "Duplicated entry not allowed";
            }

            else {
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

                dml.Insert("INSERT INTO SET_Report ([Report_Title], [MenuId], [Report_Name], [Report_Path], [IsActive], [Hide], [SortOrder],[Record_Deleted], [CreatedBy], [CreateDate]) VALUES ('" + txtReportTitle.Text + "', '" + lblmenu.SelectedItem.Value + "', '" + txtReportName.Text + "', '" + txtReportPath.Text + "', '" + chk + "', '" + hide + "', '" + txtSortOrder.Text + "','0','"+show_username()+"','"+DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")+"');", "");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "alertme()", true);

               
                btnInsert.Visible = true;
                btnDelete.Visible = true;
                btnUpdate.Visible = false;
                btnEdit.Visible = true;
                btnFind.Visible = true;
                btnSave.Visible = false;
                btnDelete_after.Visible = false;

                lblmenu.SelectedIndex = 0;
                chkActive_Status.Checked = false;
                rdb_Hide_Y.Checked = false;
                rdb_Hide_N.Checked = false;
                Label1.Text = "";
                txtSortOrder.Text = "";
                txtReportTitle.Text = "";
                txtReportName.Text = "";
                txtReportPath.Text = "";
                txtCreatedby.Text = "";
                txtCreatedDate.Text = "";
                txtUpdateBy.Text = "";
                txtUpdateDate.Text = "";
            }
        }
        catch (Exception ex)
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = ex.Message;
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

        chkActive_Status.Checked = true;
        txtSortOrder.Enabled = true;
        rdb_Hide_N.Enabled = true;
        rdb_Hide_Y.Enabled = true;
        lblmenu.Enabled = true;
        txtSortOrder.Enabled = true;
        txtReportTitle.Enabled = true;
        txtReportName.Enabled = true;
        txtReportPath.Enabled = true;
        chkActive_Status.Enabled = true;
        txtCreatedby.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdateBy.Enabled = true;
        txtUpdateDate.Enabled = true;
        txtCreatedDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
        rdb_Hide_N.Checked = true;


        userid = Request.QueryString["UserID"];
        DataSet ds_autoUserName = dml.Find("select user_name from SET_User_Manager where UserId ='" + userid + "'");
        txtCreatedby.Text = ds_autoUserName.Tables[0].Rows[0]["user_name"].ToString();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
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
            DataSet ds_up;
            //DataSet ds_usermenuid = dml.Find("select MenuId from SET_Menu where Sno = '" + lblmenu.SelectedItem.Value + "'");
            string menuid = lblmenu.SelectedItem.Value;
            if (lblmenu.SelectedIndex !=0)
            {
                ds_up = dml.Find("SELECT * from SET_Report WHERE ([Sno]='" + ViewState["Sno"] + "') AND ([Report_Title]='" + txtReportTitle.Text + "') AND ([MenuId]='" + menuid + "') AND ([Report_Name]='" + txtReportName.Text + "') AND ([Report_Path]='" + txtReportPath.Text + "') AND ([IsActive]='" + chk + "') AND ([Hide]='" + hide + "') AND ([SortOrder]='" + txtSortOrder.Text + "') AND ([Record_Deleted]='0')");
            }
            else
            {
                ds_up = dml.Find("SELECT * from SET_Report WHERE ([Sno]='" + ViewState["Sno"] + "') AND ([Report_Title]='" + txtReportTitle.Text + "') AND ([Report_Name]='" + txtReportName.Text + "') AND ([Report_Path]='" + txtReportPath.Text + "') AND ([IsActive]='" + chk + "') AND ([Hide]='" + hide + "') AND ([SortOrder]='" + txtSortOrder.Text + "') AND ([Record_Deleted]='0')");
            }

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

                dml.Update("UPDATE SET_Report SET [Report_Title]='" + txtReportTitle.Text + "', [MenuId]='" + menuid + "', [Report_Name]='" + txtReportName.Text + "', [Report_Path]='" + txtReportPath.Text + "', [IsActive]='" + chk + "', [Hide]='" + hide + "', [SortOrder]='" + txtSortOrder.Text + "' , [UpdatedBy]='"+show_username()+"', [UpdateDate]='"+DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")+"' WHERE ([Sno]='" + ViewState["Sno"].ToString() + "')", "");
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
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            
            dml.Delete("update SET_Report set Record_Deleted = 1 where Sno = " + ViewState["Sno"].ToString() + "", "");
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
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        textClear();
        btnInsert.Visible = true;
        updatecol.Visible = false;
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
            string swhere = "";
            string squer = "select * from SET_Report";


            if (ddlEdit_RptTitle.SelectedIndex != 0)
            {
                swhere = "Report_Title = '" + ddlEdit_RptTitle.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "Report_Title is not null";
            }
            if (ddlEdit_Menu.SelectedIndex != 0)
            {
                swhere = swhere + " and MenuId = '" + ddlEdit_Menu.SelectedItem.Value + "'";
            }

            if (chkEdit_active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkEdit_active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0  ORDER BY Report_Title";
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
        btnCancel.Visible = true;
        btnFind.Visible = true;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnInsert.Visible = false;
        try
            {
            string swhere = "";
            string squer = "select * from SET_Report";


            if (ddlFind_RptTitle.SelectedIndex != 0)
            {
                swhere = "Report_Title = '" + ddlFind_RptTitle.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "Report_Title is not null";
            }
            if (ddlFind_MenuTitle.SelectedIndex != 0)
            {
                swhere = swhere + " and MenuId = '" + ddlFind_MenuTitle.SelectedItem.Value + "'";
            }

            if (ChkFInd_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (ChkFInd_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0  ORDER BY Report_Title";

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
        btnDelete.Visible = false;
        btnInsert.Visible = false;
        btnCancel.Visible = true;
        btnEdit.Visible = false;
        btnDelete_after.Visible = true;

        try
        {

            //string squer = "select * from SET_Report where Record_Deleted = '0'";
            GridView2.DataBind();
            string swhere = "";
            string squer = "select * from SET_Report";


            if (ddlDel_RptTitle.SelectedIndex != 0)
            {
                swhere = "Report_Title = '" + ddlDel_RptTitle.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "Report_Title is not null";
            }
            if (ddlDel_Menu.SelectedIndex != 0)
            {
                swhere = swhere + " and MenuId = '" + ddlDel_Menu.SelectedItem.Value + "'";
            }
                       
            if (chkDel_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkDel_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0  ORDER BY Report_Title";

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
        lblmenu.SelectedIndex = 0;
        chkActive_Status.Checked = false;
        rdb_Hide_Y.Checked = false;
        rdb_Hide_N.Checked = false;
        Label1.Text = "";
        txtSortOrder.Text = "";
        txtReportTitle.Text = "";
        txtReportName.Text = "";
        txtReportPath.Text = "";
        txtCreatedby.Text = "";
        txtCreatedDate.Text = "";
        txtUpdateBy.Text = "";
        txtUpdateDate.Text = "";

        chkActive_Status.Checked = false;
        txtSortOrder.Enabled = false;
        rdb_Hide_N.Enabled = false;
        rdb_Hide_Y.Enabled = false;
        lblmenu.Enabled = false;
        txtSortOrder.Enabled = false;
        txtReportTitle.Enabled = false;
        txtReportName.Enabled = false;
        txtReportPath.Enabled = false;
        chkActive_Status.Enabled = false;
        txtCreatedby.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdateBy.Enabled = false;
        txtUpdateDate.Enabled = false;
    }

    public string show_username()
    {
        userid = Request.QueryString["UserID"];
        DataSet ds_autoUserName = dml.Find("select user_name from SET_User_Manager where UserId='" + userid + "'");
        return ds_autoUserName.Tables[0].Rows[0]["user_name"].ToString();

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

        updatecol.Visible = true;
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {
            lblmenu.ClearSelection();
            string serialno = GridView1.SelectedRow.Cells[1].Text;
            ViewState["Sno"] = serialno;
            DataSet ds = dml.Find("select * from SET_Report where Sno = '" + serialno +"' and Record_Deleted = '0'");
            //
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtReportTitle.Text = ds.Tables[0].Rows[0]["Report_Title"].ToString();
                txtReportPath.Text = ds.Tables[0].Rows[0]["Report_Path"].ToString();
                txtReportName.Text = ds.Tables[0].Rows[0]["Report_Name"].ToString();
                txtSortOrder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();
                txtCreatedby.Text = ds.Tables[0].Rows[0]["CreatedBy"].ToString();
                txtUpdateBy.Text = ds.Tables[0].Rows[0]["UpdatedBy"].ToString();
                if (ds.Tables[0].Rows[0]["MenuId"].ToString() != "")
                {
                    lblmenu.Items.FindByValue(ds.Tables[0].Rows[0]["MenuId"].ToString()).Selected = true;
                }
                else
                {
                    lblmenu.SelectedIndex = 0;
                }

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                string hide = ds.Tables[0].Rows[0]["Hide"].ToString();

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
        textClear();

        updatecol.Visible = true;
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        Deletebox.Visible = false;
        try
        {
            lblmenu.ClearSelection();
            string serialno = GridView2.SelectedRow.Cells[1].Text;
            ViewState["Sno"] = serialno;
            DataSet ds = dml.Find("select * from SET_Report where Sno = " + serialno);
            //
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtReportTitle.Text = ds.Tables[0].Rows[0]["Report_Title"].ToString();
                txtReportPath.Text = ds.Tables[0].Rows[0]["Report_Path"].ToString();
                txtReportName.Text = ds.Tables[0].Rows[0]["Report_Name"].ToString();
                txtSortOrder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();
                txtCreatedby.Text = ds.Tables[0].Rows[0]["CreatedBy"].ToString();
                txtUpdateBy.Text = ds.Tables[0].Rows[0]["UpdatedBy"].ToString();
                if (ds.Tables[0].Rows[0]["MenuId"].ToString() != "")
                {
                    lblmenu.Items.FindByValue(ds.Tables[0].Rows[0]["MenuId"].ToString()).Selected = true;
                }
                else
                {
                    lblmenu.SelectedIndex = 0;
                }

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                string hide = ds.Tables[0].Rows[0]["Hide"].ToString();

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

        chkActive_Status.Checked = true;
        txtSortOrder.Enabled = true;
        rdb_Hide_N.Enabled = true;
        rdb_Hide_Y.Enabled = true;
        lblmenu.Enabled = true;
        txtSortOrder.Enabled = true;
        txtReportTitle.Enabled = true;
        txtReportName.Enabled = true;
        txtReportPath.Enabled = true;
        chkActive_Status.Enabled = true;
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
            lblmenu.ClearSelection();
            string serialno = GridView3.SelectedRow.Cells[1].Text;
            ViewState["Sno"] = serialno;
            DataSet ds = dml.Find("select * from SET_Report where Sno = " + serialno);
            //
            if (ds.Tables[0].Rows.Count > 0)
            {
               
                txtReportTitle.Text = ds.Tables[0].Rows[0]["Report_Title"].ToString();
                txtReportPath.Text = ds.Tables[0].Rows[0]["Report_Path"].ToString();
                txtReportName.Text = ds.Tables[0].Rows[0]["Report_Name"].ToString();
                txtSortOrder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();
                txtCreatedby.Text = ds.Tables[0].Rows[0]["CreatedBy"].ToString();
                txtUpdateBy.Text = ds.Tables[0].Rows[0]["UpdatedBy"].ToString();
                if (ds.Tables[0].Rows[0]["MenuId"].ToString() != "")
                {
                    lblmenu.Items.FindByValue(ds.Tables[0].Rows[0]["MenuId"].ToString()).Selected = true;
                }
                else
                {
                    lblmenu.SelectedIndex = 0;
                }

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                string hide = ds.Tables[0].Rows[0]["Hide"].ToString();

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


}