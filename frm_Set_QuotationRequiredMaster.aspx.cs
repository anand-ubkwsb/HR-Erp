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

            dml.dropdownsqlwithquery(ddlDocName, "select DocID,DocDescription from SET_Documents where docid in (select DocID from SET_UserGrp_Documents where UserGrpId = '" + UserGrpID + "' AND	(( StartDate <= '" + sdate + "') AND ((EndDate >= '" + enddate + "') OR (EndDate IS NULL))) and Record_Deleted = 0) and CompID = '" + compid() + "' and Record_Deleted = 0 and MenuId_Sno = '" + menuid + "' and FormId_Sno='" + FormID + "'", "DocDescription", "DocID");
            dml.dropdownsql(ddlFromStroeDept, "SET_Department", "DepartmentName", "DepartmentID");
            // dml.dropdownsqldistinct(dddlReqNo, "SET_StockRequisitionMaster", "RequisitionNo");
            txtReqDate.Attributes.Add("readonly", "readonly");
            txtRequirmentDueDate.Attributes.Add("readonly", "readonly");

            dml.dropdownsql(ddlDocTypes, "SET_DocumentType", "DocName", "DocTypeId");
            dml.dropdownsql(ddlFind_DocType, "SET_DocumentType", "DocName", "DocTypeId");
            dml.dropdownsql(ddlEdit_DocType, "SET_DocumentType", "DocName", "DocTypeId");
            dml.dropdownsql(ddlDel_DocType, "SET_DocumentType", "DocName", "DocTypeId");

            dml.dropdownsql(ddlFind_Doc, "SET_Documents", "DocDescription", "DocID");
            dml.dropdownsql(ddlEdit_Doc, "SET_Documents", "DocDescription", "DocID");
            dml.dropdownsql(ddlDel_Doc, "SET_Documents", "DocDescription", "DocID");
            //select BPartnerId,BPartnerName from SET_BusinessPartner where BPartnerId in (select BPartnerId from SET_BPartnerType where BPNatureID = 37);
            dml.dropdownsql(ddlStatus, "SET_Status", "StatusName", "StatusId");

            //   dml.dropdownsqlwithquery(ddlSupplier, "select BPartnerId,BPartnerName from SET_BusinessPartner where BPartnerId in (select BPartnerId from SET_BPartnerType where BPNatureID = 37)", "BPartnerName", "BPartnerId");
            dml.dropdownsql(ddlSupplier, "ViewSupplierId", "BPartnerName", "Sno");
            //
            CalendarExtender1.EndDate = DateTime.Now;
            CalendarExtender1.StartDate = DateTime.Parse(fiscalstart(fiscal));

            dml.dropdownsqldistinct(ddlFind_Req_No, "SET_StockRequisitionMaster", "RequisitionNo");
            dml.dropdownsqldistinct(ddlEdit_ReqNO, "SET_StockRequisitionMaster", "RequisitionNo");
            dml.dropdownsqldistinct(ddlDel_ReqNO, "SET_StockRequisitionMaster", "RequisitionNo");

            dml.dropdownsql(ddlReqStatus, "SET_Authority", "AuthorityName", "AuthorityId");
            dml.dropdownsql(ddlPriority, "Set_Priority", "PriorityName", "PriorityId");
            dml.dropdownsql(ddllocation, "Set_Location", "LocName", "LocId");
            textClear();

            updatecol.Visible = false;
            Div1.Visible = false;
            Div2.Visible = false;
            Div3.Visible = false;

            Findbox.Visible = false;
            DeleteBox.Visible = false;
            Editbox.Visible = false;
        }
    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        textClear();
        UserGrpID = Request.QueryString["UsergrpID"];
        FormID = Request.QueryString["FormID"];

        menuid = Request.QueryString["Menuid"];
        doctype(menuid, FormID, UserGrpID);
        
       


        if (lstFruits.Items.Count > 0)
        {
            txtReqDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            required_generate();
            btnSave.Visible = true;
            btnEdit.Visible = false;
            btnFind.Visible = false;
            btnCancel.Visible = true;
            btnDelete.Visible = false;
            btnInsert.Visible = false;
            btnUpdate.Visible = false;
            updatecol.Visible = false;
            chkActive.Enabled = true;
            ddlPriority.Enabled = true;
            txtReqDate.Enabled = true;
            ddlReqStatus.Enabled = true;
            ddllocation.Enabled = true;

            ddlFromStroeDept.Enabled = true;
            txtRequirmentDueDate.Enabled = true;
            txtRemarks.Enabled = true;
            imgPopup.Enabled = true;
            ImageButton1.Enabled = true;
            rdbDepartment.Enabled = true;
            rdbPurchase.Enabled = true;
            ddlSupplier.Enabled = true;
            ddlStatus.Enabled = true;
            ddlStatus.ClearSelection();

            if (ddlStatus.Items.FindByText("Open") != null)
            {
                ddlStatus.Items.FindByText("Open").Selected = true;
            }

            ddlPriority.ClearSelection();
            if (ddlPriority.Items.FindByText("Normal") != null)
            {
                ddlPriority.Items.FindByText("Normal").Selected = true;
            }
            

            txtCreatedby.Enabled = false;
            txtCreatedDate.Enabled = false;
            txtUpdateBy.Enabled = true;
            txtUpdateDate.Enabled = true;


            chkActive.Checked = true;


            txtCreatedDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
            rdbDepartment_CheckedChanged(sender, e);
            rdbPurchase_CheckedChanged(sender, e);
            userid = Request.QueryString["UserID"];
            DataSet ds_autoUserName = dml.Find("select user_name from SET_User_Manager where UserId ='" + userid + "'");
            txtCreatedby.Text = ds_autoUserName.Tables[0].Rows[0]["user_name"].ToString();
            //doctype(menuid, FormID, UserGrpID);
            if (ddlDocName.SelectedIndex != 0)
            {
                ddlDocTypes_SelectedIndexChanged(sender, e);
            }
            else
            {
                textClear();
                Label1.Text = "There is no assign documnet";
            }
            txtReqDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            lstFruits.Enabled = true;
        }
        else
        {
            Label1.Text = "There is no requisition for ask quotation";
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        FormID = Request.QueryString["FormID"];

        int chk = 0, chks = 0;

        if (chkActive.Checked == true)
        {
            chk = 1;
        }
        else
        {
            chk = 0;
        }
        string rdb = "";
        if (rdbDepartment.Checked == true)
        {
            rdb = "D";
        }
        if (rdbPurchase.Checked == true)
        {
            rdb = "P";
        }

        string doc = FormID;

        string reqno = "";
        foreach (ListItem item in lstFruits.Items)
        {
            if (item.Selected)
            {
                reqno += item.Text + ",";
            }
        }

        string doctype = ddlDocTypes.SelectedItem.Value;
        string ids = "1";
        string datereqsub;
        if (txtRequirmentDueDate.Text == "")
        {
            datereqsub = "1990-01-01";
        }
        else {
            datereqsub = dml.dateconvertforinsert(txtRequirmentDueDate);
        }

        if (select() > 0)
        {
            DataSet dsCh = dml.Find("INSERT INTO Set_QuotationReqMaster ([DocId], [DocType], [RequisitionType], [QuotationReqNO], [EntryDate], [DocumentStatus], [Priority], [ForStore_Department], [Location], [QuotationSubmissionDate], [RequisitionNo], [Remarks], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted],[Status],[BPartnerTypeID]) VALUES ('" + ddlDocName.SelectedItem.Value + "', '" + ddlDocTypes.SelectedItem.Value + "', '" + rdb + "', '" + txtReqQNo.Text + "', '" + dml.dateconvertforinsert(txtReqDate) + "', '" + ddlReqStatus.SelectedItem.Value + "', '" + ddlPriority.SelectedItem.Value + "', '" + ddlFromStroeDept.SelectedItem.Value + "', '" + ddllocation.SelectedItem.Value + "', '" + datereqsub + "', '" + reqno + "', '" + txtRemarks.Text + "','" + chk + "', " + gocid() + "," + compid() + "," + branchId() + "," + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0','" + ddlStatus.SelectedItem.Value + "', '" + ddlSupplier.SelectedItem.Value + "'); SELECT * FROM Set_QuotationReqMaster WHERE Sno = SCOPE_IDENTITY()");
            if (dsCh.Tables[0].Rows.Count > 0)
            {
                ids = dsCh.Tables[0].Rows[0]["Sno"].ToString();
                ViewState["detailid"] = ids;
            }

            detaisave(ids);

            requisupplier();
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);

            Label1.Text = "";
            textClear();
            btnInsert_Click(sender, e);

            ddlPriority.SelectedIndex = 0;

            txtReqDate.Text = "";
            ddlReqStatus.SelectedIndex = 0;
            ddllocation.SelectedIndex = 0;

            ddlFromStroeDept.SelectedIndex = 0;
            txtRequirmentDueDate.Text = "";
            txtRemarks.Text = "";

            StatusInprocess(reqno.Replace(",", ""));
            txtCreatedDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
        }
        else
        {
            Label1.Text = "Please Select Atleast 1 entry from detail table";
        }

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
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
        string rdb = "";
        if (rdbDepartment.Checked == true)
        {
            rdb = "D";
        }
        if (rdbPurchase.Checked == true)
        {
            rdb = "P";
        }
        string entrydate = dml.dateconvertforinsert(txtReqDate);
        string QuotSubDate = dml.dateconvertforinsert(txtRequirmentDueDate);


        string reqno = "";
        foreach (ListItem item in lstFruits.Items)
        {
            if (item.Selected)
            {
                reqno += item.Text + ",";
            }
        }

        DataSet ds_up = dml.Find("select * from Set_QuotationReqMaster WHERE ([Sno] = '" + ViewState["SNO"].ToString() + "') AND ([DocId]  = '" + FormID + "') AND ([DocType]  = '" + ddlDocTypes.SelectedItem.Value + "') AND ([RequisitionType]  = '" + rdb + "') AND ([QuotationReqNO]  = '" + txtReqQNo.Text + "') AND ([EntryDate]  = '" + entrydate + "') AND ([DocumentStatus]  = '" + ddlReqStatus.SelectedItem.Value + "') AND ([Priority]  = '" + ddlPriority.SelectedItem.Value + "') AND ([ForStore_Department]  = '" + ddlFromStroeDept.SelectedItem.Value + "') AND ([Location]  = '" + ddllocation.SelectedItem.Value + "') AND ([QuotationSubmissionDate]  = '" + QuotSubDate + "') AND ([RequisitionNo]  = '" + reqno + "') AND ([Remarks]  = '" + txtRemarks.Text + "') AND ([IsActive]  = '" + chk + "')  AND ([Record_Deleted]  = '0') AND ([Status] = '" + ddlStatus.SelectedItem.Value + "') AND ([BPartnerTypeID] = '" + ddlSupplier.SelectedItem.Value + "');");

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



            dml.Update("UPDATE Set_QuotationReqMaster SET  [DocId]='" + ddlDocName.SelectedItem.Value + "' , [RequisitionType]='" + rdb + "', [QuotationReqNO]='" + txtReqQNo.Text + "', [EntryDate]='" + entrydate + "', [DocumentStatus]='" + ddlReqStatus.SelectedItem.Value + "', [Priority]='" + ddlPriority.SelectedItem.Value + "', [ForStore_Department]='" + ddlFromStroeDept.SelectedItem.Value + "', [Location]='" + ddllocation.SelectedItem.Value + "', [QuotationSubmissionDate]='" + QuotSubDate + "', [RequisitionNo]='" + reqno + "', [Remarks]='" + txtRemarks.Text + "',[Status] = '" + ddlStatus.SelectedItem.Value + "', [BPartnerTypeID] = '" + ddlSupplier.SelectedItem.Value + "' ,  [IsActive]='" + chk + "',  [UpdatedBy]='" + show_username() + "', [UpdatedDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' WHERE ([Sno] = '" + ViewState["SNO"].ToString() + "')", "");
            detaiEdit();
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
            string squer = "select * from View_AskForQoutation";


            if (ddlDel_Doc.SelectedIndex != 0)
            {
                swhere = "DocId = '" + ddlDel_Doc.SelectedItem.Value + "'";
            }
            else
            {
                swhere = "DocId is not null";
            }
            if (ddlDel_DocType.SelectedIndex != 0)
            {
                swhere = swhere + " and DocType = '" + ddlDel_DocType.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and DocType is not null";
            }
            if (ddlDel_DocStatus.SelectedIndex != 0)
            {
                swhere = swhere + " and DocumentStatus = '" + ddlDel_DocStatus.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and DocumentStatus is not null";
            }
            if (ddlDel_ReqType.SelectedIndex != 0)
            {
                swhere = swhere + " and RequisitionType = '" + ddlDel_ReqType.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and RequisitionType is not null";
            }
            if (ddlDel_Priority.SelectedIndex != 0)
            {
                swhere = swhere + " and Priority = '" + ddlDel_Priority.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and Priority is not null";
            }
            if (ddlDel_QuotReqNo.SelectedIndex != 0)
            {
                swhere = swhere + " and QuotationReqNO = '" + ddlDel_QuotReqNo.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and QuotationReqNO is not null";
            }
            if (ddlDel_ReqNO.SelectedIndex != 0)
            {
                swhere = swhere + " and RequisitionNo = '" + ddlDel_ReqNO.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and RequisitionNo is not null";
            }
            if (chkDel_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkDel_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and CompId = '" + compid() + "' ORDER BY DocDescription";

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
            string squer = "select * from View_AskForQoutation";


            if (ddlFind_Doc.SelectedIndex != 0)
            {
                swhere = "DocId = '" + ddlFind_Doc.SelectedItem.Value + "'";
            }
            else
            {
                swhere = "DocId is not null";
            }
            if (ddlFind_DocType.SelectedIndex != 0)
            {
                swhere = swhere + " and DocType = '" + ddlFind_DocType.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and DocType is not null";
            }
            if (ddlFind_DocStatus.SelectedIndex != 0)
            {
                swhere = swhere + " and DocumentStatus = '" + ddlFind_DocStatus.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and DocumentStatus is not null";
            }
            if (ddlFind_ReqType.SelectedIndex != 0)
            {
                swhere = swhere + " and RequisitionType = '" + ddlFind_ReqType.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and RequisitionType is not null";
            }
            if (ddlFind_Prioity.SelectedIndex != 0)
            {
                swhere = swhere + " and Priority = '" + ddlFind_Prioity.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and Priority is not null";
            }
            if (ddlFind_QuoReqNO.SelectedIndex != 0)
            {
                swhere = swhere + " and QuotationReqNO = '" + ddlFind_QuoReqNO.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and QuotationReqNO is not null";
            }
            if (ddlFind_Req_No.SelectedIndex != 0)
            {
                swhere = swhere + " and RequisitionNo = '" + ddlFind_Req_No.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and RequisitionNo is not null";
            }

            if (chkFind_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkFind_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and CompId = '" + compid() + "' ORDER BY DocDescription";

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
            string squer = "select * from View_AskForQoutation";


            if (ddlEdit_Doc.SelectedIndex != 0)
            {
                swhere = "DocId = '" + ddlEdit_Doc.SelectedItem.Value + "'";
            }
            else
            {
                swhere = "DocId is not null";
            }
            if (ddlEdit_DocType.SelectedIndex != 0)
            {
                swhere = swhere + " and DocType = '" + ddlEdit_DocType.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and DocType is not null";
            }
            if (ddlEdit_DocStatus.SelectedIndex != 0)
            {
                swhere = swhere + " and DocumentStatus = '" + ddlEdit_DocStatus.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and DocumentStatus is not null";
            }
            if (ddlEdit_ReqType.SelectedIndex != 0)
            {
                swhere = swhere + " and RequisitionType = '" + ddlEdit_ReqType.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and RequisitionType is not null";
            }
            if (ddlEdit_Priority.SelectedIndex != 0)
            {
                swhere = swhere + " and Priority = '" + ddlEdit_Priority.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and Priority is not null";
            }
            if (ddlEdit_QuotationReqNO.SelectedIndex != 0)
            {
                swhere = swhere + " and QuotationReqNO = '" + ddlEdit_QuotationReqNO.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and QuotationReqNO is not null";
            }
            if (ddlEdit_ReqNO.SelectedIndex != 0)
            {
                swhere = swhere + " and RequisitionNo = '" + ddlEdit_ReqNO.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and RequisitionNo is not null";
            }
            if (chkEdit_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkEdit_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and CompId = '" + compid() + "'  ORDER BY DocDescription";

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
        chkActive.Checked = false;
        
        ddlPriority.SelectedIndex = 0;
       
        txtReqDate.Text = "";
        ddlReqStatus.SelectedIndex = 0;
        ddllocation.SelectedIndex = 0;
       
        ddlFromStroeDept.SelectedIndex = 0;
        txtRequirmentDueDate.Text = "";
        txtRemarks.Text = "";
        txtReqQNo.Enabled = false;
        ddlSupplier.SelectedIndex = 0;
        ddlSupplier.Enabled = false;

        ddlStatus.Enabled= false;

        ddlStatus.SelectedIndex = 0;
        txtReqQNo.Text = "";
        lbldocno.Text = "";

        txtCreatedby.Text = "";
        txtCreatedDate.Text = "";
        txtUpdateBy.Text = "";
        txtUpdateDate.Text = "";
        chkActive.Enabled = false;

        rdbDepartment.Enabled = false;
        rdbPurchase.Enabled = false;
       
        ddlDocTypes.Enabled = false;
       
        ddlPriority.Enabled = false;
       
        txtReqDate.Enabled = false;
        ddlReqStatus.Enabled = false;
        ddllocation.Enabled = false;
        
        ddlFromStroeDept.Enabled = false;
        txtRequirmentDueDate.Enabled = false;
        txtRemarks.Enabled = false;
        imgPopup.Enabled = false;
        ImageButton1.Enabled = false;

        lstFruits.ClearSelection();
        txtCreatedby.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdateBy.Enabled = false;
        txtUpdateDate.Enabled = false;
        Label1.Text = "";
        Div1.Visible = false;
        Div2.Visible = false;
        Div3.Visible = false;

        UserGrpID = Request.QueryString["UsergrpID"];
        FormID = Request.QueryString["FormID"];

        menuid = Request.QueryString["Menuid"];
        doctype(menuid, FormID, UserGrpID);
        ddlDocName.SelectedIndex = 0;
        ddlDocTypes.SelectedIndex = 0;
        lstFruits.Enabled = false;



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
           


            dml.Delete("update Set_QuotationReqMaster set Record_Deleted = 1 where Sno = " + ViewState["SNO"].ToString() + "", "");
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



            DataSet ds = dml.Find("select * from Set_QuotationReqMaster WHERE ([Sno]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDocName.ClearSelection();
                ddlPriority.ClearSelection();
                ddlReqStatus.ClearSelection();
                ddlFromStroeDept.ClearSelection();
                ddlDocName.Items.FindByValue(ds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;
                ddlReqStatus.Items.FindByValue(ds.Tables[0].Rows[0]["DocumentStatus"].ToString()).Selected = true;
                ddlPriority.Items.FindByValue(ds.Tables[0].Rows[0]["Priority"].ToString()).Selected = true;
                ddlFromStroeDept.Items.FindByValue(ds.Tables[0].Rows[0]["ForStore_Department"].ToString()).Selected = true;
                txtReqQNo.Text = ds.Tables[0].Rows[0]["QuotationReqNO"].ToString();
                ddlSupplier.ClearSelection();
                ddlStatus.ClearSelection();
                ddlSupplier.Items.FindByValue(ds.Tables[0].Rows[0]["BPartnerTypeID"].ToString()).Selected = true;
                ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["Status"].ToString()).Selected = true;
                txtReqDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                string rdb_pd = ds.Tables[0].Rows[0]["RequisitionType"].ToString();
                ddllocation.ClearSelection();
                ddllocation.Items.FindByValue(ds.Tables[0].Rows[0]["Location"].ToString()).Selected = true;
                txtRequirmentDueDate.Text = ds.Tables[0].Rows[0]["QuotationSubmissionDate"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();

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
                if (rdb_pd == "D")
                {
                    rdbDepartment.Checked = true;
                    rdbPurchase.Checked = false;
                }
                if (rdb_pd == "P")
                {
                    rdbPurchase.Checked = true;
                    rdbDepartment.Checked = false;
                }


                if (ds.Tables[0].Rows[0]["CreatedDate"].ToString() == "")
                {
                    txtCreatedDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                    txtCreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdatedDate"].ToString() == "")
                {
                    txtUpdateDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdatedDate"].ToString());
                    txtUpdateDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                dml.dateConvert(txtReqDate);
                dml.dateConvert(txtRequirmentDueDate);

                updatecol.Visible = true;
                string sid = ds.Tables[0].Rows[0]["Sno"].ToString();
                showdetail(sid);
                selectmul(ds.Tables[0].Rows[0]["RequisitionNo"].ToString());
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
        string status = "0";
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
            DataSet ds1 = dml.Find("select Status from Set_QuotationReqMaster where Sno = '" + serial_id + "'");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                status = ds1.Tables[0].Rows[0]["Status"].ToString();

            }

            if (status == "1")
            {

                DataSet ds = dml.Find("select * from Set_QuotationReqMaster WHERE ([Sno]='" + serial_id + "') and Record_Deleted = 0");
                if (ds.Tables[0].Rows.Count > 0)
                {

                    ddlDocName.ClearSelection();
                    ddlPriority.ClearSelection();
                    ddlReqStatus.ClearSelection();
                    ddlFromStroeDept.ClearSelection();
                    ddlDocName.Items.FindByValue(ds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;
                    ddlReqStatus.Items.FindByValue(ds.Tables[0].Rows[0]["DocumentStatus"].ToString()).Selected = true;
                    ddlPriority.Items.FindByValue(ds.Tables[0].Rows[0]["Priority"].ToString()).Selected = true;
                    ddlFromStroeDept.Items.FindByValue(ds.Tables[0].Rows[0]["ForStore_Department"].ToString()).Selected = true;
                    txtReqQNo.Text = ds.Tables[0].Rows[0]["QuotationReqNO"].ToString();
                    ddlSupplier.ClearSelection();
                    ddlStatus.ClearSelection();
                    ddlSupplier.Items.FindByValue(ds.Tables[0].Rows[0]["BPartnerTypeID"].ToString()).Selected = true;
                    ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["Status"].ToString()).Selected = true;
                    txtReqDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                    string rdb_pd = ds.Tables[0].Rows[0]["RequisitionType"].ToString();
                    ddllocation.ClearSelection();
                    ddllocation.Items.FindByValue(ds.Tables[0].Rows[0]["Location"].ToString()).Selected = true;
                    txtRequirmentDueDate.Text = ds.Tables[0].Rows[0]["QuotationSubmissionDate"].ToString();
                    txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();

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
                    if (rdb_pd == "D")
                    {
                        rdbDepartment.Checked = true;
                        rdbPurchase.Checked = false;
                    }
                    if (rdb_pd == "P")
                    {
                        rdbPurchase.Checked = true;
                        rdbDepartment.Checked = false;
                    }


                    if (ds.Tables[0].Rows[0]["CreatedDate"].ToString() == "")
                    {
                        txtCreatedDate.Text = "";
                    }
                    else {
                        DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                        txtCreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                    }
                    if (ds.Tables[0].Rows[0]["UpdatedDate"].ToString() == "")
                    {
                        txtUpdateDate.Text = "";
                    }
                    else {
                        DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdatedDate"].ToString());
                        txtUpdateDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                    }
                    dml.dateConvert(txtReqDate);
                    dml.dateConvert(txtRequirmentDueDate);

                    updatecol.Visible = true;
                    string sid = ds.Tables[0].Rows[0]["Sno"].ToString();
                    showdetailDel(sid);
                    selectmul(ds.Tables[0].Rows[0]["RequisitionNo"].ToString());


                }
            }
            else
            {
                btnDelete_after.Visible = false;

                DataSet ds = dml.Find("select * from Set_QuotationReqMaster WHERE ([Sno]='" + serial_id + "') and Record_Deleted = 0");
                if (ds.Tables[0].Rows.Count > 0)
                {

                    ddlDocName.ClearSelection();
                    ddlPriority.ClearSelection();
                    ddlReqStatus.ClearSelection();
                    ddlFromStroeDept.ClearSelection();
                    ddlDocName.Items.FindByValue(ds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;
                    ddlReqStatus.Items.FindByValue(ds.Tables[0].Rows[0]["DocumentStatus"].ToString()).Selected = true;
                    ddlPriority.Items.FindByValue(ds.Tables[0].Rows[0]["Priority"].ToString()).Selected = true;
                    ddlFromStroeDept.Items.FindByValue(ds.Tables[0].Rows[0]["ForStore_Department"].ToString()).Selected = true;
                    txtReqQNo.Text = ds.Tables[0].Rows[0]["QuotationReqNO"].ToString();
                    ddlSupplier.ClearSelection();
                    ddlStatus.ClearSelection();
                    ddlSupplier.Items.FindByValue(ds.Tables[0].Rows[0]["BPartnerTypeID"].ToString()).Selected = true;
                    ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["Status"].ToString()).Selected = true;
                    txtReqDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                    string rdb_pd = ds.Tables[0].Rows[0]["RequisitionType"].ToString();
                    ddllocation.ClearSelection();
                    ddllocation.Items.FindByValue(ds.Tables[0].Rows[0]["Location"].ToString()).Selected = true;
                    txtRequirmentDueDate.Text = ds.Tables[0].Rows[0]["QuotationSubmissionDate"].ToString();
                    txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();

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
                    if (rdb_pd == "D")
                    {
                        rdbDepartment.Checked = true;
                        rdbPurchase.Checked = false;
                    }
                    if (rdb_pd == "P")
                    {
                        rdbPurchase.Checked = true;
                        rdbDepartment.Checked = false;
                    }


                    if (ds.Tables[0].Rows[0]["CreatedDate"].ToString() == "")
                    {
                        txtCreatedDate.Text = "";
                    }
                    else {
                        DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                        txtCreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                    }
                    if (ds.Tables[0].Rows[0]["UpdatedDate"].ToString() == "")
                    {
                        txtUpdateDate.Text = "";
                    }
                    else {
                        DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdatedDate"].ToString());
                        txtUpdateDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                    }
                    dml.dateConvert(txtReqDate);
                    dml.dateConvert(txtRequirmentDueDate);

                    updatecol.Visible = true;
                    string sid = ds.Tables[0].Rows[0]["Sno"].ToString();
                    //showdetailDel(sid);
                    showdetail(sid);
                    selectmul(ds.Tables[0].Rows[0]["RequisitionNo"].ToString());


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

        //ddlQuotationReqNo.Enabled = true;

       
        string status = "0";

        updatecol.Visible = true;
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {
            string serial_id;
            serial_id = GridView3.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;

            DataSet ds1 = dml.Find("select Status from Set_QuotationReqMaster where Sno = '" + serial_id + "'");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                status = ds1.Tables[0].Rows[0]["Status"].ToString();

            }

            if (status == "1")
            {
            DataSet ds = dml.Find("select * from Set_QuotationReqMaster WHERE ([Sno]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {

                    textClear();

                    btnInsert.Visible = false;
                    btnCancel.Visible = true;
                    btnUpdate.Visible = true;
                    btnDelete.Visible = false;
                    btnEdit.Visible = false;
                    btnFind.Visible = false;
                    Label1.Text = "";
                    ddlPriority.Enabled = true;
                    //dddlReqNo.Enabled = true;
                    txtReqDate.Enabled = true;
                    ddlReqStatus.Enabled = true;
                    ddllocation.Enabled = true;

                    ddlFromStroeDept.Enabled = true;
                    txtRequirmentDueDate.Enabled = true;
                    txtRemarks.Enabled = true;
                    imgPopup.Enabled = true;
                    ImageButton1.Enabled = true;
                    chkActive.Enabled = true;
                    rdbDepartment.Enabled = true;
                    rdbPurchase.Enabled = true;
                    txtCreatedby.Enabled = false;
                    txtCreatedDate.Enabled = false;
                    txtUpdateBy.Enabled = false;
                    txtUpdateDate.Enabled = false;


                    ddlDocName.ClearSelection();
                ddlPriority.ClearSelection();
                ddlReqStatus.ClearSelection();
                ddlFromStroeDept.ClearSelection();
                ddlDocName.Items.FindByValue(ds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;
                ddlReqStatus.Items.FindByValue(ds.Tables[0].Rows[0]["DocumentStatus"].ToString()).Selected = true;
                ddlPriority.Items.FindByValue(ds.Tables[0].Rows[0]["Priority"].ToString()).Selected = true;
                ddlFromStroeDept.Items.FindByValue(ds.Tables[0].Rows[0]["ForStore_Department"].ToString()).Selected = true;
                txtReqQNo.Text = ds.Tables[0].Rows[0]["QuotationReqNO"].ToString();
                ddlSupplier.ClearSelection();
                ddlStatus.ClearSelection();
                ddlSupplier.Items.FindByValue(ds.Tables[0].Rows[0]["BPartnerTypeID"].ToString()).Selected = true;
                ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["Status"].ToString()).Selected = true;
                txtReqDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                string rdb_pd = ds.Tables[0].Rows[0]["RequisitionType"].ToString();
                ddllocation.ClearSelection();
                ddllocation.Items.FindByValue(ds.Tables[0].Rows[0]["Location"].ToString()).Selected = true;
                txtRequirmentDueDate.Text = ds.Tables[0].Rows[0]["QuotationSubmissionDate"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();

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
                if (rdb_pd == "D")
                {
                    rdbDepartment.Checked = true;
                    rdbPurchase.Checked = false;
                }
                if (rdb_pd == "P")
                {
                    rdbPurchase.Checked = true;
                    rdbDepartment.Checked = false;
                }


                if (ds.Tables[0].Rows[0]["CreatedDate"].ToString() == "")
                {
                    txtCreatedDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                    txtCreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdatedDate"].ToString() == "")
                {
                    txtUpdateDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdatedDate"].ToString());
                    txtUpdateDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                dml.dateConvert(txtReqDate);
                dml.dateConvert(txtRequirmentDueDate);

                updatecol.Visible = true;
                string sid = ds.Tables[0].Rows[0]["Sno"].ToString();
                     showdetailEdit(sid);
                   
                    selectmul(ds.Tables[0].Rows[0]["RequisitionNo"].ToString());


            }
        }
            else
            {

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


    //protected void dddlReqNo_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    if (lstFruits.SelectedIndex != 0)
    //    {

    //            showdata();

    //    }
    //    else
    //    {
    //        Div1.Visible = false;
    //    }
    //    select();

    //}

    protected void Button1_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow g in GridView5.Rows)
        {
            ListBox lstbox = (ListBox)g.FindControl("ListBox1");
            Label lblheadsub = (Label)g.FindControl("lblsubhead");
            Label lblitemmaster = (Label)g.FindControl("lblitemmaster");
            Label lbluom = (Label)g.FindControl("lbluom");
            Label lblreqQ = (Label)g.FindControl("lblreqQ");

            DataSet ds = dml.Find("select  * from Set_QuotationReqDetail where ItemSubHead = '" + lblheadsub.Text + "' and ItemMaster = '" + lblitemmaster.Text + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {

            }
            else
            {


                if (lstbox.SelectedIndex != -1)
                {


                    dml.Insert("INSERT INTO Set_QuotationReqDetail ([ItemSubHead], [ItemMaster], [UOM], [Quantity], [Supplier], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate],  [Record_Deleted]) VALUES ('" + lblheadsub.Text + "', '" + lblitemmaster.Text + "', '" + lbluom.Text + "', '" + lblreqQ.Text + "', '" + lstbox.SelectedItem.Text + "', '1', " + gocid() + "," + compid() + "," + branchId() + "," + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0');", "");
                    string name = lstbox.SelectedItem.Text;
                    DataSet ds_email = dml.Find("select BPartnerName, EmailAddress from SET_BusinessPartner WHERE BPartnerId in (select BPartnerId from SET_BPartnerType where BPNatureID = 1) and BPartnerName='" + name + "'");
                    if (ds_email.Tables[0].Rows.Count > 0)
                    {

                        string email = ds_email.Tables[0].Rows[0]["EmailAddress"].ToString();
                        emails(email, name);
                    }
                }
                else
                {
                    Button2.Text = "Not insert";
                }

            }
        }
    }

    public void requisupplier()
    {
        foreach (GridViewRow g in GridView5.Rows)
        {
            ListBox lstbox = (ListBox)g.FindControl("ListBox1");
            Label lblheadsub = (Label)g.FindControl("lblsubhead");
            Label lblitemmaster = (Label)g.FindControl("lblitemmaster");
            Label lblsupp = (Label)g.FindControl("lblsupp");
            Label lbluom = (Label)g.FindControl("lbluom");
            Label lblreqQ = (Label)g.FindControl("lblreqQ");
            Label lblStockQ = (Label)g.FindControl("lblStockQ");

            Label lblsubheadid = (Label)g.FindControl("lblsubheadid");
            Label lblitemmasterid = (Label)g.FindControl("lblitemmasterid");
            Label lbluomid = (Label)g.FindControl("lbluomid");

            CheckBox chkselect = (CheckBox)g.FindControl("chkSelect");
            if (chkselect.Checked == true)
            {
                DataSet ds = dml.Find("select  * from Set_QuotationReqDetail where ItemSubHead = '" + lblsubheadid.Text + "' and ItemMaster = '" + lblitemmasterid.Text + "'");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string sno = ds.Tables[0].Rows[0]["Sno"].ToString();
                    //UPDATE Set_QuotationReqDetail SET  [Supplier]='ZIA TRADERS (ASIF)', [UpdatedBy]=NULL, [UpdatedDate]=NULL WHERE ([Sno]='11') 
                    dml.Update("UPDATE Set_QuotationReqDetail SET  [Supplier]='" + ddlSupplier.SelectedItem.Text + "', [UpdatedBy]='" + show_username() + "', [UpdatedDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' WHERE ([Sno]='" + sno + "') ", "");
                    string name = ddlSupplier.SelectedItem.Text;
                    DataSet ds_email = dml.Find("select BPartnerName, EmailAddress from SET_BusinessPartner WHERE BPartnerId in (select BPartnerId from SET_BPartnerType where BPNatureID = 1) and BPartnerName='" + name + "'");
                    if (ds_email.Tables[0].Rows.Count > 0)
                    {

                        string email = ds_email.Tables[0].Rows[0]["EmailAddress"].ToString();
                        emails(email, name);
                    }
                }
                else
                {


                    if (lstbox.SelectedIndex != 0)
                    {


                        dml.Insert("INSERT INTO Set_QuotationReqDetail ([QuoatReqMId], [ItemSubHead], [ItemMaster], [UOM], [StockQuantity], [Supplier], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate],  [Record_Deleted],[ReqQuantity]) VALUES ('" + ViewState["detailid"].ToString() + "','" + lblsubheadid.Text + "', '" + lblitemmasterid.Text + "', '" + lbluomid.Text + "', '" + lblStockQ.Text + "', '" + lblsupp.Text + "', '1', " + gocid() + "," + compid() + "," + branchId() + "," + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0' , '" + lblreqQ.Text + "');", "");
                        string name = lblsupp.Text;
                        DataSet ds_email = dml.Find("select BPartnerName, EmailAddress from SET_BusinessPartner WHERE BPartnerId in (select BPartnerId from SET_BPartnerType where BPNatureID = 1) and BPartnerName='" + name + "'");
                        if (ds_email.Tables[0].Rows.Count > 0)
                        {

                            string email = ds_email.Tables[0].Rows[0]["EmailAddress"].ToString();
                            emails(email, name);
                        }
                    }
                    else
                    {
                        Button2.Text = "Not insert";
                    }

                }
            }
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

    public int select()
    {
        int count = 0;
        foreach (GridViewRow g in GridView5.Rows)
        {

            //chkSelect

            CheckBox chh = g.FindControl("chkSelect") as CheckBox;
            if(chh.Checked == true)
            {
                count = count + 1;
            }

        }
        return count;

    }

    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lblRowNumber") as Label).Text = (e.Row.RowIndex + 1).ToString();
            //lblSno
            (e.Row.FindControl("lblSno") as Label).Visible = false;
            //txtLocation.Text = (e.Row.FindControl("lblitemmaster") as Label).Text;
            (e.Row.FindControl("LinkButton1") as LinkButton).Visible = false;
            (e.Row.FindControl("LinkButton4") as LinkButton).Visible = false;

            dml.dropdownsql((e.Row.FindControl("DropDownList112") as DropDownList), "View_Bus_Supplier", "BPartnerName", "BPartnerId");

        }
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        int index = int.Parse(ViewState["idsno"].ToString());
        ListBox lstbox = (ListBox)GridView5.Rows[index].FindControl("ListBox1");
        ListItem items = new ListItem();
        items.Text = "aaaaaaa";
        lstbox.Items.Add(items);
    }

    protected void GridView4_SelectedIndexChanged(object sender, EventArgs e)
    {
        LinkButton linkbtn = (LinkButton)GridView5.SelectedRow.FindControl("LinkButton1");
        LinkButton linkbtn2 = (LinkButton)GridView5.SelectedRow.FindControl("LinkButton4");
        linkbtn.Visible = true;
        linkbtn2.Visible = true;
        int id = GridView5.SelectedIndex;
        ViewState["idsno"] = id;




    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        ListBox lstbox = (ListBox)GridView5.Rows[int.Parse(ViewState["idsno"].ToString())].FindControl("ListBox1");
        lstbox.Items.Clear();


    }

    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList dropbtn = (DropDownList)GridView5.Rows[int.Parse(ViewState["ddlid"].ToString())].FindControl("DropDownList112");
        int index = int.Parse(ViewState["ddlid"].ToString());
        ListBox lstbox = (ListBox)GridView5.Rows[index].FindControl("lstboxedit");
        Label lblsupplier = (Label)GridView5.Rows[index].FindControl("lblsupp");
        lblsupplier.Text = dropbtn.SelectedItem.Text;

        lstbox.Items.Clear();

        ListItem items = new ListItem();
        items.Text = dropbtn.SelectedItem.Text;

        lstbox.Items.Add(items);

        lstbox.SelectedIndex = 0;


    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = (LinkButton)GridView5.Rows[int.Parse(ViewState["idsno"].ToString())].FindControl("LinkButton1");
        linkbtn.Visible = false;
        int id = GridView5.SelectedIndex;

    }

    protected void GridView4_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        LinkButton linkbtn = (LinkButton)GridView5.Rows[int.Parse(ViewState["idsno"].ToString())].FindControl("LinkButton1");
        LinkButton linkbtn4 = (LinkButton)GridView5.Rows[int.Parse(ViewState["idsno"].ToString())].FindControl("LinkButton4");
        linkbtn.Visible = false;
        linkbtn4.Visible = false;
        int id = GridView5.SelectedIndex;
    }

    public void detaisave(string id)
    {
        //INSERT INTO Set_QuotationReqDetail ([Sno], [ItemSubHead], [ItemMaster], [UOM], [Quantity], [Supplier], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [Record_Deleted]) VALUES ('7', 'PACKING MATERIAL', 'Air Compresser (Assets)', '3', '6', 'ATTOCK TRADERS - TD', '1', '1', '1', '5', '3', 'Fahad Siddiqui', '2020-03-04 11:34:39.5248', NULL, NULL, '0');
        for (int a = 0; a < GridView5.Rows.Count; a++)
        {


            CheckBox chksel = (CheckBox)GridView5.Rows[a].FindControl("chkSelect");
            //lblsubheadid
            Label lblsubheadid = (Label)GridView5.Rows[a].FindControl("lblsubheadid");
            Label lblreq = (Label)GridView5.Rows[a].FindControl("lblreq");
            Label lblsubhead = (Label)GridView5.Rows[a].FindControl("lblsubhead");
            Label lblitemmaster = (Label)GridView5.Rows[a].FindControl("lblitemmaster");
            Label lblitemmasterid = (Label)GridView5.Rows[a].FindControl("lblitemmasterid");
            Label lbluom = (Label)GridView5.Rows[a].FindControl("lbluom");
            Label lbluomid = (Label)GridView5.Rows[a].FindControl("lbluomid");
            Label lblStockQ = (Label)GridView5.Rows[a].FindControl("lblStockQ");
            Label lblreqQ = (Label)GridView5.Rows[a].FindControl("lblreqQ");
            Label lblsupp = (Label)GridView5.Rows[a].FindControl("lblsupp");
            ListBox lstbox = (ListBox)GridView5.Rows[a].FindControl("ListBox1");
            if (chksel.Checked == true)
            {
                dml.Insert("INSERT INTO Set_QuotationReqDetail ([QuoatReqMId], [ItemSubHead], [ItemMaster], [UOM], [StockQuantity],[ReqQuantity], [Supplier], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted],[RequisitionNo],[BalanceQty]) VALUES "
                                                        + "('" + id + "', '" + lblsubheadid.Text + "', '" + lblitemmasterid.Text + "', '" + lbluomid.Text + "', '" + lblStockQ.Text + "', '" + lblreqQ.Text + "','" + ddlSupplier.SelectedItem.Value + "', '1', " + gocid() + ", " + compid() + ", " + branchId() + ", " + FiscalYear() + ", '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0','"+ lblreq.Text+ "','"+lblreqQ.Text+"');", "");
            }
        }
    }

    public void detaiEdit()
    {
        //INSERT INTO Set_QuotationReqDetail ([Sno], [ItemSubHead], [ItemMaster], [UOM], [Quantity], [Supplier], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [Record_Deleted]) VALUES ('7', 'PACKING MATERIAL', 'Air Compresser (Assets)', '3', '6', 'ATTOCK TRADERS - TD', '1', '1', '1', '5', '3', 'Fahad Siddiqui', '2020-03-04 11:34:39.5248', NULL, NULL, '0');
        for (int a = 0; a < GridView5.Rows.Count; a++)
        {
            Label lblsno = (Label)GridView5.Rows[a].FindControl("lblSno");
            Label lblreq = (Label)GridView5.Rows[a].FindControl("lblreq");
            Label lblsubhead = (Label)GridView5.Rows[a].FindControl("lblsubhead");
            Label lblitemmaster = (Label)GridView5.Rows[a].FindControl("lblitemmaster");
            Label lbluom = (Label)GridView5.Rows[a].FindControl("lbluom");
            Label lblStockQ = (Label)GridView5.Rows[a].FindControl("lblStockQ");
            Label lblreqQ = (Label)GridView5.Rows[a].FindControl("lblreqQ");
            ListBox lstbox = (ListBox)GridView5.Rows[a].FindControl("ListBox1");
            if (lstbox.Items.Count > 0)
            {
                dml.Update("UPDATE Set_QuotationReqDetail SET [ItemSubHead]='" + lblsubhead.Text + "', [ItemMaster]='" + lblitemmaster.Text + "', [UOM]='" + lbluom.Text + "', [StockQuantity]='" + lblStockQ.Text + "', [Supplier]='" + lstbox.SelectedItem.Text + "', [UpdatedBy]='" + show_username() + "', [UpdatedDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', [ReqQuantity]='" + lblreqQ.Text + "' WHERE ([Sno]='" + lblsno.Text + "')", "");
            }
        }
    }

    public bool entrycheck()
    {
        bool flag = false;
        for (int a = 0; a < GridView5.Rows.Count; a++)
        {
            Label lblreq = (Label)GridView5.Rows[a].FindControl("lblreq");
            Label lblsubhead = (Label)GridView5.Rows[a].FindControl("lblsubhead");
            Label lblitemmaster = (Label)GridView5.Rows[a].FindControl("lblitemmaster");
            Label lbluom = (Label)GridView5.Rows[a].FindControl("lbluom");
            Label lblStockQ = (Label)GridView5.Rows[a].FindControl("lblStockQ");
            Label lblreqQ = (Label)GridView5.Rows[a].FindControl("lblreqQ");
            ListBox lstbox = (ListBox)GridView5.Rows[a].FindControl("ListBox1");

            DataSet ds = dml.Find("select * from Set_QuotationReqDetail where ItemSubHead='" + lblsubhead.Text + "' and ItemMaster='" + lblitemmaster.Text + "' and GocID='" + gocid() + "' and CompId ='" + compid() + "' and BranchId = '" + branchId() + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                flag = true;
            }
            else
            {
                flag = false;
            }

        }
        return flag;
    }

    public void showdetail(string qoutMid)
    {
        Div2.Visible = true;
        string query = "select * from View_SetQUOT where QuoatReqMId = '" + qoutMid + "'";
        DataSet ds = dml.grid(query);

        if (ds.Tables[0].Rows.Count > 0)
        {

            GridView6.DataSource = ds;
            GridView6.DataBind();
            for (int a = 0; a < ds.Tables[0].Rows.Count; a++)
            {
                ListBox lstbox = (ListBox)GridView6.Rows[a].FindControl("ListBox1");

                ListItem items = new ListItem();
                items.Text = ds.Tables[0].Rows[a]["Supplier"].ToString();
                //lstbox.Items.Add(items);


            }
        }
        else
        {
            GridView6.ShowHeaderWhenEmpty = true;
            GridView6.EmptyDataText = "NO RECORD";
            GridView6.DataBind();
        }
    }
    public void showdetailDel(string qoutMid)
    {
        Div3.Visible = true;
        string query = "select * from View_SetQUOT where QuoatReqMId = '" + qoutMid + "'";
        DataSet ds = dml.grid(query);

        if (ds.Tables[0].Rows.Count > 0)
        {

            GridView7.DataSource = ds;
            GridView7.DataBind();
            for (int a = 0; a < ds.Tables[0].Rows.Count; a++)
            {
                ListBox lstbox = (ListBox)GridView7.Rows[a].FindControl("ListBox1");

                ListItem items = new ListItem();
                items.Text = ds.Tables[0].Rows[a]["Supplier"].ToString();
               // lstbox.Items.Add(items);


            }
        }
        else
        {
            GridView7.ShowHeaderWhenEmpty = true;
            GridView7.EmptyDataText = "NO RECORD";
            GridView7.DataBind();
        }
    }

    public void showdetailEdit(string qoutMid)
    {
        Div1.Visible = true;
        string query = "select * from View_SetQUOT where QuoatReqMId = '" + qoutMid + "'";
        DataSet ds = dml.grid(query);

        if (ds.Tables[0].Rows.Count > 0)
        {

            GridView5.DataSource = ds;
            GridView5.DataBind();
            for (int a = 0; a < ds.Tables[0].Rows.Count; a++)
            {
                ListBox lstbox = (ListBox)GridView5.Rows[a].FindControl("ListBox1");
                CheckBox cks = (CheckBox)GridView5.Rows[a].FindControl("chkSelect");
                CheckBox cksall = (CheckBox)GridView5.HeaderRow.FindControl("chkall");
                cks.Visible = false;
                cksall.Visible = false;
                ListItem items = new ListItem();
                items.Text = ds.Tables[0].Rows[a]["Supplier"].ToString();
                // lstbox.Items.Add(items);
                

            }
        }
        else
        {
            GridView5.ShowHeaderWhenEmpty = true;
            GridView5.EmptyDataText = "NO RECORD";
            GridView5.DataBind();
        }
    }

    protected void GridView4_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridView5.Rows[e.RowIndex].Visible = false;
    }

    protected void GridView4_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView5.EditIndex = e.NewEditIndex;
        showdata();

    }

    public void showdata()
    {

        Div1.Visible = true;
        int count = 1;
        string reqno = "";
        foreach (ListItem item in lstFruits.Items)
        {

            if (item.Selected)
            {
                if (count == 1)
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
        if (count > 1)
        {
            q = "select * from View_StockReq where RequisitionNo in (" + reqno + ")";
        }
        else
        {
            q = "select * from View_StockReq where RequisitionNo in (0)";
        }

        //  string query = "select * from View_StockReq where RequisitionNo = '" + dddlReqNo.SelectedItem.Text+ "' order by ItemSubHead ,ItemMaster";
        DataSet ds = dml.grid(q);

        if (ds.Tables[0].Rows.Count > 0)
        {

            GridView5.DataSource = ds;
            GridView5.DataBind();

        }
        else
        {
            GridView5.ShowHeaderWhenEmpty = true;
            GridView5.EmptyDataText = "NO RECORD";
            GridView5.DataBind();
        }

    }
    protected void GridView5_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (IsPostBack)
        {
            GridView5.EditIndex = e.NewEditIndex;
          //   showdata();

           

             ViewState["ddlid"] = e.NewEditIndex;
            string lblsno = (GridView5.Rows[int.Parse(ViewState["ddlid"].ToString())].FindControl("lblSno") as Label).Text;

            ViewState["editindex"] = lblsno;
            showdetailEdit(lblsno);

            //LinkButton linkbtn = (LinkButton)GridView5.Rows[int.Parse(ViewState["ddlid"].ToString())].FindControl("LinkButton1");
            //linkbtn.Visible = true;
           // dml.dropdownsql(DropDownList112, "View_Bus_Supplier", "BPartnerName", "BPartnerId");
        }

    }

    protected void GridView5_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView5.EditIndex = -1;


       // showdata();

       
        //string lblsno = (GridView5.Rows[int.Parse(ViewState["ddlid"].ToString())].FindControl("lblSno") as Label).Text;
        showdetailEdit(ViewState["editindex"].ToString());
    }

    protected void GridView5_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label id = GridView5.Rows[e.RowIndex].FindControl("lblSno") as Label;
        Label id_detail = GridView5.Rows[e.RowIndex].FindControl("lblSno") as Label;
        TextBox cs = GridView5.Rows[e.RowIndex].FindControl("txtCss") as TextBox;
        TextBox rs = GridView5.Rows[e.RowIndex].FindControl("txtReqQ") as TextBox;
        Label lblsupplier = (Label)GridView5.Rows[e.RowIndex].FindControl("lblsupp");
     

        dml.Update("UPDATE Set_QuotationReqDetail SET [StockQuantity]='" + cs.Text + "', [ReqQuantity]='" + rs.Text + "' WHERE ([Sno]='" + id_detail.Text + "') ", "");
        GridView5.EditIndex = -1;
        //showdata();
        showdetailEdit(ViewState["editindex"].ToString());
       // lstadd();
    }

    protected void GridView5_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lblRowNumber") as Label).Text = (e.Row.RowIndex + 1).ToString();
            //lblSno
            (e.Row.FindControl("lblSno") as Label).Visible = false;
            (e.Row.FindControl("LinkButton1") as LinkButton).Visible = false;

        }
    }

    public void lstadd()
    {
        ListBox lstadd = GridView5.Rows[int.Parse(ViewState["ddlid"].ToString())].FindControl("ListBox1") as ListBox;
        Label lblsupplier = (Label)GridView5.Rows[int.Parse(ViewState["ddlid"].ToString())].FindControl("lblsupp");
        ListItem itemss = new ListItem();

        if (ViewState["lsteditvalue"] != null)
        {

            string str = ViewState["lsteditvalue"].ToString();
            itemss.Text = ViewState["lsteditvalue"].ToString();
            supplier[0] = str;
            lstadd.Items.Add(itemss);

            lblsupplier.Text = str.ToString();
            lstadd.SelectedIndex = 0;
        }

    }

    public void doctype(string menuid, string formid, string usergrpid )
    {
        //        DataSet ds = dml.Find("select * from ViewUserGrp_Doc where MenuId_Sno='"+ menuid + "' and FormId_Sno= '"+formid+"'");
        //FF43B221-F9E1-4423-AA61-F12880A9E13D
        ddlDocName.ClearSelection();

       
        //dml.dropdownsql2where(ddlDocName, "ViewUserGrp_Doc", "DocDescription", "DocID", "MenuId_Sno", menuid, "FormId_Sno", formid, "UserGrpId", usergrpid);

        // dml.dropdownsqlwithquery(ddlDocName, "select * from SET_DocumentType where DocTypeId in( select DocTypeId from SET_Documents where MenuId_Sno = '"+menuid+"' and FormId_Sno='"+formid+"')", "DocName", "DocTypeId");
        // dml.d

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


        //      ddlDocTypes.Items.FindByValue(ds.Tables[0].Rows[0]["DocTypeId"].ToString()).Selected = true;



    }

    protected void chkall_CheckedChanged(object sender, EventArgs e)
    {
        int co = 0;
        int ab = 0;
        CheckBox chkall = (CheckBox)GridView5.HeaderRow.FindControl("chkall");
        bool a = ((CheckBox)GridView5.HeaderRow.FindControl("chkall")).Checked;
        for (int i = 0; i < GridView5.Rows.Count; i++)
        {

            if (a == true)
            {
                ((CheckBox)GridView5.Rows[i].FindControl("chkSelect")).Checked = true;
                co = co + 1;

            }
            if (a == false)
            {
                ((CheckBox)GridView5.Rows[i].FindControl("chkSelect")).Checked = false;
                ab = ab + 1;

                //selectcheck();

            }


        }
        if (GridView5.Rows.Count == co)
        {
            chkall.Checked = true;
        }

        else if (GridView5.Rows.Count == ab)
        {
            for (int q = 0; q < ab; q++)
            {
                ((CheckBox)GridView5.Rows[q].FindControl("chkSelect")).Checked = false;
                //  selectcheck();
            }
        }
        else
        {
            //  selectcheck();

        }
    }

    protected void rdbDepartment_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbDepartment.Checked == true)
        {
            txtRequirmentDueDate.Enabled = false;
            ImageButton1.Enabled = false;
        }
    }

    protected void rdbPurchase_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbPurchase.Checked == true)
        {
            txtRequirmentDueDate.Enabled = true;
            ImageButton1.Enabled = true;
        }
    }

    public void required_generate()
    {
        //string month = DateTime.Now.Month.ToString("00");

        DateTime date = DateTime.Parse(txtReqDate.Text);
        string year = date.ToString("yy");
        string month = date.Month.ToString("00");
        //string month = date.ToString("00");
        string docno = ddlDocName.SelectedItem.Value;

        bool flag = true;
        bool flag1 = true;
      //  DateTime date = DateTime.Now;
       // string year = date.ToString("yy");
        FormID = Request.QueryString["FormID"];
        fiscal = Request.QueryString["fiscaly"];
        string fy = fiscal.Substring(2, 2) + fiscal.Substring(7, 2);
        string docval = "0";
        if (ddlDocName.SelectedIndex != 0)
        {
            docval = ddlDocName.SelectedItem.Value;
        }

        DataSet ds = dml.Find("select Monthlybase, YearlyBase from SET_Documents where DocID = '" + docval + "'; select MAX(QuotationReqNO) as maxno from Set_QuotationReqMaster where SUBSTRING(QuotationReqNO, 1, 4) = '" + month + year + "'; select Description from SET_Fiscal_Year where Description = '" + fiscal + "'; select MAX(QuotationReqNO) as maxno from Set_QuotationReqMaster where SUBSTRING(QuotationReqNO, 1, 4) = '" + fy + "'");

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

                txtReqQNo.Text = docno + "-" + month + year + "-" + incre.ToString("00000");
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
                txtReqQNo.Text = docno + "-" + fyear.Substring(2, 2) + fyear.Substring(7, 2) + "-" + incre.ToString("000000");


            }

        }
    }

    protected void lstFruits_SelectedIndexChanged(object sender, EventArgs e)
    {
        Div1.Visible = true;
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
        if (count > 0)
        {
            q = "select * from View_StockReq where RequisitionNo in (" + reqno + ")";
        }
        else
        {
            q = "select * from View_StockReq where RequisitionNo in ('0')";
        }


        // string query = "select * from View_StockReq where RequisitionNo = '" + dddlReqNo.SelectedItem.Text + "' order by ItemSubHead ,ItemMaster";
        DataSet ds = dml.grid(q);

        if (ds.Tables[0].Rows.Count > 0)
        {

            GridView5.DataSource = ds;
            GridView5.DataBind();

        }
        else
        {
            GridView5.ShowHeaderWhenEmpty = true;
            GridView5.EmptyDataText = "NO RECORD";
            GridView5.DataBind();
        }

    }

    //protected void Submit(object sender, EventArgs e)
    //{
    //    string message = "";
    //    foreach (ListItem item in lstFruits.Items)
    //    {
    //        if (item.Selected)
    //        {
    //            message += item.Text + " " + item.Value + "\\n";
    //        }
    //    }
    //    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('" + message + "');", true);
    //}
    public void selectmul(string s)
    {

        string[] s1 = Regex.Split(s, ",");

        foreach (string s2 in s1)
        {
            //  txtLocation.Text += s2;
            foreach (ListItem item in lstFruits.Items)
            {

                if (item.Text == s2)
                {
                    item.Selected = true;

                }
            }



        }

    }
    protected void ddlDocTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        required_generate();
        if (ddlDocName.SelectedIndex != 0)
        {
            int docno = int.Parse(ddlDocName.SelectedItem.Value);

            dml.dropdownsql(ddlDocTypes, "SET_DocumentType", "DocName", "DocTypeId");


            DataSet ds1 = dml.Find("select SET_Documents.docid,SET_Documents.DocDescription,SET_Documents.DocTypeId, SET_DocumentType.DocName from SET_Documents LEFT JOIN SET_DocumentType on SET_DocumentType.DocTypeId = SET_Documents.DocTypeId where SET_Documents.DocID = '" + ddlDocName.SelectedItem.Value + "'");

            ddlDocTypes.ClearSelection();
            ddlDocTypes.Items.FindByValue(ds1.Tables[0].Rows[0]["DocTypeId"].ToString()).Selected = true;



            lbldocno.Text = docno.ToString("0000");
        }
        else
        {
            ddlDocTypes.SelectedIndex = 0;
            lbldocno.Text = "";
            txtReqQNo.Text = "";
        }




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

                    dml.dropdownsqlwithquery(ddlReqStatus, "select AuthorityId, AuthorityName from SET_Authority where AuthorityId in (SELECT AuthorityId from SET_UserGrp_DocAuthority where docid= '" + ddlDocName.SelectedItem.Value + "')", "AuthorityName", "AuthorityId");

                    if (ddlReqStatus.Items.Count > 2)
                    {
                        ddlReqStatus.Enabled = true;
                        ddlReqStatus.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlReqStatus.Enabled = false;
                        ddlReqStatus.SelectedIndex = 1;
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
    public string fiscalstart(string fyear)
    {
        string sdate;
        DataSet ds = dml.Find("select StartDate from SET_Fiscal_Year where Description = '" + fyear + "'");
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
    public string supllieremail()
    {
        string emailid;
        DataSet ds = dml.Find("select BPartnerId,BPartnerName,EmailAddress from SET_BusinessPartner where BPartnerId in (select BPartnerID from SET_BPartnerType where BPNatureID in (select BPNatureID from SET_BPartnerNature where BPNatureID = 1) and GocID = '" + gocid() + "')");

        if (ds.Tables[0].Rows.Count > 0)
        {
            emailid = ds.Tables[0].Rows[0]["EmailAddress"].ToString();
        }
        else
        {
            emailid = "";
        }
        return emailid;

    }
    protected void GridView6_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lblRowNumber") as Label).Text = (e.Row.RowIndex + 1).ToString();
            //lblSno
            (e.Row.FindControl("lblSno") as Label).Visible = false;
            (e.Row.FindControl("LinkButton1") as LinkButton).Visible = false;

        }
    }
    protected void GridView7_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lblRowNumber") as Label).Text = (e.Row.RowIndex + 1).ToString();
            //lblSno
            (e.Row.FindControl("lblSno") as Label).Visible = false;
            (e.Row.FindControl("LinkButton1") as LinkButton).Visible = false;

        }
    }
    protected void GridView7_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = GridView7.Rows[e.RowIndex];
        string lblsno = (GridView7.Rows[e.RowIndex].FindControl("lblSno") as Label).Text;
               
        dml.Delete("DELETE from Set_QuotationReqDetail where Sno= '" + lblsno + "'", "");
        //dt.Rows[e.RowIndex].Delete();
        showdetailDel(lblsno);
    }
    public void StatusInprocess(string masID)
    {
        dml.Update("update SET_StockRequisitionMaster set Status = 6 where RequisitionNo = '"+masID+"'","");
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

    protected void txtReqDate_TextChanged(object sender, EventArgs e)
    {
        required_generate();
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
