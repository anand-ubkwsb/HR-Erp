using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using Telerik.Web.UI;

public partial class frm_Period : System.Web.UI.Page
{
    int DateFrom, AddDays, EditDays, DeleteDays;
    string userid, UserGrpID, FormID, fiscal, menuid;
    int valid, showd;
    string[] supplier = new string[4];
    string entrydate = "";

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
            supplier[0] = "";
            ViewState["showd"] = "0";
            btnUpdate.Visible = false;
            btnSave.Visible = false;
            btnDelete_after.Visible = false;
            userid = Request.QueryString["UserID"];
            UserGrpID = Request.QueryString["UsergrpID"];
            FormID = Request.QueryString["FormID"];
            fiscal = Request.QueryString["fiscaly"];
            menuid = Request.QueryString["Menuid"];

            string sdate = startdate(fiscal);
            string enddate = Enddate(fiscal);

            // dml.dropdownsql(ddlDocName, "SET_Documents", "DocDescription", "DocID");
            dml.dropdownsqlwithquery(ddlDocName, "select DocID,DocDescription from SET_Documents where docid in (select DocID from SET_UserGrp_Documents where UserGrpId = '" + UserGrpID + "' AND	(( StartDate <= '" + sdate + "') AND ((EndDate >= '" + enddate + "') OR (EndDate IS NULL))) and Record_Deleted = 0) and CompID = '" + compid() + "' and Record_Deleted = 0 and MenuId_Sno = '" + menuid + "' and FormId_Sno='" + FormID + "'", "DocDescription", "DocID");
            dml.dropdownsql(ddlDocAuth, "SET_Authority", "AuthorityName", "AuthorityId");
            dml.dropdownsql(ddlStatus, "SET_Status", "StatusName", "StatusId");
            dml.dropdownsql(ddlModeOFPayment, "Set_PaymentMode", "PaymentModeDescription", "PaymentModeId");
            dml.dropdownsql(ddlDocTypes, "SET_DocumentType", "DocName", "DocTypeId");
            txtEntryDate.Attributes.Add("readonly", "readonly");

            txtDeliveryDate.Attributes.Add("readonly", "readonly");

            dml.dropdownsql(ddlDepartment, "SET_Department", "DepartmentName", "DepartmentID");

            dml.dropdownsqlwithquery(ddlSupplier, "select BPartnerId,BPartnerName from SET_BusinessPartner where BPartnerId in (select BPartnerId from SET_BPartnerType where BPNatureID = 1)", "BPartnerName", "BPartnerId");
            CalendarExtender1.EndDate = DateTime.Now;
            CalendarExtender1.StartDate = DateTime.Parse(fiscalstart(fiscal));
            //select AFQ_RequisitionNo from SuppQuotationApprMaster 

            dml.dropdownsql(ddlEdit_Document, "SET_Documents", "DocDescription", "DocID");
            dml.dropdownsql(ddlEdit_DocNO, "SuppQuotationApprMaster", "DocNo", "Sno");
            dml.dropdownsql(ddlEdit_AFQRequisitionNo, "SuppQuotationApprMaster", "AFQ_RequisitionNo", "Sno");
            dml.dropdownsql(ddlEdit_Depart, "SET_Department", "DepartmentName", "DepartmentID");
            dml.dropdownsql(ddlEdit_Status, "SET_Status", "StatusName", "StatusId");

            dml.dropdownsql(ddlFind_Document, "SET_Documents", "DocDescription", "DocID");
            dml.dropdownsql(ddlFind_DocNO, "SuppQuotationApprMaster", "DocNo", "Sno");
            dml.dropdownsql(ddlFind_AFQRequisitionNo, "SuppQuotationApprMaster", "AFQ_RequisitionNo", "Sno");
            dml.dropdownsql(ddlFind_Department, "SET_Department", "DepartmentName", "DepartmentID");
            dml.dropdownsql(ddlFind_Status, "SET_Status", "StatusName", "StatusId");

            dml.dropdownsql(ddlDel_Document, "SET_Documents", "DocDescription", "DocID");
            dml.dropdownsql(ddlDel_DocNO, "SuppQuotationApprMaster", "DocNo", "Sno");
            dml.dropdownsql(ddlDel_AFQRequisitionNo, "SuppQuotationApprMaster", "AFQ_RequisitionNo", "Sno");
            dml.dropdownsql(ddlDel_Department, "SET_Department", "DepartmentName", "DepartmentID");
            dml.dropdownsql(ddlDel_Status, "SET_Status", "StatusName", "StatusId");
            textClear();
            updatecol.Visible = false;

            Div1.Visible = false;

