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
using Telerik.Web.UI;

public partial class frm_Period : System.Web.UI.Page
{
    int DateFrom, AddDays, EditDays, DeleteDays;
    string userid, UserGrpID, FormID, fiscal;
    DmlOperation dml = new DmlOperation();
    radcomboxclass cmb = new radcomboxclass();
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
            fiscal = Request.QueryString["fiscaly"];
            fd.HideUSerGrpField(Page.Controls, UserGrpID, FormID, "SET_Department");
            dml.dropdownsql(ddldelete_Description, "SET_ItemType", "Description", "ItemTypeID");
            dml.dropdownsql(ddlEdit_Description, "SET_ItemType", "Description", "ItemTypeID");
            dml.dropdownsql(ddlFind_Description, "SET_ItemType", "Description", "ItemTypeID");
            Showall_Dml();
            textClear();
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

        txtDescription.Enabled = true;
        RadComboAcct_Code.Enabled = true;
        radInventoryAcct.Enabled = true;
        RadExpenseAccount.Enabled = true;
        radCostOfGoodMAcct.Enabled = true;
        radCostOfGoodsSAcct.Enabled = true;
        radWIP_CwipAcct.Enabled = true;
        radFinishGoodsAcct.Enabled = true;
        radGST_R_Acct.Enabled = true;
        radGST_P_Acct.Enabled = true;
        radPurchase_Dis_Acct.Enabled = true;
        radSales_Disc_Acct.Enabled = true;
        radPurchaseReturnAcct.Enabled = true;
        radSalesReturnedAcct.Enabled = true;
        radFEDTax_Pay_Acct.Enabled = true;
        radSED_Tax_PAy_Acct.Enabled = true;
        radFixedAssetAcct.Enabled = true;
        radWHTax_Acct.Enabled = true;
        radFEDExpenseAcct.Enabled = true;
        radSEDExpenseAcct.Enabled = true;
        radStockAdjustmentAcct.Enabled = true;
        chk_Active.Enabled = true;
        chk_Active.Checked = true;

        txtCreatedBy.Enabled = false;
        txtSystemDate.Enabled = false;
        txtUpdatedBy.Enabled = false;
        txtUpadtedDate.Enabled = false;
        chk_Active.Checked = true;

        txtSystemDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

