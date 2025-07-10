using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class frm_SetGOC : System.Web.UI.Page
{
    int DateFrom, AddDays, EditDays, DeleteDays;
    DmlOperation dml = new DmlOperation();
    TextBox S_userid, S_LoginName, S_Displayarea;
    TextBox txtuser;
    FieldHide fd = new FieldHide();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
    DataSet ds = new DataSet();
    string userid, UserGrpID, FormID;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Page.IsPostBack == false)
        {
            CalendarExtender1.EndDate = DateTime.Now;
            btnUpdate.Visible = false;
            btnSave.Visible = false;
            btnDelete_after.Visible = false;
            userid = Request.QueryString["UserID"];
            UserGrpID = Request.QueryString["UsergrpID"];
            FormID = Request.QueryString["FormID"];
            fd.HideUSerGrpField(Page.Controls, UserGrpID, FormID, "SET_GOC");
            dml.dropdownsql(ddlCountry, "SET_Country", "CountryName", "CountryID");
            dml.dropdownsqlwithquery(ddlGrpGOC, "select gocid, GOCName from SET_GOC where GocId in (select DISTINCT GocId from SET_User_Permission_CompBrYear where UserId like '6%' and Record_Deleted = 0 and IsActive = 1 )", "GOCName", "gocid");


            dml.dropdownsql(txtFind_GOC, "SET_GOC", "GOCName", "GocId");
            dml.dropdownsql(txtEdit_GOCName, "SET_GOC", "GOCName", "GocId");
            dml.dropdownsql(txtDelete_GOC, "SET_GOC", "GOCName", "GocId");
            textClear();
            Findbox.Visible = false;
            DeleteBox.Visible = false;
            Editbox.Visible = false;
            Showall_Dml();
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


        chkActive.Enabled = true;
        txtGOCName.Enabled = true;
       // lblUserId.Enabled = true;
        txtRegisteredOffice.Enabled = true;
        txtIncorporationDate.Enabled = true;
        txtNoofSegments.Enabled = true;
        txtAccountSepartor.Enabled = true;
        txtSegment_0_Size.Enabled = true;
        txtSegment_1_Size.Enabled = true;
        txtSegment_2_Size.Enabled = true;
        txtSegment_3_Size.Enabled = true;
        txtSegment_4_Size.Enabled = true;
        txtSegment_5_Size.Enabled = true;
        txtSegment_6_Size.Enabled = true;
        txtSegment_7_Size.Enabled = true;
        txtSegment_8_Size.Enabled = true;
        txtSegment_9_Size.Enabled = true;
        ddlGrpGOC.Enabled = true;
        lblSerialNo.Enabled = true;
        txtCOA_Format.Enabled = true;
         rdb_DIFFCoaBranch_Y.Enabled = true;
        rdb_DIFFCoaBranch_N.Enabled = true;
        txtNo_of_Branches.Enabled = true;
        txtUser_Name.Enabled = true;
        txtEntry_Date.Enabled = true;
        txtSystem_Date.Enabled = true;
        ddlCity.Enabled = true;
        ddlCountry.Enabled = true;
        chkActive.Checked = true;
        rdb_DIFFCoaBranch_Y.Checked = true;
        ImageButton1.Enabled = true;
        ImageButton2.Enabled = true;

        txtSystem_Date.Enabled = false;
        txtUser_Name.Enabled = false;
        txtUser_Name.Text = show_username();
        txtSystem_Date.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");


    }
    public string show_username()
    {
        con.Close();
        con.Open();
        userid = Request.QueryString["UserID"];
        string Query="Select user_name from Set_User_Manager where UserId ='"+userid+"'";
        SqlDataAdapter UserAdapter = new SqlDataAdapter(Query,con);
        DataSet user = new DataSet();
        UserAdapter.Fill(user);
        con.Close();
     return user.Tables[0].Rows[0]["user_name"].ToString();
    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        textClear();
        string userid = Request.QueryString["UserID"];
        btnCancel.Visible = true;
        btnFind.Visible = true;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnInsert.Visible = false;

        txtGOCName.Enabled = false;
        txtRegisteredOffice.Enabled = false;
        txtIncorporationDate.Enabled = false;
        txtNoofSegments.Enabled = false;
        txtAccountSepartor.Enabled = false;
        txtSegment_0_Size.Enabled = false;
        txtSegment_1_Size.Enabled = false;
        txtSegment_2_Size.Enabled = false;
        txtSegment_3_Size.Enabled = false;
        txtSegment_4_Size.Enabled = false;
        txtSegment_5_Size.Enabled = false;
        txtSegment_6_Size.Enabled = false;
        txtSegment_7_Size.Enabled = false;
        txtSegment_8_Size.Enabled = false;
        txtSegment_9_Size.Enabled = false;
        lblSerialNo.Enabled = false;
        txtCOA_Format.Enabled = false;
        rdb_DIFFCoaBranch_N.Enabled = false;
        rdb_DIFFCoaBranch_Y.Enabled = false;

        
        txtNo_of_Branches.Enabled = false;
        txtUser_Name.Enabled = false;
        txtEntry_Date.Enabled = false;
        txtSystem_Date.Enabled = false;
        ddlCity.Enabled = false;
        ddlCountry.Enabled = false;
        chkActive.Checked = false;

        Findbox.Visible = true;
        fieldbox.Visible = false;


        try {
            string squer = "select * from SET_GOC ";
            string swhere;

            if (txtFind_GOC.SelectedIndex != 0)
            {
                swhere = "GOCName =  '" + txtFind_GOC.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "GOCName is not null";
            }
            if (txtFind_COASEg.Text != "")
            {
                swhere = swhere + " and NoofCoaSegments = '" + txtFind_COASEg.Text + "'";
            }
            else
            {
                swhere = swhere + " and NoofCoaSegments is not null";
            }
            if (txtFind_ED.Text != "")
            {
                swhere = swhere + " and EntryDate = '" + txtFind_ED.Text + "'";
            }
            else
            {
                swhere = swhere + " and EntryDate is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0 and gocid= '" + gocid() +"' ORDER BY GOCName";

            Findbox.Visible = true;
            fieldbox.Visible = false;

            DataSet dgrid = dml.grid(squer);
            if (dgrid.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = dgrid;
                GridView1.DataBind();
            }
            else
            {
                textClear();

                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", " nodatafound()", true);
            }

        }
        catch(Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", " wrong()", true);
        }
    }
    public int gocid()
    {
        string gocid = "";
        try
        {
            DataSet ds = dml.Find("select GOCId from SET_Company where CompName='" + Request.Cookies["compNAme"].Value + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                string id = gocid = ds.Tables[0].Rows[0]["GOCId"].ToString();
                return int.Parse(id);
            }
            else
            {
                return 0;
            }
        }
        catch (Exception ex)
        {
            return 0;
            Label1.Text = ex.Message;
        }
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
            string squer = "select * from SET_GOC ";
            string swhere;

            if (txtDelete_GOC.SelectedIndex != 0)
            {
                swhere = "GOCName =  '" + txtDelete_GOC.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "GOCName is not null";
            }
            if (txtDelete_ED.Text != "")
            {
                swhere = swhere + " and EntryDate = '" + txtDelete_ED.Text + "'";
            }
            else
            {
                swhere = swhere + " and EntryDate is not null";
            }
            if (txtDelete_COASeg.Text != "")
            {
                swhere = swhere + " and NoofCoaSegments = '" + txtDelete_COASeg.Text + "'";
            }
            else
            {
                swhere = swhere + " and NoofCoaSegments is not null";
            }
            if (ChkDelete_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (ChkDelete_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }
            else
            {
                swhere = swhere + " and IsActive is not null";
            }
            squer = squer + " where " + swhere + " and Record_Deleted = 0  and gocid= '" + gocid() + "' ORDER BY GOCName";

            Findbox.Visible = false;
            fieldbox.Visible = false;
            Editbox.Visible = false;

            DataSet dgrid = dml.grid(squer);
            GridView2.DataSource = dgrid;
            GridView2.DataBind();
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        txtGOCName.Enabled = true;
        // lblUserId.Enabled = true;
        txtRegisteredOffice.Enabled = true;
        txtIncorporationDate.Enabled = true;
        txtNoofSegments.Enabled = true;
        txtAccountSepartor.Enabled = true;
        txtSegment_0_Size.Enabled = true;
        txtSegment_1_Size.Enabled = true;
        txtSegment_2_Size.Enabled = true;
        txtSegment_3_Size.Enabled = true;
        txtSegment_4_Size.Enabled = true;
        txtSegment_5_Size.Enabled = true;
        txtSegment_6_Size.Enabled = true;
        txtSegment_7_Size.Enabled = true;
        txtSegment_8_Size.Enabled = true;
        txtSegment_9_Size.Enabled = true;
        lblSerialNo.Enabled = true;
        txtCOA_Format.Enabled = true;
        rdb_DIFFCoaBranch_Y.Enabled = true;
        rdb_DIFFCoaBranch_N.Enabled = true;
        txtNo_of_Branches.Enabled = true;
        txtUser_Name.Enabled = true;
        txtEntry_Date.Enabled = true;
        txtSystem_Date.Enabled = true;
        ddlCity.Enabled = true;
        ddlCountry.Enabled = true;
        chkActive.Checked = false;
        btnUpdate.Visible = true;
        btnInsert.Visible = false;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        ddlGrpGOC.Enabled = true;

    }
    public void Update()
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
            string rdb = "";
            if (rdb_DIFFCoaBranch_Y.Checked == true)
            {
                rdb = "Y";
            }
            else
            {
                rdb = "N";
            }
            string str0 = "", str1 = "", str2 = "", str3 = "", str4 = "", str5 = "", str6 = "", str7 = "", str8 = "", str9 = "";

            if (txtSegment_0_Size.Text == "")
            {
                str0 = "([PerSegmentSize0] IS NULL)";
            }
            else {
                str0 = "([PerSegmentSize0] = '" + txtSegment_0_Size.Text + "')";
            }

            if (txtSegment_1_Size.Text == "")
            {
                str1 = "([PerSegmentSize1] IS NULL)";
            }
            else {
                str1 = "([PerSegmentSize1] = '" + txtSegment_1_Size.Text + "')";
            }


            if (txtSegment_2_Size.Text == "")
            {
                str2 = "([PerSegmentSize2] IS NULL)";
            }
            else {
                str2 = "([PerSegmentSize2] = '" + txtSegment_2_Size.Text + "')";
            }

            if (txtSegment_3_Size.Text == "")
            {
                str3 = "([PerSegmentSize3] IS NULL)";
            }
            else {
                str3 = "([PerSegmentSize3] = '" + txtSegment_3_Size.Text + "')";
            }
            if (txtSegment_4_Size.Text == "")
            {
                str4 = "([PerSegmentSize4] IS NULL)";
            }
            else {
                str4 = "([PerSegmentSize4] = '" + txtSegment_4_Size.Text + "')";
            }
            if (txtSegment_5_Size.Text == "")
            {
                str5 = "([PerSegmentSize5] IS NULL)";
            }
            else {
                str5 = "([PerSegmentSize5] = '" + txtSegment_5_Size.Text + "')";
            }
            if (txtSegment_6_Size.Text == "")
            {
                str6 = "([PerSegmentSize6] IS NULL)";
            }
            else {
                str6 = "([PerSegmentSize6] = '" + txtSegment_6_Size.Text + "')";
            }
            if (txtSegment_7_Size.Text == "")
            {
                str7 = "([PerSegmentSize7] IS NULL)";
            }
            else {
                str7 = "([PerSegmentSize7] = '" + txtSegment_7_Size.Text + "')";
            }
            if (txtSegment_8_Size.Text == "")
            {
                str8 = "([PerSegmentSize8] IS NULL)";
            }
            else {
                str8 = "([PerSegmentSize8] = '" + txtSegment_8_Size.Text + "')";
            }
            if (txtSegment_9_Size.Text == "")
            {
                str9 = "([PerSegmentSize9] IS NULL)";
            }
            else {
                str9 = "([PerSegmentSize9] = '" + txtSegment_9_Size.Text + "')";
            }


            DataSet ds_up = dml.Find("select * from SET_GOC WHERE ([GocId]='"+ lblSerialNo.Text + "') AND ([GOCName]='"+txtGOCName.Text+ "') AND ([GrpWithGOC]='" + ddlGrpGOC.SelectedItem.Value + "') AND([Registered_Office]='" + txtRegisteredOffice.Text+ "') AND ([IncorporationDate]='" +dml.dateconvertforinsert(txtIncorporationDate) + "') AND ([CountryOfOriginId]='" +ddlCountry.SelectedItem.Value+ "') AND ([CityId]='" +ddlCity.SelectedItem.Value+ "') AND ([IsActive]='"+chk+"') AND ([NoofCoaSegments]='"+txtNoofSegments.Text+ "') AND ([CoaSegmentSeparater]='" +txtAccountSepartor.Text + "') AND "+str0+" AND "+str1+" AND "+str2+" AND "+str3+" AND "+str4+" AND "+str5+" AND "+str6+" AND "+str7+" AND "+str8+" AND "+str9+" AND ([CoaFormat]='" + txtCOA_Format.Text + "') AND ([DiffCoaForBranches]='" + rdb + "') AND ([NoOfBranches]='" + txtNo_of_Branches.Text + "')");

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
                con.Open();
                txtnull_segment();
                dml.Update("Update SET_GOC set GOCName='" + txtGOCName.Text + "', GrpWithGOC='"+ddlGrpGOC.SelectedItem.Value+"' , Registered_Office='" + txtRegisteredOffice.Text + "',IncorporationDate='" + DateTime.Now + "', CountryOfOriginId=" + ddlCountry.SelectedValue + ",	CityId=" + ddlCity.SelectedValue + ",	IsActive=" + chk + ",	NoofCoaSegments=" + txtNoofSegments.Text + ",	CoaSegmentSeparater='" + txtAccountSepartor.Text + "',PerSegmentSize0=" + txtSegment_0_Size.Text + ",	PerSegmentSize1=" + txtSegment_1_Size.Text + ",	PerSegmentSize2=" + txtSegment_2_Size.Text + ",	PerSegmentSize3=" + txtSegment_3_Size.Text + ", PerSegmentSize4=" + txtSegment_4_Size.Text + ",	PerSegmentSize5	=" + txtSegment_5_Size.Text + ",PerSegmentSize6=" + txtSegment_6_Size.Text + ",	PerSegmentSize7=" + txtSegment_7_Size.Text + ",	PerSegmentSize8=" + txtSegment_8_Size.Text + ",	PerSegmentSize9=" + txtSegment_9_Size.Text + ",	CoaFormat='" + txtCOA_Format.Text + "',	DiffCoaForBranches='" + rdb + "',NoOfBranches=" + txtNo_of_Branches.Text + ",	EntryUserId='" + userid + "',	LoginName='" + txtUser_Name.Text + "',EntryDate='" + DateTime.Now + "',	SysDate='" + DateTime.Now + "' where GOCId='" + lblSerialNo.Text + "';", "");

                textClear();
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", " Editalert()", true);
                con.Close();
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void textClear()
    {
        DeleteBox.Visible = false;
        Editbox.Visible = false;
        Findbox.Visible = false;
        fieldbox.Visible = true;
        Label1.Text = "";
        txtGOCName.Text = "";
        txtRegisteredOffice.Text = "";
        txtIncorporationDate.Text = "";
        txtNoofSegments.Text = "";
        txtAccountSepartor.Text = "";
        txtSegment_0_Size.Text = "";
        txtSegment_1_Size.Text = "";
        txtSegment_2_Size.Text = "";
        txtSegment_3_Size.Text = "";
        txtSegment_4_Size.Text = "";
        txtSegment_5_Size.Text = "";
        txtSegment_6_Size.Text = "";
        txtSegment_7_Size.Text = "";
        txtSegment_8_Size.Text = "";
        txtSegment_9_Size.Text = "";
        ddlGrpGOC.SelectedIndex = 0;
        ddlGrpGOC.Enabled = false;
        lblSerialNo.Text = "";
        txtCOA_Format.Text = "";
        rdb_DIFFCoaBranch_N.Checked = false;
        rdb_DIFFCoaBranch_Y.Checked = false;
        chkActive.Enabled = false;
        txtNo_of_Branches.Text = "";
        txtUser_Name.Text = "";
        txtEntry_Date.Text = "";
        txtSystem_Date.Text = "";
       ddlCountry.SelectedIndex = 0;
       //ddlCity.SelectedIndex = 0;


        txtGOCName.Enabled = false;
        txtRegisteredOffice.Enabled = false;
        txtIncorporationDate.Enabled = false;
        txtNoofSegments.Enabled = false;
        txtAccountSepartor.Enabled = false;
        txtSegment_0_Size.Enabled = false;
        txtSegment_1_Size.Enabled = false;
        txtSegment_2_Size.Enabled = false;
        txtSegment_3_Size.Enabled = false;
        txtSegment_4_Size.Enabled = false;
        txtSegment_5_Size.Enabled = false;
        txtSegment_6_Size.Enabled = false;
        txtSegment_7_Size.Enabled = false;
        txtSegment_8_Size.Enabled = false;
        txtSegment_9_Size.Enabled = false;
        lblSerialNo.Enabled = false;
        txtCOA_Format.Enabled = false;
        rdb_DIFFCoaBranch_N.Enabled = false;
        rdb_DIFFCoaBranch_Y.Enabled = false;
       
        txtNo_of_Branches.Enabled = false;
        txtUser_Name.Enabled = false;
        txtEntry_Date.Enabled = false;
        txtSystem_Date.Enabled = false;
        ddlCity.Enabled = false;
        ddlCountry.Enabled = false;
        chkActive.Checked = false;
        ImageButton1.Enabled = false;
        ImageButton2.Enabled = false;

        
      

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnInsert.Visible = true;
        btnDelete.Visible = true;
        btnUpdate.Visible = false;
        btnEdit.Visible = true;
        btnFind.Visible = true;
        btnSave.Visible = false;
        btnDelete_after.Visible = false;
        textClear();
    }

    public void Showall_Dml()
    {
        userid = Request.QueryString["UserID"];
        FormID = Request.QueryString["FormID"];
        DataSet FiscalStatus;
        con.Open();
        string Query = "SELECT * FROM SET_FISCAL_YEAR WHERE FISCALYEARID=" +Convert.ToInt32(Request.Cookies["FiscalYearId"].Value);
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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Update();
    }
    public void add_save()
    {
        try {
            int chk = 0;
            if (chkActive.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
            int rdb = 0;
            if (rdb_DIFFCoaBranch_Y.Checked == true)
            {
                rdb = 1;
            }
            else
            {
                rdb = 0;
            }
            txtnull_segment();
            string gocval = "0";
            if(ddlGrpGOC.SelectedIndex != 0)
            {
                gocval = ddlGrpGOC.SelectedItem.Value;
            }
            else
            {
                gocval = "0";
            }
            dml.Insert("INSERT INTO SET_GOC ([GOCName], [Registered_Office], [IncorporationDate], [CityId], [GrpWithGOC], [IsActive], [NoofCoaSegments], [CoaSegmentSeparater], [PerSegmentSize0], [PerSegmentSize1], [PerSegmentSize2], [PerSegmentSize3], [PerSegmentSize4], [PerSegmentSize5], [PerSegmentSize6], [PerSegmentSize7], [PerSegmentSize8], [PerSegmentSize9], [CoaFormat], [DiffCoaForBranches], [NoOfBranches], [EntryUserId], [LoginName], [EntryDate], [GUID], [SysDate] , [CountryOfOriginId],[MLD],[Record_Deleted]) VALUES ('" + txtGOCName.Text + "', '" + txtRegisteredOffice.Text + "', '" + dml.dateconvertforinsert(txtIncorporationDate) + "', '" + ddlCity.SelectedItem.Value + "', '"+gocval+"','" + chk + "', " + txtNoofSegments.Text + ", '" + txtAccountSepartor.Text + "', " + txtSegment_0_Size.Text + ", " + txtSegment_1_Size.Text + ", " + txtSegment_2_Size.Text + ", " + txtSegment_3_Size.Text + ", " + txtSegment_4_Size.Text + ", " + txtSegment_5_Size.Text + ", " + txtSegment_6_Size.Text + ", " + txtSegment_7_Size.Text + ", " + txtSegment_8_Size.Text + ", " + txtSegment_9_Size.Text + ", '" + txtCOA_Format.Text + "', '" +rdb+"', '" + txtNo_of_Branches.Text + "', '6B9C1166-0F4B-41DC-99E8-B47BE96C8157', 'fahad', '" + dml.dateconvertforinsert(txtEntry_Date) + "', '41A28783-6BAF-4C17-ACFD-AB49BEA99045', '" + dml.dateconvertforinsert(txtSystem_Date) + "','"+ddlCountry.SelectedValue+"' , '"+dml.Encrypt("h")+"','0')","");
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);
            textClear();
        }
        catch(Exception ex)
        {
            Label1.Text = ex.Message;
        }


    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        add_save();
        btnInsert_Click(sender, e);
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
            string squer = "select * from SET_GOC ";
            string swhere;

            if (txtEdit_GOCName.SelectedIndex != 0)
            {
                swhere = "GOCName =  '" + txtEdit_GOCName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "GOCName is not null";
            }
            if (txtEdit_NOofSeg.Text != "")
            {
                swhere = swhere + " and NoofCoaSegments = '" + txtEdit_NOofSeg.Text + "'";
            }
            else
            {
                swhere = swhere + " and NoofCoaSegments is not null";
            }
            if (txtEdit_EntryD.Text != "")
            {
                swhere = swhere + " and EntryDate = '" + txtEdit_EntryD.Text + "'";
            }
            else
            {
                swhere = swhere + " and EntryDate is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0 and gocid= '" + gocid() + "'  ORDER BY GOCName";

            Findbox.Visible = false;
            fieldbox.Visible = false;
            DeleteBox.Visible = false;

            DataSet dgrid = dml.grid(squer);
            if (dgrid.Tables[0].Rows.Count > 0)
            {
                GridView3.DataSource = dgrid;
                GridView3.DataBind();
            }
            else
            {
                textClear();
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", " nodatafound()", true);
            }

        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", " wrong()", true);
        }
        FormID = Request.QueryString["FormID"];
       
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountry.SelectedIndex == 0)
        {
            ddlCity.Items.Insert(0, "Please select...");
        }
        else
        {
            ddlCityFun();
        }

    }
    public void ddlCityFun()
    {
      
        dml.dropdownsql(ddlCity, "SET_City", "CityName", "CityID", "CountryID" , ddlCountry.SelectedValue);
    }
    public void txtnull_segment()
    {
        if (txtSegment_0_Size.Text == "")
        {
            txtSegment_0_Size.Text = "NULL";
        }
        if (txtSegment_1_Size.Text == "")
        {
            txtSegment_1_Size.Text = "NULL";
        }
        if (txtSegment_2_Size.Text == "")
        {
            txtSegment_2_Size.Text = "NULL";
        }
        if (txtSegment_3_Size.Text == "")
        {
            txtSegment_3_Size.Text = "NULL";
        }
        if (txtSegment_4_Size.Text == "")
        {
            txtSegment_4_Size.Text = "NULL";
        }
        if (txtSegment_5_Size.Text == "")
        {
            txtSegment_5_Size.Text = "NULL";
        }
        if (txtSegment_6_Size.Text == "")
        {
            txtSegment_6_Size.Text = "NULL";
        }
        if (txtSegment_7_Size.Text == "")
        {
            txtSegment_7_Size.Text = "NULL";
        }
        if (txtSegment_8_Size.Text == "")
        {
            txtSegment_8_Size.Text = "NULL";
        }
        if (txtSegment_9_Size.Text == "")
        {
            txtSegment_9_Size.Text = "NULL";
        }
    }
    public string maskFormat(TextBox seperator, TextBox NoofSegm, TextBox segm0, TextBox segm1, TextBox segm2, TextBox segm3, TextBox segm4, TextBox segm5, TextBox segm6, TextBox segm7, TextBox segm8, TextBox segm9)
    {
        // DataSet dsFormat = dml.Find("select NoofCoaSegments,CoaSegmentSeparater,PerSegmentSize0,PerSegmentSize1	,PerSegmentSize2,	PerSegmentSize3	,PerSegmentSize4,	PerSegmentSize5	,PerSegmentSize6,	PerSegmentSize7	,PerSegmentSize8,	PerSegmentSize9	,CoaFormat from SET_GOC where GocID = 1");
        string format = "";
        string noOFSEG = NoofSegm.Text;
        string separator = seperator.Text;
        string seg0 = segm0.Text;
        string seg1 = segm1.Text;
        string seg2 = segm2.Text;
        string seg3 = segm3.Text;
        string seg4 = segm4.Text;
        string seg5 = segm5.Text;
        string seg6 = segm6.Text;
        string seg7 = segm7.Text;
        string seg8 = segm8.Text;
        string seg9 = segm9.Text;
        string dd = "";

        if (seg0 != "" && segm0.Enabled == true)
        {

            for (int s1 = 1; s1 <= int.Parse(seg0) + 1; s1++)
            {
                if (s1 <= int.Parse(seg0))
                {
                    format += "0";
                }
                else
                {
                    format += separator;
                }
            }
        }

        if (seg1 != "" && segm1.Enabled == true)
        {
            for (int s2 = 1; s2 <= int.Parse(seg1) + 1; s2++)
            {
                if (s2 <= int.Parse(seg1))
                {
                    format += "0";
                }
                else
                {
                    format += separator;
                }
            }
        }

        if (seg2 != "" && segm2.Enabled == true)
        {
            for (int s3 = 1; s3 <= int.Parse(seg2) + 1; s3++)
            {
                if (s3 <= int.Parse(seg2))
                {
                    format += "0";
                }
                else
                {
                    format += separator;
                }
            }
        }

        if (seg3 != "" && segm3.Enabled == true)
        {
            for (int s4 = 1; s4 <= int.Parse(seg3) + 1; s4++)
            {
                if (s4 <= int.Parse(seg3))
                {
                    format += "0";
                }
                else
                {
                    format += separator;
                }
            }
        }

        if (seg4 != "" && segm4.Enabled == true)
        {
            for (int s5 = 1; s5 <= int.Parse(seg4) + 1; s5++)
            {
                if (s5 <= int.Parse(seg4))
                {
                    format += "0";
                }
                else
                {
                    format += separator;
                }
            }
        }

        if (seg5 != "" && segm5.Enabled == true)
        {
            for (int s6 = 1; s6 <= int.Parse(seg5) + 1; s6++)
            {
                if (s6 <= int.Parse(seg5))
                {
                    format += "0";
                }
                else
                {
                    format += separator;
                }
            }
        }

        if (seg6 != "" && segm6.Enabled == true)
        {
            for (int s7 = 1; s7 <= int.Parse(seg6) + 1; s7++)
            {
                if (s7 <= int.Parse(seg6))
                {
                    format += "0";
                }
                else
                {
                    format += separator;
                }
            }
        }
        if (seg7 != "" && segm7.Enabled == true)
        {
            for (int s8 = 1; s8 <= int.Parse(seg7) + 1; s8++)
            {
                if (s8 <= int.Parse(seg7))
                {
                    format += "0";
                }
                else
                {
                    format += separator;
                }
            }
        }

        if (seg8 != "" && segm8.Enabled == true)
        {
            for (int s9 = 1; s9 <= int.Parse(seg8) + 1; s9++)
            {
                if (s9 <= int.Parse(seg8))
                {
                    format += "0";
                }
                else
                {
                    format += separator;
                }
            }
        }

        if (seg9 != "" && segm9.Enabled == true)
        {
            for (int s10 = 1; s10 <= int.Parse(seg9) + 1; s10++)
            {
                if (s10 <= int.Parse(seg9))
                {
                    format += "0";
                }
                else
                {
                    format += separator;
                }
            }
        }
        //  str.Remove(str.Length - 1);
        if (format.Length > 0)
        {
            dd = format.Remove(format.Length - 1);

        }
        else
        {
            dd = "0";
        }
        return dd;
    }
    protected void txtNoofSegments_Load(object sender, EventArgs e)
    {
        txtSegment_0_Size.Enabled = false;
        txtSegment_1_Size.Enabled = false;
        txtSegment_2_Size.Enabled = false;
        txtSegment_3_Size.Enabled = false;
        txtSegment_4_Size.Enabled = false;
        txtSegment_5_Size.Enabled = false;
        txtSegment_6_Size.Enabled = false;
        txtSegment_7_Size.Enabled = false;
        txtSegment_8_Size.Enabled = false;
        txtSegment_9_Size.Enabled = false;

        string no = txtNoofSegments.Text;
        if (no == "")
        {
        }
        else {

            for (int i = 1; i <= int.Parse(no); i++)
            {
                switch (i)
                {
                    case 1:
                        txtSegment_0_Size.Enabled = true;
                        break;

                    case 2:
                        txtSegment_1_Size.Enabled = true;
                        break;

                    case 3:
                        txtSegment_2_Size.Enabled = true;
                        break;

                    case 4:
                        txtSegment_3_Size.Enabled = true;
                        break;

                    case 5:
                        txtSegment_4_Size.Enabled = true;
                        break;

                    case 6:
                        txtSegment_5_Size.Enabled = true;
                        break;
                    case 7:
                        txtSegment_6_Size.Enabled = true;
                        break;
                    case 8:
                        txtSegment_7_Size.Enabled = true;
                        break;
                    case 9:
                        txtSegment_8_Size.Enabled = true;
                        break;
                    case 10:
                        txtSegment_9_Size.Enabled = true;
                        break;
                }
            }
            MaskedEditExtender1.Mask = maskFormat(txtAccountSepartor, txtNoofSegments, txtSegment_0_Size, txtSegment_1_Size, txtSegment_2_Size, txtSegment_3_Size, txtSegment_4_Size, txtSegment_5_Size, txtSegment_6_Size, txtSegment_7_Size, txtSegment_8_Size, txtSegment_9_Size);
        }


    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow grow in GridView2.Rows)
        {
            CheckBox chk_del = (CheckBox)grow.FindControl("chkDeletegd");
            Label lblID = (Label)grow.FindControl("lbl_FYID");
            if (chk_del.Checked)
            {
                int GOCID = Convert.ToInt32(lblID.Text);
                dml.Delete("delete SET_GOC where GocId = " + GOCID, "");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "alertDelete()", true);
            }
            else
            {
               // ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "fillrequird()", true);
               // GridView2.DataBind();
            }
        }
        GridView2.DataBind();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnUpdate.Visible = false;
        btnInsert.Visible = false;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        Label1.Text = "";


        fieldbox.Visible = true;
        Findbox.Visible = false;
        textClear();

        try
        {
            string serial_id;
            serial_id = GridView1.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select * from SET_GOC WHERE ([GocId]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCountry.ClearSelection();
                ddlCity.ClearSelection();
              
                lblSerialNo.Text = ds.Tables[0].Rows[0]["GocId"].ToString();
                txtGOCName.Text = ds.Tables[0].Rows[0]["GOCName"].ToString();
                txtRegisteredOffice.Text = ds.Tables[0].Rows[0]["Registered_Office"].ToString();
                txtIncorporationDate.Text = ds.Tables[0].Rows[0]["IncorporationDate"].ToString();
                dml.dateConvert(txtIncorporationDate);
                ddlCountry.Items.FindByValue(ds.Tables[0].Rows[0]["CountryOfOriginId"].ToString()).Selected = true;
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                ddlCityFun();
                ddlCity.Items.FindByValue(ds.Tables[0].Rows[0]["CityId"].ToString()).Selected = true;

                txtNoofSegments.Text = ds.Tables[0].Rows[0]["NoofCoaSegments"].ToString();
                txtAccountSepartor.Text = ds.Tables[0].Rows[0]["CoaSegmentSeparater"].ToString();
                txtSegment_0_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize0"].ToString();
                txtSegment_1_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize1"].ToString();
                txtSegment_2_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize2"].ToString();
                txtSegment_3_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize3"].ToString();
                txtSegment_4_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize4"].ToString();
                txtSegment_5_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize5"].ToString();
                txtSegment_6_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize6"].ToString();
                txtSegment_7_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize7"].ToString();
                txtSegment_8_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize8"].ToString();
                txtSegment_9_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize9"].ToString();
                txtCOA_Format.Text = ds.Tables[0].Rows[0]["CoaFormat"].ToString();
                ddlGrpGOC.ClearSelection();
                if (ddlGrpGOC.Items.FindByValue(ds.Tables[0].Rows[0]["GrpWithGOC"].ToString()) != null)
                {
                    ddlGrpGOC.Items.FindByValue(ds.Tables[0].Rows[0]["GrpWithGOC"].ToString()).Selected = true;
                }

                txtUser_Name.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["EntryUserId"].ToString());
                string diif_rdb = ds.Tables[0].Rows[0]["DiffCoaForBranches"].ToString();
                txtNo_of_Branches.Text = ds.Tables[0].Rows[0]["NoOfBranches"].ToString();
                //txtUser_Name.Text = ds.Tables[0].Rows[0]["EntryUserId"].ToString();

                txtEntry_Date.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                dml.dateConvert(txtEntry_Date);
                DateTime dt = DateTime.Parse(ds.Tables[0].Rows[0]["SysDate"].ToString());
                txtSystem_Date.Text = dt.ToString("dd-MMM-yyy hh:mm:ss.ffff");
                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }

                if (diif_rdb == "Y")
                {
                    rdb_DIFFCoaBranch_Y.Checked = true;
                    rdb_DIFFCoaBranch_N.Checked = false;

                }
                else
                {
                    rdb_DIFFCoaBranch_Y.Checked = false;
                    rdb_DIFFCoaBranch_N.Checked = true;
                }

                txtNoofSegments_Load(sender, e);
                txtSegment_0_Size.Enabled = false;
                txtSegment_1_Size.Enabled = false;
                txtSegment_2_Size.Enabled = false;
                txtSegment_3_Size.Enabled = false;
                txtSegment_4_Size.Enabled = false;
                txtSegment_5_Size.Enabled = false;
                txtSegment_6_Size.Enabled = false;
                txtSegment_7_Size.Enabled = false;
                txtSegment_8_Size.Enabled = false;
                txtSegment_9_Size.Enabled = false;

            }
        }



        catch (Exception ex)
        {
            Label1.Text = ex.ToString();
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "wrong()", true);

        }
        FormID = Request.QueryString["FormID"];



    }
    protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtGOCName.Enabled = true;
        txtRegisteredOffice.Enabled = true;
        txtIncorporationDate.Enabled = true;
        txtNoofSegments.Enabled = true;
        txtAccountSepartor.Enabled = true;
        txtSegment_0_Size.Enabled = true;
        txtSegment_1_Size.Enabled = true;
        txtSegment_2_Size.Enabled = true;
        txtSegment_3_Size.Enabled = true;
        txtSegment_4_Size.Enabled = true;
        txtSegment_5_Size.Enabled = true;
        txtSegment_6_Size.Enabled = true;
        txtSegment_7_Size.Enabled = true;
        txtSegment_8_Size.Enabled = true;
        txtSegment_9_Size.Enabled = true;
        ddlGrpGOC.Enabled = true;
        lblSerialNo.Enabled = true;
        txtCOA_Format.Enabled = true;
        ImageButton1.Enabled = true;
        ImageButton2.Enabled = true;

        rdb_DIFFCoaBranch_N.Enabled = true;
        rdb_DIFFCoaBranch_Y.Enabled = true;

        txtNo_of_Branches.Enabled = true;
        txtUser_Name.Enabled = true;
        txtEntry_Date.Enabled = true;
        txtSystem_Date.Enabled = false;
        ddlCity.Enabled = true;
        ddlCountry.Enabled = true;
        chkActive.Checked = true;

        btnUpdate.Visible = true;
        btnInsert.Visible = false;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        Label1.Text = "";


        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        DeleteBox.Visible = false;


        try
        {
            string serial_id;
            serial_id = GridView3.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select * from SET_GOC WHERE ([GocId]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCountry.ClearSelection();
                ddlCity.ClearSelection();
                
                lblSerialNo.Text = ds.Tables[0].Rows[0]["GocId"].ToString();
                txtGOCName.Text = ds.Tables[0].Rows[0]["GOCName"].ToString();
                txtRegisteredOffice.Text = ds.Tables[0].Rows[0]["Registered_Office"].ToString();
                txtIncorporationDate.Text = ds.Tables[0].Rows[0]["IncorporationDate"].ToString();
                dml.dateConvert(txtIncorporationDate);
                ddlCountry.Items.FindByValue(ds.Tables[0].Rows[0]["CountryOfOriginId"].ToString()).Selected = true;
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                ddlCityFun();
                ddlCity.Items.FindByValue(ds.Tables[0].Rows[0]["CityId"].ToString()).Selected = true;
                
                txtNoofSegments.Text = ds.Tables[0].Rows[0]["NoofCoaSegments"].ToString();
                txtAccountSepartor.Text = ds.Tables[0].Rows[0]["CoaSegmentSeparater"].ToString();
                txtSegment_0_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize0"].ToString();
                txtSegment_1_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize1"].ToString();
                txtSegment_2_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize2"].ToString();
                txtSegment_3_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize3"].ToString();
                txtSegment_4_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize4"].ToString();
                txtSegment_5_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize5"].ToString();
                txtSegment_6_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize6"].ToString();
                txtSegment_7_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize7"].ToString();
                txtSegment_8_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize8"].ToString();
                txtSegment_9_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize9"].ToString();
                txtCOA_Format.Text = ds.Tables[0].Rows[0]["CoaFormat"].ToString();
                string diif_rdb = ds.Tables[0].Rows[0]["DiffCoaForBranches"].ToString();
                txtNo_of_Branches.Text = ds.Tables[0].Rows[0]["NoOfBranches"].ToString();
                //txtUser_Name.Text = ds.Tables[0].Rows[0]["EntryUserId"].ToString();
                ddlGrpGOC.ClearSelection();
                if (ddlGrpGOC.Items.FindByValue(ds.Tables[0].Rows[0]["GrpWithGOC"].ToString()) != null)
                {
                    ddlGrpGOC.Items.FindByValue(ds.Tables[0].Rows[0]["GrpWithGOC"].ToString()).Selected = true;
                }
                txtEntry_Date.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                dml.dateConvert(txtEntry_Date);
                DateTime dt = DateTime.Parse(ds.Tables[0].Rows[0]["SysDate"].ToString());
                txtSystem_Date.Text = dt.ToString("dd-MMM-yyy hh:mm:ss.ffff");
                txtUser_Name.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["EntryUserId"].ToString());
                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }

                if (diif_rdb == "Y")
                {
                    rdb_DIFFCoaBranch_Y.Checked = true;
                    rdb_DIFFCoaBranch_N.Checked = false;

                }
                else
                {
                    rdb_DIFFCoaBranch_Y.Checked = false;
                    rdb_DIFFCoaBranch_N.Checked = true;
                }
                txtNoofSegments_Load(sender, e);

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
        CalendarExtender1.EndDate = DateTime.Now;
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        DeleteBox.Visible = false;

        try
        {
            string serial_id;
            serial_id = GridView2.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select * from SET_GOC WHERE ([GocId]='" + serial_id + "') and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCountry.ClearSelection();
                ddlCity.ClearSelection();
                //[GocId], [GOCName], [Registered_Office], [IncorporationDate], [CountryOfOriginId],
                //[CityId], [IsActive], [NoofCoaSegments], [CoaSegmentSeparater], [PerSegmentSize0], [PerSegmentSize1], 
                //[PerSegmentSize2], [PerSegmentSize3], [PerSegmentSize4], [PerSegmentSize5], [PerSegmentSize6], 
                //[PerSegmentSize7], [PerSegmentSize8], [PerSegmentSize9], [CoaFormat], [DiffCoaForBranches], 
                //[NoOfBranches], [EntryUserId], [LoginName], [EntryDate], [GUID], [SysDate], [Record_Deleted])
                lblSerialNo.Text = ds.Tables[0].Rows[0]["GocId"].ToString();
                txtGOCName.Text = ds.Tables[0].Rows[0]["GOCName"].ToString();
                txtRegisteredOffice.Text = ds.Tables[0].Rows[0]["Registered_Office"].ToString();
                txtIncorporationDate.Text = ds.Tables[0].Rows[0]["IncorporationDate"].ToString();
                dml.dateConvert(txtIncorporationDate);
                ddlCountry.Items.FindByValue(ds.Tables[0].Rows[0]["CountryOfOriginId"].ToString()).Selected = true;
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                ddlCityFun();
                ddlCity.Items.FindByValue(ds.Tables[0].Rows[0]["CityId"].ToString()).Selected = true;

                txtNoofSegments.Text = ds.Tables[0].Rows[0]["NoofCoaSegments"].ToString();
                txtAccountSepartor.Text = ds.Tables[0].Rows[0]["CoaSegmentSeparater"].ToString();
                txtSegment_0_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize0"].ToString();
                txtSegment_1_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize1"].ToString();
                txtSegment_2_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize2"].ToString();
                txtSegment_3_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize3"].ToString();
                txtSegment_4_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize4"].ToString();
                txtSegment_5_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize5"].ToString();
                txtSegment_6_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize6"].ToString();
                txtSegment_7_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize7"].ToString();
                txtSegment_8_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize8"].ToString();
                txtSegment_9_Size.Text = ds.Tables[0].Rows[0]["PerSegmentSize9"].ToString();
                txtCOA_Format.Text = ds.Tables[0].Rows[0]["CoaFormat"].ToString();
                string diif_rdb = ds.Tables[0].Rows[0]["DiffCoaForBranches"].ToString();
                txtNo_of_Branches.Text = ds.Tables[0].Rows[0]["NoOfBranches"].ToString();
                //txtUser_Name.Text = ds.Tables[0].Rows[0]["EntryUserId"].ToString();
                ddlGrpGOC.ClearSelection();
                if (ddlGrpGOC.Items.FindByValue(ds.Tables[0].Rows[0]["GrpWithGOC"].ToString()) != null)
                {
                    ddlGrpGOC.Items.FindByValue(ds.Tables[0].Rows[0]["GrpWithGOC"].ToString()).Selected = true;
                }
                txtEntry_Date.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                dml.dateConvert(txtEntry_Date);
                txtUser_Name.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["EntryUserId"].ToString());

                DateTime dt = DateTime.Parse(ds.Tables[0].Rows[0]["SysDate"].ToString());
                txtSystem_Date.Text = dt.ToString("dd-MMM-yyy hh:mm:ss.ffff");
                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }

                if (diif_rdb == "Y")
                {
                    rdb_DIFFCoaBranch_Y.Checked = true;
                    rdb_DIFFCoaBranch_N.Checked = false;

                }
                else
                {
                    rdb_DIFFCoaBranch_Y.Checked = false;
                    rdb_DIFFCoaBranch_N.Checked = true;
                }
                txtNoofSegments_Load(sender, e);

            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string id = e.Row.Cells[1].Text;
        if (id != "ID")
        {
            DataSet ds = dml.Find("select GocId,MLD from SET_GOC where GocId = '" + id + "'");
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
        if (id != "ID")
        {
            if (id != "&nbsp;")
            {
                DataSet ds = dml.Find("select GocId,MLD from SET_GOC where GocId = '" + id + "'");
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
}