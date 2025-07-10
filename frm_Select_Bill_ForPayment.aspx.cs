
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
    int DateFrom, AddDays, DeleteDays, EditDays;
    string userid, UserGrpID, FormID, fiscal, bpname;
    DmlOperation dml = new DmlOperation();
    radcomboxclass cmb = new radcomboxclass();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
    FieldHide fd = new FieldHide();
    inventoryCal inv = new inventoryCal();

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
            dml.dropdownsqlwithquery(ddlsupplier, "select  BPartnerId,BPartnerName from SET_BusinessPartner  where BPartnerId in (select DISTINCT BPartnerID from Fin_PurchaseMaster where sno in (SELECT Sno_Master from Fin_PurchaseDetail where BalanceAmount > 0))", "BPartnerName", "BPartnerId");
            if (rdbBillPay.Checked == true)
            {
                dml.dropdownsqlwithquery(ddlsupplier, "select  BPartnerId,BPartnerName from SET_BusinessPartner  where BPartnerId in (select DISTINCT BPartnerID from Fin_PurchaseMaster where sno in (SELECT Sno_Master from Fin_PurchaseDetail where BalanceAmount > 0))","BPartnerName","BPartnerId");
            }
            if(rdbGstClaimable.Checked == true)
            {
               dml.dropdownsqlwithquery(ddlsupplier, "select  BPartnerId,BPartnerName from SET_BusinessPartner where GstServiceProvider = 1", "BPartnerName", "BPartnerId");
            }
            dml.dropdownsql(ddlEdit_Supplier, "ViewSupplierId", "BPartnerName", "BPartnerId");
            dml.dropdownsql(ddlDel_Supplier, "ViewSupplierId", "BPartnerName", "BPartnerId");
            dml.dropdownsql(ddlFind_Supplier, "ViewSupplierId", "BPartnerName", "BPartnerId");
            textClear();
            //datashow();
            btnInsert_Click(sender, e);
            bpname = Request.QueryString["bpname"];

            ddlsupplier.ClearSelection();
            if (ddlsupplier.Items.FindByValue(bpname) != null)
            {
                ddlsupplier.Items.FindByValue(bpname).Selected = true;
                btnShow_Click(sender, e);
            }
            else
            {
                Label1.Text = "No data found";
            }

            Findbox.Visible = false;
            DeleteBox.Visible = false;
            Editbox.Visible = false;
        }
    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        textClear();
        ddlsupplier.Enabled = true;
        btnShow.Enabled = true;
        btnSave.Visible = true;
        btnEdit.Visible = false;
        btnFind.Visible = false;
        btnCancel.Visible = true;
        btnDelete.Visible = false;
        btnInsert.Visible = false;
        btnUpdate.Visible = false;
        rdbBillPay.Checked = true;

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int goc_id = gocid();
        int _compid = compid();
        int Branch_id = branchId();
        int FiscalYear_id = FiscalYear();

        
        foreach (GridViewRow row in GridView6.Rows)
        {
            Label lblBillNo = (Label)row.FindControl("lblBillNo");
            Label lblBillDate = (Label)row.FindControl("lblBillDate");
            Label lblGLDate = (Label)row.FindControl("lblGLDate");
            Label lblSupplier = (Label)row.FindControl("lblSupplier");
            Label lblGSTClaimable = (Label)row.FindControl("lblGSTClaimable");
            Label lblBillPayable = (Label)row.FindControl("lblBillPayable");
            Label lblApprovedAmount = (Label)row.FindControl("lblApprovedAmount");
            Label lblGSTBalance = (Label)row.FindControl("lblGSTBalance");
            Label lblBillBalanceBalance = (Label)row.FindControl("lblBillBalanceBalance");
            Label lblBillBalanceBalanceHide = (Label)row.FindControl("lblBillBalanceBalanceHide");
            //
            Label lbMSno = (Label)row.FindControl("lbMSno");
            Label lbDSno = (Label)row.FindControl("lbDSno");
            Label lblCrAcct = (Label)row.FindControl("lblCrAcct");
            Label lbDrAcct = (Label)row.FindControl("lbDrAcct");




            //string a = lblBillBalanceBalance.Text;
            //string b = string.Empty;
            //float val = 0;

            //for (int i = 0; i < a.Length; i++)
            //{
            //    if (Char.IsDigit(a[i]))
            //        b += a[i];
            //}

            //if (b.Length > 0)
            //    val = float.Parse(b);



            string Sno_Master_Pur = "0", Sno_Detail_Pur = "0", Bill_VoucherNo="0", CrAccountCodeToDr ="0", Pay_Adj_Detail_Sno="0", StaxInvNo="0", TaxCodeDrToCr = "0", Pay_Adj_VoucherNo="0";
            Sno_Master_Pur = lbMSno.Text;
            Sno_Detail_Pur = lbDSno.Text;
            CrAccountCodeToDr = lbDrAcct.Text;
           

            dml.Insert("INSERT INTO [Fin_Bills4Payment] ([Sno_Master_Pur], [Sno_Detail_Pur], [EntryDate], [Bill_VoucherNo], [BPartnerID], [BillNo],"
                                    +" [BillDate], [CrAccountCodeToDr], [Pay_Adj_Detail_Sno], [StaxInvNo], [TaxCodeDrToCr],  [BalanceAmount], [Pay_Adj_VoucherNo], "
                                    +"[GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted],"
                                    + " [BalanceTax],[ApprovedAmount]) VALUES "
                                     + "('" + Sno_Master_Pur + "', '"+ Sno_Detail_Pur + "', '"+DateTime.Now.ToString("dd-MMM-yyyy")+"', '"+ Bill_VoucherNo + "', '"+ddlsupplier.SelectedItem.Value+"', '"+lblBillNo.Text+"',"
                                     +" '"+lblBillDate.Text+"', '"+ CrAccountCodeToDr + "', '"+ Pay_Adj_Detail_Sno  + "', '"+ StaxInvNo + "', '"+TaxCodeDrToCr+"', '"+lblBillPayable.Text+"', '"+ Pay_Adj_VoucherNo + "',"
                                     +" "+goc_id+", "+ _compid + ", "+Branch_id+", "+ FiscalYear_id + ", '"+show_username()+"', '"+DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")+"',  0, '"+ lblBillBalanceBalanceHide.Text + "','"+ lblApprovedAmount.Text+ "');", "");


            dml.Update("UPDATE Fin_PurchaseDetail set BalanceAmount = '"+ lblBillBalanceBalanceHide.Text + "' where Sno = '"+ lbDSno.Text + "'", "");
            Response.Write("<script>window.close();</script>");
            btnInsert_Click(sender, e);

           

        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {

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
            string squer = "select * from View_purchaseMater_Detail";


            if (ddlDel_Supplier.SelectedIndex != 0)
            {
                swhere = "BPartnerID = '" + ddlDel_Supplier.SelectedItem.Value + "'";
            }
            else
            {
                swhere = "BPartnerID is not null";
            }

            if (chkDel_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkDel_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and CompId = '" + compid() + "' ORDER BY BillNo";

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
            string squer = "select * from View_purchaseMater_Detail";


            if (ddlFind_Supplier.SelectedIndex != 0)
            {
                swhere = "BPartnerID = '" + ddlFind_Supplier.SelectedItem.Value + "'";
            }
            else
            {
                swhere = "BPartnerID is not null";
            }

            if (chkFind_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkFind_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and CompId = '" + compid() + "' ORDER BY BillNo";

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
            string squer = "select * from View_purchaseMater_Detail";


            if (ddlEdit_Supplier.SelectedIndex != 0)
            {
                swhere = "BPartnerID = '" + ddlEdit_Supplier.SelectedItem.Value + "'";
            }
            else
            {
                swhere = "BPartnerID is not null";
            }

            if (chkEdit_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkEdit_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and CompId = '" + compid() + "'  ORDER BY BillNo";

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
        ddlsupplier.Enabled = false;
        btnShow.Enabled = false;
        ddlsupplier.SelectedIndex = 0;

        DeleteBox.Visible = false;
        Editbox.Visible = false;
        Findbox.Visible = false;
        fieldbox.Visible = true;



        Label1.Text = "";




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
            DataSet ds = dml.Find("select * from SET_BankBranch where Bankid = '" + ViewState["SNO"].ToString() + "'  and Record_Deleted = '0' ");
            if (ds.Tables[0].Rows.Count > 0)
            {
                string count = ds.Tables[0].Rows.Count.ToString();
                Label1.Text = "This bank has " + count + " Branch. First delete Branch";
            }
            else {


                dml.Delete("update SET_Bank set Record_Deleted = 1 where BankID = " + ViewState["SNO"].ToString() + "", "");
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


        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {
            string serial_id;
            serial_id = GridView1.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;



            DataSet ds = dml.Find("select * from SET_Bank WHERE ([BankID]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {




                bool chks = bool.Parse(ds.Tables[0].Rows[0]["Status"].ToString());

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());





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
            string serial_id;
            serial_id = GridView2.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;

            hide_edit_del("SELECT BankID from SET_BankBranch where BankID = '" + serial_id + "'");

            DataSet ds = dml.Find("select * from SET_Bank WHERE ([BankID]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {




                bool chks = bool.Parse(ds.Tables[0].Rows[0]["Status"].ToString());

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());







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
            string serial_id;
            serial_id = GridView3.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;

            hide_edit_del("SELECT BankID from SET_BankBranch where BankID = '" + serial_id + "'");

            DataSet ds = dml.Find("select * from SET_Bank WHERE ([BankID]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {




                bool chks = bool.Parse(ds.Tables[0].Rows[0]["Status"].ToString());

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());




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
        DataSet ds_autoUserName = dml.Find("select user_name from SET_User_Manager where UserId='" + userid + "'");
        return ds_autoUserName.Tables[0].Rows[0]["user_name"].ToString();
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
    public void hide_edit_del(string query)
    {
        DataSet ds = dml.Find(query);
        if (ds.Tables[0].Rows.Count > 0)
        {
            btnUpdate.Visible = false;
            btnDelete_after.Visible = false;
        }
    }
    public void datashow()
    {

        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[10]
        {
                new DataColumn("BillNo"),
                new DataColumn("BillDate"),
                new DataColumn("GLDate"),
                new DataColumn("Supplier"),
                new DataColumn("BillPayable"),
                new DataColumn("GSTClaimable"),
                new DataColumn("CheckBOX"),
                new DataColumn("ApprovedAmount"),
                new DataColumn("GSTBalance"),
                new DataColumn("BillBalance")
        });


        ViewState["billgrid"] = dt;


        this.PopulateGridview();


    }

    public void PopulateGridview()
    {

        DataTable dtbl = (DataTable)ViewState["billgrid"];

        if (dtbl.Rows.Count > 0)
        {

            GridView6.DataSource = (DataTable)ViewState["billgrid"];
            GridView6.DataBind();

        }
        else
        {


            dtbl.Rows.Add(dtbl.NewRow());
            GridView6.DataSource = dtbl;
            GridView6.DataBind();

            GridView6.Rows[0].Cells.Clear();
            GridView6.Rows[0].Cells.Add(new TableCell());
            GridView6.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
            GridView6.Rows[0].Cells[0].Text = "No Data Found ..!";
            GridView6.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;

        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string id = e.Row.Cells[1].Text;
        bool flag = dml.isNumber(id);


        if (flag == true)
        {

            DataSet ds = dml.Find("select BankID,MLD from SET_Bank where BankId = '" + id + "'");
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

            DataSet ds = dml.Find("select BankID,MLD from SET_Bank where BankId = '" + id + "'");
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


    protected void btnShow_Click(object sender, EventArgs e)
    {
        GridView6.DataSource = null;
        GridView6.DataBind();
        string total_bal30 = "0", total_bal60 = "0", total_bal31 = "0", total_bal91 = "0";
        string totalgst30 = "0", totalgst60 = "0", totalgst31 = "0", totalgst91 = "0";
        if (ddlsupplier.SelectedIndex != 0)
        {
            //DataSet ds = dml.Find("select count(Sno) as noofinv , sum(BillBalance) as  totalbal from Fin_PurchaseMaster where sno in (SELECT Sno_Master from Fin_PurchaseDetail where BalanceAmount > 0) and BPartnerID = '" + ddlsupplier.SelectedItem.Value + "' and (EntryDate <= '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' and EntryDate >= dateadd(day, -30, getdate()))");
            DataSet ds = dml.Find("select count(Sno) as noofinv , sum(BillBalance) as  totalbal from Fin_PurchaseMaster where sno in (SELECT Sno_Master from Fin_PurchaseDetail where BalanceAmount > 0) and BPartnerID = '" + ddlsupplier.SelectedItem.Value + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lbl30_InvNO.Text = ds.Tables[0].Rows[0]["noofinv"].ToString();
                if ( ds.Tables[0].Rows[0]["totalbal"].ToString() != "")
                {
                    total_bal30 = ds.Tables[0].Rows[0]["totalbal"].ToString();
                }
                else
                {
                    total_bal30 = "0";
                }

                decimal parsed = decimal.Parse(total_bal30, CultureInfo.InvariantCulture);
                CultureInfo urdu = new CultureInfo("ur-PK");
                string text = string.Format(urdu, "{0:c}", parsed);
                lbl30_billPay.Text = text;
                lbl30_billPayHide.Text = total_bal30;

            }
            else
            {
                lbl30_InvNO.Text = "";
                lbl30_billPay.Text = "";
                lbl30_billPayHide.Text = "";
            }
            DataSet ds30gst = dml.Find("select SUM(BalanceTax) as totalgst  from Fin_PurchaseDetail where Sno_Master in (SELECT Sno from Fin_PurchaseMaster where BPartnerID = '" + ddlsupplier.SelectedItem.Value + "' and (EntryDate <= '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' and EntryDate >= dateadd(day, -30, getdate()))) and BalanceAmount > 0;");
            if (ds30gst.Tables[0].Rows.Count > 0)
            {
               
                if (ds30gst.Tables[0].Rows[0]["totalgst"].ToString() != "")
                {
                    totalgst30 = ds30gst.Tables[0].Rows[0]["totalgst"].ToString();
                }
                else
                {
                    totalgst30 = "0";
                }


                decimal parsed = decimal.Parse(totalgst30, CultureInfo.InvariantCulture);
                CultureInfo urdu = new CultureInfo("ur-PK");
                string text = string.Format(urdu, "{0:c}", parsed);
                lbl30_GstPay.Text = text;
            }
            else
            {
                lbl30_GstPay.Text = "";
            }

            //Upto 31 to 61 days
            DataSet ds31billpay = dml.Find("select Count(Sno) as inTotal, sum(BillBalance) as  totalbal  from Fin_PurchaseMaster where sno in (SELECT Sno_Master from Fin_PurchaseDetail where BalanceAmount > 0) and BPartnerID = '"+ddlsupplier.SelectedItem.Value+"' and (EntryDate BETWEEN dateadd(day, -60, getdate())  and dateadd(day, -31, getdate()));");
            if (ds31billpay.Tables[0].Rows.Count > 0)
            {
                lbl31_InvNo.Text = ds31billpay.Tables[0].Rows[0]["inTotal"].ToString();
                total_bal31 = ds31billpay.Tables[0].Rows[0]["totalbal"].ToString();
                if (total_bal31 != "")
                {
                    decimal parsed = decimal.Parse(total_bal31, CultureInfo.InvariantCulture);
                    CultureInfo urdu = new CultureInfo("ur-PK");
                    string text = string.Format(urdu, "{0:c}", parsed);
                    lbl31_BillPay.Text = text;
                }
                else
                {
                    lbl31_InvNo.Text = "0";
                    lbl31_BillPay.Text = "0";

                }

            }
            else
            {
                lbl31_InvNo.Text = "0";
                lbl31_BillPay.Text = "0";
            }

            DataSet ds31gst = dml.Find("select SUM(BalanceTax) as totalgst  from Fin_PurchaseDetail where Sno_Master in (SELECT Sno from Fin_PurchaseMaster where BPartnerID = '"+ddlsupplier.SelectedItem.Value+"' and (EntryDate BETWEEN dateadd(day, -60, getdate())  and dateadd(day, -31, getdate()))) and BalanceAmount > 0;");
            if (ds31gst.Tables[0].Rows.Count > 0)
            {
                totalgst31 = ds31gst.Tables[0].Rows[0]["totalgst"].ToString();
                if (totalgst31 != "")
                {
                    decimal parsed = decimal.Parse(totalgst31, CultureInfo.InvariantCulture);
                    CultureInfo urdu = new CultureInfo("ur-PK");
                    string text = string.Format(urdu, "{0:c}", parsed);
                    lbl31_GstPay.Text = text;
                }
                else
                {

                    lbl31_GstPay.Text = "0";
                }
            }
            else
            {
                lbl31_GstPay.Text = "0";
            }




            //Upto 60 to 90 days
            DataSet ds60_Inv = dml.Find("select Count(Sno) as inTotal, sum(BillBalance) as  totalbal  from Fin_PurchaseMaster where sno in (SELECT Sno_Master from Fin_PurchaseDetail where BalanceAmount > 0) and BPartnerID = '"+ddlsupplier.SelectedItem.Value+"' and (EntryDate BETWEEN dateadd(day, -60, getdate())  and dateadd(day, -30, getdate()));");
            if (ds60_Inv.Tables[0].Rows.Count > 0)
            {
                lbl61_InvNO.Text = ds60_Inv.Tables[0].Rows[0]["inTotal"].ToString();
                total_bal60 = ds60_Inv.Tables[0].Rows[0]["totalbal"].ToString();

                if (total_bal60 != "")
                {
                    decimal parsed = decimal.Parse(total_bal60, CultureInfo.InvariantCulture);
                    CultureInfo urdu = new CultureInfo("ur-PK");
                    string text = string.Format(urdu, "{0:c}", parsed);
                    lbl61_billPay.Text = text;
                }
                else
                {
                    lbl61_InvNO.Text = "0";
                    lbl61_billPay.Text = "0";
                }

            }
            else
            {
                lbl61_InvNO.Text = "0";
                lbl61_billPay.Text = "0";
            }


            DataSet ds60gst = dml.Find("select SUM(BalanceTax) as totalgst from Fin_PurchaseDetail where Sno_Master in (SELECT Sno from Fin_PurchaseMaster where BPartnerID = '" + ddlsupplier.SelectedItem.Value + "' and (EntryDate BETWEEN dateadd(day, -60, getdate())  and dateadd(day, -30, getdate()))) and BalanceAmount > 0;");
            if (ds30gst.Tables[0].Rows.Count > 0)
            {
                totalgst60 = ds60gst.Tables[0].Rows[0]["totalgst"].ToString();
                if (totalgst60 != "")
                {
                    decimal parsed = decimal.Parse(totalgst60, CultureInfo.InvariantCulture);
                    CultureInfo urdu = new CultureInfo("ur-PK");
                    string text = string.Format(urdu, "{0:c}", parsed);
                    lbl61_gstpay.Text = text;
                }
                else
                {
                    lbl61_gstpay.Text = "0";
                    totalgst60 = "0";
                }
            }
            else
            {
                lbl61_gstpay.Text = "0";
            }

            //Above 90 days
           
            DataSet ds91billpay = dml.Find("select count(Sno) as noofinv , sum(BillBalance) as  totalbal from Fin_PurchaseMaster where sno in (SELECT Sno_Master from Fin_PurchaseDetail where BalanceAmount > 0) and BPartnerID = '"+ddlsupplier.SelectedItem.Value+"' and (EntryDate <= '"+DateTime.Now.ToString("dd-MMM-yyyy")+"' and EntryDate <= dateadd(day, -91, getdate()))");
            if (ds91billpay.Tables[0].Rows.Count > 0)
            {
                lbl90_InvNO.Text = ds91billpay.Tables[0].Rows[0]["noofinv"].ToString();
                total_bal91 = ds91billpay.Tables[0].Rows[0]["totalbal"].ToString();
                if (total_bal31 != "")
                {
                    decimal parsed = decimal.Parse(total_bal91, CultureInfo.InvariantCulture);
                    CultureInfo urdu = new CultureInfo("ur-PK");
                    string text = string.Format(urdu, "{0:c}", parsed);
                    lbl90_billPay.Text = text;
                }
                else
                {
                    lbl90_InvNO.Text = "0";
                    lbl90_billPay.Text = "0";

                }

            }
            else
            {
                lbl90_InvNO.Text = "0";
                lbl90_billPay.Text = "0";
            }

            DataSet ds91gst = dml.Find("select SUM(BalanceTax) as totalgst  from Fin_PurchaseDetail where Sno_Master in (SELECT Sno from Fin_PurchaseMaster where BPartnerID = '" + ddlsupplier.SelectedItem.Value + "' and (EntryDate <= dateadd(day, -91, getdate()))) and BalanceAmount > 0;");
            if (ds91gst.Tables[0].Rows.Count > 0)
            {
                totalgst91 = ds91gst.Tables[0].Rows[0]["totalgst"].ToString();
                if (totalgst91 != "")
                {
                    decimal parsed = decimal.Parse(totalgst91, CultureInfo.InvariantCulture);
                    CultureInfo urdu = new CultureInfo("ur-PK");
                    string text = string.Format(urdu, "{0:c}", parsed);
                    lbl90_gstpay.Text = text;
                }
                else
                {

                    lbl90_gstpay.Text = "0";
                }
            }
            else
            {
                lbl90_gstpay.Text = "0";
            }



            //dataView show 



            DataSet dsgrid = dml.grid("SELECT CrAccountCode,DrAccountCode,BillNo,BillDate,GLDATE,BPartnerID,(TaxAmount + AddTaxAmount) as gstclaim,BalanceAmount, BalanceTax,BillBalance,BPartnerName, PurDetail,Sno FROM View_purchaseMater_Detail where sno in (SELECT Sno_Master from Fin_PurchaseDetail  where BalanceAmount > 0) and BPartnerID = '" + ddlsupplier.SelectedItem.Value + "' and (EntryDate <= '" + DateTime.Now.ToString() + "' and EntryDate >= dateadd(day, -30, getdate()))");
            if (dsgrid.Tables[0].Rows.Count > 0)
            {
                GridView6.DataSource = dsgrid.Tables[0];
                GridView6.DataBind();
            }

            //DataSet ds = dml.Find("select * from Fin_PurchaseMaster where sno in (SELECT Sno_Master from Fin_PurchaseDetail where BalanceAmount > 0) and BPartnerID = '" + ddlsupplier.SelectedItem.Value + "';");

        }
        else
        {
            Label1.Text = "Please Select supplier";
        }
        if (total_bal30 == "")
        {
            total_bal30 = "0";
        }
        if (total_bal31 == "")
        {
            total_bal31 = "0";
        }
        if (total_bal91 == "")
        {
            total_bal91 = "0";
        }
        if (total_bal60 == "")
        {
            total_bal60 = "0";
        }

        if (lbl30_InvNO.Text == "")
        {
            lbl30_InvNO.Text = "0";
        }
        if (lbl61_InvNO.Text == "")
        {
            lbl61_InvNO.Text = "0";
        }
        if (lbl90_InvNO.Text == "")
        {
            lbl90_InvNO.Text = "0";
        }
        if (lbl31_InvNo.Text == "")
        {
            lbl31_InvNo.Text = "0";
        }

        float totalbill = float.Parse(total_bal30) + float.Parse(total_bal60) + float.Parse(total_bal31) + float.Parse(total_bal91);
        lbltotal_bill.Text = totalbill.ToString();

        float totalinv = float.Parse(lbl30_InvNO.Text) + float.Parse(lbl31_InvNo.Text) + float.Parse(lbl61_InvNO.Text) + float.Parse(lbl90_InvNO.Text);
        lbltotal_inv.Text = totalinv.ToString();
        if (totalgst60 == "")
        {
            lbl61_gstpay.Text = "0";
           
        }
        if (totalgst30 == "")
        {
          //  lbl30_GstPay.Text = "0";
            totalgst30 = "0";
        }
        if (totalgst31 == "")
        {
          //  lbl31_GstPay.Text = "0";
            totalgst31 = "0";
        }
        if (totalgst60 == "")
        {
           // lbl61_gstpay.Text = "0";
            totalgst60 = "0";
        }
        if (totalgst91 == "")
        {
            //   lbl90_gstpay.Text = "0";
            totalgst91 = "0";
        }

        float total_gst = float.Parse(totalgst30) + float.Parse(totalgst31)  + float.Parse(totalgst60) + float.Parse(totalgst91);
        lbltotal_gst.Text = total_gst.ToString();
    }
    public void formatrs()
    {


    }

    protected void GridView6_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lblRowNumber") as Label).Text = (e.Row.RowIndex + 1).ToString();
            Label lblGSTClaimable = e.Row.FindControl("lblGSTClaimable") as Label;
            Label lblBillPayable = e.Row.FindControl("lblBillPayable") as Label;
            Label lblGSTBalance = e.Row.FindControl("lblGSTBalance") as Label;
            Label lblBillBalanceBalance = e.Row.FindControl("lblBillBalanceBalance") as Label;

            Label lblBillDate = e.Row.FindControl("lblBillDate") as Label;
            lblBillDate.Text = dml.dateConvert(lblBillDate.Text);

            dislaydigit_textbox(lblGSTClaimable, float.Parse(lblGSTClaimable.Text));
            dislaydigit_textbox(lblBillPayable, float.Parse(lblBillPayable.Text));
            dislaydigit_textbox(lblGSTBalance, float.Parse(lblGSTBalance.Text));
            dislaydigit_textbox(lblBillBalanceBalance, float.Parse(lblBillBalanceBalance.Text));
        }

    }
    public void dislaydigit_label(GridViewRow row, string controlname, float value)
    {
        string displ = inv.displaydigit(branchId().ToString());

        if (displ == "0")
        {

            (row.FindControl(controlname) as Label).Text = value.ToString("0");

        }

        else if (displ == "1")
        {

            (row.FindControl(controlname) as Label).Text = value.ToString("0.0");

        }
        else if (displ == "2")
        {

            (row.FindControl(controlname) as Label).Text = value.ToString("0.00");

        }
        else if (displ == "3")
        {

            (row.FindControl(controlname) as Label).Text = value.ToString("0.000");

        }
        else if (displ == "4")
        {

            (row.FindControl(controlname) as Label).Text = value.ToString("0.0000");

        }
        else if (displ == "5")
        {

            (row.FindControl(controlname) as Label).Text = value.ToString("0.00000");

        }
        else if (displ == "6")
        {

            (row.FindControl(controlname) as Label).Text = value.ToString("0.000000");

        }
        else if (displ == "7")
        {

            (row.FindControl(controlname) as Label).Text = value.ToString("0.0000000");

        }
        else if (displ == "8")
        {

            (row.FindControl(controlname) as Label).Text = value.ToString("0.00000000");

        }
        else
        {

            (row.FindControl(controlname) as Label).Text = value.ToString("0");

        }
    }

    public void dislaydigit_textbox(Label box, float value)
    {
        string displ = inv.displaydigit(branchId().ToString());

        if (displ == "0")
        {

            box.Text = value.ToString("0");

        }

        else if (displ == "1")
        {

            box.Text = value.ToString("0.0");

        }
        else if (displ == "2")
        {

            box.Text = value.ToString("0.00");

        }
        else if (displ == "3")
        {

            box.Text = value.ToString("0.000");

        }
        else if (displ == "4")
        {

            box.Text = value.ToString("0.0000");

        }
        else if (displ == "5")
        {

            box.Text = value.ToString("0.00000");

        }
        else if (displ == "6")
        {

            box.Text = value.ToString("0.000000");

        }
        else if (displ == "7")
        {

            box.Text = value.ToString("0.0000000");

        }
        else if (displ == "8")
        {

            box.Text = value.ToString("0.00000000");

        }
        else
        {

            box.Text = value.ToString("0");

        }
    }

    protected void txt30Appr_TextChanged(object sender, EventArgs e)
    {
        if (txt30Appr.Text != "")
        {
            float appramt = float.Parse(txt30Appr.Text);
            float remaingamt = 0;


            foreach (GridViewRow row in GridView6.Rows)
            {
                Label lblBillPayable = (Label)row.FindControl("lblBillPayable");
                Label lblApprovedAmount = (Label)row.FindControl("lblApprovedAmount");
                Label lblBillBalanceBalance = (Label)row.FindControl("lblBillBalanceBalance");
                Label lblBillBalanceBalanceHide = (Label)row.FindControl("lblBillBalanceBalanceHide");


                lblBillBalanceBalance.Text = "";
                lblApprovedAmount.Text = "";
                lblBillBalanceBalanceHide.Text = "";
                float fa_lblApprovedAmount = 0;
                float fa_lblBillBalanceBalance = 0;

                float fa_lblBillPayable = float.Parse(lblBillPayable.Text);
                if (lblApprovedAmount.Text != "")
                {
                    fa_lblApprovedAmount = float.Parse(lblApprovedAmount.Text);
                }
                else
                {
                    fa_lblApprovedAmount = 0;
                }

                if (lblBillBalanceBalance.Text != "")
                {
                    fa_lblBillBalanceBalance = float.Parse(lblBillBalanceBalance.Text);


                }
                else
                {
                    fa_lblBillBalanceBalance = 0;
                }





                float totalminusvalue = appramt;

                if (totalminusvalue > 0 && totalminusvalue > float.Parse(lblBillPayable.Text))
                {
                    totalminusvalue = appramt - float.Parse(lblBillPayable.Text);
                    lblApprovedAmount.Text = (appramt - totalminusvalue).ToString("0.00");
                    lblBillBalanceBalance.Text = (float.Parse(lblBillPayable.Text) - float.Parse(lblApprovedAmount.Text)).ToString("0.00");
                    lblApprovedAmount.Text = inv.FinancialroundOff_Value(branchId().ToString(), lblApprovedAmount.Text);
                    lblBillBalanceBalanceHide.Text = lblBillBalanceBalance.Text;
                    lblBillBalanceBalance.Text = inv.FinancialroundOff_Value(branchId().ToString(), lblBillBalanceBalance.Text);
                    appramt = totalminusvalue;
                }
                else
                {
                    totalminusvalue = float.Parse(lblBillPayable.Text) - appramt;
                    lblBillBalanceBalance.Text = totalminusvalue.ToString("0.00");
                    lblApprovedAmount.Text = appramt.ToString("0.00");
                    appramt = appramt - float.Parse(lblApprovedAmount.Text);
                    lblApprovedAmount.Text = inv.FinancialroundOff_Value(branchId().ToString(), lblApprovedAmount.Text);
                    lblBillBalanceBalanceHide.Text = lblBillBalanceBalance.Text;
                    lblBillBalanceBalance.Text = inv.FinancialroundOff_Value(branchId().ToString(), lblBillBalanceBalance.Text);
                }
            }
        }
    }

    protected void chk30_CheckedChanged(object sender, EventArgs e)
    {
        if (chk30.Checked == true)
        {
            
            lbl30Appr.Text = lbl30_billPayHide.Text;
            txt30Appr.Text = lbl30Appr.Text;
            txt30Appr_TextChanged(sender, e);
        }
        else
        {
            txt30Appr.Text = "";
        }
    }

    protected void rdbGstClaimable_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbBillPay.Checked == true)
        {
            dml.dropdownsqlwithquery(ddlsupplier, "select  BPartnerId,BPartnerName from SET_BusinessPartner  where BPartnerId in (select DISTINCT BPartnerID from Fin_PurchaseMaster where sno in (SELECT Sno_Master from Fin_PurchaseDetail where BalanceAmount > 0) )", "BPartnerName", "BPartnerId");
        }
        if (rdbGstClaimable.Checked == true)
        {
            dml.dropdownsqlwithquery(ddlsupplier, "select  BPartnerId,BPartnerName from SET_BusinessPartner where GstServiceProvider = 1", "BPartnerName", "BPartnerId");
        }
    }

    protected void rdbBillPay_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbBillPay.Checked == true)
        {
            dml.dropdownsqlwithquery(ddlsupplier, "select  BPartnerId,BPartnerName from SET_BusinessPartner  where BPartnerId in (select DISTINCT BPartnerID from Fin_PurchaseMaster where sno in (SELECT Sno_Master from Fin_PurchaseDetail where BalanceAmount > 0) )", "BPartnerName", "BPartnerId");
        }
        if (rdbGstClaimable.Checked == true)
        {
            dml.dropdownsqlwithquery(ddlsupplier, "select  BPartnerId,BPartnerName from SET_BusinessPartner where GstServiceProvider = 1", "BPartnerName", "BPartnerId");
        }
    }
}