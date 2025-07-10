
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
    int EditDays, DeleteDays, AddDays, DateFrom;
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


            dml.daterangeforfiscal(CalendarExtender1, Request.Cookies["fiscalyear"].Value);
            txtGLDate.Attributes.Add("readonly", "readonly");

            // dml.dropdownsql(ddlAcct_Code, "SET_COA_detail", "Acct_Code", "COA_D_ID", "Head_detail_ID", "D1");
            //select Acct_Type_Id,Acct_Type_Name,Tran_Type from SET_Acct_Type
            dml.dropdownsql(ddlAccountType, "SET_Acct_Type", "Acct_Type_Name", "Acct_Type_Id");
            // dml.dropdownsql(ddlFind_AcctCode, "SET_COA_detail", "Acct_Code", "COA_D_ID", "Head_detail_ID", "D1");
            //dml.dropdownsql(ddlEdit_AcctCode, "SET_COA_detail", "Acct_Code", "COA_D_ID", "Head_detail_ID", "D1");
            //  dml.dropdownsql(ddlDel_AcctCode, "SET_COA_detail", "Acct_Code", "COA_D_ID", "Head_detail_ID", "D1");
            CalendarExtender1.EndDate = DateTime.Now;
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

        RadComboAcct_Code.Enabled = true;
        txtOpeningValue.Enabled = true;
        imgPopup.Enabled = true;
        txtGLDate.Enabled = true;
        ddlAccountType.Enabled = false;
        ddlTranType.Enabled = true;
        chkActive.Enabled = true;

        chkActive.Checked = true;
        Button1.Enabled = true;
        FileUpload1.Enabled = true;
        //dml.dropToolTip(ddlAcct_Code);
        uploadtable.Visible = true;
        txtCreatedDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

        DataSet dataset = dml.Find("select UploadButtonDis_Ena from SET_COA_Detail_Opening where GocID='" + gocid() + "'and CompId='" + compid() + "' and BranchId='" + branchId() + "';");
        if (dataset.Tables[0].Rows.Count > 0)
        {
            string str_val = dataset.Tables[0].Rows[0]["UploadButtonDis_Ena"].ToString();
            if (str_val == "False")
            {
                FileUpload1.Enabled = false;
                Button1.Enabled = false;
            }
        }
        userid = Request.QueryString["UserID"];
        DataSet ds_autoUserName = dml.Find("select user_name from SET_User_Manager where UserId ='" + userid + "'");
        txtCreatedby.Text = ds_autoUserName.Tables[0].Rows[0]["user_name"].ToString();

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            try
            {
                if (RadComboAcct_Code.SelectedIndex != 0)
                {
                    if (txtGLDate.Text != "")
                    {
                        if (ddlTranType.SelectedIndex != 0)
                        {
                            DataSet uniqueg_B_C = dml.Find("select * from SET_COA_Detail_Opening where compid='" + compid() + "' and BranchId='" + branchId() + "' and GocID = '" + gocid() + "'  and Acct_Code = '" + RadComboAcct_Code.Text + "'");
                            if (uniqueg_B_C.Tables[0].Rows.Count > 0)
                            {
                                Label1.ForeColor = System.Drawing.Color.Red;
                                Label1.Text = "Duplicated entry not allowed";
                            }
                            else {


                                int chk = 0;
                                if (chkActive.Checked == true)
                                {
                                    chk = 1;
                                }

                                dml.Insert("INSERT INTO [SET_COA_Detail_Opening] ([Acct_Code], [GocID], [GLDate], [CompId], [BranchId], [FiscalYearID], [OpeningValue], [Acct_Type_ID], [Tran_Type], [IsActive], [CreatedBy], [CreateDate],[Record_Deleted],[UploadButtonDis_Ena]) VALUES ('" + RadComboAcct_Code.Text + "', " + gocid() + ", '" + dml.dateconvertforinsert(txtGLDate) + "'," + compid() + "," + branchId() + "," + FiscalYear() + ", '" + txtOpeningValue.Text + "', '" + ddlAccountType.SelectedItem.Value + "', '" + ddlTranType.SelectedItem.Text + "', '" + chk + "', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0','0');", "alertme()");
                                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);
                                Label1.Text = "";
                                txtUpdateBy.Text = "";
                                txtUpdateDate.Text = "";

                                RadComboAcct_Code.SelectedIndex = 0;
                                txtOpeningValue.Text = "";
                                txtGLDate.Text = "";
                                ddlAccountType.SelectedIndex = 0;
                                ddlTranType.SelectedIndex = 0;

                            }
                        }
                        else
                        {
                            Label1.Text = "Please Select Transaction Type";
                        }
                    }
                    else
                    {
                        Label1.Text = "Please Input GL Date";
                    }
                }
                else
                {
                    Label1.Text = "Please Select Account Code";
                }
            }

            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
        }
        FormID = Request.QueryString["FormID"];
        Showall_Dml();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            //
            string gldate = txtGLDate.Text;
            int chk = 0;
            if (chkActive.Checked == true)
            {
                chk = 1;
            }

            userid = Request.QueryString["UserID"];
            DataSet ds_up = dml.Find("select  * from SET_COA_Detail_Opening  WHERE ([COA_Opening_ID]='"+ViewState["SNO"].ToString()+"') AND ([Acct_Code]='"+RadComboAcct_Code.Text+ "') AND ([GLDate]='"+dml.dateconvertforinsert(txtGLDate)+"') AND ([OpeningValue]='" + txtOpeningValue.Text+"') AND ([Acct_Type_ID]='"+ddlAccountType.SelectedItem.Value+"') AND ([Tran_Type]='"+ddlTranType.SelectedItem.Text+"') AND ([IsActive]='"+chk+"') AND ([Record_Deleted]='0');");

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
                dml.Update("UPDATE [SET_COA_Detail_Opening] SET [Acct_Code]='" + RadComboAcct_Code.Text + "', [GocID]='" + gocid() + "', [GLDate]='" + gldate + "', [CompId]='" + compid() + "', [BranchId]='" + branchId() + "', [FiscalYearID]='" + FiscalYear() + "', [OpeningValue]='" + txtOpeningValue.Text + "', [Acct_Type_ID]='" + ddlAccountType.SelectedItem.Value + "', [Tran_Type]='" + ddlTranType.SelectedItem.Text + "', [IsActive]='" + chk + "', [UpdatedBy]='" + show_username() + "', [UpdateDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' WHERE ([COA_Opening_ID]='" + ViewState["SNO"].ToString() + "');", "");
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
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
        FormID = Request.QueryString["FormID"];
        Showall_Dml();
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
            string swhere;
            string squer = "select * from View_COA_Detail_opening";

            if (cmbFind_Accountcode.SelectedIndex != 0)
            {
                swhere = "Acct_Code like '" + cmbFind_Accountcode.Text + "%'";
            }
            else
            {
                swhere = "Acct_Code is not null";
            }
            if (txtFind_GlDate.Text != "")
            {
                swhere = swhere + " and GLDate = '" + txtFind_GlDate.Text + "'";
            }
            else
            {
                swhere = swhere + " and GLDate is not null";
            }

            if (txtFind_openValue.Text != "")
            {
                swhere = swhere + " and OpeningValue like '" + txtFind_openValue.Text + "%'";
            }
            else
            {
                swhere = swhere + " and OpeningValue is not null";
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
            squer = squer + " where " + swhere + " and  GocID = '" + gocid() + "' and CompId = '" + compid() + "' and BranchId = '" + branchId() + "' and Record_Deleted = '0' ORDER BY Acct_Description";

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
            string squer = "select * from View_COA_Detail_opening";

            if (cmbDel_AcctCode.SelectedIndex != 0)
            {
                swhere = "Acct_Code like '" + cmbDel_AcctCode.Text + "%'";
            }
            else
            {
                swhere = "Acct_Code is not null";
            }
            if (txtDel_GlDate.Text != "")
            {
                swhere = swhere + " and GLDate = '" + txtDel_GlDate.Text + "'";
            }
            else
            {
                swhere = swhere + " and GLDate is not null";
            }

            if (txtDel_OpenValue.Text != "")
            {
                swhere = swhere + " and OpeningValue like '" + txtDel_OpenValue.Text + "%'";
            }
            else
            {
                swhere = swhere + " and OpeningValue is not null";
            }

            if (chkDel_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkDel_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }
            else
            {
                swhere = swhere + " and IsActive is not null";
            }
            squer = squer + " where " + swhere + " and  GocID = '" + gocid() + "' and CompId = '" + compid() + "' and BranchId = '" + branchId() + "' and Record_Deleted = '0' ORDER BY Acct_Description";

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
            string squer = "select * from View_COA_Detail_opening";

            if (RadComboBoxProduct.SelectedIndex != 0)
            {
                swhere = "Acct_Code like '" + RadComboBoxProduct.Text + "%'";
            }
            else
            {
                swhere = "Acct_Code is not null";
            }
            if (txtEdit_GlDate.Text != "")
            {
                swhere = swhere + " and GLDate = '" + txtEdit_GlDate.Text + "'";
            }
            else
            {
                swhere = swhere + " and GLDate is not null";
            }

            if (txtEdit_OpenValue.Text != "")
            {
                swhere = swhere + " and OpeningValue like '" + txtEdit_OpenValue.Text + "%'";
            }
            else
            {
                swhere = swhere + " and OpeningValue is not null";
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
            squer = squer + " where " + swhere + " and  GocID = '" + gocid() + "' and CompId = '" + compid() + "' and BranchId = '" + branchId() + "' and Record_Deleted = '0' ORDER BY Acct_Description";

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
        updatecol.Visible = false;
        uploadtable.Visible = false;

        RadComboAcct_Code.Enabled = false;
        txtOpeningValue.Enabled = false;
        txtGLDate.Enabled = false;
        ddlAccountType.Enabled = false;
        ddlTranType.Enabled = false;
        chkActive.Enabled = false;
        txtCreatedby.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdateBy.Enabled = false;
        txtUpdateDate.Enabled = false;
        imgPopup.Enabled = false;

        Button1.Enabled = false;
        FileUpload1.Enabled = false;

        Label1.Text = "";
        txtCreatedby.Text = "";
        txtCreatedDate.Text = "";
        txtUpdateBy.Text = "";
        txtUpdateDate.Text = "";
        RadComboAcct_Code.Text = "";

        //RadComboAcct_Code.SelectedIndex = 0;
        txtOpeningValue.Text = "";
        txtGLDate.Text = "";
        ddlAccountType.SelectedIndex = 0;
        ddlTranType.SelectedIndex = 0;
        chkActive.Checked = false;

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
        SqlDataAdapter da = new SqlDataAdapter("select * from SET_UserGrp_Form where FormId='" + FormID + "' and DmlAllowed = 'Y' AND UserGrpId in (Select UserGrpId from SET_Assign_UserGrp where UserId='" + userid + "' AND Record_Deleted='0')", con);
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
            dml.Delete("update SET_ItemMaster set Record_Deleted = 1 where ItemID = " + ViewState["SNO"].ToString() + "", "");
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
        RadComboAcct_Code.Enabled = false;
        txtOpeningValue.Enabled = false;
        txtGLDate.Enabled = false;
        ddlAccountType.Enabled = false;
        ddlTranType.Enabled = false;
        chkActive.Enabled = false;
        txtCreatedby.Enabled = false;
        imgPopup.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdateBy.Enabled = false;
        txtUpdateDate.Enabled = false;

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
            DataSet ds = dml.Find("select * from SET_COA_Detail_Opening where COA_Opening_ID  = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {


                RadComboAcct_Code.ClearSelection();
                ddlAccountType.ClearSelection();
                ddlTranType.ClearSelection();
                string str = ds.Tables[0].Rows[0]["Acct_Code"].ToString();
                string where = "Record_Deleted = '0'";

                cmb.serachcombo4(RadComboAcct_Code, str, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");


                RadComboBoxItem item = RadComboAcct_Code.FindItemByText(ds.Tables[0].Rows[0]["Acct_Code"].ToString());
                item.Selected = true;



                // RadComboAcct_Code.FindItemByText(ds.Tables[0].Rows[0]["Acct_Code"].ToString()).Selected = true ;
                // dml.dropToolTip(RadComboAcct_Code);
                ddlAccountType.Items.FindByValue(ds.Tables[0].Rows[0]["Acct_Type_ID"].ToString()).Selected = true;
                ddlTranType.Items.FindByText(ds.Tables[0].Rows[0]["Tran_Type"].ToString()).Selected = true;
                double val = double.Parse(ds.Tables[0].Rows[0]["OpeningValue"].ToString());

                txtOpeningValue.Text = val.ToString("0.00");
                txtGLDate.Text = ds.Tables[0].Rows[0]["GLDate"].ToString();
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());

                
                txtCreatedDate.Text = ds.Tables[0].Rows[0]["CreateDate"].ToString();

                txtUpdateDate.Text = ds.Tables[0].Rows[0]["UpdateDate"].ToString();
                txtCreatedby.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdateBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());
                RadComboAcct_Code.ToolTip = RadComboAcct_Code.SelectedValue.Split(new char[] { ':' })[1];
                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
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
                dml.dateConvert(txtGLDate);
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
        uploadtable.Visible = false;
        btnInsert.Visible = false;
        btnCancel.Visible = true;
        btnUpdate.Visible = false;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnFind.Visible = false;
        btnDelete_after.Visible = true;
        txtUpdateBy.Enabled = false;
        txtUpdateDate.Enabled = false;
        textClear();

        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        DeleteBox.Visible = false;
        try
        {
            string serial_id;
            serial_id = GridView2.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select * from SET_COA_Detail_Opening where COA_Opening_ID  = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                RadComboAcct_Code.ClearSelection();
                ddlAccountType.ClearSelection();
                ddlTranType.ClearSelection();

                string str = ds.Tables[0].Rows[0]["Acct_Code"].ToString();

                string where = "Record_Deleted = '0'";

                cmb.serachcombo4(RadComboAcct_Code, str, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");


                RadComboBoxItem item = RadComboAcct_Code.FindItemByText(ds.Tables[0].Rows[0]["Acct_Code"].ToString());
                item.Selected = true;


                //RadComboAcct_Code.Items.FindItemByText(ds.Tables[0].Rows[0]["Acct_Code"].ToString()).Selected = true;
                // dml.dropToolTip(ddlAcct_Code);
                ddlAccountType.Items.FindByValue(ds.Tables[0].Rows[0]["Acct_Type_ID"].ToString()).Selected = true;
                ddlTranType.Items.FindByText(ds.Tables[0].Rows[0]["Tran_Type"].ToString()).Selected = true;
                double val = double.Parse(ds.Tables[0].Rows[0]["OpeningValue"].ToString());

                txtOpeningValue.Text = val.ToString("0.00");
                txtGLDate.Text = ds.Tables[0].Rows[0]["GLDate"].ToString();
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());

                
                txtCreatedDate.Text = ds.Tables[0].Rows[0]["CreateDate"].ToString();

                txtUpdateDate.Text = ds.Tables[0].Rows[0]["UpdateDate"].ToString();
                txtCreatedby.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdateBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());
                RadComboAcct_Code.ToolTip = RadComboAcct_Code.SelectedValue.Split(new char[] { ':' })[1];
                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
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
                dml.dateConvert(txtGLDate);
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
        uploadtable.Visible = false;
        btnInsert.Visible = false;
        btnCancel.Visible = true;
        btnUpdate.Visible = true;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnFind.Visible = false;
        Label1.Text = "";

        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {
            string serial_id;
            serial_id = GridView3.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select * from SET_COA_Detail_Opening where COA_Opening_ID  = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                RadComboAcct_Code.ClearSelection();
                ddlAccountType.ClearSelection();
                ddlTranType.ClearSelection();

                string str = ds.Tables[0].Rows[0]["Acct_Code"].ToString();

                string where = "Record_Deleted = '0'";

                cmb.serachcombo4(RadComboAcct_Code, str, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");


                RadComboBoxItem item = RadComboAcct_Code.FindItemByText(ds.Tables[0].Rows[0]["Acct_Code"].ToString());
                item.Selected = true;


                //  RadComboAcct_Code.Items.FindItemByText(ds.Tables[0].Rows[0]["Acct_Code"].ToString()).Selected = true;
                // dml.dropToolTip(ddlAcct_Code);
                ddlAccountType.Items.FindByValue(ds.Tables[0].Rows[0]["Acct_Type_ID"].ToString()).Selected = true;
                ddlTranType.Items.FindByText(ds.Tables[0].Rows[0]["Tran_Type"].ToString()).Selected = true;
                double val = double.Parse(ds.Tables[0].Rows[0]["OpeningValue"].ToString());

                txtOpeningValue.Text = val.ToString("0.00");
                txtGLDate.Text = ds.Tables[0].Rows[0]["GLDate"].ToString();
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());

                
                txtCreatedDate.Text = ds.Tables[0].Rows[0]["CreateDate"].ToString();

                txtUpdateDate.Text = ds.Tables[0].Rows[0]["UpdateDate"].ToString();
                txtCreatedby.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdateBy.Text =dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());
                RadComboAcct_Code.ToolTip = RadComboAcct_Code.SelectedValue.Split(new char[] { ':' })[1];
                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
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
                dml.dateConvert(txtGLDate);
                RadComboAcct_Code.Enabled = true;
                txtOpeningValue.Enabled = true;
                txtGLDate.Enabled = true;
                ddlAccountType.Enabled = false;
                ddlTranType.Enabled = true;
                chkActive.Enabled = true;
                imgPopup.Enabled = true;
                txtCreatedby.Enabled = false;
                txtCreatedDate.Enabled = false;
                txtUpdateBy.Enabled = false;
                txtUpdateDate.Enabled = false;
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
    protected void ddlAcct_Code_SelectedIndexChanged(object sender, EventArgs e)
    {
        string val = "0", val2 = "0";
        DataSet ds = dml.Find("select Acct_Type_ID from SET_COA_detail where COA_D_ID = '" + RadComboAcct_Code.SelectedItem.Value + "'");
        if (ds.Tables[0].Rows.Count > 0)
        {
            val = ds.Tables[0].Rows[0]["Acct_Type_ID"].ToString();
            dml.dropdownsql(ddlAccountType, "SET_Acct_Type", "Acct_Type_Name", "Acct_Type_Id", "Acct_Type_Id", val);
            ddlAccountType.ClearSelection();
            ddlAccountType.Items.FindByValue(val).Selected = true;

        }
        DataSet ds1 = dml.Find("select Tran_Type from SET_Acct_Type where Acct_Type_Id= '" + ddlAccountType.SelectedItem.Value + "'");
        if (ds1.Tables[0].Rows.Count > 0)
        {
            val2 = ds1.Tables[0].Rows[0]["Tran_Type"].ToString();
            ddlTranType.ClearSelection();
            ddlTranType.Items.FindByText(val2).Selected = true;
        }
        // dml.dropToolTip(RadComboAcct_Code);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
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



                //switch (extension)
                //{
                //    case ".xls": //Excel 97-03
                //                 //conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                //        conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + excelPath + ";Extended Properties='Excel 8.0;HDR=YES;'";
                //        break;
                //    case ".xlsx": //Excel 07 or higher
                //                  //conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                //        conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelPath + ";Extended Properties='Excel 8.0;HDR=YES'";
                //        break;


                //}
                //conString = string.Format(conString, excelPath);
                if (flag == true)
                {
                    using (OleDbConnection excel_con = new OleDbConnection(conString))
                    {
                        excel_con.Open();
                        string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                        DataTable dtExcelData = new DataTable();

                        //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                        dtExcelData.Columns.AddRange(new DataColumn[13] { new DataColumn("Acct_Code", typeof(string)),
                new DataColumn("GLDate", typeof(string)),
                new DataColumn("OpeningValue",typeof(decimal)) ,
        new DataColumn("Tran_Type",typeof(string)),
         new DataColumn("GocID",typeof(string)),
          new DataColumn("CompId",typeof(string)),
           new DataColumn("BranchId",typeof(string)),
            new DataColumn("FiscalYearID",typeof(string)),
            new DataColumn("IsActive",typeof(string)),
         new DataColumn("CreatedBy",typeof(string)),
          new DataColumn("Record_Deleted",typeof(bool)),
new DataColumn("UploadButtonDis_Ena",typeof(bool)),

new DataColumn("CreateDate",typeof(string))


        });

                        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * , '" + gocid() + "' as GocID, '" + compid() + "' as CompId, '" + branchId() + "' as BranchId , '" + FiscalYear() + "' as FiscalYearID , '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' as CreateDate ,'" + show_username() + "' as CreatedBy, 'false' as Record_Deleted, 'false' as UploadButtonDis_Ena , 'true'as IsActive FROM [" + sheet1 + "]", excel_con))
                        {
                            oda.Fill(dtExcelData);
                        }
                        excel_con.Close();

                        foreach (DataRow dtRow in dtExcelData.Rows)
                        {
                            string str = dtRow[0].ToString();
                            string consString1 = ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString;
                            SqlConnection con1 = new SqlConnection(consString1);
                            SqlDataAdapter da = new SqlDataAdapter("select * from SET_COA_Detail_Opening where Acct_Code='" + str + "'", con1);
                            DataSet ds1 = new DataSet();
                            da.Fill(ds1);
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                string id = ds1.Tables[0].Rows[0]["COA_Opening_ID"].ToString();
                                dml.Update("UPDATE SET_COA_Detail_Opening SET [Record_Deleted] = '1' where COA_Opening_ID = '" + id + "'; ", "");
                            }
                        }

                        string consString = ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(consString))
                        {
                            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                            {
                                //Set the database table name
                                sqlBulkCopy.DestinationTableName = "dbo.SET_COA_Detail_Opening";

                                //[OPTIONAL]: Map the Excel columns with that of the database table
                                sqlBulkCopy.ColumnMappings.Add("Acct_Code", "Acct_Code");
                                sqlBulkCopy.ColumnMappings.Add("GLDate", "GLDate");
                                sqlBulkCopy.ColumnMappings.Add("OpeningValue", "OpeningValue");
                                sqlBulkCopy.ColumnMappings.Add("Tran_Type", "Tran_Type");
                                sqlBulkCopy.ColumnMappings.Add("GocID", "GocID");
                                sqlBulkCopy.ColumnMappings.Add("CompId", "CompId");
                                sqlBulkCopy.ColumnMappings.Add("BranchId", "BranchId");
                                sqlBulkCopy.ColumnMappings.Add("FiscalYearID", "FiscalYearID");
                                sqlBulkCopy.ColumnMappings.Add("CreatedBy", "CreatedBy");
                                sqlBulkCopy.ColumnMappings.Add("IsActive", "IsActive");
                                sqlBulkCopy.ColumnMappings.Add("Record_Deleted", "Record_Deleted");
                                sqlBulkCopy.ColumnMappings.Add("CreateDate", "CreateDate");
                                sqlBulkCopy.ColumnMappings.Add("UploadButtonDis_Ena", "UploadButtonDis_Ena");

                                //IsActive
                                con.Open();
                                sqlBulkCopy.WriteToServer(dtExcelData);
                                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "fileupd()", true);
                                textClear();
                                btnInsert_Click(sender, e);
                                con.Close();
                            }
                        }


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
    protected void RadComboBoxProduct_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(RadComboBoxProduct, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
    protected void ddlFind_AcctCode_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(cmbFind_Accountcode, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
    protected void cmbDel_AcctCode_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(cmbDel_AcctCode, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }

    protected void RadComboAcct_Code_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        //SELECT Acct_Code,Acct_Description,Acct_Type_Name,Tran_Type from view_Search_Acct_Code
        //Select * from SET_COA_detail where (Head_detail_ID = 'd1') and (Acct_Type_ID = '1' OR Acct_Type_ID = '2' or Acct_Type_ID = '3' or  Acct_Type_ID = '6')
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(RadComboAcct_Code, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

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


                        //Change Here
                        dtExcelData.Columns.AddRange(new DataColumn[13] { new DataColumn("Acct_Code", typeof(string)),
                new DataColumn("GLDate", typeof(string)),
                new DataColumn("OpeningValue",typeof(decimal)) ,
        new DataColumn("Tran_Type",typeof(string)),
         
                            //End Change Here


                            new DataColumn("GocID",typeof(string)),
          new DataColumn("CompId",typeof(string)),
           new DataColumn("BranchId",typeof(string)),
            new DataColumn("FiscalYearID",typeof(string)),
            new DataColumn("IsActive",typeof(string)),
         new DataColumn("CreatedBy",typeof(string)),
          new DataColumn("Record_Deleted",typeof(bool)),
new DataColumn("UploadButtonDis_Ena",typeof(bool)),
new DataColumn("CreateDate",typeof(string))
//Acct_Type_ID

        });

                        int counter_index = 0;
                        string code = "";
                        
                        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * , '" + gocid() + "' as GocID, '" + compid() + "' as CompId, '" + branchId() + "' as BranchId , '" + FiscalYear() + "' as FiscalYearID , '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' as CreateDate ,'" + show_username() + "' as CreatedBy, 'false' as Record_Deleted, 'true' as UploadButtonDis_Ena , 'true'as IsActive  FROM [" + sheet1 + "]", excel_con))
                        {
                            oda.Fill(dtExcelData);
                            dtExcelData.Columns.Add("Acct_Type_ID", typeof(System.Int32));
                        }
                        excel_con.Close();

                        foreach (DataRow dtRow in dtExcelData.Rows)
                        {
                            
                            string str = dtRow[0].ToString();
                            dtRow["Acct_Type_ID"] = acct_type(str);
                            string consString1 = ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString;
                            SqlConnection con1 = new SqlConnection(consString1);

                            SqlDataAdapter da1 = new SqlDataAdapter("select * from SET_COA_detail where Acct_Code = '"+str+"'", con1);
                            DataSet dsacct_code = new DataSet();
                            da1.Fill(dsacct_code);
                            if (dsacct_code.Tables[0].Rows.Count > 0)
                            {
                                SqlDataAdapter da = new SqlDataAdapter("select * from SET_COA_Detail_Opening where Acct_Code = '" + str + "'", con1);
                                DataSet ds1 = new DataSet();
                                da.Fill(ds1);
                                if (ds1.Tables[0].Rows.Count > 0)
                                {
                                    string id = ds1.Tables[0].Rows[0]["COA_Opening_ID"].ToString();
                                    //delete from SET_COA_Detail_Opening where GocID = '1' and CompId ='1' and BranchId = '5' and COA_Opening_ID = '158'
                                    dml.Update("Delete from SET_COA_Detail_Opening where GocID = '" + gocid() + "' and CompId ='" + compid() + "' and BranchId = '" + branchId() + "' and COA_Opening_ID = '" + id + "'; ", "");
                                    
                                    
                                }
                                counter_index = counter_index + 1;
                            }
                            else
                            {
                                
                                dtExcelData.Rows[counter_index].Delete();
                                code =  code+","+ str;
                                counter_index = counter_index + 1;
                            }
                        }

                        string consString = ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(consString))
                        {
                            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                            {
                               
                                sqlBulkCopy.DestinationTableName = "dbo.SET_COA_Detail_Opening";
                                                               
                                sqlBulkCopy.ColumnMappings.Add("Acct_Code", "Acct_Code");
                                sqlBulkCopy.ColumnMappings.Add("GLDate", "GLDate");
                                sqlBulkCopy.ColumnMappings.Add("OpeningValue", "OpeningValue");
                                sqlBulkCopy.ColumnMappings.Add("Tran_Type", "Tran_Type");
                                sqlBulkCopy.ColumnMappings.Add("GocID", "GocID");
                                sqlBulkCopy.ColumnMappings.Add("CompId", "CompId");
                                sqlBulkCopy.ColumnMappings.Add("BranchId", "BranchId");
                                sqlBulkCopy.ColumnMappings.Add("FiscalYearID", "FiscalYearID");
                                sqlBulkCopy.ColumnMappings.Add("CreatedBy", "CreatedBy");
                                sqlBulkCopy.ColumnMappings.Add("IsActive", "IsActive");
                                sqlBulkCopy.ColumnMappings.Add("Record_Deleted", "Record_Deleted");
                                sqlBulkCopy.ColumnMappings.Add("CreateDate", "CreateDate");
                                sqlBulkCopy.ColumnMappings.Add("UploadButtonDis_Ena", "UploadButtonDis_Ena");
                                sqlBulkCopy.ColumnMappings.Add("Acct_Type_ID", "Acct_Type_ID");

                                //IsActive
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

                        Label1.Text = "These Code doesn't match with account code " +  code;
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

            dml.Update("update SET_COA_Detail_Opening set UploadButtonDis_Ena = '0' where GocID = '" + gocid() + "' and CompId ='" + compid() + "' and BranchId = '" + branchId() + "'", "");
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

    protected void btnNo_2_Click(object sender, EventArgs e)
    {

    }
    protected void RadComboAcct_Code_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        //Label1.Text = "You selected " + e.Text + " item";
        string val = "0", val2 = "0";
        DataSet ds = dml.Find("select Acct_Type_ID from SET_COA_detail where Acct_Code = '" + e.Text + "'");
        if (ds.Tables[0].Rows.Count > 0)
        {
            val = ds.Tables[0].Rows[0]["Acct_Type_ID"].ToString();
            dml.dropdownsql(ddlAccountType, "SET_Acct_Type", "Acct_Type_Name", "Acct_Type_Id", "Acct_Type_Id", val);
            ddlAccountType.ClearSelection();
            ddlAccountType.Items.FindByValue(val).Selected = true;

        }
        DataSet ds1 = dml.Find("select Tran_Type from SET_Acct_Type where Acct_Type_Id= '" + ddlAccountType.SelectedItem.Value + "'");
        if (ds1.Tables[0].Rows.Count > 0)
        {
            val2 = ds1.Tables[0].Rows[0]["Tran_Type"].ToString();
            ddlTranType.ClearSelection();
            ddlTranType.Items.FindByText(val2).Selected = true;
        }
        RadComboAcct_Code.ToolTip = RadComboAcct_Code.SelectedValue.Split(new char[] { ':' })[1];
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            RadComboAcct_Code.FindItemByText("1-01-01-0001").Selected = true;
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }


    public int acct_type(string code1)
    {
        DataSet ds =  dml.Find("select  Acct_Code,Acct_Type_ID from SET_COA_detail where Acct_Code='"+code1+"'");
        if (ds.Tables[0].Rows.Count > 0)
        {
            return int.Parse(ds.Tables[0].Rows[0]["Acct_Type_ID"].ToString());
        }
        else
        {
            return 1;
        }
    }
}