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
    int DeleteDays, EditDays, AddDays, DateFrom;
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
            daterangeforfiscal(CalendarExtender1);
            dml.daterangeforfiscal(CalendarExtender1, Request.Cookies["fiscalyear"].Value);
            txtEntryDate.Attributes.Add("readonly", "readonly");
            Showall_Dml();

            // fd.HideUSerGrpField(Page.Controls, UserGrpID, FormID, "SET_Fiscal_Year");
            // Showall_Dml();

            //  dml.dropdownsql(lblmenu, "V_UserGrpFrm", "Menu_title", "Sno", "Menu_title");

            dml.dropdownsql(lblUserGrpName, "SET_UserGrp", "UserGrpName", "sno", "Record_Deleted", "0");
            dml.dropdownsql(lblmenu, "View_UserGrpMenu", "Menu_title", "Sno", "UserGrpName", lblUserGrpName.SelectedItem.Text);
            dml.dropdownsql(lblform, "SET_Form", "FormTitle", "Sno", "FormTitle");
            textClear();
          


        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        DataSet ds_usergrpname = dml.Find("select UserGrpId from SET_UserGrp where Sno= " + lblUserGrpName.SelectedItem.Value + "");
        string usergrpid = ds_usergrpname.Tables[0].Rows[0]["UserGrpId"].ToString();
        DataSet ds_userformid = dml.Find("select FormId from SET_Form where Sno = " + lblform.SelectedItem.Value + "");
        string formid = ds_userformid.Tables[0].Rows[0]["FormId"].ToString();

        DataSet ds_check = dml.Find("Select * from SET_UserGrp_Form where FormId = '" + formid + "' and UserGrpId = '" + usergrpid + "' and Record_Deleted = 0 ");
        if (ds_check.Tables[0].Rows.Count > 0)
        {
            textClear();
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = " This form already exist!!";
        }
        else {

            try
            {
                string add = "Y";
                string delete = "Y";
                string edit = "Y";
                string find = "Y";
                string view = "Y";

                if (add == "Y")
                {
                    add = "Y";
                }
                else
                {
                    add = "N";
                }
                if (edit == "Y")
                {
                    edit = "Y";
                }
                else
                {
                    edit = "N";
                }
                if (delete == "Y")
                {
                    delete = "Y";
                }
                else
                {
                    delete = "N";
                }
                if (find == "Y")
                {
                    find = "Y";
                }
                else
                {
                    find = "N";
                }
                if (view == "Y")
                {
                    view = "Y";
                }
                else
                {
                    view = "N";
                }

                userid = Request.QueryString["UserID"];
                int chk = 0;
                string dmlallow = "N";
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
                if (rdb_DML_N.Checked == true)
                {
                    dmlallow = "N";
                }
                if (rdb_DML_Y.Checked == true)
                {
                    dmlallow = "Y";
                }

                DataSet ds_usergrpname1 = dml.Find("select UserGrpId from SET_UserGrp where Sno= " + lblUserGrpName.SelectedItem.Value + "");
                string usergrpid1 = ds_usergrpname1.Tables[0].Rows[0]["UserGrpId"].ToString();

                DataSet ds_usermenuid = dml.Find("select MenuId from View_UserGrpMenu where Sno = " + lblmenu.SelectedItem.Value + "");
                string menuid = ds_usermenuid.Tables[0].Rows[0]["MenuId"].ToString();

                DataSet ds_userformid1 = dml.Find("select FormId from SET_Form where Sno = " + lblform.SelectedItem.Value + "");
                string formid1 = ds_userformid1.Tables[0].Rows[0]["FormId"].ToString();
                //DataSet ds_FEndDate = dml.Find("");


                dml.Update("UPDATE SET_Form SET [MenuId]= '" + menuid + "' where FormId = '" + formid + "';", "");
                dml.Insert("INSERT INTO SET_UserGrp_Form ( [UserGrpId], [EntryDate], [MenuId], [FormId], [DmlAllowed], [DateFrom], [Add], [AddDays], [Edit], [EditDays], [Delete], [DeleteDays], [Find], [View], [IsActive], [Hide], [SortOrder], [SysDate], [EnterUserId], [PEP_Exempt],[MLD]) VALUES ('" + usergrpid1 + "', '" + dml.dateconvertforinsert(txtEntryDate) + "', '" + menuid + "', '" + formid1 + "', '" + dmlallow + "', '3', '" + add + "', '" + txtadddays.Text + "', '" + edit + "', '" + txtEditDays.Text + "', '" + delete + "', '" + txtDelDays.Text + "', '" + find + "', '" + view + "', '" + chk + "', '" + hide + "', '" + txtSortOrder.Text + "', '" + DateTime.Now + "', '" + userid + "', NULL,'"+dml.Encrypt("h")+"');", "");
                

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
        UserGrpID = Request.QueryString["UsergrpID"];
        FormID = Request.QueryString["FormID"];
        Showall_Dml();
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
        lblmenu.Enabled = true;
        lblform.Enabled = true;
        lblEntryUSer_Name.Enabled = true;
        lblUserGrpName.Enabled = true;
        lblSystemDate.Enabled = true;
        txtEntryDate.Enabled = true;
        txtSortOrder.Enabled = true;
        txtadddays.Enabled = true;
        txtEditDays.Enabled = true;
        txtDelDays.Enabled = true;
        chkActive_Status.Enabled = true;
        chk_R_Add.Enabled = true;
        chk_R_Delete.Enabled = true;
        chk_R_Edit.Enabled = true;
        chk_R_Find.Enabled = true;
        chk_R_View.Enabled = true;
        rdb_DML_N.Enabled = true;
        rdb_DML_Y.Enabled = true;
        rdb_Hide_N.Enabled = true;
        rdb_Hide_Y.Enabled = true;
        imgPopup.Enabled = true;

        chk_R_Add.Checked = true;
        chk_R_Delete.Checked = true;
        chk_R_Edit.Checked = true;
        chk_R_Find.Checked = true;
        chk_R_View.Checked = true;

        rdb_DML_Y.Checked = true;
        rdb_Hide_N.Checked = true;
        chkActive_Status.Checked = true;

        lblSystemDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff ");
        lblEntryUSer_Name.Text = show_username();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        try
        {
            string ed = txtEntryDate.Text;
           
            string add = "N";
            string delete = "N";
            string edit = "N";
            string find = "N";
            string view = "N";

            if (chk_R_Add.Checked == true)
            {
                add = "Y";
            }
            else
            {
                add = "N";
            }
            if (chk_R_Edit.Checked == true)
            {
                edit = "Y";
            }
            else
            {
                edit = "N";
            }
            if (chk_R_Delete.Checked == true)
            {
                delete = "Y";
            }
            else
            {
                delete = "N";
            }
            if (chk_R_Find.Checked == true)
            {
                find = "Y";
            }
            else
            {
                find = "N";
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
            string dmlallow = "N";
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
            if (rdb_DML_N.Checked == true)
            {
                dmlallow = "N";
            }
            if (rdb_DML_Y.Checked == true)
            {
                dmlallow = "Y";
            }
            
            DataSet ds_usergrpname = dml.Find("select UserGrpId from SET_UserGrp where Sno= " + lblUserGrpName.SelectedItem.Value + "");
            string usergrpid = ds_usergrpname.Tables[0].Rows[0]["UserGrpId"].ToString();
            DataSet ds_usermenuid = dml.Find("select MenuId from SET_Menu where Sno = " + lblmenu.SelectedItem.Value + "");
            string menuid = ds_usermenuid.Tables[0].Rows[0]["MenuId"].ToString();
            DataSet ds_userformid = dml.Find("select FormId from SET_Form where Sno = " + lblform.SelectedItem.Value + "");
            string formid = ds_userformid.Tables[0].Rows[0]["FormId"].ToString();
            //DataSet ds_FEndDate = dml.Find("");


            string edp;
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
           

            DataSet ds_up = dml.Find("SELECT * FROM SET_UserGrp_Form  WHERE ([Sno]='"+ViewState["sno"].ToString()+"') AND ([UserGrpId]='"+usergrpid+"') AND " + edp + " AND ([MenuId]='" +menuid+"') AND ([FormId]='"+formid+"') AND ([DmlAllowed]='"+dmlallow+"') AND  ([Add]='"+add+"') AND ([AddDays]='"+txtadddays.Text+"') AND ([Edit]='"+edit+"') AND ([EditDays]='"+txtEditDays.Text+"') AND ([Delete]='"+delete+"') AND ([DeleteDays]='"+txtDelDays.Text+"') AND ([Find]='"+find+"') AND ([View]='"+view+"') AND ([IsActive]='"+chk+"') AND ([Hide]='"+hide+"') AND ([SortOrder]='"+txtSortOrder.Text+"')");

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

                dml.Update("UPDATE SET_Form SET [MenuId]= '" + menuid + "' where FormId = '" + formid + "';", "");
                dml.Update("UPDATE SET_UserGrp_Form SET [UserGrpId]= '" + usergrpid + "', "+ ed + ", [MenuId]='" + menuid + "', [FormId]='" + formid + "', [DmlAllowed]='" + dmlallow + "', [DateFrom]='3', [Add]='" + add + "', [AddDays]='" + txtadddays.Text + "', [Edit]='" + edit + "', [EditDays]='" + txtEditDays.Text + "', [Delete]='" + delete + "', [DeleteDays]='" + txtDelDays.Text + "', [Find]='" + find + "', [View]='" + view + "', [IsActive]='" + chk + "', [Hide]='" + hide + "', [SortOrder]='" + txtSortOrder.Text + "', [SysDate]='" + DateTime.Now + "', [EnterUserId]='" + userid + "', [PEP_Exempt]='Y' where Sno = '" + ViewState["sno"].ToString() + "'", "");
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
            dml.Delete("update SET_UserGrp_Form set Record_Deleted = 1 where Sno =  " + ViewState["sno"].ToString() + "", "");
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
        dml.dropdownsql(lblmenu, "View_UserGrpMenu", "Menu_title", "Sno", "UserGrpName", lblUserGrpName.SelectedItem.Text);
        dml.dropdownsql(lblUserGrpName, "SET_UserGrp", "UserGrpName", "sno", "Record_Deleted", "0");
        dml.dropdownsql(lblform, "SET_Form", "FormTitle", "Sno", "FormTitle");
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
            UserGrpID = Request.QueryString["UsergrpID"];


            string squer = "select * from V_UserGrpFrm ";

            if (txtEdit_FormTitle.Text != "")
            {
                swhere = "FormTitle like '" + txtEdit_FormTitle.Text+"%'";
            }
            else
            {
                swhere = "FormTitle is not null";
            }
            if (txtEdit_MenuTitle.Text != "")
            {
                swhere = swhere + " and Menu_title like '" + txtEdit_MenuTitle.Text + "%'";
            }
            else
            {
                swhere = swhere + " and Menu_title is not null";
            }
            if (txtEdit_UserGrpName.Text != "")
            {
                swhere = swhere + " and UserGrpName like '" + txtEdit_UserGrpName.Text + "%'";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY Sno";



            Findbox.Visible = false;
            Deletebox.Visible = false;
            Editbox.Visible = true;
            fieldbox.Visible = false;

            btnInsert.Visible = false;
            btnEdit.Visible = true;
            btnDelete.Visible = false;
            btnDelete_after.Visible = false;
            btnFind.Visible = false;
            btnCancel.Visible = true;

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
            string swhere;
            UserGrpID = Request.QueryString["UsergrpID"];


            string squer = "select * from V_UserGrpFrm ";

            if (txtFind_FormTitle.Text != "")
            {
                swhere = "FormTitle like '" + txtFind_FormTitle.Text + "%'";
            }
            else
            {
                swhere = "FormTitle is not null";
            }
            if (txtFind_MenuTitle.Text != "")
            {
                swhere = swhere + " and Menu_title like '" + txtFind_MenuTitle.Text + "%'";
            }
            else
            {
                swhere = swhere + " and Menu_title is not null";
            }
            if (txtFind_UserGrpName.Text != "")
            {
                swhere = swhere + " and UserGrpName like '" + txtFind_UserGrpName.Text + "%'";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY Sno";

            Findbox.Visible = true;
            Deletebox.Visible = false;
            Editbox.Visible = false;
            fieldbox.Visible = false;


            btnInsert.Visible = false;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            btnFind.Visible = true;
            btnCancel.Visible = true;

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


            string squer = "select * from V_UserGrpFrm ";

            if (txtDel_FormTitle.Text != "")
            {
                swhere = "FormTitle like '" + txtDel_FormTitle.Text + "%'";
            }
            else
            {
                swhere = "FormTitle is not null";
            }
            if (txtDel_MenuTitle.Text != "")
            {
                swhere = swhere + " and Menu_title like '" + txtDel_MenuTitle.Text + "%'";
            }
            else
            {
                swhere = swhere + " and Menu_title is not null";
            }
            if (txtDel_UsergrpName.Text != "")
            {
                swhere = swhere + " and UserGrpName like '" + txtDel_UsergrpName.Text + "%'";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY Sno";

            Findbox.Visible = false;
            Deletebox.Visible = true;
            Editbox.Visible = false;
            fieldbox.Visible = false;

            btnInsert.Visible = false;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            btnDelete_after.Visible = true;
            btnFind.Visible = false;
            btnCancel.Visible = true;

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
        //lblUserGrpName.SelectedIndex = 0;
       // lblform.SelectedIndex = 0;
        //lblmenu.SelectedIndex = 0;
        lblEntryUSer_Name.Text = "";
        txtSortOrder.Text = "";
        lblSystemDate.Text = "";
        txtEntryDate.Text = "";
        rdb_Hide_N.Checked = false;
        rdb_Hide_Y.Checked = false;
        rdb_DML_N.Checked = false;
        rdb_DML_Y.Checked = false;
        Label1.Text = "";

        chk_R_Add.Checked = false;
        chk_R_Delete.Checked = false;
        chk_R_Edit.Checked = false;
        chk_R_Find.Checked = false;
        chk_R_View.Checked = false;

        chkActive_Status.Checked = false;
        txtadddays.Text = "";
        txtEditDays.Text = "";
        txtDelDays.Text = "";

        txtadddays.Enabled = false;
        txtEditDays.Enabled = false;
        txtDelDays.Enabled = false;

        lblmenu.Enabled = false;
        lblform.Enabled = false;
        lblEntryUSer_Name.Enabled = false;
        lblUserGrpName.Enabled = false;
        lblSystemDate.Enabled = false;
        txtEntryDate.Enabled = false;
        txtSortOrder.Enabled = false;
        imgPopup.Enabled = false;

        chkActive_Status.Enabled = false;
        chk_R_Add.Enabled = false;
        chk_R_Delete.Enabled = false;
        chk_R_Edit.Enabled = false;
        chk_R_Find.Enabled = false;
        chk_R_View.Enabled = false;
        rdb_DML_N.Enabled = false;
        rdb_DML_Y.Enabled = false;
        rdb_Hide_N.Enabled = false;
        rdb_Hide_Y.Enabled = false;
        


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
        //SqlDataAdapter da = new SqlDataAdapter("select * from SET_UserGrp_Form where FormId='" + FormID + "' and DmlAllowed = 'Y' and UserGrpId = '"+UserGrpID+ "' and Record_Deleted = '0' ", con);
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
            DataSet ds = dml.Find("select * from View_UserGrpFrm where Sno = " + serialno);

            if (ds.Tables[0].Rows.Count > 0)

            {
                DataSet dds = dml.Find("select * from SET_UserGrp_Form where Sno = " + serialno);

                string idd = dds.Tables[0].Rows[0]["FormId"].ToString();
                DataSet dform = dml.Find("select *  from SET_Form where FormId= '" + idd + "' and Record_Deleted='1'");
                if (dform.Tables[0].Rows.Count > 0)
                {
                    Label1.Text = "'"+dform.Tables[0].Rows[0]["FormTitle"].ToString() +"' form does not exist in setup from";
                }
                else {

                    lblUserGrpName.SelectedItem.Text = ds.Tables[0].Rows[0]["UserGrpName"].ToString();
                    lblUserGrpName_SelectedIndexChanged(sender, e);
                    lblmenu.SelectedItem.Text = ds.Tables[0].Rows[0]["Menu_title"].ToString();
                    lblform.SelectedItem.Text = ds.Tables[0].Rows[0]["FormTitle"].ToString();
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["SysDate"].ToString());
                    txtEntryDate.Text = dds.Tables[0].Rows[0]["EntryDate"].ToString();


                    txtadddays.Text = dds.Tables[0].Rows[0]["AddDays"].ToString();
                    txtEditDays.Text = dds.Tables[0].Rows[0]["EditDays"].ToString();
                    txtDelDays.Text = dds.Tables[0].Rows[0]["DeleteDays"].ToString();

                    dml.dateConvert(txtEntryDate);
                    bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                    string hide = dds.Tables[0].Rows[0]["Hide"].ToString();
                    string dML = ds.Tables[0].Rows[0]["DmlAllowed"].ToString();

                    string add = ds.Tables[0].Rows[0]["Add"].ToString();
                    string delete = ds.Tables[0].Rows[0]["Delete"].ToString();
                    string edit = ds.Tables[0].Rows[0]["Edit"].ToString();
                    string find = dds.Tables[0].Rows[0]["Find"].ToString();
                    string view = dds.Tables[0].Rows[0]["View"].ToString();


                    lblSystemDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

                    if (dML == "Y")
                    {
                        rdb_DML_Y.Checked = true;
                        chk_R_Add.Enabled = true;
                        chk_R_Delete.Enabled = true;
                        chk_R_Edit.Enabled = true;
                        chk_R_Find.Enabled = true;
                        chk_R_View.Enabled = true;
                        if (add == "Y")
                        {
                            chk_R_Add.Checked = true;
                        }
                        else
                        {
                            chk_R_Add.Checked = false;
                        }
                        if (edit == "Y")
                        {
                            chk_R_Edit.Checked = true;
                        }
                        else
                        {
                            chk_R_Edit.Checked = false;
                        }
                        if (delete == "Y")
                        {
                            chk_R_Delete.Checked = true;
                        }
                        else
                        {
                            chk_R_Delete.Checked = false;
                        }
                        if (find == "Y")
                        {
                            chk_R_Find.Checked = true;
                        }
                        else
                        {
                            chk_R_Find.Checked = false;
                        }
                        if (view == "Y")
                        {
                            chk_R_View.Checked = true;
                        }
                        else
                        {
                            chk_R_View.Checked = false;
                        }
                    }
                    else
                    {
                        rdb_DML_N.Checked = false;
                        chk_R_Add.Enabled = false;
                        chk_R_Delete.Enabled = false;
                        chk_R_Edit.Enabled = false;
                        chk_R_Find.Enabled = false;
                        chk_R_View.Enabled = false;

                        chk_R_Add.Checked = false;
                        chk_R_Delete.Checked = false;
                        chk_R_Edit.Checked = false;
                        chk_R_Find.Checked = false;
                        chk_R_View.Checked = false;

                    }


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

                    txtSortOrder.Text = dds.Tables[0].Rows[0]["SortOrder"].ToString();
                    string entruserid = dds.Tables[0].Rows[0]["EnterUserId"].ToString();
                    DataSet ddss = dml.Find("Select user_name from set_user_manager where userid= '" + entruserid + "'");
                    lblEntryUSer_Name.Text = ddss.Tables[0].Rows[0]["user_name"].ToString();
                    lblmenu.Enabled = false;
                    lblform.Enabled = false;
                    lblEntryUSer_Name.Enabled = false;
                    lblUserGrpName.Enabled = false;
                    lblSystemDate.Enabled = false;
                    txtEntryDate.Enabled = false;
                    txtSortOrder.Enabled = false;

                    chkActive_Status.Enabled = false;
                    chk_R_Add.Enabled = false;
                    chk_R_Delete.Enabled = false;
                    chk_R_Edit.Enabled = false;
                    chk_R_Find.Enabled = false;
                    chk_R_View.Enabled = false;
                    rdb_DML_N.Enabled = false;
                    rdb_DML_Y.Enabled = false;
                    rdb_Hide_N.Enabled = false;
                    rdb_Hide_Y.Enabled = false;

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
            ViewState["sno"] = serialno;
            DataSet ds = dml.Find("select * from View_UserGrpFrm where Sno = " + serialno);

            if (ds.Tables[0].Rows.Count > 0)

            {
                DataSet dds = dml.Find("select * from SET_UserGrp_Form where Sno = " + serialno);

                lblUserGrpName.SelectedItem.Text = ds.Tables[0].Rows[0]["UserGrpName"].ToString();
                lblUserGrpName_SelectedIndexChanged(sender, e);
                lblmenu.SelectedItem.Text = ds.Tables[0].Rows[0]["Menu_title"].ToString();
                lblform.SelectedItem.Text = ds.Tables[0].Rows[0]["FormTitle"].ToString();
                DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["SysDate"].ToString());
                txtEntryDate.Text = dds.Tables[0].Rows[0]["EntryDate"].ToString();


                txtadddays.Text = dds.Tables[0].Rows[0]["AddDays"].ToString();
                txtEditDays.Text = dds.Tables[0].Rows[0]["EditDays"].ToString();
                txtDelDays.Text = dds.Tables[0].Rows[0]["DeleteDays"].ToString();


                dml.dateConvert(txtEntryDate);
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                string hide = dds.Tables[0].Rows[0]["Hide"].ToString();
                string dML = ds.Tables[0].Rows[0]["DmlAllowed"].ToString();

                string add = ds.Tables[0].Rows[0]["Add"].ToString();
                string delete = ds.Tables[0].Rows[0]["Delete"].ToString();
                string edit = ds.Tables[0].Rows[0]["Edit"].ToString();
                string find = dds.Tables[0].Rows[0]["Find"].ToString();
                string view = dds.Tables[0].Rows[0]["View"].ToString();
                

                lblSystemDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

                if (dML == "Y")
                {
                    rdb_DML_Y.Checked = true;
                    chk_R_Add.Enabled = true;
                    chk_R_Delete.Enabled = true;
                    chk_R_Edit.Enabled = true;
                    chk_R_Find.Enabled = true;
                    chk_R_View.Enabled = true;
                    if (add == "Y")
                    {
                        chk_R_Add.Checked = true;
                    }
                    else
                    {
                        chk_R_Add.Checked = false;
                    }
                    if (edit == "Y")
                    {
                        chk_R_Edit.Checked = true;
                    }
                    else
                    {
                        chk_R_Edit.Checked = false;
                    }
                    if (delete == "Y")
                    {
                        chk_R_Delete.Checked = true;
                    }
                    else
                    {
                        chk_R_Delete.Checked = false;
                    }
                    if (find == "Y")
                    {
                        chk_R_Find.Checked = true;
                    }
                    else
                    {
                        chk_R_Find.Checked = false;
                    }
                    if (view == "Y")
                    {
                        chk_R_View.Checked = true;
                    }
                    else
                    {
                        chk_R_View.Checked = false;
                    }
                }
                else
                {
                    rdb_DML_N.Checked = false;
                    chk_R_Add.Enabled = false;
                    chk_R_Delete.Enabled = false;
                    chk_R_Edit.Enabled = false;
                    chk_R_Find.Enabled = false;
                    chk_R_View.Enabled = false;

                    chk_R_Add.Checked = false;
                    chk_R_Delete.Checked = false;
                    chk_R_Edit.Checked = false;
                    chk_R_Find.Checked = false;
                    chk_R_View.Checked = false;

                }


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

                txtSortOrder.Text = dds.Tables[0].Rows[0]["SortOrder"].ToString();
                string entruserid = dds.Tables[0].Rows[0]["EnterUserId"].ToString();
                DataSet ddss = dml.Find("Select user_name from set_user_manager where userid= '" + entruserid + "'");
                lblEntryUSer_Name.Text = ddss.Tables[0].Rows[0]["user_name"].ToString();
                lblmenu.Enabled = false;
                lblform.Enabled = false;
                lblEntryUSer_Name.Enabled = false;
                lblUserGrpName.Enabled = false;
                lblSystemDate.Enabled = false;
                txtEntryDate.Enabled = false;
                txtSortOrder.Enabled = false;

                chkActive_Status.Enabled = false;
                chk_R_Add.Enabled = false;
                chk_R_Delete.Enabled = false;
                chk_R_Edit.Enabled = false;
                chk_R_Find.Enabled = false;
                chk_R_View.Enabled = false;
                rdb_DML_N.Enabled = false;
                rdb_DML_Y.Enabled = false;
                rdb_Hide_N.Enabled = false;
                rdb_Hide_Y.Enabled = false;

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
            ViewState["sno"] = serialno;
            DataSet ds = dml.Find("select * from V_UserGrpFrm where Sno = " + serialno);
           
            if (ds.Tables[0].Rows.Count > 0)

            {
                DataSet dds = dml.Find("select * from SET_UserGrp_Form where Sno = " + serialno);

                // string defaultText = ds.Tables[0].Rows[0]["UserGrpName"].ToString();
                lblUserGrpName.ClearSelection();
                lblmenu.ClearSelection();
                lblform.ClearSelection();
                //drpFunction.Items.FindByText(t).Selected = true;
                lblUserGrpName.Items.FindByText(ds.Tables[0].Rows[0]["UserGrpName"].ToString()).Selected = true;
                lblUserGrpName_SelectedIndexChanged(sender, e);
                lblmenu.Items.FindByText(ds.Tables[0].Rows[0]["Menu_title"].ToString()).Selected = true;
                lblform.Items.FindByText(ds.Tables[0].Rows[0]["FormTitle"].ToString()).Selected = true;

                txtadddays.Text = dds.Tables[0].Rows[0]["AddDays"].ToString();
                txtEditDays.Text = dds.Tables[0].Rows[0]["EditDays"].ToString();
                txtDelDays.Text = dds.Tables[0].Rows[0]["DeleteDays"].ToString();

                // lblUserGrpName.SelectedItem.Text = ds.Tables[0].Rows[0]["UserGrpName"].ToString();
                //lblmenu.SelectedItem.Text = ds.Tables[0].Rows[0]["Menu_title"].ToString();
                //lblform.SelectedItem.Text = ds.Tables[0].Rows[0]["FormTitle"].ToString();
                DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["SysDate"].ToString());
                txtEntryDate.Text = dds.Tables[0].Rows[0]["EntryDate"].ToString();

                dml.dateConvert(txtEntryDate);
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                string hide = dds.Tables[0].Rows[0]["Hide"].ToString();
                string dML = ds.Tables[0].Rows[0]["DmlAllowed"].ToString();

                string add = ds.Tables[0].Rows[0]["Add"].ToString();
                string delete = ds.Tables[0].Rows[0]["Delete"].ToString();
                string edit = ds.Tables[0].Rows[0]["Edit"].ToString();
                string find = dds.Tables[0].Rows[0]["Find"].ToString();
                string view = dds.Tables[0].Rows[0]["View"].ToString();


                lblSystemDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

                if (dML == "Y")
                {
                    rdb_DML_Y.Checked = true;
                    chk_R_Add.Enabled = true;
                    chk_R_Delete.Enabled = true;
                    chk_R_Edit.Enabled = true;
                    chk_R_Find.Enabled = true;
                    chk_R_View.Enabled = true;
                    if (add == "Y")
                    {
                        chk_R_Add.Checked = true;
                    }
                    else
                    {
                        chk_R_Add.Checked = false;
                    }
                    if (edit == "Y")
                    {
                        chk_R_Edit.Checked = true;
                    }
                    else
                    {
                        chk_R_Edit.Checked = false;
                    }
                    if (delete == "Y")
                    {
                        chk_R_Delete.Checked = true;
                    }
                    else
                    {
                        chk_R_Delete.Checked = false;
                    }
                    if (find == "Y")
                    {
                        chk_R_Find.Checked = true;
                    }
                    else
                    {
                        chk_R_Find.Checked = false;
                    }
                    if (view == "Y")
                    {
                        chk_R_View.Checked = true;
                    }
                    else
                    {
                        chk_R_View.Checked = false;
                    }
                }
                else
                {
                    rdb_DML_N.Checked = false;
                    chk_R_Add.Enabled = false;
                    chk_R_Delete.Enabled = false;
                    chk_R_Edit.Enabled = false;
                    chk_R_Find.Enabled = false;
                    chk_R_View.Enabled = false;

                    chk_R_Add.Checked = false;
                    chk_R_Delete.Checked = false;
                    chk_R_Edit.Checked = false;
                    chk_R_Find.Checked = false;
                    chk_R_View.Checked = false;

                }


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

                txtSortOrder.Text = dds.Tables[0].Rows[0]["SortOrder"].ToString();
                string entruserid = dds.Tables[0].Rows[0]["EnterUserId"].ToString();
                DataSet ddss = dml.Find("Select user_name from set_user_manager where userid= '" + entruserid + "'");
                lblEntryUSer_Name.Text = ddss.Tables[0].Rows[0]["user_name"].ToString();

                
                lblmenu.Enabled = true;
                lblform.Enabled = false;
                lblEntryUSer_Name.Enabled = true;
                lblUserGrpName.Enabled = false;
                lblSystemDate.Enabled = true;
                txtEntryDate.Enabled = true;
                txtSortOrder.Enabled = true;
                txtadddays.Enabled = true;
                txtEditDays.Enabled = true;
                txtDelDays.Enabled = true;

                chkActive_Status.Enabled = true;
                chk_R_Add.Enabled = true;
                chk_R_Delete.Enabled = true;
                chk_R_Edit.Enabled = true;
                chk_R_Find.Enabled = true;
                chk_R_View.Enabled = true;
                rdb_DML_N.Enabled = true;
                rdb_DML_Y.Enabled = true;
                rdb_Hide_N.Enabled = true;
                rdb_Hide_Y.Enabled = true;
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


    protected void lblUserGrpName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //select Menu_title, Serial from View_UserGrpMenu where UserGrpName = 'admin'
        dml.dropdownsql(lblmenu, "View_UserGrpMenu", "Menu_title", "Sno", "UserGrpName",lblUserGrpName.SelectedItem.Text);
    }
    public void daterangeforfiscal(AjaxControlToolkit.CalendarExtender cal1)
    {
        DataSet ds = dml.Find("select StartDate, EndDate from SET_Fiscal_Year where Description = '" + Request.Cookies["fiscalYear"].Value + "'");
        if (ds.Tables[0].Rows.Count > 0)
        {
            string startdate1 = ds.Tables[0].Rows[0]["StartDate"].ToString();
            string enddate1 = ds.Tables[0].Rows[0]["EndDate"].ToString();

            DateTime startdate = DateTime.Parse(startdate1);
            DateTime enddate = DateTime.Parse(enddate1);

            cal1.StartDate = startdate;
            cal1.EndDate = enddate;


        }
    }
}