            Findbox.Visible = false;
            DeleteBox.Visible = false;
            Editbox.Visible = false;



        }
    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        UserGrpID = Request.QueryString["UsergrpID"];
        FormID = Request.QueryString["FormID"];

       
        menuid = Request.QueryString["Menuid"];
        textClear();
        btnSave.Visible = true;
        btnEdit.Visible = false;
        btnFind.Visible = false;
        btnCancel.Visible = true;
        btnDelete.Visible = false;
        btnInsert.Visible = false;
        btnUpdate.Visible = false;
        updatecol.Visible = false;
        // Div1.Visible = true;
        ddlDocName.Enabled = true;
        doctype(menuid, FormID, UserGrpID);
        //  ddlDocName.Enabled = true;
        txtEntryDate.Enabled = true;
        // txtDocNo.Enabled = true;
        ddlDocAuth.Enabled = true;
        chkDirect.Enabled = true;
        ddlAFQ_ReqNo.Enabled = false;
        txtDeliveryDate.Enabled = true;
        ddlSupplier.Enabled = true;
        ddlModeOFPayment.Enabled = true;
        txtDeliverDuration.Enabled = true;
        txtRemarks.Enabled = true;
        txtSuppRefNo.Enabled = true;
        txtCreateddate.Enabled = false;
        ddlStatus.Enabled = true;
        chkActive.Enabled = true;
        txtUpdateDate.Enabled = false;
        imgPopup.Enabled = true;
        ImageButton2.Enabled = true;
        ddlDepartment.Enabled = true;
        ddlStatus.ClearSelection();
        ddlStatus.Items.FindByText("Open").Selected = true;

        chkActive.Checked = true;

        txtEntryDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        
        txtCreateddate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss") + " " + show_username();
        chkDirect_CheckedChanged(sender, e);
        if (ddlDocName.SelectedIndex != 0)
        {
            ddlDocName_SelectedIndexChanged(sender, e);
        }
        else
        {
            textClear();
            Label1.Text = "There is no assign documnet";
        }
        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        float bal;
        float a = 0;
        foreach (GridViewRow r in GridView5.Rows)
        {

            TextBox txtapproved = r.FindControl("txtApprovedQty") as TextBox;
            //chkSelect lblSno
            CheckBox chks = r.FindControl("chkSelect") as CheckBox;
            Label lblsno = r.FindControl("lblSno") as Label;
            if (chks.Checked == true)
            {

                if (chkDirect.Checked == true)
                {

                    DataSet ds = dml.Find("select BalanceQty from SET_StockRequisitionDetail where sno ='" + lblsno.Text + "'");
                    //if (ds.Tables[0].Rows.Count > 0)
                    //{
                    //    //string balQ = ds.Tables[0].Rows[0]["BalanceQty"].ToString();
                       
                    //}
                    //else
                    //{
                    //    bal = float.Parse(txtapproved.Text);
                    //    a = a + bal;
                    //}
                    bal = float.Parse(txtapproved.Text);
                    a = a + bal;

                    dml.Update("UPDATE SET_StockRequisitionDetail set BalanceQty = '" + bal.ToString() + "' where Sno = '" + lblsno.Text + "'", "");
                    if (a > 0)
                    {
                        dml.Update("update SET_StockRequisitionMaster set Status = '8' where sno in (select StockReqMid from SET_StockRequisitionDetail where Sno = '" + lblsno.Text + "')", "");

                    }
                    else
                    {
                        dml.Update("update SET_StockRequisitionMaster set Status = '2' where sno in (select StockReqMid from SET_StockRequisitionDetail where Sno = '" + lblsno.Text + "')", "");
                    }



                }
                else
                {

                    DataSet ds = dml.Find("select  BalanceQty from Set_QuotationReqDetail where Sno ='" + lblsno.Text + "'");
                    //if (ds.Tables[0].Rows.Count > 0)
                    //{
                    //    string balQ = ds.Tables[0].Rows[0]["BalanceQty"].ToString();

                    //    bal = float.Parse(balQ) - float.Parse(txtapproved.Text);
                    //    a = a + bal;
                    //}
                    //else
                    //{
                    //    string balQ = ds.Tables[0].Rows[0]["BalanceQty"].ToString();
                    //    bal = float.Parse(balQ);
                    //    a = a + bal;
                    //}
                    bal = float.Parse(txtapproved.Text);
                     a = a + bal;
                    dml.Update("UPDATE Set_QuotationReqDetail set BalanceQty = '" + bal.ToString() + "' where Sno = '" + lblsno.Text + "'", "");


                    if (a > 0)
                    {
                        dml.Update("update Set_QuotationReqMaster set Status = '6' where sno in (select QuoatReqMId from Set_QuotationReqDetail  where Sno = '" + lblsno.Text + "')", "");

                    }
                    else
                    {
                        dml.Update("update Set_QuotationReqMaster set Status = '2' where sno in (select QuoatReqMId from Set_QuotationReqDetail  where Sno = '" + lblsno.Text + "')", "");
                    }

                }

            }
        }



        insertq();

        //INSERT INTO [SuppQuotationApprMaster] ([Sno], [DocId], [DocType], [DocAutority], [DocNo], [DirectQuotation], [RequisitionNo], [StockRequisitionNO], [Entrydate], [Supplier], [DeliveryDate], [DeliveryLocation], [ModeOfPayment], [SupplierReferenceNo], [ForStore/Department], [Location], [DeliveryPeriod], [PaymentMode], [Status], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [Record_Deleted]) VALUES ('1', '1', '2', '1', '1920-000001', '1', '1920-000001', 'No entry', '2020-06-18', 'ABC', '2020-06-18', 'KARACHI', 'CASH', '0001', '1', '11', '5', NULL, '1', '1', '1', '1', '5', '24', 'FAHAD', '2020-06-18 03:26:56.0000', NULL, NULL, '0');

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {

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
            string squer = "select * from ViewSupplierQuotApprvd";


            if (ddlDel_Document.SelectedIndex != 0)
            {
                swhere = "DocId = '" + ddlDel_Document.SelectedItem.Value + "'";
            }
            else
            {
                swhere = "DocId is not null";
            }
            if (ddlDel_DocNO.SelectedIndex != 0)
            {
                swhere = swhere + " and DocNo = '" + ddlDel_DocNO.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and DocNo is not null";
            }
            if (ddlDel_AFQRequisitionNo.SelectedIndex != 0)
            {
                swhere = swhere + " and AFQ_RequisitionNo = '" + ddlDel_AFQRequisitionNo.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and AFQ_RequisitionNo is not null";
            }
            if (ddlFind_Department.SelectedIndex != 0)
            {
                swhere = swhere + " and DepartmentID = '" + ddlFind_Department.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and DepartmentID is not null";
            }
            if (ddlFind_Status.SelectedIndex != 0)
            {
                swhere = swhere + " and StatusId = '" + ddlFind_Status.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and StatusId is not null";
            }
            if (txtDel_Entrydate.Text != "")
            {
                swhere = swhere + " and Entrydate = '" + txtDel_Entrydate.Text + "'";
            }
            else
            {
                swhere = swhere + " and Entrydate is not null";
            }
            if (txtDel_DelviDate.Text != "")
            {
                swhere = swhere + " and DeliveryDate = '" + txtDel_DelviDate.Text + "'";
            }
            else
            {
                swhere = swhere + " and DeliveryDate is not null";
            }
            if (chkDel.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkDel.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and   [GocID] = '" + gocid() + "' and [BranchId]='" + branchId() + "' and  [FiscalYearID] = '" + FiscalYear() + "' and CompId = '" + compid() + "'  ORDER BY DocDescription";

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
            string squer = "select * from ViewSupplierQuotApprvd";


            if (ddlFind_Document.SelectedIndex != 0)
            {
                swhere = "DocId = '" + ddlFind_Document.SelectedItem.Value + "'";
            }
            else
            {
                swhere = "DocId is not null";
            }
            if (ddlFind_DocNO.SelectedIndex != 0)
            {
                swhere = swhere + " and DocNo = '" + ddlFind_DocNO.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and DocNo is not null";
            }
            if (ddlFind_AFQRequisitionNo.SelectedIndex != 0)
            {
                swhere = swhere + " and AFQ_RequisitionNo = '" + ddlFind_AFQRequisitionNo.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and AFQ_RequisitionNo is not null";
            }
            if (ddlFind_Department.SelectedIndex != 0)
            {
                swhere = swhere + " and DepartmentID = '" + ddlFind_Department.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and DepartmentID is not null";
            }
            if (ddlFind_Status.SelectedIndex != 0)
            {
                swhere = swhere + " and StatusId = '" + ddlFind_Status.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and StatusId is not null";
            }
            if (txtFind_Entrydate.Text != "")
            {
                swhere = swhere + " and Entrydate = '" + txtFind_Entrydate.Text + "'";
            }
            else
            {
                swhere = swhere + " and Entrydate is not null";
            }
            if (txtFind_DeliveryDate.Text != "")
            {
                swhere = swhere + " and DeliveryDate = '" + txtFind_DeliveryDate.Text + "'";
            }
            else
            {
                swhere = swhere + " and DeliveryDate is not null";
            }
            if (chkFind.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkFind.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and   [GocID] = '" + gocid() + "' and [BranchId]='" + branchId() + "' and  [FiscalYearID] = '" + FiscalYear() + "' and CompId = '" + compid() + "'  ORDER BY DocDescription";

            Findbox.Visible = true;
            fieldbox.Visible = false;
            DeleteBox.Visible = false;

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
            string swhere = "";
            string squer = "select * from ViewSupplierQuotApprvd";


            if (ddlEdit_Document.SelectedIndex != 0)
            {
                swhere = "DocId = '" + ddlEdit_Document.SelectedItem.Value + "'";
            }
            else
            {
                swhere = "DocId is not null";
            }
            if (ddlEdit_DocNO.SelectedIndex != 0)
            {
                swhere = swhere + " and DocNo = '" + ddlEdit_DocNO.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and DocNo is not null";
            }
            if (ddlEdit_AFQRequisitionNo.SelectedIndex != 0)
            {
                swhere = swhere + " and AFQ_RequisitionNo = '" + ddlEdit_AFQRequisitionNo.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and AFQ_RequisitionNo is not null";
            }
            if (ddlEdit_Depart.SelectedIndex != 0)
            {
                swhere = swhere + " and DepartmentID = '" + ddlEdit_Depart.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and DepartmentID is not null";
            }
            if (ddlEdit_Status.SelectedIndex != 0)
            {
                swhere = swhere + " and StatusId = '" + ddlEdit_Status.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and StatusId is not null";
            }
            if (txtEditEntrydate.Text != "")
            {
                swhere = swhere + " and Entrydate = '" + txtEditEntrydate.Text + "'";
            }
            else
            {
                swhere = swhere + " and Entrydate is not null";
            }
            if (txtEdit_deliveryDate.Text != "")
            {
                swhere = swhere + " and DeliveryDate = '" + txtEdit_deliveryDate.Text + "'";
            }
            else
            {
                swhere = swhere + " and DeliveryDate is not null";
            }
            if (chkEdit_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkEdit_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and   [GocID] = '" + gocid() + "' and [BranchId]='" + branchId() + "' and  [FiscalYearID] = '" + FiscalYear() + "' and CompId = '" + compid() + "'  ORDER BY DocDescription";

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

        ddlDocName.SelectedIndex = 0;
        txtEntryDate.Text = "";
        txtDocNo.Text = "";
        ddlDocAuth.SelectedIndex = 0;
        chkDirect.Checked = false;

        txtDeliveryDate.Text = "";
        ddlSupplier.SelectedIndex = 0;
        ddlModeOFPayment.SelectedIndex = 0;
        txtDeliverDuration.Text = "";
        txtRemarks.Text = "";
        txtSuppRefNo.Text = "";
        txtCreateddate.Text = "";
        ddlStatus.SelectedIndex = 0;
        chkActive.Checked = false;
        txtUpdateDate.Text = "";
        lbldocno.Text = "";
        ddlDepartment.SelectedIndex = 0;
        ddlDepartment.Enabled = false;
        Div2.Visible = false;
        // Div3.Visible = false;
        Div1.Visible = false;

        ddlDocName.Enabled = false;
        txtEntryDate.Enabled = false;
        txtDocNo.Enabled = false;
        ddlDocAuth.Enabled = false;
        chkDirect.Enabled = false;
        ddlAFQ_ReqNo.Enabled = false;
        txtDeliveryDate.Enabled = false;
        ddlSupplier.Enabled = false;
        ddlModeOFPayment.Enabled = false;
        txtDeliverDuration.Enabled = false;
        txtRemarks.Enabled = false;
        txtSuppRefNo.Enabled = false;
        txtCreateddate.Enabled = false;
        ddlStatus.Enabled = false;
        chkActive.Enabled = false;
        txtUpdateDate.Enabled = false;

        imgPopup.Enabled = false;
        ImageButton2.Enabled = false;
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
            dml.Delete("update SuppQuotationApprMaster set Record_Deleted = 1 where Sno = " + ViewState["SNO"].ToString() + "", "");
            textClear();
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "alertDelete()", true);
            btnInsert.Visible = true;
            btnDelete.Visible = true;
            btnUpdate.Visible = false;
            btnEdit.Visible = true;
            btnFind.Visible = true;
            btnSave.Visible = false;
            btnDelete_after.Visible = false;
            //}
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

            DataSet ds = dml.Find("select * from SuppQuotationApprMaster WHERE ([Sno]='" + serial_id + "') and Record_Deleted = '0'");
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlDocName.ClearSelection();
                ddlDocAuth.ClearSelection();
                ddlAFQ_ReqNo.ClearSelection();
                ddlSupplier.ClearSelection();
                ddlModeOFPayment.ClearSelection();
                ddlDepartment.ClearSelection();
                ddlStatus.ClearSelection();
                string str = ds.Tables[0].Rows[0]["AFQ_RequisitionNo"].ToString();



                ddlDocName.Items.FindByValue(ds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;
                ddlDocAuth.Items.FindByValue(ds.Tables[0].Rows[0]["DocAutority"].ToString()).Selected = true;
                //  ddlAFQ_ReqNo.Items.FindByText(ds.Tables[0].Rows[0]["AFQ_RequisitionNo"].ToString()).Selected = true;
                ddlSupplier.Items.FindByValue(ds.Tables[0].Rows[0]["BPartnerTypeID"].ToString()).Selected = true;
                ddlModeOFPayment.Items.FindByValue(ds.Tables[0].Rows[0]["PaymentModeId"].ToString()).Selected = true;
                ddlDepartment.Items.FindByValue(ds.Tables[0].Rows[0]["DepartmentID"].ToString()).Selected = true;
                ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["StatusId"].ToString()).Selected = true;

                txtEntryDate.Text = ds.Tables[0].Rows[0]["Entrydate"].ToString();
                txtDocNo.Text = ds.Tables[0].Rows[0]["DocNo"].ToString();
                txtDeliveryDate.Text = ds.Tables[0].Rows[0]["DeliveryDate"].ToString();
                txtDeliverDuration.Text = ds.Tables[0].Rows[0]["DeliveryPeriod"].ToString();
                //  txtRemarks.Text = ds.Tables[0].Rows[0][""].ToString();
                txtSuppRefNo.Text = ds.Tables[0].Rows[0]["SupplierReferenceNo"].ToString();
                txtCreateddate.Text = ds.Tables[0].Rows[0]["CreatedDate"].ToString() + " " + dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdateDate.Text = ds.Tables[0].Rows[0]["UpdatedDate"].ToString() + " " + dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());
                dml.dateConvert(txtEntryDate);
                dml.dateConvert(txtDeliveryDate);
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                string DirectQuotation = ds.Tables[0].Rows[0]["DirectQuotation"].ToString();
                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (DirectQuotation == "1")
                {
                    chkDirect.Checked = true;
                }
                else
                {
                    chkDirect.Checked = false;
                }
                showdetailEdit(serial_id);
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

            DataSet ds = dml.Find("select * from SuppQuotationApprMaster WHERE ([Sno]='" + serial_id + "') and Record_Deleted = '0'");
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlDocName.ClearSelection();
                ddlDocAuth.ClearSelection();
                ddlAFQ_ReqNo.ClearSelection();
                ddlSupplier.ClearSelection();
                ddlModeOFPayment.ClearSelection();
                ddlDepartment.ClearSelection();
                ddlStatus.ClearSelection();
                string str = ds.Tables[0].Rows[0]["AFQ_RequisitionNo"].ToString();



                ddlDocName.Items.FindByValue(ds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;
                ddlDocAuth.Items.FindByValue(ds.Tables[0].Rows[0]["DocAutority"].ToString()).Selected = true;
                //  ddlAFQ_ReqNo.Items.FindByText(ds.Tables[0].Rows[0]["AFQ_RequisitionNo"].ToString()).Selected = true;
                ddlSupplier.Items.FindByValue(ds.Tables[0].Rows[0]["BPartnerTypeID"].ToString()).Selected = true;
                ddlModeOFPayment.Items.FindByValue(ds.Tables[0].Rows[0]["PaymentModeId"].ToString()).Selected = true;
                ddlDepartment.Items.FindByValue(ds.Tables[0].Rows[0]["DepartmentID"].ToString()).Selected = true;
                ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["StatusId"].ToString()).Selected = true;

                txtEntryDate.Text = ds.Tables[0].Rows[0]["Entrydate"].ToString();
                txtDocNo.Text = ds.Tables[0].Rows[0]["DocNo"].ToString();
                txtDeliveryDate.Text = ds.Tables[0].Rows[0]["DeliveryDate"].ToString();
                txtDeliverDuration.Text = ds.Tables[0].Rows[0]["DeliveryPeriod"].ToString();
                //  txtRemarks.Text = ds.Tables[0].Rows[0][""].ToString();
                txtSuppRefNo.Text = ds.Tables[0].Rows[0]["SupplierReferenceNo"].ToString();
                txtCreateddate.Text = ds.Tables[0].Rows[0]["CreatedDate"].ToString() + " " + dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdateDate.Text = ds.Tables[0].Rows[0]["UpdatedDate"].ToString() + " " + dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());
                dml.dateConvert(txtEntryDate);
                dml.dateConvert(txtDeliveryDate);
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                string DirectQuotation = ds.Tables[0].Rows[0]["DirectQuotation"].ToString();
                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (DirectQuotation == "1")
                {
                    chkDirect.Checked = true;
                }
                else
                {
                    chkDirect.Checked = false;
                }
                showdetailEdit(serial_id);
            }
        }
        catch (Exception ex)
        {

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



        ddlDocName.Enabled = false;
        txtEntryDate.Enabled = true;
        txtDocNo.Enabled = false;
        ddlDocAuth.Enabled = false  ;
        chkDirect.Enabled = true;
        ddlAFQ_ReqNo.Enabled = true;
        txtDeliveryDate.Enabled = true;
        ddlSupplier.Enabled = true;
        ddlModeOFPayment.Enabled = true;
        txtDeliverDuration.Enabled = true;
        txtRemarks.Enabled = true;
        txtSuppRefNo.Enabled = true;
        txtCreateddate.Enabled = false;
        ddlStatus.Enabled = true;
        chkActive.Enabled = true;
        txtUpdateDate.Enabled = false;
        ddlDepartment.Enabled = true;
        txtUpdateBy.Visible = false;
        txtUpdateDate.Enabled = false;
        imgPopup.Enabled = true;
        ImageButton2.Enabled = true;

        updatecol.Visible = true;
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;


        try
        {
            string serial_id;
            serial_id = GridView3.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;

            DataSet ds = dml.Find("select * from SuppQuotationApprMaster WHERE ([Sno]='" + serial_id + "') and Record_Deleted = '0'");
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlDocName.ClearSelection();
                ddlDocAuth.ClearSelection();
                ddlAFQ_ReqNo.ClearSelection();
                ddlSupplier.ClearSelection();
                ddlModeOFPayment.ClearSelection();
                ddlDepartment.ClearSelection();
                ddlStatus.ClearSelection();
                string str = ds.Tables[0].Rows[0]["AFQ_RequisitionNo"].ToString();



                ddlDocName.Items.FindByValue(ds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;
                ddlDocAuth.Items.FindByValue(ds.Tables[0].Rows[0]["DocAutority"].ToString()).Selected = true;
                //  ddlAFQ_ReqNo.Items.FindByText(ds.Tables[0].Rows[0]["AFQ_RequisitionNo"].ToString()).Selected = true;
                ddlSupplier.Items.FindByValue(ds.Tables[0].Rows[0]["BPartnerTypeID"].ToString()).Selected = true;
                ddlModeOFPayment.Items.FindByValue(ds.Tables[0].Rows[0]["PaymentModeId"].ToString()).Selected = true;
                ddlDepartment.Items.FindByValue(ds.Tables[0].Rows[0]["DepartmentID"].ToString()).Selected = true;
                ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["StatusId"].ToString()).Selected = true;

                txtEntryDate.Text = ds.Tables[0].Rows[0]["Entrydate"].ToString();
                txtDocNo.Text = ds.Tables[0].Rows[0]["DocNo"].ToString();
                txtDeliveryDate.Text = ds.Tables[0].Rows[0]["DeliveryDate"].ToString();
                txtDeliverDuration.Text = ds.Tables[0].Rows[0]["DeliveryPeriod"].ToString();
                //  txtRemarks.Text = ds.Tables[0].Rows[0][""].ToString();
                txtSuppRefNo.Text = ds.Tables[0].Rows[0]["SupplierReferenceNo"].ToString();
                txtCreateddate.Text = ds.Tables[0].Rows[0]["CreatedDate"].ToString() + " " + dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdateDate.Text = ds.Tables[0].Rows[0]["UpdatedDate"].ToString() + " " + dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());
                dml.dateConvert(txtEntryDate);
                dml.dateConvert(txtDeliveryDate);
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                string DirectQuotation = ds.Tables[0].Rows[0]["DirectQuotation"].ToString();
                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (DirectQuotation == "1")
                {
                    chkDirect.Checked = true;
                }
                else
                {
                    chkDirect.Checked = false;
                }
               

                DataTable dtup = new DataTable();
                dtup.Columns.AddRange(new DataColumn[11] { new DataColumn("ItemSubHeadName"), new DataColumn("Description"), new DataColumn("UOMName"), new DataColumn("Quantity"), new DataColumn("ApprovedQuantity"), new DataColumn("Rate"), new DataColumn("GST"), new DataColumn("GSTRate"), new DataColumn("GrossValue"), new DataColumn("LocName"), new DataColumn("Project") });


                //select * from view_StockUpdate
                //select ItemSubHeadName , Description,CostCenterName,UOMName from view_StockUpdate
                DataSet ds_detail = dml.Find("select * from V_SuppAQ_Update where Sno_Master = '" + serial_id + "'");
                if (ds_detail.Tables[0].Rows.Count > 0)
                {
                    ViewState["dtup"] = ds_detail.Tables[0];
                    Div2.Visible = true;
                    GridView4.DataSource = ds_detail.Tables[0];
                    GridView4.DataBind();
                }
                PopulateGridview_Up();
              //  showdetailEdit(serial_id);





            }
        }
        catch (Exception ex)
        {

        }
    }
    public string show_username()
    {
        userid = Request.QueryString["UserID"];
        return userid;
    }
    public int gocid()
    {
        return Convert.ToInt32(Request.Cookies["GocId"].Value);
    }
    public int compid()
    {
    return Convert.ToInt32(Request.Cookies["CompId"].Value);
    }
    public int branchId()
    {
    return Convert.ToInt32(Request.Cookies["BranchId"].Value);
    }
    public int FiscalYear()
    {
        return Convert.ToInt32(Request.Cookies["FiscalYearId"].Value);
    }
    public string fiscalstart(string fyear)
    {
        string sdate;
        DataSet ds = dml.Find("select StartDate from SET_Fiscal_Year where FiscalYearId = " +FiscalYear());
        if (ds.Tables[0].Rows.Count > 0)
        {
            sdate = ds.Tables[0].Rows[0]["StartDate"].ToString();

        }
        else
        {
            sdate = (DateTime.Now.Year - 1).ToString() + "-07-01";
        }
        return sdate;

    }
    public string required_generatestr()
    {
        DateTime date = DateTime.Parse(txtEntryDate.Text);
        string year = date.ToString("yy");
        string month = date.Month.ToString("00");
        //string month = date.ToString("00");
        string docno = ddlDocName.SelectedItem.Value;
        string retval = "";
       // string month = DateTime.Now.Month.ToString("00");
        bool flag = true;
        bool flag1 = true;
       // DateTime date = DateTime.Now;
      //  string year = date.ToString("yy");
        FormID = Request.QueryString["FormID"];
        fiscal = Request.QueryString["fiscaly"];
        string fy = fiscal.Substring(2, 2) + fiscal.Substring(7, 2);
        string docval = "0";
        if (ddlDocName.SelectedIndex != 0)
        {
            docval = ddlDocName.SelectedItem.Value;
        }

        DataSet ds = dml.Find("select Monthlybase, YearlyBase from SET_Documents where DocID = '" + ddlDocName.SelectedItem.Value + "'; select MAX(DocNo) as maxno from SuppQuotationApprMaster where SUBSTRING(DocNo, 1, 4) = '" + month + year + "'; select Description from SET_Fiscal_Year where Description = '" + fiscal + "'; select MAX(DocNo) as maxno from SuppQuotationApprMaster where SUBSTRING(DocNo, 1, 4) = '" + fy + "'");

        if (ds.Tables[0].Rows.Count > 0)
        {

            string monthly = ds.Tables[0].Rows[0]["Monthlybase"].ToString();
            string yearly = ds.Tables[0].Rows[0]["YearlyBase"].ToString();
            string inc;

            inc = ds.Tables[1].Rows[0]["maxno"].ToString();
            if (inc == "")
            {
                inc = "00001";
                flag = false;
            }
            int incre;
            if (monthly == "True")
            {
                if (flag == true)
                {
                    incre = int.Parse(inc.ToString().Substring(5, 5)) + 1;
                }
                else
                {
                    incre = int.Parse(inc.ToString());
                }

                retval = month + year + "-" + incre.ToString("00000");
            }

            inc = ds.Tables[3].Rows[0]["maxno"].ToString();
            if (inc == "")
            {
                inc = "000001";
                flag1 = false;
            }
            if (yearly == "True")
            {
                if (flag1 == true)
                {
                    incre = int.Parse(inc.ToString().Substring(5, 6)) + 1;
                }
                else
                {
                    incre = int.Parse(inc.ToString());
                }
                string fyear = ds.Tables[2].Rows[0]["Description"].ToString();
                retval = fyear.Substring(2, 2) + fyear.Substring(7, 2) + "-" + incre.ToString("000000");


            }

        }
        return retval;
    }
    public string detailcond()
    {
        Label dp = GridView5.Rows[0].FindControl("lblRowNumber") as Label;

        if (dp.Text != "")
        {
            return dp.Text;
        }
        else
        {
            return "";
        }
    }
    public void PopulateGridview()
    {

        DataTable dtbl = (DataTable)ViewState["Customers"];

        if (dtbl.Rows.Count > 0)
        {

            GridView5.DataSource = (DataTable)ViewState["Customers"];
            GridView5.DataBind();

        }
        else
        {


            dtbl.Rows.Add(dtbl.NewRow());
            GridView5.DataSource = dtbl;
            GridView5.DataBind();

            GridView5.Rows[0].Cells.Clear();
            GridView5.Rows[0].Cells.Add(new TableCell());
            GridView5.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
            GridView5.Rows[0].Cells[0].Text = "No Data Found ..!";
            GridView5.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;

        }
    }
    public void showdetail(string reqNO)
    {
        Div1.Visible = true;


        if (chkDirect.Checked == true)
        {
            string query = "select * from View_stockDirect where reqNOdetail = '" + reqNO + "' and BalanceQty > 0";
            DataSet ds = dml.grid(query);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlSupplier.ClearSelection();
                if (ddlSupplier.Items.FindByText(ds.Tables[0].Rows[0]["Supplier"].ToString()) != null)
                {
                    ddlSupplier.Items.FindByText(ds.Tables[0].Rows[0]["Supplier"].ToString()).Selected = true;
                }
                GridView5.DataSource = ds;
                GridView5.DataBind();
                for (int a = 0; a < ds.Tables[0].Rows.Count; a++)
                {
                    ListBox lstbox = (ListBox)GridView5.Rows[a].FindControl("ListBox1");

                    ListItem items = new ListItem();
                    items.Text = ds.Tables[0].Rows[a]["Supplier"].ToString();
                    //lstbox.Items.Add(items);


                }
            }
            else
            {
                GridView5.ShowHeaderWhenEmpty = true;
                GridView5.EmptyDataText = "NO RECORD";
                GridView5.DataBind();
            }
        }
        else {
            //select * from View_SuppApproved
            string query = "select * from View_SuppApproved where QuotationReqNO = '" + reqNO + "' and BalanceQty > 0";
            DataSet ds = dml.grid(query);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlSupplier.ClearSelection();
                if (ddlSupplier.Items.FindByText(ds.Tables[0].Rows[0]["Supplier"].ToString()) != null)
                {
                    ddlSupplier.Items.FindByText(ds.Tables[0].Rows[0]["Supplier"].ToString()).Selected = true;
                }
                GridView5.DataSource = ds;
                GridView5.DataBind();
                for (int a = 0; a < ds.Tables[0].Rows.Count; a++)
                {
                    ListBox lstbox = (ListBox)GridView5.Rows[a].FindControl("ListBox1");

                    ListItem items = new ListItem();
                    items.Text = ds.Tables[0].Rows[a]["Supplier"].ToString();
                    //lstbox.Items.Add(items);


                }
            }
            else
            {
                GridView5.ShowHeaderWhenEmpty = true;
                GridView5.EmptyDataText = "NO RECORD";
                GridView5.DataBind();
            }
        }
    }
    protected void ddlAFQ_ReqNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAFQ_ReqNo.SelectedIndex != 0)
        {
            showdetail(ddlAFQ_ReqNo.SelectedItem.Text);
            foreach (GridViewRow row in GridView5.Rows)
            {
                DropDownList ddlcost = row.FindControl("ddlCostCenter") as DropDownList;
                dml.dropdownsql(ddlcost, "SET_CostCenter", "CostCenterName", "CostCenterID");
                DropDownList ddlloc = row.FindControl("ddlLocation") as DropDownList;
                dml.dropdownsql(ddlloc, "Set_Location", "LocName", "LocId");

            }


        }
        else
        {
            Div1.Visible = false;
        }
    }
    protected void chkDirect_CheckedChanged(object sender, EventArgs e)
    {
        if (chkDirect.Checked == true)
        {
            Div1.Visible = false;
            ddlAFQ_ReqNo.Enabled = true;
            //dml.dropdownsql(ddlAFQ_ReqNo, "SET_StockRequisitionMaster", "RequisitionNo", "Sno" , "Status","1");
            dml.dropdownsqlwithquery(ddlAFQ_ReqNo, "select * from SET_StockRequisitionMaster where (Status = 1 or Status = 8) and Record_Deleted = 0 and IsActive = 1", "RequisitionNo", "Sno");
            //select from SET_StockRequisitionMaster where RequisitionNo = '1920-000003' and(Status = 1 or Status = 8)
        }
        else
        {

            Div1.Visible = false;
            ddlAFQ_ReqNo.Enabled = true;
            dml.dropdownsqlwithquery(ddlAFQ_ReqNo, "select * from Set_QuotationReqMaster where (Status = 1 or Status = 8) and Record_Deleted = 0 and IsActive = 1", "QuotationReqNO", "Sno");


            //  dml.dropdownsql(ddlAFQ_ReqNo, "Set_QuotationReqMaster", "QuotationReqNO", "Sno" , "Status", "1");

        }
    }
    protected void ddlDocName_SelectedIndexChanged(object sender, EventArgs e)
    {


        string stdate = "";
        string end = "";
        fiscal = Request.QueryString["fiscaly"];
        UserGrpID = Request.QueryString["UsergrpID"];
        DataSet dsfy = dml.Find("select StartDate,EndDate from SET_Fiscal_Year where Description = '" + fiscal + "'");
        if (dsfy.Tables[0].Rows.Count > 0)
        {
            stdate = dsfy.Tables[0].Rows[0]["StartDate"].ToString();
            end = dsfy.Tables[0].Rows[0]["EndDate"].ToString();
        }


        string enddate = "";
        string querys = "";
        DataSet dsdoc = dml.Find("select DateFrom, DateTo from SET_UserGrp_DocAuthority where  DocId = '" + ddlDocName.SelectedItem.Value + "' and Record_Deleted = 0  and UserGrpId = '" + UserGrpID + "';");
        if (dsdoc.Tables[0].Rows.Count > 0)
        {
            enddate = dml.dateconvertString(dsdoc.Tables[0].Rows[0]["DateTo"].ToString());

            if (enddate == "2000-01-01")
            {
                querys = "select DateFrom, DateTo from SET_UserGrp_DocAuthority where DateFrom <= '" + stdate + "' and DocId = '" + ddlDocName.SelectedItem.Value + "' and Record_Deleted = 0  and UserGrpId = '" + UserGrpID + "';";
            }
            else
            {
                querys = "select DateFrom, DateTo from SET_UserGrp_DocAuthority where DateFrom <= '" + stdate + "' and DateTo >='" + end + "' and DocId = '" + ddlDocName.SelectedItem.Value + "' and Record_Deleted = 0  and UserGrpId = '" + UserGrpID + "';";
            }
            DataSet dd = dml.Find(querys);
            if (dd.Tables[0].Rows.Count > 0)
            {

                if (ddlDocName.SelectedIndex != 0)
                {

                    required_generate();
                    int docno = int.Parse(ddlDocName.SelectedItem.Value);
                    lbldocno.Text = docno.ToString("0000");


                    //;select AuthorityId, AuthorityName from SET_Authority where AuthorityId in (SELECT AuthorityId from SET_UserGrp_DocAuthority where docid= 112)

                    dml.dropdownsqlwithquery(ddlDocAuth, "select AuthorityId, AuthorityName from SET_Authority where AuthorityId in (SELECT AuthorityId from SET_UserGrp_DocAuthority where docid= '" + ddlDocName.SelectedItem.Value + "')", "AuthorityName", "AuthorityId");

                    if (ddlDocAuth.Items.Count > 2)
                    {
                        ddlDocAuth.Enabled = true;
                        ddlDocAuth.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlDocAuth.Enabled = false;
                        ddlDocAuth.SelectedIndex = 1;
                    }


                }
                else
                {
                    // ddlDocument.SelectedIndex = 0;
                    lbldocno.Text = "";
                }
            }
        }

    }
    public string required_generateins()
    {

        DateTime date = DateTime.Parse(txtEntryDate.Text);
        string year = date.ToString("yy");
        string month = date.Month.ToString("00");
        //string month = date.ToString("00");
        string docno = ddlDocName.SelectedItem.Value;
        // string month = DateTime.Now.Month.ToString("00");
        bool flag = true;
        bool flag1 = true;
        // DateTime date = DateTime.Now;
        // string year = date.ToString("yy");
        FormID = Request.QueryString["FormID"];
        fiscal = Request.QueryString["fiscaly"];
        string fy = fiscal.Substring(2, 2) + fiscal.Substring(7, 2);
        string docval = "0";
        if (ddlDocName.SelectedIndex != 0)
        {
            docval = ddlDocName.SelectedItem.Value;


            DataSet ds = dml.Find("select Monthlybase, YearlyBase from SET_Documents where DocID = '" + ddlDocName.SelectedItem.Value + "'; select MAX(DocNo) as maxno from SuppQuotationApprMaster where SUBSTRING(DocNo, 1, 4) = '" + month + year + "'; select Description from SET_Fiscal_Year where Description = '" + fiscal + "'; select MAX(DocNo) as maxno from SuppQuotationApprMaster where SUBSTRING(DocNo, 1, 4) = '" + fy + "'");

            if (ds.Tables[0].Rows.Count > 0)
            {
                string monthly = ds.Tables[0].Rows[0]["Monthlybase"].ToString();
                string yearly = ds.Tables[0].Rows[0]["YearlyBase"].ToString();
                string inc;

                inc = ds.Tables[1].Rows[0]["maxno"].ToString();
                if (inc == "")
                {
                    inc = "00001";
                    flag = false;
                }
                int incre;
                if (monthly == "True")
                {
                    if (flag == true)
                    {
                        incre = int.Parse(inc.ToString().Substring(5, 5)) + 1;
                    }
                    else
                    {
                        incre = int.Parse(inc.ToString());
                    }

                    txtDocNo.Text = docno + "-" + month + year + "-" + incre.ToString("00000");
                }

                inc = ds.Tables[3].Rows[0]["maxno"].ToString();
                if (inc == "")
                {
                    inc = "000001";
                    flag1 = false;
                }
                if (yearly == "True")
                {
                    if (flag1 == true)
                    {
                        incre = int.Parse(inc.ToString().Substring(5, 6)) + 1;
                    }
                    else
                    {
                        incre = int.Parse(inc.ToString());
                    }
                    string fyear = ds.Tables[2].Rows[0]["Description"].ToString();
                    txtDocNo.Text = docno + "-" + fyear.Substring(2, 2) + fyear.Substring(7, 2) + "-" + incre.ToString("000000");


                }

            }
        }
        return  txtDocNo.Text;
    }
    public void required_generate()
    {
        
        DateTime date = DateTime.Parse(txtEntryDate.Text);
        string year = date.ToString("yy");
        string month = date.Month.ToString("00");
        //string month = date.ToString("00");
        string docno = ddlDocName.SelectedItem.Value;
       // string month = DateTime.Now.Month.ToString("00");
        bool flag = true;
        bool flag1 = true;
       // DateTime date = DateTime.Now;
       // string year = date.ToString("yy");
        FormID = Request.QueryString["FormID"];
        fiscal = Request.QueryString["fiscaly"];
        string fy = fiscal.Substring(2, 2) + fiscal.Substring(7, 2);
        string docval = "0";
        if (ddlDocName.SelectedIndex != 0)
        {
            docval = ddlDocName.SelectedItem.Value;


            DataSet ds = dml.Find("select Monthlybase, YearlyBase from SET_Documents where DocID = '" + ddlDocName.SelectedItem.Value + "'; select MAX(DocNo) as maxno from SuppQuotationApprMaster where SUBSTRING(DocNo, 1, 4) = '" + month + year + "'; select Description from SET_Fiscal_Year where Description = '" + fiscal + "'; select MAX(DocNo) as maxno from SuppQuotationApprMaster where SUBSTRING(DocNo, 1, 4) = '" + fy + "'");

            if (ds.Tables[0].Rows.Count > 0)
            {
                string monthly = ds.Tables[0].Rows[0]["Monthlybase"].ToString();
                string yearly = ds.Tables[0].Rows[0]["YearlyBase"].ToString();
                string inc;

                inc = ds.Tables[1].Rows[0]["maxno"].ToString();
                if (inc == "")
                {
                    inc = "00001";
                    flag = false;
                }
                int incre;
                if (monthly == "True")
                {
                    if (flag == true)
                    {
                        incre = int.Parse(inc.ToString().Substring(5, 5)) + 1;
                    }
                    else
                    {
                        incre = int.Parse(inc.ToString());
                    }

                    txtDocNo.Text = docno+ "-" + month + year + "-" + incre.ToString("00000");
                }

                inc = ds.Tables[3].Rows[0]["maxno"].ToString();
                if (inc == "")
                {
                    inc = "000001";
                    flag1 = false;
                }
                if (yearly == "True")
                {
                    if (flag1 == true)
                    {
                        incre = int.Parse(inc.ToString().Substring(5, 6)) + 1;
                    }
                    else
                    {
                        incre = int.Parse(inc.ToString());
                    }
                    string fyear = ds.Tables[2].Rows[0]["Description"].ToString();
                    txtDocNo.Text = docno + "-" + fyear.Substring(2, 2) + fyear.Substring(7, 2) + "-" + incre.ToString("000000");
                    string doc = txtDocNo.Text;

                }

            }
        }
    }
    public void doctype(string menuid, string formid, string usergrpid)
    {

        ddlDocName.ClearSelection();
       //dml.dropdownsql2where(ddlDocName, "ViewUserGrp_Doc", "DocDescription", "DocID", "MenuId_Sno", menuid, "FormId_Sno", formid, "UserGrpId", usergrpid);



        if (ddlDocName.Items.Count == 0)
        {

            ddlDocName.SelectedIndex = 0;
            ddlDocName.Items.Insert(0, "Please select...");
            ddlDocName.Enabled = false;

        }
        else if (ddlDocName.Items.Count == 2)
        {
            ddlDocName.SelectedIndex = 1;

            ddlDocName.Enabled = false;
        }
        else
        {

            ddlDocName.SelectedIndex = 0;
            ddlDocName.Enabled = true;
        }






    }
    protected void GridView5_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lblRowNumber") as Label).Text = (e.Row.RowIndex + 1).ToString();


            string rate = (e.Row.FindControl("txtRate") as TextBox).Text;
            string gstPer = (e.Row.FindControl("lblGST") as Label).Text;
            float gstrate;
            if (rate == "")
            {
                gstrate = 0 * (0 / 100);
            }
            else
            {
                gstrate = float.Parse(rate) * (float.Parse(gstPer) / 100);
            }
            (e.Row.FindControl("lblGSTRate") as Label).Text = gstrate.ToString();

        }
    }
    protected void txtRate_TextChanged(object sender, EventArgs e)
    {

        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;

        string rate = (row.FindControl("txtRate") as TextBox).Text;
        string gstPer = (row.FindControl("txtGST") as TextBox).Text;
        string appQty = (row.FindControl("txtApprovedQty") as TextBox).Text;


        float gstrate = 0;
        if (rate == "")
        {
            gstrate = 0 * (0 / 100);
        }
        else
        {
            if (gstPer != "")
            {
                gstrate = float.Parse(rate) * (float.Parse(gstPer) / 100);
            }
            else
            {
                gstrate = float.Parse(rate) * 0;
            }
        }
            (row.FindControl("lblGSTRate") as Label).Text = gstrate.ToString();

        float qtyval = float.Parse(appQty) * float.Parse(rate);
        float gstval = float.Parse(appQty) * gstrate;

        //lblGrossValue
        (row.FindControl("lblQtyVal") as Label).Text = qtyval.ToString();

        (row.FindControl("lblGstValue") as Label).Text = gstval.ToString();
        float grossval = qtyval + gstval;


        (row.FindControl("lblGrossValue") as Label).Text = grossval.ToString();



    }
    protected void txtGST_TextChanged(object sender, EventArgs e)
    {

        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;

        string rate = (row.FindControl("txtRate") as TextBox).Text;
        string gstPer = (row.FindControl("txtGST") as TextBox).Text;
        string appQty = (row.FindControl("txtApprovedQty") as TextBox).Text;


        float gstrate = 0;
        if (rate == "")
        {
            gstrate = 0 * (0 / 100);
        }
        else
        {
            if (gstPer != "")
            {
                gstrate = float.Parse(rate) * (float.Parse(gstPer) / 100);
            }
        }
            (row.FindControl("lblGSTRate") as Label).Text = gstrate.ToString();

        float qtyval = float.Parse(appQty) * float.Parse(rate);
        float gstval = float.Parse(appQty) * gstrate;

        //lblGrossValue
        (row.FindControl("lblQtyVal") as Label).Text = qtyval.ToString();

        (row.FindControl("lblGstValue") as Label).Text = gstval.ToString();
        float grossval = qtyval + gstval;


        (row.FindControl("lblGrossValue") as Label).Text = grossval.ToString();



    }
    public void detailinsert(string masterid)
    {
        foreach (GridViewRow g in GridView5.Rows)
        {
            Label lblheadsub = (Label)g.FindControl("lblsubhead");
            Label requisno = (Label)g.FindControl("lblreq");
            Label lblitemmaster = (Label)g.FindControl("lblitemmaster");
            Label lbluom = (Label)g.FindControl("lbluom");
            Label lblQty = (Label)g.FindControl("lblQty");
            TextBox lblApprovedQty = (TextBox)g.FindControl("txtApprovedQty");
            TextBox lblRate = (TextBox)g.FindControl("txtRate");
            TextBox lblGST = (TextBox)g.FindControl("txtGST");
            Label lblGSTRate = (Label)g.FindControl("lblGSTRate");
            Label lblQtyVal = (Label)g.FindControl("lblQtyVal");
            Label lblGstValue = (Label)g.FindControl("lblGstValue");
            Label lblGrossValue = (Label)g.FindControl("lblGrossValue");
            DropDownList lblCostCenter = (DropDownList)g.FindControl("ddlCostCenter");
            DropDownList lblLocation = (DropDownList)g.FindControl("ddlLocation");
            TextBox lblproject = (TextBox)g.FindControl("txtproject");

            //select ItemSubHeadID, ItemSubHeadName from SET_ItemSubHead lblproject

            DataSet ds = dml.Find("select ItemSubHeadID, ItemSubHeadName from SET_ItemSubHead where ItemSubHeadName = '" + lblheadsub.Text + "'");
            string subhead, itemmaster, uom1, costcenter;
            if (ds.Tables[0].Rows.Count > 0)
            {
                subhead = ds.Tables[0].Rows[0]["ItemSubHeadID"].ToString();
            }
            else
            {
                subhead = "0";
            }
            DataSet ds1 = dml.Find("select  ItemID , Description from SET_ItemMaster where Description = '" + lblitemmaster.Text + "'");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                itemmaster = ds1.Tables[0].Rows[0]["ItemID"].ToString();
            }
            else
            {
                itemmaster = "0";
            }
            DataSet ds2 = dml.Find("select  UOMID,UOMName from SET_UnitofMeasure where UOMName = '" + lbluom.Text + "'");
            if (ds2.Tables[0].Rows.Count > 0)
            {
                uom1 = ds2.Tables[0].Rows[0]["UOMID"].ToString();
            }
            else
            {
                uom1 = "0";
            }

            DataSet ds3 = dml.Find("select  CostCenterID,CostCenterName from SET_CostCenter where CostCenterName = '" + lblCostCenter.SelectedItem.Text + "'");
            if (ds3.Tables[0].Rows.Count > 0)
            {
                costcenter = ds3.Tables[0].Rows[0]["CostCenterID"].ToString();
            }
            else
            {
                costcenter = "0";
            }


            //DataSet ds = dml.Find("select  * from Set_QuotationReqDetail where ItemSubHead = '" + lblheadsub.Text + "' and ItemMaster = '" + lblitemmaster.Text + "'");
            //if (ds.Tables[0].Rows.Count > 0)
            //{

            //}
            //else
            //{

            //  dml.Insert("INSERT INTO Set_QuotationReqDetail([QuoatReqMId], [ItemSubHead], [ItemMaster], [UOM], [StockQuantity], [Supplier], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted], [ReqQuantity]) VALUES ('" + masterid + "','" + lblheadsub.Text + "', '" + lblitemmaster.Text + "', '" + lbluom.Text + "', '" + lblcurrstock.Text + "', '" + lblSupp.Text + "', '1', '" + gocid() + "', '" + compid() + "', '" + branchId() + "', '" + FiscalYear() + "', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0' , '" + lblreqQ.Text + "');", "");
            //nechy
            dml.Insert("INSERT INTO SuppQuotApprDetail ([Sno_Master], [ItemSubHeadID], [ItemID], [UOMID], [Quantity], [Rate], [ApprovedQuantity], [GST], [Project], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted], [GSTRate], [GSTValue], [QtyValue], [GrossValue], [LocId], [BalanceQty]) VALUES ('" + masterid + "', '" + subhead + "', '" + itemmaster + "', '" + uom1 + "', " + lblQty.Text + ", '" + lblRate.Text + "', " + lblApprovedQty.Text + ", '" + lblGST.Text + "', '" + lblproject.Text + "', 1, " + gocid() + ", " + compid() + ", " + branchId() + "," + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0', '" + lblGSTRate.Text + "', '" + lblGstValue.Text + "', '" + lblQtyVal.Text + "', '" + lblGrossValue.Text + "', '" + lblLocation.SelectedItem.Value + "', NULL);", "");
            dml.Update("update Set_QuotationReqMaster set Status = '6' where QuotationReqNO = '" + requisno.Text + "'", "");


            //}
        }
    }
    public void insertq()
    {
        string chkDirectval = "";
        if (chkDirect.Checked == true)
        {
            chkDirectval = "1";
        }
        else
        {
            chkDirectval = "0";
        }
        DataSet uniqueg_B_C = dml.Find("select * from SuppQuotationApprMaster where DocType='" + ddlDocName.SelectedItem.Value + "' and DocNo = '" + txtDocNo.Text + "' and Record_Deleted = '0'");
        if (uniqueg_B_C.Tables[0].Rows.Count > 0)
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = "Duplicated entry not allowed";
        }
        else
        {


            int chk = 0, chks = 0;

            if (chkActive.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
             entrydate = txtEntryDate.Text;
            string Entrydate = dml.dateconvertString(entrydate);
            string deliverydate = dml.dateconvertforinsert(txtDeliveryDate);
            txtDocNo.Text = required_generatestr();
            string dc = detailcond();
            if (dc != "")
            {
                dml.Insert("INSERT INTO SuppQuotationApprMaster ( [DocId], [DocType], [DocAutority], [DocNo], [DirectQuotation], [AFQ_RequisitionNo], [Entrydate], [BPartnerTypeID], [DeliveryDate], [PaymentModeId], [SupplierReferenceNo], [DepartmentID], [DeliveryPeriod], [StatusId], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted]) VALUES ('" + ddlDocName.SelectedItem.Value + "', '" + ddlDocName.SelectedItem.Value + "', '" + ddlDocAuth.SelectedItem.Value + "', '" + required_generateins() + "', '" + chkDirectval + "', '" + ddlAFQ_ReqNo.SelectedItem.Text + "', '" + Entrydate + "', '" + ddlSupplier.SelectedItem.Value + "', '" + deliverydate + "', '" + ddlModeOFPayment.SelectedItem.Value + "', '" + txtSuppRefNo.Text + "', '" + ddlDepartment.SelectedItem.Value + "', '" + txtDeliverDuration.Text + "', '" + ddlStatus.SelectedItem.Value + "', '" + chk + "', " + gocid() + ", " + compid() + ", " + branchId() + ", " + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0'); select  Sno from SuppQuotationApprMaster where Sno = SCOPE_IDENTITY()", "");

                //dml.Insert("INSERT INTO [SuppQuotationApprMaster] ([DocId], [DocType], [DocAutority], [DocNo], [DirectQuotation], [AFQ_RequisitionNo], [Entrydate], [BPartnerTypeID], [DeliveryDate], [PaymentModeId], [SupplierReferenceNo], [DeliveryPeriod], [Status], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted]) VALUES ('1', '" + ddlDocTypes.SelectedItem.Text + "', '" + ddlDocAuth.SelectedItem.Value + "', '" + txtDocNo.Text + "', '" + chkDirectval + "', '" + ddlAFQ_ReqNo.SelectedItem.Value + "', '" + Entrydate + "', '" + ddlSupplier.SelectedItem.Text + "', '" + deliverydate + "',  '" + ddlModeOFPayment.SelectedItem.Value + "', '" + txtSuppRefNo.Text + "',  '" + txtDeliverDuration.Text + "', '" + ddlStatus.SelectedItem.Text + "', '" + chk + "', '" + gocid() + "', '" + compid() + "', '" + branchId() + "', '" + FiscalYear() + "', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0'); select  Sno from SuppQuotationApprMaster where Sno = SCOPE_IDENTITY()", "");
                string ids = "1";
                DataSet dsCh = dml.Find("select Sno from SuppQuotationApprMaster WHERE ([DocType]='" + ddlDocName.SelectedItem.Value + "') AND ([DocAutority]='" + ddlDocAuth.SelectedItem.Value + "') AND ([DocNo]='" + txtDocNo.Text + "') AND ([DirectQuotation]='" + chkDirectval + "') AND ([AFQ_RequisitionNo]='" + ddlAFQ_ReqNo.SelectedItem.Text + "') AND ([Entrydate]='" + Entrydate + "') AND ([BPartnerTypeID]='" + ddlSupplier.SelectedItem.Value + "') AND ([DeliveryDate]='" + deliverydate + "')  AND ([PaymentModeId]='" + ddlModeOFPayment.SelectedItem.Value + "') AND ([SupplierReferenceNo]='" + txtSuppRefNo.Text + "') AND ([DeliveryPeriod]='" + txtDeliverDuration.Text + "') AND ([StatusId]='" + ddlStatus.SelectedItem.Value + "') AND ([IsActive]='" + chk + "') AND ([GocID]=" + gocid() + ") AND ([CompId]=" + compid() + ") AND ([BranchId]=" + branchId() + ") AND ([FiscalYearID]=" + FiscalYear() + ") AND ([Record_Deleted]='0')");
                if (dsCh.Tables[0].Rows.Count > 0)
                {
                    ids = dsCh.Tables[0].Rows[0]["Sno"].ToString();
                }

                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);
                Label1.Text = "";
                ddlDocName.SelectedIndex = 0;
                txtEntryDate.Text = "";
                txtDocNo.Text = "";
                ddlDocAuth.SelectedIndex = 0;
                chkDirect.Checked = false;
                ddlAFQ_ReqNo.SelectedIndex = 0;
                txtDeliveryDate.Text = "";
                ddlSupplier.SelectedIndex = 0;
                ddlModeOFPayment.SelectedIndex = 0;
                txtDeliverDuration.Text = "";
                txtRemarks.Text = "";
                txtSuppRefNo.Text = "";

                ddlStatus.SelectedIndex = 0;
                chkActive.Checked = false;
                detailinsert(ids);




                txtCreateddate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                Div1.Visible = false;
            }
            else
            {
                Label1.Text = "Please entry altest 1 entry in detail table";
            }
        }
    }
    public void showdetailEdit(string qoutMid)
    {
        Div2.Visible = true;


        if (chkDirect.Checked == true)
        {
            string query = "select * from View_ShowdetailSupplierQuotAppr where Sno_Master = '" + qoutMid + "'";
            DataSet ds = dml.grid(query);

            if (ds.Tables[0].Rows.Count > 0)
            {

                GridView4.DataSource = ds;
                GridView4.DataBind();
                for (int a = 0; a < ds.Tables[0].Rows.Count; a++)
                {
                    ListBox lstbox = (ListBox)GridView4.Rows[a].FindControl("ListBox1");

                    ListItem items = new ListItem();
                    items.Text = ds.Tables[0].Rows[a]["Supplier"].ToString();
                    //lstbox.Items.Add(items);


                }
            }
            else
            {
                GridView5.ShowHeaderWhenEmpty = true;
                GridView5.EmptyDataText = "NO RECORD";
                GridView5.DataBind();
            }
        }
        else {
            //select * from View_SuppApproved
            string query = "select * from View_ShowdetailSupplierQuotAppr where Sno_Master = '" + qoutMid + "'";
            DataSet ds = dml.grid(query);

            if (ds.Tables[0].Rows.Count > 0)
            {

                GridView4.DataSource = ds;
                GridView4.DataBind();
                for (int a = 0; a < ds.Tables[0].Rows.Count; a++)
                {
                    ListBox lstbox = (ListBox)GridView4.Rows[a].FindControl("ListBox1");

                    ListItem items = new ListItem();
                    items.Text = ds.Tables[0].Rows[a]["Supplier"].ToString();
                    //lstbox.Items.Add(items);


                }
            }
            else
            {
                GridView5.ShowHeaderWhenEmpty = true;
                GridView5.EmptyDataText = "NO RECORD";
                GridView5.DataBind();
            }
        }
    }
    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lblRowNumber") as Label).Text = (e.Row.RowIndex + 1).ToString();

        }
    }
    protected void GridView4_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView4.EditIndex = e.NewEditIndex;

        DataSet ds_detail = dml.Find("select * from V_SuppAQ_Update where Sno_Master = '" + ViewState["SNO"].ToString() + "'");
        if (ds_detail.Tables[0].Rows.Count > 0)
        {
            Div2.Visible = true;
            GridView4.DataSource = ds_detail.Tables[0];
            GridView4.DataBind();
        }
        DropDownList ddlitemsub = GridView4.Rows[GridView4.EditIndex].FindControl("ddlitemsubEDIT") as DropDownList;
        dml.dropdownsql(ddlitemsub, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");
        Label lbl1 = GridView4.Rows[GridView4.EditIndex].FindControl("lblsubhead") as Label;

        DropDownList ddlitemmaster = GridView4.Rows[GridView4.EditIndex].FindControl("ddlItemmter") as DropDownList;
        dml.dropdownsql(ddlitemmaster, "SET_ItemMaster", "Description", "ItemID");
        Label lbl2 = GridView4.Rows[GridView4.EditIndex].FindControl("lblitemmaster") as Label;

        ddlitemmaster.ClearSelection();
        ddlitemmaster.Items.FindByText(lbl2.Text).Selected = true;
        lbl2.Visible = false;

        DropDownList uddluom = GridView4.Rows[GridView4.EditIndex].FindControl("ddlUOMs") as DropDownList;
        // dml.dropdownsql(uddluom, "SET_UnitofMeasure", "UOMName", "UOMID");
        dml.dropdownsqlwithquery(uddluom, "select UOMID,UOMName from SET_UnitofMeasure where UOMID in ((SELECT UOMId as uom FROM SET_ItemMaster where ItemID = '" + ddlitemmaster.SelectedItem.Value + "' ) UNION (SELECT UOMId2 as uom FROM SET_ItemMaster where ItemID = '" + ddlitemmaster.SelectedItem.Value + "' ))", "UOMName", "UOMID");
        Label lbl3 = GridView4.Rows[GridView4.EditIndex].FindControl("lbluom") as Label;

        DropDownList ddlcc = GridView4.Rows[GridView4.EditIndex].FindControl("ddlLocation") as DropDownList;
        dml.dropdownsql(ddlcc, "Set_Location", "LocName", "LocId");
        
        Label lblcc = GridView4.Rows[GridView4.EditIndex].FindControl("lblloc") as Label;

        ddlitemsub.ClearSelection();
        ddlitemsub.Items.FindByText(lbl1.Text).Selected = true;
        lbl1.Visible = false;

        uddluom.ClearSelection();
        uddluom.Items.FindByText(lbl3.Text).Selected = true;
        lbl3.Visible = false;

        ddlcc.ClearSelection();
        ddlcc.Items.FindByText(lblcc.Text).Selected = true;
        lblcc.Visible = false;
    }
    protected void GridView4_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {

            DropDownList ddlitemsubedit = (GridView4.Rows[e.RowIndex].FindControl("ddlitemsubEDIT") as DropDownList);



            DropDownList ddlitemMasteredit = (GridView4.Rows[e.RowIndex].FindControl("ddlItemmter") as DropDownList);
            DropDownList ddluomedit = (GridView4.Rows[e.RowIndex].FindControl("ddlUOMs") as DropDownList);
            string txtQtyEdit = (GridView4.Rows[e.RowIndex].FindControl("txtQty") as Label).Text;
            string txtApprovedQtyEdit = (GridView4.Rows[e.RowIndex].FindControl("txtApprovedQty") as TextBox).Text;
            string txtRateEdit = (GridView4.Rows[e.RowIndex].FindControl("txtRate") as TextBox).Text;
            //string lblRate = (GridView4.Rows[e.RowIndex].FindControl("lblRate") as Label).Text;
            string lblGST = (GridView4.Rows[e.RowIndex].FindControl("txtGSTRate") as Label).Text;
            string txtGSTRate = (GridView4.Rows[e.RowIndex].FindControl("lblGST") as Label).Text;
            string txtGrossValue = (GridView4.Rows[e.RowIndex].FindControl("txtGrossValue") as Label).Text;
            DropDownList ddlLocation = (GridView4.Rows[e.RowIndex].FindControl("ddlLocation") as DropDownList);
            string txtproject = (GridView4.Rows[e.RowIndex].FindControl("txtproject") as TextBox).Text;
            string lblsno = (GridView4.Rows[e.RowIndex].FindControl("lblsno") as Label).Text;
            //dt.Columns.AddRange(new DataColumn[6] 

            //ddlSupplierEdit, ddlUomEdit, ddlItemMasterEdit
            GridViewRow row = GridView4.Rows[e.RowIndex];
            DataTable dt = (DataTable)ViewState["dtup"];

            dt.Rows[row.DataItemIndex]["ItemSubHeadName"] = ddlitemsubedit.SelectedItem.Text;
            dt.Rows[row.DataItemIndex]["Description"] = ddlitemMasteredit.SelectedItem.Text;
            dt.Rows[row.DataItemIndex]["UOMName"] = ddluomedit.SelectedItem.Text;
            dt.Rows[row.DataItemIndex]["Quantity"] = txtQtyEdit;
            dt.Rows[row.DataItemIndex]["ApprovedQuantity"] = txtApprovedQtyEdit;
            dt.Rows[row.DataItemIndex]["Rate"] = txtRateEdit;
            dt.Rows[row.DataItemIndex]["GST"] = float.Parse(lblGST);
            dt.Rows[row.DataItemIndex]["GSTRate"] = txtGSTRate;
            dt.Rows[row.DataItemIndex]["GrossValue"] = txtGrossValue;
            dt.Rows[row.DataItemIndex]["LocName"] = ddlLocation.SelectedItem.Text;
            dt.Rows[row.DataItemIndex]["Project"] = txtproject;



            //dml.Update("UPDATE [SET_StockRequisitionDetail] SET [ItemSubHead]='" + txtitemsubfooter.SelectedItem.Value + "', [ItemMaster]='" + txtdesc.SelectedItem.Value + "', [CostCenter]='" + txtsupplierFooter.SelectedItem.Value + "', [UOM]='" + txtuomFooter.SelectedItem.Value + "', [Project]='" + txtProjectEdit + "', [CurrentStock]='" + txtcurrStockFooter + "', [RequiredQuantity]='" + txtReqStockFooter + "', [Remarks]=NULL, [IsActive]='1', [GocID]='" + gocid() + "', [CompId]='" + compid() + "', [BranchId]='" + branchId() + "', [FiscalYearID]='" + FiscalYear() + "', [UpdatedBy]='" + show_username() + "', [UpdatedDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', [Record_Deleted]='0' WHERE ([Sno]='" + lblsno + "');", "");
            dml.Update("UPDATE  SuppQuotApprDetail SET  [ItemSubHeadID]='"+ddlitemsubedit.SelectedItem.Value+"', [ItemID]='"+ddlitemMasteredit.SelectedItem.Value+"', [UOMID]='"+ddluomedit.SelectedItem.Value+"', [Quantity]='"+txtQtyEdit+"', [Rate]='"+txtRateEdit+"', [ApprovedQuantity]='"+txtApprovedQtyEdit+"', [GST]='"+lblGST+"', [GrossRate]=NULL, [Project]='"+txtproject+"', [IsActive]='1',[UpdatedBy]='"+show_username()+"', [UpdatedDate]='"+DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")+"', [GSTRate]='"+txtGSTRate+"', [GrossValue]='"+txtGrossValue+"', [LocId]='"+ddlLocation.SelectedItem.Value+ "'  WHERE ([Sno]='"+lblsno+"') ", "");
            GridView4.EditIndex = -1;

            //fl = true;
            PopulateGridview_Up();


        }

        catch (Exception ex)
        {
            // lblSuccessMessage.Text = "";
            // lblErrorMessage.Text = ex.Message;
        }
    }
    //txtApprovedQtygd4_TextChanged
    protected void txtApprovedQtygd4_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;

        string rate = (row.FindControl("txtRate") as TextBox).Text;
        string gstPer = (row.FindControl("lblGST") as Label).Text;
        string appQty = (row.FindControl("txtApprovedQty") as TextBox).Text;


        float gstrate = 0;
        if (rate == "")
        {
            gstrate = 0 * (0 / 100);
        }
        else
        {
            if (gstPer != "")
            {
                gstrate = float.Parse(rate) * (float.Parse(gstPer) / 100);
            }
        }
           (row.FindControl("txtGSTRate") as Label).Text = gstrate.ToString();
        float qtyval = 0, gstval = 0;
        if (rate != "")
        {
            qtyval = float.Parse(appQty) * float.Parse(rate);
            gstval = float.Parse(appQty) * gstrate;
        }
       //lblGrossValue
      // (row.FindControl("lblQtyVal") as Label).Text = qtyval.ToString();

      //  (row.FindControl("lblGstValue") as Label).Text = gstval.ToString();
        float grossval = qtyval + gstval;

        (row.FindControl("txtGrossValue") as Label).Text = grossval.ToString();

    }
    protected void txtRateQtygd4_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;

        string rate = (row.FindControl("txtRate") as TextBox).Text;
        string gstPer = (row.FindControl("lblGST") as Label).Text;
        string appQty = (row.FindControl("txtApprovedQty") as TextBox).Text;


        float gstrate = 0;
        if (rate == "")
        {
            gstrate = 0 * (0 / 100);
        }
        else
        {
            if (gstPer != "")
            {
                gstrate = float.Parse(rate) * (float.Parse(gstPer) / 100);
            }
        }
           (row.FindControl("txtGSTRate") as Label).Text = gstrate.ToString();
        float qtyval = 0, gstval = 0;
        if (rate != "")
        {
            qtyval = float.Parse(appQty) * float.Parse(rate);
            gstval = float.Parse(appQty) * gstrate;
        }
        //lblGrossValue
        // (row.FindControl("lblQtyVal") as Label).Text = qtyval.ToString();

        //  (row.FindControl("lblGstValue") as Label).Text = gstval.ToString();
        float grossval = qtyval + gstval;

        (row.FindControl("txtGrossValue") as Label).Text = grossval.ToString();

    }
    public void PopulateGridview_Up()
    {

        DataTable dtbl = (DataTable)ViewState["dtup"];

        if (dtbl.Rows.Count > 0)
        {

            GridView4.DataSource = (DataTable)ViewState["dtup"];
            GridView4.DataBind();

        }
        else
        {


            dtbl.Rows.Add(dtbl.NewRow());
            GridView4.DataSource = dtbl;
            GridView4.DataBind();

            GridView4.Rows[0].Cells.Clear();
            GridView4.Rows[0].Cells.Add(new TableCell());
            GridView4.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
            GridView4.Rows[0].Cells[0].Text = "No Data Found ..!";
            GridView4.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;

        }
    }
    protected void GridView4_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView4.EditIndex = -1;

        //DataSet ds_detail = dml.Find("select * from view_StockUpdate where StockReqMId = '" + ViewState["SNO"].ToString() + "'");
        //if (ds_detail.Tables[0].Rows.Count > 0)
        //{
        //    Div1.Visible = true;
        //    GridView5.DataSource = ds_detail.Tables[0];
        //    GridView5.DataBind();
        //}
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string status = e.Row.Cells[5].Text;
        if (status == "Open")
        {

        }
        else
        {
            // TextBox txtCountry = (e.Row.FindControl("txtCountry") as TextBox);
            //  txtCountry.Enabled = false;
            e.Row.Enabled = false;
            e.Row.BackColor = System.Drawing.Color.LightGray;

        }
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string status = e.Row.Cells[5].Text;
        if (status == "Open")
        {

        }
        else
        {
            // TextBox txtCountry = (e.Row.FindControl("txtCountry") as TextBox);
            //  txtCountry.Enabled = false;
            e.Row.Enabled = false;
            e.Row.BackColor = System.Drawing.Color.LightGray;

        }
    }
    protected void txtApprovedQty_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;

        string rate = (row.FindControl("txtRate") as TextBox).Text;
        string gstPer = (row.FindControl("txtGST") as TextBox).Text;
        string appQty = (row.FindControl("txtApprovedQty") as TextBox).Text;


        float gstrate = 0;
        if (rate == "")
        {
            gstrate = 0 * (0 / 100);
        }
        else
        {
            if (gstPer != "")
            {
                gstrate = float.Parse(rate) * (float.Parse(gstPer) / 100);
            }
        }
            (row.FindControl("lblGSTRate") as Label).Text = gstrate.ToString();
        float qtyval = 0, gstval = 0;
        if (rate != "")
        {
             qtyval = float.Parse(appQty) * float.Parse(rate);
             gstval = float.Parse(appQty) * gstrate;
        }
        //lblGrossValue
        (row.FindControl("lblQtyVal") as Label).Text = qtyval.ToString();

        (row.FindControl("lblGstValue") as Label).Text = gstval.ToString();
        float grossval = qtyval + gstval;

        (row.FindControl("lblGrossValue") as Label).Text = grossval.ToString();

    }
    protected void txtEntryDate_TextChanged(object sender, EventArgs e)
    {
      
   //     required_generate();
    }
    public string startdate(string fy)
    {
        string sdate = "";


        DataSet ds = dml.Find("select StartDate,EndDate from SET_Fiscal_Year where Description = '" + fy + "'");
        if (ds.Tables[0].Rows.Count > 0)
        {
            sdate = ds.Tables[0].Rows[0]["StartDate"].ToString();

        }

        return sdate;

    }
    public string Enddate(string fy)
    {

        string Edate = "";


        DataSet ds = dml.Find("select StartDate,EndDate from SET_Fiscal_Year where Description = '" + fy + "'");
        if (ds.Tables[0].Rows.Count > 0)
        {

            Edate = ds.Tables[0].Rows[0]["EndDate"].ToString();
        }

        return Edate;

    }
}
