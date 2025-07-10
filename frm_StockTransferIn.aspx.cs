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
    int DateFrom, EditDays, DeleteDays, AddDays;
    string userid, UserGrpID, FormID, fiscal, menuid;
    int valid, showd;
    string[] supplier = new string[4];

    DmlOperation dml = new DmlOperation();
    radcomboxclass cmb = new radcomboxclass();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
    FieldHide fd = new FieldHide();
    GLEntry gl = new GLEntry();
  
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
            CalendarExtender1.EndDate = DateTime.Now;
            string sd = fiscalstart(fiscal);
            CalendarExtender1.StartDate = DateTime.Parse(sd);

            dml.dropdownsql(ddlSupplier, "ViewSupplierId", "BPartnerName", "BPartnerId");
            dml.dropdownsql(ddlStatus, "SET_Status", "StatusName", "StatusId");
            string sdate = startdate(fiscal);
            string enddate = Enddate(fiscal);
            dml.dropdownsqlwithquery(ddlDocName, "select DocID,DocDescription from SET_Documents where docid in (select DocID from SET_UserGrp_Documents where UserGrpId = '" + UserGrpID + "' AND	(( StartDate <= '" + sdate + "') AND ((EndDate >= '" + enddate + "') OR (EndDate IS NULL))) and Record_Deleted = 0) and CompID = '" + compid() + "' and Record_Deleted = 0 and MenuId_Sno = '" + menuid + "' and FormId_Sno='" + FormID + "'", "DocDescription", "DocID");

            
            dml.dropdownsql(ddlDocAuth, "SET_Authority", "AuthorityName", "AuthorityId");
            dml.dropdownsql(ddlLocationTo, "SET_Department", "DepartmentName", "DepartmentID", "IsWarehouse", "1");
            dml.dropdownsql(ddlLocationFrom, "SET_Department", "DepartmentName", "DepartmentID", "IsWarehouse", "1");
            dml.dropdownsql(ddlCostCenterTo, "SET_CostCenter", "CostCenterName", "CostCenterID");
            dml.dropdownsql(ddlCostCentrFrom, "SET_CostCenter", "CostCenterName", "CostCenterID");
            dml.dropdownsqlwithquery(ddlEdit_Document, "select SET_Documents.DocID as docs , * from SET_DocumentType LEFT JOIN SET_Documents on SET_DocumentType.DocTypeId = SET_Documents.DocTypeId where SET_DocumentType.DocTypeId in( select DocTypeId from SET_Documents where MenuId_Sno = '" + menuid + "' and FormId_Sno = '" + FormID + "')", "DocName", "docs");
            dml.dropdownsqlwithquery(ddlDel_Document, "select SET_Documents.DocID as docs , * from SET_DocumentType LEFT JOIN SET_Documents on SET_DocumentType.DocTypeId = SET_Documents.DocTypeId where SET_DocumentType.DocTypeId in( select DocTypeId from SET_Documents where MenuId_Sno = '" + menuid + "' and FormId_Sno = '" + FormID + "')", "DocName", "docs");
            dml.dropdownsqlwithquery(ddlFind_Document, "select SET_Documents.DocID as docs , * from SET_DocumentType LEFT JOIN SET_Documents on SET_DocumentType.DocTypeId = SET_Documents.DocTypeId where SET_DocumentType.DocTypeId in( select DocTypeId from SET_Documents where MenuId_Sno = '" + menuid + "' and FormId_Sno = '" + FormID + "')", "DocName", "docs");
            dml.dropdownsqlwithquery(ddlEdit_DocNO, "SELECT DocumentNo,Sno from Inv_InventoryInMaster", "DocumentNo", "Sno");
            dml.dropdownsqlwithquery(ddlFind_DocNO, "SELECT DocumentNo,Sno from Inv_InventoryInMaster", "DocumentNo", "Sno");
            dml.dropdownsqlwithquery(ddlDel_DocNO, "SELECT DocumentNo,Sno from Inv_InventoryInMaster", "DocumentNo", "Sno");
            dml.dropdownsql(ddlFind_Status, "SET_Status", "StatusName", "StatusId");
            dml.dropdownsql(ddlEdit_Status, "SET_Status", "StatusName", "StatusId");
            dml.dropdownsql(ddlDel_Status, "SET_Status", "StatusName", "StatusId");
            dml.dropdownsql(ddlFind_Department, "SET_Department", "DepartmentName", "DepartmentID");
            dml.dropdownsql(ddlEdit_Depart, "SET_Department", "DepartmentName", "DepartmentID");
            dml.dropdownsql(ddlDel_Department, "SET_Department", "DepartmentName", "DepartmentID");
            dml.dropdownsqlwithquery(ddltransfer, "select Sno, DocumentNo from Inv_InventoryOutMaster where docid = 10 and Record_Deleted = 0 and IsActive = 1 and Status = 1 ORDER BY DocumentNo", "DocumentNo", "Sno");
            
            textClear();
            Div1.Visible = false;
            Div2.Visible = false;
            Div7.Visible = false;
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
        ddlDocName.Enabled = true;
        doctype(menuid, FormID, UserGrpID);
        UserGrpID = Request.QueryString["UsergrpID"];
        FormID = Request.QueryString["FormID"];
        dml.dateRuleForm(UserGrpID, FormID, CalendarExtender1, "A");
        fiscal = Request.QueryString["fiscaly"];
        string sdate = startdate(fiscal);
        string enddate = Enddate(fiscal);
        dml.dropdownsqlwithquery(ddltransfer, "select Sno, DocumentNo from Inv_InventoryOutMaster where docid = 10 and Record_Deleted = 0 and IsActive = 1 and Status = 1 ORDER BY DocumentNo", "DocumentNo", "Sno");
        ddltransfer.SelectedIndex = 0;
        ddlDocName_SelectedIndexChanged(sender, e);

    }
    protected void btnUpdatePO_Click(object sender, EventArgs e)
    {


    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        string vno = txtDocNo.Text, billno = txtbillNo.Text, billdate = txtBilldate.Text, shopname= txtShopOffName.Text;

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
        if(ddlLocationFrom.SelectedIndex != 0)
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




        string trantype = "", doctype = "0";
        DataSet dsCRDR = dml.Find("select  Tran_Type from SET_Acct_Type where Acct_Type_Id in ( SELECT Acct_Type_ID from SET_COA_detail where Acct_Code = '"+RadComboAcct_Code.Text+ "');select DocTypeId from SET_Documents where DocID = '"+ddlDocName.SelectedItem.Value+"';");
        if (dsCRDR.Tables[0].Rows.Count > 0)
        {
            trantype = dsCRDR.Tables[0].Rows[0]["Tran_Type"].ToString();
            doctype = dsCRDR.Tables[1].Rows[0]["DocTypeId"].ToString();

        }

        DataSet ds;
        if (trantype == "Debit")
        {
            ds = dml.Find("INSERT INTO Inv_InventoryInMaster ([DocId], [DocType], [EntryDate], [DocumentNo],  [DocumentAuthority], [LocationFrom], [LocationTo], [CostCenter], [CostCenter2], [Status], [Remarks], [DrAccountCode], [CrAccountCode], [GLReferNo], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted]) VALUES ('" + ddlDocName.SelectedItem.Value + "', '"+ doctype + "', '" + txtEntryDate.Text + "', '" + required_generateforIns() + "',  '" + ddlDocAuth.SelectedItem.Value + "', '" + lf + "', '" + lt + "', '" + cf + "', '" + ct + "', '" + ddlStatus.SelectedItem.Value + "','" + txtRemarks.Text + "', '" + RadComboAcct_Code.Text+ "', NULL, NULL, '" + chk + "'," + gocid() + "," + compid() + "," + branchId() + "," + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0');  SELECT * FROM Inv_InventoryInMaster WHERE Sno = SCOPE_IDENTITY()");

        }
        else
        {
            ds = dml.Find("INSERT INTO Inv_InventoryInMaster ([DocId], [DocType], [EntryDate], [DocumentNo], [DocumentAuthority],  [LocationFrom], [LocationTo], [CostCenter], [CostCenter2], [Status], [Remarks], [DrAccountCode], [CrAccountCode], [GLReferNo], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted]) VALUES ('" + ddlDocName.SelectedItem.Value + "', '"+ doctype + "', '" + txtEntryDate.Text + "', '" + required_generateforIns() + "', '" + ddlDocAuth.SelectedItem.Value + "', '" + lf + "', '" + lt + "', '" + cf + "', '" + ct + "', '" + ddlStatus.SelectedItem.Value + "','" + txtRemarks.Text + "', NULL, '"+ RadComboAcct_Code.Text + "', NULL, '" + chk + "'," + gocid() + "," + compid() + "," + branchId() + "," + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0');  SELECT * FROM Inv_InventoryInMaster WHERE Sno = SCOPE_IDENTITY()");
        }


        

        if (ds.Tables[0].Rows.Count > 0)
        {
            ids = ds.Tables[0].Rows[0]["Sno"].ToString();
            ViewState["detailid"] = ids;
        }

        
            detailinsert(ids);


        
            //detailinsertNOrmal(ids);
        

        gl.GlentrySummaryDetail(show_username(), DateTime.Now.ToString(), "0",txtDocNo.Text);

        dml.Update("update Inv_InventoryOutMaster set Status = 6 where DocumentNo = '"+ddltransfer.SelectedItem.Text+"'", "");

        dml.Update("UPDATE Inv_InventoryOutMaster set ReferNo = '" + ids + "' where DocumentNo = '" + ddltransfer.SelectedItem.Text + "'", "");
        ////  detaisaveforAFQ(ids);
        textClear();
        btnInsert_Click(sender, e);
        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);
       Response.Redirect("frm_JV_Diplay.aspx?UserID="+userid+"&UsergrpID="+UserGrpID+"&fiscaly="+fiscal+"&FormID="+FormID+"&Menuid="+menuid+"&VoucherNo="+vno+"&bilno="+billno+"&billdate="+billdate+"&shopname="+shopname+ "");
       ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);

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
            DataSet ds_up = dml.Find("select * from Inv_InventoryInMaster WHERE ([DocId]='" + ddlDocName.SelectedItem.Value + "') AND ([EntryDate]='" + dateEntry + "') AND ([DocumentNo]='" + txtDocNo.Text + "') AND ([DocumentAuthority]='" + ddlDocAuth.SelectedItem.Value + "') AND ([LocationFrom]='" + ddlLocationFrom.SelectedItem.Value + "') AND ([LocationTo]='" + ddlLocationTo.SelectedItem.Value + "') AND ([CostCenter]='" + ddlCostCentrFrom.SelectedItem.Value + "') AND ([CostCenter2]='" + ddlCostCenterTo.SelectedItem.Value + "') AND ([Status]='" + ddlStatus.SelectedItem.Value + "') AND ([Remarks]='" + txtRemarks.Text + "') AND ([CrAccountCode] IS NULL) AND ([DrAccountCode]='" + RadComboAcct_Code.Text + "') AND ([GLReferNo] IS NULL) AND ([IsActive]='" + chk + "') AND ([GocID]='" + gocid() + "') AND ([CompId]='" + compid() + "') AND ([BranchId]='" + branchId() + "') AND ([FiscalYearID]='" + FiscalYear() + "') AND ([Record_Deleted]='0')");

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

                dml.Update("UPDATE Inv_InventoryInMaster SET  [DocId]='" + ddlDocName.SelectedItem.Value + "', [DocType]=NULL, [EntryDate]='" + dateEntry + "', [DocumentNo]='" + txtDocNo.Text + "',  [DocumentAuthority]='" + ddlDocAuth.SelectedItem.Value + "',  [LocationFrom]='" + ddlLocationFrom.SelectedItem.Value + "', [LocationTo]='" + ddlLocationTo.SelectedItem.Value + "', [CostCenter]='" + ddlCostCentrFrom.SelectedItem.Value + "', [CostCenter2]='" + ddlCostCenterTo.SelectedItem.Value + "', [Status]='" + ddlStatus.SelectedItem.Value + "', [Remarks]='" + txtRemarks.Text + "',  [CrAccountCode]=NULL, [DrAccountCode]='" + RadComboAcct_Code.Text + "', [GLReferNo]=NULL, [IsActive]='" + chk + "', [GocID]='" + gocid() + "', [CompId]='" + compid() + "', [BranchId]='" + branchId() + "', [FiscalYearID]='" + FiscalYear() + "', [UpdatedBy]='" + show_username() + "', [UpdatedDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', [Record_Deleted]='0' WHERE ([Sno]='" + ViewState["SNO"].ToString() + "') ", "");
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
            DataSet ds_up = dml.Find("select * from Inv_InventoryInMaster WHERE ([DocId]='" + ddlDocName.SelectedItem.Value + "') AND ([EntryDate]='" + dateEntry + "') AND ([DocumentNo]='" + txtDocNo.Text + "') AND ([DocumentAuthority]='" + ddlDocAuth.SelectedItem.Value + "')  AND ([LocationFrom]='" + ddlLocationFrom.SelectedItem.Value + "') AND ([LocationTo]='" + ddlLocationTo.SelectedItem.Value + "') AND ([CostCenter]='" + ddlCostCentrFrom.SelectedItem.Value + "') AND ([CostCenter2]='" + ddlCostCenterTo.SelectedItem.Value + "') AND ([Status]='" + ddlStatus.SelectedItem.Value + "') AND ([Remarks]='" + txtRemarks.Text + "') AND ([DrAccountCode] IS NULL) AND ([CrAccountCode]='" + RadComboAcct_Code.Text + "') AND ([GLReferNo] IS NULL) AND ([IsActive]='" + chk + "') AND ([GocID]='" + gocid() + "') AND ([CompId]='" + compid() + "') AND ([BranchId]='" + branchId() + "') AND ([FiscalYearID]='" + FiscalYear() + "') AND ([Record_Deleted]='0')");

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
                dml.Delete("update Act_GL_Detail set Record_Deleted = 1 where MasterSno= " + ViewState["SNO"].ToString() + "", "");
                dml.Delete("INSERT INTO Act_GL_Detail_Deleted ([DocId], [DocDescription], [MasterSno], [DetailSno], [EntryDate], [VoucherNo], [AccountCode], [BPNatureID], [BPartnerId], [Name], [ChqNo], [ChqDate], [InvoiceNo], [InvoiceDate], [DrAmount], [CrAmount], [Narration], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [Record_Deleted]) SELECT [DocId], [DocDescription], [MasterSno], [DetailSno], [EntryDate], [VoucherNo], [AccountCode], [BPNatureID], [BPartnerId], [Name], [ChqNo], [ChqDate], [InvoiceNo], [InvoiceDate], [DrAmount], [CrAmount], [Narration], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [Record_Deleted] FROM Act_GL_Detail WHERE MasterSno='" + ViewState["SNO"].ToString() + "';", "");

                dml.Delete("Delete from Act_GL_Detail where MasterSno = " + ViewState["SNO"].ToString() + "", "");

                
                dml.Update("UPDATE Inv_InventoryInMaster SET  [DocId]='" + ddlDocName.SelectedItem.Value + "', [DocType]=NULL, [EntryDate]='" + dateEntry + "', [DocumentNo]='" + txtDocNo.Text + "', [DocumentAuthority]='" + ddlDocAuth.SelectedItem.Value + "', [LocationFrom]='" +lf + "', [LocationTo]='" + lt+ "', [CostCenter]='" + cf+ "', [CostCenter2]='" + ct + "', [Status]='" + ddlStatus.SelectedItem.Value + "', [Remarks]='" + txtRemarks.Text + "', [DrAccountCode]=NULL, [CrAccountCode]='" + RadComboAcct_Code.Text + "', [GLReferNo]=NULL, [IsActive]='" + chk + "', [GocID]='" + gocid() + "', [CompId]='" + compid() + "', [BranchId]='" + branchId() + "', [FiscalYearID]='" + FiscalYear() + "', [UpdatedBy]='" + show_username() + "', [UpdatedDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', [Record_Deleted]='0' WHERE ([Sno]='" + ViewState["SNO"].ToString() + "') ", "");



                foreach (GridViewRow g in GridView4.Rows)
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
                    gl.GlentryInsertofTransferIN(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, ViewState["SNO"].ToString(), "0", dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, RadComboAcct_Code.Text, txtShopOffName.Text, txtDeliveryChallan.Text, txtDeliveryChallanDate.Text, totalcrdb.ToString(), "0", txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff"), "0", "0", "0", "Inv_InventoryInDetail");
                }
                else
                {
                    gl.GlentryInsertofTransferIN(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, ViewState["SNO"].ToString(), "0", dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, RadComboAcct_Code.Text, txtShopOffName.Text, txtDeliveryChallan.Text, txtDeliveryChallanDate.Text, "0", totalcrdb.ToString(), txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff"), "0", "0", "0", "Inv_InventoryInDetail");
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
            string squer = "select * from ViewForFUD_GRNInvIn";


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
                swhere = swhere + " and DocumentNo = '" + ddlDel_DocNO.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and DocumentNo is not null";
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

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and  [DocID] = '9' and    [GocID] = '" + gocid() + "' and [BranchId]='" + branchId() + "' and  [FiscalYearID] = '" + FiscalYear() + "' and CompId = '" + compid() + "'  ORDER BY DocDescription";

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
            GridView1.DataBind();
            string swhere = "";
            string squer = "select * from ViewForFUD_GRNInvIn";
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
                swhere = swhere + " and DocumentNo = '" + ddlFind_DocNO.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and DocumentNo is not null";
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

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and  [DocID] = '9' and    [GocID] = '" + gocid() + "' and [BranchId]='" + branchId() + "' and  [FiscalYearID] = '" + FiscalYear() + "' and CompId = '" + compid() + "'  ORDER BY DocDescription";

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
            string squer = "select * from ViewForFUD_GRNInvIn";


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
                swhere = swhere + " and DocumentNo = '" + ddlEdit_DocNO.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and DocumentNo is not null";
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

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and  [DocID] = '9' and  [GocID] = '" + gocid() + "' and [BranchId]='" + branchId() + "' and  [FiscalYearID] = '" + FiscalYear() + "' and CompId = '" + compid() + "'  ORDER BY DocDescription";



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
        txttransferNo.Visible = false;
        ddltransfer.Visible = true;
        lstFruits.Enabled = false;
        lstFruits.ClearSelection();

        ddlDocName.SelectedIndex = 0;
        ddlSupplier.SelectedIndex = 0;
        ddlDocAuth.SelectedIndex = 0;
        ddlLocationFrom.SelectedIndex = 0;
        ddlLocationTo.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        ddlCostCentrFrom.SelectedIndex = 0;
        ddlCostCenterTo.SelectedIndex = 0;
        ddltransfer.SelectedIndex = 0;

        ddltransfer.Enabled = false;
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


                DataSet ds = dml.Find("Select balQty,Quantity from Set_PurchaseOrderDetail  where Sno = '" + lblSno.Text + "'");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    balqtyPO = ds.Tables[0].Rows[0]["balQty"].ToString();
                    qtydetail = ds.Tables[0].Rows[0]["Quantity"].ToString();
                }
                if (float.Parse(lblPObalQty.Text) <= float.Parse(qtydetail))
                {
                    newbalqtyPO = float.Parse(balqtyPO) + float.Parse(lblPObalQty.Text);

                    //lblPONo
                    dml.Update("Update Set_PurchaseOrderDetail set balQty='" + newbalqtyPO.ToString() + "' where Sno =  '" + lblSno.Text + "' ", "");

                    dml.Update("UPDATE Set_PurcahseOrderMaster set Status = '1' where Sno in (select Sno_Master from Set_PurchaseOrderDetail where Sno = '" + lblSno.Text + "')", "");
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
            dml.Delete("INSERT INTO Act_GL_Detail_Deleted ([DocId], [DocDescription], [MasterSno], [DetailSno], [EntryDate], [VoucherNo], [AccountCode], [BPNatureID], [BPartnerId], [Name], [ChqNo], [ChqDate], [InvoiceNo], [InvoiceDate], [DrAmount], [CrAmount], [Narration], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [Record_Deleted]) SELECT [DocId], [DocDescription], [MasterSno], [DetailSno], [EntryDate], [VoucherNo], [AccountCode], [BPNatureID], [BPartnerId], [Name], [ChqNo], [ChqDate], [InvoiceNo], [InvoiceDate], [DrAmount], [CrAmount], [Narration], [GocID], [CompId], [BranchId], [FiscalYearID], '"+show_username()+"', '"+DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")+"', [UpdatedBy], [UpdatedDate], '1' FROM Act_GL_Detail WHERE MasterSno='"+ ViewState["SNO"].ToString() + "';", "");
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

      
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        txttransferNo.Visible = true;
        txttransferNo.Enabled = false;
        ddltransfer.Visible = false;
        try
        {
            string serial_id;
            serial_id = GridView1.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;

            DataSet ds = dml.Find("select * from Inv_InventoryInMaster WHERE ([Sno]='" + serial_id + "') and Record_Deleted = '0'");
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlDocName.ClearSelection();
                ddlDocAuth.ClearSelection();
                ddlLocationFrom.ClearSelection();
                ddlLocationTo.ClearSelection();
                ddlStatus.ClearSelection();
                ddlCostCentrFrom.ClearSelection();
                ddlCostCenterTo.ClearSelection();

                ddlDocName.Items.FindByValue(ds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;
                ddlDocAuth.Items.FindByValue(ds.Tables[0].Rows[0]["DocumentAuthority"].ToString()).Selected = true;

                if (ds.Tables[0].Rows[0]["LocationFrom"].ToString() != "0")
                {
                    ddlLocationFrom.Items.FindByValue(ds.Tables[0].Rows[0]["LocationFrom"].ToString()).Selected = true;
                }
                if (ds.Tables[0].Rows[0]["LocationTo"].ToString() != "0")
                {
                    ddlLocationTo.Items.FindByValue(ds.Tables[0].Rows[0]["LocationTo"].ToString()).Selected = true;
                }
                if (ds.Tables[0].Rows[0]["CostCenter"].ToString() != "0")
                {
                    ddlCostCentrFrom.Items.FindByValue(ds.Tables[0].Rows[0]["CostCenter"].ToString()).Selected = true;
                }
                if (ds.Tables[0].Rows[0]["CostCenter2"].ToString() != "0")
                {
                    ddlCostCenterTo.Items.FindByValue(ds.Tables[0].Rows[0]["CostCenter2"].ToString()).Selected = true;
                }

                ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["Status"].ToString()).Selected = true;
                txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtDocNo.Text = ds.Tables[0].Rows[0]["DocumentNo"].ToString();

                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();

                txtCreateddate.Text = dml.dateConvert(ds.Tables[0].Rows[0]["CreatedDate"].ToString()) + " " + dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());

                dml.dateConvert(txtEntryDate);

                DataSet dsref = dml.Find("select DocumentNo from Inv_InventoryOutMaster where ReferNo = '" + ViewState["SNO"].ToString() + "'");
                if (dsref.Tables[0].Rows.Count > 0)
                {
                    txttransferNo.Text = dsref.Tables[0].Rows[0]["DocumentNo"].ToString();
                }

                string drcode = ds.Tables[0].Rows[0]["DrAccountCode"].ToString();
                string Crcode = ds.Tables[0].Rows[0]["CrAccountCode"].ToString();

                if (drcode != "")
                {
                    RadComboAcct_Code.Text = drcode;
                }

                if (Crcode != "")
                {
                    RadComboAcct_Code.Text = Crcode;
                }


                bool chkActive_b = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());

                if (chkActive_b == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }

                lstFruits.Visible = false;

                DataTable dtup = new DataTable();
                dtup.Columns.AddRange(new DataColumn[14] { new DataColumn("DrAccountCode"), new DataColumn("PONO1"), new DataColumn("ItemSubHeadName"), new DataColumn("Description"), new DataColumn("UOMName"), new DataColumn("Rate"), new DataColumn("Value"), new DataColumn("CostCenterName"), new DataColumn("LocName"), new DataColumn("Project"), new DataColumn("Warrentee"), new DataColumn("uomName2"), new DataColumn("Qty2"), new DataColumn("Rate2") });

                DataSet ds_detail = dml.Find("select * from ViewInvINDetail_FED where Sno_Master = '" + serial_id + "'");
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
                Div5.Visible = false;
                PopulateGridview_Up();

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
        btnShowJV.Visible = true;
        txtUpdateDate.Enabled = false;

        textClear();
        btnShowJV.Visible = true;

        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        DeleteBox.Visible = false;
        txttransferNo.Visible = true;
        txttransferNo.Enabled = false;
        ddltransfer.Visible = false;
        try
        {
            string serial_id;
            serial_id = GridView2.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;

            DataSet ds = dml.Find("select * from Inv_InventoryInMaster WHERE ([Sno]='" + serial_id + "') and Record_Deleted = '0'");
            if (ds.Tables[0].Rows.Count > 0)
            {
             
                ddlDocName.ClearSelection();
                ddlDocAuth.ClearSelection();
                ddlLocationFrom.ClearSelection();
                ddlLocationTo.ClearSelection();
                ddlStatus.ClearSelection();
                ddlCostCentrFrom.ClearSelection();
                ddlCostCenterTo.ClearSelection();
                ddlDocName.Items.FindByValue(ds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;
                ddlDocAuth.Items.FindByValue(ds.Tables[0].Rows[0]["DocumentAuthority"].ToString()).Selected = true;
               
                ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["Status"].ToString()).Selected = true;
               
                if (ds.Tables[0].Rows[0]["LocationFrom"].ToString() != "0")
                {
                    ddlLocationFrom.Items.FindByValue(ds.Tables[0].Rows[0]["LocationFrom"].ToString()).Selected = true;
                }
                if (ds.Tables[0].Rows[0]["LocationTo"].ToString() != "0")
                {
                    ddlLocationTo.Items.FindByValue(ds.Tables[0].Rows[0]["LocationTo"].ToString()).Selected = true;
                }
                if (ds.Tables[0].Rows[0]["CostCenter"].ToString() != "0")
                {
                    ddlCostCentrFrom.Items.FindByValue(ds.Tables[0].Rows[0]["CostCenter"].ToString()).Selected = true;
                }
                if (ds.Tables[0].Rows[0]["CostCenter2"].ToString() != "0")
                {
                    ddlCostCenterTo.Items.FindByValue(ds.Tables[0].Rows[0]["CostCenter2"].ToString()).Selected = true;
                }
                
                txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtDocNo.Text = ds.Tables[0].Rows[0]["DocumentNo"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                //txtCreateddate.Text = ds.Tables[0].Rows[0]["CreatedDate"].ToString();
                dml.dateConvert(txtEntryDate);
                DataSet dsref = dml.Find("select DocumentNo from Inv_InventoryOutMaster where ReferNo = '" + ViewState["SNO"].ToString() + "'");
                if (dsref.Tables[0].Rows.Count > 0)
                {
                    txttransferNo.Text = dsref.Tables[0].Rows[0]["DocumentNo"].ToString();
                }
                txtCreateddate.Text = dml.dateConvert(ds.Tables[0].Rows[0]["CreatedDate"].ToString()) + " " + dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                string drcode = ds.Tables[0].Rows[0]["DrAccountCode"].ToString();
                string Crcode = ds.Tables[0].Rows[0]["CrAccountCode"].ToString();

                if (drcode != "")
                {
                    RadComboAcct_Code.Text = drcode;
                }

                if (Crcode != "")
                {
                    RadComboAcct_Code.Text = Crcode;
                }


                bool chkActive_b = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
             
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
                    dtup.Columns.AddRange(new DataColumn[14] { new DataColumn("DrAccountCode"), new DataColumn("PONO1"), new DataColumn("ItemSubHeadName"), new DataColumn("Description"), new DataColumn("UOMName"), new DataColumn("Rate"), new DataColumn("Value"), new DataColumn("CostCenterName"), new DataColumn("LocName"), new DataColumn("Project"), new DataColumn("Warrentee"), new DataColumn("uomName2"), new DataColumn("Qty2"), new DataColumn("Rate2") });

                DataSet ds_detail = dml.Find("select * from ViewInvINDetail_FED where Sno_Master = '" + serial_id + "'");
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
                    Div5.Visible = false;
                    PopulateGridview_Up();

                

                UserGrpID = Request.QueryString["UsergrpID"];
                FormID = Request.QueryString["FormID"];
                dml.dateRuleForm(UserGrpID, FormID, CalendarExtender1, "D");
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
        btnUpdatePO.Visible = false;
        btnShowJV.Visible = true;
        Label1.Text = "";

        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        btnShowJV.Visible = true;

        ddlDocName.Enabled = false;
        ddlSupplier.Enabled = true;
        ddlDocAuth.Enabled = false;
        ddlLocationFrom.Enabled = true;
        ddlLocationTo.Enabled = true;
        ddlStatus.Enabled = false;
        ddlCostCentrFrom.Enabled = true;
        ddlCostCenterTo.Enabled = true;
        ddltransfer.Visible = false;
        txttransferNo.Visible = true;
        txttransferNo.Enabled = false;


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

            DataSet ds = dml.Find("select * from Inv_InventoryInMaster WHERE ([Sno]='" + serial_id + "') and Record_Deleted = '0'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDocName.ClearSelection();
                ddlDocAuth.ClearSelection();
                ddlLocationFrom.ClearSelection();
                ddlLocationTo.ClearSelection();
                ddlStatus.ClearSelection();
                ddlCostCentrFrom.ClearSelection();
                ddlCostCenterTo.ClearSelection();
                ddlDocName.Items.FindByValue(ds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;
                ddlDocAuth.Items.FindByValue(ds.Tables[0].Rows[0]["DocumentAuthority"].ToString()).Selected = true;
                ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["Status"].ToString()).Selected = true;
                if (ds.Tables[0].Rows[0]["LocationFrom"].ToString() != "0")
                {
                    ddlLocationFrom.Items.FindByValue(ds.Tables[0].Rows[0]["LocationFrom"].ToString()).Selected = true;
                }
                if (ds.Tables[0].Rows[0]["LocationTo"].ToString() != "0")
                {
                    ddlLocationTo.Items.FindByValue(ds.Tables[0].Rows[0]["LocationTo"].ToString()).Selected = true;
                }
                if (ds.Tables[0].Rows[0]["CostCenter"].ToString() != "0")
                {
                    ddlCostCentrFrom.Items.FindByValue(ds.Tables[0].Rows[0]["CostCenter"].ToString()).Selected = true;
                }
                if (ds.Tables[0].Rows[0]["CostCenter2"].ToString() != "0")
                {
                    ddlCostCenterTo.Items.FindByValue(ds.Tables[0].Rows[0]["CostCenter2"].ToString()).Selected = true;
                }

                txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtDocNo.Text = ds.Tables[0].Rows[0]["DocumentNo"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                txtbillNo.Text = ds.Tables[0].Rows[0]["BillNo"].ToString();
                DataSet dsref = dml.Find("select DocumentNo from Inv_InventoryOutMaster where ReferNo = '"+ViewState["SNO"].ToString()+"'");
                if (dsref.Tables[0].Rows.Count > 0)
                {
                    txttransferNo.Text = dsref.Tables[0].Rows[0]["DocumentNo"].ToString();
                }
                txtCreateddate.Text = dml.dateConvert(ds.Tables[0].Rows[0]["CreatedDate"].ToString()) + " " + dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());

               // dml.dateConvert(txtCreateddate);
                string drcode = ds.Tables[0].Rows[0]["DrAccountCode"].ToString();
                string Crcode = ds.Tables[0].Rows[0]["CrAccountCode"].ToString();

                if (drcode != "")
                {
                    RadComboAcct_Code.Text = drcode;
                }

                if (Crcode != "")
                {
                    RadComboAcct_Code.Text = Crcode;
                }
                

                bool chkActive_b = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
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
                    dtup.Columns.AddRange(new DataColumn[17] { new DataColumn("Sno"),new DataColumn("DrAccountCode"), new DataColumn("PONO1"), new DataColumn("ItemSubHeadName"), new DataColumn("Description"), new DataColumn("UOMName"), new DataColumn("pobal"), new DataColumn("pobal1"), new DataColumn("Rate"), new DataColumn("Value"), new DataColumn("CostCenterName"), new DataColumn("LocName"), new DataColumn("Project"), new DataColumn("Warrentee"), new DataColumn("uomName2"), new DataColumn("Qty2"), new DataColumn("Rate2") });
                    DataSet ds_detail = dml.Find("select * from ViewInvINDetail_FED where Sno_Master = '" + serial_id + "'");
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
                UserGrpID = Request.QueryString["UsergrpID"];
                FormID = Request.QueryString["FormID"];
                dml.dateRuleForm(UserGrpID, FormID, CalendarExtender1, "E");

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
        DataSet ds = dml.Find("select StartDate from SET_Fiscal_Year where FiscalYearId ="+ FiscalYear());
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
            Label lblItemSubHead = (Label)g.FindControl("lblItemSubHead");
            Label lblitemmaster = (Label)g.FindControl("lblItemMaster");
            Label lbluom = (Label)g.FindControl("lblUOM");
            Label lblPObalQty = (Label)g.FindControl("lblPObalQty");
            
            Label lblRate = (Label)g.FindControl("lblRate");
            Label lblValue = (Label)g.FindControl("lblValue");
          
            Label lblCostCenter = (Label)g.FindControl("lblCostCenter");
            Label lblLocation = (Label)g.FindControl("lblLocation");
            Label lblProject = (Label)g.FindControl("lblProject");

            Label lblWarrantee = (Label)g.FindControl("lblWarrantee");
            

            //
            //select ItemSubHeadID, ItemSubHeadName from SET_ItemSubHead lblproject

            DataSet ds = dml.Find("select ItemSubHeadID, ItemSubHeadName from SET_ItemSubHead where ItemSubHeadName = '" + lblItemSubHead.Text + "'");
            string subhead, itemmaster, uom1, uom2, costcenter, location,itemcode, itemtypeid;
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

            float balqty, balqty2;
            


         string dbcr_detail = "",dbcr_plusminus= "";
            string trantype = "";
            DataSet dsCRDR = dml.Find("select  Tran_Type from SET_Acct_Type where Acct_Type_Id in ( SELECT Acct_Type_ID from SET_COA_detail where Acct_Code = '" + lblAccountcode.Text + "')");
            if (dsCRDR.Tables[0].Rows.Count > 0)
            {
                trantype = dsCRDR.Tables[0].Rows[0]["Tran_Type"].ToString();

            }
            DataSet dsDeB;
            if (trantype == "Debit")
            {


                DataSet dsinsertdetail  = dml.Find("INSERT INTO Inv_InventoryInDetail ( [Sno_Master],  [DrAccountCode],  [ItemSubHead],"
                                       + " [ItemMaster], [UOM], [Quantity], [Rate], [Value], [Remarks], "
                                       + " [ReferNo], [GLReferNo], [CostCenter], [Location], [CompId], [BranchId], [FiscalYearID],"
                                       + " [CreatedBy], [CreatedDate], [Record_Deleted], [Project], [Warrentee]) "

                                       + "VALUES ('" + masterid + "', '" + lblAccountcode.Text + "', '" + subhead + "', '" + itemmaster + "', '" + uom1 + "', '" + lblPObalQty.Text + "',"
                                       + " '" + lblRate.Text + "', '" + lblValue.Text + "', NULL, NULL, NULL,"
                                       + " '" + costcenter + "', '" + location + "'," + compid() + "," + branchId() + "," + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0' ,"
                                       + " '" + lblProject.Text + "', '" + lblWarrantee.Text + "');  SELECT * FROM Inv_InventoryInDetail WHERE Sno = SCOPE_IDENTITY()");
             
                if (dsinsertdetail.Tables[0].Rows.Count > 0)
                {
                    detailid = dsinsertdetail.Tables[0].Rows[0]["Sno"].ToString();
                    
                }
                //DataSet dsnature = dml.Find("select  BPNatureID,BPNatureDescription from SET_BPartnerNature where BPNatureID in (select BPNatureID from SET_BPartnerType where BPartnerID = '"+ddlSupplier.SelectedItem.Value+ "')");
                //if(dsnature.Tables[0].Rows.Count> 0)
                //{
                //    bpnatureid = dsnature.Tables[0].Rows[0]["BPNatureID"].ToString();
                //}
                DataSet dsdebcr_detail = dml.Find("SELECT	GLImpact, InventoryImpact FROM SET_AcRules4Doc WHERE DocId = '" + ddlDocName.SelectedItem.Value + "' AND MasterDetail = 'D' AND CompID = '" + compid() + "' AND Record_Deleted = 0 AND IsActive = 1 AND IsHide = 0 ;");
                if (dsdebcr_detail.Tables[0].Rows.Count > 0)
                {
                    dbcr_detail = dsdebcr_detail.Tables[0].Rows[0]["GLImpact"].ToString();
                    dbcr_plusminus = dsdebcr_detail.Tables[0].Rows[0]["InventoryImpact"].ToString();
                }


                string CODEitemtype = gl.itemtypecode(dbcr_detail, itemtypeid);



                //gl Detail entry
                gl.GlentryInsertofTransferIN(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, masterid, detailid, dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, lblAccountcode.Text, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), lblValue.Text, "0", txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyy hh:mm:ss.ffff"), itemcode, lblPObalQty.Text, "0", "Inv_InventoryInDetail");

                //  dml.Update("update Set_QuotationReqMaster set Status = '6' where QuotationReqNO = '" + requisno.Text + "'", "");

            }
            else
            {
                DataSet dsinsertdetail = dml.Find("INSERT INTO Inv_InventoryInDetail ( [Sno_Master], [CrAccountCode], [ItemSubHead],"
                                     + " [ItemMaster], [UOM], [Quantity], [Rate], [Value], [Remarks],"
                                     + " [ReferNo], [GLReferNo], [CostCenter], [Location], [CompId], [BranchId], [FiscalYearID],"
                                     + " [CreatedBy], [CreatedDate], [Record_Deleted], [Project], [Warrentee]) "

                                     + "VALUES ('" + masterid + "', '" + lblAccountcode.Text + "', '" + subhead + "', '" + itemmaster + "', '" + uom1 + "', '" + lblPObalQty.Text + "',"
                                     + " '" + lblRate.Text + "', '" + lblValue.Text + "', NULL, NULL, NULL,"
                                     + " '" + costcenter + "', '" + location + "', " + compid() + ", " + branchId() + "," + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0' ,"
                                     + " '" + lblProject.Text + "', '" + lblWarrantee.Text + "');SELECT * FROM Inv_InventoryInDetail WHERE Sno = SCOPE_IDENTITY()");



               
                if (dsinsertdetail.Tables[0].Rows.Count > 0)
                {
                    detailid = dsinsertdetail.Tables[0].Rows[0]["Sno"].ToString();

                }


               
                DataSet dsdebcr_detail = dml.Find("SELECT	GLImpact, InventoryImpact FROM SET_AcRules4Doc WHERE DocId = '"+ddlDocName.SelectedItem.Value+"' AND MasterDetail = 'D' AND CompID = '"+compid()+"' AND Record_Deleted = 0 AND IsActive = 1 AND IsHide = 0 ;");
                if (dsdebcr_detail.Tables[0].Rows.Count > 0)
                {
                    dbcr_detail = dsdebcr_detail.Tables[0].Rows[0]["GLImpact"].ToString();
                    dbcr_plusminus = dsdebcr_detail.Tables[0].Rows[0]["InventoryImpact"].ToString();
                }


                string CODEitemtype = gl.itemtypecode(dbcr_detail, itemtypeid);
               
               
                //select ItemCode from SET_ItemMaster where ItemID = 1
                gl.GlentryInsertofTransferIN(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, masterid, detailid, dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, lblAccountcode.Text, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), "0", lblValue.Text, txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyy hh:mm:ss.ffff"), itemcode, lblPObalQty.Text, "0", "Inv_InventoryInDetail");

               

            }
            if(dbcr_plusminus == "PLUS")
            {
                totalcrdb = totalcrdb + float.Parse(lblValue.Text);
            }
            else if(dbcr_plusminus=="MINUS")
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
        DataSet dsdebcr = dml.Find("SELECT	GLImpact, InventoryImpact FROM SET_AcRules4Doc WHERE DocId = '"+ddlDocName.SelectedItem.Value+"' AND MasterDetail = 'M' AND CompID = 1 AND Record_Deleted = 0 AND IsActive = 1 AND IsHide = 0 ;");
        if (dsdebcr.Tables[0].Rows.Count > 0)
        {
            dbcr = dsdebcr.Tables[0].Rows[0]["GLImpact"].ToString();
        }
        if (dbcr == "Debit Impact")
        {
          //  gl.GlentryInsert(ddlDocName.SelectedItem.Value,  doctype,ddlDocName.SelectedItem.Text, masterid, "0", dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, RadComboAcct_Code.Text, bpnatureid, ddlSupplier.SelectedItem.Value, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), totalcrdb.ToString(), "0", txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff"), "0", "0", "0");

            string ids = gl.GlentryInsert_WITHID(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, masterid, "0", dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, RadComboAcct_Code.Text, "", "", txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), totalcrdb.ToString(), "0", txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff"), "0", "0", "0", "Inv_InventoryInMaster");

            dml.Update("UPDATE Inv_InventoryInMaster set GLReferNo = '" + ids + "' where = Sno = '" + masterid + "'", "");
            gl.fwdidIn(ids, masterid);

        }
        else
        {
           // gl.GlentryInsert(ddlDocName.SelectedItem.Value, doctype,ddlDocName.SelectedItem.Text, masterid, "0", dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, RadComboAcct_Code.Text, bpnatureid, ddlSupplier.SelectedItem.Value, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), "0", totalcrdb.ToString(), txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff"), "0", "0", "0");

            string ids = gl.GlentryInsert_WITHID(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, masterid, "0", dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, RadComboAcct_Code.Text, "", "", txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), "0", totalcrdb.ToString(), txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff"), "0", "0", "0", "Inv_InventoryInMaster");

            dml.Update("UPDATE Inv_InventoryInMaster set GLReferNo = '" + ids + "' where Sno = '" + masterid + "'", "");
            gl.fwdidIn(ids, masterid);

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




            dml.Insert("INSERT INTO Set_PurchaseOrderDetail ([Sno_Master], [PRNo_AQno], [ItemSubHead], [ItemMaster], [UOM], [Quantity], [Rate], [GST], [GSTRate], [QtyValue], [GstValue], [GrossValue], [Remarts], [Qty2], [UOM2], [Rate2], [Location], [Project], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted],[ApprovedQuantity]) VALUES ('" + id + "', NULL, '" + subhead + "', '" + itemmaster + "', '" + uom1 + "', '" + lblQty.Text + "','" + lblRate.Text + "', '" + lblGST.Text + "', '" + lblGSTRate.Text + "', '" + qtyval + "', '" + gstval + "', '" + lblGrossValue1.Text + "', NULL, '" + lblQty2.Text + "', '" + uom1 + "', '" + lblRate2.Text + "', '" + location + "', '" + lblProject.Text + "', '1'," + gocid() + "," + compid() + "," + branchId() + "," + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyy hh:mm:ss.ffff") + "', 0,'" + lblApprovedQty.Text + "');", "");

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

                dml.Insert("INSERT INTO Set_PurchaseOrderDetail ([Sno_Master], [PRNo_AQno], [ItemSubHead], [ItemMaster], [UOM], [Quantity], [Rate], [GST], [GSTRate], [QtyValue], [GstValue], [GrossValue], [Remarts], [Qty2], [UOM2], [Rate2], [Location], [Project], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted],[ApprovedQuantity]) VALUES ('" + id + "', '" + lblreq.Text + "', '" + subhead + "', '" + itemmaster + "', '" + uom1 + "', '" + lblQty.Text + "','" + lblRate.Text + "', '" + lblGST.Text + "', '" + lblGSTRate.Text + "', '" + lblQtyVal.Text + "', '" + lblGstValue.Text + "', '" + lblGrossValue.Text + "', NULL, '" + lblQty2.Text + "', '" + uom2 + "', '" + lblRate2.Text + "', '" + location + "', '" + lblProject.Text + "', '1'," + gocid() + "," + compid() + "," + branchId() + "," + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyy hh:mm:ss.ffff") + "', 0,'" + lblApprovedQty.Text + "');", "");
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
                dml.Insert("INSERT INTO Set_PurchaseOrderDetail ([Sno_Master], [PRNo_AQno], [ItemSubHead], [ItemMaster], [UOM], [Quantity], [Rate], [GST], [GSTRate], [QtyValue], [GstValue], [GrossValue], [Remarts], [Qty2], [UOM2], [Rate2], [Location], [Project], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted],[ApprovedQuantity]) VALUES ('" + id + "', '" + lstFruits.SelectedItem.Text + "', '" + subhead + "', '" + itemmaster + "', '" + uom1 + "', '" + lblQty.Text + "','" + txtRate.Text + "', '" + txtGST.Text + "', '" + lblGSTRate.Text + "', '" + lblQtyVal.Text + "', '" + lblGstValue.Text + "', '" + lblGrossValue.Text + "', NULL, '" + lblQty2.Text + "', '" + uom2 + "', '" + lblRate2.Text + "', '" + location + "', '" + lblProject.Text + "', '1', " + gocid() + ", " + compid() + ", " + branchId() + "," + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyy hh:mm:ss.ffff") + "', 0,'" + txtApprovedQty.Text + "');", "");
            }
        }
    }
    protected void GridView7_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lblRowNumber") as Label).Text = (e.Row.RowIndex + 1).ToString();
            //ItemMasterid
            Label ItemMasterid = e.Row.FindControl("ItemMasterid") as Label;

            Label lblitemmaster = e.Row.FindControl("lblitemmaster") as Label;
            TextBox txtAccCode = e.Row.FindControl("txtAccCode") as TextBox;
            

            bool iscon = false;
            bool isAsst = false;
            bool isExp = false;
            string ItemTypeID = "0";
           

            DataSet itemCode = dml.Find("select IsAsset,IsConsumable,IsExpense,ItemTypeID from SET_ItemMaster where ItemID = '" + ItemMasterid.Text + "'");
            ItemMasterid.Visible = false;
            if (itemCode.Tables[0].Rows.Count > 0)
            {
                isAsst = bool.Parse(itemCode.Tables[0].Rows[0]["IsAsset"].ToString());
                iscon = bool.Parse(itemCode.Tables[0].Rows[0]["IsConsumable"].ToString());
                isExp = bool.Parse(itemCode.Tables[0].Rows[0]["IsExpense"].ToString());
                ItemTypeID = itemCode.Tables[0].Rows[0]["ItemTypeID"].ToString();
            }



            DataSet itemtype = dml.Find("select InventoryAcct,ExpenseAcct,PurchasesAcct from SET_ItemType where ItemTypeID = '" + ItemTypeID + "'");
            if (itemtype.Tables[0].Rows.Count > 0)
            {
                if (isAsst == true)
                {
                    txtAccCode.Text = itemtype.Tables[0].Rows[0]["PurchasesAcct"].ToString();
                }
                if (iscon == true)
                {
                    txtAccCode.Text = itemtype.Tables[0].Rows[0]["InventoryAcct"].ToString();
                }
                if (isExp == true)
                {
                    txtAccCode.Text = itemtype.Tables[0].Rows[0]["ExpenseAcct"].ToString();
                }

            }


        }
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
           // ddlStatus.Enabled = true;
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
            chkNormalGRN.Enabled = true;
            ddltransfer.Enabled = true;
            txtCreateddate.Text = show_username() + " " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
            txtEntryDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtDeliveryChallanDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtBilldate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            chkNormalGRN.Checked = true;

            lstFruits.Enabled = true;
            //bindlist();

            //chkDirectGRN_CheckedChanged(sender, e);
            chkNormalGRN_CheckedChanged(sender, e);
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
            else if(ddlDocAuth.Items.Count == 1)
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


            if(ddlDocAuth.Items.Count == 0)
            {

            }



            //    DataSet dsradio = dml.Find("select RadioButton from SET_DocRadioBinding where DocId= '" + ddlDocName.SelectedItem.Value + "' and Record_Deleted = 0 and IsActive = 1;");

            //if (dsradio.Tables[0].Rows.Count > 0)
            //{

            //    string d_p = dsradio.Tables[0].Rows[0]["RadioButton"].ToString();
            //    if (d_p == "DIRECT")
            //    {
            //        chkDirectGRN.Checked = true;
            //        chkNormalGRN.Checked = false;
            //        chkDirectGRN.Enabled = false;
            //        chkNormalGRN.Enabled = false;
            //        chkDirectGRN_CheckedChanged(sender, e);

            //    }
            //   else if (d_p == "NORMAL")
            //    {
            //        chkNormalGRN.Checked = true;
            //        chkDirectGRN.Checked = false;
            //        chkDirectGRN.Enabled = false;
            //        chkNormalGRN.Enabled = false;
            //        //chkNormalGRN_CheckedChanged(sender, e);
            //    }
            //    else
            //    {
            //        chkDirectGRN.Enabled = true;
            //        chkNormalGRN.Enabled = true;
            //        chkNormalGRN.Checked = false;
            //        chkDirectGRN.Checked = false;
            //    }
            //}
            //else
            //{
            //    textClear();
            //    Label1.Text = "No radio Button binding";
            //}


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

            DataSet ds = dml.Find("select Monthlybase, YearlyBase from SET_Documents where DocID = '" + ddlDocName.SelectedItem.Value + "';select MAX(DocumentNo) as maxno from Inv_InventoryInMaster where SUBSTRING(DocumentNo, 0, 7) = '"+ a+ "'; select Description from SET_Fiscal_Year where Description = '" + fiscal + "'; select MAX(DocumentNo) as maxno from Inv_InventoryInMaster where SUBSTRING(DocumentNo, 0, 7) = '" + docval +"-"+fy + "'");

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
                        incre = int.Parse(inc.ToString().Substring(7, 5)) + 1;
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
                        incre = int.Parse(inc.ToString().Substring(7, 5)) + 1;
                    }
                    else
                    {
                        incre = int.Parse(inc.ToString());
                    }
                    string fyear = ds.Tables[2].Rows[0]["Description"].ToString();
                    txtDocNo.Text = docno + "-" + fyear.Substring(2, 2) + fyear.Substring(7, 2) + "-" + incre.ToString("00000");


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
        DataSet ds = dml.Find("select Monthlybase, YearlyBase from SET_Documents where DocID = '" + ddlDocName.SelectedItem.Value + "';select MAX(DocumentNo) as maxno from Inv_InventoryInMaster where SUBSTRING(DocumentNo, 0, 7) = '" + a + "'; select Description from SET_Fiscal_Year where Description = '" + fiscal + "'; select MAX(DocumentNo) as maxno from Inv_InventoryInMaster where SUBSTRING(DocumentNo, 0, 7) = '" + docval + "-" + fy + "'");

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
                    incre = int.Parse(inc.ToString().Substring(7, 5)) + 1;
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
                    incre = int.Parse(inc.ToString().Substring(7, 5)) + 1;
                }
                else
                {
                    incre = int.Parse(inc.ToString());
                }
                string fyear = ds.Tables[2].Rows[0]["Description"].ToString();
                //txtDocNo.Text = docno + "-" + fyear.Substring(2, 2) + fyear.Substring(7, 2) + "-" + incre.ToString("00000");
                pono = docno + "-" + fyear.Substring(2, 2) + fyear.Substring(7, 2) + "-" + incre.ToString("00000");

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
            dt.Columns.AddRange(new DataColumn[16]
            {
                new DataColumn("Accode"),
                new DataColumn("PONo"),
                new DataColumn("ItemSubHead"),
                new DataColumn("ItemMaster"),
                new DataColumn("UOM"),
                new DataColumn("PObalQty"),
                new DataColumn("DCQty"),
                new DataColumn("Rate"),
                new DataColumn("Value"),
                new DataColumn("CostCenter"),
                new DataColumn("Location"),
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
            dml.dropdownsql(ddlLocation, "SET_Department", "DepartmentName", "DepartmentID", "IsWarehouse", "1");

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
            dml.dropdownsql(ddlLocation, "SET_Department", "DepartmentName", "DepartmentID", "IsWarehouse", "1");
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
        
        (row.FindControl("lblValueFooter") as TextBox).Text = value.ToString() ;
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

       (row.FindControl("lblValueFooter") as TextBox).Text = value.ToString();
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

     (row.FindControl("txtValueEdit") as TextBox).Text = value.ToString();
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

       (row.FindControl("lblValueFooter") as TextBox).Text = value.ToString();
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

       (row.FindControl("lblRate2Footer") as TextBox).Text = value.ToString();
    }
    protected void txtQty2Edit_TextChanged(object sender, EventArgs e)
    {

        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;
        float value=0;
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
                foreach (GridViewRow g in GridView6.Rows)
                {
                    Label ll = (Label)g.FindControl("lblItemSubHead");
                    Label l = (Label)g.FindControl("lblItemMaster");

                    string Accode = (GridView6.FooterRow.FindControl("RadComboAcct_CodeFooter") as RadComboBox).Text;
                    string PONoFooter = (GridView6.FooterRow.FindControl("lblPONoFooter") as Label).Text;
                    string ItemSubHeadName = (GridView6.FooterRow.FindControl("ddlItemSubHeadFooter") as DropDownList).SelectedItem.Text;
                    string Description = (GridView6.FooterRow.FindControl("ddlItemMasterFooter") as DropDownList).SelectedItem.Text;
                    string UOM = (GridView6.FooterRow.FindControl("ddlUOMFooter") as DropDownList).SelectedItem.Text;
                    string Qty = (GridView6.FooterRow.FindControl("txtPObalQtyFooter") as TextBox).Text;
                    string DCQtyFooter = (GridView6.FooterRow.FindControl("txtDCQtyFooter") as TextBox).Text;
                    string Rate = (GridView6.FooterRow.FindControl("txtRateFooter") as TextBox).Text;
                    string Value = (GridView6.FooterRow.FindControl("lblValueFooter") as TextBox).Text;

                    string CostCenter = (GridView6.FooterRow.FindControl("lblCostCenterFooter") as DropDownList).SelectedItem.Text;
                    string Location = (GridView6.FooterRow.FindControl("ddlLocationFooter") as DropDownList).SelectedItem.Text;
                    string Project = (GridView6.FooterRow.FindControl("lblProjectFooter") as TextBox).Text;
                    string WarranteeFooter = (GridView6.FooterRow.FindControl("lblWarranteeFooter") as TextBox).Text;
                    string UOM2 = (GridView6.FooterRow.FindControl("ddlUOM2Footer") as DropDownList).SelectedItem.Text;
                    string Qty2 = (GridView6.FooterRow.FindControl("lblQty2Footer") as TextBox).Text;
                    string Rate2 = (GridView6.FooterRow.FindControl("lblRate2Footer") as TextBox).Text;


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


                        // Accode,PONo,ItemSubHead,ItemMaster,UOM,PObalQty,DCQty,/Rate,Value,CostCenter,Location,Project,Warrantee,UOM2,Qty2,Rate2

                        //(ItemSubHeadName, Description, UOM, Qty, ApprovedQty, Rate, GST, GSTRate, GrossValue, CostCenter,Location,Project,UOM2,Qty2,Rate2)
                        dt.Rows.Add(Accode, PONoFooter, ItemSubHeadName, Description, UOM, Qty, DCQtyFooter, Rate, Value, CostCenter, Location, Project, WarranteeFooter, UOM2, Qty2, Rate2);

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
                        dml.dropdownsql(ddlLocation, "SET_Department", "DepartmentName", "DepartmentID", "IsWarehouse", "1");


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
                    reqno += reqno + "'" + item.Text.Substring(0,11) + "'";
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
                q = "select * from View_InvGrn_Normal where docno in (" + reqno+ ") and Status != '2';";
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


            string rate ="0", value="0", gst="0", gstrate="0", qty="0";
            DataSet dsratevalue = dml.Find("select Quantity, Rate, GrossValue, GST, GSTRate from Set_PurchaseOrderDetail where Sno_Master in (SELECT Sno from Set_PurcahseOrderMaster where[DocumentNo.] = '"+ lblPoNoN .Text+ "')");
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
            DataSet ds5 = dml.Find("select LocId,LocName from Set_Location where LocName = '" + lblLocation.Text + "'; select Sno from Set_PurcahseOrderMaster where [DocumentNo.] = '"+lblPoNoN.Text+"' and Record_Deleted = 0");
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

            float balqty=0, balqty2=0;


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
            string dbcr_detail = "", dbcr_plusminus = "" ;

            string acctcode = Acct.Text, trantype="";

            DataSet dsCRDR = dml.Find("select  Tran_Type from SET_Acct_Type where Acct_Type_Id in ( SELECT Acct_Type_ID from SET_COA_detail where Acct_Code = '" + acctcode + "')");
            if (dsCRDR.Tables[0].Rows.Count > 0)
            {
                trantype = dsCRDR.Tables[0].Rows[0]["Tran_Type"].ToString();


            }
            DataSet dsDeB;
            if (trantype == "Debit")
            {
                DataSet dsinsertdetail =   dml.Find("INSERT INTO Inv_InventoryInDetail ( [Sno_Master], [PONo], [DrAccountCode], [ItemSubHead],"
                                     + " [ItemMaster], [UOM], [Quantity], [Remarks], [Qty2], [UOM2], [BalQty],"
                                     + " [BalQty2], [ReferNo], [GLReferNo], [CostCenter], [Location], [CompId], [BranchId], [FiscalYearID],"
                                     + " [CreatedBy], [CreatedDate], [Record_Deleted], [Warrentee],[Project]) "

                                     + "VALUES ('" + masterid + "', '" + lblPoNoN.Text + "','"+acctcode+"', '" + subhead + "', '" + itemmaster + "', '" + uom1 + "', '" + lblDCQtyN.Text + "',"
                                     + " NULL, '" + lblQty2.Text + "', '" + uom2 + "', '" + lblQty.Text + "', '" + balqty2 + "', NULL, NULL,"
                                     + " '" + costcenter + "', '" + location + "'," + compid() + "," + branchId() + "," + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0' , '"+txtWarranttee.Text+"',"
                                     + " '" + lblproject.Text + "'); SELECT * FROM Inv_InventoryInDetail WHERE Sno = SCOPE_IDENTITY()");




                //balqDC
               dml.Update("Update View_InvGrn_Normal set balQty = '"+ balqDC + "' where Sno = '"+ lblPOSNO.Text + "'", "");



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
                gl.GlentryInsert(ddlDocName.SelectedItem.Value, doctype,ddlDocName.SelectedItem.Text, masterid, detailid, dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, Acct.Text, bpnatureid, ddlSupplier.SelectedItem.Value, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), valuetot.ToString(), "0" , txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyy hh:mm:ss.ffff"), itemcode, qty, "0", "Inv_InventoryOutDetail");


                //Glentry Detail Normal End




            }
            else
            {
               DataSet dsinsertdetail =  dml.Find("INSERT INTO Inv_InventoryInDetail ( [Sno_Master], [PONo], [CrAccountCode], [ItemSubHead],"
                                       + " [ItemMaster], [UOM], [Quantity], [Remarks], [Qty2], [UOM2], [BalQty],"
                                       + " [BalQty2], [ReferNo], [GLReferNo], [CostCenter], [Location], [CompId], [BranchId], [FiscalYearID],"
                                       + " [CreatedBy], [CreatedDate], [Record_Deleted],[Warrentee], [Project]) "

                                       + "VALUES ('" + masterid + "', '" + lblPoNoN.Text + "','" + acctcode + "', '" + subhead + "', '" + itemmaster + "', '" + uom1 + "', '" + lblDCQtyN.Text + "',"
                                       + " NULL, '" + lblQty2.Text + "', '" + uom2 + "', '" + lblQty.Text + "', '" + balqty2 + "', NULL, NULL,"
                                       + " '" + costcenter + "', '" + location + "'," + compid() + "," + branchId() + "," + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0' ,'"+txtWarranttee.Text+"',"
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
                gl.GlentryInsert(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, masterid, detailid, dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, Acct.Text, bpnatureid, ddlSupplier.SelectedItem.Value, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), "0", valuetot.ToString(), txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyy hh:mm:ss.ffff"), itemcode, qty, "0", "Inv_InventoryOutDetail");


                //Glentry Detail Normal End




            }

            if(dbcr_plusminus == "PLUS")
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
        DataSet dsdebcr = dml.Find("SELECT	GLImpact, InventoryImpact FROM SET_AcRules4Doc WHERE DocId = '"+ddlDocName.SelectedItem.Value+"' AND MasterDetail = 'M' AND CompID = 1 AND Record_Deleted = 0 AND IsActive = 1 AND IsHide = 0 ;");
        if (dsdebcr.Tables[0].Rows.Count > 0)
        {
            dbcr = dsdebcr.Tables[0].Rows[0]["GLImpact"].ToString();
        }
        if (dbcr == "Debit Impact")
        {
            gl.GlentryInsert(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, masterid, "0", dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, RadComboAcct_Code.Text, bpnatureid, ddlSupplier.SelectedItem.Value, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), totalcrdb.ToString(), "0", txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff"), "0", "0", "0","");
            
        }
        else
        {
            gl.GlentryInsert(ddlDocName.SelectedItem.Value, doctype , ddlDocName.SelectedItem.Text, masterid, "0", dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, RadComboAcct_Code.Text, bpnatureid, ddlSupplier.SelectedItem.Value, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), "0", totalcrdb.ToString(), txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff"), "0", "0", "0", "Inv_InventoryInDetail");
            
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
        GridView6.EditIndex = e.NewEditIndex;
        PopulateGridview();
        //DataSet ds_detail = dml.Find("select * from ViewInvINDetail_FED where Sno_Master = '" + ViewState["SNO"].ToString() + "'");
        //if (ds_detail.Tables[0].Rows.Count > 0)
        //{
        //    Div3.Visible = true;
        //    GridView6.DataSource = ds_detail.Tables[0];
        //    GridView6.DataBind();
        //}

        string avv = GridView6.EditIndex.ToString();
        string avv1 = GridView6.Rows[GridView6.EditIndex].ToString();

        DropDownList ddlitemsub = GridView6.Rows[GridView6.EditIndex].FindControl("ddlItemSubHeadEdit") as DropDownList;
        dml.dropdownsql(ddlitemsub, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");
        Label lbl1 = GridView6.Rows[GridView6.EditIndex].FindControl("lblItemSubHead") as Label;

        DropDownList ddlitemmaster = GridView6.Rows[GridView6.EditIndex].FindControl("ddlItemMasterEdit") as DropDownList;
        dml.dropdownsql(ddlitemmaster, "SET_ItemMaster", "Description", "ItemID");
        Label lbl2 = GridView6.Rows[GridView6.EditIndex].FindControl("lblItemMaster") as Label;

        ddlitemmaster.ClearSelection();
        ddlitemmaster.Items.FindByText(lbl2.Text).Selected = true;
        lbl2.Visible = false;

        DropDownList uddluom = GridView6.Rows[GridView6.EditIndex].FindControl("ddlUOMEdit") as DropDownList;
        
        dml.dropdownsqlwithquery(uddluom, "select UOMID,UOMName from SET_UnitofMeasure where UOMID in ((SELECT UOMId as uom FROM SET_ItemMaster where ItemID = '" + ddlitemmaster.SelectedItem.Value + "' ) UNION (SELECT UOMId2 as uom FROM SET_ItemMaster where ItemID = '" + ddlitemmaster.SelectedItem.Value + "' ))", "UOMName", "UOMID");
        Label lbl3 = GridView6.Rows[GridView6.EditIndex].FindControl("lblUOMEdit") as Label;

        DropDownList ddlcc = GridView6.Rows[GridView6.EditIndex].FindControl("ddlLocationEdit") as DropDownList;
        dml.dropdownsql(ddlcc, "Set_Location", "LocName", "LocId");
        Label lblcc = GridView6.Rows[GridView6.EditIndex].FindControl("lblLocationEdit") as Label;


        DropDownList ddlCostCenter = GridView6.Rows[GridView6.EditIndex].FindControl("ddlCostCenterEdit") as DropDownList;
        dml.dropdownsql(ddlCostCenter, "SET_CostCenter", "CostCenterName", "CostCenterID");
        Label lblcostcenter = GridView6.Rows[GridView6.EditIndex].FindControl("lblCostCenterEdit") as Label;

        DropDownList ddluom2 = GridView6.Rows[GridView6.EditIndex].FindControl("ddlUOM2Edit") as DropDownList;
        dml.dropdownsql(ddluom2, "SET_UnitofMeasure", "UOMName", "UOMID");
        Label lblUom2 = GridView6.Rows[GridView6.EditIndex].FindControl("lblUOM2Edit") as Label;



        ddlitemsub.ClearSelection();
        ddlitemsub.Items.FindByText(lbl1.Text).Selected = true;
        lbl1.Visible = false;

        uddluom.ClearSelection();
        uddluom.Items.FindByText(lbl3.Text).Selected = true;
        lbl3.Visible = false;

        ddlcc.ClearSelection();
        ddlcc.Items.FindByText(lblcc.Text).Selected = true;
        lblcc.Visible = false;

        ddlCostCenter.ClearSelection();
        ddlCostCenter.Items.FindByText(lblcostcenter.Text).Selected = true;
        lblcostcenter.Visible = false;

        ddluom2.ClearSelection();
        ddluom2.Items.FindByText(lblUom2.Text).Selected = true;
        lblUom2.Visible = false;



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
            string valitemcode= "";
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
            if(valitem1 != "")
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



            string asset = "0", consum = "0", expense = "0", itemactasset="0", itemactconsum = "0", itemactexpense = "0", itemtypeid = "0";

            DataSet ds_as_C_E = dml.Find("select IsAsset,IsConsumable,IsExpense,ItemTypeID from SET_ItemMaster where ItemSubHeadID = '" + ddlsubitem.SelectedItem.Value+ "' AND Record_Deleted = 0 and ItemID = '"+ddlmaster.SelectedItem.Value+ "';");
            if (ds_as_C_E.Tables[0].Rows.Count > 0)
            {
                asset = ds_as_C_E.Tables[0].Rows[0]["IsAsset"].ToString();
                expense = ds_as_C_E.Tables[0].Rows[0]["IsExpense"].ToString();
                consum = ds_as_C_E.Tables[0].Rows[0]["IsConsumable"].ToString();
                itemtypeid = ds_as_C_E.Tables[0].Rows[0]["ItemTypeID"].ToString();


                DataSet dsitemtype = dml.Find("select ExpenseAcct,FixedAssetAcct,InventoryAcct from SET_ItemType where ItemTypeID = '"+ itemtypeid + "'");

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
                if(expense == "True")
                {
                    ddlaccout.Text = itemactexpense;
                }
                if(consum == "True")
                {
                    ddlaccout.Text = itemactconsum;

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
            string PONoFooter = (GridView6.Rows[e.RowIndex].FindControl("txtPoNEdit") as TextBox).Text;
            string ItemSubHeadName = (GridView6.Rows[e.RowIndex].FindControl("ddlItemSubHeadEdit") as DropDownList).SelectedItem.Text;
            string Description = (GridView6.Rows[e.RowIndex].FindControl("ddlItemMasterEdit") as DropDownList).SelectedItem.Text;
            string UOM = (GridView6.Rows[e.RowIndex].FindControl("ddlUOMEdit") as DropDownList).SelectedItem.Text;
            string Qty = (GridView6.Rows[e.RowIndex].FindControl("txtPObalQty") as TextBox).Text;
            string DCQtyFooter = (GridView6.Rows[e.RowIndex].FindControl("txtDCQtyEdit") as TextBox).Text;
            string Rate = (GridView6.Rows[e.RowIndex].FindControl("lblRateEdit") as TextBox).Text;
            string Value = (GridView6.Rows[e.RowIndex].FindControl("txtValueEdit") as TextBox).Text;

            string CostCenter = (GridView6.Rows[e.RowIndex].FindControl("ddlCostCenterEdit") as DropDownList).SelectedItem.Text;
            string Location = (GridView6.Rows[e.RowIndex].FindControl("ddlLocationEdit") as DropDownList).SelectedItem.Text;
            string Project = (GridView6.Rows[e.RowIndex].FindControl("txtProjectEdit") as TextBox).Text;
            string WarranteeFooter = (GridView6.Rows[e.RowIndex].FindControl("txtWarranteeEdit") as TextBox).Text;
            string UOM2 = (GridView6.Rows[e.RowIndex].FindControl("ddlUOM2Edit") as DropDownList).SelectedItem.Text;
            string Qty2 = (GridView6.Rows[e.RowIndex].FindControl("txtQty2Edit") as TextBox).Text;
            string Rate2 = (GridView6.Rows[e.RowIndex].FindControl("txtRate2Edit") as TextBox).Text;


            GridViewRow row = GridView6.Rows[e.RowIndex];
            DataTable dt = (DataTable)ViewState["Customers"];

            dt.Rows[row.DataItemIndex]["Accode"] = Accode;
            dt.Rows[row.DataItemIndex]["PONo"] = PONoFooter;
            dt.Rows[row.DataItemIndex]["ItemSubHead"] = ItemSubHeadName;
            dt.Rows[row.DataItemIndex]["ItemMaster"] = Description;
            dt.Rows[row.DataItemIndex]["UOM"] = UOM;
            dt.Rows[row.DataItemIndex]["PObalQty"] = Qty;
            dt.Rows[row.DataItemIndex]["DCQty"] = DCQtyFooter;
            dt.Rows[row.DataItemIndex]["Rate"] = Rate;
            dt.Rows[row.DataItemIndex]["Value"] = Value;
            dt.Rows[row.DataItemIndex]["CostCenter"] = CostCenter;
            dt.Rows[row.DataItemIndex]["Location"] = Location;
            dt.Rows[row.DataItemIndex]["Project"] = Project;
            dt.Rows[row.DataItemIndex]["Warrantee"] = WarranteeFooter;
            dt.Rows[row.DataItemIndex]["UOM2"] = UOM2;
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
        }
        catch(Exception ex)
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
        dml.dropdownsql(ddlLocation, "SET_Department", "DepartmentName", "DepartmentID", "IsWarehouse", "1");
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
        if(ddlSupplier.SelectedIndex != 0)
        {
            DataSet ds = dml.Find("select Sno, [DocumentNo.] + '--' + BPartnerName as docnosupp FROM View_POlistPoNo where Status <> 2 and balQty >0 and IsActive = 1 and BPartnerTypeID = '"+ddlSupplier.SelectedItem.Value+ "' and Record_Deleted = 0 and GocID = '" + gocid() + "' and CompId = '" + compid() + "' and BranchId = '" + branchId() + "' and FiscalYearID = '" + FiscalYear() + "'");
            

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
        DataSet ds = dml.Find("select Sno, [DocumentNo.] + '--' + BPartnerName as docnosupp FROM View_POlistPoNo where Status <> 2 and balQty > 0 and IsActive = 1 and Record_Deleted = 0 and GocID = '" + gocid() + "' and CompId = '" + compid() + "' and BranchId = '" + branchId() + "' and FiscalYearID = '" + FiscalYear() +"'");
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

            dml.Delete("update Act_GL_Detail set Record_Deleted = 1 where MasterSno= " + ddltransfer.SelectedItem.Value+ "", "");

          

            dml.Delete("INSERT INTO Act_GL_Detail_Deleted ([DocId], [DocDescription], [MasterSno], [DetailSno], [EntryDate], [VoucherNo], [AccountCode], [BPNatureID], [BPartnerId], [Name], [ChqNo], [ChqDate], [InvoiceNo], [InvoiceDate], [DrAmount], [CrAmount], [Narration], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [Record_Deleted]) SELECT [DocId], [DocDescription], [MasterSno], [DetailSno], [EntryDate], [VoucherNo], [AccountCode], [BPNatureID], [BPartnerId], [Name], [ChqNo], [ChqDate], [InvoiceNo], [InvoiceDate], [DrAmount], [CrAmount], [Narration], [GocID], [CompId], [BranchId], [FiscalYearID], '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', [UpdatedBy], [UpdatedDate], '1' FROM Act_GL_Detail WHERE MasterSno='" + ddltransfer.SelectedItem.Value + "';", "");
            dml.Delete("Delete from Act_GL_Detail where MasterSno = " + ddltransfer.SelectedItem.Value + "", "");
            dml.Delete("Delete from Act_GL where MasterSno = " + ddltransfer.SelectedItem.Value+ "", "");
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

        DataSet ds_detail = dml.Find("select * from ViewInvOutDetail_FED where Sno_Master = '" + ddltransfer.SelectedItem.Value + "'");
        if (ds_detail.Tables[0].Rows.Count > 0)
        {
            Div7.Visible = true;
            GridView10.DataSource = ds_detail.Tables[0];
            GridView10.DataBind();
        }
        //DropDownList ddlitemsub1 = GridView10.Rows[GridView10.EditIndex].FindControl("ddlItemSubHeadEdit") as DropDownList;
        //dml.dropdownsql(ddlitemsub1, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");
        Label lbl1 = GridView10.Rows[GridView10.EditIndex].FindControl("lblItemSubHead") as Label;

        //DropDownList ddlitemmaster = GridView10.Rows[GridView10.EditIndex].FindControl("ddlItemMasterEdit") as DropDownList;
        //dml.dropdownsql(ddlitemmaster, "SET_ItemMaster", "Description", "ItemID");
        Label lbl2 = GridView10.Rows[GridView10.EditIndex].FindControl("lblItemMaster") as Label;
        
        //DropDownList ddlCostCenterEdit = GridView10.Rows[GridView10.EditIndex].FindControl("ddlCostCenterEdit") as DropDownList;
        //dml.dropdownsql(ddlCostCenterEdit, "SET_CostCenter", "CostCenterName", "CostCenterID");
        Label lblcost = GridView10.Rows[GridView10.EditIndex].FindControl("lblCostCenterEdit") as Label;
        //ddlCostCenterEdit

        //ddlCostCenterEdit.ClearSelection();
        //ddlCostCenterEdit.Items.FindByText(lblcost.Text).Selected = true;
      //  lblcost.Visible = false;


        //ddlitemmaster.ClearSelection();
        //ddlitemmaster.Items.FindByText(lbl2.Text).Selected = true;
      //  lbl2.Visible = false;

        //DropDownList uddluom = GridView10.Rows[GridView10.EditIndex].FindControl("ddlUOMEdit") as DropDownList;
        //dml.dropdownsqlwithquery(uddluom, "select UOMID,UOMName from SET_UnitofMeasure where UOMID in ((SELECT UOMId as uom FROM SET_ItemMaster where ItemID = '" + ddlitemmaster.SelectedItem.Value + "' ) UNION (SELECT UOMId2 as uom FROM SET_ItemMaster where ItemID = '" + ddlitemmaster.SelectedItem.Value + "' ))", "UOMName", "UOMID");
        Label lbl3 = GridView10.Rows[GridView10.EditIndex].FindControl("lblUOM") as Label;



       // DropDownList ddlUOM2Edit = GridView10.Rows[GridView10.EditIndex].FindControl("ddlUOM2Edit") as DropDownList;
       // //dml.dropdownsqlwithquery(ddlUOM2Edit, "select UOMID,UOMName from SET_UnitofMeasure where UOMID in ((SELECT UOMId as uom FROM SET_ItemMaster where ItemID = '" + ddlitemmaster.SelectedItem.Value + "' ) UNION (SELECT UOMId2 as uom FROM SET_ItemMaster where ItemID = '" + ddlitemmaster.SelectedItem.Value + "' ))", "UOMName", "UOMID");
       //dml.dropdownsqlwithquery(ddlUOM2Edit, "select UOMID,UOMName from SET_UnitofMeasure", "UOMName", "UOMID");
        Label lblUOM2Edit = GridView10.Rows[GridView10.EditIndex].FindControl("lblUOM2Edit") as Label;

        //ddlUOM2Edit.ClearSelection();
        //ddlUOM2Edit.Items.FindByText(lblUOM2Edit.Text).Selected = true;
       // lblUOM2Edit.Visible = false;


        //DropDownList ddlcc = GridView10.Rows[GridView10.EditIndex].FindControl("ddlLocationEdit") as DropDownList;
        //dml.dropdownsql(ddlcc, "Set_Location", "LocName", "LocId");
        TextBox lblcc = GridView10.Rows[GridView10.EditIndex].FindControl("txtLocationEdit") as TextBox;

        //ddlitemsub1.ClearSelection();
        //ddlitemsub1.Items.FindByText(lbl1.Text).Selected = true;
        //lbl1.Visible = false;

        //uddluom.ClearSelection();
        //uddluom.Items.FindByText(lbl3.Text).Selected = true;
        //lbl3.Visible = false;

        //ddlcc.ClearSelection();
        //ddlcc.Items.FindByText(lblcc.Text).Selected = true;
        //lblcc.Visible = false;

    }
    protected void GridView10_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView10.EditIndex = -1;
        DataSet ds_detail = dml.Find("select * from ViewInvINDetail_FED where Sno_Master = '" + ddltransfer.SelectedItem.Value + "'");
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
        //DataSet ds_detail = dml.Find("select * from ViewInvINDetail_FED where Sno_Master = '10'");
        //if (ds_detail.Tables[0].Rows.Count > 0)
        //{
        //    Div7.Visible = true;
        //    GridView10.DataSource = ds_detail.Tables[0];
        //    GridView10.DataBind();
        //}
        
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
            gl.GlentryInsert(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, masterid, "0", dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, RadComboAcct_Code.Text, bpnatureid, ddlSupplier.SelectedItem.Value, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), totalcrdb.ToString(), "0", txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff"), "0", "0", "0", "Inv_InventoryInMaster");

        }
        else
        {
            gl.GlentryInsert(ddlDocName.SelectedItem.Value, doctype, ddlDocName.SelectedItem.Text, masterid, "0", dml.dateconvertforinsertNEW(txtEntryDate), txtDocNo.Text, RadComboAcct_Code.Text, bpnatureid, ddlSupplier.SelectedItem.Value, txtShopOffName.Text, txtDeliveryChallan.Text, dml.dateconvertforinsertNEW(txtDeliveryChallanDate), "0", totalcrdb.ToString(), txtRemarks.Text, gocid().ToString(), compid().ToString(), branchId().ToString(), FiscalYear().ToString(), show_username(), DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff"), "0", "0", "0", "Inv_InventoryInMaster");

        }
    }
    protected void btnShowJV_Click(object sender,EventArgs e)
    {
        userid = Request.QueryString["UserID"];
        UserGrpID = Request.QueryString["UsergrpID"];
        FormID = Request.QueryString["FormID"];
        fiscal = Request.QueryString["fiscaly"];
        menuid = Request.QueryString["Menuid"];
        Response.Redirect("frm_JV_Diplay.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "&Menuid=" + menuid + "&VoucherNo=" + txtDocNo.Text + "&bilno=" + txtbillNo.Text + "&billdate=" + txtBilldate.Text + "&shopname=" + txtShopOffName.Text + "");
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
        if(ddltransfer.SelectedIndex != 0)
        {

            DataSet ds = dml.Find("select Location2,Location,CostCenter,CostCenter2 from Inv_InventoryOutMaster where DocumentNo='" + ddltransfer.SelectedItem.Text+"'");
            if(ds.Tables[0].Rows.Count> 0)
            {

                ddlCostCenterTo.ClearSelection();
                ddlCostCentrFrom.ClearSelection();
                ddlLocationFrom.ClearSelection();
                ddlLocationTo.ClearSelection();


                ddlCostCenterTo.Items.FindByValue(ds.Tables[0].Rows[0]["CostCenter"].ToString()).Selected = true;
                ddlCostCentrFrom.Items.FindByValue(ds.Tables[0].Rows[0]["CostCenter2"].ToString()).Selected = true;
                ddlLocationFrom.Items.FindByValue(ds.Tables[0].Rows[0]["Location"].ToString()).Selected = true;
                ddlLocationTo.Items.FindByValue(ds.Tables[0].Rows[0]["Location2"].ToString()).Selected = true;



                DataTable dtup = new DataTable();
                dtup.Columns.AddRange(new DataColumn[17] { new DataColumn("Sno"), new DataColumn("DrAccountCode"), new DataColumn("PONO1"), new DataColumn("ItemSubHeadName"), new DataColumn("Description"), new DataColumn("UOMName"), new DataColumn("pobal"), new DataColumn("pobal1"), new DataColumn("Rate"), new DataColumn("Value"), new DataColumn("CostCenterName"), new DataColumn("LocName"), new DataColumn("Project"), new DataColumn("Warrentee"), new DataColumn("uomName2"), new DataColumn("Qty2"), new DataColumn("Rate2") });


                //select * from view_StockUpdate
                //select ItemSubHeadName , Description,CostCenterName,UOMName from view_StockUpdate
                DataSet ds_detail = dml.Find("select * from ViewInvOutDetail_FED where Sno_Master = '" + ddltransfer.SelectedItem.Value + "'");
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
}
