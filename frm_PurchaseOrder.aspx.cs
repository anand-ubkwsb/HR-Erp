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
    Utilities utl = new Utilities();
    DateTime DeliveryDate, TEntryDate;
    DataSet dsCh, FiscalDataSet;
    string userid, UserGrpID, FormID, fiscal, menuid, FiscalId;
    int valid, showd, DmlAllowedDays, DateFrom, AddDays, EditDays, DeleteDays;
    string[] supplier = new string[4];
    Conditions OperationConditions = new Conditions();
    DmlOperation dml = new DmlOperation();
    radcomboxclass cmb = new radcomboxclass();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
    FieldHide fd = new FieldHide();
    Nullable<int> Supplier = null, DocAuth = null, DocName = null, Status = null, billToDept = null, FCurrency = null, shipVia = null, ModeOfPay = null, FreightTerms = null, SupplyRefNo = null, TotalLocalCurVal = null;
    Nullable<Double> AdvaceAgaPo = null, FCRate = null, TotalFCVal = null;
    string EntryDate = "", Remark = "", Delivery = "", DeliDueDay = "", PoDoc = "";
    string itemtype, itemhead, itemsubhead;
    float i;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {

            FormID = Request.QueryString["FormID"];
            userid = Request.QueryString["UserID"];
            FiscalId = Request.QueryString["Fiscal"];
            //FiscalDataSet=dml.Find("Select * From Set_Fiscal_Year Where FiscalYearId='" + FiscalId + "' AND RECORD_DELETED='0'");
            ViewState["flag"] = "true";
            ViewState["FormId"] = FormID;
            ViewState["userid"] = userid;
            //btnUpdate.Visible = false;
            btnSave.Visible = false;
            btnDelete_after.Visible = false;
            btnUpdatePO.Visible = false;
            btnUpdate.Visible = false;
            UserGrpID = Request.QueryString["UsergrpID"];
            fiscal = Request.QueryString["fiscaly"];
            menuid = Request.QueryString["Menuid"];

            Showall_Dml();
            txtEntryDate.Attributes.Add("readonly", "readonly");
            txtDeliveryDate.Attributes.Add("readonly", "readonly");
            string StartDate = fiscalstart(FiscalId);
            ViewState["StartDate"] = StartDate;
            string EndDate = FiscalEnd(FiscalId);
            ViewState["EndDate"] = EndDate;
            OperationConditions.SetStartAndEndDate(CalendarExtender1, CalendarExtender3, StartDate, EndDate, DateFrom);
            txtEntryDate.Text = CalendarExtender1.StartDate.ToString();

            dml.dropdownsql(ddlbilltodeprt, "SET_Department", "DepartmentName", "DepartmentID");
            //dml.dropdownsqlwithquery(ddlPODocName, "select SET_Documents.DocID as docs , * from SET_DocumentType LEFT JOIN SET_Documents on SET_DocumentType.DocTypeId = SET_Documents.DocTypeId where SET_DocumentType.DocTypeId in( select DocTypeId from SET_Documents where MenuId_Sno = '"+menuid+"' and FormId_Sno = '"+FormID+"')", "DocName", "docs");
            string sdate = startdate(fiscal);
            string enddate = Enddate(fiscal);

            dml.dropdownsqlwithquery(ddlPODocName, "select DocID,DocDescription from SET_Documents where docid in (select DocID from SET_UserGrp_Documents where UserGrpId = '" + UserGrpID + "' AND	(( StartDate <= '" + sdate + "') AND ((EndDate >= '" + enddate + "') OR (EndDate IS NULL))) and Record_Deleted = 0) and CompID = '" + compid() + "' and Record_Deleted = 0 and MenuId_Sno = '" + menuid + "' and FormId_Sno='" + FormID + "'", "DocDescription", "DocID");

            dml.dropdownsql(ddlSupplier, "ViewSupplierId", "BPartnerName", "BPartnerId");
            dml.dropdownsql(ddlStatus, "SET_Status", "StatusName", "StatusId");
            //dml.dropdownsql(ddlFreightTerms, "ViewSupplierId", "BPartnerName", "Sno");
            dml.dropdownsql(ddlmodeofpay, "Set_PaymentMode", "PaymentModeDescription", "PaymentModeId");
            dml.dropdownsql(ddlFCurrency, "SET_Currency", "CurrencyName", "CurrencyID");
            dml.dropdownsql(ddlDocAuth, "SET_Authority", "AuthorityName", "AuthorityId");

            //ddlEdit_Document

            dml.dropdownsqlwithquery(ddlEdit_Document, "select SET_Documents.DocID as docs , * from SET_DocumentType LEFT JOIN SET_Documents on SET_DocumentType.DocTypeId = SET_Documents.DocTypeId where SET_DocumentType.DocTypeId in( select DocTypeId from SET_Documents where MenuId_Sno = '" + menuid + "' and FormId_Sno = '" + FormID + "')", "DocName", "docs");
            dml.dropdownsqlwithquery(ddlDel_Document, "select SET_Documents.DocID as docs , * from SET_DocumentType LEFT JOIN SET_Documents on SET_DocumentType.DocTypeId = SET_Documents.DocTypeId where SET_DocumentType.DocTypeId in( select DocTypeId from SET_Documents where MenuId_Sno = '" + menuid + "' and FormId_Sno = '" + FormID + "')", "DocName", "docs");
            dml.dropdownsqlwithquery(ddlFind_Document, "select SET_Documents.DocID as docs , * from SET_DocumentType LEFT JOIN SET_Documents on SET_DocumentType.DocTypeId = SET_Documents.DocTypeId where SET_DocumentType.DocTypeId in( select DocTypeId from SET_Documents where MenuId_Sno = '" + menuid + "' and FormId_Sno = '" + FormID + "')", "DocName", "docs");
            // select[DocumentNo.] Sno from Set_PurcahseOrderMaster

            dml.dropdownsqlwithquery(ddlEdit_DocNO, "select [DocumentNo.] as DocNo, Sno from Set_PurcahseOrderMaster", "DocNo", "Sno");
            dml.dropdownsqlwithquery(ddlFind_DocNO, "select [DocumentNo.] as DocNo, Sno from Set_PurcahseOrderMaster", "DocNo", "Sno");
            dml.dropdownsqlwithquery(ddlDel_DocNO, "select [DocumentNo.] as DocNo, Sno from Set_PurcahseOrderMaster", "DocNo", "Sno");


            dml.dropdownsql(ddlFind_Status, "SET_Status", "StatusName", "StatusId");
            dml.dropdownsql(ddlEdit_Status, "SET_Status", "StatusName", "StatusId");
            dml.dropdownsql(ddlDel_Status, "SET_Status", "StatusName", "StatusId");


            dml.dropdownsql(ddlFind_Department, "SET_Department", "DepartmentName", "DepartmentID");
            dml.dropdownsql(ddlEdit_Depart, "SET_Department", "DepartmentName", "DepartmentID");
            dml.dropdownsql(ddlDel_Department, "SET_Department", "DepartmentName", "DepartmentID");

            dml.dropdownsql(ddlFCRate, "SET_CurrencyConversion", "Rate", "SerialNo");

            dml.dropdownsqlwithquery(ddlFreightTerms, "select Sno, FreightTerm + ' | ' + FreightDescription as frTName from SET_FreightTerms", "frTName", "Sno");

            dml.dropdownsql(ddlShipVia, "SET_ShipmentType", "ShipmentType", "Sno");
            textClear();
            Div1.Visible = false;
            Div2.Visible = false;
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
        btnSave.Visible = true;
        btnEdit.Visible = false;
        btnFind.Visible = false;
        btnCancel.Visible = true;
        btnDelete.Visible = false;
        btnInsert.Visible = false;
        btnUpdate.Visible = false;
        ddlPODocName.Enabled = true;
        doctype(menuid, FormID, UserGrpID);
        ddlPODocName_SelectedIndexChanged(sender, e);
        if (DateFrom != 0)
        {
            txtEntryDate.Text = Convert.ToString(DateTime.Now.AddDays(0 - DateFrom));
        }
        else {
            txtEntryDate.Text = Convert.ToString(ViewState["EndDate"]);
            //txtEntryDate.Text = CalendarExtender1.StartDate.ToString();
        }
    }

    protected void btnUpdatePO_Click(object sender, EventArgs e)
    {
        string deliduedays = "0", forceclose = "0", entrydate = "1900-01-01";

        DataSet ds = dml.Find("select DeliveryDueDays,forceclosed,EntryDate from Set_PurcahseOrderMaster where Sno = '" + ViewState["SNO"].ToString() + "';");
        if (ds.Tables[0].Rows.Count > 0)
        {
            deliduedays = ds.Tables[0].Rows[0]["DeliveryDueDays"].ToString();
            forceclose = ds.Tables[0].Rows[0]["forceclosed"].ToString();
            entrydate = ds.Tables[0].Rows[0]["EntryDate"].ToString();
        }
        if (string.IsNullOrEmpty(deliduedays))
        {
            deliduedays = "0";
        }
        if (string.IsNullOrEmpty(forceclose))
        {
            forceclose = "0";
        }

        DateTime date = DateTime.Parse(entrydate);
        int days = Convert.ToInt32(deliduedays) + Convert.ToInt32(forceclose);
        string newdate = date.AddDays(days).ToString("yyyy-mm-dd");


        string nowdate = DateTime.Now.ToString("yyyy-mm-dd");
        if (nowdate == newdate)
        {
            if (chkDirect.Checked == false && chkPRNo.Checked == false)
            {
                dml.Update("UPDATE SuppQuotationApprMaster set StatusId = '2' where  Sno = '" + ViewState["SNO"].ToString() + "' ", "");
                Label1.Text = "Force Closed has been applied";
            }
            if (chkPRNo.Checked == true)
            {
                dml.Update("UPDATE Set_QuotationReqMaster set Status = '2' where Sno = '" + ViewState["SNO"].ToString() + "'", "");
                Label1.Text = "Force Closed has been applied";
            }
            if (chkDirect.Checked == true)
            {
                dml.Update("UPDATE Set_PurcahseOrderMaster set Status = '2' where Sno = '" + ViewState["SNO"].ToString() + "'", "");
                Label1.Text = "Force Closed has been applied";
            }

        }
        else
        {
            Label1.Text = "Force closed PO have no due date close value";
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (chkDirect.Checked == false && chkPRNo.Checked == false)
        {
            string dc = detailcond6();
            string direct_po = "0";
            string chk = "0";
            string ids = "0";
            if (chkDirect.Checked == true)
            {
                direct_po = "1";
            }
            if (chkActive.Checked == true)
            {
                chk = "1";
            }

            if (lstFruits.Items.Count > 0)
            {


                if (dc != "" && dc == "No Data")
                {
                    if (Convert.ToDateTime(txtDeliveryDate.Text) >= Convert.ToDateTime(txtEntryDate.Text))
                    {

                        if (ddlPODocName.SelectedItem.Value == "Please select...")
                        { DocName = 0; }
                        else { DocName = Convert.ToInt32(ddlPODocName.SelectedItem.Value.ToString()); }

                        if (ddlSupplier.SelectedItem.Value == "Please select...")
                        { Supplier = 0; }
                        else { Supplier = Convert.ToInt32(ddlSupplier.SelectedValue.ToString()); }

                        if (ddlDocAuth.SelectedItem.Value == "Please select...")
                        { DocAuth = 0; }
                        else {
                            DocAuth = Convert.ToInt32(ddlDocAuth.SelectedItem.Value.ToString());
                        }

                        if (ddlbilltodeprt.SelectedItem.Value == "Please select...")
                        { billToDept = 0; }
                        else {
                            billToDept = Convert.ToInt32(ddlDocAuth.SelectedItem.Value.ToString());
                        }

                        if (ddlShipVia.SelectedItem.Value == "Please select...")
                        { shipVia = 0; }
                        else {
                            shipVia = Convert.ToInt32(ddlShipVia.SelectedItem.Value.ToString());
                        }

                        if (ddlmodeofpay.SelectedItem.Value == "Please select...")
                        { ModeOfPay = 0; }
                        else {
                            ModeOfPay = Convert.ToInt32(ddlmodeofpay.SelectedItem.Value.ToString());
                        }

                        if (ddlFreightTerms.SelectedItem.Value == "Please select...")
                        { FreightTerms = 0; }
                        else {
                            FreightTerms = Convert.ToInt32(ddlFreightTerms.SelectedItem.Value.ToString());
                        }

                        if (ddlFCurrency.SelectedItem.Value == "Please select...")
                        { FCurrency = 0; }
                        else {
                            FCurrency = Convert.ToInt32(ddlFCurrency.SelectedItem.Value.ToString());
                        }

                        if (ddlFCRate.SelectedItem.Value == "Please select...")
                        { FCRate = 0; }
                        else {
                            FCRate = Convert.ToDouble(ddlFCRate.SelectedItem.Value.ToString());
                        }
                        if (ddlStatus.SelectedItem.Value == "Please select...")
                        { Status = 0; }
                        else {
                            Status = Convert.ToInt32(ddlStatus.SelectedItem.Value.ToString());
                        }
                        if (string.IsNullOrEmpty(txtEntryDate.Text.ToString()))
                        { EntryDate = "NULL"; }
                        else { EntryDate = txtEntryDate.Text.ToString(); }

                        if (string.IsNullOrEmpty(txtTotalFCVal.Text.ToString()))
                        { TotalFCVal = 0; }
                        else { TotalFCVal = Convert.ToDouble(txtTotalFCVal.Text.ToString()); }


                        if (string.IsNullOrEmpty(txtTotalLocalCurVal.Text.ToString()))
                        { TotalLocalCurVal = 0; }
                        else { TotalLocalCurVal = Convert.ToInt32(txtTotalLocalCurVal.Text.ToString()); }

                        if (string.IsNullOrEmpty(txtRemarks.Text.ToString()))
                        { Remark = "NULL"; }
                        else { Remark = txtRemarks.Text.ToString(); }

                        if (string.IsNullOrEmpty(txtDeliveryDate.Text.ToString()))
                        { Delivery = "NULL"; }
                        else { Delivery = txtDeliveryDate.Text.ToString(); }

                        if (string.IsNullOrEmpty(txtDeliDuedays.Text.ToString()))
                        { DeliDueDay = "NULL"; }
                        else { DeliDueDay = txtDeliDuedays.Text.ToString(); }

                        if (string.IsNullOrEmpty(txtAdvaceAgaPO.Text.ToString()))
                        { AdvaceAgaPo = 0; }
                        else { AdvaceAgaPo = Convert.ToDouble(txtAdvaceAgaPO.Text.ToString()); }
                        if (!string.IsNullOrEmpty(txtSuppRefNo.Text.ToString()))
                        {
                            SupplyRefNo = Convert.ToInt32(txtSuppRefNo.Text.ToString());
                        }
                        PoDoc = required_generateforIns();
                        if (string.IsNullOrEmpty(PoDoc))
                        {
                            PoDoc = "NULL";
                        }


                        string Query = "INSERT INTO Set_PurcahseOrderMaster ([DocId],[DocType],[EntryDate],[DocumentNo.],[BPartnerTypeID],[DirectPO_PRNo],[DocumentAuthority],[PurReqNo_AQNo],[BillingLocation],[ShipmentVia],[ModeofPayment],[FreightTerms],[ForeignCurrency],[FcRate],[TotalFcValue],[TotalLocalCurrencyValue],[Remarks],[DeliveryDueDate],[DeliveryDueDays],[AdvanceAgainstPO%],[Status],[IsActive],[GocID],[CompId],[BranchId],[FiscalYearID],[CreatedBy],[CreatedDate],[Record_Deleted],[SuppRefNo],[forceclosed],[Carry_Fwd],[ReferNo],[Brough_Fwd]) VALUES " + "(" + DocName + ",null,'" + EntryDate + "', '" + PoDoc + "'," + Supplier + ",'" + direct_po + "'," + DocAuth + ",'" + lstFruits.SelectedItem.Text.ToString() + "'," + billToDept + "," + shipVia + "," + ModeOfPay + "," + FreightTerms + "," + FCurrency + "," + FCRate + "," + TotalFCVal + ",'" + TotalLocalCurVal + "','" + Remark + "','" + Delivery + "','" + DeliDueDay + "'," + AdvaceAgaPo + ", " + Status + ",'" + chk + "'," + gocid() + "," + compid() + "," + branchId() + "," + FiscalYear() + ",'" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "'," + SupplyRefNo + ",'0','0'," + 0 + "," + 0 + ",'0'); SELECT * FROM Set_PurcahseOrderMaster WHERE Sno = SCOPE_IDENTITY()";
                        Label1.Text = "";
                        dsCh = dml.Find(Query);
                    }
                    else
                    {
                        Label1.Text = "Delivery Date can not be before current date";
                    }

                    if (dsCh.Tables[0].Rows.Count > 0)
                    {
                        ids = dsCh.Tables[0].Rows[0]["Sno"].ToString();
                        ViewState["detailid"] = ids;
                    }
                    detaisaveforAFQ(ids);
                    textClear();
                    btnInsert_Click(sender, e);
                    ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);


                }
                else
                {
                    Label1.Text = "Please entry altest 1 entry in detail table";
                }

            }
            else
            {
                Label1.Text = "Entry not allowed. There is no Ask for quotation No.";
            }
        }
        else
        {
            string direct_po = "0";
            string chk = "0";
            string ids = "0";
            if (chkDirect.Checked == true)
            {
                direct_po = "1";
            }
            if (chkActive.Checked == true)
            {
                chk = "1";
            }


            if (chkDirect.Checked == true)
            {
                string dc = detailcond();
                if (dc != "" && dc != "No Data")
                {
                    if (Convert.ToDateTime(txtDeliveryDate.Text) >= Convert.ToDateTime(txtEntryDate.Text))
                    {
                        if (ddlPODocName.SelectedItem.Value == "Please select...")
                        { DocName = 0; }
                        else { DocName = Convert.ToInt32(ddlPODocName.SelectedItem.Value.ToString()); }

                        if (ddlSupplier.SelectedItem.Value == "Please select...")
                        { Supplier = 0; }
                        else { Supplier = Convert.ToInt32(ddlSupplier.SelectedValue.ToString()); }

                        if (ddlDocAuth.SelectedItem.Value == "Please select...")
                        { DocAuth = 0; }
                        else {
                            DocAuth = Convert.ToInt32(ddlDocAuth.SelectedItem.Value.ToString());
                        }

                        if (ddlbilltodeprt.SelectedItem.Value == "Please select...")
                        { billToDept = 0; }
                        else {
                            billToDept = Convert.ToInt32(ddlDocAuth.SelectedItem.Value.ToString());
                        }

                        if (ddlShipVia.SelectedItem.Value == "Please select...")
                        { shipVia = 0; }
                        else {
                            shipVia = Convert.ToInt32(ddlShipVia.SelectedItem.Value.ToString());
                        }

                        if (ddlmodeofpay.SelectedItem.Value == "Please select...")
                        { ModeOfPay = 0; }
                        else {
                            ModeOfPay = Convert.ToInt32(ddlmodeofpay.SelectedItem.Value.ToString());
                        }

                        if (ddlFreightTerms.SelectedItem.Value == "Please select...")
                        { FreightTerms = 0; }
                        else {
                            FreightTerms = Convert.ToInt32(ddlFreightTerms.SelectedItem.Value.ToString());
                        }

                        if (ddlFCurrency.SelectedItem.Value == "Please select...")
                        { FCurrency = 0; }
                        else {
                            FCurrency = Convert.ToInt32(ddlFCurrency.SelectedItem.Value.ToString());
                        }

                        if (ddlFCRate.SelectedItem.Value == "Please select...")
                        { FCRate = 0; }
                        else {
                            FCRate = Convert.ToDouble(ddlFCRate.SelectedItem.Value.ToString());
                        }
                        if (ddlStatus.SelectedItem.Value == "Please select...")
                        { Status = 0; }
                        else {
                            Status = Convert.ToInt32(ddlStatus.SelectedItem.Value.ToString());
                        }
                        if (string.IsNullOrEmpty(txtEntryDate.Text.ToString()))
                        { EntryDate = "NULL"; }
                        else { EntryDate = txtEntryDate.Text.ToString(); }

                        if (string.IsNullOrEmpty(txtTotalFCVal.Text.ToString()))
                        { TotalFCVal = 0; }
                        else { TotalFCVal = Convert.ToDouble(txtTotalFCVal.Text.ToString()); }


                        if (string.IsNullOrEmpty(txtTotalLocalCurVal.Text.ToString()))
                        { TotalLocalCurVal = 0; }
                        else { TotalLocalCurVal = Convert.ToInt32(txtTotalLocalCurVal.Text.ToString()); }

                        if (string.IsNullOrEmpty(txtRemarks.Text.ToString()))
                        { Remark = "NULL"; }
                        else { Remark = txtRemarks.Text.ToString(); }

                        if (string.IsNullOrEmpty(txtDeliveryDate.Text.ToString()))
                        { Delivery = "NULL"; }
                        else { Delivery = txtDeliveryDate.Text.ToString(); }

                        if (string.IsNullOrEmpty(txtDeliDuedays.Text.ToString()))
                        { DeliDueDay = "NULL"; }
                        else { DeliDueDay = txtDeliDuedays.Text.ToString(); }

                        if (string.IsNullOrEmpty(txtAdvaceAgaPO.Text.ToString()))
                        { AdvaceAgaPo = 0; }
                        else { AdvaceAgaPo = Convert.ToDouble(txtAdvaceAgaPO.Text.ToString()); }
                        if (!string.IsNullOrEmpty(txtSuppRefNo.Text.ToString()))
                        {
                            SupplyRefNo = Convert.ToInt32(txtSuppRefNo.Text.ToString());
                        }
                        PoDoc = required_generateforIns();
                        if (string.IsNullOrEmpty(PoDoc))
                        {
                            PoDoc = "NULL";
                        }
                        string Query = "INSERT INTO Set_PurcahseOrderMaster ([DocId],[DocType],[EntryDate],[DocumentNo.],[BPartnerTypeID],[DirectPO_PRNo],[DocumentAuthority],[PurReqNo_AQNo],[BillingLocation],[ShipmentVia],[ModeofPayment],[FreightTerms],[ForeignCurrency],[FcRate],[TotalFcValue],[TotalLocalCurrencyValue],[Remarks],[DeliveryDueDate],[DeliveryDueDays],[AdvanceAgainstPO%],[Status],[IsActive],[GocID],[CompId],[BranchId],[FiscalYearID],[CreatedBy],[CreatedDate],[Record_Deleted],[SuppRefNo],[forceclosed],[Carry_Fwd],[ReferNo],[Brough_Fwd]) VALUES " + "(" + DocName + ",null,'" + EntryDate + "', '" + PoDoc + "'," + Supplier + ",'" + direct_po + "'," + DocAuth + ",'" + lstFruits.SelectedItem.Text.ToString() + "'," + billToDept + "," + shipVia + "," + ModeOfPay + "," + FreightTerms + "," + FCurrency + "," + FCRate + "," + TotalFCVal + ",'" + TotalLocalCurVal + "','" + Remark + "','" + Delivery + "','" + DeliDueDay + "'," + AdvaceAgaPo + ", " + Status + ",'" + chk + "'," + gocid() + "," + compid() + "," + branchId() + "," + FiscalYear() + ",'" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "'," + SupplyRefNo + ",'0','0'," + 0 + "," + 0 + ",'0'); SELECT * FROM Set_PurcahseOrderMaster WHERE Sno = SCOPE_IDENTITY()";
                        dsCh = dml.Find(Query);
                        if (dsCh.Tables[0].Rows.Count > 0)
                        {
                            ids = dsCh.Tables[0].Rows[0]["Sno"].ToString();
                            ViewState["detailid"] = ids;
                        }
                        detaisave(ids);
                    }
                    else {
                        Label1.Text = "Delivery Date can not be of date before then entry date";
                    }

                    //supplier email
                    if (ddlSupplier.SelectedIndex != 0)
                    {
                        DataSet ds_email = dml.Find("select BPartnerName, EmailAddress from SET_BusinessPartner WHERE BPartnerId ='" + ddlSupplier.SelectedItem.Value + "'");
                        if (ds_email.Tables[0].Rows.Count > 0)
                        {

                            string email = ds_email.Tables[0].Rows[0]["EmailAddress"].ToString();
                            emails(email, ddlSupplier.SelectedItem.Text);
                        }
                    }


                    textClear();
                    btnInsert_Click(sender, e);
                    ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);
                }
                else
                {
                    Label1.Text = "Please entry alteast 1 entry in detail table";
                }
            }
            else
            {
                if (lstFruits.Items.Count > 0)
                {
                    string dc = detailcond();
                    if (dc != "")
                    {
                        if (Convert.ToDateTime(txtDeliveryDate.Text) >= Convert.ToDateTime(txtEntryDate.Text))
                        {


                            if (ddlPODocName.SelectedItem.Value == "Please select...")
                            { DocName = 0; }
                            else { DocName = Convert.ToInt32(ddlPODocName.SelectedItem.Value.ToString()); }

                            if (ddlSupplier.SelectedItem.Value == "Please select...")
                            { Supplier = 0; }
                            else { Supplier = Convert.ToInt32(ddlSupplier.SelectedValue.ToString()); }

                            if (ddlDocAuth.SelectedItem.Value == "Please select...")
                            { DocAuth = 0; }
                            else {
                                DocAuth = Convert.ToInt32(ddlDocAuth.SelectedItem.Value.ToString());
                            }

                            if (ddlbilltodeprt.SelectedItem.Value == "Please select...")
                            { billToDept = 0; }
                            else {
                                billToDept = Convert.ToInt32(ddlDocAuth.SelectedItem.Value.ToString());
                            }

                            if (ddlShipVia.SelectedItem.Value == "Please select...")
                            { shipVia = 0; }
                            else {
                                shipVia = Convert.ToInt32(ddlShipVia.SelectedItem.Value.ToString());
                            }

                            if (ddlmodeofpay.SelectedItem.Value == "Please select...")
                            { ModeOfPay = 0; }
                            else {
                                ModeOfPay = Convert.ToInt32(ddlmodeofpay.SelectedItem.Value.ToString());
                            }

                            if (ddlFreightTerms.SelectedItem.Value == "Please select...")
                            { FreightTerms = 0; }
                            else {
                                FreightTerms = Convert.ToInt32(ddlFreightTerms.SelectedItem.Value.ToString());
                            }

                            if (ddlFCurrency.SelectedItem.Value == "Please select...")
                            { FCurrency = 0; }
                            else {
                                FCurrency = Convert.ToInt32(ddlFCurrency.SelectedItem.Value.ToString());
                            }

                            if (ddlFCRate.SelectedItem.Value == "Please select...")
                            { FCRate = 0; }
                            else {
                                FCRate = Convert.ToDouble(ddlFCRate.SelectedItem.Value.ToString());
                            }
                            if (ddlStatus.SelectedItem.Value == "Please select...")
                            { Status = 0; }
                            else {
                                Status = Convert.ToInt32(ddlStatus.SelectedItem.Value.ToString());
                            }
                            if (string.IsNullOrEmpty(txtEntryDate.Text.ToString()))
                            { EntryDate = "NULL"; }
                            else { EntryDate = txtEntryDate.Text.ToString(); }

                            if (string.IsNullOrEmpty(txtTotalFCVal.Text.ToString()))
                            { TotalFCVal = 0; }
                            else { TotalFCVal = Convert.ToDouble(txtTotalFCVal.Text.ToString()); }


                            if (string.IsNullOrEmpty(txtTotalLocalCurVal.Text.ToString()))
                            { TotalLocalCurVal = 0; }
                            else { TotalLocalCurVal = Convert.ToInt32(txtTotalLocalCurVal.Text.ToString()); }

                            if (string.IsNullOrEmpty(txtRemarks.Text.ToString()))
                            { Remark = "NULL"; }
                            else { Remark = txtRemarks.Text.ToString(); }

                            if (string.IsNullOrEmpty(txtDeliveryDate.Text.ToString()))
                            { Delivery = "NULL"; }
                            else { Delivery = txtDeliveryDate.Text.ToString(); }

                            if (string.IsNullOrEmpty(txtDeliDuedays.Text.ToString()))
                            { DeliDueDay = "NULL"; }
                            else { DeliDueDay = txtDeliDuedays.Text.ToString(); }

                            if (string.IsNullOrEmpty(txtAdvaceAgaPO.Text.ToString()))
                            { AdvaceAgaPo = 0; }
                            else { AdvaceAgaPo = Convert.ToDouble(txtAdvaceAgaPO.Text.ToString()); }
                            if (!string.IsNullOrEmpty(txtSuppRefNo.Text.ToString()))
                            {
                                SupplyRefNo = Convert.ToInt32(txtSuppRefNo.Text.ToString());
                            }
                            PoDoc = required_generateforIns();
                            if (string.IsNullOrEmpty(PoDoc))
                            {
                                PoDoc = "NULL";
                            }


                            string Query = "INSERT INTO Set_PurcahseOrderMaster ([DocId],[DocType],[EntryDate],[DocumentNo.],[BPartnerTypeID],[DirectPO_PRNo],[DocumentAuthority],[PurReqNo_AQNo],[BillingLocation],[ShipmentVia],[ModeofPayment],[FreightTerms],[ForeignCurrency],[FcRate],[TotalFcValue],[TotalLocalCurrencyValue],[Remarks],[DeliveryDueDate],[DeliveryDueDays],[AdvanceAgainstPO%],[Status],[IsActive],[GocID],[CompId],[BranchId],[FiscalYearID],[CreatedBy],[CreatedDate],[Record_Deleted],[SuppRefNo],[forceclosed],[Carry_Fwd],[ReferNo],[Brough_Fwd]) VALUES " + "(" + DocName + ",null,'" + EntryDate + "', '" + PoDoc + "'," + Supplier + ",'" + direct_po + "'," + DocAuth + ",'" + lstFruits.SelectedItem.Text.ToString() + "'," + billToDept + "," + shipVia + "," + ModeOfPay + "," + FreightTerms + "," + FCurrency + "," + FCRate + "," + TotalFCVal + ",'" + TotalLocalCurVal + "','" + Remark + "','" + Delivery + "','" + DeliDueDay + "'," + AdvaceAgaPo + ", " + Status + ",'" + chk + "'," + gocid() + "," + compid() + "," + branchId() + "," + FiscalYear() + ",'" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "'," + SupplyRefNo + ",'0','0'," + 0 + "," + 0 + ",'0'); SELECT * FROM Set_PurcahseOrderMaster WHERE Sno = SCOPE_IDENTITY()";
                            dsCh = dml.Find(Query);
                            if (dsCh.Tables[0].Rows.Count > 0)
                            {
                                ids = dsCh.Tables[0].Rows[0]["Sno"].ToString();
                                ViewState["detailid"] = ids;
                            }
                            detaisaveForPR(ids);
                            textClear();
                            btnInsert_Click(sender, e);
                            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);
                        }
                        else
                        {
                            Label1.Text = "Delivery Date can not be previous date then entry date";
                        }

                    }
                    else
                    {
                        Label1.Text = "Please entry altest 1 entry in detail table";
                    }
                }
                else
                {
                    Label1.Text = "Entry not allowed. There is no Purchase Requisition No.";
                }

            }

        }

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string entrydate = "", deliverydate = "";
        if (!string.IsNullOrEmpty(txtEditEntrydate.Text.ToString()))
        {
            entrydate = txtEntryDate.ToString();
        }
        if (!string.IsNullOrEmpty(txtDeliveryDate.Text.ToString()))
        {
            deliverydate = txtDeliveryDate.ToString();
        }
        string chk = "0";
        if (chkActive.Checked == true)
        {
            chk = "1";
        }

        DataSet ds_up = dml.Find("select * from Set_PurcahseOrderMaster  WHERE ([Sno]='" + ViewState["SNO"].ToString() + "') AND ([DocId]='" + ddlPODocName.SelectedItem.Value + "') AND ([EntryDate]='" + entrydate + "') AND ([BPartnerTypeID]='" + ddlSupplier.SelectedItem.Value + "') AND ([DirectPO_PRNo]='0') AND ([DocumentAuthority]='" + ddlDocAuth.SelectedItem.Value + "') AND  ([BillingLocation]='" + ddlbilltodeprt.SelectedItem.Value + "') AND ([ShipmentVia]='" + ddlShipVia.SelectedItem.Value + "') AND ([ModeofPayment]='" + ddlmodeofpay.SelectedItem.Value + "') AND ([FreightTerms]='" + ddlFreightTerms.SelectedItem.Value + "') AND ([ForeignCurrency]='" + ddlFCurrency.SelectedItem.Value + "') AND ([FcRate]='" + ddlFCRate.SelectedItem.Text + "') AND ([TotalFcValue]='" + txtTotalFCVal.Text + "') AND ([TotalLocalCurrencyValue]='" + txtTotalLocalCurVal.Text + "') AND ([Remarks]='" + txtRemarks.Text + "') AND ([DeliveryDueDate]='" + deliverydate + "') AND ([DeliveryDueDays]='" + txtDeliDuedays.Text + "') AND ([AdvanceAgainstPO%]='" + txtAdvaceAgaPO.Text + "') AND ([Status]='" + ddlStatus.SelectedItem.Value + "') AND ([IsActive]='" + chk + "') AND ([SuppRefNo]='" + txtSuppRefNo.Text + "') AND ([forceclosed]='" + txtforceclosed.Text + "')");

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
            if (ddlPODocName.SelectedItem.Value == "Please select...")
            { DocName = 0; }
            else { DocName = Convert.ToInt32(ddlPODocName.SelectedItem.Value.ToString()); }

            if (ddlSupplier.SelectedItem.Value == "Please select...")
            { Supplier = 0; }
            else { Supplier = Convert.ToInt32(ddlSupplier.SelectedValue.ToString()); }

            if (ddlDocAuth.SelectedItem.Value == "Please select...")
            { DocAuth = 0; }
            else {
                DocAuth = Convert.ToInt32(ddlDocAuth.SelectedItem.Value.ToString());
            }

            if (ddlbilltodeprt.SelectedItem.Value == "Please select...")
            { billToDept = 0; }
            else {
                billToDept = Convert.ToInt32(ddlDocAuth.SelectedItem.Value.ToString());
            }

            if (ddlShipVia.SelectedItem.Value == "Please select...")
            { shipVia = 0; }
            else {
                shipVia = Convert.ToInt32(ddlShipVia.SelectedItem.Value.ToString());
            }

            if (ddlmodeofpay.SelectedItem.Value == "Please select...")
            { ModeOfPay = 0; }
            else {
                ModeOfPay = Convert.ToInt32(ddlmodeofpay.SelectedItem.Value.ToString());
            }

            if (ddlFreightTerms.SelectedItem.Value == "Please select...")
            { FreightTerms = 0; }
            else {
                FreightTerms = Convert.ToInt32(ddlFreightTerms.SelectedItem.Value.ToString());
            }

            if (ddlFCurrency.SelectedItem.Value == "Please select...")
            { FCurrency = 0; }
            else {
                FCurrency = Convert.ToInt32(ddlFCurrency.SelectedItem.Value.ToString());
            }

            if (ddlFCRate.SelectedItem.Value == "Please select...")
            { FCRate = 0; }
            else {
                FCRate = Convert.ToDouble(ddlFCRate.SelectedItem.Value.ToString());
            }
            if (ddlStatus.SelectedItem.Value == "Please select...")
            { Status = 0; }
            else {
                Status = Convert.ToInt32(ddlStatus.SelectedItem.Value.ToString());
            }
            if (string.IsNullOrEmpty(txtEntryDate.Text.ToString()))
            { EntryDate = "NULL"; }
            else { EntryDate = txtEntryDate.Text.ToString(); }

            if (string.IsNullOrEmpty(txtTotalFCVal.Text.ToString()))
            { TotalFCVal = 0; }
            else { TotalFCVal = Convert.ToDouble(txtTotalFCVal.Text.ToString()); }


            if (string.IsNullOrEmpty(txtTotalLocalCurVal.Text.ToString()))
            { TotalLocalCurVal = 0; }
            else { TotalLocalCurVal = Convert.ToInt32(txtTotalLocalCurVal.Text.ToString()); }

            if (string.IsNullOrEmpty(txtRemarks.Text.ToString()))
            { Remark = "NULL"; }
            else { Remark = txtRemarks.Text.ToString(); }

            if (string.IsNullOrEmpty(txtDeliveryDate.Text.ToString()))
            { Delivery = "NULL"; }
            else { Delivery = txtDeliveryDate.Text.ToString(); }

            if (string.IsNullOrEmpty(txtDeliDuedays.Text.ToString()))
            { DeliDueDay = "NULL"; }
            else { DeliDueDay = txtDeliDuedays.Text.ToString(); }

            if (string.IsNullOrEmpty(txtAdvaceAgaPO.Text.ToString()))
            { AdvaceAgaPo = 0; }
            else { AdvaceAgaPo = Convert.ToDouble(txtAdvaceAgaPO.Text.ToString()); }

            if (!string.IsNullOrEmpty(txtSuppRefNo.Text.ToString()))
            {
                SupplyRefNo = Convert.ToInt32(txtSuppRefNo.Text.ToString());
            }
            PoDoc = required_generateforIns();
            if (string.IsNullOrEmpty(PoDoc))
            {
                PoDoc = "NULL";
            }


            dml.Update("UPDATE Set_PurcahseOrderMaster SET [DocId]='" + DocName + "', [EntryDate]='" + EntryDate + "', [BPartnerTypeID]=" + Supplier + ", [DirectPO_PRNo]='0', [DocumentAuthority]=" + DocAuth + ", [BillingLocation]=" + billToDept + ", [ShipmentVia]=" + shipVia + ", [ModeofPayment]=" + ModeOfPay + ", [FreightTerms]=" + FreightTerms + ", [ForeignCurrency]=" + FCurrency + ", [FcRate]=" + FCRate + ", [TotalFcValue]=" + TotalFCVal + ", [TotalLocalCurrencyValue]=" + TotalLocalCurVal + ", [Remarks]='" + Remark + "', [DeliveryDueDate]='" + Convert.ToDateTime(Delivery) + "', [DeliveryDueDays]='" + DeliDueDay + "', [AdvanceAgainstPO%]=" + AdvaceAgaPo + ", [Status]=" + Status + ", [IsActive]='" + chk + "' , [UpdatedBy]='" + show_username() + "', [UpdatedDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', [SuppRefNo]='" + SupplyRefNo + "', [forceclosed]='" + txtforceclosed.Text + "' WHERE ([Sno]='" + ViewState["SNO"].ToString() + "')", "");
            // detaiEdit();
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
        btnDelete.Visible = true;
        btnUpdate.Visible = false;
        btnEdit.Visible = true;
        btnFind.Visible = true;
        btnSave.Visible = false;
        btnDelete_after.Visible = false;
        Showall_Dml();
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
            string squer = "select * from ViewForFUD_PO";


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

            if (ddlFind_Department.SelectedIndex != 0)
            {
                swhere = swhere + " and BillingLocation = '" + ddlFind_Department.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and BillingLocation is not null";
            }
            if (ddlFind_Status.SelectedIndex != 0)
            {
                swhere = swhere + " and Status = '" + ddlFind_Status.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and Status is not null";
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
                swhere = swhere + " and DeliveryDueDate = '" + txtDel_DelviDate.Text + "'";
            }
            else
            {
                swhere = swhere + " and DeliveryDueDate is not null";
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
            string squer = "select * from ViewForFUD_PO";


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

            if (ddlFind_Department.SelectedIndex != 0)
            {
                swhere = swhere + " and BillingLocation = '" + ddlFind_Department.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and BillingLocation is not null";
            }
            if (ddlFind_Status.SelectedIndex != 0)
            {
                swhere = swhere + " and Status = '" + ddlFind_Status.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and Status is not null";
            }
            if (txtFind_Entrydate.Text != "")
            {
                swhere = swhere + " and EntryDate = '" + txtFind_Entrydate.Text + "'";
            }
            else
            {
                swhere = swhere + " and EntryDate is not null";
            }
            if (txtFind_DeliveryDate.Text != "")
            {
                swhere = swhere + " and DeliveryDueDate = '" + txtFind_DeliveryDate.Text + "'";
            }
            else
            {
                swhere = swhere + " and DeliveryDueDate is not null";
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
        Findbox.Visible = false;
        fieldbox.Visible = false;
        DeleteBox.Visible = false;
        try
        {
            GridView3.DataBind();
            string swhere = "";
            string squer = "select * from ViewForFUD_PO";


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

            if (ddlEdit_Depart.SelectedIndex != 0)
            {
                swhere = swhere + " and BillingLocation = '" + ddlEdit_Depart.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and BillingLocation is not null";
            }
            if (ddlEdit_Status.SelectedIndex != 0)
            {
                swhere = swhere + " and Status = '" + ddlEdit_Status.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and Status is not null";
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
                swhere = swhere + " and DeliveryDueDate = '" + txtEdit_deliveryDate.Text + "'";
            }
            else
            {
                swhere = swhere + " and DeliveryDueDate is not null";
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
        btnUpdatePO.Visible = false;
        Div1.Visible = false;
        Div2.Visible = false;
        Div3.Visible = false;
        Div4.Visible = false;

        if (lstFruits.SelectedIndex > 0)
        {
            lstFruits.SelectedIndex = 0;
        }

        ddlPODocName.SelectedIndex = 0;
        ddlSupplier.SelectedIndex = 0;
        ddlDocAuth.SelectedIndex = 0;
        ddlbilltodeprt.SelectedIndex = 0;
        ddlmodeofpay.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        txtEntryDate.Text = "";
        txtPODocNo.Text = "";
        ddlShipVia.SelectedIndex = 0;
        txtSuppRefNo.Text = "";
        ddlFreightTerms.SelectedIndex = 0;
        txtDeliDuedays.Text = "";
        txtDeliveryDate.Text = "";
        txtforceclosed.Text = "";
        ddlFCurrency.SelectedIndex = 0;
        ddlFCRate.SelectedIndex = 0;
        txtTotalFCVal.Text = "";
        txtTotalLocalCurVal.Text = "";
        txtAdvaceAgaPO.Text = "";
        txtCreateddate.Text = "";
        txtUpdateDate.Text = "";
        txtRemarks.Text = "";

        chkActive.Checked = false;
        chkDirect.Checked = false;
        chkDirect.Enabled = false;

        lstFruits.Enabled = false;

        chkPRNo.Enabled = false;
        ddlPODocName.Enabled = false;
        ddlSupplier.Enabled = false;
        ddlDocAuth.Enabled = false;
        ddlbilltodeprt.Enabled = false;
        ddlmodeofpay.Enabled = false;
        ddlStatus.Enabled = false;
        txtEntryDate.Enabled = false;
        txtPODocNo.Enabled = false;
        ddlShipVia.Enabled = false;
        txtSuppRefNo.Enabled = false;
        ddlFreightTerms.Enabled = false;
        txtDeliDuedays.Enabled = false;
        txtDeliveryDate.Enabled = false;
        txtforceclosed.Enabled = false;
        ddlFCurrency.Enabled = false;
        ddlFCRate.Enabled = false;
        txtTotalFCVal.Enabled = false;
        txtTotalLocalCurVal.Enabled = false;
        txtAdvaceAgaPO.Enabled = false;
        txtCreateddate.Enabled = false;
        txtUpdateDate.Enabled = false;
        txtRemarks.Enabled = false;
        chkActive.Enabled = false;
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

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            //SELECT * from Set_PurchaseOrderDetail where Sno_Master = 4
            dml.Delete("update Set_PurcahseOrderMaster set Record_Deleted = 1 where Sno= " + ViewState["SNO"].ToString() + "", "");
            dml.Delete("update Set_PurchaseOrderDetail set Record_Deleted = 1 where Sno_Master= " + ViewState["SNO"].ToString() + "", "");
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


        //  updatecol.Visible = true;
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {
            string serial_id;
            serial_id = GridView1.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;

            DataSet ds = dml.Find("select * from Set_PurcahseOrderMaster WHERE ([Sno]='" + serial_id + "') and Record_Deleted = '0'");
            if (ds.Tables[0].Rows.Count > 0)
            {


                ddlPODocName.ClearSelection();
                ddlSupplier.ClearSelection();
                ddlDocAuth.ClearSelection();
                ddlbilltodeprt.ClearSelection();
                ddlmodeofpay.ClearSelection();
                ddlStatus.ClearSelection();
                ddlShipVia.ClearSelection();
                ddlFreightTerms.ClearSelection();
                ddlFCurrency.ClearSelection();
                ddlFCRate.ClearSelection();

                ddlPODocName.Items.FindByValue(ds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;
                ddlSupplier.Items.FindByValue(ds.Tables[0].Rows[0]["BPartnerTypeID"].ToString()).Selected = true;
                ddlDocAuth.Items.FindByValue(ds.Tables[0].Rows[0]["DocumentAuthority"].ToString()).Selected = true;
                ddlbilltodeprt.Items.FindByValue(ds.Tables[0].Rows[0]["BillingLocation"].ToString()).Selected = true;
                ddlmodeofpay.Items.FindByValue(ds.Tables[0].Rows[0]["ModeofPayment"].ToString()).Selected = true;
                ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["Status"].ToString()).Selected = true;
                ddlShipVia.Items.FindByValue(ds.Tables[0].Rows[0]["ShipmentVia"].ToString()).Selected = true;
                ddlFreightTerms.Items.FindByValue(ds.Tables[0].Rows[0]["FreightTerms"].ToString()).Selected = true;
                ddlFCurrency.Items.FindByValue(ds.Tables[0].Rows[0]["ForeignCurrency"].ToString()).Selected = true;
                ddlFCRate.Items.FindByText(ds.Tables[0].Rows[0]["FcRate"].ToString()).Selected = true;

                txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtPODocNo.Text = ds.Tables[0].Rows[0]["DocumentNo."].ToString();
                txtSuppRefNo.Text = ds.Tables[0].Rows[0]["SuppRefNo"].ToString();
                txtDeliDuedays.Text = ds.Tables[0].Rows[0]["DeliveryDueDays"].ToString();
                txtDeliveryDate.Text = ds.Tables[0].Rows[0]["DeliveryDueDate"].ToString();
                txtforceclosed.Text = ds.Tables[0].Rows[0]["forceclosed"].ToString();

                txtTotalFCVal.Text = ds.Tables[0].Rows[0]["TotalFcValue"].ToString();
                txtTotalLocalCurVal.Text = ds.Tables[0].Rows[0]["TotalLocalCurrencyValue"].ToString();
                txtAdvaceAgaPO.Text = ds.Tables[0].Rows[0]["AdvanceAgainstPO%"].ToString();

                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();

                txtCreateddate.Text = dml.dateConvert(ds.Tables[0].Rows[0]["CreatedDate"].ToString()) + " " + dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdateDate.Text = ds.Tables[0].Rows[0]["UpdatedDate"].ToString() + " " + dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());
                dml.dateConvert(txtEntryDate);
                dml.dateConvert(txtDeliveryDate);
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                string DirectQuotation = ds.Tables[0].Rows[0]["DirectPO_PRNo"].ToString();
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
                    chkPRNo.Checked = false;
                }
                else
                {
                    chkPRNo.Checked = true;
                    chkDirect.Checked = false;
                }
                showdetailEdit(serial_id);
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

            DataSet ds = dml.Find("select * from Set_PurcahseOrderMaster WHERE ([Sno]='" + serial_id + "') and Record_Deleted = '0'");
            if (ds.Tables[0].Rows.Count > 0)
            {


                ddlPODocName.ClearSelection();
                ddlSupplier.ClearSelection();
                ddlDocAuth.ClearSelection();
                ddlbilltodeprt.ClearSelection();
                ddlmodeofpay.ClearSelection();
                ddlStatus.ClearSelection();
                ddlShipVia.ClearSelection();
                ddlFreightTerms.ClearSelection();
                ddlFCurrency.ClearSelection();
                ddlFCRate.ClearSelection();

                ddlPODocName.Items.FindByValue(ds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;
                ddlSupplier.Items.FindByValue(ds.Tables[0].Rows[0]["BPartnerTypeID"].ToString()).Selected = true;
                ddlDocAuth.Items.FindByValue(ds.Tables[0].Rows[0]["DocumentAuthority"].ToString()).Selected = true;
                ddlbilltodeprt.Items.FindByValue(ds.Tables[0].Rows[0]["BillingLocation"].ToString()).Selected = true;
                ddlmodeofpay.Items.FindByValue(ds.Tables[0].Rows[0]["ModeofPayment"].ToString()).Selected = true;
                ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["Status"].ToString()).Selected = true;
                ddlShipVia.Items.FindByValue(ds.Tables[0].Rows[0]["ShipmentVia"].ToString()).Selected = true;
                ddlFreightTerms.Items.FindByValue(ds.Tables[0].Rows[0]["FreightTerms"].ToString()).Selected = true;
                ddlFCurrency.Items.FindByValue(ds.Tables[0].Rows[0]["ForeignCurrency"].ToString()).Selected = true;
                ddlFCRate.Items.FindByValue(ds.Tables[0].Rows[0]["FcRate"].ToString()).Selected = true;

                txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtPODocNo.Text = ds.Tables[0].Rows[0]["DocumentNo."].ToString();
                txtSuppRefNo.Text = ds.Tables[0].Rows[0]["SuppRefNo"].ToString();
                txtDeliDuedays.Text = ds.Tables[0].Rows[0]["DeliveryDueDays"].ToString();
                txtDeliveryDate.Text = ds.Tables[0].Rows[0]["DeliveryDueDate"].ToString();
                txtforceclosed.Text = ds.Tables[0].Rows[0]["forceclosed"].ToString();

                txtTotalFCVal.Text = ds.Tables[0].Rows[0]["TotalFcValue"].ToString();
                txtTotalLocalCurVal.Text = ds.Tables[0].Rows[0]["TotalLocalCurrencyValue"].ToString();
                txtAdvaceAgaPO.Text = ds.Tables[0].Rows[0]["AdvanceAgainstPO%"].ToString();

                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();

                txtCreateddate.Text = dml.dateConvert(ds.Tables[0].Rows[0]["CreatedDate"].ToString()) + " " + dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdateDate.Text = ds.Tables[0].Rows[0]["UpdatedDate"].ToString() + " " + dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());
                dml.dateConvert(txtEntryDate);
                dml.dateConvert(txtDeliveryDate);
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                string DirectQuotation = ds.Tables[0].Rows[0]["DirectPO_PRNo"].ToString();
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
                    chkPRNo.Checked = false;
                }
                else
                {
                    chkPRNo.Checked = true;
                    chkDirect.Checked = false;
                }
                showdetailEdit(serial_id);
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
        btnUpdatePO.Visible = true;
        Label1.Text = "";

        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;


        try
        {
            string serial_id;
            serial_id = GridView3.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;

            DataSet ds = dml.Find("select * from Set_PurcahseOrderMaster WHERE ([Sno]='" + serial_id + "') and Record_Deleted = '0'");
            if (ds.Tables[0].Rows.Count > 0)
            {


                ddlSupplier.Enabled = true;
                ddlDocAuth.Enabled = true;
                ddlbilltodeprt.Enabled = true;
                ddlmodeofpay.Enabled = true;
                ddlStatus.Enabled = true;
                txtEntryDate.Enabled = true;
                chkPRNo.Enabled = true;
                ddlShipVia.Enabled = true;
                txtSuppRefNo.Enabled = true;
                ddlFreightTerms.Enabled = true;
                txtDeliDuedays.Enabled = true;
                txtDeliveryDate.Enabled = true;
                txtforceclosed.Enabled = true;
                ddlFCurrency.Enabled = true;
                ddlFCRate.Enabled = true;
                txtTotalFCVal.Enabled = true;
                txtTotalLocalCurVal.Enabled = false;
                txtAdvaceAgaPO.Enabled = true;
                txtCreateddate.Enabled = false;
                txtUpdateDate.Enabled = false;
                txtRemarks.Enabled = true;
                chkActive.Enabled = true;
                chkDirect.Checked = true;
                chkActive.Checked = true;
                chkDirect.Enabled = true;





                ddlPODocName.ClearSelection();
                ddlSupplier.ClearSelection();
                ddlDocAuth.ClearSelection();
                ddlbilltodeprt.ClearSelection();
                ddlmodeofpay.ClearSelection();
                ddlStatus.ClearSelection();
                ddlShipVia.ClearSelection();
                ddlFreightTerms.ClearSelection();
                ddlFCurrency.ClearSelection();
                ddlFCRate.ClearSelection();

                ddlPODocName.Items.FindByValue(ds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;
                ddlSupplier.Items.FindByValue(ds.Tables[0].Rows[0]["BPartnerTypeID"].ToString()).Selected = true;
                ddlDocAuth.Items.FindByValue(ds.Tables[0].Rows[0]["DocumentAuthority"].ToString()).Selected = true;
                ddlbilltodeprt.Items.FindByValue(ds.Tables[0].Rows[0]["BillingLocation"].ToString()).Selected = true;
                ddlmodeofpay.Items.FindByValue(ds.Tables[0].Rows[0]["ModeofPayment"].ToString()).Selected = true;
                ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["Status"].ToString()).Selected = true;
                ddlShipVia.Items.FindByValue(ds.Tables[0].Rows[0]["ShipmentVia"].ToString()).Selected = true;
                ddlFreightTerms.Items.FindByValue(ds.Tables[0].Rows[0]["FreightTerms"].ToString()).Selected = true;
                ddlFCurrency.Items.FindByValue(ds.Tables[0].Rows[0]["ForeignCurrency"].ToString()).Selected = true;
                ddlFCRate.Items.FindByValue(ds.Tables[0].Rows[0]["FcRate"].ToString()).Selected = true;

                txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                if (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["DocumentNo."].ToString()))
                {
                    txtPODocNo.Text = "";
                }
                else {
                    txtPODocNo.Text = ds.Tables[0].Rows[0]["DocumentNo."].ToString();
                }

                if (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["SuppRefNo"].ToString()))
                {
                    txtSuppRefNo.Text = "";
                }
                else {
                    txtSuppRefNo.Text = ds.Tables[0].Rows[0]["SuppRefNo"].ToString();

                }
                txtDeliDuedays.Text = ds.Tables[0].Rows[0]["DeliveryDueDays"].ToString();
                txtDeliveryDate.Text = ds.Tables[0].Rows[0]["DeliveryDueDate"].ToString();
                txtforceclosed.Text = ds.Tables[0].Rows[0]["forceclosed"].ToString();
                CalendarExtender3.StartDate = Convert.ToDateTime(txtEntryDate.Text.ToString());
                txtTotalFCVal.Text = ds.Tables[0].Rows[0]["TotalFcValue"].ToString();
                txtTotalLocalCurVal.Text = ds.Tables[0].Rows[0]["TotalLocalCurrencyValue"].ToString();
                txtAdvaceAgaPO.Text = ds.Tables[0].Rows[0]["AdvanceAgainstPO%"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                string Id = ds.Tables[0].Rows[0]["CreatedBy"].ToString();
                txtCreateddate.Text = dml.dateConvert(ds.Tables[0].Rows[0]["CreatedDate"].ToString()) + " " + dml.show_usernameFED(Id);
                txtUpdateDate.Text = ds.Tables[0].Rows[0]["UpdatedDate"].ToString() + " " + dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());
                dml.dateConvert(txtEntryDate);
                dml.dateConvert(txtDeliveryDate);
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                string DirectQuotation = ds.Tables[0].Rows[0]["DirectPO_PRNo"].ToString();
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
                    chkPRNo.Checked = false;
                }
                else
                {
                    chkPRNo.Checked = true;
                    chkDirect.Checked = false;
                }

                DataTable dtup = new DataTable();
                dtup.Columns.AddRange(new DataColumn[14] { new DataColumn("ItemSubHeadName"), new DataColumn("Description"), new DataColumn("UOMName"), new DataColumn("Quantity"), new DataColumn("ApprovedQuantity"), new DataColumn("Rate"), new DataColumn("GST"), new DataColumn("GSTRate"), new DataColumn("GrossValue"), new DataColumn("LocName"), new DataColumn("Project"), new DataColumn("uom2name"), new DataColumn("Qty2"), new DataColumn("Rate2") });


                //select * from view_StockUpdate
                //select ItemSubHeadName , Description,CostCenterName,UOMName from view_StockUpdate
                DataSet ds_detail = dml.Find("select * from ViewPODetail_FED where Sno_Master = '" + serial_id + "'");
                if (ds_detail.Tables[0].Rows.Count > 0)
                {
                    ViewState["dtup"] = ds_detail.Tables[0];
                    Div2.Visible = true;
                    GridView4.DataSource = ds_detail.Tables[0];
                    GridView4.DataBind();
                }
                Div1.Visible = false;
                Div2.Visible = false;
                Div3.Visible = false;
                Div4.Visible = false;
                PopulateGridview_Up();


                //showdetailEdit(serial_id);
            }

        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    protected void Check_CustomDateForDeliveryDate(object source, ServerValidateEventArgs e)
    {
        if (txtDeliveryDate.Text != null && txtEntryDate != null)
            if (Convert.ToDateTime(txtDeliveryDate.Text) >= Convert.ToDateTime(txtEntryDate.Text))
            { e.IsValid = true; }
            else
                e.IsValid = false;
    }
    public void PopulateGridview_Up()
    {
        Div3.Visible = true;
        DataTable dtbl = new DataTable();
        dtbl = (DataTable)ViewState["dtup"];

        if (dtbl == null)
        {
            Div3.Visible = false;
        }
        else
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
        DataSet ds = dml.Find("SELECT * FROM SET_Fiscal_Year where FiscalYearId =" + FiscalYear());
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
    public string FiscalEnd(string fyear)
    {
        string sdate;
        DataSet ds = dml.Find("SELECT * FROM SET_Fiscal_Year where FIscalYearId =" + FiscalYear());
        if (ds.Tables[0].Rows.Count > 0)
        {
            sdate = ds.Tables[0].Rows[0]["EndDate"].ToString();

        }
        else
        {
            sdate = (DateTime.Now.Year - 1).ToString() + "-06-30";
        }
        return sdate;

    }
    public string detailcond()
    {
        if (GridView7.Rows.Count > 0)
        {
            Label dp = GridView7.Rows[0].FindControl("lblRowNumber") as Label;

            if (dp.Text != "")
            {
                return dp.Text;
            }
            else
            {
                return "No Data";
            }
        }
        else return "";
    }
    public string detailcond6()
    {
        if (GridView6.Rows.Count > 0)
        {
            Label dp = GridView6.Rows[0].FindControl("lblRowNumber") as Label;

            if (dp.Text != "")
            {
                return dp.Text;
            }
            else
            {
                return "";
            }
        }
        else {
            return "No Data";
        }
    }
    public void PopulateGridview()
    {

        DataTable dtbl = (DataTable)ViewState["Customers"];

        if (dtbl.Rows.Count > 0)
        {

            GridView6.DataSource = (DataTable)ViewState["Customers"];
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
    public void showdetail(string reqNO)
    {
        Div1.Visible = true;


        if (chkDirect.Checked == true)
        {
            string query = "select * from View_stockDirect where reqNOdetail = '" + reqNO + "' and BalanceQty > 0";
            DataSet ds = dml.grid(query);

            if (ds.Tables[0].Rows.Count > 0)
            {

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
            string query = "select * from View_SuppApproved where reqNOdetail = '" + reqNO + "'";
            DataSet ds = dml.grid(query);

            if (ds.Tables[0].Rows.Count > 0)
            {

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
    public void doctype(string menuid, string formid, string usergrpid)
    {

        ddlPODocName.ClearSelection();
        dml.dropdownsql2where(ddlPODocName, "ViewUserGrp_Doc", "docn", "DocID", "MenuId_Sno", menuid, "FormId_Sno", formid, "UserGrpId", usergrpid);



        if (ddlPODocName.Items.Count == 0)
        {

            ddlPODocName.SelectedIndex = 0;
            ddlPODocName.Items.Insert(0, "Please select...");
            ddlPODocName.Enabled = false;

        }
        else if (ddlPODocName.Items.Count == 2)
        {
            ddlPODocName.SelectedIndex = 1;

            ddlPODocName.Enabled = false;
        }
        else
        {

            ddlPODocName.SelectedIndex = 0;
            ddlPODocName.Enabled = true;
        }






    }
    protected void GridView5_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lblRowNumber") as Label).Text = (e.Row.RowIndex + 1).ToString();


            string rate = (e.Row.FindControl("txtRate") as TextBox).Text;
            string gstPer = (e.Row.FindControl("txtGST") as TextBox).Text;
            string ApprQty = (e.Row.FindControl("txtApprovedQty") as TextBox).Text;
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


            float QtyValue = float.Parse(ApprQty) * float.Parse(rate);
            float GSTValue = float.Parse(ApprQty) * gstrate;

            (e.Row.FindControl("lblQtyVal") as Label).Text = QtyValue.ToString();

            (e.Row.FindControl("lblGstValue") as Label).Text = GSTValue.ToString();
            float GrossValue = QtyValue + GSTValue;
            (e.Row.FindControl("lblGrossValue") as Label).Text = GrossValue.ToString();
            //lblGrossValue
        }
    }
    protected void txtRate_TextChanged(object sender, EventArgs e)
    {

        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;

        string rate = (row.FindControl("txtRate") as TextBox).Text;
        string gstPer = (row.FindControl("txtGST") as TextBox).Text;
        string appQty = (row.FindControl("txtApprovedQty") as TextBox).Text;


        float gstrate;
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
    protected void txtRate_PR_TextChanged(object sender, EventArgs e)
    {

        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;

        string rate = (row.FindControl("txtRate_PR") as TextBox).Text;
        string gstPer = (row.FindControl("txtGST_PR") as TextBox).Text;
        string appQty = (row.FindControl("txtApprovedQty") as TextBox).Text;


        float gstrate;
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
        DataSet BranchData = dml.Find("select FinancialRoundOff,DisplayDigit from Set_Branch where BranchId =" + Convert.ToInt32(Request.Cookies["BranchId"].Value));
        var DisplayDigit = Convert.ToInt32(BranchData.Tables[0].Rows[0]["DisplayDigit"].ToString());
        var FinancialRoundOff = Convert.ToInt32(BranchData.Tables[0].Rows[0]["FinancialRoundOff"].ToString());
        float grossval = qtyval + gstval;

        if (DisplayDigit > 0)
        {

            qtyval = utl.RoundOffToFloat(DisplayDigit, FinancialRoundOff, qtyval.ToString());

            grossval = utl.RoundOffToFloat(DisplayDigit, FinancialRoundOff, grossval.ToString());
        }
        (row.FindControl("lblGstValue") as Label).Text = gstval.ToString();


        (row.FindControl("lblGrossValue") as Label).Text = grossval.ToString();
        txtTotalLocalCurVal.Text = grossval.ToString();

    }
    protected void txtGST_TextChanged(object sender, EventArgs e)
    {

        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;

        string rate = (row.FindControl("txtRate") as TextBox).Text;
        string gstPer = (row.FindControl("txtGST") as TextBox).Text;
        string appQty = (row.FindControl("txtApprovedQty") as TextBox).Text;


        float gstrate;
        if (rate == "")
        {
            gstrate = 0 * (0 / 100);
        }
        else
        {
            gstrate = float.Parse(rate) * (float.Parse(gstPer) / 100);
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
    //txtGST_PR_TextChanged
    protected void txtGST_PR_TextChanged(object sender, EventArgs e)
    {

        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;

        string rate = (row.FindControl("txtRate_PR") as TextBox).Text;
        string gstPer = (row.FindControl("txtGST_PR") as TextBox).Text;
        string appQty = (row.FindControl("txtApprovedQty") as TextBox).Text;


        float gstrate;
        if (rate == "")
        {
            gstrate = 0 * (0 / 100);
        }
        else
        {
            gstrate = float.Parse(rate) * (float.Parse(gstPer) / 100);

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
        foreach (GridViewRow g in GridView6.Rows)
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
            DataSet BranchData = dml.Find("select FinancialRoundOff,DisplayDigit from Set_Branch where BranchId =" + Convert.ToInt32(Request.Cookies["BranchId"].Value));
            var DisplayDigit = Convert.ToInt32(BranchData.Tables[0].Rows[0]["DisplayDigit"].ToString());
            var FinancialRoundOff = Convert.ToInt32(BranchData.Tables[0].Rows[0]["FinancialRoundOff"].ToString());
            if (DisplayDigit > 0)
            {
                lblGrossValue.Text = utl.RoundOff(DisplayDigit,FinancialRoundOff,lblGrossValue.Text);

                lblQtyVal.Text = utl.RoundOff(DisplayDigit,FinancialRoundOff,lblQtyVal.Text);

            }
            //DataSet ds = dml.Find("select  * from Set_QuotationReqDetail where ItemSubHead = '" + lblheadsub.Text + "' and ItemMaster = '" + lblitemmaster.Text + "'");
            //if (ds.Tables[0].Rows.Count > 0)
            //{

            //}
            //else
            //{

            //  dml.Insert("INSERT INTO Set_QuotationReqDetail([QuoatReqMId], [ItemSubHead], [ItemMaster], [UOM], [StockQuantity], [Supplier], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted], [ReqQuantity]) VALUES ('" + masterid + "','" + lblheadsub.Text + "', '" + lblitemmaster.Text + "', '" + lbluom.Text + "', '" + lblcurrstock.Text + "', '" + lblSupp.Text + "', '1', '" + gocid() + "', '" + compid() + "', '" + branchId() + "', '" + FiscalYear() + "', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0' , '" + lblreqQ.Text + "');", "");
            //nechy
            dml.Insert("INSERT INTO SuppQuotApprDetail ([Sno_Master], [ItemSubHeadID], [ItemID], [UOMID], [Quantity], [Rate], [ApprovedQuantity], [GST], [Project], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted], [GSTRate], [GSTValue], [QtyValue], [GrossValue], [LocId], [BalanceQty]) VALUES ('" + masterid + "', '" + subhead + "', '" + itemmaster + "', '" + uom1 + "', " + lblQty.Text + ", '" + lblRate.Text + "', " + lblApprovedQty.Text + ", '" + lblGST.Text + "', '" + lblproject.Text + "', 1, " + gocid() + ", " + compid() + "," + branchId() + ", " + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0', '" + lblGSTRate.Text + "', '" + lblGstValue.Text + "', '" + lblQtyVal.Text + "', '" + lblGrossValue.Text + "', '" + lblLocation.SelectedItem.Value + "', NULL);", "");
            dml.Update("update Set_QuotationReqMaster set Status = '6' where QuotationReqNO = '" + requisno.Text + "'", "");


            //}
        }
    }
    public void insertq()
    {
        string chkDirectval;
        if (chkDirect.Checked == true)
        {
            chkDirectval = "1";
        }
        else
        {
            chkDirectval = "0";
        }
        DataSet uniqueg_B_C = dml.Find("select * from SuppQuotationApprMaster where DocType='" + ddlPODocName.SelectedItem.Value + "' and DocNo = '" + txtPODocNo.Text + "' and Record_Deleted = '0'");
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

            string Entrydate = dml.dateconvertforinsert(txtEntryDate);
            string deliverydate = dml.dateconvertforinsert(txtDeliveryDate);

            string dc = detailcond();
            if (dc != "")
            {



                //   dml.Insert("INSERT INTO SuppQuotationApprMaster ( [DocId], [DocType], [DocAutority], [DocNo], [DirectQuotation], [AFQ_RequisitionNo], [Entrydate], [BPartnerTypeID], [DeliveryDate], [PaymentModeId], [SupplierReferenceNo], [DepartmentID], [DeliveryPeriod], [StatusId], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted]) VALUES ('"+ddlDocName.SelectedItem.Value+"', '"+ ddlDocName.SelectedItem.Value + "', '"+ddlDocAuth.SelectedItem.Value+"', '"+txtDocNo.Text+"', '"+chkDirectval+"', '"+ddlAFQ_ReqNo.SelectedItem.Text+"', '"+Entrydate+"', '"+ddlSupplier.SelectedItem.Value+"', '"+deliverydate+"', '"+ddlModeOFPayment.SelectedItem.Value+"', '"+txtSuppRefNo.Text+"', '"+ddlDepartment.SelectedItem.Value+"', '"+txtDeliverDuration.Text+"', '"+ddlStatus.SelectedItem.Value+"', '"+chk+"', '"+gocid()+ "', '" + compid() + "', '" + branchId() + "', '" + FiscalYear() + "', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0'); select  Sno from SuppQuotationApprMaster where Sno = SCOPE_IDENTITY()", "");

                //dml.Insert("INSERT INTO [SuppQuotationApprMaster] ([DocId], [DocType], [DocAutority], [DocNo], [DirectQuotation], [AFQ_RequisitionNo], [Entrydate], [BPartnerTypeID], [DeliveryDate], [PaymentModeId], [SupplierReferenceNo], [DeliveryPeriod], [Status], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted]) VALUES ('1', '" + ddlDocTypes.SelectedItem.Text + "', '" + ddlDocAuth.SelectedItem.Value + "', '" + txtDocNo.Text + "', '" + chkDirectval + "', '" + ddlAFQ_ReqNo.SelectedItem.Value + "', '" + Entrydate + "', '" + ddlSupplier.SelectedItem.Text + "', '" + deliverydate + "',  '" + ddlModeOFPayment.SelectedItem.Value + "', '" + txtSuppRefNo.Text + "',  '" + txtDeliverDuration.Text + "', '" + ddlStatus.SelectedItem.Text + "', '" + chk + "', '" + gocid() + "', '" + compid() + "', '" + branchId() + "', '" + FiscalYear() + "', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0'); select  Sno from SuppQuotationApprMaster where Sno = SCOPE_IDENTITY()", "");
                string ids = "1";
                //   DataSet dsCh = dml.Find("select Sno from SuppQuotationApprMaster WHERE ([DocType]='" + ddlDocName.SelectedItem.Value + "') AND ([DocAutority]='" + ddlDocAuth.SelectedItem.Value + "') AND ([DocNo]='" + txtDocNo.Text + "') AND ([DirectQuotation]='" + chkDirectval + "') AND ([AFQ_RequisitionNo]='" + ddlAFQ_ReqNo.SelectedItem.Text + "') AND ([Entrydate]='" + Entrydate + "') AND ([BPartnerTypeID]='" + ddlSupplier.SelectedItem.Value + "') AND ([DeliveryDate]='" + deliverydate + "')  AND ([PaymentModeId]='" + ddlModeOFPayment.SelectedItem.Value + "') AND ([SupplierReferenceNo]='" + txtSuppRefNo.Text + "') AND ([DeliveryPeriod]='" + txtDeliverDuration.Text + "') AND ([StatusId]='" + ddlStatus.SelectedItem.Value + "') AND ([IsActive]='" + chk + "') AND ([GocID]='" + gocid() + "') AND ([CompId]='" + compid() + "') AND ([BranchId]='" + branchId() + "') AND ([FiscalYearID]='" + FiscalYear() + "') AND ([Record_Deleted]='0')");
                //   if (dsCh.Tables[0].Rows.Count > 0)
                {
                    //       ids = dsCh.Tables[0].Rows[0]["Sno"].ToString();
                }

                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);
                Label1.Text = "";

                txtDeliveryDate.Text = "";
                ddlSupplier.SelectedIndex = 0;
                //  ddlModeOFPayment.SelectedIndex = 0;
                //  txtDeliverDuration.Text = "";
                //   txtRemarks.Text = "";
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
        Div3.Visible = true;
        string query = "select * from ViewPODetail_FED where Sno_Master = '" + qoutMid + "'";
        DataSet ds = dml.grid(query);
        if (ds.Tables[0].Rows.Count > 0)
        {

            GridView4.DataSource = ds;
            GridView4.DataBind();
        }
        else
        {
            GridView4.ShowHeaderWhenEmpty = true;
            GridView4.EmptyDataText = "NO RECORD";
            GridView4.DataBind();
        }
    }
    public string required_generateforIns()
    {
        string podoc = "";
        string month = DateTime.Now.Month.ToString("00");
        bool flag = true;
        bool flag1 = true;
        DateTime date = DateTime.Now;
        string year = date.ToString("yy");
        FormID = Request.QueryString["FormID"];
        fiscal = Request.QueryString["fiscaly"];
        string fy = fiscal.Substring(2, 2) + fiscal.Substring(7, 2);
        string docval = "0";
        if (ddlPODocName.SelectedIndex != 0)
        {
            docval = ddlPODocName.SelectedItem.Value;


            DataSet ds = dml.Find("select Monthlybase, YearlyBase from SET_Documents where DocID = '" + ddlPODocName.SelectedItem.Value + "';select MAX([DocumentNo.]) as maxno from Set_PurcahseOrderMaster where SUBSTRING([DocumentNo.], 1, 4) = '" + month + year + "'; select Description from SET_Fiscal_Year where Description = '" + fiscal + "'; select MAX([DocumentNo.]) as maxno from Set_PurcahseOrderMaster where SUBSTRING([DocumentNo.], 1, 4) = '" + fy + "'");

            if (ds.Tables[0].Rows.Count > 0)
            {
                string monthly = ds.Tables[0].Rows[0]["Monthlybase"].ToString();
                string yearly = ds.Tables[0].Rows[0]["YearlyBase"].ToString();
                string inc;

                inc = ds.Tables[1].Rows[0]["maxno"].ToString();
                if (inc == "")
                {
                    inc = "000001";
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

                    podoc = month + year + "-" + incre.ToString("00000");
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
                    podoc = fyear.Substring(2, 2) + fyear.Substring(7, 2) + "-" + incre.ToString("000000");


                }

            }
        }
        return podoc;
    }
    public void required_generate()
    {
        DateTime date = DateTime.Parse(txtEntryDate.Text);
        string year = date.ToString("yyyy");
        string month = date.Month.ToString("00");
        //string month = date.ToString("00");
        string docno = ddlPODocName.SelectedItem.Value;


        //  string month = DateTime.Now.Month.ToString("00");
        bool flag = true;
        bool flag1 = true;
        // DateTime date = DateTime.Now;
        // string year = date.ToString("yy");
        FormID = Request.QueryString["FormID"];
        fiscal = Request.QueryString["fiscaly"];
        string fy = fiscal.Substring(2, 2) + fiscal.Substring(7, 2);
        string docval = "0";
        if (ddlPODocName.SelectedIndex != 0)
        {
            docval = ddlPODocName.SelectedItem.Value;


            DataSet ds = dml.Find("select Monthlybase, YearlyBase from SET_Documents where DocID = '" + ddlPODocName.SelectedItem.Value + "';select MAX([DocumentNo.]) as maxno from Set_PurcahseOrderMaster where SUBSTRING([DocumentNo.], 1, 4) = '" + month + year + "'; select Description from SET_Fiscal_Year where Description = '" + fiscal + "'; select MAX([DocumentNo.]) as maxno from Set_PurcahseOrderMaster where SUBSTRING([DocumentNo.], 1, 4) = '" + fy + "'");

            if (ds.Tables[0].Rows.Count > 0)
            {
                string monthly = ds.Tables[0].Rows[0]["Monthlybase"].ToString();
                string yearly = ds.Tables[0].Rows[0]["YearlyBase"].ToString();
                string inc;

                inc = ds.Tables[1].Rows[0]["maxno"].ToString();
                if (inc == "")
                {
                    inc = "000001";
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

                    txtPODocNo.Text = docno + "-" + month + year + "-" + incre.ToString("00000");
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
                    txtPODocNo.Text = docno + "-" + fyear.Substring(2, 2) + fyear.Substring(7, 2) + "-" + incre.ToString("000000");


                }

            }
        }
    }
    protected void ddlPODocName_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlPODocName.SelectedIndex != 0)
        {


            ddlSupplier.Enabled = true;
            ddlDocAuth.Enabled = true;
            ddlbilltodeprt.Enabled = true;
            ddlmodeofpay.Enabled = true;
            ddlStatus.Enabled = true;
            txtEntryDate.Enabled = true;
            chkPRNo.Enabled = true;
            ddlShipVia.Enabled = true;
            txtSuppRefNo.Enabled = true;
            ddlFreightTerms.Enabled = true;
            txtDeliDuedays.Enabled = true;
            txtDeliveryDate.Enabled = true;
            txtforceclosed.Enabled = true;
            ddlFCurrency.Enabled = true;
            ddlFCRate.Enabled = true;
            txtTotalFCVal.Enabled = true;
            txtTotalLocalCurVal.Enabled = false;
            txtAdvaceAgaPO.Enabled = true;
            txtCreateddate.Enabled = false;
            txtUpdateDate.Enabled = false;
            txtRemarks.Enabled = true;
            chkActive.Enabled = true;
            chkDirect.Checked = true;
            // chkDirect_CheckedChanged(sender, e);
            chkActive.Checked = true;
            chkDirect.Enabled = true;
            txtCreateddate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss") + " " + show_username();
            txtEntryDate.Text = Convert.ToString(ViewState["EndDate"]);
            CalendarExtender3.StartDate = Convert.ToDateTime(Convert.ToString(ViewState["EndDate"]));

            string currID = "0";
            DataSet ds = dml.Find("select CurrencyID,CurrencyName from SET_Currency where CurrencyID in (select BaseCurrencyID from SET_Branch where BranchId = '" + branchId() + "')");
            if (ds.Tables[0].Rows.Count > 0)
            {
                currID = ds.Tables[0].Rows[0]["CurrencyID"].ToString();
            }
            ddlFCurrency.ClearSelection();

            if (ddlFCurrency.Items.FindByValue(currID) != null)
            {
                ddlFCurrency.Items.FindByValue(currID).Selected = true;
            }

            ddlStatus.ClearSelection();
            ddlStatus.Items.FindByText("Open").Selected = true;




            txtAdvaceAgaPO.Text = "0";
            // this line open befiore insert query
            required_generate();
            txtforceclosed.Text = "7";
            ddlFCurrency_SelectedIndexChanged(sender, e);


            dml.dropdownsqlwithquery(ddlDocAuth, "select AuthorityId, AuthorityName from SET_Authority where AuthorityId in (SELECT AuthorityId from SET_UserGrp_DocAuthority where docid= '" + ddlPODocName.SelectedItem.Value + "')", "AuthorityName", "AuthorityId");

            if (ddlDocAuth.Items.Count > 2)
            {
                ddlDocAuth.Enabled = true;
                ddlDocAuth.SelectedIndex = 0;
            }
            else if (ddlDocAuth.Items.Count == 1)
            {
                ddlDocAuth.Enabled = false;

                Label1.Text = "This documnet is not authorized";
            }
            else
            {
                ddlDocAuth.Enabled = false;
                ddlDocAuth.SelectedIndex = 1;
            }


            DataSet dsradio = dml.Find("select RadioButton from SET_DocRadioBinding where DocId= '" + ddlPODocName.SelectedItem.Value + "' and Record_Deleted = 0 and IsActive = 1;");

            if (dsradio.Tables[0].Rows.Count > 0)
            {

                string d_p = dsradio.Tables[0].Rows[0]["RadioButton"].ToString();
                if (d_p == "DIRECT")
                {
                    chkDirect.Checked = true;
                    chkPRNo.Checked = false;
                    chkDirect.Enabled = false;
                    chkPRNo.Enabled = false;
                    chkDirect_CheckedChanged(sender, e);

                }
                if (d_p == "NORMAL")
                {
                    chkPRNo.Checked = true;
                    chkDirect.Checked = false;
                    chkDirect.Enabled = false;
                    chkPRNo.Enabled = false;
                    chkPRNo_CheckedChanged(sender, e);
                }
            }
            else
            {
                chkDirect.Enabled = false;
                chkPRNo.Enabled = false;
                chkPRNo.Checked = false;
                chkDirect.Checked = false;

                if (chkDirect.Checked == false && chkPRNo.Checked == false)
                {
                    lstFruits.Enabled = true;
                    DataSet dspr = dml.Find("select Sno,RequisitionNo from V_PurchaseOrder_NocHeck where GocID='" + gocid() + "'and CompId='" + compid() + "' and BranchId = '" + branchId() + "' and FiscalYearID = '" + FiscalYear() + "' and  EntryDate<= '" + txtEntryDate.Text + "'");
                    if (dspr.Tables[0].Rows.Count > 0)
                    {

                        lstFruits.DataSource = dspr.Tables[0];
                        lstFruits.DataBind();
                    }
                }

            }


        }
        else
        {
            ddlSupplier.Enabled = false;
            ddlDocAuth.Enabled = false;
            ddlbilltodeprt.Enabled = false;
            ddlmodeofpay.Enabled = false;
            ddlStatus.Enabled = false;
            txtEntryDate.Enabled = false;
            chkPRNo.Enabled = false;
            ddlShipVia.Enabled = false;
            txtSuppRefNo.Enabled = false;
            ddlFreightTerms.Enabled = false;
            txtDeliDuedays.Enabled = false;
            txtDeliveryDate.Enabled = false;
            txtforceclosed.Enabled = false;
            ddlFCurrency.Enabled = false;
            ddlFCRate.Enabled = false;
            txtTotalFCVal.Enabled = false;
            txtTotalLocalCurVal.Enabled = false;
            txtAdvaceAgaPO.Enabled = false;
            txtCreateddate.Enabled = false;
            txtUpdateDate.Enabled = false;
            txtRemarks.Enabled = false;
            chkActive.Enabled = false;
            chkDirect.Checked = false;

            chkActive.Checked = false;
            chkDirect.Enabled = false;

            chkDirect.Checked = false;
            chkPRNo.Checked = false;
        }
    }
    protected void chkPRNo_CheckedChanged(object sender, EventArgs e)
    {
        chkDirect.Checked = false;
        Div2.Visible = false;
        lstFruits.Items.Clear();
        //select * from SET_StockRequisitionDetail where BalanceQty > 0 and StockReqMId in (select Sno from SET_StockRequisitionMaster where Status != 2)
        if (chkPRNo.Checked == false)
        {
            Div2.Visible = false;
            if (chkDirect.Checked == false && chkPRNo.Checked == false)
            {
                lstFruits.Enabled = true;
                DataSet dspr = dml.Find("select Sno, RequisitionNo from V_NoCheckPOForAQ where GocID='" + gocid() + "'and CompId='" + compid() + "' and BranchId = '" + branchId() + "' and FiscalYearID = '" + FiscalYear() + "' and  EntryDate<= '" + txtEntryDate.Text + "'");
                if (dspr.Tables[0].Rows.Count > 0)
                {
                    lstFruits.DataSource = dspr.Tables[0];
                    lstFruits.DataBind();
                }
            }
        }
        else {
            lstFruits.Enabled = true;
            DataSet ds = dml.Find("select Sno, RequisitionNo from V_PurchaseOrder_DirectPRNo where RequistionDate<= '" + txtEntryDate.Text + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstFruits.DataSource = ds.Tables[0];
                lstFruits.DataBind();
            }
        }
    }
    protected void lstFruits_SelectedIndexChanged(object sender, EventArgs e)
    {
        Div1.Visible = false;
        Div2.Visible = false;
        Div3.Visible = false;
        Div4.Visible = false;
        int count = 0;
        string reqno = "";
        foreach (ListItem item in lstFruits.Items)
        {

            if (item.Selected)
            {
                if (count == 0)
                {
                    reqno += reqno + "'" + item.Text + "'";
                    count = count + 1;
                }
                else
                {
                    reqno += "," + "'" + item.Text + "'";
                }
            }


        }

        string q;
        if (chkDirect.Checked == false && chkPRNo.Checked == false)
        {

            if (count > 0)
            {
                q = "select * from View_AQReqPO where RequisitionNo in (" + reqno + ") and StatusId != '2';";
            }
            else
            {
                q = "select * from View_AQReqPO where RequisitionNo in ('0') and StatusId != '2' and BalanceQty > 0;";
            }
        }
        else
        {
            if (count > 0)
            {

                q = "select * from V_AFQForPO where RequisitionNo in (" + reqno + ") and Status != '2' and BalanceQty > 0;";
            }
            else
            {
                q = "select * from V_AFQForPO where RequisitionNo in ('0') and Status != '2' and BalanceQty > 0;";
            }
        }


        DataSet ds = dml.grid(q);


        if (ds.Tables[0].Rows.Count > 0)
        {
            if (chkDirect.Checked == false && chkPRNo.Checked == false)
            {
                Div1.Visible = true;
                ddlSupplier.ClearSelection();
                if (ddlSupplier.Items.FindByText(ds.Tables[0].Rows[0]["BPartnerName"].ToString()) != null)
                {
                    ddlSupplier.Items.FindByText(ds.Tables[0].Rows[0]["BPartnerName"].ToString()).Selected = true;
                }
                GridView5.DataSource = ds;
                GridView5.DataBind();
            }

            if (chkPRNo.Checked == true)
            {
                Div4.Visible = true;
                GridView7.DataSource = ds;
                GridView7.DataBind();
            }
            if (chkDirect.Checked == true)
            {
                Div1.Visible = false;
                GridView5.DataSource = ds;
                GridView5.DataBind();
            }

            foreach (GridViewRow row in GridView5.Rows)
            {
                DropDownList ddlcostcentr = row.FindControl("ddlCostCenter") as DropDownList;
                dml.dropdownsql(ddlcostcentr, "SET_CostCenter", "CostCenterName", "CostCenterID");

                //select LocId,LocName from Set_Location
                DropDownList ddlLocation = row.FindControl("ddlLocation") as DropDownList;
                dml.dropdownsql(ddlLocation, "Set_Location", "LocName", "LocId");

                DropDownList ddluom2 = row.FindControl("ddlUOM2") as DropDownList;
                dml.dropdownsql(ddluom2, "SET_UnitofMeasure", "UOMName", "UOMID");


            }
            foreach (GridViewRow row in GridView7.Rows)
            {
                DropDownList ddlcostcentr = row.FindControl("ddlCostCenter") as DropDownList;
                dml.dropdownsql(ddlcostcentr, "SET_CostCenter", "CostCenterName", "CostCenterID");

                //select LocId,LocName from Set_Location
                DropDownList ddlLocation = row.FindControl("ddlLocation") as DropDownList;
                dml.dropdownsql(ddlLocation, "Set_Location", "LocName", "LocId");
                //select UOMID, UOMName from SET_UnitofMeasure

                DropDownList ddluom2 = row.FindControl("ddlUOM2") as DropDownList;
                dml.dropdownsql(ddluom2, "SET_UnitofMeasure", "UOMName", "UOMID");

            }



        }
        else
        {
            GridView5.ShowHeaderWhenEmpty = true;
            GridView5.EmptyDataText = "NO RECORD";
            GridView5.DataBind();
        }
        Label1.Text = "";
    }
    protected void chkDirect_CheckedChanged(object sender, EventArgs e)
    {
        chkPRNo.Checked = false;

        if (chkDirect.Checked == false)
        {
            Div2.Visible = false;
            if (chkDirect.Checked == false && chkPRNo.Checked == false)
            {
                lstFruits.Enabled = true;
                DataSet dspr = dml.Find("select Sno,RequisitionNo from V_NoCheckPOForAQ where  GocID='" + gocid() + "'and CompId='" + compid() + "' and BranchId = '" + branchId() + "' and FiscalYearID = '" + FiscalYear() + "' and EntryDate<= '" + txtEntryDate.Text + "'");
                if (dspr.Tables[0].Rows.Count > 0)
                {
                    lstFruits.DataSource = dspr.Tables[0];
                    lstFruits.DataBind();
                }
            }
        }
        else
        {
            lstFruits.ClearSelection();
            lstFruits.DataSource = null;
            lstFruits.DataBind();
            lstFruits.Enabled = false;

            Div2.Visible = true;

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[16]
            {
                new DataColumn("ItemSubHeadName"),
                new DataColumn("Description"),
                new DataColumn("UOM"),
                new DataColumn("Qty"),
                new DataColumn("ApprovedQty"),
                new DataColumn("Rate"),
                new DataColumn("GST"),
                new DataColumn("GSTRate"),
                new DataColumn("GrossValue"),
                new DataColumn("GrossValue1"),
                new DataColumn("CostCenter"),
                new DataColumn("Location"),
                new DataColumn("Project"),
                new DataColumn("UOM2"),
                new DataColumn("Qty2"),
                new DataColumn("Rate2")
            });


            ViewState["Customers"] = dt;


            this.PopulateGridview();
            DropDownList ddlsub = GridView6.FooterRow.FindControl("ddlitemsub") as DropDownList;
            dml.dropdownsql(ddlsub, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");

            DropDownList ddlmaster = GridView6.FooterRow.FindControl("ddlitemMaster") as DropDownList;
            dml.dropdownsql(ddlmaster, "SET_ItemMaster", "Description", "ItemID");

            DropDownList ddluom = GridView6.FooterRow.FindControl("ddlUomFooter") as DropDownList;
            dml.dropdownsql(ddluom, "SET_UnitofMeasure", "UOMName", "UOMID");

            DropDownList ddlsupFooter = GridView6.FooterRow.FindControl("lblCostCenterFooter") as DropDownList;
            dml.dropdownsql(ddlsupFooter, "SET_CostCenter", "CostCenterName", "CostCenterID");

            DropDownList ddluom2 = GridView6.FooterRow.FindControl("ddlUOM2Footer") as DropDownList;
            dml.dropdownsql(ddluom2, "SET_UnitofMeasure", "UOMName", "UOMID");

            DropDownList ddlloc = GridView6.FooterRow.FindControl("ddlLocationFooter") as DropDownList;
            dml.dropdownsql(ddlloc, "Set_Location", "LocName", "LocId");
        }
    }
    protected void GridView6_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("AddNew"))
            {
                foreach (GridViewRow g in GridView6.Rows)
                {
                    Label ll = (Label)g.FindControl("lblItemSubHeadName");
                    Label l = (Label)g.FindControl("lblDescription");

                    string ItemSubHeadName = (GridView6.FooterRow.FindControl("ddlitemsub") as DropDownList).SelectedItem.Text;
                    string Description = (GridView6.FooterRow.FindControl("ddlitemMaster") as DropDownList).SelectedItem.Text;
                    string UOM = (GridView6.FooterRow.FindControl("ddlUomFooter") as DropDownList).SelectedItem.Text;
                    string Qty = (GridView6.FooterRow.FindControl("txtQtyFooter") as TextBox).Text;
                    string ApprovedQty = (GridView6.FooterRow.FindControl("txtApprovedQtyFooter") as TextBox).Text;
                    string Rate = (GridView6.FooterRow.FindControl("txtRateFooter") as TextBox).Text;
                    string GST = (GridView6.FooterRow.FindControl("lblGSTFooter") as TextBox).Text;
                    //string GSTRate = (GridView6.FooterRow.FindControl("lblGSTRateFooter") as TextBox).Text;
                    string GrossValue = (GridView6.FooterRow.FindControl("lblGrossValueFooter") as TextBox).Text;
                    string GrossValue1 = (GridView6.FooterRow.FindControl("lblfloatgV") as TextBox).Text;
                    string CostCenter = (GridView6.FooterRow.FindControl("lblCostCenterFooter") as DropDownList).SelectedItem.Text;
                    string Location = (GridView6.FooterRow.FindControl("ddlLocationFooter") as DropDownList).SelectedItem.Text;
                    string Project = (GridView6.FooterRow.FindControl("lblProjectFooter") as TextBox).Text;
                    string UOM2 = (GridView6.FooterRow.FindControl("ddlUOM2Footer") as DropDownList).SelectedItem.Text;
                    string Qty2 = (GridView6.FooterRow.FindControl("lblQty2Footer") as TextBox).Text;
                    string Rate2 = (GridView6.FooterRow.FindControl("lblRate2Footer") as TextBox).Text;

                    float GSTRate = float.Parse(Rate) * (float.Parse(GST) / 100);



                    if (ll.Text == ItemSubHeadName && l.Text == Description)
                    {
                        Label1.Text = "Already inseted";
                    }
                    else {
                        Label1.Text = "";
                        DataTable dt = (DataTable)ViewState["Customers"];

                        if (ViewState["flag"].ToString() == "true")
                        {
                            dt.Rows[0].Delete();
                            ViewState["flag"] = "false";
                        }



                        //(ItemSubHeadName, Description, UOM, Qty, ApprovedQty, Rate, GST, GSTRate, GrossValue, CostCenter,Location,Project,UOM2,Qty2,Rate2)
                        dt.Rows.Add(ItemSubHeadName, Description, UOM, Qty, ApprovedQty, Rate, GST, GSTRate.ToString(), GrossValue, GrossValue1, CostCenter, Location, Project, UOM2, Qty2, Rate2);

                        ViewState["Customers"] = dt;
                        this.PopulateGridview();
                        DropDownList ddlsub = GridView6.FooterRow.FindControl("ddlitemsub") as DropDownList;
                        dml.dropdownsql(ddlsub, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");


                        DropDownList ddlmaster = GridView6.FooterRow.FindControl("ddlitemMaster") as DropDownList;
                        dml.dropdownsql(ddlmaster, "SET_ItemMaster", "Description", "ItemID");

                        DropDownList ddluom = GridView6.FooterRow.FindControl("ddlUomFooter") as DropDownList;
                        dml.dropdownsql(ddluom, "SET_UnitofMeasure", "UOMName", "UOMID");

                        DropDownList ddlsupFooter = GridView6.FooterRow.FindControl("lblCostCenterFooter") as DropDownList;
                        dml.dropdownsql(ddlsupFooter, "SET_CostCenter", "CostCenterName", "CostCenterID");

                        DropDownList ddluom2 = GridView6.FooterRow.FindControl("ddlUOM2Footer") as DropDownList;
                        dml.dropdownsql(ddluom2, "SET_UnitofMeasure", "UOMName", "UOMID");

                        DropDownList ddlloc = GridView6.FooterRow.FindControl("ddlLocationFooter") as DropDownList;
                        dml.dropdownsql(ddlloc, "Set_Location", "LocName", "LocId");


                        float totallocal = 0;
                        if (txtTotalLocalCurVal.Text == "")
                        {
                            txtTotalLocalCurVal.Text = GrossValue1;
                        }
                        else
                        {
                            float localTo = float.Parse(txtTotalLocalCurVal.Text);
                            //totallocal = localTo + float.Parse(GrossValue1);
                            txtTotalLocalCurVal.Text = localTo.ToString();
                        }


                    }
                }
            }
        }
        catch (Exception ex)
        {
            // lblSuccessMessage.Text = "";
            //lblErrorMessage.Text = ex.Message;
        }
    }
    protected void ddlitemsub_SelectedIndexChanged(object sender, EventArgs e)
    {
        //select * from SET_ItemMaster where ItemSubHeadID= 3
        DropDownList ddlsubitem = GridView6.FooterRow.FindControl("ddlitemsub") as DropDownList;
        DropDownList ddlmaster = GridView6.FooterRow.FindControl("ddlitemMaster") as DropDownList;
        dml.dropdownsql(ddlmaster, "SET_ItemMaster", "Description", "ItemID", "ItemSubHeadID", ddlsubitem.SelectedItem.Value);
    }
    protected void ddlitemMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlmaster = GridView6.FooterRow.FindControl("ddlitemMaster") as DropDownList;
        if (ddlmaster.SelectedIndex != 0)
        {
            //
            string valitem = "";
            string valitem1 = "";
            DropDownList ddluomm = GridView6.FooterRow.FindControl("ddlUomFooter") as DropDownList;
            dml.dropdownsqlwithquery(ddluomm, "select UOMID,UOMName from SET_UnitofMeasure where UOMID in ((SELECT UOMId as uom FROM SET_ItemMaster where ItemID = '" + ddlmaster.SelectedItem.Value + "' ) UNION (SELECT UOMId2 as uom FROM SET_ItemMaster where ItemID = '" + ddlmaster.SelectedItem.Value + "' ))", "UOMName", "UOMID");


            DataSet ds = dml.Find("select UOMId,UOMId2 from SET_ItemMaster where Description = '" + ddlmaster.SelectedItem.Text + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {

                valitem = ds.Tables[0].Rows[0]["UOMId"].ToString();
                valitem1 = ds.Tables[0].Rows[0]["UOMId2"].ToString();
            }
            if (valitem != "")
            {
                ddluomm.ClearSelection();
                if (ddluomm.Items.FindByValue(valitem) != null)
                {
                    ddluomm.Items.FindByValue(valitem).Selected = true;
                }
            }
            else
            {
                ddluomm.ClearSelection();
                if (ddluomm.Items.FindByValue(valitem1) != null)
                {
                    ddluomm.Items.FindByValue(valitem1).Selected = true;
                }
            }

        }
    }
    protected void lblGSTFooter_TextChanged(object sender, EventArgs e)
    {
        string Rate = (GridView6.FooterRow.FindControl("txtRateFooter") as TextBox).Text;
        string GST = (GridView6.FooterRow.FindControl("lblGSTFooter") as TextBox).Text;
        string Qty = (GridView6.FooterRow.FindControl("txtApprovedQtyFooter") as TextBox).Text;


        float GSTRATE = float.Parse(Rate) * (float.Parse(GST) / 100);
        (GridView6.FooterRow.FindControl("lblGSTRateFooter") as TextBox).Text = GSTRATE.ToString();


        // Qty Value = Quantity x Rate
        //GST Value = Quantity x GST Rate
        //Gross Value = Quantity Value + GSt Value
        float QtyValue = float.Parse(Qty) * float.Parse(Rate);
        float GSTValue = float.Parse(Qty) * GSTRATE;


        float GrossValue = QtyValue + GSTValue;


        decimal parsed = decimal.Parse(GrossValue.ToString(), CultureInfo.InvariantCulture);
        CultureInfo hindi = new CultureInfo("ur-PK");
        string rsform = string.Format(hindi, "{0:c}", parsed);


        (GridView6.FooterRow.FindControl("lblGrossValueFooter") as TextBox).Text = rsform;

        (GridView6.FooterRow.FindControl("lblfloatgV") as TextBox).Text = GrossValue.ToString();

    }
    public static string CurrencyConvert(decimal amount, string fromCurrency, string toCurrency)
    {

        //Grab your values and build your Web Request to the API
        string apiURL = String.Format("https://www.google.com/finance/converter?a={0}&from={1}&to={2}&meta={3}", amount, fromCurrency, toCurrency, Guid.NewGuid().ToString());

        //Make your Web Request and grab the results
        var request = WebRequest.Create(apiURL);

        //Get the Response
        var streamReader = new StreamReader(request.GetResponse().GetResponseStream(), System.Text.Encoding.ASCII);

        //Grab your converted value (ie 2.45 USD)
        var result = Regex.Matches(streamReader.ReadToEnd(), "<span class=\"?bld\"?>([^<]+)</span>")[0].Groups[1].Value;

        //Get the Result
        return result;
    }
    public void detaisave(string id)
    {
        //INSERT INTO Set_QuotationReqDetail ([Sno], [ItemSubHead], [ItemMaster], [UOM], [Quantity], [Supplier], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [Record_Deleted]) VALUES ('7', 'PACKING MATERIAL', 'Air Compresser (Assets)', '3', '6', 'ATTOCK TRADERS - TD', '1', '1', '1', '5', '3', 'Fahad Siddiqui', '2020-03-04 11:34:39.5248', NULL, NULL, '0');
        for (int a = 0; a < GridView6.Rows.Count; a++)
        {
            // CheckBox chksel = (CheckBox)GridView6.Rows[a].FindControl("chkSelect");
            Label lblItemSubHeadName = (Label)GridView6.Rows[a].FindControl("lblItemSubHeadName");
            Label lblDescription = (Label)GridView6.Rows[a].FindControl("lblDescription");
            Label lblUom = (Label)GridView6.Rows[a].FindControl("lblUom");
            Label lblQty = (Label)GridView6.Rows[a].FindControl("lblQty");
            Label lblApprovedQty = (Label)GridView6.Rows[a].FindControl("lblApprovedQty");
            Label lblRate = (Label)GridView6.Rows[a].FindControl("lblRate");
            Label lblGST = (Label)GridView6.Rows[a].FindControl("lblGST");
            Label lblGSTRate = (Label)GridView6.Rows[a].FindControl("lblGSTRate");
            Label lblGrossValue = (Label)GridView6.Rows[a].FindControl("lblGrossValue");
            Label lblGrossValue1 = (Label)GridView6.Rows[a].FindControl("lblFGV");

            Label lblCostCenter = (Label)GridView6.Rows[a].FindControl("lblCostCenter");
            Label lblLocation = (Label)GridView6.Rows[a].FindControl("lblLocation");
            Label lblProject = (Label)GridView6.Rows[a].FindControl("lblProject");
            Label lblUOM2 = (Label)GridView6.Rows[a].FindControl("lblUOM2");
            Label lblQty2 = (Label)GridView6.Rows[a].FindControl("lblQty2");
            Label lblRate2 = (Label)GridView6.Rows[a].FindControl("lblRate2");


            DataSet ds = dml.Find("select ItemSubHeadID, ItemSubHeadName from SET_ItemSubHead where ItemSubHeadName = '" + lblItemSubHeadName.Text + "'");
            string subhead, itemmaster, uom1, costcenter, location;
            if (ds.Tables[0].Rows.Count > 0)
            {
                subhead = ds.Tables[0].Rows[0]["ItemSubHeadID"].ToString();
            }
            else
            {
                subhead = "0";
            }
            DataSet ds1 = dml.Find("select  ItemID , Description from SET_ItemMaster where Description = '" + lblDescription.Text + "'");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                itemmaster = ds1.Tables[0].Rows[0]["ItemID"].ToString();
            }
            else
            {
                itemmaster = "0";
            }
            DataSet ds2 = dml.Find("select  UOMID,UOMName from SET_UnitofMeasure where UOMName = '" + lblUom.Text + "'");
            if (ds2.Tables[0].Rows.Count > 0)
            {
                uom1 = ds2.Tables[0].Rows[0]["UOMID"].ToString();
            }
            else
            {
                uom1 = "0";
            }

            DataSet ds3 = dml.Find("select  CostCenterID,CostCenterName from SET_CostCenter where CostCenterName = '" + lblCostCenter.Text + "'");
            if (ds3.Tables[0].Rows.Count > 0)
            {
                costcenter = ds3.Tables[0].Rows[0]["CostCenterID"].ToString();
            }
            else
            {
                costcenter = "0";
            }
            DataSet ds4 = dml.Find("select LocId,LocName from Set_Location where LocName = '" + lblLocation.Text + "'");
            if (ds4.Tables[0].Rows.Count > 0)
            {
                location = ds3.Tables[0].Rows[0]["CostCenterID"].ToString();
            }
            else
            {
                location = "0";
            }
            float balq = float.Parse(lblQty.Text) - float.Parse(lblApprovedQty.Text);

            //            Qty Value = Quantity x Rate
            //GST Value = Quantity x GST Rate


            float qtyval = float.Parse(lblQty.Text) * float.Parse(lblRate.Text);
            float gstval = float.Parse(lblQty.Text) * float.Parse(lblGSTRate.Text);

            DataSet BranchData = dml.Find("select FinancialRoundOff,DisplayDigit from Set_Branch where BranchId =" + Convert.ToInt32(Request.Cookies["BranchId"].Value));
            var DisplayDigit = Convert.ToInt32(BranchData.Tables[0].Rows[0]["DisplayDigit"].ToString());
            var FinancialRoundOff = Convert.ToInt32(BranchData.Tables[0].Rows[0]["FinancialRoundOff"].ToString());
            if (DisplayDigit > 0)
            {
                qtyval = utl.RoundOffToFloat(DisplayDigit, FinancialRoundOff, qtyval.ToString());
                lblGrossValue.Text = utl.RoundOff(DisplayDigit, FinancialRoundOff, lblGrossValue.Text);
                
            }
            dml.Insert("INSERT INTO Set_PurchaseOrderDetail ([Sno_Master], [PRNo_AQno], [ItemSubHead], [ItemMaster], [UOM], [Quantity], [Rate], [GST], [GSTRate], [QtyValue], [GstValue], [GrossValue], [Remarts], [Qty2], [UOM2], [Rate2], [Location], [Project], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted],[ApprovedQuantity],[CostCenter],[balQty]) VALUES ('" + id + "', NULL, '" + subhead + "', '" + itemmaster + "', '" + uom1 + "', '" + lblApprovedQty.Text + "','" + lblRate.Text + "', '" + lblGST.Text + "', '" + lblGSTRate.Text + "', '" + qtyval + "', '" + gstval + "', '" + lblGrossValue1.Text + "', NULL, '" + lblQty2.Text + "', '" + uom1 + "', '" + lblRate2.Text + "', '" + location + "', '" + lblProject.Text + "', '1'," + gocid() + "," + compid() + "," + branchId() + "," + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyy hh:mm:ss.ffff") + "', 0,'" + lblApprovedQty.Text + "','" + costcenter + "','" + lblApprovedQty.Text + "');", "");
        }
    }
    public void detaisaveForPR(string id)
    {
        for (int a = 0; a < GridView7.Rows.Count; a++)
        {          //lbldetailSno  chkSelect
            CheckBox chkSelect = (CheckBox)GridView7.Rows[a].FindControl("chkSelect");
            Label lbldetailSno = (Label)GridView7.Rows[a].FindControl("lbldetailSno");
            Label lblreq = (Label)GridView7.Rows[a].FindControl("lblreq");
            Label lblItemSubHeadName = (Label)GridView7.Rows[a].FindControl("lblsubhead");
            Label lblDescription = (Label)GridView7.Rows[a].FindControl("lblitemmaster");
            Label lblUom = (Label)GridView7.Rows[a].FindControl("lbluom");
            Label lblQty = (Label)GridView7.Rows[a].FindControl("lblQty");
            TextBox lblApprovedQty = (TextBox)GridView7.Rows[a].FindControl("txtApprovedQty");
            TextBox lblRate = (TextBox)GridView7.Rows[a].FindControl("txtRate_PR");
            TextBox lblGST = (TextBox)GridView7.Rows[a].FindControl("txtGST_PR");
            Label lblGSTRate = (Label)GridView7.Rows[a].FindControl("lblGSTRate");
            Label lblQtyVal = (Label)GridView7.Rows[a].FindControl("lblQtyVal");
            Label lblGrossValue = (Label)GridView7.Rows[a].FindControl("lblGrossValue");
            Label lblGstValue = (Label)GridView7.Rows[a].FindControl("lblGstValue");
            DropDownList lblCostCenter = (DropDownList)GridView7.Rows[a].FindControl("ddlCostCenter");
            DropDownList lblLocation = (DropDownList)GridView7.Rows[a].FindControl("ddlLocation");
            TextBox lblProject = (TextBox)GridView7.Rows[a].FindControl("txtproject");
            DropDownList lblUOM2 = (DropDownList)GridView7.Rows[a].FindControl("ddlUOM2");
            TextBox lblQty2 = (TextBox)GridView7.Rows[a].FindControl("lblQty2");
            Label lblRate2 = (Label)GridView7.Rows[a].FindControl("lblRate2");

            if (chkSelect.Checked == true)
            {
                DataSet ds = dml.Find("select ItemSubHeadID, ItemSubHeadName from SET_ItemSubHead where ItemSubHeadName = '" + lblItemSubHeadName.Text + "'");
                string subhead, itemmaster, uom1, costcenter, location, uom2;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    subhead = ds.Tables[0].Rows[0]["ItemSubHeadID"].ToString();
                }
                else
                {
                    subhead = "0";
                }
                DataSet ds1 = dml.Find("select  ItemID , Description from SET_ItemMaster where Description = '" + lblDescription.Text + "'");
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    itemmaster = ds1.Tables[0].Rows[0]["ItemID"].ToString();
                }
                else
                {
                    itemmaster = "0";
                }
                DataSet ds2 = dml.Find("select  UOMID,UOMName from SET_UnitofMeasure where UOMName = '" + lblUom.Text + "'");
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
                DataSet ds4 = dml.Find("select LocId,LocName from Set_Location where LocName = '" + lblLocation.SelectedItem.Text + "'");
                if (ds4.Tables[0].Rows.Count > 0)
                {
                    location = ds4.Tables[0].Rows[0]["LocId"].ToString();
                }
                else
                {
                    location = "0";
                }

                DataSet ds5 = dml.Find("select  UOMID,UOMName from SET_UnitofMeasure where UOMName = '" + lblUOM2.SelectedItem.Text + "'");
                if (ds5.Tables[0].Rows.Count > 0)
                {
                    uom2 = ds5.Tables[0].Rows[0]["UOMID"].ToString();
                }
                else
                {
                    uom2 = "0";
                }

                float balq = float.Parse(lblQty.Text) - float.Parse(lblApprovedQty.Text);
                dml.Update("update Set_QuotationReqDetail set BalanceQty = '" + balq.ToString() + "' WHERE Sno = '" + lbldetailSno.Text + "'", "");
                string Mid = "";
                DataSet dd = dml.Find("select QuoatReqMId from Set_QuotationReqDetail WHERE Sno = '" + lbldetailSno.Text + "'");
                if (dd.Tables[0].Rows.Count > 0)
                {
                    Mid = dd.Tables[0].Rows[0]["QuoatReqMId"].ToString();
                }
                else
                {
                    Mid = "0";
                }

                DataSet dstotalbal = dml.Find("select sum(BalanceQty) as totalbal from Set_QuotationReqDetail where QuoatReqMId ='" + Mid + "'");
                if (dstotalbal.Tables[0].Rows.Count > 0)
                {
                    float totalbalQty = float.Parse(dstotalbal.Tables[0].Rows[0]["totalbal"].ToString());
                    if (totalbalQty == 0)
                    {
                        dml.Update("UPDATE Set_QuotationReqMaster set Status = '2' where Sno = '" + Mid + "'", "");
                        dml.Update("UPDATE SET_StockRequisitionMaster set Status = '2' where RequisitionNo = '" + lblreq.Text + "'", "");
                    }
                    else if (totalbalQty > 0)
                    {
                        dml.Update("UPDATE Set_QuotationReqMaster set Status = '8' where Sno = '" + Mid + "'", "");
                    }

                }

                DataSet BranchDataSet = dml.Find("Select DisplayDigit From SetBranch Where BranchId=" + Convert.ToInt32(Request.Cookies["BranchId"].Value));
                var DisplayDigit = Convert.ToInt32(BranchDataSet.Tables[0].Rows[0]["DisplayDigit"].ToString());
                if (DisplayDigit > 0)
                {
                    string[] array = lblGrossValue.Text.Split(new char[] { '.' });
                    int decimals = array[1].Length;
                    if (decimals > DisplayDigit)
                    {
                        lblGrossValue.Text = Convert.ToString(Math.Round(Convert.ToDouble(lblGrossValue.Text.ToString()), DisplayDigit));
                    }
                    array = lblQty.Text.Split(new char[] { '.' });
                    decimals = array[1].Length;
                    if (decimals > DisplayDigit)
                    {
                        lblQty.Text = Convert.ToString(Math.Round(Convert.ToDouble(lblQty.Text.ToString()), DisplayDigit));
                    }
                }

                dml.Insert("INSERT INTO Set_PurchaseOrderDetail ([Sno_Master], [PRNo_AQno], [ItemSubHead], [ItemMaster], [UOM], [Quantity], [Rate], [GST], [GSTRate], [QtyValue], [GstValue], [GrossValue], [Remarts], [Qty2], [UOM2], [Rate2], [Location], [Project], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted],[ApprovedQuantity],[CostCenter],[balQty]) VALUES ('" + id + "', '" + lblreq.Text + "', '" + subhead + "', '" + itemmaster + "', '" + uom1 + "', '" + lblApprovedQty.Text + "','" + lblRate.Text + "', '" + lblGST.Text + "', '" + lblGSTRate.Text + "', '" + lblQtyVal.Text + "', '" + lblGstValue.Text + "', '" + lblGrossValue.Text + "', NULL, '" + lblQty2.Text + "', '" + uom2 + "', '" + lblRate2.Text + "', '" + location + "', '" + lblProject.Text + "', '1', " + gocid() + ", " + compid() + ", " + branchId() + ", " + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyy hh:mm:ss.ffff") + "', 0,'" + lblApprovedQty.Text + "','" + costcenter + "','" + balq + "');", "");
            }
        }
    }
    public void detaisaveforAFQ(string id)
    {

        for (int a = 0; a < GridView5.Rows.Count; a++)
        {
            CheckBox chkSelect = (CheckBox)GridView5.Rows[a].FindControl("chkSelect");
            Label lbldetailSno = (Label)GridView5.Rows[a].FindControl("lbldetailSno");
            Label lblreq = (Label)GridView5.Rows[a].FindControl("lblreq");

            Label lblItemSubHeadName = (Label)GridView5.Rows[a].FindControl("lblsubhead");
            Label lblDescription = (Label)GridView5.Rows[a].FindControl("lblitemmaster");
            Label lblUom = (Label)GridView5.Rows[a].FindControl("lbluom");
            Label lblQty = (Label)GridView5.Rows[a].FindControl("lblQty");
            TextBox txtApprovedQty = (TextBox)GridView5.Rows[a].FindControl("txtApprovedQty");
            TextBox txtRate = (TextBox)GridView5.Rows[a].FindControl("txtRate");
            TextBox txtGST = (TextBox)GridView5.Rows[a].FindControl("txtGST");
            Label lblGSTRate = (Label)GridView5.Rows[a].FindControl("lblGSTRate");
            Label lblGrossValue = (Label)GridView5.Rows[a].FindControl("lblGrossValue");
            DropDownList lblCostCenter = (DropDownList)GridView5.Rows[a].FindControl("ddlCostCenter");
            DropDownList lblLocation = (DropDownList)GridView5.Rows[a].FindControl("ddlLocation");
            TextBox lblProject = (TextBox)GridView5.Rows[a].FindControl("txtproject");
            DropDownList lblUOM2 = (DropDownList)GridView5.Rows[a].FindControl("ddlUOM2");
            TextBox lblQty2 = (TextBox)GridView5.Rows[a].FindControl("lblQty2A");
            Label lblRate2 = (Label)GridView5.Rows[a].FindControl("lblRate2");
            Label lblQtyVal = (Label)GridView5.Rows[a].FindControl("lblQtyVal");
            Label lblGstValue = (Label)GridView5.Rows[a].FindControl("lblGstValue");
            if (chkSelect.Checked == true)
            {


                DataSet ds = dml.Find("select ItemSubHeadID, ItemSubHeadName from SET_ItemSubHead where ItemSubHeadName = '" + lblItemSubHeadName.Text + "'");
                string subhead, itemmaster, uom1, costcenter, location, uom2;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    subhead = ds.Tables[0].Rows[0]["ItemSubHeadID"].ToString();
                }
                else
                {
                    subhead = "0";
                }
                DataSet ds1 = dml.Find("select  ItemID , Description from SET_ItemMaster where Description = '" + lblDescription.Text + "'");
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    itemmaster = ds1.Tables[0].Rows[0]["ItemID"].ToString();
                }
                else
                {
                    itemmaster = "0";
                }
                DataSet ds2 = dml.Find("select  UOMID,UOMName from SET_UnitofMeasure where UOMName = '" + lblUom.Text + "'");
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
                DataSet ds5 = dml.Find("select  UOMID,UOMName from SET_UnitofMeasure where UOMName = '" + lblUOM2.SelectedItem.Text + "'");
                if (ds5.Tables[0].Rows.Count > 0)
                {
                    uom2 = ds5.Tables[0].Rows[0]["UOMID"].ToString();
                }
                else
                {
                    uom2 = "0";
                }

                DataSet ds4 = dml.Find("select LocId,LocName from Set_Location where LocName = '" + lblLocation.SelectedItem.Text + "'");
                if (ds4.Tables[0].Rows.Count > 0)
                {
                    location = ds3.Tables[0].Rows[0]["CostCenterID"].ToString();
                }
                else
                {
                    location = "0";
                }

                float balq = float.Parse(lblQty.Text) - float.Parse(txtApprovedQty.Text);

                dml.Update("update SuppQuotApprDetail set BalanceQty = '" + balq.ToString() + "' WHERE Sno= '" + lbldetailSno.Text + "'", "");
                string Mid = "";
                DataSet dd = dml.Find("select Sno_Master from SuppQuotApprDetail WHERE Sno = '" + lbldetailSno.Text + "'");
                if (dd.Tables[0].Rows.Count > 0)
                {
                    Mid = dd.Tables[0].Rows[0]["Sno_Master"].ToString();
                }
                else
                {
                    Mid = "0";
                }
                DataSet dstotalbal = dml.Find("select sum(BalanceQty) as totalbal from SuppQuotApprDetail where Sno_Master ='" + Mid + "'");
                if (dstotalbal.Tables[0].Rows.Count > 0)
                {
                    float totalbalQty = float.Parse(dstotalbal.Tables[0].Rows[0]["totalbal"].ToString());
                    if (totalbalQty == 0)
                    {
                        dml.Update("UPDATE SuppQuotationApprMaster set StatusId = '2' where  Sno = '" + Mid + "' ", "");
                    }
                    else if (totalbalQty > 0)
                    {
                        dml.Update("UPDATE SuppQuotationApprMaster set StatusId = '8' where  Sno = '" + Mid + "'", "");
                    }
                }

                DataSet BranchDataSet = dml.Find("Select * From SetBranch Where BranchId="+Convert.ToInt32(Request.Cookies["BranchId"].Value));
                var DisplayDigit = Convert.ToInt32(BranchDataSet.Tables[0].Rows[0]["DisplayDigit"].ToString());
                var FinancialRoundOff = Convert.ToInt32(BranchDataSet.Tables[0].Rows[0]["FinancialRoundOff"].ToString());

                if (DisplayDigit > 0)
                {
                    lblGrossValue.Text = utl.RoundOff(DisplayDigit,FinancialRoundOff,lblGrossValue.Text);
                    lblQty.Text = utl.RoundOff(DisplayDigit,FinancialRoundOff,lblQty.Text);
                }

                dml.Insert("INSERT INTO Set_PurchaseOrderDetail ([Sno_Master], [PRNo_AQno], [ItemSubHead], [ItemMaster], [UOM], [Quantity], [Rate], [GST], [GSTRate], [QtyValue], [GstValue], [GrossValue], [Remarts], [Qty2], [UOM2], [Rate2], [Location], [Project], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted],[ApprovedQuantity],[CostCenter],[balQty]) VALUES ('" + id + "', '" + lstFruits.SelectedItem.Text + "', '" + subhead + "', '" + itemmaster + "', '" + uom1 + "', '" + txtApprovedQty.Text + "','" + txtRate.Text + "', '" + txtGST.Text + "', '" + lblGSTRate.Text + "', '" + lblQtyVal.Text + "', '" + lblGstValue.Text + "', '" + lblGrossValue.Text + "', NULL, '" + lblQty2.Text + "', '" + uom2 + "', '" + lblRate2.Text + "', '" + location + "', '" + lblProject.Text + "', '1', " + gocid() + ", " + compid() + ", " + branchId() + ", " + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyy hh:mm:ss.ffff") + "', 0,'" + txtApprovedQty.Text + "','" + costcenter + "','" + txtApprovedQty.Text + "');", "");
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
    protected void GridView7_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lblRowNumber") as Label).Text = (e.Row.RowIndex + 1).ToString();
        }
    }
    protected void lblQty2_TextChanged(object sender, EventArgs e)
    {

        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;
        string Qty2 = (row.FindControl("lblQty2") as TextBox).Text;
        string lblQtyVal = (row.FindControl("lblQtyVal") as Label).Text;

        float Rate2;
        if (Qty2 == "")
        {
            Rate2 = 0;
        }
        else
        {
            Rate2 = float.Parse(lblQtyVal) / float.Parse(Qty2);
        }
       (row.FindControl("lblRate2") as Label).Text = Rate2.ToString();




    }
    protected void lblQty2A_TextChanged(object sender, EventArgs e)
    {

        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;
        string Qty2 = (row.FindControl("lblQty2A") as TextBox).Text;
        string lblQtyVal = (row.FindControl("lblQtyVal") as Label).Text;

        float Rate2;
        if (Qty2 == "")
        {
            Rate2 = 0;
        }
        else
        {
            Rate2 = float.Parse(lblQtyVal) / float.Parse(Qty2);
        }
      (row.FindControl("lblRate2") as Label).Text = Rate2.ToString();




    }
    protected void ddlFCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        string localcurr = "0", rate = "0";

        DataSet dsbranch = dml.Find("select BaseCurrencyID from SET_Branch where BranchId = '" + branchId() + "'");
        if (dsbranch.Tables[0].Rows.Count > 0)
        {
            localcurr = dsbranch.Tables[0].Rows[0]["BaseCurrencyID"].ToString();
        }




        DataSet ds = dml.Find("select * from SET_CurrencyConversion  where CurrencyID = '" + ddlFCurrency.SelectedItem.Value + "'");
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ddlFCurrency.SelectedIndex != 0)
            {
                dml.dropdownsqlwithquery(ddlFCRate, "select * from SET_CurrencyConversion  where CurrencyID = '" + ddlFCurrency.SelectedItem.Value + "'", "Rate", "SerialNo");
                ddlFCRate.SelectedIndex = 1;
            }
        }
        else
        {
            dml.dropdownsql(ddlFCRate, "SET_CurrencyConversion", "Rate", "SerialNo");
            ddlFCRate.SelectedIndex = 0;

        }

        if (ddlFCurrency.SelectedItem.Value == localcurr)
        {
            txtTotalFCVal.Text = ddlFCRate.SelectedItem.Text;
        }

    }
    protected void GridView4_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView4.EditIndex = e.NewEditIndex;


        DataSet ds_detail = dml.Find("select * from ViewPODetail_FED where Sno_Master = '" + ViewState["SNO"].ToString() + "'");
        if (ds_detail.Tables[0].Rows.Count > 0)
        {
            Div3.Visible = true;
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
        TextBox lblcc = GridView4.Rows[GridView4.EditIndex].FindControl("txtLocation") as TextBox;

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

            string lblreq = (GridView4.Rows[e.RowIndex].FindControl("lblreq") as Label).Text;
            string txtQtyEdit = (GridView4.Rows[e.RowIndex].FindControl("txtQty") as TextBox).Text;
            string txtApprovedQtyEdit = (GridView4.Rows[e.RowIndex].FindControl("txtApprovedQty") as TextBox).Text;
            string txtRateEdit = (GridView4.Rows[e.RowIndex].FindControl("txtRate") as TextBox).Text;
            //string lblRate = (GridView4.Rows[e.RowIndex].FindControl("lblRate") as Label).Text;

            string lblGST = (GridView4.Rows[e.RowIndex].FindControl("txtGSTRate") as TextBox).Text;
            string txtGSTRate = (GridView4.Rows[e.RowIndex].FindControl("txtGST") as TextBox).Text;
            string txtgstvalue = "0";
            string txtqtyvalue = "0";

            string txtGrossValue = (GridView4.Rows[e.RowIndex].FindControl("txtGrossValue") as TextBox).Text;

            DropDownList ddlLocation = (GridView4.Rows[e.RowIndex].FindControl("ddlLocation") as DropDownList);

            string txtproject = (GridView4.Rows[e.RowIndex].FindControl("txtproject") as TextBox).Text;
            string txtUOM2Edit = (GridView4.Rows[e.RowIndex].FindControl("txtUOM2Edit") as TextBox).Text;
            string txtRate2Edit = (GridView4.Rows[e.RowIndex].FindControl("txtRate2Edit") as TextBox).Text;
            string txtQty2Edit = (GridView4.Rows[e.RowIndex].FindControl("txtQty2Edit") as TextBox).Text;
            string lblsno = (GridView4.Rows[e.RowIndex].FindControl("lbldetailSno") as Label).Text;
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
            dt.Rows[row.DataItemIndex]["GST"] = lblGST;
            dt.Rows[row.DataItemIndex]["GSTRate"] = txtGSTRate;
            dt.Rows[row.DataItemIndex]["GrossValue"] = txtGrossValue;
            dt.Rows[row.DataItemIndex]["LocName"] = ddlLocation.SelectedItem.Text;
            dt.Rows[row.DataItemIndex]["Project"] = txtproject;

            dt.Rows[row.DataItemIndex]["uom2name"] = txtUOM2Edit;
            dt.Rows[row.DataItemIndex]["Qty2"] = txtQty2Edit;
            dt.Rows[row.DataItemIndex]["Rate2"] = txtRate2Edit;

            float totallocal = 0;
            if (txtTotalLocalCurVal.Text == "")
            {
                txtTotalLocalCurVal.Text = txtGrossValue;
            }
            else
            {
                float localTo = float.Parse(txtTotalLocalCurVal.Text);
                totallocal = localTo + float.Parse(txtGrossValue);
                txtTotalLocalCurVal.Text = totallocal.ToString();
            }

            //dml.Update("UPDATE [SET_StockRequisitionDetail] SET [ItemSubHead]='" + txtitemsubfooter.SelectedItem.Value + "', [ItemMaster]='" + txtdesc.SelectedItem.Value + "', [CostCenter]='" + txtsupplierFooter.SelectedItem.Value + "', [UOM]='" + txtuomFooter.SelectedItem.Value + "', [Project]='" + txtProjectEdit + "', [CurrentStock]='" + txtcurrStockFooter + "', [RequiredQuantity]='" + txtReqStockFooter + "', [Remarks]=NULL, [IsActive]='1', [GocID]='" + gocid() + "', [CompId]='" + compid() + "', [BranchId]='" + branchId() + "', [FiscalYearID]='" + FiscalYear() + "', [UpdatedBy]='" + show_username() + "', [UpdatedDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', [Record_Deleted]='0' WHERE ([Sno]='" + lblsno + "');", "");
            dml.Update("UPDATE Set_PurchaseOrderDetail SET  [PRNo_AQno]='" + lblreq + "', [ItemSubHead]='" + ddlitemsubedit.SelectedItem.Value + "', [ItemMaster]='" + ddlitemMasteredit.SelectedItem.Value + "', [UOM]='" + ddluomedit.SelectedItem.Value + "', [Quantity]='" + txtqtyvalue + "', [Rate]='" + txtRateEdit + "', [GST]='" + lblGST + "', [GSTRate]='" + txtGSTRate + "', [QtyValue]='" + txtqtyvalue + "', [GstValue]='" + txtgstvalue + "', [GrossValue]='" + txtGrossValue + "', [Remarts]=NULL, [Qty2]='" + txtQty2Edit + "', [UOM2]='" + ddluomedit.SelectedItem.Value == null ? null : ddluomedit.SelectedItem.Value + "', [Rate2]='" + txtRate2Edit + "', [Location]='" + ddlLocation.SelectedItem.Value + "', [Project]='" + txtproject + "', [UpdatedBy]='" + show_username() + "', [UpdatedDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' , [ApprovedQuantity]='" + txtApprovedQtyEdit + "' WHERE ([Sno]='" + lblsno + "')", "");
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
    protected void GridView4_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView4.EditIndex = -1;

        DataSet ds_detail = dml.Find("select * from ViewPODetail_FED where Sno_Master = '" + ViewState["SNO"].ToString() + "'");
        if (ds_detail.Tables[0].Rows.Count > 0)
        {
            Div3.Visible = true;
            GridView4.DataSource = ds_detail.Tables[0];
            GridView4.DataBind();
        }
    }
    protected void GridView6_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lblRowNumber") as Label).Text = (e.Row.RowIndex + 1).ToString();
        }
    }
    protected void GridView6_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = GridView6.Rows[e.RowIndex];
        DataTable dt = (DataTable)ViewState["Customers"];
        dt.Rows[e.RowIndex].Delete();
        this.PopulateGridview();
        DropDownList ddlsub = GridView6.FooterRow.FindControl("ddlitemsub") as DropDownList;
        dml.dropdownsql(ddlsub, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");

        DropDownList ddlmaster = GridView6.FooterRow.FindControl("ddlitemMaster") as DropDownList;
        dml.dropdownsql(ddlmaster, "SET_ItemMaster", "Description", "ItemID");

        DropDownList ddluom = GridView6.FooterRow.FindControl("ddlUomFooter") as DropDownList;
        dml.dropdownsql(ddluom, "SET_UnitofMeasure", "UOMName", "UOMID");

        DropDownList ddlsupFooter = GridView6.FooterRow.FindControl("lblCostCenterFooter") as DropDownList;
        dml.dropdownsql(ddlsupFooter, "SET_CostCenter", "CostCenterName", "CostCenterID");

        DropDownList ddluom2 = GridView6.FooterRow.FindControl("ddlUOM2Footer") as DropDownList;
        dml.dropdownsql(ddluom2, "SET_UnitofMeasure", "UOMName", "UOMID");

        DropDownList ddlloc = GridView6.FooterRow.FindControl("ddlLocationFooter") as DropDownList;
        dml.dropdownsql(ddlloc, "Set_Location", "LocName", "LocId");

    }
    public void emails(string supp_email, string supp_name)
    {
        try
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = true;
            client.Credentials = new NetworkCredential("elizirkhan@gmail.com", "Haier159");
            MailMessage msgOBj = new MailMessage();
            msgOBj.To.Add(supp_email);
            msgOBj.From = new MailAddress("elizirkhan@gmail.com");
            msgOBj.Subject = "Company Email to Supplier";


            string htmlString = @"<html>

	                      <body>

	                      <p>Dear <b> " + supp_name + @" ,</b></p>

	                      <p>Thank you for your letter of yesterday inviting me to come for an interview on Friday afternoon, 5th July, at 2:30.

	                              I shall be happy to be there as requested and will bring my diploma and other papers with me.</p>

	                      <p>Sincerely,<br>-Jack</br></p>

	                      </body>

	                      </html>";


            msgOBj.IsBodyHtml = true;
            msgOBj.Body = htmlString;
            client.Send(msgOBj);
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertEmail()", true);

        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    protected void lblQty2Footer_TextChanged(object sender, EventArgs e)
    {
        int DisplayDigit=0, FinancialRoundOff=0;
        string Qty2 = (GridView6.FooterRow.FindControl("lblQty2Footer") as TextBox).Text;
        string txtRateFooter = (GridView6.FooterRow.FindControl("txtRateFooter") as TextBox).Text;
        string Qty = (GridView6.FooterRow.FindControl("txtQtyFooter") as TextBox).Text;

        float qtyval = float.Parse(Qty) * float.Parse(txtRateFooter);
        DataSet Branch = dml.Find("Select * From Set_Branch Where BranchId=" + Convert.ToInt32(Request.Cookies["BranchId"].Value));
        if (Branch.Tables[0].Rows.Count > 0)
        {
            DisplayDigit = Convert.ToInt32(Branch.Tables[0].Rows[0]["DisplayDigit"].ToString());
            FinancialRoundOff = Convert.ToInt32(Branch.Tables[0].Rows[0]["FinancialRoundOff"].ToString());
        }
        float qt2rate = qtyval / float.Parse(Qty2);
        if (DisplayDigit > 0 && FinancialRoundOff > 0)
        {
            qtyval = utl.RoundOffToFloat(DisplayDigit, FinancialRoundOff, qtyval.ToString());
            qt2rate = utl.RoundOffToFloat(DisplayDigit,FinancialRoundOff,qt2rate.ToString());

        }
        (GridView6.FooterRow.FindControl("lblRate2Footer") as TextBox).Text = qt2rate.ToString();

    }
    protected void txtEntryDate_TextChanged(object sender, EventArgs e)
    {
        required_generate();
        CalendarExtender3.StartDate = Convert.ToDateTime(txtEntryDate.Text);
        ddlPODocName_SelectedIndexChanged(sender, e);
    }
    protected void txtDeliveryDate_TextChanged(object sender, EventArgs e)
    {
        var DeliveryDate = Convert.ToDateTime(txtDeliveryDate.Text.ToString()).Date;
        var EntryDate = Convert.ToDateTime(txtEntryDate.Text.ToString()).Date;
        int Date = (int)(DeliveryDate - EntryDate).TotalDays;
        txtDeliDuedays.Text = Date.ToString();
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
    public string startdate(string fy)
    {
        string sdate = "";
        DataSet ds = dml.Find("select StartDate,EndDate from SET_Fiscal_Year where FiscalYearId = " + Convert.ToInt32(Request.Cookies["FiscalYearId"].Value));
        if (ds.Tables[0].Rows.Count > 0)
        {
            sdate = ds.Tables[0].Rows[0]["StartDate"].ToString();
        }
        return sdate;
    }
    public string Enddate(string fy)
    {
        string Edate = "";
        DataSet ds = dml.Find("select StartDate,EndDate from SET_Fiscal_Year where FiscalYearId = " + Convert.ToInt32(Request.Cookies["FiscalYearId"].Value));
        if (ds.Tables[0].Rows.Count > 0)
        {
            Edate = ds.Tables[0].Rows[0]["EndDate"].ToString();
        }
        return Edate;
    }
}
