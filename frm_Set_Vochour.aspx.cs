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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Telerik.Web.UI;

public partial class frm_Period : System.Web.UI.Page
{
    int AddDays, EditDays, DeleteDays, DateFrom;
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

         
            fd.HideUSerGrpField(Page.Controls, UserGrpID, FormID, "SET_ItemSubHead");
            
            dml.dropdownsql(ddlDocument, "SET_Documents", "DocDescription", "DocID", "DocDescription");
            dml.dropdownsql(ddlUsergrp, "SET_UserGrp", "UserGrpName", "UserGrpId", "Record_Deleted","0");

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

        ddlUsergrp.Enabled = true;
        txtDocumentDesc.Enabled = true;
        txtAppleDate.Enabled = true;
        rdb_Site.Enabled = true;
        rdb_Main.Enabled = true;
        rdb_Hide_Y.Enabled = true;
        rdb_Hide_N.Enabled = true;
        ddlDocument.Enabled = true;

        txtSortOrder.Enabled = true;


        txtCreatedBy.Enabled = false;
        txtcreatedDate.Enabled = false;
        txtUpdatedDate.Enabled = false;
        txtUpdatedBy.Enabled = false;
        chkActive.Enabled =true;
       
        chkActive.Checked =true;

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
                //string imgpath = "~/dist/img/" + Path.GetFileName(FileUpload1.FileName);
                dml.Insert("INSERT INTO [SET_UserGrp_Documents] ([UserGrpId], [DocDescription], [Main_Site], [ApplyDate], [IsActive], [IsHide], [SortOrder],[CreatedBy],[CreateDate] ,[Record_Deleted],[DocID]) VALUES ('"+ddlUsergrp.SelectedItem.Value+"','" + txtDocumentDesc.Text+"', '"+main_site+"', '"+dml.dateconvertforinsert(txtAppleDate)+"', '"+chk+"', '"+hide+"', '"+txtSortOrder.Text+"', '"+show_username()+"', '"+DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")+"', '0','"+ddlDocument.SelectedItem.Value+"')", "alertme()");
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
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
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

            //string imgpath = "~/dist/img/" + Path.GetFileName(FileUpload1.FileName);
            userid = Request.QueryString["UserID"];
            txtUpdatedBy.Text = show_username();
            dml.Update("UPDATE [SET_UserGrp_Documents] SET [DocID] = '"+ddlDocument.SelectedItem.Value+"', [UserGrpId]='"+ddlUsergrp.SelectedItem.Value+"',  [DocDescription]='" + txtDocumentDesc.Text+"', [Main_Site]='"+main_site+"', [ApplyDate]='"+dml.dateconvertforinsert(txtAppleDate)+"', [IsActive]='"+chk+"', [IsHide]='"+hide+"', [SortOrder]='"+txtSortOrder.Text+"', [Record_Deleted]='0', [UpdatedBy]='"+show_username()+"', [UpdateDate]='"+DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")+"' WHERE ([Sno]='"+ViewState["SNO"].ToString()+"')", "");
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
    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        DeleteBox.Visible = true;
        btnFind.Visible = false;
        btnUpdate.Visible = false;
        btnSave.Visible = false;
        btnDelete.Visible = false;
        btnInsert.Visible = false;
        btnCancel.Visible = true;
        btnEdit.Visible = false;
        btnDelete_after.Visible = true;
        try
        {
            GridView2.DataBind();
            string swhere;
            string squer = "select * from SET_UserGrp_Documents";

            if (radDel_DocumentName.Text != "")
            {
                swhere = "DocDescription like '" + radDel_DocumentName.Text + "%'";
            }
            else
            {
                swhere = "DocDescription is not null";
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
            GridView1.DataBind();
            string swhere;
            string squer = "select * from SET_UserGrp_Documents";

            if (radFind_DocumentName.Text != "")
            {
                swhere = "DocDescription like '" + radFind_DocumentName.Text + "%'";
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
            else
            {
                swhere = swhere + " and IsActive is not null";
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
            GridView3.DataBind();
            string swhere;
            string squer = "select * from SET_UserGrp_Documents";

            if (radEdit_DocumetName.Text != "")
            {
                swhere = "DocDescription like '" + radEdit_DocumetName.Text + "%'";
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
            else
            {
                swhere = swhere + " and IsActive is not null";
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


        ddlDocument.Enabled = false;
        ddlUsergrp.Enabled = false;
        txtDocumentDesc.Enabled = false;
        txtAppleDate.Enabled = false;
        rdb_Site.Enabled = false;
        rdb_Main.Enabled = false;
        rdb_Hide_Y.Enabled = false;
        rdb_Hide_N.Enabled = false;

        ddlDocument.SelectedIndex = 0;
        ddlUsergrp.SelectedIndex = 0;
        txtCreatedBy.Enabled = false;
        txtcreatedDate.Enabled = false;
        txtUpdatedDate.Enabled = false;
        txtUpdatedBy.Enabled = false;
        txtSortOrder.Enabled = false;
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
                txtSortOrder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();



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
                txtSortOrder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();

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
                txtSortOrder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();

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

    public void voucher()
    {
        int year_mon = 0;
        DataSet ds_inc = dml.Find("SELECT (SELECT SUBSTRING(MAX(VoucherNo),6,1) from set_voucher where GocId = '"+gocid()+"'and CompId = '"+compid()+"' and BranchId='"+branchId()+"' and FiscalYearID = '"+FiscalYear()+"' and  year_mon_base = '"+year_mon+ "') as ExtractString, year_mon_base from set_voucher  where year_mon_base = '"+year_mon+"'");
        string fiscal = Request.Cookies["fiscalYear"].Value;
        if (ds_inc.Tables[0].Rows.Count > 0)
        {
            string mon_year = ds_inc.Tables[0].Rows[0]["year_mon_base"].ToString();
            string startdate = fiscal.Substring(0, 4);
            string enddate = fiscal.Substring(5, 4);
            int cur_year = DateTime.Now.Year;
            int cur_mon = DateTime.Now.Month;
            if (ds_inc.Tables[0].Rows.Count > 0)
            {
                if (mon_year == "1")
                {

                    string val;
                    int count = 0;
                    if (int.Parse(startdate) <= cur_year && int.Parse(enddate) > cur_year)
                    {
                        string voucher = ds_inc.Tables[0].Rows[0]["ExtractString"].ToString();
                        if (voucher != "")
                        {
                            count = count + int.Parse(voucher) + 1;
                            txtSortOrder.Text = cur_year.ToString().Substring(2, 2) + "-" + count.ToString();

                        }
                        else
                        {
                            count = count + 0 + 1;
                            txtSortOrder.Text = cur_year.ToString().Substring(2,2) + "-" + count.ToString();


                        }
                    }
                    else
                    {
                        txtSortOrder.Text = "please input in current fiscal year";
                    }
                }
                else
                {

                    string val;
                    int count = 0;
                    if (int.Parse(startdate) <= cur_year && int.Parse(enddate) > cur_year)
                    {
                        string voucher = ds_inc.Tables[0].Rows[0]["ExtractString"].ToString();
                        if (voucher != "")
                        {
                            count = count + int.Parse(voucher) + 1;
                            txtSortOrder.Text = cur_year.ToString().Substring(2, 2) + cur_mon + "-" + count.ToString();

                        }
                        else
                        {
                            count = count + 0 + 1;
                            txtSortOrder.Text = cur_year.ToString().Substring(2, 2) + cur_mon + "-" + count.ToString();
                        }
                    }
                    else
                    {
                        txtSortOrder.Text = "please input in current fiscal year";
                    }
                }
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        voucher();
       Session["voc"] = txtSortOrder.Text;
        if (Session["voc"].ToString() == "please input in current fiscal year")
        {
            //alerttime_wr
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alerttime_wr()", true);
        }
        else {
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alerttime()", true);
            //   ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "newFunc", "alerttime()", true);
        }


    }

    protected void Button2_Click(object sender, EventArgs e)
    {

        //ReportDocument crystalReport = new ReportDocument();
        //crystalReport.Load(Server.MapPath("~/rpt_ItemOpenReport.rpt"));


        //DataSet ds = dml.Find("select * from View_forCR_ItemOpen");
        // crystalReport.SetDataSource(ds);

        //CrystalReportViewer1.ReportSource = crystalReport;
        //CrystalReportViewer1.RefreshReport();

    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('frmlogin.aspx','_blank')", true);
    }
}