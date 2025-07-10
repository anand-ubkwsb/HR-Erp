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
    int AddDays, EditDays, DeleteDays, DateFrom;
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
            CalendarExtender1.EndDate = DateTime.Now;
            
            dml.dropdownsql(ddlDepartment, "SET_Department", "DepartmentName", "DepartmentID");
            dml.dropdownsql(ddlFind_Department, "SET_Department", "DepartmentName", "DepartmentID");
            dml.dropdownsql(ddlEdit_Department, "SET_Department", "DepartmentName", "DepartmentID");
            dml.dropdownsql(ddlDel_Department, "SET_Department", "DepartmentName", "DepartmentID");
            dml.dropdownsql(ddlBPartner, "SET_BusinessPartner", "BPartnerName", "BPartnerId");

            dml.daterangeforfiscal(CalendarExtender1, Request.Cookies["fiscalyear"].Value);
            txtGLDate.Attributes.Add("readonly", "readonly");
            dml.daterangeforfiscal(CalendarExtender2, Request.Cookies["fiscalyear"].Value);
            txtBillDate.Attributes.Add("readonly", "readonly");
            dml.daterangeforfiscal(CalendarExtender3, Request.Cookies["fiscalyear"].Value);
            txtExpiryDate.Attributes.Add("readonly", "readonly");
            // dml.dropdownsql(ddlFind_ItemCode, "SET_ItemMasterOthers", "ItemCode", "ItemMasterOthersID");
            // dml.dropdownsql(ddlEdit_ItemCode, "SET_ItemMasterOthers", "ItemCode", "ItemMasterOthersID");
            //  dml.dropdownsql(ddlDel_ItemCode, "SET_ItemMasterOthers", "ItemCode", "ItemMasterOthersID");
           
            Showall_Dml();

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

        //dml.dropToolTip(RadComboItem_Code, "SET_ItemMaster", "ItemID", "ItemCode", "Description");
        RadComboItem_Code.Enabled = true;
        rdbPur.Enabled = true;
        rdbSale.Enabled = true;
        txtGLDate.Enabled = true;
        ddlDepartment.Enabled = true;
        ddlBPartner.Enabled = true;
        txtBillNo.Enabled = true;
        txtBillDate.Enabled = true;
        txtOpeningValue.Enabled = true;
        txtOpeningQuantity.Enabled = true;
        txtItemRate.Enabled = false;
        txtExpiryDate.Enabled = true;
        txtBatchNo.Enabled = true;
        txtBalance_Quantity.Enabled = false;
        txtBalance_Value.Enabled = false;
        chkActive.Enabled = true;
        txtCreatedby.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdateBy.Enabled = false;
        txtUpdateDate.Enabled = false;
        imgPopup.Enabled = true;
        imgPopup1.Enabled = true;
        imgPopup12.Enabled = true;
        uploadtable.Visible = true;
        chkActive.Checked = true;
        DataSet dataset = dml.Find("select UploadButtonDis_Ena from SET_ItemMasterOpening where GocID='" + gocid() + "'and CompId='" + compid() + "' and BranchId='" + branchId() + "';");
        if (dataset.Tables[0].Rows.Count > 0)
        {
            string str_val = dataset.Tables[0].Rows[0]["UploadButtonDis_Ena"].ToString();
            if (str_val == "False")
            {
                FileUpload1.Enabled = false;
                Button1.Enabled = false;
            }
        }

        txtCreatedDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

        userid = Request.QueryString["UserID"];
        DataSet ds_autoUserName = dml.Find("select user_name from SET_User_Manager where UserId ='" + userid + "'");
        txtCreatedby.Text = ds_autoUserName.Tables[0].Rows[0]["user_name"].ToString();

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {


        DataSet uniqueg_B_C = dml.Find("select * from SET_ItemMasterOpening where GocID = '" + gocid() + "'  and ItemCode = '" + RadComboItem_Code.Text + "' and  CompId = '" + compid() + "' and  BranchId = '" + branchId() + "'");
        if (uniqueg_B_C.Tables[0].Rows.Count > 0)
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = "Duplicated entry not allowed";
        }

        else {
            if (rdbPur.Checked == false && rdbSale.Checked == false)
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "Please Select Purchase Or sale Option";
            }
            else {

                int chk = 0;
                if (chkActive.Checked == true)
                {
                    chk = 1;
                }
                else
                {
                    chk = 0;
                }
                int rdbPS = 0;
                if (rdbPur.Checked == true)
                {
                    rdbPS = 0;
                }
                if (rdbSale.Checked == true)
                {
                    rdbPS = 1;
                }

                string val;
                if (String.IsNullOrEmpty(RadComboItem_Code.SelectedValue))
                {
                    val = String.Empty;
                }
                else
                {
                    val = RadComboItem_Code.SelectedValue.Split(new char[] { ':' })[0];
                }
                string bp_val = "0";
                if (ddlBPartner.SelectedIndex != 0)
                {
                    bp_val = ddlBPartner.SelectedItem.Value;
                }

                string date = "", exp_date = "";
                if (txtBillDate.Text != "")
                {
                    date = dml.dateconvertforinsert(txtBillDate);
                }
                if (txtExpiryDate.Text != "")
                {
                    exp_date = dml.dateconvertforinsert(txtExpiryDate);
                }

                dml.Insert("INSERT INTO [SET_ItemMasterOpening] ([GocID], [ItemID], [ItemCode], [Purchase_Sales],[GLDate], [CompId], [BranchId], [DepartmentId],[BPartnerId], [BillNo], [BillDate], [OpeningQuantity], [ItemRate], [OpeningValue], [ExpiryDate],[BatchNo], [BalanceQuatity], [BalanceValue], [IsActive], [CreatedBy], [CreateDate],[Record_Deleted],[MLD]) VALUES ('" + gocid() + "', '" + val + "', '" + RadComboItem_Code.Text + "', '" + rdbPS + "', '" + txtGLDate.Text + "', '" + compid() + "', '" + branchId() + "', '" + ddlDepartment.SelectedItem.Value + "', '" + bp_val + "', '" + txtBillNo.Text + "', '" + date + "', '" + txtOpeningQuantity.Text + "', '" + txtItemRate.Text + "', '" + txtOpeningValue.Text + "', '" + exp_date + "', '" + txtBatchNo.Text + "', '" + txtBalance_Quantity.Text + "', '" + txtBalance_Value.Text + "', '" + chk + "', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' , '0','"+dml.Encrypt("h")+"');", "alertme()");

                dml.Update("update SET_BusinessPartner set MLD = '" + dml.Encrypt("q") + "' where BPartnerID = '" + ddlBPartner.SelectedItem.Value + "'", "");
                dml.Update("update Set_Department set MLD = '" + dml.Encrypt("q") + "' where DepartmentID = '" + ddlDepartment.SelectedItem.Value + "'", "");

                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);
                Label1.Text = "";
                Label1.Text = "";
                RadComboItem_Code.SelectedIndex = 0;

                txtGLDate.Text = "";
                ddlDepartment.SelectedIndex = 0;
                ddlBPartner.SelectedIndex = 0;
                txtBillNo.Text = "";
                txtBillDate.Text = "";
                txtOpeningValue.Text = "";
                txtOpeningQuantity.Text = "";
                txtItemRate.Text = "";
                txtExpiryDate.Text = "";
                txtBatchNo.Text = "";
                txtBalance_Quantity.Text = "";
                txtBalance_Value.Text = "";

                txtCreatedby.Text = "";
                txtCreatedDate.Text = "";
                txtUpdateBy.Text = "";
                txtUpdateDate.Text = "";
                RadComboItem_Code.Focus();
                chkActive.Checked = true;
            }
        }
        FormID = Request.QueryString["FormID"];
        Showall_Dml();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string abbb = txtGLDate.Text;
        int rdbPS = 0;
        if (rdbPur.Checked == true)
        {
            rdbPS = 0;
        }
        if (rdbSale.Checked == true)
        {
            rdbPS = 1;
        }
        int chk = 0;
        if (chkActive.Checked == true)
        {
            chk = 1;
        }
        else
        {
            chk = 0;
        }
        if(txtBalance_Value.Text == "")
        {
            txtBalance_Value.Text = "0.0";    
        }


        string val;
        if (String.IsNullOrEmpty(RadComboItem_Code.SelectedValue))
        {
            val = String.Empty;
        }
        else
        {
            val = RadComboItem_Code.SelectedValue.Split(new char[] { ':' })[0];
        }
        string bp_val = "0";
        if (ddlBPartner.SelectedIndex != 0)
        {
            bp_val = ddlBPartner.SelectedItem.Value;
        }

        string date = "", exp_date = "";
        //if (txtArea.Text == "")
        //{
        //    str_area = "([AreaID] IS NULL)";
        //}
        //else
        //{
        //    str_area = "([AreaID] = '" + txtArea.Text + "')";
        //}
        string str_billdate;
        if (txtBillDate.Text != "")
        {
            date = dml.dateconvertforinsert(txtBillDate);
            str_billdate = "([BillDate] = '" + date + "')";
           
        }
        else
        {
            str_billdate = "([BillDate] ='1900-01-01')";
        }
        string str_ExpiryDate;
        if (txtExpiryDate.Text != "")
        {
            exp_date = dml.dateconvertforinsert(txtExpiryDate);
            str_ExpiryDate = "([ExpiryDate] = '" + exp_date + "')";

        }
        else
        {
            str_ExpiryDate = "([ExpiryDate]='1900-01-01')";
        }

        int bpid, dep;
        if (ddlBPartner.SelectedIndex == 0)
        {
            bpid = 0;
        }
        else
        {
            bpid = int.Parse(ddlBPartner.SelectedItem.Value);
        }
        if (ddlDepartment.SelectedIndex == 0)
        {
            dep = 0;
        }
        else
        {
            dep = int.Parse(ddlDepartment.SelectedItem.Value);
        }
        DataSet ds_up = dml.Find("select * from SET_ItemMasterOpening WHERE ([ItemMasterOpeningID]='" + ViewState["SNO"].ToString() + "') AND ([GocID]='" + gocid() + "') AND ([ItemCode]='" + RadComboItem_Code.Text + "') AND ([Purchase_Sales]='" + rdbPS + "') AND ([GLDate]='" + dml.dateconvertforinsert(txtGLDate) + "') AND ([CompId]='" + compid() + "') AND ([BranchId]='" + branchId() +"') AND ([DepartmentId]='"+dep+"') AND ([BPartnerId]='"+ bpid + "') AND ([BillNo]='"+txtBillNo.Text+"') AND "+str_billdate+" AND ([OpeningQuantity]='"+txtOpeningQuantity.Text+"') AND ([ItemRate]='"+txtItemRate.Text+"') AND ([OpeningValue]='"+txtOpeningValue.Text+"') AND "+str_ExpiryDate+" AND ([BatchNo]='"+txtBatchNo.Text+"') AND ([BalanceQuatity]='"+txtBalance_Quantity.Text+"') AND ([BalanceValue]='"+txtBalance_Value.Text+"') AND ([IsActive]='"+chk+"')");

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

            string gldate = "";
            if (abbb != "")
            {
                gldate = dml.dateconvertString(abbb);
            }

            dml.Update("UPDATE [SET_ItemMasterOpening] SET  [ItemID]='" + val + "', [ItemCode]='" + RadComboItem_Code.SelectedItem.Text + "', [Purchase_Sales]='" + rdbPS + "', [GLDate]='" + gldate + "', [DepartmentId]='" + ddlDepartment.SelectedItem.Value + "', [BPartnerId]='" + bp_val + "', [BillNo]='" + txtBillNo.Text + "', [BillDate]='" + date + "', [OpeningQuantity]='" + txtOpeningQuantity.Text + "', [ItemRate]='" + txtItemRate.Text + "', [OpeningValue]='" + txtOpeningValue.Text + "', [ExpiryDate]='" + exp_date + "', [BatchNo]='" + txtBatchNo.Text + "', [BalanceQuatity]='" + txtBalance_Quantity.Text + "', [BalanceValue]='" + txtBalance_Value.Text + "', [IsActive]='" + chk + "', [UpdatedBy]='" + show_username() + "', [UpdateDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' WHERE ([ItemMasterOpeningID]='" + ViewState["SNO"].ToString() + "');", "");
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
        FormID = Request.QueryString["FormID"];
        Showall_Dml();
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
        FormID = Request.QueryString["FormID"];
        Showall_Dml();
        RadComboItem_Code.Text = "";
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
            string squer = "select * from SET_ItemMasterOpening";
            // ItemCode,GLDate,DepartmentId,OpeningValue,IsActive

            if (radcmbDel_Item_Code.Text != "")
            {
                swhere = "ItemCode = '" + radcmbDel_Item_Code.Text + "'";
            }
            else
            {
                swhere = "ItemCode is not null";
            }

            if (txtdel_Gldate.Text != "")
            {
                swhere = swhere + " and GLDate = '" + txtdel_Gldate.Text + "'";
            }
            if (ddlDel_Department.SelectedIndex != 0)
            {
                swhere = swhere + " and DepartmentId = '" + ddlDel_Department.SelectedItem.Value + "'";
            }
            if (txtdel_OpenVal.Text != "")
            {
                swhere = swhere + " and OpeningValue = '" + txtdel_OpenVal.Text + "'";
            }

            if (chkDel_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkDel_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and GocId = '" + gocid() + "' and Compid = '" + compid() + "' and BranchId= '" + branchId() + "' ORDER BY ItemCode";

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
            string squer = "select * from SET_ItemMasterOpening";
            // ItemCode,GLDate,DepartmentId,OpeningValue,IsActive

            if (radcmbFind_ItemCode.Text != "")
            {
                swhere = "ItemCode = '" + radcmbFind_ItemCode.Text + "'";
            }
            else
            {
                swhere = "ItemCode is not null";
            }

            if (txtFind_GlDate.Text != "")
            {
                swhere   = swhere + " and GLDate = '" + txtFind_GlDate.Text + "'";
            }
            if (ddlFind_Department.SelectedIndex != 0)
            {
                swhere = swhere + " and DepartmentId = '" + ddlFind_Department.SelectedItem.Value + "'";
            }
            if (txtFind_OpenVal.Text != "")
            {
                swhere = swhere + " and OpeningValue = '" + txtFind_OpenVal.Text + "'";
            }

            if (chkFind_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkFind_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and GocId = '" + gocid() + "' and Compid = '" + compid() + "' and BranchId= '" + branchId() +"' ORDER BY ItemCode";

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
            string swhere = "";
            string squer = "select * from SET_ItemMasterOpening";
            // ItemCode,GLDate,DepartmentId,OpeningValue,IsActive

            if (RadcmbItem_Code.Text != "")
            {
                swhere = "ItemCode = '" + RadcmbItem_Code.Text + "'";
            }
            else
            {
                swhere = "ItemCode is not null";
            }

            if (txtEdit_GlDate.Text != "")
            {
                swhere = swhere + " and GLDate = '" + txtEdit_GlDate.Text + "'";
            }
            if (ddlEdit_Department.SelectedIndex != 0)
            {
                swhere = swhere + " and DepartmentId = '" + ddlEdit_Department.SelectedItem.Value + "'";
            }
            if (txtEdit_OpenVal.Text != "")
            {
                swhere = swhere + " and OpeningValue = '" + txtEdit_OpenVal.Text + "'";
            }

            if (chkEdit_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkEdit_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and GocId = '" + gocid() + "' and Compid = '" + compid() + "' and BranchId= '" + branchId() + "' ORDER BY ItemCode";

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
        uploadtable.Visible = false;
        RadComboItem_Code.Text = "";
        RadComboItem_Code.Enabled = false;
        rdbPur.Enabled = false;
        rdbSale.Enabled = false;
        txtGLDate.Enabled = false;
        ddlDepartment.Enabled = false;
        ddlBPartner.Enabled = false;
        txtBillNo.Enabled = false;
        txtBillDate.Enabled = false;
        txtOpeningValue.Enabled = false;
        txtOpeningQuantity.Enabled = false;
        txtItemRate.Enabled = false;
        txtExpiryDate.Enabled = false;
        txtBatchNo.Enabled = false;
        txtBalance_Quantity.Enabled = false;
        txtBalance_Value.Enabled = false;
        chkActive.Enabled = false;
        txtCreatedby.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdateBy.Enabled = false;
        txtUpdateDate.Enabled = false;
        imgPopup.Enabled = false;
        imgPopup1.Enabled = false;
        imgPopup12.Enabled = false;

        chkActive.Checked = true;

        Label1.Text = "";
        RadComboItem_Code.Text = "";
       // rdbPur.Text  = "";
       // rdbSale.Text  = "";
        txtGLDate.Text  = "";
        ddlDepartment.SelectedIndex  = 0;
        ddlBPartner.SelectedIndex  = 0;
        txtBillNo.Text  = "";
        txtBillDate.Text  = "";
        txtOpeningValue.Text  = "";
        txtOpeningQuantity.Text  = "";
        txtItemRate.Text  = "";
        txtExpiryDate.Text  = "";
        txtBatchNo.Text  = "";
        txtBalance_Quantity.Text  = "";
        txtBalance_Value.Text  = "";
        //chkActive.Text  = "";
        txtCreatedby.Text  = "";
        txtCreatedDate.Text  = "";
        txtUpdateBy.Text  = "";
        txtUpdateDate.Text  = "";
        rdbPur.Checked = false;
        rdbSale.Checked = false;
        


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
            dml.Delete("update SET_ItemMasterOpening set Record_Deleted = 1 where ItemMasterOpeningID = " + ViewState["SNO"].ToString() + "", "");
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
        uploadtable.Visible = false;
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
            DataSet ds = dml.Find("select * from SET_ItemMasterOpening where ItemMasterOpeningID = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlDepartment.ClearSelection();
                ddlBPartner.ClearSelection();
                RadComboItem_Code.ClearSelection();

                string str = ds.Tables[0].Rows[0]["ItemCode"].ToString();


                string where = "GocID = '" + gocid() + "' and Record_Deleted = '0'";

                cmb.serachcombo3(RadComboItem_Code, str, "ItemCode", "ItemID", "Description", "Local_Import", "SET_ItemMaster", where, "ItemCode");
                RadComboBoxItem item = RadComboItem_Code.FindItemByText(ds.Tables[0].Rows[0]["ItemCode"].ToString());

               
                    item.Selected = true;
               
                txtGLDate.Text = ds.Tables[0].Rows[0]["GLDate"].ToString();

                if (ddlDepartment.Items.FindByValue(ds.Tables[0].Rows[0]["DepartmentId"].ToString()) != null)
                {
                    ddlDepartment.Items.FindByValue(ds.Tables[0].Rows[0]["DepartmentId"].ToString()).Selected = true;
                }
                if (ds.Tables[0].Rows[0]["BPartnerId"].ToString() == "" || ds.Tables[0].Rows[0]["BPartnerId"].ToString() == null || ds.Tables[0].Rows[0]["BPartnerId"].ToString() == "0")
                {
                    ddlBPartner.SelectedIndex = 0;
                }
                else
                {
                    if (ddlBPartner.Items.FindByValue(ds.Tables[0].Rows[0]["BPartnerId"].ToString()) != null)
                    {
                        ddlBPartner.Items.FindByValue(ds.Tables[0].Rows[0]["BPartnerId"].ToString()).Selected = true;
                    }
                    
                }
                txtBillNo.Text = ds.Tables[0].Rows[0]["BillNo"].ToString();
                txtBillDate.Text = ds.Tables[0].Rows[0]["BillDate"].ToString();
                txtOpeningValue.Text = ds.Tables[0].Rows[0]["OpeningValue"].ToString();
                txtOpeningQuantity.Text = ds.Tables[0].Rows[0]["OpeningQuantity"].ToString();
                txtItemRate.Text = ds.Tables[0].Rows[0]["ItemRate"].ToString();
                txtExpiryDate.Text = ds.Tables[0].Rows[0]["ExpiryDate"].ToString();
                txtBatchNo.Text = ds.Tables[0].Rows[0]["BatchNo"].ToString();
                txtBalance_Quantity.Text = ds.Tables[0].Rows[0]["BalanceQuatity"].ToString();
                txtBalance_Value.Text = ds.Tables[0].Rows[0]["BalanceValue"].ToString();

                txtCreatedby.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdateBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());
                txtCreatedDate.Text = ds.Tables[0].Rows[0]["CreateDate"].ToString();
                
                txtUpdateDate.Text = ds.Tables[0].Rows[0]["UpdateDate"].ToString();

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                bool pur_sal = bool.Parse(ds.Tables[0].Rows[0]["Purchase_Sales"].ToString());
                if(pur_sal== true)
                {
                    rdbSale.Checked = true;
                    rdbPur.Checked = false;
                }
                if (pur_sal == false)
                {
                    rdbSale.Checked = false;
                    rdbPur.Checked = true;
                }
                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() == "")
                {
                    txtCreatedDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                    txtCreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdateDate"].ToString() == "")
                {
                    txtUpdateDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateDate"].ToString());
                    txtUpdateDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }

                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (txtGLDate.Text != "")
                {
                    dml.dateConvert(txtGLDate);
                    if (txtGLDate.Text == "01-Jan-2000")
                    {
                        txtGLDate.Text = "";
                    }
                }
                if (txtExpiryDate.Text != "")
                {
                    dml.dateConvert(txtExpiryDate);
                    if (txtExpiryDate.Text == "01-Jan-2000")
                    {
                        txtExpiryDate.Text = "";
                    }
                }
                if (txtBillDate.Text != "")
                {
                    dml.dateConvert(txtBillDate);
                    if (txtBillDate.Text == "01-Jan-2000")
                    {
                        txtBillDate.Text = "";
                    }
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
        uploadtable.Visible = false;
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
            DataSet ds = dml.Find("select * from SET_ItemMasterOpening where ItemMasterOpeningID = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlDepartment.ClearSelection();
                ddlBPartner.ClearSelection();
                RadComboItem_Code.ClearSelection();

                string str = ds.Tables[0].Rows[0]["ItemCode"].ToString();


                string where = "GocID = '" + gocid() + "' and Record_Deleted = '0'";

                cmb.serachcombo3(RadComboItem_Code, str, "ItemCode", "ItemID", "Description", "Local_Import", "SET_ItemMaster", where, "ItemCode");
                RadComboBoxItem item = RadComboItem_Code.FindItemByText(ds.Tables[0].Rows[0]["ItemCode"].ToString());
                item.Selected = true;
                txtGLDate.Text = ds.Tables[0].Rows[0]["GLDate"].ToString();
                ddlDepartment.Items.FindByValue(ds.Tables[0].Rows[0]["DepartmentId"].ToString()).Selected = true;
                if (ds.Tables[0].Rows[0]["BPartnerId"].ToString() == "" || ds.Tables[0].Rows[0]["BPartnerId"].ToString() == null || ds.Tables[0].Rows[0]["BPartnerId"].ToString() == "0")
                {
                    ddlBPartner.SelectedIndex = 0;
                }
                else
                {
                    ddlBPartner.Items.FindByValue(ds.Tables[0].Rows[0]["BPartnerId"].ToString()).Selected = true;
                }
                txtBillNo.Text = ds.Tables[0].Rows[0]["BillNo"].ToString();
                txtBillDate.Text = ds.Tables[0].Rows[0]["BillDate"].ToString();
                txtOpeningValue.Text = ds.Tables[0].Rows[0]["OpeningValue"].ToString();
                txtOpeningQuantity.Text = ds.Tables[0].Rows[0]["OpeningQuantity"].ToString();
                txtItemRate.Text = ds.Tables[0].Rows[0]["ItemRate"].ToString();
                txtExpiryDate.Text = ds.Tables[0].Rows[0]["ExpiryDate"].ToString();
                txtBatchNo.Text = ds.Tables[0].Rows[0]["BatchNo"].ToString();
                txtBalance_Quantity.Text = ds.Tables[0].Rows[0]["BalanceQuatity"].ToString();
                txtBalance_Value.Text = ds.Tables[0].Rows[0]["BalanceValue"].ToString();

                txtCreatedby.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdateBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());
                txtCreatedDate.Text = ds.Tables[0].Rows[0]["CreateDate"].ToString();
                
                txtUpdateDate.Text = ds.Tables[0].Rows[0]["UpdateDate"].ToString();

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                bool pur_sal = bool.Parse(ds.Tables[0].Rows[0]["Purchase_Sales"].ToString());
                if (pur_sal == true)
                {
                    rdbSale.Checked = true;
                    rdbPur.Checked = false;
                }
                if (pur_sal == false)
                {
                    rdbSale.Checked = false;
                    rdbPur.Checked = true;
                }
                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() == "")
                {
                    txtCreatedDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                    txtCreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdateDate"].ToString() == "")
                {
                    txtUpdateDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateDate"].ToString());
                    txtUpdateDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }

                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }

                if (txtGLDate.Text != "")
                {
                    dml.dateConvert(txtGLDate);
                    if (txtGLDate.Text == "01-Jan-2000")
                    {
                        txtGLDate.Text = "";
                    }
                }
                if (txtExpiryDate.Text != "")
                {
                    dml.dateConvert(txtExpiryDate);
                    if (txtExpiryDate.Text == "01-Jan-2000")
                    {
                        txtExpiryDate.Text = "";
                    }
                }
                if (txtBillDate.Text != "")
                {
                    dml.dateConvert(txtBillDate);
                    if (txtBillDate.Text == "01-Jan-2000")
                    {
                        txtBillDate.Text = "";
                    }
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
        uploadtable.Visible = false;
        btnInsert.Visible = false;
        btnCancel.Visible = true;
        btnUpdate.Visible = true;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnFind.Visible = false;
        Label1.Text = "";

        RadComboItem_Code.Enabled = true;
        rdbPur.Enabled = true;
        rdbSale.Enabled = true;
        txtGLDate.Enabled = true;
        ddlDepartment.Enabled = true;
        ddlBPartner.Enabled = true;
        txtBillNo.Enabled = true;
        txtBillDate.Enabled = true;
        txtOpeningValue.Enabled = true;
        txtOpeningQuantity.Enabled = true;
        txtItemRate.Enabled = false;
        txtExpiryDate.Enabled = true;
        txtBatchNo.Enabled = true;
        txtBalance_Quantity.Enabled = false;
        txtBalance_Value.Enabled = false;
        chkActive.Enabled = true;
        txtCreatedby.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdateBy.Enabled = false;
        txtUpdateDate.Enabled = false;
        imgPopup.Enabled = true;
        imgPopup1.Enabled = true;
        imgPopup12.Enabled = true;
      

        updatecol.Visible = true;
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {
            string serial_id;
            serial_id = GridView3.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select * from SET_ItemMasterOpening where ItemMasterOpeningID = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlDepartment.ClearSelection();
                ddlBPartner.ClearSelection();
                RadComboItem_Code.ClearSelection();

                string str = ds.Tables[0].Rows[0]["ItemCode"].ToString();


                string where = "GocID = '" + gocid() + "' and Record_Deleted = '0'";

                cmb.serachcombo3(RadComboItem_Code, str, "ItemCode", "ItemID", "Description", "Local_Import", "SET_ItemMaster", where, "ItemCode");
                RadComboBoxItem item = RadComboItem_Code.FindItemByText(ds.Tables[0].Rows[0]["ItemCode"].ToString());
                item.Selected = true;
                txtGLDate.Text = ds.Tables[0].Rows[0]["GLDate"].ToString();
                ddlDepartment.Items.FindByValue(ds.Tables[0].Rows[0]["DepartmentId"].ToString()).Selected = true;
                if (ds.Tables[0].Rows[0]["BPartnerId"].ToString() == "" || ds.Tables[0].Rows[0]["BPartnerId"].ToString() == null || ds.Tables[0].Rows[0]["BPartnerId"].ToString() == "0")
                {
                    ddlBPartner.SelectedIndex = 0;
                }
                else
                {
                    ddlBPartner.Items.FindByValue(ds.Tables[0].Rows[0]["BPartnerId"].ToString()).Selected = true;
                }
                txtBillNo.Text = ds.Tables[0].Rows[0]["BillNo"].ToString();
                txtBillDate.Text = ds.Tables[0].Rows[0]["BillDate"].ToString();
                txtOpeningValue.Text = ds.Tables[0].Rows[0]["OpeningValue"].ToString();
                txtOpeningQuantity.Text = ds.Tables[0].Rows[0]["OpeningQuantity"].ToString();
                txtItemRate.Text = ds.Tables[0].Rows[0]["ItemRate"].ToString();
                txtExpiryDate.Text = ds.Tables[0].Rows[0]["ExpiryDate"].ToString();
                txtBatchNo.Text = ds.Tables[0].Rows[0]["BatchNo"].ToString();
                txtBalance_Quantity.Text = ds.Tables[0].Rows[0]["BalanceQuatity"].ToString();
                txtBalance_Value.Text = ds.Tables[0].Rows[0]["BalanceValue"].ToString();

                txtCreatedby.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdateBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());

                txtCreatedDate.Text = ds.Tables[0].Rows[0]["CreateDate"].ToString();
                txtUpdateDate.Text = ds.Tables[0].Rows[0]["UpdateDate"].ToString();

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                bool pur_sal = bool.Parse(ds.Tables[0].Rows[0]["Purchase_Sales"].ToString());
                if (pur_sal == true)
                {
                    rdbSale.Checked = true;
                    rdbPur.Checked = false;
                }
                if (pur_sal == false)
                {
                    rdbSale.Checked = false;
                    rdbPur.Checked = true;
                }
                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() == "")
                {
                    txtCreatedDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                    txtCreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdateDate"].ToString() == "")
                {
                    txtUpdateDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateDate"].ToString());
                    txtUpdateDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }

                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (txtGLDate.Text != "")
                {
                    dml.dateConvert(txtGLDate);
                    if (txtGLDate.Text == "01-Jan-2000")
                    {
                        txtGLDate.Text = "";
                    }
                }
                if (txtExpiryDate.Text != "")
                {
                    dml.dateConvert(txtExpiryDate);
                    if (txtExpiryDate.Text == "01-Jan-2000")
                    {
                        txtExpiryDate.Text = "";
                    }
                }
                if (txtBillDate.Text != "")
                {
                    dml.dateConvert(txtBillDate);
                    if (txtBillDate.Text == "01-Jan-2000")
                    {
                        txtBillDate.Text = "";
                    }
                }

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



    protected void btnNO_Click(object sender, EventArgs e)
    {

    }
    protected void btnYes_Click1(object sender, EventArgs e)
    {

    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        Panel3.Visible = false;
        if (FileUpload1.HasFile)
        {
            try
            {
                bool flag = false;
                //Upload and save the file
                // string filename = Path.GetFileName(FileUpload1.FileName);
                string excelPath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(excelPath);

                string conString = string.Empty;
                string extension = Path.GetExtension(FileUpload1.FileName);

                if (extension == ".xls")
                {
                    conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + excelPath + ";Extended Properties='Excel 8.0;HDR=YES;'";
                    flag = true;
                }
                else if (extension == ".xlsx")
                {
                    conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelPath + ";Extended Properties='Excel 8.0;HDR=YES'";
                    flag = true;
                }
                else
                {
                    Label1.Text = "This file is not be accepted. please input only excel file";
                }




                if (flag == true)
                {
                    using (OleDbConnection excel_con = new OleDbConnection(conString))
                    {
                        excel_con.Open();
                        string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                        DataTable dtExcelData = new DataTable();

                        //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                        dtExcelData.Columns.AddRange(new DataColumn[22] { new DataColumn("ItemCode", typeof(string)),
                            new DataColumn("ItemID", typeof(string)),
                new DataColumn("Purchase_Sales", typeof(bool)),
                new DataColumn("GLDate",typeof(string)) ,
                  new DataColumn("DepartmentId",typeof(string)),
                  new DataColumn("BPartnerId",typeof(string)),
                  new DataColumn("BillNo",typeof(string)),
                  new DataColumn("BillDate",typeof(string)),
                   new DataColumn("OpeningQuantity",typeof(string)),
                    new DataColumn("ItemRate",typeof(string)),
              new DataColumn("OpeningValue",typeof(string)),
               new DataColumn("ExpiryDate",typeof(string)),
                new DataColumn("BalanceQuatity",typeof(string)),
                 new DataColumn("BalanceValue",typeof(string)),
                  new DataColumn("IsActive",typeof(bool)),
                   new DataColumn("CreatedBy",typeof(string)),
                    new DataColumn("Record_Deleted",typeof(bool)),
                     new DataColumn("GocID",typeof(string)),
                      new DataColumn("CompId",typeof(string)),
                       new DataColumn("BranchId",typeof(string)),
                       new DataColumn("UploadButtonDis_Ena",typeof(bool)),
                new DataColumn("CreateDate",typeof(string))
//Acct_Type_ID UploadButtonDis_Ena

        });

                        int counter_index = 0;
                        string code = "";

                        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * , '" + gocid() + "' as GocID, '" + compid() + "' as CompId, '" + branchId() + "' as BranchId , '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' as CreateDate ,'" + show_username() + "' as CreatedBy, 'false' as Record_Deleted, 'true'as IsActive,'true' as UploadButtonDis_Ena FROM [" + sheet1 + "]", excel_con))
                        {
                            oda.Fill(dtExcelData);
                            //dtExcelData.Columns.Add("Acct_Type_ID", typeof(System.Int32));
                        }
                        excel_con.Close();

                        foreach (DataRow dtRow in dtExcelData.Rows)
                        {

                            string str = dtRow[0].ToString();
                            //dtRow["Acct_Type_ID"] = acct_type(str);
                            dtRow["ItemID"] = itemid(str);
                            if((string)dtRow["ExpiryDate"] ==  "" || dtRow["ExpiryDate"] == null)
                            {
                                dtRow["ExpiryDate"] = "1-jan-0001";
                            }
                            dtRow["BalanceQuatity"] = dtRow["OpeningQuantity"];
                            dtRow["BalanceValue"] = dtRow["OpeningValue"];


                            string consString1 = ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString;
                            SqlConnection con1 = new SqlConnection(consString1);

                            SqlDataAdapter da1 = new SqlDataAdapter("select * from SET_ItemMaster where ItemCode = '" + str + "'", con1);
                            DataSet dsacct_code = new DataSet();
                            da1.Fill(dsacct_code);
                            if (dsacct_code.Tables[0].Rows.Count > 0)
                            {
                                SqlDataAdapter da = new SqlDataAdapter("select * from SET_ItemMasterOpening where ItemCode = '" + str + "'", con1);
                                DataSet ds1 = new DataSet();
                                da.Fill(ds1);
                                if (ds1.Tables[0].Rows.Count > 0)
                                {
                                    string id = ds1.Tables[0].Rows[0]["ItemID"].ToString();
                                   
                                    //delete from SET_COA_Detail_Opening where GocID = '1' and CompId ='1' and BranchId = '5' and COA_Opening_ID = '158'
                                    dml.Update("Delete from SET_ItemMasterOpening where GocID = '" + gocid() + "' and CompId ='" + compid() + "' and BranchId = '" + branchId() + "' and ItemID = '" + id + "'; ", "");


                                }
                                counter_index = counter_index + 1;
                            }
                            else
                            {

                                dtExcelData.Rows[counter_index].Delete();
                                code = code + "," + str;
                                counter_index = counter_index + 1;
                            }
                        }

                        string consString = ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(consString))
                        {
                            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                            {
                                //Set the database table name
                                sqlBulkCopy.DestinationTableName = "dbo.SET_ItemMasterOpening";

                                //[OPTIONAL]: Map the Excel columns with that of the database table
                                sqlBulkCopy.ColumnMappings.Add("ItemCode", "ItemCode");
                                sqlBulkCopy.ColumnMappings.Add("ItemID", "ItemID");
                                sqlBulkCopy.ColumnMappings.Add("Purchase_Sales", "Purchase_Sales");
                                sqlBulkCopy.ColumnMappings.Add("GLDate", "GLDate");
                                sqlBulkCopy.ColumnMappings.Add("DepartmentId", "DepartmentId");
                                sqlBulkCopy.ColumnMappings.Add("BillNo", "BillNo");
                                sqlBulkCopy.ColumnMappings.Add("BillDate", "BillDate");
                                sqlBulkCopy.ColumnMappings.Add("OpeningQuantity", "OpeningQuantity");
                                sqlBulkCopy.ColumnMappings.Add("ItemRate", "ItemRate");
                                sqlBulkCopy.ColumnMappings.Add("OpeningValue", "OpeningValue");
                                sqlBulkCopy.ColumnMappings.Add("ExpiryDate", "ExpiryDate");
                                sqlBulkCopy.ColumnMappings.Add("BalanceQuatity", "BalanceQuatity");
                                sqlBulkCopy.ColumnMappings.Add("BalanceValue", "BalanceValue");
                                sqlBulkCopy.ColumnMappings.Add("IsActive", "IsActive");
                                sqlBulkCopy.ColumnMappings.Add("CreatedBy", "CreatedBy");
                                sqlBulkCopy.ColumnMappings.Add("Record_Deleted", "Record_Deleted");
                                sqlBulkCopy.ColumnMappings.Add("BPartnerId", "BPartnerId");
                                sqlBulkCopy.ColumnMappings.Add("GocID", "GocID");
                                sqlBulkCopy.ColumnMappings.Add("CompId", "CompId");
                                sqlBulkCopy.ColumnMappings.Add("BranchId", "BranchId");
                                sqlBulkCopy.ColumnMappings.Add("UploadButtonDis_Ena", "UploadButtonDis_Ena");
                                sqlBulkCopy.ColumnMappings.Add("CreateDate", "CreateDate");

                                //IsActive UploadButtonDis_Ena
                                con.Open();
                                sqlBulkCopy.WriteToServer(dtExcelData);
                                Panel3.Visible = true;
                                ModalPopupExtender3.Show();

                                // ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "fileupd()", true);

                                textClear();

                                btnInsert_Click(sender, e);

                                con.Close();
                            }
                        }
                        Label1.ForeColor = System.Drawing.Color.Green;
                        Label1.Text = "These Code doesn't match with Item code     " + code;
                    }

                }
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
        }
        else
        {
            Label1.Text = "Please Select the file";
        }
    }
    protected void btnYes_2_Click(object sender, EventArgs e)
    {
        try
        {

            dml.Update("update SET_ItemMasterOpening set UploadButtonDis_Ena = '0' where GocID = '" + gocid() + "' and CompId ='" + compid() + "' and BranchId = '" + branchId() + "'", "");
            FileUpload1.Enabled = false;
            Button1.Enabled = false;
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Show();
        // Panel3.Attributes.Add("", "");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            //RadComboAcct_Code.FindItemByText("1-01-01-0001").Selected = true;
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    protected void btnNo_2_Click(object sender, EventArgs e)
    {

    }
    public string itemid(string str)
    {
        string itemid = "";
        DataSet ds = dml.Find("select * from SET_ItemMaster where ItemCode = '" + str + "'");
        if(ds.Tables[0].Rows.Count > 0)
        {
            itemid = ds.Tables[0].Rows[0]["ItemID"].ToString();
           
        }
        return itemid;
    }



    protected void txtOpeningQuantity_TextChanged(object sender, EventArgs e)
    {
        txtBalance_Quantity.Text = txtOpeningQuantity.Text;
        decimal itemvalue;
        if (txtOpeningValue.Text == "")
        {
            itemvalue = 0;
        }
        else
        {
            itemvalue = decimal.Parse(txtOpeningValue.Text);
        }
        

        decimal itemq = decimal.Parse(txtOpeningQuantity.Text);

        decimal itemrate = itemvalue / itemq;
        txtItemRate.Text = itemrate.ToString("0.0000");
       

    }

    protected void txtOpeningValue_TextChanged(object sender, EventArgs e)
    {
       
        if(txtOpeningValue.Text == "")
        {
            txtOpeningValue.Text = "0";
        }
        if (txtOpeningQuantity.Text == "")
        {
            txtOpeningQuantity.Text = "0";
        }
        txtBalance_Value.Text = txtOpeningValue.Text;
        decimal itemvalue = decimal.Parse(txtOpeningValue.Text);
        decimal itemq = decimal.Parse(txtOpeningQuantity.Text);
        if (itemvalue > 0 && itemq > 0)
        {
            decimal itemrate = itemvalue / itemq;

            txtItemRate.Text = itemrate.ToString("0.0000");
        }
    }
    protected void RadComboItem_Code_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "GocID = '" + gocid() + "' and Record_Deleted = '0'";

        cmb.serachcombo3(RadComboItem_Code, e.Text, "ItemCode", "ItemID", "Description", "Local_Import", "SET_ItemMaster", where, "ItemCode");

    }
    protected void RadcmbItem_Code_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "GocID = '" + gocid() + "' and Record_Deleted = '0'";

        cmb.serachcombo3(RadcmbItem_Code, e.Text, "ItemCode", "ItemID", "Description", "Local_Import", "SET_ItemMaster", where, "ItemCode");

    }
    //radcmbFind_ItemCode_ItemsRequested
    protected void radcmbFind_ItemCode_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "GocID = '" + gocid() + "' and Record_Deleted = '0'";

        cmb.serachcombo3(radcmbFind_ItemCode, e.Text, "ItemCode", "ItemID", "Description", "Local_Import", "SET_ItemMaster", where, "ItemCode");

    }
    protected void radcmbDel_Item_Code_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "GocID = '" + gocid() + "' and Record_Deleted = '0'";

        cmb.serachcombo3(radcmbDel_Item_Code, e.Text, "ItemCode", "ItemID", "Description", "Local_Import", "SET_ItemMaster", where, "ItemCode");

    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string id = e.Row.Cells[1].Text;
        bool flag = dml.isNumber(id);


        if (flag == true)
        {

            DataSet ds = dml.Find("select ItemMasterOpeningID,MLD from SET_ItemMasterOpening where ItemMasterOpeningID = '" + id + "'");
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

            DataSet ds = dml.Find("select ItemMasterOpeningID,MLD from SET_ItemMasterOpening where ItemMasterOpeningID = '" + id + "'");
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