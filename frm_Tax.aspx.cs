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
using Telerik.Web.UI;

public partial class frm_Period : System.Web.UI.Page
{
    int DateFrom, AddDays, EditDays, DeleteDays;
    string userid, UserGrpID, FormID, fiscal;
    DmlOperation dml = new DmlOperation();
    radcomboxclass cmb = new radcomboxclass();
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

            txtTaxDesc.Focus();
            fd.HideUSerGrpField(Page.Controls, UserGrpID, FormID, "SET_Department");

            dml.dropdownsql(txtEdit_TaxDescription, "SET_Tax", "TaxDescription", "TaxTypeID");
            dml.dropdownsql(txtFind_TaxDescription, "SET_Tax", "TaxDescription", "TaxTypeID");
            dml.dropdownsql(txtdelete_tax, "SET_Tax", "TaxDescription", "TaxTypeID");

            dml.dropdownsql(txtEdit_TaxType, "SET_Tax", "TaxType", "TaxTypeID");
            dml.dropdownsql(txtFind_TaxType, "SET_Tax", "TaxType", "TaxTypeID");
            dml.dropdownsql(txtdelete_TaxType, "SET_Tax", "TaxType", "TaxTypeID");


            //dml.dropdownsql(ddlAccountCode, "SET_COA_detail", "Acct_Code", "COA_D_ID");
            txtApplydate.Attributes.Add("readonly", "readonly");

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

        txtTaxDesc.Focus();
        imgPopup.Enabled = true;
        txtTaxDesc.Enabled = true;
        txtPercentage.Enabled = true;
        txtTaxType.Enabled = true;
        RadComboAcct_Code.Enabled = true;
        txtApplydate.Enabled = true;
        txtCreatedBy.Enabled = false;
        txtCreatedDate.Enabled = false;

        chkActive.Enabled = true;
        chkActive.Checked = true;
        
        txtCreatedDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

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
            if (RadComboAcct_Code.Text != "")
            {

                dml.Insert("INSERT INTO [SET_Tax] ([TaxDescription], [Percentage], [TaxType], [ApplyDate],[AccountCode], [Status], [IsActive], [CreatedBy], [CreateDate],[Record_Deleted]) VALUES ('" + txtTaxDesc.Text + "', '" + txtPercentage.Text + "', '" + txtTaxType.Text + "', '" + dml.dateconvertforinsert(txtApplydate) + "', '" + RadComboAcct_Code.Text + "', '1', '" + chk + "', '" + txtCreatedBy.Text + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' , '0');", "alertme()");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);

                Label1.Text = "";
                txtTaxDesc.Text = "";
                txtPercentage.Text = "";
                txtTaxType.Text = "";
                RadComboAcct_Code.Text = "";
                txtApplydate.Text = "";
                txtCreatedBy.Text = "";
                txtCreatedDate.Text = "";

                btnInsert_Click(sender, e);
                UserGrpID = Request.QueryString["UsergrpID"];
                FormID = Request.QueryString["FormID"];
                Showall_Dml();
                //Response.Redirect("frm_Set_Department.aspx?UserID="+userid+"&UsergrpID="+UserGrpID+"&fiscaly="+fiscal+"&FormID="+FormID+"");
            }
            else
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "Please Select Account Code";
            }
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
            string date = txtApplydate.Text;
            int chk = 0;
            if (chkActive.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }


