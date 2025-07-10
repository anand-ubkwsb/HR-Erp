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
    int DateFrom,EditDays,DeleteDays,AddDays;
    int FiscalYearId = 0,GocId=0,CompId=0,BranchId=0;
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
            FiscalYearId =Convert.ToInt32(Request.Cookies["FiscalYearId"].Value);
            GocId = Convert.ToInt32(Request.Cookies["GocId"].Value);
            CompId = Convert.ToInt32(Request.Cookies["CompId"].Value);
            BranchId=Convert.ToInt32(Request.Cookies["BranchId"].Value);
            btnUpdate.Visible = false;
            btnSave.Visible = false;
            btnDelete_after.Visible = false;
            userid = Request.QueryString["UserID"];
            UserGrpID = Request.QueryString["UsergrpID"];
            FormID = Request.QueryString["FormID"];
            fiscal = Request.QueryString["fiscaly"];

            dml.dropdownsql(ddlEdit_MenuTitle, "SET_BankAccountDetail", "BankAccountNumber", "BankAcctDetailID");
            dml.dropdownsql(ddlFind_MenuName, "SET_BankAccountDetail", "BankAccountNumber", "BankAcctDetailID");
            dml.dropdownsql(ddlDel_MenuName, "SET_BankAccountDetail", "BankAccountNumber", "BankAcctDetailID");
            //ddlBank
            dml.dropdownsql(ddlBank, "SET_Bank", "BankName", "BankID");
            dml.dropdownsql(ddlPeriod, "SET_Period", "Description", "PeriodID");
            dml.dropdownsql(ddlBankBranch, "SET_BankBranch", "BankBranchName", "BankBranchID");
            // CalendarExtender1.EndDate = DateTime.Now;
            //   CalendarExtender2.EndDate = DateTime.Now;
            txtEntryDate.Attributes.Add("readonly", "readonly");
            txtDateValidForm.Attributes.Add("readonly", "readonly");


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

        ddlBank.Enabled = true;
        txtBankAccountNumber.Enabled = true;
        txtEntryDate.Enabled = true;
        txtChequeNoFrom.Enabled = true;
        txtChequeNoTo.Enabled = true;
        ddlPeriod.Enabled = true;
        ddlBankBranch.Enabled = false;
        txtDateValidForm.Enabled = true;
        chkActive.Enabled = true;
        imgPopup.Enabled = true;
        ImageButton1.Enabled = true;

        txtCreatedby.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdateBy.Enabled = true;
        txtUpdateDate.Enabled = true;


        chkActive.Checked = true;


        txtCreatedDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

        userid = Request.QueryString["UserID"];
        DataSet ds_autoUserName = dml.Find("select user_name from SET_User_Manager where UserId ='" + userid + "'");
        txtCreatedby.Text = ds_autoUserName.Tables[0].Rows[0]["user_name"].ToString();

        txtEntryDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        chkActive.Enabled = true;
        string entrydate = dml.dateconvertforinsert(txtEntryDate);
        string DateValidForm = dml.dateconvertforinsert(txtDateValidForm);
        DataSet uniqueg_B_C = dml.Find("select * from SET_BankAccountDetail where BankAccountNumber = '" + txtBankAccountNumber.Text + "' and Record_Deleted = '0'");
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
            string periodid = "";
            DataSet ds_period = dml.Find("select PeriodID FROM SET_Fiscal_Year where FiscalYearID= '" + FiscalYear() + "'");
            if (ds_period.Tables[0].Rows.Count > 0)
            {
                periodid = ds_period.Tables[0].Rows[0]["PeriodID"].ToString();
            }


            DataSet ds = dml.Find("INSERT INTO SET_BankAccountDetail ([BankAccountNumber], [EntryDate], [ChqNoFrom], [ChqNoTo], [PeriodID],[BankID], [BankBranchID], [DateValidForm], [Active], [Status], [GUID], [CompID], [CreatedBy], [CreateDate], [Record_Deleted]) VALUES ('" + txtBankAccountNumber.Text + "', '" + entrydate + "', '" + txtChequeNoFrom.Text + "', '" + txtChequeNoTo.Text + "', '" + periodid + "', '"+ddlBank.SelectedItem.Value+"' ,'" + ddlBankBranch.SelectedItem.Value + "', '" + DateValidForm + "', '" + chk + "', '1', NULL, " + compid() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0'); SELECT BankAcctDetailID FROM SET_BankAccountDetail WHERE BankAcctDetailID = SCOPE_IDENTITY();");
            dml.Update("update SET_BankBranch set MLD = '" + dml.Encrypt("q") + "' where BankBranchID = '" + ddlBankBranch.SelectedItem.Value + "'", "");

            //select BankBranchID from SET_BankBranch
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["masterID"] = ds.Tables[0].Rows[0]["BankAcctDetailID"].ToString();
            }
            detailInsert();


            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);

            txtBankAccountNumber.Text = "";
            txtEntryDate.Text = "";
            txtChequeNoFrom.Text = "";
            txtChequeNoTo.Text = "";
            ddlPeriod.SelectedIndex = 0;
            ddlBankBranch.SelectedIndex = 0;
            txtDateValidForm.Text = "";
            chkActive.Checked = false;

            txtCreatedby.Text = show_username();
            txtCreatedDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");


            chkActive.Checked = true;
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string entrydate = dml.dateconvertforinsert(txtEntryDate);
        string DateValidForm = dml.dateconvertforinsert(txtDateValidForm);
        int chk = 0;
        if (chkActive.Checked == true)
        {
            chk = 1;
        }
        else
        {
            chk = 0;
        }

        DataSet ds_up = dml.Find("select * from SET_BankAccountDetail  WHERE ([BankAcctDetailID]='" + ViewState["SNO"].ToString() + "') AND ([BankAccountNumber]='" + txtBankAccountNumber.Text + "') AND ([EntryDate]='" + entrydate + "') AND ([BankID]='" + ddlBank.SelectedItem.Value + "') AND  ([ChqNoFrom]='" + txtChequeNoFrom.Text + "') AND ([ChqNoTo]='" + txtChequeNoTo.Text + "') AND ([PeriodID]='" + ddlPeriod.SelectedItem.Value + "') AND ([BankBranchID]='" + ddlBankBranch.SelectedItem.Value + "') AND ([DateValidForm]='" + DateValidForm + "') AND ([Active]='" + chk + "') AND ([Record_Deleted]='0')");

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



            dml.Update("UPDATE [SET_BankAccountDetail] SET [BankAccountNumber]='" + txtBankAccountNumber.Text + "', [EntryDate]='" + entrydate + "', [ChqNoFrom]='" + txtChequeNoFrom.Text + "', [ChqNoTo]='" + txtChequeNoTo.Text + "', [PeriodID]='" + ddlPeriod.SelectedItem.Value + "',[BankID]='" + ddlBank.SelectedItem.Value + "', [BankBranchID]='" + ddlBankBranch.SelectedItem.Value + "', [DateValidForm]='" + DateValidForm + "', [Active]='" + chk + "', [UpdatedBy]='" + show_username() + "', [UpdateDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "'  WHERE ([BankAcctDetailID]='" + ViewState["SNO"].ToString() + "')", "");

            DataSet ds = dml.Find("select  * from SET_BankChequeDetail where Sno_Master = '" + ViewState["SNO"].ToString() + "'");

            if (ds.Tables[0].Rows.Count > 0)
            {
                bool noedit =bool.Parse(ds.Tables[0].Rows[0]["no_Edit"].ToString());

                if(noedit == false)
                {
                    dml.Delete("Delete from SET_BankChequeDetail where Sno_Master = '" + ViewState["SNO"].ToString() + "'", "");
                    detailInsertEdit();
                }

                
            }
            else
            {
                detailInsertEdit();
            }

           
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
            string squer = "select * from SET_BankAccountDetail";


            if (ddlDel_MenuName.SelectedIndex != 0)
            {
                swhere = "BankAccountNumber = '" + ddlDel_MenuName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "BankAccountNumber is not null";
            }

            if (chkDel_Active.Checked == true)
            {
                swhere = swhere + " and Active = '1'";
            }
            else if (chkDel_Active.Checked == false)
            {
                swhere = swhere + " and Active = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0  ORDER BY BankAccountNumber";

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
            string squer = "select * from SET_BankAccountDetail";


            if (ddlDel_MenuName.SelectedIndex != 0)
            {
                swhere = "BankAccountNumber = '" + ddlDel_MenuName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "BankAccountNumber is not null";
            }

            if (chkDel_Active.Checked == true)
            {
                swhere = swhere + " and Active = '1'";
            }
            else if (chkDel_Active.Checked == false)
            {
                swhere = swhere + " and Active = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and  CompID='" + compid() + "'  ORDER BY BankAccountNumber";

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
            string squer = "select * from SET_BankAccountDetail";


            if (ddlDel_MenuName.SelectedIndex != 0)
            {
                swhere = "BankAccountNumber = '" + ddlDel_MenuName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "BankAccountNumber is not null";
            }

            if (chkDel_Active.Checked == true)
            {
                swhere = swhere + " and Active = '1'";
            }
            else if (chkDel_Active.Checked == false)
            {
                swhere = swhere + " and Active = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0  ORDER BY BankAccountNumber";

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
        DeleteBox.Visible = false;
        Editbox.Visible = false;
        Findbox.Visible = false;
        fieldbox.Visible = true;

        ddlBank.SelectedIndex = 0;
        txtBankAccountNumber.Text = "";
        txtEntryDate.Text = "";
        txtChequeNoFrom.Text = "";
        txtChequeNoTo.Text = "";
        ddlPeriod.SelectedIndex = 0;
        ddlBankBranch.SelectedIndex = 0;
        txtDateValidForm.Text = "";
        chkActive.Checked = false;

        txtCreatedby.Text = "";
        txtCreatedDate.Text = "";
        txtUpdateBy.Text = "";
        txtUpdateDate.Text = "";

        ddlBank.Enabled = false;
        txtBankAccountNumber.Enabled = false;
        txtEntryDate.Enabled = false;
        txtChequeNoFrom.Enabled = false;
        txtChequeNoTo.Enabled = false;
        ddlPeriod.Enabled = false;
        ddlBankBranch.Enabled = false;
        txtDateValidForm.Enabled = false;
        chkActive.Enabled = false;
        imgPopup.Enabled = false;
        ImageButton1.Enabled = false;

        txtCreatedby.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdateBy.Enabled = false;
        txtUpdateDate.Enabled = false;
        Div1.Visible = false;
        Div2.Visible = false;
        Div3.Visible = false;


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
            dml.Delete("update SET_BankAccountDetail set Record_Deleted = 1 where BankAcctDetailID = " + ViewState["SNO"].ToString() + "", "");
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



            DataSet ds = dml.Find("select * from SET_BankAccountDetail WHERE ([BankAcctDetailID]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlPeriod.ClearSelection();
                ddlBankBranch.ClearSelection();
                ddlBank.ClearSelection();
                ddlBank.Items.FindByValue(ds.Tables[0].Rows[0]["BankID"].ToString()).Selected = true;
                txtBankAccountNumber.Text = ds.Tables[0].Rows[0]["BankAccountNumber"].ToString();
                txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtChequeNoFrom.Text = ds.Tables[0].Rows[0]["ChqNoFrom"].ToString();
                txtChequeNoTo.Text = ds.Tables[0].Rows[0]["ChqNoTo"].ToString();
                ddlPeriod.Items.FindByValue(ds.Tables[0].Rows[0]["PeriodID"].ToString()).Selected = true;
                ddlBankBranch.Items.FindByValue(ds.Tables[0].Rows[0]["BankBranchID"].ToString()).Selected = true;
                txtDateValidForm.Text = ds.Tables[0].Rows[0]["DateValidForm"].ToString();


                bool chk = bool.Parse(ds.Tables[0].Rows[0]["Active"].ToString());


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
                dml.dateConvert(txtDateValidForm);

                updatecol.Visible = true;

                PopulateGridviewFind();
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



            DataSet ds = dml.Find("select * from SET_BankAccountDetail WHERE ([BankAcctDetailID]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlPeriod.ClearSelection();
                ddlBankBranch.ClearSelection();
                ddlBank.ClearSelection();
                ddlBank.Items.FindByValue(ds.Tables[0].Rows[0]["BankID"].ToString()).Selected = true;
                txtBankAccountNumber.Text = ds.Tables[0].Rows[0]["BankAccountNumber"].ToString();
                txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtChequeNoFrom.Text = ds.Tables[0].Rows[0]["ChqNoFrom"].ToString();
                txtChequeNoTo.Text = ds.Tables[0].Rows[0]["ChqNoTo"].ToString();
                ddlPeriod.Items.FindByValue(ds.Tables[0].Rows[0]["PeriodID"].ToString()).Selected = true;
                ddlBankBranch.Items.FindByValue(ds.Tables[0].Rows[0]["BankBranchID"].ToString()).Selected = true;
                txtDateValidForm.Text = ds.Tables[0].Rows[0]["DateValidForm"].ToString();


                bool chk = bool.Parse(ds.Tables[0].Rows[0]["Active"].ToString());


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
                dml.dateConvert(txtDateValidForm);
                updatecol.Visible = true;
                PopulateGridviewFind();

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

        txtBankAccountNumber.Enabled = true;
        txtEntryDate.Enabled = true;
        txtChequeNoFrom.Enabled = true;
        txtChequeNoTo.Enabled = true;
        ddlPeriod.Enabled = true;
        ddlBankBranch.Enabled = true;
        txtDateValidForm.Enabled = true;
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



            DataSet ds = dml.Find("select * from SET_BankAccountDetail WHERE ([BankAcctDetailID]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlPeriod.ClearSelection();
                ddlBankBranch.ClearSelection();
                ddlBank.ClearSelection();
                ddlBank.Items.FindByValue(ds.Tables[0].Rows[0]["BankID"].ToString()).Selected = true;

                txtBankAccountNumber.Text = ds.Tables[0].Rows[0]["BankAccountNumber"].ToString();
                txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtChequeNoFrom.Text = ds.Tables[0].Rows[0]["ChqNoFrom"].ToString();
                txtChequeNoTo.Text = ds.Tables[0].Rows[0]["ChqNoTo"].ToString();
                ddlPeriod.Items.FindByValue(ds.Tables[0].Rows[0]["PeriodID"].ToString()).Selected = true;
                ddlBankBranch.Items.FindByValue(ds.Tables[0].Rows[0]["BankBranchID"].ToString()).Selected = true;
                txtDateValidForm.Text = ds.Tables[0].Rows[0]["DateValidForm"].ToString();
                

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["Active"].ToString());


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
                dml.dateConvert(txtDateValidForm);
                updatecol.Visible = true;

                PopulateGridviewUpdate();
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
       string fy= Request.Cookies["FiscalYearId"].Value;
        return Convert.ToInt32(fy);
    }
    protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBank.SelectedIndex != 0)
        {
            ddlBankBranch.Enabled = true;
            dml.dropdownsql(ddlBankBranch, "SET_BankBranch", "BankBranchName", "BankBranchID", "BankID", ddlBank.SelectedItem.Value);
        }
        else
        {
            ddlBankBranch.SelectedIndex = 0;

            ddlBankBranch.Enabled = false;
        }

    }
    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //lblRowNumber
        if (e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
            float a = 10;//float.Parse(txtChequeNoFrom.Text);
            float b = 30;//float.Parse(txtChequeNoTo.Text);

            float c = b - a;
            TableCell cell = new TableCell();
            cell.Text = c.ToString();
            Label1.Text = c.ToString();
            for (int d = 1; d <= 20; d++)
            {
                e.Row.Cells.Add(cell);
            }

            //   (e.Row.FindControl("lblRowNumber") as Label).Text = (e.Row.RowIndex + 1).ToString();


        }
    }

    public void daad()
    {
        DataTable dt = new DataTable();

        if (dt.Columns.Count == 0)
        {
            dt.Columns.Add("Sno", typeof(string));
            dt.Columns.Add("Cheque No.", typeof(string));
            dt.Columns.Add("Voucher Date", typeof(string));
            dt.Columns.Add("Voucher Amount", typeof(string));
            dt.Columns.Add("Voucher No.", typeof(string));
            dt.Columns.Add("B.Statement Reference", typeof(string));
            dt.Columns.Add("B.Statement Date", typeof(string));
            dt.Columns.Add("B.Statement Amount", typeof(string));
            dt.Columns.Add("Difference", typeof(string));
        }

        string ch1 = txtChequeNoFrom.Text;
        string ch2 = txtChequeNoTo.Text;
        string ch3 = "";
        for (int b = 0; b < ch1.Length; b++)
        {
            if (ch1.Substring(b, 1) == "0")
            {
                ch3 += "0";
            }
            else {

                break;
            }

        }


        float p = float.Parse(ch2) - float.Parse(ch1);

        for (int a = 1; a <= p + 1; a++)
        {
            if (a == 1)
            {
                DataRow NewRow = dt.NewRow();
                NewRow[0] = a;
                NewRow[1] = ch3 + (float.Parse(ch1));
                dt.Rows.Add(NewRow);
            }
            else {
                DataRow NewRow = dt.NewRow();
                NewRow[0] = a;
                NewRow[1] = ch3 + (float.Parse(ch1) + (a - 1));
                dt.Rows.Add(NewRow);
            }

        }

        //GridView4.DataSource = dt;
        //GridView4.DataBind();

    }

    protected void txtChequeNoTo_TextChanged(object sender, EventArgs e)
    {
        if (txtChequeNoTo.Text != "" && txtChequeNoFrom.Text != "")
        {
            Div1.Visible = true;
            Div2.Visible = false;
            Div3.Visible = false;
            
            // daad();
            PopulateGridview();
        }
        else
        {
            Div1.Visible = false;
        }
    }

    public void PopulateGridview()
    {

        DataTable dt = new DataTable();

        if (dt.Columns.Count == 0)
        {
            dt.Columns.Add("Sno", typeof(string));
            dt.Columns.Add("ChequeNo", typeof(string));
            dt.Columns.Add("VoucherDate", typeof(string));
            dt.Columns.Add("VoucherAmount", typeof(string));
            dt.Columns.Add("VoucherNo", typeof(string));
            dt.Columns.Add("BStatementRef", typeof(string));
            dt.Columns.Add("BStatementDate", typeof(string));
            dt.Columns.Add("BStatementAmount", typeof(string));
            dt.Columns.Add("Difference", typeof(string));



            string ch1 = txtChequeNoFrom.Text;
            string ch2 = txtChequeNoTo.Text;
            string ch3 = "";
            for (int b = 0; b < ch1.Length; b++)
            {
                if (ch1.Substring(b, 1) == "0")
                {
                    ch3 += "0";
                }
                else {

                    break;
                }

            }


            double p = double.Parse(ch2) - double.Parse(ch1);

            for (int a = 1; a <= p + 1; a++)
            {
                if (a == 1)
                {
                    DataRow NewRow = dt.NewRow();
                    NewRow[0] = a;
                    NewRow[1] = ch3 + (double.Parse(ch1));
                    dt.Rows.Add(NewRow);
                }
                else {
                    DataRow NewRow = dt.NewRow();
                    NewRow[0] = a;
                    NewRow[1] = ch3 + (double.Parse(ch1) + (a - 1));
                    dt.Rows.Add(NewRow);
                }

            }

            ViewState["cheqTable"] = dt;


            GridView5.DataSource = dt;
            GridView5.DataBind();
        }


    }

    public void PopulateGridviewUpdate()
    {
        Div2.Visible = true;
        DataSet ds = dml.Find("select * from SET_BankChequeDetail where Sno_Master = '" + ViewState["SNO"].ToString() + "'");

        if (ds.Tables[0].Rows.Count > 0)
        {
            ViewState["cheqTableUP"] = ds.Tables[0];

            bool noedit = bool.Parse(ds.Tables[0].Rows[0]["no_Edit"].ToString());

            if(noedit == true)
            {
                txtChequeNoFrom.Enabled = false;
                txtChequeNoTo.Enabled = false;
            }
            else
            {
                txtChequeNoFrom.Enabled = true;
                txtChequeNoTo.Enabled = true;
            }
           
        }
        GridView4.DataSource = ds.Tables[0];
        GridView4.DataBind();

    }

    public void PopulateGridviewFind()
    {
        Div3.Visible = true;
        DataSet ds = dml.Find("select * from SET_BankChequeDetail where Sno_Master = '"+ViewState["SNO"].ToString()+"'");

        if (ds.Tables[0].Rows.Count > 0)
        {
           // ViewState["cheqTableUP"] = ds.Tables[0];
            GridView6.DataSource = ds.Tables[0];
            GridView6.DataBind();
        }



    }

    protected void GridView5_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView5.EditIndex = e.NewEditIndex;
        GridView5.DataSource = (DataTable)ViewState["cheqTable"];
        GridView5.DataBind();
    }

    protected void GridView5_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView5.EditIndex = -1;

        GridView5.DataSource = (DataTable)ViewState["cheqTable"];
        GridView5.DataBind();


    }

    protected void GridView5_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            string lblChq = (GridView5.Rows[e.RowIndex].FindControl("lblChq") as Label).Text;
            string txtVoucherDate = (GridView5.Rows[e.RowIndex].FindControl("txtVoucherDate") as TextBox).Text;
            string txtVoucherAmount = (GridView5.Rows[e.RowIndex].FindControl("txtVoucherAmount") as TextBox).Text;
            string txtVoucherNo = (GridView5.Rows[e.RowIndex].FindControl("txtVoucherNo") as TextBox).Text;
            string txtBStatementRef = (GridView5.Rows[e.RowIndex].FindControl("txtBStatementRef") as TextBox).Text;
            string txtBStatementDate = (GridView5.Rows[e.RowIndex].FindControl("txtBStatementDate") as TextBox).Text;
            string txtBStatementAmount = (GridView5.Rows[e.RowIndex].FindControl("txtBStatementAmount") as TextBox).Text;
            string txtDifference = (GridView5.Rows[e.RowIndex].FindControl("txtDifference") as TextBox).Text;

            GridViewRow row = GridView5.Rows[e.RowIndex];
            DataTable dt = (DataTable)ViewState["cheqTable"];


            dt.Rows[row.DataItemIndex]["ChequeNo"] = lblChq;
            dt.Rows[row.DataItemIndex]["VoucherDate"] = txtVoucherDate;
            dt.Rows[row.DataItemIndex]["VoucherAmount"] = txtVoucherAmount;
            dt.Rows[row.DataItemIndex]["VoucherNo"] = txtVoucherNo;
            dt.Rows[row.DataItemIndex]["BStatementRef"] = txtBStatementRef;
            dt.Rows[row.DataItemIndex]["BStatementDate"] = txtBStatementDate;
            dt.Rows[row.DataItemIndex]["BStatementAmount"] = txtBStatementAmount;
            dt.Rows[row.DataItemIndex]["Difference"] = txtDifference;

            GridView5.EditIndex = -1;
            GridView5.DataSource = (DataTable)ViewState["cheqTable"];
            GridView5.DataBind();


        }
        catch (Exception ex)
        {

        }
    }

    public void detailInsert()
    {
        string onoff = "";
        for (int a = 0; a < GridView5.Rows.Count; a++)
        {
            Label lblChqNo = (Label)GridView5.Rows[a].FindControl("lblChqNo");
            Label lblVoucherDate = (Label)GridView5.Rows[a].FindControl("lblVoucherDate");
            Label lblVoucherAmount = (Label)GridView5.Rows[a].FindControl("lblVoucherAmount");
            Label lblVoucherNo = (Label)GridView5.Rows[a].FindControl("lblVoucherNo");
            Label lblBStatementRef = (Label)GridView5.Rows[a].FindControl("lblBStatementRef");
            Label lblBStatementDate = (Label)GridView5.Rows[a].FindControl("lblBStatementDate");
            Label lblBStatementAmount = (Label)GridView5.Rows[a].FindControl("lblBStatementAmount");
            Label lblDifference = (Label)GridView5.Rows[a].FindControl("lblDifference");

            string ss = lblVoucherDate.Text;

            if (lblVoucherDate.Text == "")
            {
                lblVoucherDate.Text = "NULL";
            }
            else
            {
                lblVoucherDate.Text = "'" + dml.dateconvertforinsert(lblVoucherDate) + "'";
            }

            if (lblVoucherAmount.Text == "")
            {
                lblVoucherAmount.Text = "NULL";
            }
            else
            {
                lblVoucherAmount.Text = "'" + lblVoucherAmount.Text + "'";
            }

            if (lblVoucherNo.Text == "")
            {

                lblVoucherNo.Text = "NULL";
            }
            else
            {
                onoff = "on";
                lblVoucherNo.Text = "'" + lblVoucherNo.Text + "'";
            }

            if (lblBStatementRef.Text == "")
            {
                lblBStatementRef.Text = "NULL";
            }
            else
            {
                lblBStatementRef.Text = "'" + lblBStatementRef.Text + "'";
            }

            if (lblBStatementDate.Text == "")
            {
                lblBStatementDate.Text = "NULL";
            }
            else
            {
                lblBStatementDate.Text = "'" + dml.dateconvertforinsert(lblBStatementDate) + "'";
            }

            if (lblBStatementAmount.Text == "")
            {
                lblBStatementAmount.Text = "NULL";
            }
            else
            {
                lblBStatementAmount.Text = "'" + lblBStatementAmount.Text + "'";
            }

            if (lblDifference.Text == "")
            {
                lblDifference.Text = "NULL";
            }
            else
            {
                lblDifference.Text = "'" + lblDifference.Text + "'";
            }
            dml.Insert("INSERT INTO SET_BankChequeDetail ([Sno_Master], [ChequeNo], [VoucherDate], [VoucherAmount], [VoucherNo], [BStatementRef], [BStatementDate], [BStatementAmount], [Difference], [GocId], [CompID], [BranchID], [FiscalYearID], [IsActive], [CreatedBy], [CreatedDate], [Record_Deleted],[no_Edit]) VALUES "
                               + "('" + ViewState["masterID"].ToString() + "', '" + lblChqNo.Text + "', " + lblVoucherDate.Text + ", " + lblVoucherAmount.Text + ", " + lblVoucherNo.Text + ", " + lblBStatementRef.Text + ", " + lblBStatementDate.Text + ", " + lblBStatementAmount.Text + ", " + lblDifference.Text + "," + gocid() + "," + compid() + "," + branchId() + "," + FiscalYear() + ", '1', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', 0,0)", "");

           
        }
        if (onoff == "on")
        {
            dml.Update("UPDATE SET_BankChequeDetail SET [no_Edit]=1 where Sno_Master='" + ViewState["masterID"].ToString() + "' ", "");
        }
        else
        {

        }
       
    }
    public void detailInsertEdit()
    {
        for (int a = 0; a < GridView5.Rows.Count; a++)
        {
            Label lblChqNo = (Label)GridView5.Rows[a].FindControl("lblChqNo");
            Label lblVoucherDate = (Label)GridView5.Rows[a].FindControl("lblVoucherDate");
            Label lblVoucherAmount = (Label)GridView5.Rows[a].FindControl("lblVoucherAmount");
            Label lblVoucherNo = (Label)GridView5.Rows[a].FindControl("lblVoucherNo");
            Label lblBStatementRef = (Label)GridView5.Rows[a].FindControl("lblBStatementRef");
            Label lblBStatementDate = (Label)GridView5.Rows[a].FindControl("lblBStatementDate");
            Label lblBStatementAmount = (Label)GridView5.Rows[a].FindControl("lblBStatementAmount");
            Label lblDifference = (Label)GridView5.Rows[a].FindControl("lblDifference");

            string ss = lblVoucherDate.Text;

            if (lblVoucherDate.Text == "")
            {
                lblVoucherDate.Text = "NULL";
            }
            else
            {
                lblVoucherDate.Text = "'" + dml.dateconvertforinsert(lblVoucherDate) + "'";
            }

            if (lblVoucherAmount.Text == "")
            {
                lblVoucherAmount.Text = "NULL";
            }
            else
            {
                lblVoucherAmount.Text = "'" + lblVoucherAmount.Text + "'";
            }

            if (lblVoucherNo.Text == "")
            {
                lblVoucherNo.Text = "NULL";
            }
            else
            {
                lblVoucherNo.Text = "'" + lblVoucherNo.Text + "'";
            }

            if (lblBStatementRef.Text == "")
            {
                lblBStatementRef.Text = "NULL";
            }
            else
            {
                lblBStatementRef.Text = "'" + lblBStatementRef.Text + "'";
            }

            if (lblBStatementDate.Text == "")
            {
                lblBStatementDate.Text = "NULL";
            }
            else
            {
                lblBStatementDate.Text = "'" + dml.dateconvertforinsert(lblBStatementDate) + "'";
            }

            if (lblBStatementAmount.Text == "")
            {
                lblBStatementAmount.Text = "NULL";
            }
            else
            {
                lblBStatementAmount.Text = "'" + lblBStatementAmount.Text + "'";
            }

            if (lblDifference.Text == "")
            {
                lblDifference.Text = "NULL";
            }
            else
            {
                lblDifference.Text = "'" + lblDifference.Text + "'";
            }


            dml.Insert("INSERT INTO SET_BankChequeDetail ([Sno_Master], [ChequeNo], [VoucherDate], [VoucherAmount], [VoucherNo], [BStatementRef], [BStatementDate], [BStatementAmount], [Difference], [GocId], [CompID], [BranchID], [FiscalYearID], [IsActive], [CreatedBy], [CreatedDate], [Record_Deleted],[no_Edit]) VALUES "
                            + "('" + ViewState["SNO"].ToString() + "', '" + lblChqNo.Text + "', " + lblVoucherDate.Text + ", " + lblVoucherAmount.Text + ", " + lblVoucherNo.Text + ", " + lblBStatementRef.Text + ", " + lblBStatementDate.Text + ", " + lblBStatementAmount.Text + ", " + lblDifference.Text + "," + gocid() + "," + compid() + "," + branchId() + "," + FiscalYear() + ", '1', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', 0 , 0)", "");
        }
    }


    protected void GridView4_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView4.EditIndex = e.NewEditIndex;
        GridView4.DataSource = (DataTable)ViewState["cheqTableUP"];
        GridView4.DataBind();
    }

    protected void GridView4_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView4.EditIndex = -1;
        GridView4.DataSource = (DataTable)ViewState["cheqTableUP"];
        GridView4.DataBind();


    }
    protected void GridView4_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
       
        Label Sno = (Label)GridView4.Rows[e.RowIndex].FindControl("lblSno");
        Label lblChqNo = (Label)GridView4.Rows[e.RowIndex].FindControl("lblChqNo");
        TextBox lblVoucherDate = (TextBox)GridView4.Rows[e.RowIndex].FindControl("txtVoucherDate");
        TextBox lblVoucherAmount = (TextBox)GridView4.Rows[e.RowIndex].FindControl("txtVoucherAmount");
        TextBox lblVoucherNo = (TextBox)GridView4.Rows[e.RowIndex].FindControl("txtVoucherNo");
        TextBox lblBStatementRef = (TextBox)GridView4.Rows[e.RowIndex].FindControl("txtBStatementRef");
        TextBox lblBStatementDate = (TextBox)GridView4.Rows[e.RowIndex].FindControl("txtBStatementDate");
        TextBox lblBStatementAmount = (TextBox)GridView4.Rows[e.RowIndex].FindControl("txtBStatementAmount");
        TextBox lblDifference = (TextBox)GridView4.Rows[e.RowIndex].FindControl("txtDifference");

       

        if (lblVoucherDate.Text == "")
        {
            lblVoucherDate.Text = "NULL";
        }
        else
        {
            lblVoucherDate.Text = "'" + dml.dateconvertforinsert(lblVoucherDate) + "'";
        }

        if (lblVoucherAmount.Text == "")
        {
            lblVoucherAmount.Text = "NULL";
        }
        else
        {
            lblVoucherAmount.Text = "'" + lblVoucherAmount.Text + "'";
        }

        if (lblVoucherNo.Text == "")
        {
            lblVoucherNo.Text = "NULL";
        }
        else
        {
            lblVoucherNo.Text = "'" + lblVoucherNo.Text + "'";
        }

        if (lblBStatementRef.Text == "")
        {
            lblBStatementRef.Text = "NULL";
        }
        else
        {
            lblBStatementRef.Text = "'" + lblBStatementRef.Text + "'";
        }

        if (lblBStatementDate.Text == "")
        {
            lblBStatementDate.Text = "NULL";
        }
        else
        {
            lblBStatementDate.Text = "'" + dml.dateconvertforinsert(lblBStatementDate) + "'";
        }

        if (lblBStatementAmount.Text == "")
        {
            lblBStatementAmount.Text = "NULL";
        }
        else
        {
            lblBStatementAmount.Text = "'" + lblBStatementAmount.Text + "'";
        }

        if (lblDifference.Text == "")
        {
            lblDifference.Text = "NULL";
        }
        else
        {
            lblDifference.Text = "'" + lblDifference.Text + "'";
        }


        dml.Update("UPDATE SET_BankChequeDetail SET [VoucherDate]="+lblVoucherDate.Text+", [VoucherAmount]="+lblVoucherAmount.Text+", [VoucherNo]="+lblVoucherNo.Text+", [BStatementRef]="+lblBStatementRef.Text+", [BStatementDate]="+lblBStatementDate.Text+", [BStatementAmount]="+lblBStatementAmount.Text+", [Difference]="+lblDifference.Text+", [UpdatedBy]='"+show_username()+"', [UpdatedDate]='"+DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")+"' WHERE ([Sno]='"+Sno.Text+"')", "");

        dml.Update("UPDATE SET_BankChequeDetail SET [no_Edit]=1 where Sno_Master='"+ViewState["SNO"].ToString()+"' ", "");


        GridView4.EditIndex = -1;
        PopulateGridviewUpdate();
    }
}