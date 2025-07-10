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
            dml.dropdownsql(txtFind_Dep, "SET_Department", "DepartmentName", "DepartmentID");
            dml.dropdownsql(txtEdit_DEp, "SET_Department", "DepartmentName", "DepartmentID");
            dml.dropdownsql(txtDelete_Dep, "SET_Department", "DepartmentName", "DepartmentID");

            dml.dropdownsql(txtDelete_UGN, "SET_UserGrp", "UserGrpName", "UserGrpId", "Record_Deleted", "0");
            dml.dropdownsql(txtEdit_UGN, "SET_UserGrp", "UserGrpName", "UserGrpId", "Record_Deleted", "0");
            dml.dropdownsql(txtFind_UGN, "SET_UserGrp", "UserGrpName", "UserGrpId", "Record_Deleted", "0");


            dml.dropdownsql(txtEdit_LN, "SET_User_Manager", "user_name", "LoginName", "Record_Deleted", "0");
            dml.dropdownsql(txtFind_LN, "SET_User_Manager", "user_name", "LoginName", "Record_Deleted", "0");
            dml.dropdownsql(txtDelete_LN, "SET_User_Manager", "user_name", "LoginName", "Record_Deleted", "0");


            Findbox.Visible = false;
            DeleteBox.Visible = false;
            Editbox.Visible = false;
            txtwrgAttemp.Visible = false;
            FileUpload1.Visible = true;
            imgDisplayArea.Visible = false;
            txtEntry_Date.Attributes.Add("readonly", "readonly");
            txtDateOFStartWork.Attributes.Add("readonly", "readonly");
            textClear();
           
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataSet ds_loginname = dml.Find("select LoginName from SET_User_Manager where LoginName = '" + txtLoginName.Text + "' and Record_Deleted = '0'");

        if (ds_loginname.Tables[0].Rows.Count > 0)
        {
            Label1.Text = "Login Name already exsit ";
        }
        else {
            try
            {
                int chk = 0;

                if (chkActive.Checked == true)
                {
                    chk = 1;
                }
                else
                {
                    chk = 0;
                }
                string pwdchange = "";
                if (rdb_ForcePWDChn_Y.Checked == true)
                {
                    pwdchange = "1";
                }
                if (rdb_ForcePWDChn_N.Checked == true)
                {
                    pwdchange = "0";
                }
                string imgpath = "~/dist/img/" + Path.GetFileName(FileUpload1.FileName);

                DataSet ds_usergrpname = dml.Find("select UserGrpId from SET_UserGrp where Sno= " + ddlUserGrpName.SelectedItem.Value + "");
                string usergrpid = ds_usergrpname.Tables[0].Rows[0]["UserGrpId"].ToString();
                DataSet ds_Department = dml.Find("select MenuId from SET_Menu where Sno = " + ddlDepartment.SelectedItem.Value + "");
                string Department = ds_Department.Tables[0].Rows[0]["MenuId"].ToString();

                //DataSet ds_FEndDate = dml.Find("");
                dml.Insert("insert into SET_User_Manager(LoginName,pwd,SysDate,remarks,pic_path,user_name,department,emp_id, IsActive, Last_Login,Last_pwd_date, last_pwd, Lock_user, Wrong_attempt, ForcePwdChange,PwdChangeDays, PwdChangeCounter, login_ip, user_login_status, Last_Mac_add, Last_Ip,UserGrpId,user_Left_Date,Record_Deleted,DateOfStartWork) values ('" + txtLoginName.Text + "', '" + txtPassword.Text + "', GETDATE(), '" + txtRemarks.Text + "','"+ imgpath + "', '" + txtUserName.Text + "', '" + ddlDepartment.SelectedItem.Value + "', " + txtEmployee.Text + ", " + chk + ", '2019-06-24', '2019-06-18', 'fahad', " + txtAllowedWrongAttempts.Text + ", 0, " + pwdchange + ", 30, 6, '" + txtIpAddress.Text + "', " + txtLoginStatus.Text + ",  'F0DEF194CC4D', '192.168.15.8','" + usergrpid + "','"+txtEmployeeLeftDate.Text+"',0, '"+dml.dateconvertforinsert(txtDateOFStartWork)+ "')", "");
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


        FileUpload1.Visible = true;
        imgDisplayArea.Visible = false ;

        txtPassword.Enabled = true;
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
        chkActive.Checked = true;
        rdb_ForcePWDChn_N.Checked = true;
        txtRemarks.Enabled = true;
        lblSystemDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
        FileUpload1.Enabled = true;
        btnUpload.Enabled = true;
        imgPopup.Enabled = true;
        ImageButton1.Enabled = true;
        ddlUserGrpName.Enabled = true;

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string dsw = txtDateOFStartWork.Text;
        string ed = txtEntry_Date.Text;
        string empld = txtEmployeeLeftDate.Text; 
        try {
            string imgpath;
            int chk = 0;

            if (chkActive.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
            string pwdchange = "";
            if (rdb_ForcePWDChn_Y.Checked == true)
            {
                pwdchange = "1";
            }
            if (rdb_ForcePWDChn_N.Checked == true)
            {
                pwdchange = "0";
            }
            if (FileUpload1.HasFile)
            {
                 imgpath = "~/dist/img/" + Path.GetFileName(FileUpload1.FileName);
            }
            else
            {
                imgpath = imgDisplayArea.ImageUrl;
            }
            string emp, d,edp;
            if (ed == "")
            {
                ed = "EntryDate = NULL";
                edp = "EntryDate IS NULL";
            }
            else
            {
                ed = "EntryDate = '" + txtEntry_Date.Text + "'";
                edp = "([EntryDate] = '" + txtEntry_Date.Text + "')";
            }
            if (empld == "")
            {
                empld = "user_Left_Date = NULL";
                emp = "user_Left_Date IS NULL";
            }
            else
            {
                empld = "[user_Left_Date] = '" + txtEmployeeLeftDate.Text + "'";
                emp = "([user_Left_Date] = '" + txtEmployeeLeftDate.Text + "')";
            }
            if (dsw == "")
            {
                dsw = "DateOfStartWork = NULL";
                d = "DateOfStartWork IS NULL";
            }
            else
            {
                dsw = "DateOfStartWork = '" + txtDateOFStartWork.Text + "'";
                d = "([DateOfStartWork] = '"+txtDateOFStartWork.Text+"')";
            }
            
            DataSet ds_usergrpname = dml.Find("select UserGrpId from SET_UserGrp where Sno= " + ddlUserGrpName.SelectedItem.Value + "");
        string usergrpid = ds_usergrpname.Tables[0].Rows[0]["UserGrpId"].ToString();
        DataSet ds_Department= dml.Find("select MenuId from SET_Menu where Sno = " + ddlDepartment.SelectedItem.Value + "");
        string Department = ds_Department.Tables[0].Rows[0]["MenuId"].ToString();

            DataSet ds_up = dml.Find("select * from SET_User_Manager WHERE ([Sno]='"+ViewState["Sno"].ToString()+"') AND ([LoginName]='"+txtLoginName.Text+"') AND ([pwd]='"+txtPassword.Text+"') AND ([remarks]='"+txtRemarks.Text+"') AND ([pic_path]='"+ imgpath + "') AND ([department]='"+ddlDepartment.SelectedItem.Value+"') AND ([emp_id]='"+txtEmployee.Text+"') AND ([IsActive]='"+chk+"') AND ([Lock_user]='"+txtAllowedWrongAttempts.Text+"') AND  ([ForcePwdChange]='"+pwdchange+ "') AND  ([UserGrpId]='" + usergrpid + "') AND ([PwdChangeDays]='" + lblChangePasswordDays.Text+"') AND ([PwdChangeCounter]='"+lblRemaingDaysToChangePwd.Text+"') AND "+emp+"  AND "+d+" AND "+edp+"");

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
                                
                dml.Update("Update SET_User_Manager set LoginName = '" + txtLoginName.Text + "', pwd = '" + txtPassword.Text + "', SysDate = GETDATE(), remarks = '" + txtRemarks.Text + "', pic_path = '" + imgpath + "', user_name = '" + txtUserName.Text + "', department = '" + ddlDepartment.SelectedItem.Value + "', emp_id = " + txtEmployee.Text + ", IsActive = " + chk + ", ForcePwdChange = " + pwdchange + ", login_ip = '" + txtIpAddress.Text + "', user_login_status = " + txtLoginStatus.Text + ", "+empld+", Last_Mac_add = '', Last_Ip = '', Wrong_attempt=  " + lblNoOfWrongsAttempts.Text + " , UserGrpId= '" + usergrpid + "' ,  " + dsw + " ,  " + ed + " where Sno = '" + ViewState["Sno"].ToString() + "'", "");
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
            dml.Delete("update SET_User_Manager set Record_Deleted = 1 where Sno =  " + ViewState["Sno"].ToString() + "", "");
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
        btnDelete.Visible = true;
        btnUpdate.Visible = false;
        btnEdit.Visible = true;
        btnFind.Visible = true;
        btnSave.Visible = false;


        btnDelete_after.Visible = false;

        imgDisplayArea.Visible = true;

        DeleteBox.Visible = false;
        Editbox.Visible = false;
        Findbox.Visible = false;
        fieldbox.Visible = true;
        
        dml.dropdownsql(ddlUserGrpName, "SET_UserGrp", "UserGrpName", "sno", "Record_Deleted", "0");
        dml.dropdownsql(ddlDepartment, "SET_Department", "DepartmentName", "DepartmentID");



    }

    protected void btnSearchEdit_Click(object sender, EventArgs e)
    {
        btnSave.Visible = false;
        btnEdit.Visible = true;
        btnFind.Visible = false;
        btnCancel.Visible = true;
        btnDelete.Visible = false;
        btnInsert.Visible = false;
        btnUpdate.Visible = false;
        try
        {
            string squer = "select * from View_UserManger ";
            string swhere;
            if (txtEdit_LN.SelectedIndex != 0)
            {
                swhere = "user_name = '" + txtEdit_LN.SelectedItem.Text + "'";
            }

            else
            {
                swhere = "user_name is not null";
            }

            if (txtEdit_DEp.SelectedIndex != 0)
            {
                swhere = swhere + " and department = '" + txtEdit_DEp.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and department is not null";
            }

            if (txtEdit_UGN.SelectedIndex != 0)
            {
                swhere = swhere + " and UserGrpId = '" + txtEdit_UGN.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and UserGrpId is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY user_name";


            Findbox.Visible = false;
            DeleteBox.Visible = false;
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
        btnSave.Visible = false;
        btnEdit.Visible = false;
        btnFind.Visible = true;
        btnCancel.Visible = true;
        btnDelete.Visible = false;
        btnInsert.Visible = false;
        btnUpdate.Visible = false;
        try
        {
            string squer = "select * from View_UserManger ";
            string swhere;
            if (txtFind_LN.SelectedIndex != 0)
            {
                swhere = "user_name = '" + txtFind_LN.SelectedItem.Text + "'";
            }

            else
            {
                swhere = "user_name is not null";
            }

            if (txtFind_Dep.SelectedIndex != 0)
            {
                swhere = swhere + " and department = '"+txtFind_Dep.SelectedItem.Value+"'";
            }
            else 
            {
                swhere = swhere + " and department is not null";
            }
            if (txtFind_UGN.SelectedIndex != 0)
            {
                swhere = swhere + " and UserGrpId = '" + txtFind_UGN.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and UserGrpId is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY user_name";


            Findbox.Visible = true;
            DeleteBox.Visible = false;
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
           string squer = "select * from View_UserManger ";
           string swhere;
            if (txtDelete_LN.SelectedIndex != 0)
            {
                swhere = "user_name = '" + txtDelete_LN.SelectedItem.Text + "'";
            }
                     
            else
            {
                swhere = "user_name is not null";
            }
            //txtDelete_UGN
            if (txtDelete_Dep.SelectedIndex != 0)
            {
                swhere = swhere + " and department = '" + txtDelete_Dep.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and department is not null";
            }

            if (txtDelete_UGN.SelectedIndex != 0)
            {
                swhere = swhere + " and UserGrpId = '" + txtDelete_UGN.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and UserGrpId is not null";
            }

            if (chkFind_Active.Checked == true)
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY user_name";

            Findbox.Visible = false;
            DeleteBox.Visible = true;
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

    public void textClear()
    {
        DeleteBox.Visible = false;
        Editbox.Visible = false;
        Findbox.Visible = false;
        fieldbox.Visible = true;
        txtPassword.Attributes["value"] = "";
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
        FileUpload1.Enabled = false;
        btnUpload.Enabled = false;
        imgPopup.Enabled = false;
        ImageButton1.Enabled = false;
        ddlUserGrpName.Enabled = false;
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


        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {
            string serialno = GridView1.SelectedRow.Cells[1].Text;
            
            DataSet ds = dml.Find("select * from View_UserManger where Sno = " + serialno);

            if (ds.Tables[0].Rows.Count > 0)

            {
                DataSet dds = dml.Find("select * from SET_User_Manager where Sno = " + serialno);

                ddlUserGrpName.ClearSelection();
                ddlDepartment.ClearSelection();

                txtLoginName.Text = dds.Tables[0].Rows[0]["LoginName"].ToString();
                txtPassword.Attributes["value"] = dds.Tables[0].Rows[0]["pwd"].ToString();
                DateTime sys = DateTime.Parse(dds.Tables[0].Rows[0]["SysDate"].ToString());
                txtRemarks.Text = dds.Tables[0].Rows[0]["remarks"].ToString();
                imgDisplayArea.ImageUrl = dds.Tables[0].Rows[0]["pic_path"].ToString();
                
                txtUserName.Text = dds.Tables[0].Rows[0]["user_name"].ToString();
                ddlDepartment.Items.FindByValue(dds.Tables[0].Rows[0]["department"].ToString()).Selected = true;
                txtEmployee.Text = dds.Tables[0].Rows[0]["emp_id"].ToString();
                string chk = dds.Tables[0].Rows[0]["IsActive"].ToString();
                txtAllowedWrongAttempts.Text = dds.Tables[0].Rows[0]["Lock_user"].ToString();
                lblNoOfWrongsAttempts.Text = dds.Tables[0].Rows[0]["Wrong_attempt"].ToString();
                string rdb = dds.Tables[0].Rows[0]["ForcePwdChange"].ToString();
                lblChangePasswordDays.Text = dds.Tables[0].Rows[0]["PwdChangeDays"].ToString();
                lblRemaingDaysToChangePwd.Text = dds.Tables[0].Rows[0]["PwdChangeCounter"].ToString();
                txtIpAddress.Text = Server.HtmlDecode(dds.Tables[0].Rows[0]["login_ip"].ToString());
                txtLoginStatus.Text = dds.Tables[0].Rows[0]["user_login_status"].ToString();
                txtEmployeeLeftDate.Text = dds.Tables[0].Rows[0]["user_Left_Date"].ToString();
                ddlUserGrpName.Items.FindByText(ds.Tables[0].Rows[0]["UserGrpName"].ToString()).Selected = true;
                txtDateOFStartWork.Text = dds.Tables[0].Rows[0]["DateOfStartWork"].ToString();
                txtEntry_Date.Text = dds.Tables[0].Rows[0]["EntryDate"].ToString();
                dml.dateConvert(txtEntry_Date);
                dml.dateConvert(txtDateOFStartWork);
                dml.dateConvert(txtEmployeeLeftDate);
                lblSystemDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                if(chk == "1"){
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if(rdb == "True") {
                    rdb_ForcePWDChn_Y.Checked = true;
                }
                else
                {
                    rdb_ForcePWDChn_N.Checked = true;
                }
                txtPassword.Enabled = false;
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
                ddlUserGrpName.Enabled = false; 
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
        DeleteBox.Visible = false;
        try
        {
            string serialno = GridView2.SelectedRow.Cells[1].Text;
            ViewState["Sno"] = serialno;
            DataSet ds = dml.Find("select * from View_UserManger where Sno = " + serialno);

            if (ds.Tables[0].Rows.Count > 0)

            {
                DataSet dds = dml.Find("select * from SET_User_Manager where Sno = " + serialno);

                ddlUserGrpName.ClearSelection();
                ddlDepartment.ClearSelection();

                txtLoginName.Text = dds.Tables[0].Rows[0]["LoginName"].ToString();
                txtPassword.Attributes["value"] = dds.Tables[0].Rows[0]["pwd"].ToString();
                DateTime sys = DateTime.Parse(dds.Tables[0].Rows[0]["SysDate"].ToString());
                txtRemarks.Text = dds.Tables[0].Rows[0]["remarks"].ToString();
                imgDisplayArea.ImageUrl = dds.Tables[0].Rows[0]["pic_path"].ToString();
              
                txtUserName.Text = dds.Tables[0].Rows[0]["user_name"].ToString();
                ddlDepartment.Items.FindByValue(dds.Tables[0].Rows[0]["department"].ToString()).Selected = true;
                txtEmployee.Text = dds.Tables[0].Rows[0]["emp_id"].ToString();
                string chk = dds.Tables[0].Rows[0]["IsActive"].ToString();
                txtAllowedWrongAttempts.Text = dds.Tables[0].Rows[0]["Lock_user"].ToString();
                lblNoOfWrongsAttempts.Text = dds.Tables[0].Rows[0]["Wrong_attempt"].ToString();
                string rdb = dds.Tables[0].Rows[0]["ForcePwdChange"].ToString();
                lblChangePasswordDays.Text = dds.Tables[0].Rows[0]["PwdChangeDays"].ToString();
                lblRemaingDaysToChangePwd.Text = dds.Tables[0].Rows[0]["PwdChangeCounter"].ToString();
                txtIpAddress.Text = Server.HtmlDecode(dds.Tables[0].Rows[0]["login_ip"].ToString());
                txtLoginStatus.Text = dds.Tables[0].Rows[0]["user_login_status"].ToString();
                txtEmployeeLeftDate.Text = dds.Tables[0].Rows[0]["user_Left_Date"].ToString();
                ddlUserGrpName.Items.FindByText(ds.Tables[0].Rows[0]["UserGrpName"].ToString()).Selected = true;
                txtEntry_Date.Text = dds.Tables[0].Rows[0]["EntryDate"].ToString();
                dml.dateConvert(txtEntry_Date);
                dml.dateConvert(txtEmployeeLeftDate);
                txtDateOFStartWork.Text = dds.Tables[0].Rows[0]["DateOfStartWork"].ToString();
                dml.dateConvert(txtDateOFStartWork);
                lblSystemDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                if (chk == "1")
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (rdb == "True")
                {
                    rdb_ForcePWDChn_Y.Checked = true;
                }
                else
                {
                    rdb_ForcePWDChn_N.Checked = true;
                }
                
                txtPassword.Enabled = false;
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
                ddlUserGrpName.Enabled = false;
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
            DataSet ds = dml.Find("select * from View_UserManger where Sno = " + serialno);

            if (ds.Tables[0].Rows.Count > 0)

            {
                DataSet dds = dml.Find("select * from SET_User_Manager where Sno = " + serialno);

                ddlUserGrpName.ClearSelection();
                ddlDepartment.ClearSelection();

                txtLoginName.Text = dds.Tables[0].Rows[0]["LoginName"].ToString();
                txtPassword.Attributes["value"] = dds.Tables[0].Rows[0]["pwd"].ToString();
                DateTime sys = DateTime.Parse(dds.Tables[0].Rows[0]["SysDate"].ToString());
                txtRemarks.Text = dds.Tables[0].Rows[0]["remarks"].ToString();
                imgDisplayArea.ImageUrl = dds.Tables[0].Rows[0]["pic_path"].ToString();

                txtUserName.Text = dds.Tables[0].Rows[0]["user_name"].ToString();
                ddlDepartment.Items.FindByValue(dds.Tables[0].Rows[0]["department"].ToString()).Selected = true;
                txtEmployee.Text = dds.Tables[0].Rows[0]["emp_id"].ToString();
                string chk = dds.Tables[0].Rows[0]["IsActive"].ToString();
                txtAllowedWrongAttempts.Text = dds.Tables[0].Rows[0]["Lock_user"].ToString();
                lblNoOfWrongsAttempts.Text = dds.Tables[0].Rows[0]["Wrong_attempt"].ToString();
                string rdb = dds.Tables[0].Rows[0]["ForcePwdChange"].ToString();
                lblChangePasswordDays.Text = dds.Tables[0].Rows[0]["PwdChangeDays"].ToString();
                lblRemaingDaysToChangePwd.Text = dds.Tables[0].Rows[0]["PwdChangeCounter"].ToString();
                txtIpAddress.Text = Server.HtmlDecode(dds.Tables[0].Rows[0]["login_ip"].ToString());
                txtLoginStatus.Text = dds.Tables[0].Rows[0]["user_login_status"].ToString();
                txtEmployeeLeftDate.Text = dds.Tables[0].Rows[0]["user_Left_Date"].ToString();
                ddlUserGrpName.Items.FindByText(ds.Tables[0].Rows[0]["UserGrpName"].ToString()).Selected = true;
                txtDateOFStartWork.Text = dds.Tables[0].Rows[0]["DateOfStartWork"].ToString();
                txtEntry_Date.Text = dds.Tables[0].Rows[0]["EntryDate"].ToString();
                dml.dateConvert(txtEntry_Date);
                //txtDateOFStartWork.Text = dds.Tables[0].Rows[0]["Wrong_attempt"].ToString();
                txtDateOFStartWork.Text = dds.Tables[0].Rows[0]["DateOfStartWork"].ToString();
                dml.dateConvert(txtDateOFStartWork);
                dml.dateConvert(txtEmployeeLeftDate);
                lblSystemDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                if (chk == "1")
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (rdb == "True")
                {
                    rdb_ForcePWDChn_Y.Checked = true;
                }
                else
                {
                    rdb_ForcePWDChn_N.Checked = true;
                }
                txtPassword.Enabled = true;
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
             
                txtEmployeeLeftDate.Enabled = true;
                txtLoginStatus.Enabled = true;
                txtDateOFStartWork.Enabled = true;
                txtEntry_Date.Enabled = true;
                txtRemarks.Enabled = true;
                ddlUserGrpName.Enabled = true;
                FileUpload1.Visible = true;
                imgDisplayArea.Visible = true;
                FileUpload1.Enabled = true;
                btnUpload.Enabled = true;
                imgPopup.Enabled = true;
                ImageButton1.Enabled = true;
                ddlUserGrpName.Enabled = true;
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


    protected void btnUpload_Click(object sender, EventArgs e)
    {
       
        if (FileUpload1.HasFile)
        {
            try
            {
                if (FileUpload1.PostedFile.ContentType == "image/jpeg" || FileUpload1.PostedFile.ContentType == "image/png")
                {
                    imgDisplayArea.Visible = true;
                    
                    Label1.Text = "";
                    string filename = Path.GetFileName(FileUpload1.FileName);
                    FileUpload1.SaveAs(Server.MapPath("~/dist/img/") + filename);
                    imgDisplayArea.ImageUrl = "~/dist/img/" + filename;
                    ViewState["img"] = "~/dist/img/" + filename;

                }
                else
                {
                    Label1.Text = "Upload status: Only JPEG files are accepted!";
                }
            }

            catch (Exception ex)
            {
                Label1.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
            }
        }
        else
        {
            Label1.Text = "Please Select the file";
        }
    }
}

