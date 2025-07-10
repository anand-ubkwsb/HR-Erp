using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frm_SET_COA_Master : System.Web.UI.Page
{
    int DateFrom, DeleteDays, EditDays, AddDays;
   public string compidinsert;
    public string gocidinsert;
    string userid, UserGrpID, FormID; SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
   
    DmlOperation dml = new DmlOperation();
    FieldHide fd = new FieldHide();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            
            Findbox.Visible = false;
            btnUpdate.Visible = false;
            btnSave.Visible = false;
            userid = Request.QueryString["UserID"];
            UserGrpID = Request.QueryString["UsergrpID"];
            FormID = Request.QueryString["FormID"];
          
            Deletebox.Visible = false;
            Editbox.Visible = false;
            btnDelete_after.Visible = false;
            
            // fd.HideUSerGrpField(Page.Controls, UserGrpID, FormID, "SET_Fiscal_Year");
             Showall_Dml();
            dml.dropdownsql(lblModule, "SET_Module", "ModuleDescription", "ModuleId");
            dml.dropdownsql(lblUserGrpName, "SET_UserGrp", "UserGrpName", "sno");


            dml.dropdownsql(txtEdit_UserGrpName, "SET_UserGrp", "UserGrpName", "UserGrpId");
            dml.dropdownsql(txtFind_UserGrpName, "SET_UserGrp", "UserGrpName", "UserGrpId");
            dml.dropdownsql(txtDelete_UserGrpName, "SET_UserGrp", "UserGrpName", "UserGrpId");


            dml.dropdownsql(txtEdit_Module, "SET_Module", "ModuleDescription", "ModuleId");
            dml.dropdownsql(txtFind_Module, "SET_Module", "ModuleDescription", "ModuleId");
            dml.dropdownsql(txtDelete_Module, "SET_Module", "ModuleDescription", "ModuleId");

            textClear();


        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {       
            userid = Request.QueryString["UserID"];
            int chk = 0;
            if(chkActive_Status.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
            string hide = "";
            if (rdb_Hide_Y.Checked == true)
            {
                hide = "Y";
            }
            if(rdb_Hide_N.Checked == true)
            {
                hide = "N";
            }
            DataSet ds_usergrpname = dml.Find("select UserGrpId from SET_UserGrp where Sno = "+lblUserGrpName.SelectedItem.Value+"");
            string usergrpid = ds_usergrpname.Tables[0].Rows[0]["UserGrpId"].ToString();
            DataSet ds_userModuleuid = dml.Find("select ModuleId from SET_Module where ModuleId = " + lblModule.SelectedItem.Value + "");
            string moduleid = ds_userModuleuid.Tables[0].Rows[0]["ModuleId"].ToString();
            
            //DataSet ds_FEndDate = dml.Find("");
            dml.Insert("INSERT INTO SET_UserGrp_Module ([UserGrpId], [ModuleId], [IsActive], [Hide], [SortOrder], [SysDate], [EntryUserId],[Record_Deleted]) VALUES ('"+usergrpid+"', '"+ moduleid+ "', '"+chk+"', '"+hide+"', '"+txtSortOrder.Text+"', '"+DateTime.Now+"', '"+userid+"','0');", "");
              ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "alertme()", true);

                textClear();
                btnInsert.Visible = true;
                btnDelete.Visible = true;
                btnUpdate.Visible = false;
                btnEdit.Visible = true;
                btnFind.Visible = true;
                btnSave.Visible = false;
                btnDelete_after.Visible = false;
            btnInsert_Click(sender, e);

        }
        catch (Exception ex)
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = ex.Message;
        }
        UserGrpID = Request.QueryString["UsergrpID"];
        FormID = Request.QueryString["FormID"];
        Showall_Dml();


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

        chkActive_Status.Checked = true;
        txtSortOrder.Enabled = true;
        rdb_Hide_N.Enabled = true;
        rdb_Hide_Y.Enabled = true;
        lblModule.Enabled = true;
        rdb_Hide_N.Checked = true;
        
        lblUserGrpName.Enabled = true;
        lblSystemDate.Enabled = true;
        lblEntryUSer_Name.Enabled = true;
        chkActive_Status.Enabled = true;
        lblSystemDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff ");
        lblEntryUSer_Name.Text =  show_username();

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            userid = Request.QueryString["UserID"];
            int chk = 0;
            if (chkActive_Status.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
            string hide = "";
            if (rdb_Hide_Y.Checked == true)
            {
                hide = "Y";
            }
            if (rdb_Hide_N.Checked == true)
            {
                hide = "N";
            }

            DataSet ds_usergrpname = dml.Find("select UserGrpId from SET_UserGrp where Sno= " + lblUserGrpName.SelectedItem.Value + "");
            string usergrpid = ds_usergrpname.Tables[0].Rows[0]["UserGrpId"].ToString();

            DataSet ds_userModuleuid = dml.Find("select ModuleId from SET_Module where ModuleId = " + lblModule.SelectedItem.Value + "");
            string moduleid = ds_userModuleuid.Tables[0].Rows[0]["ModuleId"].ToString();

            DataSet ds_up = dml.Find("select * from SET_UserGrp_Module  WHERE ([Serial]='"+ ViewState["sno"].ToString() + "') AND ([UserGrpId]='"+usergrpid+"') AND ([ModuleId]='"+lblModule.SelectedItem.Value+"') AND ([IsActive]='"+chk+"') AND ([Hide]='"+hide+"') AND ([SortOrder]='"+txtSortOrder.Text+"')");

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



            

                dml.Update("UPDATE SET_UserGrp_Module SET [UserGrpId]='" + usergrpid + "', [ModuleId]='" + moduleid + "', [IsActive]='" + chk + "', [Hide]='" + hide + "', [SortOrder]='" + txtSortOrder.Text + "', [SysDate]='" + DateTime.Now + "', [EntryUserId]='" + userid + "' WHERE Serial ='" + ViewState["sno"].ToString() + "'", "");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "Editalert()", true);

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
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = ex.Message;
        }
        UserGrpID = Request.QueryString["UsergrpID"];
        FormID = Request.QueryString["FormID"];
        Showall_Dml();


    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            dml.Delete("update SET_UserGrp_Module set Record_Deleted = 1 where Serial = " + ViewState["sno"].ToString() + "", "");
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
        UserGrpID = Request.QueryString["UsergrpID"];
        FormID = Request.QueryString["FormID"];
        Showall_Dml();



    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        dml.dropdownsql(lblUserGrpName, "SET_UserGrp", "UserGrpName", "sno");
        dml.dropdownsql(lblModule, "SET_Module", "ModuleDescription", "ModuleId");
        textClear();
        btnInsert.Visible = true;
        btnDelete.Visible = true;
        btnUpdate.Visible = false;
        btnEdit.Visible = true;
        btnFind.Visible = true;
        btnSave.Visible = false;

        btnDelete_after.Visible = false;

        Deletebox.Visible = false;
        Editbox.Visible = false;
        Findbox.Visible = false;
        fieldbox.Visible = true;
        UserGrpID = Request.QueryString["UsergrpID"];
        FormID = Request.QueryString["FormID"];
        Showall_Dml();



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
            string swhere;
            string squer = "Select * from View_UserGrpModule";

            if (txtEdit_Module.SelectedIndex != 0)
            {
                swhere = "ModuleDescription = '" + txtEdit_Module.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "ModuleDescription is not null";
            }
            if (txtEdit_UserGrpName.SelectedIndex != 0)
            {
                swhere = swhere + " and UserGrpName = '" + txtEdit_UserGrpName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and UserGrpName is not null";
            }
            if (ChkEdit_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (ChkEdit_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }
            else
            {
                swhere = swhere + " and IsActive is not null";
            }
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY ModuleDescription";
            Findbox.Visible = false;
            Deletebox.Visible = false;
            Editbox.Visible = true;
            fieldbox.Visible = false;

           
            DataSet dgrid = dml.grid(squer);
            GridView3.DataSource = dgrid;
            GridView3.DataBind();
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
            string swhere;
            string squer = "Select * from View_UserGrpModule";

            if (txtFind_Module.SelectedIndex != 0)
            {
                swhere = "ModuleDescription = '" + txtFind_Module.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "ModuleDescription is not null";
            }
            if (txtFind_UserGrpName.SelectedIndex != 0)
            {
                swhere = swhere + " and UserGrpName = '" + txtFind_UserGrpName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and UserGrpName is not null";
            }
            if (ChkFInd_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (ChkFInd_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }
            else
            {
                swhere = swhere + " and IsActive is not null";
            }
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY ModuleDescription";

            Findbox.Visible = true;
            Deletebox.Visible = false;
            Editbox.Visible = false;
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

    protected void btn_delete_Click(object sender, EventArgs e)
    {
        Deletebox.Visible = true;
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
            string swhere;
            string squer = "Select * from View_UserGrpModule";

            if (txtDelete_Module.SelectedIndex != 0)
            {
                swhere = "ModuleDescription = '" + txtDelete_Module.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "ModuleDescription is not null";
            }
            if (txtDelete_UserGrpName.SelectedIndex != 0)
            {
                swhere = swhere + " and UserGrpName = '" + txtDelete_UserGrpName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and UserGrpName is not null";
            }
            if (chkDelete_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkDelete_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }
            else
            {
                swhere = swhere + " and IsActive is not null";
            }
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY ModuleDescription";

            Findbox.Visible = false;
            fieldbox.Visible = false;
            Editbox.Visible = false;
            Deletebox.Visible = true;

            DataSet dgrid = dml.grid(squer);
            GridView2.DataSource = dgrid;
            GridView2.DataBind();
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }

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
    public void textClear()
    {

        lblUserGrpName.SelectedIndex = 0;
        lblModule.SelectedIndex = 0;
        
        chkActive_Status.Checked = false;
        rdb_Hide_Y.Checked = false;
        rdb_Hide_N.Checked = false;
        Label1.Text = "";
        txtSortOrder.Text = "";
        lblSystemDate.Text = "";
        lblEntryUSer_Name.Text = "";
        
       
        chkActive_Status.Checked = false;
        txtSortOrder.Enabled = false;
        rdb_Hide_N.Enabled = false;
        rdb_Hide_Y.Enabled = false;
        
        lblUserGrpName.Enabled = false;
        lblModule.Enabled = false;
        lblSystemDate.Enabled = false;
        lblEntryUSer_Name.Enabled = false;
        chkActive_Status.Enabled = false;


    }

    public string show_username()
    {
        userid = Request.QueryString["UserID"];
        return userid;
    }


    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        btnInsert.Visible = false;
        btnCancel.Visible = true;
        btnUpdate.Visible = false;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnFind.Visible = true;
         textClear();


        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {

            string serialno = GridView1.SelectedRow.Cells[1].Text;
            ViewState["sno"] = serialno;
            lblUserGrpName.ClearSelection();
            lblModule.ClearSelection();

            DataSet ds = dml.Find("Select * from SET_UserGrp_Module where Serial = " + serialno );
            //
            if (ds.Tables[0].Rows.Count > 0)

            {
                DataSet dds = dml.Find("select * from View_UserGrpModule where Serial = " + serialno);
                
                lblUserGrpName.Items.FindByText(dds.Tables[0].Rows[0]["UserGrpName"].ToString()).Selected = true;
                lblModule.Items.FindByText(dds.Tables[0].Rows[0]["ModuleDescription"].ToString()).Selected = true ;
                
                DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["SysDate"].ToString());
                
               

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                string hide = ds.Tables[0].Rows[0]["Hide"].ToString();

                lblSystemDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

                if (chk == true)
                {
                    chkActive_Status.Checked = true;
                }
                else
                {
                    chkActive_Status.Checked = false;
                }
                if (hide == "N")
                {
                    rdb_Hide_N.Checked = true;
                }
                else
                {
                    rdb_Hide_Y.Checked = true;
                }


                txtSortOrder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();
                string entruserid = ds.Tables[0].Rows[0]["EntryUserId"].ToString();
                DataSet ddss = dml.Find("Select user_name from set_user_manager where userid= '" + entruserid + "'");
                lblEntryUSer_Name.Text = dml.show_usernameFED(ddss.Tables[0].Rows[0]["user_name"].ToString());


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

        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        Deletebox.Visible = false;
        try
        {
            lblUserGrpName.ClearSelection();
            lblModule.ClearSelection();
            string serialno = GridView2.SelectedRow.Cells[1].Text;
            ViewState["sno"] = serialno;

            DataSet ds = dml.Find("Select * from SET_UserGrp_Module where Serial = " + serialno);
            //
            if (ds.Tables[0].Rows.Count > 0)

            {
                DataSet dds = dml.Find("select * from View_UserGrpModule where Serial = " + serialno);
                // [], [SortOrder], [SysDate], [EnterUserId])
                lblUserGrpName.Items.FindByText(dds.Tables[0].Rows[0]["UserGrpName"].ToString()).Selected = true;
                lblModule.Items.FindByText(dds.Tables[0].Rows[0]["ModuleDescription"].ToString()).Selected = true;

                DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["SysDate"].ToString());

               
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                string hide = ds.Tables[0].Rows[0]["Hide"].ToString();

                lblSystemDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

                if (chk == true)
                {
                    chkActive_Status.Checked = true;
                }
                else
                {
                    chkActive_Status.Checked = false;
                }
                if (hide == "N")
                {
                    rdb_Hide_N.Checked = true;
                }
                else
                {
                    rdb_Hide_Y.Checked = true;
                }


                txtSortOrder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();
                string entruserid = ds.Tables[0].Rows[0]["EntryUserId"].ToString();
                DataSet ddss = dml.Find("Select user_name from set_user_manager where userid= '" + entruserid + "'");
                lblEntryUSer_Name.Text = dml.show_usernameFED(ddss.Tables[0].Rows[0]["user_name"].ToString());


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

        chkActive_Status.Checked = true;
        txtSortOrder.Enabled = true;
        rdb_Hide_N.Enabled = true;
        rdb_Hide_Y.Enabled = true;
        lblModule.Enabled = true;
        lblUserGrpName.Enabled = true;
        
        lblSystemDate.Enabled = true;
        lblEntryUSer_Name.Enabled = true;
       
        chkActive_Status.Enabled = true;
        lblSystemDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff ");
        lblEntryUSer_Name.Text = show_username();


        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;

        try
        {

            string serialno = GridView3.SelectedRow.Cells[1].Text;

            ViewState["sno"] = serialno;
            DataSet ds = dml.Find("Select * from SET_UserGrp_Module where Serial = " + serialno);
            //
            if (ds.Tables[0].Rows.Count > 0)

            {
                lblUserGrpName.ClearSelection();
                lblModule.ClearSelection();
                DataSet dds = dml.Find("select * from View_UserGrpModule where Serial = " + serialno);
                // [], [SortOrder], [SysDate], [EnterUserId])
                lblUserGrpName.Items.FindByText(dds.Tables[0].Rows[0]["UserGrpName"].ToString()).Selected = true;
                lblModule.Items.FindByText(dds.Tables[0].Rows[0]["ModuleDescription"].ToString()).Selected = true;

                DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["SysDate"].ToString());

                

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                string hide = ds.Tables[0].Rows[0]["Hide"].ToString();

                lblSystemDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

                if (chk == true)
                {
                    chkActive_Status.Checked = true;
                }
                else
                {
                    chkActive_Status.Checked = false;
                }
                if (hide == "N")
                {
                    rdb_Hide_N.Checked = true;
                }
                else
                {
                    rdb_Hide_Y.Checked = true;
                }


                txtSortOrder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();
                string entruserid = ds.Tables[0].Rows[0]["EntryUserId"].ToString();
                DataSet ddss = dml.Find("Select user_name from set_user_manager where userid= '" + entruserid + "'");
                lblEntryUSer_Name.Text = dml.show_usernameFED(ddss.Tables[0].Rows[0]["user_name"].ToString());


            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
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
    
}