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
            ViewState["FormId"] = FormID;
            fiscal = Request.QueryString["fiscaly"];
            ViewState["userid"] = userid;
            dml.dropdownsql(ddlEdit_doctype, "SET_Documents", "DocDescription", "DocID");
            dml.dropdownsql(ddlFind_DocType, "SET_Documents", "DocDescription", "DocID");
            dml.dropdownsql(ddlDel_DocType, "SET_Documents", "DocDescription", "DocID");
   
            dml.dropdownsql(ddldoc, "SET_Documents", "DocDescription", "DocID");
           
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
        userid = Request.QueryString["UserID"];
        ddldoc.Enabled = true;
        txtRadioBtnName.Enabled = true;
        chkActive.Enabled = true;

        txtCreatedby.Text = dml.show_usernameFED(userid);
        txtCreatedDate.Text = DateTime.Now.ToString("dd-MMM-yyy hh:mm:ss.ffff");
        chkActive.Checked = true;


    }
    protected void btnSave_Click(object sender, EventArgs e)
    {


        DataSet uniqueg_B_C = dml.Find("select * from SET_DocRadioBinding where DocId = '"+ddldoc.SelectedItem.Value+"' and Record_Deleted = 0");
        if (uniqueg_B_C.Tables[0].Rows.Count > 0)
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = "Duplicated entry not allowed";
        }

        else {


            int chk = 0 ;
            if (chkActive.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }

            
            dml.Insert("INSERT INTO SET_DocRadioBinding ([DocId], [RadioButton], [IsActive], [CreatedBy], [CreateDate], [Record_Deleted]) VALUES  ('"+ddldoc.SelectedItem.Value+"', '"+txtRadioBtnName.Text.ToUpper() +"', '"+chk+"', '"+show_username()+"', '"+DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")+"', '0');", "");
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);

            textClear();
            btnInsert_Click(sender, e);
           
 
           
        }
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


        DataSet ds_up = dml.Find("select * from SET_DocRadioBinding WHERE ([Sno]='" + ViewState["SNO"].ToString() + "') AND ([DocId]='" + ddldoc.SelectedItem.Value + "') AND ([RadioButton]='" + txtRadioBtnName.Text + "') AND ([IsActive]='" + chk + "') AND ([Record_Deleted]='0');" );

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

            dml.Update("UPDATE SET_DocRadioBinding SET  [DocId]='" + ddldoc.SelectedItem.Value + "', [RadioButton]='" + txtRadioBtnName.Text.ToUpper() +"', [IsActive]='"+chk+"', [UpdatedBy]='"+show_username()+"', [UpdateDate]='"+DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")+"' WHERE ([Sno]='"+ViewState["SNO"].ToString()+"')", "");
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
            string squer = "select * from View_DocRdbBtnBind";


            if (ddlDel_DocType.SelectedIndex != 0)
            {
                swhere = "DocDescription = '" + ddlDel_DocType.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "DocDescription is not null";
            }
           
            if (chkDel_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkDel_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY DocDescription";

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
            string squer = "select * from View_DocRdbBtnBind";


            if (ddlFind_DocType.SelectedIndex != 0)
            {
                swhere = "DocDescription = '" + ddlFind_DocType.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "DocDescription is not null";
            }

            if (chkFind_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkFind_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY DocDescription";

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
            string squer = "select * from View_DocRdbBtnBind";


            if (ddlEdit_doctype.SelectedIndex != 0)
            {
                swhere = "DocDescription = '" + ddlEdit_doctype.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "DocDescription is not null";
            }

            if (chkEdit_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkEdit_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY DocDescription";

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


        ddldoc.Enabled = false;
        txtRadioBtnName.Enabled = false;
        txtCreatedby.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdateBy.Enabled = false;
        txtUpdateDate.Enabled = false;
        chkActive.Enabled = false;

        ddldoc.SelectedIndex = 0;
        txtRadioBtnName.Text = "";
        txtCreatedby.Text = "";
        txtCreatedDate.Text = "";
        txtUpdateBy.Text = "";
        txtUpdateDate.Text = "";
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

                dml.Delete("update SET_DocRadioBinding set Record_Deleted = 1 where Sno = " + ViewState["SNO"].ToString() + "", "");
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



            DataSet ds = dml.Find("select * from SET_DocRadioBinding WHERE ([Sno]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {


                ddldoc.ClearSelection();
                ddldoc.Items.FindByValue(ds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;
                txtRadioBtnName.Text = ds.Tables[0].Rows[0]["RadioButton"].ToString();
                
                txtCreatedDate.Text = ds.Tables[0].Rows[0]["CreateDate"].ToString();
                txtCreatedby.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdateBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());
                txtUpdateDate.Text = ds.Tables[0].Rows[0]["UpdateDate"].ToString();
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
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



            DataSet ds = dml.Find("select * from SET_DocRadioBinding WHERE ([Sno]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {


                ddldoc.ClearSelection();
                ddldoc.Items.FindByValue(ds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;
                txtRadioBtnName.Text = ds.Tables[0].Rows[0]["RadioButton"].ToString();
                
                txtCreatedDate.Text = ds.Tables[0].Rows[0]["CreateDate"].ToString();
                txtCreatedby.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdateBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());
                txtUpdateDate.Text = ds.Tables[0].Rows[0]["UpdateDate"].ToString();
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
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

      txtRadioBtnName.Enabled = true;
        ddldoc.Enabled = true;
      //chkStatus.Enabled = true;
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



            DataSet ds = dml.Find("select * from SET_DocRadioBinding WHERE ([Sno]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {


                ddldoc.ClearSelection();
                ddldoc.Items.FindByValue(ds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;
                txtRadioBtnName.Text = ds.Tables[0].Rows[0]["RadioButton"].ToString();
                
                txtCreatedDate.Text = ds.Tables[0].Rows[0]["CreateDate"].ToString();
                txtCreatedby.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdateBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());
                txtUpdateDate.Text = ds.Tables[0].Rows[0]["UpdateDate"].ToString();
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
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



  
    
}