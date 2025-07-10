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
    int DateFrom, DeleteDays, EditDays, AddDays;
    string userid, UserGrpID, FormID, fiscal, menuid;
    DmlOperation dml = new DmlOperation();
    radcomboxclass cmb = new radcomboxclass();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
   

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
            //userid = Request.QueryString["UserID"];
            //UserGrpID = Request.QueryString["UsergrpID"];
            //FormID = Request.QueryString["FormID"];
            //menuid = Request.QueryString["Menuid"];
            //fiscal = Request.QueryString["fiscaly"];

            

            Findbox.Visible = false;
            DeleteBox.Visible = false;
            Editbox.Visible = false;


        }
    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
       
      
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
       
        
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
       
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

            if (txtempid.Text != "")
            {
                swhere = "DocDescription = '" + txtempid.Text + "'";
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
       
        DeleteBox.Visible = false;
        Editbox.Visible = false;
        Findbox.Visible = false;
        fieldbox.Visible = true;
   
      



    }
    public void Showall_Dml()
    {
        userid = Request.QueryString["UserID"];
        FormID = Request.QueryString["FormID"];
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("select * from SET_UserGrp_Form where FormId='" + FormID + "' and DmlAllowed = 'Y'", con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["Add"].ToString() == "N")
            {
                btnInsert.Visible = false;
            }
            if (ds.Tables[0].Rows[0]["Edit"].ToString() == "N")
            {
                btnEdit.Visible = false;
            }
            if (ds.Tables[0].Rows[0]["Delete"].ToString() == "N")
            {
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
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            

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
   
       
    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    public string show_username()
    {
        userid = Request.QueryString["UserID"];
        DataSet ds_autoUserName = dml.Find("select user_name from SET_User_Manager where UserId='" + userid + "'");
        return ds_autoUserName.Tables[0].Rows[0]["user_name"].ToString();
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
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
}