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
    int DateFrom, AddDays, DeleteDays, EditDays;
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

            
            fd.HideUSerGrpField(Page.Controls, UserGrpID, FormID, "SET_Department");

            dml.dropdownsql(txtEdit_BPnature, "SET_BPartnerNature", "BPNatureDescription", "BPNatureID");
            dml.dropdownsql(txtFind_BpNature, "SET_BPartnerNature", "BPNatureDescription", "BPNatureID");
            dml.dropdownsql(txtdelete_BPNature, "SET_BPartnerNature", "BPNatureDescription", "BPNatureID");

            
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

        

        imgPopup.Enabled = true;
        txtBPNature.Enabled = true;
        
        RadComboAcct_Code.Enabled = true;
        txtApplydate.Enabled = true;
        txtCreatedBy.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtApplydate.Text = DateTime.Now.ToString("dd-MMM-yyyy");

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
               
                DataSet s = dml.Find("select  COA_D_ID from SET_COA_detail where Acct_Code = '"+RadComboAcct_Code.Text+"'");
                if (s.Tables[0].Rows.Count > 0)
                {
                    string coaid = s.Tables[0].Rows[0]["COA_D_ID"].ToString();


                    dml.Insert("INSERT INTO [SET_BPartnerNature] ([BPNatureDescription], [Acct_Code], [IsActive], [COA_D_ID], [EntryDate], [CreatedBy], [CreatedDate], [Record_Deleted],[MLD]) VALUES  ('" + txtBPNature.Text + "',  '" + RadComboAcct_Code.Text + "', '" + chk + "', '"+ coaid + "' ,'" + dml.dateconvertforinsert(txtApplydate) + "' , '" + txtCreatedBy.Text + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' , '0','"+dml.Encrypt("h")+"');", "alertme()");
                    ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);

                    Label1.Text = "";
                    txtBPNature.Text = "";

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


            DataSet ds_up = dml.Find("select * from SET_BPartnerNature WHERE ([BPNatureID]='"+ViewState["SNO"].ToString() +"') AND ([BPNatureDescription]='"+txtBPNature.Text+"') AND ([Acct_Code]='"+RadComboAcct_Code.Text+"') AND ([IsActive]='"+chk+"')  AND ([EntryDate]='"+txtApplydate.Text+"')");

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

                dml.Update("UPDATE SET_BPartnerNature  SET [BPNatureDescription]='"+txtBPNature.Text+"', [Acct_Code]='"+RadComboAcct_Code.Text+"', [IsActive]='"+chk+"', [EntryDate]='"+txtApplydate.Text+"',  [UpdatedBy]='"+show_username()+"', [UpdatedDate]='"+DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")+ "' where BPNatureID = " + ViewState["SNO"].ToString() + "", "");
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
            string squer = "Select * from View_BP_NatureBusiness";

            if (txtdelete_BPNature.SelectedIndex != 0)
            {
                swhere = "BPNatureDescription = '" + txtdelete_BPNature.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "BPNatureDescription is not null";
            }

            if (txtdelete_AccountCode.Text != "")
            {
                swhere = swhere + " and Acct_Code like '" + txtdelete_AccountCode.Text + "%'";
            }
            else
            {
                swhere = swhere + " and Acct_Code is not null";

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
            squer = squer + " where " + swhere + " and Record_Deleted = 0  ORDER BY BPNatureDescription";


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
        txtBPNature.Enabled = false;
      
        RadComboAcct_Code.Enabled = false;
        txtApplydate.Enabled = false;
        txtCreatedBy.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdatedBy.Enabled = false;
        txtUpadtedDate.Enabled = false;

        btnCancel.Visible = true;
        btnFind.Visible = true;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnInsert.Visible = false;

        GridView1.DataBind();
        try {
            string swhere;
            string squer = "Select * from View_BP_NatureBusiness";

            if (txtFind_BpNature.SelectedIndex != 0)
            {
                swhere = "BPNatureDescription = '" + txtFind_BpNature.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "BPNatureDescription is not null";
            }
          
            if (txtFind_AccountCode.Text != "")
            {
                swhere = swhere + " and Acct_Code like '" + txtFind_AccountCode.Text + "%'";
            }
            else
            {
                swhere = swhere + " and Acct_Code is not null";
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

            
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY BPNatureDescription";

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
            string squer = "Select * from View_BP_NatureBusiness";

            if (txtEdit_BPnature.SelectedIndex != 0)
            {
                swhere = "BPNatureDescription = '" + txtEdit_BPnature.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "BPNatureDescription is not null";
            }
          
            if (txtEdit_AccountCode.Text != "")
            {
                swhere = swhere + " and Acct_Code like '" + txtEdit_AccountCode.Text + "%'";
            }
            else
            {
                swhere = swhere + " and Acct_Code is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0  ORDER BY BPNatureDescription";


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

        txtBPNature.Enabled = false;
        txtApplydate.Enabled = false;
        RadComboAcct_Code.Enabled = false;
      
        txtCreatedBy.Enabled = false;
        txtCreatedDate.Enabled = false;

        txtUpdatedBy.Enabled = false;
        txtUpadtedDate.Enabled = false;
        chkActive.Enabled = false;
        chkActive.Checked = false;
        imgPopup.Enabled = false;

        Label1.Text = "";
        txtBPNature.Text = "";
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
            dml.Delete("update SET_BPartnerNature set Record_Deleted = 1 where BPNatureID = " + ViewState["SNO"].ToString() + "", "");
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
        txtBPNature.Enabled = false;
        
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
            DataSet ds = dml.Find("select * from SET_BPartnerNature where BPNatureID = '" + ViewState["SNO"].ToString() +"' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                RadComboAcct_Code.ClearSelection();
                txtBPNature.Text = ds.Tables[0].Rows[0]["BPNatureDescription"].ToString();

                string s = "";
                string code = ds.Tables[0].Rows[0]["Acct_Code"].ToString();
                DataSet dsfind = dml.Find("select Acct_Description from SET_COA_detail where Acct_Code = '" + code + "' and Record_Deleted = 0");
                if (dsfind.Tables[0].Rows.Count > 0)
                {
                    s = dsfind.Tables[0].Rows[0]["Acct_Description"].ToString();
                }
                RadComboAcct_Code.ClearSelection();
                txtBPNature.Text = ds.Tables[0].Rows[0]["BPNatureDescription"].ToString();

                //valuegetcombobox(ds, "Acct_Code", RadComboAcct_Code);
                //RadComboAcct_Code.Items.FindByValue(ds.Tables[0].Rows[0]["AccountCode"].ToString()).Selected = true;

                RadComboAcct_Code.Text = code;// ds.Tables[0].Rows[0]["Acct_Code"].ToString();
                RadComboAcct_Code.ToolTip = s;


                txtApplydate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtCreatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtCreatedDate.Text = ds.Tables[0].Rows[0]["CreatedDate"].ToString();

                if (ds.Tables[0].Rows[0]["CreatedDate"].ToString() == "")
                {
                    txtCreatedDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                    txtCreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdatedBy"].ToString() == "")
                {
                    txtUpadtedDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdatedDate"].ToString());
                    txtUpadtedDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
               bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                txtUpdatedBy.Text = ds.Tables[0].Rows[0]["UpdatedBy"].ToString();

                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                dml.dateConvert(txtApplydate);
                
              
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
            DataSet ds = dml.Find("select * from SET_BPartnerNature where BPNatureID = '" + ViewState["SNO"].ToString() + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                RadComboAcct_Code.ClearSelection();
                txtBPNature.Text = ds.Tables[0].Rows[0]["BPNatureDescription"].ToString();
                string s = "";
                string code = ds.Tables[0].Rows[0]["Acct_Code"].ToString();
                DataSet dsfind = dml.Find("select Acct_Description from SET_COA_detail where Acct_Code = '" + code + "' and Record_Deleted = 0");
                if (dsfind.Tables[0].Rows.Count > 0)
                {
                    s = dsfind.Tables[0].Rows[0]["Acct_Description"].ToString();
                }
                RadComboAcct_Code.ClearSelection();
                txtBPNature.Text = ds.Tables[0].Rows[0]["BPNatureDescription"].ToString();

                //valuegetcombobox(ds, "Acct_Code", RadComboAcct_Code);
                //RadComboAcct_Code.Items.FindByValue(ds.Tables[0].Rows[0]["AccountCode"].ToString()).Selected = true;

                RadComboAcct_Code.Text = code;// ds.Tables[0].Rows[0]["Acct_Code"].ToString();
                RadComboAcct_Code.ToolTip = s;

                txtApplydate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtCreatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtCreatedDate.Text = ds.Tables[0].Rows[0]["CreatedDate"].ToString();

                if (ds.Tables[0].Rows[0]["CreatedDate"].ToString() == "")
                {
                    txtCreatedDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                    txtCreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdatedBy"].ToString() == "")
                {
                    txtUpadtedDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdatedDate"].ToString());
                    txtUpadtedDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                txtUpdatedBy.Text = ds.Tables[0].Rows[0]["UpdatedBy"].ToString();

                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                dml.dateConvert(txtApplydate);
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

        txtBPNature.Enabled = true;
       
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
            DataSet ds = dml.Find("select * from SET_BPartnerNature where BPNatureID = '" + ViewState["SNO"].ToString() + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                string s="";
                string code = ds.Tables[0].Rows[0]["Acct_Code"].ToString();
                DataSet dsfind = dml.Find("select Acct_Description from SET_COA_detail where Acct_Code = '"+ code + "' and Record_Deleted = 0");
                if (dsfind.Tables[0].Rows.Count>0)
                {
                    s = dsfind.Tables[0].Rows[0]["Acct_Description"].ToString();
                }
                RadComboAcct_Code.ClearSelection();
                txtBPNature.Text = ds.Tables[0].Rows[0]["BPNatureDescription"].ToString();

                //valuegetcombobox(ds, "Acct_Code", RadComboAcct_Code);
                //RadComboAcct_Code.Items.FindByValue(ds.Tables[0].Rows[0]["AccountCode"].ToString()).Selected = true;

                RadComboAcct_Code.Text = code;// ds.Tables[0].Rows[0]["Acct_Code"].ToString();
                RadComboAcct_Code.ToolTip = s;

                //   RadComboAcct_Code.Items.FindItemByText(ds.Tables[0].Rows[0]["Acct_Code"].ToString()).Selected = true;

                txtApplydate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtCreatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtCreatedDate.Text = ds.Tables[0].Rows[0]["CreatedDate"].ToString();

                if (ds.Tables[0].Rows[0]["CreatedDate"].ToString() == "")
                {
                    txtCreatedDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                    txtCreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdatedBy"].ToString() == "")
                {
                    txtUpadtedDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdatedDate"].ToString());
                    txtUpadtedDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                txtUpdatedBy.Text = ds.Tables[0].Rows[0]["UpdatedBy"].ToString();

                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                dml.dateConvert(txtApplydate);
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
            
            cmb.serachcombo4(rad, str, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");
            rad.FindItemByText(dataset.Tables[0].Rows[0][colname].ToString()).Selected = true;
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

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string id = e.Row.Cells[1].Text;
        bool flag = dml.isNumber(id);


        if (flag == true)
        {

            DataSet ds = dml.Find("select BPNatureID,MLD from SET_BPartnerNature where BPNatureID = '" + id + "'");
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

            DataSet ds = dml.Find("select BPNatureID,MLD from SET_BPartnerNature where BPNatureID = '" + id + "'");
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