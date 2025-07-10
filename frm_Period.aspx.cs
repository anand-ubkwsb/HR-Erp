using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frm_Period : System.Web.UI.Page
{
    int DeleteDays, AddDays, EditDays, DateFrom;
    string userid, UserGrpID, FormID;
    DmlOperation dml = new DmlOperation();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
    FieldHide fd = new FieldHide();
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
            CalendarExtender3.EndDate = DateTime.Now;

            txtEntry_Date.Attributes.Add("readonly", "readonly");
            txtStart_Date.Attributes.Add("readonly", "readonly");
            txtEnd_Date.Attributes.Add("readonly", "readonly");

            fd.HideUSerGrpField(Page.Controls, UserGrpID, FormID, "SET_Period");
            Showall_Dml();
            textClear();

            Findbox.Visible = false;
            DeleteBox.Visible = false;
            Editbox.Visible = false;
            dml.dropdownsql(ddlCompany_Name, "SET_Company", "CompName", "CompId");

            dml.dropdownsql(txtEdit_PDID, "SET_Period", "Description", "PeriodID");
            dml.dropdownsql(txtdelete_PeriodID, "SET_Period", "Description", "PeriodID");
            dml.dropdownsql(txtUserIdEdit, "SET_Period", "Description", "PeriodID");

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

        txtStart_Date.Enabled = true;
        txtEnd_Date.Enabled = true;
        txtPeriod_Description.Enabled = true;
        txtEntry_Date.Enabled = true;
        lblSystem_Date.Text = DateTime.Now.ToString();


        txtEntry_Date.Enabled = true;
        txtPeriod_Description.Enabled = true;
        txtStart_Date.Enabled = true;
        txtEnd_Date.Enabled = true;
        lblDb_Name.Enabled = true;
        lblNo_of_Fiscal_Years.Enabled = true;
        lblPeriod_Id.Enabled = true;
        lblSystem_Date.Enabled = true;
        lblUser_Name.Enabled = true;
        chkActive_Status.Enabled = true;
        Label1.Enabled = true;
        ddlCompany_Name.Enabled = true;
        chkActive_Status.Checked= true;
        imgPopup.Enabled = true;
        ImageButton1.Enabled = true;
        ImageButton2.Enabled = true;


        userid = Request.QueryString["UserID"];
        DataSet ds_autoUserName = dml.Find("select user_name from SET_User_Manager where UserId='" + userid + "'");
        lblUser_Name.Text = ds_autoUserName.Tables[0].Rows[0]["user_name"].ToString();


        string dbname = "";
        DataSet DB_ds = dml.Find("select convert(varchar, (SELECT TOP(1)DbPath  as dbname FROM SET_Period )) + ' ' + convert(varchar, (SELECT  COUNT(DbPath) as count  FROM SET_Period)) as dbcount");
        if (DB_ds.Tables[0].Rows.Count > 0)
        {
            dbname = DB_ds.Tables[0].Rows[0]["dbcount"].ToString();
        }
        else
        {
            dbname = "HRERP";
        }
        lblDb_Name.Text = dbname;
       
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try {
            int chk;
            if (chkActive_Status.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
            string dbname = "";
            userid = Request.QueryString["UserID"];
            DataSet DB_ds = dml.Find("select convert(varchar, (SELECT TOP(1)DbPath  as dbname FROM SET_Period )) + ' ' + convert(varchar, (SELECT  COUNT(DbPath) as count  FROM SET_Period)) as dbcount");
            if (DB_ds.Tables[0].Rows.Count > 0)
            {
                dbname = DB_ds.Tables[0].Rows[0]["dbcount"].ToString();
            }
            else
            {
                dbname = "HRERP";
            }

            string st = dml.dateconvertforinsert(txtStart_Date);
            string ed = dml.dateconvertforinsert(txtEnd_Date);
            string entryD = dml.dateconvertforinsert(txtEntry_Date);
            string sysD = dml.dateconvertforinsert(lblSystem_Date);
            if (Label1.Text == "Everything is okay!!")
            {
                dml.Insert("Insert into SET_Period (Description,StartDate,EndDate,PeriodYearsSpan,DbPath,EntryDate,SysDate,EntryUserId,IsActive,Compid,GUID,MLD) values ('" + txtPeriod_Description.Text + "', '" + st + "', '" + ed + "', " + lblNo_of_Fiscal_Years.Text + ", '" + dbname + "', '" + entryD + "', '" + sysD + "', '" + userid + "', " + chk + ", " + ddlCompany_Name.SelectedItem.Value + ", 'A452FD2F-E6B4-4D5F-BEE9-DF5F23B99A59','"+dml.Encrypt("h")+"')", "alertme()");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);
                btnInsert_Click(sender, e);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "periodoverlap()", true);
            }
        }
        catch(Exception ex)
        {
            Label1.Text = ex.Message;
        }

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            int chk = 0;
            if (chkActive_Status.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
            userid = Request.QueryString["UserID"];

            string sd = dml.dateconvertforinsert(txtStart_Date);
            string ed = dml.dateconvertforinsert(txtEnd_Date);
            string end = dml.dateconvertforinsert(txtEntry_Date);
            DataSet ds_up = dml.Find("select * from SET_Period WHERE ([PeriodID]='" + ViewState["SNO"].ToString() +"') AND ([Description]='"+txtPeriod_Description.Text+"') AND ([StartDate]='"+sd+"') AND ([EndDate]='"+ed+"') AND ([PeriodYearsSpan]='"+lblNo_of_Fiscal_Years.Text+"') AND ([DbPath]='"+lblDb_Name.Text+"') AND ([EntryDate]='"+end+"')  AND ([IsActive]='"+chk+"') AND ([Compid]='"+ddlCompany_Name.SelectedItem.Value+"')");

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


                userid = Request.QueryString["UserID"];
                dml.Update("update SET_Period set 	Description='" + txtPeriod_Description.Text + "',	StartDate='" + sd+ "',	EndDate	='" + ed + "',PeriodYearsSpan='" + lblNo_of_Fiscal_Years.Text + "',	DbPath='" + lblDb_Name.Text + "',EntryDate='" + end + "',	SysDate='" + DateTime.Now + "',	EntryUserId='" + userid + "',	IsActive='" + chk + "',	Compid='" + ddlCompany_Name.SelectedItem.Value + "' where PeriodID = '" + ViewState["SNO"].ToString() + "'", "");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", " Editalert()", true);


                ddlCompany_Name.SelectedIndex = 0;

                //ddlCompany_Name.Text = "Please Select Company";
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
            Label1.Text = ex.Message;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnInsert.Visible = true;
        btnDelete.Visible = true;
        btnUpdate.Visible = false;
        btnEdit.Visible = true;
        btnFind.Visible = true;
        btnSave.Visible = false;
        btnDelete_after.Visible = false;

        ddlCompany_Name.SelectedIndex = 0;
       
        //ddlCompany_Name.Text = "Please Select Company";
        textClear();
    }

    protected void txtEnd_Date_TextChanged(object sender, EventArgs e)
    {
        if (txtStart_Date.Text == "" || txtEnd_Date.Text == "")
        {


        }

        else {

            DataSet dsst_en_date = dml.Find("select StartDate ,EndDate from SET_Period");
            int count = dsst_en_date.Tables[0].Rows.Count;

            for (int i = 0; i <= count - 1; i++)
            {
                string overlap = periodoverlap(dsst_en_date.Tables[0].Rows[i]["StartDate"].ToString(), dsst_en_date.Tables[0].Rows[i]["EndDate"].ToString(), txtStart_Date.Text, txtEnd_Date.Text);
                Label1.Text = overlap;
            }

        }
        DateTime st = Convert.ToDateTime(txtStart_Date.Text);
        DateTime end = Convert.ToDateTime(txtEnd_Date.Text);
        int span = end.Year - st.Year;
        lblNo_of_Fiscal_Years.Text = span.ToString();
    }
    public void timespan() {

        DateTime start = Convert.ToDateTime(txtStart_Date.Text);
        DateTime End = Convert.ToDateTime(txtEnd_Date.Text);

        TimeSpan span1 = End - start;

        DateTime zeroTime = new DateTime(1, 1, 1);

        //DateTime start, end;

        int yearspan = (zeroTime + span1).Year - 1;


        //DateTime a = new DateTime(DateTime.Parse((txtStart_Date.Text));
        //DateTime b = new DateTime();

       // TimeSpan span = End - start;
        //// Because we start at year 1 for the Gregorian
        //// calendar, we must subtract a year here.
       // int years = (zeroTime + span).Year - 1;

        //// 1, where my other algorithm resulted in 0.
        lblNo_of_Fiscal_Years.Text = yearspan.ToString();


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
            string squer = "select * from SET_Period ";
            string swhere;

            if (txtdelete_PeriodID.SelectedIndex != 0)
            {
                swhere = "PeriodID = " + txtdelete_PeriodID.SelectedItem.Value;
            }
            else
            {
                swhere = "PeriodID is not null";
            }
            if (txtdelete_StartDate.Text != "")
            {
                swhere = swhere + " and StartDate = '" + txtdelete_StartDate.Text + "'";
            }
            else
            {
                swhere = swhere + " and StartDate is not null";
            }
            if (chkdelete_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkdelete_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }
            else
            {
                swhere = swhere + " and IsActive is not null";
            }
            squer = squer + " where " + swhere + " and Record_Deleted = 0 and CompId = '" + compid() + "'";

            Findbox.Visible = false;
            fieldbox.Visible = false;
            Editbox.Visible = false;

            DataSet dgrid = dml.grid(squer);
            GridView2.DataSource = dgrid;
            GridView2.DataBind();
        }
        catch (Exception ex)
        {

        }
           
        }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        ddlCompany_Name.Enabled = false;
        txtEntry_Date.Enabled = false;
        txtPeriod_Description.Enabled = false;
        txtStart_Date.Enabled = false;
        txtEnd_Date.Enabled = false;
        lblDb_Name.Enabled = false;
        lblNo_of_Fiscal_Years.Enabled = false;
        lblPeriod_Id.Enabled = false;
        lblSystem_Date.Enabled = false;
        lblUser_Name.Enabled = false;
        chkActive_Status.Enabled = false;

        btnCancel.Visible = true;
        btnFind.Visible = true;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnInsert.Visible = false;
      
        try
        {
            GridView1.DataBind();
            string squer = "select * from SET_Period ";
        string swhere;

            if (txtUserIdEdit.SelectedIndex != 0)
            {
                swhere = "PeriodID = " + txtUserIdEdit.SelectedItem.Value;
            }
            else
            {
                swhere = "PeriodID is not null";
            }
            if (txtSD.Text != "")
        {
            swhere = swhere + " and StartDate = '" + txtSD.Text +"'";
        }
        else
        {
            swhere = swhere + " and StartDate is not null";
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
        squer = squer + " where " + swhere + " and Record_Deleted = 0 and CompId = '" + compid() + "'";

            Findbox.Visible = true;
            fieldbox.Visible = false;

        DataSet dgrid = dml.grid(squer);
        GridView1.DataSource = dgrid;
        GridView1.DataBind();
       

        
        DataSet ds = dml.Find(squer);
        if(ds.Tables[0].Rows.Count> 0)
        {
            //DataSet dscomp = dml.Find("select CompName from SET_Company where CompId = "+ ds.Tables[0].Rows[0]["Compid"].ToString() +"");
            DataSet dsUsername = dml.Find("select USER_NAME from SET_User_Manager where UserId='"+ ds.Tables[0].Rows[0]["EntryUserID"].ToString()+"'");


            //ddlCompany_Name.DataTextField = dscomp.Tables[0].Rows[0]["CompName"].ToString();

            ddlCompany_Name.SelectedItem.Value = ds.Tables[0].Rows[0]["compid"].ToString();
            txtEntry_Date.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
            txtPeriod_Description.Text = ds.Tables[0].Rows[0]["Description"].ToString();
            txtStart_Date.Text = ds.Tables[0].Rows[0]["StartDate"].ToString();
            txtEnd_Date.Text = ds.Tables[0].Rows[0]["EndDate"].ToString();
            lblDb_Name.Text = ds.Tables[0].Rows[0]["DbPath"].ToString();
            lblNo_of_Fiscal_Years.Text = ds.Tables[0].Rows[0]["PeriodYearsSpan"].ToString();
            //lblPeriod_Id.Text = ds.Tables[0].Rows[0]["PeriodID"].ToString();
            lblSystem_Date.Text = ds.Tables[0].Rows[0]["SysDate"].ToString();
            lblUser_Name.Text = dsUsername.Tables[0].Rows[0]["USER_NAME"].ToString();
            
            dml.dateConvert(txtEnd_Date);
            dml.dateConvert(txtStart_Date);
            dml.dateConvert(txtEntry_Date);
           

            if (ds.Tables[0].Rows[0]["IsActive"].ToString() == "True")
            {
                chkActive_Status.Checked = true;
            }
            else
            {
                chkActive_Status.Checked = false;
            }
        }
        else
        {
            textClear();
                //Label1.ForeColor = System.Drawing.Color.Red;
                //Label1.Text = "No data found";
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", " nodatafound()", true);
            }
        }
        catch (Exception ex)
        {
            
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", " wrong()", true);
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
        btnDelete_after.Visible = false ;

        try
        {
            GridView3.DataBind();
            string squer = "select * from SET_Period ";
            string swhere;

            if (txtEdit_PDID.SelectedIndex != 0)
            {
                swhere = "PeriodID = " + txtEdit_PDID.SelectedItem.Value;
            }
            else
            {
                swhere = "PeriodID is not null";
            }
            if (txtEdit_SD.Text != "")
            {
                swhere = swhere + " and StartDate = '" + txtEdit_SD.Text + "'";
            }
            else
            {
                swhere = swhere + " and StartDate is not null";
            }
            if (chkEdit_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkEdit_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }
            else
            {
                swhere = swhere + " and IsActive is not null";
            }
            squer = squer + " where " + swhere + " and Record_Deleted = 0 and CompId = '" + compid() + "'";

            Findbox.Visible = false;
            fieldbox.Visible = false;
            DeleteBox.Visible = false;

            DataSet dgrid = dml.grid(squer);
            if (dgrid.Tables[0].Rows.Count > 0)
            {
                GridView3.DataSource = dgrid;
                GridView3.DataBind();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", " nodatafound()", true);
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", " wrong()", true);
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
            string str =  "Date period overlap!";
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
        DeleteBox.Visible = false;
        Editbox.Visible = false;
        Findbox.Visible = false;
        fieldbox.Visible = true;
        txtEntry_Date.Text = "";
        txtPeriod_Description.Text = "";
        txtStart_Date.Text = "";
        txtEnd_Date.Text = "";
        lblDb_Name.Text = "";
        lblNo_of_Fiscal_Years.Text = "";
        lblPeriod_Id.Text = "";
        lblSystem_Date.Text = "";
        lblUser_Name.Text = "";
        chkActive_Status.Checked = false;
        Label1.Text = "";

        txtEntry_Date.Enabled = false;
        txtPeriod_Description.Enabled = false;
        txtStart_Date.Enabled = false;
        txtEnd_Date.Enabled = false;
        lblDb_Name.Enabled = false;
        lblNo_of_Fiscal_Years.Enabled = false;
        lblPeriod_Id.Enabled = false;
        lblSystem_Date.Enabled = false;
        lblUser_Name.Enabled = false;
        chkActive_Status.Enabled = false;
        Label1.Enabled = false;
        ddlCompany_Name.Enabled = false;
        imgPopup.Enabled = false;
        ImageButton1.Enabled = false;
        ImageButton2.Enabled = false;



    }
    public void Showall_Dml()
    {
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
        foreach (GridViewRow grow in GridView2.Rows)
        {
            CheckBox chk_del = (CheckBox)grow.FindControl("chkDeletegd");

            if (chk_del.Checked)
            {
                int periodID = Convert.ToInt32(GridView2.DataKeys[grow.RowIndex].Value);
                dml.Delete("delete SET_Period where PeriodID = " + periodID, "");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "alertDelete()", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "fillrequird()", true);
                GridView2.DataBind();
            }
        }
        GridView2.DataBind();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnUpdate.Visible = false;
        btnInsert.Visible = false;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        Label1.Text = "";

        
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {


            string serial_id;
            serial_id = GridView1.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select * from SET_Period WHERE ([PeriodID]='"+serial_id+"') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCompany_Name.ClearSelection();
                //[PeriodID], [Description], [StartDate], [EndDate], [PeriodYearsSpan], [DbPath], [GUID],
                //[EntryDate], [SysDate], [EntryUserId], [IsActive], [Compid], [Record_Deleted]) 
                txtPeriod_Description.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                txtStart_Date.Text = ds.Tables[0].Rows[0]["StartDate"].ToString();
                txtEnd_Date.Text = ds.Tables[0].Rows[0]["EndDate"].ToString();
                lblNo_of_Fiscal_Years.Text = ds.Tables[0].Rows[0]["PeriodYearsSpan"].ToString();
                lblDb_Name.Text = ds.Tables[0].Rows[0]["DbPath"].ToString();
                txtEntry_Date.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                lblSystem_Date.Text = ds.Tables[0].Rows[0]["SysDate"].ToString();
                string user = ds.Tables[0].Rows[0]["EntryUserId"].ToString();
                ddlCompany_Name.Items.FindByValue(ds.Tables[0].Rows[0]["Compid"].ToString()).Selected = true;
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                if (chk == true)
                {
                    chkActive_Status.Checked = true;
                }
                else
                {
                    chkActive_Status.Checked = false;
                }

                DataSet ds_user = dml.Find("select user_name from SET_User_Manager where UserId = '"+user+"'");
                if (ds_user.Tables[0].Rows.Count > 0)
                {
                    lblUser_Name.Text = ds_user.Tables[0].Rows[0]["user_name"].ToString();
                }

            }
        }
        catch (Exception ex)
        {

        }



    }
    protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
    {
        textClear();
        ddlCompany_Name.Enabled = true;
        txtEntry_Date.Enabled = true;
        txtPeriod_Description.Enabled = true;
        txtStart_Date.Enabled = true;
        txtEnd_Date.Enabled = true;
        lblDb_Name.Enabled = true;
        lblNo_of_Fiscal_Years.Enabled = true;
        lblPeriod_Id.Enabled = true;
        lblSystem_Date.Enabled = true;
        lblUser_Name.Enabled = true;
        chkActive_Status.Enabled = true;
        imgPopup.Enabled = true;
        ImageButton1.Enabled = true;
        ImageButton2.Enabled = true;

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

            ddlCompany_Name.ClearSelection();
            string serial_id;
            serial_id = GridView3.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select * from SET_Period WHERE ([PeriodID]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                //[PeriodID], [Description], [StartDate], [EndDate], [PeriodYearsSpan], [DbPath], [GUID],
                //[EntryDate], [SysDate], [EntryUserId], [IsActive], [Compid], [Record_Deleted]) 
                txtPeriod_Description.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                txtStart_Date.Text = ds.Tables[0].Rows[0]["StartDate"].ToString();
                txtEnd_Date.Text = ds.Tables[0].Rows[0]["EndDate"].ToString();
                lblNo_of_Fiscal_Years.Text = ds.Tables[0].Rows[0]["PeriodYearsSpan"].ToString();
                lblDb_Name.Text = ds.Tables[0].Rows[0]["DbPath"].ToString();
                txtEntry_Date.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                lblSystem_Date.Text = ds.Tables[0].Rows[0]["SysDate"].ToString();
                string user = ds.Tables[0].Rows[0]["EntryUserId"].ToString();
                ddlCompany_Name.Items.FindByValue(ds.Tables[0].Rows[0]["Compid"].ToString()).Selected = true;
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                if (chk == true)
                {
                    chkActive_Status.Checked = true;
                }
                else
                {
                    chkActive_Status.Checked = false;
                }

                DataSet ds_user = dml.Find("select user_name from SET_User_Manager where UserId = '" + user + "'");
                if (ds_user.Tables[0].Rows.Count > 0)
                {
                    lblUser_Name.Text = ds_user.Tables[0].Rows[0]["user_name"].ToString();
                }

            }
        }
        catch (Exception ex)
        {

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
        DeleteBox.Visible = false;
        try
        {
            ddlCompany_Name.ClearSelection();

            string serial_id;
            serial_id = GridView2.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select * from SET_Period WHERE ([PeriodID]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                //[PeriodID], [Description], [StartDate], [EndDate], [PeriodYearsSpan], [DbPath], [GUID],
                //[EntryDate], [SysDate], [EntryUserId], [IsActive], [Compid], [Record_Deleted]) 
                txtPeriod_Description.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                txtStart_Date.Text = ds.Tables[0].Rows[0]["StartDate"].ToString();
                txtEnd_Date.Text = ds.Tables[0].Rows[0]["EndDate"].ToString();
                lblNo_of_Fiscal_Years.Text = ds.Tables[0].Rows[0]["PeriodYearsSpan"].ToString();
                lblDb_Name.Text = ds.Tables[0].Rows[0]["DbPath"].ToString();
                txtEntry_Date.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                lblSystem_Date.Text = ds.Tables[0].Rows[0]["SysDate"].ToString();
                string user = ds.Tables[0].Rows[0]["EntryUserId"].ToString();
                ddlCompany_Name.Items.FindByValue(ds.Tables[0].Rows[0]["Compid"].ToString()).Selected = true;
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                if (chk == true)
                {
                    chkActive_Status.Checked = true;
                }
                else
                {
                    chkActive_Status.Checked = false;
                }

                DataSet ds_user = dml.Find("select user_name from SET_User_Manager where UserId = '" + user + "'");
                if (ds_user.Tables[0].Rows.Count > 0)
                {
                    lblUser_Name.Text = ds_user.Tables[0].Rows[0]["user_name"].ToString();
                }

            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string id = e.Row.Cells[1].Text;
        bool flag = dml.isNumber(id);


        if (flag == true)
        {

            DataSet ds = dml.Find("select PeriodId,MLD from SET_Period where PeriodId = '" + id + "'");
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

            DataSet ds = dml.Find("select PeriodId,MLD from SET_Period where PeriodId = '" + id + "'");
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

    public int compid()
    {
        return Convert.ToInt32(Request.Cookies["CompId"].Value);
    }
}
