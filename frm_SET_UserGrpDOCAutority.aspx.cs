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
    int DateFrom, EditDays, AddDays, DeleteDays;
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

            // fd.HideUSerGrpField(Page.Controls, UserGrpID, FormID, "SET_Fiscal_Year");
            // Showall_Dml();

            dml.dropdownsql(lblAuth, "SET_Authority", "AuthorityName", "AuthorityId");
            txtDateFrom.Attributes.Add("readonly", "readonly");
            txtDateTo.Attributes.Add("readonly", "readonly");
            txtEntryDate.Attributes.Add("readonly", "readonly");

            dml.dropdownsql(lblDoc, "SET_Documents", "DocDescription", "DocID");
            dml.dropdownsql(lblDoc, "SET_Documents", "DocDescription", "DocID");
            dml.dropdownsql(lblUserGrpName, "SET_UserGrp", "UserGrpName", "sno");


            dml.dropdownsql(ddlEdit_GOCName, "SET_Authority", "AuthorityName", "AuthorityId");
            dml.dropdownsql(ddlEdit_DOC, "SET_Documents", "DocDescription", "DocID");

            dml.dropdownsql(ddlFInd_AUTH, "SET_Authority", "AuthorityName", "AuthorityId");
            dml.dropdownsql(ddlFind_DOC, "SET_Documents", "DocDescription", "DocID");
            dml.dropdownsql(ddlDel_AUTH, "SET_Authority", "AuthorityName", "AuthorityId");
            dml.dropdownsql(ddlDel_DOC, "SET_Documents", "DocDescription", "DocID");

            dml.dropdownsql(ddlFind_UserGrpName, "SET_UserGrp", "UserGrpName", "UserGrpId");
            dml.dropdownsql(ddlEdit_USergrpName, "SET_UserGrp", "UserGrpName", "UserGrpId");
            dml.dropdownsql(ddlDel_UserGrpName, "SET_UserGrp", "UserGrpName", "UserGrpId");



            // dml.dropdownsql(lblDoc, "SET_Documents", "DocDescription", "DocID");

            textClear();
           

        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            UserGrpID = Request.QueryString["UsergrpID"];
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

            DataSet ds_duplic;
            if (lblDoc.SelectedIndex != 0)
            {
                ds_duplic = dml.Find("select  * from SET_UserGrp_DocAuthority where AuthorityId = '" + lblAuth.SelectedItem.Value + "' and CompID='" + compid() + "' and BranchId='" + branchId() + "' and UserGrpId='" + UserGrpID + "' and DocId = '" + lblDoc.SelectedItem.Value + "'");
            }
            else
            {
                ds_duplic = dml.Find("select  * from SET_UserGrp_DocAuthority where AuthorityId = '" + lblAuth.SelectedItem.Value + "' and CompID='" + compid() + "' and BranchId='" + branchId() + "' and UserGrpId='" + UserGrpID + "'");
            }
            
            if (ds_duplic.Tables[0].Rows.Count > 0)
            {
                Label1.Text = "Duplicate Entry not allowed";
            }
            else {
                string fromdate = dml.dateconvertforinsert(txtDateFrom);
                string todate = "";
                if (txtDateTo.Text != "")
                {
                   todate =  dml.dateconvertforinsert(txtDateTo);
                }
                else
                {
                    todate = "2120-01-01";
                }
                string entDate = dml.dateconvertforinsert(txtEntryDate);
                if (lblDoc.SelectedIndex != 0)
                {
                    DataSet ds_doc_check = dml.Find("select * from SET_UserGrp_DocAuthority where docid = '" + lblDoc.SelectedItem.Value + "' and globallyEntry = 0");
                    if (ds_doc_check.Tables[0].Rows.Count > 0)
                    {
                        dml.Delete("delete from SET_UserGrp_DocAuthority where docid = '"+ lblDoc.SelectedItem.Value +"' and globallyEntry = 0", "");
                        string txtremarkschange = txtRemarks.Text.Replace("'", "''");
                        dml.Insert("INSERT INTO SET_UserGrp_DocAuthority ([AuthorityId], [UserGrpId], [DocId], [EntryDate], [DateFrom], [DateTo], [Remarks], [CompID], [BranchId], [Active], [CreatedBy],[CreateDate] ,[Record_Deleted],[globallyEntry],[MLD]) VALUES ('" + lblAuth.SelectedItem.Value + "', '" + UserGrpID + "', '" + lblDoc.SelectedItem.Value + "', '" + entDate + "', '" + fromdate + "', '" + todate + "', '" + txtremarkschange + "'," + compid() + "," + branchId() + ", '" + chk + "', '" + show_username() + "','" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "','0',1,'"+dml.Encrypt("h")+"');", "");
                        dml.Update("update Set_Authority set MLD = '" + dml.Encrypt("q") + "' where AuthorityId = '" + lblAuth.SelectedItem.Value + "'", "");

                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "alertme()", true);
                    }
                    else
                    {
                       

                        string txtremarkschange = txtRemarks.Text.Replace("'", "''");
                        dml.Insert("INSERT INTO SET_UserGrp_DocAuthority ([AuthorityId], [UserGrpId], [DocId], [EntryDate], [DateFrom], [DateTo], [Remarks], [CompID], [BranchId], [Active], [CreatedBy],[CreateDate] ,[Record_Deleted],[globallyEntry],[MLD]) VALUES ('" + lblAuth.SelectedItem.Value + "', '" + UserGrpID + "', '" + lblDoc.SelectedItem.Value + "', '" + entDate + "', '" + fromdate + "', '" + todate + "', '" + txtremarkschange + "', '" + compid() + "', '" + branchId() + "', '" + chk + "', '" + show_username() + "','" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "','0','1','"+ dml.Encrypt("h") + "');", "");
                        dml.Update("update Set_Authority set MLD = '" + dml.Encrypt("q") + "' where AuthorityId = '" + lblAuth.SelectedItem.Value + "'", "");
                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "alertme()", true);
                    }
                }
                else
                { //select * from SET_UserGrp_DocAuthority where globallyEntry = 0 and DocId = 97

                    for (int a = 1; a < lblDoc.Items.Count; a++)
                    {
                        DataSet ds_doc_check1 = dml.Find("select * from SET_UserGrp_DocAuthority where globallyEntry = 0 and DocId = '" + lblDoc.Items[a].Value + "'");
                        if (ds_doc_check1.Tables[0].Rows.Count > 0)
                        {
                            string txtremarkschange = txtRemarks.Text.Replace("'", "''");
                            dml.Update("UPDATE SET_UserGrp_DocAuthority SET [AuthorityId]='" + lblAuth.SelectedItem.Value + "',[UserGrpId]='" + UserGrpID + "', [CompID]='" + compid() + "', [BranchId]='" + branchId() + "', [DocId]='" + lblDoc.Items[a].Value + "', [EntryDate]='" + entDate + "', [DateFrom]='" + fromdate + "', [DateTo]='" + todate + "',[Remarks]='" + txtremarkschange + "', [Active]='" + chk + "', [UpdatedBy]='" + show_username() +"', [UpdateDate]='"+DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")+ "', [Record_Deleted]='0', [globallyEntry]='0' where DocId = '"+lblDoc.Items[a].Value+ "' and globallyEntry = '0'", "");
                        }
                        else
                        {
                            DataSet ds_doc_checkelse = dml.Find("select * from SET_UserGrp_DocAuthority where globallyEntry = 1 and DocId = '" + lblDoc.Items[a].Value + "'");
                            if (ds_doc_checkelse.Tables[0].Rows.Count > 0)
                            {

                            }
                            else
                            {
                                string txtremarkschange = txtRemarks.Text.Replace("'", "''");
                                dml.Insert("INSERT INTO SET_UserGrp_DocAuthority ([AuthorityId], [UserGrpId], [DocId], [EntryDate], [DateFrom], [DateTo], [Remarks], [CompID], [BranchId], [Active], [CreatedBy],[CreateDate] ,[Record_Deleted],[globallyEntry],[MLD]) VALUES ('" + lblAuth.SelectedItem.Value + "', '" + UserGrpID + "', '" + lblDoc.Items[a].Value + "', '" + entDate + "', '" + fromdate + "', '" + todate + "', '" + txtremarkschange + "'," + compid() + "," + branchId() + ", '" + chk + "', '" + show_username() + "','" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "','0','0','"+dml.Encrypt("h")+"');", "");
                                dml.Update("update Set_Authority set MLD = '" + dml.Encrypt("q") + "' where AuthorityId = '" + lblAuth.SelectedItem.Value + "'", "");
                                ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "alertme()", true);
                            }
                        }
                    }
                }
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
        }
        catch (Exception ex)
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = ex.Message;
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

        lblDoc.Enabled = true;
        lblAuth.Enabled = true;

        imgPopup.Enabled = true;
        ImageButton1.Enabled = true;
        ImageButton2.Enabled = true;

        lblUserGrpName.Enabled = true ;
        txtEntryDate.Enabled = true;
        txtRemarks.Enabled = true;
        txtDateFrom.Enabled = true;
        txtDateTo.Enabled = true;
        chkActive_Status.Checked = true;
        chkActive_Status.Enabled = true;
        userid = Request.QueryString["UserID"];
        DataSet ds = dml.Find("select DateOfStartWork from SET_User_Manager where UserId = '" + userid+"'");
        if (ds.Tables[0].Rows.Count > 0)
        {
           // txtDateFrom.Text = dml.dateConvert(ds.Tables[0].Rows[0]["DateOfStartWork"].ToString());
        }
        
        UserGrpID = Request.QueryString["UsergrpID"];
        lblUserGrpName.ClearSelection();
        DataSet dss = dml.Find("select * from SET_UserGrp where UserGrpId = '" + UserGrpID + "'");
        if (dss.Tables[0].Rows.Count > 0)
        {
            lblUserGrpName.Items.FindByText(dss.Tables[0].Rows[0]["UserGrpName"].ToString()).Selected = true;
        }


        txtcreatedDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff ");
        txtCreatedBy.Text = dml.show_usernameFED(userid);
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        try
        {
            string add = "N";
            string delete = "N";
            string edit = "N";
            string find = "N";
            string view = "N";

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
            string txtremarkschange = txtRemarks.Text.Replace("'", "''");
            string fromdate = dml.dateconvertforinsert(txtDateFrom);
            string todate = "";
            if (txtDateTo.Text != "")
            {
                todate = dml.dateconvertString(txtDateTo.Text);
            }
            else
            {
                todate = "1900-01-01";
            }

            string ed, df, dt;
            ed = txtEntryDate.Text;
            df = fromdate;
            dt = todate;

            DataSet ds_usergrpname = dml.Find("select UserGrpId from SET_UserGrp where Sno= " + lblUserGrpName.SelectedItem.Value + "");
            string usergrpid = ds_usergrpname.Tables[0].Rows[0]["UserGrpId"].ToString();

             DataSet dscheck = dml.Find("select * from SET_UserGrp_DocAuthority WHERE ([UserGrpDocAuthId]='"+ViewState["SNO"].ToString()+"') AND ([AuthorityId]='"+lblAuth.SelectedItem.Value+"') AND ([UserGrpId]='"+ usergrpid + "') AND ([CompID]='"+compid()+"') AND ([BranchId]='"+branchId()+"') AND ([DocId]='"+lblDoc.SelectedItem.Value+"') AND ([EntryDate]='"+dml.dateconvertString(ed)+"') AND ([DateFrom]='"+dml.dateconvertString(df)+ "') AND ([DateTo]='" + dml.dateconvertString(dt) + "') AND ([Remarks]='"+ txtremarkschange+"') AND ([Active]='" +chk+"') AND  ([Record_Deleted]='0')");
            if (dscheck.Tables[0].Rows.Count > 0)
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
            else
            {
                //DataSet ds_usermenuid = dml.Find("select MenuId from SET_Menu where Sno = " + lblmenu.SelectedItem.Value + "");
                //string menuid = ds_usermenuid.Tables[0].Rows[0]["MenuId"].ToString();
                //DataSet ds_userformid = dml.Find("select FormId from SET_Form where Sno = " + lblform.SelectedItem.Value + "");
                //string formid = ds_userformid.Tables[0].Rows[0]["FormId"].ToString();
                ////DataSet ds_FEndDate = dml.Find("");

                dml.Update(" UPDATE SET_UserGrp_DocAuthority SET [AuthorityId]='" + lblAuth.SelectedItem.Value + "', [UserGrpId]='" + usergrpid + "', [DocId]='" + lblDoc.SelectedItem.Value + "', [EntryDate]='" + dml.dateconvertforinsert(txtEntryDate) + "', [DateFrom]='" + df + "', [DateTo]='" + todate + "', [Remarks]='" + txtremarkschange + "', [Active]='" + chk + "', [UpdateDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', [UpdatedBy]='" + show_username() + "' WHERE UserGrpDocAuthId ='" + ViewState["SNO"].ToString() + "'", "");
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
            dml.Delete("update SET_UserGrp_DocAuthority set Record_Deleted = 1 where UserGrpDocAuthId =  " + ViewState["SNO"].ToString() + "", "");
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
            string squer = "select * from View_DocAuthority";

            if (ddlEdit_GOCName.SelectedIndex != 0)
            {
                swhere = "AuthorityId = '" + ddlEdit_GOCName.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere +  " AuthorityId is not null";
            }

            if (ddlEdit_DOC.SelectedIndex != 0)
            {
                swhere = swhere + " and DocId = '" + ddlEdit_DOC.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and AuthorityId is not null";
            }

            if (ddlEdit_USergrpName.SelectedIndex != 0)
            {
                swhere = swhere +  " and  UserGrpId = '" + ddlEdit_USergrpName.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere +  " and UserGrpId is not null";
            }

            if (ChkEdit_Active.Checked == true)
            {
                if (swhere == "")
                {
                    swhere = " Active = '1'";
                }
                else
                {
                    swhere = swhere + " and Active = '1'";
                }
            }
            else if (ChkEdit_Active.Checked == false)
            {
                swhere = swhere + " and Active = '0'";
            }
            else
            {
                swhere = swhere + " and Active is not null";
            }
            string fiscal = Request.QueryString["fiscaly"];
            string stdate = dml.daterangefiscal_start(fiscal);
            string enddate = dml.daterangefiscal_end(fiscal);
            

            squer = squer + " where " + swhere + "  and Record_Deleted = 0  and (EntryDate > '" + stdate + "' and EntryDate < '" + enddate + "') Order By DocDescription";



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
            string swhere="";
            string squer = "select * from View_DocAuthority";

            if (ddlFInd_AUTH.SelectedIndex != 0)
            {
                swhere = "AuthorityId = '" + ddlFInd_AUTH.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " AuthorityId is not null";
            }

            if (ddlFind_DOC.SelectedIndex != 0)
            {
                swhere = swhere + " and DocId = '" + ddlFind_DOC.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and DocId is not null";
            }

            if (ddlFind_UserGrpName.SelectedIndex != 0)
            {
                swhere = swhere + " and  UserGrpId = '" + ddlFind_UserGrpName.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and UserGrpId is not null";
            }

            if (ChkEdit_Active.Checked == true)
            {
                if (swhere == "")
                {
                    swhere = " Active = '1'";
                }
                else
                {
                    swhere = swhere + " and Active = '1'";
                }
            }
            else if (ChkEdit_Active.Checked == false)
            {
                swhere = swhere + " and Active = '0'";
            }
            else
            {
                swhere = swhere + " and Active is not null";
            }
            string fiscal = Request.QueryString["fiscaly"];
            string stdate = dml.daterangefiscal_start(fiscal);
            string enddate = dml.daterangefiscal_end(fiscal);
            squer = squer + " where " + swhere + " and Record_Deleted = 0  and (EntryDate > '" + stdate + "' and EntryDate < '" + enddate + "') Order By DocDescription";


            btnInsert.Visible = false;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            btnFind.Visible = true;
            btnCancel.Visible = true;

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
            string swhere = "";
            string squer = "select * from View_DocAuthority";

            if (ddlDel_AUTH.SelectedIndex != 0)
            {
                swhere = "AuthorityId = '" + ddlDel_AUTH.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " AuthorityId is not null";
            }

            if (ddlDel_DOC.SelectedIndex != 0)
            {
                swhere = swhere + " and DocId = '" + ddlDel_DOC.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and AuthorityId is not null";
            }

            if (ddlDel_UserGrpName.SelectedIndex != 0)
            {
                swhere = swhere + " and  UserGrpId = '" + ddlDel_UserGrpName.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and UserGrpId is not null";
            }

            if (ChkEdit_Active.Checked == true)
            {
                if (swhere == "")
                {
                    swhere = swhere + " Active = '1'";
                }
                else
                {
                    swhere = swhere + " and Active = '1'";
                }
            }
            else if (ChkEdit_Active.Checked == false)
            {
                swhere = swhere + " and Active = '0'";
            }
            else
            {
                swhere = swhere + " and Active is not null";
            }
            string fiscal = Request.QueryString["fiscaly"];
            string stdate = dml.daterangefiscal_start(fiscal);
            string enddate = dml.daterangefiscal_end(fiscal);
            squer = squer + " where " + swhere + " and Record_Deleted = 0  and (EntryDate > '" + stdate + "' and EntryDate < '" + enddate + "') Order By DocDescription";

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

    public void textClear()
    {
        lblUserGrpName.SelectedIndex = 0;
        lblDoc.SelectedIndex = 0;
        lblAuth.SelectedIndex = 0;

        txtCreatedBy.Text = "";
       
        txtcreatedDate.Text = "";
        txtEntryDate.Text = "";
        txtRemarks.Text = "";
        txtDateFrom.Text = "";
        txtDateTo.Text = "";

        Label1.Text = "";

        chkActive_Status.Checked = false;

        lblAuth.Enabled = false;
        
        lblDoc.Enabled = false;
        txtCreatedBy.Enabled = false;
        lblUserGrpName.Enabled = false;

        imgPopup.Enabled = false;
        ImageButton1.Enabled = false;
        ImageButton2.Enabled = false;


        txtcreatedDate.Enabled = false;
        txtEntryDate.Enabled = false;
        txtRemarks.Enabled = false;
        txtDateFrom.Enabled = false;
        txtDateTo.Enabled = false;

        chkActive_Status.Enabled = false;
        updatecol.Visible = false;
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
            ViewState["SNO"] = serialno;
            //DataSet ds = dml.Find("select * from View_UserGrpFrm where Sno = " + serialno);

            //if (ds.Tables[0].Rows.Count > 0)

            {
                DataSet dds = dml.Find("select * from SET_UserGrp_DocAuthority where UserGrpDocAuthId = " + serialno);
                string usrG;
                lblUserGrpName.ClearSelection();
                lblAuth.ClearSelection();
                lblDoc.ClearSelection();
                usrG = dds.Tables[0].Rows[0]["UserGrpId"].ToString();


                DataSet dss = dml.Find("select * from SET_UserGrp where UserGrpId = '" + usrG + "'");
                if (dss.Tables[0].Rows.Count > 0)
                {
                    lblUserGrpName.Items.FindByText(dss.Tables[0].Rows[0]["UserGrpName"].ToString()).Selected = true;
                }
                // lblUserGrpName.Items.FindByValue(dds.Tables[0].Rows[0]["UserGrpId"].ToString()).Selected = true ;
                lblAuth.Items.FindByValue(dds.Tables[0].Rows[0]["AuthorityId"].ToString()).Selected = true;
                lblDoc.Items.FindByValue(dds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;

                DateTime sys = DateTime.Parse(dds.Tables[0].Rows[0]["CreateDate"].ToString());
                bool chk = bool.Parse(dds.Tables[0].Rows[0]["Active"].ToString());
                txtEntryDate.Text = dds.Tables[0].Rows[0]["EntryDate"].ToString();
                dml.dateConvert(txtEntryDate);
                txtcreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                txtDateFrom.Text = dds.Tables[0].Rows[0]["DateFrom"].ToString();
                txtDateTo.Text = dds.Tables[0].Rows[0]["DateTo"].ToString();
                txtRemarks.Text = dds.Tables[0].Rows[0]["Remarks"].ToString();
                dml.dateConvert(txtDateFrom);
                dml.dateConvert(txtDateTo);


                if (chk == true)
                {
                    chkActive_Status.Checked = true;
                }
                else
                {
                    chkActive_Status.Checked = false;
                }
               
              
                txtCreatedBy.Text = dds.Tables[0].Rows[0]["CreatedBy"].ToString();
                lblDoc.Enabled = false;
               
                txtCreatedBy.Enabled = false;
                lblUserGrpName.Enabled = false;
                txtcreatedDate.Enabled = false;
                txtEntryDate.Enabled = false;
                txtDateFrom.Enabled = false;
                txtDateTo.Enabled = false;

                chkActive_Status.Enabled = false;
                
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
            ViewState["SNO"] = serialno;
            //  DataSet dds = dml.Find("select * from SET_UserGrp_DocAuthority where UserGrpDocAuthId = " + serialno);

            {
                DataSet dds = dml.Find("select * from SET_UserGrp_DocAuthority where UserGrpDocAuthId = " + serialno);
                string usrG;
                lblUserGrpName.ClearSelection();
                lblAuth.ClearSelection();
                lblDoc.ClearSelection();
                usrG = dds.Tables[0].Rows[0]["UserGrpId"].ToString();


                DataSet dss = dml.Find("select * from SET_UserGrp where UserGrpId = '" + usrG + "'");
                if (dss.Tables[0].Rows.Count > 0)
                {
                    lblUserGrpName.Items.FindByText(dss.Tables[0].Rows[0]["UserGrpName"].ToString()).Selected = true;
                }
                // lblUserGrpName.Items.FindByValue(dds.Tables[0].Rows[0]["UserGrpId"].ToString()).Selected = true ;
                lblAuth.Items.FindByValue(dds.Tables[0].Rows[0]["AuthorityId"].ToString()).Selected = true;
                lblDoc.Items.FindByValue(dds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;

                DateTime sys = DateTime.Parse(dds.Tables[0].Rows[0]["CreateDate"].ToString());
                bool chk = bool.Parse(dds.Tables[0].Rows[0]["Active"].ToString());
                txtEntryDate.Text = dds.Tables[0].Rows[0]["EntryDate"].ToString();
                dml.dateConvert(txtEntryDate);
                txtcreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                txtDateFrom.Text = dds.Tables[0].Rows[0]["DateFrom"].ToString();
                txtDateTo.Text = dds.Tables[0].Rows[0]["DateTo"].ToString();
                txtRemarks.Text = dds.Tables[0].Rows[0]["Remarks"].ToString();
                dml.dateConvert(txtDateFrom);
                dml.dateConvert(txtDateTo);


                if (chk == true)
                {
                    chkActive_Status.Checked = true;
                }
                else
                {
                    chkActive_Status.Checked = false;
                }


                txtCreatedBy.Text = dml.show_usernameFED(dds.Tables[0].Rows[0]["CreatedBy"].ToString());
                lblDoc.Enabled = false;

                txtCreatedBy.Enabled = false;
                lblUserGrpName.Enabled = false;
                txtcreatedDate.Enabled = false;
                txtEntryDate.Enabled = false;
                txtDateFrom.Enabled = false;
                txtDateTo.Enabled = false;

                chkActive_Status.Enabled = false;

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
            ViewState["SNO"] = serialno;

            DataSet dds = dml.Find("select * from SET_UserGrp_DocAuthority where UserGrpDocAuthId = " + serialno);
            string usrG;
            lblUserGrpName.ClearSelection();
            lblAuth.ClearSelection();
            lblDoc.ClearSelection();
            usrG = dds.Tables[0].Rows[0]["UserGrpId"].ToString();


            DataSet dss = dml.Find("select * from SET_UserGrp where UserGrpId = '" + usrG + "'");
            if (dss.Tables[0].Rows.Count > 0)
            {
                lblUserGrpName.Items.FindByText(dss.Tables[0].Rows[0]["UserGrpName"].ToString()).Selected = true;
            }
            // lblUserGrpName.Items.FindByValue(dds.Tables[0].Rows[0]["UserGrpId"].ToString()).Selected = true ;
            lblAuth.Items.FindByValue(dds.Tables[0].Rows[0]["AuthorityId"].ToString()).Selected = true;
            lblDoc.Items.FindByValue(dds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;

            DateTime sys = DateTime.Parse(dds.Tables[0].Rows[0]["CreateDate"].ToString());
            bool chk = bool.Parse(dds.Tables[0].Rows[0]["Active"].ToString());
            txtEntryDate.Text = dds.Tables[0].Rows[0]["EntryDate"].ToString();
            dml.dateConvert(txtEntryDate);
            txtcreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
            txtDateFrom.Text = dds.Tables[0].Rows[0]["DateFrom"].ToString();
            txtDateTo.Text = dds.Tables[0].Rows[0]["DateTo"].ToString();
            txtRemarks.Text = dds.Tables[0].Rows[0]["Remarks"].ToString();
            dml.dateConvert(txtDateFrom);

            if (dml.dateconvertString(txtDateTo.Text) != "2000-01-01")
            {
                dml.dateConvert(txtDateTo);
            }
            else
            {
                txtDateTo.Text = "";
            }
           

                if (chk == true)
                {
                    chkActive_Status.Checked = true;
                }
                else
                {
                    chkActive_Status.Checked = false;
                }


            txtCreatedBy.Text = dml.show_usernameFED(dds.Tables[0].Rows[0]["CreatedBy"].ToString());
            lblAuth.Enabled = true;
                lblDoc.Enabled = true;
                 txtRemarks.Enabled = true;
                txtCreatedBy.Enabled = false;

                imgPopup.Enabled = true;
                 ImageButton1.Enabled = true;
                  ImageButton2.Enabled = true;

            lblUserGrpName.Enabled = false;
                txtcreatedDate.Enabled = false;
                txtEntryDate.Enabled = true;
                txtDateFrom.Enabled = true;
                txtDateTo.Enabled = true;

                chkActive_Status.Enabled = true;

            
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


    protected void txtDateFrom_TextChanged(object sender, EventArgs e)
    {
        txtDateTo.Text = "";
        DateTime dt = DateTime.Parse(txtDateFrom.Text);
        CalendarExtender2.StartDate = dt;
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string id = e.Row.Cells[1].Text;
        bool flag = dml.isNumber(id);


        if (flag == true)
        {

            DataSet ds = dml.Find("select UserGrpDocAuthId,MLD from SET_UserGrp_DocAuthority where UserGrpDocAuthId = '" + id + "'");
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

            DataSet ds = dml.Find("select UserGrpDocAuthId,MLD from SET_UserGrp_DocAuthority where UserGrpDocAuthId = '" + id + "'");
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