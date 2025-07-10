using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Configuration;

public partial class frm_SET_COA_Master : System.Web.UI.Page
{
    int DateFrom, AddDays, EditDays, DeleteDays;
    string userid, UserGrpID, FormID;
    DmlOperation dml = new DmlOperation();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
    radcomboxclass cmb = new radcomboxclass();
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
            //CalendarExtender2.EndDate = DateTime.Now;
            rowid.Visible = false;
            Deletebox.Visible = false;
            Editbox.Visible = false;
            btnDelete_after.Visible = false;
            lblaccout_code.Visible = false;

            fd.HideUSerGrpField(Page.Controls, UserGrpID, FormID, "SET_COA_detail");
            Showall_Dml();
            try
            {
                dml.dropdownsql(ddlAcc_type, "SET_Acct_Type", "Acct_Type_Name", "Acct_Type_Id");
                dml.dropdownsql(ddlGOC, "SET_GOC", "GOCName", "GocId");

                dml.dropdownsql(txtDelete_AccountDesc, "SET_COA_detail", "Acct_Description", "COA_D_ID");
                dml.dropdownsql(txtDelete_AccountType, "SET_Acct_Type", "Acct_Type_Name", "Acct_Type_Id");
                dml.dropdownsql(txtDelete_AccountStype, "SET_Acct_Sub_Type", "Acct_Sub_Type_Name", "Acct_Sub_Type_Id");

                dml.dropdownsql(txtFind_AccountDesc, "SET_COA_detail", "Acct_Description", "COA_D_ID");
                dml.dropdownsql(txtFind_AccountType, "SET_Acct_Type", "Acct_Type_Name", "Acct_Type_Id");
                dml.dropdownsql(txtFind_AccountSubT, "SET_Acct_Sub_Type", "Acct_Sub_Type_Name", "Acct_Sub_Type_Id");

                dml.dropdownsql(txtEdit_AccountDes, "SET_COA_detail", "Acct_Description", "COA_D_ID");
                dml.dropdownsql(txtEdit_AccountType, "SET_Acct_Type", "Acct_Type_Name", "Acct_Type_Id");
                dml.dropdownsql(txtEdit_AccountSubType, "SET_Acct_Sub_Type", "Acct_Sub_Type_Name", "Acct_Sub_Type_Id");
                // dml.dropdownsql(txtDelete_TransType, "SET_Acct_Type", "Acct_Type_Name", "Acct_Type_Id");



                textclear();
                
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
            

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
        //DataSet ds = dml.Find("select * from SET_UserGrp_Form where FormId='" + FormID + "' and DmlAllowed = 'Y'");
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
        //    btnCancel.Visible = false;
        //}
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int tempCompid = 0;
            int head_detail = 0;
            int chk = 0;
            if (chkActive.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
            string str = combination();

            if (FindDate() > 0)
            {
                if (ViewState["comp_Goc"].ToString() == "False")
                {
                    tempCompid = 0;
                }
                else {
                    tempCompid = compid();
                }
            }

           

            txtAcoount_Code.Text = lblaccout_code.Text;
            accountCode_increment();


            //select Acct_Code from SET_COA_detail where Acct_Code = '"+trimcode(txtAcoount_Code.Text)+ "' and IsActive = '1' and Record_Deleted = 0 and gocid = 1 and Compid = 3 order by Acct_Code;
            DataSet ds = dml.Find("select Acct_Code from SET_COA_detail where Acct_Code = '" + trimcode(txtAcoount_Code.Text) + "' and IsActive = '1' and Record_Deleted = 0 and gocid = '"+gocid()+"' and Compid = '"+ tempCompid + "' order by Acct_Code");
            if (ds.Tables[0].Rows.Count > 0)
            {
                Label1.ForeColor = Color.Red;
                Label1.Text = "Account code already inserted ";
            }
            else {

                dml.Insert("INSERT INTO SET_COA_detail ([COA_M_ID], [Acct_Code], [Acct_Description], [Head_detail_ID], [Acct_Type_ID], [Acct_Sub_Type_Id], [Head_Acct_Code], [Acct_Sub_Type], [SysDate], [User], [GocId],[Compid],[IsActive], [Bar_Code], [Record_Deleted]) VALUES ('" + COAMasterID() + "', '" + trimcode(txtAcoount_Code.Text) + "', '" + txtAccountDescrip.Text.ToUpper() + "', '" + headdetail() + "', '" + ddlAcc_type.SelectedItem.Value + "', '" + ddlAcc_sub_type.SelectedItem.Value + "', '" + txtbaseAcc_Code.Text + "', NULL, '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', '" + show_username() + "', " + gocid() + ", '" + tempCompid + "', '" + chk + "', '" + txtAcoount_Code.Text + "', '0');", "");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "alertme()", true);
                btnInsert_Click(sender, e);
            }
            
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
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = ex.Message;
        }
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        btnSave.Visible = true;
        btnEdit.Visible = false;
        btnFind.Visible = false;
        btnCancel.Visible = true;
        btnDelete.Visible = false;
        btnInsert.Visible = false;
        btnUpdate.Visible = false;

        Findbox.Visible = false;
        Deletebox.Visible = false;
        Editbox.Visible = false;
        fieldbox.Visible = true;

        txtSystemDATE.Enabled = false;
        LinklblGenerarte.Visible = true;
        txtSystemDATE.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff ");
        ddlGOC.SelectedValue = gocid().ToString();
        txtAcoount_Code.Enabled = true;

        chkActive.Checked = true;
        txtAccountDescrip.Enabled = true;
        txtHead_Detail.Enabled = false;
        ddlAcc_type.Enabled = true;
        txtbaseAcc_Code.Enabled = false;
        txtTrancstion_type.Enabled = false;
        ddlAcc_sub_type.Enabled = true;
        ddlGOC.Enabled = false;
        txtUserName.Enabled = false;
        txtUserName.Text = show_username();
        chkActive.Enabled = true;


        masktemplate();


    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
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

            userid = Request.QueryString["UserID"];


            DataSet ds_up = dml.Find("select  * from SET_COA_detail WHERE ([COA_D_ID]='"+lblserialNo.Text+"') AND ([Acct_Code]='"+txtAcoount_Code.Text+"') AND ([Acct_Description]='"+txtAccountDescrip.Text+"') AND ([Head_detail_ID]='"+txtHead_Detail.Text+"') AND ([Acct_Type_ID]='"+ddlAcc_type.SelectedItem.Value+"') AND ([Acct_Sub_Type_Id]='"+ddlAcc_sub_type.SelectedItem.Value+"') AND ([Head_Acct_Code]='"+txtbaseAcc_Code.Text+"') AND ([GocId]='"+ddlGOC.SelectedItem.Value+"') AND ([IsActive]='"+chk+"') AND ([Record_Deleted]='0')");

            if (ds_up.Tables[0].Rows.Count > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", " noupdate()", true);

                textclear();
                btnInsert.Visible = true;
                btnDelete.Visible = true;
                btnUpdate.Visible = false;
                btnEdit.Visible = true;
                btnFind.Visible = true;
                btnSave.Visible = false;
                btnDelete_after.Visible = false;
            }
            else {

                dml.Update("UPDATE SET_COA_detail SET  [Acct_Description]='" + txtAccountDescrip.Text.ToUpper() + "', [Head_detail_ID]='" + txtHead_Detail.Text + "', [Acct_Type_ID]='" + ddlAcc_type.SelectedItem.Value + "', [Acct_Sub_Type_Id]='" + ddlAcc_sub_type.SelectedItem.Value + "', [Acct_Sub_Type]=NULL, [SysDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', [User]='" + show_username() + "', [GocId]='1', [IsActive]='" + chk + "', [Bar_Code]='0', [Record_Deleted]='0' WHERE ([COA_D_ID]='" + lblserialNo.Text + "')", "");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", " Editalert()", true);

                textclear();
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
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            dml.Delete("update SET_COA_detail set Record_Deleted = 1 where COA_D_ID =  " + lblserialNo.Text + "", "");
            textclear();
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        textclear();
        btnInsert.Visible = true;
        btnDelete.Visible = true;
        btnUpdate.Visible = false;
        btnEdit.Visible = true;
        btnFind.Visible = true;
        btnSave.Visible = false;
        rowid.Visible = false;
        btnDelete_after.Visible = false;
        Label1.Text = "";
        Deletebox.Visible = false;
        Editbox.Visible = false;
        Findbox.Visible = false;
        fieldbox.Visible = true;
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
            Label1.Text = "";
            string squer = "select * from SET_COA_detail ";
            string swhere;

            if (txtEdit_AccountCode.Text != "")
            {
                swhere = "Acct_Code = '" + txtEdit_AccountCode.Text + "'";
            }
            else
            {
                swhere = "Acct_Code is not null";
            }
            if (txtEdit_AccountDes.SelectedIndex != 0)
            {
                swhere = swhere + " and Acct_Description = '" + txtEdit_AccountDes.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and Acct_Description is not null";
            }
            if (txtEdit_AccountType.SelectedIndex != 0)
            {
                swhere = swhere + " and Acct_Type_ID = '" + txtEdit_AccountType.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and Acct_Type_ID is not null";
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
            string str = combination();




            if (FindDate() > 0)
            {
                if (ViewState["comp_Goc"].ToString() == "False")
                {
                    squer = squer + " where " + swhere + " and Record_Deleted = 0 and gocid = " + gocid() + " and Compid = 0 order by Acct_Code";
                }
                else {
                    squer = squer + " where " + swhere + " and Record_Deleted = 0 and gocid = " + gocid() + " and Compid = " + compid() + " order by Acct_Code";

                }
            }
            else
            {
               
                squer = squer + " where " + swhere + " and Record_Deleted = 0 and gocid = " + gocid() + " and Compid = " + compid() + "  order by Acct_Code";
            }


         
            Findbox.Visible = false;
            Deletebox.Visible = false;
            Editbox.Visible = true;
            fieldbox.Visible = false;

            btnInsert.Visible = false;
            btnEdit.Visible = true;
            btnDelete.Visible = false;
            btnDelete_after.Visible = false;
            btnFind.Visible = false;
            btnCancel.Visible = true;

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
        rowid.Visible = true;
        Label1.Text = "";
        try
        {
            string squer = "select * from SET_COA_detail ";
            string swhere;

            if (txtFind_AccountCode.Text != "")
            {
                swhere = "Acct_Code = '" + txtFind_AccountCode.Text + "'";
            }
            else
            {
                swhere = "Acct_Code is not null";
            }
            if (txtFind_AccountDesc.SelectedIndex != 0)
            {
                swhere = swhere + " and Acct_Description = '" + txtFind_AccountDesc.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and Acct_Description is not null";
            }
            if (txtFind_AccountType.SelectedIndex != 0)
            {
                swhere = swhere + " and Acct_Type_ID = '" + txtFind_AccountType.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and Acct_Type_ID is not null";
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
            string str = combination();

            


            if (FindDate() > 0)
            {
                if (ViewState["comp_Goc"].ToString() == "False")
                {
                    squer = squer + " where " + swhere + " and Record_Deleted = 0 and gocid = " + gocid() + " and Compid = 0 order by Acct_Code";
                }
                else {
                    squer = squer + " where " + swhere + " and Record_Deleted = 0 and gocid = " + gocid() + " and Compid = " + compid() + " order by Acct_Code";
                    
                }
            }
            else
            {
                squer = squer + " where " + swhere + " and Record_Deleted = 0 and gocid = " + gocid() + " and Compid = " + compid() + "  order by Acct_Code";
            }

            //if (str == "False")
            //{
            //    squer = squer + " where " + swhere + " and Record_Deleted = 0 and gocid = " + gocid() + " and Compid = 0 order by Acct_Code";
            //}
            //else {
                
            //}
            Findbox.Visible = true;
            Deletebox.Visible = false;
            Editbox.Visible = false;
            fieldbox.Visible = false;

            btnInsert.Visible = false;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            btnFind.Visible = true;
            btnCancel.Visible = true;



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
            Label1.Text = "";
            string squer = "select * from SET_COA_detail ";
            string swhere;

            if (txtDelete_AccountCode.Text != "")
            {
                swhere = "Acct_Code = '" + txtDelete_AccountCode.Text + "'";
            }
            else
            {
                swhere = "Acct_Code is not null";
            }
            if (txtDelete_AccountDesc.SelectedIndex != 0)
            {
                swhere = swhere + " and Acct_Description = '" + txtDelete_AccountDesc.SelectedItem.Text + "'";
            }
            else
            {
                swhere = swhere + " and Acct_Description is not null";
            }
            if (txtDelete_AccountType.SelectedIndex != 0)
            {
                swhere = swhere + " and Acct_Type_ID = '" + txtDelete_AccountType.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and Acct_Type_ID is not null";
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
            string str = combination();




            if (FindDate() > 0)
            {
                if (ViewState["comp_Goc"].ToString() == "False")
                {
                    squer = squer + " where " + swhere + " and Record_Deleted = 0 and gocid = " + gocid() + " and Compid = 0 order by Acct_Code";
                }
                else {
                    squer = squer + " where " + swhere + " and Record_Deleted = 0 and gocid = " + gocid() + " and Compid = " + compid() + " order by Acct_Code";

                }
            }
            else
            {

                squer = squer + " where " + swhere + " and Record_Deleted = 0 and gocid = " + gocid() + " and Compid = " + compid() + "  order by Acct_Code";
            }

            //string str = combination();
            //if (str == "False")
            //{
            //    squer = squer + " where " + swhere + " and Record_Deleted = 0 and gocid = " + gocid() + " and Compid = 0 order by Acct_Code";
            //}
            //else
            //{
            //    squer = squer + " where " + swhere + " and Record_Deleted = 0 and gocid = " + gocid() + " and Compid = " + compid() + " order by Acct_Code";
            //}

            Findbox.Visible = false;
            fieldbox.Visible = false;
            Editbox.Visible = false;
            Deletebox.Visible = true;

            btnInsert.Visible = false;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            btnDelete_after.Visible = true;
            btnFind.Visible = false;
            btnCancel.Visible = true;


            DataSet dgrid = dml.grid(squer);
            GridView2.DataSource = dgrid;
            GridView2.DataBind();
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

    public void barcodegenrate()
    {
        string barCode = trimcode(txtAcoount_Code.Text);
        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        using (Bitmap bitMap = new Bitmap(barCode.Length * 26, 80))
        {
            using (Graphics graphics = Graphics.FromImage(bitMap))
            {
                Font oFont = new Font("IDAutomationSYHC39XL Demo Sym", 16);
                PointF point = new PointF(2f, 2f);
                SolidBrush blackBrush = new SolidBrush(Color.Black);
                SolidBrush whiteBrush = new SolidBrush(Color.White);
                graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
            }
            using (MemoryStream ms = new MemoryStream())
            {
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();

                Convert.ToBase64String(byteImage);
                imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            }
            plBarCode.Controls.Add(imgBarCode);
        }
    }

    protected void txtAcoount_Code_TextChanged(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
           
            if (txtAcoount_Code.Text.Substring(0, 1) != "_")
            {
                accountCode_increment();
                //  plBarCode.Visible = true;

                txtbaseAcc_Code.Text = Head_Acct_Code();
                txtHead_Detail.Text = headdetail();
              //  barcodegenrate();
            }

        }
        txtAcoount_Code.Focus();
        
    }

    public void ddlACCSUBFun()
    {
        dml.dropdownsql(ddlAcc_sub_type, "SET_Acct_Sub_Type", "Acct_Sub_Type_Name", "Acct_Sub_Type_Id", "Acct_Type_Id", ddlAcc_type.SelectedItem.Value);

    }
    public void textclear()
    {

        txtAcoount_Code.Text = "";
        txtAccountDescrip.Text = "";
        txtHead_Detail.Text = "";
        ddlAcc_type.SelectedIndex = 0;
        txtbaseAcc_Code.Text = "";
        txtTrancstion_type.Text = "";
        txtSystemDATE.Text = "";
        ddlGOC.SelectedIndex = 0;
        txtUserName.Text = "";
        chkActive.Checked = false;
        plBarCode.Visible = false;
        lbl_head1.Text = "";
        lbl_head2.Text = "";
        lbl_head3.Text = "";
        lblserialNo.Text = "";
        lblMasterID.Text = "";
        Label1.Text = "";
        lbl_error.Text = "";
        txtAcoount_Code.Enabled = false;
        txtAccountDescrip.Enabled = false;
        txtHead_Detail.Enabled = false;
        ddlAcc_type.Enabled = false;
        txtbaseAcc_Code.Enabled = false;
        txtTrancstion_type.Enabled = false;
        txtSystemDATE.Enabled = false;
        ddlGOC.Enabled = false;
        ddlAcc_sub_type.Enabled = false;
        chkActive.Enabled = false;
        txtUserName.Enabled = false;
        plBarCode.Visible = false;
        LinklblGenerarte.Visible = true;

    }
    protected void ddlAcc_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTrancstion_type.Text = ddlTran_Type();
    }
    public void masktemplate()
    {
        DataSet ds = dml.Find("select Template from SET_COA_Master WHERE COA_M_ID = "+COAMasterID()+" and Record_Deleted = 0");

        string mask = ds.Tables[0].Rows[0]["Template"].ToString();
        MaskedEditExtender1.Mask = mask;
        MaskedEditExtender1.ClearMaskOnLostFocus = false;

    }

    public int noseg()
    {
        DataSet ds = dml.Find("select NoofSegments from SET_COA_Master where Goc_Comp_Coa = 0");
        string seg = ds.Tables[0].Rows[0]["NoofSegments"].ToString();

        return int.Parse(seg);
    }
    public void accountCode_increment()
    {
        try
        {
            if (noseg() == 3)
            {
                lbl_head1.Text = "";
                lbl_head2.Text = "";
                lbl_head3.Text = "";
                string a1 = "", a2 = "", a3 = "";
                string data = txtAcoount_Code.Text;
                lblaccout_code.Text = data;
                string value1 = "", value2 = "", value3 = "";
                string[] words = data.Split('-');
                string s1 = words[0].ToString();
                string s2 = words[1].ToString();
                string s3 = words[2].ToString();


                a1 = trimcode(s1);
                a2 = trimcode(s2);
                a3 = trimcode(s3);


                if (a1 == "")
                {
                    DataSet ds1 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H1' and Record_Deleted = 0");
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        value1 = ds1.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                        if (value1 == "")
                        {

                        }
                        else {
                            string code1 = value1;
                            string[] increment1 = code1.Split('-');
                            string newa = increment1[0].ToString();
                            float aa1 = float.Parse(newa);
                            lbl_head1.Text = "H1 : " + aa1.ToString("0");
                            string code_genarate1 = aa1.ToString();
                            txtAcoount_Code.Text = code_genarate1;
                            lbl_error.Text = "";
                            lbl_head2.Text = "";
                            lbl_head3.Text = "";
                        }
                    }

                }
                else
                {
                    DataSet ds1 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H1' and Record_Deleted = 0");
                    value1 = ds1.Tables[0].Rows[0]["ACOUNTCODE"].ToString();


                    string code1 = value1;
                    string[] increment1 = code1.Split('-');
                    string newa = increment1[0].ToString();


                    if (float.Parse(newa) < float.Parse(a1))
                    {
                        float aa1 = float.Parse(newa) + 01;
                        lbl_head1.Text = aa1.ToString("0");
                        string code_genarate1 = aa1.ToString();
                        txtAcoount_Code.Text = code_genarate1;
                    }
                    lbl_error.Text = "";
                    lbl_head2.Text = "";
                    lbl_head3.Text = "";


                    value1 = a1;
                }


                if (a2 == "" && a1.Length <= 1)
                {
                    if (a1 != "" && a2.Length <= 2)
                    {
                        DataSet ds2 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H2' AND Head_Acct_Code = '" + a1 + "' and Record_Deleted = 0");

                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            value2 = ds2.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value2 == "")
                            {
                                //  lbl_error.Text = " H2 not found";
                            }
                            else {
                                string code2 = value2;
                                string[] increment2 = code2.Split('-');
                                string newa = increment2[1].ToString();
                                float aa2 = float.Parse(newa);
                                lbl_head1.Text = a1;
                                lbl_error.Text = "";
                                lbl_head2.Text = "-" + aa2.ToString("00");

                                //string code_genarate1 = txtAcoount_Code.Text + aa2.ToString("00");
                                //code_genarate1 = trimcode(code_genarate1);
                                //txtAcoount_Code.Text = code_genarate1;
                                //value2 = a1 + "-" + aa2.ToString("00");
                                lbl_head3.Text = "";
                            }
                        }
                    }

                }
                else
                {
                    DataSet ds2 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H2' AND Head_Acct_Code = '" + a1 + "' and Record_Deleted = 0");

                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        value2 = ds2.Tables[0].Rows[0]["ACOUNTCODE"].ToString();

                        if (value2 == "")
                        {
                            lbl_head1.Text = "";
                            lbl_head2.Text = "";
                            lbl_head3.Text = "";
                            lbl_error.Text = " H1 not found.. Please input H1 code";
                        }
                        else {
                            string code1 = value2;
                            string[] increment1 = code1.Split('-');
                            string newa = increment1[1].ToString();


                            if (float.Parse(newa) < float.Parse(a2))
                            {
                                float aa2 = float.Parse(newa) + 01;
                                lbl_head2.Text = a1 + "-" + aa2.ToString("00");
                                string code_genarate1 = aa2.ToString("00");
                                txtAcoount_Code.Text = a1 + "-" + code_genarate1;

                            }

                            lbl_head3.Text = "";


                            value2 = a1 + "-" + a2;
                        }
                    }
                    else
                    {
                        txtAcoount_Code.Text = a1;
                        lbl_error.Text = "H1 not found first insert H1 ";
                        
                    }
                }

                if (a3 == "" && a2.Length <= 2)
                {
                    if (a2 != "" && a3.Length <= 2)
                    {
                        DataSet ds3 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'D1' AND Head_Acct_Code = '" + value2 + "' and Record_Deleted = 0");
                        if (ds3.Tables[0].Rows.Count > 0)
                        {
                            value3 = ds3.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value3 == "")
                            {


                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                                lbl_error.Text = " H2 not found.. Please input H2 code";
                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();


                            }
                            else
                            {

                                string codes = value3;
                                string[] increment = codes.Split('-');
                                string newa = increment[2].ToString();
                                float aa3 = float.Parse(newa) + 1;
                                string code_genarate = txtAcoount_Code.Text + aa3.ToString("0000");
                                // lblaccout_code.Text = codes;
                                txtAcoount_Code.Text = code_genarate;

                            }
                        }
                    }

                }
                else
                {
                    value3 = a1 + "-" + a2 + "-" + a3;
                }

            }