        userid = Request.QueryString["UserID"];
        DataSet ds_autoUserName = dml.Find("select user_name from SET_User_Manager where UserId ='" + userid + "'");
        txtCreatedBy.Text = ds_autoUserName.Tables[0].Rows[0]["user_name"].ToString();



    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        userid = Request.QueryString["UserID"];
        UserGrpID = Request.QueryString["UsergrpID"];
        FormID = Request.QueryString["FormID"];
        fiscal = Request.QueryString["fiscaly"];
        try
        {
            int chk;

            if (chk_Active.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
            dml.Insert("INSERT INTO [SET_ItemType] ([Description], [IsActive], [PurchasesAcct], [InventoryAcct], [ExpenseAcct], [CostofGoodsManfAcct], [CostofGoodsSoldAcct], [WIP_CwipAcct], [FinishedGoodsAcct], [GSTReceivableAcct], [GSTPayableAcct], [PurchaseDiscountAcct], [SalesDiscountAcct], [PurchaseReturnAcct], [SalesReturnAcct], [FEDTaxPayableAcct], [SEDTaxPayableAcct], [WHTaxAcct], [FEDExpenseAcct], [SEDExpenseAcct], [StockAdjustmentAcct], [CreatedBy], [CreateDate],[Record_Deleted],[FixedAssetAcct],[MLD]) VALUES ('" + txtDescription.Text + "', '" + chk + "', '" + RadComboAcct_Code.Text + "', '" + radInventoryAcct.Text + "', '" + RadExpenseAccount.Text + "', '" + radCostOfGoodMAcct.Text + "', '" + radCostOfGoodsSAcct.Text + "', '" + radWIP_CwipAcct.Text + "', '" + radFinishGoodsAcct.Text + "', '" + radGST_R_Acct.Text + "', '" + radGST_P_Acct.Text + "', '" + radPurchase_Dis_Acct.Text + "', '" + radSales_Disc_Acct.Text + "', '" + radPurchaseReturnAcct.Text + "', '" + radSalesReturnedAcct.Text + "','" + radFEDTax_Pay_Acct.Text + "', '" + radSED_Tax_PAy_Acct.Text + "', '" + radWHTax_Acct.Text + "', '" + radFEDExpenseAcct.Text + "', '" + radSEDExpenseAcct.Text + "', '" + radStockAdjustmentAcct.Text + "', '" + txtCreatedBy.Text + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0','"+radFixedAssetAcct.Text+"','"+dml.Encrypt("h")+"');", "alertme()");
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);

            txtDescription.Text = "";
            RadComboAcct_Code.Text = "";
            radInventoryAcct.Text = "";
            RadExpenseAccount.Text = "";
            radCostOfGoodMAcct.Text = "";
            radCostOfGoodsSAcct.Text = "";
            radWIP_CwipAcct.Text = "";
            radFinishGoodsAcct.Text = "";
            radGST_R_Acct.Text = "";
            radGST_P_Acct.Text = "";
            radPurchase_Dis_Acct.Text = "";
            radSales_Disc_Acct.Text = "";
            radPurchaseReturnAcct.Text = "";
            radSalesReturnedAcct.Text = "";
            radFEDTax_Pay_Acct.Text = "";
            radSED_Tax_PAy_Acct.Text = "";
            radWHTax_Acct.Text = "";
            radFEDExpenseAcct.Text = "";
            radSEDExpenseAcct.Text = "";
            radStockAdjustmentAcct.Text = "";
            radFixedAssetAcct.Text = "";
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            txtUpdatedBy.Text = show_username();
            int chk = 0;
            if (chk_Active.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }

            DataSet ds_up = dml.Find("select * from SET_ItemType WHERE ([ItemTypeID]='" + ViewState["SNO"].ToString() + "') AND ([Description]='" + txtDescription.Text + "') AND ([IsActive]='" + chk + "') AND ([PurchasesAcct]='" + RadComboAcct_Code.Text + "') AND ([InventoryAcct]='" + radInventoryAcct.Text + "') AND ([ExpenseAcct]='" + RadExpenseAccount.Text + "') AND ([CostofGoodsManfAcct]='" + radCostOfGoodMAcct.Text + "') AND ([CostofGoodsSoldAcct]='" + radCostOfGoodsSAcct.Text + "') AND ([WIP_CwipAcct]='" + radWIP_CwipAcct.Text + "') AND ([FinishedGoodsAcct]='" + radFinishGoodsAcct.Text + "') AND ([GSTReceivableAcct]='" + radGST_R_Acct.Text + "') AND ([GSTPayableAcct]='" + radGST_P_Acct.Text + "') AND ([PurchaseDiscountAcct]='" + radPurchase_Dis_Acct.Text + "') AND ([SalesDiscountAcct]='" + radSales_Disc_Acct.Text + "') AND ([PurchaseReturnAcct]='" + radPurchaseReturnAcct.Text + "') AND ([SalesReturnAcct]='" + radSalesReturnedAcct.Text + "') AND ([FEDTaxPayableAcct]='" + radFEDTax_Pay_Acct.Text + "') AND ([SEDTaxPayableAcct]='" + radSED_Tax_PAy_Acct.Text + "') AND ([WHTaxAcct]='" + radWHTax_Acct.Text + "') AND ([FEDExpenseAcct]='" + radFEDExpenseAcct.Text + "') AND ([SEDExpenseAcct]='" + radSEDExpenseAcct.Text + "') AND ([StockAdjustmentAcct]='" + radStockAdjustmentAcct.Text + "') AND ([FixedAssetAcct]= '"+radFixedAssetAcct.Text+"')");

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
                dml.Update("UPDATE [SET_ItemType] SET  [Description]='" + txtDescription.Text + "', [IsActive]='" + chk + "', [PurchasesAcct]='" + RadComboAcct_Code.Text + "', [InventoryAcct]='" + radInventoryAcct.Text + "', [ExpenseAcct]='" + RadExpenseAccount.Text + "', [CostofGoodsManfAcct]='" + radCostOfGoodMAcct.Text + "', [CostofGoodsSoldAcct]='" + radCostOfGoodsSAcct.Text + "', [WIP_CwipAcct]='" + radWIP_CwipAcct.Text + "', [FinishedGoodsAcct]='" + radFinishGoodsAcct.Text + "', [GSTReceivableAcct]='" + radGST_R_Acct.Text + "', [GSTPayableAcct]='" + radGST_P_Acct.Text + "', [PurchaseDiscountAcct]='" + radPurchase_Dis_Acct.Text + "', [SalesDiscountAcct]='" + radSales_Disc_Acct.Text + "', [PurchaseReturnAcct]='" + radPurchaseReturnAcct.Text + "', [SalesReturnAcct]='" + radSalesReturnedAcct.Text + "', [FEDTaxPayableAcct]='" + radFEDTax_Pay_Acct.Text + "', [SEDTaxPayableAcct]='" + radSED_Tax_PAy_Acct.Text + "', [WHTaxAcct]='" + radWHTax_Acct.Text + "', [FEDExpenseAcct]='" + radFEDExpenseAcct.Text + "', [SEDExpenseAcct]='" + radSEDExpenseAcct.Text + "', [StockAdjustmentAcct]='" + radStockAdjustmentAcct.Text + "', [UpdatedBy]='" + txtUpdatedBy.Text + "', [UpdateDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', [FixedAssetAcct] = '"+radFixedAssetAcct.Text+"' WHERE ([ItemTypeID]='" + ViewState["SNO"].ToString() + "');", "");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", " Editalert()", true);
                textClear();
                btnInsert.Visible = true;
                btnDelete.Visible = true;
                btnUpdate.Visible = false;
                btnEdit.Visible = true;
                btnFind.Visible = true;
                btnSave.Visible = false;
                btnDelete_after.Visible = false;
                Showall_Dml();
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
        updatecol.Visible = false;
        btnDelete.Visible = true;
        btnUpdate.Visible = false;
        btnEdit.Visible = true;
        btnFind.Visible = true;
        btnSave.Visible = false;
        btnDelete_after.Visible = false;
        textClear();
        Showall_Dml();
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
            string swhere;
            string squer = "SELECT * from SET_ItemType";

            if (ddldelete_Description.SelectedIndex != 0)
            {
                swhere = "Description like '" + ddldelete_Description.SelectedItem.Text + "%'";
            }
            else
            {
                swhere = "Description is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY Description";

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

        txtDescription.Enabled = false;
        RadComboAcct_Code.Enabled = false;
        radInventoryAcct.Enabled = false;
        RadExpenseAccount.Enabled = false;
        radCostOfGoodMAcct.Enabled = false;
        radCostOfGoodsSAcct.Enabled = false;
        radWIP_CwipAcct.Enabled = false;
        radFinishGoodsAcct.Enabled = false;
        radGST_R_Acct.Enabled = false;
        radGST_P_Acct.Enabled = false;
        radPurchase_Dis_Acct.Enabled = false;
        radSales_Disc_Acct.Enabled = false;
        radPurchaseReturnAcct.Enabled = false;
        radSalesReturnedAcct.Enabled = false;
        radFEDTax_Pay_Acct.Enabled = false;
        radSED_Tax_PAy_Acct.Enabled = false;
        radFixedAssetAcct.Enabled = false;
        radWHTax_Acct.Enabled = false;
        radFEDExpenseAcct.Enabled = false;
        radSEDExpenseAcct.Enabled = false;
        radStockAdjustmentAcct.Enabled = false;
        txtCreatedBy.Enabled = false;
        txtSystemDate.Enabled = false;
        txtUpdatedBy.Enabled = false;
        txtUpadtedDate.Enabled = false;
        chk_Active.Checked = false;

        btnCancel.Visible = true;
        btnFind.Visible = true;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnInsert.Visible = false;
        try
        {
            GridView1.DataBind();
            string swhere;
            string squer = "SELECT * from SET_ItemType";

            if (ddlFind_Description.SelectedIndex != 0)
            {
                swhere = "Description like '" + ddlFind_Description.SelectedItem.Text + "%'";
            }
            else
            {
                swhere = "Description is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY Description";

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
            GridView3.DataBind();
            string swhere;
            string squer = "SELECT * from SET_ItemType";

            if (ddlEdit_Description.SelectedIndex != 0)
            {
                swhere = "Description like '" + ddlEdit_Description.SelectedItem.Text + "%'";
            }
            else
            {
                swhere = "Description is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY Description";

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
    public void textClear()
    {
        DeleteBox.Visible = false;
        Editbox.Visible = false;
        Findbox.Visible = false;
        fieldbox.Visible = true;
        updatecol.Visible = false;

        txtDescription.Enabled = false;
        RadComboAcct_Code.Enabled = false;
        radInventoryAcct.Enabled = false;
        RadExpenseAccount.Enabled = false;
        radCostOfGoodMAcct.Enabled = false;
        radCostOfGoodsSAcct.Enabled = false;
        radWIP_CwipAcct.Enabled = false;
        radFinishGoodsAcct.Enabled = false;
        radGST_R_Acct.Enabled = false;
        radGST_P_Acct.Enabled = false;
        radPurchase_Dis_Acct.Enabled = false;
        radSales_Disc_Acct.Enabled = false;
        radPurchaseReturnAcct.Enabled = false;
        radSalesReturnedAcct.Enabled = false;
        radFEDTax_Pay_Acct.Enabled = false;
        radSED_Tax_PAy_Acct.Enabled = false;
        radWHTax_Acct.Enabled = false;
        radFEDExpenseAcct.Enabled = false;
        radFixedAssetAcct.Enabled = false;
        radSEDExpenseAcct.Enabled = false;
        radStockAdjustmentAcct.Enabled = false;
        chk_Active.Enabled = false;
        chk_Active.Checked = false;
        txtCreatedBy.Enabled = false;
        txtSystemDate.Enabled = false;
        txtUpdatedBy.Enabled = false;
        txtUpadtedDate.Enabled = false;


        txtDescription.Text = "";
        Label1.Text = "";
        RadComboAcct_Code.Text = "";
        radInventoryAcct.Text = "";
        RadExpenseAccount.Text = "";
        radCostOfGoodMAcct.Text = "";
        radCostOfGoodsSAcct.Text = "";
        radWIP_CwipAcct.Text = "";
        radFinishGoodsAcct.Text = "";
        radGST_R_Acct.Text = "";
        radGST_P_Acct.Text = "";
        radPurchase_Dis_Acct.Text = "";
        radSales_Disc_Acct.Text = "";
        radPurchaseReturnAcct.Text = "";
        radSalesReturnedAcct.Text = "";
        radFEDTax_Pay_Acct.Text = "";
        radSED_Tax_PAy_Acct.Text = "";
        radWHTax_Acct.Text = "";
        radFEDExpenseAcct.Text = "";
        radSEDExpenseAcct.Text = "";
        radStockAdjustmentAcct.Text = "";
        radFixedAssetAcct.Text = "";

        RadComboAcct_Code.ClearSelection();
        radInventoryAcct.ClearSelection();
        RadExpenseAccount.ClearSelection();
        radCostOfGoodMAcct.ClearSelection();
        radCostOfGoodsSAcct.ClearSelection();
        radWIP_CwipAcct.ClearSelection();
        radFinishGoodsAcct.ClearSelection();
        radGST_R_Acct.ClearSelection();
        radGST_P_Acct.ClearSelection();
        radPurchase_Dis_Acct.ClearSelection();
        radSales_Disc_Acct.ClearSelection();
        radPurchaseReturnAcct.ClearSelection();
        radSalesReturnedAcct.ClearSelection();
        radFEDTax_Pay_Acct.ClearSelection();
        radSED_Tax_PAy_Acct.ClearSelection();
        radWHTax_Acct.ClearSelection();
        radFEDExpenseAcct.ClearSelection();
        radSEDExpenseAcct.ClearSelection();
        radStockAdjustmentAcct.ClearSelection();
        radFixedAssetAcct.ClearSelection();


        txtCreatedBy.Text = "";
        txtSystemDate.Text = "";
        txtUpdatedBy.Text = "";
        txtUpadtedDate.Text = "";
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

        //FormID = Request.QueryString["FormID"];
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            dml.Delete("update SET_ItemType set Record_Deleted = 1 where ItemTypeID = " + ViewState["SNO"].ToString() + "", "");
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
        txtDescription.Enabled = false;
        RadComboAcct_Code.Enabled = false;
        radInventoryAcct.Enabled = false;
        RadExpenseAccount.Enabled = false;
        radCostOfGoodMAcct.Enabled = false;
        radCostOfGoodsSAcct.Enabled = false;
        radWIP_CwipAcct.Enabled = false;
        radFinishGoodsAcct.Enabled = false;
        radGST_R_Acct.Enabled = false;
        radGST_P_Acct.Enabled = false;
        radPurchase_Dis_Acct.Enabled = false;
        radSales_Disc_Acct.Enabled = false;
        radPurchaseReturnAcct.Enabled = false;
        radSalesReturnedAcct.Enabled = false;
        radFEDTax_Pay_Acct.Enabled = false;
        radSED_Tax_PAy_Acct.Enabled = false;
        radWHTax_Acct.Enabled = false;
        radFixedAssetAcct.Enabled = false;
        radFEDExpenseAcct.Enabled = false;
        radSEDExpenseAcct.Enabled = false;
        radStockAdjustmentAcct.Enabled = false;
        txtCreatedBy.Enabled = false;
        txtSystemDate.Enabled = false;
        txtUpdatedBy.Enabled = false;
        txtUpadtedDate.Enabled = false;
        chk_Active.Checked = false;

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
            DataSet ds = dml.Find("select * from SET_ItemType where ItemTypeID  = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                RadComboAcct_Code.ClearSelection();
                radInventoryAcct.ClearSelection();
                RadExpenseAccount.ClearSelection();
                radCostOfGoodMAcct.ClearSelection();
                radCostOfGoodsSAcct.ClearSelection();
                radWIP_CwipAcct.ClearSelection();
                radFinishGoodsAcct.ClearSelection();
                radGST_R_Acct.ClearSelection();
                radGST_P_Acct.ClearSelection();
                radPurchase_Dis_Acct.ClearSelection();
                radSales_Disc_Acct.ClearSelection();
                radPurchaseReturnAcct.ClearSelection();
                radSalesReturnedAcct.ClearSelection();
                radFEDTax_Pay_Acct.ClearSelection();
                radSED_Tax_PAy_Acct.ClearSelection();
                radWHTax_Acct.ClearSelection();
                radFEDExpenseAcct.ClearSelection();
                radSEDExpenseAcct.ClearSelection();
                radStockAdjustmentAcct.ClearSelection();
                radFixedAssetAcct.ClearSelection();


                txtDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                valuegetcombobox(ds, "PurchasesAcct", RadComboAcct_Code);
                valuegetcombobox(ds, "InventoryAcct", radInventoryAcct);
                valuegetcombobox(ds, "ExpenseAcct", RadExpenseAccount);
                valuegetcombobox(ds, "CostofGoodsManfAcct", radCostOfGoodMAcct);
                valuegetcombobox(ds, "CostofGoodsSoldAcct", radCostOfGoodsSAcct);
                valuegetcombobox(ds, "WIP_CwipAcct", radWIP_CwipAcct);
                valuegetcombobox(ds, "FinishedGoodsAcct", radFinishGoodsAcct);
                valuegetcombobox(ds, "GSTReceivableAcct", radGST_R_Acct);
                valuegetcombobox(ds, "GSTPayableAcct", radGST_P_Acct);
                valuegetcombobox(ds, "PurchaseDiscountAcct", radPurchase_Dis_Acct);
                valuegetcombobox(ds, "SalesDiscountAcct", radSales_Disc_Acct);
                valuegetcombobox(ds, "FEDTaxPayableAcct", radFEDTax_Pay_Acct);
                valuegetcombobox(ds, "SEDTaxPayableAcct", radSED_Tax_PAy_Acct);
                valuegetcombobox(ds, "WHTaxAcct", radWHTax_Acct);
                valuegetcombobox(ds, "FEDExpenseAcct", radFEDExpenseAcct);
                valuegetcombobox(ds, "SEDExpenseAcct", radSEDExpenseAcct);
                valuegetcombobox(ds, "StockAdjustmentAcct", radStockAdjustmentAcct);
                valuegetcombobox(ds, "PurchaseReturnAcct", radPurchaseReturnAcct);
                valuegetcombobox(ds, "SalesReturnAcct", radSalesReturnedAcct);
                valuegetcombobox(ds, "FixedAssetAcct", radFixedAssetAcct);




                txtCreatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());

                if (chk == true)
                {
                    chk_Active.Checked = true;
                }
                else
                {
                    chk_Active.Checked = false;
                }


                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() == "")
                {
                    txtSystemDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                    txtSystemDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdateDate"].ToString() == "")
                {
                    txtUpadtedDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateDate"].ToString());
                    txtUpadtedDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
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
        txtUpdatedBy.Enabled = false;
        txtUpadtedDate.Enabled = false;
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
            DataSet ds = dml.Find("select * from SET_ItemType where ItemTypeID  = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                RadComboAcct_Code.ClearSelection();
                radInventoryAcct.ClearSelection();
                RadExpenseAccount.ClearSelection();
                radCostOfGoodMAcct.ClearSelection();
                radCostOfGoodsSAcct.ClearSelection();
                radWIP_CwipAcct.ClearSelection();
                radFinishGoodsAcct.ClearSelection();
                radGST_R_Acct.ClearSelection();
                radGST_P_Acct.ClearSelection();
                radPurchase_Dis_Acct.ClearSelection();
                radSales_Disc_Acct.ClearSelection();
                radPurchaseReturnAcct.ClearSelection();
                radSalesReturnedAcct.ClearSelection();
                radFEDTax_Pay_Acct.ClearSelection();
                radSED_Tax_PAy_Acct.ClearSelection();
                radWHTax_Acct.ClearSelection();
                radFEDExpenseAcct.ClearSelection();
                radSEDExpenseAcct.ClearSelection();
                radStockAdjustmentAcct.ClearSelection();
                radFixedAssetAcct.ClearSelection();

                txtDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();


                valuegetcombobox(ds, "PurchasesAcct", RadComboAcct_Code);
                valuegetcombobox(ds, "InventoryAcct", radInventoryAcct);
                valuegetcombobox(ds, "ExpenseAcct", RadExpenseAccount);
                valuegetcombobox(ds, "CostofGoodsManfAcct", radCostOfGoodMAcct);
                valuegetcombobox(ds, "CostofGoodsSoldAcct", radCostOfGoodsSAcct);
                valuegetcombobox(ds, "WIP_CwipAcct", radWIP_CwipAcct);
                valuegetcombobox(ds, "FinishedGoodsAcct", radFinishGoodsAcct);
                valuegetcombobox(ds, "GSTReceivableAcct", radGST_R_Acct);
                valuegetcombobox(ds, "GSTPayableAcct", radGST_P_Acct);
                valuegetcombobox(ds, "PurchaseDiscountAcct", radPurchase_Dis_Acct);
                valuegetcombobox(ds, "SalesDiscountAcct", radSales_Disc_Acct);
                valuegetcombobox(ds, "FEDTaxPayableAcct", radFEDTax_Pay_Acct);
                valuegetcombobox(ds, "SEDTaxPayableAcct", radSED_Tax_PAy_Acct);
                valuegetcombobox(ds, "WHTaxAcct", radWHTax_Acct);
                valuegetcombobox(ds, "FEDExpenseAcct", radFEDExpenseAcct);
                valuegetcombobox(ds, "SEDExpenseAcct", radSEDExpenseAcct);
                valuegetcombobox(ds, "StockAdjustmentAcct", radStockAdjustmentAcct);
                valuegetcombobox(ds, "PurchaseReturnAcct", radPurchaseReturnAcct);
                valuegetcombobox(ds, "SalesReturnAcct", radSalesReturnedAcct);
                valuegetcombobox(ds, "FixedAssetAcct", radFixedAssetAcct);


                txtCreatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());

                if (chk == true)
                {
                    chk_Active.Checked = true;
                }
                else
                {
                    chk_Active.Checked = false;
                }


                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() == "")
                {
                    txtSystemDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                    txtSystemDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdateDate"].ToString() == "")
                {
                    txtUpadtedDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateDate"].ToString());
                    txtUpadtedDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }

                
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

        updatecol.Visible = true;
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {
            string serial_id;
            serial_id = GridView3.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select * from SET_ItemType where ItemTypeID  = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                RadComboAcct_Code.ClearSelection();
                radInventoryAcct.ClearSelection();
                RadExpenseAccount.ClearSelection();
                radCostOfGoodMAcct.ClearSelection();
                radCostOfGoodsSAcct.ClearSelection();
                radWIP_CwipAcct.ClearSelection();
                radFinishGoodsAcct.ClearSelection();
                radGST_R_Acct.ClearSelection();
                radGST_P_Acct.ClearSelection();
                radPurchase_Dis_Acct.ClearSelection();
                radSales_Disc_Acct.ClearSelection();
                radPurchaseReturnAcct.ClearSelection();
                radSalesReturnedAcct.ClearSelection();
                radFEDTax_Pay_Acct.ClearSelection();
                radSED_Tax_PAy_Acct.ClearSelection();
                radWHTax_Acct.ClearSelection();
                radFEDExpenseAcct.ClearSelection();
                radSEDExpenseAcct.ClearSelection();
                radStockAdjustmentAcct.ClearSelection();
                radFixedAssetAcct.ClearSelection();

                txtDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                valuegetcombobox(ds, "PurchasesAcct", RadComboAcct_Code);
                valuegetcombobox(ds, "InventoryAcct", radInventoryAcct);
                valuegetcombobox(ds, "ExpenseAcct", RadExpenseAccount);
                valuegetcombobox(ds, "CostofGoodsManfAcct", radCostOfGoodMAcct);
                valuegetcombobox(ds, "CostofGoodsSoldAcct", radCostOfGoodsSAcct);
                valuegetcombobox(ds, "WIP_CwipAcct", radWIP_CwipAcct);
                valuegetcombobox(ds, "FinishedGoodsAcct", radFinishGoodsAcct);
                valuegetcombobox(ds, "GSTReceivableAcct", radGST_R_Acct);
                valuegetcombobox(ds, "GSTPayableAcct", radGST_P_Acct);
                valuegetcombobox(ds, "PurchaseDiscountAcct", radPurchase_Dis_Acct);
                valuegetcombobox(ds, "SalesDiscountAcct", radSales_Disc_Acct);
                valuegetcombobox(ds, "FEDTaxPayableAcct", radFEDTax_Pay_Acct);
                valuegetcombobox(ds, "SEDTaxPayableAcct", radSED_Tax_PAy_Acct);
                valuegetcombobox(ds, "WHTaxAcct", radWHTax_Acct);
                valuegetcombobox(ds, "FEDExpenseAcct", radFEDExpenseAcct);
                valuegetcombobox(ds, "SEDExpenseAcct", radSEDExpenseAcct);
                valuegetcombobox(ds, "StockAdjustmentAcct", radStockAdjustmentAcct);
                valuegetcombobox(ds, "PurchaseReturnAcct", radPurchaseReturnAcct);
                valuegetcombobox(ds, "SalesReturnAcct", radSalesReturnedAcct);
                valuegetcombobox(ds, "FixedAssetAcct", radFixedAssetAcct);

                txtCreatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());

                if (chk == true)
                {
                    chk_Active.Checked = true;
                }
                else
                {
                    chk_Active.Checked = false;
                }


                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() == "")
                {
                    txtSystemDate.Text = "";
                }
                else
                {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                    txtSystemDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdateDate"].ToString() == "")
                {
                    txtUpadtedDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateDate"].ToString());
                    txtUpadtedDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                
                txtDescription.Enabled = true;
                RadComboAcct_Code.Enabled = true;
                radInventoryAcct.Enabled = true;
                RadExpenseAccount.Enabled = true;
                radCostOfGoodMAcct.Enabled = true;
                radCostOfGoodsSAcct.Enabled = true;
                radWIP_CwipAcct.Enabled = true;
                radFinishGoodsAcct.Enabled = true;
                radGST_R_Acct.Enabled = true;
                radGST_P_Acct.Enabled = true;
                radPurchase_Dis_Acct.Enabled = true;
                radSales_Disc_Acct.Enabled = true;
                radPurchaseReturnAcct.Enabled = true;
                radSalesReturnedAcct.Enabled = true;
                radFixedAssetAcct.Enabled = true;
                radFEDTax_Pay_Acct.Enabled = true;
                radSED_Tax_PAy_Acct.Enabled = true;
                radWHTax_Acct.Enabled = true;
                radFEDExpenseAcct.Enabled = true;
                radSEDExpenseAcct.Enabled = true;
                radStockAdjustmentAcct.Enabled = true;
                chk_Active.Enabled = true;


                txtCreatedBy.Enabled = false;
                txtSystemDate.Enabled = false;
                txtUpdatedBy.Enabled = false;
                txtUpadtedDate.Enabled = false;


               // tooltip();
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
    public void tooltip()
    {
        if (RadComboAcct_Code.SelectedValue != "")
        {
            RadComboAcct_Code.ToolTip = RadComboAcct_Code.SelectedValue.Split(new char[] { ':' })[1];
        }
        if (radInventoryAcct.SelectedValue != "")
        {
            radInventoryAcct.ToolTip = radInventoryAcct.SelectedValue.Split(new char[] { ':' })[1];
        }
        if (radInventoryAcct.SelectedValue != "")
        {
            radInventoryAcct.ToolTip = radInventoryAcct.SelectedValue.Split(new char[] { ':' })[1];
        }

        if (RadExpenseAccount.SelectedValue != "")
        {
            RadExpenseAccount.ToolTip = RadExpenseAccount.SelectedValue.Split(new char[] { ':' })[1];
        }
        if (radCostOfGoodMAcct.SelectedValue != "")
        {
            radCostOfGoodMAcct.ToolTip = radCostOfGoodMAcct.SelectedValue.Split(new char[] { ':' })[1];
        }
        if (radWIP_CwipAcct.SelectedValue != "")
        {
            radWIP_CwipAcct.ToolTip = radWIP_CwipAcct.SelectedValue.Split(new char[] { ':' })[1];
        }
        if (radCostOfGoodsSAcct.SelectedValue != "")
        {
            radCostOfGoodsSAcct.ToolTip = radCostOfGoodsSAcct.SelectedValue.Split(new char[] { ':' })[1];
        }
        if (radFinishGoodsAcct.SelectedValue != "")
        {
            radFinishGoodsAcct.ToolTip = radFinishGoodsAcct.SelectedValue.Split(new char[] { ':' })[1];
        }
        if (radGST_R_Acct.SelectedValue != "")
        {
            radGST_R_Acct.ToolTip = radGST_R_Acct.SelectedValue.Split(new char[] { ':' })[1];
        }
        if (radGST_P_Acct.SelectedValue != "")
        {
            radGST_P_Acct.ToolTip = radGST_P_Acct.SelectedValue.Split(new char[] { ':' })[1];
        }
        if (radPurchase_Dis_Acct.SelectedValue != "")
        {
            radPurchase_Dis_Acct.ToolTip = radPurchase_Dis_Acct.SelectedValue.Split(new char[] { ':' })[1];
        }
        if (radSales_Disc_Acct.SelectedValue != "")
        {
            radSales_Disc_Acct.ToolTip = radSales_Disc_Acct.SelectedValue.Split(new char[] { ':' })[1];
        }
        if (radPurchaseReturnAcct.SelectedValue != "")
        {
            radPurchaseReturnAcct.ToolTip = radPurchaseReturnAcct.SelectedValue.Split(new char[] { ':' })[1];
        }
        if (radSalesReturnedAcct.SelectedValue != "")
        {
            radSalesReturnedAcct.ToolTip = radSalesReturnedAcct.SelectedValue.Split(new char[] { ':' })[1];
        }
        if (radFEDTax_Pay_Acct.SelectedValue != "")
        {
            radFEDTax_Pay_Acct.ToolTip = radFEDTax_Pay_Acct.SelectedValue.Split(new char[] { ':' })[1];
        }
        if (radSED_Tax_PAy_Acct.SelectedValue != "")
        {
            radSED_Tax_PAy_Acct.ToolTip = radSED_Tax_PAy_Acct.SelectedValue.Split(new char[] { ':' })[1];
        }
        if (radWHTax_Acct.SelectedValue != "")
        {
            radWHTax_Acct.ToolTip = radWHTax_Acct.SelectedValue.Split(new char[] { ':' })[1];

        }
        if (radFEDExpenseAcct.SelectedValue != "")
        {
            radFEDExpenseAcct.ToolTip = radFEDExpenseAcct.SelectedValue.Split(new char[] { ':' })[1];
        }
        if (radSEDExpenseAcct.SelectedValue != "")
        {
            radSEDExpenseAcct.ToolTip = radSEDExpenseAcct.SelectedValue.Split(new char[] { ':' })[1];
        }
        if (radStockAdjustmentAcct.SelectedValue != "")
        {
            radStockAdjustmentAcct.ToolTip = radStockAdjustmentAcct.SelectedValue.Split(new char[] { ':' })[1];
        }

    }

    protected void RadComboAcct_Code_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(RadComboAcct_Code, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
    protected void radInventoryAcct_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(radInventoryAcct, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
    protected void RadExpenseAccount_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(RadExpenseAccount, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
    protected void radCostOfGoodMAcct_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(radCostOfGoodMAcct, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
    protected void radWIP_CwipAcct_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(radWIP_CwipAcct, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
    protected void radCostOfGoodsSAcct_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(radCostOfGoodsSAcct, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
    protected void radFinishGoodsAcct_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(radFinishGoodsAcct, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
    protected void radGST_R_Acct_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(radGST_R_Acct, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
    protected void radGST_P_Acct_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(radGST_P_Acct, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
    protected void radPurchase_Dis_Acct_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(radPurchase_Dis_Acct, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
    protected void radSales_Disc_Acct_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(radSales_Disc_Acct, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
    protected void radPurchaseReturnAcct_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(radPurchaseReturnAcct, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
    protected void radSalesReturnedAcct_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(radSalesReturnedAcct, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
    protected void radFEDTax_Pay_Acct_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(radFEDTax_Pay_Acct, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
    protected void radSED_Tax_PAy_Acct_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(radSED_Tax_PAy_Acct, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
    protected void radWHTax_Acct_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(radWHTax_Acct, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
    protected void radFEDExpenseAcct_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(radFEDExpenseAcct, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
    protected void radSEDExpenseAcct_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(radSEDExpenseAcct, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
    protected void radStockAdjustmentAcct_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(radStockAdjustmentAcct, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
    protected void radFixedAssetAcct_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(radFixedAssetAcct, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }



    protected void RadComboAcct_Code_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (RadComboAcct_Code.SelectedValue != "")
        {
            RadComboAcct_Code.ToolTip = RadComboAcct_Code.SelectedValue.Split(new char[] { ':' })[1];
        }
    }
    protected void radInventoryAcct_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (radInventoryAcct.SelectedValue != "")
        {
            radInventoryAcct.ToolTip = radInventoryAcct.SelectedValue.Split(new char[] { ':' })[1];
        }
    }
    protected void RadExpenseAccount_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (RadExpenseAccount.SelectedValue != "")
        {
            RadExpenseAccount.ToolTip = RadExpenseAccount.SelectedValue.Split(new char[] { ':' })[1];
        }
    }
    protected void radCostOfGoodMAcct_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (radCostOfGoodMAcct.SelectedValue != "")
        {
            radCostOfGoodMAcct.ToolTip = radCostOfGoodMAcct.SelectedValue.Split(new char[] { ':' })[1];
        }
    }
    protected void radWIP_CwipAcct_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (radWIP_CwipAcct.SelectedValue != "")
        {
            radWIP_CwipAcct.ToolTip = radWIP_CwipAcct.SelectedValue.Split(new char[] { ':' })[1];
        }
    }
    protected void radCostOfGoodsSAcct_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (radCostOfGoodsSAcct.SelectedValue != "")
        {
            radCostOfGoodsSAcct.ToolTip = radCostOfGoodsSAcct.SelectedValue.Split(new char[] { ':' })[1];
        }
    }
    protected void radFinishGoodsAcct_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (radFinishGoodsAcct.SelectedValue != "")
        {
            radFinishGoodsAcct.ToolTip = radFinishGoodsAcct.SelectedValue.Split(new char[] { ':' })[1];
        }

    }
    protected void radGST_R_Acct_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (radGST_R_Acct.SelectedValue != "")
        {
            radGST_R_Acct.ToolTip = radGST_R_Acct.SelectedValue.Split(new char[] { ':' })[1];
        }
    }
    protected void radGST_P_Acct_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (radGST_P_Acct.SelectedValue != "")
        {
            radGST_P_Acct.ToolTip = radGST_P_Acct.SelectedValue.Split(new char[] { ':' })[1];
        }
    }
    protected void radPurchase_Dis_Acct_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (radPurchase_Dis_Acct.SelectedValue != "")
        {
            radPurchase_Dis_Acct.ToolTip = radPurchase_Dis_Acct.SelectedValue.Split(new char[] { ':' })[1];
        }
    }
    protected void radSales_Disc_Acct_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (radSales_Disc_Acct.SelectedValue != "")
        {
            radSales_Disc_Acct.ToolTip = radSales_Disc_Acct.SelectedValue.Split(new char[] { ':' })[1];
        }
    }
    protected void radPurchaseReturnAcct_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (radPurchaseReturnAcct.SelectedValue != "")
        {
            radPurchaseReturnAcct.ToolTip = radPurchaseReturnAcct.SelectedValue.Split(new char[] { ':' })[1];
        }
    }
    protected void radSalesReturnedAcct_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (radSalesReturnedAcct.SelectedValue != "")
        {
            radSalesReturnedAcct.ToolTip = radSalesReturnedAcct.SelectedValue.Split(new char[] { ':' })[1];
        }
    }
    protected void radFEDTax_Pay_Acct_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (radFEDTax_Pay_Acct.SelectedValue != "")
        {
            radFEDTax_Pay_Acct.ToolTip = radFEDTax_Pay_Acct.SelectedValue.Split(new char[] { ':' })[1];
        }
    }
    protected void radSED_Tax_PAy_Acct_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (radSED_Tax_PAy_Acct.SelectedValue != "")
        {
            radSED_Tax_PAy_Acct.ToolTip = radSED_Tax_PAy_Acct.SelectedValue.Split(new char[] { ':' })[1];
        }
    }
    protected void radWHTax_Acct_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (radWHTax_Acct.SelectedValue != "")
        {
            radWHTax_Acct.ToolTip = radWHTax_Acct.SelectedValue.Split(new char[] { ':' })[1];
        }
    }
    protected void radFEDExpenseAcct_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (radFEDExpenseAcct.SelectedValue != "")
        {
            radFEDExpenseAcct.ToolTip = radFEDExpenseAcct.SelectedValue.Split(new char[] { ':' })[1];
        }
    }
    protected void radSEDExpenseAcct_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (radSEDExpenseAcct.SelectedValue != "")
        {
            radSEDExpenseAcct.ToolTip = radSEDExpenseAcct.SelectedValue.Split(new char[] { ':' })[1];
        }
    }
    protected void radStockAdjustmentAcct_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (radStockAdjustmentAcct.SelectedValue != "")
        {
            radStockAdjustmentAcct.ToolTip = radStockAdjustmentAcct.SelectedValue.Split(new char[] { ':' })[1];
        }
    }
    protected void radFixedAssetAcct_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (radFixedAssetAcct.SelectedValue != "")
        {
            radFixedAssetAcct.ToolTip = radFixedAssetAcct.SelectedValue.Split(new char[] { ':' })[1];
        }
    }


    public void valuegetcombobox(DataSet dataset, string colname, RadComboBox rad)
    {
        string s = "";
        string code = dataset.Tables[0].Rows[0][colname].ToString();


        rad.Text = code;

        DataSet ds1 = dml.Find("select  Acct_Code,Acct_Description from SET_COA_detail where  Head_detail_ID = 'd1' and Record_Deleted = 0  and Acct_Code = '" + code + "'");
        if (ds1.Tables[0].Rows.Count > 0)
        {
            rad.ToolTip = ds1.Tables[0].Rows[0]["Acct_Description"].ToString();
        }


        //string str = dataset.Tables[0].Rows[0][colname].ToString();
        //if (str != "")
        //{
        //    string where = "Record_Deleted = '0'";
        //    cmb.serachcombo4(rad, str, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");
        //    rad.FindItemByText(dataset.Tables[0].Rows[0][colname].ToString()).Selected = true;
        //}
        //else
        //{
        //    rad.Text = "";
        //}

    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string id = e.Row.Cells[1].Text;
        bool flag = dml.isNumber(id);


        if (flag == true)
        {

            DataSet ds = dml.Find("select ItemTypeID,MLD from SET_ItemType where ItemTypeID = '" + id + "'");
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

            DataSet ds = dml.Find("select ItemTypeID,MLD from SET_ItemType where ItemTypeID = '" + id + "'");
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