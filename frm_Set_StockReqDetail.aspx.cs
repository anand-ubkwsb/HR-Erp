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
    int DateFrom, AddDays, EditDays, DeleteDays;
    string userid, UserGrpID, FormID, fiscal;
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
            btnUpdate.Visible = false;
            btnSave.Visible = false;
            btnDelete_after.Visible = false;
            userid = Request.QueryString["UserID"];
            UserGrpID = Request.QueryString["UsergrpID"];
            FormID = Request.QueryString["FormID"];
            fiscal = Request.QueryString["fiscaly"];

            dml.dropdownsql(ddlItemMaster, "SET_ItemMaster", "Description", "ItemID");
            dml.dropdownsql(ddlItemSubHead, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");
            dml.dropdownsql(ddlCostCenter, "SET_CostCenter", "CostCenterName", "CostCenterID");
            dml.dropdownsql(ddlUOM, "SET_UnitofMeasure", "UOMName", "UOMID");

            dml.dropdownsql(ddlEdit_ItemSubHead, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");
            dml.dropdownsql(ddlDel_ItemSubHead, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");
            dml.dropdownsql(ddlFind_ItemSubHead, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");

            dml.dropdownsql(ddlEdit_ItemMaster, "SET_ItemMaster", "Description", "ItemID");
            dml.dropdownsql(ddlDel_ItemMaster, "SET_ItemMaster", "Description", "ItemID");
            dml.dropdownsql(ddlFind_ItemMaster, "SET_ItemMaster", "Description", "ItemID");

            dml.dropdownsql(ddlEdit_CostCenter, "SET_CostCenter", "CostCenterName", "CostCenterID");
            dml.dropdownsql(ddlDel_CostCenetr, "SET_CostCenter", "CostCenterName", "CostCenterID");
            dml.dropdownsql(ddlFind_CostCenter, "SET_CostCenter", "CostCenterName", "CostCenterID");
           
            dml.dropdownsql(ddlReqNO, "SET_StockRequisitionMaster", "RequisitionNo", "Sno");

            //View_DropDocStockMAster

         //   dml.dropdownsql(ddlStockMasterDoc, "View_DropDocStockMAster", "DocDescription", "Sno");

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


        chkActive.Enabled = true;

        ddlItemMaster.Enabled = true;
        ddlItemSubHead.Enabled = true;
        ddlCostCenter.Enabled = true;
        ddlReqNO.Enabled = true;
        ddlUOM.Enabled = true;
        txtRemarks.Enabled = true;
        txtProject.Enabled = true;
        txtCurrentStock.Enabled = true;
        txtReqQuantity.Enabled = true;

        txtCreatedby.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdateBy.Enabled = true;
        txtUpdateDate.Enabled = true;

        
        chkActive.Checked = true;
        txtCreatedDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

        userid = Request.QueryString["UserID"];
        DataSet ds_autoUserName = dml.Find("select user_name from SET_User_Manager where UserId ='" + userid + "'");
        txtCreatedby.Text = ds_autoUserName.Tables[0].Rows[0]["user_name"].ToString();

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        int chk = 0;
            if (chkActive.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
            dml.Insert("INSERT INTO SET_StockRequisitionDetail ([ItemSubHead], [ItemMaster], [CostCenter], [UOM],[StockReqMId], [Project], [CurrentStock], [RequiredQuantity], [Remarks], [IsActive], [GocID], [CompId], [BranchId], [FiscalYearID], [CreatedBy], [CreatedDate], [Record_Deleted]) VALUES ('" + ddlItemSubHead.SelectedItem.Value+"', '"+ddlItemMaster.SelectedItem.Value+"', '"+ddlCostCenter.SelectedItem.Value+"', '"+ddlUOM.SelectedItem.Value+"', '"+ddlReqNO.SelectedItem.Value+"' ,'"+txtProject.Text+"', '"+txtCurrentStock.Text+"', '"+txtReqQuantity.Text+"', '"+txtRemarks.Text+"', '"+chk+"', "+gocid()+", "+compid()+", "+branchId()+", "+FiscalYear()+", '"+show_username()+"', '"+DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")+"', '0');", "");
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);

            Label1.Text = "";

        ddlItemMaster.SelectedIndex = 0;
        ddlItemSubHead.SelectedIndex = 0;
        ddlCostCenter.SelectedIndex = 0;
        ddlReqNO.SelectedIndex = 0;
        ddlUOM.SelectedIndex = 0;
        txtRemarks.Text = "";
        txtProject.Text = "";
        txtCurrentStock.Text = "";
        txtReqQuantity.Text = "";

        txtCreatedDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int chk = 0; 
        if (chkActive.Checked == true)
        {
            chk = 1;
        }
        else
        {
            chk = 0;
        }


        DataSet ds_up = dml.Find("select * from SET_StockRequisitionDetail WHERE ([ItemSubHead]='" + ddlItemSubHead.SelectedItem.Value+"') AND ([ItemMaster]='"+ddlItemMaster.SelectedItem.Value+"') AND ([CostCenter]='"+ddlCostCenter.SelectedItem.Value+"') AND ([UOM]='"+ddlUOM.SelectedItem.Value+ "') AND ([StockReqMId]= '"+ddlReqNO.SelectedItem.Value+"') AND ([Project]='" + txtProject.Text+"') AND ([CurrentStock]='"+txtCurrentStock.Text+"') AND ([RequiredQuantity]='"+txtReqQuantity.Text+"') AND ([Remarks]='"+txtRemarks.Text+"') AND ([IsActive]='"+chk+"')");

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



            dml.Update("UPDATE SET_StockRequisitionDetail SET  [ItemSubHead]='"+ddlItemSubHead.SelectedItem.Value+"', [ItemMaster]='"+ddlItemMaster.SelectedItem.Value+"', [CostCenter]='"+ddlCostCenter.SelectedItem.Value+"', [UOM]='"+ddlUOM.SelectedItem.Value+ "', [StockReqMId]='"+ddlReqNO.SelectedItem.Text+"', [Project]='" + txtProject.Text+"', [CurrentStock]='"+txtCurrentStock.Text+"', [RequiredQuantity]='"+txtReqQuantity.Text+"', [Remarks]='"+txtRemarks.Text+"', [IsActive]='"+chk+"', [UpdatedBy]='"+show_username()+"', [UpdatedDate]='"+DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")+"' WHERE ([Sno]='"+ViewState["SNO"].ToString()+"')", "");
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
            string squer = "select * from View_stockReqDetail";


            if (ddlDel_ItemSubHead.SelectedIndex != 0)
            {
                swhere = "ItemSubHeadName = '" + ddlDel_ItemSubHead.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "ItemSubHeadName is not null";
            }

            if (ddlDel_ItemMaster.SelectedIndex != 0)
            {
                swhere = swhere + " and Description = '" + ddlDel_ItemMaster.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and Description is not null";
            }
            if (ddlFind_CostCenter.SelectedIndex != 0)
            {
                swhere = swhere + " and CostCenterName = '" + ddlFind_CostCenter.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and CostCenterName is not null";
            }

            if (chkDel_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkDel_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and FiscalYearID = '" + FiscalYear() + "' and GocID='" + gocid() + "' and CompId = '" + compid() + "' and BranchId='" + branchId() + "' ORDER BY Description";

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
            string squer = "select * from View_stockReqDetail";


            if (ddlFind_ItemSubHead.SelectedIndex != 0)
            {
                swhere = "ItemSubHeadName = '" + ddlFind_ItemSubHead.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "ItemSubHeadName is not null";
            }

            if (ddlFind_ItemMaster.SelectedIndex != 0)
            {
                swhere = swhere + " and Description = '" + ddlFind_ItemMaster.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and Description is not null";
            }
            if (ddlFind_CostCenter.SelectedIndex != 0)
            {
                swhere = swhere + " and CostCenterName = '" + ddlFind_CostCenter.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and CostCenterName is not null";
            }

            if (chkFind_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkFind_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and FiscalYearID = '" + FiscalYear() + "' and GocID='" + gocid() + "' and CompId = '" + compid() + "' and BranchId='" + branchId() + "' ORDER BY Description";

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
            string squer = "select * from View_stockReqDetail";
            //SELECT Description,ItemSubHeadName,CostCenterName,IsActive FROM View_stockReqDetail


            if (ddlEdit_ItemSubHead.SelectedIndex != 0)
            {
                swhere = "ItemSubHeadName = '" + ddlEdit_ItemSubHead.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "ItemSubHeadName is not null";
            }

            if (ddlEdit_ItemMaster.SelectedIndex != 0)
            {
                swhere = swhere + " and Description = '" + ddlEdit_ItemMaster.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and Description is not null";
            }
            if (ddlEdit_CostCenter.SelectedIndex != 0)
            {
                swhere = swhere + " and CostCenterName = '" + ddlEdit_CostCenter.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and CostCenterName is not null";
            }
            if (chkEdit_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkEdit_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and FiscalYearID = '" + FiscalYear() + "' and GocID='" + gocid() + "' and CompId = '" + compid() + "' and BranchId='" + branchId() + "'  ORDER BY Description";

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

        ddlItemMaster.SelectedIndex = 0;
        ddlItemSubHead.SelectedIndex= 0;
        ddlCostCenter.SelectedIndex = 0;
        ddlReqNO.SelectedIndex = 0;
        ddlUOM.SelectedIndex = 0;
        txtRemarks.Text = "";
        txtProject.Text = "";
        txtCurrentStock.Text = "";
        txtReqQuantity.Text = "";



        txtCreatedby.Text = "";
        txtCreatedDate.Text = "";
        txtUpdateBy.Text = "";
        txtUpdateDate.Text = "";
        chkActive.Enabled = false;

        ddlItemMaster.Enabled = false;
        ddlItemSubHead.Enabled = false;
        ddlCostCenter.Enabled = false;
        ddlUOM.Enabled = false;
        txtRemarks.Enabled = false;
        txtProject.Enabled = false;
        txtCurrentStock.Enabled = false;
        txtReqQuantity.Enabled = false;
        ddlReqNO.Enabled = false;


        txtCreatedby.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdateBy.Enabled = false;
        txtUpdateDate.Enabled = false;
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
            //DataSet ds = dml.Find("select * from SET_BankBranch where Bankid = '" + ViewState["SNO"].ToString() + "'  and Record_Deleted = '0' ");
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    string count = ds.Tables[0].Rows.Count.ToString();
            //    Label1.Text = "This bank has " + count + " Branch. First delete Branch"; 
            //}
            //else {


                dml.Delete("update SET_StockRequisitionDetail set Record_Deleted = 1 where sno = " + ViewState["SNO"].ToString() + "", "");
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



            DataSet ds = dml.Find("select * from SET_StockRequisitionDetail WHERE ([Sno]='" + serial_id+"') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlItemMaster.ClearSelection();
                ddlItemSubHead.ClearSelection();
                ddlCostCenter.ClearSelection();
                ddlReqNO.ClearSelection();
                ddlUOM.ClearSelection();

                ddlItemMaster.Items.FindByValue(ds.Tables[0].Rows[0]["ItemMaster"].ToString()).Selected = true;
                ddlItemSubHead.Items.FindByValue(ds.Tables[0].Rows[0]["ItemSubHead"].ToString()).Selected = true;
                ddlCostCenter.Items.FindByValue(ds.Tables[0].Rows[0]["CostCenter"].ToString()).Selected = true;
                
                ddlReqNO.Items.FindByValue(ds.Tables[0].Rows[0]["StockReqMId"].ToString()).Selected = true;
                ddlUOM.Items.FindByValue(ds.Tables[0].Rows[0]["UOM"].ToString()).Selected = true;
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                txtProject.Text = ds.Tables[0].Rows[0]["Project"].ToString();
                txtCurrentStock.Text = ds.Tables[0].Rows[0]["CurrentStock"].ToString();
                txtReqQuantity.Text = ds.Tables[0].Rows[0]["RequiredQuantity"].ToString();

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
             

                updatecol.Visible = true;


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



            DataSet ds = dml.Find("select * from SET_StockRequisitionDetail WHERE ([Sno]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlItemMaster.ClearSelection();
                ddlItemSubHead.ClearSelection();
                ddlCostCenter.ClearSelection();
                ddlReqNO.ClearSelection();
                ddlUOM.ClearSelection();

                ddlItemMaster.Items.FindByValue(ds.Tables[0].Rows[0]["ItemMaster"].ToString()).Selected = true;
                ddlItemSubHead.Items.FindByValue(ds.Tables[0].Rows[0]["ItemSubHead"].ToString()).Selected = true;
                ddlCostCenter.Items.FindByValue(ds.Tables[0].Rows[0]["CostCenter"].ToString()).Selected = true;
                ddlUOM.Items.FindByValue(ds.Tables[0].Rows[0]["UOM"].ToString()).Selected = true;
                ddlReqNO.Items.FindByValue(ds.Tables[0].Rows[0]["StockReqMId"].ToString()).Selected = true;
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                txtProject.Text = ds.Tables[0].Rows[0]["Project"].ToString();
                txtCurrentStock.Text = ds.Tables[0].Rows[0]["CurrentStock"].ToString();
                txtReqQuantity.Text = ds.Tables[0].Rows[0]["RequiredQuantity"].ToString();

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


                updatecol.Visible = true;


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


       

        ddlItemMaster.Enabled = true;
        ddlItemSubHead.Enabled = true;
        ddlCostCenter.Enabled = true;
        ddlUOM.Enabled = true;
        txtRemarks.Enabled = true;
        txtProject.Enabled = true;
        txtCurrentStock.Enabled = true;
        txtReqQuantity.Enabled = true;
        ddlReqNO.Enabled = true;

        chkActive.Enabled = true;
       
        txtCreatedby.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdateBy.Enabled = false;
        txtUpdateDate.Enabled = false;


        updatecol.Visible = true;
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {
            string serial_id;
            serial_id = GridView3.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;



            DataSet ds = dml.Find("select * from SET_StockRequisitionDetail WHERE ([Sno]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlItemMaster.ClearSelection();
                ddlItemSubHead.ClearSelection();
                ddlCostCenter.ClearSelection();
                ddlReqNO.ClearSelection();
                ddlUOM.ClearSelection();

                ddlItemMaster.Items.FindByValue(ds.Tables[0].Rows[0]["ItemMaster"].ToString()).Selected = true;
                ddlItemSubHead.Items.FindByValue(ds.Tables[0].Rows[0]["ItemSubHead"].ToString()).Selected = true;
                ddlCostCenter.Items.FindByValue(ds.Tables[0].Rows[0]["CostCenter"].ToString()).Selected = true;
                ddlUOM.Items.FindByValue(ds.Tables[0].Rows[0]["UOM"].ToString()).Selected = true;
                ddlReqNO.Items.FindByValue(ds.Tables[0].Rows[0]["StockReqMId"].ToString()).Selected = true;
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                txtProject.Text = ds.Tables[0].Rows[0]["Project"].ToString();
                txtCurrentStock.Text = ds.Tables[0].Rows[0]["CurrentStock"].ToString();
                txtReqQuantity.Text = ds.Tables[0].Rows[0]["RequiredQuantity"].ToString();

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


                updatecol.Visible = true;


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
    protected void ddlItemSubHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlItemSubHead.SelectedIndex != 0)
        {
            dml.dropdownsql(ddlItemMaster, "SET_ItemMaster", "Description", "ItemID", "ItemSubHeadID", ddlItemSubHead.SelectedItem.Value);


        }
    }
}