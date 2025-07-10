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
    int EditDays, DeleteDays, AddDays, DateFrom;
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
            fiscal = Request.QueryString["fiscaly"];
            
            dml.dropdownsql(ddlEdit_MenuTitle, "SET_Documents", "DocDescription", "DocID");
            dml.dropdownsql(ddlFind_MenuName, "SET_Documents", "DocDescription", "DocID");
            dml.dropdownsql(ddlDel_MenuName, "SET_Documents", "DocDescription", "DocID");
            txtEntryDate.Attributes.Add("readonly", "readonly");
            //select Sno,FormTitle from SET_Form
            //ddlBpType ddlactBP
            dml.dropdownsql(ddlBpType, "SET_BPartnerNature", "BPNatureDescription", "BPNatureID");
            dml.dropdownsql(ddlactBP, "SET_BPartnerNature", "BPNatureDescription", "BPNatureID");

            dml.dropdownsql(ddlReferDoc, "SET_Documents", "DocDescription", "DocID");
            dml.dropdownsql(ddllDocName, "SET_Documents", "DocDescription", "DocID");

            dml.dropdownsql(ddlFormName, "SET_Form", "FormTitle", "Sno");

            asccounttypeitem.Visible = false;
            DivBptype.Visible = false;
          
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

        ddllDocName.Enabled = true;
        rdb_Master.Enabled = true;
        rdb_Detail.Enabled = true;
        ddlReferDoc.Enabled = true;
        ddlFormName.Enabled = true;

        Radcmb_RefDocAcctCode.Enabled = true;
        ddlRefDocGlImapct.Enabled = true;
        DivBptype.Visible = false;
        ddlBpType.Enabled = true;

        ddlItemTypes.Enabled = true;

        radForceAccount.Enabled = true;

        ddlaccounttype.Enabled = true;

        chkDiff.Enabled = true;
        chkRefDocRule.Enabled = true;
        ddlGLImpact.Enabled = true;
        txtEntryDate.Enabled = true;
        imgPopup.Enabled = true;
        ddlInventoryImpact.Enabled = true;
        chkHide.Enabled = true;
        chkActive.Enabled = true;
        txtCreatedby.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdateBy.Enabled = false;
        txtUpdateDate.Enabled = false;
        Label1.Text = "";
        txtEntryDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");

        txtCreatedby.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdateBy.Enabled = true;
        txtUpdateDate.Enabled = true;

        
        chkActive.Checked = true;
       

        txtCreatedDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

        userid = Request.QueryString["UserID"];
        DataSet ds_autoUserName = dml.Find("select user_name from SET_User_Manager where UserId ='" + userid + "'");
        txtCreatedby.Text = ds_autoUserName.Tables[0].Rows[0]["user_name"].ToString();
        rdb_Master.Checked = true;
        rdb_Master_CheckedChanged(sender, e);

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (ddllDocName.SelectedIndex != 0)
        {
            string refdoc = "0";

            int chk = 0, hide = 0, refdocrule = 0;
            string mstDet = "D", type = "0", acct = "0", diff = "N";
            string InsertQuery = "Insert into Set_AcRules4Doc (";

            InsertQuery = InsertQuery + "[CompId],[ApplyDate],[DocId],[MasterDetail]";

            if (rdb_Master.Checked == true)
            {
                mstDet = "M";

                type = ddlBpType.SelectedItem.Text;
                acct = ddlactBP.SelectedItem.Text;

            }

            if (rdb_Detail.Checked == true)
            {

                mstDet = "D";

                type = ddlItemTypes.SelectedItem.Text;
                acct = ddlaccounttype.SelectedItem.Text;


            }
            if (ddlReferDoc.SelectedIndex != 0)
            {
                InsertQuery = InsertQuery + ",[ReferDocument]";
                refdoc = ddlReferDoc.SelectedItem.Value;
            }
            if (ddlFormName.SelectedIndex != 0)
            {
                InsertQuery = InsertQuery + ",[FormId_Sno]";
            }

            if (ddlBpType.SelectedIndex != 0)
            {
                InsertQuery = InsertQuery + ",[Type]";
            }
            if (ddlaccounttype.SelectedIndex != 0)
            {
                InsertQuery = InsertQuery + ",[AccountType]";
            }

            if (radForceAccount.Text != "")
            {
                InsertQuery = InsertQuery + ",[ForceAccountCode]";
            }

            if (chkRefDocRule.Checked == true)
            {
                InsertQuery = InsertQuery + ",[RefDocRules]";
                refdocrule = 1;
            }

            if (ddlGLImpact.SelectedIndex != 0)
            {
                InsertQuery = InsertQuery + ",[GlImpact]";
            }

            if (ddlInventoryImpact.SelectedIndex != 0)
            {
                InsertQuery = InsertQuery + ",[InventoryImpact]";
            }
            InsertQuery = InsertQuery + ",[IsHide],[IsActive],[CreatedBy],[CreateDate],[Record_Deleted],[Diff]";
            if (Radcmb_RefDocAcctCode.Text != "")
            {
                InsertQuery = InsertQuery + ",[RefDocAcctCode]";
            }

            if (ddlRefDocGlImapct.SelectedIndex != 0)
            {
                InsertQuery = InsertQuery + ",[RefDocGlImpact]";
            }

            InsertQuery = InsertQuery + ") Values (" + compid() + ",'" + dml.dateconvertforinsert(txtEntryDate) + "','" + ddllDocName.SelectedItem.Value + "','" + mstDet + "','";

            if (ddlReferDoc.SelectedIndex != 0)
            {
                refdoc = ddlReferDoc.SelectedItem.Value;
                InsertQuery = InsertQuery + refdoc + "','";
            }
            if (ddlFormName.SelectedIndex != 0)
            {
                InsertQuery = InsertQuery + ddlFormName.SelectedItem.Value + "','";
            }

            if (ddlBpType.SelectedIndex != 0)
            {
                InsertQuery = InsertQuery + ddlBpType.SelectedItem.Value + "','";
            }
            if (ddlaccounttype.SelectedIndex != 0)
            {
                InsertQuery = InsertQuery + ddlaccounttype.SelectedItem.Value + "','";
            }

            if (radForceAccount.Text != "")
            {
                InsertQuery = InsertQuery + radForceAccount.Text + "','";
            }

            if (chkRefDocRule.Checked == true)
            {
                InsertQuery = InsertQuery + 1 + "','";

            }
            else
            {
                InsertQuery = InsertQuery + 0 + "','";
            }
            if (ddlGLImpact.SelectedIndex != 0)
            {
                InsertQuery = InsertQuery + ddlGLImpact.SelectedItem.Value + "','";
            }

            if (ddlInventoryImpact.SelectedIndex != 0)
            {
                InsertQuery = InsertQuery + ddlInventoryImpact.SelectedItem.Value + "','";
            }


            if (chkActive.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
            if (chkHide.Checked == true)
            {
                hide = 1;
            }
            else
            {
                hide = 0;
            }

            InsertQuery = InsertQuery + hide + "','" + chk + "','" + Request.QueryString["UserID"] + "','" + DateTime.UtcNow + "','" + 0 + "',";


            if (chkDiff.Checked == true)
            {
                diff = "Y";
            }
            else
            {
                diff = "N";
            }
            InsertQuery = InsertQuery + "'" + diff + "'";


            if (Radcmb_RefDocAcctCode.Text != "")
            {
                InsertQuery = InsertQuery + ",'" + Radcmb_RefDocAcctCode.Text + "'";
            }

            if (ddlRefDocGlImapct.SelectedIndex != 0)
            {
                InsertQuery = InsertQuery + ",'" + ddlRefDocGlImapct.SelectedItem.Value + "'";
            }
            InsertQuery = InsertQuery + ")";
            DataSet ds1 = dml.Find("select * from SET_ItemType where ItemTypeID in (select ItemTypeID from SET_ItemMaster where ItemTypeID=1)");




            //string Query= "INSERT INTO  SET_AcRules4Doc ([CompID], [ApplyDate], [DocId], [MasterDetail], [ReferDocument], [FormId_Sno],[Type], [AccountType], [ForceAccountCode], [RefDocRules], [GLImpact], [InventoryImpact], [IsHide],[IsActive], [CreatedBy], [CreateDate], [Record_Deleted],[diff], [RefDocAcctCode], [RefDocGLImpact]) VALUES (" + compid() + ", '" + dml.dateconvertforinsert(txtEntryDate) + "', '" + ddllDocName.SelectedItem.Value + "', '" + mstDet + "', '" + refdoc + "', '" + ddlFormName.SelectedItem.Value + "', '" + type + "', '" + acct + "', '" + radForceAccount.Text + "', '" + refdocrule + "', '" + ddlGLImpact.SelectedItem.Text + "', '" + ddlInventoryImpact.SelectedItem.Text + "', '" + hide + "', '" + chk + "', '" + show_username() + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "','0','" + diff + "','" + Radcmb_RefDocAcctCode.Text + "','" + ddlRefDocGlImapct.SelectedItem.Text + "')";

            dml.Insert(InsertQuery, "");
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);

            Label1.Text = "";

            chkActive.Checked = false;

            txtCreatedby.Text = show_username();
            txtCreatedDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");


            chkActive.Checked = true;
            //}
            Label1.Text = "";
        }
        else {

            Label1.Text = "Selecting document is essential";
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int chk = 0, hide = 0,  refdocrule = 0;
        string mstDet = "D", type = "0", acct = "0", diff= "N";
        if (chkActive.Checked == true)
        {
            chk = 1;
        }
        else
        {
            chk = 0;
        }
        if (chkHide.Checked == true)
        {
            hide = 1;
        }
        else
        {
            hide = 0;
        }

        if (rdb_Master.Checked == true)
        {
            mstDet = "M";

            type = ddlBpType.SelectedItem.Text;
            acct = ddlactBP.SelectedItem.Text;

        }

        if (rdb_Detail.Checked == true)
        {

            mstDet = "D";

            type = ddlItemTypes.SelectedItem.Text;
            acct = ddlaccounttype.SelectedItem.Text;


        }


        
        if (chkRefDocRule.Checked == true)
        {
            refdocrule = 1;
        }

        if (chkDiff.Checked == true)
        {
            diff = "Y";
        }

        DataSet ds_up = dml.Find("select * from SET_AcRules4Doc where ([CompID]="+compid()+") AND ([ApplyDate]='"+txtEntryDate.Text+"') AND ([DocId]='"+ddllDocName.SelectedItem.Value+"') AND ([MasterDetail]='"+mstDet+"') AND ([ReferDocument]='"+ddlReferDoc.SelectedItem.Value +"') AND ([FormId_Sno]='"+ddlFormName.SelectedItem.Value+"') AND ([Type]='"+type+"') AND ([AccountType]='"+ acct+"') AND ([ForceAccountCode]='"+radForceAccount.Text+"') AND ([RefDocRules]='"+refdocrule+"') AND ([GLImpact]='"+ddlGLImpact.SelectedItem.Text+"') AND ([InventoryImpact]='"+ddlInventoryImpact.SelectedItem.Text+"') AND ([IsHide]='"+hide+"') AND ([IsActive]='"+chk+ "')  AND ([Record_Deleted]='0') AND ([diff]='"+ diff + "') AND ([RefDocAcctCode]='" + Radcmb_RefDocAcctCode.Text + "') AND ([RefDocGLImpact]='" + ddlRefDocGlImapct.SelectedItem.Text+ "')");

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



            dml.Update("UPDATE SET_AcRules4Doc SET  [CompID]=" + compid() + ", [ApplyDate]='" + txtEntryDate.Text + "', [DocId]='" + ddllDocName.SelectedItem.Value + "', [MasterDetail]='" + mstDet + "', [ReferDocument]='" + ddlReferDoc.SelectedItem.Value + "', [FormId_Sno]='" + ddlFormName.SelectedItem.Value + "', [Type]='" + type + "', [AccountType]='" + acct+ "', [ForceAccountCode]='" + radForceAccount.Text + "', [RefDocRules]='" + refdocrule + "', [GLImpact]='" + ddlGLImpact.SelectedItem.Text + "', [InventoryImpact]='" + ddlInventoryImpact.SelectedItem.Text + "', [IsHide]='" + hide + "', [IsActive]='" + chk + "', [UpdatedBy]='" + show_username() + "',[diff]='"+ diff + "',[RefDocAcctCode]='"+Radcmb_RefDocAcctCode.Text+"',[RefDocGLImpact] = '"+ddlRefDocGlImapct.SelectedItem.Text+"' ,[UpdateDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "' WHERE ([Sno]='" + ViewState["SNO"].ToString() +"')", "");
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
            string squer = "select * from View_AcctRule4doc";


            if (ddlDel_MenuName.SelectedIndex != 0)
            {
                swhere = "DocDescription = '" + ddlDel_MenuName.SelectedItem.Text + "'";
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

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and CompId = '" + compid() + "' ORDER BY DocDescription";

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
            string squer = "select * from View_AcctRule4doc";
            //SelectCommand="SELECT [Sno], [ApplyDate], [DocDescription], [MasterDetail], [refDocName], [FormTitle], [Type], [AccountType], [RefDocRules], [GLImpact], [InventoryImpact] FROM [View_AcctRule4doc]"></asp:SqlDataSource>

            if (ddlFind_MenuName.SelectedIndex != 0)
            {
                swhere = "DocDescription = '" + ddlFind_MenuName.SelectedItem.Text + "'";
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

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and CompId = '" + compid() + "' ORDER BY DocDescription";

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
            string squer = "select * from View_AcctRule4doc";


            if (ddlEdit_MenuTitle.SelectedIndex != 0)
            {
                swhere = "DocDescription = '" + ddlEdit_MenuTitle.SelectedItem.Text + "'";
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

            squer = squer + " where " + swhere + " and Record_Deleted = 0 and CompId = '"+compid()+ "'  ORDER BY DocDescription";

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


        ddllDocName.SelectedIndex = 0;
        rdb_Master.Checked = false;
        rdb_Detail.Checked = false;
        txtEntryDate.Text = "";
        ddlReferDoc.SelectedIndex = 0;
        ddlFormName.SelectedIndex = 0;

        ddlBpType.SelectedIndex = 0;
        //rdb_IsExpense.Checked = false;
        //rdb_IsConsumable.Checked = false;
        //rdb_IsAsset.Checked = false;

        ddlItemTypes.SelectedIndex = 0;

        radForceAccount.Text = "";
        ddlaccounttype.SelectedIndex = 0;
        chkRefDocRule.Checked = false;
        ddlGLImpact.SelectedIndex = 0;

        ddlInventoryImpact.SelectedIndex = 0;
        chkHide.Checked = false;
        chkActive.Checked = false;
        txtCreatedby.Text = "";
        txtCreatedDate.Text = "";
        ddlactBP.Enabled = false;

        chkDiff.Enabled = false;



        Radcmb_RefDocAcctCode.Text = "";
        ddlRefDocGlImapct.SelectedIndex = 0;


        txtCreatedby.Text = "";
        txtCreatedDate.Text = "";
        txtUpdateBy.Text = "";
        txtUpdateDate.Text = "";


        Radcmb_RefDocAcctCode.Enabled = false;
        ddlRefDocGlImapct.Enabled = false;
        ddllDocName.Enabled = false;
        rdb_Master.Enabled = false;
        rdb_Detail.Enabled = false;
        ddlReferDoc.Enabled = false;
        ddlFormName.Enabled = false;
        ddlBpType.Enabled = false;

        //rdb_IsExpense.Enabled = false;
        //rdb_IsConsumable.Enabled = false;
        //rdb_IsAsset.Enabled = false;

        ddlItemTypes.Enabled = false;


        radForceAccount.Enabled = false;
        ddlaccounttype.Enabled = false;
        chkRefDocRule.Enabled = false;
        ddlGLImpact.Enabled = false;
        txtEntryDate.Enabled = false;
        imgPopup.Enabled = false;
        ddlInventoryImpact.Enabled = false;
        chkHide.Enabled = false;
        chkActive.Enabled = false;
        txtCreatedby.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdateBy.Enabled = false;
        txtUpdateDate.Enabled = false;
        Label1.Text = "";




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


                dml.Delete("update SET_AcRules4Doc set Record_Deleted = 1 where Sno = " + ViewState["SNO"].ToString() + "", "");
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



            DataSet ds = dml.Find("select * from SET_AcRules4Doc WHERE ([Sno]='" + serial_id+"') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddllDocName.ClearSelection();
                ddlReferDoc.ClearSelection();
                ddlFormName.ClearSelection();
                ddlGLImpact.ClearSelection();
                ddlInventoryImpact.ClearSelection();
                ddlaccounttype.ClearSelection();
                ddlRefDocGlImapct.ClearSelection();

                ddllDocName.Items.FindByValue(ds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;
                ddlReferDoc.Items.FindByValue(ds.Tables[0].Rows[0]["ReferDocument"].ToString()).Selected = true;
                ddlFormName.Items.FindByValue(ds.Tables[0].Rows[0]["FormId_Sno"].ToString()).Selected = true;
                ddlGLImpact.Items.FindByText(ds.Tables[0].Rows[0]["GLImpact"].ToString()).Selected = true;
                ddlInventoryImpact.Items.FindByText(ds.Tables[0].Rows[0]["InventoryImpact"].ToString()).Selected = true;

                if (ddlRefDocGlImapct.Items.FindByText(ds.Tables[0].Rows[0]["RefDocGLImpact"].ToString()) != null)
                {
                    ddlRefDocGlImapct.Items.FindByText(ds.Tables[0].Rows[0]["RefDocGLImpact"].ToString()).Selected = true;
                }


                Radcmb_RefDocAcctCode.Text = ds.Tables[0].Rows[0]["RefDocAcctCode"].ToString();
                string MasterDetail = (ds.Tables[0].Rows[0]["MasterDetail"].ToString());
                string type = (ds.Tables[0].Rows[0]["Type"].ToString());
                string accttype = (ds.Tables[0].Rows[0]["AccountType"].ToString());

                string diff = (ds.Tables[0].Rows[0]["diff"].ToString());
                if (diff == "Y")
                {
                    chkDiff.Checked = true;
                }
                else
                {
                    chkDiff.Checked = false;
                }
                if (MasterDetail == "M")
                {
                    rdb_Master.Checked = true;
                    rdb_Detail.Checked = false;


                    ddlBpType.ClearSelection();
                    ddlactBP.ClearSelection();

                    DivBptype.Visible = true;
                    accounttypeBP.Visible = true;
                    divitemtypes.Visible = false;
                    asccounttypeitem.Visible = false;

                    ddlBpType.Items.FindByText(type).Selected = true;
                    ddlactBP.Items.FindByText(accttype).Selected = true;

                }
                else
                {
                    rdb_Master.Checked = false;
                    rdb_Detail.Checked = true;

                    DivBptype.Visible = false;
                    accounttypeBP.Visible = false;
                    divitemtypes.Visible = true;
                    asccounttypeitem.Visible = true;

                    ddlItemTypes.ClearSelection();
                    ddlaccounttype.ClearSelection();

                    ddlItemTypes.Items.FindByText(type).Selected = true;
                    ddlaccounttype.Items.FindByText(accttype).Selected = true;
                }

                string Type = (ds.Tables[0].Rows[0]["Type"].ToString());




                radForceAccount.Text = ds.Tables[0].Rows[0]["ForceAccountCode"].ToString();
                // ddlaccounttype.Items.FindByText(ds.Tables[0].Rows[0]["AccountType"].ToString()).Selected = true;

                bool refdocrule = bool.Parse(ds.Tables[0].Rows[0]["RefDocRules"].ToString());
                if (refdocrule == true)
                {
                    chkRefDocRule.Checked = true;
                }
                else
                {
                    chkRefDocRule.Checked = true;
                }

                txtEntryDate.Text = ds.Tables[0].Rows[0]["ApplyDate"].ToString(); ;


                bool hide = bool.Parse(ds.Tables[0].Rows[0]["IsHide"].ToString());
                if (hide == true)
                {
                    chkHide.Checked = true;
                }
                else
                {
                    chkHide.Checked = false;
                }

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = true;
                }



                txtCreatedby.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdateBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());


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

                dml.dateConvert(txtEntryDate);


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



            DataSet ds = dml.Find("select * from SET_AcRules4Doc WHERE ([Sno]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddllDocName.ClearSelection();
                ddlReferDoc.ClearSelection();
                ddlFormName.ClearSelection();
                ddlGLImpact.ClearSelection();
                ddlInventoryImpact.ClearSelection();
                ddlaccounttype.ClearSelection();
                ddlRefDocGlImapct.ClearSelection();
                ddllDocName.Items.FindByValue(ds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;
                ddlReferDoc.Items.FindByValue(ds.Tables[0].Rows[0]["ReferDocument"].ToString()).Selected = true;
                ddlFormName.Items.FindByValue(ds.Tables[0].Rows[0]["FormId_Sno"].ToString()).Selected = true;
                ddlGLImpact.Items.FindByText(ds.Tables[0].Rows[0]["GLImpact"].ToString()).Selected = true;
                ddlInventoryImpact.Items.FindByText(ds.Tables[0].Rows[0]["InventoryImpact"].ToString()).Selected = true;
               // ddlRefDocGlImapct.Items.FindByText(ds.Tables[0].Rows[0]["RefDocGLImpact"].ToString()).Selected = true;
                Radcmb_RefDocAcctCode.Text = ds.Tables[0].Rows[0]["RefDocAcctCode"].ToString();
                string MasterDetail = (ds.Tables[0].Rows[0]["MasterDetail"].ToString());
                string type = (ds.Tables[0].Rows[0]["Type"].ToString());
                string accttype = (ds.Tables[0].Rows[0]["AccountType"].ToString());

                string diff = (ds.Tables[0].Rows[0]["diff"].ToString());
                if (diff == "Y")
                {
                    chkDiff.Checked = true;
                }
                else
                {
                    chkDiff.Checked = false;
                }
                if (MasterDetail == "M")
                {
                    rdb_Master.Checked = true;
                    rdb_Detail.Checked = false;


                    ddlBpType.ClearSelection();
                    ddlactBP.ClearSelection();

                    DivBptype.Visible = true;
                    accounttypeBP.Visible = true;
                    divitemtypes.Visible = false;
                    asccounttypeitem.Visible = false;

                    ddlBpType.Items.FindByText(type).Selected = true;
                    ddlactBP.Items.FindByText(accttype).Selected = true;

                }
                else
                {
                    rdb_Master.Checked = false;
                    rdb_Detail.Checked = true;

                    DivBptype.Visible = false;
                    accounttypeBP.Visible = false;
                    divitemtypes.Visible = true;
                    asccounttypeitem.Visible = true;

                    ddlItemTypes.ClearSelection();
                    ddlaccounttype.ClearSelection();

                    ddlItemTypes.Items.FindByText(type).Selected = true;
                    ddlaccounttype.Items.FindByText(accttype).Selected = true;
                }

                string Type = (ds.Tables[0].Rows[0]["Type"].ToString());




                radForceAccount.Text = ds.Tables[0].Rows[0]["ForceAccountCode"].ToString();
                // ddlaccounttype.Items.FindByText(ds.Tables[0].Rows[0]["AccountType"].ToString()).Selected = true;

                bool refdocrule = bool.Parse(ds.Tables[0].Rows[0]["RefDocRules"].ToString());
                if (refdocrule == true)
                {
                    chkRefDocRule.Checked = true;
                }
                else
                {
                    chkRefDocRule.Checked = true;
                }

                txtEntryDate.Text = ds.Tables[0].Rows[0]["ApplyDate"].ToString(); ;


                bool hide = bool.Parse(ds.Tables[0].Rows[0]["IsHide"].ToString());
                if (hide == true)
                {
                    chkHide.Checked = true;
                }
                else
                {
                    chkHide.Checked = false;
                }

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = true;
                }



                txtCreatedby.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdateBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());


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

                dml.dateConvert(txtEntryDate);


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


        ddllDocName.Enabled = true;
        rdb_Master.Enabled = true;
        rdb_Detail.Enabled = true;
        ddlReferDoc.Enabled = true;
        ddlFormName.Enabled = true;
        //rdb_BPartnerType.Enabled = true;
        //rdb_ItemType.Enabled = true;
        radForceAccount.Enabled = true;
        ddlaccounttype.Enabled = true;
        chkRefDocRule.Enabled = true;
        ddlGLImpact.Enabled = true;
        txtEntryDate.Enabled = true;
        imgPopup.Enabled = true;
        ddlInventoryImpact.Enabled = true;
        chkHide.Enabled = true;
        chkActive.Enabled = true;
        chkDiff.Enabled = true;
        ddlRefDocGlImapct.Enabled = true;
        //rdb_IsAsset.Enabled = true;
        //rdb_IsConsumable.Enabled = true;
        //rdb_IsExpense.Enabled = true;
        Radcmb_RefDocAcctCode.Enabled = true;
        ddlItemTypes.Enabled = true;
        ddlaccounttype.Enabled = true;
        ddlactBP.Enabled = true;
        ddlBpType.Enabled = true;

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



            DataSet ds = dml.Find("select * from SET_AcRules4Doc WHERE ([Sno]='" + serial_id + "') and Record_Deleted = 0 AND Sort by Sno");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddllDocName.ClearSelection();
                ddlReferDoc.ClearSelection();
                ddlFormName.ClearSelection();
                ddlGLImpact.ClearSelection();
                ddlInventoryImpact.ClearSelection();
                ddlaccounttype.ClearSelection();
                ddlRefDocGlImapct.ClearSelection();
                ddllDocName.Items.FindByValue(ds.Tables[0].Rows[0]["DocId"].ToString()).Selected = true;
                ddlReferDoc.Items.FindByValue(ds.Tables[0].Rows[0]["ReferDocument"].ToString()).Selected = true;
                ddlFormName.Items.FindByValue(ds.Tables[0].Rows[0]["FormId_Sno"].ToString()).Selected = true;
                ddlGLImpact.Items.FindByText(ds.Tables[0].Rows[0]["GLImpact"].ToString()).Selected = true;
                ddlInventoryImpact.Items.FindByText(ds.Tables[0].Rows[0]["InventoryImpact"].ToString()).Selected = true;

               // ddlRefDocGlImapct.Items.FindByText(ds.Tables[0].Rows[0]["RefDocGLImpact"].ToString()).Selected = true;
                Radcmb_RefDocAcctCode.Text = ds.Tables[0].Rows[0]["RefDocAcctCode"].ToString();

                string MasterDetail = (ds.Tables[0].Rows[0]["MasterDetail"].ToString());
                string type = (ds.Tables[0].Rows[0]["Type"].ToString());
                string accttype = (ds.Tables[0].Rows[0]["AccountType"].ToString());

                string diff = (ds.Tables[0].Rows[0]["diff"].ToString());
                if (diff == "Y")
                {
                    chkDiff.Checked = true;
                }
                else
                {
                    chkDiff.Checked = false;
                }
                if (MasterDetail == "M")
                {
                    rdb_Master.Checked = true;
                    rdb_Detail.Checked = false;
                    

                    ddlBpType.ClearSelection();
                    ddlactBP.ClearSelection();

                    DivBptype.Visible = true;
                    accounttypeBP.Visible = true;
                 
                    divitemtypes.Visible = false;
                    asccounttypeitem.Visible = false;

                    ddlBpType.Items.FindByText(type).Selected = true;
                    ddlactBP.Items.FindByText(accttype).Selected = true;
                    
                }
                else
                {
                    rdb_Master.Checked = false;
                    rdb_Detail.Checked = true;

                    DivBptype.Visible = false;
                    accounttypeBP.Visible = false;
                    divitemtypes.Visible = true;
                    asccounttypeitem.Visible = true;

                    ddlItemTypes.ClearSelection();
                    ddlaccounttype.ClearSelection();

                    ddlItemTypes.Items.FindByText(type).Selected = true;
                    ddlaccounttype.Items.FindByText(accttype).Selected = true;
                }

                string Type = (ds.Tables[0].Rows[0]["Type"].ToString());




                radForceAccount.Text = ds.Tables[0].Rows[0]["ForceAccountCode"].ToString();
               // ddlaccounttype.Items.FindByText(ds.Tables[0].Rows[0]["AccountType"].ToString()).Selected = true;

                bool refdocrule = bool.Parse(ds.Tables[0].Rows[0]["RefDocRules"].ToString());
                if (refdocrule == true)
                {
                    chkRefDocRule.Checked = true;
                }
                else
                {
                    chkRefDocRule.Checked = true;
                }

                txtEntryDate.Text = ds.Tables[0].Rows[0]["ApplyDate"].ToString(); ;


                bool hide = bool.Parse(ds.Tables[0].Rows[0]["IsHide"].ToString());
                if (hide == true)
                {
                    chkHide.Checked = true;
                }
                else
                {
                    chkHide.Checked = false;
                }

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = true;
                }



                txtCreatedby.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdateBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());


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

                dml.dateConvert(txtEntryDate);
                
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


    //protected void radAccountCode_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    //{
    //    string where = "Record_Deleted = '0'";

    //    cmb.serachcombo4(radAccountCode, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    //}
    protected void radForceAccount_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(radForceAccount, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }

    protected void Radcmb_RefDocAcctCode_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "Record_Deleted = '0'";

        cmb.serachcombo4(Radcmb_RefDocAcctCode, e.Text, "Acct_Code", "COA_D_ID", "Acct_Description", "Acct_Type_Name", "Tran_Type", "view_Search_Acct_Code", where, "Acct_Code");

    }
    protected void ddlFormName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFormName.SelectedItem.Text == "Business Partner")
        {
            DivBptype.Visible = true;
            ddlBpType.Enabled = true;
        }
        else { 
            ddlBpType.Enabled = false;
        }
        //if (ddlFormName.SelectedItem.Text == "Item Type")
        //{
        //    DivBptype.Visible = false;

        //}
    }

    protected void rdb_Detail_CheckedChanged(object sender, EventArgs e)
    {

        ddlItemTypes.Enabled = true;
        ddlaccounttype.Enabled = true;

        ddlBpType.Enabled = false;
        ddlactBP.Enabled = false;

        divitemtypes.Visible = true;
        DivBptype.Visible = false;
        asccounttypeitem.Visible = true;
        accounttypeBP.Visible = false;

    }

    protected void rdb_Master_CheckedChanged(object sender, EventArgs e)
    {
        ddlBpType.Enabled = true;
        ddlactBP.Enabled = true;

        ddlItemTypes.Enabled = false;
      
        divitemtypes.Visible = false;
        DivBptype.Visible = true;

        asccounttypeitem.Visible = false;
        accounttypeBP.Visible = true;


    }

    protected void ddlReferDoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReferDoc.SelectedIndex != 0)
        {
            DataSet ds = dml.Find("select AccountCode from SET_Documents where DocID = '"+ddlReferDoc.SelectedItem.Value+"'");
            if(ds.Tables[0].Rows.Count> 0)
            {
                Radcmb_RefDocAcctCode.Text = ds.Tables[0].Rows[0]["AccountCode"].ToString();
            }
        }
                 
    }
}