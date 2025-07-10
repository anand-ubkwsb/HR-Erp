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
    int DeleteDays, EditDays, AddDays, DateFrom;
    string userid, UserGrpID, FormID, fiscal, menuid;
    int valid, showd;
    string[] supplier = new string[4];

    DmlOperation dml = new DmlOperation();
    radcomboxclass cmb = new radcomboxclass();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
    FieldHide fd = new FieldHide();
    GLEntry gl = new GLEntry();
    inventoryCal inv = new inventoryCal();
    float totalval = 0;

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
            CalendarExtender1.EndDate = DateTime.Now;
            string sd = fiscalstart(fiscal);
            CalendarExtender1.StartDate = DateTime.Parse(sd);

            dml.dropdownsql(ddlBusinessPartner, "ViewSupplierId", "BPartnerName", "BPartnerId");
            dml.dropdownsql(ddlStatus, "SET_Status", "StatusName", "StatusId");
            string sdate = startdate(fiscal);
            string enddate = Enddate(fiscal);
            //dml.dropdownsqlwithquery(ddlDocName, "select DocID,DocDescription from SET_Documents where docid in (select DocID from SET_UserGrp_Documents where UserGrpId = '" + UserGrpID + "' and Record_Deleted = 0) and CompID = '" + compid() + "' and Record_Deleted = 0 and MenuId_Sno = '" + menuid + "' and FormId_Sno='" + FormID + "'", "DocDescription", "DocID");
            dml.dropdownsqlwithquery(ddlDocName, "Select DocID,DocDescription From SET_Documents where DocDescription in ('Normal payment','Bills payment')", "DocDescription", "DocID");
            //dml.dropdownsqlwithquery(ddlDocName, "select DocID, DocDescription from SET_Documents where docid in (select DocID from SET_UserGrp_Documents where UserGrpId in (Select UserGrpId From SET_Assign_UserGrp Where UserId = '"+userid+"' And Record_Deleted='0')  and Record_Deleted = '0')", "DocDescription", "DocID");
            dml.dropdownsqlwithquery(ddlbpType, "select BPNatureID,BPNatureDescription from SET_BPartnerNature", "BPNatureDescription", "BPNatureID");



            dml.dropdownsql(ddlDocAuth, "SET_Authority", "AuthorityName", "AuthorityId");
            
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
            dml.dropdownsql(ddlDocType, "SET_DocumentType", "DocName", "DocTypeId");
            
            textClear();
            ddlDocType.Enabled = false;
            Findbox.Visible = false;
            DeleteBox.Visible = false;
            Editbox.Visible = false;
            datashow();
            //gridv10_load();
            //gridv4_load();
            Div1.Visible = false;
            Div7.Visible = false;
        }
    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        con.Close();
        con.Open();
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
        ddlDocType.Enabled = true;
        doctype(menuid, FormID, UserGrpID);
        UserGrpID = Request.QueryString["UsergrpID"];
        FormID = Request.QueryString["FormID"];
        dml.dateRuleForm(UserGrpID, FormID, CalendarExtender1, "A");
        fiscal = Request.QueryString["fiscaly"];
        string sdate = startdate(fiscal);
        string enddate = Enddate(fiscal);
        ViewState["FormId"] = FormID;
        ViewState["MenuId"] = menuid;
        ViewState["UserId"] = Request.QueryString["UserID"];
        SqlDataAdapter cmd = new SqlDataAdapter("select UserGrpId From SET_Assign_UserGrp where UserId = '" + Request.QueryString["UserID"] + "'", con);
        DataSet ds = new DataSet();
        cmd.Fill(ds);
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ViewState["UserGrpId" + i.ToString()] = ds.Tables[0].Rows[i]["UserGrpId"].ToString();
        }
        
        ddlDocName_SelectedIndexChanged(sender, e);

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        string vno = txtVoucherNo.Text;

        int direct_normal = 0, chk = 0;
        string ids = "0";
        
        if (chkActive.Checked == true)
        {
            chk = 1;
        }
        string paymenttype = "";
        if(chkBillPayment.Checked == true)
        {
            paymenttype = "Bill Payment";
        }
        else
        {
            paymenttype = "Normal Payment";
        }
        string  glrefer = "0";
        string txt = txtEntryDate.Text;
        DataSet ds;

        string Query = "Insert into [Fin_PaymentMaster](";

        if (ddlDocName.SelectedIndex != 0) {
            Query += "[DocId],";
        }
        Query += "[DocType],[EntryDate],[VoucherNo]";
        if (ddlbpType.SelectedIndex != 0) {
            Query += ",[BPNatureID]";
        }

        if (ddlBusinessPartner.SelectedIndex != 0) {
            Query += ",[BPartnerId]";
        }
        Query += ",[PaymentType]";
        if (ddlDocAuth.SelectedIndex != 0) {
            Query += ",[DocumentAuthority]";
        }
        Query += ",[CNIC_NTN_No],[GST_NO]";
        if (ddlStatus.SelectedIndex != 0) {
            Query += ",[Status]";
        }
        Query += ",[Narration],[Paid_To],[DrAccountCode],[GlReferNo],[IsActive],[GocId],[CompId],[BranchId],[FiscalYearId],[CreatedBy],[CreatedDate],[Record_Deleted],[PO_No]) Values ('";
        
        if (ddlDocName.SelectedIndex != 0) {
            Query += ddlDocName.SelectedItem.Value+"',";
        }
        string DocType = "";
        if (ddlDocType.SelectedIndex != 0)
        {
            DocType = ddlDocType.SelectedItem.Value;
        }
        else {
            DocType = null;
        }
        
        Query += "'"+DocType+"','"+ dml.dateconvertforinsert(txtEntryDate) + "','"+txtVoucherNo.Text+"'";
        if (ddlbpType.SelectedIndex != 0) {
            Query += ",'"+ddlbpType.SelectedItem.Value+"'";
        }

        if (ddlBusinessPartner.SelectedIndex != 0) {
            Query += ",'"+ddlBusinessPartner.SelectedItem.Value+"'";
        }
        Query += ",'"+ paymenttype + "'";
        if (ddlDocAuth.SelectedIndex != 0) {
            Query += ",'"+ddlDocAuth.SelectedItem.Value+"'";
        }
        Query += ",'"+txtNtn.Text+"','"+txtGST.Text+"'";
        if (ddlStatus.SelectedIndex != 0) {
            Query += ",'" + ddlStatus.SelectedItem.Value +"'";
        }
        Query += ",'"+txtDescription.Text+"','"+txtPaidTo.Text+"','"+RadComboAcct_Code.Text+"','"+glrefer+"','"+chk+"',"+gocid()+","+compid()+","+branchId()+","+FiscalYear()+",'"+show_username()+"','"+DateTime.UtcNow+"','"+0+"','"+txtPoNO.Text+"');SELECT * FROM Fin_PaymentMaster WHERE Sno = SCOPE_IDENTITY()";



        //string Fin_PaymentMasterQuery = "INSERT INTO [Fin_PaymentMaster] ([DocId], [DocType], [EntryDate], [VoucherNo], [BPNatureID], [BPartnerID],"
        //                + " [PaymentType], [DocumentAuthority], [CNIC_NTN_No], [GST_No], [Status], [Narration],"
        //                + " [Paid_To], [DrAccountCode], [GLReferNo], [IsActive], [GocID], [CompId], [BranchId],"
        //                + " [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted],"
        //                + " [ReferNo], [StaxInvNo], [FWD_Id], [PO_No]) VALUES "
        //                + "('" + ddlDocName.SelectedItem.Value + "', '9', '" + dml.dateconvertforinsert(txtEntryDate) + "', '" + txtVoucherNo.Text + "', '" + ddlbpType.SelectedItem.Value + "', '" + ddlBusinessPartner.SelectedItem.Value + "', '" + paymenttype + "', '" + ddlDocAuth.SelectedItem.Value + "', '" + txtNtn.Text + "', '" + txtGST.Text + "',"
        //                + " '" + ddlStatus.SelectedItem.Value + "', '" + txtDescription.Text + "', '" + txtPaidTo.Text + "', '" + RadComboAcct_Code.Text + "', '" + glrefer + "', '" + chk + "', '" + gocid() + "', '" + compid() + "', '" + branchId() + "', '" + FiscalYear() + "', '" + show_username() + "',"
        //                + " '" + DateTime.Now.ToString("dd-MMM-yyy hh:mm:ss.ffff") + "', '0', NULL, NULL, NULL, '" + txtPoNO.Text + "');SELECT * FROM Fin_PaymentMaster WHERE Sno = SCOPE_IDENTITY()";
        ds = dml.Find(Query);


        if (ds.Tables[0].Rows.Count > 0)
        {
            ids = ds.Tables[0].Rows[0]["Sno"].ToString();
            ViewState["detailid"] = ids;
        }

        if (chkNormalPayment.Checked == true)
        {
            detailinsertDetail(ids);//here done
            detailinsert_BanKMaster(ids);// here done
        }
        if (chkBillPayment.Checked == true)
        {
            detailinsert(ids);//here done
            detailinsert_BanKMaster(ids);//here done 
        }

         // to insert payment detail into act_Gl_Detaill and Act_Gl
         // here goes the method to be
         // implemented after database
         // changes by Mr.Rizwan
     
        // gl.GlentrySummaryDetail(show_username(), DateTime.Now.ToString(), "0",txtVoucherNo.Text);

        textClear();
        btnInsert_Click(sender, e);
        ddlDocType.SelectedIndex = 0;
        ddlDocType.Enabled = false;
        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);
      // Response.Redirect("frm_JV_Diplay.aspx?UserID="+userid+"&UsergrpID="+UserGrpID+"&fiscaly="+fiscal+"&FormID="+FormID+"&Menuid="+menuid+"&VoucherNo="+vno+"&bilno="+billno+"&billdate="+billdate+"&shopname="+shopname+ "");
       ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string chk = "0";
        if (chkActive.Checked == true)
        {
            chk = "1";
        }


        string rdb = "";
        if (chkBillPayment.Checked == true)
        {
            rdb = "Bill Payment";

        }
        else
        {
            rdb = "Normal Payment";
        }
        string DocName = "", BusinessPartner = "", BusinessPartnerNatureId = "", DocAuthority = "",Status="";

        if (ddlDocName.SelectedIndex != 0)
        {
            DocName = ddlDocName.SelectedItem.Value;
        }
        else {
            DocName = null;
        }

        if (ddlBusinessPartner.SelectedIndex != 0)
        {
            BusinessPartner = ddlBusinessPartner.SelectedItem.Value;
        }
        else
        {
            BusinessPartner = null;
        }

        if (ddlbpType.SelectedIndex != 0)
        {
            BusinessPartnerNatureId = ddlbpType.SelectedItem.Value;
        }
        else {
            BusinessPartnerNatureId = null;
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
            Status = ddlStatus.SelectedItem.Value;
        }
        else {
            Status = null;
        }
        dml.Update("UPDATE Fin_PaymentMaster  SET  [DocId]='" + DocName+ "', [EntryDate]='" + txtEntryDate.Text + "', [VoucherNo]='" + txtVoucherNo.Text + "', [BPNatureID]='" + BusinessPartnerNatureId + "', [BPartnerID]='" + BusinessPartner + "', [PaymentType]='"+rdb+"', [DocumentAuthority]='" + DocAuthority + "', [CNIC_NTN_No]='" + txtNtn.Text + "', [GST_No]='" + txtGST.Text + "', [Status]='" + Status + "', [Narration]='" + txtDescription.Text + "', [Paid_To]='" + txtPaidTo.Text + "', [DrAccountCode]='" + RadComboAcct_Code.Text + "', [IsActive]='"+ chk + "', [UpdatedBy]='" + show_username() + "', [UpdatedDate]='" + DateTime.Now.ToString() +"', [Record_Deleted]='0', [ReferNo]=NULL, [StaxInvNo]=NULL, [FWD_Id]=NULL, [PO_No]='45' WHERE ([Sno]='1' AND [GocID]='" + gocid() + "' AND  [CompId]='" + compid() + "' AND  [BranchId]='" + branchId() + "' AND  [FiscalYearID]='" + FiscalYear() +"')", "");

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
        ddlDocType.Enabled = false;
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
            string squer = "select * from View_paymentFUD";


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

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and  [DocID] = '20' and    [GocID] = '" + gocid() + "' and [BranchId]='" + branchId() + "' and  [FiscalYearID] = '" + FiscalYear() + "' and CompId = '" + compid() + "'  ORDER BY DocDescription";

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
            string squer = "select * from View_paymentFUD";
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

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and [GocID] = '" + gocid() + "' and [BranchId]='" + branchId() + "' and  [FiscalYearID] = '" + FiscalYear() + "' and CompId = '" + compid() + "'  ORDER BY DocDescription";

            Findbox.Visible = true;
            fieldbox.Visible = false;
            DeleteBox.Visible = false;

            DataSet dgrid = dml.grid(squer);
            GridView1.DataSource = dgrid;
            GridView1.DataBind();
            //GridView7.Visible = false;
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
            string squer = "select * from View_paymentFUD";


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

            squer = squer + " where " + swhere + " and Record_Deleted = 0  and  [GocID] = '" + gocid() + "' and [BranchId]='" + branchId() + "' and  [FiscalYearID] = '" + FiscalYear() + "' and CompId = '" + compid() + "'  ORDER BY DocDescription";



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

        chkBillPayment.Enabled = false;
        chkNormalPayment.Enabled = false;
        ddlDocName.Enabled = false;
        ddlDocAuth.Enabled = false;
        ddlStatus.Enabled = false;
        txtVoucherNo.Enabled = false;
        RadComboAcct_Code.Enabled = false;
        txtPoNO.Enabled = false;

        txtPoNO.Text = "";
        RadComboAcct_Code.Text = "";


        ddlbpType.Enabled = false;
        ddlBusinessPartner.Enabled = false;
        txtPaidTo.Enabled = false;
        
        txtNtn.Enabled = false;
        txtGST.Enabled = false;
        txtDescription.Enabled = false;
        txtCreateBy.Enabled = false;


        txtEntryDate.Text = "";
       //chkActive.Text = "";
        ddlDocName.SelectedIndex = 0;
        ddlDocAuth.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        txtVoucherNo.Text = "";
       
        ddlbpType.SelectedIndex =0;
        ddlBusinessPartner.SelectedIndex = 0;
        txtPaidTo.Text = "";
        
        txtNtn.Text = "";
        txtGST.Text = "";
        txtDescription.Text = "";
        Div1.Visible = false;
        Div7.Visible = false;
        Div2.Visible = false;
        Div3.Visible = false;
        GridView10.DataSource = null;
        GridView10.DataBind();

        txtCreateBy.Enabled = false;
        chkActive.Checked = false;
        txtUpdateDate.Enabled = false;
        btnShowJV.Visible = false;


        GridView10.DataSource = null;
        GridView10.DataBind();

        GridView4.DataSource = null;
        GridView4.DataBind();
        GridView7.DataSource = null;
        GridView7.DataBind();
      

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

            //SELECT * from Set_PurchaseOrderDetail where Sno_Master = 4
            dml.Delete("update Inv_InventoryInMaster set Record_Deleted = 1 where Sno= " + ViewState["SNO"].ToString() + "", "");
            dml.Delete("update Inv_InventoryInDetail set Record_Deleted = 1 where Sno_Master= " + ViewState["SNO"].ToString() + "", "");
            dml.Delete("update Act_GL_Detail set Record_Deleted = 1 where MasterSno= " + ViewState["SNO"].ToString() + "", "");
            dml.Delete("INSERT INTO Act_GL_Detail_Deleted ([DocId], [DocDescription], [MasterSno], [DetailSno], [EntryDate], [VoucherNo], [AccountCode], [BPNatureID], [BPartnerId], [Name], [ChqNo], [ChqDate], [InvoiceNo], [InvoiceDate], [DrAmount], [CrAmount], [Narration], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [Record_Deleted]) SELECT [DocId], [DocDescription], [MasterSno], [DetailSno], [EntryDate], [VoucherNo], [AccountCode], [BPNatureID], [BPartnerId], [Name], [ChqNo], [ChqDate], [InvoiceNo], [InvoiceDate], [DrAmount], [CrAmount], [Narration], [GocID], [CompId], [BranchId], [FiscalYearID], '"+show_username()+"', '"+DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")+"', [UpdatedBy], [UpdatedDate], '1' FROM Act_GL_Detail WHERE MasterSno='"+ ViewState["SNO"].ToString() + "';", "");
            dml.Delete("Delete from Act_GL_Detail where MasterSno = " + ViewState["SNO"].ToString() + "", "");
            dml.Delete("Delete from Act_GL where MasterSno = " + ViewState["SNO"].ToString() + "", "");

            gl.GlentrySummaryDelete(txtVoucherNo.Text);


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
              
        try
        {
            string serial_id;
            serial_id = GridView1.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;

            DataSet ds = dml.Find("select * from Fin_PaymentMaster WHERE ([Sno]='" + serial_id + "') and Record_Deleted = '0'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDocName.ClearSelection();
                ddlBusinessPartner.ClearSelection();
                ddlbpType.ClearSelection();
                ddlDocAuth.ClearSelection();
                ddlStatus.ClearSelection();


                if(ddlDocName.Items.FindByValue(ds.Tables[0].Rows[0]["DocID"].ToString()) != null)
                {
                    ddlDocName.Items.FindByValue(ds.Tables[0].Rows[0]["DocID"].ToString()).Selected = true;
                }
                if (ddlBusinessPartner.Items.FindByValue(ds.Tables[0].Rows[0]["BPartnerID"].ToString()) != null)
                {
                    ddlBusinessPartner.Items.FindByValue(ds.Tables[0].Rows[0]["BPartnerID"].ToString()).Selected = true;
                }
                if (ddlbpType.Items.FindByValue(ds.Tables[0].Rows[0]["BPNatureID"].ToString()) != null)
                {
                    ddlbpType.Items.FindByValue(ds.Tables[0].Rows[0]["BPNatureID"].ToString()).Selected = true;
                }

                if (ddlDocAuth.Items.FindByValue(ds.Tables[0].Rows[0]["DocumentAuthority"].ToString()) != null)
                {
                    ddlDocAuth.Items.FindByValue(ds.Tables[0].Rows[0]["DocumentAuthority"].ToString()).Selected = true;
                }

                if (ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["Status"].ToString()) != null)
                {
                    ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["Status"].ToString()).Selected = true;
                }

                txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtVoucherNo.Text = ds.Tables[0].Rows[0]["VoucherNo"].ToString();
                txtPaidTo.Text =  ds.Tables[0].Rows[0]["Paid_To"].ToString();
                txtPoNO.Text =   ds.Tables[0].Rows[0]["PO_No"].ToString();
                RadComboAcct_Code.Text = ds.Tables[0].Rows[0]["DrAccountCode"].ToString();
                txtNtn.Text = ds.Tables[0].Rows[0]["CNIC_NTN_No"].ToString();
                txtGST.Text = ds.Tables[0].Rows[0]["GST_No"].ToString();
                txtDescription.Text = ds.Tables[0].Rows[0]["Narration"].ToString();
                txtCreateBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString()) + " " + ds.Tables[0].Rows[0]["CreatedDate"].ToString();
                txtUpdateDate.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString()) + " " + ds.Tables[0].Rows[0]["UpdatedDate"].ToString();
                dml.dateConvert(txtEntryDate);
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                string payType = ds.Tables[0].Rows[0]["PaymentType"].ToString();

                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }

                if (payType == "Normal Payment")
                {
                    chkBillPayment.Checked = false;
                    chkNormalPayment.Checked = true;
                }
                else
                {
                    chkBillPayment.Checked = true;
                    chkNormalPayment.Checked = false;

                }

                DataSet ds_detail_Payment = dml.Find("Select * from View_PaymentvoucherDetail where BPartnerID = '" + ddlBusinessPartner.SelectedItem.Value + "'");
                if (ds_detail_Payment.Tables[0].Rows.Count > 0)
                {                    
                    Div2.Visible = true;
                    GridView5.DataSource = ds_detail_Payment.Tables[0];
                    GridView5.DataBind();
                }

                DataSet ds_detail_Bank = dml.Find("select * from View_PayVoucher_Bankk_FED where SnoMaster = '" + serial_id + "'");
                if (ds_detail_Bank.Tables[0].Rows.Count > 0)
                {
                    Div3.Visible = true;
                    GridView6.DataSource = ds_detail_Bank.Tables[0];
                    GridView6.DataBind();

                    GridView6.Columns[11].Visible = false;
                }

                Div1.Visible = false;
                Div7.Visible = false;



                




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
        
        try
        {
            string serial_id;
            serial_id = GridView2.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;

            DataSet ds = dml.Find("select * from Fin_PaymentMaster WHERE ([Sno]='" + serial_id + "') and Record_Deleted = '0'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDocName.ClearSelection();
                ddlBusinessPartner.ClearSelection();
                ddlbpType.ClearSelection();
                ddlDocAuth.ClearSelection();
                ddlStatus.ClearSelection();


                if (ddlDocName.Items.FindByValue(ds.Tables[0].Rows[0]["DocID"].ToString()) != null)
                {
                    ddlDocName.Items.FindByValue(ds.Tables[0].Rows[0]["DocID"].ToString()).Selected = true;
                }
                if (ddlBusinessPartner.Items.FindByValue(ds.Tables[0].Rows[0]["BPartnerID"].ToString()) != null)
                {
                    ddlBusinessPartner.Items.FindByValue(ds.Tables[0].Rows[0]["BPartnerID"].ToString()).Selected = true;
                }
                if (ddlbpType.Items.FindByValue(ds.Tables[0].Rows[0]["BPNatureID"].ToString()) != null)
                {
                    ddlbpType.Items.FindByValue(ds.Tables[0].Rows[0]["BPNatureID"].ToString()).Selected = true;
                }

                if (ddlDocAuth.Items.FindByValue(ds.Tables[0].Rows[0]["DocumentAuthority"].ToString()) != null)
                {
                    ddlDocAuth.Items.FindByValue(ds.Tables[0].Rows[0]["DocumentAuthority"].ToString()).Selected = true;
                }

                if (ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["Status"].ToString()) != null)
                {
                    ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["Status"].ToString()).Selected = true;
                }

                txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtVoucherNo.Text = ds.Tables[0].Rows[0]["VoucherNo"].ToString();
                txtPaidTo.Text = ds.Tables[0].Rows[0]["Paid_To"].ToString();
                txtPoNO.Text = ds.Tables[0].Rows[0]["PO_No"].ToString();
                RadComboAcct_Code.Text = ds.Tables[0].Rows[0]["DrAccountCode"].ToString();
                txtNtn.Text = ds.Tables[0].Rows[0]["CNIC_NTN_No"].ToString();
                txtGST.Text = ds.Tables[0].Rows[0]["GST_No"].ToString();
                txtDescription.Text = ds.Tables[0].Rows[0]["Narration"].ToString();
                txtCreateBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString()) + " " + ds.Tables[0].Rows[0]["CreatedDate"].ToString();
                txtUpdateDate.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString()) + " " + ds.Tables[0].Rows[0]["UpdatedDate"].ToString();
                dml.dateConvert(txtEntryDate);
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                string payType = ds.Tables[0].Rows[0]["PaymentType"].ToString();

                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }

                if (payType == "Normal Payment")
                {
                    chkBillPayment.Checked = false;
                    chkNormalPayment.Checked = true;
                }
                else
                {
                    chkBillPayment.Checked = true;
                    chkNormalPayment.Checked = false;

                }

                DataSet ds_detail_Payment = dml.Find("Select * from View_PaymentvoucherDetail where BPartnerID = '" + ddlBusinessPartner.SelectedItem.Value + "'");
                if (ds_detail_Payment.Tables[0].Rows.Count > 0)
                {
                    Div2.Visible = true;
                    GridView5.DataSource = ds_detail_Payment.Tables[0];
                    GridView5.DataBind();
                }

                DataSet ds_detail_Bank = dml.Find("select * from View_PayVoucher_Bankk_FED where SnoMaster = '" + serial_id + "'");
                if (ds_detail_Bank.Tables[0].Rows.Count > 0)
                {
                    Div3.Visible = true;
                    GridView6.DataSource = ds_detail_Bank.Tables[0];
                    GridView6.DataBind();

                    GridView6.Columns[11].Visible = false;
                }

                Div1.Visible = false;
                Div7.Visible = false;


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
        ddlBusinessPartner.Enabled = true;
        ddlDocAuth.Enabled = false;
        txtEntryDate.Enabled = true;
        chkBillPayment.Enabled = true;
        chkNormalPayment.Enabled = true;
        txtPoNO.Enabled = true;
        txtPaidTo.Enabled = true;
        txtNtn.Enabled = true;
        txtGST.Enabled = true;
        txtDescription.Enabled = true;
        ddlStatus.Enabled = true;
        ddlbpType.Enabled = true;

        txtEntryDate.Enabled = true;
        chkActive.Enabled = true;

        try
        {
            string serial_id;
            serial_id = GridView3.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;

            DataSet ds = dml.Find("select * from Fin_PaymentMaster WHERE ([Sno]='" + serial_id + "') and Record_Deleted = '0'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDocName.ClearSelection();
                ddlBusinessPartner.ClearSelection();
                ddlbpType.ClearSelection();
                ddlDocAuth.ClearSelection();
                ddlStatus.ClearSelection();


                if (ddlDocName.Items.FindByValue(ds.Tables[0].Rows[0]["DocID"].ToString()) != null)
                {
                    ddlDocName.Items.FindByValue(ds.Tables[0].Rows[0]["DocID"].ToString()).Selected = true;
                }
                if (ddlBusinessPartner.Items.FindByValue(ds.Tables[0].Rows[0]["BPartnerID"].ToString()) != null)
                {
                    ddlBusinessPartner.Items.FindByValue(ds.Tables[0].Rows[0]["BPartnerID"].ToString()).Selected = true;
                }
                if (ddlbpType.Items.FindByValue(ds.Tables[0].Rows[0]["BPNatureID"].ToString()) != null)
                {
                    ddlbpType.Items.FindByValue(ds.Tables[0].Rows[0]["BPNatureID"].ToString()).Selected = true;
                }

                if (ddlDocAuth.Items.FindByValue(ds.Tables[0].Rows[0]["DocumentAuthority"].ToString()) != null)
                {
                    ddlDocAuth.Items.FindByValue(ds.Tables[0].Rows[0]["DocumentAuthority"].ToString()).Selected = true;
                }

                if (ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["Status"].ToString()) != null)
                {
                    ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["Status"].ToString()).Selected = true;
                }

                txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtVoucherNo.Text = ds.Tables[0].Rows[0]["VoucherNo"].ToString();
                txtPaidTo.Text = ds.Tables[0].Rows[0]["Paid_To"].ToString();
                txtPoNO.Text = ds.Tables[0].Rows[0]["PO_No"].ToString();
                RadComboAcct_Code.Text = ds.Tables[0].Rows[0]["DrAccountCode"].ToString();
                txtNtn.Text = ds.Tables[0].Rows[0]["CNIC_NTN_No"].ToString();
                txtGST.Text = ds.Tables[0].Rows[0]["GST_No"].ToString();
                txtDescription.Text = ds.Tables[0].Rows[0]["Narration"].ToString();
                txtCreateBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString()) + " " + ds.Tables[0].Rows[0]["CreatedDate"].ToString();
                txtUpdateDate.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString()) + " " + ds.Tables[0].Rows[0]["UpdatedDate"].ToString();
                dml.dateConvert(txtEntryDate);
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                string payType = ds.Tables[0].Rows[0]["PaymentType"].ToString();

                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }

                if (payType == "Normal Payment")
                {
                    chkBillPayment.Checked = false;
                    chkNormalPayment.Checked = true;
                }
                else
                {
                    chkBillPayment.Checked = true;
                    chkNormalPayment.Checked = false;

                }

                DataSet ds_detail_Payment = dml.Find("Select * from View_PaymentvoucherDetail where BPartnerID = '" + ddlBusinessPartner.SelectedItem.Value + "'");
                if (ds_detail_Payment.Tables[0].Rows.Count > 0)
                {
                    Div2.Visible = true;
                    GridView5.DataSource = ds_detail_Payment.Tables[0];
                    GridView5.DataBind();
                }

                DataSet ds_detail_Bank = dml.Find("select * from View_PayVoucher_Bankk_FED where SnoMaster = '" + serial_id + "'");
                if (ds_detail_Bank.Tables[0].Rows.Count > 0)
                {
                    Div3.Visible = true;
                    GridView6.DataSource = ds_detail_Bank.Tables[0];
                    GridView6.DataBind();
                    
                }

                Div1.Visible = false;
                Div7.Visible = false;
                
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
        string gocid = Request.Cookies["GocId"].Value;
        return Convert.ToInt32(gocid);
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
        DataSet ds = dml.Find("select StartDate from SET_Fiscal_Year where FiscalYearId = " + FiscalYear() );
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

    public void PopulateGridview()
    {

        DataTable dtbl = (DataTable)ViewState["DirectDetail"];

        if (dtbl.Rows.Count > 0)
        {

            GridView4.DataSource = (DataTable)ViewState["DirectDetail"];
            GridView4.DataBind();

        }
        else
        {


            dtbl.Rows.Add(dtbl.NewRow());
            GridView4.DataSource = dtbl;
            GridView4.DataBind();

            GridView4.Rows[0].Cells.Clear();
            //  GridView6.Rows[0].Cells.Add(new TableCell());
            //GridView6.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
            //  GridView6.Rows[0].Cells[0].Text = "No Data Found ..!";
            //  GridView6.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;

        }
    }

    public void PopulateGridview10()
    {

        DataTable dtbl = (DataTable)ViewState["DirectDetailaa"];

        if (dtbl.Rows.Count > 0)
        {
            GridView10.DataSource = (DataTable)ViewState["DirectDetailaa"];
            GridView10.DataBind();

        }
        else
        {
            dtbl.Rows.Add(dtbl.NewRow());
            GridView10.DataSource = dtbl;
            GridView10.DataBind();
            GridView10.Rows[0].Cells.Clear();
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
        foreach (GridViewRow g in GridView10.Rows)
        {

            Label lblSno = (Label)g.FindControl("lbMSno");
            Label lblCrAcct = (Label)g.FindControl("lblCrAcct");
            
            Label lblBillNo = (Label)g.FindControl("lblBillNo");
            Label lblBillDate = (Label)g.FindControl("lblBillDate");
            Label lblGLDate = (Label)g.FindControl("lblGLDate");
            Label lblSupplier = (Label)g.FindControl("lblSupplier");
            Label lblGSTClaimable = (Label)g.FindControl("lblGSTClaimable");
            Label lblBillPayable = (Label)g.FindControl("lblBillPayable");
            Label lblApprovedAmount = (Label)g.FindControl("lblApprovedAmount");
            Label lblGSTBalance = (Label)g.FindControl("lblGSTBalance");
            Label lblBillBalanceBalanceHide = (Label)g.FindControl("lblBillBalanceBalanceHide");
            string DocName;
            if (ddlDocName.SelectedIndex != 0)
            {
                DocName = ddlDocName.SelectedItem.Value;
            }
            else {
                DocName = null;
            }
            if (lblSno.Text == "" && lblCrAcct.Text == "" && lblBillNo.Text == "" && lblBillDate.Text == "" && lblGLDate.Text == "" &&
                lblSupplier.Text == "" && lblGSTClaimable.Text == "" && lblBillPayable.Text == "" && lblApprovedAmount.Text == "" && lblGSTBalance.Text == ""
                && lblBillBalanceBalanceHide.Text == "")
            { }
            else
            {
                dml.Insert("INSERT INTO [Fin_PaymentDetail] ([Sno_Master], [DcNo], [GrnNo],"
                    + " [PoNo], [ItemSubHead], [ItemMaster], [DrAccountCode], [UOM]," +
                    " [Quantity], [Rate], [Value], [ReferNo],"
                    + " [GLReferNo], [Store_Department], [CostCenter], [GocId], [CompId],"
                    + " [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate],  [Record_Deleted], [Project],[BalanceAmount]) "

                    + "VALUES ('" + lblSno + "', '" + DocName + "', NULL, NULL, NULL, NULL,"
                    + " '" + lblCrAcct.Text + "', NULL, NULL, NULL, '" + lblApprovedAmount.Text + "', "
                    + "NULL, NULL, NULL, NULL, " + gocid() + ", " + compid() + ", " + branchId() + "," + FiscalYear() + ","
                    + " '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "',"
                    + " '0', NULL,'" + lblBillBalanceBalanceHide.Text + "');", "");
            }
        }

    }

    public void detailinsert_BanKMaster(string masterid)
    {
        foreach (GridViewRow g in GridView4.Rows)
        {

            Label lblBankName = (Label)g.FindControl("lblBankName");
            Label lblBranch = (Label)g.FindControl("lblBranch");
            Label lblAcctCode = (Label)g.FindControl("lblAcctCode");
            Label lblAccountNo = (Label)g.FindControl("lblAccountNo");
            Label lblPaythrough = (Label)g.FindControl("lblPaythrough");
            Label lblInstrumentNo = (Label)g.FindControl("lblInstrumentNo");
            Label lblInstrument_Date = (Label)g.FindControl("lblInstrument_Date");
            Label lblNarration = (Label)g.FindControl("lblNarration");
            Label lblAmount = (Label)g.FindControl("lblAmount");

            if (lblBankName.Text == "" && lblBranch.Text == "" && lblAcctCode.Text == "" && lblAccountNo.Text == ""
                && lblPaythrough.Text == "" && lblInstrumentNo.Text == "" && lblInstrument_Date.Text == "" && lblNarration.Text == "" && lblAmount.Text == "")
            {
            }
            else
            {

                string bankid = "0";
                string bank_ID = "0", branch_ID = "0";

                DataSet ds1 = dml.Find("select BankID from SET_Bank where BankName = '" + lblBankName.Text + "'");
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    bank_ID = ds1.Tables[0].Rows[0]["BankID"].ToString();
                }
                else
                {
                    bank_ID = "0";

                }
                DataSet ds2 = dml.Find("select BankBranchID from SET_BankBranch where BankBranchName ='" + lblBranch.Text + "'");
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    branch_ID = ds2.Tables[0].Rows[0]["BankBranchID"].ToString();
                }
                else
                {
                    branch_ID = "0";
                }
                string date = lblInstrument_Date.Text;
                string Fin_PaymentBanksQuery = "INSERT INTO [Fin_PaymentBanks] ([Bank],[BankBranch],[AccountCode], [AccountNo], [PayThrough],"
                    + " [InstrumentNo], [InstrumentDate], [Amount],[SnoMaster],[GocID], [CompId], [BranchId],"
                    + " [FiscalYearID], [CreatedBy], [CreatedDate],"
                    + " [Record_Deleted]) VALUES "

                    + "('" + bank_ID + "', '" + branch_ID + "','" + lblAcctCode.Text + "', '" + lblAccountNo.Text + "', '" + lblPaythrough.Text + "', '" + lblInstrumentNo.Text + "', '" + dml.dateconvertforinsert(lblInstrument_Date) + "', "
                    + " '" + lblAmount.Text + "','" + masterid + "' ," + gocid() + "," + compid() + "," + branchId() + "," + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "','0');SELECT * FROM Fin_PaymentBanks WHERE Sno = SCOPE_IDENTITY()";
                DataSet ds = dml.Find(Fin_PaymentBanksQuery);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    bankid = ds.Tables[0].Rows[0]["Sno"].ToString();

                }

                float amount = float.Parse(lblAmount.Text);


                foreach (GridViewRow row in GridView10.Rows)
                {
                    Label lblSno = (Label)row.FindControl("lblSno");
                    Label lblValue = (Label)row.FindControl("lblValue");
                    float value = float.Parse(lblValue.Text);


                    string balamt = "0";
                    DataSet ds_balamt = dml.Find("select BalanceAmount from Fin_PaymentDetail where Sno = '" + lblSno.Text + "'");
                    if (ds_balamt.Tables[0].Rows.Count > 0)
                    {
                        balamt = ds_balamt.Tables[0].Rows[0]["BalanceAmount"].ToString();
                    }
                    if (balamt != "0")
                    {

                        if (value <= amount)
                        {
                            string Fin_PaymentBankDetailQuery = "INSERT INTO [Fin_PaymentBankDetail] ([Sno_masterPayment], [Sno_PayDetail], "
                           + " [Sno_PayBank], [Amount], [GocId], [CompId],"
                           + " [BranchId], [FiscalYearID], [CreatedBy],"
                           + " [CreatedDate])"

                           + " VALUES ('" + masterid + "', '" + lblSno.Text + "', '" + bankid + "', '" + value + "',"
                           + " " + gocid() + ", " + compid() + ", " + branchId() + "," + FiscalYear() + ","
                           + " '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "')";
                            dml.Insert(Fin_PaymentBankDetailQuery, "");
                            amount = amount - value;

                            dml.Update("UPDATE Fin_PaymentDetail set BalanceAmount = 0 where Sno= '" + lblSno.Text + "'", "");

                        }
                        else
                        {

                            dml.Insert("INSERT INTO [Fin_PaymentBankDetail] ([Sno_masterPayment], [Sno_PayDetail], "
                          + " [Sno_PayBank], [Amount], [GocId], [CompId],"
                          + " [BranchId], [FiscalYearID], [CreatedBy],"
                          + " [CreatedDate])"

                          + " VALUES ('" + masterid + "', '" + lblSno.Text + "', '" + bankid + "', '" + amount + "',"
                          + " " + gocid() + ", " + compid() + ", " + branchId() + ", " + FiscalYear() + ","
                          + " '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "')", "");
                            value = value - amount;

                            dml.Update("UPDATE Fin_PaymentDetail set BalanceAmount = '" + value + "' where Sno= '" + lblSno.Text + "'", "");
                        }

                    }

                }

            }

        }

    }

    public void detailinsert_BanKDetail(string masterid)
    {
        foreach (GridViewRow g in GridView6.Rows)
        {

            Label lblBankName = (Label)g.FindControl("lblBankName");
            Label lblBranch = (Label)g.FindControl("lblBranch");
            Label lblAcctCode = (Label)g.FindControl("lblAcctCode");
            Label lblAccountNo = (Label)g.FindControl("lblAccountNo");
            Label lblPaythrough = (Label)g.FindControl("lblPaythrough");
            Label lblInstrumentNo = (Label)g.FindControl("lblInstrumentNo");
            Label lblInstrument_Date = (Label)g.FindControl("lblInstrument_Date");
            Label lblNarration = (Label)g.FindControl("lblNarration");
            Label lblAmount = (Label)g.FindControl("lblAmount");

            string bankid = "0";
            string bank_ID = "0", branch_ID = "0";

            DataSet ds1 = dml.Find("select BankID from SET_Bank where BankName = '" + lblBankName.Text + "'");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                bank_ID = ds1.Tables[0].Rows[0]["BankID"].ToString();
            }
            else
            {
                bank_ID = "0";

            }
            DataSet ds2 = dml.Find("select BankBranchID from SET_BankBranch where BankBranchName ='" + lblBranch.Text + "'");
            if (ds2.Tables[0].Rows.Count > 0)
            {
                branch_ID = ds2.Tables[0].Rows[0]["BankBranchID"].ToString();
            }
            else
            {
                branch_ID = "0";
            }

            DataSet ds = dml.Find("INSERT INTO [Fin_PaymentBanks] ([Bank],[AccountCode], [BankBranch], [AccountNo], [PayThrough],"
                + " [InstrumentNo], [InstrumentDate], [Amount],[SnoMaster],[GocID], [CompId], [BranchId],"
                + " [FiscalYearID], [CreatedBy], [CreatedDate],"
                + " [Record_Deleted]) VALUES "

                + "('" + bank_ID + "','" + lblAcctCode.Text + "','"+branch_ID+"', '" + lblAccountNo.Text + "', '" + lblPaythrough.Text + "', '" + lblInstrumentNo.Text + "', '" + dml.dateconvertforinsert(lblInstrument_Date) + "', "
                + " '" + lblAmount.Text + "','" + masterid + "' ," + gocid() + ", " + compid() + ", " + branchId() + ", " + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "','0');SELECT * FROM Fin_PaymentBanks WHERE Sno = SCOPE_IDENTITY()");

            if (ds.Tables[0].Rows.Count > 0)
            {
                bankid = ds.Tables[0].Rows[0]["Sno"].ToString();

            }

            float amount = float.Parse(lblAmount.Text);


            foreach (GridViewRow row in GridView5.Rows)
            {
                Label lblSno = (Label)row.FindControl("lblSno");
                Label lblValue = (Label)row.FindControl("lblValue");
                float value = float.Parse(lblValue.Text);


                string balamt = "0";
                DataSet ds_balamt = dml.Find("select BalanceAmount from Fin_PaymentDetail where Sno = '" + lblSno.Text + "'");
                if (ds_balamt.Tables[0].Rows.Count > 0)
                {
                    balamt = ds_balamt.Tables[0].Rows[0]["BalanceAmount"].ToString();
                }
                if (balamt != "0")
                {

                    if (value <= amount)
                    {
                        dml.Insert("INSERT INTO [Fin_PaymentBankDetail] ([Sno_masterPayment], [Sno_PayDetail], "
                       + " [Sno_PayBank], [Amount], [GocId], [CompId],"
                       + " [BranchId], [FiscalYearID], [CreatedBy],"
                       + " [CreatedDate])"

                       + " VALUES ('" + masterid + "', '" + lblSno.Text + "', '" + bankid + "', '" + value + "',"
                       + " " + gocid() + "," + compid() + "," + branchId() + "," + FiscalYear() + ","
                       + " '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "')", "");
                        amount = amount - value;

                        dml.Update("UPDATE Fin_PaymentDetail set BalanceAmount = 0 where Sno= '" + lblSno.Text + "'", "");

                    }
                    else
                    {

                        dml.Insert("INSERT INTO [Fin_PaymentBankDetail] ([Sno_masterPayment], [Sno_PayDetail], "
                      + " [Sno_PayBank], [Amount], [GocId], [CompId],"
                      + " [BranchId], [FiscalYearID], [CreatedBy],"
                      + " [CreatedDate])"

                      + " VALUES ('" + masterid + "', '" + lblSno.Text + "', '" + bankid + "', '" + amount + "',"
                      + " " + gocid() + ", " + compid() + "," + branchId() + ", " + FiscalYear() + ","
                      + " '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "')", "");
                        value = value - amount;

                        dml.Update("UPDATE Fin_PaymentDetail set BalanceAmount = '" + value + "' where Sno= '" + lblSno.Text + "'", "");
                    }

                }

            }



        }
    }

    public void detaisave(string id)
    {
       
       // for (int a = 0; a < GridView6.Rows.Count; a++)
        //{
            
        //    Label lblItemSubHeadName = (Label)GridView6.Rows[a].FindControl("lblItemSubHeadName");
        //    Label lblDescription = (Label)GridView6.Rows[a].FindControl("lblDescription");
        //    Label lblUom = (Label)GridView6.Rows[a].FindControl("lblUom");
        //    Label lblQty = (Label)GridView6.Rows[a].FindControl("lblQty");
        //    Label lblApprovedQty = (Label)GridView6.Rows[a].FindControl("lblApprovedQty");
        //    Label lblRate = (Label)GridView6.Rows[a].FindControl("lblRate");
        //    Label lblGST = (Label)GridView6.Rows[a].FindControl("lblGST");
        //    Label lblGSTRate = (Label)GridView6.Rows[a].FindControl("lblGSTRate");
        //    Label lblGrossValue = (Label)GridView6.Rows[a].FindControl("lblGrossValue");
        //    Label lblGrossValue1 = (Label)GridView6.Rows[a].FindControl("lblFGV");

        //    Label lblCostCenter = (Label)GridView6.Rows[a].FindControl("lblCostCenter");
        //    Label lblLocation = (Label)GridView6.Rows[a].FindControl("lblLocation");
        //    Label lblProject = (Label)GridView6.Rows[a].FindControl("lblProject");
        //    Label lblUOM2 = (Label)GridView6.Rows[a].FindControl("lblUOM2");
        //    Label lblQty2 = (Label)GridView6.Rows[a].FindControl("lblQty2");
        //    Label lblRate2 = (Label)GridView6.Rows[a].FindControl("lblRate2");


        //    DataSet ds = dml.Find("select ItemSubHeadID, ItemSubHeadName from SET_ItemSubHead where ItemSubHeadName = '" + lblItemSubHeadName.Text + "'");
        //    string subhead, itemmaster, uom1, costcenter, location;
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        subhead = ds.Tables[0].Rows[0]["ItemSubHeadID"].ToString();
        //    }
        //    else
        //    {
        //        subhead = "0";
        //    }
        //    DataSet ds1 = dml.Find("select  ItemID , Description from SET_ItemMaster where Description = '" + lblDescription.Text + "'");
        //    if (ds1.Tables[0].Rows.Count > 0)
        //    {
        //        itemmaster = ds1.Tables[0].Rows[0]["ItemID"].ToString();
        //    }
        //    else
        //    {
        //        itemmaster = "0";
        //    }
        //    DataSet ds2 = dml.Find("select  UOMID,UOMName from SET_UnitofMeasure where UOMName = '" + lblUom.Text + "'");
        //    if (ds2.Tables[0].Rows.Count > 0)
        //    {
        //        uom1 = ds2.Tables[0].Rows[0]["UOMID"].ToString();
        //    }
        //    else
        //    {
        //        uom1 = "0";
        //    }

        //    DataSet ds3 = dml.Find("select  CostCenterID,CostCenterName from SET_CostCenter where CostCenterName = '" + lblCostCenter.Text + "'");
        //    if (ds3.Tables[0].Rows.Count > 0)
        //    {
        //        costcenter = ds3.Tables[0].Rows[0]["CostCenterID"].ToString();
        //    }
        //    else
        //    {
        //        costcenter = "0";
        //    }
        //    DataSet ds4 = dml.Find("select LocId,LocName from Set_Location where LocName = '" + lblLocation.Text + "'");
        //    if (ds4.Tables[0].Rows.Count > 0)
        //    {
        //        location = ds3.Tables[0].Rows[0]["CostCenterID"].ToString();
        //    }
        //    else
        //    {
        //        location = "0";
        //    }
        //    float qtyval = float.Parse(lblQty.Text) * float.Parse(lblRate.Text);
        //    float gstval = float.Parse(lblQty.Text) * float.Parse(lblGSTRate.Text);
        //    dml.Insert("INSERT INTO Set_PurchaseOrderDetail ([Sno_Master], [PRNo_AQno], [ItemSubHead], [ItemMaster], [UOM], [Quantity], [Rate], [GST], [GSTRate], [QtyValue], [GstValue], [GrossValue], [Remarts], [Qty2], [UOM2], [Rate2], [Location], [Project], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted],[ApprovedQuantity]) VALUES ('" + id + "', NULL, '" + subhead + "', '" + itemmaster + "', '" + uom1 + "', '" + lblQty.Text + "','" + lblRate.Text + "', '" + lblGST.Text + "', '" + lblGSTRate.Text + "', '" + qtyval + "', '" + gstval + "', '" + lblGrossValue1.Text + "', NULL, '" + lblQty2.Text + "', '" + uom1 + "', '" + lblRate2.Text + "', '" + location + "', '" + lblProject.Text + "', '1', '" + gocid() + "', '" + compid() + "', '" + branchId() + "', '" + FiscalYear() + "', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyy hh:mm:ss.ffff") + "', 0,'" + lblApprovedQty.Text + "');", "");

        //}
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

            ddlBusinessPartner.Enabled = true;
            ddlDocAuth.Enabled = true;
            txtEntryDate.Enabled = true;
            ddlbpType.Enabled = true;
            ddlBusinessPartner.Enabled = true;
            txtPaidTo.Enabled = true;
            txtPoNO.Enabled = true;
            RadComboAcct_Code.Enabled = true;
            txtNtn.Enabled = true;
            txtGST.Enabled = true;
            txtDescription.Enabled = true;
            chkActive.Enabled = true;
            ddlBusinessPartner.Enabled = false;
            txtCreateBy.Text = show_username() + " " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
            txtEntryDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");

            //bindlist();

            //chkDirectGRN_CheckedChanged(sender, e);
          
            chkActive.Checked = true;
           
            ddlStatus.ClearSelection();
            ddlStatus.Items.FindByText("Open").Selected = true;
            required_generate();
           
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
               
                Label1.Text = "This document is not authorized";
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



               DataSet dsradio = dml.Find("select RadioButton from SET_DocRadioBinding where DocId= '" + ddlDocName.SelectedItem.Value + "' and Record_Deleted = 0 and IsActive = 1;");

            if (dsradio.Tables[0].Rows.Count > 0)
            {

                string d_p = dsradio.Tables[0].Rows[0]["RadioButton"].ToString();
                if (d_p == "BILL PAYMENT")
                {
                    chkBillPayment.Checked = true;
                    chkNormalPayment.Checked = false;
                    chkBillPayment.Enabled = false;
                    chkNormalPayment.Enabled = false;
                    // chkDirectGRN_CheckedChanged(sender, e);
                    Div2.Visible = true;
                    Div4.Visible = true;
                    Div1.Visible = false;
                    Div7.Visible = false;

                }
                else if (d_p == "NORMAL PAYMENT")
                {
                    chkNormalPayment.Checked = true;
                    chkBillPayment.Checked = false;
                    chkBillPayment.Enabled = false;
                    chkNormalPayment.Enabled = false;
                    //chkNormalGRN_CheckedChanged(sender, e);
                    Div2.Visible = false;
                    Div4.Visible = false;
                    Div1.Visible = true;
                    Div7.Visible = true;
                }
                else
                {
                    // chkDirectGRN.Enabled = true;
                    // chkNormalGRN.Enabled = true;
                    // chkNormalGRN.Checked = false;
                    //chkDirectGRN.Checked = false;
                    textClear();
                }
                Label1.Text = "";
            }
            else
            {
                textClear();
                Label1.Text = "No radio Button binding";
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

                    txtVoucherNo.Text = docno + "-" + month + year + "-" + incre.ToString("00000");
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
                    txtVoucherNo.Text = docno + "-" + fyear.Substring(2, 2) + fyear.Substring(7, 2) + "-" + incre.ToString("00000");


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



           

        }

    }
   
    protected void btnShowJV_Click(object sender,EventArgs e)
    {
        userid = Request.QueryString["UserID"];
        UserGrpID = Request.QueryString["UsergrpID"];
        FormID = Request.QueryString["FormID"];
        fiscal = Request.QueryString["fiscaly"];
        menuid = Request.QueryString["Menuid"];
        string URL = "frm_JV_Diplay.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "&Menuid=" + menuid + "&VoucherNo=" + txtVoucherNo.Text + "";
        Response.Redirect(URL);
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

    public void gridv10_load()
    {
        DataSet ds_detail = dml.Find("select * from Fin_PaymentDetail where Sno_Master = '5'");
        if (ds_detail.Tables[0].Rows.Count > 0)
        {
            ViewState["Customers1"] = ds_detail.Tables[0];
            Div7.Visible = true;
            GridView10.DataSource = ds_detail.Tables[0];
            GridView10.DataBind();
        }
        else
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
    }

    public void gridv4_load()
    {
        DataSet ds_detail = dml.Find("select * from Fin_PaymentBanks where Sno = '6'");
        if (ds_detail.Tables[0].Rows.Count > 0)
        {
            ViewState["Customers1"] = ds_detail.Tables[0];
            Div1.Visible = true;
            GridView4.DataSource = ds_detail.Tables[0];
            GridView4.DataBind();
        }
        else
        {

            DataTable dtbl = (DataTable)ViewState["Customers1"];

            if (dtbl.Rows.Count > 0)
            {

                GridView4.DataSource = (DataTable)ViewState["Customers1"];
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

    protected void RadComboAcct_Code_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(RadComboAcct_Code, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }

    protected void ddlBusinessPartner_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (chkBillPayment.Checked == true)
        {
            btnFetchData.Visible = true;
            if (ddlBusinessPartner.SelectedIndex != 0)
            {
                var bpname = ddlBusinessPartner.SelectedItem.Value;
                // Response.Redirect("frm_Select_Bill_ForPayment.aspx?UserID=6b9c1166-0f4b-41dc-99e8-b47be96c8157&UsergrpID=ff43b221-f9e1-4423-aa61-f12880a9e13d&fiscaly=2020-2021&FormID=797acfee-34d4-469a-be60-53ba7bc21b11&Menuid=0b7b1f65-e6d3-45e5-881b-a61df68652d9");
                Response.Write("<script>window.open('frm_Select_Bill_ForPayment.aspx?UserID="+ViewState["UserId"].ToString()+"&UsergrpID=ff43b221-f9e1-4423-aa61-f12880a9e13d&fiscaly=2020-2021&FormID="+ViewState["FormId"].ToString()+"&Menuid="+ViewState["MenuId"]+"&bpname=" + ddlBusinessPartner.SelectedItem.Value + "','_blank');</script>");

            }
        }
        else
        {
            btnFetchData.Visible = false;
            Div1.Visible = true;
             Div7.Visible = true;
            datashowdetail();
            datashow();
            //DataSet dsgrid = dml.grid("Select * from View_PaymentvoucherDetail where BPartnerID = '" + ddlBusinessPartner.SelectedItem.Value + "' AND BalanceAmount > 0");
            //if (dsgrid.Tables[0].Rows.Count > 0)
            //{
            //    Div1.Visible = true;
            //    Div7.Visible = true;
            //    GridView10.DataSource = dsgrid.Tables[0];
            //    GridView10.DataBind();
            //}
            //else
            //{
            //    Div1.Visible = false;
            //    Div7.Visible = false;
            //    GridView10.DataSource = null;
            //    GridView10.DataBind();
            //    lblGrid10_Err.Text = "Data Not Found";
            //}

        }
    }
    
    protected void ddlbpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlbpType.SelectedIndex > 0)
        {
            ddlBusinessPartner.Enabled = true;
            string Query = "select BPartnerId,BPartnerName from SET_BusinessPartner where BPartnerId in (Select BPartnerID from SET_BPartnerType where  BPNatureID = '"+ddlbpType.SelectedItem.Value+ "') order by BPartnerName ASC";
            dml.dropdownsqlwithquery(ddlBusinessPartner, "select BPartnerId,BPartnerName from SET_BusinessPartner where BPartnerId in (Select BPartnerID from SET_BPartnerType where  BPNatureID = '"+ddlbpType.SelectedItem.Value+ "') order by BPartnerName ASC", "BPartnerName", "BPartnerId");
            DataSet ds = dml.Find("select Acct_Code from SET_BPartnerNature where BPNatureID ='" + ddlbpType.SelectedItem.Value + "'");
            if(ds.Tables[0].Rows.Count> 0)
            {
                RadComboAcct_Code.Text = ds.Tables[0].Rows[0]["Acct_Code"].ToString();
            }
            else
            {
                lblDrCode.Text = "0";
            }
        }
    }
    protected void RadComboAcct_CodeFooter_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        RadComboBox RadComboAcct_CodeFooter = GridView4.FooterRow.FindControl("RadComboAcct_CodeFooter") as RadComboBox;

        cmb.serachcombo4(RadComboAcct_CodeFooter, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
    public void datashow()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[9]
        {
        
        new DataColumn("Bank"),
              new DataColumn("Branch"),
                new DataColumn("AcctCode"),
                new DataColumn("AccountNo"),
                new DataColumn("Pay"),
                new DataColumn("InstrumentNo"),
                new DataColumn("InstrumentDate"),
                new DataColumn("Narration"),
                new DataColumn("Amount"),
               
        });
        ViewState["DirectDetail"] = dt;
        this.PopulateGridview();
        DropDownList ddlBankID_Footer = GridView4.FooterRow.FindControl("ddlBankID_Footer") as DropDownList;
        dml.dropdownsql(ddlBankID_Footer, "SET_Bank", "BankName", "BankID");
       
        DropDownList ddlBankBranch_Footer = GridView4.FooterRow.FindControl("ddlBankBranch_Footer") as DropDownList;
        dml.dropdownsql(ddlBankBranch_Footer, "SET_BankBranch", "BankBranchName", "BankBranchID");

        
        DropDownList ddlAcctNo_Footer = GridView4.FooterRow.FindControl("ddlAcctNo_Footer") as DropDownList;
        dml.dropdownsql(ddlAcctNo_Footer, "SET_BankAccount", "BankAccountNumber", "BankAccountID");

        RadComboBox RadComboAcct_CodeFooter = GridView4.FooterRow.FindControl("RadComboAcct_CodeFooter") as RadComboBox;
        RadComboAcct_CodeFooter.Enabled = false;
    }

    protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            float val = float.Parse(lbltotalvaldetail.Text);
            if (e.CommandName.Equals("AddNew"))
            {
                DataTable dt = (DataTable)ViewState["DirectDetail"];
                
                string ddlBankID_Footer = (GridView4.FooterRow.FindControl("ddlBankID_Footer") as DropDownList).SelectedItem.Text;
                string ddlBankBranch_Footer = (GridView4.FooterRow.FindControl("ddlBankBranch_Footer") as DropDownList).SelectedItem.Text;
                string RadComboAcct_CodeFooter = (GridView4.FooterRow.FindControl("RadComboAcct_CodeFooter") as RadComboBox).Text;
                string ddlAcctNo_Footer = (GridView4.FooterRow.FindControl("ddlAcctNo_Footer") as DropDownList).SelectedItem.Text;
                string ddlpay_Footer = (GridView4.FooterRow.FindControl("ddlpay_Footer") as DropDownList).SelectedItem.Text;
                string txtInsNo_Footer = (GridView4.FooterRow.FindControl("txtInsNo_Footer") as TextBox).Text;
                string txtINSDate_Footer = (GridView4.FooterRow.FindControl("txtINSDate_Footer") as TextBox).Text;
                string txtNarration_Footer = (GridView4.FooterRow.FindControl("txtNarration_Footer") as TextBox).Text;
                string txtAmount_Footer = (GridView4.FooterRow.FindControl("txtAmount_Footer") as TextBox).Text;
                float detvalamount = float.Parse(txtAmount_Footer);
                if (ViewState["flag"].ToString() == "true")
                {
                    dt.Rows[0].Delete();
                    ViewState["flag"] = "false";
                }
                if (val >= detvalamount)
                {
                    dt.Rows.Add(ddlBankID_Footer, ddlBankBranch_Footer, RadComboAcct_CodeFooter, ddlAcctNo_Footer, ddlpay_Footer, txtInsNo_Footer, txtINSDate_Footer, txtNarration_Footer, txtAmount_Footer);
                    lbltotalvaldetail.Text = (val - detvalamount).ToString();
                    Label1.Text = "";
                    ViewState["DirectDetail"] = dt;
                    this.PopulateGridview();
                    DropDownList ddlBankID_Footer1 = GridView4.FooterRow.FindControl("ddlBankID_Footer") as DropDownList;
                    dml.dropdownsql(ddlBankID_Footer1, "SET_Bank", "BankName", "BankID");

                    DropDownList ddlBankBranch_Footer1 = GridView4.FooterRow.FindControl("ddlBankBranch_Footer") as DropDownList;
                    dml.dropdownsql(ddlBankBranch_Footer1, "SET_BankBranch", "BankBranchName", "BankBranchID");

                    DropDownList ddlAcctNo_Footer1 = GridView4.FooterRow.FindControl("ddlAcctNo_Footer") as DropDownList;
                    dml.dropdownsql(ddlAcctNo_Footer1, "SET_BankAccount", "BankAccountNumber", "BankAccountID");
                }
                else
                {
                    Label1.Text = "Value exceed from total value";
                }
            }
        }
        catch (Exception ex)
        {
            
        }
    }

    protected void ddlBankID_Footer_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlBankID_Footer = GridView4.FooterRow.FindControl("ddlBankID_Footer") as DropDownList;
        DropDownList ddlBankBranch_Footer = GridView4.FooterRow.FindControl("ddlBankBranch_Footer") as DropDownList;
        if (ddlBankID_Footer.SelectedIndex > 0)
        {
            dml.dropdownsql(ddlBankBranch_Footer, "SET_BankBranch", "BankBranchName", "BankBranchID", "BankID", ddlBankID_Footer.SelectedItem.Value);
        }
    }

    protected void GridView10_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lblRowNumber") as Label).Text = (e.Row.RowIndex + 1).ToString();
            Label lblVal = (e.Row.FindControl("lblValue") as Label);
            Label lblRate = (e.Row.FindControl("lblRate") as Label);

            string val = (e.Row.FindControl("lblValue") as Label).Text;

            if (val != null)
            {
                if (val != "")
                {
                    totalval = totalval + float.Parse(val);
                    lbltotalvaldetail.Text = totalval.ToString();

                    dislaydigit_Label(lblVal, float.Parse(val));
                }
            }

            if (lblRate != null)
            {
                if (lblRate.Text != "")
                {
                    dislaydigit_Label(lblRate, float.Parse(lblRate.Text));
                }
            }

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
           // (e.Row.FindControl("lblValue_footer") as Label).Text = totalval.ToString();

        }


    }
    protected void GridView5_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lblRowNumber") as Label).Text = (e.Row.RowIndex + 1).ToString();
            Label lblVal = (e.Row.FindControl("lblValue") as Label);
            Label lblRate = (e.Row.FindControl("lblRate") as Label);

            dislaydigit_Label(lblVal, float.Parse(lblVal.Text));
            dislaydigit_Label(lblRate, float.Parse(lblRate.Text));

        }

    }
    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lblRowNumber") as Label).Text = (e.Row.RowIndex + 1).ToString();
        }

    }

    protected void ddlAcctNo_Footer_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlAcctNo_Footer = GridView4.FooterRow.FindControl("ddlAcctNo_Footer") as DropDownList;
        RadComboBox RadComboAcct_CodeFooter = GridView4.FooterRow.FindControl("RadComboAcct_CodeFooter") as RadComboBox;

        if (ddlAcctNo_Footer.SelectedIndex > 0)
        {
            DataSet ds = dml.Find("select ChartofAccountCode from SET_BankAccount where BankAccountID = '"+ ddlAcctNo_Footer.SelectedItem.Value+ "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                RadComboAcct_CodeFooter.Text = ds.Tables[0].Rows[0]["ChartofAccountCode"].ToString();
            }
            
        }
    }
   
    protected void GridView4_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView4.EditIndex = e.NewEditIndex;
        
        GridView4.DataSource = ViewState["DirectDetail"];
        GridView4.DataBind();

        DropDownList ddlBankID_Edit = GridView4.Rows[GridView4.EditIndex].FindControl("ddlBankID_Edit") as DropDownList;
        dml.dropdownsql(ddlBankID_Edit, "SET_Bank", "BankName", "BankID");
        Label lbl1 = GridView4.Rows[GridView4.EditIndex].FindControl("lblBankNameE") as Label;

        DropDownList ddlBankBranch_Edit = GridView4.Rows[GridView4.EditIndex].FindControl("ddlBankBranch_Edit") as DropDownList;
        dml.dropdownsql(ddlBankBranch_Edit, "SET_BankBranch", "BankBranchName", "BankBranchID");
        Label lbl2 = GridView4.Rows[GridView4.EditIndex].FindControl("lblbankBranchEdit") as Label;

        ddlBankBranch_Edit.ClearSelection();
        ddlBankBranch_Edit.Items.FindByText(lbl2.Text).Selected = true;
        lbl2.Visible = false;

        DropDownList ddlAcctNo_Edit = GridView4.Rows[GridView4.EditIndex].FindControl("ddlAcctNo_Edit") as DropDownList;
        dml.dropdownsql(ddlAcctNo_Edit, "SET_BankAccount", "BankAccountNumber", "BankAccountID");
        Label lbl3 = GridView4.Rows[GridView4.EditIndex].FindControl("lblAcctNoEdit") as Label;
                
        ddlBankID_Edit.ClearSelection();
        ddlBankID_Edit.Items.FindByText(lbl1.Text).Selected = true;
        lbl1.Visible = false;

        ddlAcctNo_Edit.ClearSelection();
        ddlAcctNo_Edit.Items.FindByText(lbl3.Text).Selected = true;
        lbl3.Visible = false;
    }

    protected void GridView4_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView4.EditIndex = -1;
        PopulateGridview();


        DropDownList ddlBankID_Footer1 = GridView4.FooterRow.FindControl("ddlBankID_Footer") as DropDownList;
        dml.dropdownsql(ddlBankID_Footer1, "SET_Bank", "BankName", "BankID");

        DropDownList ddlBankBranch_Footer1 = GridView4.FooterRow.FindControl("ddlBankBranch_Footer") as DropDownList;
        dml.dropdownsql(ddlBankBranch_Footer1, "SET_BankBranch", "BankBranchName", "BankBranchID");

        DropDownList ddlAcctNo_Footer1 = GridView4.FooterRow.FindControl("ddlAcctNo_Footer") as DropDownList;
        dml.dropdownsql(ddlAcctNo_Footer1, "SET_BankAccount", "BankAccountNumber", "BankAccountID");

    }

    protected void GridView4_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = GridView10.Rows[e.RowIndex];
        DataTable dt = (DataTable)ViewState["DirectDetail"];
        dt.Rows[e.RowIndex].Delete();
        this.PopulateGridview();
        GridView4.DataSource = ViewState["DirectDetail"];
        GridView4.DataBind();

        DropDownList ddlBankID_Footer1 = GridView4.FooterRow.FindControl("ddlBankID_Footer") as DropDownList;
        dml.dropdownsql(ddlBankID_Footer1, "SET_Bank", "BankName", "BankID");

        DropDownList ddlBankBranch_Footer1 = GridView4.FooterRow.FindControl("ddlBankBranch_Footer") as DropDownList;
        dml.dropdownsql(ddlBankBranch_Footer1, "SET_BankBranch", "BankBranchName", "BankBranchID");

        DropDownList ddlAcctNo_Footer1 = GridView4.FooterRow.FindControl("ddlAcctNo_Footer") as DropDownList;
        dml.dropdownsql(ddlAcctNo_Footer1, "SET_BankAccount", "BankAccountNumber", "BankAccountID");
    }

    protected void GridView4_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DropDownList ddlBankID_Edit = (GridView4.Rows[e.RowIndex].FindControl("ddlBankID_Edit") as DropDownList);
        DropDownList ddlBankBranch_Edit = (GridView4.Rows[e.RowIndex].FindControl("ddlBankBranch_Edit") as DropDownList);
        DropDownList ddlAcctNo_Footer = (GridView4.Rows[e.RowIndex].FindControl("ddlAcctNo_Edit") as DropDownList);
        DropDownList ddlpay_Edit = (GridView4.Rows[e.RowIndex].FindControl("ddlpay_Edit") as DropDownList);
                
        string lblAcctCode_Edit = (GridView4.Rows[e.RowIndex].FindControl("lblAcctCode_Edit") as Label).Text;
        string lblInstrumentNoEdit = (GridView4.Rows[e.RowIndex].FindControl("lblInstrumentNoEdit") as TextBox).Text;
        string lblInstrument_Date_Edit = (GridView4.Rows[e.RowIndex].FindControl("lblInstrument_Date_Edit") as TextBox).Text;
        string lblNarrationEdit = (GridView4.Rows[e.RowIndex].FindControl("lblNarrationEdit") as TextBox).Text;
        string lblAmountEdit = (GridView4.Rows[e.RowIndex].FindControl("lblAmountEdit") as TextBox).Text;
        string lblAmountEditr = (GridView4.Rows[e.RowIndex].FindControl("lblAmountEditr") as Label).Text;
       
        float detvalamount = float.Parse(lblAmountEdit);
        float Fixdetvalamount = float.Parse(lblAmountEditr);
        GridViewRow row = GridView4.Rows[e.RowIndex];
        DataTable dt = (DataTable)ViewState["DirectDetail"];

        dt.Rows[row.DataItemIndex]["Bank"] = ddlBankID_Edit.SelectedItem.Text;
        dt.Rows[row.DataItemIndex]["Branch"] = ddlBankBranch_Edit.SelectedItem.Text;
        dt.Rows[row.DataItemIndex]["AccountNo"] = ddlAcctNo_Footer.SelectedItem.Text;
        dt.Rows[row.DataItemIndex]["Pay"] = ddlpay_Edit.SelectedItem.Text;
        dt.Rows[row.DataItemIndex]["AcctCode"] = lblAcctCode_Edit.ToString();
        dt.Rows[row.DataItemIndex]["InstrumentNo"] = lblInstrumentNoEdit.ToString();
        dt.Rows[row.DataItemIndex]["InstrumentDate"] = lblInstrument_Date_Edit;
        dt.Rows[row.DataItemIndex]["Narration"] = lblNarrationEdit;
        dt.Rows[row.DataItemIndex]["Amount"] = lblAmountEdit;
        float tolval = float.Parse(lbltotalvaldetail.Text);
        if (detvalamount >= Fixdetvalamount)
        {
            float a = detvalamount- Fixdetvalamount;
            lbltotalvaldetail.Text = (tolval - a).ToString() ;
        }
        else
        {
            float a = Fixdetvalamount - detvalamount;
            lbltotalvaldetail.Text = (tolval + a).ToString();
        }

            GridView4.EditIndex = -1;
        PopulateGridview();

        DropDownList ddlBankID_Footer1 = GridView4.FooterRow.FindControl("ddlBankID_Footer") as DropDownList;
        dml.dropdownsql(ddlBankID_Footer1, "SET_Bank", "BankName", "BankID");

        DropDownList ddlBankBranch_Footer1 = GridView4.FooterRow.FindControl("ddlBankBranch_Footer") as DropDownList;
        dml.dropdownsql(ddlBankBranch_Footer1, "SET_BankBranch", "BankBranchName", "BankBranchID");

        DropDownList ddlAcctNo_Footer1 = GridView4.FooterRow.FindControl("ddlAcctNo_Footer") as DropDownList;
        dml.dropdownsql(ddlAcctNo_Footer1, "SET_BankAccount", "BankAccountNumber", "BankAccountID");


    }

    protected void GridView6_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DataSet ds_detail_Bank = dml.Find("select * from View_PayVoucher_Bankk_FED where SnoMaster = '" + ViewState["SNO"].ToString() + "'");
        string lblAmountEdit = (GridView6.Rows[e.RowIndex].FindControl("lblAmountEdit") as TextBox).Text;
        string SNODetail = (GridView6.Rows[e.RowIndex].FindControl("SNODetail") as Label).Text;
        dml.Insert("insert into Fin_PaymentBankDetail_Del select * from Fin_PaymentBankDetail where Sno_PayBank = '"+ SNODetail + "'", "");
        dml.Delete("Delete from Fin_PaymentBankDetail where Sno_PayBank = '"+SNODetail+"'", "");
        detailinsert_BanKDetail(SNODetail);

        GridViewRow row = GridView6.Rows[e.RowIndex];
        DataTable dt = ds_detail_Bank.Tables[0];
        dt.Rows[row.DataItemIndex]["Amount"] = lblAmountEdit;
        GridView6.EditIndex = -1;
        if (ds_detail_Bank.Tables[0].Rows.Count > 0)
        {
            Div3.Visible = true;
            GridView6.DataSource = ds_detail_Bank.Tables[0];
            GridView6.DataBind();

        }
    }
    protected void GridView6_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView6.EditIndex = e.NewEditIndex;
        DataSet ds_detail_Bank = dml.Find("select * from View_PayVoucher_Bankk_FED where SnoMaster = '" + ViewState["SNO"].ToString() + "'");
        if (ds_detail_Bank.Tables[0].Rows.Count > 0)
        {
            Div3.Visible = true;
            GridView6.DataSource = ds_detail_Bank.Tables[0];
            GridView6.DataBind();

        }

    }
    
    protected void GridView6_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView6.EditIndex = -1;
        DataSet ds_detail_Bank = dml.Find("select * from View_PayVoucher_Bankk_FED where SnoMaster = '" + ViewState["SNO"].ToString() + "'");
        if (ds_detail_Bank.Tables[0].Rows.Count > 0)
        {
            Div3.Visible = true;
            GridView6.DataSource = ds_detail_Bank.Tables[0];
            GridView6.DataBind();

        }
    }
    protected void GridView6_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataSet ds_detail_Bank = dml.Find("select * from View_PayVoucher_Bankk_FED where SnoMaster = '" + ViewState["SNO"].ToString()+ "'");
        if (ds_detail_Bank.Tables[0].Rows.Count > 0)
        {
            GridViewRow row = GridView6.Rows[e.RowIndex];
            DataTable dt = ds_detail_Bank.Tables[0];
            dt.Rows[e.RowIndex].Delete();
            Div3.Visible = true;
            GridView6.DataSource = ds_detail_Bank.Tables[0];
            GridView6.DataBind();

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
   
    public void dislaydigit_Label(Label box, float value)
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

    protected void btnFetchData_Click(object sender, EventArgs e)
    {
        //DataSet dsgrid = dml.grid("Select * from View_PaymentvoucherDetail where BPartnerID = '" + ddlBusinessPartner.SelectedItem.Value + "' AND BalanceAmount > 0");
        //if (dsgrid.Tables[0].Rows.Count > 0)
        //{
        //    Div1.Visible = true;
        //    Div7.Visible = true;
        //    GridView10.DataSource = dsgrid.Tables[0];
        //    GridView10.DataBind();
        //}
        //else
        //{
        //    Div1.Visible = false;
        //    Div7.Visible = false;
        //    GridView10.DataSource = null;
        //    GridView10.DataBind();
        //    lblGrid10_Err.Text = "Data Not Found";
        //}
        if (ddlBusinessPartner.SelectedIndex != 0)
        {
            Div4.Visible = true;
            Div1.Visible = true;
            datanBillpay();
            datashow();
        }
    }

    protected void GridView10_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView10.EditIndex = e.NewEditIndex;
        DataSet dsgrid = dml.grid("Select * from View_PaymentvoucherDetail where BPartnerID = '" + ddlBusinessPartner.SelectedItem.Value + "' AND BalanceAmount > 0");
        if (dsgrid.Tables[0].Rows.Count > 0)
        {
            Div1.Visible = true;
            Div7.Visible = true;
            GridView10.DataSource = dsgrid.Tables[0];
            GridView10.DataBind();
        }
    }

    protected void lblRateEdit_TextChanged(object sender, EventArgs e)
    {
        

        Label lblDCQtya = GridView10.FindControl("lblDCQty") as Label;
        Label lblDCQty = GridView10.FindControl("lblDCQtyEdit") as Label;
        TextBox lblRateEdit = GridView10.FindControl("lblRateEdit") as TextBox;
        Label lblValue = GridView10.FindControl("lblRateEdit") as Label;
        
        float qty = float.Parse(lblDCQty.Text);
        float rate = float.Parse(lblRateEdit.Text);

        float amount = qty * rate;
        lblValue.Text = amount.ToString();
    }

    protected void GridView10_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DataSet ds_detail_Bank = dml.Find("Select * from View_PaymentvoucherDetail where BPartnerID = '" + ddlBusinessPartner.SelectedItem.Value + "' AND BalanceAmount > 0");
        string lblRateEdit = (GridView10.Rows[e.RowIndex].FindControl("lblRateEdit") as TextBox).Text;

        GridViewRow row = GridView6.Rows[e.RowIndex];
        DataTable dt = ds_detail_Bank.Tables[0];
        dt.Rows[row.DataItemIndex]["Rate"] = lblRateEdit;
        GridView10.EditIndex = -1;
        if (ds_detail_Bank.Tables[0].Rows.Count > 0)
        {
            Div3.Visible = true;
            GridView10.DataSource = ds_detail_Bank.Tables[0];
            GridView10.DataBind();

        }
    }
    //
    public void datanBillpay()
    {
        DataSet dsgrid = dml.grid("SELECT CrAccountCode,DrAccountCode,BillNo,BillDate,GLDATE,BPartnerID,(TaxAmount + AddTaxAmount) as gstclaim,BalanceAmount, BalanceTax,BillBalance,BPartnerName,ApprovedAmount, PurDetail,Sno FROM View_purchaseMater_Detail where sno in (SELECT Sno_Master from Fin_PurchaseDetail  where BalanceAmount > 0) and BPartnerID = '" + ddlBusinessPartner.SelectedItem.Value + "' and (EntryDate <= '" + DateTime.Now.ToString() + "' and EntryDate >= dateadd(day, -30, getdate()))");
        if (dsgrid.Tables[0].Rows.Count > 0)
        {
            GridView7.DataSource = dsgrid.Tables[0];
            GridView7.DataBind();
        }

    }
    protected void GridView7_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lblRowNumber") as Label).Text = (e.Row.RowIndex + 1).ToString();
            Label lblGSTClaimable = e.Row.FindControl("lblGSTClaimable") as Label;
            Label lblBillPayable = e.Row.FindControl("lblBillPayable") as Label;
            Label lblGSTBalance = e.Row.FindControl("lblGSTBalance") as Label;
            Label lblBillBalanceBalance = e.Row.FindControl("lblBillBalanceBalance") as Label;

            Label lblBillDate = e.Row.FindControl("lblBillDate") as Label;
            Label txtBillDateEdit = e.Row.FindControl("txtBillDateEdit") as Label;
            if (lblBillDate != null)
            {
                lblBillDate.Text = dml.dateConvert(lblBillDate.Text);
            }

            if (txtBillDateEdit != null)
            {
                txtBillDateEdit.Text = dml.dateConvert(txtBillDateEdit.Text);
            }
            if (lblGSTClaimable != null)
            {
                dislaydigit_Label(lblGSTClaimable, float.Parse(lblGSTClaimable.Text));
            }
            if (lblBillPayable != null)
            {
                dislaydigit_Label(lblBillPayable, float.Parse(lblBillPayable.Text));
            }
            if (lblGSTBalance != null)
            {
                dislaydigit_Label(lblGSTBalance, float.Parse(lblGSTBalance.Text));
            }
            if (lblBillBalanceBalance != null)
            {
                dislaydigit_Label(lblBillBalanceBalance, float.Parse(lblBillBalanceBalance.Text));
            }



            Label lblVal = (e.Row.FindControl("lblApprovedAmount") as Label);
           

            string val = (e.Row.FindControl("lblApprovedAmount") as Label).Text;

            if (val != null)
            {
                if (val != "")
                {
                    totalval = totalval + float.Parse(val);
                    lbltotalvaldetail.Text = totalval.ToString();

                    dislaydigit_Label(lblVal, float.Parse(val));
                }
            }

          



        }

    }


    protected void GridView7_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView7.EditIndex = e.NewEditIndex;
        datanBillpay();
    }

    protected void GridView7_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DataSet ds_detail_Bank = dml.Find("SELECT CrAccountCode,DrAccountCode,BillNo,BillDate,GLDATE,BPartnerID,(TaxAmount + AddTaxAmount) as gstclaim,BalanceAmount, BalanceTax,BillBalance,BPartnerName, PurDetail,Sno FROM View_purchaseMater_Detail where sno in (SELECT Sno_Master from Fin_PurchaseDetail  where BalanceAmount > 0) and BPartnerID = '" + ddlBusinessPartner.SelectedItem.Value + "' and (EntryDate <= '" + DateTime.Now.ToString() + "' and EntryDate >= dateadd(day, -30, getdate()))");



        string txtBillNoEdit = (GridView7.Rows[e.RowIndex].FindControl("txtBillNoEdit") as TextBox).Text;
        string txtBillDateEdit = (GridView7.Rows[e.RowIndex].FindControl("txtBillDateEdit") as TextBox).Text;
        string txtGLDateEdit = (GridView7.Rows[e.RowIndex].FindControl("txtGLDateEdit") as TextBox).Text;
        string txtSupplierEdit = (GridView7.Rows[e.RowIndex].FindControl("txtSupplierEdit") as TextBox).Text;
        string txtGSTClaimableEdit = (GridView7.Rows[e.RowIndex].FindControl("txtGSTClaimableEdit") as TextBox).Text;
        string txtBillPayableEdit = (GridView7.Rows[e.RowIndex].FindControl("txtBillPayableEdit") as TextBox).Text;
        string txtApprovedAmountEdit = (GridView7.Rows[e.RowIndex].FindControl("txtApprovedAmountEdit") as TextBox).Text;
        string txtGSTBalanceEdit = (GridView7.Rows[e.RowIndex].FindControl("txtGSTBalanceEdit") as TextBox).Text;
        string txtBillBalanceEdit = (GridView7.Rows[e.RowIndex].FindControl("txtBillBalanceEdit") as TextBox).Text;




        GridViewRow row = GridView7.Rows[e.RowIndex];
        DataTable dt = ds_detail_Bank.Tables[0];
        dt.Rows[row.DataItemIndex]["BillNo"] = txtBillNoEdit;
        dt.Rows[row.DataItemIndex]["BillDate"] = txtBillDateEdit;
        if (txtGLDateEdit != "")
        {
            dt.Rows[row.DataItemIndex]["GLDATE"] = txtGLDateEdit;
        }

        dt.Rows[row.DataItemIndex]["BPartnerName"] = txtSupplierEdit;
        dt.Rows[row.DataItemIndex]["gstclaim"] = txtGSTClaimableEdit;
        dt.Rows[row.DataItemIndex]["BalanceAmount"] = txtBillPayableEdit;
       
        //approved amount

        dt.Rows[row.DataItemIndex]["BalanceTax"] = txtGSTBalanceEdit;
        dt.Rows[row.DataItemIndex]["BillBalance"] = txtBillBalanceEdit;
        
        GridView7.EditIndex = -1;
        if (ds_detail_Bank.Tables[0].Rows.Count > 0)
        {
            Div3.Visible = true;
            GridView7.DataSource = ds_detail_Bank.Tables[0];
            GridView7.DataBind();

        }
    }

    protected void GridView7_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView7.EditIndex = -1;
        datanBillpay();
    }

    protected void GridView7_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataSet ds_detail_Bank = dml.Find("SELECT CrAccountCode,DrAccountCode,BillNo,BillDate,GLDATE,BPartnerID,(TaxAmount + AddTaxAmount) as gstclaim,BalanceAmount, BalanceTax,BillBalance,BPartnerName, PurDetail,Sno FROM View_purchaseMater_Detail where sno in (SELECT Sno_Master from Fin_PurchaseDetail  where BalanceAmount > 0) and BPartnerID = '" + ddlBusinessPartner.SelectedItem.Value + "' and (EntryDate <= '" + DateTime.Now.ToString() + "' and EntryDate >= dateadd(day, -30, getdate()))");
        if (ds_detail_Bank.Tables[0].Rows.Count > 0)
        {
            GridViewRow row = GridView7.Rows[e.RowIndex];
            DataTable dt = ds_detail_Bank.Tables[0];
            dt.Rows[e.RowIndex].Delete();
            Div3.Visible = true;
            GridView7.DataSource = ds_detail_Bank.Tables[0];
            GridView7.DataBind();

        }
    }


    protected void ddlitemMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlmaster = GridView10.FooterRow.FindControl("ddlItemMasterFooter") as DropDownList; ;
        DropDownList ddlDr_CR = GridView10.FooterRow.FindControl("ddlDr_CR") as DropDownList; 
         //DropDownList ddlsubitem = GridView10.FooterRow.FindControl("ddlItemSubHeadFooter") as DropDownList;
         RadComboBox ddlaccout = GridView10.FooterRow.FindControl("RadComboAcct_CodeFooter") as RadComboBox;
        if (ddlmaster.SelectedIndex != 0)
        {
            //
            string valitem = "";
            string valitem1 = "";
            string valitemcode = "";
            DropDownList ddluomm = GridView10.FooterRow.FindControl("ddlUOMFooter") as DropDownList;
            dml.dropdownsqlwithquery(ddluomm, "select UOMID,UOMName from SET_UnitofMeasure where UOMID in ((SELECT UOMId as uom FROM SET_ItemMaster where ItemID = '" + ddlmaster.SelectedItem.Value + "' ) UNION (SELECT UOMId2 as uom FROM SET_ItemMaster where ItemID = '" + ddlmaster.SelectedItem.Value + "' ))", "UOMName", "UOMID");

            DataSet ds = dml.Find("select UOMId,UOMId2 from SET_ItemMaster where Description = '" + ddlmaster.SelectedItem.Text + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                valitem = ds.Tables[0].Rows[0]["UOMId"].ToString();
            }
            if (valitem != "")
            {
                ddluomm.ClearSelection();
                if (ddluomm.Items.FindByValue(valitem) != null)
                {
                    ddluomm.Items.FindByValue(valitem).Selected = true;
                }
            }
           
            string dbcr_detail = "";
            DataSet dsdebcr_detail = dml.Find("SELECT	GLImpact, InventoryImpact,AccountType FROM SET_AcRules4Doc WHERE DocId = '" + ddlDocName.SelectedItem.Value + "' AND MasterDetail = 'D' AND CompID = '" + compid() + "' AND Record_Deleted = 0 AND IsActive = 1 AND IsHide = 0 ;");
            if (dsdebcr_detail.Tables[0].Rows.Count > 0)
            {
                dbcr_detail = dsdebcr_detail.Tables[0].Rows[0]["AccountType"].ToString();
            }



            string asset = "0", consum = "0", expense = "0", itemactasset = "0", itemactconsum = "0", itemactexpense = "0", itemtypeid = "0";

            DataSet ds_as_C_E = dml.Find("select IsAsset,IsConsumable,IsExpense,ItemTypeID from SET_ItemMaster where  Record_Deleted = 0 and ItemID = '" + ddlmaster.SelectedItem.Value + "';");
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
                ddlDr_CR.ClearSelection();
                if (ddlDr_CR.Items.FindByText("Debit") != null)
                {
                    ddlDr_CR.Items.FindByText("Debit").Selected = true;
                }

            }
            

        }
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

      (row.FindControl("txtAmount_footer") as TextBox).Text = value.ToString("0.00");
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

      (row.FindControl("txtAmount_footer") as TextBox).Text = value.ToString("0.00");
    }

    public void datashowdetail()
    {
        Div2.Visible = true;
      
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[13]
        {
            
             new DataColumn("CrAccountCodeToDr"),
             new DataColumn("dr_CR_f"),
              new DataColumn("BillNo"),
                new DataColumn("BillDate"),
                new DataColumn("Description"),
                new DataColumn("UOMName"),
                new DataColumn("Quantity"),
                new DataColumn("Rate"),
                new DataColumn("Value"),
                new DataColumn("Remarks"),
                 new DataColumn("Store_Department"),
                new DataColumn("CostCenter"),
                new DataColumn("Project"),
                
        });

        
        ViewState["DirectDetailaa"] = dt;


        this.PopulateGridview10();
        

        DropDownList ddlmaster = GridView10.FooterRow.FindControl("ddlItemMasterFooter") as DropDownList;
        dml.dropdownsql(ddlmaster, "SET_ItemMaster", "Description", "ItemID");

        DropDownList ddluom = GridView10.FooterRow.FindControl("ddlUOMFooter") as DropDownList;
        dml.dropdownsql(ddluom, "SET_UnitofMeasure", "UOMName", "UOMID");

        DropDownList ddlsupFooter = GridView10.FooterRow.FindControl("ddlCostCenterFooter") as DropDownList;
        dml.dropdownsql(ddlsupFooter, "SET_CostCenter", "CostCenterName", "CostCenterID");

        DropDownList ddlLocation = GridView10.FooterRow.FindControl("lblLocationFooter") as DropDownList;
        dml.dropdownsql(ddlLocation, "SET_Department", "DepartmentName", "DepartmentID", "IsWarehouse", "1");

        Div2.Visible = true;
        Div4.Visible = false;

    }
    protected void GridView10_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("AddNew"))
            {
                DataTable dt = (DataTable)ViewState["DirectDetailaa"];
                string AcctCodeDetail = (GridView10.FooterRow.FindControl("RadComboAcct_CodeFooter") as RadComboBox).Text;
                string txtdr_CR_f = (GridView10.FooterRow.FindControl("ddlDr_CR") as DropDownList).SelectedItem.Text;
                string txtdrBillNo_f = (GridView10.FooterRow.FindControl("txtdrBillNo_f") as TextBox).Text;
                string txtbilldate_f = (GridView10.FooterRow.FindControl("txtbilldate_f") as TextBox).Text;
                string ddlItemMasterFooter = (GridView10.FooterRow.FindControl("ddlItemMasterFooter") as DropDownList).SelectedItem.Text;
                string ddlUOMFooter = (GridView10.FooterRow.FindControl("ddlUOMFooter") as DropDownList).SelectedItem.Text;
                string txtDCQtyFooter = (GridView10.FooterRow.FindControl("txtDCQtyFooter") as TextBox).Text;
                string txtRateFooter = (GridView10.FooterRow.FindControl("txtRateFooter") as TextBox).Text;
                string txtAmount_footer = (GridView10.FooterRow.FindControl("txtAmount_footer") as TextBox).Text;
                string txtnarration_footer = (GridView10.FooterRow.FindControl("txtnarration_footer") as TextBox).Text;
                string lblLocationFooter = (GridView10.FooterRow.FindControl("lblLocationFooter") as DropDownList).SelectedItem.Text;
                string ddlCostCenterFooter = (GridView10.FooterRow.FindControl("ddlCostCenterFooter") as DropDownList).SelectedItem.Text;
                string lblProjectFooter = (GridView10.FooterRow.FindControl("lblProjectFooter") as TextBox).Text;

                foreach (GridViewRow g in GridView10.Rows)
                {
                    Label ll = (Label)g.FindControl("lblItemSubHead");
                    Label l = (Label)g.FindControl("lblItemMaster");
                                      

                        if (ViewState["flag"].ToString() == "true")
                        {
                            dt.Rows[0].Delete();
                            ViewState["flag"] = "false";
                        }
                    
                }
                if(ddlItemMasterFooter == "Please select...")
                {
                    ddlItemMasterFooter = "";
                }
                //Please select...

                if(txtdr_CR_f == "  Please select...")
                {
                    txtdr_CR_f = "";
                }


                dt.Rows.Add(AcctCodeDetail, txtdr_CR_f, txtdrBillNo_f, txtbilldate_f, ddlItemMasterFooter, ddlUOMFooter, txtDCQtyFooter, txtRateFooter, txtAmount_footer, txtnarration_footer, lblLocationFooter, ddlCostCenterFooter, lblProjectFooter);

                ViewState["DirectDetailaa"] = dt;
                this.PopulateGridview10();

                DropDownList ddlmaster = GridView10.FooterRow.FindControl("ddlItemMasterFooter") as DropDownList;
                dml.dropdownsql(ddlmaster, "SET_ItemMaster", "Description", "ItemID");

                DropDownList ddluom = GridView10.FooterRow.FindControl("ddlUOMFooter") as DropDownList;
                dml.dropdownsql(ddluom, "SET_UnitofMeasure", "UOMName", "UOMID");

                DropDownList ddlsupFooter = GridView10.FooterRow.FindControl("ddlCostCenterFooter") as DropDownList;
                dml.dropdownsql(ddlsupFooter, "SET_CostCenter", "CostCenterName", "CostCenterID");

                DropDownList ddlLocation = GridView10.FooterRow.FindControl("lblLocationFooter") as DropDownList;
                dml.dropdownsql(ddlLocation, "SET_Department", "DepartmentName", "DepartmentID", "IsWarehouse", "1");
            }
        }
        catch (Exception ex)
        {
            // lblSuccessMessage.Text = "";
            //lblErrorMessage.Text = ex.Message;
        }
    }


    public void detailinsertDetail(string masterid)
    {
        foreach (GridViewRow g in GridView10.Rows)
        {

            Label lblSno = (Label)g.FindControl("lblSno");
            Label lblAccountCode = (Label)g.FindControl("lblAccountCode");
            Label lbldr_CR = (Label)g.FindControl("lbldr_CR");
            Label lblBillNo = (Label)g.FindControl("lblBillNo");
            Label lblBillDate = (Label)g.FindControl("lblBillDate");
            Label lblItemMaster = (Label)g.FindControl("lblItemMaster");
            Label lblUOM = (Label)g.FindControl("lblUOM");
            Label lblDCQty = (Label)g.FindControl("lblDCQty");
            Label lblRate = (Label)g.FindControl("lblRate");
            Label lblValue = (Label)g.FindControl("lblValue");
            Label lblNarration = (Label)g.FindControl("lblNarration");
            Label lblLocation = (Label)g.FindControl("lblLocation");
            Label lblCostCenter = (Label)g.FindControl("lblCostCenter");
            Label lblProject = (Label)g.FindControl("lblProject");
            if (lblSno.Text == "" && lblAccountCode.Text == "" && lbldr_CR.Text == "" && lblBillNo.Text == "" && lblBillDate.Text == ""
                && lblItemMaster.Text == "" && lblUOM.Text == "" && lblDCQty.Text == "" && lblRate.Text == "" && lblValue.Text == ""
                && lblNarration.Text == "" && lblLocation.Text == "" && lblCostCenter.Text == "" && lblProject.Text == "") { }
            else
            {
                string itemmaster, uom1;
                DataSet ds1 = dml.Find("select  ItemID , Description, ItemTypeID, ItemCode from SET_ItemMaster where Description = '" + lblItemMaster.Text + "'");
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    itemmaster = ds1.Tables[0].Rows[0]["ItemID"].ToString();
                }
                else
                {
                    itemmaster = "0";

                }
                DataSet ds2 = dml.Find("select  UOMID,UOMName from SET_UnitofMeasure where UOMName = '" + lblUOM.Text + "'");
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    uom1 = ds2.Tables[0].Rows[0]["UOMID"].ToString();
                }
                else
                {
                    uom1 = "0";
                }
                string loc, costcentr;

                DataSet dsLoc = dml.Find("select DepartmentID,DepartmentName from SET_Department where DepartmentName = '" + lblLocation.Text + "'");
                if (dsLoc.Tables[0].Rows.Count > 0)
                {
                    loc = dsLoc.Tables[0].Rows[0]["DepartmentID"].ToString();
                }
                else
                {
                    loc = "0";
                }
                DataSet dsCost = dml.Find("select CostCenterID,CostCenterName from SET_CostCenter where CostCenterName =  '" + lblCostCenter.Text + "'");
                if (dsCost.Tables[0].Rows.Count > 0)
                {
                    costcentr = dsCost.Tables[0].Rows[0]["CostCenterID"].ToString();
                }
                else
                {
                    costcentr = "0";
                }
                string DocName;
                if (ddlDocName.SelectedIndex != 0)
                {
                    DocName = ddlDocName.SelectedItem.Value;
                }
                else
                {
                    DocName = null;
                }
                string PaymentDetailQuery = "INSERT INTO [Fin_PaymentDetail] ([Sno_Master], [DcNo], [GrnNo],"
                    + " [PoNo], [ItemSubHead], [ItemMaster], [DrAccountCode], [UOM]," +
                    " [Quantity], [Rate], [Value], [ReferNo],"
                    + " [GLReferNo], [Store_Department], [CostCenter], [GocId], [CompId],"
                    + " [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate],  [Record_Deleted], [Project],[BalanceAmount]) "

                    + "VALUES ('" + masterid + "', '" + DocName + "', NULL, NULL, NULL, '" + itemmaster + "',"
                    + " '" + lbldr_CR.Text + "', '" + uom1 + "', '" + lblDCQty.Text + "', '" + lblRate.Text + "', '" + lblValue.Text + "', "
                    + "NULL, NULL, '" + loc + "', '" + costcentr + "', " + gocid() + ", " + compid() + ", " + branchId() + ", " + FiscalYear() + ","
                    + " '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "',"
                    + " '0', '" + lblProject.Text + "','" + lblValue.Text + "');";

                dml.Insert(PaymentDetailQuery, "");
            }
        }

    }

    //RadComboAcct_CodeFooter

    protected void RadComboAcct_CodeFooter_ItemsRequested10(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        RadComboBox RadComboAcct_CodeFooter = GridView10.FooterRow.FindControl("RadComboAcct_CodeFooter") as RadComboBox;

        cmb.serachcombo4(RadComboAcct_CodeFooter, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }


}
