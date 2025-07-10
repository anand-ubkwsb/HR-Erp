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
    int DateFrom, AddDays, DeleteDays, EditDays;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
    public string compidinsert;
    public string gocidinsert;
    string userid, UserGrpID, FormID;
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
            
          
            Showall_Dml();
            dml.dropdownsql(lblmenu, "SET_Menu", "Menu_title", "Sno");
            dml.dropdownsql(lblUserGrpName, "SET_UserGrp", "UserGrpName", "UserGrpId", "Record_Deleted","0");
            dml.dropdownsql(lblModule, "SET_Module", "ModuleDescription", "ModuleId");


            dml.dropdownsql(txtFind_MenuTitle, "SET_Menu", "Menu_title", "Sno");
            dml.dropdownsql(txtEdit_MenuTitle, "SET_Menu", "Menu_title", "Sno");
            dml.dropdownsql(txtDel_MenuTitle, "SET_Menu", "Menu_title", "Sno");


            dml.dropdownsql(txtDel_ModuleDesc, "SET_Module", "ModuleDescription", "ModuleId");
            dml.dropdownsql(txtFind_ModuleDesc, "SET_Module", "ModuleDescription", "ModuleId");
            dml.dropdownsql(txtEdit_Moduole, "SET_Module", "ModuleDescription", "ModuleId");

            dml.dropdownsql(txtFind_UserGrpName, "SET_UserGrp", "UserGrpName", "UserGrpId", "Record_Deleted", "0");
            dml.dropdownsql(txtEdit_UserGrpName, "SET_UserGrp", "UserGrpName", "UserGrpId", "Record_Deleted", "0");
            dml.dropdownsql(txtDel_UserGrpName, "SET_UserGrp", "UserGrpName", "UserGrpId", "Record_Deleted", "0");
            lblUserGrpName.SelectedIndex = 0;


            datamenu_view();
            selectcheck();
            


            textClear();


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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataSet ds_usergrpname = dml.Find("select UserGrpId from SET_UserGrp where UserGrpId= '" + lblUserGrpName.SelectedItem.Value + "'");
        string usergrpid = ds_usergrpname.Tables[0].Rows[0]["UserGrpId"].ToString();
        DataSet ds_usermenuid = dml.Find("select MenuId from SET_Menu where Sno = " + lblmenu.SelectedItem.Value + "");
        string menuid = ds_usermenuid.Tables[0].Rows[0]["MenuId"].ToString();

        DataSet ds_check = dml.Find("Select * from SET_UserGrp_Menu where MenuId = '" + menuid + "' and UserGrpId = '"+usergrpid+"' and Record_Deleted = 0 ");
        if (ds_check.Tables[0].Rows.Count > 0)
            {
               
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = " This Menu already exist!!";
            }
            else {
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
                  
                    //DataSet ds_FEndDate = dml.Find("");
                    dml.Insert("INSERT INTO SET_UserGrp_Menu ([UserGrpId], [ModuleId], [MenuId], [IsActive],[Hide], [SortOrder], [SysDate], [EnterrUserId],[MLD]) VALUES ( '" + usergrpid + "', '" + lblModule.SelectedItem.Value + "', '" + menuid + "', '" + chk + "', '" + hide + "', " + txtSortOrder.Text + ", '" + DateTime.Now + "', '" + userid + "','"+dml.Encrypt("h")+"');", "");
                    dml.Update("update Set_Menu set MLD = '" + dml.Encrypt("q") + "' where Sno = '" + lblmenu.SelectedItem.Value + "'", "");

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
        Div1.Visible = true;

        chkActive_Status.Checked = true;
        txtSortOrder.Enabled = true;
        rdb_Hide_N.Enabled = true;
        rdb_Hide_Y.Enabled = true;
        lblmenu.Enabled = true;
        lblUserGrpName.Enabled = true;
        lblModule.Enabled = true;
        lblSystemDate.Enabled = true;
        lblEntryUSer_Name.Enabled = true;
        rdb_Hide_N.Checked = true;
        chkActive_Status.Enabled = true;
        lblSystemDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff ");
        lblEntryUSer_Name.Text =  show_username();
        

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        DataSet ds_usergrpname = dml.Find("select UserGrpId from SET_UserGrp where UserGrpId= '" + lblUserGrpName.SelectedItem.Value + "'");
        string usergrpid = ds_usergrpname.Tables[0].Rows[0]["UserGrpId"].ToString();
        DataSet ds_usermenuid = dml.Find("select MenuId from SET_Menu where Sno = " + lblmenu.SelectedItem.Value + "");
        string menuid = ds_usermenuid.Tables[0].Rows[0]["MenuId"].ToString();


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
            string sorder;
            if(txtSortOrder.Text == "")
            {
                sorder = "([SortOrder] IS NULL)";
            }
            else
            {
                sorder = "([SortOrder] = '" + txtSortOrder.Text + "')";
            }
            //DataSet ds_FEndDate = dml.Find("");

            DataSet ds_up = dml.Find("SELECT * FROM SET_UserGrp_Menu WHERE ([Serial]='"+ViewState["sno"].ToString()+"') AND ([UserGrpId]='"+usergrpid+"') AND ([ModuleId]='"+lblModule.SelectedItem.Value+"') AND ([MenuId]='"+menuid+"') AND ([IsActive]='"+chk+"') AND ([Hide]='"+hide+"') AND "+sorder+"");
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

                dml.Update("UPDATE SET_UserGrp_Menu SET [UserGrpId]= '" + usergrpid + "' , [ModuleId]= '" + lblModule.SelectedItem.Value + "' , [MenuId] = '" + menuid + "' , [IsActive]= '" + chk + "' , [Hide]= '" + hide + "' , [SortOrder]= '" + txtSortOrder.Text + "' , [SysDate]= '" + DateTime.Now + "' , [EnterrUserId]= '" + userid + "' where Serial = " + ViewState["sno"].ToString() + " ", "");
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
            dml.Delete("update SET_UserGrp_Menu set Record_Deleted = 1 where Serial =  " + ViewState["sno"].ToString() + "", "");
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
        dml.dropdownsql(lblmenu, "SET_Menu", "Menu_title", "Sno");
        dml.dropdownsql(lblUserGrpName, "SET_UserGrp", "UserGrpName", "UserGrpId", "Record_Deleted", "0");
        dml.dropdownsql(lblModule, "SET_Module", "ModuleDescription", "ModuleId");
       
        textClear();
        btnInsert.Visible = true;
        btnDelete.Visible = true;
        btnUpdate.Visible = false;
        btnEdit.Visible = true;
        btnFind.Visible = true;
        btnSave.Visible = false;
        Div1.Visible = false;

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
        Div1.Visible = false;
        try
        {
            string swhere;
            UserGrpID = Request.QueryString["UsergrpID"];


            string squer = "select * from View_UserGrpMenu";

            if (txtEdit_MenuTitle.SelectedIndex != 0)
            {
                swhere = "Menu_title = '" + txtEdit_MenuTitle.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "Menu_title is not null";
            }
            if (txtEdit_Moduole.SelectedIndex != 0)
            {
                swhere = swhere + " and ModuleDescription = '" + txtEdit_Moduole.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and ModuleDescription is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY Menu_title";
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
        Div1.Visible = false;
        try
        {
            string swhere;
            UserGrpID = Request.QueryString["UsergrpID"];


            string squer = "select * from View_UserGrpMenu";

            if (txtFind_MenuTitle.SelectedIndex != 0)
            {
                swhere = "Menu_title = '" + txtFind_MenuTitle.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "Menu_title is not null";
            }
            if (txtFind_ModuleDesc.SelectedIndex != 0)
            {
                swhere = swhere + " and ModuleDescription = '" + txtFind_ModuleDesc.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and ModuleDescription is not null";
            }
            if (txtFind_UserGrpName.SelectedIndex != 0)
            {
                swhere = swhere + " and UserGrpName = '" + txtFind_UserGrpName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and UserGrpName is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY Menu_title";

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
        Div1.Visible = false;

        try
        {
            string swhere;
            UserGrpID = Request.QueryString["UsergrpID"];


            string squer = "select * from View_UserGrpMenu";

            if (txtDel_MenuTitle.SelectedIndex != 0)
            {
                swhere = "Menu_title = '" + txtDel_MenuTitle.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "Menu_title is not null";
            }
            if (txtDel_UserGrpName.SelectedIndex != 0)
            {
                swhere = swhere + " and UserGrpName = '" + txtDel_UserGrpName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and UserGrpName is not null";
            }
            if (txtDel_ModuleDesc.SelectedIndex != 0)
            {
                swhere = swhere + " and ModuleDescription like '" + txtDel_ModuleDesc.SelectedItem.Text + "%'";
            }
            else
            {
                swhere = swhere + " and ModuleDescription is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY Menu_title";

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

    public void textClear()
    {

        lblUserGrpName.SelectedIndex = 0;
        lblModule.SelectedIndex = 0;
        lblmenu.SelectedIndex = 0;
        chkActive_Status.Checked = false;
        rdb_Hide_Y.Checked = false;
        rdb_Hide_N.Checked = false;
        Label1.Text = "";
        txtSortOrder.Text = "";
        lblSystemDate.Text = "";
        lblEntryUSer_Name.Text = "";
        Div1.Visible = false;
        chkActive_Status.Checked = false;
        txtSortOrder.Enabled = false;
        rdb_Hide_N.Enabled = false;
        rdb_Hide_Y.Enabled = false;
        lblmenu.Enabled = false;
        lblUserGrpName.Enabled = false;
        lblModule.Enabled = false;
        lblSystemDate.Enabled = false;
        lblEntryUSer_Name.Enabled = false;
        chkActive_Status.Enabled = false;
       
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
        lblmenu.Enabled = true;
        lblUserGrpName.Enabled = true;
        lblModule.Enabled = true;
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
            DataSet ds = dml.Find("select UserGrpName, Menu_title,ModuleDescription, IsActive , Hide,SysDate from View_UserGrpMenu where Serial = " + serialno);
            //
            if (ds.Tables[0].Rows.Count > 0)

            {
                lblUserGrpName.ClearSelection();
                lblmenu.ClearSelection();
                lblModule.ClearSelection();
                //drpFunction.Items.FindByText(t).Selected = true;
                lblUserGrpName.Items.FindByText(ds.Tables[0].Rows[0]["UserGrpName"].ToString()).Selected = true;
                lblmenu.Items.FindByText(ds.Tables[0].Rows[0]["Menu_title"].ToString()).Selected = true;
                lblModule.Items.FindByText(ds.Tables[0].Rows[0]["ModuleDescription"].ToString()).Selected = true;

                //lblUserGrpName.SelectedItem.Text = ds.Tables[0].Rows[0]["UserGrpName"].ToString();
               // lblmenu.SelectedItem.Text = ds.Tables[0].Rows[0]["Menu_title"].ToString();
               // lblModule.SelectedItem.Text = ds.Tables[0].Rows[0]["ModuleDescription"].ToString();
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

                DataSet dds = dml.Find("select SortOrder, EnterrUserId from SET_UserGrp_Menu where Serial = " + serialno);
                txtSortOrder.Text = dds.Tables[0].Rows[0]["SortOrder"].ToString();
                string entruserid = dds.Tables[0].Rows[0]["EnterrUserId"].ToString();
                DataSet ddss = dml.Find("Select user_name from set_user_manager where userid= '" + entruserid + "'");
                lblEntryUSer_Name.Text = ddss.Tables[0].Rows[0]["user_name"].ToString();


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
            DataSet ds = dml.Find("select UserGrpName, Menu_title,ModuleDescription, IsActive , Hide,SysDate from View_UserGrpMenu where Serial = " + serialno );
            //
            if (ds.Tables[0].Rows.Count > 0)

            {
                lblUserGrpName.ClearSelection();
                lblmenu.ClearSelection();
                lblModule.ClearSelection();
                //drpFunction.Items.FindByText(t).Selected = true;
                lblUserGrpName.Items.FindByText(ds.Tables[0].Rows[0]["UserGrpName"].ToString()).Selected = true;
                lblmenu.Items.FindByText(ds.Tables[0].Rows[0]["Menu_title"].ToString()).Selected = true;
                lblModule.Items.FindByText(ds.Tables[0].Rows[0]["ModuleDescription"].ToString()).Selected = true;

                //lblUserGrpName.SelectedItem.Text= ds.Tables[0].Rows[0]["UserGrpName"].ToString();
               // lblmenu.SelectedItem.Text = ds.Tables[0].Rows[0]["Menu_title"].ToString();
                //lblModule.SelectedItem.Text = ds.Tables[0].Rows[0]["ModuleDescription"].ToString();
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

                DataSet dds = dml.Find("select SortOrder, EnterrUserId from SET_UserGrp_Menu where Serial = " + serialno);
                txtSortOrder.Text = dds.Tables[0].Rows[0]["SortOrder"].ToString();
                string entruserid = dds.Tables[0].Rows[0]["EnterrUserId"].ToString();
                DataSet ddss = dml.Find("Select user_name from set_user_manager where userid= '" + entruserid + "'");
                lblEntryUSer_Name.Text = ddss.Tables[0].Rows[0]["user_name"].ToString();


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
            string serialno = GridView2.SelectedRow.Cells[1].Text;
            ViewState["sno"] = serialno;
            DataSet ds = dml.Find("select UserGrpName, Menu_title,ModuleDescription, IsActive , Hide,SysDate from View_UserGrpMenu where Serial = " + serialno);
            //
            if (ds.Tables[0].Rows.Count > 0)

            {
                lblUserGrpName.ClearSelection();
                lblmenu.ClearSelection();
                lblModule.ClearSelection();
                //drpFunction.Items.FindByText(t).Selected = true;
                lblUserGrpName.Items.FindByText(ds.Tables[0].Rows[0]["UserGrpName"].ToString()).Selected = true;
                lblmenu.Items.FindByText(ds.Tables[0].Rows[0]["Menu_title"].ToString()).Selected = true;
                lblModule.Items.FindByText(ds.Tables[0].Rows[0]["ModuleDescription"].ToString()).Selected = true;

               // lblUserGrpName.SelectedItem.Text = ds.Tables[0].Rows[0]["UserGrpName"].ToString();
               // lblmenu.SelectedItem.Text = ds.Tables[0].Rows[0]["Menu_title"].ToString();
               // lblModule.SelectedItem.Text = ds.Tables[0].Rows[0]["ModuleDescription"].ToString();
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

                DataSet dds = dml.Find("select SortOrder, EnterrUserId from SET_UserGrp_Menu where Serial = " + serialno);
                txtSortOrder.Text = dds.Tables[0].Rows[0]["SortOrder"].ToString();
                string entruserid = dds.Tables[0].Rows[0]["EnterrUserId"].ToString();
                DataSet ddss = dml.Find("Select user_name from set_user_manager where userid= '" + entruserid + "'");
                lblEntryUSer_Name.Text = ddss.Tables[0].Rows[0]["user_name"].ToString();


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

    public string menuid(string menutitle)
    {
        try
        {
            userid = Request.QueryString["UserID"];
            string compid = "";
            string gocid = "";
            DataSet ds = dml.Find("select MenuId from Set_Menu where Menu_title='" + menutitle + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                compid = ds.Tables[0].Rows[0]["MenuId"].ToString();
                return compid;
            }
            else
            {
                return "";
            }
        }
        catch (Exception ex)
        {
            return "";
            Label1.Text = ex.Message;
        }
    }
    public string menuidsno(string menutitle)
    {
        try
        {
            userid = Request.QueryString["UserID"];
            string compid = "";
            string gocid = "";
            DataSet ds = dml.Find("select Sno from Set_Menu where Menu_title='" + menutitle + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                compid = ds.Tables[0].Rows[0]["Sno"].ToString();
                return compid;
            }
            else
            {
                return "";
            }
        }
        catch (Exception ex)
        {
            return "";
            Label1.Text = ex.Message;
        }
    }
    public string moduleid(string module)
    {
        try
        {
            userid = Request.QueryString["UserID"];
            string compid = "";
            string gocid = "";
            DataSet ds = dml.Find("select ModuleId,ModuleDescription from SET_Module where ModuleDescription='" + module + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                compid = ds.Tables[0].Rows[0]["ModuleId"].ToString();
                return compid;
            }
            else
            {
                return "1";
            }
        }
        catch (Exception ex)
        {
            return "";
            Label1.Text = ex.Message;
        }
    }

    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtdate = ((TextBox)e.Row.FindControl("txtAppleDate123"));
            txtdate.Attributes.Add("readonly", "readonly");
            e.Row.Attributes.Add("ondblclick", "__doPostBack('GridView4','Select$" + e.Row.RowIndex + "');");
        }
    }

    protected void GridView4_SelectedIndexChanged(object sender, EventArgs e)
    {
        int lbl = lblUserGrpName.SelectedIndex;
        
        textClear();
       lblUserGrpName.SelectedIndex = lbl;
        btnInsert.Visible = false;
        btnCancel.Visible = true;
        btnUpdate.Visible = true;
        btnDelete.Visible = false;
        btnSave.Visible = false;
        btnEdit.Visible = false;
        btnFind.Visible = false;
        Label1.Text = "";

        chkActive_Status.Checked = true;
        txtSortOrder.Enabled = true;
        rdb_Hide_N.Enabled = true;
        rdb_Hide_Y.Enabled = true;
        lblmenu.Enabled = true;
        lblUserGrpName.Enabled = true;
        lblModule.Enabled = true;
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
            UserGrpID = Request.QueryString["UsergrpID"];
            Label serialno = (Label)GridView4.SelectedRow.FindControl("lblNatureID");
            //string serialno = GridView4.SelectedRow.Cells[1].Text;
            ViewState["sno"] = serialno.Text;
            DataSet ds;
            if (lbl != 0)
            {
                 ds = dml.Find("select UserGrpName, Menu_title,ModuleDescription, IsActive , Hide,SysDate from View_UserGrpMenu where Sno = " + serialno.Text + " and UserGrpId = '"+lblUserGrpName.SelectedItem.Value+ "'  and Record_Deleted = '0'");
                //
            }
            else
            {
                ds = dml.Find("select UserGrpName, Menu_title,ModuleDescription, IsActive , Hide,SysDate from View_UserGrpMenu where Sno = " + serialno.Text + " and UserGrpId = '"+UserGrpID+ "' and Record_Deleted = '0'");
            }
            if (ds.Tables[0].Rows.Count > 0)

            {
                lblUserGrpName.ClearSelection();
                lblmenu.ClearSelection();
                lblModule.ClearSelection();
                //drpFunction.Items.FindByText(t).Selected = true;
                lblUserGrpName.Items.FindByText(ds.Tables[0].Rows[0]["UserGrpName"].ToString()).Selected = true;
                lblmenu.Items.FindByText(ds.Tables[0].Rows[0]["Menu_title"].ToString()).Selected = true;
                lblModule.Items.FindByText(ds.Tables[0].Rows[0]["ModuleDescription"].ToString()).Selected = true;

                //lblUserGrpName.SelectedItem.Text = ds.Tables[0].Rows[0]["UserGrpName"].ToString();
                // lblmenu.SelectedItem.Text = ds.Tables[0].Rows[0]["Menu_title"].ToString();
                // lblModule.SelectedItem.Text = ds.Tables[0].Rows[0]["ModuleDescription"].ToString();                                                                           
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

                DataSet dds = dml.Find("select SortOrder, EnterrUserId from SET_UserGrp_Menu where Sno = " + serialno.Text);
                txtSortOrder.Text = dds.Tables[0].Rows[0]["SortOrder"].ToString();
                string entruserid = dds.Tables[0].Rows[0]["EnterrUserId"].ToString();
                DataSet ddss = dml.Find("Select user_name from set_user_manager where userid= '" + entruserid + "'");
                lblEntryUSer_Name.Text = ddss.Tables[0].Rows[0]["user_name"].ToString();


            }
            else
            {

                textClear();
                Label1.Text = "Entry Not Inserted";
                Div1.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }

    }


    public void selectcheck()
    {
        string rdbY, MAin, apply;
        try
        {
            UserGrpID = Request.QueryString["UsergrpID"];
            DataSet ds;

            int aa = lblUserGrpName.SelectedIndex;

            if (lblUserGrpName.SelectedIndex != 0)
            {
                ds = dml.Find("select * from View_Set_Menu where UserGrpId = '" + lblUserGrpName.SelectedItem.Value + "' and Record_deleted = 0 order by Sno asc");
            }
            else
            {
                ds = dml.Find("select * from View_Set_Menu where UserGrpId = '" + UserGrpID + "' and Record_deleted = 0 order by Sno asc");
            }
            int countrow = ds.Tables[0].Rows.Count;
            if (countrow > 0)
            {

                for (int i = 0; i <= countrow - 1; i++)
                {
                    string val = ds.Tables[0].Rows[i]["Sno"].ToString();
                    rdbY = ds.Tables[0].Rows[i]["Hide"].ToString();
                    apply = ds.Tables[0].Rows[i]["CreatedDate"].ToString();
                    foreach (GridViewRow grow in GridView4.Rows)
                    {
                        CheckBox chk_del = (CheckBox)grow.FindControl("chkSelect");
                        RadioButton rdbyes = (RadioButton)grow.FindControl("rdb_HYes");
                        RadioButton rdbNo = (RadioButton)grow.FindControl("rdb_NYes");
                       
                        TextBox txtapply = (TextBox)grow.FindControl("txtAppleDate123");
                        Label lblID = (Label)grow.FindControl("lblNatureID");

                        if (lblID.Text == val)
                        {


                            if (rdbY == "True")
                            {
                                rdbyes.Checked = true;
                                rdbNo.Checked = false;
                            }
                            if (rdbY == "False")
                            {
                                rdbyes.Checked = false;
                                rdbNo.Checked = true;
                            }
                            txtapply.Text = dml.dateConvert(apply);
                            // dml.Insert("INSERT INTO [SET_BPartnerType] ([GocID], [BPartnerID], [BPNatureID], [SysDate]) VALUES  ('1', '" + ViewState["BPID"].ToString() + "', '" + lblID.Text + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "');", "");
                            chk_del.Checked = true;

                        }
                    }
                }


            }
            else {

                bool a = ((CheckBox)GridView4.HeaderRow.FindControl("chkall")).Checked;
                for (int i = 0; i < GridView4.Rows.Count; i++)
                {
                    TextBox txtdate = ((TextBox)GridView4.Rows[i].FindControl("txtAppleDate123"));
                    txtdate.Attributes.Add("readonly", "readonly");

                    ((CheckBox)GridView4.Rows[i].FindControl("chkSelect")).Checked = false;
                    txtdate.Text = "";
                    // selectcheck();

                }

            }


        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

    protected void chkall_CheckedChanged(object sender, EventArgs e)
    {
        int co = 0;
        int ab = 0;
        CheckBox chkall = (CheckBox)GridView4.HeaderRow.FindControl("chkall");
        bool a = ((CheckBox)GridView4.HeaderRow.FindControl("chkall")).Checked;
        for (int i = 0; i < GridView4.Rows.Count; i++)
        {
            TextBox txtdate = ((TextBox)GridView4.Rows[i].FindControl("txtAppleDate123"));
            txtdate.Attributes.Add("readonly", "readonly");
            if (a == true)
            {
                ((CheckBox)GridView4.Rows[i].FindControl("chkSelect")).Checked = true;
                co = co + 1;

            }
            if (a == false)
            {
                ((CheckBox)GridView4.Rows[i].FindControl("chkSelect")).Checked = false;
                ab = ab + 1;
                txtdate.Text = "";
                selectcheck();

            }


        }
        if (GridView4.Rows.Count == co)
        {
            chkall.Checked = true;
        }

        else if (GridView4.Rows.Count == ab)
        {
            for (int q = 0; q < ab; q++)
            {
                ((CheckBox)GridView4.Rows[q].FindControl("chkSelect")).Checked = false;
                selectcheck();
            }
        }
        else
        {
            selectcheck();

        }

    }


    protected void Button1_Click(object sender, EventArgs e)
    {


        //  selectcount();
        int count = 0;
        int uncount = 0;

        int ca = 0;
        int ca1 = 0;
        int un = 0;
        bool flag = true;

        UserGrpID = Request.QueryString["UsergrpID"];
        DataSet ds, ds1, ds2;
        if (lblUserGrpName.SelectedIndex != 0)
        {
            ds = dml.Find("select * from SET_UserGrp_Menu where UserGrpId = '" + lblUserGrpName.SelectedItem.Value + "' and Record_deleted = 0 order by Menu_Title");
            ds1 = dml.Find("select * from SET_UserGrp_Menu where UserGrpId = '" + lblUserGrpName.SelectedItem.Value + "' and Record_deleted = 1 order by Menu_Title");
        }
        else
        {
            ds = dml.Find("select * from SET_UserGrp_Menu where UserGrpId = '" + UserGrpID + "' and Record_deleted = 0 order by Menu_Title");
            ds1 = dml.Find("select * from SET_UserGrp_Menu where UserGrpId = '" + UserGrpID + "' and Record_deleted = 1 order by Menu_Title");
        }


        int countrow = ds.Tables[0].Rows.Count;
        int countdel = ds1.Tables[0].Rows.Count;
        if (countrow > 0)
        {
            string chkname;
            foreach (GridViewRow g in GridView4.Rows)
            {
                CheckBox chk_del = (CheckBox)g.FindControl("chkSelect");
                chkname = chk_del.Checked.ToString();
                chkname = chk_del.Text;

                Label lblID = (Label)g.FindControl("lblNatureID");
                if (chk_del.Checked == true)
                {
                    if (countdel > 0)
                    {
                        for (int i = 0; i <= countdel - 1; i++)
                        {
                            string val = ds1.Tables[0].Rows[i]["Sno"].ToString();
                            if (val == lblID.Text && chk_del.Checked == true)
                            {
                                ca1 = ca1 + 1;
                                if (lblUserGrpName.SelectedIndex != 0)
                                {
                                    dml.Update("update SET_UserGrp_Menu set Record_Deleted = '0' , UpdatedBy= '" + show_username() + "',UpdatedDate='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' where Sno = '" + lblID.Text + "' and UserGrpId = '" + lblUserGrpName.SelectedItem.Value + "';", "");
                                }
                                else
                                {
                                    dml.Update("update SET_UserGrp_Menu set Record_Deleted = '0' , UpdatedBy= '" + show_username() + "',UpdatedDate='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' where Sno = '" + lblID.Text + "' and UserGrpId = '" + UserGrpID + "';", "");
                                }
                                flag = false;

                            }


                            }
                    }
                    else
                    {
                        if (chk_del.Checked == true)
                        {

                            if (lblUserGrpName.SelectedIndex != 0)
                            {
                                ds2 = dml.Find("select * from SET_UserGrp_Menu where UserGrpId = '" + lblUserGrpName.SelectedItem.Value + "' and Sno = '" + lblID.Text + "' and Record_deleted = 0 order by Menu_Title");
                            }
                            else
                            {
                                ds2 = dml.Find("select * from SET_UserGrp_Menu where UserGrpId = '" + UserGrpID + "' and Sno = '" + lblID.Text + "' and Record_deleted = 0 order by Menu_Title");
                            }

                            if (ds2.Tables[0].Rows.Count > 0)
                            {


                            }
                            else {
                                //Insert
                                UserGrpID = Request.QueryString["UsergrpID"];
                                string mainsite = "", hideyn = "";
                                Label lbdocdes = (Label)g.FindControl("lbldoc");
                                Label lbdocname = (Label)g.FindControl("lbldocname");
                                TextBox applydate = (TextBox)g.FindControl("txtAppleDate123");
                               
                                RadioButton rdbHideY = (RadioButton)g.FindControl("rdb_HYes");
                                RadioButton rdbHideN = (RadioButton)g.FindControl("rdb_NYes");
                               
                                if (rdbHideY.Checked == true)
                                {
                                    hideyn = "Y";
                                }
                                if (rdbHideN.Checked == true)
                                {
                                    hideyn = "N";
                                }

                                string date = DateTime.Now.ToString("yyyy-MMM-dd"); ;//dml.dateconvertString(txtAppleDate.Text);

                                if (lblUserGrpName.SelectedIndex != 0)
                                {
                                    dml.Insert("INSERT INTO SET_UserGrp_Menu ([Sno],[UserGrpId], [ModuleId], [ModuleDescription], [MenuId], [Menu_Title], [IsActive], [Hide], [SortOrder], [SysDate], [EnterrUserId], [Record_Deleted], [CreatedBy], [CreatedDate]) VALUES ('"+menuidsno(lbdocdes.Text)+"' ,'" + lblUserGrpName.SelectedItem.Value + "', '" + moduleid(lbdocname.Text) +"', '"+lbdocname.Text+"', '"+menuid(lbdocdes.Text)+"', '"+lbdocdes.Text+"', '1', '"+hideyn+"', NULL, '"+DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")+"', '6B9C1166-0F4B-41DC-99E8-B47BE96C8157', '0', '"+show_username()+"', '"+DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")+"')", "");
                                }
                                else
                                {
                                    dml.Insert("INSERT INTO SET_UserGrp_Menu ([Sno],[UserGrpId], [ModuleId], [ModuleDescription], [MenuId], [Menu_Title], [IsActive], [Hide], [SortOrder], [SysDate], [EnterrUserId], [Record_Deleted], [CreatedBy], [CreatedDate]) VALUES ('" + menuidsno(lbdocdes.Text) + "','" + UserGrpID + "', '" + moduleid(lbdocname.Text) + "', '" + lbdocname.Text + "', '" + menuid(lbdocdes.Text) + "', '" + lbdocdes.Text + "', '1', '" + hideyn + "', NULL, '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '6B9C1166-0F4B-41DC-99E8-B47BE96C8157', '0', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "')", "");
                                }
                                Label1.Text = "data Inserted";
                                //insert

                            }

                        }
                    }
                }
                else
                {

                    for (int i = 0; i <= countrow - 1; i++)
                    {
                        string val = ds.Tables[0].Rows[i]["Sno"].ToString();
                        if (val == lblID.Text && chk_del.Checked == false)
                        {


                            if (lblUserGrpName.SelectedIndex != 0)
                            {
                                dml.Update("update SET_UserGrp_Menu set Record_Deleted = '1' , UpdatedBy= '" + show_username() + "',UpdatedDate='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' where Sno = '" + lblID.Text + "' and UserGrpId = '" + lblUserGrpName.SelectedItem.Value + "';", "");
                            }
                            else
                            {
                                dml.Update("update SET_UserGrp_Menu set Record_Deleted = '1' , UpdatedBy= '" + show_username() + "',UpdatedDate='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' where Sno = '" + lblID.Text + "' and UserGrpId = '" + UserGrpID + "';", "");
                            }
                            flag = false;
                        }


                    }
                    if (flag == false)
                    {
                        Label1.Text = "Updated Success";
                        GridView4.DataBind();
                        selectcheck();


                    }

                }


            }


        }
        else
        {

            foreach (GridViewRow g in GridView4.Rows)
            {
                CheckBox chk_del = (CheckBox)g.FindControl("chkSelect");
                Label lblID = (Label)g.FindControl("lblNatureID");
                UserGrpID = Request.QueryString["UsergrpID"];
                if (chk_del.Checked == true)
                {


                    if (lblUserGrpName.SelectedIndex != 0)
                    {
                        ds2 = dml.Find("select * from SET_UserGrp_Menu where UserGrpId = '" + lblUserGrpName.SelectedItem.Value + "' and Sno = '" + lblID.Text + "' and Record_deleted = 1 order by Menu_Title");
                    }
                    else
                    {
                        ds2 = dml.Find("select * from SET_UserGrp_Menu where UserGrpId = '" + UserGrpID + "' and Sno = '" + lblID.Text + "' and Record_deleted = 1 order by Menu_Title");
                    }

                    if (ds2.Tables[0].Rows.Count > 0)
                    {


                        if (lblUserGrpName.SelectedIndex != 0)
                        {
                            dml.Update("update SET_UserGrp_Menu set Record_Deleted = '0' , UpdatedBy= '" + show_username() + "',UpdatedDate='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' where Sno = '" + lblID.Text + "' and UserGrpId = '" + lblUserGrpName.SelectedItem.Value + "';", "");
                        }
                        else
                        {
                            dml.Update("update SET_UserGrp_Menu set Record_Deleted = '0' , UpdatedBy= '" + show_username() + "',UpdatedDate='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' where Sno = '" + lblID.Text + "' and UserGrpId = '" + UserGrpID + "';", "");
                        }
                        Label1.Text = "Updated Success";


                    }
                    else
                    {


                        int mainsite = 0, hideyn = 0;
                        Label lbdocdes = (Label)g.FindControl("lbldoc");
                        Label lbdocname = (Label)g.FindControl("lbldocname");
                        TextBox applydate = (TextBox)g.FindControl("txtAppleDate123");
                       
                        RadioButton rdbHideY = (RadioButton)g.FindControl("rdb_HYes");
                        RadioButton rdbHideN = (RadioButton)g.FindControl("rdb_NYes");
                      
                        if (rdbHideY.Checked == true)
                        {
                            hideyn = 1;
                        }
                        if (rdbHideN.Checked == true)
                        {
                            hideyn = 0;
                        }

                        string date = DateTime.Now.ToString("yyyy-MMM-dd"); ;//dml.dateconvertString(txtAppleDate.Text);

                        if (lblUserGrpName.SelectedIndex != 0)
                        {
                            dml.Insert("INSERT INTO SET_UserGrp_Menu ([Sno],[UserGrpId], [ModuleId], [ModuleDescription], [MenuId], [Menu_Title], [IsActive], [Hide], [SortOrder], [SysDate], [EnterrUserId], [Record_Deleted], [CreatedBy], [CreatedDate]) VALUES ('" + menuidsno(lbdocdes.Text) + "','" + lblUserGrpName.SelectedItem.Value + "', '" + moduleid(lbdocname.Text) + "', '" + lbdocname.Text + "', '" + menuid(lbdocdes.Text) + "', '" + lbdocdes.Text + "', '1', '" + hideyn + "', NULL, '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '6B9C1166-0F4B-41DC-99E8-B47BE96C8157', '0', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "')", "");
                        }
                        else
                        {
                            dml.Insert("INSERT INTO SET_UserGrp_Menu ([Sno],[UserGrpId], [ModuleId], [ModuleDescription], [MenuId], [Menu_Title], [IsActive], [Hide], [SortOrder], [SysDate], [EnterrUserId], [Record_Deleted], [CreatedBy], [CreatedDate]) VALUES ('" + menuidsno(lbdocdes.Text) + "','" + UserGrpID + "', '" + moduleid(lbdocname.Text) + "', '" + lbdocname.Text + "', '" + menuid(lbdocdes.Text) + "', '" + lbdocdes.Text + "', '1', '" + hideyn + "', NULL, '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '6B9C1166-0F4B-41DC-99E8-B47BE96C8157', '0', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "')", "");
                        }
                        Label1.Text = "data Inserted";
                    }
                }
            }
        }

        datamenu_view();
        GridView4.DataBind();
        selectcheck();
    }


    protected void lblUserGrpName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Text = "";
        datamenu_view();
        selectcheck();
    }
    public void datamenu_view()
    {
        UserGrpID = Request.QueryString["UsergrpID"];
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        try
        {
            cmd = new SqlCommand("sp_UserMenu", con);

            if (lblUserGrpName.SelectedIndex != 0)
            {
                cmd.Parameters.Add(new SqlParameter("@id", lblUserGrpName.SelectedItem.Value));
            }
            else
            {
                cmd.Parameters.Add(new SqlParameter("@id", UserGrpID));
            }


            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            da.Fill(dt);
            GridView4.DataSource = dt;
            GridView4.DataBind();
        }
        catch (Exception x)
        {

        }
        finally
        {
            cmd.Dispose();
            con.Close();
        }


    }

    public void ccc()
    {
        foreach (GridViewRow g in GridView4.Rows)
        {
            CheckBox chk = (CheckBox)g.FindControl("chkSelect");
            string text = chk.Checked.ToString();
        }
    }


    protected void Button2_Click(object sender, EventArgs e)
    {

        ccc();
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string id = e.Row.Cells[1].Text;
        bool flag = dml.isNumber(id);


        if (flag == true)
        {

            DataSet ds = dml.Find("select Serial,MLD from SET_UserGrp_Menu where Serial = '" + id + "'");
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

            DataSet ds = dml.Find("select Serial,MLD from SET_UserGrp_Menu where Serial = '" + id + "'");
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