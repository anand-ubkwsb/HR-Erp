using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frm_Period : System.Web.UI.Page
{
    string userid, UserGrpID, FormID, fiscal;
    int DateFrom, AddDays, EditDays, DeleteDays;
    DmlOperation dml = new DmlOperation();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
    FieldHide fd = new FieldHide();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Page.IsPostBack == false)
        {

            updatecol.Visible = false;
            btnUpdate.Visible = false;
            btnSave.Visible = false;
            btnDelete_after.Visible = false;
            userid = Request.QueryString["UserID"];
            UserGrpID = Request.QueryString["UsergrpID"];
            FormID = Request.QueryString["FormID"];
            fiscal = Request.QueryString["fiscaly"];

            txtCostCntrName.Focus();
            fd.HideUSerGrpField(Page.Controls, UserGrpID, FormID, "SET_Department");
            dml.dropdownsql(ddlDeparment, "SET_Department", "DepartmentName", "DepartmentID");
            dml.dropdownsql(ddlHeadingCost, "SET_CostCenter", "CostCenterName", "CostCenterID");

            dml.dropdownsql(txtFind_CostCenterName, "SET_CostCenter", "CostCenterName", "CostCenterID");
            dml.dropdownsql(txtEdit_CostCenterName, "SET_CostCenter", "CostCenterName", "CostCenterID");
            dml.dropdownsql(txtdelete_CostCenterName, "SET_CostCenter", "CostCenterName", "CostCenterID");

            dml.dropdownsql(txtFind_HeadingCostCenterName, "SET_CostCenter", "HeadingCostCenterName", "CostCenterID");
            dml.dropdownsql(txtEdit_HeadingCostCenterName, "SET_CostCenter", "HeadingCostCenterName", "CostCenterID");
            dml.dropdownsql(txtdelete_HeadingCostCenterName, "SET_CostCenter", "HeadingCostCenterName", "CostCenterID");

            Showall_Dml();
            textClear();
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

        txtCostCntrName.Focus();

        txtCostCntrName.Enabled = true;
        ddlDeparment.Enabled = true;
        ddlHeadingCost.Enabled = true;
        txtSystemDate.Enabled = false;
        chkActive.Checked = true;
        chkActive.Enabled = true;
        txtCreatedBy.Enabled = false;
        txtSystemDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

        userid  =  Request.QueryString["UserID"];
        DataSet ds_autoUserName = dml.Find("select user_name from SET_User_Manager where UserId ='" + userid + "'");
        txtCreatedBy.Text = ds_autoUserName.Tables[0].Rows[0]["user_name"].ToString();

        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        userid = Request.QueryString["UserID"];
        UserGrpID = Request.QueryString["UsergrpID"];
        FormID = Request.QueryString["FormID"];
        fiscal = Request.QueryString["fiscaly"];
        try
        {
            int chk;
           
            if (chkActive.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
            dml.Insert("INSERT INTO SET_CostCenter([CostCenterName], [CreatedBy], [CreateDate], [IsActive], [HeadingCostCenterName], [DepartmentId],[Record_Deleted]) VALUES ( '"+txtCostCntrName.Text+"', '"+txtCreatedBy.Text+"', '"+DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")+"', '"+chk+"', '"+ddlHeadingCost.SelectedItem.Text+"', '"+ddlDeparment.SelectedItem.Value+"','0');", "alertme()");
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);

            Label1.Text = "";
            txtCostCntrName.Text = "";
            ddlDeparment.SelectedIndex = 0;
            ddlHeadingCost.SelectedIndex = 0;
            txtSystemDate.Text = "";
            txtCreatedBy.Text = "";
            FormID = Request.QueryString["FormID"];
            Showall_Dml();

            //var onBlurScript = Page.ClientScript.GetPostBackEventReference(txtCostCntrName, "OnFocus");
            //txtCostCntrName.Attributes.Add("onFocus", onBlurScript);
            //  btnInsert_Click(sender, e);
            //Response.Redirect("frm_Set_Department.aspx?UserID="+userid+"&UsergrpID="+UserGrpID+"&fiscaly="+fiscal+"&FormID="+FormID+"");
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
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


            DataSet ds_up = dml.Find("select * from SET_CostCenter  WHERE ([CostCenterID]='"+ViewState["SNO"].ToString() +"') AND ([CostCenterName]='"+txtCostCntrName.Text+"') AND ([IsActive]='"+chk+"') AND ([HeadingCostCenterName]='"+ddlHeadingCost.SelectedItem.Text+"') AND ([DepartmentId]='" + ddlDeparment.SelectedItem.Value +"');");

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
                FormID = Request.QueryString["FormID"];
                Showall_Dml();
            }
            else {
                userid = Request.QueryString["UserID"];

                dml.Update("UPDATE [SET_CostCenter] SET [CostCenterName]='" + txtCostCntrName.Text + "', [UpdatedBy]='" + show_username() + "', [UpdateDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', [IsActive]='" + chk + "', [HeadingCostCenterName]='" + ddlHeadingCost.SelectedItem.Text + "', [DepartmentId]='" + ddlDeparment.SelectedItem.Value + "' WHERE ([CostCenterID]='" + ViewState["SNO"].ToString() + "');", "");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", " Editalert()", true);

                textClear();
                btnInsert.Visible = true;
                btnDelete.Visible = true;
                btnUpdate.Visible = false;
                btnEdit.Visible = true;
                btnFind.Visible = true;
                btnSave.Visible = false;
                btnDelete_after.Visible = false;
                FormID = Request.QueryString["FormID"];
                Showall_Dml();
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
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
        FormID = Request.QueryString["FormID"];
        Showall_Dml();
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
            string swhere;
            string squer = "select * from SET_CostCenter";

            if (txtdelete_CostCenterName.SelectedIndex != 0)
            {
                swhere = "CostCenterName = '" + txtdelete_CostCenterName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "CostCenterName is not null";
            }
            if (txtdelete_HeadingCostCenterName.SelectedIndex != 0)
            {
                swhere = swhere + " and HeadingCostCenterName = '" + txtdelete_HeadingCostCenterName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and HeadingCostCenterName is not null";
            }


            if (chkdelete_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkdelete_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }
            else
            {
                swhere = swhere + " and IsActive is not null";
            }
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY CostCenterName";

            Findbox.Visible = false;
            fieldbox.Visible = false;
            Editbox.Visible = false;

            DataSet dgrid = dml.grid(squer);
            GridView2.DataSource = dgrid;
            GridView2.DataBind();
        }
        catch (Exception ex)
        {

        }

    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        txtCostCntrName.Enabled = false;
        ddlDeparment.Enabled = false;
        ddlHeadingCost.Enabled = false;
        txtSystemDate.Enabled = false;
        chkActive.Checked = false;
        chkActive.Enabled = false;
        txtCreatedBy.Enabled = false;


        btnCancel.Visible = true;
        btnFind.Visible = true;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnInsert.Visible = false;
        try {
            GridView1.DataBind();
            string swhere;
            string squer = "select * from SET_CostCenter";

            if (txtFind_CostCenterName.SelectedIndex != 0)
            {
                swhere = "CostCenterName = '" + txtFind_CostCenterName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "CostCenterName is not null";
            }
            if (txtFind_HeadingCostCenterName.SelectedIndex != 0)
            {
                swhere = swhere + " and HeadingCostCenterName = '" + txtFind_HeadingCostCenterName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and HeadingCostCenterName is not null";
            }


            if (chkFind_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkFind_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }
            else
            {
                swhere = swhere + " and IsActive is not null";
            }
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY CostCenterName";

            Findbox.Visible = true;
            fieldbox.Visible = false;

            DataSet dgrid = dml.grid(squer);
            GridView1.DataSource = dgrid;
            GridView1.DataBind();
        }
        catch(Exception ex)
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
            string swhere;
            string squer = "select * from SET_CostCenter";

            if (txtEdit_CostCenterName.SelectedIndex != 0)
            {
                swhere = "CostCenterName = '" + txtEdit_CostCenterName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "CostCenterName is not null";
            }
            if (txtEdit_HeadingCostCenterName.SelectedIndex != 0)
            {
                swhere = swhere + " and HeadingCostCenterName = '" + txtEdit_HeadingCostCenterName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and HeadingCostCenterName is not null";
            }
           

            if (chkEdit_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkEdit_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }
            else
            {
                swhere = swhere + " and IsActive is not null";
            }
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY CostCenterName";

            Findbox.Visible = false;
            fieldbox.Visible = false;
            DeleteBox.Visible = false;

            DataSet dgrid = dml.grid(squer);
            if (dgrid.Tables[0].Rows.Count > 0)
            {
                GridView3.DataSource = dgrid;
                GridView3.DataBind();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", " nodatafound()", true);
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", " wrong()", true);
        }
    }
    public void textClear()
        {
        DeleteBox.Visible = false;
        Editbox.Visible = false;
        Findbox.Visible = false;
        fieldbox.Visible = true;
        updatecol.Visible = false;
        txtCostCntrName.Enabled = false;
        ddlDeparment.Enabled = false;
        ddlHeadingCost.Enabled = false;
        txtSystemDate.Enabled = false;
        chkActive.Checked = false;
        chkActive.Enabled = false;
        txtCreatedBy.Enabled = false;

        Label1.Text = "";
        txtCostCntrName.Text = "";
        ddlDeparment.SelectedIndex = 0;
        ddlHeadingCost.SelectedIndex = 0;
        txtSystemDate.Text = "";
        txtCreatedBy.Text = "";

    }
    public void Showall_Dml()
    {
        userid = Request.QueryString["UserID"];
        FormID = Request.QueryString["FormID"];
        DataSet FiscalStatus;
        con.Open();
        string Query = "SELECT * FROM SET_FISCAL_YEAR WHERE FISCALYEARID=" + Convert.ToInt32(Request.Cookies["FiscalYearId"].Value);
        DataSet Fiscal = dml.Find(Query);
        string FiscalStatusQuery = "SELECT * FROM SET_FISCAL_YEAR_STATUS WHERE FISCALYEARSTATUSID=(SELECT FISCALYEARSTATUSID FROM SET_FISCAL_YEAR WHERE FISCALYEARID=" + Convert.ToInt32(Request.Cookies["FiscalYearId"].Value)+ ") AND RECORD_DELETED='0'";
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
            dml.Delete("update SET_CostCenter set Record_Deleted = 1 where CostCenterID = " + ViewState["SNO"].ToString() + "", "");
            textClear();
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "alertDelete()", true);

            btnInsert.Visible = true;
            btnDelete.Visible = true;
            btnUpdate.Visible = false;
            btnEdit.Visible = true;
            btnFind.Visible = true;
            btnSave.Visible = false;
            btnDelete_after.Visible = false;

            FormID = Request.QueryString["FormID"];
            Showall_Dml();

        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
      {
        txtCostCntrName.Enabled = false;
        txtCreatedBy.Enabled = false;
        txtSystemDate.Enabled = false;
        chkActive.Enabled = false;
        ddlDeparment.Enabled = false;
        ddlHeadingCost.Enabled = false;
        txtUpdatedBy.Enabled = false;
        txtUpadtedDate.Enabled = false;

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
            ddlDeparment.ClearSelection();
            ddlHeadingCost.ClearSelection();

            string serial_id;
            serial_id = GridView1.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select  * from  SET_CostCenter where CostCenterID = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtCostCntrName.Text = ds.Tables[0].Rows[0]["CostCenterName"].ToString();
                
                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() == "")
                {
                    txtSystemDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                    txtSystemDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdatedBy"].ToString() == "")
                {
                    txtUpadtedDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateDate"].ToString());
                    txtUpadtedDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                ddlDeparment.Items.FindByValue(ds.Tables[0].Rows[0]["DepartmentId"].ToString()).Selected = true;
                string sssss = ds.Tables[0].Rows[0]["HeadingCostCenterName"].ToString();
                if (ds.Tables[0].Rows[0]["HeadingCostCenterName"].ToString() == "")
                {
                    ddlHeadingCost.Items.FindByText(ds.Tables[0].Rows[0]["HeadingCostCenterName"].ToString()).Selected = true;
                }
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                txtCreatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());

                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
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
        btnInsert.Visible = false;
        btnCancel.Visible = true;
        btnUpdate.Visible = false;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnFind.Visible = false;
        btnDelete_after.Visible = true;
        txtUpdatedBy.Enabled = false;
        txtUpadtedDate.Enabled = false;
        textClear();

        updatecol.Visible = true;
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        DeleteBox.Visible = false;
        try
        {
            ddlDeparment.ClearSelection();
            ddlHeadingCost.ClearSelection();

            string serial_id;
            serial_id = GridView2.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select  * from  SET_CostCenter where CostCenterID = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtCostCntrName.Text = ds.Tables[0].Rows[0]["CostCenterName"].ToString();
                
                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() == "")
                {
                    txtSystemDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                    txtSystemDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdatedBy"].ToString() == "")
                {
                    txtUpadtedDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateDate"].ToString());
                    txtUpadtedDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                ddlDeparment.Items.FindByValue(ds.Tables[0].Rows[0]["DepartmentId"].ToString()).Selected = true;
                if (ds.Tables[0].Rows[0]["HeadingCostCenterName"].ToString() != "")
                {
                    ddlHeadingCost.Items.FindByText(ds.Tables[0].Rows[0]["HeadingCostCenterName"].ToString()).Selected = true;
                }
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                txtCreatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());

                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
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

        txtCostCntrName.Enabled = true;
        txtCreatedBy.Enabled = false;
        txtSystemDate.Enabled = false;
        chkActive.Enabled = true;
        ddlDeparment.Enabled = true;
        ddlHeadingCost.Enabled = true;
        txtUpadtedDate.Enabled = false;
        txtUpdatedBy.Enabled = false;

        updatecol.Visible = true;
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {
            ddlDeparment.ClearSelection();
            ddlHeadingCost.ClearSelection();
            string serial_id;
            serial_id = GridView3.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select  * from  SET_CostCenter where CostCenterID = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtCostCntrName.Text = ds.Tables[0].Rows[0]["CostCenterName"].ToString();
                
                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() == "")
                {
                    txtSystemDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                    txtSystemDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdatedBy"].ToString() == "")
                {
                    txtUpadtedDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateDate"].ToString());
                    txtUpadtedDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                ddlDeparment.Items.FindByValue(ds.Tables[0].Rows[0]["DepartmentId"].ToString()).Selected = true;
                if (ds.Tables[0].Rows[0]["HeadingCostCenterName"].ToString() != "")
                {
                    ddlHeadingCost.Items.FindByText(ds.Tables[0].Rows[0]["HeadingCostCenterName"].ToString()).Selected = true;
                }
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                txtCreatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());

                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
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
        DataSet ds_autoUserName = dml.Find("select user_name from SET_User_Manager where UserId='" + userid + "'");
        return ds_autoUserName.Tables[0].Rows[0]["user_name"].ToString();
    }

   

}