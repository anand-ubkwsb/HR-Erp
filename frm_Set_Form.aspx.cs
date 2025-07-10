
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
           
            fiscal = Request.QueryString["fiscaly"];
            FormID = Request.QueryString["FormID"];
            Showall_Dml();
            
            dml.dropdownsql(ddlMenu, "SET_Menu", "Menu_title", "MenuId");
            

            dml.dropdownsql(ddlEdit_MenuTitle, "SET_Form", "FormTitle", "Sno");
            dml.dropdownsql(ddlEdit_Module, "SET_Menu", "Menu_title", "MenuId");

            dml.dropdownsql(ddlFind_MenuName, "SET_Form", "FormTitle", "Sno");
            dml.dropdownsql(ddlFind_Module, "SET_Menu", "Menu_title", "MenuId");

            dml.dropdownsql(ddlDel_MenuName, "SET_Form", "FormTitle", "Sno");
            dml.dropdownsql(ddlDel_Module, "SET_Menu", "Menu_title", "MenuId");



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

        txtMenuNAme.Enabled = true;
        ddlMenu.Enabled = true;
        chkActive.Enabled = true;
        rdbHide_N.Enabled = true;
        rdbHide_Y.Enabled = true;
        rdbHAVDOC_Y.Enabled = true;
        rdbHAVDOC_N.Enabled = true;
        rdb_PEP_N.Enabled = true;
        rdb_PEP_Y.Enabled = true;
        
        txtSortOrder.Enabled = true;
        txtFormPath.Enabled = true;
        txtCreatedby.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdateBy.Enabled = true;
        txtUpdateDate.Enabled = true;

        rdbHide_N.Checked = true;
        rdbHAVDOC_N.Checked = true;
        rdb_PEP_N.Checked = true;

        
        chkActive.Checked = true;
       

        txtCreatedDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

        userid = Request.QueryString["UserID"];
        DataSet ds_autoUserName = dml.Find("select user_name from SET_User_Manager where UserId ='" + userid + "'");
        txtCreatedby.Text = ds_autoUserName.Tables[0].Rows[0]["user_name"].ToString();

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        DataSet uniqueg_B_C = dml.Find("select * from SET_Menu where Menu_title = '" + txtMenuNAme.Text + "' and Record_Deleted = '0'");
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
            else
            {
                chk = 0;
            }
            string hide = "",HAVDOC ="",PEP="";
            if (rdbHide_N.Checked == true)
            {
                hide = "N";
            }
            if (rdbHide_Y.Checked == true)
            {
                hide = "Y";
            }
            if (rdbHAVDOC_Y.Checked == true)
            {
                HAVDOC = "Y";
            }
            if (rdbHAVDOC_N.Checked == true)
            {
                HAVDOC = "N";
            }
            if (rdbHide_Y.Checked == true)
            {
                PEP = "Y";
            }
            if (rdbHide_N.Checked == true)
            {
                PEP = "N";
            }
            string m;
            if(ddlMenu.SelectedIndex == 0)
            {
                m = "NULL";
            }
            else
            {
                m = "'" + ddlMenu.SelectedItem.Value + "'";
            }


            dml.Insert("INSERT INTO [SET_Form] ([FormTitle], [MenuId], [FormPath], [IsActive], [HaveDocNo], [Hide], [SortOrder], [PEP_Exempt], [Record_Deleted], [CreatedBy], [CreateDate],[MLD])  VALUES ('" + txtMenuNAme.Text + "', " + m + ", '" + txtFormPath.Text + "', '" + chk + "', '" + HAVDOC + "', '" + hide + "', '" + txtSortOrder.Text + "', '" + PEP + "', '0','" + show_username() +"','"+DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")+"','"+dml.Encrypt("h")+"')", "");
            dml.Update("update Set_Menu set MLD = '" + dml.Encrypt("q") + "' where MenuId = '" + ddlMenu.SelectedItem.Value + "'", "");

            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);

            Label1.Text = "";
            txtMenuNAme.Text = "";
            ddlMenu.SelectedIndex = 0;
            chkActive.Checked = false;
            rdbHide_N.Checked = false;
            rdbHide_Y.Checked = false;
            
            rdbHAVDOC_Y.Checked = false;
            txtSortOrder.Text = "";
            txtFormPath.Text = "";
            txtCreatedby.Text = "";
            txtCreatedDate.Text = "";
            txtUpdateBy.Text = "";
            txtUpdateDate.Text = "";

            chkActive.Checked = true;
            FormID = Request.QueryString["FormID"];
            Showall_Dml();
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
        string hide = "", HAVDOC = "", PEP = "";
        if (rdbHide_N.Checked == true)
        {
            hide = "N";
        }
        if (rdbHide_Y.Checked == true)
        {
            hide = "Y";
        }
        if (rdbHAVDOC_Y.Checked == true)
        {
            HAVDOC = "Y";
        }
        if (rdbHAVDOC_N.Checked == true)
        {
            HAVDOC = "N";
        }
        if (rdb_PEP_Y.Checked == true)
        {
            PEP = "Y";
        }
        if (rdb_PEP_N.Checked == true)
        {
            PEP = "N";
        }
        string m;
        if (ddlMenu.SelectedIndex == 0)
        {
            m = "NULL";
        }
        else
        {
            m = "'" + ddlMenu.SelectedItem.Value + "'";
        }

    
        DataSet ds_up = dml.Find("select * from SET_Form WHERE ([Sno]='"+ViewState["SNO"].ToString()+"') AND ([FormTitle]='"+txtMenuNAme.Text+"') AND ([MenuId]="+m+") AND ([FormPath]='"+txtFormPath.Text+"') AND ([IsActive]='"+chk+"') AND ([HaveDocNo]='"+HAVDOC+"') AND ([Hide]='"+hide+"') AND ([SortOrder]='"+txtSortOrder.Text+"') AND ([PEP_Exempt]='"+PEP+"')");

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

            dml.Update("UPDATE [SET_Form] SET  [FormTitle]='"+txtMenuNAme.Text+"', [MenuId]="+m+", [FormPath]='"+txtFormPath.Text+"', [IsActive]='"+chk+"', [HaveDocNo]='"+HAVDOC+"', [Hide]='"+hide+"', [SortOrder]='"+txtSortOrder.Text+"', [PEP_Exempt]='"+PEP+ "' , [UpdatedBy]='"+show_username()+"', [UpdateDate]='"+DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")+"'  WHERE ([Sno]='"+ ViewState["SNO"].ToString()+ "')", "");
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
            
            string swhere = "";
            string squer = "select * from View_SetFormFED";


            if (ddlDel_MenuName.SelectedIndex != 0)
            {
                swhere = "FormTitle like '" + ddlDel_MenuName.SelectedItem.Text + "%'";
            }
            else
            {
                swhere = "FormTitle is not null";
            }
            if (ddlDel_Module.SelectedIndex != 0)
            {
                swhere = swhere + " and MenuId = '" + ddlDel_Module.SelectedItem.Value + "'";
            }


            if (rdbDel_M.Checked == true)
            {
                swhere = swhere + " and HaveDocNo = 'Y'";
            }
            else if (rdbDel_S.Checked == true)
            {
                swhere = swhere + " and HaveDocNo = 'N'";
            }
           
            if (chkDel_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkDel_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0  ORDER BY FormTitle";

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
       
            string squer = "select * from View_SetFormFED";


            if (ddlFind_MenuName.SelectedIndex != 0)
            {
                swhere = "FormTitle like '" + ddlFind_MenuName.SelectedItem.Text + "%'";
            }
            else
            {
                swhere = "FormTitle is not null";
            }
            if (ddlFind_Module.SelectedIndex != 0)
            {
                swhere = swhere + " and MenuId = '" + ddlFind_Module.SelectedItem.Value + "'";
            }


            if (rdbFind_M.Checked == true)
            {
                swhere = swhere + " and HaveDocNo = 'Y'";
            }
            else if (rdbFind_S.Checked == true)
            {
                swhere = swhere + " and HaveDocNo = 'N'";
            }
          
            if (chkFind_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkFind_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0  ORDER BY FormTitle";

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
            string squer = "select * from View_SetFormFED";
            

            if (ddlEdit_MenuTitle.SelectedIndex != 0)
            {
                swhere = "FormTitle like '" + ddlEdit_MenuTitle.SelectedItem.Text + "%'";
            }
            else
            {
                swhere = "FormTitle is not null";
            }
            if (ddlEdit_Module.SelectedIndex != 0)
            {
                swhere = swhere + " and MenuId = '" + ddlEdit_Module.SelectedItem.Value+"'";
            }
           

            if (rdbEditMain.Checked == true)
            {
                swhere = swhere + " and HaveDocNo = 'Y'";
            }
            else if (rdbEditSub.Checked == true)
            {
                swhere = swhere + " and HaveDocNo = 'N'";
            }
          
            if (chkEdit_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkEdit_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }

            squer = squer + " where " + swhere + " and Record_Deleted = 0  ORDER BY FormTitle";

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
       

        txtMenuNAme.Text = "";
        
        chkActive.Checked = false;
        rdbHide_N.Checked = false;
        rdbHide_Y.Checked = false;
        rdbHAVDOC_Y.Checked = false;
        rdbHAVDOC_N.Checked = false;

        rdb_PEP_N.Checked = false;
        rdb_PEP_Y.Checked = false;
        ddlMenu.SelectedIndex = 0;
        txtSortOrder.Text="";
        txtFormPath.Text = "";
        txtCreatedby.Text = "";
        txtCreatedDate.Text = "";
        txtUpdateBy.Text = "";
        txtUpdateDate.Text = "";
        Label1.Text = "";

        txtMenuNAme.Enabled = false;
        rdb_PEP_N.Enabled = false;
        rdb_PEP_Y.Enabled = false;
        chkActive.Enabled = false;
        rdbHide_N.Enabled = false;
        rdbHide_Y.Enabled = false;
        rdbHAVDOC_Y.Enabled = false;
        rdbHAVDOC_N.Enabled = false;
        ddlMenu.Enabled = false;
        txtSortOrder.Enabled = false;
        txtFormPath.Enabled = false;
        txtCreatedby.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdateBy.Enabled = false;
        txtUpdateDate.Enabled = false;

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
            DataSet dsformid = dml.Find("select FormId, FormTitle from SET_Form where Sno= '" + ViewState["SNO"].ToString() + "' and Record_Deleted='0'");
            if (dsformid.Tables[0].Rows.Count > 0)
            {
                string formid = dsformid.Tables[0].Rows[0]["FormId"].ToString();
                DataSet ds = dml.Find("select  * from SET_UserGrp_Form where FormId = '" + formid +"' and Record_Deleted = '0'");
                
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Label1.Text = "'" + dsformid.Tables[0].Rows[0]["FormTitle"].ToString().ToUpper() +"' Already in used. First delete from User Group Form";
                }
                else
                {
                    dml.Delete("update SET_Form set Record_Deleted = 1 where Sno = " + ViewState["SNO"].ToString() + "", "");
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
            }
            

           
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
            DataSet ds = dml.Find("select * from SET_Form WHERE ([Sno]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {


                ddlMenu.ClearSelection();


                txtMenuNAme.Text = ds.Tables[0].Rows[0]["FormTitle"].ToString();
                txtFormPath.Text = ds.Tables[0].Rows[0]["FormPath"].ToString();

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());

                string hideYN = ds.Tables[0].Rows[0]["Hide"].ToString();

                string MAinSub = ds.Tables[0].Rows[0]["HaveDocNo"].ToString();
                string PEP = ds.Tables[0].Rows[0]["PEP_Exempt"].ToString();
                if (ds.Tables[0].Rows[0]["MenuId"].ToString() == "")
                {
                    ddlMenu.SelectedIndex = 0;
                }
                else {
                    ddlMenu.Items.FindByValue(ds.Tables[0].Rows[0]["MenuId"].ToString()).Selected = true;
                }


                txtSortOrder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();
                txtFormPath.Text = ds.Tables[0].Rows[0]["FormPath"].ToString();
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
                if (hideYN == "Y")
                {
                    rdbHide_Y.Checked = true;
                    rdbHide_N.Checked = false;
                }
                else
                {
                    rdbHide_N.Checked = true;
                    rdbHide_Y.Checked = false;
                }
                if (PEP == "Y")
                {
                    rdb_PEP_Y.Checked = true;
                    rdb_PEP_N.Checked = false;
                }
                else
                {
                    rdb_PEP_N.Checked = true;
                    rdb_PEP_Y.Checked = false;
                }
                if (MAinSub == "Y")
                {
                    rdbHAVDOC_Y.Checked = true;
                    rdbHAVDOC_N.Checked = false;
                }
                else
                {
                    rdbHAVDOC_N.Checked = true;
                    rdbHAVDOC_Y.Checked = false;
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
            DataSet ds = dml.Find("select * from SET_Form WHERE ([Sno]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {


                ddlMenu.ClearSelection();


                txtMenuNAme.Text = ds.Tables[0].Rows[0]["FormTitle"].ToString();
                txtFormPath.Text = ds.Tables[0].Rows[0]["FormPath"].ToString();


                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());

                string hideYN = ds.Tables[0].Rows[0]["Hide"].ToString();

                string MAinSub = ds.Tables[0].Rows[0]["HaveDocNo"].ToString();
                string PEP = ds.Tables[0].Rows[0]["PEP_Exempt"].ToString();
                if (ds.Tables[0].Rows[0]["MenuId"].ToString() == "")
                {
                    ddlMenu.SelectedIndex = 0;
                }
                else {
                    ddlMenu.Items.FindByValue(ds.Tables[0].Rows[0]["MenuId"].ToString()).Selected = true;
                }


                txtSortOrder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();
                txtFormPath.Text = ds.Tables[0].Rows[0]["FormPath"].ToString();
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
                if (hideYN == "Y")
                {
                    rdbHide_Y.Checked = true;
                    rdbHide_N.Checked = false;
                }
                else
                {
                    rdbHide_N.Checked = true;
                    rdbHide_Y.Checked = false;
                }
                if (PEP == "Y")
                {
                    rdb_PEP_Y.Checked = true;
                    rdb_PEP_N.Checked = false;
                }
                else
                {
                    rdb_PEP_N.Checked = true;
                    rdb_PEP_Y.Checked = false;
                }
                if (MAinSub == "Y")
                {
                    rdbHAVDOC_Y.Checked = true;
                    rdbHAVDOC_N.Checked = false;
                }
                else
                {
                    rdbHAVDOC_N.Checked = true;
                    rdbHAVDOC_Y.Checked = false;
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

        txtMenuNAme.Enabled = true;
        
        chkActive.Enabled = true;
        rdbHide_N.Enabled = true;
        rdbHide_Y.Enabled = true;
        rdbHAVDOC_Y.Enabled = true;
        rdbHAVDOC_N.Enabled = true;
        rdb_PEP_N.Enabled = true;
        rdb_PEP_Y.Enabled = true;
        ddlMenu.Enabled = true;
        txtFormPath.Enabled = true;
        txtSortOrder.Enabled = true;
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
            DataSet ds = dml.Find("select * from SET_Form WHERE ([Sno]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {


                ddlMenu.ClearSelection();


                txtMenuNAme.Text = ds.Tables[0].Rows[0]["FormTitle"].ToString();
                txtFormPath.Text = ds.Tables[0].Rows[0]["FormPath"].ToString();


                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());

                string hideYN = ds.Tables[0].Rows[0]["Hide"].ToString();

                string MAinSub = ds.Tables[0].Rows[0]["HaveDocNo"].ToString();
                string PEP = ds.Tables[0].Rows[0]["PEP_Exempt"].ToString();
                if (ds.Tables[0].Rows[0]["MenuId"].ToString() == "")
                {
                    ddlMenu.SelectedIndex = 0;
                }
                else {
                    ddlMenu.Items.FindByValue(ds.Tables[0].Rows[0]["MenuId"].ToString()).Selected = true;
                }


                txtSortOrder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();
                txtFormPath.Text = ds.Tables[0].Rows[0]["FormPath"].ToString();
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
                if (hideYN == "Y")
                {
                    rdbHide_Y.Checked = true;
                    rdbHide_N.Checked = false;
                }
                else
                {
                    rdbHide_N.Checked = true;
                    rdbHide_Y.Checked = false;
                }
                if (PEP == "Y")
                {
                    rdb_PEP_Y.Checked = true;
                    rdb_PEP_N.Checked = false;
                }
                else
                {
                    rdb_PEP_N.Checked = true;
                    rdb_PEP_Y.Checked = false;
                }
                if (MAinSub == "Y")
                {
                    rdbHAVDOC_Y.Checked = true;
                    rdbHAVDOC_N.Checked = false;
                }
                else
                {
                    rdbHAVDOC_N.Checked = true;
                    rdbHAVDOC_Y.Checked = false;
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

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string id = e.Row.Cells[1].Text;
        bool flag = dml.isNumber(id);


        if (flag == true)
        {

            DataSet ds = dml.Find("select Sno,MLD from SET_Form where Sno = '" + id + "'");
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

            DataSet ds = dml.Find("select Sno,MLD from SET_Form where Sno = '" + id + "'");
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