            //seg 4
            else if (noseg() == 4)

            {
                lbl_head1.Text = "";
                lbl_head2.Text = "";
                lbl_head3.Text = "";
                string a1 = "", a2 = "", a3 = "", a4 = "";
                string s1 = "_", s2 = "_", s3 = "_", s4 = "_";
                string data = txtAcoount_Code.Text;
                lblaccout_code.Text = data;
                string value1 = "", value2 = "", value3 = "", value4 = "";
                string[] words = data.Split('-');
                string query = "";

                if (words[0] != "")
                {
                     s1 = words[0].ToString();
                }
                if (words[1] != "")
                {
                     s2 = words[1].ToString();
                }
                if (words[2] != "")
                {
                     s3 = words[2].ToString();
                }
                if (words[3] != "")
                {
                     s4 = words[3].ToString();
                }
              

                a1 = trimcode(s1);
                a2 = trimcode(s2);
                a3 = trimcode(s3);
                a4 = trimcode(s4);

                if (a1 == "")
                {
                   // string str = combination();

                    if (FindDate() > 0)
                    {
                        if (ViewState["comp_Goc"].ToString() == "False")
                        {
                            query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H1' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = " + compid() + "";
                        }
                        else {
                            query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H1' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = 0 ";
                        }
                    }
                    else
                    {
                        query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H1' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = 0 ";
                    }

                    //if (str == "False")
                    //{
                    //    query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H1' and Record_Deleted = 0 and GocId = "+gocid()+" and CompId = "+compid()+"";
                    //}
                    //else
                    //{
                    //    query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H1' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = 0 ";
                    //}
                    DataSet ds1 = dml.Find(query);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        value1 = ds1.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                        if (value1 == "")
                        {

                        }
                        else {
                            string code1 = value1;
                            string[] increment1 = code1.Split('-');
                            string newa = increment1[0].ToString();
                            float aa1 = float.Parse(newa);
                            lbl_head1.Text = "H1 : " + aa1.ToString("0");
                            string code_genarate1 = aa1.ToString();
                            txtAcoount_Code.Text = code_genarate1;
                            lbl_error.Text = "";
                            lbl_head2.Text = "";
                            lbl_head3.Text = "";
                        }
                    }

                }
                else
                {
                    //string str = combination();



                    if (FindDate() > 0)
                    {
                        if (ViewState["comp_Goc"].ToString() == "False")
                        {
                            query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H1' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = 0";
                        }
                        else {
                              query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H1' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = " + compid() + "";
                        }
                    }
                    else
                    {
                        query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H1' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = 0";
                    }



                    //if (str == "False")
                    //{
                    //    query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H1' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = 0";
                    //}
                    //else
                    //{
                    //    query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H1' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = " + compid() + "";
                    //}
                    DataSet ds1 = dml.Find(query);
                    value1 = ds1.Tables[0].Rows[0]["ACOUNTCODE"].ToString();


                    string code1 = value1;
                    string[] increment1 = code1.Split('-');
                    string newa = increment1[0].ToString();


                    if (float.Parse(newa) < float.Parse(a1))
                    {
                        float aa1 = float.Parse(newa) + 01;
                        lbl_head1.Text = aa1.ToString("0");
                        string code_genarate1 = aa1.ToString();
                        txtAcoount_Code.Text = code_genarate1;

                    }
                    lbl_error.Text = "";
                    lbl_head2.Text = "";
                    lbl_head3.Text = "";


                    value1 = a1;
                }

//a2
                if (a2 == "" && a1.Length <= 1)
                {
                    if (a1 != "" && a2.Length <= 2)
                    {
                        //string str = combination();


                        if (FindDate() > 0)
                        {
                            if (ViewState["comp_Goc"].ToString() == "False")
                            {
                                query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H2' AND Head_Acct_Code = '" + a1 + "' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = 0";
                            }
                            else {
                                query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H2' AND Head_Acct_Code = '" + a1 + "' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = " + compid() + "";
                            }
                        }
                        else
                        {
                            query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H2' AND Head_Acct_Code = '" + a1 + "' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = 0";
                        }



                        //if (str == "False")
                        //{
                        //    query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H2' AND Head_Acct_Code = '" + a1 + "' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = 0";
                        //}
                        //else
                        //{
                        //    query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H2' AND Head_Acct_Code = '" + a1 + "' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = " + compid() + "";
                        //}

                        DataSet ds2 = dml.Find(query);

                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            value2 = ds2.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value2 == "")
                            {
                                //  lbl_error.Text = " H2 not found";
                            }
                            else {
                                string code2 = value2;
                                string[] increment2 = code2.Split('-');
                                string newa = increment2[1].ToString();
                                float aa2 = float.Parse(newa);
                                lbl_head1.Text = a1;
                                lbl_error.Text = "";
                                lbl_head2.Text = "-" + aa2.ToString("00");
                                
                                //string code_genarate1 = txtAcoount_Code.Text + aa2.ToString("00");
                                //code_genarate1 = trimcode(code_genarate1);
                                //txtAcoount_Code.Text = code_genarate1;
                                //value2 = a1 + "-" + aa2.ToString("00");
                                lbl_head3.Text = "";
                            }
                        }
                    }

                }
                else
                {
                    string str = combination();

                    if (FindDate() > 0)
                    {
                        if (ViewState["comp_Goc"].ToString() == "False")
                        {
                            query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H2' AND Head_Acct_Code = '" + a1 + "' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = 0";
                        }
                        else {
                            query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H2' AND Head_Acct_Code = '" + a1 + "' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = " + compid() + "";
                        }
                    }
                    else
                    {
                        query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H2' AND Head_Acct_Code = '" + a1 + "' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = 0";
                    }

                    DataSet ds2 = dml.Find(query);

                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        value2 = ds2.Tables[0].Rows[0]["ACOUNTCODE"].ToString();

                        if (value2 == "")
                        {
                            lbl_head1.Text = "";
                            lbl_head2.Text = "";
                            lbl_head3.Text = "";
                            lbl_error.Text = " H2 not found.. Please input H2 code";
                           
                        }
                        else {
                            string code1 = value2;
                            string[] increment1 = code1.Split('-');
                            string newa = increment1[1].ToString();


                            if (float.Parse(newa) < float.Parse(a2))
                            {
                                float aa2 = float.Parse(newa) + 01;
                                lbl_head2.Text = a1 + "-" + aa2.ToString("00");
                                string code_genarate1 = aa2.ToString("00");

                                
                                txtAcoount_Code.Text = a1 + "-" + code_genarate1;

                            }

                            lbl_head3.Text = "";


                            value2 = a1 + "-" + a2;
                        }
                    }
                    else
                    {
                        txtAcoount_Code.Text = a1;
                        lbl_error.Text = "H2 not found first insert H2 ";
                        
                    }
                }

                if (a3 == "" && a2.Length <= 2)
                {
                    if (a2 != "" && a3.Length <= 2)
                    {

                        if (FindDate() > 0)
                        {
                            if (ViewState["comp_Goc"].ToString() == "False")
                            {
                                query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H3' AND Head_Acct_Code = '" + value2 + "' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = 0";
                            }
                            else {
                                query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H3' AND Head_Acct_Code = '" + value2 + "' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = " + compid() + "";
                            }
                        }
                        else {
                            query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H3' AND Head_Acct_Code = '" + value2 + "' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = 0";
                        }




                        //string str = combination();
                        //if (str == "False")
                        //{
                        //    query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H3' AND Head_Acct_Code = '" + value2 + "' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = 0";
                        //}
                        //else
                        //{
                        //    query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H3' AND Head_Acct_Code = '" + value2 + "' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = " + compid() + "";
                        //}

                        DataSet ds3 = dml.Find(query);
                        if (ds3.Tables[0].Rows.Count > 0)
                        {
                            value3 = ds3.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value3 == "")
                            {
                                   lbl_error.Text = " H2 not found";
                            }
                            else {

                                string code3 = value3;
                                string[] increment3 = code3.Split('-');
                                string newa = increment3[2].ToString();
                                float aa3 = float.Parse(newa);
                                lbl_head1.Text = a1;
                                lbl_head2.Text = "-" + a2;
                                lbl_head3.Text = "-" + aa3.ToString("00");
                                txtAcoount_Code.Focus();
                               

                                //string code_genarate1 = txtAcoount_Code.Text + aa3.ToString("00");
                                //txtAcoount_Code.Text = code_genarate1;
                                //value3 = value2 + "-" + aa3.ToString("00");
                            }
                        }
                    }

                }
                else
                {
                    string str = combination();

                    if (FindDate() > 0)
                    {
                        if (ViewState["comp_Goc"].ToString() == "False")
                        {
                            query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H3' AND Head_Acct_Code = '" + value2 + "' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = 0";
                        }
                        else {
                            query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H3' AND Head_Acct_Code = '" + value2 + "' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = " + compid() + "";
                        }
                    }
                    else {
                        query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H3' AND Head_Acct_Code = '" + value2 + "' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = 0";
                    }

                    DataSet ds3 = dml.Find(query);

                    if (ds3.Tables[0].Rows.Count > 0)
                    {
                        value3 = ds3.Tables[0].Rows[0]["ACOUNTCODE"].ToString();

                        if (value3 == "")
                        {
                            lbl_head1.Text = "";
                            lbl_head2.Text = "";
                            lbl_head3.Text = "";
                            lbl_error.Text = " H2 not found.. Please input H2 code";

                        }
                        else {
                            string code3 = value3;
                            string[] increment3 = code3.Split('-');
                            string newa = increment3[2].ToString();


                            if (float.Parse(newa) < float.Parse(a3))
                            {
                                float aa3 = float.Parse(newa) + 01;
                                lbl_head2.Text = a1 + "-" + aa3.ToString("00");
                                string code_genarate1 = aa3.ToString("00");


                                txtAcoount_Code.Text = a1 + "-" + a2 +"-"+ code_genarate1;

                            }
                          

                            lbl_head3.Text = "";


                            value3 = a1 + "-" + a2 + "-" + a3; 
                        }
                    }
                    else
                    {
                        txtAcoount_Code.Text = a2;
                        lbl_error.Text = "H3 not found first insert H3 ";

                    }
                }
                if (a4 == "" && a3.Length <= 2)
                {

                    if (a3 != "" && a4.Length <= 4)
                    {


                        if (FindDate() > 0)
                        {
                            if (ViewState["comp_Goc"].ToString() == "False")
                            {
                                query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'D1' AND Head_Acct_Code = '" + value3 + "' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = 0";
                            }
                            else {
                                query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'D1' AND Head_Acct_Code = '" + value3 + "' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = " + compid() + "";
                            }
                        }
                        else {
                            query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'D1' AND Head_Acct_Code = '" + value3 + "' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = 0";
                        }


                        //string str = combination();
                        //if (str == "False")
                        //{
                        //    query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'D1' AND Head_Acct_Code = '" + value3 + "' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = 0";
                        //}
                        //else
                        //{
                        //    query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'D1' AND Head_Acct_Code = '" + value3 + "' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = " + compid() + "";
                        //}


                        DataSet ds4 = dml.Find(query);
                        if (ds4.Tables[0].Rows.Count > 0)
                        {
                            value4 = ds4.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value4 == "")
                            {
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                              //  lbl_error.Text = " H3 not found.. Please input H3 code";
                              
                                string codes = value3;
                                string[] increment = codes.Split('-');
                                string newa = increment[3].ToString();
                                float aa4 = float.Parse(newa) + 1;
                                string code_genarate = txtAcoount_Code.Text + aa4.ToString("0000");
                                lblaccout_code.Text = codes;

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();
                            }
                            else
                            {
                                string codes = value4;
                                string[] increment = codes.Split('-');
                                string newa = increment[3].ToString();

                                
                                    float aa4 = float.Parse(newa) + 1;
                                    string code_genarate = txtAcoount_Code.Text + aa4.ToString("0000");
                                    lblaccout_code.Text = codes;
                                    txtAcoount_Code.Text = code_genarate;
                                
                                   
                                

                            }
                        }
                    }
                else
                    {

                    }
                }
                else if(a4 != "")
                {
                    string str = combination();

                    if (FindDate() > 0)
                    {
                        if (ViewState["comp_Goc"].ToString() == "False")
                        {
                            query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'D1' AND Head_Acct_Code = '" + value3 + "' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = 0";
                        }
                        else {
                            query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'D1' AND Head_Acct_Code = '" + value3 + "' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = " + compid() + "";
                        }
                    }
                    else {
                        query = "SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'D1' AND Head_Acct_Code = '" + value3 + "' and Record_Deleted = 0 and GocId = " + gocid() + " and CompId = 0";
                    }

                    DataSet ds4 = dml.Find(query);

                    if (ds4.Tables[0].Rows.Count > 0)
                    {
                        value4 = ds4.Tables[0].Rows[0]["ACOUNTCODE"].ToString();

                        if (value4 == "")
                        {
                            lbl_head1.Text = "";
                            lbl_head2.Text = "";
                            lbl_head3.Text = "";
                            lbl_error.Text = " D1 not found.. Please input D1 code";

                        }
                        else {
                            string code4 = value4;
                            string[] increment4 = code4.Split('-');
                            string newa = increment4[3].ToString();


                            if (float.Parse(newa) <= float.Parse(a4))
                            {
                                float aa4 = float.Parse(newa) + 01;
                               // lbl_head2.Text = a1 + "-" + a2 + "-" + a3 + "-" + aa4.ToString("0000");
                                string code_genarate1 = aa4.ToString("0000");


                                txtAcoount_Code.Text = a1 + "-" + a2 + "-" + a3 + "-" + code_genarate1;

                            }
                            if (float.Parse(newa) >= float.Parse(a4))
                            {
                                float aa4 = float.Parse(newa) + 01;
                                // lbl_head2.Text = a1 + "-" + a2 + "-" + a3 + "-" + aa4.ToString("0000");
                                string code_genarate1 = aa4.ToString("0000");


                                txtAcoount_Code.Text = a1 + "-" + a2 + "-" + a3 + "-" + code_genarate1;

                            }

                            lbl_head3.Text = "";


                            value2 = a1 + "-" + a2 + "-" + a3 + "-" + a4;
                        }
                    }
                    else
                    {
                        txtAcoount_Code.Text = a3;
                        lbl_error.Text = "H2 not found first insert H2 ";

                    }
                }
            }

            else if (noseg() == 5)
            {
                lbl_head1.Text = "";
                lbl_head2.Text = "";
                lbl_head3.Text = "";
                string a1 = "", a2 = "", a3 = "", a4 = "", a5 = "";
                string data = txtAcoount_Code.Text;
                lblaccout_code.Text = data;
                string value1 = "", value2 = "", value3 = "", value4 = "", value5 = "";
                string[] words = data.Split('-');
                string s1 = words[0].ToString();
                string s2 = words[1].ToString();
                string s3 = words[2].ToString();
                string s4 = words[3].ToString();
                string s5 = words[4].ToString();

                a1 = trimcode(s1);
                a2 = trimcode(s2);
                a3 = trimcode(s3);
                a4 = trimcode(s4);
                a5 = trimcode(s5);

                if (a1 == "")
                {
                    DataSet ds1 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H1' and Record_Deleted = 0");
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        value1 = ds1.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                        if (value1 == "")
                        {

                        }
                        else {
                            string code1 = value1;
                            string[] increment1 = code1.Split('-');
                            string newa = increment1[0].ToString();
                            float aa1 = float.Parse(newa);
                            lbl_head1.Text = "H1 : " + aa1.ToString("0");
                            string code_genarate1 = aa1.ToString();
                            txtAcoount_Code.Text = code_genarate1;
                            lbl_error.Text = "";
                            lbl_head2.Text = "";
                            lbl_head3.Text = "";
                        }
                    }

                }
                else
                {
                    DataSet ds1 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H1' and Record_Deleted = 0");
                    value1 = ds1.Tables[0].Rows[0]["ACOUNTCODE"].ToString();


                    string code1 = value1;
                    string[] increment1 = code1.Split('-');
                    string newa = increment1[0].ToString();


                    if (float.Parse(newa) < float.Parse(a1))
                    {
                        float aa1 = float.Parse(newa) + 01;
                        lbl_head1.Text = aa1.ToString("0");
                        string code_genarate1 = aa1.ToString();
                        txtAcoount_Code.Text = code_genarate1;
                    }
                    lbl_error.Text = "";
                    lbl_head2.Text = "";
                    lbl_head3.Text = "";


                    value1 = a1;
                }


                if (a2 == "" && a1.Length <= 1)
                {
                    if (a1 != "" && a2.Length <= 2)
                    {
                        DataSet ds2 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H2' AND Head_Acct_Code = '" + a1 + "' and Record_Deleted = 0");

                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            value2 = ds2.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value2 == "")
                            {
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                                lbl_error.Text = " H1 not found.. Please input H1 code";
                            }
                            else {
                                string code2 = value2;
                                string[] increment2 = code2.Split('-');
                                string newa = increment2[1].ToString();
                                float aa2 = float.Parse(newa);
                                lbl_head1.Text = a1;
                                lbl_error.Text = "";
                                lbl_head2.Text = "-" + aa2.ToString("00");

                                //string code_genarate1 = txtAcoount_Code.Text + aa2.ToString("00");
                                //code_genarate1 = trimcode(code_genarate1);
                                //txtAcoount_Code.Text = code_genarate1;
                                //value2 = a1 + "-" + aa2.ToString("00");
                                lbl_head3.Text = "";
                            }
                        }
                    }

                }
                else
                {
                    DataSet ds2 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H2' AND Head_Acct_Code = '" + a1 + "' and Record_Deleted = 0");

                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        value2 = ds2.Tables[0].Rows[0]["ACOUNTCODE"].ToString();

                        if (value2 == "")
                        {
                            lbl_head1.Text = "";
                            lbl_head2.Text = "";
                            lbl_head3.Text = "";
                            lbl_error.Text = " H1 not found.. Please input H1 code";
                        }
                        else {
                            string code1 = value2;
                            string[] increment1 = code1.Split('-');
                            string newa = increment1[1].ToString();


                            if (float.Parse(newa) < float.Parse(a2))
                            {
                                float aa2 = float.Parse(newa) + 01;
                                lbl_head2.Text = a1 + "-" + aa2.ToString("00");
                                string code_genarate1 = aa2.ToString("00");
                                txtAcoount_Code.Text = a1 + "-" + code_genarate1;

                            }

                            lbl_head3.Text = "";


                            value2 = a1 + "-" + a2;
                        }
                    }
                    else
                    {
                        txtAcoount_Code.Text = a1;
                        lbl_error.Text = "H1 not found first insert H1 ";
                    }
                }

                if (a3 == "" && a2.Length <= 2)
                {
                    if (a2 != "" && a3.Length <= 2)
                    {
                        DataSet ds3 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H3' AND Head_Acct_Code = '" + value2 + "' and Record_Deleted = 0");
                        if (ds3.Tables[0].Rows.Count > 0)
                        {
                            value3 = ds3.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value3 == "")
                            {
                                //   lbl_error.Text = " H3 not found";
                            }
                            else {

                                string code3 = value3;
                                string[] increment3 = code3.Split('-');
                                string newa = increment3[2].ToString();
                                float aa3 = float.Parse(newa);
                                lbl_head1.Text = a1;
                                lbl_head2.Text = "-" + a2;
                                lbl_head3.Text = "-" + aa3.ToString("00");

                                //string code_genarate1 = txtAcoount_Code.Text + aa3.ToString("00");
                                //txtAcoount_Code.Text = code_genarate1;
                                //value3 = value2 + "-" + aa3.ToString("00");
                            }
                        }
                    }

                }
                else
                {
                    value3 = a1 + "-" + a2 + "-" + a3;
                }

                if (a4 == "" && a3.Length <= 2)
                {
                    if (a3 != "" && a4.Length <= 2)
                    {
                        DataSet ds3 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H4' AND Head_Acct_Code = '" + value3 + "' and Record_Deleted = 0");
                        if (ds3.Tables[0].Rows.Count > 0)
                        {
                            value4 = ds3.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value4 == "")
                            {
                                //   lbl_error.Text = " H3 not found";
                            }
                            else {

                                string code3 = value4;
                                string[] increment3 = code3.Split('-');
                                string newa = increment3[2].ToString();
                                float aa3 = float.Parse(newa);
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = value4;

                                //string code_genarate1 = txtAcoount_Code.Text + aa3.ToString("00");
                                //txtAcoount_Code.Text = code_genarate1;
                                //value3 = value2 + "-" + aa3.ToString("00");
                            }
                        }
                    }

                }
                else
                {
                    value4 = a1 + "-" + a2 + "-" + a3 + "-" + a4;
                }

                if (a5 == "" && a4.Length <= 2)
                {

                    if (a4 != "" && a5.Length <= 4)
                    {
                        DataSet ds4 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'D1' AND Head_Acct_Code = '" + value4 + "' and Record_Deleted = 0");
                        if (ds4.Tables[0].Rows.Count > 0)
                        {
                            value5 = ds4.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value5 == "")
                            {
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                                lbl_error.Text = " D1 not found.. Please input 0001";

                                // string codes = value4;
                                // string[] increment = codes.Split('-');
                                // string newa = increment[3].ToString();
                                // float aa4 = float.Parse(newa) + 1;
                                // string code_genarate = txtAcoount_Code.Text + aa4.ToString("0000");
                                //// lblaccout_code.Text = codes;
                                // txtAcoount_Code.Text = code_genarate;

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();
                            }
                            else
                            {

                                string codes = value5;
                                string[] increment = codes.Split('-');
                                string newa = increment[3].ToString();
                                float aa4 = float.Parse(newa) + 1;
                                string code_genarate = txtAcoount_Code.Text + aa4.ToString("0000");
                                //  lblaccout_code.Text = codes;
                                txtAcoount_Code.Text = code_genarate;

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();


                            }
                            lblaccout_code.Text = txtAcoount_Code.Text;
                            txtHead_Detail.Text = headdetail();
                            txtbaseAcc_Code.Text = Head_Acct_Code();
                        }

                    }
                    else
                    {

                    }
                }
            }


            //seg 6
            else if (noseg() == 6)
            {
                lbl_head1.Text = "";
                lbl_head2.Text = "";
                lbl_head3.Text = "";
                string a1 = "", a2 = "", a3 = "", a4 = "", a5 = "", a6 = "";
                string data = txtAcoount_Code.Text;
                lblaccout_code.Text = data;
                string value1 = "", value2 = "", value3 = "", value4 = "", value5 = "", value6 = "";
                string[] words = data.Split('-');
                string s1 = words[0].ToString();
                string s2 = words[1].ToString();
                string s3 = words[2].ToString();
                string s4 = words[3].ToString();
                string s5 = words[4].ToString();
                string s6 = words[5].ToString();

                a1 = trimcode(s1);
                a2 = trimcode(s2);
                a3 = trimcode(s3);
                a4 = trimcode(s4);
                a5 = trimcode(s5);
                a6 = trimcode(s6);

                if (a1 == "")
                {
                    DataSet ds1 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H1' and Record_Deleted = 0");
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        value1 = ds1.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                        if (value1 == "")
                        {

                        }
                        else {
                            string code1 = value1;
                            string[] increment1 = code1.Split('-');
                            string newa = increment1[0].ToString();
                            float aa1 = float.Parse(newa);
                            lbl_head1.Text = "H1 : " + aa1.ToString("0");
                            string code_genarate1 = aa1.ToString();
                            txtAcoount_Code.Text = code_genarate1;
                            lbl_error.Text = "";
                            lbl_head2.Text = "";
                            lbl_head3.Text = "";
                        }
                    }

                }
                else
                {
                    DataSet ds1 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H1' and Record_Deleted = 0");
                    value1 = ds1.Tables[0].Rows[0]["ACOUNTCODE"].ToString();


                    string code1 = value1;
                    string[] increment1 = code1.Split('-');
                    string newa = increment1[0].ToString();


                    if (float.Parse(newa) < float.Parse(a1))
                    {
                        float aa1 = float.Parse(newa) + 01;
                        lbl_head1.Text = aa1.ToString("0");
                        string code_genarate1 = aa1.ToString();
                        txtAcoount_Code.Text = code_genarate1;
                    }
                    lbl_error.Text = "";
                    lbl_head2.Text = "";
                    lbl_head3.Text = "";


                    value1 = a1;
                }


                if (a2 == "" && a1.Length <= 1)
                {
                    if (a1 != "" && a2.Length <= 2)
                    {
                        DataSet ds2 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H2' AND Head_Acct_Code = '" + a1 + "' and Record_Deleted = 0");

                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            value2 = ds2.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value2 == "")
                            {
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                                lbl_error.Text = " H1 not found.. Please input H1 code";
                            }
                            else {
                                string code2 = value2;
                                string[] increment2 = code2.Split('-');
                                string newa = increment2[1].ToString();
                                float aa2 = float.Parse(newa);
                                lbl_head1.Text = a1;
                                lbl_error.Text = "";
                                lbl_head2.Text = "-" + aa2.ToString("00");

                                //string code_genarate1 = txtAcoount_Code.Text + aa2.ToString("00");
                                //code_genarate1 = trimcode(code_genarate1);
                                //txtAcoount_Code.Text = code_genarate1;
                                //value2 = a1 + "-" + aa2.ToString("00");
                                lbl_head3.Text = "";
                            }
                        }
                    }

                }
                else
                {
                    DataSet ds2 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H2' AND Head_Acct_Code = '" + a1 + "' and Record_Deleted = 0");

                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        value2 = ds2.Tables[0].Rows[0]["ACOUNTCODE"].ToString();

                        if (value2 == "")
                        {
                            lbl_head1.Text = "";
                            lbl_head2.Text = "";
                            lbl_head3.Text = "";
                            lbl_error.Text = " H1 not found.. Please input H1 code";
                        }
                        else {
                            string code1 = value2;
                            string[] increment1 = code1.Split('-');
                            string newa = increment1[1].ToString();


                            if (float.Parse(newa) < float.Parse(a2))
                            {
                                float aa2 = float.Parse(newa) + 01;
                                lbl_head2.Text = a1 + "-" + aa2.ToString("00");
                                string code_genarate1 = aa2.ToString("00");
                                txtAcoount_Code.Text = a1 + "-" + code_genarate1;

                            }

                            lbl_head3.Text = "";


                            value2 = a1 + "-" + a2;
                        }
                    }
                    else
                    {
                        txtAcoount_Code.Text = a1;
                        lbl_error.Text = "H1 not found first insert H1 ";
                    }
                }

                if (a3 == "" && a2.Length <= 2)
                {
                    if (a2 != "" && a3.Length <= 2)
                    {
                        DataSet ds3 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H3' AND Head_Acct_Code = '" + value2 + "' and Record_Deleted = 0");
                        if (ds3.Tables[0].Rows.Count > 0)
                        {
                            value3 = ds3.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value3 == "")
                            {
                                //   lbl_error.Text = " H3 not found";
                            }
                            else {

                                string code3 = value3;
                                string[] increment3 = code3.Split('-');
                                string newa = increment3[2].ToString();
                                float aa3 = float.Parse(newa);
                                lbl_head1.Text = a1;
                                lbl_head2.Text = "-" + a2;
                                lbl_head3.Text = "-" + aa3.ToString("00");

                                //string code_genarate1 = txtAcoount_Code.Text + aa3.ToString("00");
                                //txtAcoount_Code.Text = code_genarate1;
                                //value3 = value2 + "-" + aa3.ToString("00");
                            }
                        }
                    }

                }
                else
                {
                    value3 = a1 + "-" + a2 + "-" + a3;
                }

                if (a4 == "" && a3.Length <= 2)
                {
                    if (a3 != "" && a4.Length <= 2)
                    {
                        DataSet ds3 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H4' AND Head_Acct_Code = '" + value3 + "' and Record_Deleted = 0");
                        if (ds3.Tables[0].Rows.Count > 0)
                        {
                            value4 = ds3.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value4 == "")
                            {
                                //   lbl_error.Text = " H3 not found";
                            }
                            else {

                                string code3 = value4;
                                string[] increment3 = code3.Split('-');
                                string newa = increment3[2].ToString();
                                float aa3 = float.Parse(newa);
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = value4;

                                //string code_genarate1 = txtAcoount_Code.Text + aa3.ToString("00");
                                //txtAcoount_Code.Text = code_genarate1;
                                //value3 = value2 + "-" + aa3.ToString("00");
                            }
                        }
                    }

                }
                else
                {
                    value4 = a1 + "-" + a2 + "-" + a3 + "-" + a4;
                }

                if (a5 == "" && a4.Length <= 2)
                {

                    if (a4 != "" && a5.Length <= 4)
                    {
                        DataSet ds4 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H5' AND Head_Acct_Code = '" + value4 + "' and Record_Deleted = 0");
                        if (ds4.Tables[0].Rows.Count > 0)
                        {
                            value5 = ds4.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value5 == "")
                            {
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                                lbl_error.Text = " D1 not found.. Please input 0001";

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();
                            }
                            else
                            {

                                string codes = value5;
                                string[] increment = codes.Split('-');
                                string newa = increment[3].ToString();
                                float aa4 = float.Parse(newa) + 1;
                                string code_genarate = txtAcoount_Code.Text + aa4.ToString("0000");
                                //  lblaccout_code.Text = codes;
                                txtAcoount_Code.Text = code_genarate;

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();


                            }
                            lblaccout_code.Text = txtAcoount_Code.Text;
                            txtHead_Detail.Text = headdetail();
                            txtbaseAcc_Code.Text = Head_Acct_Code();
                        }

                    }
                    else
                    {
                        value5 = a1 + "-" + a2 + "-" + a3 + "-" + a4 + "-" + a5;
                    }
                }

                if (a6 == "" && a5.Length <= 2)
                {

                    if (a5 != "" && a6.Length <= 4)
                    {
                        DataSet ds4 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'D1' AND Head_Acct_Code = '" + value5 + "' and Record_Deleted = 0");
                        if (ds4.Tables[0].Rows.Count > 0)
                        {
                            value6 = ds4.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value6 == "")
                            {
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                                lbl_error.Text = " D1 not found.. Please input 0001";

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();
                            }
                            else
                            {

                                string codes = value6;
                                string[] increment = codes.Split('-');
                                string newa = increment[3].ToString();
                                float aa4 = float.Parse(newa) + 1;
                                string code_genarate = txtAcoount_Code.Text + aa4.ToString("0000");
                                //  lblaccout_code.Text = codes;
                                txtAcoount_Code.Text = code_genarate;

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();


                            }
                            lblaccout_code.Text = txtAcoount_Code.Text;
                            txtHead_Detail.Text = headdetail();
                            txtbaseAcc_Code.Text = Head_Acct_Code();
                        }

                    }
                    else
                    {
                        value6 = a1 + "-" + a2 + "-" + a3 + "-" + a4 + "-" + a5 + "-" + a6;
                    }
                }
            }

            //seg 7
            else if (noseg() == 7)
            {
                lbl_head1.Text = "";
                lbl_head2.Text = "";
                lbl_head3.Text = "";
                string a1 = "", a2 = "", a3 = "", a4 = "", a5 = "", a6 = "", a7 = "";
                string data = txtAcoount_Code.Text;
                lblaccout_code.Text = data;
                string value1 = "", value2 = "", value3 = "", value4 = "", value5 = "", value6 = "", value7 = "";
                string[] words = data.Split('-');
                string s1 = words[0].ToString();
                string s2 = words[1].ToString();
                string s3 = words[2].ToString();
                string s4 = words[3].ToString();
                string s5 = words[4].ToString();
                string s6 = words[5].ToString();
                string s7 = words[6].ToString();

                a1 = trimcode(s1);
                a2 = trimcode(s2);
                a3 = trimcode(s3);
                a4 = trimcode(s4);
                a5 = trimcode(s5);
                a6 = trimcode(s6);
                a7 = trimcode(s7);

                if (a1 == "")
                {
                    DataSet ds1 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H1' and Record_Deleted = 0");
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        value1 = ds1.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                        if (value1 == "")
                        {

                        }
                        else {
                            string code1 = value1;
                            string[] increment1 = code1.Split('-');
                            string newa = increment1[0].ToString();
                            float aa1 = float.Parse(newa);
                            lbl_head1.Text = "H1 : " + aa1.ToString("0");
                            string code_genarate1 = aa1.ToString();
                            txtAcoount_Code.Text = code_genarate1;
                            lbl_error.Text = "";
                            lbl_head2.Text = "";
                            lbl_head3.Text = "";
                        }
                    }

                }
                else
                {
                    DataSet ds1 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H1' and Record_Deleted = 0");
                    value1 = ds1.Tables[0].Rows[0]["ACOUNTCODE"].ToString();


                    string code1 = value1;
                    string[] increment1 = code1.Split('-');
                    string newa = increment1[0].ToString();


                    if (float.Parse(newa) < float.Parse(a1))
                    {
                        float aa1 = float.Parse(newa) + 01;
                        lbl_head1.Text = aa1.ToString("0");
                        string code_genarate1 = aa1.ToString();
                        txtAcoount_Code.Text = code_genarate1;
                    }
                    lbl_error.Text = "";
                    lbl_head2.Text = "";
                    lbl_head3.Text = "";


                    value1 = a1;
                }


                if (a2 == "" && a1.Length <= 1)
                {
                    if (a1 != "" && a2.Length <= 2)
                    {
                        DataSet ds2 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H2' AND Head_Acct_Code = '" + a1 + "' and Record_Deleted = 0");

                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            value2 = ds2.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value2 == "")
                            {
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                                lbl_error.Text = " H1 not found.. Please input H1 code";
                            }
                            else {
                                string code2 = value2;
                                string[] increment2 = code2.Split('-');
                                string newa = increment2[1].ToString();
                                float aa2 = float.Parse(newa);
                                lbl_head1.Text = a1;
                                lbl_error.Text = "";
                                lbl_head2.Text = "-" + aa2.ToString("00");

                                //string code_genarate1 = txtAcoount_Code.Text + aa2.ToString("00");
                                //code_genarate1 = trimcode(code_genarate1);
                                //txtAcoount_Code.Text = code_genarate1;
                                //value2 = a1 + "-" + aa2.ToString("00");
                                lbl_head3.Text = "";
                            }
                        }
                    }

                }
                else
                {
                    DataSet ds2 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H2' AND Head_Acct_Code = '" + a1 + "' and Record_Deleted = 0");

                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        value2 = ds2.Tables[0].Rows[0]["ACOUNTCODE"].ToString();

                        if (value2 == "")
                        {
                            lbl_head1.Text = "";
                            lbl_head2.Text = "";
                            lbl_head3.Text = "";
                            lbl_error.Text = " H1 not found.. Please input H1 code";
                        }
                        else {
                            string code1 = value2;
                            string[] increment1 = code1.Split('-');
                            string newa = increment1[1].ToString();


                            if (float.Parse(newa) < float.Parse(a2))
                            {
                                float aa2 = float.Parse(newa) + 01;
                                lbl_head2.Text = a1 + "-" + aa2.ToString("00");
                                string code_genarate1 = aa2.ToString("00");
                                txtAcoount_Code.Text = a1 + "-" + code_genarate1;

                            }

                            lbl_head3.Text = "";


                            value2 = a1 + "-" + a2;
                        }
                    }
                    else
                    {
                        txtAcoount_Code.Text = a1;
                        lbl_error.Text = "H1 not found first insert H1 ";
                    }
                }

                if (a3 == "" && a2.Length <= 2)
                {
                    if (a2 != "" && a3.Length <= 2)
                    {
                        DataSet ds3 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H3' AND Head_Acct_Code = '" + value2 + "' and Record_Deleted = 0");
                        if (ds3.Tables[0].Rows.Count > 0)
                        {
                            value3 = ds3.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value3 == "")
                            {
                                //   lbl_error.Text = " H3 not found";
                            }
                            else {

                                string code3 = value3;
                                string[] increment3 = code3.Split('-');
                                string newa = increment3[2].ToString();
                                float aa3 = float.Parse(newa);
                                lbl_head1.Text = a1;
                                lbl_head2.Text = "-" + a2;
                                lbl_head3.Text = "-" + aa3.ToString("00");

                                //string code_genarate1 = txtAcoount_Code.Text + aa3.ToString("00");
                                //txtAcoount_Code.Text = code_genarate1;
                                //value3 = value2 + "-" + aa3.ToString("00");
                            }
                        }
                    }

                }
                else
                {
                    value3 = a1 + "-" + a2 + "-" + a3;
                }

                if (a4 == "" && a3.Length <= 2)
                {
                    if (a3 != "" && a4.Length <= 2)
                    {
                        DataSet ds3 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H4' AND Head_Acct_Code = '" + value3 + "' and Record_Deleted = 0");
                        if (ds3.Tables[0].Rows.Count > 0)
                        {
                            value4 = ds3.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value4 == "")
                            {
                                //   lbl_error.Text = " H3 not found";
                            }
                            else {

                                string code3 = value4;
                                string[] increment3 = code3.Split('-');
                                string newa = increment3[2].ToString();
                                float aa3 = float.Parse(newa);
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = value4;

                                //string code_genarate1 = txtAcoount_Code.Text + aa3.ToString("00");
                                //txtAcoount_Code.Text = code_genarate1;
                                //value3 = value2 + "-" + aa3.ToString("00");
                            }
                        }
                    }

                }
                else
                {
                    value4 = a1 + "-" + a2 + "-" + a3 + "-" + a4;
                }

                if (a5 == "" && a4.Length <= 2)
                {

                    if (a4 != "" && a5.Length <= 4)
                    {
                        DataSet ds4 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H5' AND Head_Acct_Code = '" + value4 + "' and Record_Deleted = 0");
                        if (ds4.Tables[0].Rows.Count > 0)
                        {
                            value5 = ds4.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value5 == "")
                            {
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                                lbl_error.Text = " H5 not found.. Please input H5";

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();
                            }
                            else
                            {

                                string codes = value5;
                                string[] increment = codes.Split('-');
                                string newa = increment[3].ToString();
                                float aa4 = float.Parse(newa) + 1;
                                string code_genarate = txtAcoount_Code.Text + aa4.ToString("0000");
                                //  lblaccout_code.Text = codes;
                                txtAcoount_Code.Text = code_genarate;

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();


                            }
                            lblaccout_code.Text = txtAcoount_Code.Text;
                            txtHead_Detail.Text = headdetail();
                            txtbaseAcc_Code.Text = Head_Acct_Code();
                        }

                    }
                    else
                    {
                        value5 = a1 + "-" + a2 + "-" + a3 + "-" + a4 + "-" + a5;
                    }
                }

                if (a6 == "" && a5.Length <= 2)
                {

                    if (a5 != "" && a6.Length <= 4)
                    {
                        DataSet ds4 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H6' AND Head_Acct_Code = '" + value5 + "' and Record_Deleted = 0");
                        if (ds4.Tables[0].Rows.Count > 0)
                        {
                            value6 = ds4.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value6 == "")
                            {
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                                lbl_error.Text = " D1 not found.. Please input 0001";

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();
                            }
                            else
                            {

                                string codes = value6;
                                string[] increment = codes.Split('-');
                                string newa = increment[3].ToString();
                                float aa4 = float.Parse(newa) + 1;
                                string code_genarate = txtAcoount_Code.Text + aa4.ToString("0000");
                                //  lblaccout_code.Text = codes;
                                txtAcoount_Code.Text = code_genarate;

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();


                            }
                            lblaccout_code.Text = txtAcoount_Code.Text;
                            txtHead_Detail.Text = headdetail();
                            txtbaseAcc_Code.Text = Head_Acct_Code();
                        }

                    }
                    else
                    {
                        value6 = a1 + "-" + a2 + "-" + a3 + "-" + a4 + "-" + a5 + "-" + a6;
                    }
                }

                if (a7 == "" && a6.Length <= 2)
                {

                    if (a6 != "" && a7.Length <= 4)
                    {
                        DataSet ds4 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'D1' AND Head_Acct_Code = '" + 6 + "' and Record_Deleted = 0");
                        if (ds4.Tables[0].Rows.Count > 0)
                        {
                            value7 = ds4.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value7 == "")
                            {
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                                lbl_error.Text = " D1 not found.. Please input 0001";

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();
                            }
                            else
                            {

                                string codes = value6;
                                string[] increment = codes.Split('-');
                                string newa = increment[3].ToString();
                                float aa4 = float.Parse(newa) + 1;
                                string code_genarate = txtAcoount_Code.Text + aa4.ToString("0000");
                                //  lblaccout_code.Text = codes;
                                txtAcoount_Code.Text = code_genarate;

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();


                            }
                            lblaccout_code.Text = txtAcoount_Code.Text;
                            txtHead_Detail.Text = headdetail();
                            txtbaseAcc_Code.Text = Head_Acct_Code();
                        }

                    }
                    else
                    {
                        value7 = a1 + "-" + a2 + "-" + a3 + "-" + a4 + "-" + a5 + "-" + a6 + "-" + a7;
                    }
                }
            }

            //seg 8 
            else if (noseg() == 8)
            {
                lbl_head1.Text = "";
                lbl_head2.Text = "";
                lbl_head3.Text = "";
                string a1 = "", a2 = "", a3 = "", a4 = "", a5 = "", a6 = "", a7 = "", a8 = "";
                string data = txtAcoount_Code.Text;
                lblaccout_code.Text = data;
                string value1 = "", value2 = "", value3 = "", value4 = "", value5 = "", value6 = "", value7 = "", value8 = "";
                string[] words = data.Split('-');
                string s1 = words[0].ToString();
                string s2 = words[1].ToString();
                string s3 = words[2].ToString();
                string s4 = words[3].ToString();
                string s5 = words[4].ToString();
                string s6 = words[5].ToString();
                string s7 = words[6].ToString();
                string s8 = words[7].ToString();

                a1 = trimcode(s1);
                a2 = trimcode(s2);
                a3 = trimcode(s3);
                a4 = trimcode(s4);
                a5 = trimcode(s5);
                a6 = trimcode(s6);
                a7 = trimcode(s7);
                a8 = trimcode(s8);

                if (a1 == "")
                {
                    DataSet ds1 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H1' and Record_Deleted = 0");
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        value1 = ds1.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                        if (value1 == "")
                        {

                        }
                        else {
                            string code1 = value1;
                            string[] increment1 = code1.Split('-');
                            string newa = increment1[0].ToString();
                            float aa1 = float.Parse(newa);
                            lbl_head1.Text = "H1 : " + aa1.ToString("0");
                            string code_genarate1 = aa1.ToString();
                            txtAcoount_Code.Text = code_genarate1;
                            lbl_error.Text = "";
                            lbl_head2.Text = "";
                            lbl_head3.Text = "";
                        }
                    }

                }
                else
                {
                    DataSet ds1 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H1' and Record_Deleted = 0");
                    value1 = ds1.Tables[0].Rows[0]["ACOUNTCODE"].ToString();


                    string code1 = value1;
                    string[] increment1 = code1.Split('-');
                    string newa = increment1[0].ToString();


                    if (float.Parse(newa) < float.Parse(a1))
                    {
                        float aa1 = float.Parse(newa) + 01;
                        lbl_head1.Text = aa1.ToString("0");
                        string code_genarate1 = aa1.ToString();
                        txtAcoount_Code.Text = code_genarate1;
                    }
                    lbl_error.Text = "";
                    lbl_head2.Text = "";
                    lbl_head3.Text = "";


                    value1 = a1;
                }


                if (a2 == "" && a1.Length <= 1)
                {
                    if (a1 != "" && a2.Length <= 2)
                    {
                        DataSet ds2 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H2' AND Head_Acct_Code = '" + a1 + "' and Record_Deleted = 0");

                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            value2 = ds2.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value2 == "")
                            {
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                                lbl_error.Text = " H1 not found.. Please input H1 code";
                            }
                            else {
                                string code2 = value2;
                                string[] increment2 = code2.Split('-');
                                string newa = increment2[1].ToString();
                                float aa2 = float.Parse(newa);
                                lbl_head1.Text = a1;
                                lbl_error.Text = "";
                                lbl_head2.Text = "-" + aa2.ToString("00");

                                //string code_genarate1 = txtAcoount_Code.Text + aa2.ToString("00");
                                //code_genarate1 = trimcode(code_genarate1);
                                //txtAcoount_Code.Text = code_genarate1;
                                //value2 = a1 + "-" + aa2.ToString("00");
                                lbl_head3.Text = "";
                            }
                        }
                    }

                }
                else
                {
                    DataSet ds2 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H2' AND Head_Acct_Code = '" + a1 + "' and Record_Deleted = 0");

                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        value2 = ds2.Tables[0].Rows[0]["ACOUNTCODE"].ToString();

                        if (value2 == "")
                        {
                            lbl_head1.Text = "";
                            lbl_head2.Text = "";
                            lbl_head3.Text = "";
                            lbl_error.Text = " H1 not found.. Please input H1 code";
                        }
                        else {
                            string code1 = value2;
                            string[] increment1 = code1.Split('-');
                            string newa = increment1[1].ToString();


                            if (float.Parse(newa) < float.Parse(a2))
                            {
                                float aa2 = float.Parse(newa) + 01;
                                lbl_head2.Text = a1 + "-" + aa2.ToString("00");
                                string code_genarate1 = aa2.ToString("00");
                                txtAcoount_Code.Text = a1 + "-" + code_genarate1;

                            }

                            lbl_head3.Text = "";


                            value2 = a1 + "-" + a2;
                        }
                    }
                    else
                    {
                        txtAcoount_Code.Text = a1;
                        lbl_error.Text = "H1 not found first insert H1 ";
                    }
                }

                if (a3 == "" && a2.Length <= 2)
                {
                    if (a2 != "" && a3.Length <= 2)
                    {
                        DataSet ds3 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H3' AND Head_Acct_Code = '" + value2 + "' and Record_Deleted = 0");
                        if (ds3.Tables[0].Rows.Count > 0)
                        {
                            value3 = ds3.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value3 == "")
                            {
                                //   lbl_error.Text = " H3 not found";
                            }
                            else {

                                string code3 = value3;
                                string[] increment3 = code3.Split('-');
                                string newa = increment3[2].ToString();
                                float aa3 = float.Parse(newa);
                                lbl_head1.Text = a1;
                                lbl_head2.Text = "-" + a2;
                                lbl_head3.Text = "-" + aa3.ToString("00");

                                //string code_genarate1 = txtAcoount_Code.Text + aa3.ToString("00");
                                //txtAcoount_Code.Text = code_genarate1;
                                //value3 = value2 + "-" + aa3.ToString("00");
                            }
                        }
                    }

                }
                else
                {
                    value3 = a1 + "-" + a2 + "-" + a3;
                }

                if (a4 == "" && a3.Length <= 2)
                {
                    if (a3 != "" && a4.Length <= 2)
                    {
                        DataSet ds3 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H4' AND Head_Acct_Code = '" + value3 + "' and Record_Deleted = 0");
                        if (ds3.Tables[0].Rows.Count > 0)
                        {
                            value4 = ds3.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value4 == "")
                            {
                                //   lbl_error.Text = " H3 not found";
                            }
                            else {

                                string code3 = value4;
                                string[] increment3 = code3.Split('-');
                                string newa = increment3[2].ToString();
                                float aa3 = float.Parse(newa);
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = value4;

                                //string code_genarate1 = txtAcoount_Code.Text + aa3.ToString("00");
                                //txtAcoount_Code.Text = code_genarate1;
                                //value3 = value2 + "-" + aa3.ToString("00");
                            }
                        }
                    }

                }
                else
                {
                    value4 = a1 + "-" + a2 + "-" + a3 + "-" + a4;
                }

                if (a5 == "" && a4.Length <= 2)
                {

                    if (a4 != "" && a5.Length <= 4)
                    {
                        DataSet ds4 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H5' AND Head_Acct_Code = '" + value4 + "' and Record_Deleted = 0");
                        if (ds4.Tables[0].Rows.Count > 0)
                        {
                            value5 = ds4.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value5 == "")
                            {
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                                lbl_error.Text = " H5 not found.. Please input H5";

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();
                            }
                            else
                            {

                                string codes = value5;
                                string[] increment = codes.Split('-');
                                string newa = increment[3].ToString();
                                float aa4 = float.Parse(newa) + 1;
                                string code_genarate = txtAcoount_Code.Text + aa4.ToString("0000");
                                //  lblaccout_code.Text = codes;
                                txtAcoount_Code.Text = code_genarate;

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();


                            }
                            lblaccout_code.Text = txtAcoount_Code.Text;
                            txtHead_Detail.Text = headdetail();
                            txtbaseAcc_Code.Text = Head_Acct_Code();
                        }

                    }
                    else
                    {
                        value5 = a1 + "-" + a2 + "-" + a3 + "-" + a4 + "-" + a5;
                    }
                }

                if (a6 == "" && a5.Length <= 2)
                {

                    if (a5 != "" && a6.Length <= 4)
                    {
                        DataSet ds4 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H6' AND Head_Acct_Code = '" + value5 + "' and Record_Deleted = 0");
                        if (ds4.Tables[0].Rows.Count > 0)
                        {
                            value6 = ds4.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value6 == "")
                            {
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                                lbl_error.Text = " D1 not found.. Please input 0001";

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();
                            }
                            else
                            {

                                string codes = value6;
                                string[] increment = codes.Split('-');
                                string newa = increment[3].ToString();
                                float aa4 = float.Parse(newa) + 1;
                                string code_genarate = txtAcoount_Code.Text + aa4.ToString("0000");
                                //  lblaccout_code.Text = codes;
                                txtAcoount_Code.Text = code_genarate;

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();


                            }
                            lblaccout_code.Text = txtAcoount_Code.Text;
                            txtHead_Detail.Text = headdetail();
                            txtbaseAcc_Code.Text = Head_Acct_Code();
                        }

                    }
                    else
                    {
                        value6 = a1 + "-" + a2 + "-" + a3 + "-" + a4 + "-" + a5 + "-" + a6;
                    }
                }

                if (a7 == "" && a6.Length <= 2)
                {

                    if (a6 != "" && a7.Length <= 4)
                    {
                        DataSet ds4 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H7' AND Head_Acct_Code = '" + value6 + "' and Record_Deleted = 0");
                        if (ds4.Tables[0].Rows.Count > 0)
                        {
                            value7 = ds4.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value7 == "")
                            {
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                                lbl_error.Text = " D1 not found.. Please input 0001";

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();
                            }
                            else
                            {

                                string codes = value6;
                                string[] increment = codes.Split('-');
                                string newa = increment[3].ToString();
                                float aa4 = float.Parse(newa) + 1;
                                string code_genarate = txtAcoount_Code.Text + aa4.ToString("0000");
                                //  lblaccout_code.Text = codes;
                                txtAcoount_Code.Text = code_genarate;

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();


                            }
                            lblaccout_code.Text = txtAcoount_Code.Text;
                            txtHead_Detail.Text = headdetail();
                            txtbaseAcc_Code.Text = Head_Acct_Code();
                        }

                    }
                    else
                    {
                        value7 = a1 + "-" + a2 + "-" + a3 + "-" + a4 + "-" + a5 + "-" + a6 + "-" + a7;
                    }
                }

                if (a8 == "" && a7.Length <= 2)
                {

                    if (a7 != "" && a8.Length <= 4)
                    {
                        DataSet ds4 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'D1' AND Head_Acct_Code = '" + value7 + "' and Record_Deleted = 0");
                        if (ds4.Tables[0].Rows.Count > 0)
                        {
                            value8 = ds4.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value8 == "")
                            {
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                                lbl_error.Text = " D1 not found.. Please input 0001";

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();
                            }
                            else
                            {

                                string codes = value6;
                                string[] increment = codes.Split('-');
                                string newa = increment[3].ToString();
                                float aa4 = float.Parse(newa) + 1;
                                string code_genarate = txtAcoount_Code.Text + aa4.ToString("0000");
                                //  lblaccout_code.Text = codes;
                                txtAcoount_Code.Text = code_genarate;

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();


                            }
                            lblaccout_code.Text = txtAcoount_Code.Text;
                            txtHead_Detail.Text = headdetail();
                            txtbaseAcc_Code.Text = Head_Acct_Code();
                        }

                    }
                    else
                    {
                        value8 = a1 + "-" + a2 + "-" + a3 + "-" + a4 + "-" + a5 + "-" + a6 + "-" + a7 + "-" + a8;
                    }
                }
            }

            //seg 9 
            else if (noseg() == 9)
            {
                lbl_head1.Text = "";
                lbl_head2.Text = "";
                lbl_head3.Text = "";
                string a1 = "", a2 = "", a3 = "", a4 = "", a5 = "", a6 = "", a7 = "", a8 = "", a9 = "";
                string data = txtAcoount_Code.Text;
                lblaccout_code.Text = data;
                string value1 = "", value2 = "", value3 = "", value4 = "", value5 = "", value6 = "", value7 = "", value8 = "", value9 = ""; ;
                string[] words = data.Split('-');
                string s1 = words[0].ToString();
                string s2 = words[1].ToString();
                string s3 = words[2].ToString();
                string s4 = words[3].ToString();
                string s5 = words[4].ToString();
                string s6 = words[5].ToString();
                string s7 = words[6].ToString();
                string s8 = words[7].ToString();
                string s9 = words[8].ToString();

                a1 = trimcode(s1);
                a2 = trimcode(s2);
                a3 = trimcode(s3);
                a4 = trimcode(s4);
                a5 = trimcode(s5);
                a6 = trimcode(s6);
                a7 = trimcode(s7);
                a8 = trimcode(s8);
                a9 = trimcode(s9);

                if (a1 == "")
                {
                    DataSet ds1 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H1' and Record_Deleted = 0");
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        value1 = ds1.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                        if (value1 == "")
                        {

                        }
                        else {
                            string code1 = value1;
                            string[] increment1 = code1.Split('-');
                            string newa = increment1[0].ToString();
                            float aa1 = float.Parse(newa);
                            lbl_head1.Text = "H1 : " + aa1.ToString("0");
                            string code_genarate1 = aa1.ToString();
                            txtAcoount_Code.Text = code_genarate1;
                            lbl_error.Text = "";
                            lbl_head2.Text = "";
                            lbl_head3.Text = "";
                        }
                    }

                }
                else
                {
                    DataSet ds1 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H1' and Record_Deleted = 0");
                    value1 = ds1.Tables[0].Rows[0]["ACOUNTCODE"].ToString();


                    string code1 = value1;
                    string[] increment1 = code1.Split('-');
                    string newa = increment1[0].ToString();


                    if (float.Parse(newa) < float.Parse(a1))
                    {
                        float aa1 = float.Parse(newa) + 01;
                        lbl_head1.Text = aa1.ToString("0");
                        string code_genarate1 = aa1.ToString();
                        txtAcoount_Code.Text = code_genarate1;
                    }
                    lbl_error.Text = "";
                    lbl_head2.Text = "";
                    lbl_head3.Text = "";


                    value1 = a1;
                }


                if (a2 == "" && a1.Length <= 1)
                {
                    if (a1 != "" && a2.Length <= 2)
                    {
                        DataSet ds2 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H2' AND Head_Acct_Code = '" + a1 + "' and Record_Deleted = 0");

                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            value2 = ds2.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value2 == "")
                            {
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                                lbl_error.Text = " H1 not found.. Please input H1 code";
                            }
                            else {
                                string code2 = value2;
                                string[] increment2 = code2.Split('-');
                                string newa = increment2[1].ToString();
                                float aa2 = float.Parse(newa);
                                lbl_head1.Text = a1;
                                lbl_error.Text = "";
                                lbl_head2.Text = "-" + aa2.ToString("00");

                                //string code_genarate1 = txtAcoount_Code.Text + aa2.ToString("00");
                                //code_genarate1 = trimcode(code_genarate1);
                                //txtAcoount_Code.Text = code_genarate1;
                                //value2 = a1 + "-" + aa2.ToString("00");
                                lbl_head3.Text = "";
                            }
                        }
                    }

                }
                else
                {
                    DataSet ds2 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H2' AND Head_Acct_Code = '" + a1 + "' and Record_Deleted = 0");

                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        value2 = ds2.Tables[0].Rows[0]["ACOUNTCODE"].ToString();

                        if (value2 == "")
                        {
                            lbl_head1.Text = "";
                            lbl_head2.Text = "";
                            lbl_head3.Text = "";
                            lbl_error.Text = " H1 not found.. Please input H1 code";
                        }
                        else {
                            string code1 = value2;
                            string[] increment1 = code1.Split('-');
                            string newa = increment1[1].ToString();


                            if (float.Parse(newa) < float.Parse(a2))
                            {
                                float aa2 = float.Parse(newa) + 01;
                                lbl_head2.Text = a1 + "-" + aa2.ToString("00");
                                string code_genarate1 = aa2.ToString("00");
                                txtAcoount_Code.Text = a1 + "-" + code_genarate1;

                            }

                            lbl_head3.Text = "";


                            value2 = a1 + "-" + a2;
                        }
                    }
                    else
                    {
                        txtAcoount_Code.Text = a1;
                        lbl_error.Text = "H1 not found first insert H1 ";
                    }
                }

                if (a3 == "" && a2.Length <= 2)
                {
                    if (a2 != "" && a3.Length <= 2)
                    {
                        DataSet ds3 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H3' AND Head_Acct_Code = '" + value2 + "' and Record_Deleted = 0");
                        if (ds3.Tables[0].Rows.Count > 0)
                        {
                            value3 = ds3.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value3 == "")
                            {
                                //   lbl_error.Text = " H3 not found";
                            }
                            else {

                                string code3 = value3;
                                string[] increment3 = code3.Split('-');
                                string newa = increment3[2].ToString();
                                float aa3 = float.Parse(newa);
                                lbl_head1.Text = a1;
                                lbl_head2.Text = "-" + a2;
                                lbl_head3.Text = "-" + aa3.ToString("00");

                                //string code_genarate1 = txtAcoount_Code.Text + aa3.ToString("00");
                                //txtAcoount_Code.Text = code_genarate1;
                                //value3 = value2 + "-" + aa3.ToString("00");
                            }
                        }
                    }

                }
                else
                {
                    value3 = a1 + "-" + a2 + "-" + a3;
                }

                if (a4 == "" && a3.Length <= 2)
                {
                    if (a3 != "" && a4.Length <= 2)
                    {
                        DataSet ds3 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H4' AND Head_Acct_Code = '" + value3 + "' and Record_Deleted = 0");
                        if (ds3.Tables[0].Rows.Count > 0)
                        {
                            value4 = ds3.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value4 == "")
                            {
                                //   lbl_error.Text = " H3 not found";
                            }
                            else {

                                string code3 = value4;
                                string[] increment3 = code3.Split('-');
                                string newa = increment3[2].ToString();
                                float aa3 = float.Parse(newa);
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = value4;

                                //string code_genarate1 = txtAcoount_Code.Text + aa3.ToString("00");
                                //txtAcoount_Code.Text = code_genarate1;
                                //value3 = value2 + "-" + aa3.ToString("00");
                            }
                        }
                    }

                }
                else
                {
                    value4 = a1 + "-" + a2 + "-" + a3 + "-" + a4;
                }

                if (a5 == "" && a4.Length <= 2)
                {

                    if (a4 != "" && a5.Length <= 4)
                    {
                        DataSet ds4 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H5' AND Head_Acct_Code = '" + value4 + "' and Record_Deleted = 0");
                        if (ds4.Tables[0].Rows.Count > 0)
                        {
                            value5 = ds4.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value5 == "")
                            {
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                                lbl_error.Text = " H5 not found.. Please input H5";

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();
                            }
                            else
                            {

                                string codes = value5;
                                string[] increment = codes.Split('-');
                                string newa = increment[3].ToString();
                                float aa4 = float.Parse(newa) + 1;
                                string code_genarate = txtAcoount_Code.Text + aa4.ToString("0000");
                                //  lblaccout_code.Text = codes;
                                txtAcoount_Code.Text = code_genarate;

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();


                            }
                            lblaccout_code.Text = txtAcoount_Code.Text;
                            txtHead_Detail.Text = headdetail();
                            txtbaseAcc_Code.Text = Head_Acct_Code();
                        }

                    }
                    else
                    {
                        value5 = a1 + "-" + a2 + "-" + a3 + "-" + a4 + "-" + a5;
                    }
                }

                if (a6 == "" && a5.Length <= 2)
                {

                    if (a5 != "" && a6.Length <= 4)
                    {
                        DataSet ds4 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H6' AND Head_Acct_Code = '" + value5 + "' and Record_Deleted = 0");
                        if (ds4.Tables[0].Rows.Count > 0)
                        {
                            value6 = ds4.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value6 == "")
                            {
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                                lbl_error.Text = " D1 not found.. Please input 0001";

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();
                            }
                            else
                            {

                                string codes = value6;
                                string[] increment = codes.Split('-');
                                string newa = increment[3].ToString();
                                float aa4 = float.Parse(newa) + 1;
                                string code_genarate = txtAcoount_Code.Text + aa4.ToString("0000");
                                //  lblaccout_code.Text = codes;
                                txtAcoount_Code.Text = code_genarate;

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();


                            }
                            lblaccout_code.Text = txtAcoount_Code.Text;
                            txtHead_Detail.Text = headdetail();
                            txtbaseAcc_Code.Text = Head_Acct_Code();
                        }

                    }
                    else
                    {
                        value6 = a1 + "-" + a2 + "-" + a3 + "-" + a4 + "-" + a5 + "-" + a6;
                    }
                }

                if (a7 == "" && a6.Length <= 2)
                {

                    if (a6 != "" && a7.Length <= 4)
                    {
                        DataSet ds4 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H7' AND Head_Acct_Code = '" + value6 + "' and Record_Deleted = 0");
                        if (ds4.Tables[0].Rows.Count > 0)
                        {
                            value7 = ds4.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value7 == "")
                            {
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                                lbl_error.Text = " D1 not found.. Please input 0001";

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();
                            }
                            else
                            {

                                string codes = value6;
                                string[] increment = codes.Split('-');
                                string newa = increment[3].ToString();
                                float aa4 = float.Parse(newa) + 1;
                                string code_genarate = txtAcoount_Code.Text + aa4.ToString("0000");
                                //  lblaccout_code.Text = codes;
                                txtAcoount_Code.Text = code_genarate;

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();


                            }
                            lblaccout_code.Text = txtAcoount_Code.Text;
                            txtHead_Detail.Text = headdetail();
                            txtbaseAcc_Code.Text = Head_Acct_Code();
                        }

                    }
                    else
                    {
                        value7 = a1 + "-" + a2 + "-" + a3 + "-" + a4 + "-" + a5 + "-" + a6 + "-" + a7;
                    }
                }

                if (a8 == "" && a7.Length <= 2)
                {

                    if (a7 != "" && a8.Length <= 4)
                    {
                        DataSet ds4 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H8' AND Head_Acct_Code = '" + value7 + "' and Record_Deleted = 0");
                        if (ds4.Tables[0].Rows.Count > 0)
                        {
                            value8 = ds4.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value8 == "")
                            {
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                                lbl_error.Text = " H8 not found.. Please input H8";

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();
                            }
                            else
                            {

                                string codes = value7;
                                string[] increment = codes.Split('-');
                                string newa = increment[3].ToString();
                                float aa4 = float.Parse(newa) + 1;
                                string code_genarate = txtAcoount_Code.Text + aa4.ToString("0000");
                                //  lblaccout_code.Text = codes;
                                txtAcoount_Code.Text = code_genarate;

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();


                            }
                            lblaccout_code.Text = txtAcoount_Code.Text;
                            txtHead_Detail.Text = headdetail();
                            txtbaseAcc_Code.Text = Head_Acct_Code();
                        }

                    }
                    else
                    {
                        value8 = a1 + "-" + a2 + "-" + a3 + "-" + a4 + "-" + a5 + "-" + a6 + "-" + a7 + "-" + a8;
                    }
                }

                if (a9 == "" && a8.Length <= 2)
                {

                    if (a8 != "" && a9.Length <= 4)
                    {
                        DataSet ds4 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'D1' AND Head_Acct_Code = '" + value7 + "' and Record_Deleted = 0");
                        if (ds4.Tables[0].Rows.Count > 0)
                        {
                            value9 = ds4.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value9 == "")
                            {
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                                lbl_error.Text = " D1 not found.. Please input 0001";

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();
                            }
                            else
                            {

                                string codes = value8;
                                string[] increment = codes.Split('-');
                                string newa = increment[3].ToString();
                                float aa4 = float.Parse(newa) + 1;
                                string code_genarate = txtAcoount_Code.Text + aa4.ToString("0000");
                                //  lblaccout_code.Text = codes;
                                txtAcoount_Code.Text = code_genarate;

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();


                            }
                            lblaccout_code.Text = txtAcoount_Code.Text;
                            txtHead_Detail.Text = headdetail();
                            txtbaseAcc_Code.Text = Head_Acct_Code();
                        }

                    }
                    else
                    {
                        value8 = a1 + "-" + a2 + "-" + a3 + "-" + a4 + "-" + a5 + "-" + a6 + "-" + a7 + "-" + a8 + "-" + a9;
                    }
                }
            }

            //seg 10 
            else if (noseg() == 9)
            {
                lbl_head1.Text = "";
                lbl_head2.Text = "";
                lbl_head3.Text = "";
                string a1 = "", a2 = "", a3 = "", a4 = "", a5 = "", a6 = "", a7 = "", a8 = "", a9 = "", a10 = "";
                string data = txtAcoount_Code.Text;
                lblaccout_code.Text = data;
                string value1 = "", value2 = "", value3 = "", value4 = "", value5 = "", value6 = "", value7 = "", value8 = "", value9 = "", value10 = "";
                string[] words = data.Split('-');
                string s1 = words[0].ToString();
                string s2 = words[1].ToString();
                string s3 = words[2].ToString();
                string s4 = words[3].ToString();
                string s5 = words[4].ToString();
                string s6 = words[5].ToString();
                string s7 = words[6].ToString();
                string s8 = words[7].ToString();
                string s9 = words[8].ToString();
                string s10 = words[9].ToString();

                a1 = trimcode(s1);
                a2 = trimcode(s2);
                a3 = trimcode(s3);
                a4 = trimcode(s4);
                a5 = trimcode(s5);
                a6 = trimcode(s6);
                a7 = trimcode(s7);
                a8 = trimcode(s8);
                a9 = trimcode(s9);
                a10 = trimcode(s10);

                if (a1 == "")
                {
                    DataSet ds1 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H1' and Record_Deleted = 0");
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        value1 = ds1.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                        if (value1 == "")
                        {

                        }
                        else {
                            string code1 = value1;
                            string[] increment1 = code1.Split('-');
                            string newa = increment1[0].ToString();
                            float aa1 = float.Parse(newa);
                            lbl_head1.Text = "H1 : " + aa1.ToString("0");
                            string code_genarate1 = aa1.ToString();
                            txtAcoount_Code.Text = code_genarate1;
                            lbl_error.Text = "";
                            lbl_head2.Text = "";
                            lbl_head3.Text = "";
                        }
                    }

                }
                else
                {
                    DataSet ds1 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H1' and Record_Deleted = 0");
                    value1 = ds1.Tables[0].Rows[0]["ACOUNTCODE"].ToString();


                    string code1 = value1;
                    string[] increment1 = code1.Split('-');
                    string newa = increment1[0].ToString();


                    if (float.Parse(newa) < float.Parse(a1))
                    {
                        float aa1 = float.Parse(newa) + 01;
                        lbl_head1.Text = aa1.ToString("0");
                        string code_genarate1 = aa1.ToString();
                        txtAcoount_Code.Text = code_genarate1;
                    }
                    lbl_error.Text = "";
                    lbl_head2.Text = "";
                    lbl_head3.Text = "";


                    value1 = a1;
                }


                if (a2 == "" && a1.Length <= 1)
                {
                    if (a1 != "" && a2.Length <= 2)
                    {
                        DataSet ds2 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H2' AND Head_Acct_Code = '" + a1 + "' and Record_Deleted = 0");

                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            value2 = ds2.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value2 == "")
                            {
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                                lbl_error.Text = " H1 not found.. Please input H1 code";
                            }
                            else {
                                string code2 = value2;
                                string[] increment2 = code2.Split('-');
                                string newa = increment2[1].ToString();
                                float aa2 = float.Parse(newa);
                                lbl_head1.Text = a1;
                                lbl_error.Text = "";
                                lbl_head2.Text = "-" + aa2.ToString("00");

                                //string code_genarate1 = txtAcoount_Code.Text + aa2.ToString("00");
                                //code_genarate1 = trimcode(code_genarate1);
                                //txtAcoount_Code.Text = code_genarate1;
                                //value2 = a1 + "-" + aa2.ToString("00");
                                lbl_head3.Text = "";
                            }
                        }
                    }

                }
                else
                {
                    DataSet ds2 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H2' AND Head_Acct_Code = '" + a1 + "' and Record_Deleted = 0");

                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        value2 = ds2.Tables[0].Rows[0]["ACOUNTCODE"].ToString();

                        if (value2 == "")
                        {
                            lbl_head1.Text = "";
                            lbl_head2.Text = "";
                            lbl_head3.Text = "";
                            lbl_error.Text = " H1 not found.. Please input H1 code";
                        }
                        else {
                            string code1 = value2;
                            string[] increment1 = code1.Split('-');
                            string newa = increment1[1].ToString();


                            if (float.Parse(newa) < float.Parse(a2))
                            {
                                float aa2 = float.Parse(newa) + 01;
                                lbl_head2.Text = a1 + "-" + aa2.ToString("00");
                                string code_genarate1 = aa2.ToString("00");
                                txtAcoount_Code.Text = a1 + "-" + code_genarate1;

                            }

                            lbl_head3.Text = "";


                            value2 = a1 + "-" + a2;
                        }
                    }
                    else
                    {
                        txtAcoount_Code.Text = a1;
                        lbl_error.Text = "H1 not found first insert H1 ";
                    }
                }

                if (a3 == "" && a2.Length <= 2)
                {
                    if (a2 != "" && a3.Length <= 2)
                    {
                        DataSet ds3 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H3' AND Head_Acct_Code = '" + value2 + "' and Record_Deleted = 0");
                        if (ds3.Tables[0].Rows.Count > 0)
                        {
                            value3 = ds3.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value3 == "")
                            {
                                //   lbl_error.Text = " H3 not found";
                            }
                            else {

                                string code3 = value3;
                                string[] increment3 = code3.Split('-');
                                string newa = increment3[2].ToString();
                                float aa3 = float.Parse(newa);
                                lbl_head1.Text = a1;
                                lbl_head2.Text = "-" + a2;
                                lbl_head3.Text = "-" + aa3.ToString("00");

                                //string code_genarate1 = txtAcoount_Code.Text + aa3.ToString("00");
                                //txtAcoount_Code.Text = code_genarate1;
                                //value3 = value2 + "-" + aa3.ToString("00");
                            }
                        }
                    }

                }
                else
                {
                    value3 = a1 + "-" + a2 + "-" + a3;
                }

                if (a4 == "" && a3.Length <= 2)
                {
                    if (a3 != "" && a4.Length <= 2)
                    {
                        DataSet ds3 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H4' AND Head_Acct_Code = '" + value3 + "' and Record_Deleted = 0");
                        if (ds3.Tables[0].Rows.Count > 0)
                        {
                            value4 = ds3.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value4 == "")
                            {
                                //   lbl_error.Text = " H3 not found";
                            }
                            else {

                                string code3 = value4;
                                string[] increment3 = code3.Split('-');
                                string newa = increment3[2].ToString();
                                float aa3 = float.Parse(newa);
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = value4;

                                //string code_genarate1 = txtAcoount_Code.Text + aa3.ToString("00");
                                //txtAcoount_Code.Text = code_genarate1;
                                //value3 = value2 + "-" + aa3.ToString("00");
                            }
                        }
                    }

                }
                else
                {
                    value4 = a1 + "-" + a2 + "-" + a3 + "-" + a4;
                }

                if (a5 == "" && a4.Length <= 2)
                {

                    if (a4 != "" && a5.Length <= 4)
                    {
                        DataSet ds4 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H5' AND Head_Acct_Code = '" + value4 + "' and Record_Deleted = 0");
                        if (ds4.Tables[0].Rows.Count > 0)
                        {
                            value5 = ds4.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value5 == "")
                            {
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                                lbl_error.Text = " H5 not found.. Please input H5";

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();
                            }
                            else
                            {

                                string codes = value5;
                                string[] increment = codes.Split('-');
                                string newa = increment[3].ToString();
                                float aa4 = float.Parse(newa) + 1;
                                string code_genarate = txtAcoount_Code.Text + aa4.ToString("0000");
                                //  lblaccout_code.Text = codes;
                                txtAcoount_Code.Text = code_genarate;

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();


                            }
                            lblaccout_code.Text = txtAcoount_Code.Text;
                            txtHead_Detail.Text = headdetail();
                            txtbaseAcc_Code.Text = Head_Acct_Code();
                        }

                    }
                    else
                    {
                        value5 = a1 + "-" + a2 + "-" + a3 + "-" + a4 + "-" + a5;
                    }
                }

                if (a6 == "" && a5.Length <= 2)
                {

                    if (a5 != "" && a6.Length <= 4)
                    {
                        DataSet ds4 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H6' AND Head_Acct_Code = '" + value5 + "' and Record_Deleted = 0");
                        if (ds4.Tables[0].Rows.Count > 0)
                        {
                            value6 = ds4.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value6 == "")
                            {
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                                lbl_error.Text = " D1 not found.. Please input 0001";

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();
                            }
                            else
                            {

                                string codes = value6;
                                string[] increment = codes.Split('-');
                                string newa = increment[3].ToString();
                                float aa4 = float.Parse(newa) + 1;
                                string code_genarate = txtAcoount_Code.Text + aa4.ToString("0000");
                                //  lblaccout_code.Text = codes;
                                txtAcoount_Code.Text = code_genarate;

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();


                            }
                            lblaccout_code.Text = txtAcoount_Code.Text;
                            txtHead_Detail.Text = headdetail();
                            txtbaseAcc_Code.Text = Head_Acct_Code();
                        }

                    }
                    else
                    {
                        value6 = a1 + "-" + a2 + "-" + a3 + "-" + a4 + "-" + a5 + "-" + a6;
                    }
                }

                if (a7 == "" && a6.Length <= 2)
                {

                    if (a6 != "" && a7.Length <= 4)
                    {
                        DataSet ds4 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H7' AND Head_Acct_Code = '" + value6 + "' and Record_Deleted = 0");
                        if (ds4.Tables[0].Rows.Count > 0)
                        {
                            value7 = ds4.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value7 == "")
                            {
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                                lbl_error.Text = " D1 not found.. Please input 0001";

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();
                            }
                            else
                            {

                                string codes = value6;
                                string[] increment = codes.Split('-');
                                string newa = increment[3].ToString();
                                float aa4 = float.Parse(newa) + 1;
                                string code_genarate = txtAcoount_Code.Text + aa4.ToString("0000");
                                //  lblaccout_code.Text = codes;
                                txtAcoount_Code.Text = code_genarate;

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();


                            }
                            lblaccout_code.Text = txtAcoount_Code.Text;
                            txtHead_Detail.Text = headdetail();
                            txtbaseAcc_Code.Text = Head_Acct_Code();
                        }

                    }
                    else
                    {
                        value7 = a1 + "-" + a2 + "-" + a3 + "-" + a4 + "-" + a5 + "-" + a6 + "-" + a7;
                    }
                }

                if (a8 == "" && a7.Length <= 2)
                {

                    if (a7 != "" && a8.Length <= 4)
                    {
                        DataSet ds4 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H8' AND Head_Acct_Code = '" + value7 + "' and Record_Deleted = 0");
                        if (ds4.Tables[0].Rows.Count > 0)
                        {
                            value8 = ds4.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value8 == "")
                            {
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                                lbl_error.Text = " H8 not found.. Please input H8";

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();
                            }
                            else
                            {

                                string codes = value7;
                                string[] increment = codes.Split('-');
                                string newa = increment[3].ToString();
                                float aa4 = float.Parse(newa) + 1;
                                string code_genarate = txtAcoount_Code.Text + aa4.ToString("0000");
                                //  lblaccout_code.Text = codes;
                                txtAcoount_Code.Text = code_genarate;

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();


                            }
                            lblaccout_code.Text = txtAcoount_Code.Text;
                            txtHead_Detail.Text = headdetail();
                            txtbaseAcc_Code.Text = Head_Acct_Code();
                        }

                    }
                    else
                    {
                        value8 = a1 + "-" + a2 + "-" + a3 + "-" + a4 + "-" + a5 + "-" + a6 + "-" + a7 + "-" + a8;
                    }
                }

                if (a9 == "" && a8.Length <= 2)
                {

                    if (a8 != "" && a9.Length <= 4)
                    {
                        DataSet ds4 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'H9' AND Head_Acct_Code = '" + value8 + "' and Record_Deleted = 0");
                        if (ds4.Tables[0].Rows.Count > 0)
                        {
                            value9 = ds4.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value9 == "")
                            {
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                                lbl_error.Text = " H9 not found.. Please input H9";

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();
                            }
                            else
                            {

                                string codes = value8;
                                string[] increment = codes.Split('-');
                                string newa = increment[3].ToString();
                                float aa4 = float.Parse(newa) + 1;
                                string code_genarate = txtAcoount_Code.Text + aa4.ToString("0000");
                                //  lblaccout_code.Text = codes;
                                txtAcoount_Code.Text = code_genarate;

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();


                            }
                            lblaccout_code.Text = txtAcoount_Code.Text;
                            txtHead_Detail.Text = headdetail();
                            txtbaseAcc_Code.Text = Head_Acct_Code();
                        }

                    }
                    else
                    {
                        value9 = a1 + "-" + a2 + "-" + a3 + "-" + a4 + "-" + a5 + "-" + a6 + "-" + a7 + "-" + a8 + "-" + a9;
                    }
                }

                if (a10 == "" && a9.Length <= 2)
                {

                    if (a9 != "" && a10.Length <= 4)
                    {
                        DataSet ds4 = dml.Find("SELECT MAX(Acct_Code) AS ACOUNTCODE FROM SET_COA_detail WHERE Head_detail_ID = 'D1' AND Head_Acct_Code = '" + value9 + "' and Record_Deleted = 0");
                        if (ds4.Tables[0].Rows.Count > 0)
                        {
                            value10 = ds4.Tables[0].Rows[0]["ACOUNTCODE"].ToString();
                            if (value10 == "")
                            {
                                lbl_head1.Text = "";
                                lbl_head2.Text = "";
                                lbl_head3.Text = "";
                                lbl_error.Text = " D1 not found.. Please input 0001";

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();
                            }
                            else
                            {

                                string codes = value9;
                                string[] increment = codes.Split('-');
                                string newa = increment[3].ToString();
                                float aa4 = float.Parse(newa) + 1;
                                string code_genarate = txtAcoount_Code.Text + aa4.ToString("0000");
                                //  lblaccout_code.Text = codes;
                                txtAcoount_Code.Text = code_genarate;

                                txtHead_Detail.Text = headdetail();
                                txtbaseAcc_Code.Text = Head_Acct_Code();


                            }
                            lblaccout_code.Text = txtAcoount_Code.Text;
                            txtHead_Detail.Text = headdetail();
                            txtbaseAcc_Code.Text = Head_Acct_Code();
                        }

                    }
                    else
                    {
                        value10 = a1 + "-" + a2 + "-" + a3 + "-" + a4 + "-" + a5 + "-" + a6 + "-" + a7 + "-" + a8 + "-" + a9 + "-" + a10;
                    }
                }

            }
        }
        catch(Exception ex)
        {
            Label1.ForeColor = Color.Red;
            Label1.Text = ex.Message;
           
        }

    }

    public string trimcode(string code)
    {

        char[] charsToTrim2 = { '-', '_' };
        string s = code.Trim(charsToTrim2);
        return s;

    }
    public string trimcodeinsert(string code)
    {

        char[] charsToTrim2 = { '_' };
        string s = code.Trim(charsToTrim2);
        return s;

    }

    public string COAMasterID()
    {
        
            userid = Request.QueryString["UserID"];
            string compid = "";
            string gocid = "";
            DataSet ds = dml.Find("select compid, GOCId from SET_Company where CompName='" + Request.Cookies["compNAme"].Value + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                compid = ds.Tables[0].Rows[0]["compid"].ToString();
                gocid = ds.Tables[0].Rows[0]["GOCId"].ToString();

            }
            DataSet ds_goc = dml.Find("select COA_M_ID from SET_COA_Master where CompId = " + compid + " and  Gocid=" + gocid + "");


            string COA_M_ID = ds_goc.Tables[0].Rows[0]["COA_M_ID"].ToString();

            return COA_M_ID;
    }

    public string show_username()
    {
        userid = Request.QueryString["UserID"];
        return userid;
    }

    public string headdetail()
    {
       
        string str = "";
         if (noseg() == 3)
        {
            string h_D = lblaccout_code.Text;
            int inc = 0;
            string[] words = h_D.Split('-');
            string a1 = words[0].ToString();
            string a2 = trimcode(words[1].ToString());
            string a3 = trimcode(words[2].ToString());
           

            if (a1 != "")
            {
                inc = inc + 1;
            }
            if (a2 != "")
            {
                inc = inc + 1;
            }
            if (a3 != "")
            {
                inc = inc + 1;
            }
           


            if (inc == 1)
            {
                str = "H1";
            }
            if (inc == 2)
            {
                str = "H2";
            }
            if (inc == 3)
            {
                str = "D1";
            }
         

            return str;
        }
        else if(noseg() == 4)
        {
            string h_D = lblaccout_code.Text;
            int inc = 0;
            string[] words = h_D.Split('-');
            string a1 = words[0].ToString();
            string a2 = trimcode(words[1].ToString());
            string a3 = trimcode(words[2].ToString());
            string a4 = trimcode(words[3].ToString());

            if (a1 != "")
            {
                inc = inc + 1;
            }
            if (a2 != "")
            {
                inc = inc + 1;
            }
            if (a3 != "")
            {
                inc = inc + 1;
            }
            if (a4 != "")
            {
                inc = inc + 1;
            }


            if (inc == 1)
            {
                str = "H1";
            }
            if (inc == 2)
            {
                str = "H2";
            }
            if (inc == 3)
            {
                str = "H3";
            }
            if (inc == 4)
            {
                str = "D1";
            }

            return str;
        }
        else if (noseg() == 5)
        {
            string h_D = lblaccout_code.Text;
            int inc = 0;
            string[] words = h_D.Split('-');
            string a1 = words[0].ToString();
            string a2 = trimcode(words[1].ToString());
            string a3 = trimcode(words[2].ToString());
            string a4 = trimcode(words[3].ToString());
            string a5 = trimcode(words[4].ToString());

            if (a1 != "")
            {
                inc = inc + 1;
            }
            if (a2 != "")
            {
                inc = inc + 1;
            }
            if (a3 != "")
            {
                inc = inc + 1;
            }
            if (a4 != "")
            {
                inc = inc + 1;
            }
            if (a5 != "")
            {
                inc = inc + 1;
            }


            if (inc == 1)
            {
                str = "H1";
            }
            if (inc == 2)
            {
                str = "H2";
            }
            if (inc == 3)
            {
                str = "H3";
            }
            if (inc == 4)
            {
                str = "H4";
            }
            if (inc == 5)
            {
                str = "D1";
            }

            return str;
        }

        else if (noseg() == 6)
        {
            string h_D = lblaccout_code.Text;
            int inc = 0;
            string[] words = h_D.Split('-');
            string a1 = words[0].ToString();
            string a2 = trimcode(words[1].ToString());
            string a3 = trimcode(words[2].ToString());
            string a4 = trimcode(words[3].ToString());
            string a5 = trimcode(words[4].ToString());
            string a6 = trimcode(words[5].ToString());

            if (a1 != "")
            {
                inc = inc + 1;
            }
            if (a2 != "")
            {
                inc = inc + 1;
            }
            if (a3 != "")
            {
                inc = inc + 1;
            }
            if (a4 != "")
            {
                inc = inc + 1;
            }
            if (a5 != "")
            {
                inc = inc + 1;
            }
            if (a6 != "")
            {
                inc = inc + 1;
            }


            if (inc == 1)
            {
                str = "H1";
            }
            if (inc == 2)
            {
                str = "H2";
            }
            if (inc == 3)
            {
                str = "H3";
            }
            if (inc == 4)
            {
                str = "H4";
            }
            if (inc == 5)
            {
                str = "H5";
            }
            if (inc == 6)
            {
                str = "D1";
            }

            return str;
        }

        else if (noseg() == 7)
        {
            string h_D = lblaccout_code.Text;
            int inc = 0;
            string[] words = h_D.Split('-');
            string a1 = words[0].ToString();
            string a2 = trimcode(words[1].ToString());
            string a3 = trimcode(words[2].ToString());
            string a4 = trimcode(words[3].ToString());
            string a5 = trimcode(words[4].ToString());
            string a6 = trimcode(words[5].ToString());
            string a7 = trimcode(words[6].ToString());

            if (a1 != "")
            {
                inc = inc + 1;
            }
            if (a2 != "")
            {
                inc = inc + 1;
            }
            if (a3 != "")
            {
                inc = inc + 1;
            }
            if (a4 != "")
            {
                inc = inc + 1;
            }
            if (a5 != "")
            {
                inc = inc + 1;
            }
            if (a6 != "")
            {
                inc = inc + 1;
            }
            if (a7 != "")
            {
                inc = inc + 1;
            }


            if (inc == 1)
            {
                str = "H1";
            }
            if (inc == 2)
            {
                str = "H2";
            }
            if (inc == 3)
            {
                str = "H3";
            }
            if (inc == 4)
            {
                str = "H4";
            }
            if (inc == 5)
            {
                str = "H5";
            }
            if (inc == 6)
            {
                str = "H6";
            }
            if (inc == 7)
            {
                str = "D1";
            }

            return str;
        }

        else if (noseg() == 8)
        {
            string h_D = lblaccout_code.Text;
            int inc = 0;
            string[] words = h_D.Split('-');
            string a1 = words[0].ToString();
            string a2 = trimcode(words[1].ToString());
            string a3 = trimcode(words[2].ToString());
            string a4 = trimcode(words[3].ToString());
            string a5 = trimcode(words[4].ToString());
            string a6 = trimcode(words[5].ToString());
            string a7 = trimcode(words[6].ToString());
            string a8 = trimcode(words[7].ToString());

            if (a1 != "")
            {
                inc = inc + 1;
            }
            if (a2 != "")
            {
                inc = inc + 1;
            }
            if (a3 != "")
            {
                inc = inc + 1;
            }
            if (a4 != "")
            {
                inc = inc + 1;
            }
            if (a5 != "")
            {
                inc = inc + 1;
            }
            if (a6 != "")
            {
                inc = inc + 1;
            }
            if (a7 != "")
            {
                inc = inc + 1;
            }
            if (a8 != "")
            {
                inc = inc + 1;
            }


            if (inc == 1)
            {
                str = "H1";
            }
            if (inc == 2)
            {
                str = "H2";
            }
            if (inc == 3)
            {
                str = "H3";
            }
            if (inc == 4)
            {
                str = "H4";
            }
            if (inc == 5)
            {
                str = "H5";
            }
            if (inc == 6)
            {
                str = "H6";
            }
            if (inc == 7)
            {
                str = "H7";
            }
            if (inc == 8)
            {
                str = "D1";
            }

            return str;
        }

        else if (noseg() == 8)
        {
            string h_D = lblaccout_code.Text;
            int inc = 0;
            string[] words = h_D.Split('-');
            string a1 = words[0].ToString();
            string a2 = trimcode(words[1].ToString());
            string a3 = trimcode(words[2].ToString());
            string a4 = trimcode(words[3].ToString());
            string a5 = trimcode(words[4].ToString());
            string a6 = trimcode(words[5].ToString());
            string a7 = trimcode(words[6].ToString());
            string a8 = trimcode(words[7].ToString());
            string a9 = trimcode(words[8].ToString());

            if (a1 != "")
            {
                inc = inc + 1;
            }
            if (a2 != "")
            {
                inc = inc + 1;
            }
            if (a3 != "")
            {
                inc = inc + 1;
            }
            if (a4 != "")
            {
                inc = inc + 1;
            }
            if (a5 != "")
            {
                inc = inc + 1;
            }
            if (a6 != "")
            {
                inc = inc + 1;
            }
            if (a7 != "")
            {
                inc = inc + 1;
            }
            if (a8 != "")
            {
                inc = inc + 1;
            }
            if (a9 != "")
            {
                inc = inc + 1;
            }


            if (inc == 1)
            {
                str = "H1";
            }
            if (inc == 2)
            {
                str = "H2";
            }
            if (inc == 3)
            {
                str = "H3";
            }
            if (inc == 4)
            {
                str = "H4";
            }
            if (inc == 5)
            {
                str = "H5";
            }
            if (inc == 6)
            {
                str = "H6";
            }
            if (inc == 7)
            {
                str = "H7";
            }
            if (inc == 8)
            {
                str = "H8";
            }
            if (inc == 9)
            {
                str = "D1";
            }

            return str;
        }
        else if (noseg() == 9)
        {
            string h_D = lblaccout_code.Text;
            int inc = 0;
            string[] words = h_D.Split('-');
            string a1 = words[0].ToString();
            string a2 = trimcode(words[1].ToString());
            string a3 = trimcode(words[2].ToString());
            string a4 = trimcode(words[3].ToString());
            string a5 = trimcode(words[4].ToString());
            string a6 = trimcode(words[5].ToString());
            string a7 = trimcode(words[6].ToString());
            string a8 = trimcode(words[7].ToString());
            string a9 = trimcode(words[8].ToString());
            string a10 = trimcode(words[9].ToString());


            if (a1 != "")
            {
                inc = inc + 1;
            }
            if (a2 != "")
            {
                inc = inc + 1;
            }
            if (a3 != "")
            {
                inc = inc + 1;
            }
            if (a4 != "")
            {
                inc = inc + 1;
            }
            if (a5 != "")
            {
                inc = inc + 1;
            }
            if (a6 != "")
            {
                inc = inc + 1;
            }
            if (a7 != "")
            {
                inc = inc + 1;
            }
            if (a8 != "")
            {
                inc = inc + 1;
            }
            if (a9 != "")
            {
                inc = inc + 1;
            }
            if (a10 != "")
            {
                inc = inc + 1;
            }


            if (inc == 1)
            {
                str = "H1";
            }
            if (inc == 2)
            {
                str = "H2";
            }
            if (inc == 3)
            {
                str = "H3";
            }
            if (inc == 4)
            {
                str = "H4";
            }
            if (inc == 5)
            {
                str = "H5";
            }
            if (inc == 6)
            {
                str = "H6";
            }
            if (inc == 7)
            {
                str = "H7";
            }
            if (inc == 8)
            {
                str = "H8";
            }
            if (inc == 9)
            {
                str = "H9";
            }
            if (inc == 10)
            {
                str = "D1";
            }

            return str;
        }
        else
        {
            return str = "";
        }

    }

    public string Head_Acct_Code()
    {
        string str = "";
        if (noseg() == 3)
        {
            string h_D = lblaccout_code.Text;
            int inc = 0;
            string[] words = h_D.Split('-');

            string a1 = words[0].ToString();
            string a2 = trimcode(words[1].ToString());
            string a3 = trimcode(words[2].ToString());
           

            if (a1 != "")
            {
                inc = inc + 1;
            }
            if (a2 != "")
            {
                inc = inc + 1;
            }
            if (a3 != "")
            {
                inc = inc + 1;
            }
           

            if (inc == 1)
            {
                str = "-";
            }
            if (inc == 2)
            {
                str = a1.ToString();
            }
            if (inc == 3)
            {
                str = a1.ToString() + "-" + a2.ToString();
            }

            return str;
        }
        else if (noseg() == 4)
        {
            string h_D = lblaccout_code.Text;
            int inc = 0;
            string[] words = h_D.Split('-');

            string a1 = words[0].ToString();
            string a2 = trimcode(words[1].ToString());
            string a3 = trimcode(words[2].ToString());
            string a4 = trimcode(words[3].ToString());

            if (a1 != "")
            {
                inc = inc + 1;
            }
            if (a2 != "")
            {
                inc = inc + 1;
            }
            if (a3 != "")
            {
                inc = inc + 1;
            }
            if (a4 != "")
            {
                inc = inc + 1;
            }

           
            if (inc == 1)
            {
                str = "-";
            }
            if (inc == 2)
            {
                str = a1.ToString();
            }
            if (inc == 3)
            {
                str = a1.ToString() + "-" + a2.ToString();
            }
            if (inc == 4)
            {
                str = a1.ToString() + "-" + a2.ToString() + "-" + a3.ToString();
            }

            return str;
        }

        else if (noseg() == 5)
        {
            string h_D = lblaccout_code.Text;
            int inc = 0;
            string[] words = h_D.Split('-');

            string a1 = words[0].ToString();
            string a2 = trimcode(words[1].ToString());
            string a3 = trimcode(words[2].ToString());
            string a4 = trimcode(words[3].ToString());
            string a5 = trimcode(words[4].ToString());

            if (a1 != "")
            {
                inc = inc + 1;
            }
            if (a2 != "")
            {
                inc = inc + 1;
            }
            if (a3 != "")
            {
                inc = inc + 1;
            }
            if (a4 != "")
            {
                inc = inc + 1;
            }
            if (a5 != "")
            {
                inc = inc + 1;
            }


            if (inc == 1)
            {
                str = "-";
            }
            if (inc == 2)
            {
                str = a1.ToString();
            }
            if (inc == 3)
            {
                str = a1.ToString() + "-" + a2.ToString();
            }
            if (inc == 4)
            {
                str = a1.ToString() + "-" + a2.ToString() + "-" + a3.ToString();
            }
            if (inc == 5)
            {
                str = a1.ToString() + "-" + a2.ToString() + "-" + a3.ToString() + "-" + a4.ToString();
            }

            return str;
        }

        else if (noseg() == 6)
        {
            string h_D = lblaccout_code.Text;
            int inc = 0;
            string[] words = h_D.Split('-');

            string a1 = words[0].ToString();
            string a2 = trimcode(words[1].ToString());
            string a3 = trimcode(words[2].ToString());
            string a4 = trimcode(words[3].ToString());
            string a5 = trimcode(words[4].ToString());
            string a6 = trimcode(words[5].ToString());

            if (a1 != "")
            {
                inc = inc + 1;
            }
            if (a2 != "")
            {
                inc = inc + 1;
            }
            if (a3 != "")
            {
                inc = inc + 1;
            }
            if (a4 != "")
            {
                inc = inc + 1;
            }
            if (a5 != "")
            {
                inc = inc + 1;
            }
            if (a6 != "")
            {
                inc = inc + 1;
            }


            if (inc == 1)
            {
                str = "-";
            }
            if (inc == 2)
            {
                str = a1.ToString();
            }
            if (inc == 3)
            {
                str = a1.ToString() + "-" + a2.ToString();
            }
            if (inc == 4)
            {
                str = a1.ToString() + "-" + a2.ToString() + "-" + a3.ToString();
            }
            if (inc == 5)
            {
                str = a1.ToString() + "-" + a2.ToString() + "-" + a3.ToString() + "-" + a4.ToString();
            }
            if (inc == 6)
            {
                str = a1.ToString() + "-" + a2.ToString() + "-" + a3.ToString() + "-" + a4.ToString() + "-" + a5.ToString();
            }

            return str;
        }
        else if (noseg() == 7)
        {
            string h_D = lblaccout_code.Text;
            int inc = 0;
            string[] words = h_D.Split('-');

            string a1 = words[0].ToString();
            string a2 = trimcode(words[1].ToString());
            string a3 = trimcode(words[2].ToString());
            string a4 = trimcode(words[3].ToString());
            string a5 = trimcode(words[4].ToString());
            string a6 = trimcode(words[5].ToString());
            string a7 = trimcode(words[6].ToString());

            if (a1 != "")
            {
                inc = inc + 1;
            }
            if (a2 != "")
            {
                inc = inc + 1;
            }
            if (a3 != "")
            {
                inc = inc + 1;
            }
            if (a4 != "")
            {
                inc = inc + 1;
            }
            if (a5 != "")
            {
                inc = inc + 1;
            }
            if (a6 != "")
            {
                inc = inc + 1;
            }
            if (a7 != "")
            {
                inc = inc + 1;
            }


            if (inc == 1)
            {
                str = "-";
            }
            if (inc == 2)
            {
                str = a1.ToString();
            }
            if (inc == 3)
            {
                str = a1.ToString() + "-" + a2.ToString();
            }
            if (inc == 4)
            {
                str = a1.ToString() + "-" + a2.ToString() + "-" + a3.ToString();
            }
            if (inc == 5)
            {
                str = a1.ToString() + "-" + a2.ToString() + "-" + a3.ToString() + "-" + a4.ToString();
            }
            if (inc == 6)
            {
                str = a1.ToString() + "-" + a2.ToString() + "-" + a3.ToString() + "-" + a4.ToString() + "-" + a5.ToString();
            }
            if (inc == 7)
            {
                str = a1.ToString() + "-" + a2.ToString() + "-" + a3.ToString() + "-" + a4.ToString() + "-" + a5.ToString() + "-" + a6.ToString();
            }

            return str;
        }

        else if (noseg() == 8)
        {
            string h_D = lblaccout_code.Text;
            int inc = 0;
            string[] words = h_D.Split('-');

            string a1 = words[0].ToString();
            string a2 = trimcode(words[1].ToString());
            string a3 = trimcode(words[2].ToString());
            string a4 = trimcode(words[3].ToString());
            string a5 = trimcode(words[4].ToString());
            string a6 = trimcode(words[5].ToString());
            string a7 = trimcode(words[6].ToString());
            string a8 = trimcode(words[7].ToString());

            if (a1 != "")
            {
                inc = inc + 1;
            }
            if (a2 != "")
            {
                inc = inc + 1;
            }
            if (a3 != "")
            {
                inc = inc + 1;
            }
            if (a4 != "")
            {
                inc = inc + 1;
            }
            if (a5 != "")
            {
                inc = inc + 1;
            }
            if (a6 != "")
            {
                inc = inc + 1;
            }
            if (a7 != "")
            {
                inc = inc + 1;
            }
            if (a8 != "")
            {
                inc = inc + 1;
            }


            if (inc == 1)
            {
                str = "-";
            }
            if (inc == 2)
            {
                str = a1.ToString();
            }
            if (inc == 3)
            {
                str = a1.ToString() + "-" + a2.ToString();
            }
            if (inc == 4)
            {
                str = a1.ToString() + "-" + a2.ToString() + "-" + a3.ToString();
            }
            if (inc == 5)
            {
                str = a1.ToString() + "-" + a2.ToString() + "-" + a3.ToString() + "-" + a4.ToString();
            }
            if (inc == 6)
            {
                str = a1.ToString() + "-" + a2.ToString() + "-" + a3.ToString() + "-" + a4.ToString() + "-" + a5.ToString();
            }
            if (inc == 7)
            {
                str = a1.ToString() + "-" + a2.ToString() + "-" + a3.ToString() + "-" + a4.ToString() + "-" + a5.ToString() + "-" + a6.ToString();
            }
            if (inc == 8)
            {
                str = a1.ToString() + "-" + a2.ToString() + "-" + a3.ToString() + "-" + a4.ToString() + "-" + a5.ToString() + "-" + a6.ToString() + "-" + a7.ToString();
            }

            return str;
        }

        else if (noseg() == 9)
        {
            string h_D = lblaccout_code.Text;
            int inc = 0;
            string[] words = h_D.Split('-');

            string a1 = words[0].ToString();
            string a2 = trimcode(words[1].ToString());
            string a3 = trimcode(words[2].ToString());
            string a4 = trimcode(words[3].ToString());
            string a5 = trimcode(words[4].ToString());
            string a6 = trimcode(words[5].ToString());
            string a7 = trimcode(words[6].ToString());
            string a8 = trimcode(words[7].ToString());
            string a9 = trimcode(words[8].ToString());

            if (a1 != "")
            {
                inc = inc + 1;
            }
            if (a2 != "")
            {
                inc = inc + 1;
            }
            if (a3 != "")
            {
                inc = inc + 1;
            }
            if (a4 != "")
            {
                inc = inc + 1;
            }
            if (a5 != "")
            {
                inc = inc + 1;
            }
            if (a6 != "")
            {
                inc = inc + 1;
            }
            if (a7 != "")
            {
                inc = inc + 1;
            }
            if (a8 != "")
            {
                inc = inc + 1;
            }
            if (a9 != "")
            {
                inc = inc + 1;
            }


            if (inc == 1)
            {
                str = "-";
            }
            if (inc == 2)
            {
                str = a1.ToString();
            }
            if (inc == 3)
            {
                str = a1.ToString() + "-" + a2.ToString();
            }
            if (inc == 4)
            {
                str = a1.ToString() + "-" + a2.ToString() + "-" + a3.ToString();
            }
            if (inc == 5)
            {
                str = a1.ToString() + "-" + a2.ToString() + "-" + a3.ToString() + "-" + a4.ToString();
            }
            if (inc == 6)
            {
                str = a1.ToString() + "-" + a2.ToString() + "-" + a3.ToString() + "-" + a4.ToString() + "-" + a5.ToString();
            }
            if (inc == 7)
            {
                str = a1.ToString() + "-" + a2.ToString() + "-" + a3.ToString() + "-" + a4.ToString() + "-" + a5.ToString() + "-" + a6.ToString();
            }
            if (inc == 8)
            {
                str = a1.ToString() + "-" + a2.ToString() + "-" + a3.ToString() + "-" + a4.ToString() + "-" + a5.ToString() + "-" + a6.ToString() + "-" + a7.ToString();
            }
            if (inc == 9)
            {
                str = a1.ToString() + "-" + a2.ToString() + "-" + a3.ToString() + "-" + a4.ToString() + "-" + a5.ToString() + "-" + a6.ToString() + "-" + a7.ToString() + "-" + a8.ToString();
            }

            return str;
        }


        else
        {
            return str = "";
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnInsert.Visible = false;
        btnCancel.Visible = true;
        btnUpdate.Visible = false;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnFind.Visible = true;
       
        textclear();

        fieldbox.Visible = true;
        LinklblGenerarte.Visible = false;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {

            lblserialNo.Text = GridView1.SelectedRow.Cells[1].Text;
            
            DataSet ds = dml.Find("select * from SET_COA_detail where COA_D_ID = '" + lblserialNo.Text + "'  and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblMasterID.Text = ds.Tables[0].Rows[0]["COA_M_ID"].ToString();

                masktemplate();
                ddlAcc_type.ClearSelection();
                ddlAcc_sub_type.ClearSelection();
                txtAccountDescrip.Text = ds.Tables[0].Rows[0]["Acct_Description"].ToString();
                ddlAcc_type.Items.FindByValue(ds.Tables[0].Rows[0]["Acct_Type_ID"].ToString()).Selected = true;
                ddlACCSUBFun();
                txtHead_Detail.Text = ds.Tables[0].Rows[0]["Head_detail_ID"].ToString();

                string zeronull = ds.Tables[0].Rows[0]["Acct_Sub_Type_Id"].ToString();
                if (zeronull == "")
                {
                    ddlAcc_sub_type.SelectedIndex = 0;
                }
                else {
                  
                    ddlAcc_sub_type.Items.FindByValue(zeronull).Selected = true;
                }
                DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["SysDate"].ToString());
                txtbaseAcc_Code.Text = ds.Tables[0].Rows[0]["Head_Acct_Code"].ToString();
                txtUserName.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["User"].ToString());

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                //  txtTrancstion_type.Text = ddlTran_Type();
                txtSystemDATE.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                txtAcoount_Code.Text = ds.Tables[0].Rows[0]["Acct_Code"].ToString();

                txtTrancstion_type.Text = ddlTran_Type();
                ddlAcc_sub_type.Enabled = false;

                ddlGOC.SelectedValue = ds.Tables[0].Rows[0]["GocId"].ToString();
                plBarCode.Visible = true;
                barcodegenrate();
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }

    }

    protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
    {
        textclear();
        btnInsert.Visible = false;
        btnCancel.Visible = true;
        btnUpdate.Visible = true;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnFind.Visible = false;

        Label1.Text = "";
        txtAcoount_Code.Enabled = false;
        txtbaseAcc_Code.Enabled = false;
        txtAccountDescrip.Enabled = true;
        ddlAcc_type.Enabled = true;
        ddlAcc_sub_type.Enabled = true;
        txtbaseAcc_Code.Enabled = true;
        txtTrancstion_type.Enabled = true;
        chkActive.Enabled = true;
        rowid.Visible = true;

        fieldbox.Visible = true;
        LinklblGenerarte.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;

        try
        {

            lblserialNo.Text = GridView3.SelectedRow.Cells[1].Text;
            DataSet ds = dml.Find("select * from SET_COA_detail where COA_D_ID = '" + lblserialNo.Text + "'  and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblMasterID.Text = ds.Tables[0].Rows[0]["COA_M_ID"].ToString();

                masktemplate();
                ddlAcc_type.ClearSelection();
                
                txtAccountDescrip.Text = ds.Tables[0].Rows[0]["Acct_Description"].ToString();
                ddlAcc_type.Items.FindByValue(ds.Tables[0].Rows[0]["Acct_Type_ID"].ToString()).Selected = true;
              
                txtHead_Detail.Text = ds.Tables[0].Rows[0]["Head_detail_ID"].ToString();

                string zeronull = ds.Tables[0].Rows[0]["Acct_Sub_Type_Id"].ToString();

               
                DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["SysDate"].ToString());
                txtbaseAcc_Code.Text = ds.Tables[0].Rows[0]["Head_Acct_Code"].ToString();
                txtUserName.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["User"].ToString());

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                //  txtTrancstion_type.Text = ddlTran_Type();
                txtSystemDATE.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                txtAcoount_Code.Text = ds.Tables[0].Rows[0]["Acct_Code"].ToString();

                txtTrancstion_type.Text = ddlTran_Type();
                //ddlAcc_sub_type.Enabled = false;
                if (zeronull == "")
                {
                    ddlAcc_sub_type.SelectedIndex = 0;
                }
                else {
                    ddlACCSUBFun();
                    ddlAcc_sub_type.ClearSelection();
                    ddlAcc_sub_type.Items.FindByValue(ds.Tables[0].Rows[0]["Acct_Sub_Type_Id"].ToString()).Selected = true;
                }
                ddlGOC.SelectedValue = ds.Tables[0].Rows[0]["GocId"].ToString();
                plBarCode.Visible = true;
                barcodegenrate();
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
        chkActive.Enabled = false;
        rowid.Visible = true;
        LinklblGenerarte.Visible = false;
        textclear();

        fieldbox.Visible = true;
        LinklblGenerarte.Visible = false;
        Findbox.Visible = false;
        Editbox.Visible = false;
        Deletebox.Visible = false;

        try
        {

            lblserialNo.Text = GridView2.SelectedRow.Cells[1].Text;
            DataSet ds = dml.Find("select * from SET_COA_detail where COA_D_ID = '" + lblserialNo.Text + "'  and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblMasterID.Text = ds.Tables[0].Rows[0]["COA_M_ID"].ToString();

                masktemplate();
                ddlAcc_type.ClearSelection();
                ddlAcc_sub_type.ClearSelection();
                txtAccountDescrip.Text = ds.Tables[0].Rows[0]["Acct_Description"].ToString();
                ddlAcc_type.Items.FindByValue(ds.Tables[0].Rows[0]["Acct_Type_ID"].ToString()).Selected = true;
                ddlACCSUBFun();
                txtHead_Detail.Text = ds.Tables[0].Rows[0]["Head_detail_ID"].ToString();

                string zeronull = ds.Tables[0].Rows[0]["Acct_Sub_Type_Id"].ToString();
                if (zeronull == "")
                {
                    ddlAcc_sub_type.SelectedIndex = 0;
                }
                else {
                    ddlAcc_sub_type.Items.FindByValue(zeronull).Selected = true;
                }
                DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["SysDate"].ToString());
                txtbaseAcc_Code.Text = ds.Tables[0].Rows[0]["Head_Acct_Code"].ToString();
                txtUserName.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["User"].ToString());

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                //  txtTrancstion_type.Text = ddlTran_Type();
                txtSystemDATE.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                txtAcoount_Code.Text = ds.Tables[0].Rows[0]["Acct_Code"].ToString();

                txtTrancstion_type.Text = ddlTran_Type();
                ddlAcc_sub_type.Enabled = false;

                ddlGOC.SelectedValue = ds.Tables[0].Rows[0]["GocId"].ToString();
                plBarCode.Visible = true;
                barcodegenrate();
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        txtHead_Detail.Text = headdetail();
    }

    public string ddlTran_Type()
    {
        txtTrancstion_type.Enabled = false;
        if (ddlAcc_type.SelectedIndex != 0)
        {
            DataSet ds_TranType = dml.Find("select Tran_Type from SET_Acct_Type where Acct_Type_Id = " + ddlAcc_type.SelectedItem.Value);
            ddlAcc_sub_type.Enabled = true;
            ddlACCSUBFun();
            return ds_TranType.Tables[0].Rows[0]["Tran_Type"].ToString();
        }
        else
        {
            ddlAcc_sub_type.Enabled = false;
            ddlAcc_sub_type.SelectedIndex = 0;
            return "";
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
    protected void LinklblGenerarte_Click(object sender, EventArgs e)
    {
        LinklblGenerarte.Visible = false;
        plBarCode.Visible = true;
        barcodegenrate();
    }
    public void newlogic()
    {
        userid = Request.QueryString["UserID"];
        string compid = "";
        string gocid = "";
        DataSet ds = dml.Find("select compid, GOCId,UseCompanyCOA from SET_Company where CompName='" + Request.Cookies["compNAme"].Value + "'");
        if (ds.Tables[0].Rows.Count > 0)
        {
            compid = ds.Tables[0].Rows[0]["compid"].ToString();
            gocid = ds.Tables[0].Rows[0]["GOCId"].ToString();
                    
        }
             
        DataSet ds1 = dml.Find("select * from SET_COA_Master where Goc_Comp_Coa = 0 and CompId = "+ compid+ " and GocId= "+gocid+" ");
        if (ds.Tables[0].Rows.Count > 0)
        {
            // Coa_Goc_Foramt = "Y";
            string comid = ds1.Tables[0].Rows[0]["CompId"].ToString();
            string gocid1= ds1.Tables[0].Rows[0]["GocId"].ToString();
            string COa = ds1.Tables[0].Rows[0]["Goc_Comp_Coa"].ToString();
           
            if(comid == compid && gocid1 == gocid && COa == "False")
            {
                Label1.Text = "defualt Entry";
            }
            else
            {

                Label1.Text = "New Entry";
            }
        }
        else
        {

            Label1.Text = "New Entry";
        }

    }

    public string combination()
    {

        userid = Request.QueryString["UserID"];
        string compid = "";
        string gocid = "";
        DataSet ds = dml.Find("select compid , GOCId,UseCompanyCOA from SET_Company where CompName='" + Request.Cookies["compNAme"].Value + "'");
        if (ds.Tables[0].Rows.Count > 0)
        {
            compid = ds.Tables[0].Rows[0]["compid"].ToString();
            gocid = ds.Tables[0].Rows[0]["GOCId"].ToString();

        }

        string coaforcomp = "";
        DataSet ds1 = dml.Find("select Goc_Comp_Coa from SET_COA_Master WHERE gocid = "+gocid+" and Compid = "+compid+" order by COA_M_ID DESC");
        if (ds1.Tables[0].Rows.Count > 0)
        {
            coaforcomp = ds1.Tables[0].Rows[0]["Goc_Comp_Coa"].ToString();
            return coaforcomp;  
        }
        else
        {
            return coaforcomp;
        }

        

      
    }

    protected int FindDate()
    {
        int count = 0;
        string StartdateFY = "", EndDateFY = "", fiscalid;
        string StartdateMaster = "", EndDateMaster = "";

        string fiscal = Request.QueryString.Get("fiscaly");
        DataSet ds = dml.Find("select FiscalYearID,StartDate,EndDate from SET_Fiscal_Year where Description = '" + fiscal + "'");
        if (ds.Tables[0].Rows.Count > 0)
        {
            // select Fiscal_Start_Dt_for_COA,Fiscal_End_Dt_for_COA,Goc_Comp_Coa from SET_COA_Master where FiscalYearID = '"++"' and gocid='" + gocid() + "' and compid = '" + compid() + "'


            StartdateFY = dml.dateconvertString(ds.Tables[0].Rows[0]["StartDate"].ToString());
            EndDateFY = dml.dateconvertString(ds.Tables[0].Rows[0]["EndDate"].ToString());
            fiscalid = ds.Tables[0].Rows[0]["FiscalYearID"].ToString();

            DataSet dsCoa = dml.Find("select Fiscal_Start_Dt_for_COA,Fiscal_End_Dt_for_COA,Goc_Comp_Coa from SET_COA_Master where FiscalYearID = '" + fiscalid + "' and gocid='" + gocid() + "' and compid = '" + compid() + "'");
            for (int i = 0; i <= dsCoa.Tables[0].Rows.Count - 1; i++)
            {
                StartdateMaster = dml.dateconvertString(dsCoa.Tables[0].Rows[i]["Fiscal_Start_Dt_for_COA"].ToString());
                EndDateMaster = dml.dateconvertString(dsCoa.Tables[0].Rows[i]["Fiscal_End_Dt_for_COA"].ToString());

                DateTime time = DateTime.Parse(StartdateFY);
                DateTime startDate = DateTime.Parse(StartdateMaster);
                DateTime endDate = DateTime.Parse(EndDateMaster);

                int time1 = int.Parse(time.Year.ToString());
                int startyear = int.Parse(startDate.Year.ToString());
                int endyear = int.Parse(endDate.Year.ToString());

                if (time1 >= startyear && time1 <= endyear)
                {
                    count = count + 1;
                    ViewState["comp_Goc"] = dsCoa.Tables[0].Rows[i]["Goc_Comp_Coa"].ToString();
                }
                else
                {
                    count = 0;
                }
            }
        }
        return count;
    }


    protected void txtEdit_AccountCode_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(txtEdit_AccountCode, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }

    protected void txtFind_AccountCode_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(txtFind_AccountCode, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }

    protected void txtDelete_AccountCode_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(txtDelete_AccountCode, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }

}