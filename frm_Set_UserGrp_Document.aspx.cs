using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
            rad1.Visible = false;
            rad2.Visible = false;
            rad3.Visible = false;

            updatecol.Visible = false;
            btnUpdate.Visible = false;
            btnSave.Visible = false;
            btnDelete_after.Visible = false;
            userid = Request.QueryString["UserID"];
            UserGrpID = Request.QueryString["UsergrpID"];
            FormID = Request.QueryString["FormID"];
            fiscal = Request.QueryString["fiscaly"];

            dml.daterangeforfiscal(CalendarExtender1, Request.Cookies["fiscalyear"].Value);
            txtAppleDate.Attributes.Add("readonly", "readonly");
            
            fd.HideUSerGrpField(Page.Controls, UserGrpID, FormID, "SET_ItemSubHead");
            
            dml.dropdownsql(ddlDocument, "SET_Documents", "DocDescription", "DocID", "DocDescription");
            dml.dropdownsql(ddlUsergrp, "SET_UserGrp", "UserGrpName", "UserGrpId", "Record_Deleted","0");

            dml.dropdownsql(ddlFind_USerGrpName, "SET_UserGrp", "UserGrpName", "UserGrpId", "UserGrpName");
            dml.dropdownsql(ddlEdit_USERGRP, "SET_UserGrp", "UserGrpName", "UserGrpId", "UserGrpName");
            dml.dropdownsql(ddlDel_UserGRP, "SET_UserGrp", "UserGrpName", "UserGrpId", "UserGrpName");

            dml.dropdownsql(ddlEdit_DocName, "SET_Documents", "DocDescription", "DocID", "DocDescription");
            dml.dropdownsql(ddlFind_DOCNAme, "SET_Documents", "DocDescription", "DocID", "DocDescription");
            dml.dropdownsql(ddlDel_DocNAme, "SET_Documents", "DocDescription", "DocID", "DocDescription");
            dml.dropdownsqldateF(ddlStartDate, "SET_Fiscal_Year", "convert(varchar, StartDate, 106)", "stdate", "FiscalYearID", "StartDate");
            dml.dropdownsqldateF(ddlEndDate, "SET_Fiscal_Year", "convert(varchar, EndDate, 106)", "eddate", "FiscalYearID", "EndDate");

            textClear();
            ddlUsergrp.SelectedIndex = 0;
           
            Showall_Dml();
           
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
        Div1.Visible = true;

        ddlUsergrp.Enabled = true;
        txtDocumentDesc.Enabled = true;
        txtAppleDate.Enabled = true;
        rdb_Site.Enabled = true;
        rdb_Main.Enabled = true;
        rdb_Hide_Y.Enabled = true;
        rdb_Hide_N.Enabled = true;
        ddlDocument.Enabled = true;
        rdb_Main.Checked = true;
        rdb_Hide_N.Checked = true;
        txtSortOrder.Enabled = true;
        ddlStartDate.Enabled = true;
        ddlEndDate.Enabled = true;

        txtCreatedBy.Enabled = false;
        txtcreatedDate.Enabled = false;
        txtUpdatedDate.Enabled = false;
        txtUpdatedBy.Enabled = false;
        chkActive.Enabled =true;
       
        chkActive.Checked =true;
        datamenu_view();
        datacall();
        selectcheck();
        txtcreatedDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

        userid = Request.QueryString["UserID"];
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
            int chk, main_site, hide;

            if (chkActive.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
            if (rdb_Hide_Y.Checked == true)
            {
                hide = 1;
            }
            else
            {
                hide = 0;
            }

            if (rdb_Main.Checked == true)
            {
                main_site = 1;
            }
            else
            {
                main_site = 0;
            }
            bool flags = false;
          
                string st_date = dml.dateconvertString(ddlStartDate.SelectedItem.Text);
                string ed_date;
                if (ddlEndDate.SelectedIndex != 0)
                {
                    ed_date = dml.dateconvertString(ddlEndDate.SelectedItem.Text);
                }
                else
                {
                    ed_date = "";

                }
                //select * from SET_UserGrp_Documents where DocID = 1
                DataSet uniqueg_B_C = dml.Find("select EndDate from SET_UserGrp_Documents where DocID = '" + ddlDocument.SelectedItem.Value + "'");
                if (uniqueg_B_C.Tables[0].Rows.Count > 0)
                {
                    string ed = uniqueg_B_C.Tables[0].Rows[0]["EndDate"].ToString();
                    if (string.IsNullOrEmpty(ed))
                    {
                        DataSet dsS = dml.Find("select * from SET_UserGrp_Documents where StartDate < = '" + st_date + "' AND DocID = '" + ddlDocument.SelectedItem.Value + "'");
                        if (dsS.Tables[0].Rows.Count > 0)
                        {
                            Label1.ForeColor = System.Drawing.Color.Red;
                            Label1.Text = "Duplicated entry not allowed";
                        }

                    }
                    else
                    {
                        string edate = "0";

                        DataSet ds_id = dml.Find("select StartDate from SET_UserGrp_Documents where DocID = '" + ddlDocument.SelectedItem.Value + "'");
                        if (ds_id.Tables[0].Rows.Count > 0)
                        {

                            for (int a = 0; a < ds_id.Tables[0].Rows.Count; a++)
                            {
                                edate = ds_id.Tables[0].Rows[a]["StartDate"].ToString();


                                DateTime dst = DateTime.Parse(st_date);
                                DateTime Sst = DateTime.Parse(edate);

                                if (Sst == dst)
                                {
                                    Label1.ForeColor = System.Drawing.Color.Red;
                                    Label1.Text = "this Start Date aleardy inserted";
                                    flags = false;
                                }
                                else if (dst > Sst)
                                {
                                    flags = true;
                                }
                                else
                                {
                                    Label1.ForeColor = System.Drawing.Color.Red;
                                    Label1.Text = "this Start Date aleardy inserted OK";
                                    flags = false;
                                }
                            }

                        }

                    }
                }
                else
                {
                    //string imgpath = "~/dist/img/" + Path.GetFileName(FileUpload1.FileName);
                dml.Insert("INSERT INTO [SET_UserGrp_Documents] ([UserGrpId], [DocDescription], [Main_Site], [ApplyDate], [IsActive], [IsHide], [SortOrder],[CreatedBy],[CreateDate] ,[Record_Deleted],[DocID], [StartDate], [EndDate]) VALUES ('" + ddlUsergrp.SelectedItem.Value + "','" + txtDocumentDesc.Text + "', '" + main_site + "', '" + dml.dateconvertforinsert(txtAppleDate) + "', '" + chk + "', '" + hide + "', '" + txtSortOrder.Text + "', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0','" + ddlDocument.SelectedItem.Value + "','"+st_date+"','"+ed_date+"')", "alertme()");
                dml.Update("update Set_Documents set MLD = '" + dml.Encrypt("q") + "' where DocID = '" + ddlDocument.SelectedItem.Value + "'", "");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);

                    Label1.Text = "";

                    txtDocumentDesc.Text = "";
                    ddlDocument.SelectedIndex = 0;
                    txtAppleDate.Text = "";
                    txtSortOrder.Text = "";
                    txtCreatedBy.Text = "";
                    txtcreatedDate.Text = "";
                    txtUpdatedDate.Text = "";
                    txtUpdatedBy.Text = "";


                }
            
            if (flags == true)
            {
                //string imgpath = "~/dist/img/" + Path.GetFileName(FileUpload1.FileName);
                dml.Insert("INSERT INTO [SET_UserGrp_Documents] ([UserGrpId], [DocDescription], [Main_Site], [ApplyDate], [IsActive], [IsHide], [SortOrder],[CreatedBy],[CreateDate] ,[Record_Deleted],[DocID], [StartDate], [EndDate],[MLD]) VALUES ('" + ddlUsergrp.SelectedItem.Value + "','" + txtDocumentDesc.Text + "', '" + main_site + "', '" + dml.dateconvertforinsert(txtAppleDate) + "', '" + chk + "', '" + hide + "', '" + txtSortOrder.Text + "', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '0','" + ddlDocument.SelectedItem.Value + "','"+st_date+"','"+ed_date+"','"+dml.Encrypt("h")+"')", "alertme()");
                dml.Update("update Set_Documents set MLD = '" + dml.Encrypt("q") + "' where DocID = '" + ddlDocument.SelectedItem.Value + "'", "");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);

                Label1.Text = "";
               
                txtDocumentDesc.Text = "";
                ddlDocument.SelectedIndex = 0;
                txtAppleDate.Text = "";
                txtSortOrder.Text = "";
                txtCreatedBy.Text = "";
                txtcreatedDate.Text = "";
                txtUpdatedDate.Text = "";
                txtUpdatedBy.Text = "";
            }

            }

            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
            UserGrpID = Request.QueryString["UsergrpID"];
            FormID = Request.QueryString["FormID"];
            datacall();
            Showall_Dml();
        }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            int chk, main_site, hide;
            string docname;

            if (chkActive.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
            if (rdb_Hide_Y.Checked == true)
            {
                hide = 1;
            }
            else
            {
                hide = 0;
            }

            if (rdb_Main.Checked == true)
            {
                main_site = 1;
            }
            else
            {
                main_site = 0;
            }


            string app_date = txtAppleDate.Text;
            string st_date = dml.dateconvertString(ddlStartDate.SelectedItem.Text);
            string ed_date;
            if (ddlEndDate.SelectedIndex != 0)
            {
                ed_date = dml.dateconvertString(ddlEndDate.SelectedItem.Text);
            }
            else
            {
                ed_date = "1900-01-01";

            }

            DataSet ds_up = dml.Find("select * from SET_UserGrp_Documents  WHERE ([Sno]='"+ViewState["SNO"].ToString()+"') AND ([UserGrpId]='"+ddlUsergrp.SelectedItem.Value+"') AND ([DocID]='"+ddlDocument.SelectedItem.Value+"') AND ([DocDescription]='" + txtDocumentDesc.Text + "') AND ([Main_Site]='" + main_site + "') AND ([ApplyDate]='" + app_date +"') AND([IsActive] = '"+chk+"') AND([IsHide] = '"+hide+"') AND([SortOrder] = '"+txtSortOrder.Text+ "') AND ([StartDate] = '"+st_date+"') AND ([EndDate]= '"+ ed_date + "')");

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
                //DataSet ds_up1 = dml.Find("SELECT DocDescription from SET_UserGrp_Documents where UserGrpId='"+ddlUsergrp.SelectedItem.Value+ "' and DocDescription='"+txtDocumentDesc.Text+"'");


                //if (ds_up1.Tables[0].Rows.Count>0)
                //{
                //    Label1.Text = "Document description already exist";
                //}
                //else
                //{ 

               
                //string imgpath = "~/dist/img/" + Path.GetFileName(FileUpload1.FileName);
                userid = Request.QueryString["UserID"];
                    txtUpdatedBy.Text = show_username();
                    dml.Update("UPDATE [SET_UserGrp_Documents] SET [DocID] = '" + ddlDocument.SelectedItem.Value + "', [UserGrpId]='" + ddlUsergrp.SelectedItem.Value + "',  [DocDescription]='" + txtDocumentDesc.Text + "', [Main_Site]='" + main_site + "', [ApplyDate]='" + dml.dateconvertforinsert(txtAppleDate) + "', [IsActive]='" + chk + "', [IsHide]='" + hide + "', [SortOrder]='" + txtSortOrder.Text + "', [StartDate] = '" + ddlStartDate.SelectedItem.Text + "' , [EndDate]= '" + ed_date + "', [Record_Deleted]='0', [UpdatedBy]='" + show_username() + "', [UpdateDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' WHERE ([Sno]='" + ViewState["SNO"].ToString() + "')", "");
                    ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", " Editalert()", true);

                    textClear();
                    btnInsert.Visible = true;
                    btnDelete.Visible = true;
                    btnUpdate.Visible = false;
                    btnEdit.Visible = true;
                    btnFind.Visible = true;
                    btnSave.Visible = false;
                    btnDelete_after.Visible = false;
                //}
            }
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
        btnInsert.Visible = true;
        updatecol.Visible = false;
        btnDelete.Visible = true;
        btnUpdate.Visible = false;
        btnEdit.Visible = true;
        btnFind.Visible = true;
        btnSave.Visible = false;
        btnDelete_after.Visible = false;
        Div1.Visible = false;
        textClear();
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
        Div1.Visible = false;
        try
        {
            UserGrpID = Request.QueryString["UsergrpID"];
            GridView2.DataBind();
            string swhere;
            string squer = "select * from View_SETUSERGRPDOC";

            if (ddlDel_UserGRP.SelectedIndex != 0)
            {
                swhere = "UserGrpId = '" + ddlDel_UserGRP.SelectedItem.Value + "'";
            }
            else
            {
                swhere = "UserGrpId is not null";
            }
            if (ddlDel_DocNAme.SelectedIndex != 0)
            {
                swhere = swhere + " and DocDescription = '" + ddlDel_DocNAme.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + "  and DocDescription is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0  ORDER BY DocDescription";


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
        Div1.Visible = false;
        try
        {

            UserGrpID = Request.QueryString["UsergrpID"];
            GridView1.DataBind();
            string swhere;

            string squer = "select * from View_SETUSERGRPDOC";

            if (ddlFind_USerGrpName.SelectedIndex != 0)
            {
                swhere = "UserGrpId = '" + ddlFind_USerGrpName.SelectedItem.Value + "'";
            }
            else
            {
                swhere = "UserGrpId is not null";
            }
            if (ddlFind_DOCNAme.SelectedIndex != 0)
            {
                swhere = swhere + " and DocDescription = '" + ddlFind_DOCNAme.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + "  and DocDescription is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0   ORDER BY DocDescription";

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
        Div1.Visible = false;
        try
        {
            UserGrpID = Request.QueryString["UsergrpID"];
            GridView3.DataBind();
            string swhere;
            string squer = "select * from View_SETUSERGRPDOC";

            if (ddlEdit_USERGRP.SelectedIndex != 0)
            {
                swhere = "UserGrpId = '" + ddlEdit_USERGRP.SelectedItem.Value + "'";
            }
            else
            {
                swhere = "UserGrpId is not null";
            }
            if (ddlEdit_DocName.SelectedIndex != 0)
            {
                swhere = swhere + " and DocDescription = '" + ddlEdit_DocName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + "  and DocDescription is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0   ORDER BY DocDescription";


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
        Div1.Visible = false;


        ddlDocument.Enabled = false;
        ddlUsergrp.Enabled = false;
        txtDocumentDesc.Enabled = false;
        txtAppleDate.Enabled = false;
        rdb_Site.Enabled = false;
        rdb_Main.Enabled = false;
        rdb_Hide_Y.Enabled = false;
        rdb_Hide_N.Enabled = false;
        ddlStartDate.Enabled = false;
        ddlEndDate.Enabled = false;

        ddlDocument.SelectedIndex = 0;
        ddlUsergrp.SelectedIndex = 0;
        txtCreatedBy.Enabled = false;
        txtcreatedDate.Enabled = false;
        txtUpdatedDate.Enabled = false;
        txtUpdatedBy.Enabled = false;
        txtSortOrder.Enabled = false;
        chkActive.Enabled = false;
        Label1.Text = "";

        txtDocumentDesc.Text = "";
        txtAppleDate.Text = "";
        txtSortOrder.Text = "";
        txtCreatedBy.Text = "";
        txtcreatedDate.Text = "";
        txtUpdatedDate.Text = "";
        txtUpdatedBy.Text = "";

        rdb_Site.Checked = false;
        rdb_Main.Checked = false;
        rdb_Hide_Y.Checked = false;
        rdb_Hide_N.Checked = false;
        ddlStartDate.SelectedIndex = 0;
        ddlEndDate.SelectedIndex = 0;

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
            dml.Delete("update SET_UserGrp_Documents set Record_Deleted = 1 where [Sno] = " + ViewState["SNO"].ToString() + "", "");
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
            DataSet ds = dml.Find("select * from SET_UserGrp_Documents where Sno = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDocument.ClearSelection();
                ddlUsergrp.ClearSelection();
                ddlUsergrp.Items.FindByValue(ds.Tables[0].Rows[0]["UserGrpId"].ToString()).Selected = true;
                ddlDocument.Items.FindByValue(ds.Tables[0].Rows[0]["DocID"].ToString()).Selected = true;
                txtDocumentDesc.Text = ds.Tables[0].Rows[0]["DocDescription"].ToString();
                txtAppleDate.Text = ds.Tables[0].Rows[0]["ApplyDate"].ToString();
                dml.dateConvert(txtAppleDate);
                txtSortOrder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();

                ddlStartDate.ClearSelection();
                ddlEndDate.ClearSelection();
                string sd = dml.dateConvert(ds.Tables[0].Rows[0]["StartDate"].ToString());
                string ed = ds.Tables[0].Rows[0]["EndDate"].ToString();

                string fst = sd.Replace('-', ' ');
               

                ddlStartDate.Items.FindByText(fst).Selected = true;

                if (ed != "")
                {
                    // dml.dateConvert(ed);
                    string fed = dml.dateConvert(ed).Replace('-', ' ');
                    ddlEndDate.Items.FindByText(fed).Selected = true;
                }
                else
                {
                    ddlEndDate.SelectedIndex = 0;
                }


                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                bool hide = bool.Parse(ds.Tables[0].Rows[0]["IsHide"].ToString());
                bool main_site = bool.Parse(ds.Tables[0].Rows[0]["Main_Site"].ToString());


                txtCreatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());

                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() == "")
                {
                    txtcreatedDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                    txtcreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdateDate"].ToString() == "")
                {
                    txtUpdatedDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateDate"].ToString());
                    txtUpdatedDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }

              
                
              
                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (hide == true)
                {
                    rdb_Hide_Y.Checked = true;
                }
                else
                {
                    rdb_Hide_N.Checked = true;
                }
                if (main_site == true)
                {
                    rdb_Main.Checked = true;
                }
                else
                {
                    rdb_Site.Checked = true;
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
        txtUpdatedDate.Enabled = false;
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
            DataSet ds = dml.Find("select * from SET_UserGrp_Documents where Sno = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDocument.ClearSelection();
                ddlUsergrp.ClearSelection();
                ddlUsergrp.Items.FindByValue(ds.Tables[0].Rows[0]["UserGrpId"].ToString()).Selected = true;
                ddlDocument.Items.FindByValue(ds.Tables[0].Rows[0]["DocID"].ToString()).Selected = true;
                txtDocumentDesc.Text = ds.Tables[0].Rows[0]["DocDescription"].ToString();
                txtAppleDate.Text = ds.Tables[0].Rows[0]["ApplyDate"].ToString();
                dml.dateConvert(txtAppleDate);
                txtSortOrder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();


                ddlStartDate.ClearSelection();
                ddlEndDate.ClearSelection();
                string sd = dml.dateConvert(ds.Tables[0].Rows[0]["StartDate"].ToString());
                string ed = ds.Tables[0].Rows[0]["EndDate"].ToString();

                string fst = sd.Replace('-', ' ');
               

                ddlStartDate.Items.FindByText(fst).Selected = true;

                if (ed != "")
                {
                    // dml.dateConvert(ed);
                    string fed = dml.dateConvert(ed).Replace('-', ' ');
                    ddlEndDate.Items.FindByText(fed).Selected = true;
                }
                else
                {
                    ddlEndDate.SelectedIndex = 0;
                }


                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                bool hide = bool.Parse(ds.Tables[0].Rows[0]["IsHide"].ToString());
                bool main_site = bool.Parse(ds.Tables[0].Rows[0]["Main_Site"].ToString());


                txtCreatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());

                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() == "")
                {
                    txtcreatedDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                    txtcreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdateDate"].ToString() == "")
                {
                    txtUpdatedDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateDate"].ToString());
                    txtUpdatedDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }




                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (hide == true)
                {
                    rdb_Hide_Y.Checked = true;
                }
                else
                {
                    rdb_Hide_N.Checked = true;
                }
                if (main_site == true)
                {
                    rdb_Main.Checked = true;
                }
                else
                {
                    rdb_Site.Checked = true;
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
        ddlUsergrp.Enabled = true;
        txtDocumentDesc.Enabled = true;
        txtAppleDate.Enabled = true;
        rdb_Site.Enabled = true;
        rdb_Main.Enabled = true;
        rdb_Hide_Y.Enabled = true;
        rdb_Hide_N.Enabled = true;
        txtSortOrder.Enabled = true;
        txtCreatedBy.Enabled = false;
        txtcreatedDate.Enabled = false;
        txtUpdatedDate.Enabled = false;
        ddlDocument.Enabled = true;
        txtUpdatedBy.Enabled = false;
        ddlDocument.Enabled = false;
        ddlStartDate.Enabled = true;
        ddlEndDate.Enabled = true;
        chkActive.Enabled = true;
       
        

        updatecol.Visible = true;
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {

            string serial_id;
            serial_id = GridView3.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select * from SET_UserGrp_Documents where Sno = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDocument.ClearSelection();
                ddlUsergrp.ClearSelection();
                ddlUsergrp.Items.FindByValue(ds.Tables[0].Rows[0]["UserGrpId"].ToString()).Selected = true;
                ddlDocument.Items.FindByValue(ds.Tables[0].Rows[0]["DocID"].ToString()).Selected = true;
                txtDocumentDesc.Text = ds.Tables[0].Rows[0]["DocDescription"].ToString();
                txtAppleDate.Text = ds.Tables[0].Rows[0]["ApplyDate"].ToString();
                dml.dateConvert(txtAppleDate);
                txtSortOrder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                bool hide = bool.Parse(ds.Tables[0].Rows[0]["IsHide"].ToString());
                bool main_site = bool.Parse(ds.Tables[0].Rows[0]["Main_Site"].ToString());

                ddlStartDate.ClearSelection();
                ddlEndDate.ClearSelection();
                string sd = dml.dateConvert(ds.Tables[0].Rows[0]["StartDate"].ToString());


                string ed = ds.Tables[0].Rows[0]["EndDate"].ToString();
                
                string fst = sd.Replace('-', ' ');
              

                ddlStartDate.Items.FindByText(fst).Selected = true;

                if (ed != "")
                {
                   // dml.dateConvert(ed);
                    string fed = dml.dateConvert(ed).Replace('-', ' ');
                    if (ddlEndDate.Items.FindByText(fed) != null)
                    {
                        ddlEndDate.Items.FindByText(fed).Selected = true;
                    }
                }
                else
                {
                    ddlEndDate.SelectedIndex = 0;
                }

                txtCreatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());

                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() == "")
                {
                    txtcreatedDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                    txtcreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdateDate"].ToString() == "")
                {
                    txtUpdatedDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateDate"].ToString());
                    txtUpdatedDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }




                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (hide == true)
                {
                    rdb_Hide_Y.Checked = true;
                }
                else
                {
                    rdb_Hide_N.Checked = true;
                }
                if (main_site == true)
                {
                    rdb_Main.Checked = true;
                }
                else
                {
                    rdb_Site.Checked = true;
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
    protected void radEdit_DocumetName_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo2(radEdit_DocumetName, e.Text, "DocName", "DocTypeId", "DocAbbr", "SET_DocumentType", where, "DocName");
    }
    protected void radFind_DocumetName_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo2(radFind_DocumentName, e.Text, "DocName", "DocTypeId", "DocAbbr", "SET_DocumentType", where, "DocName");

    }
    protected void radDel_DocumentName_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo2(radDel_DocumentName, e.Text, "DocName", "DocTypeId", "DocAbbr", "SET_DocumentType", where, "DocName");

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
        btnSave.Visible = false; 
        btnInsert.Visible = false;
        btnCancel.Visible = true;
        btnUpdate.Visible = true;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnFind.Visible = false;
        Label1.Text = "";
        ddlUsergrp.Enabled = true;
        txtDocumentDesc.Enabled = true;
        txtAppleDate.Enabled = true;
        rdb_Site.Enabled = true;
        rdb_Main.Enabled = true;
        rdb_Hide_Y.Enabled = true;
        rdb_Hide_N.Enabled = true;
        txtSortOrder.Enabled = true;
        txtCreatedBy.Enabled = false;
        txtcreatedDate.Enabled = false;
        txtUpdatedDate.Enabled = false;
        ddlDocument.Enabled = true;
        txtUpdatedBy.Enabled = false;
        ddlDocument.Enabled = false;

        chkActive.Enabled = true;


        try {
        
            
            Label serial_id = (Label)GridView4.SelectedRow.FindControl("lblNatureID");
          //  serial_id = GridView4.SelectedRow.Cells[2].Text;
            ViewState["SNO"] = serial_id.Text;
            DataSet ds = dml.Find("select * from viewDOCUSER where DocID = '" + serial_id.Text + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDocument.ClearSelection();
                ddlUsergrp.ClearSelection();
               

                ddlUsergrp.Items.FindByValue(ds.Tables[0].Rows[0]["UserGrpId"].ToString()).Selected = true;
                ddlDocument.Items.FindByValue(ds.Tables[0].Rows[0]["DocID"].ToString()).Selected = true;
                txtDocumentDesc.Text = ds.Tables[0].Rows[0]["DocDescription"].ToString();
                txtAppleDate.Text = ds.Tables[0].Rows[0]["ApplyDate"].ToString();
                txtSortOrder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();
                ddlStartDate.ClearSelection();
                ddlEndDate.ClearSelection();
                string sd = dml.dateConvert(ds.Tables[0].Rows[0]["StartDate"].ToString());
                string ed = dml.dateConvert(ds.Tables[0].Rows[0]["EndDate"].ToString());

                string fst = sd.Replace('-', ' ');
                string fed = ed.Replace('-', ' ');

                ddlStartDate.Items.FindByText(fst).Selected = true;
                ddlEndDate.Items.FindByText(fed).Selected = true;
                dml.dateConvert(txtAppleDate);


                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                bool hide = bool.Parse(ds.Tables[0].Rows[0]["IsHide"].ToString());
                bool main_site = bool.Parse(ds.Tables[0].Rows[0]["Main_Site"].ToString());


                txtCreatedBy.Text = ds.Tables[0].Rows[0]["CreatedBy"].ToString();
                txtUpdatedBy.Text = ds.Tables[0].Rows[0]["UpdatedBy"].ToString();

                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() == "")
                {
                    txtcreatedDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                    txtcreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdateDate"].ToString() == "")
                {
                    txtUpdatedDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateDate"].ToString());
                    txtUpdatedDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }




                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (hide == true)
                {
                    rdb_Hide_Y.Checked = true;
                }
                else
                {
                    rdb_Hide_N.Checked = true;
                }
                if (main_site == true)
                {
                    rdb_Main.Checked = true;
                }
                else
                {
                    rdb_Site.Checked = true;
                }

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
            Label1.Enabled = true;
            Label1.Text = ex.Message;
        }


    }
    public void selectcheck()
    {
        fiscal = Request.QueryString["fiscaly"];
        string fst = "",fed= "";
        
        DataSet dsf = dml.Find("select StartDate,EndDate from SET_Fiscal_Year where Description = '"+fiscal+"' and Record_Deleted = 0;");
        if(dsf.Tables[0].Rows.Count> 0)
        {
            fst = dsf.Tables[0].Rows[0]["StartDate"].ToString();
            fed = dsf.Tables[0].Rows[0]["EndDate"].ToString();
        }


        string rdbY, MAin ,apply;
        try
        {
            UserGrpID = Request.QueryString["UsergrpID"];
            DataSet ds;

            int aa = ddlUsergrp.SelectedIndex;
            
            if (ddlUsergrp.SelectedIndex != 0)
            {
               ds  =   dml.Find("select DocID, Main_Site, IsHide, ApplyDate from viewDOCUSER where UserGrpId = '" + ddlUsergrp.SelectedItem.Value + "' and Record_deleted = 0 order by DocID asc");
            }
            else
            {
                ds  =  dml.Find("select DocID, Main_Site, IsHide, ApplyDate from viewDOCUSER where UserGrpId = '" + UserGrpID + "' and Record_deleted = 0 order by DocID asc");
            }
            int countrow = ds.Tables[0].Rows.Count;
            if (countrow > 0)
            {
                
                for (int i = 0; i <= countrow - 1; i++)
                {
                    string val =  ds.Tables[0].Rows[i]["DocID"].ToString();
                    rdbY = ds.Tables[0].Rows[i]["IsHide"].ToString();
                    MAin = ds.Tables[0].Rows[i]["Main_Site"].ToString();
                    apply = ds.Tables[0].Rows[i]["ApplyDate"].ToString();
                    foreach (GridViewRow grow in GridView4.Rows)
                    {
                        DropDownList ddl_menu = (DropDownList)grow.FindControl("ddlGridView4stdate");
                        DropDownList ddl_menu_ed = (DropDownList)grow.FindControl("ddlGridView4Enddate");
                      
                        Label lbl_ED_date = (Label)grow.FindControl("lbleddate");
                        Label lbl = (Label)grow.FindControl("lblStDate");

                        CheckBox chk_del = (CheckBox)grow.FindControl("chkSelect");
                        RadioButton rdbyes = (RadioButton)grow.FindControl("rdb_HYes");
                        RadioButton rdbNo = (RadioButton)grow.FindControl("rdb_NYes");
                        RadioButton rdbMain = (RadioButton)grow.FindControl("rdb_HMain");
                        RadioButton rdbSite = (RadioButton)grow.FindControl("rdb_NSite");
                        TextBox txtapply = (TextBox)grow.FindControl("txtAppleDate123");
                        Label lblID = (Label)grow.FindControl("lblNatureID");

                        if (lblID.Text == val)
                        {

                            if (MAin == "True")
                            {
                                rdbMain.Checked = true;
                                rdbSite.Checked = false;
                            }
                            if (MAin == "False")
                            {
                                rdbMain.Checked = false;
                                rdbSite.Checked = true;
                            }

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


                            string s;
                            if (lbl.Text != "")
                            {
                                s = dml.dateConvert(lbl.Text);
                            }
                            else
                            {
                                s = fst;
                            }
                            string ed;
                            if (lbl_ED_date.Text != "")
                            {
                                ed = dml.dateConvert(lbl_ED_date.Text);
                            }
                            else
                            {
                                ed = "";
                            }

                            string sR = s.Replace('-', ' ');
                            string dR = ed.Replace('-', ' ');

                            ddl_menu.ClearSelection();
                            ddl_menu_ed.ClearSelection();
                           
                            string ss = ddl_menu.Items.Count.ToString();
                         
                           // ddl_menu.Items.FindByText(sR).Selected = true;
                           
                            if (s != "01 Jan 2000")
                            {
                                if (ddl_menu.Items.FindByText(sR).Text != null)
                                {
                                    ddl_menu.Items.FindByText(sR).Selected = true;
                                }
                            }
                            else
                            {
                                ddl_menu.SelectedIndex = 0;
                            }
                            string sss = ddl_menu_ed.Items.Count.ToString();

                           // ddl_menu_ed.Items.FindByText(dR).Selected = true;
                            if (ed != "")
                            {
                                if (ddl_menu_ed.Items.FindByText(dR).Text != null)
                                {
                                    ddl_menu_ed.Items.FindByText(dR).Selected = true;
                                }
                            }
                            else
                            {
                                ddl_menu_ed.SelectedIndex = 0;
                            }


                            //if (ddl_menu.Items.FindByText(lbl.Text) != null)
                            //{
                            //    ddl_menu.ClearSelection();
                            //    ddl_menu.SelectedValue = lbl.Text;

                            //}
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
        for(int i = 0; i< GridView4.Rows.Count; i++)
        {
            TextBox txtdate = ((TextBox)GridView4.Rows[i].FindControl("txtAppleDate123"));
            txtdate.Attributes.Add("readonly", "readonly");
            if ( a == true)
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
      
        else if( GridView4.Rows.Count == ab)
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
        DataSet ds,ds1,ds2;
        if (ddlUsergrp.SelectedIndex != 0)
        {
            ds = dml.Find("select DocID, Main_Site, IsHide, ApplyDate,DocDescription from SET_UserGrp_Documents where UserGrpId = '" + ddlUsergrp.SelectedItem.Value + "' and Record_deleted = 0 order by DocDescription");
            ds1 = dml.Find("select DocID, Main_Site, IsHide, ApplyDate,DocDescription from SET_UserGrp_Documents where UserGrpId = '" + ddlUsergrp.SelectedItem.Value + "' and Record_deleted = 1 order by DocDescription");
        }
        else
        {
            ds = dml.Find("select DocID, Main_Site, IsHide, ApplyDate,DocDescription from SET_UserGrp_Documents where UserGrpId = '" + UserGrpID + "' and Record_deleted = 0 order by DocDescription");
            ds1 = dml.Find("select DocID, Main_Site, IsHide, ApplyDate,DocDescription from SET_UserGrp_Documents where UserGrpId = '" + UserGrpID + "' and Record_deleted = 1 order by DocDescription");
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
                            string val = ds1.Tables[0].Rows[i]["DocID"].ToString();
                            if (val == lblID.Text && chk_del.Checked == true)
                            {
                                ca1 = ca1 + 1;
                                if (ddlUsergrp.SelectedIndex != 0)
                                {
                                    dml.Update("update SET_UserGrp_Documents set Record_Deleted = '0' , UpdatedBy= '" + show_username() + "',UpdateDate='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' where DocID = '" + lblID.Text + "' and UserGrpId = '" + ddlUsergrp.SelectedItem.Value + "';", "");
                                }
                                else
                                {
                                    dml.Update("update SET_UserGrp_Documents set Record_Deleted = '0' , UpdatedBy= '" + show_username() + "',UpdateDate='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' where DocID = '" + lblID.Text + "' and UserGrpId = '" + UserGrpID + "';", "");
                                }
                                flag = false;

                            }
                        }
                    }
                    else
                    {
                        if (chk_del.Checked == true)
                        {

                            if (ddlUsergrp.SelectedIndex != 0)
                            {
                                ds2 = dml.Find("select DocID, Main_Site, IsHide, ApplyDate,DocDescription from SET_UserGrp_Documents where UserGrpId = '" + ddlUsergrp.SelectedItem.Value + "' and DocID = '" + lblID.Text + "' and Record_deleted = 0 order by DocDescription");
                            }
                            else
                            {
                                ds2 = dml.Find("select DocID, Main_Site, IsHide, ApplyDate,DocDescription from SET_UserGrp_Documents where UserGrpId = '" + UserGrpID + "' and DocID = '" + lblID.Text + "' and Record_deleted = 0 order by DocDescription");
                            }

                            if (ds2.Tables[0].Rows.Count > 0)
                            {


                            }
                            else {
                                //Insert
                                UserGrpID = Request.QueryString["UsergrpID"];
                                int mainsite = 0, hideyn = 0;
                                Label lbdocdes = (Label)g.FindControl("lbldoc");
                                Label lbdocname = (Label)g.FindControl("lbldocname");
                                TextBox applydate = (TextBox)g.FindControl("txtAppleDate123");
                                RadioButton rdbmain = (RadioButton)g.FindControl("rdb_HMain");
                                RadioButton rdbsite = (RadioButton)g.FindControl("rdb_NSite");
                                RadioButton rdbHideY = (RadioButton)g.FindControl("rdb_HYes");
                                RadioButton rdbHideN = (RadioButton)g.FindControl("rdb_NYes");

                                DropDownList ddlst = (DropDownList)g.FindControl("ddlGridView4stdate");
                                DropDownList ddled = (DropDownList)g.FindControl("ddlGridView4Enddate");
                                if (rdbmain.Checked == true)
                                {
                                    mainsite = 1;
                                }
                                if (rdbsite.Checked == true)
                                {
                                    mainsite = 0;
                                }
                                if (rdbHideY.Checked == true)
                                {
                                    hideyn = 1;
                                }
                                if (rdbHideN.Checked == true)
                                {
                                    hideyn = 0;
                                }

                                string date = DateTime.Now.ToString("yyyy-MMM-dd"); ;//dml.dateconvertString(txtAppleDate.Text);

                                string startdate = "";
                                string enddate = "";
                                if(ddlst.SelectedIndex != 0)
                                {
                                    startdate = dml.dateconvertString(ddlst.SelectedItem.Text);
                                }
                                //if(ddled.SelectedIndex != 0 )
                                //{
                                //    enddate = dml.dateconvertString(ddled.SelectedItem.Text);
                                //}



                                string endfinal = "";
                                if (ddled.SelectedIndex != 0)
                                {
                                    enddate = dml.dateconvertString(ddled.SelectedItem.Text);
                                    endfinal = "'" + enddate + "'";

                                }
                                else
                                {
                                    endfinal = "NULL";
                                }


                                if (ddlUsergrp.SelectedIndex != 0)
                                {
                                    dml.Insert("INSERT INTO SET_UserGrp_Documents([UserGrpId], [DocID], [DocDescription], [Main_Site], [ApplyDate], [IsActive], [IsHide], [SortOrder], [Record_Deleted], [CreatedBy], [CreateDate],[StartDate],[EndDate]) VALUES ('" + ddlUsergrp.SelectedItem.Value + "', '" + lblID.Text + "', '" + lbdocdes.Text + "', '" + mainsite + "', '" + date + "', '1', '" + hideyn + "', '0', '0', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "','"+startdate+"',"+ endfinal + ")", "");
                                    dml.Update("update Set_Documents set MLD = '" + dml.Encrypt("q") + "' where DocID = '" + ddlDocument.SelectedItem.Value + "'", "");
                                }
                                else
                                {
                                    dml.Insert("INSERT INTO SET_UserGrp_Documents([UserGrpId], [DocID], [DocDescription], [Main_Site], [ApplyDate], [IsActive], [IsHide], [SortOrder], [Record_Deleted], [CreatedBy], [CreateDate],[StartDate],[EndDate]) VALUES ( '" + UserGrpID + "', '" + lblID.Text + "', '" + lbdocdes.Text + "', '" + mainsite + "', '" + date + "', '1', '" + hideyn + "', '0', '0', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "','"+startdate+"',"+ endfinal + ")", "");
                                    dml.Update("update Set_Documents set MLD = '" + dml.Encrypt("q") + "' where DocID = '" + ddlDocument.SelectedItem.Value + "'", "");
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
                        string val = ds.Tables[0].Rows[i]["DocID"].ToString();
                        if (val == lblID.Text && chk_del.Checked == false)
                        {


                            if (ddlUsergrp.SelectedIndex != 0)
                            {
                                dml.Update("update SET_UserGrp_Documents set Record_Deleted = '1' , UpdatedBy= '" + show_username() + "',UpdateDate='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' where DocID = '" + lblID.Text + "' and UserGrpId = '" + ddlUsergrp.SelectedItem.Value + "';", "");
                            }
                            else
                            {
                                dml.Update("update SET_UserGrp_Documents set Record_Deleted = '1' , UpdatedBy= '" + show_username() + "',UpdateDate='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' where DocID = '" + lblID.Text + "' and UserGrpId = '" + UserGrpID + "';", "");
                            }
                            flag = false;
                        }


                    }
                    if (flag == false)
                    {
                        Label1.Text = "Updated Success";
                        GridView4.DataBind();
                        datacall();
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


                    if (ddlUsergrp.SelectedIndex != 0)
                    {
                        ds2 = dml.Find("select DocID, Main_Site, IsHide, ApplyDate,DocDescription from SET_UserGrp_Documents where UserGrpId = '" + ddlUsergrp.SelectedItem.Value + "' and DocID = '" + lblID.Text + "' and Record_deleted = 1 order by DocDescription");
                    }
                    else
                    {
                        ds2 = dml.Find("select DocID, Main_Site, IsHide, ApplyDate,DocDescription from SET_UserGrp_Documents where UserGrpId = '" + UserGrpID + "' and DocID = '" + lblID.Text + "' and Record_deleted = 1 order by DocDescription");
                    }

                    if (ds2.Tables[0].Rows.Count > 0)
                    {


                        if (ddlUsergrp.SelectedIndex != 0)
                        {
                            dml.Update("update SET_UserGrp_Documents set Record_Deleted = '0' , UpdatedBy= '" + show_username() + "',UpdateDate='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' where DocID = '" + lblID.Text + "' and UserGrpId = '" + ddlUsergrp.SelectedItem.Value + "';", "");
                        }
                        else
                        {
                            dml.Update("update SET_UserGrp_Documents set Record_Deleted = '0' , UpdatedBy= '" + show_username() + "',UpdateDate='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' where DocID = '" + lblID.Text + "' and UserGrpId = '" + UserGrpID + "';", "");
                        }
                        Label1.Text = "Updated Success";


                    }
                    else
                    {
                        
                       
                        int mainsite = 0, hideyn = 0;
                        Label lbdocdes = (Label)g.FindControl("lbldoc");
                        Label lbdocname = (Label)g.FindControl("lbldocname");
                        TextBox applydate = (TextBox)g.FindControl("txtAppleDate123");
                        RadioButton rdbmain = (RadioButton)g.FindControl("rdb_HMain");
                        RadioButton rdbsite = (RadioButton)g.FindControl("rdb_NSite");
                        RadioButton rdbHideY = (RadioButton)g.FindControl("rdb_HYes");
                        RadioButton rdbHideN = (RadioButton)g.FindControl("rdb_NYes");

                        DropDownList ddlst = (DropDownList)g.FindControl("ddlGridView4stdate");
                        DropDownList ddled = (DropDownList)g.FindControl("ddlGridView4Enddate");
                        if (rdbmain.Checked == true)
                        {
                            mainsite = 1;
                        }
                        if (rdbsite.Checked == true)
                        {
                            mainsite = 0;
                        }
                        if (rdbHideY.Checked == true)
                        {
                            hideyn = 1;
                        }
                        if (rdbHideN.Checked == true)
                        {
                            hideyn = 0;
                        }

                        string startdate = "";
                        string enddate = "";
                        if (ddlst.SelectedIndex != 0)
                        {
                            startdate = dml.dateconvertString(ddlst.SelectedItem.Text);
                        }
                        string endfinal = "";
                        if (ddled.SelectedIndex != 0)
                        {
                            enddate = dml.dateconvertString(ddled.SelectedItem.Text);
                            endfinal = "'" + enddate + "'";

                        }
                        else
                        {
                            endfinal = "NULL";
                        }
                       



                        string date = DateTime.Now.ToString("yyyy-MMM-dd"); ;//dml.dateconvertString(txtAppleDate.Text);

                        if (ddlUsergrp.SelectedIndex != 0)
                        {

                        dml.Insert("INSERT INTO SET_UserGrp_Documents([UserGrpId], [DocID], [DocDescription], [Main_Site], [ApplyDate], [IsActive], [IsHide], [SortOrder], [Record_Deleted], [CreatedBy], [CreateDate],[StartDate], [EndDate]) VALUES ('" + ddlUsergrp.SelectedItem.Value + "', '" + lblID.Text + "', '" + lbdocdes.Text + "', '" + mainsite + "', '" + date + "', '1', '" + hideyn + "', '0', '0', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "','"+ startdate + "',"+ endfinal + ")", "");
                    }
                        else
                        {
                            dml.Insert("INSERT INTO SET_UserGrp_Documents([UserGrpId], [DocID], [DocDescription], [Main_Site], [ApplyDate], [IsActive], [IsHide], [SortOrder], [Record_Deleted], [CreatedBy], [CreateDate],[StartDate], [EndDate]) VALUES ( '" + UserGrpID + "', '" + lblID.Text + "', '" + lbdocdes.Text + "', '" + mainsite + "', '" + date + "', '1', '" + hideyn + "', '0', '0', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "','" + startdate + "',"+ endfinal + ")", "");
                        }
                        Label1.Text = "data Inserted";
                    }
                }
            }
        }

        datamenu_view();
        GridView4.DataBind();
        datacall();
        selectcheck();
    }
    protected void ddlUsergrp_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Text = "";
        datamenu_view();
        datacall();
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
            cmd = new SqlCommand("sp_USERDOC", con);

            if(ddlUsergrp.SelectedIndex !=  0)
            {
                cmd.Parameters.Add(new SqlParameter("@id", ddlUsergrp.SelectedItem.Value));
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
        foreach(GridViewRow g in GridView4.Rows)
        {
            CheckBox chk = (CheckBox)g.FindControl("chkSelect");
            string text = chk.Checked.ToString();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        
        ccc();
    }
    public void selectcount()
    {

        int count = 0;
        int uncount = 0;
        int ca = 0;
        int s = 0;


        foreach (GridViewRow grow in GridView4.Rows)
        {
            CheckBox chk_del = (CheckBox)grow.FindControl("chkSelect");
            Label lblID = (Label)grow.FindControl("lblNatureID");
            if (chk_del.Checked)
            {

                count = count + 1;
            }
            else
                uncount = uncount + 1;

        }

        string[] chkarray = new string[count];
        int c = 0;
        int e = 0;
        foreach (GridViewRow grow in GridView4.Rows)
        {
            CheckBox chk_del = (CheckBox)grow.FindControl("chkSelect");
            Label lblID = (Label)grow.FindControl("lblNatureID");
            if (chk_del.Checked)
            {
                chkarray[c] = lblID.Text;
                c = c + 1;

            }
            else
            {

            }


        }
        DataSet ds;
        UserGrpID = Request.QueryString["UsergrpID"];
        foreach (GridViewRow grow in GridView4.Rows)
        {
            UserGrpID = Request.QueryString["UsergrpID"];
            CheckBox chk_del = (CheckBox)grow.FindControl("chkSelect");
            Label lblID = (Label)grow.FindControl("lblNatureID");
            if (chk_del.Checked)
            {
                for (int a = 0; a < chkarray.Length; a++)
                {
                    if (chkarray[a].ToString() == lblID.Text)
                    {

                        if (ddlUsergrp.SelectedIndex != 0)
                        {
                            ds = dml.Find("SELECT * from SET_UserGrp_Documents where DocID= '" + lblID.Text + "' and UserGrpId='" + ddlUsergrp.SelectedItem.Value + "' and Record_Deleted = '0'");
                        }
                        else
                        {
                            ds = dml.Find("SELECT * from SET_UserGrp_Documents where DocID= '" + lblID.Text + "' and UserGrpId='" + UserGrpID + "' and Record_Deleted = '0'");
                        }



                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            e = e + 1;
                        }
                        else
                        {
                            UserGrpID = Request.QueryString["UsergrpID"];
                            int mainsite = 0, hideyn = 0;
                            Label lbdocdes = (Label)grow.FindControl("lbldoc");
                            Label lbdocname = (Label)grow.FindControl("lbldocname");
                            TextBox applydate = (TextBox)grow.FindControl("txtAppleDate123");
                            RadioButton rdbmain = (RadioButton)grow.FindControl("rdb_HMain");
                            RadioButton rdbsite = (RadioButton)grow.FindControl("rdb_NSite");
                            RadioButton rdbHideY = (RadioButton)grow.FindControl("rdb_HYes");
                            RadioButton rdbHideN = (RadioButton)grow.FindControl("rdb_NYes");
                            if (rdbmain.Checked == true)
                            {
                                mainsite = 1;
                            }
                            if (rdbsite.Checked == true)
                            {
                                mainsite = 0;
                            }
                            if (rdbHideY.Checked == true)
                            {
                                hideyn = 1;
                            }
                            if (rdbHideN.Checked == true)
                            {
                                hideyn = 0;
                            }

                            string date = DateTime.Now.ToString("yyyy-MMM-dd"); ;//dml.dateconvertString(txtAppleDate.Text);

                            if (ddlUsergrp.SelectedIndex != 0)
                            {
                                dml.Insert("INSERT INTO SET_UserGrp_Documents([UserGrpId], [DocID], [DocDescription], [Main_Site], [ApplyDate], [IsActive], [IsHide], [SortOrder], [Record_Deleted], [CreatedBy], [CreateDate]) VALUES ('" + ddlUsergrp.SelectedItem.Value + "', '" + lblID.Text + "', '" + lbdocdes.Text + "', '" + mainsite + "', '" + date + "', '1', '" + hideyn + "', '0', '0', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "')", "");
                            }
                            else
                            {
                                dml.Insert("INSERT INTO SET_UserGrp_Documents([UserGrpId], [DocID], [DocDescription], [Main_Site], [ApplyDate], [IsActive], [IsHide], [SortOrder], [Record_Deleted], [CreatedBy], [CreateDate]) VALUES ( '" + UserGrpID + "', '" + lblID.Text + "', '" + lbdocdes.Text + "', '" + mainsite + "', '" + date + "', '1', '" + hideyn + "', '0', '0', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "')", "");
                            }
                            Label1.Text = "data Inserted";
                            datamenu_view();
                            GridView4.DataBind();
                            selectcheck();

                        }

                    }

                }
            }
        }

        datamenu_view();
        GridView4.DataBind();


    }
    public void datacall()
    {
        foreach (GridViewRow grow in GridView4.Rows)
        {
            DropDownList ddl_menu = (DropDownList)grow.FindControl("ddlGridView4Enddate");
            DropDownList ddl_menust = (DropDownList)grow.FindControl("ddlGridView4stdate");
            //dml.dropdownsql(ddl_menu, "SET_Menu", "Menu_title", "Menuid");ddlGridView4stdate
            dml.dropdownsqldateF(ddl_menust, "SET_Fiscal_Year", "convert(varchar, StartDate, 106)", "stdate", "FiscalYearID", "StartDate");
            dml.dropdownsqldateF(ddl_menu, "SET_Fiscal_Year", "convert(varchar, EndDate, 106)", "eddate", "FiscalYearID", "EndDate");

            ddl_menust.SelectedIndex = 1;
        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string id = e.Row.Cells[1].Text;
        bool flag = dml.isNumber(id);


        if (flag == true)
        {

            DataSet ds = dml.Find("select Sno,MLD from SET_UserGrp_Documents where Sno = '" + id + "'");
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

            DataSet ds = dml.Find("select Sno,MLD from SET_UserGrp_Documents where Sno = '" + id + "'");
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