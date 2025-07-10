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
    string userid, UserGrpID, FormID, fiscal, menuid;
    DmlOperation dml = new DmlOperation();
    radcomboxclass cmb = new radcomboxclass();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
    FieldHide fd = new FieldHide();

    string itemtype, itemhead, itemsubhead;
    float i;
    bool fl;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            ViewState["flag"] = "true";
            btnUpdate.Visible = false;
            btnSave.Visible = false;
            btnDelete_after.Visible = false;
            userid = Request.QueryString["UserID"];
            UserGrpID = Request.QueryString["UsergrpID"];
            FormID = Request.QueryString["FormID"];
            menuid = Request.QueryString["Menuid"];
            fiscal = Request.QueryString["fiscaly"];
            string sdate = startdate(fiscal);
            string enddate = Enddate(fiscal);
            dml.dropdownsqlwithquery(ddlDocument, "select DocID,DocDescription from SET_Documents where docid in (select DocID from SET_UserGrp_Documents where UserGrpId = '" + UserGrpID + "' AND	(( StartDate <= '" + sdate + "') AND ((EndDate >= '" + enddate + "') OR (EndDate IS NULL))) and Record_Deleted = 0) and CompID = '" + compid() + "' and Record_Deleted = 0 and MenuId_Sno = '" + menuid + "' and FormId_Sno='" + FormID + "'", "DocDescription", "DocID");
            //dml.dropdownsqlwithquery(ddlDocument, "select DocID,DocDescription from SET_Documents where docid in (select DocID from SET_UserGrp_Documents where UserGrpId = '" + UserGrpID + "' and Record_Deleted = 0) and CompID = '" + compid() + "' and Record_Deleted = 0 and MenuId_Sno = '" + menuid + "' and FormId_Sno='" + FormID + "'", "DocDescription", "DocID");
            dml.dropdownsql(ddlDocTypes, "SET_DocumentType", "DocName", "DocTypeId");
            dml.dropdownsqlwithquery(ddlDocTypes, "select * from SET_DocumentType where DocTypeId in( select DocTypeId from SET_Documents where MenuId_Sno = '"+ menuid + "' and FormId_Sno='"+ FormID + "')", "DocName", "DocTypeId");

            
            dml.dropdownsql(ddlDepartment, "SET_Department", "DepartmentName", "DepartmentID");
            dml.dropdownsql(ddlFromStroeDept, "SET_Department", "DepartmentName", "DepartmentID");
            txtReqDate.Attributes.Add("readonly", "readonly");
            txtRequirmentDueDate.Attributes.Add("readonly", "readonly");

            dml.dropdownsql(dllFind_DocName, "SET_Documents", "DocDescription", "DocID");
            dml.dropdownsql(dllDelete_DocName, "SET_Documents", "DocDescription", "DocID");
            dml.dropdownsql(ddlEdit_DocName, "SET_Documents", "DocDescription", "DocID");
            CalendarExtender1.EndDate = DateTime.Now;
            string sd = fiscalstart(fiscal);
            CalendarExtender1.StartDate = DateTime.Parse(sd);
          
            dml.dropdownsql(ddlStatus, "SET_Status", "StatusName", "StatusId");
            dml.dropdownsql(ddlEdit_Department, "SET_Department", "DepartmentName", "DepartmentID");
            dml.dropdownsql(dllDelete_Department, "SET_Department", "DepartmentName", "DepartmentID");
            dml.dropdownsql(dllFind_Department, "SET_Department", "DepartmentName", "DepartmentID");
            dml.dropdownsql(ddlReqStatus, "SET_Authority", "AuthorityName", "AuthorityId");
            dml.dropdownsql(ddlPriority, "Set_Priority", "PriorityName", "PriorityId");

            dml.dropdownsql(ddllocation, "Set_Location", "LocName", "LocId");

            CalendarExtender2.StartDate = DateTime.Now;
            
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
      
        ddlDocument.Enabled = true;


        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[7] { new DataColumn("ItemSubHeadName"), new DataColumn("Description"), new DataColumn("UOM"), new DataColumn("CurrentStock"), new DataColumn("RequiredQuantity"), new DataColumn("CostCenter"), new DataColumn("Project") });
        ViewState["Customers"] = dt;
        
        
        this.PopulateGridview();

      
        DropDownList ddlsub = GridView6.FooterRow.FindControl("ddlitemsub") as DropDownList;
        dml.dropdownsql(ddlsub, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");

        DropDownList ddlmaster = GridView6.FooterRow.FindControl("ddlitemMaster") as DropDownList;
        dml.dropdownsql(ddlmaster, "SET_ItemMaster", "Description", "ItemID");

        DropDownList ddluom = GridView6.FooterRow.FindControl("ddlUomFooter") as DropDownList;
        dml.dropdownsql(ddluom, "SET_UnitofMeasure", "UOMName", "UOMID");
        
        DropDownList ddlsupFooter = GridView6.FooterRow.FindControl("ddlCostCenterFooter") as DropDownList;
        dml.dropdownsql(ddlsupFooter, "SET_CostCenter", "CostCenterName", "CostCenterID");

        txtCreatedby.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdateBy.Enabled = true;
        txtUpdateDate.Enabled = false;

        chkActive.Checked = true;
        ddlStatus.ClearSelection();
        ddlStatus.Items.FindByText("Open").Selected = true;

        ddlPriority.ClearSelection();
        ddlPriority.Items.FindByText("Normal").Selected = true;
        txtReqDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        userid = Request.QueryString["UserID"];
        DataSet ds_autoUserName = dml.Find("select user_name from SET_User_Manager where UserId ='" + userid + "'");
        txtCreatedby.Text = ds_autoUserName.Tables[0].Rows[0]["user_name"].ToString();
        txtCreatedDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss") + " " + txtCreatedby.Text;
        UserGrpID = Request.QueryString["UsergrpID"];
        FormID = Request.QueryString["FormID"];
        dml.dateRuleForm(UserGrpID, FormID, CalendarExtender1, "A");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblreqdate.Text = txtReqDate.Text;
        string rdb = "";
        if (rdbDepartment.Checked == true)
        {
            rdb = "D";
        }
        if (rdbPurchase.Checked == true)
        {
            rdb = "P";
        }
       
            int chk = 0, chks = 0;
           
            if (chkActive.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
          
            string reqdate = dml.dateconvertforinsert(txtRequirmentDueDate);
            string requisDate = dml.dateconvertforinsert(txtReqDate);
            txtReqNO.Text = required_generatestr();
            string dc = detailcond();
            if (dc != "")
            {
                DataSet dd = dml.Find("select DocId from SET_Documents where DocID = '" + ddlDocument.SelectedItem.Value + "'");
                string doctype = "0";
                if (dd.Tables[0].Rows.Count>0)
                {
                    doctype = dd.Tables[0].Rows[0]["DocId"].ToString();
                }
                dml.Insert("INSERT INTO SET_StockRequisitionMaster ([DocId], [RequisitionType], [RequisitionNo], [RequistionDate], [RequistionStatus], [Priority], [IssueToStore_Department], [Location], [CopyRequistionNo], [FromStroe__Department], [RequirementDueDate], [Remarks], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted],[Status],[DocType]) VALUES ('" + ddlDocument.SelectedItem.Value + "',  '" + rdb + "', '" + required_generateins() + "', '" + requisDate + "', '" + ddlReqStatus.SelectedItem.Value + "', '" + ddlPriority.SelectedItem.Value + "', '" + ddlDepartment.SelectedItem.Value + "', '" + ddllocation.SelectedItem.Value + "', '" + txtCopyReqNo.Text + "', '" + ddlFromStroeDept.SelectedItem.Value + "', '" + reqdate + "', '" + txtRemarks.Text + "', '" + chk + "'," + gocid() + "," + compid() + "," + branchId() + "," + FiscalYear() + "," + show_username() + ", '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "','0','" + ddlStatus.SelectedItem.Value + "','"+ doctype + "');", "");
                string ids = "1";
                DataSet dsCh = dml.Find("select Sno from SET_StockRequisitionMaster WHERE ([DocId]='" + ddlDocument.SelectedItem.Value + "') AND ([RequisitionType]='" + rdb + "') AND ([RequisitionNo]='" + txtReqNO.Text + "') AND ([RequistionDate]='" + requisDate + "') AND ([RequistionStatus]='" + ddlReqStatus.SelectedItem.Value + "') AND ([IssueToStore_Department]='" + ddlDepartment.SelectedItem.Value + "') AND ([Priority]='" + ddlPriority.SelectedItem.Value + "') AND ([FromStroe__Department]='" + ddlFromStroeDept.SelectedItem.Value + "') AND ([Location]='" + ddllocation.SelectedItem.Value + "') AND ([CopyRequistionNo]='" + txtCopyReqNo.Text + "') AND ([RequirementDueDate]='" + reqdate + "') AND ([Remarks]='" + txtRemarks.Text + "') AND ([IsActive]='" + chk + "') AND ([GocID]='" + gocid() + "') AND ([CompId]='" + compid() + "') AND ([BranchId]='" + branchId() + "') AND ([FiscalYearID]='" + FiscalYear() + "') AND ([Status] = '" + ddlStatus.SelectedItem.Value + "') ");
                if (dsCh.Tables[0].Rows.Count > 0)
                {
                    ids = dsCh.Tables[0].Rows[0]["Sno"].ToString();
                }
                detailinsert(ids);
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);

                Label1.Text = "";

                ddlDepartment.SelectedIndex = 0;
                ddlDocTypes.SelectedIndex = 0;
                ddlDocument.SelectedIndex = 0;
                ddlPriority.SelectedIndex = 0;
                txtReqNO.Text = "";
                txtReqDate.Text = "";
                ddlReqStatus.SelectedIndex = 0;
                ddllocation.SelectedIndex = 0;
                txtCopyReqNo.Text = "";
                ddlFromStroeDept.SelectedIndex = 0;
                txtRequirmentDueDate.Text = "";
                txtRemarks.Text = "";
                txtCreatedDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                Div2.Visible = false;
            }
            else
            {
                Label1.Text = "Please entry altest 1 entry in detail table";
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

       
        DataSet ds_up = dml.Find("select * from SET_StockRequisitionMaster WHERE ([DocId]='"+ddlDocument.SelectedItem.Value+ "') AND ([RequisitionType]='" + rdb+"') AND ([RequisitionNo]='"+txtReqNO.Text+"') AND ([RequistionDate]='"+txtReqDate.Text+"') AND ([RequistionStatus]='"+ ddlReqStatus.SelectedItem.Value+"') AND ([Priority]='"+ddlPriority.SelectedItem.Value+ "') AND ([IssueToStore_Department]='" + ddlDepartment.SelectedItem.Value+"') AND ([Location]='"+ ddllocation.SelectedItem.Value + "') AND ([CopyRequistionNo]='"+txtCopyReqNo.Text+ "') AND ([FromStroe__Department]='" + ddlFromStroeDept.SelectedItem.Value+"') AND ([RequirementDueDate]='"+txtRequirmentDueDate.Text+"') AND ([Remarks]='"+txtRemarks.Text+"') AND ([IsActive]='"+chk+"') AND ([Record_Deleted]='0') AND ([Status] = '"+ddlStatus.SelectedItem.Value+"')");

        if (ds_up.Tables[0].Rows.Count > 0 )
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

            dml.Update("UPDATE SET_StockRequisitionMaster SET  [DocId]='" + ddlDocument.SelectedItem.Value + "', [RequisitionType]='" + rdb + "', [RequisitionNo]='" + txtReqNO.Text + "', [RequistionDate]='" + txtReqDate.Text + "', [RequistionStatus]='" + ddlReqStatus.SelectedItem.Value + "', [Priority]='" + ddlPriority.SelectedItem.Value + "', [IssueToStore_Department]='" + ddlDepartment.SelectedItem.Value +"', [Location]='"+ ddllocation.SelectedItem.Value + "', [CopyRequistionNo]='"+txtCopyReqNo.Text+ "', [FromStroe__Department]='" + ddlFromStroeDept.SelectedItem.Value+"', [RequirementDueDate]='"+txtRequirmentDueDate.Text+"', [Remarks]='"+txtRemarks.Text+"', [IsActive]='"+chk+"',[Status] = '"+ddlStatus.SelectedItem.Value+"', [UpdatedBy]='"+show_username()+"', [UpdatedDate]='"+DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")+"' where sno = '"+ViewState["SNO"].ToString()+"'", "");
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
            string squer = "select * from View_StockReqMaster";


            if (dllDelete_DocName.SelectedIndex != 0)
            {
                swhere = "DocDescription = '" + dllDelete_DocName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "DocDescription is not null";
            }
            if (dllDelete_ReqType.SelectedIndex != 0)
            {
                swhere = swhere + " and RequisitionType = '" + dllDelete_ReqType.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and RequisitionType is not null";
            }
            if (txtDelete_REqDate.Text != "")
            {
                swhere = swhere + " and RequistionDate = '" + txtDelete_REqDate.Text + "'";
            }
            else
            {
                swhere = swhere + " and RequistionDate is not null";
            }
            if (dllDelete_Priority.SelectedIndex != 0)
            {
                swhere = swhere + " and Priority = '" + dllDelete_Priority.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and Priority is not null";
            }
            if (dllDelete_Department.SelectedIndex != 0)
            {
                swhere = swhere + " and IssueToStore_Department = '" + dllDelete_Department.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and IssueToStore_Department is not null";
            }

            if (chkDel_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkDel_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and FiscalYearID = '" + FiscalYear() + "' and GocID='" + gocid() + "' and CompId = '" + compid() + "' and BranchId='" + branchId() + "'  ORDER BY DocDescription";

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
            string squer = "select * from View_StockReqMaster";


            if (dllFind_DocName.SelectedIndex != 0)
            {
                swhere = "DocDescription = '" + dllFind_DocName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "DocDescription is not null";
            }
            if (dllFind_ReqType.SelectedIndex != 0)
            {
                swhere = swhere + " and RequisitionType = '" + dllFind_ReqType.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and RequisitionType is not null";
            }
            if (txtFind_ReqDate.Text != "")
            {
                swhere = swhere + " and RequistionDate = '" + txtFind_ReqDate.Text + "'";
            }
            else
            {
                swhere = swhere + " and RequistionDate is not null";
            }
            if (dllFind_Priority.SelectedIndex != 0)
            {
                swhere = swhere + " and Priority = '" + dllFind_Priority.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and Priority is not null";
            }
            if (dllFind_Department.SelectedIndex != 0)
            {
                swhere = swhere + " and IssueToStore_Department = '" + dllFind_Department.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and IssueToStore_Department is not null";
            }

            if (chkFind_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkFind_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and FiscalYearID = '" + FiscalYear() + "' and GocID='" + gocid() + "' and CompId = '" + compid() + "' and BranchId='" + branchId() + "'  ORDER BY DocDescription";

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
            string squer = "select * from View_StockReqMaster";
            //select DocName,RequisitionType,RequistionDate,Priority,IssueToStore_Department,IsActive from View_StockReqMaster

            if (ddlEdit_DocName.SelectedIndex != 0)
            {
                swhere = "DocDescription = '" + ddlEdit_DocName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "DocDescription is not null";
            }
             if (ddlEdit_ReqTypr.SelectedIndex != 0)
            {
                swhere = swhere + " and RequisitionType = '" + ddlEdit_ReqTypr.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and RequisitionType is not null";
            }
            if (txtEdit_ReqDate.Text != "")
            {
                swhere = swhere + " and RequistionDate = '" + txtEdit_ReqDate.Text + "'";
            }
            else
            {
                swhere = swhere + " and RequistionDate is not null";
            }
            if (ddlEdit_Priority.SelectedIndex != 0)
            {
                swhere = swhere + " and Priority = '" + ddlEdit_Priority.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and Priority is not null";
            }
            if (ddlEdit_Department.SelectedIndex != 0)
            {
                swhere = swhere + " and IssueToStore_Department = '" + ddlEdit_Department.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and IssueToStore_Department is not null";
            }

            if (chkEdit_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkEdit_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and FiscalYearID = '"+FiscalYear()+"' and GocID='"+gocid()+"' and CompId = '"+compid()+"' and BranchId='"+branchId()+"'  ORDER BY DocDescription";

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
        Div4.Visible = false;
        Div2.Visible = false;
        Div3.Visible = false;
        Div1.Visible = false;
        DeleteBox.Visible = false;
        Editbox.Visible = false;
        Findbox.Visible = false;
        fieldbox.Visible = true;
        chkActive.Checked = false;
        ddlDepartment.SelectedIndex = 0;
        ddlDocTypes.SelectedIndex = 0;
        ddlDocument.SelectedIndex = 0;
        ddlPriority.SelectedIndex = 0;
        txtReqNO.Text = "";
        txtReqDate.Text = "";
        ddlReqStatus.SelectedIndex = 0;
        ddllocation.SelectedIndex = 0;
        txtCopyReqNo.Text = "";
        ddlFromStroeDept.SelectedIndex = 0 ;
        txtRequirmentDueDate.Text = "";
        txtRemarks.Text = "";
        ddlStatus.SelectedIndex = 0;
        



        txtCreatedby.Text = "";
        txtCreatedDate.Text = "";
        txtUpdateBy.Text = "";
        txtUpdateDate.Text = "";
        chkActive.Enabled = false;

        rdbDepartment.Enabled = false;
        rdbPurchase.Enabled = false;
        ddlDepartment.Enabled = false;
        ddlDocTypes.Enabled = false;
        ddlDocument.Enabled = false;
        ddlPriority.Enabled = false;
        txtReqNO.Enabled = false;
        txtReqDate.Enabled = false;
        ddlReqStatus.Enabled = false;
        ddllocation.Enabled = false;
        txtCopyReqNo.Enabled = false;
        ddlFromStroeDept.Enabled = false;
        txtRequirmentDueDate.Enabled = false;
        txtRemarks.Enabled = false;
        imgPopup.Enabled = false;
        ImageButton1.Enabled = false;
        ddlStatus.Enabled = false;

        txtCreatedby.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdateBy.Enabled = false;
        txtUpdateDate.Enabled = false;
        Label1.Text = "";
        lbldocno.Text = "";
      



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
            //DataSet ds = dml.Find("select * from SET_BankBranch where Bankid = '" + ViewState["SNO"].ToString() + "'  and Record_Deleted = '0' ");
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    string count = ds.Tables[0].Rows.Count.ToString();
            //    Label1.Text = "This bank has " + count + " Branch. First delete Branch"; 
            //}
            //else {


                dml.Delete("update SET_StockRequisitionMaster set Record_Deleted = 1 where sno = " + ViewState["SNO"].ToString() + "", "");
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

        
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {
            string serial_id;
            serial_id = GridView1.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;



            DataSet ds = dml.Find("select * from SET_StockRequisitionMaster WHERE ([sno]='" + serial_id+"') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlDepartment.ClearSelection();
                ddlDocTypes.ClearSelection();
                ddlDocument.ClearSelection();
                ddlPriority.ClearSelection();
                ddlFromStroeDept.ClearSelection();
                ddlReqStatus.ClearSelection();
                ddlStatus.ClearSelection();

                ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["Status"].ToString()).Selected = true;
                ddlDepartment.Items.FindByValue(ds.Tables[0].Rows[0]["IssueToStore_Department"].ToString()).Selected = true;
               // ddlDocTypes.Items.FindByValue(ds.Tables[0].Rows[0]["DocType"].ToString()).Selected = true;
                ddlDocument.Items.FindByValue(ds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;
                ddlPriority.Items.FindByValue(ds.Tables[0].Rows[0]["Priority"].ToString()).Selected = true;

                txtReqNO.Text = ds.Tables[0].Rows[0]["RequisitionNo"].ToString();
                txtReqDate.Text = ds.Tables[0].Rows[0]["RequistionDate"].ToString();
                ddlReqStatus.Items.FindByValue(ds.Tables[0].Rows[0]["RequistionStatus"].ToString()).Selected = true;
                ddllocation.ClearSelection();
                ddllocation.Items.FindByValue(ds.Tables[0].Rows[0]["Location"].ToString()).Selected = true;
                txtCopyReqNo.Text = ds.Tables[0].Rows[0]["CopyRequistionNo"].ToString();
                ddlFromStroeDept.Items.FindByValue(ds.Tables[0].Rows[0]["FromStroe__Department"].ToString()).Selected = true;
                txtRequirmentDueDate.Text = ds.Tables[0].Rows[0]["RequirementDueDate"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();

                


                string rdb = ds.Tables[0].Rows[0]["RequisitionType"].ToString();

                if (rdb == "D")
                {
                    rdbDepartment.Checked = true;
                    rdbPurchase.Checked = false;
                }
                if (rdb == "P")
                {
                    rdbDepartment.Checked = false;
                    rdbPurchase.Checked = true;
                }


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


                if (ds.Tables[0].Rows[0]["CreatedDate"].ToString() == "")
                {
                    txtCreatedDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                    txtCreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss");
                }
                if (ds.Tables[0].Rows[0]["UpdatedDate"].ToString() == "")
                {
                    txtUpdateDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdatedDate"].ToString());
                    txtUpdateDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss");
                }

                txtUpdateDate.Text = txtUpdateDate.Text + " " + txtUpdateBy.Text;
                txtCreatedDate.Text = txtCreatedDate.Text + " " + txtCreatedby.Text;

                dml.dateConvert(txtReqDate);
                dml.dateConvert(txtRequirmentDueDate);

              
               
                DataSet ds_detail = dml.Find("select * from view_StockUpdate where StockReqMId = '" + serial_id + "'");
                if (ds_detail.Tables[0].Rows.Count > 0)
                {
                    Div4.Visible = true;
                    GridView4.DataSource = ds_detail.Tables[0];
                    GridView4.DataBind();
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
        string status = "0";
        try
        {
            string serial_id;
            serial_id = GridView2.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds1 = dml.Find("select Status from SET_StockRequisitionMaster where Sno = '" + serial_id + "'");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                status = ds1.Tables[0].Rows[0]["Status"].ToString();

            }
            // if (status == "1" || status == "3" || status == "4" || status == "5" || status == "8")
            if (status == "1")
            {
            DataSet ds = dml.Find("select * from SET_StockRequisitionMaster WHERE ([sno]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {

                    btnInsert.Visible = false;
                    btnCancel.Visible = true;
                    btnUpdate.Visible = false;
                    btnDelete.Visible = false;
                    btnEdit.Visible = false;
                    btnFind.Visible = false;

                    txtUpdateDate.Enabled = false;
                    txtUpdateBy.Enabled = false;
                   
                    textClear();


                    fieldbox.Visible = true;
                    Findbox.Visible = false;
                    Editbox.Visible = false;
                    DeleteBox.Visible = false;


                    btnDelete_after.Visible = true;

                ddlDepartment.ClearSelection();
                ddlDocTypes.ClearSelection();
                ddlDocument.ClearSelection();
                ddlPriority.ClearSelection();
                ddlFromStroeDept.ClearSelection();
                ddlReqStatus.ClearSelection();
                ddlStatus.ClearSelection();

                ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["Status"].ToString()).Selected = true;
                ddlDepartment.Items.FindByValue(ds.Tables[0].Rows[0]["IssueToStore_Department"].ToString()).Selected = true;
                // ddlDocTypes.Items.FindByValue(ds.Tables[0].Rows[0]["DocType"].ToString()).Selected = true;
                ddlDocument.Items.FindByValue(ds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;
                ddlPriority.Items.FindByValue(ds.Tables[0].Rows[0]["Priority"].ToString()).Selected = true;

                txtReqNO.Text = ds.Tables[0].Rows[0]["RequisitionNo"].ToString();
                txtReqDate.Text = ds.Tables[0].Rows[0]["RequistionDate"].ToString();
                ddlReqStatus.Items.FindByValue(ds.Tables[0].Rows[0]["RequistionStatus"].ToString()).Selected = true;
                ddllocation.ClearSelection();
                ddllocation.Items.FindByValue(ds.Tables[0].Rows[0]["Location"].ToString()).Selected = true;
                txtCopyReqNo.Text = ds.Tables[0].Rows[0]["CopyRequistionNo"].ToString();
                ddlFromStroeDept.Items.FindByValue(ds.Tables[0].Rows[0]["FromStroe__Department"].ToString()).Selected = true;
                txtRequirmentDueDate.Text = ds.Tables[0].Rows[0]["RequirementDueDate"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                string rdb = ds.Tables[0].Rows[0]["RequisitionType"].ToString();

                if (rdb == "D")
                {
                    rdbDepartment.Checked = true;
                    rdbPurchase.Checked = false;
                }
                if (rdb == "P")
                {
                    rdbDepartment.Checked = false;
                    rdbPurchase.Checked = true;
                }

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


                if (ds.Tables[0].Rows[0]["CreatedDate"].ToString() == "")
                {
                    txtCreatedDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                    txtCreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss");
                }
                if (ds.Tables[0].Rows[0]["UpdatedDate"].ToString() == "")
                {
                    txtUpdateDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdatedDate"].ToString());
                    txtUpdateDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss");
                }

                txtUpdateDate.Text = txtUpdateDate.Text + " " + txtUpdateBy.Text;
                txtCreatedDate.Text = txtCreatedDate.Text + " " + txtCreatedby.Text;
                dml.dateConvert(txtReqDate);
                dml.dateConvert(txtRequirmentDueDate);



                DataTable dtup = new DataTable();
                dtup.Columns.AddRange(new DataColumn[7] { new DataColumn("ItemSubHeadName"), new DataColumn("Description"), new DataColumn("UOM"), new DataColumn("CurrentStock"), new DataColumn("RequiredQuantity"), new DataColumn("CostCenter"), new DataColumn("Project") });


                DataSet ds_detail = dml.Find("select * from view_StockUpdate where StockReqMId = '" + serial_id + "'");
                if (ds_detail.Tables[0].Rows.Count > 0)
                {
                    ViewState["dtdel"] = ds_detail.Tables[0];
                    Div3.Visible = true;
                    GridView7.DataSource = ds_detail.Tables[0];
                    GridView7.DataBind();
                }
                PopulateGridview_Del();
            }
        }
            else
            {
                //btnDelete_after.Visible = false;

                //DataSet ds = dml.Find("select * from SET_StockRequisitionMaster WHERE ([sno]='" + serial_id + "') and Record_Deleted = 0");
                //if (ds.Tables[0].Rows.Count > 0)
                //{                    
                //    ddlDepartment.ClearSelection();
                //    ddlDocTypes.ClearSelection();
                //    ddlDocument.ClearSelection();
                //    ddlPriority.ClearSelection();
                //    ddlFromStroeDept.ClearSelection();
                //    ddlReqStatus.ClearSelection();
                //    ddlStatus.ClearSelection();

                //    ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["Status"].ToString()).Selected = true;
                //    ddlDepartment.Items.FindByValue(ds.Tables[0].Rows[0]["IssueToStore_Department"].ToString()).Selected = true;
                //    // ddlDocTypes.Items.FindByValue(ds.Tables[0].Rows[0]["DocType"].ToString()).Selected = true;
                //    ddlDocument.Items.FindByValue(ds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;
                //    ddlPriority.Items.FindByValue(ds.Tables[0].Rows[0]["Priority"].ToString()).Selected = true;

                //    txtReqNO.Text = ds.Tables[0].Rows[0]["RequisitionNo"].ToString();
                //    txtReqDate.Text = ds.Tables[0].Rows[0]["RequistionDate"].ToString();
                //    ddlReqStatus.Items.FindByValue(ds.Tables[0].Rows[0]["RequistionStatus"].ToString()).Selected = true;
                //    ddllocation.ClearSelection();
                //    ddllocation.Items.FindByValue(ds.Tables[0].Rows[0]["Location"].ToString()).Selected = true;
                //    txtCopyReqNo.Text = ds.Tables[0].Rows[0]["CopyRequistionNo"].ToString();
                //    ddlFromStroeDept.Items.FindByValue(ds.Tables[0].Rows[0]["FromStroe__Department"].ToString()).Selected = true;
                //    txtRequirmentDueDate.Text = ds.Tables[0].Rows[0]["RequirementDueDate"].ToString();
                //    txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();

                //    bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                //    string rdb = ds.Tables[0].Rows[0]["RequisitionType"].ToString();

                //    if (rdb == "D")
                //    {
                //        rdbDepartment.Checked = true;
                //        rdbPurchase.Checked = false;
                //    }
                //    if (rdb == "P")
                //    {
                //        rdbDepartment.Checked = false;
                //        rdbPurchase.Checked = true;
                //    }

                //    txtCreatedby.Text = ds.Tables[0].Rows[0]["CreatedBy"].ToString();
                //    txtUpdateBy.Text = ds.Tables[0].Rows[0]["UpdatedBy"].ToString();

                //    if (chk == true)
                //    {
                //        chkActive.Checked = true;
                //    }
                //    else
                //    {
                //        chkActive.Checked = false;
                //    }


                //    if (ds.Tables[0].Rows[0]["CreatedDate"].ToString() == "")
                //    {
                //        txtCreatedDate.Text = "";
                //    }
                //    else {
                //        DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                //        txtCreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss");
                //    }
                //    if (ds.Tables[0].Rows[0]["UpdatedDate"].ToString() == "")
                //    {
                //        txtUpdateDate.Text = "";
                //    }
                //    else {
                //        DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdatedDate"].ToString());
                //        txtUpdateDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss");
                //    }

                //    txtUpdateDate.Text = txtUpdateDate.Text + " " + txtUpdateBy.Text;
                //    txtCreatedDate.Text = txtCreatedDate.Text + " " + txtCreatedby.Text;
                //    dml.dateConvert(txtReqDate);
                //    dml.dateConvert(txtRequirmentDueDate);




                //    DataSet ds_detail = dml.Find("select * from view_StockUpdate where StockReqMId = '" + serial_id + "'");
                //    if (ds_detail.Tables[0].Rows.Count > 0)
                //    {
                //        Div4.Visible = true;
                //        GridView4.DataSource = ds_detail.Tables[0];
                //        GridView4.DataBind();
                //    }
                //}
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
        string status = "";
        string serial_id;
        serial_id = GridView3.SelectedRow.Cells[1].Text;
        ViewState["SNO"] = serial_id;
        DataSet ds1 = dml.Find("select Status from SET_StockRequisitionMaster where Sno = '" + serial_id+"'");
        if (ds1.Tables[0].Rows.Count > 0)
        {
            status = ds1.Tables[0].Rows[0]["Status"].ToString();
            
        }
     //if (status == "1" || status == "3" || status == "4" || status == "5" || status == "8")
        if (status == "1")
            {

            textClear();

            btnInsert.Visible = false;
            btnCancel.Visible = true;
            btnUpdate.Visible = true;
            btnDelete.Visible = false;
            btnEdit.Visible = false;
            btnFind.Visible = false;
            Label1.Text = "";
           
            ddlDepartment.Enabled = true;
            ddlDocTypes.Enabled = true;
            ddlDocument.Enabled = false;
            ddlPriority.Enabled = true;
            txtReqNO.Enabled = false;
            txtReqDate.Enabled = true;
            ddlReqStatus.Enabled = false;
            ddllocation.Enabled = true;
            txtCopyReqNo.Enabled = true;
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
            ddlStatus.Enabled = true;



            fieldbox.Visible = true;
            Findbox.Visible = false;
            Editbox.Visible = false;
            try
            {

                DataSet ds = dml.Find("select * from SET_StockRequisitionMaster WHERE ([sno]='" + serial_id + "') and Record_Deleted = 0");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDepartment.ClearSelection();
                    ddlDocTypes.ClearSelection();
                    ddlDocument.ClearSelection();
                    ddlPriority.ClearSelection();
                    ddlFromStroeDept.ClearSelection();
                    ddlReqStatus.ClearSelection();

                    ddlDepartment.Items.FindByValue(ds.Tables[0].Rows[0]["IssueToStore_Department"].ToString()).Selected = true;
                    // ddlDocTypes.Items.FindByValue(ds.Tables[0].Rows[0]["DocType"].ToString()).Selected = true;
                    ddlDocument.Items.FindByValue(ds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;
                    ddlPriority.Items.FindByValue(ds.Tables[0].Rows[0]["Priority"].ToString()).Selected = true;


                    txtReqNO.Text = ds.Tables[0].Rows[0]["RequisitionNo"].ToString();
                    txtReqDate.Text = ds.Tables[0].Rows[0]["RequistionDate"].ToString();
                    ddlReqStatus.Items.FindByValue(ds.Tables[0].Rows[0]["RequistionStatus"].ToString()).Selected = true;
                    ddllocation.ClearSelection();
                    ddllocation.Items.FindByValue(ds.Tables[0].Rows[0]["Location"].ToString()).Selected = true;
                    txtCopyReqNo.Text = ds.Tables[0].Rows[0]["CopyRequistionNo"].ToString();
                    ddlFromStroeDept.Items.FindByValue(ds.Tables[0].Rows[0]["FromStroe__Department"].ToString()).Selected = true;
                    txtRequirmentDueDate.Text = ds.Tables[0].Rows[0]["RequirementDueDate"].ToString();
                    txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();

                    ddlStatus.ClearSelection();

                    ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["Status"].ToString()).Selected = true;

                    bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                    string rdb = ds.Tables[0].Rows[0]["RequisitionType"].ToString();

                    if (rdb == "D")
                    {
                        rdbDepartment.Checked = true;
                        rdbPurchase.Checked = false;
                    }
                    if (rdb == "P")
                    {
                        rdbDepartment.Checked = false;
                        rdbPurchase.Checked = true;
                    }

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
                    
                    if (ds.Tables[0].Rows[0]["CreatedDate"].ToString() == "")
                    {
                        txtCreatedDate.Text = "";
                    }
                    else {
                        DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                        txtCreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss");
                    }
                    if (ds.Tables[0].Rows[0]["UpdatedDate"].ToString() == "")
                    {
                        txtUpdateDate.Text = "";
                    }
                    else {
                        DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdatedDate"].ToString());
                        txtUpdateDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss");
                    }
                    txtUpdateDate.Text = txtUpdateDate.Text + " " + txtUpdateBy.Text;
                    txtCreatedDate.Text = txtCreatedDate.Text + " " + txtCreatedby.Text;
                    dml.dateConvert(txtReqDate);
                    dml.dateConvert(txtRequirmentDueDate);




                    DataTable dtup = new DataTable();
                    dtup.Columns.AddRange(new DataColumn[7] { new DataColumn("ItemSubHeadName"), new DataColumn("Description"), new DataColumn("UOM"), new DataColumn("CurrentStock"), new DataColumn("RequiredQuantity"), new DataColumn("CostCenter"), new DataColumn("Project") });

                    //select * from view_StockUpdate
                    //select ItemSubHeadName , Description,CostCenterName,UOMName from view_StockUpdate
                    DataSet ds_detail = dml.Find("select * from view_StockUpdate where StockReqMId = '" + serial_id + "'");
                    if (ds_detail.Tables[0].Rows.Count > 0)
                    {
                        ViewState["dtup"] = ds_detail.Tables[0];
                        Div1.Visible = true;
                        GridView5.DataSource = ds_detail.Tables[0];
                        GridView5.DataBind();
                    }
                    PopulateGridview_Up();
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
        else
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
    protected void ddlDocTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDocTypes.SelectedIndex != 0)
        {
            dml.dropdownsql(ddlDocument, "SET_Documents", "DocDescription", "DocID", "DocId", ddlDocTypes.SelectedItem.Value);

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
            //GridView6.Rows[0].Cells.Add(new TableCell());
           // GridView6.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
           // GridView6.Rows[0].Cells[0].Text = "No Data Found ..!";
           // GridView6.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            
        }
    }
    public void PopulateGridview_Up()
    {

        DataTable dtbl = (DataTable)ViewState["dtup"];

        if (dtbl.Rows.Count > 0)
        {

            GridView5.DataSource = (DataTable)ViewState["dtup"];
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
    public void PopulateGridview_Del()
    {

        DataTable dtbl = (DataTable)ViewState["dtdel"];
      
        if (dtbl.Rows.Count > 0)
        {

            GridView7.DataSource = (DataTable)ViewState["dtdel"];
            
            GridView7.DataBind();
            

        }
        else
        {


            dtbl.Rows.Add(dtbl.NewRow());
            GridView7.DataSource = dtbl;
            GridView7.DataBind();

            GridView7.Rows[0].Cells.Clear();
            GridView7.Rows[0].Cells.Add(new TableCell());
            GridView7.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
            GridView7.Rows[0].Cells[0].Text = "No Data Found ..!";
            GridView7.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;

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
                    Label ll = (Label)g.FindControl("Label2");
                    Label l = (Label)g.FindControl("Label3");
                    Label lblerror = (Label)g.FindControl("lblerroritemmaster");

                    string txtitemsubfooter = (GridView6.FooterRow.FindControl("ddlitemsub") as DropDownList).SelectedItem.Text;
                    string txtdesc = (GridView6.FooterRow.FindControl("ddlitemMaster") as DropDownList).SelectedItem.Text;
                    string txtuomFooter = (GridView6.FooterRow.FindControl("ddlUomFooter") as DropDownList).SelectedItem.Text;
                    string txtcurrStockFooter = (GridView6.FooterRow.FindControl("txtcurrStockFooter") as TextBox).Text;
                    string txtReqStockFooter = (GridView6.FooterRow.FindControl("txtReqStockFooter") as TextBox).Text;
                    string txtCostCenterFooter = (GridView6.FooterRow.FindControl("ddlCostCenterFooter") as DropDownList).SelectedItem.Text;
                    string txtProjectfooter = (GridView6.FooterRow.FindControl("lblProjectFooter") as TextBox).Text;


                    if (txtitemsubfooter == "Please select..." || txtdesc == "Please select..." || txtuomFooter == "Please select...")
                    {
                        if (txtitemsubfooter == "Please select...")
                        {
                            lblerror.Text = "Please select Item Sub head from detail table";
                        }
                        if (txtdesc == "Please select...")
                        {
                            Label1.Text = "Please select Item Master from detail table";
                        }
                        if (txtuomFooter == "Please select...")
                        {
                            Label1.Text = "Please select UOM from detail table";
                        }
                    }
                    else {

                        if (ll.Text == txtitemsubfooter && l.Text == txtdesc)
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

                            dt.Rows.Add(txtitemsubfooter, txtdesc, txtuomFooter, txtcurrStockFooter, txtReqStockFooter, txtCostCenterFooter, txtProjectfooter);

                            ViewState["Customers"] = dt;
                            this.PopulateGridview();
                            DropDownList ddlsub = GridView6.FooterRow.FindControl("ddlitemsub") as DropDownList;
                            dml.dropdownsql(ddlsub, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");


                            DropDownList ddlmaster = GridView6.FooterRow.FindControl("ddlitemMaster") as DropDownList;
                            dml.dropdownsql(ddlmaster, "SET_ItemMaster", "Description", "ItemID");

                            DropDownList ddluom = GridView6.FooterRow.FindControl("ddlUomFooter") as DropDownList;
                            dml.dropdownsql(ddluom, "SET_UnitofMeasure", "UOMName", "UOMID");
                            //ddlUomFooter  ddlitemMaster ddlsupFooter
                            //select CostCenterID,CostCenterName from SET_CostCenter
                            DropDownList ddlsupFooter = GridView6.FooterRow.FindControl("ddlCostCenterFooter") as DropDownList;
                            dml.dropdownsql(ddlsupFooter, "SET_CostCenter", "CostCenterName", "CostCenterID");

                            ddlDocument_SelectedIndexChanged(sender, e);
                            // ddlitemMaster_SelectedIndexChanged(sender, e);

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
    protected void GridView6_RowEditing(object sender, GridViewEditEventArgs e)
    {

        GridView6.EditIndex = e.NewEditIndex;
        PopulateGridview();
        DropDownList ddlsub1 = GridView6.Rows[GridView6.EditIndex].FindControl("ddlitemsubEdit") as DropDownList;
        dml.dropdownsql(ddlsub1, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");
        //Label2
        Label lbl1 = GridView6.Rows[GridView6.EditIndex].FindControl("Label2") as Label;

       
        //ddlSupplierEdit, ddlUomEdit, ddlItemMasterEdit

        DropDownList ddlitemsub = GridView6.Rows[GridView6.EditIndex].FindControl("ddlItemMasterEdit") as DropDownList;
        dml.dropdownsql(ddlitemsub, "SET_ItemMaster", "Description", "ItemID");
        Label lblitemmaster = GridView6.Rows[GridView6.EditIndex].FindControl("lblitemmaster") as Label;

        DropDownList ddlUOMEdit = GridView6.Rows[GridView6.EditIndex].FindControl("ddlUomEdit") as DropDownList;
        dml.dropdownsql(ddlUOMEdit, "SET_UnitofMeasure", "UOMName", "UOMID");
        Label lblUOM = GridView6.Rows[GridView6.EditIndex].FindControl("lblUOMEdit") as Label;

        DropDownList ddlsupplierEdit = GridView6.Rows[GridView6.EditIndex].FindControl("ddlCostCenterEdit") as DropDownList;
        dml.dropdownsql(ddlsupplierEdit, "SET_CostCenter", "CostCenterName", "CostCenterID");
        Label lblSupp = GridView6.Rows[GridView6.EditIndex].FindControl("lblcostCenterEdit") as Label;
        Label lblProjectEdit = GridView6.Rows[GridView6.EditIndex].FindControl("lblProjectEdit") as Label;

       
        ddlsub1.ClearSelection();
        ddlsub1.Items.FindByText(lbl1.Text).Selected = true;
        
        lbl1.Visible = false;

        ddlitemsub.ClearSelection();
        ddlitemsub.Items.FindByText(lblitemmaster.Text).Selected = true;
        lblitemmaster.Visible = false;

        ddlUOMEdit.ClearSelection();
        ddlUOMEdit.Items.FindByText(lblUOM.Text).Selected = true;
        lblUOM.Visible = false;

        ddlsupplierEdit.ClearSelection();
        ddlsupplierEdit.Items.FindByText(lblSupp.Text).Selected = true;
        lblSupp.Visible = false;
        lblProjectEdit.Visible = false;

        DropDownList ddlsub = GridView6.FooterRow.FindControl("ddlitemsub") as DropDownList;
        dml.dropdownsql(ddlsub, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");

        DropDownList ddlmaster = GridView6.FooterRow.FindControl("ddlitemMaster") as DropDownList;
        dml.dropdownsql(ddlmaster, "SET_ItemMaster", "Description", "ItemID");

        DropDownList ddluom = GridView6.FooterRow.FindControl("ddlUomFooter") as DropDownList;
        dml.dropdownsql(ddluom, "SET_UnitofMeasure", "UOMName", "UOMID");
        //ddlUomFooter  ddlitemMaster ddlsupFooter

        DropDownList ddlsupFooter = GridView6.FooterRow.FindControl("ddlCostCenterFooter") as DropDownList;
        dml.dropdownsql(ddlsupFooter, "SET_CostCenter", "CostCenterName", "CostCenterID");



    }
    protected void GridView6_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView6.EditIndex = -1;
        PopulateGridview();
        DropDownList ddlsub = GridView6.FooterRow.FindControl("ddlitemsub") as DropDownList;
        dml.dropdownsql(ddlsub, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");

        DropDownList ddlmaster = GridView6.FooterRow.FindControl("ddlitemMaster") as DropDownList;
        dml.dropdownsql(ddlmaster, "SET_ItemMaster", "Description", "ItemID");

        DropDownList ddluom = GridView6.FooterRow.FindControl("ddlUomFooter") as DropDownList;
        dml.dropdownsql(ddluom, "SET_UnitofMeasure", "UOMName", "UOMID");
        //ddlUomFooter  ddlitemMaster ddlsupFooter

        DropDownList ddlsupFooter = GridView6.FooterRow.FindControl("ddlCostCenterFooter") as DropDownList;
        dml.dropdownsql(ddlsupFooter, "SET_CostCenter", "CostCenterName", "CostCenterID");

    }
    protected void GridView6_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            //GridViewRow row = GridView6.Rows[e.RowIndex]

            //  DropDownList ddlsub = GridView6.Rows[e.RowIndex].FindControl("ddlitemsub") as DropDownList;
            //  dml.dropdownsql(ddlsub, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");

            string txtitemsubfooter = (GridView6.Rows[e.RowIndex].FindControl("ddlitemsubEdit") as DropDownList).SelectedItem.Text;
            string txtdesc = (GridView6.Rows[e.RowIndex].FindControl("ddlItemMasterEdit") as DropDownList).SelectedItem.Text;
            string txtuomFooter = (GridView6.Rows[e.RowIndex].FindControl("ddlUomEdit") as DropDownList).SelectedItem.Text;
            string txtcurrStockFooter = (GridView6.Rows[e.RowIndex].FindControl("TextBox4") as TextBox).Text;
            string txtReqStockFooter = (GridView6.Rows[e.RowIndex].FindControl("TextBox5") as TextBox).Text;
            string txtsupplierFooter = (GridView6.Rows[e.RowIndex].FindControl("ddlCostCenterEdit") as DropDownList).SelectedItem.Text;
            string txtProjectEdit = (GridView6.Rows[e.RowIndex].FindControl("txtProjectEdit") as TextBox).Text;

            //dt.Columns.AddRange(new DataColumn[6] 

            //ddlSupplierEdit, ddlUomEdit, ddlItemMasterEdit
            GridViewRow row = GridView6.Rows[e.RowIndex];
            DataTable dt = (DataTable)ViewState["Customers"];

            dt.Rows[row.DataItemIndex]["ItemSubHeadName"] = txtitemsubfooter;
            dt.Rows[row.DataItemIndex]["Description"] = txtdesc;
            dt.Rows[row.DataItemIndex]["UOM"] = txtuomFooter;
            dt.Rows[row.DataItemIndex]["CurrentStock"] = txtcurrStockFooter;
            dt.Rows[row.DataItemIndex]["RequiredQuantity"] = txtReqStockFooter;
            dt.Rows[row.DataItemIndex]["CostCenter"] = txtsupplierFooter;
            dt.Rows[row.DataItemIndex]["Project"] = txtProjectEdit;


            GridView6.EditIndex = -1;
            PopulateGridview();
            DropDownList ddlsub = GridView6.FooterRow.FindControl("ddlitemsub") as DropDownList;
            dml.dropdownsql(ddlsub, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");

            DropDownList ddlmaster = GridView6.FooterRow.FindControl("ddlitemMaster") as DropDownList;
            dml.dropdownsql(ddlmaster, "SET_ItemMaster", "Description", "ItemID");

            DropDownList ddluom = GridView6.FooterRow.FindControl("ddlUomFooter") as DropDownList;
            dml.dropdownsql(ddluom, "SET_UnitofMeasure", "UOMName", "UOMID");
            //ddlUomFooter  ddlitemMaster ddlsupFooter

            DropDownList ddlsupFooter = GridView6.FooterRow.FindControl("ddlCostCenterFooter") as DropDownList;
            dml.dropdownsql(ddlsupFooter, "SET_CostCenter", "CostCenterName", "CostCenterID");

        }

        catch (Exception ex)
        {
            // lblSuccessMessage.Text = "";
            // lblErrorMessage.Text = ex.Message;
        }
    }
    protected void GridView6_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        GridViewRow row = GridView6.Rows[e.RowIndex];
        DataTable dt = (DataTable)ViewState["Customers"];
        dt.Rows[e.RowIndex].Delete();
        PopulateGridview();
        DropDownList ddlsub = GridView6.FooterRow.FindControl("ddlitemsub") as DropDownList;
        dml.dropdownsql(ddlsub, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");

        DropDownList ddlmaster = GridView6.FooterRow.FindControl("ddlitemMaster") as DropDownList;
        dml.dropdownsql(ddlmaster, "SET_ItemMaster", "Description", "ItemID");

        DropDownList ddluom = GridView6.FooterRow.FindControl("ddlUomFooter") as DropDownList;
        dml.dropdownsql(ddluom, "SET_UnitofMeasure", "UOMName", "UOMID");
        //ddlUomFooter  ddlitemMaster ddlsupFooter

        DropDownList ddlsupFooter = GridView6.FooterRow.FindControl("ddlCostCenterFooter") as DropDownList;
        dml.dropdownsql(ddlsupFooter, "SET_CostCenter", "CostCenterName", "CostCenterID");
    }
    protected void GridView6_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lblRowNumber") as Label).Text = (e.Row.RowIndex + 1).ToString();

        }
    }

    public void detailinsert(string masterid)
    {
        foreach (GridViewRow g in GridView6.Rows)
        {
            Label lblheadsub = (Label)g.FindControl("Label2");
            Label lblitemmaster = (Label)g.FindControl("Label3");
            Label lbluom = (Label)g.FindControl("Label4");
            //Label5
            Label lblcurrstock = (Label)g.FindControl("Label5");
            Label lblreqQ = (Label)g.FindControl("Label6");
            Label lblcost = (Label)g.FindControl("Label7");
            Label lblproject = (Label)g.FindControl("Label8");
            //select ItemSubHeadID, ItemSubHeadName from SET_ItemSubHead

            DataSet ds = dml.Find("select ItemSubHeadID, ItemSubHeadName from SET_ItemSubHead where ItemSubHeadName = '"+ lblheadsub.Text + "'");
            string subhead, itemmaster, uom1, costcenter;
            if (ds.Tables[0].Rows.Count> 0)
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
            DataSet ds3 = dml.Find("select  CostCenterID,CostCenterName from SET_CostCenter where CostCenterName = '" + lblcost.Text + "'");
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

            dml.Insert("INSERT INTO SET_StockRequisitionDetail ([StockReqMId], [ItemSubHead], [ItemMaster], [CostCenter], [UOM], [Project], [CurrentStock], [RequiredQuantity], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted],[BalanceQty]) VALUES ('" + masterid + "', '" + subhead + "', '" + itemmaster + "', '" + costcenter + "', '" + uom1 + "', '" + lblproject.Text + "', '" + lblcurrstock.Text + "', '" + lblreqQ.Text + "', '1', '" + gocid() + "', '" + compid() + "', '" + branchId() +"', '"+ FiscalYear()+ "', '"+show_username()+"', '"+DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")+"', '0','"+ lblreqQ.Text   + "');", "");



            //}
        }
    }

    //public string reqauto()
    //{
    //    string maxno = "0";
    //    DataSet ds = dml.Find("select MAX(RequisitionNo)as reqno from SET_StockRequisitionMaster where Record_Deleted = 0 and GocID= '"+gocid()+"' and CompId='"+compid()+"' and BranchId = '"+branchId()+"' and FiscalYearID = '"+FiscalYear()+"'");
    //    if(ds.Tables[0].Rows.Count> 0)
    //    {
    //         maxno = ds.Tables[0].Rows[0]["reqno"].ToString();
    //    }

    //    int max = int.Parse(maxno) + 1;
    //    return max.ToString();
    //}

    public string required_generateins()
    {

        //  DateTime dateM = DateTime.Parse(txtReqDate.Text);

        bool flag = true;
        bool flag1 = true;
        DateTime date = DateTime.Parse(lblreqdate.Text);
        string year = date.ToString("yy");
        string month = date.Month.ToString("00");
        //string month = date.ToString("00");
        string docno = ddlDocument.SelectedItem.Value;
        FormID = Request.QueryString["FormID"];
        fiscal = Request.QueryString["fiscaly"];
        string fy = fiscal.Substring(2, 2) + fiscal.Substring(7, 2);
        string docval = "0";
        if (ddlDocument.SelectedIndex != 0)
        {
            docval = ddlDocument.SelectedItem.Value;
        }

        DataSet ds = dml.Find("select Monthlybase, YearlyBase from SET_Documents where DocID = '" + ddlDocument.SelectedItem.Value + "'; select MAX(RequisitionNo) as maxno from SET_StockRequisitionMaster where SUBSTRING(RequisitionNo, 3, 4) = '" + month + year + "'; select Description from SET_Fiscal_Year where Description = '" + fiscal + "'; select MAX(RequisitionNo) as maxno from SET_StockRequisitionMaster where SUBSTRING(RequisitionNo, 3, 4) = '" + fy + "'");

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

                txtReqNO.Text = docno + "-" + month + year + "-" + incre.ToString("00000");
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
                txtReqNO.Text = docno + "-" + fyear.Substring(2, 2) + fyear.Substring(7, 2) + "-" + incre.ToString("000000");


            }

        }
        return txtReqNO.Text;
    }

    public void required_generate()
    {

        //  DateTime dateM = DateTime.Parse(txtReqDate.Text);
        lblreqdate.Text = txtReqDate.Text;
        bool flag = true;
        bool flag1 = true;
        DateTime date = DateTime.Parse(txtReqDate.Text);
        string year = date.ToString("yy");
        string month = date.Month.ToString("00");
        //string month = date.ToString("00");
        string docno = ddlDocument.SelectedItem.Value;
        FormID = Request.QueryString["FormID"];
        fiscal = Request.QueryString["fiscaly"];
        string fy = fiscal.Substring(2, 2) + fiscal.Substring(7, 2);
        string docval = "0";
        if (ddlDocument.SelectedIndex != 0)
        {
            docval = ddlDocument.SelectedItem.Value;
        }

        DataSet ds = dml.Find("select Monthlybase, YearlyBase from SET_Documents where DocID = '"+ddlDocument.SelectedItem.Value+"'; select MAX(RequisitionNo) as maxno from SET_StockRequisitionMaster where SUBSTRING(RequisitionNo, 3, 4) = '" + month + year + "'; select Description from SET_Fiscal_Year where Description = '" + fiscal + "'; select MAX(RequisitionNo) as maxno from SET_StockRequisitionMaster where SUBSTRING(RequisitionNo, 3, 4) = '" + fy + "'");

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

                txtReqNO.Text = docno +"-"+ month + year + "-" + incre.ToString("00000");
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
                txtReqNO.Text = docno + "-" + fyear.Substring(2, 2) + fyear.Substring(7, 2) + "-" + incre.ToString("000000");


            }

        }
    }

    public string required_generatestr()
    {
        string retval = "";
        string month = DateTime.Now.Month.ToString("00");
        bool flag = true;
        bool flag1 = true;
        DateTime date = DateTime.Now;
        string year = date.ToString("yy");
        FormID = Request.QueryString["FormID"];
        fiscal = Request.QueryString["fiscaly"];
        string fy = fiscal.Substring(2, 2) + fiscal.Substring(7, 2);
        string docval = "0";
        if (ddlDocument.SelectedIndex != 0)
        {
            docval = ddlDocument.SelectedItem.Value;
        }

        DataSet ds = dml.Find("select Monthlybase, YearlyBase from SET_Documents where DocID = '" + ddlDocument.SelectedItem.Value + "'; select MAX(RequisitionNo) as maxno from SET_StockRequisitionMaster where SUBSTRING(RequisitionNo, 1, 4) = '" + month + year + "'; select Description from SET_Fiscal_Year where Description = '" + fiscal + "'; select MAX(RequisitionNo) as maxno from SET_StockRequisitionMaster where SUBSTRING(RequisitionNo, 1, 4) = '" + fy + "'");

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
    protected void ddlitemsub_SelectedIndexChanged(object sender, EventArgs e)
    {
        //select * from SET_ItemMaster where ItemSubHeadID= 3
        DropDownList ddlsubitem = GridView6.FooterRow.FindControl("ddlitemsub") as DropDownList;
        DropDownList ddlmaster = GridView6.FooterRow.FindControl("ddlitemMaster") as DropDownList;
        dml.dropdownsql(ddlmaster, "SET_ItemMaster", "Description", "ItemID", "ItemSubHeadID", ddlsubitem.SelectedItem.Value);
    }

    protected void rdbDepartment_CheckedChanged(object sender, EventArgs e)
    {
        
    }

    protected void rdbPurchase_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbPurchase.Checked == true)
        {
            if (ddlDocument.SelectedIndex != 0)
            {
                required_generate();
            }
        }
        else
        {

        }
    }

    protected void GridView5_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView5.EditIndex = e.NewEditIndex;

        DataSet ds_detail = dml.Find("select * from view_StockUpdate where StockReqMId = '" + ViewState["SNO"].ToString() + "'");
        if (ds_detail.Tables[0].Rows.Count > 0)
        {

            
            Div1.Visible = true;
            GridView5.DataSource = ds_detail.Tables[0];
            GridView5.DataBind();

        }
        DropDownList ddlitemsub = GridView5.Rows[GridView5.EditIndex].FindControl("ddlitemsubEDIT") as DropDownList;
        dml.dropdownsql(ddlitemsub, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");
        Label lbl1 = GridView5.Rows[GridView5.EditIndex].FindControl("lblitemsubn") as Label;

        DropDownList ddlitemmaster = GridView5.Rows[GridView5.EditIndex].FindControl("ddlItemmter") as DropDownList;
        dml.dropdownsql(ddlitemmaster, "SET_ItemMaster", "Description", "ItemID");
        Label lbl2 = GridView5.Rows[GridView5.EditIndex].FindControl("lblitemmas") as Label;

        ddlitemmaster.ClearSelection();
        ddlitemmaster.Items.FindByText(lbl2.Text).Selected = true;
        lbl2.Visible = false;

        DropDownList uddluom = GridView5.Rows[GridView5.EditIndex].FindControl("ddlUOMs") as DropDownList;
       // dml.dropdownsql(uddluom, "SET_UnitofMeasure", "UOMName", "UOMID");
        dml.dropdownsqlwithquery(uddluom, "select UOMID,UOMName from SET_UnitofMeasure where UOMID in ((SELECT UOMId as uom FROM SET_ItemMaster where ItemID = '" + ddlitemmaster.SelectedItem.Value + "' ) UNION (SELECT UOMId2 as uom FROM SET_ItemMaster where ItemID = '" + ddlitemmaster.SelectedItem.Value + "' ))", "UOMName", "UOMID");
        Label lbl3 = GridView5.Rows[GridView5.EditIndex].FindControl("lblUOMs") as Label;

        DropDownList ddlcc = GridView5.Rows[GridView5.EditIndex].FindControl("ddlCostCenter") as DropDownList;
        dml.dropdownsql(ddlcc, "SET_CostCenter", "CostCenterName", "CostCenterID");
        Label lblcc = GridView5.Rows[GridView5.EditIndex].FindControl("lblCC") as Label;

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

    protected void GridView5_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView5.EditIndex = -1;

        DataSet ds_detail = dml.Find("select * from view_StockUpdate where StockReqMId = '" + ViewState["SNO"].ToString() + "'");
        if (ds_detail.Tables[0].Rows.Count > 0)
        {
            Div1.Visible = true;
            GridView5.DataSource = ds_detail.Tables[0];
            GridView5.DataBind();
        }
    }

    protected void GridView5_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {

            DropDownList txtitemsubfooter = (GridView5.Rows[e.RowIndex].FindControl("ddlitemsubEDIT") as DropDownList);


           
            DropDownList txtdesc = (GridView5.Rows[e.RowIndex].FindControl("ddlItemmter") as DropDownList);
            DropDownList txtuomFooter = (GridView5.Rows[e.RowIndex].FindControl("ddlUOMs") as DropDownList);
            string txtcurrStockFooter = (GridView5.Rows[e.RowIndex].FindControl("TextBox4") as TextBox).Text;
            string txtReqStockFooter = (GridView5.Rows[e.RowIndex].FindControl("TextBox5") as TextBox).Text;
            DropDownList txtsupplierFooter = (GridView5.Rows[e.RowIndex].FindControl("ddlCostCenter") as DropDownList);
            string txtProjectEdit = (GridView5.Rows[e.RowIndex].FindControl("TextBox7") as TextBox).Text;
            string lblsno = (GridView5.Rows[e.RowIndex].FindControl("lblsno") as Label).Text;
            //dt.Columns.AddRange(new DataColumn[6] 

            //ddlSupplierEdit, ddlUomEdit, ddlItemMasterEdit
            GridViewRow row = GridView5.Rows[e.RowIndex];
            DataTable dt = (DataTable)ViewState["dtup"];

            dt.Rows[row.DataItemIndex]["ItemSubHeadName"] = txtitemsubfooter.SelectedItem.Text;
            dt.Rows[row.DataItemIndex]["Description"] = txtdesc.SelectedItem.Text;
            dt.Rows[row.DataItemIndex]["UOMName"] = txtuomFooter.SelectedItem.Text;
            dt.Rows[row.DataItemIndex]["CurrentStock"] = txtcurrStockFooter;
            dt.Rows[row.DataItemIndex]["RequiredQuantity"] = txtReqStockFooter;
            dt.Rows[row.DataItemIndex]["CostCenterName"] = txtsupplierFooter.SelectedItem.Text;
            dt.Rows[row.DataItemIndex]["Project"] = txtProjectEdit;


            dml.Update("UPDATE [SET_StockRequisitionDetail] SET [ItemSubHead]='"+txtitemsubfooter.SelectedItem.Value + "', [ItemMaster]='"+txtdesc.SelectedItem.Value + "', [CostCenter]='"+txtsupplierFooter.SelectedItem.Value + "', [UOM]='"+txtuomFooter.SelectedItem.Value + "', [Project]='"+txtProjectEdit+"', [CurrentStock]='"+txtcurrStockFooter+"', [RequiredQuantity]='"+txtReqStockFooter+"', [Remarks]=NULL, [IsActive]='1', [GocID]='"+gocid()+"', [CompId]='"+compid()+"', [BranchId]='"+branchId()+"', [FiscalYearID]='"+FiscalYear()+"', [UpdatedBy]='"+show_username()+"', [UpdatedDate]='"+DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")+"', [Record_Deleted]='0' WHERE ([Sno]='"+lblsno+"');", "");
           
            GridView5.EditIndex = -1;

            fl = true;
            PopulateGridview_Up();

           
        }

        catch (Exception ex)
        {
            // lblSuccessMessage.Text = "";
            // lblErrorMessage.Text = ex.Message;
        }
    }

    protected void GridView7_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = GridView7.Rows[e.RowIndex];
        DataTable dt = (DataTable)ViewState["dtdel"];
        string lblsno = (GridView7.Rows[e.RowIndex].FindControl("lblsno") as Label).Text;

        dml.Delete("DELETE from SET_StockRequisitionDetail where Sno= '"+lblsno+"'", "");
        dt.Rows[e.RowIndex].Delete();
        PopulateGridview_Del();
    }

    protected void ddlDocument_SelectedIndexChanged(object sender, EventArgs e)
    {
        Div2.Visible = true;
        if (rdbDepartment.Checked == false)
        {
            if (ddlDocument.SelectedIndex != 0)
            {
                required_generate();
            }
        }

        chkActive.Enabled = true;

        ddlDepartment.Enabled = true;
        ddlDocTypes.Enabled = true;
        
        ddlPriority.Enabled = true;
        txtReqNO.Enabled = false;

        txtReqDate.Enabled = true;

        ddllocation.Enabled = true;
        txtCopyReqNo.Enabled = false;
        ddlFromStroeDept.Enabled = true;
        txtRequirmentDueDate.Enabled = true;
        txtRemarks.Enabled = true;
        imgPopup.Enabled = true;
        ImageButton1.Enabled = true;
        rdbDepartment.Enabled = true;
        rdbPurchase.Enabled = true;
        ddlStatus.Enabled = true;



        string datatype = "0";
        DataSet ds = dml.Find("select DocId from SET_Documents where DocID ='" + ddlDocument.SelectedItem.Value + "' and Record_Deleted = 0 and CompID='" + compid() + "' and IsActive = 1;");
        if(ds.Tables[0].Rows.Count> 0)
        {
            datatype = ds.Tables[0].Rows[0]["DocId"].ToString();

        }
        DataSet dsradio = dml.Find("select RadioButton from SET_DocRadioBinding where DocId= '" + ddlDocument.SelectedItem.Value + "' and Record_Deleted = 0 and IsActive = 1;");

        if (dsradio.Tables[0].Rows.Count > 0)
        {

            string d_p = dsradio.Tables[0].Rows[0]["RadioButton"].ToString();
            if(d_p == "DEPARTMENT")
            {
                rdbDepartment.Checked = true;
                rdbPurchase.Checked = false;
                rdbDepartment.Enabled = false;
                rdbPurchase.Enabled = false;
            }
            if(d_p == "PURCHASE")
            {
                rdbPurchase.Checked = true;
                rdbDepartment.Checked = false;
                rdbDepartment.Enabled = false;
                rdbPurchase.Enabled = false;
            }
        }
        else
        {
            rdbDepartment.Enabled = true;
            rdbPurchase.Enabled = true;
            rdbPurchase.Checked = false;
            rdbDepartment.Checked = false;
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
        DataSet dsdoc = dml.Find("select DateFrom, DateTo from SET_UserGrp_DocAuthority where  DocId = '" + ddlDocument.SelectedItem.Value + "' and Record_Deleted = 0  and UserGrpId = '" + UserGrpID + "';");
        if (dsdoc.Tables[0].Rows.Count > 0)
        {
           

            enddate =dml.dateconvertString(dsdoc.Tables[0].Rows[0]["DateTo"].ToString());
           

            if (enddate == "2000-01-01")
            {
                querys = "select DateFrom, DateTo from SET_UserGrp_DocAuthority where DateFrom <= '"+ stdate + "' and DocId = '" + ddlDocument.SelectedItem.Value + "' and Record_Deleted = 0  and UserGrpId = '" + UserGrpID + "';";
            }
            else
            {
                querys = "select DateFrom, DateTo from SET_UserGrp_DocAuthority where DateFrom <= '" + stdate + "' and DateTo >='"+end+"' and DocId = '" + ddlDocument.SelectedItem.Value + "' and Record_Deleted = 0  and UserGrpId = '" + UserGrpID + "';";
            }
            DataSet dd = dml.Find(querys);
            if (dd.Tables[0].Rows.Count > 0)
            {

                if (ddlDocument.SelectedIndex != 0)
                {

                    required_generate();
                    int docno = int.Parse(ddlDocument.SelectedItem.Value);
                    lbldocno.Text = docno.ToString("0000");


                    //;select AuthorityId, AuthorityName from SET_Authority where AuthorityId in (SELECT AuthorityId from SET_UserGrp_DocAuthority where docid= 112)

                    dml.dropdownsqlwithquery(ddlReqStatus, "select AuthorityId, AuthorityName from SET_Authority where AuthorityId in (SELECT AuthorityId from SET_UserGrp_DocAuthority where docid= '" + ddlDocument.SelectedItem.Value + "')", "AuthorityName", "AuthorityId");

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


        string str = ddlDocument.SelectedItem.Text;

        if(str.Contains("RAW MATERIAL"))
        {
            //txtCopyReqNo.Text = "bbbb";
             DropDownList ddlsub = GridView6.FooterRow.FindControl("ddlitemsub") as DropDownList;
            ddlsub.ClearSelection();
            if (ddlsub.Items.FindByText("RAW MATERIAL") != null)
            {
                ddlsub.Items.FindByText("RAW MATERIAL").Selected = true;
            }
            if (ddlsub.SelectedIndex != 0)
            {
                DropDownList ddlmaster = GridView6.FooterRow.FindControl("ddlitemMaster") as DropDownList;
                dml.dropdownsql(ddlmaster, "SET_ItemMaster", "Description", "ItemID", "ItemSubHeadID", ddlsub.SelectedItem.Value);


              
            }
        }
        else if (str.Contains("ACID"))
        {
            DropDownList ddlsub = GridView6.FooterRow.FindControl("ddlitemsub") as DropDownList;
            ddlsub.ClearSelection();

            
            if (ddlsub.Items.FindByText("ACID") != null)
            {
                ddlsub.Items.FindByText("ACID").Selected = true;
            }

            if (ddlsub.SelectedIndex != 0)
            {
                DropDownList ddlmaster = GridView6.FooterRow.FindControl("ddlitemMaster") as DropDownList;
                dml.dropdownsql(ddlmaster, "SET_ItemMaster", "Description", "ItemID", "ItemSubHeadID", ddlsub.SelectedItem.Value);
            }

        }

    }
    public string fiscalstart(string fyear)
    {
        string sdate;
        DataSet ds = dml.Find("select StartDate from SET_Fiscal_Year where Description = '"+fyear+"'");
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

    protected void ddlitemMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlmaster = GridView6.FooterRow.FindControl("ddlitemMaster") as DropDownList;
        if (ddlmaster.SelectedIndex != 0)
        {
            //
            string valitem = "";
            string valitem1 = "";
            DropDownList ddluomm = GridView6.FooterRow.FindControl("ddlUomFooter") as DropDownList;
            dml.dropdownsqlwithquery(ddluomm, "select UOMID,UOMName from SET_UnitofMeasure where UOMID in ((SELECT UOMId as uom FROM SET_ItemMaster where ItemID = '"+ ddlmaster.SelectedItem.Value + "' ) UNION (SELECT UOMId2 as uom FROM SET_ItemMaster where ItemID = '"+ ddlmaster.SelectedItem.Value + "' ))", "UOMName", "UOMID");


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

    protected void txtReqDate_TextChanged(object sender, EventArgs e)
    {
        if (txtReqDate.Text != "")
        {
            lblreqdate.Text = txtReqNO.Text;
            CalendarExtender2.StartDate = DateTime.Parse(txtReqDate.Text);
            required_generate();
        }
    }
    public string detailcond()
    {
        Label dp = GridView6.Rows[0].FindControl("lblRowNumber") as Label;

        if (dp.Text != "")
        {
            return  dp.Text; 
        }
        else
        {
            return "";
        }
    }
    protected void RadComboAuth_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Compid = '" + compid() + "' and Record_Deleted = '0'";
               
        cmb.serachcombo2Stock(RadComboAuth, e.Text, "AuthorityId", "AuthorityLevel", "AuthorityName", "SET_Authority", where, "AuthorityName");

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

    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}