            DataSet ds_up = dml.Find("select * from SET_Tax WHERE ([TaxTypeID]='" + ViewState["SNO"].ToString() +"') AND ([TaxDescription]='"+txtTaxDesc.Text+"') AND ([Percentage]='"+txtPercentage.Text+"') AND ([TaxType]='"+txtTaxType.Text+"') AND ([ApplyDate]='"+dml.dateconvertforinsert(txtApplydate)+"') AND ([AccountCode]='"+ RadComboAcct_Code.Text+"') AND ([IsActive]='"+chk+"')");

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
                UserGrpID = Request.QueryString["UsergrpID"];
                FormID = Request.QueryString["FormID"];
                Showall_Dml();
            }
            else {
                userid = Request.QueryString["UserID"];

                dml.Update("UPDATE [SET_Tax] SET  [TaxDescription]='" + txtTaxDesc.Text + "', [Percentage]='" + txtPercentage.Text + "', [TaxType]='" + txtTaxType.Text + "', [ApplyDate]='" + dml.dateconvertString(date) + "', [AccountCode]='" + RadComboAcct_Code.Text + "', [Status]='1', [IsActive]='" + chk + "',[UpdatedBy]='" + txtUpdatedBy.Text + "', [UpdateDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' WHERE ([TaxTypeID]='" + ViewState["SNO"].ToString() + "');", "");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", " Editalert()", true);

                textClear();
                btnInsert.Visible = true;
                btnDelete.Visible = true;
                btnUpdate.Visible = false;
                btnEdit.Visible = true;
                btnFind.Visible = true;
                btnSave.Visible = false;
                btnDelete_after.Visible = false;
                UserGrpID = Request.QueryString["UsergrpID"];
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
        textClear();
        btnInsert.Visible = true;
        updatecol.Visible = false;
        btnDelete.Visible = true;
        btnUpdate.Visible = false;
        btnEdit.Visible = true;
        btnFind.Visible = true;
        btnSave.Visible = false;
        btnDelete_after.Visible = false;
        UserGrpID = Request.QueryString["UsergrpID"];
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
            string squer = "Select * from SET_Tax";

            if (txtdelete_tax.SelectedIndex != 0)
            {
                swhere = "TaxDescription = '" + txtdelete_tax.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "TaxDescription is not null";
            }
            if (txtdelete_TaxType.SelectedIndex != 0)
            {
                swhere = swhere + " and TaxType = '" + txtdelete_TaxType.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and TaxType is not null";
            }
            if (txtdelete_AccountCode.Text != "")
            {
                swhere = swhere + " and AccountCode like '" + txtdelete_AccountCode.Text + "%'";
            }
            else
            {
                swhere = swhere + " and AccountCode is not null";
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
            fiscal = Request.QueryString["fiscaly"];
            string stdate = dml.daterangefiscal_start(fiscal);
            string enddate = dml.daterangefiscal_end(fiscal);
            squer = squer + " where " + swhere + " and Record_Deleted = 0   ORDER BY TaxDescription";


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
        txtTaxDesc.Enabled = false;
        txtPercentage.Enabled = false;
        txtTaxType.Enabled = false;
        RadComboAcct_Code.Enabled = false;
        txtApplydate.Enabled = false;
        txtCreatedBy.Enabled = false;
        txtCreatedDate.Enabled = false;


        btnCancel.Visible = true;
        btnFind.Visible = true;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnInsert.Visible = false;

        GridView1.DataBind();
        try {
            string swhere;
            string squer = "Select * from SET_Tax";

            if (txtFind_TaxDescription.SelectedIndex != 0)
            {
                swhere = "TaxDescription = '" + txtFind_TaxDescription.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "TaxDescription is not null";
            }
            if (txtFind_TaxType.SelectedIndex != 0)
            {
                swhere = swhere + " and TaxType = '" + txtFind_TaxType.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and TaxType is not null";
            }
            if (txtFind_AccountCode.Text != "")
            {
                swhere = swhere + " and AccountCode like '" + txtFind_AccountCode.Text + "%'";
            }
            else
            {
                swhere = swhere + " and AccountCode is not null";
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

            fiscal = Request.QueryString["fiscaly"];
            string stdate = dml.daterangefiscal_start(fiscal);
            string enddate = dml.daterangefiscal_end(fiscal);
            squer = squer + " where " + swhere + " and Record_Deleted = 0  ORDER BY TaxDescription";

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
            string squer = "Select * from SET_Tax";

            if (txtEdit_TaxDescription.SelectedIndex != 0)
            {
                swhere = "TaxDescription = '" + txtEdit_TaxDescription.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "TaxDescription is not null";
            }
            if (txtEdit_TaxType.SelectedIndex != 0)
            {
                swhere = swhere + " and TaxType = '" + txtEdit_TaxType.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and TaxType is not null";
            }
            if (txtEdit_AccountCode.Text != "")
            {
                swhere = swhere + " and AccountCode like '" + txtEdit_AccountCode.Text + "%'";
            }
            else
            {
                swhere = swhere + " and AccountCode is not null";
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

            fiscal = Request.QueryString["fiscaly"];
            string stdate = dml.daterangefiscal_start(fiscal);
            string enddate = dml.daterangefiscal_end(fiscal);
            squer = squer + " where " + swhere + " and Record_Deleted = 0  ORDER BY TaxDescription";


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

        txtTaxDesc.Enabled = false;
        txtPercentage.Enabled = false;
        txtTaxType.Enabled = false;
        RadComboAcct_Code.Enabled = false;
        txtApplydate.Enabled = false;
        txtCreatedBy.Enabled = false;
        txtCreatedDate.Enabled = false;
        chkActive.Enabled = false;
        chkActive.Checked = false;
        imgPopup.Enabled = false;

        Label1.Text = "";
        txtTaxDesc.Text = "";
        txtPercentage.Text = "";
        txtTaxType.Text = "";
        RadComboAcct_Code.Text = "";
        txtApplydate.Text = "";
        txtCreatedBy.Text = "";
        txtCreatedDate.Text = "";
    }
    public void Showall_Dml()
    {
        userid = Request.QueryString["UserID"];
        FormID = Request.QueryString["FormID"];
        DataSet FiscalStatus;
        con.Open();
        string Query = "SELECT * FROM SET_FISCAL_YEAR WHERE FISCALYEARID=" + Convert.ToInt32(Request.Cookies["FiscalYearId"].Value);
        DataSet Fiscal = dml.Find(Query);
        string FiscalStatusQuery = "SELECT * FROM SET_FISCAL_YEAR_STATUS WHERE FISCALYEARSTATUSID=(SELECT FISCALYEARSTATUSID FROM SET_FISCAL_YEAR WHERE FISCALYEARID=" + Convert.ToInt32(Request.Cookies["FiscalYearId"].Value) + ") AND RECORD_DELETED='0'";
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

        //DataSet ds = dml.Find("select * from SET_UserGrp_Form where FormId='" + FormID + "' and DmlAllowed = 'Y' and UserGrpId = '" + UserGrpID + "' and Record_Deleted = '0'");
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
        //    btnCancel.Visible = false;
        //}
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            dml.Delete("update Set_Tax set Record_Deleted = 1 where TaxTypeID = " + ViewState["SNO"].ToString() + "", "");
            textClear();
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "alertDelete()", true);

            btnInsert.Visible = true;
            btnDelete.Visible = true;
            btnUpdate.Visible = false;
            btnEdit.Visible = true;
            btnFind.Visible = true;
            btnSave.Visible = false;
            btnDelete_after.Visible = false;
            UserGrpID = Request.QueryString["UsergrpID"];
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
        txtTaxDesc.Enabled = false;
        txtPercentage.Enabled = false;
        txtTaxType.Enabled = false;
        RadComboAcct_Code.Enabled = false;
        txtApplydate.Enabled = false;
        txtCreatedBy.Enabled = false;
        txtCreatedDate.Enabled = false;
        updatecol.Visible = true;
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
            DataSet ds = dml.Find("select * from SET_Tax where TaxTypeID = '" + ViewState["SNO"].ToString() +"' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                RadComboAcct_Code.ClearSelection();
                txtTaxDesc.Text = ds.Tables[0].Rows[0]["TaxDescription"].ToString();
                txtPercentage.Text = ds.Tables[0].Rows[0]["Percentage"].ToString();
                txtTaxType.Text = ds.Tables[0].Rows[0]["TaxType"].ToString();
                valuegetcombobox(ds, "AccountCode", RadComboAcct_Code);
                //RadComboAcct_Code.Items.FindByValue(ds.Tables[0].Rows[0]["AccountCode"].ToString()).Selected = true;

                txtApplydate.Text = ds.Tables[0].Rows[0]["ApplyDate"].ToString();
                
                txtCreatedDate.Text = ds.Tables[0].Rows[0]["CreateDate"].ToString();

                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() == "")
                {
                    txtCreatedDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                    txtCreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdatedBy"].ToString() == "")
                {
                    txtUpadtedDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateDate"].ToString());
                    txtUpadtedDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
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
                dml.dateConvert(txtApplydate);
                DataSet ds1 = dml.Find("select  Acct_Code,Acct_Description from SET_COA_detail where  Head_detail_ID = 'd1' and Record_Deleted = 0  and Acct_Code = '" + ds.Tables[0].Rows[0]["AccountCode"].ToString() + "'");
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    RadComboAcct_Code.ToolTip = ds1.Tables[0].Rows[0]["Acct_Description"].ToString();
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
        textClear();
        updatecol.Visible = true;
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        DeleteBox.Visible = false;
        try
        {
            RadComboAcct_Code.ClearSelection();
            string serial_id;
            serial_id = GridView2.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select * from SET_Tax where TaxTypeID = '" + ViewState["SNO"].ToString() + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtTaxDesc.Text = ds.Tables[0].Rows[0]["TaxDescription"].ToString();
                txtPercentage.Text = ds.Tables[0].Rows[0]["Percentage"].ToString();
                txtTaxType.Text = ds.Tables[0].Rows[0]["TaxType"].ToString();
                valuegetcombobox(ds, "AccountCode", RadComboAcct_Code);
                txtApplydate.Text = ds.Tables[0].Rows[0]["ApplyDate"].ToString();
                
                txtCreatedDate.Text = ds.Tables[0].Rows[0]["CreateDate"].ToString();

                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() == "")
                {
                    txtCreatedDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                    txtCreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdatedBy"].ToString() == "")
                {
                    txtUpadtedDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateDate"].ToString());
                    txtUpadtedDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
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
                dml.dateConvert(txtApplydate);
                DataSet ds1 = dml.Find("select  Acct_Code,Acct_Description from SET_COA_detail where  Head_detail_ID = 'd1' and Record_Deleted = 0  and Acct_Code = '" + ds.Tables[0].Rows[0]["AccountCode"].ToString() + "'");
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    RadComboAcct_Code.ToolTip = ds1.Tables[0].Rows[0]["Acct_Description"].ToString();
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

        txtTaxDesc.Enabled = true;
        txtPercentage.Enabled = true;
        txtTaxType.Enabled = true;
        RadComboAcct_Code.Enabled = true;
        txtApplydate.Enabled = true;
        txtCreatedBy.Enabled = false;
        txtCreatedDate.Enabled = false;
        imgPopup.Enabled = true;

        txtUpadtedDate.Enabled = false;
        txtUpdatedBy.Enabled = false;

        updatecol.Visible = true;
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {
            RadComboAcct_Code.ClearSelection();
            string serial_id;
            serial_id = GridView3.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select * from SET_Tax where TaxTypeID = '" + ViewState["SNO"].ToString() + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtTaxDesc.Text = ds.Tables[0].Rows[0]["TaxDescription"].ToString();
                txtPercentage.Text = ds.Tables[0].Rows[0]["Percentage"].ToString();
                txtTaxType.Text = ds.Tables[0].Rows[0]["TaxType"].ToString();
                valuegetcombobox(ds, "AccountCode", RadComboAcct_Code);
                txtApplydate.Text = ds.Tables[0].Rows[0]["ApplyDate"].ToString();
                
                txtCreatedDate.Text = ds.Tables[0].Rows[0]["CreateDate"].ToString();

                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() == "")
                {
                    txtCreatedDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                    txtCreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdatedBy"].ToString() == "")
                {
                    txtUpadtedDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateDate"].ToString());
                    txtUpadtedDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                txtCreatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdatedBy.Text =dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());
                txtUpadtedDate.Text = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss.ffff");
               // txtUpdatedBy.Text = show_username();
                chkActive.Enabled = true;
                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                dml.dateConvert(txtApplydate);
                DataSet ds1 = dml.Find("select  Acct_Code,Acct_Description from SET_COA_detail where  Head_detail_ID = 'd1' and Record_Deleted = 0  and Acct_Code = '" + ds.Tables[0].Rows[0]["AccountCode"].ToString() +"'");
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    RadComboAcct_Code.ToolTip = ds1.Tables[0].Rows[0]["Acct_Description"].ToString();
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

    protected void RadComboAcct_Code_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(RadComboAcct_Code, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
    protected void RadComboAcct_Code_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        RadComboAcct_Code.ToolTip = RadComboAcct_Code.SelectedValue.Split(new char[] { ':' })[1];
    }
    public void valuegetcombobox(DataSet dataset, string colname, RadComboBox rad)
    {
        string str = dataset.Tables[0].Rows[0][colname].ToString();
        if (str != "")
        {
            string where = "Record_Deleted = '0'";

            rad.Text = str;
         //   cmb.serachcombo4(rad, str, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");
       //     rad.FindItemByText(dataset.Tables[0].Rows[0][colname].ToString()).Selected = true;
        }
        else
        {
            rad.Text = "";
        }

    }

    protected void RadComboBoxProduct_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(txtEdit_AccountCode, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
    protected void txtFind_AccountCode_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(txtFind_AccountCode, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
    protected void txtdelete_AccountCode_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(txtdelete_AccountCode, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
}