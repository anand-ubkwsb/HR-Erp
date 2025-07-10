using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class frm_Period : System.Web.UI.Page
{
    int DateFrom, EditDays, DeleteDays, AddDays;
    string userid, UserGrpID, FormID, fiscal;
    DmlOperation dml = new DmlOperation();
    radcomboxclass cmb = new radcomboxclass();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
    FieldHide fd = new FieldHide();

    string itemtype, itemhead, itemsubhead;
    float i;
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
            fiscal = Request.QueryString["fiscaly"];

            dml.dropdownsql(ddlbankName, "SET_Bank", "BankName", "BankID");


            dml.dropdownsql(ddlEdit_AcctTitle, "SET_BankAccount", "AccountTitle", "BankAccountID");
            dml.dropdownsql(ddlFind_AcctTitle, "SET_BankAccount", "AccountTitle", "BankAccountID");
            dml.dropdownsql(ddlDel_AcctTitle, "SET_BankAccount", "AccountTitle", "BankAccountID");

            dml.dropdownsql(ddlEdit_BankName, "SET_Bank", "BankName", "BankID");
            dml.dropdownsql(ddlFind_BankName, "SET_Bank", "BankName", "BankID");
            dml.dropdownsql(ddlDel_BankName, "SET_Bank", "BankName", "BankID");

            dml.dropdownsql(ddlEdit_BankBranchName, "SET_BankBranch", "BankBranchName", "BankBranchID");
            dml.dropdownsql(ddlFind_BankBranchName, "SET_BankBranch", "BankBranchName", "BankBranchID");
            dml.dropdownsql(ddlDel_BankBranchName, "SET_BankBranch", "BankBranchName", "BankBranchID");

            dml.dropdownsql(ddlEdit_BankAcctType, "SET_BankAccountType", "BankAcctTypeName", "BankAcctTypeID");
            dml.dropdownsql(ddlFind_BankAcctType, "SET_BankAccountType", "BankAcctTypeName", "BankAcctTypeID");
            dml.dropdownsql(ddlDel_BankAcctType, "SET_BankAccountType", "BankAcctTypeName", "BankAcctTypeID");



            dml.dropdownsql(ddlBankAccountType, "SET_BankAccountType", "BankAcctTypeName", "BankAcctTypeID");
            dml.dropdownsql(ddlCurrency, "SET_Currency", "CurrencyName", "CurrencyID");
            dml.dropdownsql(ddlBankBranch, "SET_BankBranch", "BankBranchName", "BankBranchID");
            CalendarExtender1.EndDate = DateTime.Now;
           // CalendarExtender2.EndDate = DateTime.Now;

            txtEntryDate.Attributes.Add("readonly", "readonly");
            txtInActiveDate.Attributes.Add("readonly", "readonly");
            txtdatevalidfrom.Attributes.Add("readonly", "readonly");
            textClear();
            updatecol.Visible = false;

            Findbox.Visible = false;
            DeleteBox.Visible = false;
            Editbox.Visible = false;
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

        ddlbankName.Enabled = true;
        ddlBankAccountType.Enabled = true;
        txtBankAccountNumber.Enabled = true;
        ddlBankBranch.Enabled = false;
        txtAccountHolderName1.Enabled = true;
        txtAccountHolderName2.Enabled = true;
        txtAccountHolderName3.Enabled = true;
        txtAccountHolderName4.Enabled = true;
        ddlCurrency.Enabled = true;
        txtDescription.Enabled = true;
        txtInActiveDate.Enabled = true;
        RadComboAcct_Code.Enabled = true;
        txtEntryDate.Enabled = true;
        txtAcctTitle.Enabled = true;
        txtdatevalidfrom.Enabled = true;
        chkActive.Enabled = true;
        imgPopup.Enabled = true;
        ImageButton1.Enabled = true;
        ImageButton2.Enabled = true;
        txtCreatedby.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdateBy.Enabled = true;
        txtUpdateDate.Enabled = true;

        
        chkActive.Checked = true;
        UserGrpID = Request.QueryString["UsergrpID"];
        FormID = Request.QueryString["FormID"];
        dml.dateRuleForm(UserGrpID, FormID, CalendarExtender1, "A");
        txtEntryDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        txtCreatedDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

        userid = Request.QueryString["UserID"];
        DataSet ds_autoUserName = dml.Find("select user_name from SET_User_Manager where UserId ='" + userid + "'");
        txtCreatedby.Text = ds_autoUserName.Tables[0].Rows[0]["user_name"].ToString();

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
       
        chkActive.Enabled = true;
        
        DataSet uniqueg_B_C = dml.Find("select * from SET_BankAccount where BankID = '" + ddlbankName.SelectedItem.Value + "' and BankAccountNumber = '"+txtBankAccountNumber.Text+"' and Record_Deleted = '0'");
        if (uniqueg_B_C.Tables[0].Rows.Count > 0)
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = "Duplicated entry not allowed";
        }
        else {
            int chk = 0;
            if (chkActive.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
           
            dml.Insert("INSERT INTO SET_BankAccount ([BankID], [BankAccTypeID], [BankAccountNumber], [BankBranchID], [CompId], [BranchId], [AccountholderName1], [AccountholderName2], [AccountholderName3], [AccountholderName4], [CurrencyID], [Description], [InActiveDate], [ChartofAccountCode], [EntryDate],  [CreatedBy], [CreateDate], [Record_Deleted], [IsActive],[AccountTitle],[DateValidFrom],[MLD]) VALUES ('" + ddlbankName.SelectedItem.Value +"', '"+ddlBankAccountType.SelectedItem.Value+"', '"+txtBankAccountNumber.Text+"', '"+ddlBankBranch.SelectedItem.Value+"', "+compid()+", "+branchId()+", '"+txtAccountHolderName1.Text+"', '"+txtAccountHolderName2.Text+"', '"+txtAccountHolderName3.Text+"', '"+txtAccountHolderName4.Text+"', '"+ddlCurrency.SelectedItem.Value+"', '"+txtDescription.Text+"', '"+dml.dateconvertforinsert(txtInActiveDate)+"', '"+RadComboAcct_Code.Text+"', '"+dml.dateconvertforinsert(txtEntryDate)+"', '"+show_username()+"', '"+DateTime.Now.ToString()+"', '0', '"+chk+"','"+txtAcctTitle.Text+"','"+dml.dateconvertforinsert(txtdatevalidfrom)+"','"+dml.Encrypt("h")+"')", "");

            dml.Update("update SET_BankAccountType set MLD = '" + dml.Encrypt("q") + "' where BankAcctTypeID = '" + ddlBankBranch.SelectedItem.Value + "'", "");
            dml.Update("update SET_BankBranch set MLD = '" + dml.Encrypt("q") + "' where BankBranchID = '" + ddlBankBranch.SelectedItem.Value + "'", "");
            dml.Update("update SET_Bank set MLD = '" + dml.Encrypt("q") + "' where BankID = '" + ddlbankName.SelectedItem.Value + "'", "");

            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);

            ddlbankName.SelectedIndex = 0;
            ddlBankAccountType.SelectedIndex = 0;
            txtBankAccountNumber.Text = "";
            ddlBankBranch.SelectedIndex = 0;
            txtAccountHolderName1.Text = "";
            txtAccountHolderName2.Text = "";
            txtAccountHolderName3.Text = "";
            txtAccountHolderName4.Text = "";
            ddlCurrency.SelectedIndex = 0;
            txtDescription.Text = "";
            txtdatevalidfrom.Text = "";
            txtAcctTitle.Text = "";
            txtInActiveDate.Text = "";
            RadComboAcct_Code.Text = "";
            txtEntryDate.Text = "";

            chkActive.Enabled = true;

            txtCreatedby.Text = show_username();
            txtCreatedDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
            

            chkActive.Checked = true;
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string entrydate = dml.dateconvertforinsert(txtEntryDate);
        string DateValidForm = dml.dateconvertforinsert(txtInActiveDate);
        int chk = 0;
        if (chkActive.Checked == true)
        {
            chk = 1;
        }
        else
        {
            chk = 0;
        }
       
        DataSet ds_up = dml.Find("select * from SET_BankAccount WHERE ([BankAccountID]='"+ViewState["SNO"].ToString()+ "') AND ([BankID]='" + ddlbankName.SelectedItem.Value+"') AND ([BankAccTypeID]='"+ddlBankAccountType.SelectedItem.Value+"') AND ([BankAccountNumber]='"+txtBankAccountNumber.Text+"') AND ([BankBranchID]='"+ddlBankBranch.SelectedItem.Value+ "') AND ([AccountholderName1]='" + txtAccountHolderName1.Text + "') AND ([AccountholderName2]='" + txtAccountHolderName2.Text+ "') AND ([AccountholderName3]='" + txtAccountHolderName3.Text + "') AND ([AccountholderName4] ='" + txtAccountHolderName4.Text + "') AND ([CurrencyID]='" + ddlCurrency.SelectedItem.Value+"') AND ([Description] ='"+txtDescription.Text+ "') AND ([AccountTitle]='"+txtAcctTitle.Text + "') AND ([DateValidFrom]='"+dml.dateconvertforinsertNEW(txtdatevalidfrom)+"') AND ([InActiveDate]='" + DateValidForm+"') AND ([ChartofAccountCode]='"+RadComboAcct_Code.Text+"') AND ([EntryDate]='"+entrydate+"') AND ([Record_Deleted]='0') AND ([IsActive]='"+chk+"')");

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
            dml.Update("UPDATE SET_BankAccount SET [BankID]='" + ddlbankName.SelectedItem.Value + "', [BankAccTypeID]='" + ddlBankAccountType.SelectedItem.Value + "', [BankAccountNumber]='" + txtBankAccountNumber.Text + "', [BankBranchID]='" + ddlBankBranch.SelectedItem.Value + "', [AccountholderName1]='" + txtAccountHolderName1.Text + "', [AccountholderName2]='" + txtAccountHolderName2.Text + "', [AccountholderName3]='" + txtAccountHolderName3.Text + "', [AccountholderName4]='" + txtAccountHolderName4.Text + "', [CurrencyID]='" + ddlCurrency.SelectedItem.Value + "', [Description]='" + txtDescription.Text + "', [InActiveDate]='" + DateValidForm + "', [ChartofAccountCode]='" + RadComboAcct_Code.Text + "',[AccountTitle] = '"+txtAcctTitle.Text+ "' , [DateValidFrom] = '"+dml.dateconvertforinsertNEW(txtdatevalidfrom)+"', [EntryDate]='" + entrydate + "', [UpdatedBy]='" + show_username() + "', [UpdateDate]='" + DateTime.Now.ToString() +"', [IsActive]='"+chk+"' WHERE ([BankAccountID]='"+ViewState["SNO"].ToString()+"')", "");
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", " Editalert()", true);
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnInsert.Visible = true;
        updatecol.Visible = false;
        btnDelete.Visible = true;
        btnUpdate.Visible = false;
        btnEdit.Visible = true;
        btnFind.Visible = true;
        btnSave.Visible = false;
        btnDelete_after.Visible = false;
        textClear();
        
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
            string swhere = "";
            string squer = "select * from View_SET_BankAccount";


            if (ddlDel_BankName.SelectedIndex != 0)
            {
                swhere = "BankID = '" + ddlDel_BankName.SelectedItem.Value + "'";
            }
            else
            {
                swhere = "BankID is not null";
            }
            if (ddlDel_BankBranchName.SelectedIndex != 0)
            {
                swhere = swhere + " AND BankBranchID = '" + ddlDel_BankBranchName.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " AND BankBranchID is not null";
            }
            if (ddlDel_BankAcctType.SelectedIndex != 0)
            {
                swhere = swhere + " AND BankAccTypeID = '" + ddlDel_BankAcctType.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " AND BankAccTypeID is not null";
            }
            if (ddlDel_AcctTitle.SelectedIndex != 0)
            {
                swhere = swhere + " AND AccountTitle = '" + ddlDel_AcctTitle.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " AND AccountTitle is not null";
            }
            if (txtDel_bankAcctNo.Text != "")
            {
                swhere = swhere + " AND  BankAccountNumber = '" + txtDel_bankAcctNo.Text + "'";
            }
            else
            {
                swhere = swhere + " AND BankAccountNumber is not null";
            }

            if (chkDel_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkDel_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and CompId= '" + compid() + "' and BranchId= '" + branchId() + "'  ORDER BY AccountTitle";

            Findbox.Visible = false;
            fieldbox.Visible = false;
            Editbox.Visible = false;

            DataSet dgrid = dml.grid(squer);
            GridView2.DataSource = dgrid;
            GridView2.DataBind();
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
            //SELECT DepartmentId, BPartnerId,BillNo,IsActive FROM SET_ItemMasterOpening WHERE GocID = '1' AND CompId = '1' AND BranchId='5' AND IsActive = '1' AND Record_Deleted = '0'
            GridView1.DataBind();
            string swhere = "";
            string squer = "select * from View_SET_BankAccount";


            if (ddlFind_BankName.SelectedIndex != 0)
            {
                swhere = "BankID = '" + ddlFind_BankName.SelectedItem.Value + "'";
            }
            else
            {
                swhere = "BankID is not null";
            }
            if (ddlFind_BankBranchName.SelectedIndex != 0)
            {
                swhere = swhere + " AND BankBranchID = '" + ddlFind_BankBranchName.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " AND  BankBranchID is not null";
            }
            if (ddlFind_BankAcctType.SelectedIndex != 0)
            {
                swhere = swhere + " AND  BankAccTypeID = '" + ddlFind_BankAcctType.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " AND BankAccTypeID is not null";
            }
            if (ddlFind_AcctTitle.SelectedIndex != 0)
            {
                swhere = swhere + " AND  AccountTitle = '" + ddlFind_AcctTitle.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " AND AccountTitle is not null";
            }
            if (txtFind_BankAcctNo.Text != "")
            {
                swhere = swhere + " AND BankAccountNumber = '" + txtFind_BankAcctNo.Text + "'";
            }
            else
            {
                swhere = swhere + " AND BankAccountNumber is not null";
            }

            if (chkDel_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkDel_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and CompId= '" + compid() + "' and BranchId= '" + branchId() + "'  ORDER BY AccountTitle";

            Findbox.Visible = true;
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
            
            //SELECT Menu_Title, ModuleId, Menu_SubMenu, IsActive from SET_Menu
            GridView3.DataBind();
            string swhere = "";
            string squer = "select * from View_SET_BankAccount";


            if (ddlEdit_BankName.SelectedIndex != 0)
            {
                swhere = "BankID = '" + ddlEdit_BankName.SelectedItem.Value + "'";
            }
            else
            {
                swhere = "BankID is not null";
            }

            if (ddlEdit_BankBranchName.SelectedIndex != 0)
            {
                swhere = swhere + " AND BankBranchID = '" + ddlEdit_BankBranchName.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " AND BankBranchID is not null";
            }
            if (ddlEdit_BankAcctType.SelectedIndex != 0)
            {
                swhere = swhere + " AND BankAccTypeID = '" + ddlEdit_BankAcctType.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " AND BankAccTypeID is not null";
            }
            if (ddlEdit_AcctTitle.SelectedIndex != 0)
            {
                swhere = swhere + " AND AccountTitle = '" + ddlEdit_AcctTitle.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " AND AccountTitle is not null";
            }
            if (txtEdit_BankAcctno.Text != "")
            {
                swhere = swhere + " AND BankAccountNumber = '" + txtEdit_BankAcctno.Text + "'";
            }
            else
            {
                swhere = swhere + " AND BankAccountNumber is not null";
            }

            if (chkDel_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkDel_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and CompId= '" + compid() + "' and BranchId= '" + branchId() + "'  ORDER BY AccountTitle";

            Findbox.Visible = false;
            fieldbox.Visible = false;
            DeleteBox.Visible = false;

            DataSet dgrid = dml.grid(squer);
            GridView3.DataSource = dgrid;
            GridView3.DataBind();
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    public void textClear()
    {
        Label1.Text = "";
        DeleteBox.Visible = false;
        Editbox.Visible = false;
        Findbox.Visible = false;
        fieldbox.Visible = true;

        ddlbankName.SelectedIndex=0;
        ddlBankAccountType.SelectedIndex = 0;
        txtBankAccountNumber.Text = "";
        ddlBankBranch.SelectedIndex = 0;
        txtAccountHolderName1.Text = "";
        txtAccountHolderName2.Text = "";
        txtAccountHolderName3.Text = "";
        txtAccountHolderName4.Text = "";
        ddlCurrency.SelectedIndex = 0;
        txtDescription.Text = "";
        txtInActiveDate.Text = "";
        RadComboAcct_Code.Text = "";
        txtEntryDate.Text = "";

        txtAcctTitle.Text = "";
        txtdatevalidfrom.Text = "";
        txtAcctTitle.Enabled = false;
        txtdatevalidfrom.Enabled = false;
        ImageButton2.Enabled = false;
        chkActive.Checked = false;

        txtCreatedby.Text = "";
        txtCreatedDate.Text = "";
        txtUpdateBy.Text = "";
        txtUpdateDate.Text = "";

        ddlbankName.Enabled = false;
        ddlBankAccountType.Enabled = false;
        txtBankAccountNumber.Enabled = false;
        ddlBankBranch.Enabled = false;
        txtAccountHolderName1.Enabled = false;
        txtAccountHolderName2.Enabled = false;
        txtAccountHolderName3.Enabled = false;
        txtAccountHolderName4.Enabled = false;
        ddlCurrency.Enabled = false;
        txtDescription.Enabled = false;
        txtInActiveDate.Enabled = false;
        RadComboAcct_Code.Enabled = false;
        txtEntryDate.Enabled = false;

        chkActive.Enabled = false;
        imgPopup.Enabled = false;
        ImageButton1.Enabled = false;

        txtCreatedby.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdateBy.Enabled = false;
        txtUpdateDate.Enabled = false;





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
        try
        {
            dml.Delete("update SET_BankAccount set Record_Deleted = 1 where BankAccountID = " + ViewState["SNO"].ToString() + "", "");
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

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        btnUpdate.Visible = false;
        btnInsert.Visible = false;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        Label1.Text = "";

        updatecol.Visible = true;
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {
            string serial_id;
            serial_id = GridView1.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;



            DataSet ds = dml.Find("select * from SET_BankAccount WHERE ([BankAccountID]='" + serial_id+"') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlBankAccountType.ClearSelection();
                ddlBankBranch.ClearSelection();
                ddlCurrency.ClearSelection();
                ddlbankName.ClearSelection();
                ddlbankName.Items.FindByValue(ds.Tables[0].Rows[0]["BankID"].ToString()).Selected = true;
                //txtBankAccountName.Text = ds.Tables[0].Rows[0]["BankAccountName"].ToString();
                ddlBankAccountType.Items.FindByValue(ds.Tables[0].Rows[0]["BankAccTypeID"].ToString()).Selected = true;
                txtBankAccountNumber.Text = ds.Tables[0].Rows[0]["BankAccountNumber"].ToString();
                ddlBankBranch.Items.FindByValue(ds.Tables[0].Rows[0]["BankBranchID"].ToString()).Selected = true;
                txtAccountHolderName1.Text = ds.Tables[0].Rows[0]["AccountholderName1"].ToString();
                txtAccountHolderName2.Text = ds.Tables[0].Rows[0]["AccountholderName2"].ToString();
                txtAccountHolderName3.Text = ds.Tables[0].Rows[0]["AccountholderName3"].ToString();
                txtAccountHolderName4.Text = ds.Tables[0].Rows[0]["AccountholderName4"].ToString();
                txtAcctTitle.Text = ds.Tables[0].Rows[0]["AccountTitle"].ToString();
                txtdatevalidfrom.Text = ds.Tables[0].Rows[0]["DateValidFrom"].ToString();
                dml.dateConvert(txtdatevalidfrom);
                ddlCurrency.Items.FindByValue(ds.Tables[0].Rows[0]["CurrencyID"].ToString()).Selected = true;
                txtDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                txtInActiveDate.Text = ds.Tables[0].Rows[0]["InActiveDate"].ToString();
                RadComboAcct_Code.Text = ds.Tables[0].Rows[0]["ChartofAccountCode"].ToString();
                txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                
                
                txtCreatedby.Text = ds.Tables[0].Rows[0]["CreatedBy"].ToString();
                txtUpdateBy.Text = ds.Tables[0].Rows[0]["UpdatedBy"].ToString();

               if(chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
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
                dml.dateConvert(txtEntryDate);
                dml.dateConvert(txtInActiveDate);
                
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
        txtUpdateDate.Enabled = false;
        txtUpdateBy.Enabled = false;
        textClear();

        updatecol.Visible = true;
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        DeleteBox.Visible = false;

        try
        {
            string serial_id;
            serial_id = GridView2.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;



            DataSet ds = dml.Find("select * from SET_BankAccount WHERE ([BankAccountID]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlBankAccountType.ClearSelection();
                ddlBankBranch.ClearSelection();
                ddlCurrency.ClearSelection();
                ddlbankName.ClearSelection();
                ddlbankName.Items.FindByValue(ds.Tables[0].Rows[0]["BankID"].ToString()).Selected = true;
                ddlBankAccountType.Items.FindByValue(ds.Tables[0].Rows[0]["BankAccTypeID"].ToString()).Selected = true;
                txtBankAccountNumber.Text = ds.Tables[0].Rows[0]["BankAccountNumber"].ToString();
                ddlBankBranch.Items.FindByValue(ds.Tables[0].Rows[0]["BankBranchID"].ToString()).Selected = true;
                txtAccountHolderName1.Text = ds.Tables[0].Rows[0]["AccountholderName1"].ToString();
                txtAccountHolderName2.Text = ds.Tables[0].Rows[0]["AccountholderName2"].ToString();
                txtAccountHolderName3.Text = ds.Tables[0].Rows[0]["AccountholderName3"].ToString();
                txtAccountHolderName4.Text = ds.Tables[0].Rows[0]["AccountholderName4"].ToString();
                txtAcctTitle.Text = ds.Tables[0].Rows[0]["AccountTitle"].ToString();
                txtdatevalidfrom.Text = ds.Tables[0].Rows[0]["DateValidFrom"].ToString();
                dml.dateConvert(txtdatevalidfrom);
                ddlCurrency.Items.FindByValue(ds.Tables[0].Rows[0]["CurrencyID"].ToString()).Selected = true;
                txtDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                txtInActiveDate.Text = ds.Tables[0].Rows[0]["InActiveDate"].ToString();
                RadComboAcct_Code.Text = ds.Tables[0].Rows[0]["ChartofAccountCode"].ToString();
                txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());


                txtCreatedby.Text = ds.Tables[0].Rows[0]["CreatedBy"].ToString();
                txtUpdateBy.Text = ds.Tables[0].Rows[0]["UpdatedBy"].ToString();

                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
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
                dml.dateConvert(txtEntryDate);
                dml.dateConvert(txtInActiveDate);

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

        ddlbankName.Enabled = true;
        ddlBankAccountType.Enabled = true;
        txtBankAccountNumber.Enabled = true;
        ddlBankBranch.Enabled = true;
        txtAcctTitle.Enabled = true;
        txtdatevalidfrom.Enabled = true;
        ImageButton2.Enabled = true;
        txtAccountHolderName1.Enabled = true;
        txtAccountHolderName2.Enabled = true;
        txtAccountHolderName3.Enabled = true;
        txtAccountHolderName4.Enabled = true;
        ddlCurrency.Enabled = true;
        txtDescription.Enabled = true;
        txtInActiveDate.Enabled = true;
        RadComboAcct_Code.Enabled = true;
        txtEntryDate.Enabled = true;

        chkActive.Enabled = true;
        imgPopup.Enabled = true;
        ImageButton1.Enabled = true;

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
            string serial_id;
            serial_id = GridView3.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;



            DataSet ds = dml.Find("select * from SET_BankAccount WHERE ([BankAccountID]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlBankAccountType.ClearSelection();
                ddlBankBranch.ClearSelection();
                ddlCurrency.ClearSelection();
                ddlbankName.ClearSelection();
                ddlbankName.Items.FindByValue(ds.Tables[0].Rows[0]["BankID"].ToString()).Selected = true;
                ddlBankAccountType.Items.FindByValue(ds.Tables[0].Rows[0]["BankAccTypeID"].ToString()).Selected = true;
                txtBankAccountNumber.Text = ds.Tables[0].Rows[0]["BankAccountNumber"].ToString();
                ddlbankName_SelectedIndexChanged(sender, e);

                ddlBankBranch.Items.FindByValue(ds.Tables[0].Rows[0]["BankBranchID"].ToString()).Selected = true;
                txtAccountHolderName1.Text = ds.Tables[0].Rows[0]["AccountholderName1"].ToString();
                txtAccountHolderName2.Text = ds.Tables[0].Rows[0]["AccountholderName2"].ToString();
                txtAccountHolderName3.Text = ds.Tables[0].Rows[0]["AccountholderName3"].ToString();
                txtAccountHolderName4.Text = ds.Tables[0].Rows[0]["AccountholderName4"].ToString();
                txtAcctTitle.Text = ds.Tables[0].Rows[0]["AccountTitle"].ToString();
                txtdatevalidfrom.Text = ds.Tables[0].Rows[0]["DateValidFrom"].ToString();
                dml.dateConvert(txtdatevalidfrom);
                ddlCurrency.Items.FindByValue(ds.Tables[0].Rows[0]["CurrencyID"].ToString()).Selected = true;
                txtDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                txtInActiveDate.Text = ds.Tables[0].Rows[0]["InActiveDate"].ToString();
                RadComboAcct_Code.Text = ds.Tables[0].Rows[0]["ChartofAccountCode"].ToString();
                txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());


                txtCreatedby.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdateBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());

                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
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
                dml.dateConvert(txtEntryDate);
                dml.dateConvert(txtInActiveDate);

                updatecol.Visible = true;


            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }

    }
    public string show_username()
    {
        userid = Request.QueryString["UserID"];
        return userid;
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


    protected void RadComboAcct_Code_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        //SELECT Acct_Code,Acct_Description,Acct_Type_Name,Tran_Type from view_Search_Acct_Code
        //Select * from SET_COA_detail where (Head_detail_ID = 'd1') and (Acct_Type_ID = '1' OR Acct_Type_ID = '2' or Acct_Type_ID = '3' or  Acct_Type_ID = '6')
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(RadComboAcct_Code, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }

    protected void ddlbankName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBankBranch.Enabled = true;
        if (ddlbankName.SelectedIndex != 0)
        {
            dml.dropdownsqlwithquery(ddlBankBranch, "select BankBranchID,BankBranchName from SET_BankBranch where BankID = '"+ddlbankName.SelectedItem.Value+"'", "BankBranchName", "BankBranchID");
        }
        else
        {
            ddlBankBranch.Enabled = false;
        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string id = e.Row.Cells[1].Text;
        bool flag = dml.isNumber(id);


        if (flag == true)
        {

            DataSet ds = dml.Find("select BankAccountID,MLD from SET_BankAccount where BankAccountID = '" + id + "'");
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

            DataSet ds = dml.Find("select BankAccountID,MLD from SET_BankAccount where BankAccountID = '" + id + "'");
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