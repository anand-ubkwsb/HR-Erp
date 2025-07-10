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
            txtDateFrom.Attributes.Add("readonly", "readonly");
            txtdateTo1.Attributes.Add("readonly", "readonly");
            Showall_Dml();

            // fd.HideUSerGrpField(Page.Controls, UserGrpID, FormID, "SET_Fiscal_Year");
            // Showall_Dml();

            //  dml.dropdownsql(lblmenu, "V_UserGrpFrm", "Menu_title", "Sno", "Menu_title");

            dml.dropdownsql(lblUserGrpName, "SET_UserGrp", "UserGrpName", "UserGrpId", "Record_Deleted", "0");
            dml.dropdownsql(lblmenu, "View_UserGrpMenu", "Menu_title", "Sno", "UserGrpName", lblUserGrpName.SelectedItem.Text);
            dml.dropdownsql(lblform, "SET_Form", "FormTitle", "Sno", "FormTitle");
            textClear();


            

            dml.dropdownsql(txtFind_MenuTitle, "SET_Menu", "Menu_title", "Sno");
            dml.dropdownsql(txtEdit_MenuTitle, "SET_Menu", "Menu_title", "Sno");
            dml.dropdownsql(txtDel_MenuTitle, "SET_Menu", "Menu_title", "Sno");
            

                

            dml.dropdownsql(txtFind_FormTitle, "SET_Form", "FormTitle", "Sno");
            dml.dropdownsql(txtEdit_FormTitle, "SET_Form", "FormTitle", "Sno");
            dml.dropdownsql(txtDel_FormTitle, "SET_Form", "FormTitle", "Sno");

            dml.dropdownsql(txtFind_UserGrpName, "SET_UserGrp", "UserGrpName", "sno", "Record_Deleted", "0");
            dml.dropdownsql(txtEdit_UserGrpName, "SET_UserGrp", "UserGrpName", "sno", "Record_Deleted", "0");
            dml.dropdownsql(txtDel_UsergrpName, "SET_UserGrp", "UserGrpName", "sno", "Record_Deleted", "0");
       
            datamenu_view();
            datacall();
            selectcheck();

        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
       
        //DataSet ds_usergrpname = dml.Find("select UserGrpId from SET_UserGrp where Sno= " + lblUserGrpName.SelectedItem.Value + "");
        //string usergrpid = ds_usergrpname.Tables[0].Rows[0]["UserGrpId"].ToString();
        DataSet ds_userformid = dml.Find("select FormId from SET_Form where Sno = " + lblform.SelectedItem.Value + "");
        string formid = ds_userformid.Tables[0].Rows[0]["FormId"].ToString();

        DataSet ds_check = dml.Find("Select * from SET_UserGrp_Form where FormId = '" + formid + "' and UserGrpId = '" + lblUserGrpName.SelectedItem.Value + "' and Record_Deleted = 0 ");
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
                string todate = txtdateTo1.Text;

                if (todate == "")
                {
                    todate = "NULL";
                    //    todateP = "ToDate IS NULL";
                }
                else
                {
                    todate = "'" + dml.dateconvertforinsertNEW(txtdateTo1) + "'";
                }
                    //  todateP = "([ToDate] = '" + txtEntryDate.Text + "')";

                    //DataSet ds_usergrpname1 = dml.Find("select UserGrpId from SET_UserGrp where Sno= " + lblUserGrpName.SelectedItem.Value + "");
                    //string usergrpid1 = ds_usergrpname1.Tables[0].Rows[0]["UserGrpId"].ToString();

                    DataSet ds_usermenuid = dml.Find("select MenuId from View_UserGrpMenu where Sno = " + lblmenu.SelectedItem.Value + "");
                    string menuid = ds_usermenuid.Tables[0].Rows[0]["MenuId"].ToString();

                    DataSet ds_userformid1 = dml.Find("select FormId,Sno from SET_Form where Sno = " + lblform.SelectedItem.Value + "");
                    string formid1 = ds_userformid1.Tables[0].Rows[0]["FormId"].ToString();
                    string snoFid = ds_userformid1.Tables[0].Rows[0]["Sno"].ToString();
                    //DataSet ds_FEndDate = dml.Find("");


                    dml.Update("UPDATE SET_Form SET [MenuId]= '" + menuid + "' where FormId = '" + formid + "';", "");
                    dml.Insert("INSERT INTO SET_UserGrp_Form ( [UserGrpId],[FromDate],[ToDate],[EntryDate], [MenuId], [FormId], [DmlAllowed], [DateFrom], [Add], [AddDays], [Edit], [EditDays], [Delete], [DeleteDays], [Find], [View], [IsActive], [Hide], [SortOrder], [SysDate], [EnterUserId], [PEP_Exempt],[Sno1]) VALUES ('" + lblUserGrpName.SelectedItem.Value + "', '" + dml.dateconvertforinsertNEW(txtDateFrom) + "' , " + todate + ",'" + dml.dateconvertforinsert(txtEntryDate) + "', '" + menuid + "', '" + formid1 + "', '" + dmlallow + "', '3', '" + add + "', '" + txtadddays.Text + "', '" + edit + "', '" + txtEditDays.Text + "', '" + delete + "', '" + txtDelDays.Text + "', '" + find + "', '" + view + "', '" + chk + "', '" + hide + "', '" + txtSortOrder.Text + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '" + userid + "', NULL,'" + snoFid + "');", "");
                    dml.Update("update Set_Form set MLD = '" + dml.Encrypt("q") + "' where Sno = '" + lblform.SelectedItem.Value + "'", "");


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
        datamenu_view();
        datacall();
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
        txtDateFrom.Enabled = true;
        txtdateTo1.Enabled = true;

        chk_R_Add.Checked = true;
        chk_R_Delete.Checked = true;
        chk_R_Edit.Checked = true;
        chk_R_Find.Checked = true;
        chk_R_View.Checked = true;

        rdb_DML_Y.Checked = true;
        rdb_Hide_N.Checked = true;
        chkActive_Status.Checked = true;
        Div1.Visible = true;
        lblSystemDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff ");
        lblEntryUSer_Name.Text = show_username();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string a = txtDateFrom.Text;
        string datefrom = txtDateFrom.Text;
        string todate = txtdateTo1.Text;
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

            //DataSet ds_usergrpname = dml.Find("select UserGrpId from SET_UserGrp where Sno= " + lblUserGrpName.SelectedItem.Value + "");
            //string usergrpid = ds_usergrpname.Tables[0].Rows[0]["UserGrpId"].ToString();
            DataSet ds_usermenuid = dml.Find("select MenuId from SET_Menu where Sno = " + lblmenu.SelectedItem.Value + "");
            string menuid = ds_usermenuid.Tables[0].Rows[0]["MenuId"].ToString();
            DataSet ds_userformid = dml.Find("select FormId from SET_Form where Sno = " + lblform.SelectedItem.Value + "");
            string formid = ds_userformid.Tables[0].Rows[0]["FormId"].ToString();
            //DataSet ds_FEndDate = dml.Find("");


            string edp;
            string todateP;
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

            if (todate == "")
            {
                todate = "ToDate = NULL";
                todateP = "ToDate IS NULL";
            }
            else
            {
                todate = "ToDate = '" + txtdateTo1.Text + "'";
                todateP = "([ToDate] = '" + dml.dateconvertforinsertNEW(txtdateTo1) + "')";
            }


            DataSet ds_up = dml.Find("SELECT * FROM SET_UserGrp_Form  WHERE ([Sno1]='" + ViewState["sno"].ToString() + "') AND ([UserGrpId]='" + lblUserGrpName.SelectedItem.Value + "') AND " + edp + "  AND "+ todateP+" AND ([MenuId]='" + menuid + "') AND ([FormId]='" + formid + "') AND ([DmlAllowed]='" + dmlallow + "') AND  ([Add]='" + add + "') AND ([AddDays]='" + txtadddays.Text + "') AND ([Edit]='" + edit + "') AND ([EditDays]='" + txtEditDays.Text + "') AND ([Delete]='" + delete + "') AND ([DeleteDays]='" + txtDelDays.Text + "') AND ([Find]='" + find + "') AND ([View]='" + view + "') AND ([IsActive]='" + chk + "') AND ([Hide]='" + hide + "') AND ([SortOrder]='" + txtSortOrder.Text + "')");

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
                dml.Update("UPDATE SET_UserGrp_Form SET [UserGrpId]= '" + lblUserGrpName.SelectedItem.Value + "', " + ed + ", [MenuId]='" + menuid + "', [FormId]='" + formid + "', [DmlAllowed]='" + dmlallow + "', [DateFrom]='3', [Add]='" + add + "', [AddDays]='" + txtadddays.Text + "', [Edit]='" + edit + "', [EditDays]='" + txtEditDays.Text + "', [Delete]='" + delete + "', [DeleteDays]='" + txtDelDays.Text + "', [Find]='" + find + "', [View]='" + view + "', [IsActive]='" + chk + "', [Hide]='" + hide + "', [SortOrder]='" + txtSortOrder.Text + "', [SysDate]='" + DateTime.Now + "', [EnterUserId]='" + userid + "', [PEP_Exempt]='Y', [FromDate] = '" + dml.dateconvertforinsertNEW(txtDateFrom) + "', "+ todate + ", [UpdatedDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "',[UpdatedBy]='" + show_username() +"' where Sno = '" + ViewState["sno"].ToString() + "'", "");
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
        dml.dropdownsql(lblUserGrpName, "SET_UserGrp", "UserGrpName", "UserGrpId", "Record_Deleted", "0");
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

            if (txtEdit_FormTitle.SelectedIndex != 0)
            {
                swhere = "FormTitle = '" + txtEdit_FormTitle.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "FormTitle is not null";
            }
            if (txtEdit_MenuTitle.SelectedIndex != 0)
            {
                swhere = swhere + " and Menu_title = '" + txtEdit_MenuTitle.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and Menu_title is not null";
            }
            if (txtEdit_UserGrpName.SelectedIndex != 0)
            {
                swhere = swhere + " and UserGrpName = '" + txtEdit_UserGrpName.SelectedItem.Text + "'";
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

            //and (EntryDate > '" + stdate + "' and EntryDate < '" + enddate + "')
            squer = squer + " where " + swhere + " and Record_Deleted = 0   ORDER BY FormTitle";

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

            if (txtFind_FormTitle.SelectedIndex != 0)
            {
                swhere = "FormTitle = '" + txtFind_FormTitle.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "FormTitle is not null";
            }
            if (txtFind_MenuTitle.SelectedIndex != 0)
            {
                swhere = swhere + " and Menu_title = '" + txtFind_MenuTitle.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and Menu_title is not null";
            }
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

            //and (EntryDate > '" + stdate + "' and EntryDate <= '" + enddate + "') 
            squer = squer + " where " + swhere + " and Record_Deleted = 0  ORDER BY FormTitle";


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

            if (txtDel_FormTitle.SelectedIndex != 0)
            {
                swhere = "FormTitle = '" + txtDel_FormTitle.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "FormTitle is not null";
            }
            if (txtDel_MenuTitle.SelectedIndex != 0)
            {
                swhere = swhere + " and Menu_title = '" + txtDel_MenuTitle.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and Menu_title is not null";
            }
            if (txtDel_UsergrpName.SelectedIndex != 0)
            {
                swhere = swhere + " and UserGrpName = '" + txtDel_UsergrpName.SelectedItem.Text + "'";
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

            // and (EntryDate > '" + stdate + "' and EntryDate < '" + enddate + "') 
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY FormTitle";


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
        txtDateFrom.Enabled = false;
        txtdateTo1.Enabled = false;

        txtDateFrom.Text = "";
        txtdateTo1.Text = "";

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
        Div1.Visible = false;
        ImageButton3.Visible = false;


    }
    public void Showall_Dml()
    {
        userid = Request.QueryString["UserID"];
        FormID = Request.QueryString["FormID"];
        DataSet FiscalStatus;
        con.Open();
        string Query = "SELECT * FROM SET_FISCAL_YEAR WHERE FISCALYEARID=" + Convert.ToInt32(Request.Cookies["FiscalYearId"].Value);
        DataSet Fiscal = dml.Find(Query);
        string FiscalStatusQuery = "SELECT * FROM SET_FISCAL_YEAR_STATUS WHERE FISCALYEARSTATUSID=(SELECT FISCALYEARSTATUSID FROM SET_FISCAL_YEAR WHERE FISCALYEARID=" +Convert.ToInt32(Request.Cookies["FiscalYearId"].Value)+ ") AND RECORD_DELETED='0'";
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
        //SqlDataAdapter da = new SqlDataAdapter("select * from SET_UserGrp_Form where FormId='" + FormID + "' and DmlAllowed = 'Y' and UserGrpId = '" + UserGrpID + "' and Record_Deleted = '0' ", con);
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
            DataSet ds = dml.Find("select * from View_UserGrpFrm where Sno = " + serialno);

            if (ds.Tables[0].Rows.Count > 0)

            {
                DataSet dds = dml.Find("select * from SET_UserGrp_Form where Sno = " + serialno);

                string idd = dds.Tables[0].Rows[0]["FormId"].ToString();
                DataSet dform = dml.Find("select *  from SET_Form where FormId= '" + idd + "' and Record_Deleted='1'");
                if (dform.Tables[0].Rows.Count > 0)
                {
                    Label1.Text = "'" + dform.Tables[0].Rows[0]["FormTitle"].ToString() + "' form does not exist in setup from";
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

                    txtDateFrom.Text = dds.Tables[0].Rows[0]["FromDate"].ToString();
                    txtdateTo1.Text = dds.Tables[0].Rows[0]["ToDate"].ToString();
                    dml.dateConvert(txtDateFrom);
                    dml.dateConvert(txtdateTo1);

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
                //lblUserGrpName_SelectedIndexChanged(sender, e);
                dml.dropdownsql(lblmenu, "SET_Menu", "Menu_title", "Sno");
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

                txtDateFrom.Text = dds.Tables[0].Rows[0]["FromDate"].ToString();
                txtdateTo1.Text = dds.Tables[0].Rows[0]["ToDate"].ToString();
                dml.dateConvert(txtDateFrom);
                dml.dateConvert(txtdateTo1);
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

        txtDateFrom.Enabled = true;
        txtdateTo1.Enabled = true;
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        ImageButton3.Visible = true;
        try
        {
            string serialno = GridView3.SelectedRow.Cells[1].Text;
            ViewState["sno"] = serialno;
            DataSet ds = dml.Find("select * from V_UserGrpFrm where Sno = " + serialno);

            if (ds.Tables[0].Rows.Count > 0)

            {
                DataSet dds = dml.Find("select CONVERT(char(10), ToDate,126) as Todate1,* from SET_UserGrp_Form where Sno = " + serialno);

                // string defaultText = ds.Tables[0].Rows[0]["UserGrpName"].ToString();
                lblUserGrpName.ClearSelection();
                lblmenu.ClearSelection();
                lblform.ClearSelection();
                //drpFunction.Items.FindByText(t).Selected = true;
                lblUserGrpName.Items.FindByText(ds.Tables[0].Rows[0]["UserGrpName"].ToString()).Selected = true;
                //   lblUserGrpName_SelectedIndexChanged(sender, e);
                dml.dropdownsql(lblmenu, "SET_Menu", "Menu_title", "Sno");
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
                txtDateFrom.Text = dds.Tables[0].Rows[0]["FromDate"].ToString();
                txtdateTo1.Text = dds.Tables[0].Rows[0]["Todate1"].ToString();
                dml.dateConvert(txtDateFrom);
                dml.dateConvert(txtdateTo1);

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
                    rdb_DML_N.Checked = true;
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
        Label1.Text = "";
        datamenu_view();
        datacall();
        selectcheck();
        //select Menu_title, Serial from View_UserGrpMenu where UserGrpName = 'admin'
        if (lblUserGrpName.SelectedIndex != 0)
        {
            dml.dropdownsql(lblmenu, "View_UserGrpMenu", "Menu_title", "Sno", "UserGrpName", lblUserGrpName.SelectedItem.Text);
        }
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
    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtdate = ((TextBox)e.Row.FindControl("txtAppleDate123"));
            txtdate.Attributes.Add("readonly", "readonly");
           // e.Row.Attributes.Add("ondblclick", "__doPostBack('GridView4','Select$" + e.Row.RowIndex + "');");
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


        try
        {
            Label serial_id = (Label)GridView4.SelectedRow.FindControl("lblNatureID");
             ViewState["sno"] = serial_id.Text;
            DataSet ds = dml.Find("select * from V_UserGrpFrm where Sno1 = " + serial_id.Text);

            if (ds.Tables[0].Rows.Count > 0)

            {
                DataSet dds = dml.Find("select * from SET_UserGrp_Form where Sno1 = " + serial_id.Text);

                // string defaultText = ds.Tables[0].Rows[0]["UserGrpName"].ToString();
                lblUserGrpName.ClearSelection();
                lblmenu.ClearSelection();
                lblform.ClearSelection();
                //drpFunction.Items.FindByText(t).Selected = true;
                lblUserGrpName.Items.FindByText(ds.Tables[0].Rows[0]["UserGrpName"].ToString()).Selected = true;
                //lblUserGrpName_SelectedIndexChanged(sender, e);
                dml.dropdownsql(lblmenu, "SET_Menu", "Menu_title", "Sno");
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

                txtDateFrom.Text = dds.Tables[0].Rows[0]["FromDate"].ToString();
                txtdateTo1.Text = dds.Tables[0].Rows[0]["ToDate"].ToString();
                dml.dateConvert(txtDateFrom);
                dml.dateConvert(txtdateTo1);


              


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
    public void selectcheck()
    {
        string rdbY, addNY, editny,deleteny,view, apply, ddlmenu;
        DataSet dsformchkfalse;
        try
        {
            UserGrpID = Request.QueryString["UsergrpID"];
            DataSet ds;

            int aa = lblUserGrpName.SelectedIndex;

            if (lblUserGrpName.SelectedIndex != 0)
            {
                ds = dml.Find("select * from V_UserGrpFrm where UserGrpId = '" + lblUserGrpName.SelectedItem.Value + "' and Record_deleted = 0  order by Sno1 asc");
            }
            else
            {
                ds = dml.Find("select * from V_UserGrpFrm where UserGrpId = '" + UserGrpID + "' and Record_deleted = 0 order by Sno1 asc");
            }
            int countrow = ds.Tables[0].Rows.Count;
            if (countrow > 0)
            {

                for (int i = 0; i <= countrow - 1; i++)
                {
                    string val = ds.Tables[0].Rows[i]["Sno1"].ToString();

                    rdbY = ds.Tables[0].Rows[i]["Hide"].ToString();
                    addNY = ds.Tables[0].Rows[i]["Add"].ToString();
                    editny = ds.Tables[0].Rows[i]["Edit"].ToString();
                    ddlmenu = ds.Tables[0].Rows[i]["Menuid"].ToString();
                    deleteny = ds.Tables[0].Rows[i]["Delete"].ToString();
                    apply = ds.Tables[0].Rows[i]["EntryDate"].ToString();
                    foreach (GridViewRow grow in GridView4.Rows)
                    {
                        DropDownList ddl_menu = (DropDownList)grow.FindControl("ddlGridView4Menuid");
                       //  dml.dropdownsql(ddl_menu, "SET_Menu", "Menu_title", "Menuid");
                        Label lbl = (Label)grow.FindControl("lbldoc1");

                        CheckBox chk_del = (CheckBox)grow.FindControl("chkSelect");
                        RadioButton rdbhideyes = (RadioButton)grow.FindControl("rdb_HYes");
                        RadioButton rdbhideNo = (RadioButton)grow.FindControl("rdb_NYes");

                        RadioButton rdbaddy = (RadioButton)grow.FindControl("rdb_ADDY");
                        RadioButton rdbaddNo = (RadioButton)grow.FindControl("rdb_ADDN");

                        RadioButton rdbedityes = (RadioButton)grow.FindControl("rdb_EditY");
                        RadioButton rdbEditNo = (RadioButton)grow.FindControl("rdb_EditN");

                        RadioButton rdbDelyes = (RadioButton)grow.FindControl("rdb_DeleteY");
                        RadioButton rdbDelNo = (RadioButton)grow.FindControl("rdb_DeleteN");


                        TextBox txtapply = (TextBox)grow.FindControl("txtAppleDate123");
                        Label lblID = (Label)grow.FindControl("lblNatureID");

                        if (lblID.Text == val)
                        {

                            if (rdbY == "Y")
                            {
                                rdbhideyes.Checked = true;
                                rdbhideNo.Checked = false;
                            }
                            if (rdbY == "N")
                            {
                                rdbhideyes.Checked = false;
                                rdbhideNo.Checked = true;
                            }

                            if (addNY == "Y")
                            {
                                rdbaddy.Checked = true;
                                rdbaddNo.Checked = false;
                            }
                            if (addNY == "N")
                            {
                                rdbaddy.Checked = false;
                                rdbaddNo.Checked = true;
                            }
                            if (editny == "Y")
                            {
                                rdbedityes.Checked = true;
                                rdbEditNo.Checked = false;
                            }
                            if (editny == "N")
                            {
                                rdbedityes.Checked = false;
                                rdbEditNo.Checked = true;
                            }
                            if (deleteny == "Y")
                            {
                                rdbDelyes.Checked = true;
                                rdbDelNo.Checked = false;
                            }
                            if (deleteny == "N")
                            {
                                rdbDelyes.Checked = false;
                                rdbDelNo.Checked = true;
                            }
                            if (ddl_menu.Items.FindByValue(lbl.Text) != null)
                            {
                                ddl_menu.ClearSelection();
                                ddl_menu.SelectedValue = lbl.Text;
                                txtapply.Text = dml.dateConvert(apply);
                            }
                           
                            // dml.Insert("INSERT INTO [SET_BPartnerType] ([GocID], [BPartnerID], [BPNatureID], [SysDate]) VALUES  ('1', '" + ViewState["BPID"].ToString() + "', '" + lblID.Text + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "');", "");
                            chk_del.Checked = true;

                        }
                        else
                        {
                            if (chk_del.Checked == false && lblID.Text != val)
                            {
                                dsformchkfalse = dml.Find("select * from SET_Form where Sno = '" + lblID.Text + "'" );
                                if (dsformchkfalse.Tables[0].Rows.Count > 0)
                                {
                                    ddl_menu.ClearSelection();
                                    string fff = dsformchkfalse.Tables[0].Rows[0]["Menuid"].ToString();
                                    ddl_menu.SelectedValue = fff;
                                }

                            }
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
    public void datamenu_view()
    {
        UserGrpID = Request.QueryString["UsergrpID"];
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        try
        {
            cmd = new SqlCommand("sp_USERFORM", con);

            if (lblUserGrpName.SelectedIndex != 0)
            {
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
   public void datacall()
    {
        foreach (GridViewRow grow in GridView4.Rows)
        {
            DropDownList ddl_menu = (DropDownList)grow.FindControl("ddlGridView4Menuid");
            dml.dropdownsql(ddl_menu, "SET_Menu", "Menu_title", "Menuid");
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
            ds = dml.Find("select * from SET_UserGrp_Form where UserGrpId = '" + lblUserGrpName.SelectedItem.Value + "' and Record_deleted = 0");
            ds1 = dml.Find("select * from SET_UserGrp_Form where UserGrpId = '" + lblUserGrpName.SelectedItem.Value + "' and Record_deleted = 1");
        }
        else
        {
            ds = dml.Find("select * from SET_UserGrp_Form where UserGrpId = '" + UserGrpID + "' and Record_deleted = 0 ");
            ds1 = dml.Find("select * from SET_UserGrp_Form where UserGrpId = '" + UserGrpID + "' and Record_deleted = 1 ");
        }
        int countrow = ds.Tables[0].Rows.Count;
        int countdel = ds1.Tables[0].Rows.Count;
        if (countrow > 0)
        {
            string chkname;
            foreach (GridViewRow g in GridView4.Rows)
            {
                CheckBox chk_del = (CheckBox)g.FindControl("chkSelect");
                DropDownList ddl_menu = (DropDownList)g.FindControl("ddlGridView4Menuid");
                chkname = chk_del.Checked.ToString();
                chkname = chk_del.Text;

                Label lblID = (Label)g.FindControl("lblNatureID");
                if (chk_del.Checked == true)
                {
                    if (countdel > 0)
                    {
                        for (int i = 0; i <= countdel - 1; i++)
                        {
                            string val = ds1.Tables[0].Rows[i]["Sno1"].ToString();
                            if (val == lblID.Text && chk_del.Checked == true)
                            {
                                ca1 = ca1 + 1;
                                if (lblUserGrpName.SelectedIndex != 0)
                                {
                                    dml.Update("update SET_UserGrp_Form set Record_Deleted = '0', menuid = '" + ddl_menu.SelectedItem.Value + "' , UpdatedBy= '" + show_username() + "',UpdatedDate ='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' where Sno1 = '" + lblID.Text + "' and UserGrpId = '" + lblUserGrpName.SelectedItem.Value + "';", "");
                                }
                                else
                                {
                                    dml.Update("update SET_UserGrp_Form set Record_Deleted = '0', menuid = '" + ddl_menu.SelectedItem.Value + "' , UpdatedBy= '" + show_username() + "',UpdatedDate ='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' where Sno1 = '" + lblID.Text + "' and UserGrpId = '" + UserGrpID + "';", "");
                                }
                                flag = false;

                            }
                          
                        }
                        if (chk_del.Checked == true)
                        {
                            DataSet dsm;
                            if (lblUserGrpName.SelectedIndex != 0)
                            {
                                 dsm = dml.Find("select * from SET_UserGrp_Form where Sno1 = '" + lblID.Text + "' and Record_Deleted = '0' and UserGrpId='" + lblUserGrpName.SelectedItem.Value + "'");
                            }
                            else
                            {
                                 dsm = dml.Find("select * from SET_UserGrp_Form where Sno1 = '" + lblID.Text + "' and Record_Deleted = '0' and UserGrpId='" + UserGrpID + "'");
                            }
                            if (dsm.Tables[0].Rows.Count > 0)
                            {

                            }
                            else
                            {
                                string formid = "", snoid = "";
                                UserGrpID = Request.QueryString["UsergrpID"];
                                string add = "N", edit = "N", del = "N", hide = "N";
                                DropDownList ddl_menu1 = (DropDownList)g.FindControl("ddlGridView4Menuid");
                                Label lbdocdes = (Label)g.FindControl("lbldoc");
                                Label lbdocname = (Label)g.FindControl("lbldocname");
                                TextBox applydate = (TextBox)g.FindControl("txtAppleDate123");
                                RadioButton rdbhideyes = (RadioButton)g.FindControl("rdb_HYes");
                                RadioButton rdbhideNo = (RadioButton)g.FindControl("rdb_NYes");

                                RadioButton rdbaddy = (RadioButton)g.FindControl("rdb_ADDY");
                                RadioButton rdbaddNo = (RadioButton)g.FindControl("rdb_ADDN");

                                RadioButton rdbedityes = (RadioButton)g.FindControl("rdb_EditY");
                                RadioButton rdbEditNo = (RadioButton)g.FindControl("rdb_EditN");

                                RadioButton rdbDelyes = (RadioButton)g.FindControl("rdb_DeleteY");
                                RadioButton rdbDelNo = (RadioButton)g.FindControl("rdb_DeleteN");
                                if (rdbaddy.Checked == true)
                                {
                                    add = "Y";
                                }
                                if (rdbedityes.Checked == true)
                                {
                                    edit = "Y";
                                }
                                if (rdbDelyes.Checked == true)
                                {
                                    del = "Y";
                                }
                                if (rdbhideyes.Checked == true)
                                {
                                    hide = "Y";
                                }

                                string date = DateTime.Now.ToString("yyyy-MMM-dd"); ;//dml.dateconvertString(txtAppleDate.Text);

                                DataSet dsform = dml.Find("select FormTitle,Sno,FormId FROM SET_Form where FormTitle = '" + lbdocdes.Text + "'");
                                if (dsform.Tables[0].Rows.Count > 0)
                                {
                                    formid = dsform.Tables[0].Rows[0]["FormId"].ToString();
                                    snoid = dsform.Tables[0].Rows[0]["Sno"].ToString();

                                }

                                if (lblUserGrpName.SelectedIndex != 0)
                                {
                                    //dml.Insert("INSERT INTO SET_UserGrp_Form([UserGrpId], [DocID], [DocDescription], [Main_Site], [ApplyDate], [IsActive], [IsHide], [SortOrder], [Record_Deleted], [CreatedBy], [CreateDate]) VALUES ('" + lblUserGrpName.SelectedItem.Value + "', '" + lblID.Text + "', '" + lbdocdes.Text + "', '" + mainsite + "', '" + date + "', '1', '" + hideyn + "', '0', '0', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "')", "");

                                    dml.Insert("INSERT INTO  SET_UserGrp_Form([Sno1], [FormId], [UserGrpId], [EntryDate], [FromDate], [ToDate], [MenuId], [DmlAllowed], [DateFrom], [Add], [AddDays], [Edit], [EditDays], [Delete], [DeleteDays], [Find], [View], [IsActive], [Hide], [SortOrder], [SysDate], [EnterUserId], [PEP_Exempt], [Record_Deleted], [CreatedBy], [CreatedDate]) VALUES ('" + snoid + "', '" + formid + "', '" + lblUserGrpName.SelectedItem.Value + "', '" + date + "', '" + DateTime.Now.ToString("yyyy-MMM-dd") + "', '" + DateTime.Now.AddDays(3).ToString("yyyy-MMM-dd") + "', '" + ddl_menu1.SelectedItem.Value + "', 'Y', '3', '" + add + "', '3', '" + edit + "', '3', '" + del + "', '3', 'Y', 'Y', '1', 'N', '0', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '6B9C1166-0F4B-41DC-99E8-B47BE96C8157', 'Y', '0', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "');", "");
                                }
                                else
                                {
                                    //dml.Insert("INSERT INTO SET_UserGrp_Form([UserGrpId], [DocID], [DocDescription], [Main_Site], [ApplyDate], [IsActive], [IsHide], [SortOrder], [Record_Deleted], [CreatedBy], [CreateDate]) VALUES ( '" + UserGrpID + "', '" + lblID.Text + "', '" + lbdocdes.Text + "', '" + mainsite + "', '" + date + "', '1', '" + hideyn + "', '0', '0', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "')", "");

                                    dml.Insert("INSERT INTO  SET_UserGrp_Form([Sno1], [FormId], [UserGrpId], [EntryDate], [FromDate], [ToDate], [MenuId], [DmlAllowed], [DateFrom], [Add], [AddDays], [Edit], [EditDays], [Delete], [DeleteDays], [Find], [View], [IsActive], [Hide], [SortOrder], [SysDate], [EnterUserId], [PEP_Exempt], [Record_Deleted], [CreatedBy], [CreatedDate]) VALUES ('" + snoid + "', '" + formid + "', '" + UserGrpID + "', '" + date + "', '" + DateTime.Now.ToString("yyyy-MMM-dd") + "', '" + DateTime.Now.AddDays(3).ToString("yyyy-MMM-dd") + "', '" + ddl_menu1.SelectedItem.Value + "', 'Y', '3', '" + add + "', '3', '" + edit + "', '3', '" + del + "', '3', 'Y', 'Y', '1', 'N', '0', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '6B9C1166-0F4B-41DC-99E8-B47BE96C8157', 'Y', '0', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "');", "");
                                }
                                Label1.Text = "data Inserted";
                                //insert
                            }
                        }
                    }
                    else
                    {
                        if (chk_del.Checked == true)
                        {

                            if (lblUserGrpName.SelectedIndex != 0)
                            {
                                ds2 = dml.Find("select * from SET_UserGrp_Form where UserGrpId = '" + lblUserGrpName.SelectedItem.Value + "' and Sno1 = '" + lblID.Text + "' and Record_deleted = 0");
                            }
                            else
                            {
                                ds2 = dml.Find("select * from SET_UserGrp_Form where UserGrpId = '" + UserGrpID + "' and Sno1 = '" + lblID.Text + "' and Record_deleted = 0 ");
                            }

                            if (ds2.Tables[0].Rows.Count > 0)
                            {


                            }
                            else {
                                //Insert
                                string formid = "",snoid= "";
                                UserGrpID = Request.QueryString["UsergrpID"];
                                string add = "N", edit = "N", del = "N", hide="N";
                                DropDownList ddl_menu1 = (DropDownList)g.FindControl("ddlGridView4Menuid");
                                Label lbdocdes = (Label)g.FindControl("lbldoc");
                                Label lbdocname = (Label)g.FindControl("lbldocname");
                                TextBox applydate = (TextBox)g.FindControl("txtAppleDate123");
                                RadioButton rdbhideyes = (RadioButton)g.FindControl("rdb_HYes");
                                RadioButton rdbhideNo = (RadioButton)g.FindControl("rdb_NYes");

                                RadioButton rdbaddy = (RadioButton)g.FindControl("rdb_ADDY");
                                RadioButton rdbaddNo = (RadioButton)g.FindControl("rdb_ADDN");

                                RadioButton rdbedityes = (RadioButton)g.FindControl("rdb_EditY");
                                RadioButton rdbEditNo = (RadioButton)g.FindControl("rdb_EditN");

                                RadioButton rdbDelyes = (RadioButton)g.FindControl("rdb_DeleteY");
                                RadioButton rdbDelNo = (RadioButton)g.FindControl("rdb_DeleteN");
                                if (rdbaddy.Checked == true)
                                {
                                    add = "Y";
                                }
                                if (rdbedityes.Checked == true)
                                {
                                    edit = "Y";
                                }
                                if (rdbDelyes.Checked == true)
                                {
                                    del = "Y";
                                }
                                if (rdbhideyes.Checked == true)
                                {
                                    hide = "Y";
                                }

                                string date = DateTime.Now.ToString("yyyy-MMM-dd"); ;//dml.dateconvertString(txtAppleDate.Text);

                                DataSet dsform = dml.Find("select FormTitle,Sno,FormId FROM SET_Form where FormTitle = '"+lbdocdes.Text+"'");
                                if(dsform.Tables[0].Rows.Count > 0)
                                {
                                    formid = dsform.Tables[0].Rows[0]["FormId"].ToString();
                                    snoid = dsform.Tables[0].Rows[0]["Sno"].ToString();
                                    
                                }

                                if (lblUserGrpName.SelectedIndex != 0)
                                {
                                    //dml.Insert("INSERT INTO SET_UserGrp_Form([UserGrpId], [DocID], [DocDescription], [Main_Site], [ApplyDate], [IsActive], [IsHide], [SortOrder], [Record_Deleted], [CreatedBy], [CreateDate]) VALUES ('" + lblUserGrpName.SelectedItem.Value + "', '" + lblID.Text + "', '" + lbdocdes.Text + "', '" + mainsite + "', '" + date + "', '1', '" + hideyn + "', '0', '0', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "')", "");

                                    dml.Insert("INSERT INTO  SET_UserGrp_Form([Sno1], [FormId], [UserGrpId], [EntryDate], [FromDate], [ToDate], [MenuId], [DmlAllowed], [DateFrom], [Add], [AddDays], [Edit], [EditDays], [Delete], [DeleteDays], [Find], [View], [IsActive], [Hide], [SortOrder], [SysDate], [EnterUserId], [PEP_Exempt], [Record_Deleted], [CreatedBy], [CreatedDate]) VALUES ('" + snoid + "', '" + formid + "', '" + lblUserGrpName.SelectedItem.Value + "', '" + date + "', '" + DateTime.Now.ToString("yyyy-MMM-dd") + "', '" + DateTime.Now.AddDays(3).ToString("yyyy-MMM-dd") + "', '"+ ddl_menu1.SelectedItem.Value+"', 'Y', '3', '" + add + "', '3', '" + edit + "', '3', '" + del + "', '3', 'Y', 'Y', '1', 'N', '0', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '6B9C1166-0F4B-41DC-99E8-B47BE96C8157', 'Y', '0', '" + show_username() +"', '"+DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")+"');", "");
                                }
                                else
                                {
                                    //dml.Insert("INSERT INTO SET_UserGrp_Form([UserGrpId], [DocID], [DocDescription], [Main_Site], [ApplyDate], [IsActive], [IsHide], [SortOrder], [Record_Deleted], [CreatedBy], [CreateDate]) VALUES ( '" + UserGrpID + "', '" + lblID.Text + "', '" + lbdocdes.Text + "', '" + mainsite + "', '" + date + "', '1', '" + hideyn + "', '0', '0', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "')", "");

                                    dml.Insert("INSERT INTO  SET_UserGrp_Form([Sno1], [FormId], [UserGrpId], [EntryDate], [FromDate], [ToDate], [MenuId], [DmlAllowed], [DateFrom], [Add], [AddDays], [Edit], [EditDays], [Delete], [DeleteDays], [Find], [View], [IsActive], [Hide], [SortOrder], [SysDate], [EnterUserId], [PEP_Exempt], [Record_Deleted], [CreatedBy], [CreatedDate]) VALUES ('" + snoid + "', '" + formid + "', '" + UserGrpID + "', '" + date + "', '" + DateTime.Now.ToString("yyyy-MMM-dd") + "', '" + DateTime.Now.AddDays(3).ToString("yyyy-MMM-dd") + "', '"+ ddl_menu1.SelectedItem.Value + "', 'Y', '3', '" + add + "', '3', '" + edit + "', '3', '" + del + "', '3', 'Y', 'Y', '1', 'N', '0', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '6B9C1166-0F4B-41DC-99E8-B47BE96C8157', 'Y', '0', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "');", "");
                                }
                                Label1.Text = "data Inserted";
                                //insert

                            }

                        }
                    }
                }
                else
                {
                    DropDownList ddl_menu1 = (DropDownList)g.FindControl("ddlGridView4Menuid");
                    for (int i = 0; i <= countrow - 1; i++)
                    {
                        string val = ds.Tables[0].Rows[i]["Sno1"].ToString();
                        if (val == lblID.Text && chk_del.Checked == false)
                        {


                            if (lblUserGrpName.SelectedIndex != 0)
                            {
                                dml.Update("update SET_UserGrp_Form set Record_Deleted = '1', menuid = '" + ddl_menu1.SelectedItem.Value + "' , UpdatedBy= '" + show_username() + "',UpdatedDate='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' where Sno1 = '" + lblID.Text + "' and UserGrpId = '" + lblUserGrpName.SelectedItem.Value + "';", "");
                            }
                            else
                            {
                                dml.Update("update SET_UserGrp_Form set Record_Deleted = '1', menuid = '" + ddl_menu1.SelectedItem.Value + "' , UpdatedBy= '" + show_username() + "',UpdatedDate='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' where Sno1 = '" + lblID.Text + "' and UserGrpId = '" + UserGrpID + "';", "");
                            }
                            flag = false;
                        }
                        


                    }
                    if (flag == false)
                    {
                        Label1.Text = "Updated Success";
                        datamenu_view();
                        
                        GridView4.DataBind();
                        datacall();
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
                DropDownList ddl_menu = (DropDownList)g.FindControl("ddlGridView4Menuid");
                Label lblID = (Label)g.FindControl("lblNatureID");
                UserGrpID = Request.QueryString["UsergrpID"];
                if (chk_del.Checked == true)
                {


                    if (lblUserGrpName.SelectedIndex != 0)
                    {
                        ds2 = dml.Find("select * from SET_UserGrp_Form where UserGrpId = '" + lblUserGrpName.SelectedItem.Value + "' and Sno1 = '" + lblID.Text + "' and Record_deleted = 1");
                    }
                    else
                    {
                        ds2 = dml.Find("select * from SET_UserGrp_Form where UserGrpId = '" + UserGrpID + "' and Sno1 = '" + lblID.Text + "' and Record_deleted = 1 ");
                    }

                    if (ds2.Tables[0].Rows.Count > 0)
                    {


                        if (lblUserGrpName.SelectedIndex != 0)
                        {
                            dml.Update("update SET_UserGrp_Form set Record_Deleted = '0', Menuid = '"+ ddl_menu.SelectedItem.Value+ "' , UpdatedBy= '" + show_username() + "',UpdatedDate='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' where Sno1 = '" + lblID.Text + "' and UserGrpId = '" + lblUserGrpName.SelectedItem.Value + "';", "");
                        }
                        else
                        {
                            dml.Update("update SET_UserGrp_Form set Record_Deleted = '0' , Menuid = '" + ddl_menu.SelectedItem.Value + "' , UpdatedBy= '" + show_username() + "',UpdatedDate='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' where Sno1 = '" + lblID.Text + "' and UserGrpId = '" + UserGrpID + "';", "");
                        }
                        Label1.Text = "Updated Success";


                    }
                    else
                    {


                        string formid = "", snoid = "";
                        UserGrpID = Request.QueryString["UsergrpID"];
                        string add = "N", edit = "N", del = "N", hide = "N";
                        DropDownList ddl_menu1 = (DropDownList)g.FindControl("ddlGridView4Menuid");
                        Label lbdocdes = (Label)g.FindControl("lbldoc");
                        Label lbdocname = (Label)g.FindControl("lbldocname");
                        TextBox applydate = (TextBox)g.FindControl("txtAppleDate123");
                        RadioButton rdbhideyes = (RadioButton)g.FindControl("rdb_HYes");
                        RadioButton rdbhideNo = (RadioButton)g.FindControl("rdb_NYes");

                        RadioButton rdbaddy = (RadioButton)g.FindControl("rdb_ADDY");
                        RadioButton rdbaddNo = (RadioButton)g.FindControl("rdb_ADDN");

                        RadioButton rdbedityes = (RadioButton)g.FindControl("rdb_EditY");
                        RadioButton rdbEditNo = (RadioButton)g.FindControl("rdb_EditN");

                        RadioButton rdbDelyes = (RadioButton)g.FindControl("rdb_DeleteY");
                        RadioButton rdbDelNo = (RadioButton)g.FindControl("rdb_DeleteN");
                        if (rdbaddy.Checked == true)
                        {
                            add = "Y";
                        }
                        if (rdbedityes.Checked == true)
                        {
                            edit = "Y";
                        }
                        if (rdbDelyes.Checked == true)
                        {
                            del = "Y";
                        }
                        if (rdbhideyes.Checked == true)
                        {
                            hide = "Y";
                        }

                        string date = DateTime.Now.ToString("yyyy-MMM-dd"); ;//dml.dateconvertString(txtAppleDate.Text);

                        DataSet dsform = dml.Find("select FormTitle,Sno,FormId FROM SET_Form where FormTitle = '" + lbdocdes.Text + "'");
                        if (dsform.Tables[0].Rows.Count > 0)
                        {
                            formid = dsform.Tables[0].Rows[0]["FormId"].ToString();
                            snoid = dsform.Tables[0].Rows[0]["Sno"].ToString();

                        }

                        if (lblUserGrpName.SelectedIndex != 0)
                        {
                            //dml.Insert("INSERT INTO SET_UserGrp_Documents([UserGrpId], [DocID], [DocDescription], [Main_Site], [ApplyDate], [IsActive], [IsHide], [SortOrder], [Record_Deleted], [CreatedBy], [CreateDate]) VALUES ('" + lblUserGrpName.SelectedItem.Value + "', '" + lblID.Text + "', '" + lbdocdes.Text + "', '" + mainsite + "', '" + date + "', '1', '" + hideyn + "', '0', '0', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "')", "");
                            dml.Insert("INSERT INTO  SET_UserGrp_Form([Sno1], [FormId], [UserGrpId], [EntryDate], [FromDate], [ToDate], [MenuId], [DmlAllowed], [DateFrom], [Add], [AddDays], [Edit], [EditDays], [Delete], [DeleteDays], [Find], [View], [IsActive], [Hide], [SortOrder], [SysDate], [EnterUserId], [PEP_Exempt], [Record_Deleted], [CreatedBy], [CreatedDate]) VALUES ('" + snoid + "', '" + formid + "', '" + lblUserGrpName.SelectedItem.Value + "', '" + date + "', '" + DateTime.Now.ToString("yyyy-MMM-dd") + "', '" + DateTime.Now.AddDays(3).ToString("yyyy-MMM-dd") + "', '"+ ddl_menu1.SelectedItem.Value+ "', 'Y', '3', '" + add + "', '3', '" + edit + "', '3', '" + del + "', '3', 'Y', 'Y', '1', 'N', '0', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '6B9C1166-0F4B-41DC-99E8-B47BE96C8157', 'Y', '0', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "');", "");
                        }
                        else
                        {
                            //dml.Insert("INSERT INTO SET_UserGrp_Documents([UserGrpId], [DocID], [DocDescription], [Main_Site], [ApplyDate], [IsActive], [IsHide], [SortOrder], [Record_Deleted], [CreatedBy], [CreateDate]) VALUES ( '" + UserGrpID + "', '" + lblID.Text + "', '" + lbdocdes.Text + "', '" + mainsite + "', '" + date + "', '1', '" + hideyn + "', '0', '0', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "')", "");
                            dml.Insert("INSERT INTO  SET_UserGrp_Form([Sno1], [FormId], [UserGrpId], [EntryDate], [FromDate], [ToDate], [MenuId], [DmlAllowed], [DateFrom], [Add], [AddDays], [Edit], [EditDays], [Delete], [DeleteDays], [Find], [View], [IsActive], [Hide], [SortOrder], [SysDate], [EnterUserId], [PEP_Exempt], [Record_Deleted], [CreatedBy], [CreatedDate]) VALUES ('" + snoid + "', '" + formid + "', '" + UserGrpID+ "', '" + date + "', '" + DateTime.Now.ToString("yyyy-MMM-dd") + "', '" + DateTime.Now.AddDays(3).ToString("yyyy-MMM-dd") + "', '"+ ddl_menu1.SelectedItem.Value + "', 'Y', '3', '" + add + "', '3', '" + edit + "', '3', '" + del + "', '3', 'Y', 'Y', '1', 'N', '0', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '6B9C1166-0F4B-41DC-99E8-B47BE96C8157', 'Y', '0', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "');", "");
                        }
                        Label1.Text = "data Inserted";
                    }
                }
            }
        }

        datamenu_view();
      
        GridView4.DataBind();
        datacall();
        selectcheck();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
    }


    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        txtdateTo1.Text = "";
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string id = e.Row.Cells[1].Text;
        bool flag = dml.isNumber(id);


        if (flag == true)
        {

            DataSet ds = dml.Find("select Sno,MLD from SET_UserGrp_Form where Sno = '" + id + "'");
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

            DataSet ds = dml.Find("select Sno,MLD from SET_UserGrp_Form where Sno = '" + id + "'");
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