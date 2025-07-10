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
    int DateFrom, EditDays, DeleteDays, AddDays;
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

            dml.dropdownsql(ddlMenu, "Set_Menu", "Menu_title", "MenuId", "Menu_title");
            dml.dropdownsql(ddlForm, "Set_Form", "FormTitle", "FormId", "FormTitle");
            dml.dropdownsql(ddlDocType, "SET_DocumentType", "DocName", "DocTypeId");


            dml.dropdownsql(ddlEdit_DOCType, "SET_DocumentType", "DocName", "DocTypeId", "DocName");
            dml.dropdownsql(ddlFind_DocType, "SET_DocumentType", "DocName", "DocTypeId", "DocName");
            dml.dropdownsql(ddlDel_DocTypes, "SET_DocumentType", "DocName", "DocTypeId", "DocName");

            dml.dropdownsqldateF(ddlStartDate, "SET_Fiscal_Year", "convert(varchar, StartDate, 106)", "stdate", "FiscalYearID", "StartDate");
            dml.dropdownsqldateF(ddlEndDate, "SET_Fiscal_Year", "convert(varchar, EndDate, 106)", "eddate","FiscalYearID", "EndDate");


            dml.dropdownsql(ddlFind_Menu, "Set_Menu", "Menu_title", "MenuId", "Menu_title");
            dml.dropdownsql(ddlEdit_Menu, "Set_Menu", "Menu_title", "MenuId", "Menu_title");
            dml.dropdownsql(ddlDel_Menu, "Set_Menu", "Menu_title", "MenuId", "Menu_title");

            dml.dropdownsql(ddlDel_Form, "Set_Form", "FormTitle", "FormId", "FormTitle");
            dml.dropdownsql(ddFind_Form, "Set_Form", "FormTitle", "FormId", "FormTitle");
            dml.dropdownsql(ddlEdit_form, "Set_Form", "FormTitle", "FormId", "FormTitle");

            


            fd.HideUSerGrpField(Page.Controls, UserGrpID, FormID, "SET_ItemSubHead");
            //daterangeforfiscal(CalendarExtender1);
            //CalendarExtender1.EndDate = DateTime.Now;
            txtApplyDate.Attributes.Add("readonly", "readonly");

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

        txtDocDES.Enabled = true;
        txtApplyDate.Enabled = true;
        ddlDocType.Enabled = true;
      //  txtdescription.Enabled = true;
        ddlMenu.Enabled = true;
        ddlForm.Enabled = true;
        RadComboAcct_Code.Enabled = true;
        ddlGLImpact.Enabled = true;
        ddlInventoryImpact.Enabled = true;
        txtSortORder.Enabled = true;
        ddlStartDate.Enabled = true;
        ddlEndDate.Enabled = true;

        rdb_Hide_Y.Enabled = true;
        rdb_Hide_N.Enabled = true;
        rdb_hDOCN.Enabled = true;
        rdb_hDOCY.Enabled = true;
        rdb_Monthly.Enabled = true;
        rdb_yearly.Enabled = true;
        rdb_MAC_N.Enabled = true;
        rdb_MAC_Y.Enabled = true;
        rdb_SiteN.Enabled = true;
        rdb_SiteY.Enabled = true;
        rdb_MBP_Y.Enabled = true;
        rdb_MBP_N.Enabled = true;
        chkActive.Enabled = true;
        imgPopup.Enabled = true;
       
        rdb_Hide_N.Checked = true;
        rdb_hDOCY.Checked = true;
        
       rdb_Monthly.Checked = true;
       rdb_MAC_N.Checked = true;
       rdb_SiteY.Checked = true;
       rdb_MBP_N.Checked = true;

        rdb_Monthly.Checked = false;
        rdb_yearly.Checked = false;
        rdb_yearly.Enabled = false;
        rdb_Monthly.Enabled = false;

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
        rdb_hDOCY_CheckedChanged(sender, e);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        userid = Request.QueryString["UserID"];
        UserGrpID = Request.QueryString["UsergrpID"];
        FormID = Request.QueryString["FormID"];
        fiscal = Request.QueryString["fiscaly"];
        bool flags = false;
        try
        {
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
            DataSet uniqueg_B_C = dml.Find("select EndDate from SET_Documents where DocDescription = '" + txtDocDES.Text + "'");
            if (uniqueg_B_C.Tables[0].Rows.Count > 0)
            {
                string ed = uniqueg_B_C.Tables[0].Rows[0]["EndDate"].ToString();
                if (string.IsNullOrEmpty(ed))
                {
                    DataSet dsS = dml.Find("select * from SET_Documents where StartDate < = '" + st_date + "' AND DocDescription = '" + txtDocDES.Text + "'");
                    if (dsS.Tables[0].Rows.Count > 0)
                    {
                        Label1.ForeColor = System.Drawing.Color.Red;
                        Label1.Text = "Duplicated entry not allowed";
                    }

                }
                else
                {
                    string edate = "0";

                    DataSet ds_id = dml.Find("select StartDate from SET_Documents where DocDescription = '" + txtDocDES.Text+ "'");
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


            else {
               
                    int chk, hide, havedoc, mon = 0, year = 0, MAC, MBP, Site;

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
                    if (rdb_hDOCY.Checked == true)
                    {
                        havedoc = 1;
                    }
                    else
                    {
                        havedoc = 0;
                    }

                    if (rdb_Monthly.Checked == true)
                    {
                        mon = 1;
                    }
                    else
                    {
                        mon = 0;
                    }
                    if (rdb_yearly.Checked == true)
                    {
                        year = 1;
                    }
                    else
                    {
                        year = 0;
                    }


                    if (rdb_MAC_Y.Checked == true)
                    {
                        MAC = 1;
                    }
                    else
                    {
                        MAC = 0;
                    }
                    if (rdb_MBP_Y.Checked == true)
                    {
                        MBP = 1;
                    }
                    else
                    {
                        MBP = 0;
                    }
                    if (rdb_SiteY.Checked == true)
                    {
                        Site = 1;
                    }
                    else
                    {
                        Site = 0;
                    }

                    dml.Insert("INSERT INTO [SET_Documents] ([CompID], [DocDescription], [ApplyDate], [DocTypeId], [Description],[StartDate],[EndDate], [MenuId_Sno], [FormId_Sno], [IsHide], [AccountCode], [GLImpact], [InventoryImpact], [HaveDocNo], [MonthlyBase], [YearlyBase], [SortOrder], [IsActive], [MoreAcctCode], [MoreBusinessPartners], [Ho_Site], [UserGrpSno], [CreatedBy], [CreateDate], [Record_Deleted],[MLD]) VALUES (" + compid() + ", '" + txtDocDES.Text.ToUpper() + "', '" + dml.dateconvertforinsert(txtApplyDate) + "', '" + ddlDocType.SelectedItem.Value + "', '" + ddlDocType.SelectedItem.Text + "','" + st_date + "' ,'" + ed_date + "','" + ddlMenu.SelectedItem.Value + "', '" + ddlForm.SelectedItem.Value + "', '" + hide + "', '" + RadComboAcct_Code.Text + "', '" + ddlGLImpact.SelectedItem.Text + "', '" + ddlInventoryImpact.SelectedItem.Text + "', '" + havedoc + "', '" + mon + "', '" + year + "', '" + txtSortORder.Text + "', '" + chk + "', '" + MAC + "', '" + MBP + "', '" + Site + "', NULL, '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "','0','"+dml.Encrypt("h")+"');", "alertme()");
                    dml.Update("update SET_DocumentType set MLD = '" + dml.Encrypt("q") + "' where DocTypeId = '" + ddlDocType.SelectedItem.Value + "'", "");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);
                    Label1.Text = "";
                    txtDocDES.Text = "";
                    txtApplyDate.Text = "";
                    ddlDocType.SelectedIndex = 0;
                    //txtdescription.Text = "";
                    ddlMenu.SelectedIndex = 0;
                    ddlForm.SelectedIndex = 0;
                    RadComboAcct_Code.Text = "";
                    ddlGLImpact.SelectedIndex = 0;
                    ddlInventoryImpact.SelectedIndex = 0;
                    txtSortORder.Text = "";

                    txtCreatedBy.Text = "";
                    txtcreatedDate.Text = "";
                    txtUpdatedDate.Text = "";
                    txtUpdatedBy.Text = "";


                    rdb_Hide_Y.Checked = false;
                    rdb_Hide_N.Checked = false;
                    rdb_hDOCN.Checked = false;
                    rdb_hDOCY.Checked = false;
                    rdb_Monthly.Checked = false;
                    rdb_yearly.Checked = false;
                    rdb_MAC_N.Checked = false;
                    rdb_MAC_Y.Checked = false;
                    rdb_SiteN.Checked = false;
                    rdb_SiteY.Checked = false;
                    rdb_MBP_Y.Checked = false;
                    rdb_MBP_N.Checked = false;
                    chkActive.Checked = false;

                }
            if(flags== true)
            {
                int chk, hide, havedoc, mon = 0, year = 0, MAC, MBP, Site;

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
                if (rdb_hDOCY.Checked == true)
                {
                    havedoc = 1;
                }
                else
                {
                    havedoc = 0;
                }

                if (rdb_Monthly.Checked == true)
                {
                    mon = 1;
                }
                else
                {
                    mon = 0;
                }
                if (rdb_yearly.Checked == true)
                {
                    year = 1;
                }
                else
                {
                    year = 0;
                }


                if (rdb_MAC_Y.Checked == true)
                {
                    MAC = 1;
                }
                else
                {
                    MAC = 0;
                }
                if (rdb_MBP_Y.Checked == true)
                {
                    MBP = 1;
                }
                else
                {
                    MBP = 0;
                }
                if (rdb_SiteY.Checked == true)
                {
                    Site = 1;
                }
                else
                {
                    Site = 0;
                }

                dml.Insert("INSERT INTO [SET_Documents] ([CompID], [DocDescription], [ApplyDate], [DocTypeId], [Description],[StartDate],[EndDate], [MenuId_Sno], [FormId_Sno], [IsHide], [AccountCode], [GLImpact], [InventoryImpact], [HaveDocNo], [MonthlyBase], [YearlyBase], [SortOrder], [IsActive], [MoreAcctCode], [MoreBusinessPartners], [Ho_Site], [UserGrpSno], [CreatedBy], [CreateDate], [Record_Deleted],[MLD]) VALUES ('" + compid() + "', '" + txtDocDES.Text + "', '" + dml.dateconvertforinsert(txtApplyDate) + "', '" + ddlDocType.SelectedItem.Value + "', '" + ddlDocType.SelectedItem.Text + "','" + st_date + "' ,'" + ed_date + "','" + ddlMenu.SelectedItem.Value + "', '" + ddlForm.SelectedItem.Value + "', '" + hide + "', '" + RadComboAcct_Code.Text + "', '" + ddlGLImpact.SelectedItem.Text + "', '" + ddlInventoryImpact.SelectedItem.Text + "', '" + havedoc + "', '" + mon + "', '" + year + "', '" + txtSortORder.Text + "', '" + chk + "', '" + MAC + "', '" + MBP + "', '" + Site + "', NULL, '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "','0','"+dml.Encrypt("h")+"');", "alertme()");
                dml.Update("update SET_DocumentType set MLD = '" + dml.Encrypt("q") + "' where DocTypeId = '" + ddlDocType.SelectedItem.Value + "'", "");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);
                Label1.Text = "";
                txtDocDES.Text = "";
                txtApplyDate.Text = "";
                ddlDocType.SelectedIndex = 0;
              //  txtdescription.Text = "";
                ddlMenu.SelectedIndex = 0;
                ddlForm.SelectedIndex = 0;
                RadComboAcct_Code.Text = "";
                ddlGLImpact.SelectedIndex = 0;
                ddlInventoryImpact.SelectedIndex = 0;
                txtSortORder.Text = "";

                txtCreatedBy.Text = "";
                txtcreatedDate.Text = "";
                txtUpdatedDate.Text = "";
                txtUpdatedBy.Text = "";


                rdb_Hide_Y.Checked = false;
                rdb_Hide_N.Checked = false;
                rdb_hDOCN.Checked = false;
                rdb_hDOCY.Checked = false;
                rdb_Monthly.Checked = false;
                rdb_yearly.Checked = false;
                rdb_MAC_N.Checked = false;
                rdb_MAC_Y.Checked = false;
                rdb_SiteN.Checked = false;
                rdb_SiteY.Checked = false;
                rdb_MBP_Y.Checked = false;
                rdb_MBP_N.Checked = false;
                chkActive.Checked = false;
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
            string code = "";

            int chk, hide, havedoc, mon = 0, year = 0, MAC, MBP, Site;

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
            if (rdb_hDOCY.Checked == true)
            {
                havedoc = 1;
            }
            else
            {
                havedoc = 0;
            }

            if (rdb_Monthly.Checked == true)
            {
                mon = 1;
            }
            else
            {
                mon = 0;
            }
            if (rdb_yearly.Checked == true)
            {
                year = 1;
            }
            else
            {
                year = 0;
            }


            if (rdb_MAC_Y.Checked == true)
            {
                MAC = 1;
            }
            else
            {
                MAC = 0;
            }
            if (rdb_MBP_Y.Checked == true)
            {
                MBP = 1;
            }
            else
            {
                MBP = 0;
            }
            if (rdb_SiteY.Checked == true)
            {
                Site = 1;
            }
            else
            {
                Site = 0;
            }
            if (RadComboAcct_Code.Text != "")
            {
                code = "([AccountCode] =  '" + RadComboAcct_Code.Text + "')";
            }
            else
            {
                code = "([AccountCode] IS NULL)";
            }
            string val = "791F7F5E-BC4C-4018-A5E6-817EA65D67DC";
            if (ddlForm.SelectedIndex != 0)
            {
                val = ddlForm.SelectedItem.Value;
            }

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
            DataSet ds_up = dml.Find("select * from SET_Documents WHERE ([DocID]='"+ViewState["SNO"].ToString()+"') AND ([DocDescription]='"+txtDocDES.Text+ "') AND ([StartDate] ='"+st_date+ "') AND ([EndDate] ='" + ed_date + "') AND ([ApplyDate]='" + txtApplyDate.Text+"') AND ([DocTypeId]='"+ddlDocType.SelectedItem.Value+"') AND ([Description]='"+ddlDocType.SelectedItem.Text+"') AND ([MenuId_Sno]='"+ddlMenu.SelectedItem.Value+"') AND ([FormId_Sno]='"+ ddlForm.SelectedItem.Value + "') AND ([IsHide]='"+hide+"') AND "+ code + " AND ([GLImpact]='"+ddlGLImpact.SelectedItem.Text+"') AND ([InventoryImpact]='"+ddlInventoryImpact.SelectedItem.Text+"') AND ([HaveDocNo]='"+havedoc+"') AND ([MonthlyBase]='"+mon+"') AND ([YearlyBase]='"+year+"') AND ([SortOrder]='"+txtSortORder.Text+"') AND ([IsActive]='"+chk+"') AND ([MoreAcctCode]='"+MAC+"') AND ([MoreBusinessPartners]='"+MBP+"') AND ([Ho_Site]='"+Site+"') AND ([Record_Deleted]='0')");
                        
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

                userid = Request.QueryString["UserID"];
                txtUpdatedBy.Text = show_username();
                dml.Update("UPDATE [SET_Documents] SET [CompID]='" + compid() + "', [DocDescription]='" + txtDocDES.Text + "',[StartDate] ='" + st_date + "' , [EndDate] ='" + ed_date + "', [ApplyDate]='" + dml.dateconvertforinsert(txtApplyDate) + "', [DocTypeId]='" + ddlDocType.SelectedItem.Value + "', [Description]='" + ddlDocType.SelectedItem.Text + "', [MenuId_Sno]='" + ddlMenu.SelectedItem.Value + "', [FormId_Sno]='" + val + "', [IsHide]='" + hide + "', [AccountCode]='" + RadComboAcct_Code.Text + "', [GLImpact]='" + ddlGLImpact.SelectedItem.Text + "', [InventoryImpact]='" + ddlInventoryImpact.SelectedItem.Text + "', [HaveDocNo]='" + havedoc + "', [MonthlyBase]='" + mon + "', [YearlyBase]='" + year + "', [SortOrder]='" + txtSortORder.Text + "', [IsActive]='" + chk + "', [MoreAcctCode]='" + MAC + "', [MoreBusinessPartners]='" + MBP + "', [Ho_Site]='" + Site + "', [UserGrpSno]='1', [UpdatedBy]='" + txtUpdatedBy.Text + "', [UpdateDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' WHERE ([DocID]='" + ViewState["SNO"].ToString() + "');", "");
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
            string swhere;
            string squer = "select * from view_Setdoc";

            if (ddlDel_DocTypes.SelectedIndex != 0)
            {
                swhere = "DocName = '" + ddlDel_DocTypes.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "DocName is not null";
            }

            if (radDel_DocumentName.Text != "")
            {
                swhere = swhere + " and DocDescription = '" + radDel_DocumentName.Text + "'";
            }
            else
            {
                swhere = swhere + " and DocDescription is not null";
            }


            if (ddlDel_Menu.SelectedIndex != 0)
            {
                swhere = swhere + " and MenuId_Sno = '" + ddlDel_Menu.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and MenuId_Sno is not null";
            }

            if (ddlDel_Form.SelectedIndex != 0)
            {
                swhere = swhere + " and FormId_Sno = '" + ddlDel_Form.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and FormId_Sno is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0 and CompID = '" + compid() + "' ORDER BY DocDescription";

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
            string squer = "select * from view_Setdoc";

            if (ddlFind_DocType.SelectedIndex != 0)
            {
                swhere = "DocName = '" + ddlFind_DocType.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "DocName is not null";
            }

            if (radFind_DocumentName.Text != "")
            {
                swhere = swhere + " and DocDescription = '" + radFind_DocumentName.Text + "'";
            }
            else
            {
                swhere = swhere + " and DocDescription is not null";
            }

            if (ddlFind_Menu.SelectedIndex != 0)
            {
                swhere = swhere + " and MenuId_Sno = '" + ddlFind_Menu.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and MenuId_Sno is not null";
            }

            if (ddFind_Form.SelectedIndex != 0)
            {
                swhere = swhere + " and FormId_Sno = '" + ddFind_Form.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and FormId_Sno is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0 and CompID = '" + compid() +"'  ORDER BY DocDescription";

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
            string squer = "select * from view_Setdoc";


            if (ddlEdit_DOCType.SelectedIndex != 0)
            {
                swhere = "DocName = '" + ddlEdit_DOCType.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "DocName is not null";
            }

            if (radEdit_DocumetName.Text != "")
            {
                swhere = swhere + " and DocDescription = '" + radEdit_DocumetName.Text + "'";
            }
            else
            {
                swhere = swhere + " and DocDescription is not null";
            }


            if (ddlEdit_Menu.SelectedIndex != 0)
            {
                swhere = swhere + " and MenuId_Sno = '" + ddlEdit_Menu.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and MenuId_Sno is not null";
            }

            if (ddlEdit_form.SelectedIndex != 0)
            {
                swhere = swhere + " and FormId_Sno = '" + ddlEdit_form.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and FormId_Sno is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0 and CompID = '" + compid() + "' ORDER BY DocDescription";

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

        ddlStartDate.Enabled = false;
        ddlEndDate.Enabled = false;
        txtDocDES.Enabled = false;
        txtApplyDate.Enabled = false;
        ddlDocType.Enabled = false;
        //txtdescription.Enabled = false;
        ddlMenu.Enabled = false;
        ddlForm.Enabled = false;
        RadComboAcct_Code.Enabled = false;
        ddlGLImpact.Enabled = false;
        ddlInventoryImpact.Enabled = false;
        txtSortORder.Enabled = false;
        txtCreatedBy.Enabled = false;
        txtcreatedDate.Enabled = false;
        txtUpdatedDate.Enabled = false;
        txtUpdatedBy.Enabled = false;
      
        imgPopup.Enabled = false;

        rdb_Hide_Y.Enabled = false;
        rdb_Hide_N.Enabled = false;
        rdb_hDOCN.Enabled = false;
        rdb_hDOCY.Enabled = false;
        rdb_Monthly.Enabled = false;
        rdb_yearly.Enabled = false;
        rdb_MAC_N.Enabled = false;
        rdb_MAC_Y.Enabled = false;
        rdb_SiteN.Enabled = false;
        rdb_SiteY.Enabled = false; 
        rdb_MBP_Y.Enabled = false;
        rdb_MBP_N.Enabled = false;
        chkActive.Enabled = false;



        Label1.Text = "";
        
        txtDocDES.Text = "";
        txtApplyDate.Text = "";
        ddlDocType.SelectedIndex = 0;
       // txtdescription.Text = "";
        ddlMenu.SelectedIndex = 0;
        ddlForm.SelectedIndex = 0;
        RadComboAcct_Code.Text = "";
        ddlGLImpact.SelectedIndex = 0;
        ddlInventoryImpact.SelectedIndex = 0;
        txtSortORder.Text = "";
        ddlStartDate.SelectedIndex = 0;
        ddlEndDate.SelectedIndex = 0;
        txtCreatedBy.Text = "";
        txtcreatedDate.Text = "";
        txtUpdatedDate.Text = "";
        txtUpdatedBy.Text = "";
       

        rdb_Hide_Y.Checked = false;
        rdb_Hide_N.Checked = false;
        rdb_hDOCN.Checked = false;
        rdb_hDOCY.Checked = false;
        rdb_Monthly.Checked = false;
        rdb_yearly.Checked = false;
        rdb_MAC_N.Checked = false;
        rdb_MAC_Y.Checked = false;
        rdb_SiteN.Checked = false;
        rdb_SiteY.Checked = false;
        rdb_MBP_Y.Checked = false;
        rdb_MBP_N.Checked = false;
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
            dml.Delete("update SET_Documents set Record_Deleted = 1 where [DocID] = " + ViewState["SNO"].ToString() + "", "");
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
            DataSet ds = dml.Find("select * from SET_Documents where DocID = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlForm.ClearSelection();
                ddlMenu.ClearSelection();
                ddlGLImpact.ClearSelection();
                ddlInventoryImpact.ClearSelection();
                RadComboAcct_Code.ClearSelection();
                ddlDocType.ClearSelection();

                txtDocDES.Text = ds.Tables[0].Rows[0]["DocDescription"].ToString();
                txtApplyDate.Text = ds.Tables[0].Rows[0]["ApplyDate"].ToString();
                ddlDocType.Items.FindByValue(ds.Tables[0].Rows[0]["DocTypeId"].ToString()).Selected = true;
               // txtdescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                ddlMenu.Items.FindByValue(ds.Tables[0].Rows[0]["MenuId_Sno"].ToString()).Selected = true;
                ddlForm.Items.FindByValue(ds.Tables[0].Rows[0]["FormId_Sno"].ToString()).Selected = true;
                RadComboAcct_Code.Text = ds.Tables[0].Rows[0]["AccountCode"].ToString();

                ddlGLImpact.Items.FindByText(ds.Tables[0].Rows[0]["GLImpact"].ToString()).Selected = true;
                ddlInventoryImpact.Items.FindByText(ds.Tables[0].Rows[0]["InventoryImpact"].ToString()).Selected= true;

                txtSortORder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();
                              
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                bool hide = bool.Parse(ds.Tables[0].Rows[0]["IsHide"].ToString());
                bool havedoc = bool.Parse(ds.Tables[0].Rows[0]["HaveDocNo"].ToString());
                bool mon = bool.Parse(ds.Tables[0].Rows[0]["MonthlyBase"].ToString());
                bool year = bool.Parse(ds.Tables[0].Rows[0]["YearlyBase"].ToString());
                bool MAC = bool.Parse(ds.Tables[0].Rows[0]["MoreAcctCode"].ToString());
                bool MBP = bool.Parse(ds.Tables[0].Rows[0]["MoreBusinessPartners"].ToString());
                bool Site = bool.Parse(ds.Tables[0].Rows[0]["Ho_Site"].ToString());

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

                if (hide == true)
                {
                    rdb_Hide_Y.Checked = true;
                }
                else
                {
                    rdb_Hide_N.Checked = true;
                }

                if (havedoc == true)
                {
                    rdb_hDOCY.Checked = true;
                }
                else
                {
                    rdb_hDOCN.Checked = true;
                }


                if (mon == true)
                {
                    rdb_Monthly.Checked = true;
                }
                else
                {
                    rdb_yearly.Checked = true;
                }

                if (year == true)
                {
                    rdb_yearly.Checked = true;
                }
                else
                {
                    rdb_Monthly.Checked = true;
                }

                if (MAC == true)
                {
                    rdb_MAC_Y.Checked = true;
                }
                else
                {
                    rdb_MAC_N.Checked = true;
                }

                if (MBP == true)
                {
                    rdb_MBP_Y.Checked = true;
                }
                else
                {
                    rdb_MBP_N.Checked = true;
                }

                if (Site == true)
                {
                    rdb_SiteY.Checked = true;
                }
                else
                {
                    rdb_SiteN.Checked = true;
                }



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


                dml.dateConvert(txtApplyDate);

                ddlStartDate.ClearSelection();
                ddlEndDate.ClearSelection();
                dml.dateConvert(txtApplyDate);

                string s = dml.dateConvert(ds.Tables[0].Rows[0]["StartDate"].ToString());
                string d = dml.dateConvert(ds.Tables[0].Rows[0]["EndDate"].ToString());
                string sR = s.Replace('-', ' ');
                string dR = d.Replace('-', ' ');

                ddlStartDate.Items.FindByText(sR).Selected = true;
                if (dR != "01 Jan 2000")
                {
                    ddlEndDate.Items.FindByText(dR).Selected = true;
                }
                else
                {
                    ddlEndDate.SelectedIndex = 0;
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
            DataSet ds = dml.Find("select * from SET_Documents where DocID = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlForm.ClearSelection();
                ddlMenu.ClearSelection();
                ddlGLImpact.ClearSelection();
                ddlInventoryImpact.ClearSelection();
                RadComboAcct_Code.ClearSelection();
                ddlDocType.ClearSelection();

                txtDocDES.Text = ds.Tables[0].Rows[0]["DocDescription"].ToString();
                txtApplyDate.Text = ds.Tables[0].Rows[0]["ApplyDate"].ToString();
                ddlDocType.Items.FindByValue(ds.Tables[0].Rows[0]["DocTypeId"].ToString()).Selected = true;
               // txtdescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                ddlMenu.Items.FindByValue(ds.Tables[0].Rows[0]["MenuId_Sno"].ToString()).Selected = true;
                ddlForm.Items.FindByValue(ds.Tables[0].Rows[0]["FormId_Sno"].ToString()).Selected = true;
                RadComboAcct_Code.Text = ds.Tables[0].Rows[0]["AccountCode"].ToString();
                ddlGLImpact.Items.FindByText(ds.Tables[0].Rows[0]["GLImpact"].ToString()).Selected = true;
                ddlInventoryImpact.Items.FindByText(ds.Tables[0].Rows[0]["InventoryImpact"].ToString()).Selected = true;
                txtSortORder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                bool hide = bool.Parse(ds.Tables[0].Rows[0]["IsHide"].ToString());
                bool havedoc = bool.Parse(ds.Tables[0].Rows[0]["HaveDocNo"].ToString());
                bool mon = bool.Parse(ds.Tables[0].Rows[0]["MonthlyBase"].ToString());
                bool year = bool.Parse(ds.Tables[0].Rows[0]["YearlyBase"].ToString());
                bool MAC = bool.Parse(ds.Tables[0].Rows[0]["MoreAcctCode"].ToString());
                bool MBP = bool.Parse(ds.Tables[0].Rows[0]["MoreBusinessPartners"].ToString());
                bool Site = bool.Parse(ds.Tables[0].Rows[0]["Ho_Site"].ToString());

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

                if (hide == true)
                {
                    rdb_Hide_Y.Checked = true;
                }
                else
                {
                    rdb_Hide_N.Checked = true;
                }

                if (havedoc == true)
                {
                    rdb_hDOCY.Checked = true;
                }
                else
                {
                    rdb_hDOCN.Checked = true;
                }


                if (mon == true)
                {
                    rdb_Monthly.Checked = true;
                }
                else
                {
                    rdb_yearly.Checked = true;
                }

                if (year == true)
                {
                    rdb_yearly.Checked = true;
                }
                else
                {
                    rdb_Monthly.Checked = true;
                }

                if (MAC == true)
                {
                    rdb_MAC_Y.Checked = true;
                }
                else
                {
                    rdb_MAC_N.Checked = true;
                }

                if (MBP == true)
                {
                    rdb_MBP_Y.Checked = true;
                }
                else
                {
                    rdb_MBP_N.Checked = true;
                }

                if (Site == true)
                {
                    rdb_SiteY.Checked = true;
                }
                else
                {
                    rdb_SiteN.Checked = true;
                }



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


                dml.dateConvert(txtApplyDate);
                ddlStartDate.ClearSelection();
                ddlEndDate.ClearSelection();
                dml.dateConvert(txtApplyDate);

                string s = dml.dateConvert(ds.Tables[0].Rows[0]["StartDate"].ToString());
                string d = dml.dateConvert(ds.Tables[0].Rows[0]["EndDate"].ToString());
                string sR = s.Replace('-', ' ');
                string dR = d.Replace('-', ' ');

                ddlStartDate.Items.FindByText(sR).Selected = true;
                if (dR != "01 Jan 2000")
                {
                    ddlEndDate.Items.FindByText(dR).Selected = true;
                }
                else
                {
                    ddlEndDate.SelectedIndex = 0;
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

        txtDocDES.Enabled = true;
        txtApplyDate.Enabled = true;
        ddlDocType.Enabled = true;
       // txtdescription.Enabled = true;
        ddlMenu.Enabled = true;
        ddlForm.Enabled = true;
        RadComboAcct_Code.Enabled = true;
        ddlGLImpact.Enabled = true;
        ddlInventoryImpact.Enabled = true;
        txtSortORder.Enabled = true;
        imgPopup.Enabled = true;
        chkActive.Enabled = true;

        ddlStartDate.Enabled = true;
        ddlEndDate.Enabled = true;
        rdb_Hide_Y.Enabled = true;
        rdb_Hide_N.Enabled = true;
        rdb_hDOCN.Enabled = true;
        rdb_hDOCY.Enabled = true;
        rdb_Monthly.Enabled = true;
        rdb_yearly.Enabled = true;
        rdb_MAC_N.Enabled = true;
        rdb_MAC_Y.Enabled = true;
        rdb_SiteN.Enabled = true;
        rdb_SiteY.Enabled = true;
        rdb_MBP_Y.Enabled = true;
        rdb_MBP_N.Enabled = true;
        chkActive.Enabled = true;
       
        
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
            DataSet ds = dml.Find("select * from SET_Documents where DocID = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlForm.ClearSelection();
                ddlMenu.ClearSelection();
                ddlGLImpact.ClearSelection();
                ddlInventoryImpact.ClearSelection();
                RadComboAcct_Code.ClearSelection();
                ddlDocType.ClearSelection();
               

                txtDocDES.Text = ds.Tables[0].Rows[0]["DocDescription"].ToString();
                txtApplyDate.Text = ds.Tables[0].Rows[0]["ApplyDate"].ToString();
                ddlDocType.Items.FindByValue(ds.Tables[0].Rows[0]["DocTypeId"].ToString()).Selected = true;
              //  txtdescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();


                

                ddlMenu.Items.FindByValue(ds.Tables[0].Rows[0]["MenuId_Sno"].ToString()).Selected = true;
                ddlForm.Items.FindByValue(ds.Tables[0].Rows[0]["FormId_Sno"].ToString()).Selected = true;

                RadComboAcct_Code.Text = ds.Tables[0].Rows[0]["AccountCode"].ToString();
                ddlGLImpact.Items.FindByText(ds.Tables[0].Rows[0]["GLImpact"].ToString()).Selected = true;
                ddlInventoryImpact.Items.FindByText(ds.Tables[0].Rows[0]["InventoryImpact"].ToString()).Selected = true;
                txtSortORder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                bool hide = bool.Parse(ds.Tables[0].Rows[0]["IsHide"].ToString());
                bool havedoc = bool.Parse(ds.Tables[0].Rows[0]["HaveDocNo"].ToString());
                bool mon = bool.Parse(ds.Tables[0].Rows[0]["MonthlyBase"].ToString());
                bool year = bool.Parse(ds.Tables[0].Rows[0]["YearlyBase"].ToString());
                bool MAC = bool.Parse(ds.Tables[0].Rows[0]["MoreAcctCode"].ToString());
                bool MBP = bool.Parse(ds.Tables[0].Rows[0]["MoreBusinessPartners"].ToString());
                bool Site = bool.Parse(ds.Tables[0].Rows[0]["Ho_Site"].ToString());

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

                if (hide == true)
                {
                    rdb_Hide_Y.Checked = true;
                }
                else
                {
                    rdb_Hide_N.Checked = true;
                }

                if (havedoc == true)
                {
                    rdb_hDOCY.Checked = true;
                }
                else
                {
                    rdb_hDOCN.Checked = true;
                }


                if (mon == true)
                {
                    rdb_Monthly.Checked = true;
                }
                else
                {
                    rdb_yearly.Checked = true;
                }

                if (year == true)
                {
                    rdb_yearly.Checked = true;
                }
                else
                {
                    rdb_Monthly.Checked = true;
                }

                if (MAC == true)
                {
                    rdb_MAC_Y.Checked = true;
                }
                else
                {
                    rdb_MAC_N.Checked = true;
                }

                if (MBP == true)
                {
                    rdb_MBP_Y.Checked = true;
                }
                else
                {
                    rdb_MBP_N.Checked = true;
                }

                if (Site == true)
                {
                    rdb_SiteY.Checked = true;
                }
                else
                {
                    rdb_SiteN.Checked = true;
                }



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

                ddlStartDate.ClearSelection();
                ddlEndDate.ClearSelection();
                dml.dateConvert(txtApplyDate);

                string s = dml.dateConvert(ds.Tables[0].Rows[0]["StartDate"].ToString());
                string d = dml.dateConvert(ds.Tables[0].Rows[0]["EndDate"].ToString());
                string sR= s.Replace('-', ' ');
                string dR = d.Replace('-', ' ');

                ddlStartDate.Items.FindByText(sR).Selected = true;
                if (dR != "01 Jan 2000")
                {
                    ddlEndDate.Items.FindByText(dR).Selected = true;
                }
                else
                {
                    ddlEndDate.SelectedIndex = 0;
                }
              





                }
        }
        catch (Exception ex)
        {
           // Label1.Text = ex.Message;
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
      
        cmb.serachcombo2(radEdit_DocumetName, e.Text, "DocDescription", "DocID", "Menu_title", "View_findDocumnet", where, "DocDescription");

    }
    protected void radFind_DocumetName_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo2(radFind_DocumentName, e.Text, "DocDescription", "DocID", "Menu_title", "View_findDocumnet", where, "DocDescription");

    }
    protected void radDel_DocumentName_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo2(radDel_DocumentName, e.Text, "DocDescription", "DocID", "Menu_title", "View_findDocumnet", where, "DocDescription");

    }
    protected void RadComboAcct_Code_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        //SELECT Acct_Code,Acct_Description,Acct_Type_Name,Tran_Type from view_Search_Acct_Code
        //Select * from SET_COA_detail where (Head_detail_ID = 'd1') and (Acct_Type_ID = '1' OR Acct_Type_ID = '2' or Acct_Type_ID = '3' or  Acct_Type_ID = '6')
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(RadComboAcct_Code, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

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


    protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMenu.SelectedIndex > 0)
        {
            dml.dropdownsql(ddlForm, "Set_Form", "FormTitle", "FormId", "MenuId", ddlMenu.SelectedItem.Value);
        }
        else
        {
            ddlForm.SelectedIndex = 0;
        }

    }

    public void daterangeforfiscal(AjaxControlToolkit.CalendarExtender cal1)
    {
        DataSet ds = dml.Find("select StartDate, EndDate from SET_Fiscal_Year where Description = '"+ Request.Cookies["fiscalYear"].Value + "'");
        if(ds.Tables[0].Rows.Count > 0)
        {
            string startdate1 = ds.Tables[0].Rows[0]["StartDate"].ToString();
            string enddate1 = ds.Tables[0].Rows[0]["EndDate"].ToString();

            DateTime startdate = DateTime.Parse(startdate1);
            DateTime enddate = DateTime.Parse(enddate1);

            cal1.StartDate = startdate;
            cal1.EndDate = enddate;

            
        }
    }

    protected void rdb_hDOCY_CheckedChanged(object sender, EventArgs e)
    {
       if(rdb_hDOCY.Checked == true)
        {
            rdb_Monthly.Checked = true;
            rdb_yearly.Checked = false;
            rdb_yearly.Enabled = true;
            rdb_Monthly.Enabled = true;
        }
    }

    protected void rdb_hDOCN_CheckedChanged(object sender, EventArgs e)
    {
        if (rdb_hDOCN.Checked == true)
        {
            rdb_Monthly.Checked = false;
            rdb_yearly.Checked = false;
            rdb_yearly.Enabled = false;
            rdb_Monthly.Enabled = false;
        }
    }




    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string id = e.Row.Cells[1].Text;
        bool flag = dml.isNumber(id);


        if (flag == true)
        {

            DataSet ds = dml.Find("select DocId,MLD from SET_Documents where DocId = '" + id + "'");
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

            DataSet ds = dml.Find("select DocId,MLD from SET_Documents where DocId = '" + id + "'");
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