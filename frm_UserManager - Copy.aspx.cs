using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class frm_UserManager : System.Web.UI.Page
{
    int DateFrom, AddDays, EditDays, DeleteDays;
    DmlOperation dml = new DmlOperation();
    FieldHide fd = new FieldHide();
    TextBox S_userid, S_LoginName, S_Displayarea;
    TextBox txtuser;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
    DataSet ds = new DataSet();


    string userid, UserGrpID, FormID;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Page.IsPostBack == false)
        {
            CalendarExtender1.EndDate = DateTime.Now;
            btnUpdate.Visible = false;
            btnSave.Visible = false;
            btnDelete_after.Visible = false;
            userid = Request.QueryString["UserID"];
            UserGrpID = Request.QueryString["UsergrpID"];
            FormID = Request.QueryString["FormID"];
            dml.dropdownsql(ddlDepartment, "SET_Department", "DepartmentName", "DepartmentID");
            fd.HideUSerGrpField(Page.Controls, UserGrpID, FormID, "SET_User_Manager");
            dml.dropdownsql(ddlUserGrpName, "SET_UserGrp", "UserGrpName", "sno", "Record_Deleted", "0");
            Findbox.Visible = false;
            DeleteBox.Visible = false;
            Editbox.Visible = false;
            txtwrgAttemp.Visible = false;

            textClear();
            Showall_Dml();
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


        txtPassword.Enabled = true;
        lblUserId.Enabled = true;
        txtLoginName.Enabled = true;
        chkActive.Checked = false;
        chkActive.Enabled = true;
        lblNoOfWrongsAttempts.Enabled = true;
        txtIpAddress.Enabled = true;
        txtAllowedWrongAttempts.Enabled = true;
        rdb_ForcePWDChn_Y.Enabled = true;
        rdb_ForcePWDChn_N.Enabled = true;
        lblChangePasswordDays.Enabled = true;
        lblRemaingDaysToChangePwd.Enabled = true;
        txtEmployee.Enabled = true;
        txtUserName.Enabled = true;
        ddlDepartment.Enabled = true;
        lblSystemDate.Enabled = true;
        imgDisplayArea.Enabled = true;
       // txtAutorityLevel.Enabled = true;
        txtEmployeeLeftDate.Enabled = false;
        txtLoginStatus.Enabled = true;
        txtDateOFStartWork.Enabled = true;
        txtEntry_Date.Enabled = true;
        txtRemarks.Enabled = true;
        lblSystemDate.Text = DateTime.Now.ToString();

    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        textClear();
        string userid = Request.QueryString["UserID"];
        txtPassword.Enabled = false;
        lblUserId.Enabled = false;
        txtLoginName.Enabled = false;
        chkActive.Enabled = false;
        lblNoOfWrongsAttempts.Enabled = false;
        txtIpAddress.Enabled = false;
        txtAllowedWrongAttempts.Enabled = false;
        rdb_ForcePWDChn_Y.Enabled = false;
        rdb_ForcePWDChn_N.Enabled = false;
        lblChangePasswordDays.Enabled = false;
        lblRemaingDaysToChangePwd.Enabled = false;
        txtEmployee.Enabled = false;
        txtUserName.Enabled = false;
        ddlDepartment.Enabled = false;
        lblSystemDate.Enabled = false;
        imgDisplayArea.Enabled = false;
       // txtAutorityLevel.Enabled = false; 
        txtEmployeeLeftDate.Enabled = false;
        txtLoginStatus.Enabled = false;
        txtDateOFStartWork.Enabled = false;
        txtEntry_Date.Enabled = false;
        txtRemarks.Enabled = false;
        //txtUserIdEdit.AutoPostBack = true;

        btnCancel.Visible = true;
        btnFind.Visible = true;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnInsert.Visible = false;
        try
        {
            GridView1.DataBind();
            string squer = "select * from SET_User_Manager ";
            string swhere;

            if (txtFind_LN.Text != "")
            {
                swhere = "LoginName = '" + txtFind_LN.Text +"'";
            }
            else
            {
                swhere = "LoginName is not null";
            }
            if (txtFind_UGN.Text != "")
            {
                swhere = swhere + " and UserGrpId = '" + txtFind_UGN.Text + "'";
            }
            else
            {
                swhere = swhere + " and UserGrpId is not null";
            }
            if (txtFind_Dep.Text != "")
            {
                swhere = swhere + " and department = '" + txtFind_Dep.Text + "'";
            }
            else
            {
                swhere = swhere + " and department is not null";
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
            squer = squer + " where " + swhere;

            Findbox.Visible = true;
            fieldbox.Visible = false;
            Editbox.Visible = false;

            DataSet dgrid = dml.grid(squer);
            if (dgrid.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = dgrid;
                GridView1.DataBind();
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


    protected void btn_delete_Click(object sender, EventArgs e)
    {
        DeleteBox.Visible = true;
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
            GridView2.DataBind();
            string squer = "select * from SET_User_Manager ";
            string swhere;

            if (txtDelete_LN.Text != "")
            {
                swhere = "LoginName = '" + txtDelete_LN.Text + "'";
            }
            else
            {
                swhere = "LoginName is not null";
            }
            if (txtDelete_UGN.Text != "")
            {
                swhere = swhere + " and UserGrpId = '" + txtDelete_UGN.Text + "'";
            }
            else
            {
                swhere = swhere + " and UserGrpId is not null";
            }
            if (txtDelete_Dep.Text != "")
            {
                swhere = swhere + " and department = '" + txtDelete_Dep.Text + "'";
            }
            else
            {
                swhere = swhere + " and department is not null";
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
            squer = squer + " where " + swhere;

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

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        txtPassword.Enabled = true;
        lblUserId.Enabled = true;
        txtLoginName.Enabled = true;
        chkActive.Enabled = true;
        lblNoOfWrongsAttempts.Enabled = true;
        txtIpAddress.Enabled = true;
        txtAllowedWrongAttempts.Enabled = true;
        rdb_ForcePWDChn_Y.Enabled = true;
        rdb_ForcePWDChn_N.Enabled = true;
        lblChangePasswordDays.Enabled = true;
        lblRemaingDaysToChangePwd.Enabled = true;
        txtEmployee.Enabled = true;
        txtUserName.Enabled = true;
        ddlDepartment.Enabled = true;
        lblSystemDate.Enabled = true;
        imgDisplayArea.Enabled = true;
       // txtAutorityLevel.Enabled = true;
        txtEmployeeLeftDate.Enabled = true;
        txtLoginStatus.Enabled = true;
        txtDateOFStartWork.Enabled = true;
        txtEntry_Date.Enabled = true;
        txtRemarks.Enabled = true;
        btnUpdate.Visible = true;
        btnInsert.Visible = false;
        btnDelete.Visible = false;
        btnEdit.Visible = false;

    }
    public void Update()
    {
        int rdb = 0;
        if (rdb_ForcePWDChn_Y.Checked == true)
        {
            rdb = 1;
        }
        else
        {
            rdb = 0;
        }
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
            
            dml.Update("Update SET_User_Manager set LoginName = '" + txtLoginName.Text + "', pwd = '" + txtPassword.Text + "', SysDate = GETDATE(), remarks = '" + txtRemarks.Text + "', pic_path = '', user_name = '" + txtUserName.Text + "', department = '" + ddlDepartment.SelectedItem.Value + "', emp_id = " + txtEmployee.Text + ", IsActive = " + chk + ", ForcePwdChange = " + rdb + ", login_ip = '" + txtIpAddress.Text + "', user_login_status = " + txtLoginStatus.Text + ", user_Left_Date = '" + txtEmployeeLeftDate.Text + "', Last_Mac_add = '', Last_Ip = '', Wrong_attempt='"+ txtwrgAttemp.Text+ "'  where Sno = '" + lblUserId.Text + "'", "");
            textClear();
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", " Editalert()", true);
            con.Close();
        }
        catch(Exception ex)
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
        txtPassword.Attributes["value"] = "";
        lblUserId.Text = "";
        txtLoginName.Text = "";

        ddlDepartment.SelectedIndex = 0;
        chkActive.Checked = false;
        lblNoOfWrongsAttempts.Text = "";
        txtIpAddress.Text = "";
        Label1.Text = "";
        txtAllowedWrongAttempts.Text = "";
        rdb_ForcePWDChn_N.Checked = false;
        rdb_ForcePWDChn_Y.Checked = false;
        lblChangePasswordDays.Text = "";
        lblRemaingDaysToChangePwd.Text = "";
        txtEmployee.Text = "";
        txtUserName.Text = "";

        lblSystemDate.Text = "";
        imgDisplayArea.ImageUrl = "";
      
        txtEmployeeLeftDate.Text = "";
        txtLoginStatus.Text = "";
        txtDateOFStartWork.Text = "";
        txtEntry_Date.Text = "";
        txtRemarks.Text = "";

        txtPassword.Enabled = false;
        lblUserId.Enabled = false;
        txtLoginName.Enabled = false;
        chkActive.Enabled = false;
        lblNoOfWrongsAttempts.Enabled = false;
        txtIpAddress.Enabled = false;
        txtAllowedWrongAttempts.Enabled = false;
        rdb_ForcePWDChn_Y.Enabled = false;
        rdb_ForcePWDChn_N.Enabled = false;
        lblChangePasswordDays.Enabled = false;
        lblRemaingDaysToChangePwd.Enabled = false;
        txtEmployee.Enabled = false;
        txtUserName.Enabled = false;
        ddlDepartment.Enabled = false;
        lblSystemDate.Enabled = false;
        imgDisplayArea.Enabled = false;
     //   txtAutorityLevel.Enabled = false;
        txtEmployeeLeftDate.Enabled = false;
        txtLoginStatus.Enabled = false;
        txtDateOFStartWork.Enabled = false;
        txtEntry_Date.Enabled = false;
        txtRemarks.Enabled = false;


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
        textClear();

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
        //SqlDataAdapter da = new SqlDataAdapter("select * from SET_UserGrp_Form where FormId='"+FormID+"' and DmlAllowed = 'Y'", con);
        //DataSet ds = new DataSet();
        //da.Fill(ds);
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    if(ds.Tables[0].Rows[0]["Add"].ToString() == "N")
        //    {
        //        btnInsert.Visible = false;
        //    }
        //    if (ds.Tables[0].Rows[0]["Edit"].ToString() == "N")
        //    {
        //        btnEdit.Visible = false;
        //    }
        //     if (ds.Tables[0].Rows[0]["Delete"].ToString() == "N")
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
        //    btnCancel.Visible = false;
        //}
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Update();
    }
    public void add_save()
    {
        try {
            DataSet ds_loginname = dml.Find("select LoginName from SET_User_Manager where LoginName = '" + txtLoginName.Text + "'");

            if (ds_loginname.Tables[0].Rows.Count > 0)
            {
                Label1.Text = "Login Name already exsit ";
            }
            else {
                int rdb = 0;
                if (rdb_ForcePWDChn_Y.Checked == true)
                {
                    rdb = 1;
                }
                else
                {
                    rdb = 0;
                }
                int chk = 0;
                if (chkActive.Checked == true)
                {
                    chk = 1;
                }
                else
                {
                    chk = 0;
                }
                con.Open();
                DataSet ds_usergrpname = dml.Find("select UserGrpId from SET_UserGrp where Sno= " + ddlUserGrpName.SelectedItem.Value + "");
                string usergrpid = ds_usergrpname.Tables[0].Rows[0]["UserGrpId"].ToString();
                SqlCommand cmd = new SqlCommand("insert into SET_User_Manager(LoginName,pwd,SysDate,remarks,pic_path,user_name,department,emp_id, IsActive, Last_Login,Last_pwd_date, last_pwd, Lock_user, Wrong_attempt, ForcePwdChange,PwdChangeDays, PwdChangeCounter, login_ip, user_login_status, Last_Mac_add, Last_Ip,UserGrpId,user_Left_Date) values ('" + txtLoginName.Text + "', '" + txtPassword.Text + "', GETDATE(), '" + txtRemarks.Text + "',NULL, '" + txtUserName.Text + "', '" + ddlDepartment.SelectedItem.Value + "', " + txtEmployee.Text + ", " + chk + ", '2019-06-24', '2019-06-18', 'fahad', " + txtAllowedWrongAttempts.Text + ", 3, " + rdb + ", 30, 6, '" + txtIpAddress.Text + "', " + txtLoginStatus.Text + ",  'F0DEF194CC4D', '192.168.15.8','"+ usergrpid + "','2019-05-27')", con);
                cmd.ExecuteNonQuery();
                Label1.Enabled = false;
                textClear();
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", " alertme()", true);
                con.Close();
            }
        }
        catch(Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        add_save();
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
            GridView3.DataBind();
            string squer = "select * from SET_User_Manager ";
            string swhere;

            if (txtEdit_LN.Text != "")
            {
                swhere = "LoginName = '" + txtEdit_LN.Text + "'";
            }
            else
            {
                swhere = "LoginName is not null";
            }
            if (txtEdit_UGN.Text != "")
            {
                swhere = swhere + " and UserGrpId = '" + txtEdit_UGN.Text + "'";
            }
            else
            {
                swhere = swhere + " and UserGrpId is not null";
            }
            if (txtEdit_DEp.Text != "")
            {
                swhere = swhere + " and department = '" + txtEdit_DEp.Text + "'";
            }
            else
            {
                swhere = swhere + " and department is not null";
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
            squer = squer + " where " + swhere;


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

        FormID = Request.QueryString["FormID"];
       
    }


    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtPassword.Enabled = true;
        lblUserId.Enabled = true;
        txtLoginName.Enabled = true;
        chkActive.Enabled = true;
        lblNoOfWrongsAttempts.Enabled = true;
        txtIpAddress.Enabled = true;
        txtAllowedWrongAttempts.Enabled = true;
        rdb_ForcePWDChn_N.Enabled = true;
        rdb_ForcePWDChn_Y.Enabled = true;
        lblChangePasswordDays.Enabled = true;
        lblRemaingDaysToChangePwd.Enabled = true;
        txtEmployee.Enabled = true;
        txtUserName.Enabled = true;
        ddlDepartment.Enabled = true;
        lblSystemDate.Enabled = true;
        imgDisplayArea.Enabled = true;
       
        txtEmployeeLeftDate.Enabled = true;
        txtLoginStatus.Enabled = true;
        txtDateOFStartWork.Enabled = true;
        txtEntry_Date.Enabled = true;
        txtRemarks.Enabled = true;

        btnUpdate.Visible = false;
        btnInsert.Visible = false;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        Label1.Text = "";


        fieldbox.Visible = true;
        Findbox.Visible = false;
        textClear();


        txtPassword.Attributes["value"] = GridView1.SelectedRow.Cells[4].Text;
        lblUserId.Text = GridView1.SelectedRow.Cells[1].Text;
        txtLoginName.Text = GridView1.SelectedRow.Cells[3].Text;
        lblNoOfWrongsAttempts.Text = GridView1.SelectedRow.Cells[10].Text;
        txtIpAddress.Text = Server.HtmlDecode(GridView1.SelectedRow.Cells[20].Text);
        txtAllowedWrongAttempts.Text = GridView1.SelectedRow.Cells[11].Text;
        //txtForcePasswordChange.Text = GridView1.SelectedRow.Cells[9].Text;
        CheckBox rdb = GridView1.SelectedRow.Cells[9].Controls[0] as CheckBox;
        lblChangePasswordDays.Text = GridView1.SelectedRow.Cells[18].Text;
        lblRemaingDaysToChangePwd.Text = GridView1.SelectedRow.Cells[19].Text;
        txtEmployee.Text = GridView1.SelectedRow.Cells[17].Text;
        txtUserName.Text = GridView1.SelectedRow.Cells[8].Text;
        lblSystemDate.Text = GridView1.SelectedRow.Cells[5].Text;
        ddlDepartment.SelectedIndex = int.Parse(GridView1.SelectedRow.Cells[16].Text);
        imgDisplayArea.ImageUrl = GridView1.SelectedRow.Cells[7].Text;
        txtEmployeeLeftDate.Text = GridView1.SelectedRow.Cells[23].Text;
        txtLoginStatus.Text = GridView1.SelectedRow.Cells[22].Text;
        //txtDateOFStartWork.Text = GridView1.SelectedRow.Cells[1].Text;
        //txtEntry_Date.Text = GridView1.SelectedRow.Cells[1].Text;
        txtRemarks.Text = GridView1.SelectedRow.Cells[6].Text;
        //chk
        string chk = GridView1.SelectedRow.Cells[15].Text;
        if (rdb.Checked == true)
        {
            rdb_ForcePWDChn_Y.Checked = true;
            rdb_ForcePWDChn_N.Checked = false;
        }
        else
        {
            rdb_ForcePWDChn_Y.Checked = false;
            rdb_ForcePWDChn_N.Checked = true;
        }

        if (chk == "1")
        {
            chkActive.Checked = true;
        }
        else
        {
            chkActive.Checked = false;
        }
        dml.dateConvert(txtEmployeeLeftDate);

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow grow in GridView2.Rows)
        {
            CheckBox chk_del = (CheckBox)grow.FindControl("chkDeletegd");
            Label lblID = (Label)grow.FindControl("lbl_ID");
            if (chk_del.Checked)
            {
                int ID = Convert.ToInt32(lblID.Text);
              
                dml.Delete("delete SET_User_Manager where Sno = " + ID , "");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "alertDelete()", true);
            }
            else
            {
               // ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "fillrequird()", true);
                GridView2.DataBind();
            }
        }
        GridView2.DataBind();
    }

    protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtPassword.Enabled = true;
        lblUserId.Enabled = true;
        txtLoginName.Enabled = true;
        chkActive.Enabled = true;
        lblNoOfWrongsAttempts.Enabled = true;
        txtIpAddress.Enabled = true;
        txtAllowedWrongAttempts.Enabled = true;
        rdb_ForcePWDChn_N.Enabled = true;
        rdb_ForcePWDChn_Y.Enabled = true;
        lblChangePasswordDays.Enabled = true;
        lblRemaingDaysToChangePwd.Enabled = true;
        txtEmployee.Enabled = true;
        txtUserName.Enabled = true;
        ddlDepartment.Enabled = true;
        wrgAttempt.Visible = false;
        txtwrgAttemp.Visible = true;
        lblSystemDate.Enabled = true;
        imgDisplayArea.Enabled = true;
        //   txtAutorityLevel.Enabled = true;
        txtEmployeeLeftDate.Enabled = true;
        txtLoginStatus.Enabled = true;
        txtDateOFStartWork.Enabled = true;
        txtEntry_Date.Enabled = true;
        txtRemarks.Enabled = true;

        btnUpdate.Visible = true;
        btnInsert.Visible = false;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        Label1.Text = "";


        fieldbox.Visible = true;
        Editbox.Visible = false;
        //textClear();


        txtPassword.Attributes["value"] = GridView3.SelectedRow.Cells[4].Text;
        lblUserId.Text = GridView3.SelectedRow.Cells[1].Text;
        txtLoginName.Text = GridView3.SelectedRow.Cells[3].Text;
        txtwrgAttemp.Text = GridView3.SelectedRow.Cells[10].Text;
        txtIpAddress.Text = Server.HtmlDecode(GridView3.SelectedRow.Cells[20].Text);
        txtAllowedWrongAttempts.Text = GridView3.SelectedRow.Cells[11].Text;
        CheckBox rdb = GridView3.SelectedRow.Cells[9].Controls[0] as CheckBox;
        lblChangePasswordDays.Text = GridView3.SelectedRow.Cells[18].Text;
        lblRemaingDaysToChangePwd.Text = GridView3.SelectedRow.Cells[19].Text;
        txtEmployee.Text = GridView3.SelectedRow.Cells[17].Text;
        txtUserName.Text = GridView3.SelectedRow.Cells[8].Text;
        lblSystemDate.Text = GridView3.SelectedRow.Cells[5].Text;
        ddlDepartment.SelectedIndex = int.Parse(GridView3.SelectedRow.Cells[16].Text);
        imgDisplayArea.ImageUrl = GridView3.SelectedRow.Cells[7].Text;
        txtEmployeeLeftDate.Text = GridView3.SelectedRow.Cells[23].Text;
        txtLoginStatus.Text = GridView3.SelectedRow.Cells[22].Text;
        txtDateOFStartWork.Text = GridView3.SelectedRow.Cells[1].Text;
        txtEntry_Date.Text = GridView3.SelectedRow.Cells[1].Text;
        txtRemarks.Text = GridView3.SelectedRow.Cells[6].Text;
        
        string chk = GridView3.SelectedRow.Cells[15].Text;
       
        if (rdb.Checked == true)
        {
            rdb_ForcePWDChn_Y.Checked = true;
            rdb_ForcePWDChn_N.Checked = false;
        }
        else
        {
            rdb_ForcePWDChn_Y.Checked = false;
            rdb_ForcePWDChn_N.Checked = true;
        }

        if (chk == "1")
        {
            chkActive.Checked = true;
        }
        else
        {
            chkActive.Checked = false;
        }
        dml.dateConvert(txtEmployeeLeftDate);

    }
}

