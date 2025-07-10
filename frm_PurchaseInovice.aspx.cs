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
    int DateFrom, AddDays, EditDays, DeleteDays;
    string userid, UserGrpID, FormID, fiscal, menuid;
    int valid, showd;
    string[] supplier = new string[4];

    DmlOperation dml = new DmlOperation();
    radcomboxclass cmb = new radcomboxclass();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
    FieldHide fd = new FieldHide();
    GLEntry gl = new GLEntry();
    inventoryCal inv = new inventoryCal();

    string itemtype, itemhead, itemsubhead;

    float i;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {

            Showall_Dml();
            ViewState["flag"] = "true";
            btnUpdate.Visible = false;
            btnSave.Visible = false;
            btnDelete_after.Visible = false;
            btnUpdatePO.Visible = false;
            userid = Request.QueryString["UserID"];
            UserGrpID = Request.QueryString["UsergrpID"];
            FormID = Request.QueryString["FormID"];
            fiscal = Request.QueryString["fiscaly"];
            menuid = Request.QueryString["Menuid"];


            txtEntryDate.Attributes.Add("readonly", "readonly");
            txtDeliveryChallanDate.Attributes.Add("readonly", "readonly");
            txtBilldate.Attributes.Add("readonly", "readonly");
            txtDueDate.Attributes.Add("readonly", "readonly");
            CalendarExtender1.EndDate = DateTime.Now;
            string sd = fiscalstart(fiscal);
            CalendarExtender1.StartDate = DateTime.Parse(sd);

            CalendarExtender3.EndDate = DateTime.Now;
            CalendarExtender3.StartDate = DateTime.Parse(sd);

            CalendarExtender10.EndDate = DateTime.Now;
            CalendarExtender10.StartDate = DateTime.Parse(sd);
            



            // dml.dropdownsqlwithquery(ddladdGST

            dml.dropdownsql(ddlSupplier, "ViewSupplierId", "BPartnerName", "BPartnerId");
            dml.dropdownsql(ddlStatus, "SET_Status", "StatusName", "StatusId");
            //dml.dropdownsql(ddlFreightTerms, "ViewSupplierId", "BPartnerName", "Sno");

            string sdate = startdate(fiscal);
            string enddate = Enddate(fiscal);
            dml.dropdownsqlwithquery(ddlDocName, "select DocID,DocDescription from SET_Documents where docid in (select DocID from SET_UserGrp_Documents where UserGrpId = '" + UserGrpID + "' AND	(( StartDate <= '" + sdate + "') AND ((EndDate >= '" + enddate + "') OR (EndDate IS NULL))) and Record_Deleted = 0) and CompID = '" + compid() + "' and Record_Deleted = 0 and MenuId_Sno = '" + menuid + "' and FormId_Sno='" + FormID + "'", "DocDescription", "DocID");


            dml.dropdownsql(ddlDocAuth, "SET_Authority", "AuthorityName", "AuthorityId");
            dml.dropdownsqlwithquery(ddlLocationTo, "select DepartmentID, DepartmentName from SET_Department where IsWarehouse = 1", "DepartmentName", "DepartmentID");
            


            dml.dropdownsql(ddlLocationFrom, "Set_Location", "LocName", "LocId");
            dml.dropdownsql(ddlCostCenterTo, "SET_CostCenter", "CostCenterName", "CostCenterID");
            dml.dropdownsql(ddlCostCentrFrom, "SET_CostCenter", "CostCenterName", "CostCenterID");


            //ddlEdit_Document

            dml.dropdownsqlwithquery(ddlEdit_Document, "select SET_Documents.DocID as docs , * from SET_DocumentType LEFT JOIN SET_Documents on SET_DocumentType.DocTypeId = SET_Documents.DocTypeId where SET_DocumentType.DocTypeId in( select DocTypeId from SET_Documents where MenuId_Sno = '" + menuid + "' and FormId_Sno = '" + FormID + "')", "DocName", "docs");
            dml.dropdownsqlwithquery(ddlDel_Document, "select SET_Documents.DocID as docs , * from SET_DocumentType LEFT JOIN SET_Documents on SET_DocumentType.DocTypeId = SET_Documents.DocTypeId where SET_DocumentType.DocTypeId in( select DocTypeId from SET_Documents where MenuId_Sno = '" + menuid + "' and FormId_Sno = '" + FormID + "')", "DocName", "docs");
            dml.dropdownsqlwithquery(ddlFind_Document, "select * From SET_DocumentType where DocName like 'Purchase%'", "DocName", "DocTypeId");
            // select[DocumentNo.] Sno from Set_PurcahseOrderMaster

            dml.dropdownsqlwithquery(ddlEdit_DocNO, "SELECT DocumentNo,Sno from Inv_InventoryInMaster", "DocumentNo", "Sno");
            dml.dropdownsqlwithquery(ddlFind_DocNO, "SELECT DocId,VoucherNo from Fin_PurchaseMaster", "VoucherNo", "DocId");
            dml.dropdownsqlwithquery(ddlDel_DocNO, "SELECT DocumentNo,Sno from Inv_InventoryInMaster", "DocumentNo", "Sno");


            dml.dropdownsql(ddlFind_Status, "SET_Status", "StatusName", "StatusId");
            dml.dropdownsql(ddlEdit_Status, "SET_Status", "StatusName", "StatusId");
            dml.dropdownsql(ddlDel_Status, "SET_Status", "StatusName", "StatusId");


            dml.dropdownsql(ddlFind_Department, "SET_Department", "DepartmentName", "DepartmentID");
            dml.dropdownsql(ddlEdit_Depart, "SET_Department", "DepartmentName", "DepartmentID");
            dml.dropdownsql(ddlDel_Department, "SET_Department", "DepartmentName", "DepartmentID");
            //dml.dropdownsqlwithquery(ddltransfer, "select Sno, DocumentNo from Inv_InventoryInMaster where docid = 8 and Record_Deleted = 0 and IsActive = 1", "DocumentNo", "Sno");





            textClear();
            Div1.Visible = false;
            Div2.Visible = false;
            Div7.Visible = false;
            Findbox.Visible = false;
            DeleteBox.Visible = false;
            Editbox.Visible = false;
            datashow1(grd_Add);

            datashow1(grd_Deduction);
            add_red("Add_RED");
            add_red("ADD_DED");
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

        ddlDocName.Enabled = true;

        // ddlDocName_SelectedIndexChanged(sender, e);

        doctype(menuid, FormID, UserGrpID);
        userid = Request.QueryString["UserID"];
        UserGrpID = Request.QueryString["UsergrpID"];
        FormID = Request.QueryString["FormID"];
        fiscal = Request.Cookies["FiscalYearId"].Value;
        dml.DateRuleForm(userid, FormID, CalendarExtender1, "A",Convert.ToInt32(fiscal));

        string sdate = startdate(fiscal);
        string enddate = Enddate(fiscal);
        // dml.dropdownsqlwithquery(ddlDocName, "select DocID,DocDescription from SET_Documents where docid in (select DocID from SET_UserGrp_Documents where UserGrpId = '" + UserGrpID + "' AND	(( StartDate <= '" + sdate + "') AND ((EndDate >= '" + enddate + "') OR (EndDate IS NULL))) and Record_Deleted = 0) and CompID = '" + compid() + "' and Record_Deleted = 0 and MenuId_Sno = '" + menuid + "' and FormId_Sno='" + FormID + "'", "DocDescription", "DocID");
        
        ddlDocName_SelectedIndexChanged(sender, e);
        datashow();
        div_add_A.Visible = true;
        div_add_FED.Visible = false;


    }

    protected void ddlFind_Document_SelectedIndexChange(object sender, EventArgs e) {
        ddlFind_DocNO.Items.Clear();
        dml.dropdownsqlwithquery(ddlFind_DocNO, "SELECT DocumentNo,VoucherNo from  where DocType="+Convert.ToInt32(ddlFind_DocNO.SelectedItem.Value), "DocumentNo", "Sno");

    }
    protected void btnUpdatePO_Click(object sender, EventArgs e)
    {
      

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (grd_Add.Rows.Count>1) {
            string vno = txtDocNo.Text, billno = txtbillNo.Text, billdate = txtBilldate.Text, shopname = txtShopOffName.Text;
            string Voucher = required_generateforInsforDoc("8");
            txtDocNo.Text = required_generateforIns();
            int direct_normal = 0, chk = 0;
            string ids = "0";
            string InvMasterID = "0";
            if (chkDirectGRN.Checked == true)
            {
                direct_normal = 1;
            }
            if (chkActive.Checked == true)
            {
                chk = 1;
            }


            string lt, lf, ct, cf;
            if (ddlLocationFrom.SelectedIndex != 0)
            {
                lf = ddlLocationFrom.SelectedItem.Value;
            }
            else
            {
                lf = "0";
            }
            if (ddlLocationTo.SelectedIndex != 0)
            {
                lt = ddlLocationTo.SelectedItem.Value;
            }
            else
            {
                lt = "0";
            }
            if (ddlCostCentrFrom.SelectedIndex != 0)
            {
                cf = ddlCostCentrFrom.SelectedItem.Value;
            }
            else
            {
                cf = "0";
            }
            if (ddlCostCenterTo.SelectedIndex != 0)
            {
                ct = ddlCostCenterTo.SelectedItem.Value;
            }
            else
            {
                ct = "0";
            }

            string trantype = "", doctype = "0", purchaseType = "";
            DataSet dsdoctype = dml.Find("select DocTypeId from SET_Documents where DocID = '" + ddlDocName.SelectedItem.Value + "';");
            if (dsdoctype.Tables[0].Rows.Count > 0)
            {
                doctype = dsdoctype.Tables[0].Rows[0]["DocTypeId"].ToString();
            }
            DataSet ds; DataSet dsInv;
            string inv_impact = "", refdocM = "", refdocD = "";
            DataSet dsinventImpact = dml.Find("select InventoryImpact from SET_Documents where DocID = '" + ddlDocName.SelectedItem.Value + "'");
            if (dsinventImpact.Tables[0].Rows.Count > 0)
            {
                inv_impact = dsinventImpact.Tables[0].Rows[0]["InventoryImpact"].ToString();
            }


            DataSet dsrefacctcodeMaster = dml.Find("SELECT RefDocAcctCode from SET_AcRules4Doc where DocId = '" + ddlDocName.SelectedItem.Value + "' and MasterDetail ='M'");
            if (dsrefacctcodeMaster.Tables[0].Rows.Count > 0)
            {
                refdocM = dsrefacctcodeMaster.Tables[0].Rows[0]["RefDocAcctCode"].ToString();
            }

            DataSet dsrefacctcodeDetail = dml.Find("SELECT RefDocAcctCode from SET_AcRules4Doc where DocId = '" + ddlDocName.SelectedItem.Value + "' and MasterDetail ='D'");
            if (dsrefacctcodeDetail.Tables[0].Rows.Count > 0)
            {
                for (int a = 0; a < dsrefacctcodeDetail.Tables[0].Rows.Count; a++)
                {
                    if (dsrefacctcodeDetail.Tables[0].Rows[a]["RefDocAcctCode"].ToString() != "")
                    {
                        refdocD = dsrefacctcodeDetail.Tables[0].Rows[a]["RefDocAcctCode"].ToString();
                    }
                    if (refdocD != "")
                    {
                        break;
                    }
                }

            }
            string Supplier, DocAuthority, status;

            if (ddlSupplier.SelectedIndex != 0)
            {
                Supplier = ddlSupplier.SelectedItem.Value;
            }
            else {
                Supplier = null;
            }

            if (ddlSupplier.SelectedIndex != 0)
            {
                Supplier = ddlSupplier.SelectedItem.Value;
            }
            else
            {
                Supplier = null;
            }

            if (ddlDocAuth.SelectedIndex != 0)
            {
                DocAuthority = ddlDocAuth.SelectedItem.Value;
            }
            else {
                DocAuthority = null;
            }

            if (ddlStatus.SelectedIndex != 0)
            {
                status = ddlStatus.SelectedItem.Value;
            }
            else
            {
                status = null;
            }
            if (refdocM != "" && refdocD == "")
            {
                
                if (chkDirectGRN.Checked == true)
                {
                    if (inv_impact != "No Impact")
                    {
                        purchaseType = "Direct";
                        dsInv = dml.Find("INSERT INTO Inv_InventoryInMaster ([DocId], [DocType], [EntryDate], [DocumentNo], [BPartnerID], [DirectPO_NoramlPO], [DocumentAuthority], [DeliveryChallan], [DeliveryChallanDate], [InwardGatePass], [LocationFrom], [LocationTo], [CostCenter], [CostCenter2], [Status], [Remarks], [Shop_OfficeName], [BillNo], [BillDate], [WeighbridgeNo], [NetWeight], [DrAccountCode], [CrAccountCode], [GLReferNo], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted]) VALUES "
                                                   + "('8', '" + doctype + "', '" + txtEntryDate.Text + "', '" + Voucher + "', '" + Supplier + "', '" + direct_normal + "', '" + DocAuthority + "', '" + txtDeliveryChallan.Text + "', '" + txtDeliveryChallanDate.Text + "', '" + txtInwardGatePassNo.Text + "', '" + lf + "', '" + lt + "', '" + cf + "', '" + ct + "', '" +status + "','" + txtRemarks.Text + "', '" + txtShopOffName.Text + "', '" + txtbillNo.Text + "', '" + txtBilldate.Text + "', '" + txtWeighbridgeNo.Text + "', '" + txtNetWeight.Text + "', '" + RadComboAcct_Code.Text + "', NULL, NULL, '" + chk + "', " + gocid() + ", " + compid() + ", " + branchId() + "," + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0');  SELECT * FROM Inv_InventoryInMaster WHERE Sno = SCOPE_IDENTITY()");
                        if (dsInv.Tables[0].Rows.Count > 0)
                        {
                            InvMasterID = dsInv.Tables[0].Rows[0]["Sno"].ToString();
                        }

                        string dbcr = "";
                        DataSet dsdebcr = dml.Find("SELECT GLImpact, InventoryImpact FROM SET_AcRules4Doc WHERE DocId = '" + ddlDocName.SelectedItem.Value + "' AND MasterDetail = 'M' AND CompID = 1 AND Record_Deleted = 0 AND IsActive = 1 AND IsHide = 0 ;");
                        if (dsdebcr.Tables[0].Rows.Count > 0)
                        {
                            dbcr = dsdebcr.Tables[0].Rows[0]["GLImpact"].ToString();
                        }
                       
                        string ids_grn = gl.GlentryInsert_WITHID("8", doctype, "GRN Direct", InvMasterID, "0", dml.dateconvertforinsertNEW(txtEntryDate), Voucher, RadComboAcct_Code.Text, "", "", txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), "0", txtBillBal.Text, txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff"), "0", "0", "0", "Inv_InventoryInMaster");

                        dml.Update("UPDATE Inv_InventoryInMaster set GLReferNo = '" + ids + "' where Sno = '" + InvMasterID + "'", "");
                        gl.fwdidIn(ids_grn, InvMasterID);

                    }

                    string Query = "INSERT INTO [Fin_PurchaseMaster] ([DocId], [DocType], [EntryDate], [VoucherNo], [BPartnerID], [PurchaseType], [DocumentAuthority], [DeliveryChallan], [DeliveryChallanDate], [InwardGatePass], [Store_Department], [CostCenter], [Status], [Remarks], [Shop_OfficeName], [BillNo], [BillDate], [WeighbridgeNo], [NetWeight], [CrAccountCode], [GLReferNo], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted], [ReferNo], [GrnNo], [PoNo], [TruckNo], [StaxInvNo], [CreditTerm], [DueDate], [BillBalance], [FWD_Id]) VALUES "
                                                    + "('" + ddlDocName.SelectedItem.Value + "', '" + doctype + "', '" + dml.dateconvertforinsertNEW(txtEntryDate) + "', '" + required_generateforIns() + "', '" + ddlSupplier.SelectedItem.Value + "', '" + purchaseType + "', '" + ddlDocAuth.SelectedItem.Value + "', '" + txtDeliveryChallan.Text + "', '" + dml.dateconvertforinsertNEW(txtDeliveryChallanDate) + "', '" + txtInwardGatePassNo.Text + "', '" + lt + "', '" + ct + "', '" + ddlStatus.SelectedItem.Value + "', '" + txtRemarks.Text + "', '" + txtShopOffName.Text + "', '" + txtbillNo.Text + "', '" + dml.dateconvertforinsertNEW(txtBilldate) + "', '" + txtWeighbridgeNo.Text + "', '" + txtNetWeight.Text + "', '" + RadComboAcct_Code.Text + "', NULL, '" + chk + "', " + gocid() + ", " + compid() + ", " + branchId() + ", " + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "',  0, NULL, NULL, NULL, '" + txtTruckNo.Text + "', '" + txtSalTax.Text + "', '" + txtCreditterm.Text + "', '" + dml.dateconvertforinsertNEW(txtDueDate) + "', '" + txtBillBal.Text + "', NULL);  SELECT * FROM Fin_PurchaseMaster WHERE Sno = SCOPE_IDENTITY()";

                    ds = dml.Find(Query);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ids = ds.Tables[0].Rows[0]["Sno"].ToString();
                        ViewState["detailid"] = ids;
                    }

                    if (grd_Add.Rows.Count > 0)
                    {
                        btnApply_Click(ids);
                    }
                    if (grd_Deduction.Rows.Count > 0)
                    {
                        btnDec_Apply_Click(ids);
                    }

                    detailinsert(ids, InvMasterID, Voucher);

                }
                else if (chkPO.Checked == true)
                {
                    if (inv_impact != "No Impact")
                    {
                        purchaseType = "PO";

                        ds = dml.Find("INSERT INTO Inv_InventoryInMaster ([DocId], [DocType], [EntryDate], [DocumentNo], [BPartnerID], [DirectPO_NoramlPO], [DocumentAuthority], [DeliveryChallan], [DeliveryChallanDate], [InwardGatePass], [LocationFrom], [LocationTo], [CostCenter], [CostCenter2], [Status], [Remarks], [Shop_OfficeName], [BillNo], [BillDate], [WeighbridgeNo], [NetWeight], [DrAccountCode], [CrAccountCode], [GLReferNo], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted]) VALUES "
                                                    + "('" + ddlDocName.SelectedItem.Value + "', '" + doctype + "', '" + txtEntryDate.Text + "', '" + required_generateforInsforDoc("7") + "', '" + ddlSupplier.SelectedItem.Value + "', '" + direct_normal + "', '" + ddlDocAuth.SelectedItem.Value + "', '" + txtDeliveryChallan.Text + "', '" + txtDeliveryChallanDate.Text + "', '" + txtInwardGatePassNo.Text + "', '" + lf + "', '" + lt + "', '" + cf + "', '" + ct + "', '" + ddlStatus.SelectedItem.Value + "','" + txtRemarks.Text + "', '" + txtShopOffName.Text + "', '" + txtbillNo.Text + "', '" + txtBilldate.Text + "', '" + txtWeighbridgeNo.Text + "', '" + txtNetWeight.Text + "', '" + RadComboAcct_Code.Text + "', NULL, NULL, '" + chk + "'," + gocid() + "," + compid() + ", " + branchId() + ", " + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0');  SELECT * FROM Inv_InventoryInMaster WHERE Sno = SCOPE_IDENTITY()");

                    }

                    ds = dml.Find("INSERT INTO [Fin_PurchaseMaster] ([DocId], [DocType], [EntryDate], [VoucherNo], [BPartnerID], [PurchaseType], [DocumentAuthority], [DeliveryChallan], [DeliveryChallanDate], [InwardGatePass], [Store_Department], [CostCenter], [Status], [Remarks], [Shop_OfficeName], [BillNo], [BillDate], [WeighbridgeNo], [NetWeight], [CrAccountCode], [GLReferNo], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted], [ReferNo], [GrnNo], [PoNo], [TruckNo], [StaxInvNo], [CreditTerm], [DueDate], [BillBalance], [FWD_Id]) VALUES "
                                                    + "('" + ddlDocName.SelectedItem.Value + "', '" + doctype + "', '" + dml.dateconvertforinsertNEW(txtEntryDate) + "', '" + required_generateforIns() + "', '" + ddlSupplier.SelectedItem.Value + "', '" + purchaseType + "', '" + ddlDocAuth.SelectedItem.Value + "', '" + txtDeliveryChallan.Text + "', '" + dml.dateconvertforinsertNEW(txtDeliveryChallanDate) + "', '" + txtInwardGatePassNo.Text + "', '" + ddlLocationTo.SelectedItem.Value + "', '" + ddlCostCenterTo.SelectedItem.Value + "', '" + ddlStatus.SelectedItem.Value + "', '" + txtRemarks.Text + "', '" + txtShopOffName.Text + "', '" + txtbillNo.Text + "', '" + dml.dateconvertforinsertNEW(txtBilldate) + "', '" + txtWeighbridgeNo.Text + "', '" + txtNetWeight.Text + "', '" + RadComboAcct_Code.Text + "', NULL, '" + chk + "', " + gocid() + "," + compid() + "," + branchId() + "," + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "',  0, NULL, NULL, '" + ddlPO_GRN.SelectedItem.Value + "', '" + txtTruckNo.Text + "', '" + txtSalTax.Text + "', '" + txtCreditterm.Text + "', '" + dml.dateconvertforinsertNEW(txtDueDate) + "', '" + txtBillBal.Text + "', NULL);  SELECT * FROM Fin_PurchaseMaster WHERE Sno = SCOPE_IDENTITY()");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ids = ds.Tables[0].Rows[0]["Sno"].ToString();
                        ViewState["detailid"] = ids;
                    }
                    detailinsertForPO(ids, InvMasterID);
                    if (grd_Add.Rows.Count > 0)
                    {
                        btnApply_Click(ids);
                    }
                    if (grd_Deduction.Rows.Count > 0)
                    {
                        btnDec_Apply_Click(ids);
                    }
                    //
                    dml.Update("update Set_PurcahseOrderMaster set Status = '2' where Sno = '" + ddlPO_GRN.SelectedItem.Value + "'", "");
                }
                else
                {
                    purchaseType = "GRN";

                    ds = dml.Find("INSERT INTO [Fin_PurchaseMaster] ([DocId], [DocType], [EntryDate], [VoucherNo], [BPartnerID], [PurchaseType], [DocumentAuthority], [DeliveryChallan], [DeliveryChallanDate], [InwardGatePass], [Store_Department], [CostCenter], [Status], [Remarks], [Shop_OfficeName], [BillNo], [BillDate], [WeighbridgeNo], [NetWeight], [CrAccountCode], [GLReferNo], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted], [ReferNo], [GrnNo], [PoNo], [TruckNo], [StaxInvNo], [CreditTerm], [DueDate], [BillBalance], [FWD_Id]) VALUES "
                                                    + "('" + ddlDocName.SelectedItem.Value + "', '" + doctype + "', '" + dml.dateconvertforinsertNEW(txtEntryDate) + "', '" + required_generateforIns() + "', '" + ddlSupplier.SelectedItem.Value + "', '" + purchaseType + "', '" + ddlDocAuth.SelectedItem.Value + "', '" + txtDeliveryChallan.Text + "', '" + dml.dateconvertforinsertNEW(txtDeliveryChallanDate) + "', '" + txtInwardGatePassNo.Text + "', '" + ddlLocationTo.SelectedItem.Value + "', '" + ddlCostCenterTo.SelectedItem.Value + "', '" + ddlStatus.SelectedItem.Value + "', '" + txtRemarks.Text + "', '" + txtShopOffName.Text + "', '" + txtbillNo.Text + "', '" + dml.dateconvertforinsertNEW(txtBilldate) + "', '" + txtWeighbridgeNo.Text + "', '" + txtNetWeight.Text + "', '" + RadComboAcct_Code.Text + "', NULL, '" + chk + "'," + gocid() + "," + compid() + "," + branchId() + "," + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "',  0, NULL, '" + ddlPO_GRN.SelectedItem.Value + "', NULL, '" + txtTruckNo.Text + "', '" + txtSalTax.Text + "', '" + txtCreditterm.Text + "', '" + dml.dateconvertforinsertNEW(txtDueDate) + "', '" + txtBillBal.Text + "', NULL);  SELECT * FROM Fin_PurchaseMaster WHERE Sno = SCOPE_IDENTITY()");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ids = ds.Tables[0].Rows[0]["Sno"].ToString();
                        ViewState["detailid"] = ids;
                    }

                    detailinsertForGRN(ids, InvMasterID);
                    if (grd_Add.Rows.Count > 0)
                    {
                        btnApply_Click(ids);
                    }
                    if (grd_Deduction.Rows.Count > 0)
                    {
                        btnDec_Apply_Click(ids);
                    }


                    dml.Update("UPDATE Inv_InventoryInMaster set Status = '2' WHERE Sno='" + ddlPO_GRN.SelectedItem.Value + "' AND	DocId = '8'  AND IsActive = 1 AND GocID = '" + gocid() + "' AND CompId = '" + compid() + "' AND branchid = '" + branchId() + "' AND FiscalYearID = '" + FiscalYear() + "'", "");

                }


                gl.GlentrySummaryDetail(show_username(), DateTime.Now.ToString(), "0", Voucher);
                gl.GlentrySummaryDetail(show_username(), DateTime.Now.ToString(), "0", txtDocNo.Text);

                textClear();
                btnInsert_Click(sender, e);
                
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);
                Response.Redirect("frm_JV_Diplay.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "&Menuid=" + menuid + "&VoucherNo=" + vno + "&bilno=" + billno + "&billdate=" + billdate + "&shopname=" + shopname + "");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);
            }
            else
            {
                Label1.Text = "Both Master and Detail have reference Account Code";
            }
        }
        else {
            Label1.Text = "Grid Data Must Be Saved Before";
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int direct_normal = 0, chk = 0;
        string ids = "0";
        if (chkDirectGRN.Checked == true)
        {
            direct_normal = 1;
        }
        if (chkActive.Checked == true)
        {
            chk = 1;
        }

        string lt, lf, ct, cf;
        if (ddlLocationFrom.SelectedIndex != 0)
        {
            lf = ddlLocationFrom.SelectedItem.Value;
        }
        else
        {
            lf = "0";
        }
        if (ddlLocationTo.SelectedIndex != 0)
        {
            lt = ddlLocationTo.SelectedItem.Value;
        }
        else
        {
            lt = "0";
        }
        if (ddlCostCentrFrom.SelectedIndex != 0)
        {
            cf = ddlCostCentrFrom.SelectedItem.Value;
        }
        else
        {
            cf = "0";
        }
        if (ddlCostCenterTo.SelectedIndex != 0)
        {
            ct = ddlCostCenterTo.SelectedItem.Value;
        }
        else
        {
            ct = "0";
        }


        string dateEntry = "", dateDelChallan = "", dateBill = "";

        dateEntry = dml.dateconvertString(txtEntryDate.Text);
        dateDelChallan = dml.dateconvertString(txtDeliveryChallanDate.Text);
        dateBill = dml.dateconvertString(txtBilldate.Text);

        string trantype = "";
        DataSet dsCRDR = dml.Find("select  Tran_Type from SET_Acct_Type where Acct_Type_Id in ( SELECT Acct_Type_ID from SET_COA_detail where Acct_Code = '2-02-02-0006')");
        if (dsCRDR.Tables[0].Rows.Count > 0)
        {
            trantype = dsCRDR.Tables[0].Rows[0]["Tran_Type"].ToString();

        }
        DataSet ds;
        string bpnatureid = "0";
        float totalcrdb = 0;
        string dbcr_detail = "", dbcr_plusminus = "";

        string doctype = "0";


        if (trantype == "Debit")
        {
            DataSet ds_up = dml.Find("select * from Inv_InventoryInMaster WHERE ([DocId]='" + ddlDocName.SelectedItem.Value + "') AND ([EntryDate]='" + dateEntry + "') AND ([DocumentNo]='" + txtDocNo.Text + "') AND ([BPartnerID]='" + ddlSupplier.SelectedItem.Value + "') AND ([DirectPO_NoramlPO]='" + direct_normal + "') AND ([DocumentAuthority]='" + ddlDocAuth.SelectedItem.Value + "') AND ([DeliveryChallan]='" + txtDeliveryChallan.Text + "') AND ([DeliveryChallanDate]='" + dateDelChallan + "') AND ([InwardGatePass]='" + txtInwardGatePassNo.Text + "') AND  ([LocationTo]='" + ddlLocationTo.SelectedItem.Value + "') AND  ([CostCenter2]='" + ddlCostCenterTo.SelectedItem.Value + "') AND ([Status]='" + ddlStatus.SelectedItem.Value + "') AND ([Remarks]='" + txtRemarks.Text + "') AND ([Shop_OfficeName]='" + txtShopOffName.Text + "') AND ([BillNo]='" + txtbillNo.Text + "') AND ([BillDate]='" + dateBill + "') AND ([WeighbridgeNo]='" + txtWeighbridgeNo.Text + "') AND ([NetWeight]='" + txtNetWeight.Text + "') AND ([CrAccountCode] IS NULL) AND ([DrAccountCode]='" + RadComboAcct_Code.Text + "') AND ([GLReferNo] IS NULL) AND ([IsActive]='" + chk + "') AND ([GocID]='" + gocid() + "') AND ([CompId]='" + compid() + "') AND ([BranchId]='" + branchId() + "') AND ([FiscalYearID]='" + FiscalYear() + "') AND ([Record_Deleted]='0')");

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

                dml.Update("UPDATE Fin_PurchaseMaster SET  [DocId]='" + ddlDocName.SelectedItem.Value + "', [DocType]='9', [EntryDate]='" + dateEntry + "', [VoucherNo]='" + txtDocNo.Text + "', [BPartnerID]='" + ddlSupplier.SelectedItem.Value + "', [PurchaseType]='Direct' , [DocumentAuthority]='" + ddlDocAuth.SelectedItem.Value + "', [DeliveryChallan]='" + txtDeliveryChallan.Text + "', [DeliveryChallanDate]='" + dateDelChallan + "', [InwardGatePass]='" + txtInwardGatePassNo.Text + "', [Store_Department]='" + ddlLocationTo.SelectedItem.Value + "', [CostCenter]='" + ddlCostCentrFrom.SelectedItem.Value + "', [Status]='" + ddlStatus.SelectedItem.Value + "', [Remarks]='" + txtRemarks.Text + "', [Shop_OfficeName]='" + txtShopOffName.Text + "', [BillNo]='" + txtbillNo.Text + "', [BillDate]='" + dateBill + "', [WeighbridgeNo]='" + txtWeighbridgeNo.Text + "', [NetWeight]='" + txtNetWeight.Text + "', [CrAccountCode]='" + RadComboAcct_Code.Text + "', [GLReferNo]='58', [IsActive]='" + chk + "',  [GocID]='" + gocid() + "', [CompId]='" + compid() + "', [BranchId]='" + branchId() + "', [FiscalYearID]='" + FiscalYear() + "', [UpdatedBy]='" + show_username() + "', [UpdatedDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', [Record_Deleted]='0', [ReferNo]=NULL, [GrnNo]=NULL, [PoNo]=NULL, [TruckNo]='" + txtTruckNo.Text + "', [StaxInvNo]='" + txtSalTax.Text + "', [CreditTerm]='" + txtCreditterm.Text + "', [DueDate]='" + txtDueDate.Text + "', [BillBalance]='" + txtBillBal.Text + "', [FWD_Id]=NULL WHERE ([Sno]='" + ViewState["SNO"].ToString() + "')", "");
                string grnSno = "0";
                DataSet dsGrnsno = dml.Find("select GrnNo from Fin_PurchaseMaster WHERE Sno ='" + ViewState["SNO"].ToString() + "'");
                if (dsGrnsno.Tables[0].Rows.Count > 0)
                {
                    grnSno = dsGrnsno.Tables[0].Rows[0]["GrnNo"].ToString();
                }

                dml.Update("UPDATE Inv_InventoryInMaster SET  [DocId]='" + ddlDocName.SelectedItem.Value + "', [DocType]=NULL, [EntryDate]='" + dateEntry + "', [DocumentNo]='" + txtDocNo.Text + "', [BPartnerID]='" + ddlSupplier.SelectedItem.Value + "', [DirectPO_NoramlPO]='" + direct_normal + "', [DocumentAuthority]='" + ddlDocAuth.SelectedItem.Value + "', [DeliveryChallan]='" + txtDeliveryChallan.Text + "', [DeliveryChallanDate]='" + dateDelChallan + "', [InwardGatePass]='" + txtInwardGatePassNo.Text + "',  [LocationTo]='" + ddlLocationTo.SelectedItem.Value + "', [CostCenter2]='" + ddlCostCenterTo.SelectedItem.Value + "', [Status]='" + ddlStatus.SelectedItem.Value + "', [Remarks]='" + txtRemarks.Text + "', [Shop_OfficeName]='" + txtShopOffName.Text + "', [BillNo]='" + txtbillNo.Text + "', [BillDate]='" + dateBill + "', [WeighbridgeNo]='" + txtWeighbridgeNo.Text + "', [NetWeight]='" + txtNetWeight.Text + "', [CrAccountCode]=NULL, [DrAccountCode]='" + RadComboAcct_Code.Text + "', [GLReferNo]=NULL, [IsActive]='" + chk + "', [GocID]='" + gocid() + "', [CompId]='" + compid() + "', [BranchId]='" + branchId() + "', [FiscalYearID]='" + FiscalYear() + "', [UpdatedBy]='" + show_username() + "', [UpdatedDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', [Record_Deleted]='0' WHERE ([Sno]='" + grnSno + "') ", "");
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
        else
        {
            DataSet ds_up = dml.Find("select * from Inv_InventoryInMaster WHERE ([DocId]='" + ddlDocName.SelectedItem.Value + "') AND ([EntryDate]='" + dateEntry + "') AND ([DocumentNo]='" + txtDocNo.Text + "') AND ([BPartnerID]='" + ddlSupplier.SelectedItem.Value + "') AND ([DirectPO_NoramlPO]='" + direct_normal + "') AND ([DocumentAuthority]='" + ddlDocAuth.SelectedItem.Value + "') AND ([DeliveryChallan]='" + txtDeliveryChallan.Text + "') AND ([DeliveryChallanDate]='" + dateDelChallan + "') AND ([InwardGatePass]='" + txtInwardGatePassNo.Text + "') AND  ([LocationTo]='" + ddlLocationTo.SelectedItem.Value + "') AND ([CostCenter2]='" + ddlCostCenterTo.SelectedItem.Value + "') AND ([Status]='" + ddlStatus.SelectedItem.Value + "') AND ([Remarks]='" + txtRemarks.Text + "') AND ([Shop_OfficeName]='" + txtShopOffName.Text + "') AND ([BillNo]='" + txtbillNo.Text + "') AND ([BillDate]='" + dateBill + "') AND ([WeighbridgeNo]='" + txtWeighbridgeNo.Text + "') AND ([NetWeight]='" + txtNetWeight.Text + "') AND ([DrAccountCode] IS NULL) AND ([CrAccountCode]='" + RadComboAcct_Code.Text + "') AND ([GLReferNo] IS NULL) AND ([IsActive]='" + chk + "') AND ([GocID]='" + gocid() + "') AND ([CompId]='" + compid() + "') AND ([BranchId]='" + branchId() + "') AND ([FiscalYearID]='" + FiscalYear() + "') AND ([Record_Deleted]='0')");

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
                dml.Update("UPDATE Fin_PurchaseMaster SET  [DocId]='" + ddlDocName.SelectedItem.Value + "', [DocType]='9', [EntryDate]='" + dateEntry + "', [VoucherNo]='" + txtDocNo.Text + "', [BPartnerID]='" + ddlSupplier.SelectedItem.Value + "', [PurchaseType]='Direct' , [DocumentAuthority]='" + ddlDocAuth.SelectedItem.Value + "', [DeliveryChallan]='" + txtDeliveryChallan.Text + "', [DeliveryChallanDate]='" + dateDelChallan + "', [InwardGatePass]='" + txtInwardGatePassNo.Text + "', [Store_Department]='" + ddlLocationTo.SelectedItem.Value + "', [CostCenter]='" + ddlCostCenterTo.SelectedItem.Value + "', [Status]='" + ddlStatus.SelectedItem.Value + "', [Remarks]='" + txtRemarks.Text + "', [Shop_OfficeName]='" + txtShopOffName.Text + "', [BillNo]='" + txtbillNo.Text + "', [BillDate]='" + dateBill + "', [WeighbridgeNo]='" + txtWeighbridgeNo.Text + "', [NetWeight]='" + txtNetWeight.Text + "', [CrAccountCode]='" + RadComboAcct_Code.Text + "', [GLReferNo]='58', [IsActive]='" + chk + "',  [GocID]='" + gocid() + "', [CompId]='" + compid() + "', [BranchId]='" + branchId() + "', [FiscalYearID]='" + FiscalYear() + "', [UpdatedBy]='" + show_username() + "', [UpdatedDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', [Record_Deleted]='0', [ReferNo]=NULL, [GrnNo]=NULL, [PoNo]=NULL, [TruckNo]='" + txtTruckNo.Text + "', [StaxInvNo]='" + txtSalTax.Text + "', [CreditTerm]='" + txtCreditterm.Text + "', [DueDate]='" + txtDueDate.Text + "', [BillBalance]='" + txtBillBal.Text + "', [FWD_Id]=NULL WHERE ([Sno]='" + ViewState["SNO"].ToString() + "')", "");
                string grnSno = "0";
                DataSet dsGrnsno = dml.Find("select GrnNo from Fin_PurchaseMaster WHERE Sno ='" + ViewState["SNO"].ToString() + "'");
                if (dsGrnsno.Tables[0].Rows.Count > 0)
                {
                    if (dsGrnsno.Tables[0].Rows[0]["GrnNo"].ToString() != "")
                    {
                        grnSno = dsGrnsno.Tables[0].Rows[0]["GrnNo"].ToString();
                    }
                    else
                    {
                        grnSno = "0";
                    }
                }






                dml.Delete("update Act_GL_Detail set Record_Deleted = 1 where MasterSno= '" + grnSno + "'", "");
                dml.Delete("INSERT INTO Act_GL_Detail_Deleted ([DocId], [DocDescription], [MasterSno], [DetailSno], [EntryDate], [VoucherNo], [AccountCode], [BPNatureID], [BPartnerId], [Name], [ChqNo], [ChqDate], [InvoiceNo], [InvoiceDate], [DrAmount], [CrAmount], [Narration], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [Record_Deleted]) SELECT [DocId], [DocDescription], [MasterSno], [DetailSno], [EntryDate], [VoucherNo], [AccountCode], [BPNatureID], [BPartnerId], [Name], [ChqNo], [ChqDate], [InvoiceNo], [InvoiceDate], [DrAmount], [CrAmount], [Narration], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [Record_Deleted] FROM Act_GL_Detail WHERE MasterSno='" + grnSno + "';", "");

                dml.Delete("Delete from Act_GL_Detail where MasterSno = '" + grnSno + "'", "");





                dml.Update("UPDATE Inv_InventoryInMaster SET  [DocId]='" + ddlDocName.SelectedItem.Value + "', [DocType]=NULL, [EntryDate]='" + dateEntry + "', [DocumentNo]='" + txtDocNo.Text + "', [BPartnerID]='" + ddlSupplier.SelectedItem.Value + "', [DirectPO_NoramlPO]='" + direct_normal + "', [DocumentAuthority]='" + ddlDocAuth.SelectedItem.Value + "', [DeliveryChallan]='" + txtDeliveryChallan.Text + "', [DeliveryChallanDate]='" + dateDelChallan + "', [InwardGatePass]='" + txtInwardGatePassNo.Text + "', [LocationFrom]='" + lf + "', [LocationTo]='" + lt + "', [CostCenter]='" + cf + "', [CostCenter2]='" + ct + "', [Status]='" + ddlStatus.SelectedItem.Value + "', [Remarks]='" + txtRemarks.Text + "', [Shop_OfficeName]='" + txtShopOffName.Text + "', [BillNo]='" + txtbillNo.Text + "', [BillDate]='" + dateBill + "', [WeighbridgeNo]='" + txtWeighbridgeNo.Text + "', [NetWeight]='" + txtNetWeight.Text + "', [DrAccountCode]=NULL, [CrAccountCode]='" + RadComboAcct_Code.Text + "', [GLReferNo]=NULL, [IsActive]='" + chk + "', [GocID]='" + gocid() + "', [CompId]='" + compid() + "', [BranchId]='" + branchId() + "', [FiscalYearID]='" + FiscalYear() + "', [UpdatedBy]='" + show_username() + "', [UpdatedDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', [Record_Deleted]='0' WHERE ([Sno]='" + grnSno + "') ", "");



                foreach (GridViewRow g in GridView8.Rows)
                {
                    Label lblPObalQty = (Label)g.FindControl("lblPObalQty");
                    Label lblitemmaster = (Label)g.FindControl("lblItemMaster");


                    Label lblSnoD = (Label)g.FindControl("lblSno");
                    Label lblValue = (Label)g.FindControl("lblValue");
                    string itemmaster = "0", itemcode = "0";
                    DataSet ds1 = dml.Find("select  ItemID , Description, ItemCode from SET_ItemMaster where Description = '" + lblitemmaster.Text + "'");
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        itemmaster = ds1.Tables[0].Rows[0]["ItemID"].ToString();
                        itemcode = ds1.Tables[0].Rows[0]["ItemCode"].ToString();
                    }

                    //  string bpnatureid = "0";
                    DataSet dsnature = dml.Find("select  BPNatureID,BPNatureDescription from SET_BPartnerNature where BPNatureID in (select BPNatureID from SET_BPartnerType where BPartnerID = '" + ddlSupplier.SelectedItem.Value + "')");
                    if (dsnature.Tables[0].Rows.Count > 0)
                    {
                        bpnatureid = dsnature.Tables[0].Rows[0]["BPNatureID"].ToString();
                    }

                    DataSet dsdoctype = dml.Find("select DocTypeId from SET_Documents where DocID = '" + ddlDocName.SelectedItem.Value + "';");
                    if (dsdoctype.Tables[0].Rows.Count > 0)
                    {

                        doctype = dsdoctype.Tables[0].Rows[0]["DocTypeId"].ToString();

                    }


                    gl.GlentryUpdate(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, ViewState["SNO"].ToString(), lblSnoD.Text, dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, RadComboAcct_Code.Text, bpnatureid, ddlSupplier.SelectedItem.Value, txtShopOffName.Text, txtDeliveryChallan.Text, txtDeliveryChallanDate.Text, lblValue.Text, "0", txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff"), itemcode, lblPObalQty.Text, "0");

                    DataSet dsdebcr_detail = dml.Find("SELECT	GLImpact, InventoryImpact FROM SET_AcRules4Doc WHERE DocId = '" + ddlDocName.SelectedItem.Value + "' AND MasterDetail = 'D' AND CompID = '" + compid() + "' AND Record_Deleted = 0 AND IsActive = 1 AND IsHide = 0 ;");
                    if (dsdebcr_detail.Tables[0].Rows.Count > 0)
                    {
                        dbcr_detail = dsdebcr_detail.Tables[0].Rows[0]["GLImpact"].ToString();
                        dbcr_plusminus = dsdebcr_detail.Tables[0].Rows[0]["InventoryImpact"].ToString();
                    }

                    if (dbcr_plusminus == "PLUS")
                    {
                        totalcrdb = totalcrdb + float.Parse(lblValue.Text);
                    }
                    else
                    {
                        totalcrdb = totalcrdb - float.Parse(lblValue.Text);
                    }

                }

                string dbcr = "";
                DataSet dsdebcr = dml.Find("SELECT	GLImpact, InventoryImpact FROM SET_AcRules4Doc WHERE DocId = '" + ddlDocName.SelectedItem.Value + "' AND MasterDetail = 'M' AND CompID = 1 AND Record_Deleted = 0 AND IsActive = 1 AND IsHide = 0 ;");
                if (dsdebcr.Tables[0].Rows.Count > 0)
                {
                    dbcr = dsdebcr.Tables[0].Rows[0]["GLImpact"].ToString();
                }
                if (dbcr == "Debit Impact")
                {
                    gl.GlentryInsert(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, ViewState["SNO"].ToString(), "0", dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, RadComboAcct_Code.Text, bpnatureid, ddlSupplier.SelectedItem.Value, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), totalcrdb.ToString(), "0", txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff"), "0", "0", "0", "Inv_InventoryInDetail");

                }
                else
                {
                    gl.GlentryInsert(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, ViewState["SNO"].ToString(), "0", dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, RadComboAcct_Code.Text, bpnatureid, ddlSupplier.SelectedItem.Value, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), "0", totalcrdb.ToString(), txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff"), "0", "0", "0", "Inv_InventoryInDetail");

                }

                gl.GlentrySummaryDelete(txtDocNo.Text);
                gl.GlentrySummaryDetail(show_username(), DateTime.Now.ToString(), "0", txtDocNo.Text);


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
            string squer = "select * from ViewForFUD_FinPurchaseInv";


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
                swhere = swhere + " and VoucherNo = '" + ddlDel_DocNO.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and VoucherNo is not null";
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

            if (chkDel.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkDel.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and  [DocID] = '18' and    [GocID] = '" + gocid() + "' and [BranchId]='" + branchId() + "' and  [FiscalYearID] = '" + FiscalYear() + "' and CompId = '" + compid() + "'  ORDER BY DocDescription";

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
            string squer = "select * from ViewForFUD_FinPurchaseInv";


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
                swhere = swhere + " and VoucherNo = '" + ddlFind_DocNO.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and VoucherNo is not null";
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

            if (chkFind.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkFind.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and  [DocID] = '18' and    [GocID] = '" + gocid() + "' and [BranchId]='" + branchId() + "' and  [FiscalYearID] = '" + FiscalYear() + "' and CompId = '" + compid() + "'  ORDER BY DocDescription";
            //squer = squer + " where " + swhere + " and Record_Deleted = 0  and    [GocID] = '" + gocid() + "' and [BranchId]='" + branchId() + "' and  [FiscalYearID] = '" + FiscalYear() + "' and CompId = '" + compid() + "'  ORDER BY DocDescription";

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
            string squer = "select * from ViewForFUD_FinPurchaseInv";


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
                swhere = swhere + " and VoucherNo = '" + ddlEdit_DocNO.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and VoucherNo is not null";
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

            if (chkEdit_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkEdit_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and  [DocID] = '18' and  [GocID] = '" + gocid() + "' and [BranchId]='" + branchId() + "' and  [FiscalYearID] = '" + FiscalYear() + "' and CompId = '" + compid() + "'  ORDER BY DocDescription";



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
        Div5.Visible = false;
        Div6.Visible = false;
        Div7.Visible = false;


        ddlDocName.Enabled = false;
        ddlSupplier.Enabled = false;
        ddlDocAuth.Enabled = false;
        ddlLocationFrom.Enabled = false;
        ddlLocationTo.Enabled = false;
        ddlStatus.Enabled = false;
        ddlCostCentrFrom.Enabled = false;
        ddlCostCenterTo.Enabled = false;
       
        ddlPO_GRN.Enabled = false;
        txtBillBal.Enabled = false;
        txtCreditterm.Enabled = false;
        txtSalTax.Enabled = false;
        imgPopup.Enabled = false;
        ImageButton2.Enabled = false;
        ImageButton9.Enabled = false;
        ImageButton10.Enabled = false;



        txtEntryDate.Enabled = false;
        txtDocNo.Enabled = false;
        txtDeliveryChallan.Enabled = false;
        txtDeliveryChallanDate.Enabled = false;
        txtInwardGatePassNo.Enabled = false;
        txtRemarks.Enabled = false;
        txtShopOffName.Enabled = false;
        txtbillNo.Enabled = false;
        txtBilldate.Enabled = false;
        txtWeighbridgeNo.Enabled = false;
        txtNetWeight.Enabled = false;
        chkActive.Enabled = false;
        chkDirectGRN.Enabled = false;
        chkNormalGRN.Enabled = false;
        txtbillNo.Enabled = false;
        txtBilldate.Enabled = false;
        txtgstBill.Enabled = false;
        txtTruckNo.Enabled = false;
        chkPO.Enabled = false;
        chkPO.Checked = false;

        div_add_A.Visible = true;
        div_add_FED.Visible = false;

        lstFruits.Enabled = false;
        lstFruits.ClearSelection();

        if (chkDirectGRN.Checked != true)
        {
          // ddlPO_GRN.SelectedIndex = 0;
            ddlPO_GRN.Enabled = false;
        }


        ddlDocName.SelectedIndex = 0;
        ddlSupplier.SelectedIndex = 0;
        ddlDocAuth.SelectedIndex = 0;
        ddlLocationFrom.SelectedIndex = 0;
        ddlLocationTo.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        ddlCostCentrFrom.SelectedIndex = 0;
        ddlCostCenterTo.SelectedIndex = 0;
        //   ddltransfer.SelectedIndex = 0;

        //  ddltransfer.Enabled = false;
        txtEntryDate.Text = "";
        txtDocNo.Text = "";
        txtDeliveryChallan.Text = "";
        txtDeliveryChallanDate.Text = "";
        txtInwardGatePassNo.Text = "";
        txtRemarks.Text = "";
        txtShopOffName.Text = "";
        txtbillNo.Text = "";
        txtBilldate.Text = "";
        txtWeighbridgeNo.Text = "";
        txtNetWeight.Text = "";
        txtCreateddate.Text = "";
        txtUpdateDate.Text = "";
        Label1.Text = "";

        //ddlPO_GRN.Text = "";
        txtBillBal.Text = "";
        txtCreditterm.Text = "";
        txtSalTax.Text = "";
       


        chkDirectGRN.Checked = false;
        chkNormalGRN.Checked = false;
        chkActive.Checked = false;
        txtCreateddate.Enabled = false;
        txtUpdateDate.Enabled = false;

        RadComboAcct_Code.Enabled = false;
        RadComboAcct_Code.Text = "";
        btnShowJV.Visible = false;


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
        //FormID = Request.QueryString["FormID"];
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

            string balqtyPO = "0";
            float newbalqtyPO = 0;
            string qtydetail = "0";
            // gridview pono, qty

            foreach (GridViewRow row in GridView9.Rows)
            {
                Label lblPONo = (Label)row.FindControl("lblPONo");
                Label lblPObalQty = (Label)row.FindControl("lblPObalQty");

                Label lblSno = (Label)row.FindControl("lblpurSno");


                DataSet ds = dml.Find("Select BalanceAmount,Quantity from Fin_PurchaseDetail  where Sno = '" + lblSno.Text + "'");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    balqtyPO = ds.Tables[0].Rows[0]["BalanceAmount"].ToString();
                    qtydetail = ds.Tables[0].Rows[0]["Quantity"].ToString();
                }
                if (float.Parse(lblPObalQty.Text) <= float.Parse(qtydetail))
                {
                    newbalqtyPO = float.Parse(balqtyPO) + float.Parse(lblPObalQty.Text);

                    //lblPONo
                    dml.Update("Update Fin_PurchaseDetail set BalanceAmount='" + newbalqtyPO.ToString() + "' where Sno =  '" + lblSno.Text + "' ", "");
                    



                    dml.Update("UPDATE Fin_PurchaseMaster set Status = '1' where Sno in (select Sno_Master from Fin_PurchaseDetail where Sno = '" + lblSno.Text + "')", "");
                }
                else
                {
                    Label1.Text = "quantity exceeded";
                }
            }

            //SELECT * from Set_PurchaseOrderDetail where Sno_Master = 4
            dml.Delete("update Inv_InventoryInMaster set Record_Deleted = 1 where Sno= " + ViewState["SNO"].ToString() + "", "");
            dml.Delete("update Inv_InventoryInDetail set Record_Deleted = 1 where Sno_Master= " + ViewState["SNO"].ToString() + "", "");
            dml.Delete("update Act_GL_Detail set Record_Deleted = 1 where MasterSno= " + ViewState["SNO"].ToString() + "", "");
            dml.Delete("INSERT INTO Act_GL_Detail_Deleted ([DocId], [DocDescription], [MasterSno], [DetailSno], [EntryDate], [VoucherNo], [AccountCode], [BPNatureID], [BPartnerId], [Name], [ChqNo], [ChqDate], [InvoiceNo], [InvoiceDate], [DrAmount], [CrAmount], [Narration], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [Record_Deleted]) SELECT [DocId], [DocDescription], [MasterSno], [DetailSno], [EntryDate], [VoucherNo], [AccountCode], [BPNatureID], [BPartnerId], [Name], [ChqNo], [ChqDate], [InvoiceNo], [InvoiceDate], [DrAmount], [CrAmount], [Narration], [GocID], [CompId], [BranchId], [FiscalYearID], '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', [UpdatedBy], [UpdatedDate], '1' FROM Act_GL_Detail WHERE MasterSno='" + ViewState["SNO"].ToString() + "';", "");
            dml.Delete("Delete from Act_GL_Detail where MasterSno = " + ViewState["SNO"].ToString() + "", "");
            dml.Delete("Delete from Act_GL where MasterSno = " + ViewState["SNO"].ToString() + "", "");

            gl.GlentrySummaryDelete(txtDocNo.Text);



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

        btnShowJV.Visible = true;

        //  updatecol.Visible = true;
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {
            string serial_id;
            serial_id = GridView1.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;

            DataSet ds = dml.Find("select * from Fin_PurchaseMaster WHERE ([Sno]='" + serial_id + "') and Record_Deleted = '0'");
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlDocName.ClearSelection();
                //ddlSupplier.ClearSelection();
                ddlDocAuth.ClearSelection();

                ddlLocationTo.ClearSelection();
                ddlStatus.ClearSelection();
                ddlCostCentrFrom.ClearSelection();
                ddlCostCenterTo.ClearSelection();

                ddlDocName.Items.FindByValue(ds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;
                // ddlSupplier.Items.FindByValue(ds.Tables[0].Rows[0]["BPartnerID"].ToString()).Selected = true;
                ddlDocAuth.Items.FindByValue(ds.Tables[0].Rows[0]["DocumentAuthority"].ToString()).Selected = true;
                ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["Status"].ToString()).Selected = true;

                txtDeliveryChallan.Text = ds.Tables[0].Rows[0]["DeliveryChallan"].ToString();
                txtDeliveryChallanDate.Text = ds.Tables[0].Rows[0]["DeliveryChallanDate"].ToString();
                txtInwardGatePassNo.Text = ds.Tables[0].Rows[0]["InwardGatePass"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                txtShopOffName.Text = ds.Tables[0].Rows[0]["Shop_OfficeName"].ToString();
                txtbillNo.Text = ds.Tables[0].Rows[0]["BillNo"].ToString();
                txtBilldate.Text = ds.Tables[0].Rows[0]["BillDate"].ToString();
                txtWeighbridgeNo.Text = ds.Tables[0].Rows[0]["WeighbridgeNo"].ToString();
                txtNetWeight.Text = ds.Tables[0].Rows[0]["NetWeight"].ToString();
                txtTruckNo.Text = ds.Tables[0].Rows[0]["TruckNo"].ToString();
                txtSalTax.Text = ds.Tables[0].Rows[0]["StaxInvNo"].ToString();
                txtCreditterm.Text = ds.Tables[0].Rows[0]["CreditTerm"].ToString();
                txtDueDate.Text = ds.Tables[0].Rows[0]["DueDate"].ToString();




                ddlSupplier.ClearSelection();
                ddlLocationTo.ClearSelection();
                ddlCostCenterTo.ClearSelection();



                if (ds.Tables[0].Rows[0]["BPartnerID"].ToString() != null)
                {
                    ddlSupplier.Items.FindByValue(ds.Tables[0].Rows[0]["BPartnerID"].ToString()).Selected = true;
                }

                if (ds.Tables[0].Rows[0]["Store_Department"].ToString() != "0")
                {
                    ddlLocationTo.Items.FindByValue(ds.Tables[0].Rows[0]["Store_Department"].ToString()).Selected = true;
                }
                if (ds.Tables[0].Rows[0]["CostCenter"].ToString() != "0")
                {
                    ddlCostCenterTo.Items.FindByValue(ds.Tables[0].Rows[0]["CostCenter"].ToString()).Selected = true;
                }

                txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtDocNo.Text = ds.Tables[0].Rows[0]["VoucherNo"].ToString();


                txtCreateddate.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString()) + " " + ds.Tables[0].Rows[0]["CreatedDate"].ToString();
                txtUpdateDate.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString()) + " " + ds.Tables[0].Rows[0]["UpdatedDate"].ToString();


                dml.dateConvert(txtBilldate);
                dml.dateConvert(txtEntryDate);
                dml.dateConvert(txtDueDate);

                //  string drcode = ds.Tables[0].Rows[0]["DrAccountCode"].ToString();
                string Crcode = ds.Tables[0].Rows[0]["CrAccountCode"].ToString();



                if (Crcode != "")
                {
                    RadComboAcct_Code.Text = Crcode;
                }


                bool chkActive_b = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                //  bool chkd_p = bool.Parse(ds.Tables[0].Rows[0]["DirectPO_NoramlPO"].ToString());
                if (chkActive_b == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }

                chkDirectGRN.Checked = true;
                lstFruits.Visible = false;

                DataTable dtup = new DataTable();
                // dtup.Columns.AddRange(new DataColumn[17] { new DataColumn("Sno"), new DataColumn("DrAccountCode"), new DataColumn("PONO1"), new DataColumn("ItemSubHeadName"), new DataColumn("Description"), new DataColumn("UOMName"), new DataColumn("pobal"), new DataColumn("pobal1"), new DataColumn("Rate"), new DataColumn("Value"), new DataColumn("CostCenterName"), new DataColumn("LocName"), new DataColumn("Project"), new DataColumn("Warrentee"), new DataColumn("uomName2"), new DataColumn("Qty2"), new DataColumn("Rate2") });
                dtup.Columns.AddRange(new DataColumn[18]
                    {
             new DataColumn("AcctCode"),
              new DataColumn("ItemSubHead"),
                new DataColumn("ItemMaster"),
                new DataColumn("UOM"),
                new DataColumn("DCQty"),
                new DataColumn("Rate"),
                new DataColumn("Value"),
                new DataColumn("Gst"),
                new DataColumn("AddGst"),
                new DataColumn("TotalAmount"),
                new DataColumn("BalanceAmount"),
                new DataColumn("dc_po_grn"),
                new DataColumn("CostCenter"),
                new DataColumn("Location"),
                new DataColumn("Project"),
                new DataColumn("Warrantee"),
                new DataColumn("Qty2"),
                new DataColumn("Rate2")
                    });

                //select * from view_StockUpdate
                //select ItemSubHeadName , Description,CostCenterName,UOMName from view_StockUpdate
                DataSet ds_detail = dml.Find("select * from View_FinPurDetail_FED where Sno_Master = '" + serial_id + "'");
                if (ds_detail.Tables[0].Rows.Count > 0)
                {
                    ViewState["Customers1"] = ds_detail.Tables[0];
                    Div5.Visible = true;
                    GridView8.DataSource = ds_detail.Tables[0];
                    GridView8.DataBind();
                }
                //grd_Add

                DataSet ds_detailadd = dml.Find("select * from Fin_PurchaseOthers where SnoMaster = '" + serial_id + "'");
                if (ds_detailadd.Tables[0].Rows.Count > 0)
                {
                    // ViewState["Customers1"] = ds_detail.Tables[0];
                    div_add_A.Visible = false;
                    div_add_FED.Visible = true;
                    GrdV_Add_FED.DataSource = ds_detailadd.Tables[0];
                    GrdV_Add_FED.DataBind();
                }
                Div1.Visible = false;
                Div2.Visible = false;
                Div3.Visible = false;
                Div4.Visible = false;
                Div7.Visible = false;
                Div6.Visible = false;
                PopulateGridview_UpGrid10();




                UserGrpID = Request.QueryString["UsergrpID"];
                FormID = Request.QueryString["FormID"];
                dml.dateRuleForm(UserGrpID, FormID, CalendarExtender1, "E");
                txtBillBal.Text = ds.Tables[0].Rows[0]["BillBalance"].ToString();
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
        btnShowJV.Visible = true;

        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        DeleteBox.Visible = false;
        try
        {
            string serial_id;
            serial_id = GridView2.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;

            DataSet ds = dml.Find("select * from Fin_PurchaseMaster WHERE ([Sno]='" + serial_id + "') and Record_Deleted = '0'");
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlDocName.ClearSelection();
                //ddlSupplier.ClearSelection();
                ddlDocAuth.ClearSelection();

                ddlLocationTo.ClearSelection();
                ddlStatus.ClearSelection();
                ddlCostCentrFrom.ClearSelection();
                ddlCostCenterTo.ClearSelection();

                ddlDocName.Items.FindByValue(ds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;
                // ddlSupplier.Items.FindByValue(ds.Tables[0].Rows[0]["BPartnerID"].ToString()).Selected = true;
                ddlDocAuth.Items.FindByValue(ds.Tables[0].Rows[0]["DocumentAuthority"].ToString()).Selected = true;
                ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["Status"].ToString()).Selected = true;

                txtDeliveryChallan.Text = ds.Tables[0].Rows[0]["DeliveryChallan"].ToString();
                txtDeliveryChallanDate.Text = ds.Tables[0].Rows[0]["DeliveryChallanDate"].ToString();
                txtInwardGatePassNo.Text = ds.Tables[0].Rows[0]["InwardGatePass"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                txtShopOffName.Text = ds.Tables[0].Rows[0]["Shop_OfficeName"].ToString();
                txtbillNo.Text = ds.Tables[0].Rows[0]["BillNo"].ToString();
                txtBilldate.Text = ds.Tables[0].Rows[0]["BillDate"].ToString();
                txtWeighbridgeNo.Text = ds.Tables[0].Rows[0]["WeighbridgeNo"].ToString();
                txtNetWeight.Text = ds.Tables[0].Rows[0]["NetWeight"].ToString();
                txtTruckNo.Text = ds.Tables[0].Rows[0]["TruckNo"].ToString();
                txtSalTax.Text = ds.Tables[0].Rows[0]["StaxInvNo"].ToString();
                txtCreditterm.Text = ds.Tables[0].Rows[0]["CreditTerm"].ToString();
                txtDueDate.Text = ds.Tables[0].Rows[0]["DueDate"].ToString();




                ddlSupplier.ClearSelection();
                ddlLocationTo.ClearSelection();
                ddlCostCenterTo.ClearSelection();



                if (ds.Tables[0].Rows[0]["BPartnerID"].ToString() != null)
                {
                    ddlSupplier.Items.FindByValue(ds.Tables[0].Rows[0]["BPartnerID"].ToString()).Selected = true;
                }

                if (ds.Tables[0].Rows[0]["Store_Department"].ToString() != "0")
                {
                    ddlLocationTo.Items.FindByValue(ds.Tables[0].Rows[0]["Store_Department"].ToString()).Selected = true;
                }
                if (ds.Tables[0].Rows[0]["CostCenter"].ToString() != "0")
                {
                    ddlCostCenterTo.Items.FindByValue(ds.Tables[0].Rows[0]["CostCenter"].ToString()).Selected = true;
                }

                txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtDocNo.Text = ds.Tables[0].Rows[0]["VoucherNo"].ToString();


                txtCreateddate.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString()) + " " + ds.Tables[0].Rows[0]["CreatedDate"].ToString();
                txtUpdateDate.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString()) + " " + ds.Tables[0].Rows[0]["UpdatedDate"].ToString();


                dml.dateConvert(txtBilldate);
                dml.dateConvert(txtEntryDate);
                dml.dateConvert(txtDueDate);

                //  string drcode = ds.Tables[0].Rows[0]["DrAccountCode"].ToString();
                string Crcode = ds.Tables[0].Rows[0]["CrAccountCode"].ToString();



                if (Crcode != "")
                {
                    RadComboAcct_Code.Text = Crcode;
                }


                bool chkActive_b = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                //  bool chkd_p = bool.Parse(ds.Tables[0].Rows[0]["DirectPO_NoramlPO"].ToString());
                if (chkActive_b == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }

                chkDirectGRN.Checked = true;
                lstFruits.Visible = false;

                DataTable dtup = new DataTable();
                // dtup.Columns.AddRange(new DataColumn[17] { new DataColumn("Sno"), new DataColumn("DrAccountCode"), new DataColumn("PONO1"), new DataColumn("ItemSubHeadName"), new DataColumn("Description"), new DataColumn("UOMName"), new DataColumn("pobal"), new DataColumn("pobal1"), new DataColumn("Rate"), new DataColumn("Value"), new DataColumn("CostCenterName"), new DataColumn("LocName"), new DataColumn("Project"), new DataColumn("Warrentee"), new DataColumn("uomName2"), new DataColumn("Qty2"), new DataColumn("Rate2") });
                dtup.Columns.AddRange(new DataColumn[18]
                    {
             new DataColumn("AcctCode"),
              new DataColumn("ItemSubHead"),
                new DataColumn("ItemMaster"),
                new DataColumn("UOM"),
                new DataColumn("DCQty"),
                new DataColumn("Rate"),
                new DataColumn("Value"),
                new DataColumn("Gst"),
                new DataColumn("AddGst"),
                new DataColumn("TotalAmount"),
                new DataColumn("BalanceAmount"),
                new DataColumn("dc_po_grn"),
                new DataColumn("CostCenter"),
                new DataColumn("Location"),
                new DataColumn("Project"),
                new DataColumn("Warrantee"),
                new DataColumn("Qty2"),
                new DataColumn("Rate2")
                    });

                //select * from view_StockUpdate
                //select ItemSubHeadName , Description,CostCenterName,UOMName from view_StockUpdate
                DataSet ds_detail = dml.Find("select * from View_FinPurDetail_FED where Sno_Master = '" + serial_id + "'");
                if (ds_detail.Tables[0].Rows.Count > 0)
                {
                    ViewState["Customers1"] = ds_detail.Tables[0];
                    Div5.Visible = true;
                    GridView8.DataSource = ds_detail.Tables[0];
                    GridView8.DataBind();
                }
                //grd_Add

                DataSet ds_detailadd = dml.Find("select * from Fin_PurchaseOthers where SnoMaster = '" + serial_id + "'");
                if (ds_detailadd.Tables[0].Rows.Count > 0)
                {
                    // ViewState["Customers1"] = ds_detail.Tables[0];
                    div_add_A.Visible = false;
                    div_add_FED.Visible = true;
                    GrdV_Add_FED.DataSource = ds_detailadd.Tables[0];
                    GrdV_Add_FED.DataBind();
                }
                Div1.Visible = false;
                Div2.Visible = false;
                Div3.Visible = false;
                Div4.Visible = false;
                Div7.Visible = false;
                Div6.Visible = false;
                PopulateGridview_UpGrid10();




                UserGrpID = Request.QueryString["UsergrpID"];
                FormID = Request.QueryString["FormID"];
                dml.dateRuleForm(UserGrpID, FormID, CalendarExtender1, "E");
                txtBillBal.Text = ds.Tables[0].Rows[0]["BillBalance"].ToString();
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }

    }
    protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
    {
        //textClear();

        btnInsert.Visible = false;
        btnCancel.Visible = true;
        btnUpdate.Visible = true;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnFind.Visible = false;
        btnUpdatePO.Visible = false;
        Label1.Text = "";

        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        btnShowJV.Visible = true;

        //  ddlDocName.Enabled = true;
        ddlSupplier.Enabled = true;
        //ddlDocAuth.Enabled = true;
        
        ddlLocationTo.Enabled = true;
        //ddlStatus.Enabled = true;
        ddlCostCentrFrom.Enabled = true;
        ddlCostCenterTo.Enabled = true;

        lbltotalvalue.Text = "0";
        txtEntryDate.Enabled = true;
        txtDocNo.Enabled = false;
        txtDeliveryChallan.Enabled = true;
        txtDeliveryChallanDate.Enabled = true;
        txtInwardGatePassNo.Enabled = true;
        txtRemarks.Enabled = true;
        txtShopOffName.Enabled = true;
        txtbillNo.Enabled = true;
        txtBilldate.Enabled = true;
        txtWeighbridgeNo.Enabled = true;
        txtNetWeight.Enabled = true;
        chkActive.Enabled = true;
        chkDirectGRN.Enabled = true;
        chkNormalGRN.Enabled = true;


        try
        {
            string serial_id;
            serial_id = GridView3.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            string Query = "select * from Fin_PurchaseMaster WHERE ([Sno]='" + serial_id + "') and Record_Deleted = '0'";
            DataSet ds = dml.Find(Query);
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlDocName.ClearSelection();
                //ddlSupplier.ClearSelection();
                ddlDocAuth.ClearSelection();
               
                ddlLocationTo.ClearSelection();
                ddlStatus.ClearSelection();
                ddlCostCentrFrom.ClearSelection();
                ddlCostCenterTo.ClearSelection();

                ddlDocName.Items.FindByValue(ds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;
                // ddlSupplier.Items.FindByValue(ds.Tables[0].Rows[0]["BPartnerID"].ToString()).Selected = true;
                ddlDocAuth.Items.FindByValue(ds.Tables[0].Rows[0]["DocumentAuthority"].ToString()).Selected = true;
                ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["Status"].ToString()).Selected = true;

                txtDeliveryChallan.Text = ds.Tables[0].Rows[0]["DeliveryChallan"].ToString();
                txtDeliveryChallanDate.Text = ds.Tables[0].Rows[0]["DeliveryChallanDate"].ToString();
                txtInwardGatePassNo.Text = ds.Tables[0].Rows[0]["InwardGatePass"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                txtShopOffName.Text = ds.Tables[0].Rows[0]["Shop_OfficeName"].ToString();
                txtbillNo.Text = ds.Tables[0].Rows[0]["BillNo"].ToString();
                txtBilldate.Text = ds.Tables[0].Rows[0]["BillDate"].ToString();
                txtWeighbridgeNo.Text = ds.Tables[0].Rows[0]["WeighbridgeNo"].ToString();
                txtNetWeight.Text = ds.Tables[0].Rows[0]["NetWeight"].ToString();
                txtTruckNo.Text = ds.Tables[0].Rows[0]["TruckNo"].ToString();
                txtSalTax.Text = ds.Tables[0].Rows[0]["StaxInvNo"].ToString();
                txtCreditterm.Text = ds.Tables[0].Rows[0]["CreditTerm"].ToString();
                txtDueDate.Text = ds.Tables[0].Rows[0]["DueDate"].ToString();
               



                ddlSupplier.ClearSelection();
                ddlLocationTo.ClearSelection();
                ddlCostCenterTo.ClearSelection();
                


                if (ds.Tables[0].Rows[0]["BPartnerID"].ToString() != null)
                {
                    ddlSupplier.Items.FindByValue(ds.Tables[0].Rows[0]["BPartnerID"].ToString()).Selected = true;
                }

                if (ds.Tables[0].Rows[0]["Store_Department"].ToString() != "0")
                {
                    ddlLocationTo.Items.FindByValue(ds.Tables[0].Rows[0]["Store_Department"].ToString()).Selected = true;
                }
                if (ds.Tables[0].Rows[0]["CostCenter"].ToString() != "0")
                {
                    ddlCostCenterTo.Items.FindByValue(ds.Tables[0].Rows[0]["CostCenter"].ToString()).Selected = true;
                }

                txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtDocNo.Text = ds.Tables[0].Rows[0]["VoucherNo"].ToString();
              
               
                txtCreateddate.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString()) + " " + ds.Tables[0].Rows[0]["CreatedDate"].ToString();
                txtUpdateDate.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString()) + " " + ds.Tables[0].Rows[0]["UpdatedDate"].ToString();


                dml.dateConvert(txtBilldate);
                dml.dateConvert(txtEntryDate);
                dml.dateConvert(txtDueDate);

                //  string drcode = ds.Tables[0].Rows[0]["DrAccountCode"].ToString();
                string Crcode = ds.Tables[0].Rows[0]["CrAccountCode"].ToString();

               

                if (Crcode != "")
                {
                    RadComboAcct_Code.Text = Crcode;
                }


                bool chkActive_b = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                //  bool chkd_p = bool.Parse(ds.Tables[0].Rows[0]["DirectPO_NoramlPO"].ToString());
                if (chkActive_b == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }

                chkDirectGRN.Checked = true;
                lstFruits.Visible = false;

                DataTable dtup = new DataTable();
               // dtup.Columns.AddRange(new DataColumn[17] { new DataColumn("Sno"), new DataColumn("DrAccountCode"), new DataColumn("PONO1"), new DataColumn("ItemSubHeadName"), new DataColumn("Description"), new DataColumn("UOMName"), new DataColumn("pobal"), new DataColumn("pobal1"), new DataColumn("Rate"), new DataColumn("Value"), new DataColumn("CostCenterName"), new DataColumn("LocName"), new DataColumn("Project"), new DataColumn("Warrentee"), new DataColumn("uomName2"), new DataColumn("Qty2"), new DataColumn("Rate2") });
                dtup.Columns.AddRange(new DataColumn[18]
                    {
             new DataColumn("AcctCode"),
              new DataColumn("ItemSubHead"),
                new DataColumn("ItemMaster"),
                new DataColumn("UOM"),
                new DataColumn("DCQty"),
                new DataColumn("Rate"),
                new DataColumn("Value"),
                new DataColumn("Gst"),
                new DataColumn("AddGst"),
                new DataColumn("TotalAmount"),
                new DataColumn("BalanceAmount"),
                new DataColumn("dc_po_grn"),
                new DataColumn("CostCenter"),
                new DataColumn("Location"),
                new DataColumn("Project"),
                new DataColumn("Warrantee"),
                new DataColumn("Qty2"),
                new DataColumn("Rate2")
                    });
                string Query1 = "select * from View_FinPurDetail_FED where Sno_Master = '" + serial_id + "'";
                //select * from view_StockUpdate
                //select ItemSubHeadName , Description,CostCenterName,UOMName from view_StockUpdate
                DataSet ds_detail = dml.Find(Query1);
                if (ds_detail.Tables[0].Rows.Count > 0)
                {
                    ViewState["Customers1"] = ds_detail.Tables[0];
                    Div5.Visible = true;
                    GridView8.DataSource = ds_detail.Tables[0];
                    GridView8.DataBind();
                }
                //grd_Add
                Query1 = "select * from Fin_PurchaseOthers where SnoMaster = '" + serial_id + "'";
                DataSet ds_detailadd = dml.Find(Query1);
                if (ds_detailadd.Tables[0].Rows.Count > 0)
                {
                    // ViewState["Customers1"] = ds_detail.Tables[0];
                    div_add_A.Visible = false;
                    div_add_FED.Visible = true;
                    GrdV_Add_FED.DataSource = ds_detailadd.Tables[0];
                    GrdV_Add_FED.DataBind();
                }
                Div1.Visible = false;
                Div2.Visible = false;
                Div3.Visible = false;
                Div4.Visible = false;
                Div7.Visible = false;
                Div6.Visible = false;
                PopulateGridview_UpGrid10();




                UserGrpID = Request.QueryString["UsergrpID"];
                FormID = Request.QueryString["FormID"];
                dml.dateRuleForm(UserGrpID, FormID, CalendarExtender1, "E");
                txtBillBal.Text = ds.Tables[0].Rows[0]["BillBalance"].ToString();
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    public void PopulateGridview_UpGrid10()
    {

        DataTable dtbl = (DataTable)ViewState["Customers1"];

        if (dtbl.Rows.Count > 0)
        {

            GridView10.DataSource = (DataTable)ViewState["Customers1"];
            GridView10.DataBind();

        }
        else
        {


            dtbl.Rows.Add(dtbl.NewRow());
            GridView10.DataSource = dtbl;
            GridView10.DataBind();

            GridView10.Rows[0].Cells.Clear();
            GridView10.Rows[0].Cells.Add(new TableCell());
            GridView10.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
            GridView10.Rows[0].Cells[0].Text = "No Data Found ..!";
            GridView10.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;

        }
    }
    public void PopulateGridview_Up()
    {
        Div3.Visible = true;
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
    public void findeditdel()
    {
        Div6.Visible = true;
        DataTable dtbl = (DataTable)ViewState["dtup"];

        if (dtbl.Rows.Count > 0)
        {

            GridView9.DataSource = (DataTable)ViewState["dtup"];
            GridView9.DataBind();

        }
        else
        {


            dtbl.Rows.Add(dtbl.NewRow());
            GridView9.DataSource = dtbl;
            GridView9.DataBind();

            GridView9.Rows[0].Cells.Clear();
            GridView9.Rows[0].Cells.Add(new TableCell());
            GridView9.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
            GridView9.Rows[0].Cells[0].Text = "No Data Found ..!";
            GridView9.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;

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
    public string fiscalstart(string fyear)
    {
        string sdate;
        DataSet ds = dml.Find("select StartDate from SET_Fiscal_Year where FiscalYearId = "+FiscalYear());
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
    public string detailcond()
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
    public void PopulateGridview()
    {

        DataTable dtbl = (DataTable)ViewState["DirectDetail"];

        if (dtbl.Rows.Count > 0)
        {

            GridView6.DataSource = (DataTable)ViewState["DirectDetail"];
            GridView6.DataBind();

        }
        else
        {


            dtbl.Rows.Add(dtbl.NewRow());
            GridView6.DataSource = dtbl;
            GridView6.DataBind();

            GridView6.Rows[0].Cells.Clear();
          //  GridView6.Rows[0].Cells.Add(new TableCell());
            //GridView6.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
          //  GridView6.Rows[0].Cells[0].Text = "No Data Found ..!";
          //  GridView6.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;

        }
    }

    public void PopulateGridview1(GridView gv)
    {

        DataTable dtbl = (DataTable)ViewState["Customers"];
        
        if (dtbl.Rows.Count > 0)
        {

            gv.DataSource = (DataTable)ViewState["Customers"];
            gv.DataBind();

        }
        else
        {


            dtbl.Rows.Add(dtbl.NewRow());
            gv.DataSource = dtbl;
            gv.DataBind();

            gv.Rows[0].Cells.Clear();
            gv.Rows[0].Cells.Add(new TableCell());
            //  gv.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
            gv.Rows[0].Cells[0].Text = "No Data Found ..!";
            gv.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;

        }
    }

    public void PopulateGridview2(GridView gv, string add_red)
    {

        DataTable dtbl = (DataTable)ViewState[add_red];

        if (dtbl.Rows.Count > 0)
        {

            gv.DataSource = (DataTable)ViewState[add_red];
            gv.DataBind();

        }
        else
        {


            dtbl.Rows.Add(dtbl.NewRow());
            gv.DataSource = dtbl;
            gv.DataBind();

            gv.Rows[0].Cells.Clear();
            gv.Rows[0].Cells.Add(new TableCell());
            //     gv.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
            gv.Rows[0].Cells[0].Text = "No Data Found ..!";
            gv.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;

        }
    }
    public void PopulateGridviewGridView8()
    {

        //DataTable dtbl = (DataTable)ViewState["Customers"];

        //if (dtbl.Rows.Count > 0)
        //{

        //    GridView8.DataSource = (DataTable)ViewState["Customers"];
        //    GridView8.DataBind();

        //}
        //else
        //{

        //    DataSet ds = dml.Find("select  * from Set_PurchaseOrderDetail where Sno_Master in (select Sno from Set_PurcahseOrderMaster where [DocumentNo.] = '1920-000001' and Status != 2 and Status != 8  )");
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //      //  dtbl.Rows.Add(dtbl.NewRow());
        //        GridView8.DataSource = ds.Tables[0];
        //        GridView8.DataBind();

        //    }


        //    //dtbl.Rows.Add(dtbl.NewRow());
        //    //GridView8.DataSource = dtbl;
        //    //GridView8.DataBind();

        //    GridView8.Rows[0].Cells.Clear();
        //    GridView8.Rows[0].Cells.Add(new TableCell());
        //    GridView8.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
        //    GridView8.Rows[0].Cells[0].Text = "No Data Found ..!";
        //    GridView8.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;

        // }
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



    }
    protected void txtRate_PR_TextChanged(object sender, EventArgs e)
    {



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
    public void detailinsert(string masterid, string InvMasterID, string voucherNoGRNdetail)
    {

        float totalcrdb = 0;
        string detailid = "0";
        string bpnatureid = "0";
        //string detailid = "0";
        string doctype = "0";
        DataSet dsdoctype = dml.Find("select DocTypeId from SET_Documents where DocID = '" + ddlDocName.SelectedItem.Value + "';");
        if (dsdoctype.Tables[0].Rows.Count > 0)
        {

            doctype = dsdoctype.Tables[0].Rows[0]["DocTypeId"].ToString();

        }
        foreach (GridViewRow g in GridView6.Rows)
        {

            Label lblAcountDetail = (Label)g.FindControl("lblAccountCode");
            Label lblItemSubHead = (Label)g.FindControl("lblItemSubHead");
            Label lblitemmaster = (Label)g.FindControl("lblItemMaster");
            Label lbluom = (Label)g.FindControl("lblUOM");
            Label lblQty = (Label)g.FindControl("lblDCQty");
            Label lblRate = (Label)g.FindControl("lblRate");
            Label lblValue = (Label)g.FindControl("lblValue");
            Label lblGST = (Label)g.FindControl("lblGST");
            Label lblAddGST = (Label)g.FindControl("lblAddGST");
            Label lblTotalAmount = (Label)g.FindControl("lblTotalAmount");
            Label lblBalance = (Label)g.FindControl("lblBalance");
            //lblBalanceTax
            Label lblBalanceTax = (Label)g.FindControl("lblBalanceTax");
            Label lblDC_PO_GRN = (Label)g.FindControl("lblDC_PO_GRN");
            Label lblCostCenter = (Label)g.FindControl("lblCostCenter");
            Label lblLocation = (Label)g.FindControl("lblLocation");
            Label lblProject = (Label)g.FindControl("lblProject");
            Label lblWarrantee = (Label)g.FindControl("lblWarrantee");
            Label lblQty2 = (Label)g.FindControl("lblQty2");
            Label lblRate2 = (Label)g.FindControl("lblRate2");


            //
            //select ItemSubHeadID, ItemSubHeadName from SET_ItemSubHead lblproject

            DataSet ds = dml.Find("select ItemSubHeadID, ItemSubHeadName from SET_ItemSubHead where ItemSubHeadName = '" + lblItemSubHead.Text + "'");
            string subhead, itemmaster, uom1, uom2, costcenter, location, itemcode, itemtypeid;
            if (ds.Tables[0].Rows.Count > 0)
            {
                subhead = ds.Tables[0].Rows[0]["ItemSubHeadID"].ToString();
            }
            else
            {
                subhead = "0";
            }
            DataSet ds1 = dml.Find("select  ItemID , Description, ItemTypeID, ItemCode from SET_ItemMaster where Description = '" + lblitemmaster.Text + "'");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                itemmaster = ds1.Tables[0].Rows[0]["ItemID"].ToString();
                itemcode = ds1.Tables[0].Rows[0]["ItemCode"].ToString();
                itemtypeid = ds1.Tables[0].Rows[0]["ItemTypeID"].ToString();
            }
            else
            {
                itemmaster = "0";
                itemcode = "0";
                itemtypeid = "0";
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

            DataSet ds3 = dml.Find("select  CostCenterID,CostCenterName from SET_CostCenter where CostCenterName = '" + lblCostCenter.Text + "'");
            if (ds3.Tables[0].Rows.Count > 0)
            {
                costcenter = ds3.Tables[0].Rows[0]["CostCenterID"].ToString();
            }
            else
            {
                costcenter = "0";
            }


            DataSet ds5 = dml.Find("select LocId,LocName from Set_Location where LocName = '" + lblLocation.Text + "'");
            if (ds5.Tables[0].Rows.Count > 0)
            {
                location = ds5.Tables[0].Rows[0]["LocId"].ToString();
            }
            else
            {
                location = "0";
            }
            DataSet ds4 = dml.Find("select  UOMID,UOMName from SET_UnitofMeasure where UOMName = '" + lbluom.Text + "'");
            if (ds4.Tables[0].Rows.Count > 0)
            {
                uom2 = ds4.Tables[0].Rows[0]["UOMID"].ToString();
            }
            else
            {
                uom2 = "0";
            }
            float balqty, balqty2;

            string addtax = "0" ,  gst="0";
            if (lblAddGST.Text != "Please select...")
            {
                string Query = "SELECT SUBSTRING('" + lblAddGST.Text + "', CHARINDEX('(', '" + lblAddGST.Text + "') + 1, CHARINDEX(')', '" + lblAddGST.Text + "') - CHARINDEX('(', '" + lblAddGST.Text + "') - 1) AS output";
                DataSet dsgst = dml.Find(Query);
                if (dsgst.Tables[0].Rows.Count > 0)
                {
                    addtax = dsgst.Tables[0].Rows[0]["output"].ToString();
                }
            }
            if (lblGST.Text != "Please select...")
            {
                DataSet dsgsta = dml.Find("SELECT SUBSTRING('" + lblGST.Text + "', CHARINDEX('(', '" + lblGST.Text + "') + 1, CHARINDEX(')', '" + lblGST.Text + "') - CHARINDEX('(', '" + lblGST.Text + "') - 1) AS output");
                if (dsgsta.Tables[0].Rows.Count > 0)
                {
                    gst = dsgsta.Tables[0].Rows[0]["output"].ToString();
                }
            }
            int DisplayDigit = 0,FinancialRoundOff=0;
            DataSet Branch = dml.Find("Select * from Set_Branch where BranchId="+Convert.ToInt32(Request.Cookies["BranchId"].Value));

            if (dml.ReturnDataCount(Branch) > 0) {
                DisplayDigit = dml.returnDmlResultInInt(Branch, "DisplayDigit");
                FinancialRoundOff = dml.returnDmlResultInInt(Branch, "FinancialRoundOff");
            }

            if (DisplayDigit > 0) {
                lblTotalAmount.Text = utl.RoundOff(DisplayDigit, FinancialRoundOff, lblTotalAmount.Text);
            }

           string query= "INSERT INTO Fin_PurchaseDetail "
                + "([Sno_Master], [DcNo], [GrnNo], [PoNo], [ItemSubHead], [ItemMaster], "
                + "[DrAccountCode], [UOM], [Quantity], [Rate], [Value], [Tax%], [TaxAmount],"
                + " [TaxCodeDr], [AddTax%], [AddTaxAmount], [AddTaxCodeDr], [TotalAmount], [BalanceAmount], "
                + "[Remarks], [Qty2], [UOM2], [Rate2], [ReferNo], [GLReferNo], [Store_Department],"
                + " [CostCenter], [GocId], [CompId], [BranchId], [FiscalYearID], [CreatedBy],"
                + " [CreatedDate], [Record_Deleted], [Project], [Warrentee],"
                + " [TempLockUser],[BalanceTax])"

                + " VALUES ('" + masterid + "', '" + ddlDocName.SelectedItem.Value + "', NULL, NULL, '" + subhead + "', "
                + " '" + itemmaster + "', '" + lblAcountDetail.Text + "', '" + uom1 + "', '" + lblQty.Text + "', '" + lblRate.Text + "', '" + lblValue.Text + "',"
                + " '" + gst + "', '" + lbltaxamount.Text + "', NULL, '" + addtax + "', '" + lblAddtaxamount.Text + "', NULL, "
                + "'" + lblTotalAmount.Text + "', '" + lblBalance.Text + "', NULL, '" + lblQty2.Text + "', NULL, '" + lblRate2.Text + "', NULL, NULL, "
                + "'" + location + "', '" + costcenter + "', " + gocid() + ", " + compid() + ", " + branchId() + "," + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "',"
                + " '0', '" + lblProject.Text + "', '" + lblWarrantee.Text + "', NULL,'" + lblBalanceTax.Text + "'); SELECT * FROM Fin_PurchaseDetail WHERE Sno = SCOPE_IDENTITY()";

            DataSet dsinsertdetail = dml.Find(query);

            string InsertDetailQuery = "INSERT INTO Inv_InventoryInDetail ( [Sno_Master], [PONo],  [CrAccountCode], [ItemSubHead],"
                                     + " [ItemMaster], [UOM], [Quantity], [Rate], [Value], [Remarks], [Qty2], [UOM2], [Rate2], [BalQty],"
                                     + " [BalQty2], [ReferNo], [GLReferNo], [CostCenter], [Location], [CompId], [BranchId], [FiscalYearID],"
                                     + " [CreatedBy], [CreatedDate], [Record_Deleted], [Project], [Warrentee]) "

                                     + "VALUES ('" + InvMasterID + "', NULL, NULL, '" + subhead + "', '" + itemmaster + "', '" + uom1 + "', '" + lblQty.Text + "',"
                                     + " '" + lblRate.Text + "', '" + lblValue.Text + "', NULL, '" + lblQty2.Text + "', '" + uom2 + "', '" + lblRate2.Text + "', NULL, NULL, NULL, NULL,"
                                     + " '" + costcenter + "', '" + location + "', " + compid() + ", " + branchId() + ", " + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0' ,"
                                     + " '" + lblProject.Text + "', '" + lblWarrantee.Text + "');SELECT * FROM Inv_InventoryInDetail WHERE Sno = SCOPE_IDENTITY()";

            DataSet dsinsertdetailGRN = dml.Find(InsertDetailQuery);

            if (dsinsertdetail.Tables[0].Rows.Count > 0)
            {
                detailid = dsinsertdetail.Tables[0].Rows[0]["Sno"].ToString();

            }
           

            float totalbalamtDR = float.Parse(lblBalanceTax.Text) + float.Parse(lblValue.Text);
            gl.GlentryInsertofTransferIN(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, masterid, detailid, dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, lblAcountDetail.Text, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), totalbalamtDR.ToString(), "0", txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyy hh:mm:ss.ffff"), itemcode, "0", "0", "Fin_PurchaseDetail");

            gl.GlentryInsertofTransferIN("8", doctype, "GRN Direct", masterid, detailid, dml.dateconvertforinsertNEW(txtEntryDate), voucherNoGRNdetail, lblAcountDetail.Text, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), totalbalamtDR.ToString(), "0", txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyy hh:mm:ss.ffff"), itemcode, "0", "0", "Fin_PurchaseDetail");



        }
        //gl master entry

        string dbcr = "";
        DataSet dsdebcr = dml.Find("SELECT GLImpact, InventoryImpact FROM SET_AcRules4Doc WHERE DocId = '" + ddlDocName.SelectedItem.Value + "' AND MasterDetail = 'M' AND CompID = 1 AND Record_Deleted = 0 AND IsActive = 1 AND IsHide = 0 ;");
        if (dsdebcr.Tables[0].Rows.Count > 0)
        {
            dbcr = dsdebcr.Tables[0].Rows[0]["GLImpact"].ToString();
        }
       
            string ids = gl.GlentryInsert_WITHID(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, masterid, "0", dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, RadComboAcct_Code.Text, "", "", txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), "0", txtBillBal.Text, txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff"), "0", "0", "0", "Fin_PurchaseMaster");

            dml.Update("UPDATE Fin_PurchaseMaster set GLReferNo = '" + ids + "' where Sno = '" + masterid + "'", "");
            gl.fwdidIn(ids, masterid);


    
    }
    public void detailinsertForPO(string masterid, string InvMasterID)
    {

        float totalcrdb = 0;
        string detailid = "0";
        string bpnatureid = "0";
        //string detailid = "0";
        string doctype = "0";
        DataSet dsdoctype = dml.Find("select DocTypeId from SET_Documents where DocID = '" + ddlDocName.SelectedItem.Value + "';");
        if (dsdoctype.Tables[0].Rows.Count > 0)
        {

            doctype = dsdoctype.Tables[0].Rows[0]["DocTypeId"].ToString();

        }
        foreach (GridViewRow g in GridView7.Rows)
        {

            Label lblAcountDetail = (Label)g.FindControl("lblAccountCode");
            Label lblItemSubHead = (Label)g.FindControl("lblItemSubHead");
            Label lblitemmaster = (Label)g.FindControl("lblItemMaster");
            Label lbluom = (Label)g.FindControl("lblUOM");
            Label lblQty = (Label)g.FindControl("lblDCQty");
            Label lblRate = (Label)g.FindControl("lblRate");
            Label lblValue = (Label)g.FindControl("lblValue");
            Label lblGST = (Label)g.FindControl("lblGST");
            DropDownList ddlAddGST = (DropDownList)g.FindControl("ddladdGST");
            Label lblTotalAmount = (Label)g.FindControl("lblTotalAmount");
            Label lblBalance = (Label)g.FindControl("lblBalanceAmtAmount");
            Label lblDC_PO_GRN = (Label)g.FindControl("lblDC_PO_GRN");
            Label lblCostCenter = (Label)g.FindControl("lblCostCenter");
            Label lblLocation = (Label)g.FindControl("lblLocation");
            Label lblProject = (Label)g.FindControl("lblProject");
            Label lblWarrantee = (Label)g.FindControl("lblWarrantee");
            Label lblQty2 = (Label)g.FindControl("lblQty2");
            Label lblRate2 = (Label)g.FindControl("lblRate2");


            //
            //select ItemSubHeadID, ItemSubHeadName from SET_ItemSubHead lblproject

            DataSet ds = dml.Find("select ItemSubHeadID, ItemSubHeadName from SET_ItemSubHead where ItemSubHeadName = '" + lblItemSubHead.Text + "'");
            string subhead, itemmaster, uom1, uom2, costcenter, location, itemcode, itemtypeid;
            if (ds.Tables[0].Rows.Count > 0)
            {
                subhead = ds.Tables[0].Rows[0]["ItemSubHeadID"].ToString();
            }
            else
            {
                subhead = "0";
            }
            DataSet ds1 = dml.Find("select  ItemID , Description, ItemTypeID, ItemCode from SET_ItemMaster where Description = '" + lblitemmaster.Text + "'");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                itemmaster = ds1.Tables[0].Rows[0]["ItemID"].ToString();
                itemcode = ds1.Tables[0].Rows[0]["ItemCode"].ToString();
                itemtypeid = ds1.Tables[0].Rows[0]["ItemTypeID"].ToString();
            }
            else
            {
                itemmaster = "0";
                itemcode = "0";
                itemtypeid = "0";
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

            DataSet ds3 = dml.Find("select  CostCenterID,CostCenterName from SET_CostCenter where CostCenterName = '" + lblCostCenter.Text + "'");
            if (ds3.Tables[0].Rows.Count > 0)
            {
                costcenter = ds3.Tables[0].Rows[0]["CostCenterID"].ToString();
            }
            else
            {
                costcenter = "0";
            }


            DataSet ds5 = dml.Find("select LocId,LocName from Set_Location where LocName = '" + lblLocation.Text + "'");
            if (ds5.Tables[0].Rows.Count > 0)
            {
                location = ds5.Tables[0].Rows[0]["LocId"].ToString();
            }
            else
            {
                location = "0";
            }
            DataSet ds4 = dml.Find("select  UOMID,UOMName from SET_UnitofMeasure where UOMName = '" + lbluom.Text + "'");
            if (ds4.Tables[0].Rows.Count > 0)
            {
                uom2 = ds4.Tables[0].Rows[0]["UOMID"].ToString();
            }
            else
            {
                uom2 = "0";
            }
            float balqty, balqty2;

            string addtax = "0";
            if (ddlAddGST.SelectedIndex != 0)
            {
                DataSet dsgst = dml.Find("SELECT SUBSTRING('" + ddlAddGST.SelectedItem.Text + "', CHARINDEX('(', '" + ddlAddGST.SelectedItem.Text + "') + 1, CHARINDEX(')', '" + ddlAddGST.SelectedItem.Text + "') - CHARINDEX('(', '" + ddlAddGST.SelectedItem.Text + "') - 1) AS output");
                if (dsgst.Tables[0].Rows.Count > 0)
                {
                    addtax = dsgst.Tables[0].Rows[0]["output"].ToString();
                }
            }

            DataSet dsinsertdetail = dml.Find("INSERT INTO Fin_PurchaseDetail "
                + "([Sno_Master], [DcNo], [GrnNo], [PoNo], [ItemSubHead], [ItemMaster], "
                + "[DrAccountCode], [UOM], [Quantity], [Rate], [Value], [Tax%], [TaxAmount],"
                + " [TaxCodeDr], [AddTax%], [AddTaxAmount], [AddTaxCodeDr], [TotalAmount], [BalanceAmount], "
                + "[Remarks], [Qty2], [UOM2], [Rate2], [ReferNo], [GLReferNo], [Store_Department],"
                + " [CostCenter], [GocId], [CompId], [BranchId], [FiscalYearID], [CreatedBy],"
                + " [CreatedDate], [Record_Deleted], [Project], [Warrentee],"
                + " [TempLockUser])"

                + " VALUES ('" + masterid + "', NULL , NULL, '" + ddlPO_GRN.SelectedItem.Value + "', '" + subhead + "', "
                + " '" + itemmaster + "', '" + lblAcountDetail.Text + "', '" + uom1 + "', '" + lblQty.Text + "', '" + lblRate.Text + "', '" + lblValue.Text + "',"
                + " '" + lblGST.Text + "', '" + lbltaxamount.Text + "', NULL, '" + addtax + "', '" + lblAddtaxamount.Text + "', NULL, "
                + "'" + lblTotalAmount.Text + "', '" + lblBalance.Text + "', NULL, '" + lblQty2.Text + "', NULL, '" + lblRate2.Text + "', NULL, NULL, "
                + "'" + location + "', '" + costcenter + "', " + gocid() + ", " + compid() + ", " + branchId() + ", " + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "',"
                + " '0', '" + lblProject.Text + "', '" + lblWarrantee.Text + "', NULL); SELECT * FROM Fin_PurchaseDetail WHERE Sno = SCOPE_IDENTITY()");




            DataSet dsinsertdetailGRN = dml.Find("INSERT INTO Inv_InventoryInDetail ( [Sno_Master], [PONo],  [CrAccountCode], [ItemSubHead],"
                                     + " [ItemMaster], [UOM], [Quantity], [Rate], [Value], [Remarks], [Qty2], [UOM2], [Rate2], [BalQty],"
                                     + " [BalQty2], [ReferNo], [GLReferNo], [CostCenter], [Location], [CompId], [BranchId], [FiscalYearID],"
                                     + " [CreatedBy], [CreatedDate], [Record_Deleted], [Project], [Warrentee]) "

                                     + "VALUES ('" + InvMasterID + "', NULL, NULL, '" + subhead + "', '" + itemmaster + "', '" + uom1 + "', '" + lblQty.Text + "',"
                                     + " '" + lblRate.Text + "', '" + lblValue.Text + "', NULL, '" + lblQty2.Text + "', '" + uom2 + "', '" + lblRate2.Text + "', NULL, NULL, NULL, NULL,"
                                     + " '" + costcenter + "', '" + location + "', " + compid() + ", " + branchId() + ", " + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0' ,"
                                     + " '" + lblProject.Text + "', '" + lblWarrantee.Text + "');SELECT * FROM Inv_InventoryInDetail WHERE Sno = SCOPE_IDENTITY()");

            if (dsinsertdetail.Tables[0].Rows.Count > 0)
            {
                detailid = dsinsertdetail.Tables[0].Rows[0]["Sno"].ToString();

            }
            //DataSet dsnature = dml.Find("select  BPNatureID,BPNatureDescription from SET_BPartnerNature where BPNatureID in (select BPNatureID from SET_BPartnerType where BPartnerID = '"+ddlSupplier.SelectedItem.Value+ "')");
            //if(dsnature.Tables[0].Rows.Count> 0)
            //{
            //    bpnatureid = dsnature.Tables[0].Rows[0]["BPNatureID"].ToString();
            //}
            //DataSet dsdebcr_detail = dml.Find("SELECT	GLImpact, InventoryImpact FROM SET_AcRules4Doc WHERE DocId = '" + ddlDocName.SelectedItem.Value + "' AND MasterDetail = 'D' AND CompID = '" + compid() + "' AND Record_Deleted = 0 AND IsActive = 1 AND IsHide = 0 ;");
            //if (dsdebcr_detail.Tables[0].Rows.Count > 0)
            //{
            //    dbcr_detail = dsdebcr_detail.Tables[0].Rows[0]["GLImpact"].ToString();
            //    dbcr_plusminus = dsdebcr_detail.Tables[0].Rows[0]["InventoryImpact"].ToString();
            //}


            //string CODEitemtype = gl.itemtypecode(dbcr_detail, itemtypeid);



            //gl Detail entry
            gl.GlentryInsertofTransferIN(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, masterid, detailid, dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, lblAcountDetail.Text, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), lblValue.Text, "0", txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyy hh:mm:ss.ffff"), itemcode, "0", "0", "Fin_PurchaseDetail");

            //  dml.Update("update Set_QuotationReqMaster set Status = '6' where QuotationReqNO = '" + requisno.Text + "'", "");



            //if (dbcr_plusminus == "PLUS")
            //{
            //    totalcrdb = totalcrdb + float.Parse(lblValue.Text);
            //}
            //else if (dbcr_plusminus == "MINUS")
            //{
            //    totalcrdb = totalcrdb - float.Parse(lblValue.Text);
            //}
            //else
            //{
            //    totalcrdb = totalcrdb + float.Parse(lblValue.Text);
            //}


        }
        //gl master entry


        string dbcr = "";
        DataSet dsdebcr = dml.Find("SELECT	GLImpact, InventoryImpact FROM SET_AcRules4Doc WHERE DocId = '" + ddlDocName.SelectedItem.Value + "' AND MasterDetail = 'M' AND CompID = 1 AND Record_Deleted = 0 AND IsActive = 1 AND IsHide = 0 ;");
        if (dsdebcr.Tables[0].Rows.Count > 0)
        {
            dbcr = dsdebcr.Tables[0].Rows[0]["GLImpact"].ToString();
        }
       // if (dbcr == "Debit Impact")
       // {
            //  gl.GlentryInsert(ddlDocName.SelectedItem.Value,  doctype,ddlDocName.SelectedItem.Text, masterid, "0", dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, RadComboAcct_Code.Text, bpnatureid, ddlSupplier.SelectedItem.Value, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), totalcrdb.ToString(), "0", txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff"), "0", "0", "0");

        //    string ids = gl.GlentryInsert_WITHID(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, masterid, "0", dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, RadComboAcct_Code.Text, "", "", txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), totalcrdb.ToString(), "0", txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff"), "0", "0", "0", "Inv_InventoryInMaster");

        //    dml.Update("UPDATE Fin_PurchaseMaster set GLReferNo = '" + ids + "' where = Sno = '" + masterid + "'", "");
          //  gl.fwdidIn(ids, masterid);
       

      //  }
      //  else
       // {
            // gl.GlentryInsert(ddlDocName.SelectedItem.Value, doctype,ddlDocName.SelectedItem.Text, masterid, "0", dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, RadComboAcct_Code.Text, bpnatureid, ddlSupplier.SelectedItem.Value, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), "0", totalcrdb.ToString(), txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff"), "0", "0", "0");

            string ids = gl.GlentryInsert_WITHID(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, masterid, "0", dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, RadComboAcct_Code.Text, "", "", txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), "0", totalcrdb.ToString(), txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff"), "0", "0", "0", "Fin_PurchaseMaster");

            dml.Update("UPDATE Fin_PurchaseMaster set GLReferNo = '" + ids + "' where Sno = '" + masterid + "'", "");
            gl.fwdidIn(ids, masterid);


        //}
    }
    public void detailinsertForGRN(string masterid, string InvMasterID)
    {

        float totalcrdb = 0;
        string detailid = "0";
        string bpnatureid = "0";
        //string detailid = "0";
        string doctype = "0";
        DataSet dsdoctype = dml.Find("select DocTypeId from SET_Documents where DocID = '" + ddlDocName.SelectedItem.Value + "';");
        if (dsdoctype.Tables[0].Rows.Count > 0)
        {

            doctype = dsdoctype.Tables[0].Rows[0]["DocTypeId"].ToString();

        }
        foreach (GridViewRow g in GridView10.Rows)
        {

            Label lblAcountDetail = (Label)g.FindControl("lblAccountCode");
            Label lblItemSubHead = (Label)g.FindControl("lblItemSubHead");
            Label lblitemmaster = (Label)g.FindControl("lblItemMaster");
            Label lbluom = (Label)g.FindControl("lblUOM");
            Label lblQty = (Label)g.FindControl("lblDCQty");
            Label lblRate = (Label)g.FindControl("lblRate");
            Label lblValue = (Label)g.FindControl("lblValue");
         //   Label lblGST = (Label)g.FindControl("lblGST");
            DropDownList lblGST = (DropDownList)g.FindControl("ddlGST");
            DropDownList ddlAddGST = (DropDownList)g.FindControl("ddladdGST");
            Label lblTotalAmount = (Label)g.FindControl("lblTotalAmount");
            Label lblBalance = (Label)g.FindControl("lblBalanceAmtAmount");
            Label lblDC_PO_GRN = (Label)g.FindControl("lblDC_PO_GRN");
            Label lblCostCenter = (Label)g.FindControl("lblCostCenter");
            Label lblLocation = (Label)g.FindControl("lblLocation");
            Label lblProject = (Label)g.FindControl("lblProject");
            Label lblWarrantee = (Label)g.FindControl("lblWarrantee");
            Label lblQty2 = (Label)g.FindControl("lblQty2");
            Label lblRate2 = (Label)g.FindControl("lblRate2");


            //
            //select ItemSubHeadID, ItemSubHeadName from SET_ItemSubHead lblproject

            DataSet ds = dml.Find("select ItemSubHeadID, ItemSubHeadName from SET_ItemSubHead where ItemSubHeadName = '" + lblItemSubHead.Text + "'");
            string subhead, itemmaster, uom1, uom2, costcenter, location, itemcode, itemtypeid;
            if (ds.Tables[0].Rows.Count > 0)
            {
                subhead = ds.Tables[0].Rows[0]["ItemSubHeadID"].ToString();
            }
            else
            {
                subhead = "0";
            }
            DataSet ds1 = dml.Find("select  ItemID , Description, ItemTypeID, ItemCode from SET_ItemMaster where Description = '" + lblitemmaster.Text + "'");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                itemmaster = ds1.Tables[0].Rows[0]["ItemID"].ToString();
                itemcode = ds1.Tables[0].Rows[0]["ItemCode"].ToString();
                itemtypeid = ds1.Tables[0].Rows[0]["ItemTypeID"].ToString();
            }
            else
            {
                itemmaster = "0";
                itemcode = "0";
                itemtypeid = "0";
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

            DataSet ds3 = dml.Find("select  CostCenterID,CostCenterName from SET_CostCenter where CostCenterName = '" + lblCostCenter.Text + "'");
            if (ds3.Tables[0].Rows.Count > 0)
            {
                costcenter = ds3.Tables[0].Rows[0]["CostCenterID"].ToString();
            }
            else
            {
                costcenter = "0";
            }


            DataSet ds5 = dml.Find("select LocId,LocName from Set_Location where LocName = '" + lblLocation.Text + "'");
            if (ds5.Tables[0].Rows.Count > 0)
            {
                location = ds5.Tables[0].Rows[0]["LocId"].ToString();
            }
            else
            {
                location = "0";
            }
            DataSet ds4 = dml.Find("select  UOMID,UOMName from SET_UnitofMeasure where UOMName = '" + lbluom.Text + "'");
            if (ds4.Tables[0].Rows.Count > 0)
            {
                uom2 = ds4.Tables[0].Rows[0]["UOMID"].ToString();
            }
            else
            {
                uom2 = "0";
            }
            float balqty, balqty2;

            string addtax = "0", accountCodeTax = "0", accountCodeAddTax = "0";
            if (ddlAddGST.SelectedIndex != 0)
            {
                DataSet dsgst = dml.Find("SELECT SUBSTRING('" + ddlAddGST.SelectedItem.Text + "', CHARINDEX('(', '" + ddlAddGST.SelectedItem.Text + "') + 1, CHARINDEX(')', '" + ddlAddGST.SelectedItem.Text + "') - CHARINDEX('(', '" + ddlAddGST.SelectedItem.Text + "') - 1) AS output");
                if (dsgst.Tables[0].Rows.Count > 0)
                {
                    addtax = dsgst.Tables[0].Rows[0]["output"].ToString();
                }
                DataSet dsaccttax = dml.Find("select AccountCode from SET_Tax where TaxTypeID = '"+ddlAddGST.SelectedItem.Value+"'");
                if (dsaccttax.Tables[0].Rows.Count > 0)
                {
                    accountCodeTax = dsaccttax.Tables[0].Rows[0]["AccountCode"].ToString();
                }
            }


            if (lblGST.SelectedIndex != 0)
            {
                DataSet dsaccttax = dml.Find("select AccountCode from SET_Tax where TaxTypeID = '"+lblGST.SelectedItem.Value+"'");
                if (dsaccttax.Tables[0].Rows.Count > 0)
                {
                    accountCodeAddTax = dsaccttax.Tables[0].Rows[0]["AccountCode"].ToString();
                }
            }
            string dbcr_detail = "", dbcr_plusminus = "";
            string trantype = "";
            string gst="0" , addgst = "0";
            if(lblGST.SelectedIndex != 0)
            {
                gst = lblGST.SelectedItem.Value;
            }
            if (ddlAddGST.SelectedIndex != 0)
            {
                addgst = ddlAddGST.SelectedItem.Value;
            }


            string id = gl.GlentryDetailwithID(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, masterid, detailid, dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, lblAcountDetail.Text, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), lblValue.Text, "0", txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyy hh:mm:ss.ffff"), itemcode, "0", "0", "Inv_InventoryInDetail");

            DataSet dsinsertdetail = dml.Find("INSERT INTO Fin_PurchaseDetail "
                + "([Sno_Master], [DcNo], [GrnNo], [PoNo], [ItemSubHead], [ItemMaster], "
                + "[DrAccountCode], [UOM], [Quantity], [Rate], [Value], [Tax%], [TaxAmount],"
                + " [TaxCodeDr], [AddTax%], [AddTaxAmount], [AddTaxCodeDr], [TotalAmount], [BalanceAmount], "
                + "[Remarks], [Qty2], [UOM2], [Rate2], [ReferNo], [GLReferNo], [Store_Department],"
                + " [CostCenter], [GocId], [CompId], [BranchId], [FiscalYearID], [CreatedBy],"
                + " [CreatedDate], [Record_Deleted], [Project], [Warrentee],"
                + " [TempLockUser])"

                + " VALUES ('" + masterid + "', NULL , '" + ddlPO_GRN.SelectedItem.Value+"', NULL, '" + subhead + "', "
                + " '" + itemmaster + "', '" + lblAcountDetail.Text + "', '" + uom1 + "', '" + lblQty.Text + "', '" + lblRate.Text + "', '" + lblValue.Text + "',"
                + " '" + gst + "', '" + lbltaxamount.Text + "', '"+ accountCodeTax + "', '" + addtax + "', '" + lblAddtaxamount.Text + "', '"+ accountCodeAddTax + "', "
                + "'" + lblTotalAmount.Text + "', '" + lblBalance.Text + "', NULL, '" + lblQty2.Text + "', NULL, '" + lblRate2.Text + "', NULL, '"+ id + "', "
                + "'" + location + "', '" + costcenter + "', " + gocid() + ", " + compid() + ", " + branchId() + ", " + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "',"
                + " '0', '" + lblProject.Text + "', '" + lblWarrantee.Text + "', NULL); SELECT * FROM Fin_PurchaseDetail WHERE Sno = SCOPE_IDENTITY()");



            if (dsinsertdetail.Tables[0].Rows.Count > 0)
            {
                detailid = dsinsertdetail.Tables[0].Rows[0]["Sno"].ToString();

            }
            DataSet dsnature = dml.Find("select  BPNatureID,BPNatureDescription from SET_BPartnerNature where BPNatureID in (select BPNatureID from SET_BPartnerType where BPartnerID = '" + ddlSupplier.SelectedItem.Value + "')");
            if (dsnature.Tables[0].Rows.Count > 0)
            {
                bpnatureid = dsnature.Tables[0].Rows[0]["BPNatureID"].ToString();
            }
            DataSet dsdebcr_detail = dml.Find("SELECT	GLImpact, InventoryImpact FROM SET_AcRules4Doc WHERE DocId = '" + ddlDocName.SelectedItem.Value + "' AND MasterDetail = 'D' AND CompID = '" + compid() + "' AND Record_Deleted = 0 AND IsActive = 1 AND IsHide = 0 ;");
            if (dsdebcr_detail.Tables[0].Rows.Count > 0)
            {
                dbcr_detail = dsdebcr_detail.Tables[0].Rows[0]["GLImpact"].ToString();
                dbcr_plusminus = dsdebcr_detail.Tables[0].Rows[0]["InventoryImpact"].ToString();
            }


            string CODEitemtype = gl.itemtypecode(dbcr_detail, itemtypeid);



            //gl Detail entry
            string ida =  gl.GlentryDetailwithID(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, masterid, detailid, dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, lblAcountDetail.Text, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), lblValue.Text, "0", txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyy hh:mm:ss.ffff"), itemcode, "0", "0", "Fin_PurchaseDetail");

            
            
            //  dml.Update("update Set_QuotationReqMaster set Status = '6' where QuotationReqNO = '" + requisno.Text + "'", "");



            //if (dbcr_plusminus == "PLUS")
            //{
            //    totalcrdb = totalcrdb + float.Parse(lblValue.Text);
            //}
            //else if (dbcr_plusminus == "MINUS")
            //{
            //    totalcrdb = totalcrdb - float.Parse(lblValue.Text);
            //}
            //else
            //{
            //    totalcrdb = totalcrdb + float.Parse(lblValue.Text);
            //}


        }
        //gl master entry


        string dbcr = "";
        DataSet dsdebcr = dml.Find("SELECT	GLImpact, InventoryImpact FROM SET_AcRules4Doc WHERE DocId = '" + ddlDocName.SelectedItem.Value + "' AND MasterDetail = 'M' AND CompID = 1 AND Record_Deleted = 0 AND IsActive = 1 AND IsHide = 0 ;");
        if (dsdebcr.Tables[0].Rows.Count > 0)
        {
            dbcr = dsdebcr.Tables[0].Rows[0]["GLImpact"].ToString();
        }
       // if (dbcr == "Debit Impact")
       // {
            //  gl.GlentryInsert(ddlDocName.SelectedItem.Value,  doctype,ddlDocName.SelectedItem.Text, masterid, "0", dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, RadComboAcct_Code.Text, bpnatureid, ddlSupplier.SelectedItem.Value, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), totalcrdb.ToString(), "0", txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff"), "0", "0", "0");

         //   string ids = gl.GlentryInsert_WITHID(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, masterid, "0", dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, RadComboAcct_Code.Text, "", "", txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), totalcrdb.ToString(), "0", txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff"), "0", "0", "0", "Inv_InventoryInMaster");

         //   dml.Update("UPDATE Fin_PurchaseMaster set GLReferNo = '" + ids + "' where = Sno = '" + masterid + "'", "");
         //   gl.fwdidIn(ids, masterid);


      //  }
       // else
      //  {
            // gl.GlentryInsert(ddlDocName.SelectedItem.Value, doctype,ddlDocName.SelectedItem.Text, masterid, "0", dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, RadComboAcct_Code.Text, bpnatureid, ddlSupplier.SelectedItem.Value, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), "0", totalcrdb.ToString(), txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff"), "0", "0", "0");

            string ids = gl.GlentryInsert_WITHID(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, masterid, "0", dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, RadComboAcct_Code.Text, "", "", txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), "0", totalcrdb.ToString(), txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff"), "0", "0", "0", "Fin_PurchaseMaster");

            dml.Update("UPDATE Fin_PurchaseMaster set GLReferNo = '" + ids + "' where Sno = '" + masterid + "'", "");
            gl.fwdidIn(ids, masterid);


       // }
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


            //            Qty Value = Quantity x Rate
            //GST Value = Quantity x GST Rate


            float qtyval = float.Parse(lblQty.Text) * float.Parse(lblRate.Text);
            float gstval = float.Parse(lblQty.Text) * float.Parse(lblGSTRate.Text);




            dml.Insert("INSERT INTO Set_PurchaseOrderDetail ([Sno_Master], [PRNo_AQno], [ItemSubHead], [ItemMaster], [UOM], [Quantity], [Rate], [GST], [GSTRate], [QtyValue], [GstValue], [GrossValue], [Remarts], [Qty2], [UOM2], [Rate2], [Location], [Project], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted],[ApprovedQuantity]) VALUES ('" + id + "', NULL, '" + subhead + "', '" + itemmaster + "', '" + uom1 + "', '" + lblQty.Text + "','" + lblRate.Text + "', '" + lblGST.Text + "', '" + lblGSTRate.Text + "', '" + qtyval + "', '" + gstval + "', '" + lblGrossValue1.Text + "', NULL, '" + lblQty2.Text + "', '" + uom1 + "', '" + lblRate2.Text + "', '" + location + "', '" + lblProject.Text + "', '1', " + gocid() + ", " + compid() + ", " + branchId() + ", " + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyy hh:mm:ss.ffff") + "', 0,'" + lblApprovedQty.Text + "');", "");

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
                    location = ds3.Tables[0].Rows[0]["CostCenterID"].ToString();
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
                int DisplayDigit = 0, FinancialRoundOff = 0;
                DataSet Branch = dml.Find("Select * From Set_Branch where BranchId="+Convert.ToInt32(Request.Cookies["BranchId"].Value));
                if (dml.ReturnDataCount(Branch) > 0) {
                    DisplayDigit = dml.returnDmlResultInInt(Branch, "DisplayDigit");
                    FinancialRoundOff = dml.returnDmlResultInInt(Branch, "FinancialRoundOff");
                }

                lblQtyVal.Text = utl.RoundOff(DisplayDigit, FinancialRoundOff, lblQtyVal.Text);
                lblGrossValue.Text = utl.RoundOff(DisplayDigit, FinancialRoundOff, lblGrossValue.Text);
                dml.Insert("INSERT INTO Set_PurchaseOrderDetail ([Sno_Master], [PRNo_AQno], [ItemSubHead], [ItemMaster], [UOM], [Quantity], [Rate], [GST], [GSTRate], [QtyValue], [GstValue], [GrossValue], [Remarts], [Qty2], [UOM2], [Rate2], [Location], [Project], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted],[ApprovedQuantity]) VALUES ('" + id + "', '" + lblreq.Text + "', '" + subhead + "', '" + itemmaster + "', '" + uom1 + "', '" + lblQty.Text + "','" + lblRate.Text + "', '" + lblGST.Text + "', '" + lblGSTRate.Text + "', '" + lblQtyVal.Text + "', '" + lblGstValue.Text + "', '" + lblGrossValue.Text + "', NULL, '" + lblQty2.Text + "', '" + uom2 + "', '" + lblRate2.Text + "', '" + location + "', '" + lblProject.Text + "', '1', " + gocid() + ", " + compid() + ", " + branchId() + ", " + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyy hh:mm:ss.ffff") + "', 0,'" + lblApprovedQty.Text + "');", "");
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
                int DisplayDigit=0, FinancialRoundOff = 0;
                DataSet Branch = dml.Find("Select * From Set_Branch Where BranchId="+Convert.ToInt32(Request.Cookies["BranchId"].Value));
                if (Branch.Tables[0].Rows.Count > 0) {
                    DisplayDigit = dml.returnDmlResultInInt(Branch, "DisplayDigit");
                    FinancialRoundOff = dml.returnDmlResultInInt(Branch, "FinancialRoundOff");
                }
                lblQtyVal.Text = utl.RoundOff(DisplayDigit, FinancialRoundOff, lblQtyVal.Text);
                lblGrossValue.Text = utl.RoundOff(DisplayDigit, FinancialRoundOff, lblGrossValue.Text);
                dml.Insert("INSERT INTO Set_PurchaseOrderDetail ([Sno_Master], [PRNo_AQno], [ItemSubHead], [ItemMaster], [UOM], [Quantity], [Rate], [GST], [GSTRate], [QtyValue], [GstValue], [GrossValue], [Remarts], [Qty2], [UOM2], [Rate2], [Location], [Project], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted],[ApprovedQuantity]) VALUES ('" + id + "', '" + lstFruits.SelectedItem.Text + "', '" + subhead + "', '" + itemmaster + "', '" + uom1 + "', '" + lblQty.Text + "','" + txtRate.Text + "', '" + txtGST.Text + "', '" + lblGSTRate.Text + "', '" + lblQtyVal.Text + "', '" + lblGstValue.Text + "', '" + lblGrossValue.Text + "', NULL, '" + lblQty2.Text + "', '" + uom2 + "', '" + lblRate2.Text + "', '" + location + "', '" + lblProject.Text + "', '1', '" + gocid() + "', '" + compid() + "', '" + branchId() + "', '" + FiscalYear() + "', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyy hh:mm:ss.ffff") + "', 0,'" + txtApprovedQty.Text + "');", "");
            }
        }
    }
    protected void GridView7_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lblRowNumber") as Label).Text = (e.Row.RowIndex + 1).ToString();
            //ddladdGST


            DropDownList ddla = e.Row.FindControl("ddlGST7") as DropDownList;
            dml.dropdownsqlwithquery(ddla, "SELECT TaxTypeID, TaxDescription +'(' + CONVERT(VARCHAR, Percentage) + ')' as perc from SET_Tax", "perc", "TaxTypeID");


            DropDownList ddl = e.Row.FindControl("ddladdGST") as DropDownList;
            dml.dropdownsqlwithquery(ddl, "SELECT TaxTypeID, TaxDescription +'(' + CONVERT(VARCHAR, Percentage) + ')' as perc from SET_Tax", "perc", "TaxTypeID");



            string f = (e.Row.FindControl("lblValue") as Label).Text;
            float fa = 0;
            if (f != "")
            {
                fa = float.Parse(f);
            }
            string displ = inv.displaydigit(branchId().ToString());
            if (displ == "0")
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0");
            }

            else if (displ == "1")
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.0");
            }
            else if (displ == "2")
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.00");
            }
            else if (displ == "3")
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.000");
            }
            else if (displ == "4")
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.0000");
            }
            else if (displ == "5")
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.00000");
            }
            else if (displ == "6")
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.000000");
            }
            else if (displ == "7")
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.0000000");
            }
            else if (displ == "8")
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.00000000");
            }
            else
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0");
            }

        }
    }
    protected void lblQty2A_TextChanged(object sender, EventArgs e)
    {

        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;
        string Qty2 = (row.FindControl("lblQty2A") as TextBox).Text;
        string lblQtyVal = (row.FindControl("lblQtyVal") as Label).Text;
        int DisplayDigit = 0, FinancialRouncOff = 0;
        DataSet Branch = dml.Find("Select * From Set_Branch where BranchId=" + Convert.ToInt32(Request.Cookies["BranchId"].Value));
        if (dml.ReturnDataCount(Branch)>0) {
            DisplayDigit = dml.returnDmlResultInInt(Branch, "DisplayDigit");
            FinancialRouncOff = dml.returnDmlResultInInt(Branch, "FinancialRoundOff");

        }

        Qty2 = utl.RoundOff(DisplayDigit, FinancialRouncOff, Qty2);
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
            int DisplayDigit = 0, FinancialRouncOff = 0;
            DataSet Branch = dml.Find("Select * From Set_Branch where BranchId=" + Convert.ToInt32(Request.Cookies["BranchId"].Value));
            if (dml.ReturnDataCount(Branch) > 0)
            {
                DisplayDigit = dml.returnDmlResultInInt(Branch, "DisplayDigit");
                FinancialRouncOff = dml.returnDmlResultInInt(Branch, "FinancialRoundOff");

            }

            txtGrossValue = utl.RoundOff(DisplayDigit, FinancialRouncOff, txtGrossValue); 
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


            //dml.Update("UPDATE [SET_StockRequisitionDetail] SET [ItemSubHead]='" + txtitemsubfooter.SelectedItem.Value + "', [ItemMaster]='" + txtdesc.SelectedItem.Value + "', [CostCenter]='" + txtsupplierFooter.SelectedItem.Value + "', [UOM]='" + txtuomFooter.SelectedItem.Value + "', [Project]='" + txtProjectEdit + "', [CurrentStock]='" + txtcurrStockFooter + "', [RequiredQuantity]='" + txtReqStockFooter + "', [Remarks]=NULL, [IsActive]='1', [GocID]='" + gocid() + "', [CompId]='" + compid() + "', [BranchId]='" + branchId() + "', [FiscalYearID]='" + FiscalYear() + "', [UpdatedBy]='" + show_username() + "', [UpdatedDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', [Record_Deleted]='0' WHERE ([Sno]='" + lblsno + "');", "");
            dml.Update("UPDATE Set_PurchaseOrderDetail SET  [PRNo_AQno]='" + lblreq + "', [ItemSubHead]='" + ddlitemsubedit.SelectedItem.Value + "', [ItemMaster]='" + ddlitemMasteredit.SelectedItem.Value + "', [UOM]='" + ddluomedit.SelectedItem.Value + "', [Quantity]='" + txtqtyvalue + "', [Rate]='" + txtRateEdit + "', [GST]='" + lblGST + "', [GSTRate]='" + txtGSTRate + "', [QtyValue]='" + txtqtyvalue + "', [GstValue]='" + txtgstvalue + "', [GrossValue]='" + txtGrossValue + "', [Remarts]=NULL, [Qty2]='" + txtQty2Edit + "', [UOM2]='" + ddluomedit.SelectedItem.Value + "', [Rate2]='" + txtRate2Edit + "', [Location]='" + ddlLocation.SelectedItem.Value + "', [Project]='" + txtproject + "', [UpdatedBy]='" + show_username() + "', [UpdatedDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' , [ApprovedQuantity]='" + txtApprovedQtyEdit + "' WHERE ([Sno]='" + lblsno + "')", "");
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
    protected void RadComboAcct_Code_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(RadComboAcct_Code, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
    public void doctype(string menuid, string formid, string usergrpid)
    {

        ddlDocName.ClearSelection();
        dml.dropdownsql2where(ddlDocName, "ViewUserGrp_Doc", "docn", "DocID", "MenuId_Sno", menuid, "FormId_Sno", formid, "UserGrpId", usergrpid);



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
    protected void ddlDocName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDocName.SelectedIndex != 0)
        {

            ddlSupplier.Enabled = true;
            ddlDocAuth.Enabled = true;
            ddlLocationFrom.Enabled = true;
            ddlLocationTo.Enabled = true;
            ddlStatus.Enabled = true;
            ddlCostCentrFrom.Enabled = true;
            ddlCostCenterTo.Enabled = true;
            
            txtEntryDate.Enabled = true;
            txtDocNo.Enabled = false;
            txtDeliveryChallan.Enabled = true;
            txtDeliveryChallanDate.Enabled = true;
            txtInwardGatePassNo.Enabled = true;
            txtRemarks.Enabled = true;
            txtShopOffName.Enabled = true;
            txtbillNo.Enabled = true;
            txtBilldate.Enabled = true;
            txtWeighbridgeNo.Enabled = true;
            txtNetWeight.Enabled = true;
            chkActive.Enabled = true;
            chkDirectGRN.Enabled = true;
            txtTruckNo.Enabled = true;
            txtBillBal.Enabled = true;
            txtCreditterm.Enabled = true;
            ddlPO_GRN.Enabled = true;
            txtBillBal.Enabled = false;
            txtCreditterm.Enabled = true;
            txtSalTax.Enabled = true;
            imgPopup.Enabled = true;
            ImageButton2.Enabled = true;
            ImageButton9.Enabled = true;
            ImageButton10.Enabled = true;

            txtSalTax.Enabled = true;
            chkNormalGRN.Enabled = true;
            
            txtCreateddate.Text = show_username() + " " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
            txtEntryDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtDeliveryChallanDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtBilldate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            chkDirectGRN.Checked = true;
            txtgstBill.Enabled = true;
            chkPO.Enabled = true;

            lstFruits.Enabled = true;
            
            Div4.Visible = false;
            chkActive.Checked = true;

            ddlStatus.ClearSelection();
            ddlStatus.Items.FindByText("Open").Selected = true;
            required_generate();


            ddlSupplier_SelectedIndexChanged(sender, e);
            docrule(ddlDocName.SelectedItem.Value);

            dml.dropdownsqlwithquery(ddlDocAuth, "select AuthorityId, AuthorityName from SET_Authority where AuthorityId in (SELECT AuthorityId from SET_UserGrp_DocAuthority where docid= '" + ddlDocName.SelectedItem.Value + "')", "AuthorityName", "AuthorityId");

            if (ddlDocAuth.Items.Count > 2)
            {
                ddlDocAuth.Enabled = true;
                Label1.Text = "";
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
                Label1.Text = "";
                ddlDocAuth.SelectedIndex = 1;
            }


            if (ddlDocAuth.Items.Count == 0)
            {

            }


            DataSet dsradio = dml.Find("select RadioButton from SET_DocRadioBinding where DocId= '" + ddlDocName.SelectedItem.Value + "' and Record_Deleted = 0 and IsActive = 1;");

            if (dsradio.Tables[0].Rows.Count > 0)
            {

                string d_p = dsradio.Tables[0].Rows[0]["RadioButton"].ToString();
                if (d_p == "DIRECT")
                {
                    chkDirectGRN.Checked = true;
                    chkNormalGRN.Checked = false;
                    chkPO.Checked = false;
                    chkDirectGRN.Enabled = false;
                    chkNormalGRN.Enabled = false;
                    chkPO.Enabled = false;
                   // chkDirectGRN_CheckedChanged(sender, e);

                }
                else if (d_p == "GRN")
                {
                    chkNormalGRN.Checked = true;
                    chkDirectGRN.Checked = false;
                    chkPO.Checked = false;
                    chkDirectGRN.Enabled = false;
                    chkNormalGRN.Enabled = false;
                    chkPO.Enabled = false;
                    //chkNormalGRN_CheckedChanged(sender, e);
                }
                else if (d_p == "PO")
                {
                    chkNormalGRN.Checked = false;
                    chkDirectGRN.Checked = false;
                    chkPO.Checked = true;
                    chkDirectGRN.Enabled = false;
                    chkNormalGRN.Enabled = false;
                    chkPO.Enabled = false;
                }
                else
                {
                    chkDirectGRN.Enabled = true;
                    chkNormalGRN.Enabled = true;
                    chkNormalGRN.Checked = false;
                    chkDirectGRN.Checked = false;
                    chkPO.Checked = false;
                    chkPO.Enabled = true;
                }
            }
            else
            {
                
              // textClear();
              //  Label1.Text = "No radio Button binding";
            }



        }
        else
        {
            textClear();
            ddlDocName.Enabled = true;
        }

    }
    public void required_generate()
    {
        DateTime date = DateTime.Parse(txtEntryDate.Text);
        string year = date.ToString("yy");
        string month = date.Month.ToString("00");
        //string month = date.ToString("00");
        string docno = ddlDocName.SelectedItem.Value;


        //  string month = DateTime.Now.Month.ToString("00");
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

            string a = docval + "-" + month + year;

            DataSet ds = dml.Find("select Monthlybase, YearlyBase from SET_Documents where DocID = '" + ddlDocName.SelectedItem.Value + "';select MAX(VoucherNo) as maxno from Fin_PurchaseMaster where SUBSTRING(VoucherNo, 0, 8) = '" + a + "'; select Description from SET_Fiscal_Year where Description = '" + fiscal + "'; select MAX(VoucherNo) as maxno from Fin_PurchaseMaster where SUBSTRING(VoucherNo, 0, 8) = '" + docval + "-" + fy + "'");

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
                        incre = int.Parse(inc.ToString().Substring(8, 5)) + 1;
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
                    inc = "00001";
                    flag1 = false;
                }
                if (yearly == "True")
                {
                    if (flag1 == true)
                    {
                        incre = int.Parse(inc.ToString().Substring(8, 5)) + 1;
                    }
                    else
                    {
                        incre = int.Parse(inc.ToString());
                    }
                    string fyear = ds.Tables[2].Rows[0]["Description"].ToString();
                    txtDocNo.Text = docno + "-" + fyear.Substring(2, 2) + fyear.Substring(8, 2) + "-" + incre.ToString("00000");


                }

            }
        }
    }
    public string required_generateforIns()
    {
        string pono = "";
        DateTime date = DateTime.Parse(txtEntryDate.Text);
        string year = date.ToString("yy");
        string month = date.Month.ToString("00");
        //string month = date.ToString("00");
        string docno = ddlDocName.SelectedItem.Value;


        //  string month = DateTime.Now.Month.ToString("00");
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
        }

        string a = docval + "-" + month + year;
        DataSet ds = dml.Find("select Monthlybase, YearlyBase from SET_Documents where DocID = '" + ddlDocName.SelectedItem.Value + "';select MAX(VoucherNo) as maxno from Fin_PurchaseMaster where SUBSTRING(VoucherNo, 0, 8) = '" + a + "'; select Description from SET_Fiscal_Year where Description = '" + fiscal + "'; select MAX(VoucherNo) as maxno from Fin_PurchaseMaster where SUBSTRING(VoucherNo, 0, 8) = '" + docval + "-" + fy + "'");

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
                    incre = int.Parse(inc.ToString().Substring(8, 5)) + 1;
                }
                else
                {
                    incre = int.Parse(inc.ToString());
                }

                // txtDocNo.Text = docno + "-" + month + year + "-" + incre.ToString("00000");
                pono = docno + "-" + month + year + "-" + incre.ToString("00000");
            }

            inc = ds.Tables[3].Rows[0]["maxno"].ToString();
            if (inc == "")
            {
                inc = "00001";
                flag1 = false;
            }
            if (yearly == "True")
            {
                if (flag1 == true)
                {
                    incre = int.Parse(inc.ToString().Substring(8, 5)) + 1;
                }
                else
                {
                    incre = int.Parse(inc.ToString());
                }
                string fyear = ds.Tables[2].Rows[0]["Description"].ToString();
                //txtDocNo.Text = docno + "-" + fyear.Substring(2, 2) + fyear.Substring(7, 2) + "-" + incre.ToString("00000");
                pono = docno + "-" + fyear.Substring(2, 2) + fyear.Substring(8, 2) + "-" + incre.ToString("00000");

            }

        }
        return pono;
    }

    public string required_generateforInsforDoc(string dcno)
    {
         string pono = "";
        DateTime date = DateTime.Parse(txtEntryDate.Text);
        string year = date.ToString("yy");
        string month = date.Month.ToString("00");
        //string month = date.ToString("00");
        string docno = dcno;


        //  string month = DateTime.Now.Month.ToString("00");
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
            docval = docno;
        }

        string a = docval + "-" + month + year;
        DataSet ds = dml.Find("select Monthlybase, YearlyBase from SET_Documents where DocID = '" + dcno + "';select MAX(DocumentNo) as maxno from Inv_InventoryInMaster where SUBSTRING(DocumentNo, 0, 8) = '" + a + "'; select Description from SET_Fiscal_Year where Description = '" + fiscal + "'; select MAX(DocumentNo) as maxno from Inv_InventoryInMaster where SUBSTRING(DocumentNo, 0, 8) = '" + docval + "-" + fy + "'");

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
                    incre = int.Parse(inc.ToString().Substring(8, 5)) + 1;
                }
                else
                {
                    incre = int.Parse(inc.ToString());
                }

                // txtDocNo.Text = docno + "-" + month + year + "-" + incre.ToString("00000");
                pono = docno + "-" + month + year + "-" + incre.ToString("00000");
            }

            inc = ds.Tables[3].Rows[0]["maxno"].ToString();
            if (inc == "")
            {
                inc = "00001";
                flag1 = false;
            }
            if (yearly == "True")
            {
                if (flag1 == true)
                {
                    incre = int.Parse(inc.ToString().Substring(8, 5)) + 1;
                }
                else
                {
                    incre = int.Parse(inc.ToString());
                }
                string fyear = ds.Tables[2].Rows[0]["Description"].ToString();
                //txtDocNo.Text = docno + "-" + fyear.Substring(2, 2) + fyear.Substring(7, 2) + "-" + incre.ToString("00000");
                pono = docno + "-" + fyear.Substring(2, 2) + fyear.Substring(8, 2) + "-" + incre.ToString("00000");

            }

        }
        return pono;
    }



    protected void chkDirectGRN_CheckedChanged(object sender, EventArgs e)
    {

        if (chkDirectGRN.Checked == true)
        {
            Div2.Visible = true;
            Div5.Visible = false;
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[18]
            {
                new DataColumn("AcctCode"),
                new DataColumn("ItemSubHead"),
                new DataColumn("ItemMaster"),
                new DataColumn("UOM"),
                new DataColumn("DCQty"),
                new DataColumn("Rate"),
                new DataColumn("Value"),
                new DataColumn("Gst"),
                new DataColumn("AddGst"),
                new DataColumn("TotalAmount"),
                new DataColumn("BalanceAmount"),
                new DataColumn("dc_po_grn"),
                new DataColumn("CostCenter"),
                new DataColumn("Location"),
                new DataColumn("Project"),
                new DataColumn("Warrantee"),
                new DataColumn("Qty2"),
                new DataColumn("Rate2")
            });


            ViewState["DirectDetail"] = dt;


            this.PopulateGridview();
            DropDownList ddlsub = GridView6.FooterRow.FindControl("ddlItemSubHeadFooter") as DropDownList;
            dml.dropdownsql(ddlsub, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");

            DropDownList ddlmaster = GridView6.FooterRow.FindControl("ddlItemMasterFooter") as DropDownList;
            dml.dropdownsql(ddlmaster, "SET_ItemMaster", "Description", "ItemID");

            DropDownList ddluom = GridView6.FooterRow.FindControl("ddlUOMFooter") as DropDownList;
            dml.dropdownsql(ddluom, "SET_UnitofMeasure", "UOMName", "UOMID");

            DropDownList ddlsupFooter = GridView6.FooterRow.FindControl("lblCostCenterFooter") as DropDownList;
            dml.dropdownsql(ddlsupFooter, "SET_CostCenter", "CostCenterName", "CostCenterID");

            DropDownList ddluom2 = GridView6.FooterRow.FindControl("ddlUOM2Footer") as DropDownList;
            dml.dropdownsql(ddluom2, "SET_UnitofMeasure", "UOMName", "UOMID");

            DropDownList ddlLocation = GridView6.FooterRow.FindControl("ddlLocationFooter") as DropDownList;
            dml.dropdownsql(ddlLocation, "Set_Location", "LocName", "LocID");


            DropDownList ddlAddGSTFooter = GridView6.FooterRow.FindControl("ddlAddGSTFooter") as DropDownList;
            dml.dropdownsqlwithquery(ddlAddGSTFooter, "SELECT TaxTypeID, TaxDescription +'(' + CONVERT(VARCHAR, Percentage) + ')' as perc from SET_Tax", "perc", "TaxTypeID");

        }
        Div2.Visible = true;
        Div4.Visible = false;
        if (chkDirectGRN.Checked == true)
        {
            lstFruits.Visible = false;
            chkNormalGRN.Checked = false;

        }
        else
        {
            lstFruits.Visible = true;
            chkNormalGRN.Checked = true;
            chkNormalGRN_CheckedChanged(sender, e);


        }
        // gridview 1 open ho hga yaha 

    }
    protected void chkNormalGRN_CheckedChanged(object sender, EventArgs e)
    {
        if (chkNormalGRN.Checked == true)
        {

            Div4.Visible = true;

            Div5.Visible = false;
            Div2.Visible = false;
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[16]
            {
                new DataColumn("Accode"),
                new DataColumn("PONo"),
                new DataColumn("ItemSubHead"),
                new DataColumn("ItemMaster"),
                new DataColumn("UOM"),
                new DataColumn("PObalQty"),
                new DataColumn("DCQty"),
                new DataColumn("CostCenter"),
                new DataColumn("Location"),
                new DataColumn("Rate"),
                new DataColumn("Value"),
                new DataColumn("Project"),
                new DataColumn("Warrantee"),
                new DataColumn("UOM2"),
                new DataColumn("Qty2"),
                new DataColumn("Rate2")
            });


            ViewState["Customers"] = dt;


            this.PopulateGridview();
            DropDownList ddlsub = GridView6.FooterRow.FindControl("ddlItemSubHeadFooter") as DropDownList;
            dml.dropdownsql(ddlsub, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");

            DropDownList ddlmaster = GridView6.FooterRow.FindControl("ddlItemMasterFooter") as DropDownList;
            dml.dropdownsql(ddlmaster, "SET_ItemMaster", "Description", "ItemID");

            DropDownList ddluom = GridView6.FooterRow.FindControl("ddlUOMFooter") as DropDownList;
            dml.dropdownsql(ddluom, "SET_UnitofMeasure", "UOMName", "UOMID");

            DropDownList ddlsupFooter = GridView6.FooterRow.FindControl("lblCostCenterFooter") as DropDownList;
            dml.dropdownsql(ddlsupFooter, "SET_CostCenter", "CostCenterName", "CostCenterID");

            DropDownList ddluom2 = GridView6.FooterRow.FindControl("ddlUOM2Footer") as DropDownList;
            dml.dropdownsql(ddluom2, "SET_UnitofMeasure", "UOMName", "UOMID");

            DropDownList ddlLocation = GridView6.FooterRow.FindControl("ddlLocationFooter") as DropDownList;
            dml.dropdownsql(ddlLocation, "Set_Location", "LocName", "LocID");
            lstFruits.Visible = true;
            chkNormalGRN.Checked = true;
            chkDirectGRN.Checked = false;

        }


    }
    protected void txtPObalQtyFooter_TextChanged(object sender, EventArgs e)
    {


        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;

        float value;
        string Qty = (row.FindControl("txtPObalQtyFooter") as TextBox).Text;
        string rate = (row.FindControl("txtRateFooter") as TextBox).Text;
        if (rate.Length > 0 && Qty.Length > 0)
        {
            value = float.Parse(Qty) * float.Parse(rate);
        }
        else
        {
            if (Qty.Length > 0)
            {
                value = float.Parse(Qty) * 0;
            }
            else
            {
                value = 0 * float.Parse(rate);
            }
        }

        (row.FindControl("lblValueFooter") as TextBox).Text = value.ToString();
    }
    protected void txtDCQtyFooter_TextChanged(object sender, EventArgs e)
    {


        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;

        float value;
        string Qty = (row.FindControl("txtDCQtyFooter") as TextBox).Text;
        string rate = (row.FindControl("txtRateFooter") as TextBox).Text;
        if (rate.Length > 0 && Qty.Length > 0)
        {
            value = float.Parse(Qty) * float.Parse(rate);
        }
        else
        {
            if (Qty.Length > 0)
            {
                value = float.Parse(Qty) * 0;
            }
            else
            {
                value = 0 * float.Parse(rate);
            }
        }

       (row.FindControl("lblValueFooter") as TextBox).Text = value.ToString("0.00");

        float taxAddamount;
        string ddlgst = "";
        DropDownList ddlAddGSTFooter = (row.FindControl("ddlGST6") as DropDownList);
        TextBox lblTotalAmountFooter = (row.FindControl("lblTotalAmountFooter") as TextBox);

        TextBox lblBalanceAmtAmount = (row.FindControl("lblBalanceFooter") as TextBox);



        if (ddlAddGSTFooter.SelectedIndex != 0)
        {
            DataSet dsgst = dml.Find("SELECT SUBSTRING('" + ddlAddGSTFooter.SelectedItem.Text + "', CHARINDEX('(', '" + ddlAddGSTFooter.SelectedItem.Text + "') + 1, CHARINDEX(')', '" + ddlAddGSTFooter.SelectedItem.Text + "') - CHARINDEX('(', '" + ddlAddGSTFooter.SelectedItem.Text + "') - 1) AS output");
            if (dsgst.Tables[0].Rows.Count > 0)
            {
                ddlgst = dsgst.Tables[0].Rows[0]["output"].ToString();
            }
            string valuelbl = (row.FindControl("lblValueFooter") as TextBox).Text;


            if (valuelbl.Length > 0 && ddlAddGSTFooter.SelectedIndex > 0)
            {
                taxAddamount = float.Parse(valuelbl) * (float.Parse(ddlgst) / 100);
            }
            else
            {
                if (ddlAddGSTFooter.SelectedIndex > 0)
                {
                    taxAddamount = (float.Parse(ddlgst) / 100);
                }
                else
                {
                    taxAddamount = float.Parse(valuelbl);
                }
            }

            // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
            lbltaxamount.Text = taxAddamount.ToString();

            float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);


            (row.FindControl("lblGSTAmountFooter") as Label).Text = lbltaxamount.Text;
            //(row.FindControl("lblBalanceAmtAmount") as Label).Text= lbltaxamount.Text;

            (row.FindControl("lblBalanceTaxFooter") as TextBox).Text = toalamt.ToString();
            float balamount = toalamt + float.Parse(valuelbl);
            (row.FindControl("lblBalanceFooter") as TextBox).Text = balamount.ToString();
            txtBillBal.Text = balamount.ToString();

            dislaydigit_fortextbox(row, "lblBalanceTaxFooter", toalamt);
            dislaydigit_fortextbox(row, "lblBalanceFooter", balamount);
            dislaydigit_fortextbox(row, "lblValueFooter", float.Parse(valuelbl));


            float total_amount = toalamt + float.Parse(lblTotalAmountFooter.Text);

            lblTotalAmountFooter.Text = total_amount.ToString();
        }
        else
        {
            string valuelbl = (row.FindControl("lblValueFooter") as TextBox).Text;
            if (valuelbl.Length > 0 && ddlAddGSTFooter.SelectedIndex == 0)
            {
                taxAddamount = float.Parse(valuelbl) * 0;
            }
            else
            {
                if (ddlAddGSTFooter.SelectedIndex > 0)
                {
                    taxAddamount = (float.Parse(ddlgst) / 100);
                }
                else
                {
                    taxAddamount = float.Parse(valuelbl);
                }
            }

            // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
            lbltaxamount.Text = taxAddamount.ToString();

            float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);
            lblTotalAmountFooter.Text = toalamt.ToString();

            (row.FindControl("lblGSTAmountFooter") as Label).Text = lbltaxamount.Text;
            // (row.FindControl("lblBalanceAmtAmount") as Label).Text = lbltaxamount.Text;

            (row.FindControl("lblBalanceTaxFooter") as TextBox).Text = toalamt.ToString();
            float balamount = toalamt + float.Parse(valuelbl);
            (row.FindControl("lblBalanceFooter") as TextBox).Text = balamount.ToString();
            txtBillBal.Text = balamount.ToString();

            float total_amount = toalamt + float.Parse(lblTotalAmountFooter.Text);

            lblTotalAmountFooter.Text = total_amount.ToString();
            dislaydigit_fortextbox(row, "lblBalanceTaxFooter", toalamt);
            dislaydigit_fortextbox(row, "lblBalanceFooter", balamount);
            dislaydigit_fortextbox(row, "lblValueFooter", float.Parse(valuelbl));
        }

        //(row.FindControl("lblBalanceTaxFooter") as TextBox).Text = lbltaxamount.Text;

        string flblGSTAmount = (row.FindControl("lblGSTAmountFooter") as Label).Text;
        float faltotalamount = float.Parse(lblTotalAmountFooter.Text);
        float faflblGSTAmount = float.Parse(lbltaxamount.Text);





        string displ = inv.displaydigit(branchId().ToString());

        if (displ == "0")
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0");
        }

        else if (displ == "1")
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0.0");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0.0");
        }
        else if (displ == "2")
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0.00");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0.00");
        }
        else if (displ == "3")
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0.000");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0.000");
        }
        else if (displ == "4")
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0.0000");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0.0000");
        }
        else if (displ == "5")
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0.00000");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0.00000");
        }
        else if (displ == "6")
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0.000000");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0.000000");
        }
        else if (displ == "7")
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0.0000000");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0.0000000");
        }
        else if (displ == "8")
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0.00000000");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0.00000000");
        }
        else
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0");
        }

        //+++++++++++++++++++++++++++++++++++++
        if (ddlAddGSTFooter.SelectedIndex > 0)
        {
            float taxAddamount1;
            string ddlgst1 = "";
            DropDownList ddlAddGSTFooter1 = (row.FindControl("ddlAddGSTFooter") as DropDownList);
            TextBox lblTotalAmountFooter1 = (row.FindControl("lblTotalAmountFooter") as TextBox);
            Label lblGSTAmountFooter = (row.FindControl("lblGSTAmountFooter") as Label);
            //lblGSTAmountFooter
            if (ddlAddGSTFooter1.SelectedIndex != 0)
            {
                DataSet dsgst = dml.Find("SELECT SUBSTRING('" + ddlAddGSTFooter1.SelectedItem.Text + "', CHARINDEX('(', '" + ddlAddGSTFooter1.SelectedItem.Text + "') + 1, CHARINDEX(')', '" + ddlAddGSTFooter1.SelectedItem.Text + "') - CHARINDEX('(', '" + ddlAddGSTFooter1.SelectedItem.Text + "') - 1) AS output");
                if (dsgst.Tables[0].Rows.Count > 0)
                {
                    ddlgst1 = dsgst.Tables[0].Rows[0]["output"].ToString();
                }
                string valuelbl = (row.FindControl("lblValueFooter") as TextBox).Text;
                if (valuelbl.Length > 0 && ddlAddGSTFooter1.SelectedIndex > 0)
                {
                    taxAddamount1 = float.Parse(valuelbl) * (float.Parse(ddlgst1) / 100);
                }
                else
                {
                    if (ddlAddGSTFooter1.SelectedIndex > 0)
                    {
                        taxAddamount1 = (float.Parse(ddlgst1) / 100);
                    }
                    else
                    {
                        taxAddamount1 = float.Parse(valuelbl);
                    }
                }

                // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
                lblAddtaxamount.Text = taxAddamount1.ToString();

                float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);
                lblTotalAmountFooter1.Text = toalamt.ToString();
                (row.FindControl("lblBalanceFooter") as TextBox).Text = lbltaxamount.Text;
                lblGSTAmountFooter.Text = toalamt.ToString();
                dislaydigit_label(row, "lblGSTAmountFooter", toalamt);
                txtBillBal.Text = lbltaxamount.Text;
                (row.FindControl("lblBalanceTaxFooter") as TextBox).Text = toalamt.ToString();
                float balamount = toalamt + float.Parse(valuelbl);
                (row.FindControl("lblBalanceFooter") as TextBox).Text = balamount.ToString();
                txtBillBal.Text = balamount.ToString();
                lblTotalAmountFooter1.Text = balamount.ToString();

                dislaydigit_textbox(txtBillBal, balamount);
                dislaydigit_fortextbox(row, "lblTotalAmountFooter", balamount);
                dislaydigit_fortextbox(row, "lblBalanceTaxFooter", toalamt);
                dislaydigit_fortextbox(row, "lblBalanceFooter", balamount);


            }
            else
            {
                string valuelbl = (row.FindControl("lblValueFooter") as TextBox).Text;
                if (valuelbl.Length > 0 && ddlAddGSTFooter1.SelectedIndex == 0)
                {
                    taxAddamount = float.Parse(valuelbl) * 0;
                }
                else
                {
                    if (ddlAddGSTFooter1.SelectedIndex > 0)
                    {
                        taxAddamount = (float.Parse(ddlgst1) / 100);
                    }
                    else
                    {
                        taxAddamount = float.Parse(valuelbl);
                    }
                }

                // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
                lblAddtaxamount.Text = taxAddamount.ToString();

                float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);
                lblTotalAmountFooter1.Text = toalamt.ToString();


                (row.FindControl("lblBalanceFooter") as TextBox).Text = lbltaxamount.Text;
                txtBillBal.Text = lbltaxamount.Text;
                //  (row.FindControl("lblBalanceTaxFooter") as TextBox).Text = taxAddamount.ToString();
                float balamount = toalamt + float.Parse(valuelbl);
                (row.FindControl("lblBalanceFooter") as TextBox).Text = balamount.ToString();
                txtBillBal.Text = balamount.ToString();
                lblTotalAmountFooter1.Text = balamount.ToString();

                dislaydigit_fortextbox(row, "lblTotalAmountFooter", balamount);
                dislaydigit_fortextbox(row, "lblBalanceTaxFooter", toalamt);
                dislaydigit_fortextbox(row, "lblBalanceFooter", balamount);

            }
        }
        //+++++++++++++++++++++++++++++++++++++++








        //====================================================================




    }

    protected void lblRateEdit_TextChanged(object sender, EventArgs e)
    {

        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;

        float value;
        string Qty = (row.FindControl("txtDCQty") as TextBox).Text;
        string rate = (row.FindControl("lblRateEdit") as TextBox).Text;
        if (rate.Length > 0 && Qty.Length > 0)
        {
            value = float.Parse(Qty) * float.Parse(rate);
        }
        else
        {
            if (Qty.Length > 0)
            {
                value = float.Parse(Qty) * 0;
            }
            else
            {
                value = 0 * float.Parse(rate);
            }
        }

      (row.FindControl("txtValueEdit") as TextBox).Text = value.ToString();
    }
    protected void txtDCQty_TextChanged(object sender, EventArgs e)
    {

        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;

        float value;
        string Qty = (row.FindControl("txtDCQty") as TextBox).Text;
        string rate = (row.FindControl("lblRateEdit") as TextBox).Text;
        if (rate.Length > 0 && Qty.Length > 0)
        {
            value = float.Parse(Qty) * float.Parse(rate);
        }
        else
        {
            if (Qty.Length > 0)
            {
                value = float.Parse(Qty) * 0;
            }
            else
            {
                value = 0 * float.Parse(rate);
            }
        }
        ViewState["QtyDC"] = Qty;
        ViewState["RateEdit"] = rate;

        (row.FindControl("txtValueEdit") as TextBox).Text = value.ToString();
        ViewState["valueEdit"] = value;
    }


    protected void txtRateFooter_TextChanged(object sender, EventArgs e)
    {

        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;
        float value;
        string Qty = (row.FindControl("txtDCQtyFooter") as TextBox).Text;
        string rate = (row.FindControl("txtRateFooter") as TextBox).Text;
        if (rate.Length > 0 && Qty.Length > 0)
        {
            value = float.Parse(Qty) * float.Parse(rate);
        }
        else
        {
            if (Qty.Length > 0)
            {
                value = float.Parse(Qty) * 0;
            }
            else
            {
                value = 0 * float.Parse(rate);
            }
        }

     // (row.FindControl("lblValueFooter") as TextBox).Text = value.ToString("0.00");
    //   (row.FindControl("lblBalanceFooter") as TextBox).Text = value.ToString("0.00");
        dislaydigit_fortextbox(row, "lblValueFooter", value);
        dislaydigit_fortextbox(row, "lblBalanceFooter", value);
        dislaydigit_fortextbox(row, "lblTotalAmountFooter", value);

        //-----------------------------------------------------------------

        float taxAddamount;
        string ddlgst = "";
        DropDownList ddlAddGSTFooter = (row.FindControl("ddlGST6") as DropDownList);
        TextBox lblTotalAmountFooter = (row.FindControl("lblTotalAmountFooter") as TextBox);

        TextBox lblBalanceAmtAmount = (row.FindControl("lblBalanceFooter") as TextBox);



        if (ddlAddGSTFooter.SelectedIndex != 0)
        {
            DataSet dsgst = dml.Find("SELECT SUBSTRING('" + ddlAddGSTFooter.SelectedItem.Text + "', CHARINDEX('(', '" + ddlAddGSTFooter.SelectedItem.Text + "') + 1, CHARINDEX(')', '" + ddlAddGSTFooter.SelectedItem.Text + "') - CHARINDEX('(', '" + ddlAddGSTFooter.SelectedItem.Text + "') - 1) AS output");
            if (dsgst.Tables[0].Rows.Count > 0)
            {
                ddlgst = dsgst.Tables[0].Rows[0]["output"].ToString();
            }
            string valuelbl = (row.FindControl("lblValueFooter") as TextBox).Text;


            if (valuelbl.Length > 0 && ddlAddGSTFooter.SelectedIndex > 0)
            {
                taxAddamount = float.Parse(valuelbl) * (float.Parse(ddlgst) / 100);
            }
            else
            {
                if (ddlAddGSTFooter.SelectedIndex > 0)
                {
                    taxAddamount = (float.Parse(ddlgst) / 100);
                }
                else
                {
                    taxAddamount = float.Parse(valuelbl);
                }
            }

            // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
            lbltaxamount.Text = taxAddamount.ToString();

            float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);


            (row.FindControl("lblGSTAmountFooter") as Label).Text = lbltaxamount.Text;
            //(row.FindControl("lblBalanceAmtAmount") as Label).Text= lbltaxamount.Text;

            (row.FindControl("lblBalanceTaxFooter") as TextBox).Text = toalamt.ToString();
            float balamount = toalamt + float.Parse(valuelbl);
            (row.FindControl("lblBalanceFooter") as TextBox).Text = balamount.ToString();
            txtBillBal.Text = balamount.ToString();

            dislaydigit_fortextbox(row, "lblBalanceTaxFooter", toalamt);
            dislaydigit_fortextbox(row, "lblBalanceFooter", balamount);
            dislaydigit_fortextbox(row, "lblValueFooter", float.Parse(valuelbl));


            float total_amount = toalamt + float.Parse(lblTotalAmountFooter.Text);

            lblTotalAmountFooter.Text = total_amount.ToString();
        }
        else
        {
            string valuelbl = (row.FindControl("lblValueFooter") as TextBox).Text;
            if (valuelbl.Length > 0 && ddlAddGSTFooter.SelectedIndex == 0)
            {
                taxAddamount = float.Parse(valuelbl) * 0;
            }
            else
            {
                if (ddlAddGSTFooter.SelectedIndex > 0)
                {
                    taxAddamount = (float.Parse(ddlgst) / 100);
                }
                else
                {
                    taxAddamount = float.Parse(valuelbl);
                }
            }

            // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
            lbltaxamount.Text = taxAddamount.ToString();

            float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);
            lblTotalAmountFooter.Text = toalamt.ToString();

            (row.FindControl("lblGSTAmountFooter") as Label).Text = lbltaxamount.Text;
            // (row.FindControl("lblBalanceAmtAmount") as Label).Text = lbltaxamount.Text;

            (row.FindControl("lblBalanceTaxFooter") as TextBox).Text = toalamt.ToString();
            float balamount = toalamt + float.Parse(valuelbl);
            (row.FindControl("lblBalanceFooter") as TextBox).Text = balamount.ToString();
            txtBillBal.Text = balamount.ToString();

            float total_amount = toalamt + float.Parse(lblTotalAmountFooter.Text);

            lblTotalAmountFooter.Text = total_amount.ToString();
            dislaydigit_fortextbox(row, "lblBalanceTaxFooter", toalamt);
            dislaydigit_fortextbox(row, "lblBalanceFooter", balamount);
            dislaydigit_fortextbox(row, "lblValueFooter", float.Parse(valuelbl));
        }

        //(row.FindControl("lblBalanceTaxFooter") as TextBox).Text = lbltaxamount.Text;

        string flblGSTAmount = (row.FindControl("lblGSTAmountFooter") as Label).Text;
        float faltotalamount = float.Parse(lblTotalAmountFooter.Text);
        float faflblGSTAmount = float.Parse(lbltaxamount.Text);





        string displ = inv.displaydigit(branchId().ToString());

        if (displ == "0")
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0");
        }

        else if (displ == "1")
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0.0");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0.0");
        }
        else if (displ == "2")
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0.00");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0.00");
        }
        else if (displ == "3")
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0.000");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0.000");
        }
        else if (displ == "4")
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0.0000");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0.0000");
        }
        else if (displ == "5")
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0.00000");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0.00000");
        }
        else if (displ == "6")
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0.000000");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0.000000");
        }
        else if (displ == "7")
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0.0000000");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0.0000000");
        }
        else if (displ == "8")
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0.00000000");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0.00000000");
        }
        else
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0");
        }

        //+++++++++++++++++++++++++++++++++++++
        if (ddlAddGSTFooter.SelectedIndex > 0)
        {
            float taxAddamount1;
            string ddlgst1 = "";
            DropDownList ddlAddGSTFooter1 = (row.FindControl("ddlAddGSTFooter") as DropDownList);
            TextBox lblTotalAmountFooter1 = (row.FindControl("lblTotalAmountFooter") as TextBox);
            Label lblGSTAmountFooter = (row.FindControl("lblGSTAmountFooter") as Label);
            //lblGSTAmountFooter
            if (ddlAddGSTFooter1.SelectedIndex != 0)
            {
                DataSet dsgst = dml.Find("SELECT SUBSTRING('" + ddlAddGSTFooter1.SelectedItem.Text + "', CHARINDEX('(', '" + ddlAddGSTFooter1.SelectedItem.Text + "') + 1, CHARINDEX(')', '" + ddlAddGSTFooter1.SelectedItem.Text + "') - CHARINDEX('(', '" + ddlAddGSTFooter1.SelectedItem.Text + "') - 1) AS output");
                if (dsgst.Tables[0].Rows.Count > 0)
                {
                    ddlgst1 = dsgst.Tables[0].Rows[0]["output"].ToString();
                }
                string valuelbl = (row.FindControl("lblValueFooter") as TextBox).Text;
                if (valuelbl.Length > 0 && ddlAddGSTFooter1.SelectedIndex > 0)
                {
                    taxAddamount1 = float.Parse(valuelbl) * (float.Parse(ddlgst1) / 100);
                }
                else
                {
                    if (ddlAddGSTFooter1.SelectedIndex > 0)
                    {
                        taxAddamount1 = (float.Parse(ddlgst1) / 100);
                    }
                    else
                    {
                        taxAddamount1 = float.Parse(valuelbl);
                    }
                }

                // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
                lblAddtaxamount.Text = taxAddamount1.ToString();

                float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);
                lblTotalAmountFooter1.Text = toalamt.ToString();
                (row.FindControl("lblBalanceFooter") as TextBox).Text = lbltaxamount.Text;
                lblGSTAmountFooter.Text = toalamt.ToString();
                dislaydigit_label(row, "lblGSTAmountFooter", toalamt);
                txtBillBal.Text = lbltaxamount.Text;
                (row.FindControl("lblBalanceTaxFooter") as TextBox).Text = toalamt.ToString();
                float balamount = toalamt + float.Parse(valuelbl);
                (row.FindControl("lblBalanceFooter") as TextBox).Text = balamount.ToString();
                txtBillBal.Text = balamount.ToString();
                lblTotalAmountFooter1.Text = balamount.ToString();

                dislaydigit_textbox(txtBillBal, balamount);
                dislaydigit_fortextbox(row, "lblTotalAmountFooter", balamount);
                dislaydigit_fortextbox(row, "lblBalanceTaxFooter", toalamt);
                dislaydigit_fortextbox(row, "lblBalanceFooter", balamount);


            }
            else
            {
                string valuelbl = (row.FindControl("lblValueFooter") as TextBox).Text;
                if (valuelbl.Length > 0 && ddlAddGSTFooter1.SelectedIndex == 0)
                {
                    taxAddamount = float.Parse(valuelbl) * 0;
                }
                else
                {
                    if (ddlAddGSTFooter1.SelectedIndex > 0)
                    {
                        taxAddamount = (float.Parse(ddlgst1) / 100);
                    }
                    else
                    {
                        taxAddamount = float.Parse(valuelbl);
                    }
                }

                // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
                lblAddtaxamount.Text = taxAddamount.ToString();

                float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);
                lblTotalAmountFooter1.Text = toalamt.ToString();


                (row.FindControl("lblBalanceFooter") as TextBox).Text = lbltaxamount.Text;
                txtBillBal.Text = lbltaxamount.Text;
                //  (row.FindControl("lblBalanceTaxFooter") as TextBox).Text = taxAddamount.ToString();
                float balamount = toalamt + float.Parse(valuelbl);
                (row.FindControl("lblBalanceFooter") as TextBox).Text = balamount.ToString();
                txtBillBal.Text = balamount.ToString();
                lblTotalAmountFooter1.Text = balamount.ToString();

                dislaydigit_fortextbox(row, "lblTotalAmountFooter", balamount);
                dislaydigit_fortextbox(row, "lblBalanceTaxFooter", toalamt);
                dislaydigit_fortextbox(row, "lblBalanceFooter", balamount);

            }
        }
        //+++++++++++++++++++++++++++++++++++++++








        //====================================================================
    }
    protected void lblQty2Footer_TextChanged(object sender, EventArgs e)
    {

        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;
        float value;
        string Qty2 = (row.FindControl("lblQty2Footer") as TextBox).Text;
        string valuelbl = (row.FindControl("lblValueFooter") as TextBox).Text;
        if (valuelbl.Length > 0 && Qty2.Length > 0)
        {
            value = float.Parse(valuelbl) / float.Parse(Qty2);
        }
        else
        {
            if (Qty2.Length > 0)
            {
                value = float.Parse(Qty2);
            }
            else
            {
                value = float.Parse(valuelbl);
            }
        }

      // (row.FindControl("lblRate2Footer") as TextBox).Text = value.ToString();
        dislaydigit_fortextbox(row, "lblRate2Footer", value);
    }
    
    protected void txtQty2Edit_TextChanged(object sender, EventArgs e)
    {

        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;
        float value = 0;
        string Qty2 = (row.FindControl("txtQty2Edit") as TextBox).Text;
        string valuelbl = (row.FindControl("txtValueEdit") as TextBox).Text;
        if (valuelbl.Length > 0 && Qty2.Length > 0)
        {
            value = float.Parse(valuelbl) / float.Parse(Qty2);
        }


       (row.FindControl("txtRate2Edit") as TextBox).Text = value.ToString("0.0000");
    }

    protected void GridView6_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("AddNew"))
            {
                DataTable dt = (DataTable)ViewState["DirectDetail"];

                string AcctCodeDetail = (GridView6.FooterRow.FindControl("RadComboAcct_CodeFooter") as RadComboBox).Text;
                    string ItemSubHeadName = (GridView6.FooterRow.FindControl("ddlItemSubHeadFooter") as DropDownList).SelectedItem.Text;
                    string Description = (GridView6.FooterRow.FindControl("ddlItemMasterFooter") as DropDownList).SelectedItem.Text;
                    string UOM = (GridView6.FooterRow.FindControl("ddlUOMFooter") as DropDownList).SelectedItem.Text;
                    string Qty = (GridView6.FooterRow.FindControl("txtDCQtyFooter") as TextBox).Text;
                    string Rate = (GridView6.FooterRow.FindControl("txtRateFooter") as TextBox).Text;
                    string Value = (GridView6.FooterRow.FindControl("lblValueFooter") as TextBox).Text;
                    string gst = (GridView6.FooterRow.FindControl("ddlGST6") as DropDownList).SelectedItem.Text;
                    string addgst = (GridView6.FooterRow.FindControl("ddlAddGSTFooter") as DropDownList).SelectedItem.Text;
                    string totalAmount = (GridView6.FooterRow.FindControl("lblTotalAmountFooter") as TextBox).Text;
                    string balanceAmount = (GridView6.FooterRow.FindControl("lblBalanceFooter") as TextBox).Text;
                    string DC_Po_GRN = (GridView6.FooterRow.FindControl("lblDC_PO_GRNFooter") as TextBox).Text;
                    string lblGSTAmountFooter = (GridView6.FooterRow.FindControl("lblGSTAmountFooter") as Label).Text;
                    string lblBalanceTaxFooter = (GridView6.FooterRow.FindControl("lblBalanceTaxFooter") as TextBox).Text;
                    string CostCenter = (GridView6.FooterRow.FindControl("lblCostCenterFooter") as DropDownList).SelectedItem.Text;
                    string Location = (GridView6.FooterRow.FindControl("ddlLocationFooter") as DropDownList).SelectedItem.Text;
                    string Project = (GridView6.FooterRow.FindControl("lblProjectFooter") as TextBox).Text;
                    string WarranteeFooter = (GridView6.FooterRow.FindControl("lblWarranteeFooter") as TextBox).Text;
                    string Qty2 = (GridView6.FooterRow.FindControl("lblQty2Footer") as TextBox).Text;
                    string Rate2 = (GridView6.FooterRow.FindControl("lblRate2Footer") as TextBox).Text;

                foreach (GridViewRow g in GridView6.Rows)
                {
                    Label ll = (Label)g.FindControl("lblItemSubHead");
                    Label l = (Label)g.FindControl("lblItemMaster");

                    if (ll.Text == ItemSubHeadName && l.Text == Description)
                    {
                        Label1.Text = "Already inserted";
                    }
                    else {
                        Label1.Text = "";
                        

                        if (ViewState["flag"].ToString() == "true")
                        {
                            dt.Rows[0].Delete();
                            ViewState["flag"] = "false";
                        }
                    }
                }


                        // Accode,PONo,ItemSubHead,ItemMaster,UOM,PObalQty,DCQty,/Rate,Value,CostCenter,Location,Project,Warrantee,UOM2,Qty2,Rate2

                        //(ItemSubHeadName, Description, UOM, Qty, ApprovedQty, Rate, GST, GSTRate, GrossValue, CostCenter,Location,Project,UOM2,Qty2,Rate2)

                        dt.Rows.Add(AcctCodeDetail, ItemSubHeadName, Description, UOM, Qty, Rate, Value, gst, addgst,lblGSTAmountFooter, totalAmount, balanceAmount, lblBalanceTaxFooter, DC_Po_GRN, CostCenter, Location, Project, WarranteeFooter, Qty2, Rate2);

                        ViewState["DirectDetail"] = dt;
                        this.PopulateGridview();
                        DropDownList ddlsub = GridView6.FooterRow.FindControl("ddlItemSubHeadFooter") as DropDownList;
                        dml.dropdownsql(ddlsub, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");


                        DropDownList ddlmaster = GridView6.FooterRow.FindControl("ddlItemMasterFooter") as DropDownList;
                        dml.dropdownsql(ddlmaster, "SET_ItemMaster", "Description", "ItemID");

                        DropDownList ddluom = GridView6.FooterRow.FindControl("ddlUOMFooter") as DropDownList;
                        dml.dropdownsql(ddluom, "SET_UnitofMeasure", "UOMName", "UOMID");

                        DropDownList ddlsupFooter = GridView6.FooterRow.FindControl("lblCostCenterFooter") as DropDownList;
                        dml.dropdownsql(ddlsupFooter, "SET_CostCenter", "CostCenterName", "CostCenterID");

                        DropDownList ddluom2 = GridView6.FooterRow.FindControl("ddlUOM2Footer") as DropDownList;
                        dml.dropdownsql(ddluom2, "SET_UnitofMeasure", "UOMName", "UOMID");

                        DropDownList ddlLocation = GridView6.FooterRow.FindControl("ddlLocationFooter") as DropDownList;
                        dml.dropdownsql(ddlLocation, "Set_Location", "LocName", "LocID");

                        DropDownList ddlAddGSTFooter = GridView6.FooterRow.FindControl("ddlAddGSTFooter") as DropDownList;
                        dml.dropdownsqlwithquery(ddlAddGSTFooter, "SELECT Percentage, TaxDescription +'(' + CONVERT(VARCHAR, Percentage) + ')' as perc from SET_Tax", "perc", "Percentage");

                        DropDownList ddlGSTFooter = GridView6.FooterRow.FindControl("ddlGST6") as DropDownList;
                        dml.dropdownsqlwithquery(ddlGSTFooter, "SELECT Percentage, TaxDescription +'(' + CONVERT(VARCHAR, Percentage) + ')' as perc from SET_Tax", "perc", "Percentage");


                    
                
            }
        }
        catch (Exception ex)
        {
            // lblSuccessMessage.Text = "";
            //lblErrorMessage.Text = ex.Message;
        }
    }
    protected void GridView6_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    (e.Row.FindControl("lblRowNumber") as Label).Text = (e.Row.RowIndex + 1).ToString();

        //    string f = (e.Row.FindControl("lblValue") as Label).Text;
        //    float fa = 0;
        //    if (f != "")
        //    {
        //        fa = float.Parse(f);
        //    }

           
        //    Label value = e.Row.FindControl("lblValue") as Label;

        //    string ddlgst = "0";
        //    Label gsttool =  e.Row.FindControl("lblGST") as Label;
            
        //    if (gsttool.Text != "")
        //    {
        //        DataSet dsgst = dml.Find("SELECT SUBSTRING('" + gsttool.Text + "', CHARINDEX('(', '" + gsttool.Text + "') + 1, CHARINDEX(')', '" + gsttool.Text + "') - CHARINDEX('(', '" + gsttool.Text + "') - 1) AS output");
        //        if (dsgst.Tables[0].Rows.Count > 0)
        //        {
        //            ddlgst = dsgst.Tables[0].Rows[0]["output"].ToString();
        //        }


        //        float gstval = float.Parse(value.Text) * (float.Parse(ddlgst) / 100);
        //        gsttool.ToolTip = gstval.ToString();
        //    }


        //    string ddladdgst = "0";
        //    Label gstaddtool = e.Row.FindControl("lblAddGST") as Label;
        //    if (gstaddtool.Text != "")
        //    {
        //        DataSet dsaddgst = dml.Find("SELECT SUBSTRING('" + gstaddtool.Text + "', CHARINDEX('(', '" + gstaddtool.Text + "') + 1, CHARINDEX(')', '" + gstaddtool.Text + "') - CHARINDEX('(', '" + gstaddtool.Text + "') - 1) AS output");
        //        if (dsaddgst.Tables[0].Rows.Count > 0)
        //        {
        //            ddladdgst = dsaddgst.Tables[0].Rows[0]["output"].ToString();
        //        }

        //        float gstaddval = float.Parse(value.Text) * (float.Parse(ddladdgst) / 100);
        //        gstaddtool.ToolTip = gstaddval.ToString();
        //    }

        //    string displ = inv.displaydigit(branchId().ToString());
        //    if (displ == "0")
        //    {
        //        (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0");
        //    }

        //    else if (displ == "1")
        //    {
        //        (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.0");
        //    }
        //    else if (displ == "2")
        //    {
        //        (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.00");
        //    }
        //    else if (displ == "3")
        //    {
        //        (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.000");
        //    }
        //    else if (displ == "4")
        //    {
        //        (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.0000");
        //    }
        //    else if (displ == "5")
        //    {
        //        (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.00000");
        //    }
        //    else if (displ == "6")
        //    {
        //        (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.000000");
        //    }
        //    else if (displ == "7")
        //    {
        //        (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.0000000");
        //    }
        //    else if (displ == "8")
        //    {
        //        (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.00000000");
        //    }
        //    else
        //    {
        //        (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0");
        //    }



        //}
    }
    protected void GridView6_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = GridView6.Rows[e.RowIndex];
        DataTable dt = (DataTable)ViewState["DirectDetail"];
        dt.Rows[e.RowIndex].Delete();
        this.PopulateGridview();
        DropDownList ddlsub = GridView6.FooterRow.FindControl("ddlItemSubHeadFooter") as DropDownList;
        dml.dropdownsql(ddlsub, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");

        DropDownList ddlmaster = GridView6.FooterRow.FindControl("ddlItemMasterFooter") as DropDownList;
        dml.dropdownsql(ddlmaster, "SET_ItemMaster", "Description", "ItemID");

        DropDownList ddluom = GridView6.FooterRow.FindControl("ddlUOMFooter") as DropDownList;
        dml.dropdownsql(ddluom, "SET_UnitofMeasure", "UOMName", "UOMID");

        DropDownList ddlsupFooter = GridView6.FooterRow.FindControl("lblCostCenterFooter") as DropDownList;
        dml.dropdownsql(ddlsupFooter, "SET_CostCenter", "CostCenterName", "CostCenterID");

        DropDownList ddluom2 = GridView6.FooterRow.FindControl("ddlUOM2Footer") as DropDownList;
        dml.dropdownsql(ddluom2, "SET_UnitofMeasure", "UOMName", "UOMID");

        DropDownList ddlLocation = GridView6.FooterRow.FindControl("ddlLocationFooter") as DropDownList;
        dml.dropdownsql(ddlLocation, "SET_Department", "DepartmentName", "DepartmentID", "IsWarehouse", "1");

        DropDownList ddlAddGSTFooter = GridView6.FooterRow.FindControl("ddlAddGSTFooter") as DropDownList;
        dml.dropdownsqlwithquery(ddlAddGSTFooter, "SELECT Percentage, TaxDescription +'(' + CONVERT(VARCHAR, Percentage) + ')' as perc from SET_Tax", "perc", "Percentage");

        DropDownList ddlGSTFooter = GridView6.FooterRow.FindControl("ddlGST6") as DropDownList;
        dml.dropdownsqlwithquery(ddlGSTFooter, "SELECT Percentage, TaxDescription +'(' + CONVERT(VARCHAR, Percentage) + ')' as perc from SET_Tax", "perc", "Percentage");


    }
    protected void lstFruits_SelectedIndexChanged(object sender, EventArgs e)
    {
        Div1.Visible = false;
        Div2.Visible = false;
        Div3.Visible = false;
        Div4.Visible = false;
        Div5.Visible = false;
        int count = 0;
        string reqno = "";
        foreach (ListItem item in lstFruits.Items)
        {

            if (item.Selected)
            {
                if (count == 0)
                {
                    reqno += reqno + "'" + item.Text.Substring(0, 11) + "'";
                    count = count + 1;
                }
                else
                {
                    reqno += "," + "'" + item.Text.Substring(0, 11) + "'";
                }
            }


        }

        string q;
        //if (reqno.Length > 0)
        //{
        //    reqno = reqno.Substring(1, 11);
        //}

        if (count > 0)
        {
            q = "select * from View_InvGrn_Normal where docno in (" + reqno + ") and Status != '2';";
        }
        else
        {
            q = "select * from View_InvGrn_Normal where docno in ('0') and Status != '2'";//bal qtyt add 
        }




        DataSet ds = dml.grid(q);


        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlSupplier.ClearSelection();
            ddlSupplier.Items.FindByValue(ds.Tables[0].Rows[0]["BPartnerTypeID"].ToString()).Selected = true;


            if (chkNormalGRN.Checked == true)
            {
                Div4.Visible = true;
                GridView7.DataSource = ds;
                GridView7.DataBind();
            }
            if (chkDirectGRN.Checked == true)
            {
                Div1.Visible = false;
                GridView5.DataSource = ds;
                GridView5.DataBind();
            }

            foreach (GridViewRow row in GridView5.Rows)
            {
                //DropDownList ddlcostcentr = row.FindControl("ddlCostCenter") as DropDownList;
                //dml.dropdownsql(ddlcostcentr, "SET_CostCenter", "CostCenterName", "CostCenterID");

                ////select LocId,LocName from Set_Location
                //DropDownList ddlLocation = row.FindControl("ddlLocation") as DropDownList;
                //dml.dropdownsql(ddlLocation, "Set_Location", "LocName", "LocId");

                DropDownList ddluom2 = row.FindControl("ddlUOM2") as DropDownList;
                dml.dropdownsql(ddluom2, "SET_UnitofMeasure", "UOMName", "UOMID");


            }
            foreach (GridViewRow row in GridView7.Rows)
            {

                DropDownList ddluom2 = row.FindControl("ddlUOM2") as DropDownList;
                dml.dropdownsql(ddluom2, "SET_UnitofMeasure", "UOMName", "UOMID");

                Label ddlsubitem = row.FindControl("lblsubhead") as Label;
                TextBox ddlaccout = row.FindControl("txtAccCode") as TextBox;
                //  dml.dropdownsql(ddluom2, "SET_UnitofMeasure", "UOMName", "UOMID"); txtAccCode

                Label ddlmaster = row.FindControl("lblitemmaster") as Label;
                // dml.dropdownsql(ddluom2, "SET_UnitofMeasure", "UOMName", "UOMID");








                DataSet ds11 = dml.Find("select ItemSubHeadID, ItemSubHeadName from SET_ItemSubHead where ItemSubHeadName = '" + ddlsubitem.Text + "'");
                string subhead, itemmaster, uom1, uom2, costcenter, location, itemcode;
                if (ds11.Tables[0].Rows.Count > 0)
                {
                    subhead = ds11.Tables[0].Rows[0]["ItemSubHeadID"].ToString();
                }
                else
                {
                    subhead = "0";
                }
                DataSet ds1 = dml.Find("select  ItemID , Description,ItemCode,ItemTypeID from SET_ItemMaster where Description = '" + ddlmaster.Text + "'");
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    itemmaster = ds1.Tables[0].Rows[0]["ItemID"].ToString();
                    itemcode = ds1.Tables[0].Rows[0]["ItemCode"].ToString();
                    //itemtypeid = ds1.Tables[0].Rows[0]["ItemTypeID"].ToString();
                }
                else
                {
                    itemmaster = "0";
                    itemcode = "0";
                    // itemtypeid = "0";
                }









                string asset = "0", consum = "0", expense = "0", itemactasset = "0", itemactconsum = "0", itemactexpense = "0", itemtypeid = "0";

                DataSet ds_as_C_E = dml.Find("select IsAsset,IsConsumable,IsExpense,ItemTypeID from SET_ItemMaster where ItemSubHeadID = '" + subhead + "' AND Record_Deleted = 0 and ItemID = '" + itemmaster + "';");
                if (ds_as_C_E.Tables[0].Rows.Count > 0)
                {
                    asset = ds_as_C_E.Tables[0].Rows[0]["IsAsset"].ToString();
                    expense = ds_as_C_E.Tables[0].Rows[0]["IsExpense"].ToString();
                    consum = ds_as_C_E.Tables[0].Rows[0]["IsConsumable"].ToString();
                    itemtypeid = ds_as_C_E.Tables[0].Rows[0]["ItemTypeID"].ToString();


                    DataSet dsitemtype = dml.Find("select ExpenseAcct,FixedAssetAcct,InventoryAcct from SET_ItemType where ItemTypeID = '" + itemtypeid + "'");

                    if (dsitemtype.Tables[0].Rows.Count > 0)
                    {
                        itemactasset = dsitemtype.Tables[0].Rows[0]["FixedAssetAcct"].ToString();
                        itemactconsum = dsitemtype.Tables[0].Rows[0]["InventoryAcct"].ToString();
                        itemactexpense = dsitemtype.Tables[0].Rows[0]["ExpenseAcct"].ToString();
                    }


                    if (asset == "True")
                    {
                        ddlaccout.Text = itemactasset;
                    }
                    if (expense == "True")
                    {
                        ddlaccout.Text = itemactexpense;
                    }
                    if (consum == "True")
                    {
                        ddlaccout.Text = itemactconsum;

                    }

                }





            }



        }
        else
        {
            GridView5.ShowHeaderWhenEmpty = true;
            GridView5.EmptyDataText = "NO RECORD";
            GridView5.DataBind();
        }
    }
    protected void RadComboAcct_CodeFooter_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        RadComboBox RadComboAcct_CodeFooter = GridView6.FooterRow.FindControl("RadComboAcct_CodeFooter") as RadComboBox;

        cmb.serachcombo4(RadComboAcct_CodeFooter, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }

    protected void RadComboAcctCodeFooter_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        RadComboBox RadComboAcctCodeFooter = grd_Add.FooterRow.FindControl("RadComboAcctCodeFooter") as RadComboBox;

        cmb.serachcombo4(RadComboAcctCodeFooter, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }

    protected void RadComboAcctDecCodeFooter_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        RadComboBox RadComboAcctDecCodeFooter = grd_Deduction.FooterRow.FindControl("RadComboAcctDecCodeFooter") as RadComboBox;

        cmb.serachcombo4(RadComboAcctDecCodeFooter, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
    public void detailinsertNOrmal(string masterid)
    {
        float totalcrdb = 0;
        string bpnatureid = "0";
        string doctype = "0";
        DataSet dsdoctype = dml.Find("select DocTypeId from SET_Documents where DocID = '" + ddlDocName.SelectedItem.Value + "';");
        if (dsdoctype.Tables[0].Rows.Count > 0)
        {

            doctype = dsdoctype.Tables[0].Rows[0]["DocTypeId"].ToString();

        }

        foreach (GridViewRow g in GridView7.Rows)
        {
            Label lblPoNoN = (Label)g.FindControl("lblPoNoN");
            TextBox Acct = (TextBox)g.FindControl("txtAccCode");
            Label lblsubhead = (Label)g.FindControl("lblsubhead");
            Label lblitemmaster = (Label)g.FindControl("lblitemmaster");
            Label lbluom = (Label)g.FindControl("lblUOM");
            Label lblQty = (Label)g.FindControl("lblQty");
            TextBox lblDCQtyN = (TextBox)g.FindControl("lblDCQtyN");
            Label lblCostCenter = (Label)g.FindControl("lblCostCenter");
            Label lblLocation = (Label)g.FindControl("lblLocation");
            Label lblproject = (Label)g.FindControl("lblproject");
            TextBox txtWarranttee = (TextBox)g.FindControl("txtWarranteeEdit");
            DropDownList ddlUOM2 = (DropDownList)g.FindControl("ddlUOM2");
            TextBox lblQty2 = (TextBox)g.FindControl("lblQty2");
            Label lblPOSNO = (Label)g.FindControl("lblPOSNO");
            Label lblPOMaster = (Label)g.FindControl("lblPOMaster");


            float balqDC = float.Parse(lblQty.Text) - float.Parse(lblDCQtyN.Text);

            DataSet ds = dml.Find("select ItemSubHeadID, ItemSubHeadName from SET_ItemSubHead where ItemSubHeadName = '" + lblsubhead.Text + "'");
            string subhead, itemmaster, uom1, uom2, costcenter, location, itemcode, itemtypeid;
            if (ds.Tables[0].Rows.Count > 0)
            {
                subhead = ds.Tables[0].Rows[0]["ItemSubHeadID"].ToString();
            }
            else
            {
                subhead = "0";
            }
            DataSet ds1 = dml.Find("select  ItemID , Description,ItemCode,ItemTypeID from SET_ItemMaster where Description = '" + lblitemmaster.Text + "'");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                itemmaster = ds1.Tables[0].Rows[0]["ItemID"].ToString();
                itemcode = ds1.Tables[0].Rows[0]["ItemCode"].ToString();
                itemtypeid = ds1.Tables[0].Rows[0]["ItemTypeID"].ToString();
            }
            else
            {
                itemmaster = "0";
                itemcode = "0";
                itemtypeid = "0";
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

            DataSet ds3 = dml.Find("select  CostCenterID,CostCenterName from SET_CostCenter where CostCenterName = '" + lblCostCenter.Text + "'");
            if (ds3.Tables[0].Rows.Count > 0)
            {
                costcenter = ds3.Tables[0].Rows[0]["CostCenterID"].ToString();
            }
            else
            {
                costcenter = "0";
            }
            if (ddlUOM2.SelectedIndex > 0)
            {
                DataSet ds4 = dml.Find("select  UOMID,UOMName from SET_UnitofMeasure where UOMName = '" + ddlUOM2.SelectedItem.Text + "'");
                if (ds4.Tables[0].Rows.Count > 0)
                {
                    uom2 = ds4.Tables[0].Rows[0]["UOMID"].ToString();
                }
                else
                {
                    uom2 = "0";
                }
            }
            else
            {
                uom2 = "0";
            }

            //


            string rate = "0", value = "0", gst = "0", gstrate = "0", qty = "0";
            DataSet dsratevalue = dml.Find("select Quantity, Rate, GrossValue, GST, GSTRate from Set_PurchaseOrderDetail where Sno_Master in (SELECT Sno from Set_PurcahseOrderMaster where[DocumentNo.] = '" + lblPoNoN.Text + "')");
            if (dsratevalue.Tables[0].Rows.Count > 0)
            {
                rate = dsratevalue.Tables[0].Rows[0]["Rate"].ToString();
                value = dsratevalue.Tables[0].Rows[0]["GrossValue"].ToString();
                gst = dsratevalue.Tables[0].Rows[0]["GST"].ToString();
                gstrate = dsratevalue.Tables[0].Rows[0]["GSTRate"].ToString();
                qty = dsratevalue.Tables[0].Rows[0]["Quantity"].ToString();

            }

            float valuetot = float.Parse(rate) * float.Parse(lblDCQtyN.Text);


            string PONo = "";
            DataSet ds5 = dml.Find("select LocId,LocName from Set_Location where LocName = '" + lblLocation.Text + "'; select Sno from Set_PurcahseOrderMaster where [DocumentNo.] = '" + lblPoNoN.Text + "' and Record_Deleted = 0");
            if (ds5.Tables[0].Rows.Count > 0)
            {
                location = ds5.Tables[0].Rows[0]["LocId"].ToString();
                PONo = ds5.Tables[1].Rows[0]["Sno"].ToString();
            }
            else
            {
                location = "0";
                PONo = "0";
            }

            float balqty = 0, balqty2 = 0;


            //if (lblPObalQty.Text.Length > 0)
            //{
            //    balqty = float.Parse(lblPObalQty.Text) - 0;// float.Parse("issueQty");
            //}
            //else
            //{
            //    balqty = 0;

            //}


            //if (lblPObalQty.Text.Length > 0)
            //{
            //    balqty2 = float.Parse(lblQty2.Text) - 0;// float.Parse("issueQty");
            //}
            //else
            //{
            //    balqty2 = 0;

            //}
            string dbcr_detail = "", dbcr_plusminus = "";

            string acctcode = Acct.Text, trantype = "";

            DataSet dsCRDR = dml.Find("select  Tran_Type from SET_Acct_Type where Acct_Type_Id in ( SELECT Acct_Type_ID from SET_COA_detail where Acct_Code = '" + acctcode + "')");
            if (dsCRDR.Tables[0].Rows.Count > 0)
            {
                trantype = dsCRDR.Tables[0].Rows[0]["Tran_Type"].ToString();


            }
            DataSet dsDeB;
            if (trantype == "Debit")
            {
                DataSet dsinsertdetail = dml.Find("INSERT INTO Inv_InventoryInDetail ( [Sno_Master], [PONo], [DrAccountCode], [ItemSubHead],"
                                     + " [ItemMaster], [UOM], [Quantity], [Remarks], [Qty2], [UOM2], [BalQty],"
                                     + " [BalQty2], [ReferNo], [GLReferNo], [CostCenter], [Location], [CompId], [BranchId], [FiscalYearID],"
                                     + " [CreatedBy], [CreatedDate], [Record_Deleted], [Warrentee],[Project]) "

                                     + "VALUES ('" + masterid + "', '" + lblPoNoN.Text + "','" + acctcode + "', '" + subhead + "', '" + itemmaster + "', '" + uom1 + "', '" + lblDCQtyN.Text + "',"
                                     + " NULL, '" + lblQty2.Text + "', '" + uom2 + "', '" + lblQty.Text + "', '" + balqty2 + "', NULL, NULL,"
                                     + " '" + costcenter + "', '" + location + "', " + compid() + ", " + branchId() + ", " + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0' , '" + txtWarranttee.Text + "',"
                                     + " '" + lblproject.Text + "'); SELECT * FROM Inv_InventoryInDetail WHERE Sno = SCOPE_IDENTITY()");




                //balqDC
                dml.Update("Update View_InvGrn_Normal set balQty = '" + balqDC + "' where Sno = '" + lblPOSNO.Text + "'", "");



                DataSet dstotalbal = dml.Find("select sum(balQty) as totalbal from Set_PurchaseOrderDetail where Sno = '" + lblPOSNO.Text + "'");
                if (dstotalbal.Tables[0].Rows.Count > 0)
                {
                    float totalbalQty = float.Parse(dstotalbal.Tables[0].Rows[0]["totalbal"].ToString());
                    if (totalbalQty == 0)
                    {
                        dml.Update("UPDATE Set_PurcahseOrderMaster set Status = '2' where Sno = '" + lblPOMaster.Text + "'", "");

                    }
                    else if (totalbalQty > 0)
                    {
                        dml.Update("UPDATE Set_PurcahseOrderMaster set Status = '8' where Sno = '" + lblPOMaster.Text + "'", "");
                    }

                }


                //GLentry Detail Normal Start
                string detailid = "0";
                if (dsinsertdetail.Tables[0].Rows.Count > 0)
                {
                    detailid = dsinsertdetail.Tables[0].Rows[0]["Sno"].ToString();

                }


                DataSet dsnature = dml.Find("select  BPNatureID,BPNatureDescription from SET_BPartnerNature where BPNatureID in (select BPNatureID from SET_BPartnerType where BPartnerID = '" + ddlSupplier.SelectedItem.Value + "')");
                if (dsnature.Tables[0].Rows.Count > 0)
                {
                    bpnatureid = dsnature.Tables[0].Rows[0]["BPNatureID"].ToString();
                }


                DataSet dsdebcr_detail = dml.Find("SELECT	GLImpact, InventoryImpact FROM SET_AcRules4Doc WHERE DocId = '" + ddlDocName.SelectedItem.Value + "' AND MasterDetail = 'D' AND CompID = '" + compid() + "' AND Record_Deleted = 0 AND IsActive = 1 AND IsHide = 0 ;");
                if (dsdebcr_detail.Tables[0].Rows.Count > 0)
                {
                    dbcr_detail = dsdebcr_detail.Tables[0].Rows[0]["GLImpact"].ToString();
                    dbcr_plusminus = dsdebcr_detail.Tables[0].Rows[0]["InventoryImpact"].ToString();
                }


                string CODEitemtype = gl.itemtypecode(dbcr_detail, itemtypeid);



                //select ItemCode from SET_ItemMaster where ItemID = 1
                gl.GlentryInsert(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, masterid, detailid, dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, Acct.Text, bpnatureid, ddlSupplier.SelectedItem.Value, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), valuetot.ToString(), "0", txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyy hh:mm:ss.ffff"), itemcode, qty, "0", "Inv_InventoryInDetail");


                //Glentry Detail Normal End




            }
            else
            {
                DataSet dsinsertdetail = dml.Find("INSERT INTO Inv_InventoryInDetail ( [Sno_Master], [PONo], [CrAccountCode], [ItemSubHead],"
                                        + " [ItemMaster], [UOM], [Quantity], [Remarks], [Qty2], [UOM2], [BalQty],"
                                        + " [BalQty2], [ReferNo], [GLReferNo], [CostCenter], [Location], [CompId], [BranchId], [FiscalYearID],"
                                        + " [CreatedBy], [CreatedDate], [Record_Deleted],[Warrentee], [Project]) "

                                        + "VALUES ('" + masterid + "', '" + lblPoNoN.Text + "','" + acctcode + "', '" + subhead + "', '" + itemmaster + "', '" + uom1 + "', '" + lblDCQtyN.Text + "',"
                                        + " NULL, '" + lblQty2.Text + "', '" + uom2 + "', '" + lblQty.Text + "', '" + balqty2 + "', NULL, NULL,"
                                        + " '" + costcenter + "', '" + location + "', " + compid() + ", " + branchId() + ", " + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0' ,'" + txtWarranttee.Text + "',"
                                        + " '" + lblproject.Text + "');SELECT * FROM Inv_InventoryInDetail WHERE Sno = SCOPE_IDENTITY()");

                dml.Update("Update View_InvGrn_Normal set balQty = '" + balqDC + "' where Sno = '" + lblPOSNO.Text + "'", "");


                DataSet dstotalbal = dml.Find("select sum(balQty) as totalbal from Set_PurchaseOrderDetail where Sno = '" + lblPOSNO.Text + "'");
                if (dstotalbal.Tables[0].Rows.Count > 0)
                {
                    float totalbalQty = float.Parse(dstotalbal.Tables[0].Rows[0]["totalbal"].ToString());
                    if (totalbalQty == 0)
                    {
                        dml.Update("UPDATE Set_PurcahseOrderMaster set Status = '2' where Sno_Master = '" + lblPOMaster.Text + "'", "");

                    }
                    else if (totalbalQty > 0)
                    {
                        dml.Update("UPDATE Set_PurcahseOrderMaster set Status = '8' where Sno_Master = '" + lblPOMaster.Text + "'", "");
                    }

                }



                //GLentry Detail Normal Start
                string detailid = "0";
                if (dsinsertdetail.Tables[0].Rows.Count > 0)
                {
                    detailid = dsinsertdetail.Tables[0].Rows[0]["Sno"].ToString();

                }




                DataSet dsdebcr_detail = dml.Find("SELECT	GLImpact, InventoryImpact FROM SET_AcRules4Doc WHERE DocId = '" + ddlDocName.SelectedItem.Value + "' AND MasterDetail = 'D' AND CompID = '" + compid() + "' AND Record_Deleted = 0 AND IsActive = 1 AND IsHide = 0 ;");
                if (dsdebcr_detail.Tables[0].Rows.Count > 0)
                {
                    dbcr_detail = dsdebcr_detail.Tables[0].Rows[0]["GLImpact"].ToString();
                    dbcr_plusminus = dsdebcr_detail.Tables[0].Rows[0]["InventoryImpact"].ToString();
                }


                string CODEitemtype = gl.itemtypecode(dbcr_detail, itemtypeid);



                //  string bpnatureid = "0";
                DataSet dsnature = dml.Find("select  BPNatureID,BPNatureDescription from SET_BPartnerNature where BPNatureID in (select BPNatureID from SET_BPartnerType where BPartnerID = '" + ddlSupplier.SelectedItem.Value + "')");
                if (dsnature.Tables[0].Rows.Count > 0)
                {
                    bpnatureid = dsnature.Tables[0].Rows[0]["BPNatureID"].ToString();
                }

                //select ItemCode from SET_ItemMaster where ItemID = 1
                gl.GlentryInsert(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, masterid, detailid, dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, Acct.Text, bpnatureid, ddlSupplier.SelectedItem.Value, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), "0", valuetot.ToString(), txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyy hh:mm:ss.ffff"), itemcode, qty, "0", "Inv_InventoryInDetail");


                //Glentry Detail Normal End




            }

            if (dbcr_plusminus == "PLUS")
            {
                totalcrdb = totalcrdb + float.Parse(valuetot.ToString());
            }
            else if (dbcr_plusminus == "MINUS")
            {
                totalcrdb = totalcrdb - float.Parse(valuetot.ToString());
            }
            else
            {
                totalcrdb = totalcrdb + float.Parse(valuetot.ToString());
            }


        }

        //gl master entry

        string dbcr = "";
        DataSet dsdebcr = dml.Find("SELECT	GLImpact, InventoryImpact FROM SET_AcRules4Doc WHERE DocId = '" + ddlDocName.SelectedItem.Value + "' AND MasterDetail = 'M' AND CompID = 1 AND Record_Deleted = 0 AND IsActive = 1 AND IsHide = 0 ;");
        if (dsdebcr.Tables[0].Rows.Count > 0)
        {
            dbcr = dsdebcr.Tables[0].Rows[0]["GLImpact"].ToString();
        }
        if (dbcr == "Debit Impact")
        {
            gl.GlentryInsert(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, masterid, "0", dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, RadComboAcct_Code.Text, bpnatureid, ddlSupplier.SelectedItem.Value, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), totalcrdb.ToString(), "0", txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff"), "0", "0", "0", "Inv_InventoryInDetail");

        }
        else
        {
            gl.GlentryInsert(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, masterid, "0", dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, RadComboAcct_Code.Text, bpnatureid, ddlSupplier.SelectedItem.Value, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), "0", totalcrdb.ToString(), txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff"), "0", "0", "0", "Inv_InventoryInDetail");

        }
    }
    public void docrule(string docid)
    {
        if (ddlDocName.SelectedIndex != 0)
        {

            string acctcodebpnature = "";
            DataSet ds = dml.Find("SELECT AccountCode FROM SET_Documents WHERE DocID = '" + docid + "'; SELECT ForceAccountCode,AccountType FROM SET_AcRules4Doc WHERE DocId = '" + docid + "'");

            string acct_set_doc = "", acctype_set_act4rule = "", forceacct_set_act4rule = "", supplieracct = "";

            if (ds.Tables[0].Rows.Count > 0)
            {
                acct_set_doc = ds.Tables[0].Rows[0]["AccountCode"].ToString();

            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                acctype_set_act4rule = ds.Tables[1].Rows[0]["AccountType"].ToString();
                forceacct_set_act4rule = ds.Tables[1].Rows[0]["ForceAccountCode"].ToString();

                DataSet dsbpnaturacct = dml.Find("SELECT Acct_Code from SET_BPartnerNature where BPNatureDescription= '" + acctype_set_act4rule + "' and Record_Deleted = 0 and IsActive = 1");
                if (dsbpnaturacct.Tables[0].Rows.Count > 0)
                {
                    acctcodebpnature = dsbpnaturacct.Tables[0].Rows[0]["Acct_Code"].ToString();

                }




            }



            if (ddlSupplier.SelectedIndex > 0)
            {
                DataSet ds_supp_ascct = dml.Find("select  Acct_Code from SET_BPartnerNature where BPNatureID in (select BPNatureID from ViewSupplierId where BPartnerID = '" + ddlSupplier.SelectedItem.Value + "')");
                if (ds_supp_ascct.Tables[0].Rows.Count > 0)
                {
                    supplieracct = ds.Tables[0].Rows[0]["AccountType"].ToString();
                }
            }

            if (forceacct_set_act4rule != "")
            {
                RadComboAcct_Code.Text = forceacct_set_act4rule;
            }
            else
            {
                if (acctcodebpnature != "")
                {

                    RadComboAcct_Code.Text = acctcodebpnature;
                }
                else
                {

                    if (acct_set_doc != "")
                    {
                        RadComboAcct_Code.Text = acct_set_doc;
                    }
                    else
                    {
                        RadComboAcct_Code.Text = supplieracct;
                    }
                }
            }
            if (RadComboAcct_Code.SelectedValue == "")
            {

            }
            else
            {
                RadComboAcct_Code.ToolTip = RadComboAcct_Code.SelectedValue.Split(new char[] { ':' })[1];
            }


        }

    }
    protected void RadComboAcct_Code_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (RadComboAcct_Code.SelectedValue == "")
        {

        }
        else
        {
            RadComboAcct_Code.ToolTip = RadComboAcct_Code.SelectedValue.Split(new char[] { ':' })[1];
        }
    }


    protected void GridView6_RowEditing(object sender, GridViewEditEventArgs e)
    {
        inv.GV_EditCommand(GridView6, e, (DataTable)ViewState["DirectDetail"], false);
        //GridView6.EditIndex = e.NewEditIndex;
        //PopulateGridview();


        //string avv = GridView6.EditIndex.ToString();
        //string avv1 = GridView6.Rows[GridView6.EditIndex].ToString();

        //DropDownList ddlitemsub = GridView6.Rows[GridView6.EditIndex].FindControl("ddlItemSubHeadEdit") as DropDownList;
        //dml.dropdownsql(ddlitemsub, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");
        //Label lbl1 = GridView6.Rows[GridView6.EditIndex].FindControl("lblItemSubHead") as Label;

        //DropDownList ddlitemmaster = GridView6.Rows[GridView6.EditIndex].FindControl("ddlItemMasterEdit") as DropDownList;
        //dml.dropdownsql(ddlitemmaster, "SET_ItemMaster", "Description", "ItemID");
        //Label lbl2 = GridView6.Rows[GridView6.EditIndex].FindControl("lblItemMaster") as Label;

        //ddlitemmaster.ClearSelection();
        //ddlitemmaster.Items.FindByText(lbl2.Text).Selected = true;
        //lbl2.Visible = false;

        //DropDownList uddluom = GridView6.Rows[GridView6.EditIndex].FindControl("ddlUOMEdit") as DropDownList;

        //dml.dropdownsqlwithquery(uddluom, "select UOMID,UOMName from SET_UnitofMeasure where UOMID in ((SELECT UOMId as uom FROM SET_ItemMaster where ItemID = '" + ddlitemmaster.SelectedItem.Value + "' ) UNION (SELECT UOMId2 as uom FROM SET_ItemMaster where ItemID = '" + ddlitemmaster.SelectedItem.Value + "' ))", "UOMName", "UOMID");
        //Label lbl3 = GridView6.Rows[GridView6.EditIndex].FindControl("lblUOMEdit") as Label;

        //DropDownList ddlcc = GridView6.Rows[GridView6.EditIndex].FindControl("ddlLocationEdit") as DropDownList;
        //dml.dropdownsql(ddlcc, "Set_Location", "LocName", "LocId");
        //Label lblcc = GridView6.Rows[GridView6.EditIndex].FindControl("lblLocationEdit") as Label;


        //DropDownList ddlCostCenter = GridView6.Rows[GridView6.EditIndex].FindControl("ddlCostCenterEdit") as DropDownList;
        //dml.dropdownsql(ddlCostCenter, "SET_CostCenter", "CostCenterName", "CostCenterID");
        //Label lblcostcenter = GridView6.Rows[GridView6.EditIndex].FindControl("lblCostCenterEdit") as Label;

        ////DropDownList ddluom2 = GridView6.Rows[GridView6.EditIndex].FindControl("ddlUOM2Edit") as DropDownList;
        ////dml.dropdownsql(ddluom2, "SET_UnitofMeasure", "UOMName", "UOMID");
        ////Label lblUom2 = GridView6.Rows[GridView6.EditIndex].FindControl("lblUOM2Edit") as Label;



        //ddlitemsub.ClearSelection();
        //ddlitemsub.Items.FindByText(lbl1.Text).Selected = true;
        //lbl1.Visible = false;

        //uddluom.ClearSelection();
        //uddluom.Items.FindByText(lbl3.Text).Selected = true;
        //lbl3.Visible = false;

        //ddlcc.ClearSelection();
        //ddlcc.Items.FindByText(lblcc.Text).Selected = true;
        //lblcc.Visible = false;

        //ddlCostCenter.ClearSelection();
        //ddlCostCenter.Items.FindByText(lblcostcenter.Text).Selected = true;
        //lblcostcenter.Visible = false;

        //// ddluom2.ClearSelection();
        ////ddluom2.Items.FindByText(lblUom2.Text).Selected = true;
        ////   lblUom2.Visible = false;



    }
    protected void ddlitemsub_SelectedIndexChanged(object sender, EventArgs e)
    {
        //select * from SET_ItemMaster where ItemSubHeadID= 3
        DropDownList ddlsubitem = GridView6.FooterRow.FindControl("ddlItemSubHeadFooter") as DropDownList;
        DropDownList ddlmaster = GridView6.FooterRow.FindControl("ddlItemMasterFooter") as DropDownList;
        if (ddlsubitem.SelectedIndex > 0)
        {
            dml.dropdownsql(ddlmaster, "SET_ItemMaster", "Description", "ItemID", "ItemSubHeadID", ddlsubitem.SelectedItem.Value);
        }
        
    }
    protected void ddlitemMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlmaster = GridView6.FooterRow.FindControl("ddlItemMasterFooter") as DropDownList;
        DropDownList ddlsubitem = GridView6.FooterRow.FindControl("ddlItemSubHeadFooter") as DropDownList;
        RadComboBox ddlaccout = GridView6.FooterRow.FindControl("RadComboAcct_CodeFooter") as RadComboBox;
        if (ddlmaster.SelectedIndex != 0)
        {
            //
            string valitem = "";
            string valitem1 = "";
            string valitemcode = "";
            DropDownList ddluomm = GridView6.FooterRow.FindControl("ddlUOMFooter") as DropDownList;
            dml.dropdownsqlwithquery(ddluomm, "select UOMID,UOMName from SET_UnitofMeasure where UOMID in ((SELECT UOMId as uom FROM SET_ItemMaster where ItemID = '" + ddlmaster.SelectedItem.Value + "' ) UNION (SELECT UOMId2 as uom FROM SET_ItemMaster where ItemID = '" + ddlmaster.SelectedItem.Value + "' ))", "UOMName", "UOMID");


            DropDownList ddluomm2 = GridView6.FooterRow.FindControl("ddlUOM2Footer") as DropDownList;
            dml.dropdownsqlwithquery(ddluomm2, "select UOMID,UOMName from SET_UnitofMeasure where UOMID in ((SELECT UOMId as uom FROM SET_ItemMaster where ItemID = '" + ddlmaster.SelectedItem.Value + "' ) UNION (SELECT UOMId2 as uom FROM SET_ItemMaster where ItemID = '" + ddlmaster.SelectedItem.Value + "' ))", "UOMName", "UOMID");


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
            if (valitem1 != "")
            {
                ddluomm2.ClearSelection();
                if (ddluomm2.Items.FindByValue(valitem1) != null)
                {
                    ddluomm2.Items.FindByValue(valitem1).Selected = true;
                }
            }




            string dbcr_detail = "";
            DataSet dsdebcr_detail = dml.Find("SELECT	GLImpact, InventoryImpact,AccountType FROM SET_AcRules4Doc WHERE DocId = '" + ddlDocName.SelectedItem.Value + "' AND MasterDetail = 'D' AND CompID = '" + compid() + "' AND Record_Deleted = 0 AND IsActive = 1 AND IsHide = 0 ;");
            if (dsdebcr_detail.Tables[0].Rows.Count > 0)
            {
                dbcr_detail = dsdebcr_detail.Tables[0].Rows[0]["AccountType"].ToString();
            }



            string asset = "0", consum = "0", expense = "0", itemactasset = "0", itemactconsum = "0", itemactexpense = "0", itemtypeid = "0";

            DataSet ds_as_C_E = dml.Find("select IsAsset,IsConsumable,IsExpense,ItemTypeID from SET_ItemMaster where ItemSubHeadID = '" + ddlsubitem.SelectedItem.Value + "' AND Record_Deleted = 0 and ItemID = '" + ddlmaster.SelectedItem.Value + "';");
            if (ds_as_C_E.Tables[0].Rows.Count > 0)
            {
                asset = ds_as_C_E.Tables[0].Rows[0]["IsAsset"].ToString();
                expense = ds_as_C_E.Tables[0].Rows[0]["IsExpense"].ToString();
                consum = ds_as_C_E.Tables[0].Rows[0]["IsConsumable"].ToString();
                itemtypeid = ds_as_C_E.Tables[0].Rows[0]["ItemTypeID"].ToString();


                DataSet dsitemtype = dml.Find("select ExpenseAcct,FixedAssetAcct,InventoryAcct from SET_ItemType where ItemTypeID = '" + itemtypeid + "'");

                if (dsitemtype.Tables[0].Rows.Count > 0)
                {
                    itemactasset = dsitemtype.Tables[0].Rows[0]["FixedAssetAcct"].ToString();
                    itemactconsum = dsitemtype.Tables[0].Rows[0]["InventoryAcct"].ToString();
                    itemactexpense = dsitemtype.Tables[0].Rows[0]["ExpenseAcct"].ToString();
                }


                if (asset == "True")
                {
                    ddlaccout.Text = itemactasset;
                }
                if (expense == "True")
                {
                    ddlaccout.Text = itemactexpense;
                }
                if (consum == "True")
                {
                    ddlaccout.Text = itemactconsum;

                }
                DataSet dsacct = dml.Find("select Acct_Description  from  SET_COA_detail where Acct_Code = '"+ ddlaccout.Text + "'");

                if (dsacct.Tables[0].Rows.Count > 0)
                {
                    ddlaccout.ToolTip = dsacct.Tables[0].Rows[0]["Acct_Description"].ToString();
                }
                

            }





            //DataSet ds_acccocde = dml.Find("select InventoryAcct,ItemTypeID from SET_ItemType where ItemTypeID in (select  ItemTypeID from SET_ItemMaster where ItemID = '" + ddlmaster.SelectedItem.Value+"')");


            //if (ds_acccocde.Tables[0].Rows.Count > 0)
            //{
            //    string itemtype = ds_acccocde.Tables[0].Rows[0]["ItemTypeID"].ToString();
            // //   valitemcode = ds_acccocde.Tables[0].Rows[0]["InventoryAcct"].ToString();

            //     valitemcode = gl.itemtypecode(dbcr_detail, itemtype);

            //}
            //if (valitemcode != "")
            //{

            //    ddlaccout.Text = valitemcode;
            //}


        }
    }
    protected void GridView6_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {


            string Accode = (GridView6.Rows[e.RowIndex].FindControl("lblAcctCode") as Label).Text;
            string ItemSubHeadName = (GridView6.Rows[e.RowIndex].FindControl("ddlItemSubHeadEdit") as DropDownList).SelectedItem.Text;
            string Description = (GridView6.Rows[e.RowIndex].FindControl("ddlItemMasterEdit") as DropDownList).SelectedItem.Text;
            string UOM = (GridView6.Rows[e.RowIndex].FindControl("ddlUOMEdit") as DropDownList).SelectedItem.Text;
            string Qty = (GridView6.Rows[e.RowIndex].FindControl("txtDCQtyEdit") as TextBox).Text;
            string Rate = (GridView6.Rows[e.RowIndex].FindControl("lblRateEdit") as TextBox).Text;
            string Value = (GridView6.Rows[e.RowIndex].FindControl("txtValueEdit") as TextBox).Text;
            string GST = (GridView6.Rows[e.RowIndex].FindControl("ddlGST6Edit") as DropDownList).SelectedItem.Text;
            string AddGST = (GridView6.Rows[e.RowIndex].FindControl("ddlAddGSTEdit") as DropDownList).SelectedItem.Text;
            string GSTAmount = (GridView6.Rows[e.RowIndex].FindControl("lblGSTAmountEdit") as Label).Text;
            string TotalAmount = (GridView6.Rows[e.RowIndex].FindControl("txtTotalAmount") as TextBox).Text;
            string BalanceTax = (GridView6.Rows[e.RowIndex].FindControl("txtBalanceTax") as TextBox).Text;
            string BalanceEdit = (GridView6.Rows[e.RowIndex].FindControl("txtBalanceEdit") as TextBox).Text;
            string DC_PO_GRN = (GridView6.Rows[e.RowIndex].FindControl("txtDC_PO_GRNEdit") as TextBox).Text;

            string CostCenter = (GridView6.Rows[e.RowIndex].FindControl("ddlCostCenterEdit") as DropDownList).SelectedItem.Text;
            string Location = (GridView6.Rows[e.RowIndex].FindControl("ddlLocationEdit") as DropDownList).SelectedItem.Text;
            string Project = (GridView6.Rows[e.RowIndex].FindControl("txtProjectEdit") as TextBox).Text;
            string WarranteeFooter = (GridView6.Rows[e.RowIndex].FindControl("txtWarranteeEdit") as TextBox).Text;

            string Qty2 = (GridView6.Rows[e.RowIndex].FindControl("txtQty2Edit") as TextBox).Text;
            string Rate2 = (GridView6.Rows[e.RowIndex].FindControl("txtRate2Edit") as TextBox).Text;


            GridViewRow row = GridView6.Rows[e.RowIndex];
            DataTable dt = (DataTable)ViewState["DirectDetail"];


            dt.Rows[row.DataItemIndex]["AcctCode"] = Accode;
            dt.Rows[row.DataItemIndex]["ItemSubHead"] = ItemSubHeadName;
            dt.Rows[row.DataItemIndex]["ItemMaster"] = Description;
            dt.Rows[row.DataItemIndex]["UOM"] = UOM;
            dt.Rows[row.DataItemIndex]["DCQty"] = Qty;
            dt.Rows[row.DataItemIndex]["Rate"] = Rate;
            dt.Rows[row.DataItemIndex]["Value"] = Value;
            dt.Rows[row.DataItemIndex]["Gst"] = GST;
            dt.Rows[row.DataItemIndex]["AddGst"] = AddGST;
            dt.Rows[row.DataItemIndex]["TotalAmount"] = TotalAmount;
            dt.Rows[row.DataItemIndex]["BalanceAmount"] = BalanceEdit;
            dt.Rows[row.DataItemIndex]["BalanceTax"] = BalanceTax;
            dt.Rows[row.DataItemIndex]["dc_po_grn"] = DC_PO_GRN;
            dt.Rows[row.DataItemIndex]["CostCenter"] = CostCenter;
            dt.Rows[row.DataItemIndex]["Location"] = Location;
            dt.Rows[row.DataItemIndex]["Project"] = Project;
            dt.Rows[row.DataItemIndex]["Warrantee"] = WarranteeFooter;
            dt.Rows[row.DataItemIndex]["Qty2"] = Qty2;
            dt.Rows[row.DataItemIndex]["Rate2"] = Rate2;


            GridView6.EditIndex = -1;
            PopulateGridview();
            DropDownList ddlsub = GridView6.FooterRow.FindControl("ddlItemSubHeadFooter") as DropDownList;
            dml.dropdownsql(ddlsub, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");

            DropDownList ddlmaster = GridView6.FooterRow.FindControl("ddlItemMasterFooter") as DropDownList;
            dml.dropdownsql(ddlmaster, "SET_ItemMaster", "Description", "ItemID");

            DropDownList ddluom = GridView6.FooterRow.FindControl("ddlUOMFooter") as DropDownList;
            dml.dropdownsql(ddluom, "SET_UnitofMeasure", "UOMName", "UOMID");

            DropDownList ddlsupFooter = GridView6.FooterRow.FindControl("lblCostCenterFooter") as DropDownList;
            dml.dropdownsql(ddlsupFooter, "SET_CostCenter", "CostCenterName", "CostCenterID");

            DropDownList ddluom2 = GridView6.FooterRow.FindControl("ddlUOM2Footer") as DropDownList;
            dml.dropdownsql(ddluom2, "SET_UnitofMeasure", "UOMName", "UOMID");

            DropDownList ddlLocation = GridView6.FooterRow.FindControl("ddlLocationFooter") as DropDownList;
            dml.dropdownsql(ddlLocation, "SET_Department", "DepartmentName", "DepartmentID", "IsWarehouse", "1");

            DropDownList ddlAddGSTFooter = GridView6.FooterRow.FindControl("ddlAddGSTFooter") as DropDownList;
            dml.dropdownsqlwithquery(ddlAddGSTFooter, "SELECT Percentage, TaxDescription +'(' + CONVERT(VARCHAR, Percentage) + ')' as perc from SET_Tax", "perc", "Percentage");

            DropDownList ddlGSTFooter = GridView6.FooterRow.FindControl("ddlGST6") as DropDownList;
            dml.dropdownsqlwithquery(ddlGSTFooter, "SELECT Percentage, TaxDescription +'(' + CONVERT(VARCHAR, Percentage) + ')' as perc from SET_Tax", "perc", "Percentage");


        }
        catch (Exception ex)
        {

        }
    }
    protected void GridView6_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView6.EditIndex = -1;
        PopulateGridview();


        DropDownList ddlsub = GridView6.FooterRow.FindControl("ddlItemSubHeadFooter") as DropDownList;
        dml.dropdownsql(ddlsub, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");

        DropDownList ddlmaster = GridView6.FooterRow.FindControl("ddlItemMasterFooter") as DropDownList;
        dml.dropdownsql(ddlmaster, "SET_ItemMaster", "Description", "ItemID");

        DropDownList ddluom = GridView6.FooterRow.FindControl("ddlUOMFooter") as DropDownList;
        dml.dropdownsql(ddluom, "SET_UnitofMeasure", "UOMName", "UOMID");

        DropDownList ddlsupFooter = GridView6.FooterRow.FindControl("lblCostCenterFooter") as DropDownList;
        dml.dropdownsql(ddlsupFooter, "SET_CostCenter", "CostCenterName", "CostCenterID");

        DropDownList ddluom2 = GridView6.FooterRow.FindControl("ddlUOM2Footer") as DropDownList;
        dml.dropdownsql(ddluom2, "SET_UnitofMeasure", "UOMName", "UOMID");

        DropDownList ddlLocation = GridView6.FooterRow.FindControl("ddlLocationFooter") as DropDownList;
        dml.dropdownsql(ddlLocation, "Set_Location", "LocName", "LocID");
    }
    protected void GridView9_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lblRowNumber") as Label).Text = (e.Row.RowIndex + 1).ToString();
        }
    }
    protected void GridView9_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView9.EditIndex = e.NewEditIndex;


        //DataSet ds_detail = dml.Find("select * from ViewPODetail_FED where Sno_Master = '" + ViewState["SNO"].ToString() + "'");
        DataSet ds_detail = dml.Find("select * from ViewInvINDetail_FED where Sno_Master = '" + ViewState["SNO"].ToString() + "'");
        if (ds_detail.Tables[0].Rows.Count > 0)
        {
            Div6.Visible = true;
            GridView9.DataSource = ds_detail.Tables[0];
            GridView9.DataBind();
        }
        //DropDownList ddlitemsub = GridView9.Rows[GridView9.EditIndex].FindControl("ddlitemsubEDIT") as DropDownList;
        //dml.dropdownsql(ddlitemsub, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");
        //Label lbl1 = GridView9.Rows[GridView9.EditIndex].FindControl("lblsubhead") as Label;

        //DropDownList ddlitemmaster = GridView9.Rows[GridView9.EditIndex].FindControl("ddlItemmter") as DropDownList;
        //dml.dropdownsql(ddlitemmaster, "SET_ItemMaster", "Description", "ItemID");
        //Label lbl2 = GridView9.Rows[GridView9.EditIndex].FindControl("lblitemmaster") as Label;

        //ddlitemmaster.ClearSelection();
        //ddlitemmaster.Items.FindByText(lbl2.Text).Selected = true;
        //lbl2.Visible = false;

        //DropDownList uddluom = GridView9.Rows[GridView9.EditIndex].FindControl("ddlUOMs") as DropDownList;
        //// dml.dropdownsql(uddluom, "SET_UnitofMeasure", "UOMName", "UOMID");
        //dml.dropdownsqlwithquery(uddluom, "select UOMID,UOMName from SET_UnitofMeasure where UOMID in ((SELECT UOMId as uom FROM SET_ItemMaster where ItemID = '" + ddlitemmaster.SelectedItem.Value + "' ) UNION (SELECT UOMId2 as uom FROM SET_ItemMaster where ItemID = '" + ddlitemmaster.SelectedItem.Value + "' ))", "UOMName", "UOMID");
        //Label lbl3 = GridView9.Rows[GridView9.EditIndex].FindControl("lbluom") as Label;

        //DropDownList ddlcc = GridView9.Rows[GridView9.EditIndex].FindControl("ddlLocation") as DropDownList;
        //dml.dropdownsql(ddlcc, "Set_Location", "LocName", "LocId");
        //TextBox lblcc = GridView9.Rows[GridView9.EditIndex].FindControl("txtLocation") as TextBox;

        //ddlitemsub.ClearSelection();
        //ddlitemsub.Items.FindByText(lbl1.Text).Selected = true;
        //lbl1.Visible = false;

        //uddluom.ClearSelection();
        //uddluom.Items.FindByText(lbl3.Text).Selected = true;
        //lbl3.Visible = false;

        //ddlcc.ClearSelection();
        //ddlcc.Items.FindByText(lblcc.Text).Selected = true;
        //lblcc.Visible = false;

    }
    protected void GridView9_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        try
        {
            string txtDCQty = (GridView9.Rows[e.RowIndex].FindControl("txtDCQty") as TextBox).Text;
            string lblSno = (GridView9.Rows[e.RowIndex].FindControl("lblpurSnoED") as Label).Text;

            //ddlSupplierEdit, ddlUomEdit, ddlItemMasterEdit
            GridViewRow row = GridView9.Rows[e.RowIndex];
            DataTable dt = (DataTable)ViewState["dtup"];



            dt.Rows[row.DataItemIndex]["Quantity"] = txtDCQty;

            string balqtyPO = "0";
            float newbalqtyPO = 0;
            // gridview pono, qty



            DataSet ds = dml.Find("Select balQty from Set_PurchaseOrderDetail  where Sno = '" + lblSno + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                balqtyPO = ds.Tables[0].Rows[0]["balQty"].ToString();
            }

            newbalqtyPO = float.Parse(balqtyPO) + float.Parse(txtDCQty);
            //lblPONo
            dml.Update("Update Set_PurchaseOrderDetail set balQty='" + newbalqtyPO.ToString() + "' where Sno =  '" + lblSno + "' ", "");

            dml.Update("UPDATE Set_PurcahseOrderMaster set Status = '1' where Sno in (select Sno_Master from Set_PurchaseOrderDetail where Sno = '" + lblSno + "')", "");





            //dml.Update("UPDATE [SET_StockRequisitionDetail] SET [ItemSubHead]='" + txtitemsubfooter.SelectedItem.Value + "', [ItemMaster]='" + txtdesc.SelectedItem.Value + "', [CostCenter]='" + txtsupplierFooter.SelectedItem.Value + "', [UOM]='" + txtuomFooter.SelectedItem.Value + "', [Project]='" + txtProjectEdit + "', [CurrentStock]='" + txtcurrStockFooter + "', [RequiredQuantity]='" + txtReqStockFooter + "', [Remarks]=NULL, [IsActive]='1', [GocID]='" + gocid() + "', [CompId]='" + compid() + "', [BranchId]='" + branchId() + "', [FiscalYearID]='" + FiscalYear() + "', [UpdatedBy]='" + show_username() + "', [UpdatedDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', [Record_Deleted]='0' WHERE ([Sno]='" + lblsno + "');", "");
            // dml.Update("UPDATE Set_PurchaseOrderDetail SET  [PRNo_AQno]='" + lblreq + "', [ItemSubHead]='" + ddlitemsubedit.SelectedItem.Value + "', [ItemMaster]='" + ddlitemMasteredit.SelectedItem.Value + "', [UOM]='" + ddluomedit.SelectedItem.Value + "', [Quantity]='" + txtqtyvalue + "', [Rate]='" + txtRateEdit + "', [GST]='" + lblGST + "', [GSTRate]='" + txtGSTRate + "', [QtyValue]='" + txtqtyvalue + "', [GstValue]='" + txtgstvalue + "', [GrossValue]='" + txtGrossValue + "', [Remarts]=NULL, [Qty2]='" + txtQty2Edit + "', [UOM2]='" + ddluomedit.SelectedItem.Value + "', [Rate2]='" + txtRate2Edit + "', [Location]='" + ddlLocation.SelectedItem.Value + "', [Project]='" + txtproject + "', [UpdatedBy]='" + show_username() + "', [UpdatedDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' , [ApprovedQuantity]='" + txtApprovedQtyEdit + "' WHERE ([Sno]='" + lblsno + "')", "");
            GridView9.EditIndex = -1;

            //fl = true;
            findeditdel();


        }

        catch (Exception ex)
        {
            // lblSuccessMessage.Text = "";
            // lblErrorMessage.Text = ex.Message;
        }
    }
    protected void GridView9_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView9.EditIndex = -1;

        DataSet ds_detail = dml.Find("select * from ViewInvINDetail_FED where Sno_Master = '" + ViewState["SNO"].ToString() + "'");
        if (ds_detail.Tables[0].Rows.Count > 0)
        {
            Div6.Visible = true;
            GridView9.DataSource = ds_detail.Tables[0];
            GridView9.DataBind();
        }
    }
    protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSupplier.SelectedIndex != 0)
        {
            DataSet ds = dml.Find("select Sno, [DocumentNo.] + '--' + BPartnerName as docnosupp FROM View_POlistPoNo where Status <> 2 and balQty >0 and IsActive = 1 and BPartnerTypeID = '" + ddlSupplier.SelectedItem.Value + "' and Record_Deleted = 0 and GocID = '" + gocid() + "' and CompId = '" + compid() + "' and BranchId = '" + branchId() + "' and FiscalYearID = '" + FiscalYear() + "'");


            lstFruits.DataSource = ds.Tables[0];
            lstFruits.DataBind();

        }
        else
        {

            DataSet ds = dml.Find("select Sno, [DocumentNo.] + '--' + BPartnerName as docnosupp FROM View_POlistPoNo where Status <> 2 and balQty >0 and IsActive = 1 and Record_Deleted = 0 and GocID = '" + gocid() + "' and CompId = '" + compid() + "' and BranchId = '" + branchId() + "' and FiscalYearID = '" + FiscalYear() + "'");


            lstFruits.DataSource = ds.Tables[0];
            lstFruits.DataBind();



        }
    }
    public void bindlist()
    {
        DataSet ds = dml.Find("select Sno, [DocumentNo.] + '--' + BPartnerName as docnosupp FROM View_POlistPoNo where Status <> 2 and balQty > 0 and IsActive = 1 and Record_Deleted = 0 and GocID = '" + gocid() + "' and CompId = '" + compid() + "' and BranchId = '" + branchId() + "' and FiscalYearID = '" + FiscalYear() + "'");
        if (ds.Tables[0].Rows.Count > 0)
        {
            lstFruits.DataSource = ds.Tables[0];
            lstFruits.DataBind();
        }
    }
    protected void GridView10_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {

            dml.Delete("update Act_GL_Detail set Record_Deleted = 1 where MasterSno= " + ddlDocName.SelectedItem.Value + "", "");
            dml.Delete("INSERT INTO Act_GL_Detail_Deleted ([DocId], [DocDescription], [MasterSno], [DetailSno], [EntryDate], [VoucherNo], [AccountCode], [BPNatureID], [BPartnerId], [Name], [ChqNo], [ChqDate], [InvoiceNo], [InvoiceDate], [DrAmount], [CrAmount], [Narration], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [Record_Deleted]) SELECT [DocId], [DocDescription], [MasterSno], [DetailSno], [EntryDate], [VoucherNo], [AccountCode], [BPNatureID], [BPartnerId], [Name], [ChqNo], [ChqDate], [InvoiceNo], [InvoiceDate], [DrAmount], [CrAmount], [Narration], [GocID], [CompId], [BranchId], [FiscalYearID], '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', [UpdatedBy], [UpdatedDate], '1' FROM Act_GL_Detail WHERE MasterSno='" + ViewState["SNO"].ToString() + "';", "");
            dml.Delete("Delete from Act_GL_Detail where MasterSno = " + ddlDocName.SelectedItem.Value + "", "");
            dml.Delete("Delete from Act_GL where MasterSno = " + ddlDocName.SelectedItem.Value + "", "");
            gl.GlentrySummaryDelete(txtDocNo.Text);

            DropDownList ddlitemsubedit = (GridView10.Rows[e.RowIndex].FindControl("ddlItemSubHeadEdit") as DropDownList);
            DropDownList ddlitemMasteredit = (GridView10.Rows[e.RowIndex].FindControl("ddlItemMasterEdit") as DropDownList);
            DropDownList ddluomedit = (GridView10.Rows[e.RowIndex].FindControl("ddlUOMEdit") as DropDownList);

            // string POBalqty = "0";//(GridView10.Rows[e.RowIndex].FindControl("txtPObalQty") as TextBox).Text;
            // string DCQty = (GridView10.Rows[e.RowIndex].FindControl("txtDCQty") as TextBox).Text;

            // DropDownList ddlCostCenterEdit = (GridView10.Rows[e.RowIndex].FindControl("ddlCostCenterEdit") as DropDownList);
            // DropDownList ddlLocationEdit = (GridView10.Rows[e.RowIndex].FindControl("ddlLocationEdit") as DropDownList);

            //  string txtrateEdit = (GridView10.Rows[e.RowIndex].FindControl("lblRateEdit") as TextBox).Text;
            //  string txtValueEdit = (GridView10.Rows[e.RowIndex].FindControl("txtValueEdit") as TextBox).Text;

            //  string txtProjectEdit = (GridView10.Rows[e.RowIndex].FindControl("txtProjectEdit") as TextBox).Text;
            //  string txtWarranteeEdit = (GridView10.Rows[e.RowIndex].FindControl("txtWarranteeEdit") as TextBox).Text;
            //  DropDownList ddlUOM2Edit = (GridView10.Rows[e.RowIndex].FindControl("ddlUOM2Edit") as DropDownList);
            //  string txtRate2Edit = (GridView10.Rows[e.RowIndex].FindControl("txtRate2Edit") as TextBox).Text;
            //  string txtQty2Edit = (GridView10.Rows[e.RowIndex].FindControl("txtQty2Edit") as TextBox).Text;
            // string lblsno = (GridView10.Rows[e.RowIndex].FindControl("lbldetailSno") as Label).Text;
            //dt.Columns.AddRange(new DataColumn[6] 

            //ddlSupplierEdit, ddlUomEdit, ddlItemMasterEdit
            GridViewRow row = GridView10.Rows[e.RowIndex];
            DataTable dt = (DataTable)ViewState["Customers1"];





            //  dt.Rows[row.DataItemIndex]["ItemSubHeadName"] = ddlitemsubedit.SelectedItem.Text;
            //  dt.Rows[row.DataItemIndex]["Description"] = ddlitemMasteredit.SelectedItem.Text;
            //  dt.Rows[row.DataItemIndex]["UOMName"] = ddluomedit.SelectedItem.Text;
            //dt.Rows[row.DataItemIndex]["Quantity"] = POBalqty.ToString();
            //  dt.Rows[row.DataItemIndex]["Quantity"] = DCQty.ToString();

            //  dt.Rows[row.DataItemIndex]["Rate"] = txtrateEdit;
            //  dt.Rows[row.DataItemIndex]["Value"] = txtValueEdit;

            //   dt.Rows[row.DataItemIndex]["CostCenterName"] = ddlCostCenterEdit.SelectedItem.Text;
            //dt.Rows[row.DataItemIndex]["Location"] = ddlLocationEdit.SelectedItem.Text;
            //   dt.Rows[row.DataItemIndex]["Project"] = txtProjectEdit;
            //   dt.Rows[row.DataItemIndex]["Warrantee"] = txtWarranteeEdit;

            //   dt.Rows[row.DataItemIndex]["uomName2"] = ddlUOM2Edit.SelectedItem.Text;
            //   dt.Rows[row.DataItemIndex]["Qty2"] = txtQty2Edit;
            //   dt.Rows[row.DataItemIndex]["Rate2"] = txtRate2Edit;






            //   dml.Update("UPDATE Inv_InventoryInDetail SET [ItemSubHead]='"+ddlitemsubedit.SelectedItem.Value+"', [ItemMaster]='"+ddlitemMasteredit.SelectedItem.Value+"', [UOM]='"+ddluomedit.SelectedItem.Value+"', [Quantity]='"+DCQty+"', [Rate]='"+ txtrateEdit+ "', [Value]='"+txtValueEdit+"', [Qty2]='"+txtQty2Edit+"', [UOM2]='"+ddlUOM2Edit.SelectedItem.Value+"', [Rate2]='"+txtRate2Edit+"', [CostCenter]='"+ddlCostCenterEdit.SelectedItem.Value+"', [Location]='1', [CompId]='"+gocid()+"', [BranchId]='"+branchId()+"', [FiscalYearID]='"+FiscalYear()+"',  [UpdatedBy]='"+show_username()+"', [UpdatedDate]='"+DateTime.Now.ToString()+"', [Project]='"+txtProjectEdit+"', [Warrentee]='"+txtWarranteeEdit+"'  WHERE ([Sno]='"+ViewState["SNO"].ToString()+"')", "");

            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "DetEditalert()", true);

            GridView10.EditIndex = -1;

            //fl = true;
            PopulateGridview_UpGrid10();



            detailinsertEdit(ViewState["SNO"].ToString());
            gl.GlentrySummaryDetail(show_username(), DateTime.Now.ToString(), "0", txtDocNo.Text);






        }

        catch (Exception ex)
        {

            // lblSuccessMessage.Text = "";
            // lblErrorMessage.Text = ex.Message;
        }
    }
    protected void GridView10_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView10.EditIndex = e.NewEditIndex;

        DataSet ds_detail = dml.Find("select * from ViewInvINDetail_FED where Sno_Master = '" + ViewState["SNO"].ToString() + "'");
        if (ds_detail.Tables[0].Rows.Count > 0)
        {
            Div7.Visible = true;
            GridView10.DataSource = ds_detail.Tables[0];
            GridView10.DataBind();
        }

        Label lbl1 = GridView10.Rows[GridView10.EditIndex].FindControl("lblItemSubHead") as Label;
        Label lbl2 = GridView10.Rows[GridView10.EditIndex].FindControl("lblItemMaster") as Label;
        Label lblcost = GridView10.Rows[GridView10.EditIndex].FindControl("lblCostCenterEdit") as Label;
        Label lbl3 = GridView10.Rows[GridView10.EditIndex].FindControl("lblUOM") as Label;
        Label lblUOM2Edit = GridView10.Rows[GridView10.EditIndex].FindControl("lblUOM2Edit") as Label;
        TextBox lblcc = GridView10.Rows[GridView10.EditIndex].FindControl("txtLocationEdit") as TextBox;
        TextBox aaa = GridView10.Rows[GridView10.EditIndex].FindControl("txtDCQty") as TextBox;




    }
    protected void GridView10_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView10.EditIndex = -1;
        DataSet ds_detail = dml.Find("select * from ViewInvINDetail_FED where Sno_Master = '" + ddlDocName.SelectedItem.Value + "'");
        if (ds_detail.Tables[0].Rows.Count > 0)
        {
            Div7.Visible = true;
            GridView10.DataSource = ds_detail.Tables[0];
            GridView10.DataBind();
        }
    }

    protected void GridView10_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = GridView10.Rows[e.RowIndex];
        DataTable dt = (DataTable)ViewState["Customers1"];
        dt.Rows[e.RowIndex].Delete();
        this.PopulateGridview_UpGrid10();

    }


    public void detailinsertEdit(string masterid)
    {

        float totalcrdb = 0;
        string detailid = "0";
        string bpnatureid = "0";
        //string detailid = "0";
        string doctype = "0";
        DataSet dsdoctype = dml.Find("select DocTypeId from SET_Documents where DocID = '" + ddlDocName.SelectedItem.Value + "';");
        if (dsdoctype.Tables[0].Rows.Count > 0)
        {

            doctype = dsdoctype.Tables[0].Rows[0]["DocTypeId"].ToString();

        }
        foreach (GridViewRow g in GridView10.Rows)
        {

            Label lblAccountcode = (Label)g.FindControl("lblAccountCode");
            Label lblPONo = (Label)g.FindControl("lblPONo");
            Label lblItemSubHead = (Label)g.FindControl("lblItemSubHead");
            Label lblitemmaster = (Label)g.FindControl("lblItemMaster");
            Label lbluom = (Label)g.FindControl("lblUOM");
            Label lblPObalQty = (Label)g.FindControl("lblPObalQty");


            Label lblDCQty = (Label)g.FindControl("lblDCQty");

            Label lblRate = (Label)g.FindControl("lblRate");
            Label lblValue = (Label)g.FindControl("lblValue");

            Label lblCostCenter = (Label)g.FindControl("lblCostCenter");
            Label lblLocation = (Label)g.FindControl("lblLocation");
            Label lblProject = (Label)g.FindControl("lblProject");

            Label lblWarrantee = (Label)g.FindControl("lblWarrantee");
            Label lblUOM2 = (Label)g.FindControl("lblUOM2");
            Label lblQty2 = (Label)g.FindControl("lblQty2");
            Label lblRate2 = (Label)g.FindControl("lblRate2");

            lblDCQty.Text = ViewState["QtyDC"].ToString();
            lblRate.Text = ViewState["RateEdit"].ToString();
            lblValue.Text = ViewState["valueEdit"].ToString();
            //
            dml.Update("update Inv_InventoryInDetail set Quantity='" + lblDCQty.Text + "', Rate='" + lblRate.Text + "', [Value]='" + lblValue.Text + "' where Sno_Master = '" + masterid + "'", "");
            //select ItemSubHeadID, ItemSubHeadName from SET_ItemSubHead lblproject

            DataSet ds = dml.Find("select ItemSubHeadID, ItemSubHeadName from SET_ItemSubHead where ItemSubHeadName = '" + lblItemSubHead.Text + "'");
            string subhead, itemmaster, uom1, uom2, costcenter, location, itemcode, itemtypeid;
            if (ds.Tables[0].Rows.Count > 0)
            {
                subhead = ds.Tables[0].Rows[0]["ItemSubHeadID"].ToString();
            }
            else
            {
                subhead = "0";
            }
            DataSet ds1 = dml.Find("select  ItemID , Description, ItemTypeID, ItemCode from SET_ItemMaster where Description = '" + lblitemmaster.Text + "'");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                itemmaster = ds1.Tables[0].Rows[0]["ItemID"].ToString();
                itemcode = ds1.Tables[0].Rows[0]["ItemCode"].ToString();
                itemtypeid = ds1.Tables[0].Rows[0]["ItemTypeID"].ToString();
            }
            else
            {
                itemmaster = "0";
                itemcode = "0";
                itemtypeid = "0";
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

            DataSet ds3 = dml.Find("select  CostCenterID,CostCenterName from SET_CostCenter where CostCenterName = '" + lblCostCenter.Text + "'");
            if (ds3.Tables[0].Rows.Count > 0)
            {
                costcenter = ds3.Tables[0].Rows[0]["CostCenterID"].ToString();
            }
            else
            {
                costcenter = "0";
            }
            DataSet ds4 = dml.Find("select  UOMID,UOMName from SET_UnitofMeasure where UOMName = '" + lblUOM2.Text + "'");
            if (ds4.Tables[0].Rows.Count > 0)
            {
                uom2 = ds4.Tables[0].Rows[0]["UOMID"].ToString();
            }
            else
            {
                uom2 = "0";
            }

            DataSet ds5 = dml.Find("select LocId,LocName from Set_Location where LocName = '" + lblLocation.Text + "'");
            if (ds5.Tables[0].Rows.Count > 0)
            {
                location = ds5.Tables[0].Rows[0]["LocId"].ToString();
            }
            else
            {
                location = "0";
            }

            float balqty, balqty2;
            if (lblDCQty.Text.Length > 0)
            {
                balqty = float.Parse(lblDCQty.Text) - 0;// float.Parse("issueQty");
            }
            else
            {
                balqty = 0;

            }


            if (lblDCQty.Text.Length > 0)
            {
                balqty2 = float.Parse(lblDCQty.Text) - 0;// float.Parse("issueQty");
            }
            else
            {
                balqty2 = 0;

            }


            string dbcr_detail = "", dbcr_plusminus = "";
            string trantype = "";
            DataSet dsCRDR = dml.Find("select  Tran_Type from SET_Acct_Type where Acct_Type_Id in ( SELECT Acct_Type_ID from SET_COA_detail where Acct_Code = '" + lblAccountcode.Text + "')");
            if (dsCRDR.Tables[0].Rows.Count > 0)
            {
                trantype = dsCRDR.Tables[0].Rows[0]["Tran_Type"].ToString();

            }
            DataSet dsDeB;
            if (trantype == "Debit")
            {

                DataSet dsnature = dml.Find("select  BPNatureID,BPNatureDescription from SET_BPartnerNature where BPNatureID in (select BPNatureID from SET_BPartnerType where BPartnerID = '" + ddlSupplier.SelectedItem.Value + "')");
                if (dsnature.Tables[0].Rows.Count > 0)
                {
                    bpnatureid = dsnature.Tables[0].Rows[0]["BPNatureID"].ToString();
                }


                DataSet dsdebcr_detail = dml.Find("SELECT	GLImpact, InventoryImpact FROM SET_AcRules4Doc WHERE DocId = '" + ddlDocName.SelectedItem.Value + "' AND MasterDetail = 'D' AND CompID = '" + compid() + "' AND Record_Deleted = 0 AND IsActive = 1 AND IsHide = 0 ;");
                if (dsdebcr_detail.Tables[0].Rows.Count > 0)
                {
                    dbcr_detail = dsdebcr_detail.Tables[0].Rows[0]["GLImpact"].ToString();
                    dbcr_plusminus = dsdebcr_detail.Tables[0].Rows[0]["InventoryImpact"].ToString();
                }


                string CODEitemtype = gl.itemtypecode(dbcr_detail, itemtypeid);



                //gl Detail entry
                gl.GlentryInsert(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, masterid, detailid, dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, lblAccountcode.Text, bpnatureid, ddlSupplier.SelectedItem.Value, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), lblValue.Text, "0", txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyy hh:mm:ss.ffff"), itemcode, lblDCQty.Text, "0", "Inv_InventoryInDetail");




                //  dml.Update("update Set_QuotationReqMaster set Status = '6' where QuotationReqNO = '" + requisno.Text + "'", "");

            }
            else
            {


                DataSet dsdebcr_detail = dml.Find("SELECT	GLImpact, InventoryImpact FROM SET_AcRules4Doc WHERE DocId = '" + ddlDocName.SelectedItem.Value + "' AND MasterDetail = 'D' AND CompID = '" + compid() + "' AND Record_Deleted = 0 AND IsActive = 1 AND IsHide = 0 ;");
                if (dsdebcr_detail.Tables[0].Rows.Count > 0)
                {
                    dbcr_detail = dsdebcr_detail.Tables[0].Rows[0]["GLImpact"].ToString();
                    dbcr_plusminus = dsdebcr_detail.Tables[0].Rows[0]["InventoryImpact"].ToString();
                }


                string CODEitemtype = gl.itemtypecode(dbcr_detail, itemtypeid);
                //string bpnatureid = "0";
                DataSet dsnature = dml.Find("select  BPNatureID,BPNatureDescription from SET_BPartnerNature where BPNatureID in (select BPNatureID from SET_BPartnerType where BPartnerID = '" + ddlSupplier.SelectedItem.Value + "')");
                if (dsnature.Tables[0].Rows.Count > 0)
                {
                    bpnatureid = dsnature.Tables[0].Rows[0]["BPNatureID"].ToString();
                }


                //select ItemCode from SET_ItemMaster where ItemID = 1
                gl.GlentryInsert(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, masterid, detailid, dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, lblAccountcode.Text, bpnatureid, ddlSupplier.SelectedItem.Value, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), "0", lblValue.Text, txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyy hh:mm:ss.ffff"), itemcode, lblDCQty.Text, "0", "Inv_InventoryInDetail");




            }
            if (dbcr_plusminus == "PLUS")
            {
                totalcrdb = totalcrdb + float.Parse(lblValue.Text);
            }
            else if (dbcr_plusminus == "MINUS")
            {
                totalcrdb = totalcrdb - float.Parse(lblValue.Text);
            }
            else
            {
                totalcrdb = totalcrdb + float.Parse(lblValue.Text);
            }


        }
        //gl master entry


        string dbcr = "";
        DataSet dsdebcr = dml.Find("SELECT	GLImpact, InventoryImpact FROM SET_AcRules4Doc WHERE DocId = '" + ddlDocName.SelectedItem.Value + "' AND MasterDetail = 'M' AND CompID = 1 AND Record_Deleted = 0 AND IsActive = 1 AND IsHide = 0 ;");
        if (dsdebcr.Tables[0].Rows.Count > 0)
        {
            dbcr = dsdebcr.Tables[0].Rows[0]["GLImpact"].ToString();
        }
        if (dbcr == "Debit Impact")
        {
            gl.GlentryInsert(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, masterid, "0", dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, RadComboAcct_Code.Text, bpnatureid, ddlSupplier.SelectedItem.Value, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), totalcrdb.ToString(), "0", txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff"), "0", "0", "0", "Inv_InventoryInDetail");

        }
        else
        {
            gl.GlentryInsert(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, masterid, "0", dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, RadComboAcct_Code.Text, bpnatureid, ddlSupplier.SelectedItem.Value, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), "0", totalcrdb.ToString(), txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff"), "0", "0", "0", "Inv_InventoryInDetail");

        }
    }

    protected void btnShowJV_Click(object sender, EventArgs e)
    {
        userid = Request.QueryString["UserID"];
        UserGrpID = Request.QueryString["UsergrpID"];
        FormID = Request.QueryString["FormID"];
        fiscal = Request.QueryString["fiscaly"];
        menuid = Request.QueryString["Menuid"];
        string ResponseUrl = "frm_JV_Diplay.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "&Menuid=" + menuid + "&VoucherNo=" + txtDocNo.Text + "&bilno=" + txtbillNo.Text + "&billdate=" + txtBilldate.Text + "&shopname=" + txtShopOffName.Text + "";
        Response.Redirect(ResponseUrl);
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

    //ddltransfer_SelectedIndexChanged
    protected void ddltransfer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDocName.SelectedIndex != 0)
        {

            DataSet ds = dml.Find("select LocationTo,LocationFrom,CostCenter,CostCenter2 from Inv_InventoryInMaster where DocumentNo='" + ddlDocName.SelectedItem.Value + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlCostCenterTo.ClearSelection();
                ddlCostCentrFrom.ClearSelection();
                ddlLocationFrom.ClearSelection();
                ddlLocationTo.ClearSelection();


                ddlCostCenterTo.Items.FindByValue(ds.Tables[0].Rows[0]["CostCenter"].ToString()).Selected = true;
                ddlCostCentrFrom.Items.FindByValue(ds.Tables[0].Rows[0]["CostCenter2"].ToString()).Selected = true;
                ddlLocationFrom.Items.FindByValue(ds.Tables[0].Rows[0]["LocationFrom"].ToString()).Selected = true;
                ddlLocationTo.Items.FindByValue(ds.Tables[0].Rows[0]["LocationTo"].ToString()).Selected = true;



                DataTable dtup = new DataTable();
                dtup.Columns.AddRange(new DataColumn[17] { new DataColumn("Sno"), new DataColumn("DrAccountCode"), new DataColumn("PONO1"), new DataColumn("ItemSubHeadName"), new DataColumn("Description"), new DataColumn("UOMName"), new DataColumn("pobal"), new DataColumn("pobal1"), new DataColumn("Rate"), new DataColumn("Value"), new DataColumn("CostCenterName"), new DataColumn("LocName"), new DataColumn("Project"), new DataColumn("Warrentee"), new DataColumn("uomName2"), new DataColumn("Qty2"), new DataColumn("Rate2") });


                //select * from view_StockUpdate
                //select ItemSubHeadName , Description,CostCenterName,UOMName from view_StockUpdate
                DataSet ds_detail = dml.Find("select * from ViewInvINDetail_FED where Sno_Master = '" + ddlDocName.SelectedItem.Value + "'");
                if (ds_detail.Tables[0].Rows.Count > 0)
                {
                    ViewState["Customers1"] = ds_detail.Tables[0];
                    Div7.Visible = true;
                    GridView10.DataSource = ds_detail.Tables[0];
                    GridView10.DataBind();
                }
                Div1.Visible = false;
                Div2.Visible = false;
                Div3.Visible = false;
                Div4.Visible = false;
                Div5.Visible = false;
                Div6.Visible = false;
                PopulateGridview_UpGrid10();



            }


        }
        else
        {

        }

    }

    public void datashow()
    {
        Div2.Visible = true;
        Div5.Visible = false;
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[20]
        {
             new DataColumn("AcctCode"),
              new DataColumn("ItemSubHead"),
                new DataColumn("ItemMaster"),
                new DataColumn("UOM"),
                new DataColumn("DCQty"),
                new DataColumn("Rate"),
                new DataColumn("Value"),
                new DataColumn("Gst"),
                new DataColumn("AddGst"),
                 new DataColumn("Totalgst"),
                new DataColumn("TotalAmount"),
                new DataColumn("BalanceAmount"),
                new DataColumn("BalanceTax"),
                new DataColumn("dc_po_grn"),
                new DataColumn("CostCenter"),
                new DataColumn("Location"),
                new DataColumn("Project"),
                new DataColumn("Warrantee"),
                new DataColumn("Qty2"),
                new DataColumn("Rate2")
        });


        ViewState["DirectDetail"] = dt;


        this.PopulateGridview();
        DropDownList ddlsub = GridView6.FooterRow.FindControl("ddlItemSubHeadFooter") as DropDownList;
        dml.dropdownsql(ddlsub, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");

        DropDownList ddlmaster = GridView6.FooterRow.FindControl("ddlItemMasterFooter") as DropDownList;
        dml.dropdownsql(ddlmaster, "SET_ItemMaster", "Description", "ItemID");

        DropDownList ddluom = GridView6.FooterRow.FindControl("ddlUOMFooter") as DropDownList;
        dml.dropdownsql(ddluom, "SET_UnitofMeasure", "UOMName", "UOMID");

        DropDownList ddlsupFooter = GridView6.FooterRow.FindControl("lblCostCenterFooter") as DropDownList;
        dml.dropdownsql(ddlsupFooter, "SET_CostCenter", "CostCenterName", "CostCenterID");

        //   DropDownList ddluom2 = GridView6.FooterRow.FindControl("ddlUOM2Footer") as DropDownList;
        // dml.dropdownsql(ddluom2, "SET_UnitofMeasure", "UOMName", "UOMID");

        DropDownList ddlLocation = GridView6.FooterRow.FindControl("ddlLocationFooter") as DropDownList;
        dml.dropdownsql(ddlLocation, "SET_Department", "DepartmentName", "DepartmentID", "IsWarehouse", "1");

        DropDownList ddlAddGSTFooter = GridView6.FooterRow.FindControl("ddlAddGSTFooter") as DropDownList;
        dml.dropdownsqlwithquery(ddlAddGSTFooter, "SELECT Percentage, TaxDescription +'(' + CONVERT(VARCHAR, Percentage) + ')' as perc from SET_Tax", "perc", "Percentage");

        DropDownList ddlGSTFooter = GridView6.FooterRow.FindControl("ddlGST6") as DropDownList;
        dml.dropdownsqlwithquery(ddlGSTFooter, "SELECT Percentage, TaxDescription +'(' + CONVERT(VARCHAR, Percentage) + ')' as perc from SET_Tax", "perc", "Percentage");


        Div2.Visible = true;
        Div4.Visible = false;

    }


    public void datashow1(GridView gv)
    {

        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[8]
        {
                new DataColumn("Sno"),
                new DataColumn("Accode"),
                new DataColumn("AccountCode"),
                new DataColumn("AccntDesc"),
                new DataColumn("Narration"),
                new DataColumn("Rowsno_RowsAll"),
                new DataColumn("ChangeToCost"),
                new DataColumn("Amount")

        });


        ViewState["Customers"] = dt;


        this.PopulateGridview1(gv);



    }
    protected void chkamount_CheckedChanged(object sender, EventArgs e)
    {

        CheckBox chk = (CheckBox)GridView6.HeaderRow.FindControl("chkamount");
        TextBox txtQty = (TextBox)GridView6.FooterRow.FindControl("txtDCQtyFooter");
        TextBox txtrate = (TextBox)GridView6.FooterRow.FindControl("txtRateFooter");
        TextBox txtValue = (TextBox)GridView6.FooterRow.FindControl("lblValueFooter");

        if (chk.Checked == true)
        {
            txtQty.Text = "0";
            txtrate.Text = "0";
            txtQty.Enabled = false;
            txtrate.Enabled = false;
            txtValue.Enabled = true;
        }
        else
        {

            txtQty.Enabled = true;
            txtrate.Enabled = true;
            txtValue.Enabled = false;
        }
    }


    protected void GridView10_RowDataBound(object sender, GridViewRowEventArgs e)
    {       
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lblRowNumber") as Label).Text = (e.Row.RowIndex + 1).ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
            DropDownList ddl = e.Row.FindControl("ddladdGST") as DropDownList;
           dml.dropdownsqlwithquery(ddl, "SELECT TaxTypeID, TaxDescription +'(' + CONVERT(VARCHAR, Percentage) + ')' as perc from SET_Tax where TaxType = 'GST'", "perc", "TaxTypeID");

            DropDownList ddlgst = e.Row.FindControl("ddlGST") as DropDownList;
            dml.dropdownsqlwithquery(ddlgst, "SELECT TaxTypeID, TaxDescription +'(' + CONVERT(VARCHAR, Percentage) + ')' as perc from SET_Tax where TaxType = 'GST'", "perc", "TaxTypeID");


            Label lblValue = e.Row.FindControl("lblValue") as Label;
            (e.Row.FindControl("lblBalanceAmtAmount") as Label).Text = lblValue.Text;
            decimal ttlValue = Convert.ToDecimal(lbltotalvalue.Text) + Convert.ToDecimal(lblValue.Text);

           float totalamtval = float.Parse(lblValue.Text);


            lbltotalvalue.Text = ttlValue.ToString();
            
            txtBillBal.Text = lbltotalvalue.Text;
            //
            Label lblTotalAmount = e.Row.FindControl("lblTotalAmount") as Label;
            lblTotalAmount.Text = lbltotalvalue.Text;


            Label value = e.Row.FindControl("lblValue") as Label;

            string ddlgst1 = "0";
            DropDownList gsttool = e.Row.FindControl("ddlGST") as DropDownList;

            if (gsttool.SelectedItem.Text != "Please select...")
            {
                DataSet dsgst = dml.Find("SELECT SUBSTRING('" + gsttool.SelectedItem.Text + "', CHARINDEX('(', '" + gsttool.SelectedItem.Text + "') + 1, CHARINDEX(')', '" + gsttool.SelectedItem.Text + "') - CHARINDEX('(', '" + gsttool.SelectedItem.Text + "') - 1) AS output");
                if (dsgst.Tables[0].Rows.Count > 0)
                {
                    ddlgst1 = dsgst.Tables[0].Rows[0]["output"].ToString();
                }


                float gstval = float.Parse(value.Text) * (float.Parse(ddlgst1) / 100);
                gsttool.ToolTip = gstval.ToString();
            }


            string ddladdgst = "0";
            DropDownList gstaddtool = e.Row.FindControl("ddladdGST") as DropDownList;
            if (gstaddtool.SelectedItem.Text != "Please select...")
            {
                DataSet dsaddgst = dml.Find("SELECT SUBSTRING('" + gstaddtool.SelectedItem.Text + "', CHARINDEX('(', '" + gstaddtool.SelectedItem.Text + "') + 1, CHARINDEX(')', '" + gstaddtool.SelectedItem.Text + "') - CHARINDEX('(', '" + gstaddtool.SelectedItem.Text + "') - 1) AS output");
                if (dsaddgst.Tables[0].Rows.Count > 0)
                {
                    ddladdgst = dsaddgst.Tables[0].Rows[0]["output"].ToString();
                }

                float gstaddval = float.Parse(value.Text) * (float.Parse(ddladdgst) / 100);
                gstaddtool.ToolTip = gstaddval.ToString();
            }




            string f = (e.Row.FindControl("lblValue") as Label).Text;
            string flblGSTAmount = (e.Row.FindControl("lblGSTAmount") as Label).Text;
            string flblTotalAmount = (e.Row.FindControl("lblTotalAmount") as Label).Text;
            float faflblGSTAmount = float.Parse(flblGSTAmount);
            float FaflblTotalAmount = float.Parse(flblTotalAmount);
            float fa = float.Parse(f);
            string displ = inv.displaydigit(branchId().ToString());
            if (displ == "0")
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0");
                (e.Row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0");
                (e.Row.FindControl("lblTotalAmount") as Label).Text = totalamtval.ToString("0");
            }

            else if (displ == "1")
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.0");
                (e.Row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.0");
                (e.Row.FindControl("lblTotalAmount") as Label).Text = totalamtval.ToString("0.0");
            }
            else if (displ == "2")
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.00");
                (e.Row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.00");
                (e.Row.FindControl("lblTotalAmount") as Label).Text = totalamtval.ToString("0.00");
            }
            else if (displ == "3")
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.000");
                (e.Row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.000");
                (e.Row.FindControl("lblTotalAmount") as Label).Text = totalamtval.ToString("0.000");
            }
            else if (displ == "4")
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.0000");
                (e.Row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.0000");
                (e.Row.FindControl("lblTotalAmount") as Label).Text = totalamtval.ToString("0.0000");
            }
            else if (displ == "5")
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.00000");
                (e.Row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.00000");
                (e.Row.FindControl("lblTotalAmount") as Label).Text = totalamtval.ToString("0.00000");
            }
            else if (displ == "6")
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.000000");
                (e.Row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.000000");
                (e.Row.FindControl("lblTotalAmount") as Label).Text = totalamtval.ToString("0.000000");
            }
            else if (displ == "7")
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.0000000");
                (e.Row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.0000000");
                (e.Row.FindControl("lblTotalAmount") as Label).Text = totalamtval.ToString("0.0000000");
            }
            else if (displ == "8")
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.00000000");
                (e.Row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.00000000");
                (e.Row.FindControl("lblTotalAmount") as Label).Text = totalamtval.ToString("0.00000000");
            }
            else
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0");
                (e.Row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0");
                (e.Row.FindControl("lblTotalAmount") as Label).Text = totalamtval.ToString("0");
            }

        }
    }

    protected void chkNormalGRN_CheckedChanged1(object sender, EventArgs e)
    {
        if (chkNormalGRN.Checked)
        {
            Div2.Visible = false;
            Div7.Visible = true;
            ddlPO_GRN.Visible = true;
            dml.dropdownsqlwithquery(ddlPO_GRN, "select sno , DocumentNo from Inv_InventoryInMaster where DocId = '8' and Status = 1 and IsActive = 1 and GocID = '"+gocid()+"' and CompId = '"+compid()+"' and branchid = '"+branchId()+"' and FiscalYearID = '"+FiscalYear()+"'", "DocumentNo", "sno");
            ddlPO_GRN_SelectedIndexChanged(sender, e);
        }
    }

    protected void chkPO_CheckedChanged(object sender, EventArgs e)
    {
        if (chkPO.Checked)
        {
            Div2.Visible = false;
            ddlPO_GRN.Enabled = true;
            ddlPO_GRN.Visible = true;
            dml.dropdownsqlwithquery(ddlPO_GRN, "select [DocumentNo.] as docno,Sno from Set_PurcahseOrderMaster where CompId = '" + compid() + "' and BranchId= '" + branchId() + "' and GocID = '" + gocid() + "' and FiscalYearID = '" + FiscalYear() + "' and IsActive= 1", "docno", "Sno");
        }
    }

    protected void ddlPO_GRN_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (chkNormalGRN.Checked == true)
        {
            lbltotalvalue.Text = "0";

            if (ddlPO_GRN.SelectedIndex != 0)
            {
                DataSet ds1 = dml.Find("select BPartnerID,BillDate,BillNo,DeliveryChallan,DeliveryChallanDate,InwardGatePass,Shop_OfficeName,WeighbridgeNo,NetWeight from Inv_InventoryInMaster where Sno = '" + ddlPO_GRN.SelectedItem.Value + "' and Status = 1 and IsActive = 1 and GocID = '" + gocid() + "' and CompId = '" + compid() + "' and branchid = '" + branchId() + "' and FiscalYearID = '" + FiscalYear() + "'");
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    ddlSupplier.ClearSelection();
                    ddlSupplier.Items.FindByValue(ds1.Tables[0].Rows[0]["BPartnerID"].ToString()).Selected = true;
                    txtbillNo.Text = ds1.Tables[0].Rows[0]["BillNo"].ToString();
                    txtBilldate.Text = ds1.Tables[0].Rows[0]["BillDate"].ToString();
                    txtDeliveryChallan.Text = ds1.Tables[0].Rows[0]["DeliveryChallan"].ToString();
                    txtDeliveryChallanDate.Text = ds1.Tables[0].Rows[0]["DeliveryChallanDate"].ToString();
                    txtInwardGatePassNo.Text = ds1.Tables[0].Rows[0]["InwardGatePass"].ToString();
                    txtShopOffName.Text = ds1.Tables[0].Rows[0]["Shop_OfficeName"].ToString();
                    txtWeighbridgeNo.Text = ds1.Tables[0].Rows[0]["WeighbridgeNo"].ToString();
                    txtNetWeight.Text = ds1.Tables[0].Rows[0]["NetWeight"].ToString();

                    dml.dateConvert(txtDeliveryChallanDate);
                    dml.dateConvert(txtBilldate);
                    ddlSupplier_SelectedIndexChanged(sender, e);
                }

                DataSet ds = dml.Find("select * from Inv_PurchseMaster where  Status = 1 and IsActive = 1 and GocID = '" + gocid() + "' and CompId = '" + compid() + "' and branchid = '" + branchId() + "' and FiscalYearID = '" + FiscalYear() + "'");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dtup = new DataTable();
                    dtup.Columns.AddRange(new DataColumn[17] { new DataColumn("Sno"), new DataColumn("DrAccountCode"), new DataColumn("PONO1"), new DataColumn("ItemSubHeadName"), new DataColumn("Description"), new DataColumn("UOMName"), new DataColumn("pobal"), new DataColumn("pobal1"), new DataColumn("Rate"), new DataColumn("Value"), new DataColumn("CostCenterName"), new DataColumn("LocName"), new DataColumn("Project"), new DataColumn("Warrentee"), new DataColumn("uomName2"), new DataColumn("Qty2"), new DataColumn("Rate2") });


                    //select * from view_StockUpdate
                    //select ItemSubHeadName , Description,CostCenterName,UOMName from view_StockUpdate
                    DataSet ds_detail = dml.Find("select * from ViewInvINDetail_FED where Sno_Master = '" + ddlPO_GRN.SelectedItem.Value + "'");
                    if (ds_detail.Tables[0].Rows.Count > 0)
                    {
                        ViewState["Customers1"] = ds_detail.Tables[0];
                        Div7.Visible = true;
                        GridView10.DataSource = ds_detail.Tables[0];
                        GridView10.DataBind();
                    }
                    Div1.Visible = false;
                    Div2.Visible = false;
                    Div3.Visible = false;
                    Div4.Visible = false;
                    Div5.Visible = false;
                    Div6.Visible = false;
                   // PopulateGridview_UpGrid10();



                }

            }
        }
        else if (chkPO.Checked == true)
        {
            DataSet ds = dml.Find("select * from Set_PurchaseOrderDetail where FiscalYearID = 4 and CompId = 1  and  GocID = 1 and BranchId = 5");
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtup = new DataTable();
                dtup.Columns.AddRange(new DataColumn[17] { new DataColumn("Sno"), new DataColumn("DrAccountCode"), new DataColumn("PONO1"), new DataColumn("ItemSubHeadName"), new DataColumn("Description"), new DataColumn("UOMName"), new DataColumn("pobal"), new DataColumn("pobal1"), new DataColumn("Rate"), new DataColumn("Value"), new DataColumn("CostCenterName"), new DataColumn("LocName"), new DataColumn("Project"), new DataColumn("Warrentee"), new DataColumn("uomName2"), new DataColumn("Qty2"), new DataColumn("Rate2") });


                //select * from view_StockUpdate
                //select ItemSubHeadName , Description,CostCenterName,UOMName from view_StockUpdate
                DataSet ds_detail = dml.Find("select * from ViewPODetail_FED where FiscalYearID = 4 and CompId = 1  and  GocID = 1 and BranchId = 5");
                if (ds_detail.Tables[0].Rows.Count > 0)
                {
                    ViewState["Customers"] = ds_detail.Tables[0];
                    Div4.Visible = true;
                    GridView7.DataSource = ds_detail.Tables[0];
                    GridView7.DataBind();
                }
                Div1.Visible = false;
                Div2.Visible = false;
                Div3.Visible = false;
                Div7.Visible = false;
                Div5.Visible = false;
                Div6.Visible = false;
                PopulateGridview1(GridView7);


            }
        }
        else
        {

        }

    }


    protected void grd_Add_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("AddNew"))
            {
                foreach (GridViewRow g in grd_Add.Rows)
                {

                    string Accode = (grd_Add.FooterRow.FindControl("RadComboAcctCodeFooter") as RadComboBox).Text;
                    string AcDesc = (grd_Add.FooterRow.FindControl("txtFooterAccntDesc") as TextBox).Text;
                    string Narration = (grd_Add.FooterRow.FindControl("txtFooterNarration") as TextBox).Text;
                    string Rowno_RowAll = (grd_Add.FooterRow.FindControl("txtFooterRowsno_RowsAll") as TextBox).Text;
                    string ChangeToCost = (grd_Add.FooterRow.FindControl("txtFooterChangeToCost") as TextBox).Text;
                    string Amount = (grd_Add.FooterRow.FindControl("txtFooterAmount") as TextBox).Text;



                    Label1.Text = "";
                    DataTable dt = (DataTable)ViewState["Add_RED"];

                    if (ViewState["flag"].ToString() == "true")
                    {
                        //dt.Rows[0].Delete();
                        ViewState["flag"] = "false";
                    }
                    float billbal;
                    if(txtBillBal.Text == "")
                    {
                        billbal = 0;
                    }
                    else
                    {
                        billbal = float.Parse(txtBillBal.Text) + float.Parse(Amount);
                    }

                    txtBillBal.Text = billbal.ToString();

                    dt.Rows.Add(Accode, AcDesc, Narration, Rowno_RowAll, ChangeToCost, Amount);

                    ViewState["Add_RED"] = dt;
                    this.PopulateGridview2(grd_Add, "Add_RED");




                }
            }
        }

        catch (Exception ex)
        {

        }
    }

    public void add_red(string str)
    {
        Div2.Visible = false;
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[6]
        {
                new DataColumn("Accode"),
                new DataColumn("AccntDesc"),
                new DataColumn("Narration"),
                new DataColumn("Rowsno_RowsAll"),
                new DataColumn("ChangeToCost"),
                new DataColumn("Amount"),

        });


        ViewState[str] = dt;

    }

    protected void grd_Deduction_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("AddNew"))
            {
                foreach (GridViewRow g in grd_Deduction.Rows)
                {

                    string Accode = (grd_Deduction.FooterRow.FindControl("RadComboAcctDecCodeFooter") as RadComboBox).Text;
                    string AcDesc = (grd_Deduction.FooterRow.FindControl("txtAccntDescFooterDed") as TextBox).Text;
                    string Narration = (grd_Deduction.FooterRow.FindControl("txtFooterNarrationDed") as TextBox).Text;
                    string Rowno_RowAll = (grd_Deduction.FooterRow.FindControl("txtFooterRowsno_RowsAllDed") as TextBox).Text;
                    string ChangeToCost = (grd_Deduction.FooterRow.FindControl("txtFooterChangeToCostDed") as TextBox).Text;
                    string Amount = (grd_Deduction.FooterRow.FindControl("txtFooterAmountDed") as TextBox).Text;

                    Label1.Text = "";
                    DataTable dt = (DataTable)ViewState["ADD_DED"];

                    if (ViewState["flag"].ToString() == "true")
                    {
                        //dt.Rows[0].Delete();
                        ViewState["flag"] = "false";
                    }

                    dt.Rows.Add(Accode, AcDesc, Narration, Rowno_RowAll, ChangeToCost, Amount);

                    ViewState["ADD_DED"] = dt;
                    this.PopulateGridview2(grd_Deduction, "ADD_DED");


                }
            }
        }

        catch (Exception ex)
        {

        }
    }

    protected void RadComboAcctCodeFooter_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        RadComboBox RadComboAcctCodeFooter = grd_Add.FooterRow.FindControl("RadComboAcctCodeFooter") as RadComboBox;
        TextBox txtFooterAccntDesc = grd_Add.FooterRow.FindControl("txtFooterAccntDesc") as TextBox;


        if (RadComboAcctCodeFooter.SelectedValue == "")
        {

        }
        else
        {

            RadComboAcctCodeFooter.ToolTip = RadComboAcctCodeFooter.SelectedValue.Split(new char[] { ':' })[1];
            txtFooterAccntDesc.Text = RadComboAcctCodeFooter.SelectedValue.Split(new char[] { ':' })[1];
            // tab_2.Attributes.Add("Class", "tab-pane active");
        }
    }

    protected void RadComboAcctDecCodeFooter_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        RadComboBox RadComboAcctCodeFooter = grd_Deduction.FooterRow.FindControl("RadComboAcctDecCodeFooter") as RadComboBox;
        TextBox txtFooterAccntDesc = grd_Deduction.FooterRow.FindControl("txtAccntDescFooterDed") as TextBox;


        if (RadComboAcctCodeFooter.SelectedValue == "")
        {

        }
        else
        {

            RadComboAcctCodeFooter.ToolTip = RadComboAcctCodeFooter.SelectedValue.Split(new char[] { ':' })[1];
            txtFooterAccntDesc.Text = RadComboAcctCodeFooter.SelectedValue.Split(new char[] { ':' })[1];
            // tab_2.Attributes.Add("Class", "tab-pane active");
        }
    }

    protected void chkDirectGRN_CheckedChanged1(object sender, EventArgs e)
    {
        if (chkDirectGRN.Checked == true)
        {
            Div2.Visible = true;
            Div7.Visible = false;
            ddlPO_GRN.Visible = false;
        }
    }

    public void DirectGRNsave()
    {
        string trantype = "", doctype = "0";
        DataSet dsCRDR = dml.Find("select  Tran_Type from SET_Acct_Type where Acct_Type_Id in ( SELECT Acct_Type_ID from SET_COA_detail where Acct_Code = '" + RadComboAcct_Code.Text + "');select DocTypeId from SET_Documents where DocID = '" + ddlDocName.SelectedItem.Value + "';");
        if (dsCRDR.Tables[0].Rows.Count > 0)
        {
            trantype = dsCRDR.Tables[0].Rows[0]["Tran_Type"].ToString();
            doctype = dsCRDR.Tables[1].Rows[0]["DocTypeId"].ToString();

        }

        string lt, lf, ct, cf;
        if (ddlLocationFrom.SelectedIndex != 0)
        {
            lf = ddlLocationFrom.SelectedItem.Value;
        }
        else
        {
            lf = "0";
        }
        if (ddlLocationTo.SelectedIndex != 0)
        {
            lt = ddlLocationTo.SelectedItem.Value;
        }
        else
        {
            lt = "0";
        }
        if (ddlCostCentrFrom.SelectedIndex != 0)
        {
            cf = ddlCostCentrFrom.SelectedItem.Value;
        }
        else
        {
            cf = "0";
        }
        if (ddlCostCenterTo.SelectedIndex != 0)
        {
            ct = ddlCostCenterTo.SelectedItem.Value;
        }
        else
        {
            ct = "0";
        }
        int chk = 0;
        if (chkActive.Checked == true)
        {
            chk = 1;
        }
        //string query = "INSERT INTO [Inv_InventoryInMaster] ([DocId], [EntryDate], [DocumentNo], [BPartnerID], [DirectPO_NoramlPO], [DocumentAuthority], [DeliveryChallan], [DeliveryChallanDate], [InwardGatePass], [LocationFrom], [LocationTo], [CostCenter], [CostCenter2], [Status], [Remarks], [Shop_OfficeName], [BillNo], [BillDate], [WeighbridgeNo], [NetWeight], [DrAccountCode], [CrAccountCode], [GLReferNo], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [Record_Deleted], [ReferNo], [FWD_ID]) VALUES"+
        //     "('"+ddlDocName.SelectedItem.Value+"','"+dml.dateconvertforinsert(txtEntryDate)+"', '"+txtDocNo.Text+"', '"+ddlSupplier.SelectedItem.Value+"', NULL, '"+ddlDocAuth.SelectedItem.Value+"', '"+txtDeliveryChallan.Text+"', '"+txtInwardGatePassNo.Text+"', '"+ddlLocationTo.sl+"', '1', '1', '211', '211', '1', 'testing', NULL, NULL, NULL, NULL, NULL, '1-03-01-0006', NULL, '37', '1', '1', '1', '5', '4', 'Fahad Siddiqui', '2021-02-01 02:55:57.9286', NULL, NULL, '0', NULL, '37');";

        DataSet ds = dml.Find("INSERT INTO Inv_InventoryInMaster ([DocId], [DocType], [EntryDate], [DocumentNo], [BPartnerID], [DirectPO_NoramlPO], [DocumentAuthority], [DeliveryChallan], [DeliveryChallanDate], [InwardGatePass], [LocationFrom], [LocationTo], [CostCenter], [CostCenter2], [Status], [Remarks], [Shop_OfficeName], [BillNo], [BillDate], [WeighbridgeNo], [NetWeight], [DrAccountCode], [CrAccountCode], [GLReferNo], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted]) VALUES ('" + ddlDocName.SelectedItem.Value + "', '" + doctype + "', '" + txtEntryDate.Text + "', '" + required_generateforIns() + "', '" + ddlSupplier.SelectedItem.Value + "', NULL, '" + ddlDocAuth.SelectedItem.Value + "', '" + txtDeliveryChallan.Text + "', '" + txtDeliveryChallanDate.Text + "', '" + txtInwardGatePassNo.Text + "', '" + lf + "', '" + lt + "', '" + cf + "', '" + ct + "', '" + ddlStatus.SelectedItem.Value + "','" + txtRemarks.Text + "', '" + txtShopOffName.Text + "', '" + txtbillNo.Text + "', '" + txtBilldate.Text + "', '" + txtWeighbridgeNo.Text + "', '" + txtNetWeight.Text + "', '" + RadComboAcct_Code.Text + "', NULL, NULL, '" + chk + "', '" + gocid() + "', '" + compid() + "', '" + branchId() + "', '" + FiscalYear() + "', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0');  SELECT * FROM Inv_InventoryInMaster WHERE Sno = SCOPE_IDENTITY()");


        gl.GlentrySummaryDetail(show_username(), DateTime.Now.ToString(), "0", txtDocNo.Text);


    }

    protected void txtGSTFooter_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;
        float taxamount;
        string tax = (row.FindControl("txtGSTFooter") as TextBox).Text;
        string valuelbl = (row.FindControl("lblValueFooter") as TextBox).Text;
        TextBox lblTotalAmountFooter = (row.FindControl("lblTotalAmountFooter") as TextBox);
        if (valuelbl.Length > 0 && tax.Length > 0)
        {
            taxamount = float.Parse(valuelbl) * (float.Parse(tax) / 100);
        }
        else
        {
            if (tax.Length > 0)
            {
                taxamount = (float.Parse(tax) / 100);
            }
            else
            {
                taxamount = float.Parse(valuelbl);
            }
        }

        // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
        lbltaxamount.Text = taxamount.ToString();
        float toalamt = float.Parse(lbltaxamount.Text) + float.Parse(lbltaxamount.Text);
        lblTotalAmountFooter.Text = toalamt.ToString();
        txtBillBal.Text = toalamt.ToString();
    }

    protected void ddlAddGSTFooter_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((DropDownList)sender).NamingContainer;
        float taxAddamount;
        string ddlgst = "";
        DropDownList ddlAddGSTFooter = (row.FindControl("ddlAddGSTFooter") as DropDownList);
        TextBox lblTotalAmountFooter = (row.FindControl("lblTotalAmountFooter") as TextBox);
        Label lblGSTAmountFooter = (row.FindControl("lblGSTAmountFooter") as Label);
        //lblGSTAmountFooter
        if (ddlAddGSTFooter.SelectedIndex != 0)
        {
            DataSet dsgst = dml.Find("SELECT SUBSTRING('" + ddlAddGSTFooter.SelectedItem.Text + "', CHARINDEX('(', '" + ddlAddGSTFooter.SelectedItem.Text + "') + 1, CHARINDEX(')', '" + ddlAddGSTFooter.SelectedItem.Text + "') - CHARINDEX('(', '" + ddlAddGSTFooter.SelectedItem.Text + "') - 1) AS output");
            if (dsgst.Tables[0].Rows.Count > 0)
            {
                ddlgst = dsgst.Tables[0].Rows[0]["output"].ToString();
            }
            string valuelbl = (row.FindControl("lblValueFooter") as TextBox).Text;
            if (valuelbl.Length > 0 && ddlAddGSTFooter.SelectedIndex > 0)
            {
                taxAddamount = float.Parse(valuelbl) * (float.Parse(ddlgst) / 100);
            }
            else
            {
                if (ddlAddGSTFooter.SelectedIndex > 0)
                {
                    taxAddamount = (float.Parse(ddlgst) / 100);
                }
                else
                {
                    taxAddamount = float.Parse(valuelbl);
                }
            }

            // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
            lblAddtaxamount.Text = taxAddamount.ToString();

            float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);
            lblTotalAmountFooter.Text = toalamt.ToString();
            (row.FindControl("lblBalanceFooter") as TextBox).Text = lbltaxamount.Text;
            lblGSTAmountFooter.Text = toalamt.ToString();
            dislaydigit_label(row, "lblGSTAmountFooter", toalamt);
            txtBillBal.Text = lbltaxamount.Text;
            (row.FindControl("lblBalanceTaxFooter") as TextBox).Text = toalamt.ToString();
            float balamount = toalamt + float.Parse(valuelbl);
            (row.FindControl("lblBalanceFooter") as TextBox).Text = balamount.ToString();
            txtBillBal.Text = balamount.ToString();
            lblTotalAmountFooter.Text = balamount.ToString();

            dislaydigit_textbox(txtBillBal, balamount);
            dislaydigit_fortextbox(row, "lblTotalAmountFooter", balamount);
            dislaydigit_fortextbox(row, "lblBalanceTaxFooter", toalamt);
            dislaydigit_fortextbox(row, "lblBalanceFooter", balamount);
            

        }
        else
        {
            string valuelbl = (row.FindControl("lblValueFooter") as TextBox).Text;
            if (valuelbl.Length > 0 && ddlAddGSTFooter.SelectedIndex == 0)
            {
                taxAddamount = float.Parse(valuelbl) * 0;
            }
            else
            {
                if (ddlAddGSTFooter.SelectedIndex > 0)
                {
                    taxAddamount = (float.Parse(ddlgst) / 100);
                }
                else
                {
                    taxAddamount = float.Parse(valuelbl);
                }
            }

            // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
            lblAddtaxamount.Text = taxAddamount.ToString();

            float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);
            lblTotalAmountFooter.Text = toalamt.ToString();

           
            (row.FindControl("lblBalanceFooter") as TextBox).Text = lbltaxamount.Text;
            txtBillBal.Text = lbltaxamount.Text;
            //  (row.FindControl("lblBalanceTaxFooter") as TextBox).Text = taxAddamount.ToString();
            float balamount = toalamt + float.Parse(valuelbl);
            (row.FindControl("lblBalanceFooter") as TextBox).Text = balamount.ToString();
            txtBillBal.Text = balamount.ToString();
            lblTotalAmountFooter.Text = balamount.ToString();

            dislaydigit_fortextbox(row, "lblTotalAmountFooter", balamount);
            dislaydigit_fortextbox(row, "lblBalanceTaxFooter", toalamt);
            dislaydigit_fortextbox(row, "lblBalanceFooter", balamount);

        }
    }

    public void btnApply_Click(string ids)
    {
       foreach (GridViewRow g in grd_Add.Rows)
        {
            
            Label lblAccountCode = (Label)g.FindControl("lblAccountCode") as Label;
            Label lblItemAccntDesc = (Label)g.FindControl("lblItemAccntDesc");
            Label lblItemNarration = (Label)g.FindControl("lblItemNarration");
            Label lblItmeChangeToCost = (Label)g.FindControl("lblItmeChangeToCost");
            Label lblItemRowsno_RowsAll = (Label)g.FindControl("lblItemRowsno_RowsAll");
            Label lblItemAmount = (Label)g.FindControl("lblItemAmount");

            string invpurchaseMasterID = "0";
            string dbacctcode = "";
            if (lblAccountCode.Text != "")
            {
                DataSet dsinsertdetail = dml.Find("INSERT INTO [Fin_PurchaseOthers] ([AccountCode], [AccntDesc], [Narration], [Rowsno_RowsAll], [ChangeToCost], [Amount], [SnoMaster], [DrAcCode], [CrAcCode], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted]) VALUES ('" + lblAccountCode.Text + "', '" + lblItemAccntDesc.Text + "', '" + lblItemNarration.Text + "', '" + lblItemRowsno_RowsAll.Text + "', '" + lblItmeChangeToCost.Text + "', '" + lblItemAmount.Text + "','" + ids + "', '" + dbacctcode + "', NULL, '" + gocid() + "', '" + compid() + "', '" + branchId() + "', '" + FiscalYear() + "', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0');");
                //INSERT INTO [HRERPSys].[dbo].[Act_GL] ([Sno], [DocId], [DocDescription], [MasterSno], [EntryDate], [VoucherNo], [AccountCode], [BPNatureID], [BPartnerId], [Name], [ChqNo], [ChqDate], [InvoiceNo], [InvoiceDate], [DrAmount], [CrAmount], [Narration], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [Record_Deleted], [DocType]) VALUES ('112', '18', 'PURCHASE INVOICE', '43', '2021-04-12', '18-0421-00016', '2-02-02-0006', '', '0', 'Mr. Arif  ALI', NULL, NULL, '123', '2021-04-12', '.00000', '353.60000', 'testing', '1', '1', '5', '4', 'Fahad Siddiqui', '2021-04-12 10:12:18.0000', NULL, NULL, '0', '9');
                DataSet dsinsertdetailGL = dml.Find("INSERT INTO [HRERPSys].[dbo].[Act_GL] ([DocId], "
                              + "[DocDescription], [MasterSno], [EntryDate], [VoucherNo], [AccountCode], "
                              + " [DrAmount], [CrAmount], [Narration], [GocID], [CompId], "
                              + "[BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], "
                              + " [Record_Deleted]) VALUES"
                              + " ('" + ddlDocName.SelectedItem.Value + "', '" + ddlDocName.SelectedItem.Text + "',"
                              + " '" + ids + "', '" + txtEntryDate.Text + "', '" + txtDocNo.Text + "', '" + lblAccountCode.Text + "', '" + lblItemAmount.Text + "', '0', '" + lblItemNarration.Text + "',"
                              + " " + gocid() + ", " + compid() + ", " + branchId() + ", " + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.hhhh") + "', '0')");


                
            }
        }

    }

    public void btnDec_Apply_Click(string ids)
    {
        foreach (GridViewRow g in grd_Deduction.Rows)
        {
            
            Label lblAccountCode = (Label)g.FindControl("lblAccountCode");
            Label lblItemAccntDesc = (Label)g.FindControl("lblitemAccntDescDed");
            Label lblItemNarration = (Label)g.FindControl("lblItemNarrationDed");
            Label lblItmeChangeToCost = (Label)g.FindControl("lblItemChangeToCostDed");
            Label lblItemRowsno_RowsAll = (Label)g.FindControl("lblItemRowsno_RowsAllDed");
            Label lblItemAmount = (Label)g.FindControl("lblItemAmountDed");
            
            string invpurchaseMasterID = "0";
            string dbacctcode = "";
            if (lblAccountCode.Text != "")
            {
                DataSet dsinsertdetail = dml.Find("INSERT INTO [Fin_PurchaseOthers] ([AccountCode], [AccntDesc], [Narration], [Rowsno_RowsAll], [ChangeToCost], [Amount], [SnoMaster], [DrAcCode], [CrAcCode], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted]) VALUES ('" + lblAccountCode.Text + "', '" + lblItemAccntDesc.Text + "', '" + lblItemNarration.Text + "', '" + lblItemRowsno_RowsAll.Text + "', '" + lblItmeChangeToCost.Text + "', '" + lblItemAmount.Text + "','" + invpurchaseMasterID + "', NULL, '" + dbacctcode + "', " + gocid() + ", " + compid() + ", " + branchId() + ", " + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0');");

                DataSet dsinsertdetailGL = dml.Find("INSERT INTO [HRERPSys].[dbo].[Act_GL] ([DocId], "
                          + "[DocDescription], [MasterSno], [EntryDate], [VoucherNo], [AccountCode], "
                          + " [DrAmount], [CrAmount], [Narration], [GocID], [CompId], "
                          + "[BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], "
                          + " [Record_Deleted]) VALUES"
                          + " ('" + ddlDocName.SelectedItem.Value + "', '" + ddlDocName.SelectedItem.Text + "',"
                          + " '" + ids + "', '" + txtEntryDate.Text + "', '" + txtDocNo.Text + "', '" + lblAccountCode.Text + "', '0', '" + lblItemAmount.Text + "', '" + lblItemNarration.Text + "',"
                          + " '" + gocid() + "', '" + compid() + "', '" + branchId() + "', '" + FiscalYear() + "', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.hhhh") + "', '0')");

            }
    }
}

    protected void ddladdGST_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((DropDownList)sender).NamingContainer;
        float taxAddamount;
        string ddlgst = ""; 
         DropDownList ddlAddGSTFooter = (row.FindControl("ddladdGST") as DropDownList);
        Label lblTotalAmountFooter = (row.FindControl("lblTotalAmount") as Label);

        if (ddlAddGSTFooter.SelectedIndex != 0)
        {
            DataSet dsgst = dml.Find("SELECT SUBSTRING('" + ddlAddGSTFooter.SelectedItem.Text + "', CHARINDEX('(', '" + ddlAddGSTFooter.SelectedItem.Text + "') + 1, CHARINDEX(')', '" + ddlAddGSTFooter.SelectedItem.Text + "') - CHARINDEX('(', '" + ddlAddGSTFooter.SelectedItem.Text + "') - 1) AS output");
            if (dsgst.Tables[0].Rows.Count > 0)
            {
                ddlgst = dsgst.Tables[0].Rows[0]["output"].ToString();
            }
            string valuelbl = (row.FindControl("lblValue") as Label).Text;
            if (valuelbl.Length > 0 && ddlAddGSTFooter.SelectedIndex > 0)
            {
                taxAddamount = float.Parse(valuelbl) * (float.Parse(ddlgst) / 100);
            }
            else
            {
                if (ddlAddGSTFooter.SelectedIndex > 0)
                {
                    taxAddamount = (float.Parse(ddlgst) / 100);
                }
                else
                {
                    taxAddamount = float.Parse(valuelbl);
                }
            }

            // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
            lblAddtaxamount.Text = taxAddamount.ToString();

            float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);
            lblTotalAmountFooter.Text = toalamt.ToString();
            (row.FindControl("lblBalanceAmtAmount") as Label).Text = lbltaxamount.Text;
            txtBillBal.Text = lbltaxamount.Text;
            (row.FindControl("lblbalTax") as Label).Text = toalamt.ToString();
            float balamount = toalamt + float.Parse(valuelbl);
            (row.FindControl("lblBalanceAmtAmount") as Label).Text = balamount.ToString();
            txtBillBal.Text = balamount.ToString();
            lblTotalAmountFooter.Text = balamount.ToString();

            dislaydigit_textbox(txtBillBal, balamount);
            dislaydigit_label(row, "lblTotalAmount", balamount);
            dislaydigit_label(row, "lblbalTax", toalamt);
            dislaydigit_label(row, "lblBalanceAmtAmount", balamount);

        }
        else
        {
            string valuelbl = (row.FindControl("lblValue") as Label).Text;
            if (valuelbl.Length > 0 && ddlAddGSTFooter.SelectedIndex == 0)
            {
                taxAddamount = float.Parse(valuelbl) * 0;
            }
            else
            {
                if (ddlAddGSTFooter.SelectedIndex > 0)
                {
                    taxAddamount = (float.Parse(ddlgst) / 100);
                }
                else
                {
                    taxAddamount = float.Parse(valuelbl);
                }
            }

            // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
            lblAddtaxamount.Text = taxAddamount.ToString();

            float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);
            lblTotalAmountFooter.Text = toalamt.ToString();


            (row.FindControl("lblBalanceAmtAmount") as Label).Text = lbltaxamount.Text;
            txtBillBal.Text = lbltaxamount.Text;
            //  (row.FindControl("lblBalanceTaxFooter") as TextBox).Text = taxAddamount.ToString();
            float balamount = toalamt + float.Parse(valuelbl);
            (row.FindControl("lblBalanceAmtAmount") as Label).Text = balamount.ToString();
            txtBillBal.Text = balamount.ToString();
            lblTotalAmountFooter.Text = balamount.ToString();

            dislaydigit_label(row, "lblTotalAmount", balamount);
            dislaydigit_label(row, "lblbalTax", toalamt);
            dislaydigit_label(row, "lblBalanceAmtAmount", balamount);

        }
    }

    protected void ddlGST_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((DropDownList)sender).NamingContainer;
        float taxAddamount;
        string ddlgst = "";
        DropDownList ddlAddGSTFooter = (row.FindControl("ddlGST") as DropDownList);
        Label lblTotalAmountFooter = (row.FindControl("lblTotalAmount") as Label);

        Label lblBalanceAmtAmount = (row.FindControl("lblBalanceAmtAmount") as Label);



        if (ddlAddGSTFooter.SelectedIndex != 0)
        {
            DataSet dsgst = dml.Find("SELECT SUBSTRING('" + ddlAddGSTFooter.SelectedItem.Text + "', CHARINDEX('(', '" + ddlAddGSTFooter.SelectedItem.Text + "') + 1, CHARINDEX(')', '" + ddlAddGSTFooter.SelectedItem.Text + "') - CHARINDEX('(', '" + ddlAddGSTFooter.SelectedItem.Text + "') - 1) AS output");
            if (dsgst.Tables[0].Rows.Count > 0)
            {
                ddlgst = dsgst.Tables[0].Rows[0]["output"].ToString();
            }
            string valuelbl = (row.FindControl("lblValue") as Label).Text;


            if (valuelbl.Length > 0 && ddlAddGSTFooter.SelectedIndex > 0)
            {
                taxAddamount = float.Parse(valuelbl) * (float.Parse(ddlgst) / 100);
            }
            else
            {
                if (ddlAddGSTFooter.SelectedIndex > 0)
                {
                    taxAddamount = (float.Parse(ddlgst) / 100);
                }
                else
                {
                    taxAddamount = float.Parse(valuelbl);
                }
            }

            // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
            lbltaxamount.Text = taxAddamount.ToString();

            float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);


            (row.FindControl("lblGSTAmount") as Label).Text = lbltaxamount.Text;
            //(row.FindControl("lblBalanceAmtAmount") as Label).Text= lbltaxamount.Text;

            (row.FindControl("lblbalTax") as Label).Text = toalamt.ToString();
            float balamount = toalamt + float.Parse(valuelbl);
            (row.FindControl("lblBalanceAmtAmount") as Label).Text = balamount.ToString();
            txtBillBal.Text = balamount.ToString();

            dislaydigit_label(row, "lblbalTax", toalamt);
            dislaydigit_label(row, "lblBalanceAmtAmount", balamount);
            dislaydigit_label(row, "lblValue", float.Parse(valuelbl));


            float total_amount = toalamt + float.Parse(lblTotalAmountFooter.Text);

            lblTotalAmountFooter.Text = total_amount.ToString();
        }
        else
        {
            string valuelbl = (row.FindControl("lblValue") as Label).Text;
            if (valuelbl.Length > 0 && ddlAddGSTFooter.SelectedIndex == 0)
            {
                taxAddamount = float.Parse(valuelbl) * 0;
            }
            else
            {
                if (ddlAddGSTFooter.SelectedIndex > 0)
                {
                    taxAddamount = (float.Parse(ddlgst) / 100);
                }
                else
                {
                    taxAddamount = float.Parse(valuelbl);
                }
            }

            // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
            lbltaxamount.Text = taxAddamount.ToString();

            float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);
            lblTotalAmountFooter.Text = toalamt.ToString();

            (row.FindControl("lblGSTAmount") as Label).Text = lbltaxamount.Text;
            // (row.FindControl("lblBalanceAmtAmount") as Label).Text = lbltaxamount.Text;

            (row.FindControl("lblbalTax") as Label).Text = toalamt.ToString();
            float balamount = toalamt + float.Parse(valuelbl);
            (row.FindControl("lblBalanceAmtAmount") as Label).Text = balamount.ToString();
            txtBillBal.Text = balamount.ToString();

            float total_amount = toalamt + float.Parse(lblTotalAmountFooter.Text);

            lblTotalAmountFooter.Text = total_amount.ToString();
            dislaydigit_label(row, "lblbalTax", toalamt);
            dislaydigit_label(row, "lblBalanceAmtAmount", balamount);
            dislaydigit_label(row, "lblValue", float.Parse(valuelbl));
        }

        //(row.FindControl("lblBalanceTaxFooter") as TextBox).Text = lbltaxamount.Text;

        string flblGSTAmount = (row.FindControl("lblGSTAmount") as Label).Text;
        float faltotalamount = float.Parse(lblTotalAmountFooter.Text);
        float faflblGSTAmount = float.Parse(lbltaxamount.Text);





        string displ = inv.displaydigit(branchId().ToString());

        if (displ == "0")
        {

            (row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0");
            (row.FindControl("lblTotalAmount") as Label).Text = faltotalamount.ToString("0");
        }

        else if (displ == "1")
        {

            (row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.0");
            (row.FindControl("lblTotalAmount") as Label).Text = faltotalamount.ToString("0.0");
        }
        else if (displ == "2")
        {

            (row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.00");
            (row.FindControl("lblTotalAmount") as Label).Text = faltotalamount.ToString("0.00");
        }
        else if (displ == "3")
        {

            (row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.000");
            (row.FindControl("lblTotalAmount") as Label).Text = faltotalamount.ToString("0.000");
        }
        else if (displ == "4")
        {

            (row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.0000");
            (row.FindControl("lblTotalAmount") as Label).Text = faltotalamount.ToString("0.0000");
        }
        else if (displ == "5")
        {

            (row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.00000");
            (row.FindControl("lblTotalAmount") as Label).Text = faltotalamount.ToString("0.00000");
        }
        else if (displ == "6")
        {

            (row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.000000");
            (row.FindControl("lblTotalAmount") as Label).Text = faltotalamount.ToString("0.000000");
        }
        else if (displ == "7")
        {

            (row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.0000000");
            (row.FindControl("lblTotalAmount") as Label).Text = faltotalamount.ToString("0.0000000");
        }
        else if (displ == "8")
        {

            (row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.00000000");
            (row.FindControl("lblTotalAmount") as Label).Text = faltotalamount.ToString("0.00000000");
        }
        else
        {

            (row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0");
            (row.FindControl("lblTotalAmount") as Label).Text = faltotalamount.ToString("0");
        }


    }

    protected void grd_Add_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }
    protected void changetocosttext(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;

        
        TextBox txt = row.FindControl("txtFooterRowsno_RowsAll") as TextBox;
        TextBox txtFooterChangeToCost = row.FindControl("txtFooterChangeToCost") as TextBox;
        if (txtFooterChangeToCost.Text == "N")
        {
            txt.Enabled = false;
        }
        else
        {
            txt.Enabled = true;
        }



    }

    protected void ddlGST6_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((DropDownList)sender).NamingContainer;
        float taxAddamount;
        string ddlgst = "";
        DropDownList ddlAddGSTFooter = (row.FindControl("ddlGST6") as DropDownList);
        TextBox lblTotalAmountFooter = (row.FindControl("lblTotalAmountFooter") as TextBox);

        TextBox lblBalanceAmtAmount = (row.FindControl("lblBalanceFooter") as TextBox);

        

        if (ddlAddGSTFooter.SelectedIndex != 0)
        {
            DataSet dsgst = dml.Find("SELECT SUBSTRING('" + ddlAddGSTFooter.SelectedItem.Text + "', CHARINDEX('(', '" + ddlAddGSTFooter.SelectedItem.Text + "') + 1, CHARINDEX(')', '" + ddlAddGSTFooter.SelectedItem.Text + "') - CHARINDEX('(', '" + ddlAddGSTFooter.SelectedItem.Text + "') - 1) AS output");
            if (dsgst.Tables[0].Rows.Count > 0)
            {
                ddlgst = dsgst.Tables[0].Rows[0]["output"].ToString();
            }
            string valuelbl = (row.FindControl("lblValueFooter") as TextBox).Text;

            
            if (valuelbl.Length > 0 && ddlAddGSTFooter.SelectedIndex > 0)
            {
                taxAddamount = float.Parse(valuelbl) * (float.Parse(ddlgst) / 100);
            }
            else
            {
                if (ddlAddGSTFooter.SelectedIndex > 0)
                {
                    taxAddamount = (float.Parse(ddlgst) / 100);
                }
                else
                {
                    taxAddamount = float.Parse(valuelbl);
                }
            }

            // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
            lbltaxamount.Text = taxAddamount.ToString();

            float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);
           

            (row.FindControl("lblGSTAmountFooter") as Label).Text = lbltaxamount.Text;
            //(row.FindControl("lblBalanceAmtAmount") as Label).Text= lbltaxamount.Text;
            
            (row.FindControl("lblBalanceTaxFooter") as TextBox).Text = toalamt.ToString();
            float balamount = toalamt + float.Parse(valuelbl);
            (row.FindControl("lblBalanceFooter") as TextBox).Text = balamount.ToString();
            txtBillBal.Text = balamount.ToString();

            dislaydigit_fortextbox(row, "lblBalanceTaxFooter", toalamt);
            dislaydigit_fortextbox(row, "lblBalanceFooter", balamount);
            dislaydigit_fortextbox(row, "lblValueFooter", float.Parse(valuelbl));


            float total_amount = toalamt + float.Parse(lblTotalAmountFooter.Text);

            lblTotalAmountFooter.Text = total_amount.ToString();
        }
        else
        {
            string valuelbl = (row.FindControl("lblValueFooter") as Label).Text;
            if (valuelbl.Length > 0 && ddlAddGSTFooter.SelectedIndex == 0)
            {
                taxAddamount = float.Parse(valuelbl) * 0;
            }
            else
            {
                if (ddlAddGSTFooter.SelectedIndex > 0)
                {
                    taxAddamount = (float.Parse(ddlgst) / 100);
                }
                else
                {
                    taxAddamount = float.Parse(valuelbl);
                }
            }

            // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
            lbltaxamount.Text = taxAddamount.ToString();

            float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);
            lblTotalAmountFooter.Text = toalamt.ToString();

            (row.FindControl("lblGSTAmountFooter") as Label).Text = lbltaxamount.Text;
            // (row.FindControl("lblBalanceAmtAmount") as Label).Text = lbltaxamount.Text;
           
            (row.FindControl("lblBalanceTaxFooter") as TextBox).Text = toalamt.ToString();
            float balamount = toalamt + float.Parse(valuelbl);
            (row.FindControl("lblBalanceFooter") as TextBox).Text = balamount.ToString();
            txtBillBal.Text = balamount.ToString();

            float total_amount = toalamt + float.Parse(lblTotalAmountFooter.Text);

            lblTotalAmountFooter.Text = total_amount.ToString();
            dislaydigit_fortextbox(row, "lblBalanceTaxFooter", toalamt);
            dislaydigit_fortextbox(row, "lblBalanceFooter", balamount);
            dislaydigit_fortextbox(row, "lblValueFooter", float.Parse(valuelbl));
        }
        
        //(row.FindControl("lblBalanceTaxFooter") as TextBox).Text = lbltaxamount.Text;

        string flblGSTAmount = (row.FindControl("lblGSTAmountFooter") as Label).Text;
        float faltotalamount = float.Parse(lblTotalAmountFooter.Text);
        float faflblGSTAmount = float.Parse(lbltaxamount.Text);

        
        


        string displ = inv.displaydigit(branchId().ToString());

        if (displ == "0")
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0");
        }

        else if (displ == "1")
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0.0");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0.0");
        }
        else if (displ == "2")
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0.00");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0.00");
        }
        else if (displ == "3")
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0.000");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0.000");
        }
        else if (displ == "4")
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0.0000");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0.0000");
        }
        else if (displ == "5")
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0.00000");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0.00000");
        }
        else if (displ == "6")
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0.000000");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0.000000");
        }
        else if (displ == "7")
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0.0000000");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0.0000000");
        }
        else if (displ == "8")
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0.00000000");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0.00000000");
        }
        else
        {

            (row.FindControl("lblGSTAmountFooter") as Label).Text = faflblGSTAmount.ToString("0");
            (row.FindControl("lblTotalAmountFooter") as TextBox).Text = faltotalamount.ToString("0");
        }

        //+++++++++++++++++++++++++++++++++++++
        if(ddlAddGSTFooter.SelectedIndex > 0)
        {
            float taxAddamount1;
            string ddlgst1 = "";
            DropDownList ddlAddGSTFooter1 = (row.FindControl("ddlAddGSTFooter") as DropDownList);
            TextBox lblTotalAmountFooter1 = (row.FindControl("lblTotalAmountFooter") as TextBox);
            Label lblGSTAmountFooter = (row.FindControl("lblGSTAmountFooter") as Label);
            //lblGSTAmountFooter
            if (ddlAddGSTFooter1.SelectedIndex != 0)
            {
                DataSet dsgst = dml.Find("SELECT SUBSTRING('" + ddlAddGSTFooter1.SelectedItem.Text + "', CHARINDEX('(', '" + ddlAddGSTFooter1.SelectedItem.Text + "') + 1, CHARINDEX(')', '" + ddlAddGSTFooter1.SelectedItem.Text + "') - CHARINDEX('(', '" + ddlAddGSTFooter1.SelectedItem.Text + "') - 1) AS output");
                if (dsgst.Tables[0].Rows.Count > 0)
                {
                    ddlgst1 = dsgst.Tables[0].Rows[0]["output"].ToString();
                }
                string valuelbl = (row.FindControl("lblValueFooter") as TextBox).Text;
                if (valuelbl.Length > 0 && ddlAddGSTFooter1.SelectedIndex > 0)
                {
                    taxAddamount1 = float.Parse(valuelbl) * (float.Parse(ddlgst1) / 100);
                }
                else
                {
                    if (ddlAddGSTFooter1.SelectedIndex > 0)
                    {
                        taxAddamount1 = (float.Parse(ddlgst1) / 100);
                    }
                    else
                    {
                        taxAddamount1 = float.Parse(valuelbl);
                    }
                }

                // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
                lblAddtaxamount.Text = taxAddamount1.ToString();

                float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);
                lblTotalAmountFooter1.Text = toalamt.ToString();
                (row.FindControl("lblBalanceFooter") as TextBox).Text = lbltaxamount.Text;
                lblGSTAmountFooter.Text = toalamt.ToString();
                dislaydigit_label(row, "lblGSTAmountFooter", toalamt);
                txtBillBal.Text = lbltaxamount.Text;
                (row.FindControl("lblBalanceTaxFooter") as TextBox).Text = toalamt.ToString();
                float balamount = toalamt + float.Parse(valuelbl);
                (row.FindControl("lblBalanceFooter") as TextBox).Text = balamount.ToString();
                txtBillBal.Text = balamount.ToString();
                lblTotalAmountFooter1.Text = balamount.ToString();

                dislaydigit_textbox(txtBillBal, balamount);
                dislaydigit_fortextbox(row, "lblTotalAmountFooter", balamount);
                dislaydigit_fortextbox(row, "lblBalanceTaxFooter", toalamt);
                dislaydigit_fortextbox(row, "lblBalanceFooter", balamount);


            }
            else
            {
                string valuelbl = (row.FindControl("lblValueFooter") as TextBox).Text;
                if (valuelbl.Length > 0 && ddlAddGSTFooter1.SelectedIndex == 0)
                {
                    taxAddamount = float.Parse(valuelbl) * 0;
                }
                else
                {
                    if (ddlAddGSTFooter1.SelectedIndex > 0)
                    {
                        taxAddamount = (float.Parse(ddlgst1) / 100);
                    }
                    else
                    {
                        taxAddamount = float.Parse(valuelbl);
                    }
                }

                // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
                lblAddtaxamount.Text = taxAddamount.ToString();

                 float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);
                lblTotalAmountFooter1.Text = toalamt.ToString();


                (row.FindControl("lblBalanceFooter") as TextBox).Text = lbltaxamount.Text;
                txtBillBal.Text = lbltaxamount.Text;
                //  (row.FindControl("lblBalanceTaxFooter") as TextBox).Text = taxAddamount.ToString();
                float balamount = toalamt + float.Parse(valuelbl);
                (row.FindControl("lblBalanceFooter") as TextBox).Text = balamount.ToString();
                txtBillBal.Text = balamount.ToString();
                lblTotalAmountFooter1.Text = balamount.ToString();

                dislaydigit_fortextbox(row, "lblTotalAmountFooter", balamount);
                dislaydigit_fortextbox(row, "lblBalanceTaxFooter", toalamt);
                dislaydigit_fortextbox(row, "lblBalanceFooter", balamount);

            }
        }
        //+++++++++++++++++++++++++++++++++++++++
        



    }

    public void dislaydigit_fortextbox(GridViewRow row,string controlname,float value)
    {
        string displ = inv.displaydigit(branchId().ToString());

        if (displ == "0")
        {

            (row.FindControl(controlname) as TextBox).Text = value.ToString("0");
            
        }

        else if (displ == "1")
        {

            (row.FindControl(controlname) as TextBox).Text = value.ToString("0.0");
            
        }
        else if (displ == "2")
        {

            (row.FindControl(controlname) as TextBox).Text = value.ToString("0.00");
            
        }
        else if (displ == "3")
        {

            (row.FindControl(controlname) as TextBox).Text = value.ToString("0.000");
            
        }
        else if (displ == "4")
        {

            (row.FindControl(controlname) as TextBox).Text = value.ToString("0.0000");
            
        }
        else if (displ == "5")
        {

            (row.FindControl(controlname) as TextBox).Text = value.ToString("0.00000");
            
        }
        else if (displ == "6")
        {

            (row.FindControl(controlname) as TextBox).Text = value.ToString("0.000000");
            
        }
        else if (displ == "7")
        {

            (row.FindControl(controlname) as TextBox).Text = value.ToString("0.0000000");
            
        }
        else if (displ == "8")
        {

            (row.FindControl(controlname) as TextBox).Text = value.ToString("0.00000000");
            
        }
        else
        {

            (row.FindControl(controlname) as TextBox).Text = value.ToString("0");
            
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

    protected void ddlGST7_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((DropDownList)sender).NamingContainer;
        float taxAddamount;
        string ddlgst = "";
        DropDownList ddlGST7 = (row.FindControl("ddlGST7") as DropDownList);
        Label lblTotalAmountFooter = (row.FindControl("lblTotalAmount") as Label);

        Label lblBalanceAmtAmount = (row.FindControl("lblBalanceAmtAmount") as Label);






        if (ddlGST7.SelectedIndex != 0)
        {
            DataSet dsgst = dml.Find("SELECT SUBSTRING('" + ddlGST7.SelectedItem.Text + "', CHARINDEX('(', '" + ddlGST7.SelectedItem.Text + "') + 1, CHARINDEX(')', '" + ddlGST7.SelectedItem.Text + "') - CHARINDEX('(', '" + ddlGST7.SelectedItem.Text + "') - 1) AS output");
            if (dsgst.Tables[0].Rows.Count > 0)
            {
                ddlgst = dsgst.Tables[0].Rows[0]["output"].ToString();
            }
            string valuelbl = (row.FindControl("lblValue") as Label).Text;
            if (valuelbl.Length > 0 && ddlGST7.SelectedIndex > 0)
            {
                taxAddamount = float.Parse(valuelbl) * (float.Parse(ddlgst) / 100);
            }
            else
            {
                if (ddlGST7.SelectedIndex > 0)
                {
                    taxAddamount = (float.Parse(ddlgst) / 100);
                }
                else
                {
                    taxAddamount = float.Parse(valuelbl);
                }
            }

            // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
            lbltaxamount.Text = taxAddamount.ToString();

            float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);
            lblTotalAmountFooter.Text = toalamt.ToString();

            (row.FindControl("lblGSTAmount") as Label).Text = lbltaxamount.Text;
            //(row.FindControl("lblBalanceAmtAmount") as Label).Text= lbltaxamount.Text;
            txtBillBal.Text = lbltaxamount.Text;
            (row.FindControl("lblBalanceTax") as Label).Text = lbltaxamount.Text;

        }
        else
        {
            string valuelbl = (row.FindControl("lblValue") as Label).Text;
            if (valuelbl.Length > 0 && ddlGST7.SelectedIndex == 0)
            {
                taxAddamount = float.Parse(valuelbl) * 0;
            }
            else
            {
                if (ddlGST7.SelectedIndex > 0)
                {
                    taxAddamount = (float.Parse(ddlgst) / 100);
                }
                else
                {
                    taxAddamount = float.Parse(valuelbl);
                }
            }

            // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
            lbltaxamount.Text = taxAddamount.ToString();

            float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);
            lblTotalAmountFooter.Text = toalamt.ToString();

            (row.FindControl("lblGSTAmount") as Label).Text = lbltaxamount.Text;
            // (row.FindControl("lblBalanceAmtAmount") as Label).Text = lbltaxamount.Text;
            txtBillBal.Text = lbltaxamount.Text;


        }

        (row.FindControl("lblBalanceTax") as Label).Text = lbltaxamount.Text;

        string flblGSTAmount = (row.FindControl("lblGSTAmount") as Label).Text;

        float faltotalamount = float.Parse(lblTotalAmountFooter.Text);
        float faflblGSTAmount = float.Parse(lbltaxamount.Text);

        string displ = inv.displaydigit(branchId().ToString());
        if (displ == "0")
        {

            (row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0");
            (row.FindControl("lblTotalAmount") as Label).Text = faltotalamount.ToString("0");
        }

        else if (displ == "1")
        {

            (row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.0");
            (row.FindControl("lblTotalAmount") as Label).Text = faltotalamount.ToString("0.0");
        }
        else if (displ == "2")
        {

            (row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.00");
            (row.FindControl("lblTotalAmount") as Label).Text = faltotalamount.ToString("0.00");
        }
        else if (displ == "3")
        {

            (row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.000");
            (row.FindControl("lblTotalAmount") as Label).Text = faltotalamount.ToString("0.000");
        }
        else if (displ == "4")
        {

            (row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.0000");
            (row.FindControl("lblTotalAmount") as Label).Text = faltotalamount.ToString("0.0000");
        }
        else if (displ == "5")
        {

            (row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.00000");
            (row.FindControl("lblTotalAmount") as Label).Text = faltotalamount.ToString("0.00000");
        }
        else if (displ == "6")
        {

            (row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.000000");
            (row.FindControl("lblTotalAmount") as Label).Text = faltotalamount.ToString("0.000000");
        }
        else if (displ == "7")
        {

            (row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.0000000");
            (row.FindControl("lblTotalAmount") as Label).Text = faltotalamount.ToString("0.0000000");
        }
        else if (displ == "8")
        {

            (row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.00000000");
            (row.FindControl("lblTotalAmount") as Label).Text = faltotalamount.ToString("0.00000000");
        }
        else
        {

            (row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0");
            (row.FindControl("lblTotalAmount") as Label).Text = faltotalamount.ToString("0");
        }


    }

    protected void ddladdGST7_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((DropDownList)sender).NamingContainer;
        float taxAddamount;
        string ddlgst = "";
        DropDownList ddlAddGSTFooter = (row.FindControl("ddladdGST") as DropDownList);
        Label lblTotalAmountFooter = (row.FindControl("lblTotalAmount") as Label);

        if (ddlAddGSTFooter.SelectedIndex != 0)
        {
            DataSet dsgst = dml.Find("SELECT SUBSTRING('" + ddlAddGSTFooter.SelectedItem.Text + "', CHARINDEX('(', '" + ddlAddGSTFooter.SelectedItem.Text + "') + 1, CHARINDEX(')', '" + ddlAddGSTFooter.SelectedItem.Text + "') - CHARINDEX('(', '" + ddlAddGSTFooter.SelectedItem.Text + "') - 1) AS output");
            if (dsgst.Tables[0].Rows.Count > 0)
            {
                ddlgst = dsgst.Tables[0].Rows[0]["output"].ToString();
            }
            string valuelbl = (row.FindControl("lblValue") as Label).Text;
            if (valuelbl.Length > 0 && ddlAddGSTFooter.SelectedIndex > 0)
            {
                taxAddamount = float.Parse(valuelbl) * (float.Parse(ddlgst) / 100);
            }
            else
            {
                if (ddlAddGSTFooter.SelectedIndex > 0)
                {
                    taxAddamount = (float.Parse(ddlgst) / 100);
                }
                else
                {
                    taxAddamount = float.Parse(valuelbl);
                }
            }

            // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
            lblAddtaxamount.Text = taxAddamount.ToString();

            float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);
            lblTotalAmountFooter.Text = toalamt.ToString();
            (row.FindControl("lblBalanceAmtAmount") as Label).Text = lbltaxamount.Text;
            // txtBillBal.Text = lbltaxamount.Text;
            (row.FindControl("lblBalanceTax") as Label).Text = toalamt.ToString();
            txtBillBal.Text = toalamt.ToString();

        }
        else
        {
            string valuelbl = (row.FindControl("lblValue") as Label).Text;
            if (valuelbl.Length > 0 && ddlAddGSTFooter.SelectedIndex == 0)
            {
                taxAddamount = float.Parse(valuelbl) * 0;
            }
            else
            {
                if (ddlAddGSTFooter.SelectedIndex > 0)
                {
                    taxAddamount = (float.Parse(ddlgst) / 100);
                }
                else
                {
                    taxAddamount = float.Parse(valuelbl);
                }
            }

            // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
            lblAddtaxamount.Text = taxAddamount.ToString();

            float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);
            lblTotalAmountFooter.Text = toalamt.ToString();
            (row.FindControl("lblBalanceAmtAmount") as Label).Text = lbltaxamount.Text;
            //txtBillBal.Text = lbltaxamount.Text;
            (row.FindControl("lblBalanceTax") as Label).Text = toalamt.ToString();//taxAddamount.ToString();
            txtBillBal.Text = toalamt.ToString();
        }
    }

    protected void GridView8_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lblRowNumber") as Label).Text = (e.Row.RowIndex + 1).ToString();

            Label value = e.Row.FindControl("lblValue") as Label;

            string ddlgst = "0";
            Label gsttool = e.Row.FindControl("lblGSTEdit") as Label;

            if (gsttool.Text != "")
            {
               float gstval = float.Parse(value.Text) * (float.Parse(gsttool.Text) / 100);
                gsttool.ToolTip = gstval.ToString();
            }


            string ddladdgst = "0";
            Label gstaddtool = e.Row.FindControl("lbladdGSTEdit") as Label;
            if (gstaddtool.Text != "")
            {
                float gstaddval = float.Parse(value.Text) * (float.Parse(gstaddtool.Text) / 100);
                gstaddtool.ToolTip = gstaddval.ToString();
            }



            string f = (e.Row.FindControl("lblValue") as Label).Text;
            string flblGSTAmount = (e.Row.FindControl("lblGSTAmount") as Label).Text;
            string flblTotalAmount = (e.Row.FindControl("lblTotalAmount") as Label).Text;
            float faflblGSTAmount = float.Parse(flblGSTAmount);
            float FaflblTotalAmount = float.Parse(flblTotalAmount);
            float fa = float.Parse(f);
            string displ = inv.displaydigit(branchId().ToString());
            if (displ == "0")
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0");
                (e.Row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0");
                (e.Row.FindControl("lblTotalAmount") as Label).Text = FaflblTotalAmount.ToString("0");
            }

            else if (displ == "1")
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.0");
                (e.Row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.0");
                (e.Row.FindControl("lblTotalAmount") as Label).Text = FaflblTotalAmount.ToString("0.0");
            }
            else if (displ == "2")
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.00");
                (e.Row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.00");
                (e.Row.FindControl("lblTotalAmount") as Label).Text = FaflblTotalAmount.ToString("0.00");
            }
            else if (displ == "3")
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.000");
                (e.Row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.000");
                (e.Row.FindControl("lblTotalAmount") as Label).Text = FaflblTotalAmount.ToString("0.000");
            }
            else if (displ == "4")
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.0000");
                (e.Row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.0000");
                (e.Row.FindControl("lblTotalAmount") as Label).Text = FaflblTotalAmount.ToString("0.0000");
            }
            else if (displ == "5")
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.00000");
                (e.Row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.00000");
                (e.Row.FindControl("lblTotalAmount") as Label).Text = FaflblTotalAmount.ToString("0.00000");
            }
            else if (displ == "6")
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.000000");
                (e.Row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.000000");
                (e.Row.FindControl("lblTotalAmount") as Label).Text = FaflblTotalAmount.ToString("0.000000");
            }
            else if (displ == "7")
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.0000000");
                (e.Row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.0000000");
                (e.Row.FindControl("lblTotalAmount") as Label).Text = FaflblTotalAmount.ToString("0.0000000");
            }
            else if (displ == "8")
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0.00000000");
                (e.Row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0.00000000");
                (e.Row.FindControl("lblTotalAmount") as Label).Text = FaflblTotalAmount.ToString("0.00000000");
            }
            else
            {
                (e.Row.FindControl("lblValue") as Label).Text = fa.ToString("0");
                (e.Row.FindControl("lblGSTAmount") as Label).Text = faflblGSTAmount.ToString("0");
                (e.Row.FindControl("lblTotalAmount") as Label).Text = FaflblTotalAmount.ToString("0");
            }

        }
    }

    protected void changetocosttext_red(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;


        TextBox txt = row.FindControl("txtFooterRowsno_RowsAllDed") as TextBox;
        TextBox txtFooterChangeToCost = row.FindControl("txtFooterChangeToCostDed") as TextBox;
        if (txtFooterChangeToCost.Text == "N")
        {
            txt.Enabled = false;
        }
        else
        {
            txt.Enabled = true;
        }



    }

    public void dislaydigit_textbox(TextBox box, float value)
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

    protected void GridView8_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView8.EditIndex = e.NewEditIndex;
    }

    protected void GridView8_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }

    protected void GridView8_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView8.EditIndex = -1;
    }



    protected void txtDCQtyEdit_TextChanged(object sender, EventArgs e)
    {


        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;

        float value;
        string Qty = (row.FindControl("txtDCQtyEdit") as TextBox).Text;
        string rate = (row.FindControl("lblRateEdit") as TextBox).Text;
        if (rate.Length > 0 && Qty.Length > 0)
        {
            value = float.Parse(Qty) * float.Parse(rate);
        }
        else
        {
            if (Qty.Length > 0)
            {
                value = float.Parse(Qty) * 0;
            }
            else
            {
                value = 0 * float.Parse(rate);
            }
        }

      (row.FindControl("txtValueEdit") as TextBox).Text = value.ToString("0.00");

        float taxAddamount;
        string ddlgst = "";
        DropDownList ddlAddGSTFooter = (row.FindControl("ddlGST6Edit") as DropDownList);
        TextBox lblTotalAmountFooter = (row.FindControl("txtTotalAmount") as TextBox);

        TextBox lblBalanceAmtAmount = (row.FindControl("txtBalanceEdit") as TextBox);



        if (ddlAddGSTFooter.SelectedIndex != 0)
        {
            DataSet dsgst = dml.Find("SELECT SUBSTRING('" + ddlAddGSTFooter.SelectedItem.Text + "', CHARINDEX('(', '" + ddlAddGSTFooter.SelectedItem.Text + "') + 1, CHARINDEX(')', '" + ddlAddGSTFooter.SelectedItem.Text + "') - CHARINDEX('(', '" + ddlAddGSTFooter.SelectedItem.Text + "') - 1) AS output");
            if (dsgst.Tables[0].Rows.Count > 0)
            {
                ddlgst = dsgst.Tables[0].Rows[0]["output"].ToString();
            }
            string valuelbl = (row.FindControl("txtValueEdit") as TextBox).Text;


            if (valuelbl.Length > 0 && ddlAddGSTFooter.SelectedIndex > 0)
            {
                taxAddamount = float.Parse(valuelbl) * (float.Parse(ddlgst) / 100);
            }
            else
            {
                if (ddlAddGSTFooter.SelectedIndex > 0)
                {
                    taxAddamount = (float.Parse(ddlgst) / 100);
                }
                else
                {
                    taxAddamount = float.Parse(valuelbl);
                }
            }

            // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
            lbltaxamount.Text = taxAddamount.ToString();

            float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);


            (row.FindControl("lblGSTAmountEdit") as Label).Text = lbltaxamount.Text;
            //(row.FindControl("lblBalanceAmtAmount") as Label).Text= lbltaxamount.Text;

            (row.FindControl("txtBalanceTax") as TextBox).Text = toalamt.ToString();
            float balamount = toalamt + float.Parse(valuelbl);
            (row.FindControl("txtBalanceEdit") as TextBox).Text = balamount.ToString();
            txtBillBal.Text = balamount.ToString();

            dislaydigit_fortextbox(row, "txtBalanceTax", toalamt);
            dislaydigit_fortextbox(row, "txtBalanceEdit", balamount);
            dislaydigit_fortextbox(row, "txtValueEdit", float.Parse(valuelbl));


            float total_amount = toalamt + float.Parse(lblTotalAmountFooter.Text);

            lblTotalAmountFooter.Text = total_amount.ToString();
        }
        else
        {
            string valuelbl = (row.FindControl("txtValueEdit") as TextBox).Text;
            if (valuelbl.Length > 0 && ddlAddGSTFooter.SelectedIndex == 0)
            {
                taxAddamount = float.Parse(valuelbl) * 0;
            }
            else
            {
                if (ddlAddGSTFooter.SelectedIndex > 0)
                {
                    taxAddamount = (float.Parse(ddlgst) / 100);
                }
                else
                {
                    taxAddamount = float.Parse(valuelbl);
                }
            }

            // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
            lbltaxamount.Text = taxAddamount.ToString();

            float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);
            lblTotalAmountFooter.Text = toalamt.ToString();

            (row.FindControl("lblGSTAmountEdit") as Label).Text = lbltaxamount.Text;
            // (row.FindControl("lblBalanceAmtAmount") as Label).Text = lbltaxamount.Text;

            (row.FindControl("txtBalanceTax") as TextBox).Text = toalamt.ToString();
            float balamount = toalamt + float.Parse(valuelbl);
            (row.FindControl("txtBalanceEdit") as TextBox).Text = balamount.ToString();
            txtBillBal.Text = balamount.ToString();

            float total_amount = toalamt + float.Parse(lblTotalAmountFooter.Text);

            lblTotalAmountFooter.Text = total_amount.ToString();
            dislaydigit_fortextbox(row, "txtBalanceTax", toalamt);
            dislaydigit_fortextbox(row, "txtBalanceEdit", balamount);
            dislaydigit_fortextbox(row, "lblValueFooter", float.Parse(valuelbl));
        }

        //(row.FindControl("lblBalanceTaxFooter") as TextBox).Text = lbltaxamount.Text;

        string flblGSTAmount = (row.FindControl("lblGSTAmountEdit") as Label).Text;
        float faltotalamount = float.Parse(lblTotalAmountFooter.Text);
        float faflblGSTAmount = float.Parse(lbltaxamount.Text);





        string displ = inv.displaydigit(branchId().ToString());

        if (displ == "0")
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0");
        }

        else if (displ == "1")
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0.0");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0.0");
        }
        else if (displ == "2")
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0.00");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0.00");
        }
        else if (displ == "3")
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0.000");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0.000");
        }
        else if (displ == "4")
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0.0000");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0.0000");
        }
        else if (displ == "5")
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0.00000");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0.00000");
        }
        else if (displ == "6")
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0.000000");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0.000000");
        }
        else if (displ == "7")
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0.0000000");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0.0000000");
        }
        else if (displ == "8")
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0.00000000");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0.00000000");
        }
        else
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0");
        }

        //+++++++++++++++++++++++++++++++++++++
        if (ddlAddGSTFooter.SelectedIndex > 0)
        {
            float taxAddamount1;
            string ddlgst1 = "";
            DropDownList ddlAddGSTFooter1 = (row.FindControl("ddlAddGSTEdit") as DropDownList);
            TextBox lblTotalAmountFooter1 = (row.FindControl("txtTotalAmount") as TextBox);
            Label lblGSTAmountFooter = (row.FindControl("lblGSTAmountEdit") as Label);
            //lblGSTAmountFooter
            if (ddlAddGSTFooter1.SelectedIndex != 0)
            {
                DataSet dsgst = dml.Find("SELECT SUBSTRING('" + ddlAddGSTFooter1.SelectedItem.Text + "', CHARINDEX('(', '" + ddlAddGSTFooter1.SelectedItem.Text + "') + 1, CHARINDEX(')', '" + ddlAddGSTFooter1.SelectedItem.Text + "') - CHARINDEX('(', '" + ddlAddGSTFooter1.SelectedItem.Text + "') - 1) AS output");
                if (dsgst.Tables[0].Rows.Count > 0)
                {
                    ddlgst1 = dsgst.Tables[0].Rows[0]["output"].ToString();
                }
                string valuelbl = (row.FindControl("txtValueEdit") as TextBox).Text;
                if (valuelbl.Length > 0 && ddlAddGSTFooter1.SelectedIndex > 0)
                {
                    taxAddamount1 = float.Parse(valuelbl) * (float.Parse(ddlgst1) / 100);
                }
                else
                {
                    if (ddlAddGSTFooter1.SelectedIndex > 0)
                    {
                        taxAddamount1 = (float.Parse(ddlgst1) / 100);
                    }
                    else
                    {
                        taxAddamount1 = float.Parse(valuelbl);
                    }
                }

                // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
                lblAddtaxamount.Text = taxAddamount1.ToString();

                float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);
                lblTotalAmountFooter1.Text = toalamt.ToString();
                (row.FindControl("txtBalanceEdit") as TextBox).Text = lbltaxamount.Text;
                lblGSTAmountFooter.Text = toalamt.ToString();
                dislaydigit_label(row, "lblGSTAmountEdit", toalamt);
                txtBillBal.Text = lbltaxamount.Text;
                (row.FindControl("txtBalanceTax") as TextBox).Text = toalamt.ToString();
                float balamount = toalamt + float.Parse(valuelbl);
                (row.FindControl("txtBalanceEdit") as TextBox).Text = balamount.ToString();
                txtBillBal.Text = balamount.ToString();
                lblTotalAmountFooter1.Text = balamount.ToString();

                dislaydigit_textbox(txtBillBal, balamount);
                dislaydigit_fortextbox(row, "txtTotalAmount", balamount);
                dislaydigit_fortextbox(row, "txtBalanceTax", toalamt);
                dislaydigit_fortextbox(row, "txtBalanceEdit", balamount);


            }
            else
            {
                string valuelbl = (row.FindControl("txtValueEdit") as TextBox).Text;
                if (valuelbl.Length > 0 && ddlAddGSTFooter1.SelectedIndex == 0)
                {
                    taxAddamount = float.Parse(valuelbl) * 0;
                }
                else
                {
                    if (ddlAddGSTFooter1.SelectedIndex > 0)
                    {
                        taxAddamount = (float.Parse(ddlgst1) / 100);
                    }
                    else
                    {
                        taxAddamount = float.Parse(valuelbl);
                    }
                }

                // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
                lblAddtaxamount.Text = taxAddamount.ToString();

                float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);
                lblTotalAmountFooter1.Text = toalamt.ToString();


                (row.FindControl("txtBalanceEdit") as TextBox).Text = lbltaxamount.Text;
                txtBillBal.Text = lbltaxamount.Text;
                //  (row.FindControl("lblBalanceTaxFooter") as TextBox).Text = taxAddamount.ToString();
                float balamount = toalamt + float.Parse(valuelbl);
                (row.FindControl("txtBalanceEdit") as TextBox).Text = balamount.ToString();
                txtBillBal.Text = balamount.ToString();
                lblTotalAmountFooter1.Text = balamount.ToString();

                dislaydigit_fortextbox(row, "txtTotalAmount", balamount);
                dislaydigit_fortextbox(row, "txtBalanceEdit", toalamt);
                dislaydigit_fortextbox(row, "txtBalanceEdit", balamount);

            }
        }
        //+++++++++++++++++++++++++++++++++++++++

        
        //====================================================================




    }

    protected void txtRateEdit_TextChanged(object sender, EventArgs e)
    {

        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;
        float value;
        string Qty = (row.FindControl("txtDCQtyEdit") as TextBox).Text;
        string rate = (row.FindControl("lblRateEdit") as TextBox).Text;
        if (rate.Length > 0 && Qty.Length > 0)
        {
            value = float.Parse(Qty) * float.Parse(rate);
        }
        else
        {
            if (Qty.Length > 0)
            {
                value = float.Parse(Qty) * 0;
            }
            else
            {
                value = 0 * float.Parse(rate);
            }
        }

        // (row.FindControl("txtValueEdit") as TextBox).Text = value.ToString("0.00");
        //   (row.FindControl("lblBalanceFooter") as TextBox).Text = value.ToString("0.00");
        dislaydigit_fortextbox(row, "txtValueEdit", value);
        dislaydigit_fortextbox(row, "txtBalanceEdit", value);
        dislaydigit_fortextbox(row, "txtTotalAmount", value);

        //-----------------------------------------------------------------

        float taxAddamount;
        string ddlgst = "";
        DropDownList ddlAddGSTFooter = (row.FindControl("ddlGST6Edit") as DropDownList);
        TextBox lblTotalAmountFooter = (row.FindControl("txtTotalAmount") as TextBox);

        TextBox lblBalanceAmtAmount = (row.FindControl("txtBalanceEdit") as TextBox);



        if (ddlAddGSTFooter.SelectedIndex != 0)
        {
            DataSet dsgst = dml.Find("SELECT SUBSTRING('" + ddlAddGSTFooter.SelectedItem.Text + "', CHARINDEX('(', '" + ddlAddGSTFooter.SelectedItem.Text + "') + 1, CHARINDEX(')', '" + ddlAddGSTFooter.SelectedItem.Text + "') - CHARINDEX('(', '" + ddlAddGSTFooter.SelectedItem.Text + "') - 1) AS output");
            if (dsgst.Tables[0].Rows.Count > 0)
            {
                ddlgst = dsgst.Tables[0].Rows[0]["output"].ToString();
            }
            string valuelbl = (row.FindControl("txtValueEdit") as TextBox).Text;


            if (valuelbl.Length > 0 && ddlAddGSTFooter.SelectedIndex > 0)
            {
                taxAddamount = float.Parse(valuelbl) * (float.Parse(ddlgst) / 100);
            }
            else
            {
                if (ddlAddGSTFooter.SelectedIndex > 0)
                {
                    taxAddamount = (float.Parse(ddlgst) / 100);
                }
                else
                {
                    taxAddamount = float.Parse(valuelbl);
                }
            }

            // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
            lbltaxamount.Text = taxAddamount.ToString();

            float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);


            (row.FindControl("lblGSTAmountEdit") as Label).Text = lbltaxamount.Text;
            //(row.FindControl("lblBalanceAmtAmount") as Label).Text= lbltaxamount.Text;

            (row.FindControl("txtBalanceTax") as TextBox).Text = toalamt.ToString();
            float balamount = toalamt + float.Parse(valuelbl);
            (row.FindControl("txtBalanceEdit") as TextBox).Text = balamount.ToString();
            txtBillBal.Text = balamount.ToString();

            dislaydigit_fortextbox(row, "txtBalanceTax", toalamt);
            dislaydigit_fortextbox(row, "txtBalanceEdit", balamount);
            dislaydigit_fortextbox(row, "txtValueEdit", float.Parse(valuelbl));


            float total_amount = toalamt + float.Parse(lblTotalAmountFooter.Text);

            lblTotalAmountFooter.Text = total_amount.ToString();
        }
        else
        {
            string valuelbl = (row.FindControl("txtValueEdit") as TextBox).Text;
            if (valuelbl.Length > 0 && ddlAddGSTFooter.SelectedIndex == 0)
            {
                taxAddamount = float.Parse(valuelbl) * 0;
            }
            else
            {
                if (ddlAddGSTFooter.SelectedIndex > 0)
                {
                    taxAddamount = (float.Parse(ddlgst) / 100);
                }
                else
                {
                    taxAddamount = float.Parse(valuelbl);
                }
            }

            // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
            lbltaxamount.Text = taxAddamount.ToString();

            float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);
            lblTotalAmountFooter.Text = toalamt.ToString();

            (row.FindControl("lblGSTAmountEdit") as Label).Text = lbltaxamount.Text;
            // (row.FindControl("lblBalanceAmtAmount") as Label).Text = lbltaxamount.Text;

            (row.FindControl("txtBalanceTax") as TextBox).Text = toalamt.ToString();
            float balamount = toalamt + float.Parse(valuelbl);
            (row.FindControl("txtBalanceEdit") as TextBox).Text = balamount.ToString();
            txtBillBal.Text = balamount.ToString();

            float total_amount = toalamt + float.Parse(lblTotalAmountFooter.Text);

            lblTotalAmountFooter.Text = total_amount.ToString();
            dislaydigit_fortextbox(row, "txtBalanceTax", toalamt);
            dislaydigit_fortextbox(row, "txtBalanceEdit", balamount);
            dislaydigit_fortextbox(row, "txtValueEdit", float.Parse(valuelbl));
        }

        //(row.FindControl("lblBalanceTaxFooter") as TextBox).Text = lbltaxamount.Text;

        string flblGSTAmount = (row.FindControl("lblGSTAmountEdit") as Label).Text;
        float faltotalamount = float.Parse(lblTotalAmountFooter.Text);
        float faflblGSTAmount = float.Parse(lbltaxamount.Text);





        string displ = inv.displaydigit(branchId().ToString());

        if (displ == "0")
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0");
        }

        else if (displ == "1")
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0.0");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0.0");
        }
        else if (displ == "2")
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0.00");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0.00");
        }
        else if (displ == "3")
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0.000");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0.000");
        }
        else if (displ == "4")
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0.0000");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0.0000");
        }
        else if (displ == "5")
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0.00000");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0.00000");
        }
        else if (displ == "6")
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0.000000");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0.000000");
        }
        else if (displ == "7")
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0.0000000");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0.0000000");
        }
        else if (displ == "8")
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0.00000000");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0.00000000");
        }
        else
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0");
        }

        //+++++++++++++++++++++++++++++++++++++
        if (ddlAddGSTFooter.SelectedIndex > 0)
        {
            float taxAddamount1;
            string ddlgst1 = "";
            DropDownList ddlAddGSTFooter1 = (row.FindControl("ddlAddGSTEdit") as DropDownList);
            TextBox lblTotalAmountFooter1 = (row.FindControl("txtTotalAmount") as TextBox);
            Label lblGSTAmountFooter = (row.FindControl("lblGSTAmountEdit") as Label);
            //lblGSTAmountFooter
            if (ddlAddGSTFooter1.SelectedIndex != 0)
            {
                DataSet dsgst = dml.Find("SELECT SUBSTRING('" + ddlAddGSTFooter1.SelectedItem.Text + "', CHARINDEX('(', '" + ddlAddGSTFooter1.SelectedItem.Text + "') + 1, CHARINDEX(')', '" + ddlAddGSTFooter1.SelectedItem.Text + "') - CHARINDEX('(', '" + ddlAddGSTFooter1.SelectedItem.Text + "') - 1) AS output");
                if (dsgst.Tables[0].Rows.Count > 0)
                {
                    ddlgst1 = dsgst.Tables[0].Rows[0]["output"].ToString();
                }
                string valuelbl = (row.FindControl("txtValueEdit") as TextBox).Text;
                if (valuelbl.Length > 0 && ddlAddGSTFooter1.SelectedIndex > 0)
                {
                    taxAddamount1 = float.Parse(valuelbl) * (float.Parse(ddlgst1) / 100);
                }
                else
                {
                    if (ddlAddGSTFooter1.SelectedIndex > 0)
                    {
                        taxAddamount1 = (float.Parse(ddlgst1) / 100);
                    }
                    else
                    {
                        taxAddamount1 = float.Parse(valuelbl);
                    }
                }

                // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
                lblAddtaxamount.Text = taxAddamount1.ToString();

                float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);
                lblTotalAmountFooter1.Text = toalamt.ToString();
                (row.FindControl("txtBalanceEdit") as TextBox).Text = lbltaxamount.Text;
                lblGSTAmountFooter.Text = toalamt.ToString();
                dislaydigit_label(row, "lblGSTAmountEdit", toalamt);
                txtBillBal.Text = lbltaxamount.Text;
                (row.FindControl("txtBalanceTax") as TextBox).Text = toalamt.ToString();
                float balamount = toalamt + float.Parse(valuelbl);
                (row.FindControl("txtBalanceEdit") as TextBox).Text = balamount.ToString();
                txtBillBal.Text = balamount.ToString();
                lblTotalAmountFooter1.Text = balamount.ToString();

                dislaydigit_textbox(txtBillBal, balamount);
                dislaydigit_fortextbox(row, "txtTotalAmount", balamount);
                dislaydigit_fortextbox(row, "txtBalanceTax", toalamt);
                dislaydigit_fortextbox(row, "txtBalanceEdit", balamount);


            }
            else
            {
                string valuelbl = (row.FindControl("txtValueEdit") as TextBox).Text;
                if (valuelbl.Length > 0 && ddlAddGSTFooter1.SelectedIndex == 0)
                {
                    taxAddamount = float.Parse(valuelbl) * 0;
                }
                else
                {
                    if (ddlAddGSTFooter1.SelectedIndex > 0)
                    {
                        taxAddamount = (float.Parse(ddlgst1) / 100);
                    }
                    else
                    {
                        taxAddamount = float.Parse(valuelbl);
                    }
                }

                // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
                lblAddtaxamount.Text = taxAddamount.ToString();

                float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);
                lblTotalAmountFooter1.Text = toalamt.ToString();


                (row.FindControl("txtBalanceEdit") as TextBox).Text = lbltaxamount.Text;
                txtBillBal.Text = lbltaxamount.Text;
                //  (row.FindControl("lblBalanceTaxFooter") as TextBox).Text = taxAddamount.ToString();
                float balamount = toalamt + float.Parse(valuelbl);
                (row.FindControl("txtBalanceEdit") as TextBox).Text = balamount.ToString();
                txtBillBal.Text = balamount.ToString();
                lblTotalAmountFooter1.Text = balamount.ToString();

                dislaydigit_fortextbox(row, "txtTotalAmount", balamount);
                dislaydigit_fortextbox(row, "txtBalanceEdit", toalamt);
                dislaydigit_fortextbox(row, "txtBalanceEdit", balamount);

            }
        }
        //+++++++++++++++++++++++++++++++++++++++








        //====================================================================
    }
    protected void ddlGST6Edit_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((DropDownList)sender).NamingContainer;
        float taxAddamount;
        string ddlgst = "";
        DropDownList ddlAddGSTFooter = (row.FindControl("ddlGST6Edit") as DropDownList);
        TextBox lblTotalAmountFooter = (row.FindControl("txtTotalAmount") as TextBox);

        TextBox lblBalanceAmtAmount = (row.FindControl("txtBalanceEdit") as TextBox);



        if (ddlAddGSTFooter.SelectedIndex != 0)
        {
            DataSet dsgst = dml.Find("SELECT SUBSTRING('" + ddlAddGSTFooter.SelectedItem.Text + "', CHARINDEX('(', '" + ddlAddGSTFooter.SelectedItem.Text + "') + 1, CHARINDEX(')', '" + ddlAddGSTFooter.SelectedItem.Text + "') - CHARINDEX('(', '" + ddlAddGSTFooter.SelectedItem.Text + "') - 1) AS output");
            if (dsgst.Tables[0].Rows.Count > 0)
            {
                ddlgst = dsgst.Tables[0].Rows[0]["output"].ToString();
            }
            string valuelbl = (row.FindControl("txtValueEdit") as TextBox).Text;


            if (valuelbl.Length > 0 && ddlAddGSTFooter.SelectedIndex > 0)
            {
                taxAddamount = float.Parse(valuelbl) * (float.Parse(ddlgst) / 100);
            }
            else
            {
                if (ddlAddGSTFooter.SelectedIndex > 0)
                {
                    taxAddamount = (float.Parse(ddlgst) / 100);
                }
                else
                {
                    taxAddamount = float.Parse(valuelbl);
                }
            }

            // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
            lbltaxamount.Text = taxAddamount.ToString();

            float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);


            (row.FindControl("lblGSTAmountEdit") as Label).Text = lbltaxamount.Text;
            //(row.FindControl("lblBalanceAmtAmount") as Label).Text= lbltaxamount.Text;

            (row.FindControl("txtBalanceTax") as TextBox).Text = toalamt.ToString();
            float balamount = toalamt + float.Parse(valuelbl);
            (row.FindControl("txtBalanceEdit") as TextBox).Text = balamount.ToString();
            txtBillBal.Text = balamount.ToString();

            dislaydigit_fortextbox(row, "txtBalanceTax", toalamt);
            dislaydigit_fortextbox(row, "txtBalanceEdit", balamount);
            dislaydigit_fortextbox(row, "txtValueEdit", float.Parse(valuelbl));


            float total_amount = toalamt + float.Parse(lblTotalAmountFooter.Text);

            lblTotalAmountFooter.Text = total_amount.ToString();
        }
        else
        {
            string valuelbl = (row.FindControl("txtValueEdit") as Label).Text;
            if (valuelbl.Length > 0 && ddlAddGSTFooter.SelectedIndex == 0)
            {
                taxAddamount = float.Parse(valuelbl) * 0;
            }
            else
            {
                if (ddlAddGSTFooter.SelectedIndex > 0)
                {
                    taxAddamount = (float.Parse(ddlgst) / 100);
                }
                else
                {
                    taxAddamount = float.Parse(valuelbl);
                }
            }

            // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
            lbltaxamount.Text = taxAddamount.ToString();

            float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);
            lblTotalAmountFooter.Text = toalamt.ToString();

            (row.FindControl("lblGSTAmountEdit") as Label).Text = lbltaxamount.Text;
            // (row.FindControl("lblBalanceAmtAmount") as Label).Text = lbltaxamount.Text;

            (row.FindControl("txtBalanceTax") as TextBox).Text = toalamt.ToString();
            float balamount = toalamt + float.Parse(valuelbl);
            (row.FindControl("txtBalanceEdit") as TextBox).Text = balamount.ToString();
            txtBillBal.Text = balamount.ToString();

            float total_amount = toalamt + float.Parse(lblTotalAmountFooter.Text);

            lblTotalAmountFooter.Text = total_amount.ToString();
            dislaydigit_fortextbox(row, "txtBalanceTax", toalamt);
            dislaydigit_fortextbox(row, "txtBalanceEdit", balamount);
            dislaydigit_fortextbox(row, "txtValueEdit", float.Parse(valuelbl));
        }

        //(row.FindControl("lblBalanceTaxFooter") as TextBox).Text = lbltaxamount.Text;

        string flblGSTAmount = (row.FindControl("lblGSTAmountEdit") as Label).Text;
        float faltotalamount = float.Parse(lblTotalAmountFooter.Text);
        float faflblGSTAmount = float.Parse(lbltaxamount.Text);





        string displ = inv.displaydigit(branchId().ToString());

        if (displ == "0")
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0");
        }

        else if (displ == "1")
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0.0");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0.0");
        }
        else if (displ == "2")
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0.00");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0.00");
        }
        else if (displ == "3")
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0.000");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0.000");
        }
        else if (displ == "4")
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0.0000");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0.0000");
        }
        else if (displ == "5")
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0.00000");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0.00000");
        }
        else if (displ == "6")
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0.000000");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0.000000");
        }
        else if (displ == "7")
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0.0000000");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0.0000000");
        }
        else if (displ == "8")
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0.00000000");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0.00000000");
        }
        else
        {

            (row.FindControl("lblGSTAmountEdit") as Label).Text = faflblGSTAmount.ToString("0");
            (row.FindControl("txtTotalAmount") as TextBox).Text = faltotalamount.ToString("0");
        }

        //+++++++++++++++++++++++++++++++++++++
        if (ddlAddGSTFooter.SelectedIndex > 0)
        {
            float taxAddamount1;
            string ddlgst1 = "";
            DropDownList ddlAddGSTFooter1 = (row.FindControl("ddlAddGSTEdit") as DropDownList);
            TextBox lblTotalAmountFooter1 = (row.FindControl("txtTotalAmount") as TextBox);
            Label lblGSTAmountFooter = (row.FindControl("lblGSTAmountEdit") as Label);
            //lblGSTAmountFooter
            if (ddlAddGSTFooter1.SelectedIndex != 0)
            {
                DataSet dsgst = dml.Find("SELECT SUBSTRING('" + ddlAddGSTFooter1.SelectedItem.Text + "', CHARINDEX('(', '" + ddlAddGSTFooter1.SelectedItem.Text + "') + 1, CHARINDEX(')', '" + ddlAddGSTFooter1.SelectedItem.Text + "') - CHARINDEX('(', '" + ddlAddGSTFooter1.SelectedItem.Text + "') - 1) AS output");
                if (dsgst.Tables[0].Rows.Count > 0)
                {
                    ddlgst1 = dsgst.Tables[0].Rows[0]["output"].ToString();
                }
                string valuelbl = (row.FindControl("txtValueEdit") as TextBox).Text;
                if (valuelbl.Length > 0 && ddlAddGSTFooter1.SelectedIndex > 0)
                {
                    taxAddamount1 = float.Parse(valuelbl) * (float.Parse(ddlgst1) / 100);
                }
                else
                {
                    if (ddlAddGSTFooter1.SelectedIndex > 0)
                    {
                        taxAddamount1 = (float.Parse(ddlgst1) / 100);
                    }
                    else
                    {
                        taxAddamount1 = float.Parse(valuelbl);
                    }
                }

                // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
                lblAddtaxamount.Text = taxAddamount1.ToString();

                float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);
                lblTotalAmountFooter1.Text = toalamt.ToString();
                (row.FindControl("txtBalanceEdit") as TextBox).Text = lbltaxamount.Text;
                lblGSTAmountFooter.Text = toalamt.ToString();
                dislaydigit_label(row, "lblGSTAmountEdit", toalamt);
                txtBillBal.Text = lbltaxamount.Text;
                (row.FindControl("txtBalanceTax") as TextBox).Text = toalamt.ToString();
                float balamount = toalamt + float.Parse(valuelbl);
                (row.FindControl("txtBalanceEdit") as TextBox).Text = balamount.ToString();
                txtBillBal.Text = balamount.ToString();
                lblTotalAmountFooter1.Text = balamount.ToString();

                dislaydigit_textbox(txtBillBal, balamount);
                dislaydigit_fortextbox(row, "txtTotalAmount", balamount);
                dislaydigit_fortextbox(row, "txtBalanceTax", toalamt);
                dislaydigit_fortextbox(row, "txtBalanceEdit", balamount);


            }
            else
            {
                string valuelbl = (row.FindControl("txtValueEdit") as TextBox).Text;
                if (valuelbl.Length > 0 && ddlAddGSTFooter1.SelectedIndex == 0)
                {
                    taxAddamount = float.Parse(valuelbl) * 0;
                }
                else
                {
                    if (ddlAddGSTFooter1.SelectedIndex > 0)
                    {
                        taxAddamount = (float.Parse(ddlgst1) / 100);
                    }
                    else
                    {
                        taxAddamount = float.Parse(valuelbl);
                    }
                }

                // (row.FindControl("lblRate2Footer") as TextBox).Text = taxamount.ToString();
                lblAddtaxamount.Text = taxAddamount.ToString();

                float toalamt = float.Parse(lblAddtaxamount.Text) + float.Parse(lbltaxamount.Text);
                lblTotalAmountFooter1.Text = toalamt.ToString();


                (row.FindControl("txtBalanceEdit") as TextBox).Text = lbltaxamount.Text;
                txtBillBal.Text = lbltaxamount.Text;
                //  (row.FindControl("lblBalanceTaxFooter") as TextBox).Text = taxAddamount.ToString();
                float balamount = toalamt + float.Parse(valuelbl);
                (row.FindControl("txtBalanceEdit") as TextBox).Text = balamount.ToString();
                txtBillBal.Text = balamount.ToString();
                lblTotalAmountFooter1.Text = balamount.ToString();

                dislaydigit_fortextbox(row, "txtTotalAmount", balamount);
                dislaydigit_fortextbox(row, "txtBalanceEdit", toalamt);
                dislaydigit_fortextbox(row, "txtBalanceEdit", balamount);

            }
        }
        //+++++++++++++++++++++++++++++++++++++++







    }
}
