using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frm_SetFiscal : System.Web.UI.Page
{
    DataSet PeriodSet;
    int DateFrom, EditDays, DeleteDays, AddDays;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
    string userid, UserGrpID, FormID;
    DmlOperation dml = new DmlOperation();
    FieldHide fd = new FieldHide();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
           
            Findbox.Visible = false;
            btnUpdate.Visible = false;
            btnSave.Visible = false;
            Deletebox.Visible = false;
            Editbox.Visible = false;
            btnDelete_after.Visible = false;
            userid = Request.QueryString["UserID"];
            UserGrpID = Request.QueryString["UsergrpID"];
            FormID = Request.QueryString["FormID"];
            CalendarExtender1.EndDate = DateTime.Now;


            dml.dropdownsql(txtFind_FYsn, "SET_Fiscal_Year_Status", "FiscalYearStatusName", "FiscalYearStatusID");
            dml.dropdownsql(txtEdit_FYSN, "SET_Fiscal_Year_Status", "FiscalYearStatusName", "FiscalYearStatusID");
            dml.dropdownsql(txtDelete_FYSN, "SET_Fiscal_Year_Status", "FiscalYearStatusName", "FiscalYearStatusID");


            dml.dropdownsql(txtDelete_FYID, "SET_Fiscal_Year", "Description", "FiscalYearID");
            dml.dropdownsql(txtEdit_FYID, "SET_Fiscal_Year", "Description", "FiscalYearID");
            dml.dropdownsql(txtFind_FYid, "SET_Fiscal_Year", "Description", "FiscalYearID");
            txtEntry_Date.Attributes.Add("readonly", "readonly");
            txtFiscal_Year_Start_Date.Attributes.Add("readonly", "readonly");
            txtFiscal_Year_End_Date.Attributes.Add("readonly", "readonly");



            fd.HideUSerGrpField(Page.Controls, UserGrpID, FormID, "SET_Fiscal_Year");
            Showall_Dml();
           
            
            dml.dropdownsql(ddlPeriod_Description, "SET_Period", "Description", "PeriodID");
            dml.dropdownsql(ddlGOC_Name, "SET_GOC", "GOCName", "GocId");
            dml.dropdownsql(ddlFiscal_Year_Status_Name, "SET_Fiscal_Year_Status", "FiscalYearStatusName", "FiscalYearStatusID");
            textClear();
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

        // DataSet ds = dml.Find("select * from SET_UserGrp_Form where FormId='" + FormID + "' and DmlAllowed = 'Y'");
        //if (ds.Tables[0].Rows.Count > 0)
        // {
        //     if (ds.Tables[0].Rows[0]["Add"].ToString() == "N")
        //     {
        //         btnInsert.Visible = false;
        //     }
        //     if (ds.Tables[0].Rows[0]["Edit"].ToString() == "N")
        //     {
        //         btnEdit.Visible = false;
        //     }
        //     if (ds.Tables[0].Rows[0]["Delete"].ToString() == "N")
        //     {
        //         btnDelete.Visible = false;
        //     }
        // }
        // else
        // {
        //     btnInsert.Visible = false;
        //     btnEdit.Visible = false;
        //     btnDelete.Visible = false;
        //     btnFind.Visible = true;
        //     btnCancel.Visible = false;
        // }
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

        chkActive.Enabled = true;
        chkActive.Checked = true;
        txtEntry_Date.Enabled = true;
        txtFiscal_Year_Description.Enabled = true;
        txtFiscal_Year_End_Date.Enabled = true;
        txtFiscal_Year_Start_Date.Enabled = true;
        txtUser_Sorting_Order.Enabled = true;
        lblDatabase_Name.Enabled = true;
        lblFiscal_Year_Id.Enabled = true;
        lblSystem_Date.Enabled = true;
        lblUser_Name.Enabled = true;
        ddlFiscal_Year_Status_Name.Enabled = true;
       // ddlGOC_Name.Enabled = true;
        ddlPeriod_Description.Enabled = true;
        imgPopup.Enabled = true;
        ImageButton1.Enabled = true;
        ImageButton2.Enabled = true;


        txtEntry_Date.Enabled = true;
        lblSystem_Date.Text = DateTime.Now.ToString();
        ddlGOC_Name.ClearSelection();
        ddlGOC_Name.Items.FindByValue(gocid().ToString()).Selected = true;
        lblUser_Name.Text = show_username();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int chk= 0;
        try{
            if (chkActive.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }

            userid = Request.QueryString["UserID"];


            if (txtFiscal_Year_Description.Text != null)
            {
                string curyear = DateTime.Now.Year.ToString();

                string year1 = txtFiscal_Year_Description.Text.Substring(0, 4);

                
                string year2 = txtFiscal_Year_Description.Text.Substring(5, 4);
               

                if (int.Parse(curyear) < int.Parse(year1))
                {
                    Label1.ForeColor = System.Drawing.Color.Red;
                    Label1.Text = "This fiscal year does not created because this year exceed with the current year";
                }
                else
                {
                    string datecheck = dml.dateconvertforinsert(txtFiscal_Year_Start_Date);
                    DataSet ds = dml.Find("select StartDate from SET_Fiscal_Year where StartDate = '"+ datecheck + "' and Record_Deleted = 0");

                    if(ds.Tables[0].Rows.Count > 0)
                    {
                        Label1.ForeColor = System.Drawing.Color.Red;
                        Label1.Text = "This fical year already inserted";
                    }

                    dml.Insert("insert into SET_Fiscal_Year( Description,StartDate,EndDate,PeriodID,GOCid,FiscalYearStatusId,SortOrder,DbPath,IsActive,GUID,EntryDate ,EntryUsrId, SysDate,Record_Deleted,'MLD') values ('" + txtFiscal_Year_Description.Text + "','" + datecheck + "','" + dml.dateconvertforinsert(txtFiscal_Year_End_Date) + "'," + ddlPeriod_Description.SelectedValue + "," + ddlGOC_Name.SelectedValue + "," + ddlFiscal_Year_Status_Name.SelectedItem.Value + ",'" + txtUser_Sorting_Order.Text + "','" + lblDatabase_Name.Text + "'," + chk + ",'E189DEB2-034B-443F-9AA6-F5256BD0B93E','" + dml.dateconvertforinsert(txtEntry_Date) + "',NULL,'" + DateTime.Now + "',0,'"+dml.Encrypt("h")+"')", "alertme()");
                    dml.Update("update Set_Period set MLD = '" + dml.Encrypt("q") + "' where PeriodId = '" + ddlPeriod_Description.SelectedItem.Value + "'", "");
                    ClientScript.RegisterStartupScript(this.GetType(), "randomtext", " alertme()", true);
                    textClear();
                    btnInsert_Click(sender, e);
                }
            }
            

        }
        catch(Exception ex)
        {

        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try {
            int chk = 0;
            if (chkActive.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
            string sortorder;
            if (txtUser_Sorting_Order.Text == "")
            {
                sortorder = "([SortOrder] IS NULL)";
            }
            else
            {
                sortorder = "([SortOrder] = '" + txtUser_Sorting_Order.Text + "')";
                
                
            }
            userid = Request.QueryString["UserID"];
            string std = txtFiscal_Year_Start_Date.Text;
            string enddate = txtFiscal_Year_End_Date.Text;
            string entrydate = txtEntry_Date.Text;
            DataSet ds_up = dml.Find("select * from SET_Fiscal_Year  WHERE ([FiscalYearID]='"+lblFiscal_Year_Id.Text+"') AND ([Description]='"+txtFiscal_Year_Description.Text+"') AND ([StartDate]='"+dml.dateconvertforinsert(txtFiscal_Year_Start_Date)+"') AND ([EndDate]='"+dml.dateconvertforinsert(txtFiscal_Year_End_Date)+"') AND ([PeriodID]='"+ddlPeriod_Description.SelectedItem.Value+"') AND ([GOCid]='"+ddlGOC_Name.SelectedItem.Value+"') AND ([FiscalYearStatusId]='"+ddlFiscal_Year_Status_Name.SelectedItem.Value+"') AND "+ sortorder + " AND ([IsActive]='"+chk+"') AND ([EntryDate]='"+dml.dateconvertforinsert(txtEntry_Date)+"') AND ([Record_Deleted]='0')");

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
                dml.Update("update SET_Fiscal_Year set Description='" + txtFiscal_Year_Description.Text + "',	StartDate='" + dml.dateconvertString(std) + "',	EndDate='" + dml.dateconvertString(enddate) + "',	PeriodID='" + ddlPeriod_Description.SelectedValue + "',	GOCid='" + ddlGOC_Name.SelectedValue + "',	FiscalYearStatusId='" + ddlFiscal_Year_Status_Name.SelectedValue + "',	SortOrder='" + txtUser_Sorting_Order.Text + "',	DbPath='" + lblDatabase_Name.Text + "',	IsActive='" + chk + "',EntryDate='" + dml.dateconvertString(entrydate) + "',	EntryUsrId='" + userid + "',	SysDate='" + lblSystem_Date.Text + "' where FiscalYearID = " + lblFiscal_Year_Id.Text + "", "");
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
        catch(Exception ex)
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


        ddlPeriod_Description.SelectedIndex = 0;
        ddlGOC_Name.SelectedIndex = 0;
        ddlFiscal_Year_Status_Name.SelectedIndex = 0;
        txtEntry_Date.Enabled = false;
        txtFiscal_Year_Description.Enabled = false;
        txtFiscal_Year_End_Date.Enabled = false;
        txtFiscal_Year_Start_Date.Enabled = false;
        txtUser_Sorting_Order.Enabled = false;
        lblDatabase_Name.Enabled = false;
        lblFiscal_Year_Id.Enabled = false;
        lblSystem_Date.Enabled = false;
        lblUser_Name.Enabled = false;
        ddlFiscal_Year_Status_Name.Enabled = false;
        ddlGOC_Name.Enabled = false;
        ddlPeriod_Description.Enabled = false;
        Label1.Text = "";
        //ddlCompany_Name.Text = "Please Select Company";
        // textClear();
    }

    public void timespan()
    {
        //DateTime start = Convert.ToDateTime(txtStart_Date.Text);
        //DateTime End = Convert.ToDateTime(txtEnd_Date.Text);

        //TimeSpan span1 = End - start;

        //DateTime zeroTime = new DateTime(1, 1, 1);

        ////DateTime start, end;

        //int yearspan = (zeroTime + span1).Year - 1;


        ////DateTime a = new DateTime(DateTime.Parse((txtStart_Date.Text));
        ////DateTime b = new DateTime();

        //// TimeSpan span = End - start;
        ////// Because we start at year 1 for the Gregorian
        ////// calendar, we must subtract a year here.
        //// int years = (zeroTime + span).Year - 1;

        ////// 1, where my other algorithm resulted in 0.
        //lblNo_of_Fiscal_Years.Text = yearspan.ToString();


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
        string st = Request.QueryString.Get("fiscaly");
        string PeriodID = dbb_PeriodID(st);

        try {

            Label1.Text = "";
            string squer = "select * from SET_Fiscal_Year ";
            string swhere;

            if (txtDelete_FYID.SelectedIndex != 0)
            {
                swhere = "FiscalYearID = '" + txtDelete_FYID.SelectedItem.Value + "'";
            }
            else
            {
                swhere = "FiscalYearID is not null";
            }
            if (txtDelete_SD.Text != "")
            {
                swhere = swhere + " and StartDate = '" + txtDelete_SD.Text + "'";
            }
            else
            {
                swhere = swhere + " and StartDate is not null";
            }
            if (txtDelete_FYSN.SelectedIndex != 0)
            {
                swhere = swhere + " and FiscalYearStatusId = '" + txtDelete_FYSN.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and FiscalYearStatusId is not null";
            }


            if (chkDelete_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkDelete_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }
            else
            {
                swhere = swhere + " and IsActive is not null";
            }
            squer = squer + " where " + swhere + "and Record_Deleted = '0' and GOCid = '" + gocid() + "' and PeriodID = '" + PeriodID + "'";

            Deletebox.Visible = true;
            fieldbox.Visible = false;

            DataSet dgrid = dml.grid(squer);
            GridView2.DataSource = dgrid;
            GridView2.DataBind();
        }
        catch(Exception ex)
        {

        }
    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {

        btnInsert.Visible = false;
        btnCancel.Visible = true;
        btnUpdate.Visible = false;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnFind.Visible = true;
        btnDelete_after.Visible = false;
        btnSave.Visible = false;
       

        txtEntry_Date.Enabled = false;
        txtFiscal_Year_Description.Enabled = false;
        
        txtFiscal_Year_End_Date.Enabled = false;
        txtFiscal_Year_Start_Date.Enabled = false;
        txtUser_Sorting_Order.Enabled = false;
        lblDatabase_Name.Enabled = false;
        lblFiscal_Year_Id.Enabled = false;
        lblSystem_Date.Enabled = false;
        lblUser_Name.Enabled = false;
        ddlFiscal_Year_Status_Name.Enabled = false;
        ddlGOC_Name.Enabled = false;
        ddlPeriod_Description.Enabled = false;

        string st = Request.QueryString.Get("fiscaly");
        string PeriodID = dbb_PeriodID(st);

        Label1.Text = "";
        string squer = "select * from SET_Fiscal_Year ";
        string swhere;

        if (txtFind_FYid.SelectedIndex != 0)
        {
            swhere = "FiscalYearID = '" + txtFind_FYid.SelectedItem.Value + "'";
        }
        else
        {
            swhere = "FiscalYearID is not null";
        }
        if (txtFind_SD.Text != "")
        {
            swhere = swhere + " and StartDate = '" + txtFind_SD.Text + "'";
        }
        else
        {
            swhere = swhere + " and StartDate is not null";
        }
        if (txtFind_FYsn.SelectedIndex != 0)
        {
            swhere = swhere + " and FiscalYearStatusId = '" + txtFind_FYsn.SelectedItem.Value + "'";
        }
        else
        {
            swhere = swhere + " and FiscalYearStatusId is not null";
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
        squer = squer + " where " + swhere + "and Record_Deleted = '0' and GOCid = '" + gocid() + "' and PeriodID = '" + PeriodID + "'";  

        Findbox.Visible = true;
        fieldbox.Visible = false;

        DataSet dgrid = dml.grid(squer);
        GridView1.DataSource = dgrid;
        GridView1.DataBind();

        DataSet ds_search =  dml.Find(squer);
        
        if (ds_search.Tables[0].Rows.Count > 0)
        {

           // ,,,,,,,,IsActive,GUID, ,EntryUsrId, 
            txtEntry_Date.Text = ds_search.Tables[0].Rows[0]["EntryDate"].ToString();
            txtFiscal_Year_Description.Text = ds_search.Tables[0].Rows[0]["Description"].ToString();
           // ,,,PeriodID,GOCid,FiscalYearStatusId,,DbPath,IsActive,GUID,EntryDate ,EntryUsrId, SysDate
            txtFiscal_Year_End_Date.Text = ds_search.Tables[0].Rows[0]["EndDate"].ToString();
            txtFiscal_Year_Start_Date.Text = ds_search.Tables[0].Rows[0]["StartDate"].ToString();
            txtUser_Sorting_Order.Text = ds_search.Tables[0].Rows[0]["SortOrder"].ToString();
            lblDatabase_Name.Text = ds_search.Tables[0].Rows[0]["DbPath"].ToString();
            lblFiscal_Year_Id.Text = ds_search.Tables[0].Rows[0]["FiscalYearID"].ToString();
            lblSystem_Date.Text = ds_search.Tables[0].Rows[0]["SysDate"].ToString();
            lblUser_Name.Text = show_username();
            ddlFiscal_Year_Status_Name.SelectedValue = ds_search.Tables[0].Rows[0]["FiscalYearStatusId"].ToString();
            ddlGOC_Name.SelectedValue = ds_search.Tables[0].Rows[0]["GOCid"].ToString();
            ddlPeriod_Description.SelectedValue = ds_search.Tables[0].Rows[0]["PeriodID"].ToString();
            dml.dateConvert(txtFiscal_Year_Start_Date);
            dml.dateConvert(txtFiscal_Year_End_Date);
            dml.dateConvert(txtEntry_Date);
            dml.dateConvert(lblSystem_Date);
            if (ds_search.Tables[0].Rows[0]["IsActive"].ToString() == "0")
            {
                chkActive.Checked = false;
            }
            else
            {
                chkActive.Checked = true;
            }

        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", " nodatafound()", true);
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
        string st = Request.QueryString.Get("fiscaly");
        string PeriodID = dbb_PeriodID(st);
        try {

            ddlPeriod_Description.SelectedIndex = 0;
            ddlGOC_Name.SelectedIndex = 0;
            ddlFiscal_Year_Status_Name.SelectedIndex = 0;
            txtEntry_Date.Enabled = true;
            txtFiscal_Year_Description.Enabled = true;
            txtFiscal_Year_End_Date.Enabled = true;
            txtFiscal_Year_Start_Date.Enabled = true;
            txtUser_Sorting_Order.Enabled = true;
            lblDatabase_Name.Enabled = true;
            lblFiscal_Year_Id.Enabled = true;
            lblSystem_Date.Enabled = true;
            lblUser_Name.Enabled = true;
            ddlFiscal_Year_Status_Name.Enabled = true;
            ddlGOC_Name.Enabled = true;
            ddlPeriod_Description.Enabled = true;

            string squer = "select * from SET_Fiscal_Year ";
            string swhere;

            if (txtEdit_FYID.SelectedIndex != 0)
            {
                swhere = "FiscalYearID = '" + txtEdit_FYID.SelectedItem.Value + "'";
            }
            else
            {
                swhere = "FiscalYearID is not null";
            }
            if (txtEdit_STartDAte.Text != "")
            {
                swhere = swhere + " and StartDate = '" + txtEdit_STartDAte.Text + "'";
            }
            else
            {
                swhere = swhere + " and StartDate is not null";
            }
            if (txtEdit_FYSN.SelectedIndex != 0)
            {
                swhere = swhere + " and FiscalYearStatusId = '" + txtEdit_FYSN.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and FiscalYearStatusId is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = '0' and GOCid = '"+gocid()+"' and PeriodID = '"+ PeriodID + "'";

            Findbox.Visible = false;
            Deletebox.Visible = false;
            Editbox.Visible = true;
            fieldbox.Visible = false;

            DataSet dgrid = dml.grid(squer);
            GridView3.DataSource = dgrid;
            GridView3.DataBind();
        }
        catch(Exception ex)
        {

        }


    }

    public string periodoverlap(string oldStart, string oldEnd, string newStart, string newEnd)
    {
        DateTime DateA_Start = Convert.ToDateTime(oldStart); // Aug 10, 2007
        DateTime DateA_End = Convert.ToDateTime(oldEnd);  // Sept 10, 2007

        // string d1= Convert.ToDateTime(newStart).ToLongDateString();
        //string d2 = Convert.ToDateTime(newEnd).ToLongDateString();

        // Period B (Start & End date)
        DateTime DateB_Start = Convert.ToDateTime(newStart); // Sept 1, 2007


        DateTime DateB_End = Convert.ToDateTime(newEnd);  // Sept 1, 2007  // Dec 1, 2007
                                                          //  DateB_End.ToString("MM/dd/yyyy");

        // Test if the given periods overlap
        if (TimePeriodOverlap(Convert.ToDateTime(DateA_Start.ToShortDateString()), Convert.ToDateTime(DateA_End.ToShortDateString()), DateB_Start, DateB_End))
        {
            // This will be the result for the example values
            string str = "Date period overlap!";
            return str;
        }
        else {
            // Label2.Text = "Everything is okay!";
            string str = "Everything is okay!!";
            return str;
        }
    }

    public bool TimePeriodOverlap(DateTime BS, DateTime BE, DateTime TS, DateTime TE)
    {
        return ((TS >= BS && TS < BE) || (TE <= BE && TE > BS) || (TS <= BS && TE >= BE));
    }

    public void textClear()
    {
        Findbox.Visible = false;
        fieldbox.Visible = true;
        txtEntry_Date.Text = "";
        txtFiscal_Year_Description.Text = "";
     
        txtFiscal_Year_End_Date.Text = "";
        txtFiscal_Year_Start_Date.Text = "";
        txtUser_Sorting_Order.Text = "";
        lblDatabase_Name.Text = "";
        lblFiscal_Year_Id.Text = "";
        lblSystem_Date.Text = "";
        lblUser_Name.Text = "";
        ddlFiscal_Year_Status_Name.SelectedIndex = 0;
        ddlGOC_Name.SelectedIndex = 0;
        ddlPeriod_Description.SelectedIndex = 0;
        chkActive.Checked = false;

        chkActive.Enabled = false;
        ddlPeriod_Description.SelectedIndex = 0;
        ddlGOC_Name.SelectedIndex = 0;
        ddlFiscal_Year_Status_Name.SelectedIndex = 0;
        txtEntry_Date.Enabled = false;
        txtFiscal_Year_Description.Enabled = false;
        txtFiscal_Year_End_Date.Enabled = false;
        txtFiscal_Year_Start_Date.Enabled = false;
        txtUser_Sorting_Order.Enabled = false;
        lblDatabase_Name.Enabled = false;
        lblFiscal_Year_Id.Enabled = false;
        lblSystem_Date.Enabled = false;
        lblUser_Name.Enabled = false;
        ddlFiscal_Year_Status_Name.Enabled = false;
        ddlGOC_Name.Enabled = false;
        ddlPeriod_Description.Enabled = false;
        imgPopup.Enabled = false;
        ImageButton1.Enabled = false;
        ImageButton2.Enabled = false;


    }


    protected void ddlPeriod_Description_SelectedIndexChanged(object sender, EventArgs e)
    {
        try {
            if (ddlPeriod_Description.SelectedIndex != 0)
            {

                lblDatabase_Name.Text = PeriodSet.Tables[0].Rows[0]["DbPath"].ToString();
                txtFiscal_Year_Description_TextChanged(sender, e);
            }
            else
            {
                lblDatabase_Name.Text = "";
            }
        }
        catch(Exception ex)
        {

        }
    }
    public string show_username()
    {
        userid = Request.QueryString ["UserID"];
        DataSet ds_autoUserName = dml.Find("select user_name from SET_User_Manager where UserId='" + userid + "'");
        return ds_autoUserName.Tables [0].Rows [0]["user_name"].ToString();

    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnInsert.Visible = false;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        Label1.Text = "";
        chkActive.Enabled = false;

        fieldbox.Visible = true;
        Findbox.Visible = false;
        textClear();
        try
        {

            ddlFiscal_Year_Status_Name.ClearSelection();
            ddlGOC_Name.ClearSelection();
            ddlPeriod_Description.ClearSelection();
            string serial_id;
            serial_id = GridView1.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;

            DataSet ds = dml.Find("select * from SET_Fiscal_Year WHERE ([FiscalYearID]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {

                txtEntry_Date.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtFiscal_Year_Description.Text = ds.Tables[0].Rows[0]["Description"].ToString();

                txtFiscal_Year_End_Date.Text = ds.Tables[0].Rows[0]["EndDate"].ToString();
                txtFiscal_Year_Start_Date.Text = ds.Tables[0].Rows[0]["StartDate"].ToString();
                txtUser_Sorting_Order.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();
                lblDatabase_Name.Text = ds.Tables[0].Rows[0]["DbPath"].ToString();
                lblFiscal_Year_Id.Text = ds.Tables[0].Rows[0]["FiscalYearID"].ToString();
                lblSystem_Date.Text = ds.Tables[0].Rows[0]["SysDate"].ToString();
                //lblUser_Name.Text = show_username())ds.Tables[0].Rows[0]["BankAccountName"].ToString();

                ddlFiscal_Year_Status_Name.Items.FindByValue(ds.Tables[0].Rows[0]["FiscalYearStatusId"].ToString()).Selected = true;
                ddlGOC_Name.Items.FindByValue(ds.Tables[0].Rows[0]["GOCid"].ToString()).Selected = true;
                ddlPeriod_Description.Items.FindByValue(ds.Tables[0].Rows[0]["PeriodID"].ToString()).Selected = true;

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                dml.dateConvert(txtFiscal_Year_End_Date);
                dml.dateConvert(txtFiscal_Year_Start_Date);
                dml.dateConvert(txtEntry_Date);

                DataSet dsGrid = dml.Find("select USER_NAME from SET_USer_Manager where Userid  = '" + ds.Tables[0].Rows[0]["EntryUsrId"].ToString() + "'");
                lblUser_Name.Text = dsGrid.Tables[0].Rows[0]["USER_NAME"].ToString();

            }


        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            dml.Delete("update SET_Fiscal_Year set Record_Deleted = 1 where FiscalYearID = " + ViewState["SNO"].ToString() + "", "");
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

       
        txtEntry_Date.Enabled = true;
        txtFiscal_Year_Description.Enabled = true;
        txtFiscal_Year_End_Date.Enabled = true;
        txtFiscal_Year_Start_Date.Enabled = true;
        txtUser_Sorting_Order.Enabled = true;
        lblDatabase_Name.Enabled = true;
        lblFiscal_Year_Id.Enabled = true;
        lblSystem_Date.Enabled = true;
        lblUser_Name.Enabled = true;
        ddlFiscal_Year_Status_Name.Enabled = true;
        ddlGOC_Name.Enabled = true;
        ddlPeriod_Description.Enabled = true;
        chkActive.Enabled = true;
        imgPopup.Enabled = true;
        ImageButton1.Enabled = true;
        ImageButton2.Enabled = true;

        txtEntry_Date.Enabled = true;

        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
    

        try {

            ddlFiscal_Year_Status_Name.ClearSelection();
            ddlGOC_Name.ClearSelection();
            ddlPeriod_Description.ClearSelection();
            string serial_id;
            serial_id = GridView3.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;

            DataSet ds = dml.Find("select * from SET_Fiscal_Year WHERE ([FiscalYearID]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {

                txtEntry_Date.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtFiscal_Year_Description.Text = ds.Tables[0].Rows[0]["Description"].ToString();

                txtFiscal_Year_End_Date.Text = ds.Tables[0].Rows[0]["EndDate"].ToString();
                txtFiscal_Year_Start_Date.Text = ds.Tables[0].Rows[0]["StartDate"].ToString();
                txtUser_Sorting_Order.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();
                lblDatabase_Name.Text = ds.Tables[0].Rows[0]["DbPath"].ToString();
                lblFiscal_Year_Id.Text = ds.Tables[0].Rows[0]["FiscalYearID"].ToString();
                lblSystem_Date.Text = ds.Tables[0].Rows[0]["SysDate"].ToString();
                //lblUser_Name.Text = show_username())ds.Tables[0].Rows[0]["BankAccountName"].ToString();

                ddlFiscal_Year_Status_Name.Items.FindByValue(ds.Tables[0].Rows[0]["FiscalYearStatusId"].ToString()).Selected = true;
                ddlGOC_Name.Items.FindByValue(ds.Tables[0].Rows[0]["GOCid"].ToString()).Selected = true;
                ddlPeriod_Description.Items.FindByValue(ds.Tables[0].Rows[0]["PeriodID"].ToString()).Selected = true ;

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                dml.dateConvert(txtFiscal_Year_End_Date);
                dml.dateConvert(txtFiscal_Year_Start_Date);
                dml.dateConvert(txtEntry_Date);

                DataSet dsGrid = dml.Find("select USER_NAME from SET_USer_Manager where Userid  = '" + ds.Tables[0].Rows[0]["EntryUsrId"].ToString() + "'");
                lblUser_Name.Text = dsGrid.Tables[0].Rows[0]["USER_NAME"].ToString();

            }


        }
        catch(Exception ex)
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

            ddlFiscal_Year_Status_Name.ClearSelection();
            ddlGOC_Name.ClearSelection();
            ddlPeriod_Description.ClearSelection();
            string serial_id;
            serial_id = GridView2.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;

            DataSet ds = dml.Find("select * from SET_Fiscal_Year WHERE ([FiscalYearID]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {

                txtEntry_Date.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtFiscal_Year_Description.Text = ds.Tables[0].Rows[0]["Description"].ToString();

                txtFiscal_Year_End_Date.Text = ds.Tables[0].Rows[0]["EndDate"].ToString();
                txtFiscal_Year_Start_Date.Text = ds.Tables[0].Rows[0]["StartDate"].ToString();
                txtUser_Sorting_Order.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();
                lblDatabase_Name.Text = ds.Tables[0].Rows[0]["DbPath"].ToString();
                lblFiscal_Year_Id.Text = ds.Tables[0].Rows[0]["FiscalYearID"].ToString();
                lblSystem_Date.Text = ds.Tables[0].Rows[0]["SysDate"].ToString();
                //lblUser_Name.Text = show_username())ds.Tables[0].Rows[0]["BankAccountName"].ToString();

                ddlFiscal_Year_Status_Name.Items.FindByValue(ds.Tables[0].Rows[0]["FiscalYearStatusId"].ToString()).Selected = true;
                ddlGOC_Name.Items.FindByValue(ds.Tables[0].Rows[0]["GOCid"].ToString()).Selected = true;
                ddlPeriod_Description.Items.FindByValue(ds.Tables[0].Rows[0]["PeriodID"].ToString()).Selected = true;

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                dml.dateConvert(txtFiscal_Year_End_Date);
                dml.dateConvert(txtFiscal_Year_Start_Date);
                dml.dateConvert(txtEntry_Date);

                DataSet dsGrid = dml.Find("select USER_NAME from SET_USer_Manager where Userid  = '" + ds.Tables[0].Rows[0]["EntryUsrId"].ToString() + "'");
                lblUser_Name.Text = dsGrid.Tables[0].Rows[0]["USER_NAME"].ToString();

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

    protected void txtFiscal_Year_Description_TextChanged(object sender, EventArgs e)
    {
        if (txtFiscal_Year_Description.Text != null)
        {
            var test = ddlPeriod_Description.SelectedItem.Value;
            if (ddlPeriod_Description.SelectedItem.Value != "Please select...")
            {
                var id = Convert.ToInt32(ddlPeriod_Description.SelectedItem.Value);
                DataSet PeriodSet = dml.Find("Select * From Set_Period where PeriodId=" + Convert.ToInt32(ddlPeriod_Description.SelectedItem.Value));
                if (PeriodSet.Tables[0].Rows.Count > 0)
                {


                    DateTime PeriodStartDate = Convert.ToDateTime(PeriodSet.Tables[0].Rows[0]["StartDate"].ToString());
                    DateTime PeriodEndDate = Convert.ToDateTime(PeriodSet.Tables[0].Rows[0]["EndDate"].ToString());

                    string year1 = txtFiscal_Year_Description.Text.Substring(0, 4);
                    DateTime FiscalStartingDate = Convert.ToDateTime("01-07-" + year1);

                    string year2 = txtFiscal_Year_Description.Text.Substring(5, 4);
                    DateTime FiscalEndingDate = Convert.ToDateTime("30-06-" + year2);


                    if (FiscalStartingDate >= PeriodStartDate && FiscalEndingDate <= PeriodEndDate)
                    {
                        CalendarExtender2.SelectedDate = DateTime.Parse("01-07-" + year1 + "");
                        CalendarExtender2.StartDate = CalendarExtender2.SelectedDate;


                        CalendarExtender3.SelectedDate = DateTime.Parse("30-06-" + year2 + "");
                        CalendarExtender3.StartDate = CalendarExtender3.SelectedDate;
                        btnSave.Enabled = true;
                        Label1.Text = "";
                    }
                    else
                    {

                        Label1.Text = "Fiscal Year Must Be in Between selected Period TimeLine";
                        btnSave.Enabled = false;
                    }
                }

            }
        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string id = e.Row.Cells[1].Text;
        bool flag = dml.isNumber(id);


        if (flag == true)
        {

            DataSet ds = dml.Find("select FiscalYearID,MLD from SET_Fiscal_Year where FiscalYearID = '" + id + "'");
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

            DataSet ds = dml.Find("select FiscalYearID,MLD from SET_Fiscal_Year where FiscalYearID = '" + id + "'");
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
    public string dbb_PeriodID(string fiscal_year)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
        SqlDataAdapter da = new SqlDataAdapter("SELECT Description,PeriodID from SET_Period where PeriodID = (SELECT PeriodID from SET_Fiscal_Year WHERE Description = '" + fiscal_year + "') and EntryUserId = '6B9C1166-0F4B-41DC-99E8-B47BE96C8157'", conn);
        DataSet ds = new DataSet();
        da.Fill(ds);

        string db_period = ds.Tables[0].Rows[0]["PeriodID"].ToString();
        return db_period;
    }

}