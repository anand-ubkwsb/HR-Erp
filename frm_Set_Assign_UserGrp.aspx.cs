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

            

            dml.dropdownsql(ddlUserName, "SET_User_Manager", "user_name", "UserId", "Record_Deleted", "0");
            dml.dropdownsqlforUserGrp(ddlUsergrp, "SET_UserGrp", "UserGrpName", "UserGrpId", "Record_Deleted","0");
            var count = ddlUsergrp.Items.Count;

            dml.dropdownsql(ddlFind_USerGrpName, "SET_UserGrp", "UserGrpName", "UserGrpId", "UserGrpName");
            dml.dropdownsql(ddlEdit_USERGRP, "SET_UserGrp", "UserGrpName", "UserGrpId", "UserGrpName");
            dml.dropdownsql(ddlDel_UserGRP, "SET_UserGrp", "UserGrpName", "UserGrpId", "UserGrpName");

            dml.dropdownsql(ddlEdit_UserName, "SET_User_Manager", "user_name", "UserId", "user_name");
            dml.dropdownsql(ddlFind_UserNAme, "SET_User_Manager", "user_name", "UserId", "user_name");
            dml.dropdownsql(ddlDel_UserNAme, "SET_User_Manager", "user_name", "UserId", "user_name");
            

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
       

        ddlUsergrp.Enabled = true;
      
        txtAppleDate.Enabled = true;
        ddlUserName.Enabled = true;

        rdb_Hide_Y.Enabled = true;
        rdb_Hide_N.Enabled = true;
      
        rdb_Hide_N.Checked = true;
        txtSortOrder.Enabled = true;
        txtFromDate.Enabled = true;
        txtTodate.Enabled = true;

        txtCreatedBy.Enabled = false;
        txtcreatedDate.Enabled = false;
        txtUpdatedDate.Enabled = false;
        txtUpdatedBy.Enabled = false;
        chkActive.Enabled =true;
       
        chkActive.Checked =true;
        ImageButton1.Enabled = true;
        ImageButton2.Enabled = true;
        imgPopup.Enabled = true;

        txtAppleDate.Text = DateTime.Now.ToString("dd-MMM-yyy");
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
            string hide = "";
            int chk, main_site;

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
                hide = "Y";
            }
            else
            {
                hide = "N";
            }

           
            bool flags = false;
          
                string st_date = dml.dateconvertString(txtFromDate.Text);
                string ed_date;
                if (txtTodate.Text != "")
                {
                    ed_date = dml.dateconvertString(txtTodate.Text);
                }
                else
                {
                    ed_date = "";

                }
            //select * from SET_UserGrp_Documents where DocID = 1
            DataSet uniqueg_B_C = dml.Find("select ToDate from SET_Assign_UserGrp where UserID = '" + ddlUserName.SelectedItem.Value + "'");
                if (uniqueg_B_C.Tables[0].Rows.Count > 0)
                {
                    string ed = uniqueg_B_C.Tables[0].Rows[0]["ToDate"].ToString();
                    if (string.IsNullOrEmpty(ed))
                    {
                        DataSet dsS = dml.Find("select * from SET_Assign_UserGrp where FromDate < = '" + st_date + "' AND UserID = '" + ddlUserName.SelectedItem.Value + "'");
                        if (dsS.Tables[0].Rows.Count > 0)
                        {
                            Label1.ForeColor = System.Drawing.Color.Red;
                            Label1.Text = "Duplicated entry not allowed";
                        }
                    else
                    {
                        flags = true;
                    }

                    }
                    else
                    {
                        string edate = "0";

                        DataSet ds_id = dml.Find("select FromDate from SET_Assign_UserGrp where UserID = '" + ddlUserName.SelectedItem.Value + "'");
                        if (ds_id.Tables[0].Rows.Count > 0)
                        {

                            for (int a = 0; a < ds_id.Tables[0].Rows.Count; a++)
                            {
                                edate = ds_id.Tables[0].Rows[a]["FromDate"].ToString();


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
                                    Label1.Text = "this Start Date aleardy inserted";
                                    flags = false;
                                }
                            }

                        }

                    }
                }
                else
                {
                    //string imgpath = "~/dist/img/" + Path.GetFileName(FileUpload1.FileName);
                    dml.Insert("INSERT INTO [SET_Assign_UserGrp] ([UserId], [UserGrpId], [EntryDate], [FromDate], [ToDate], [IsActive], [Hide], [Record_Deleted], [CreatedBy], [CreatedDate]) VALUES "
                                    +" ('"+ddlUserName.SelectedItem.Value+"', '"+ddlUsergrp.SelectedItem.Value+"','"+dml.dateconvertforinsertNEW(txtAppleDate)+"', '"+dml.dateconvertforinsertNEW(txtFromDate)+"', '"+dml.dateconvertforinsertNEW(txtTodate)+"', '"+chk+"', '"+hide+"',  '0', '"+show_username()+"', '"+DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")+"');", "alertme()");
                    ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);

                    Label1.Text = "";

                   
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
                dml.Insert("INSERT INTO [SET_Assign_UserGrp] ([UserId], [UserGrpId], [EntryDate], [FromDate], [ToDate], [IsActive], [Hide], [Record_Deleted], [CreatedBy], [CreatedDate]) VALUES "
                                   + " ('" + ddlUserName.SelectedItem.Value + "', '" + ddlUsergrp.SelectedItem.Value + "','" + dml.dateconvertforinsertNEW(txtAppleDate) + "', '" + dml.dateconvertforinsertNEW(txtFromDate) + "', " + dml.dateconvertforNewWithNull(txtTodate) + ", '" + chk + "', '" + hide + "',  '0', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "');", "alertme()");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);

                Label1.Text = "";
               
               
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
            
            Showall_Dml();
        }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            int chk, main_site ;
            string docname, hide;

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
                hide = "Y";
            }
            else
            {
                hide = "N";
            }



            string app_date = txtAppleDate.Text;
            string st_date = dml.dateconvertString(txtFromDate.Text);
            string ed_date;
            if (txtTodate.Text != "")
            {
                ed_date = dml.dateconvertString(txtTodate.Text);
            }
            else
            {
                ed_date = "1900-01-01";

            }

            DataSet ds_up = dml.Find("select * from SET_Assign_UserGrp  WHERE ([Sno]='"+ViewState["SNO"].ToString()+"') AND ([UserId]='"+ddlUserName.SelectedItem.Value+"') AND ([UserGrpId]='"+ddlUsergrp.SelectedItem.Value+"') AND ([EntryDate]='"+txtAppleDate.Text+"') AND ([FromDate]='"+txtFromDate.Text+"') AND ([ToDate] = '"+txtTodate.Text+"') AND ([IsActive]= '"+chk+"') AND ([Hide]='"+hide+"')");

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
                //([Sno]='1') AND ([UserId]='359B6016-315C-4F0B-83DF-CDBFCC4206F9') AND ([UserGrpId]='862767BE-CB6E-4DF3-8181-03C3DF1D1906') AND ([EntryDate]='2020-12-18') AND ([FromDate]='2017-07-01') AND ([ToDate] IS NULL) AND ([IsActive] IS NULL) AND ([Hide]='N') AND ([SysDate]='2020-12-18 20:16:58.1530') AND ([EnterUserId] IS NULL) AND ([Record_Deleted]='0') AND ([CreatedBy] IS NULL) AND ([CreatedDate]='2020-12-18 20:19:31.0000') AND ([UpdatedBy] IS NULL) AND ([UpdatedDate] IS NULL);
                dml.Update("UPDATE [SET_Assign_UserGrp] SET [UserId]='"+ddlUserName.SelectedItem.Value+"', [UserGrpId]='"+ddlUsergrp.SelectedItem.Value+"', [EntryDate]='"+txtAppleDate.Text+"', [FromDate]='"+txtFromDate.Text+"', [ToDate]='"+txtTodate.Text+"', [IsActive]='"+chk+"', [Hide]='"+hide+ "',[UpdatedBy]='"+show_username()+ "',[UpdatedDate]='"+DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")+"'  WHERE ([Sno]='" + ViewState["SNO"].ToString()+"')", "");
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
       
        try
        {
            UserGrpID = Request.QueryString["UsergrpID"];
            GridView2.DataBind();
            string swhere;
            string squer = "select * from View_SET_Assign_UserGrp";

            if (ddlDel_UserGRP.SelectedIndex != 0)
            {
                swhere = "UserGrpId = '" + ddlDel_UserGRP.SelectedItem.Value + "'";
            }
            else
            {
                swhere = "UserGrpId is not null";
            }
            if (ddlDel_UserNAme.SelectedIndex != 0)
            {
                swhere = swhere + " and UserId = '" + ddlDel_UserNAme.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + "  and UserId is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0  ORDER BY UserId";


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

            UserGrpID = Request.QueryString["UsergrpID"];
            GridView1.DataBind();
            string swhere;

            string squer = "select * from View_SET_Assign_UserGrp";

            if (ddlFind_USerGrpName.SelectedIndex != 0)
            {
                swhere = "UserGrpId = '" + ddlFind_USerGrpName.SelectedItem.Value + "'";
            }
            else
            {
                swhere = "UserGrpId is not null";
            }
            if (ddlFind_UserNAme.SelectedIndex != 0)
            {
                swhere = swhere + " and UserId = '" + ddlFind_UserNAme.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + "  and UserId is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0   ORDER BY UserId";

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
            UserGrpID = Request.QueryString["UsergrpID"];
            GridView3.DataBind();
            string swhere;
            string squer = "select * from View_SET_Assign_UserGrp";

            if (ddlEdit_USERGRP.SelectedIndex != 0)
            {
                swhere = "UserGrpId = '" + ddlEdit_USERGRP.SelectedItem.Value + "'";
            }
            else
            {
                swhere = "UserGrpId is not null";
            }
            if (ddlEdit_UserName.SelectedIndex != 0)
            {
                swhere = swhere + " and UserId = '" + ddlEdit_UserName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + "  and UserId is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0   ORDER BY UserId";


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
        


      
        ddlUsergrp.Enabled = false;
      
        txtAppleDate.Enabled = false;
      
        rdb_Hide_Y.Enabled = false;
        rdb_Hide_N.Enabled = false;
        txtFromDate.Enabled = false;
        txtTodate.Enabled = false;

        ddlUserName.SelectedIndex = 0;
        ddlUsergrp.SelectedIndex = 0;
        txtCreatedBy.Enabled = false;
        txtcreatedDate.Enabled = false;
        txtUpdatedDate.Enabled = false;
        txtUpdatedBy.Enabled = false;
        txtSortOrder.Enabled = false;
        chkActive.Enabled = false;
        Label1.Text = "";
        txtFromDate.Text = "";
        txtTodate.Text = "";
        ImageButton1.Enabled = false;
        ImageButton2.Enabled = false;
        imgPopup.Enabled = false;
        ddlUserName.Enabled = false;


        txtAppleDate.Text = "";
        txtSortOrder.Text = "";
        txtCreatedBy.Text = "";
        txtcreatedDate.Text = "";
        txtUpdatedDate.Text = "";
        txtUpdatedBy.Text = "";

      
        rdb_Hide_Y.Checked = false;
        rdb_Hide_N.Checked = false;
        txtFromDate.Text= "";
        txtTodate.Text = "";

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
            dml.Delete("update SET_Assign_UserGrp set Record_Deleted = 1 where [Sno] = " + ViewState["SNO"].ToString() + "", "");
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
            DataSet ds = dml.Find("select * from SET_Assign_UserGrp where Sno = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlUsergrp.ClearSelection();
                ddlUserName.ClearSelection();
                ddlUsergrp.Items.FindByValue(ds.Tables[0].Rows[0]["UserGrpId"].ToString()).Selected = true;
                ddlUserName.Items.FindByValue(ds.Tables[0].Rows[0]["UserId"].ToString()).Selected = true;
                txtAppleDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                dml.dateConvert(txtAppleDate);

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                string hide = ds.Tables[0].Rows[0]["Hide"].ToString();
                txtFromDate.Text = dml.dateConvert(ds.Tables[0].Rows[0]["FromDate"].ToString());

                if (ds.Tables[0].Rows[0]["ToDate"].ToString() != "")
                {
                    txtTodate.Text = dml.dateConvert(ds.Tables[0].Rows[0]["ToDate"].ToString());
                }



                txtCreatedBy.Text = ds.Tables[0].Rows[0]["CreatedBy"].ToString();
                txtUpdatedBy.Text = ds.Tables[0].Rows[0]["UpdatedBy"].ToString();

                if (ds.Tables[0].Rows[0]["CreatedDate"].ToString() == "")
                {
                    txtcreatedDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                    txtcreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdatedDate"].ToString() == "")
                {
                    txtUpdatedDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdatedDate"].ToString());
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
                if (hide == "Y")
                {
                    rdb_Hide_Y.Checked = true;
                }
                else
                {
                    rdb_Hide_N.Checked = true;
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
            DataSet ds = dml.Find("select * from SET_Assign_UserGrp where Sno = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlUsergrp.ClearSelection();
                ddlUserName.ClearSelection();
                ddlUsergrp.Items.FindByValue(ds.Tables[0].Rows[0]["UserGrpId"].ToString()).Selected = true;
                ddlUserName.Items.FindByValue(ds.Tables[0].Rows[0]["UserId"].ToString()).Selected = true;
                txtAppleDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                dml.dateConvert(txtAppleDate);

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                string hide = ds.Tables[0].Rows[0]["Hide"].ToString();
                txtFromDate.Text = dml.dateConvert(ds.Tables[0].Rows[0]["FromDate"].ToString());

                if (ds.Tables[0].Rows[0]["ToDate"].ToString() != "")
                {
                    txtTodate.Text = dml.dateConvert(ds.Tables[0].Rows[0]["ToDate"].ToString());
                }



                txtCreatedBy.Text = ds.Tables[0].Rows[0]["CreatedBy"].ToString();
                txtUpdatedBy.Text = ds.Tables[0].Rows[0]["UpdatedBy"].ToString();

                if (ds.Tables[0].Rows[0]["CreatedDate"].ToString() == "")
                {
                    txtcreatedDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                    txtcreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdatedDate"].ToString() == "")
                {
                    txtUpdatedDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdatedDate"].ToString());
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
                if (hide == "Y")
                {
                    rdb_Hide_Y.Checked = true;
                }
                else
                {
                    rdb_Hide_N.Checked = true;
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
       
        txtAppleDate.Enabled = true;
       
        rdb_Hide_Y.Enabled = true;
        rdb_Hide_N.Enabled = true;
        txtSortOrder.Enabled = true;
        txtCreatedBy.Enabled = false;
        txtcreatedDate.Enabled = false;
        txtUpdatedDate.Enabled = false;
        
        txtUpdatedBy.Enabled = false;
        
        txtFromDate.Enabled = true;
        txtTodate.Enabled = true;
        chkActive.Enabled = true;
        ImageButton1.Enabled = true;
        ImageButton2.Enabled = true;
        imgPopup.Enabled = true;
        ddlUserName.Enabled = true;


        updatecol.Visible = true;
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {

            string serial_id;
            serial_id = GridView3.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select * from SET_Assign_UserGrp where Sno = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                
                ddlUsergrp.ClearSelection();
                ddlUserName.ClearSelection();
                ddlUsergrp.Items.FindByValue(ds.Tables[0].Rows[0]["UserGrpId"].ToString()).Selected = true;
                ddlUserName.Items.FindByValue(ds.Tables[0].Rows[0]["UserId"].ToString()).Selected = true;
                txtAppleDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                dml.dateConvert(txtAppleDate);
               
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                string hide = ds.Tables[0].Rows[0]["Hide"].ToString();
                txtFromDate.Text = dml.dateConvert(ds.Tables[0].Rows[0]["FromDate"].ToString());

                if (ds.Tables[0].Rows[0]["ToDate"].ToString() != "")
                {
                    txtTodate.Text = dml.dateConvert(ds.Tables[0].Rows[0]["ToDate"].ToString());
                }

                
             
              

                txtCreatedBy.Text = ds.Tables[0].Rows[0]["CreatedBy"].ToString();
                txtUpdatedBy.Text = ds.Tables[0].Rows[0]["UpdatedBy"].ToString();

                if (ds.Tables[0].Rows[0]["CreatedDate"].ToString() == "")
                {
                    txtcreatedDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                    txtcreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdatedDate"].ToString() == "")
                {
                    txtUpdatedDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdatedDate"].ToString());
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
                if (hide == "Y")
                {
                    rdb_Hide_Y.Checked = true;
                }
                else
                {
                    rdb_Hide_N.Checked = true;
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
    
  
}