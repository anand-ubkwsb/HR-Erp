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

public partial class frm_Period : System.Web.UI.Page
{
    int DateFrom, DeleteDays, EditDays, AddDays;
    string userid, UserGrpID, FormID, fiscal;
    DmlOperation dml = new DmlOperation();
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
            
            dml.dropdownsql(ddlItemHead, "SET_ItemHead", "ItemHeadName", "ItemHeadID");
            //;select ItemSubHeadID,ItemSubHeadName from  SET_ItemSubHead
            dml.dropdownsql(ddlDel_ItemSubHeadName, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");
            dml.dropdownsql(ddlEdit_ItemSubHead, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");
            dml.dropdownsql(ddlFind_ItemSubHeadName, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");



            dml.dropdownsql(ddlEdit_ItemHead, "SET_ItemHead", "ItemHeadName", "ItemHeadID");
            dml.dropdownsql(ddlDel_ItemHead, "SET_ItemHead", "ItemHeadName", "ItemHeadID");
            dml.dropdownsql(ddlFind_ItemHead, "SET_ItemHead", "ItemHeadName", "ItemHeadID");

            dml.dropdownsql(ddlEdit_ItemType, "SET_ItemType", "Description", "ItemTypeID");
            dml.dropdownsql(ddldel_ItemType, "SET_ItemType", "Description", "ItemTypeID");
            dml.dropdownsql(ddlFind_ItemType, "SET_ItemType", "Description", "ItemTypeID");

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

        txtItemSubName.Enabled =true;
        ddlItemHead.Enabled =true;
        btnUpload.Enabled = true;
        txtCreatedBy.Enabled =false;
        txtSystemDate.Enabled =false;
        txtUpadtedDate.Enabled =false;
        txtUpdatedBy.Enabled =false;
        chkActive.Enabled =true;
        
        FileUpload1.Enabled =true;
        chkActive.Checked =true;



        txtSystemDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

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
            int chk;

            if (chkActive.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }

            string maxcode = itemcodemax();


            DataSet ds = dml.Find("select  * from SET_ItemSubHead where ItemSubHeadName = '"+txtItemSubName.Text+ "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "Duplicate Entry not allowed";
            }
            else {

                //string imgpath = "~/dist/img/" + Path.GetFileName(FileUpload1.FileName);
                dml.Insert("INSERT INTO [SET_ItemSubHead] ([GocID], [ItemSubHeadName], [ItemHeadID], [IsActive], [CreatedBy], [CreateDate], [ImageFile], [Record_Deleted],[ItemsubHeadCode],[MLD]) VALUES ('1', '" + txtItemSubName.Text + "', '" + ddlItemHead.SelectedItem.Value + "', '" + chk + "', '" + txtCreatedBy.Text + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '" + imgDisplayArea.ImageUrl + "', '0','" + maxcode + "','"+dml.Encrypt("h")+"');", "alertme()");
                dml.Update("update Set_itemHead set MLD = '" + dml.Encrypt("q") + "' where ItemHeadID = '" + ddlItemHead.SelectedItem.Value + "'", "");

                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);

                Label1.Text = "";
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
            int chk;

            if (chkActive.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }

            string imgpath;

            if (imgDisplayArea.ImageUrl == "")
            {
                imgpath = "([ImageFile] is null)";
            }
            else
            {
                imgpath = "([ImageFile]= '" + imgDisplayArea.ImageUrl + "'";
            }

            DataSet ds_up = dml.Find("select * from SET_ItemSubHead WHERE ([GocID]='1') AND ([ItemSubHeadID]='" + ViewState["SNO"].ToString() + "') AND ([ItemSubHeadName]='" + txtItemSubName.Text+"') AND ([ItemHeadID]='"+ddlItemHead.SelectedItem.Value+"') AND ([IsActive]='"+chk+"') AND "+ imgpath + " ");

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

                //string imgpath = "~/dist/img/" + Path.GetFileName(FileUpload1.FileName);
                userid = Request.QueryString["UserID"];
                txtUpdatedBy.Text = show_username();
                dml.Update("UPDATE [SET_ItemSubHead] SET [ItemSubHeadName]='" + txtItemSubName.Text + "', [ItemHeadID]='" + ddlItemHead.SelectedItem.Value + "', [IsActive]='" + chk + "', [UpdatedBy]='" + txtUpdatedBy.Text + "', [UpdateDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', [ImageFile]='" + imgDisplayArea.ImageUrl + "' WHERE [ItemSubHeadID]='" + ViewState["SNO"].ToString() + "'", "");
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
            string squer = "select * from View_ItemSubHead";

            if (ddlDel_ItemSubHeadName.SelectedIndex != 0)
            {
                swhere = "ItemSubHeadName like '" + ddlDel_ItemSubHeadName.SelectedItem.Text + "%'";
            }
            else
            {
                swhere = "ItemSubHeadName is not null";
            }


            if (ddldel_ItemType.SelectedIndex != 0)
            {
                swhere = swhere + " and ItemTypeID = '" + ddldel_ItemType.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and ItemTypeID is not null";
            }

            if (ddlDel_ItemHead.SelectedIndex != 0)
            {
                swhere = swhere + " and ItemHeadID = '" + ddlDel_ItemHead.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and ItemHeadID is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY ItemSubHeadName";

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
            string squer = "select * from View_ItemSubHead";

            if (ddlFind_ItemSubHeadName.SelectedIndex != 0)
            {
                swhere = "ItemSubHeadName like '" + ddlFind_ItemSubHeadName.SelectedItem.Text + "%'";
            }
            else
            {
                swhere = "ItemSubHeadName is not null";
            }


            if (ddlFind_ItemType.SelectedIndex != 0)
            {
                swhere = swhere + " and ItemTypeID = '" + ddlFind_ItemType.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and ItemTypeID is not null";
            }

            if (ddlFind_ItemHead.SelectedIndex != 0)
            {
                swhere = swhere + " and ItemHeadID = '" + ddlFind_ItemHead.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and ItemHeadID is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY ItemSubHeadName";

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
            string squer = "select * from View_ItemSubHead";

            if (ddlEdit_ItemSubHead.SelectedIndex != 0)
            {
                swhere = "ItemSubHeadName like '" + ddlEdit_ItemSubHead.SelectedItem.Text + "%'";
            }
            else
            {
                swhere = "ItemSubHeadName is not null";
            }


            if (ddlEdit_ItemType.SelectedIndex != 0)
            {
                swhere = swhere + " and ItemTypeID = '" + ddlEdit_ItemType.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and ItemTypeID is not null";
            }

            if (ddlEdit_ItemHead.SelectedIndex != 0)
            {
                swhere = swhere + " and ItemHeadID = '" + ddlEdit_ItemHead.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and ItemHeadID is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY ItemSubHeadName";

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
        txtItemSubName.Enabled = false;
        ddlItemHead.Enabled = false;
       
        txtCreatedBy.Enabled = false;
        txtSystemDate.Enabled = false;
        txtUpadtedDate.Enabled = false;
        txtUpdatedBy.Enabled = false;
        chkActive.Enabled = false;
        Label1.Text = "";
        btnUpload.Enabled = false;

        FileUpload1.Enabled = false;
        imgDisplayArea.ImageUrl = "";

        txtItemSubName.Text = "";
        ddlItemHead.ClearSelection();
        txtCreatedBy.Text = "";
        txtSystemDate.Text = "";
        txtUpadtedDate.Text = "";
        txtUpdatedBy.Text = "";
        chkActive.Checked = false;

        dml.dropdownsql(ddlItemHead, "SET_ItemHead", "ItemHeadName", "ItemHeadID");
        //;select ItemSubHeadID,ItemSubHeadName from  SET_ItemSubHead
        dml.dropdownsql(ddlDel_ItemSubHeadName, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");
        dml.dropdownsql(ddlEdit_ItemSubHead, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");
        dml.dropdownsql(ddlFind_ItemSubHeadName, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID");

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
            dml.Delete("update SET_ItemSubHead set Record_Deleted = 1 where ItemSubHeadID = " + ViewState["SNO"].ToString() + "", "");
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
            ddlItemHead.ClearSelection();
      
            string serial_id;
            serial_id = GridView1.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select * from SET_ItemSubHead where ItemSubHeadID = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtItemSubName.Text = ds.Tables[0].Rows[0]["ItemSubHeadName"].ToString();
                txtCreatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());
                imgDisplayArea.ImageUrl = ds.Tables[0].Rows[0]["ImageFile"].ToString();


                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() == "")
                {
                    txtSystemDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                    txtSystemDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdateDate"].ToString() == "")
                {
                    txtUpadtedDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateDate"].ToString());
                    txtUpadtedDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }

                ddlItemHead.Items.FindByValue(ds.Tables[0].Rows[0]["ItemHeadID"].ToString()).Selected = true;
              
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
        txtUpdatedBy.Enabled = false;
        txtUpadtedDate.Enabled = false;
        textClear();

        updatecol.Visible = true;
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        DeleteBox.Visible = false;
        try
        {
            ddlItemHead.ClearSelection();


            string serial_id;
            serial_id = GridView2.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select * from SET_ItemSubHead where ItemSubHeadID = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtItemSubName.Text = ds.Tables[0].Rows[0]["ItemSubHeadName"].ToString();
                txtCreatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());
                imgDisplayArea.ImageUrl = ds.Tables[0].Rows[0]["ImageFile"].ToString();


                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() == "")
                {
                    txtSystemDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                    txtSystemDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdateDate"].ToString() == "")
                {
                    txtUpadtedDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateDate"].ToString());
                    txtUpadtedDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }

                ddlItemHead.Items.FindByValue(ds.Tables[0].Rows[0]["ItemHeadID"].ToString()).Selected = true;

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

        txtItemSubName.Enabled = true;
        ddlItemHead.Enabled = true;
        
        txtCreatedBy.Enabled = false;
        txtSystemDate.Enabled = false;
        txtUpadtedDate.Enabled = false;
        txtUpdatedBy.Enabled = false;
        btnUpload.Enabled = true;
        chkActive.Enabled = true;
       
        FileUpload1.Enabled = true;

        updatecol.Visible = true;
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {
            ddlItemHead.ClearSelection();


            string serial_id;
            serial_id = GridView3.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select * from SET_ItemSubHead where ItemSubHeadID = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtItemSubName.Text = ds.Tables[0].Rows[0]["ItemSubHeadName"].ToString();


                txtCreatedBy.Text =dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());

                imgDisplayArea.ImageUrl = ds.Tables[0].Rows[0]["ImageFile"].ToString();


                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() == "")
                {
                    txtSystemDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                    txtSystemDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdateDate"].ToString() == "")
                {
                    txtUpadtedDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateDate"].ToString());
                    txtUpadtedDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }

                ddlItemHead.Items.FindByValue(ds.Tables[0].Rows[0]["ItemHeadID"].ToString()).Selected = true;

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


    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            try
            {
                if (FileUpload1.PostedFile.ContentType == "image/jpeg" || FileUpload1.PostedFile.ContentType == "image/png")
                {
                    Label1.Text = "";
                    string filename = Path.GetFileName(FileUpload1.FileName);
                    FileUpload1.SaveAs(Server.MapPath("~/dist/img/") + filename);
                    imgDisplayArea.ImageUrl = "~/dist/img/" + filename;
                    ViewState["img"] = "~/dist/img/" + filename;
                   
                }
                else
                {
                    Label1.Text = "Upload status: Only JPEG files are accepted!";
                }
            }

            catch (Exception ex)
            {
                Label1.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
            }
        }
        else
        {
            Label1.Text = "Please Select the file";
        }
    }

    public string itemcodemax()
    {
        string max = "000";
        int a = 0;
        if (ddlItemHead.SelectedIndex != 0)
        {

            DataSet ds = dml.Find("select max(ItemsubHeadCode) as itemcodemax from SET_ItemSubHead where ItemHeadID='" + ddlItemHead.SelectedItem.Value + "';");
            if (ds.Tables[0].Rows.Count > 0)
            {
                max = ds.Tables[0].Rows[0]["itemcodemax"].ToString();
                if (max == "")
                {
                    max = "0";
                }
            }

            a = int.Parse(max) + 1;



        }
        return a.ToString("000");

    }


    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string id = e.Row.Cells[1].Text;
        bool flag = dml.isNumber(id);


        if (flag == true)
        {

            DataSet ds = dml.Find("select ItemSubHeadID,MLD from SET_ItemSubHead where ItemSubHeadID = '" + id + "'");
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

            DataSet ds = dml.Find("select ItemSubHeadID,MLD from SET_ItemSubHead where ItemSubHeadID = '" + id + "'");
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