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
    int DateFrom, AddDays, EditDays, DeleteDays;
   public string compidinsert;
    public string gocidinsert;
    string userid, UserGrpID, FormID;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
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
            CalendarExtender2.EndDate = DateTime.Now;
            Deletebox.Visible = false;
            Editbox.Visible = false;
            btnDelete_after.Visible = false;

            entrydaterange();
            // fd.HideUSerGrpField(Page.Controls, UserGrpID, FormID, "SET_Fiscal_Year");
            // Showall_Dml();
            txtFS_DateCOA.Attributes.Add("readonly", "readonly");
            txtFEnd_DateCOA.Attributes.Add("readonly", "readonly");
            txtEntry_Date.Attributes.Add("readonly", "readonly");
           // CalendarExtender2.EndDate = DateTime.Now;

            dml.dropdownsql(txtEdit_GOCName, "SET_GOC", "GOCName", "GocId");
            dml.dropdownsql(txtFind_Goc_Company, "SET_GOC", "GOCName", "GocId");
            dml.dropdownsql(txtDelete_GOCName, "SET_GOC", "GOCName", "GocId");

           textClear();
           

        }

    }

    protected void Showall_Dml() {
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
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            
            int  ca_Goc_Coa = 0;
            int chk = 0;

            if (validCompid() == true)
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "Alraedy Inserted this record !!";
            }
            else {


                int a = FindDate(txtFS_DateCOA.Text, txtFEnd_DateCOA.Text);
                if (a == 0)
                {
                    Label1.ForeColor = System.Drawing.Color.Red;
                    Label1.Text = "Date does not match with Fiscal year";
                }
                else {

                    if (chkActive.Checked == true)
                    {
                        chk = 1;
                    }
                    else
                    {
                        chk = 0;
                    }
                    if (rdbGoc.Checked == true)
                    {
                        ca_Goc_Coa = 0;
                    }
                    else if (rdbCompanny.Checked == true)
                    {
                        ca_Goc_Coa = 1;
                    }
                    var text = Page.Request.Form[txtEntry_Date.UniqueID];

                    dml.Update("UPDATE SET_COA_Master set Fiscal_End_Dt_for_COA = DATEADD(" + "dd" + ",-1,'" + dml.dateconvertString(text) + "') WHERE CompId =1 AND GocId=1 AND Goc_Comp_Coa = 1;", "");
                    formatsegment();
                    string fiscal = Request.QueryString.Get("fiscaly");
                    DataSet ds = dml.Find("select FiscalYearID from SET_Fiscal_Year where Description = '" + fiscal + "'");
                    fiscal = ds.Tables[0].Rows[0]["FiscalYearID"].ToString();

                    dml.Insert("INSERT INTO SET_COA_Master ([GocId], [CompId], [Active], [EntryDate], [Fiscal_Start_Dt_for_COA], [Goc_Comp_Coa], [NoofSegments], [Template], [SysDate], [User], [Narration], [FiscalYearID],[Fiscal_End_Dt_for_COA],[Record_Deleted]) VALUES('" + gocidinsert + "', '" + compidinsert + "', '" + chk + "', '" + dml.dateconvertString(text) + "', '" + dml.dateconvertforinsert(txtFS_DateCOA) + "'," + ca_Goc_Coa + ", '" + txtNoOFSEG.Text + "', '" + lblCOA_Template.Text + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff ") + "', '" + lblUSerNAme.Text + "', '" + txtNarration.Text + "','" + fiscal + "','" + dml.dateconvertforinsert(txtFEnd_DateCOA) + "','0');", "");
                    ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "alertme()", true);
                    btnInsert_Click(sender, e);
                    
                    btnInsert.Visible = true;
                    btnDelete.Visible = true;
                    btnUpdate.Visible = false;
                    btnEdit.Visible = true;
                    btnFind.Visible = true;
                    btnSave.Visible = false;
                    btnDelete_after.Visible = false;
                }
            }

        }
        catch (Exception ex)
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = ex.Message;
        }
    }

    protected void btnInsert_Click(object sender, EventArgs e) 
    {
        string goc_comp = "";
        formatsegment();
        DataSet dscomp_goc = dml.Find("select Goc_Comp_Coa from SET_COA_Master where gocid = "+gocidinsert+" and CompId = "+compidinsert+" and Goc_Comp_Coa = 0");
        if (dscomp_goc.Tables[0].Rows.Count > 0)
        {
            goc_comp = dscomp_goc.Tables[0].Rows[0]["Goc_Comp_Coa"].ToString();

        }

        btnSave.Visible = true;
        btnEdit.Visible = false;
        btnFind.Visible = false;
        btnCancel.Visible = true;
        btnDelete.Visible = false;
        btnInsert.Visible = false;
        btnUpdate.Visible = false;
        //F_SatrtDateForCoA();

        //F_EndDateForCoA();
        rdbGoc.Enabled = false;
        rdbCompanny.Enabled = false;
        chkActive.Checked = true;
        // ddlGOC.Enabled = true;
        lblUSerNAme.Enabled = true;
        txtNoOFSEG.Enabled = false;
        txtFS_DateCOA.Enabled = true;
        txtFEnd_DateCOA.Enabled = true;
        txtEntry_Date.Enabled = true;
        txtNarration.Enabled = true;
        lblCoaMAsterSerial.Enabled = true;
        lblCOA_Template.Enabled = true;
        lblSystem_Date.Enabled = true;
        chkActive.Enabled = true;
        imgPopup.Enabled = true;
        ImageButton1.Enabled = true;
        ImageButton2.Enabled = true;

        lblSystem_Date.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff ");
        formatsegment();
        lblUSerNAme.Text = show_username();
        if (goc_comp == "False")
        {
            rdbGoc.Checked = true;
            rdbCompanny.Checked = false;
            rdbCompanny.Enabled = false;
            rdbGoc.Enabled = false;
        }
       else if (goc_comp == "True")
        {
            rdbGoc.Checked = false;
            rdbCompanny.Checked = true;
            rdbCompanny.Enabled = true;
            rdbGoc.Enabled = true;
        }
       

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            int a = FindDate(txtFS_DateCOA.Text, txtFEnd_DateCOA.Text);
            if (a == 0)
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "Date does not match with Fiscal year";
            }
            else {

                //if (COmp_GOC_ID() == true)
               // {
                    int ca_Goc_Coa = 0;
                    int chk = 0;
                    if (chkActive.Checked == true)
                    {
                        chk = 1;
                    }
                    else
                    {
                        chk = 0;
                    }
                    if (rdbGoc.Checked == true)
                    {
                        ca_Goc_Coa = 0;
                    }
                    else if (rdbCompanny.Checked == true)
                    {
                        ca_Goc_Coa = 1;
                    }
                    userid = Request.QueryString["UserID"];


                    string fs_st = txtFS_DateCOA.Text;
                    string fs_end = txtFEnd_DateCOA.Text;
                    string entry = txtEntry_Date.Text;

                    DataSet ds_up = dml.Find("select * from SET_COA_Master WHERE ([COA_M_ID]='"+lblCoaMAsterSerial.Text+"') AND  ([Active]='"+chk+"') AND ([EntryDate]='"+dml.dateconvertforinsert(txtEntry_Date)+"') AND ([Goc_Comp_Coa]='"+ca_Goc_Coa+"') AND ([Fiscal_Start_Dt_for_COA]='"+dml.dateconvertforinsert(txtFS_DateCOA)+"') AND ([Fiscal_End_Dt_for_COA]='"+dml.dateconvertforinsert(txtFEnd_DateCOA)+"') AND ([NoofSegments]='"+txtNoOFSEG.Text+"') AND ([Template]='"+lblCOA_Template.Text+"') AND ([Narration]='"+txtNarration.Text+"') AND ([Record_Deleted]='0')");

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
                        dml.Update("UPDATE [SET_COA_Master] SET [Active]='" + chk + "', [EntryDate]='" + dml.dateconvertString(entry) + "', [Goc_Comp_Coa]='" + ca_Goc_Coa + "',[NoofSegments]='" + txtNoOFSEG.Text + "', [Template]='" + lblCOA_Template.Text + "', [SysDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', [User]='" + lblUSerNAme.Text + "', [Narration]='" + txtNarration.Text + "', [Fiscal_Start_Dt_for_COA]='" + dml.dateconvertString(fs_st) + "', [Fiscal_End_Dt_for_COA]='" + dml.dateconvertString(fs_end) + "' WHERE ([COA_M_ID]='" + lblCoaMAsterSerial.Text + "')", "");
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
               // }
              //  else
               // {
                //    Label1.ForeColor = System.Drawing.Color.Red;
                    //Label1.Text = txtGoc_Coaformat.Text + " is not valid";
              //  }
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

        try {
            dml.Delete("update SET_COA_Master set Record_Deleted = 1 where COA_M_ID = "+lblCoaMAsterSerial.Text+"", "");
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
        catch(Exception ex)
        {
            Label1.Text = ex.Message;
        }  
            
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
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


    }

    protected void btnSearchEdit_Click(object sender, EventArgs e)
    {
        try
        {
            formatsegment();
            string squer = "select * from SET_COA_Master ";
            string swhere;

            if (txtEdit_GOCName.SelectedIndex != 0)
            {
                swhere = "GocId = " + txtEdit_GOCName.SelectedItem.Value;
            }
            else
            {
                swhere = "GocId is not null";
            }
            if (txtEdit_NoofSeg.Text != "")
            {
                swhere = swhere + " and CompId = '" + txtEdit_NoofSeg.Text + "'";
            }
            else
            {
                swhere = swhere + " and CompId is not null";
            }
            if (txtEdit_Template.Text != "")
            {
                swhere = swhere + " and Template = '" + txtEdit_Template.Text + "'";
            }
            else
            {
                swhere = swhere + " and Template is not null";
            }

            if (ChkEdit_Active.Checked == true)
            {
                swhere = swhere + " and Active = '1'";
            }
            else if (ChkEdit_Active.Checked == false)
            {
                swhere = swhere + " and Active = '0'";
            }
            else
            {
                swhere = swhere + " and Active is not null";
            }
            squer = squer + " where " + swhere + " and Record_Deleted = 0 and gocid = "+gocidinsert+" and CompId = "+compidinsert+"";

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
        try
        {
            formatsegment();
            string squer = "select * from SET_COA_Master ";
            string swhere;

            if (txtFind_Goc_Company.SelectedIndex != 0)
            {
                swhere = "GocId = " + txtFind_Goc_Company.SelectedItem.Value;
            }
            else
            {
                swhere = "GocId is not null";
            }
            if (txtFind_NoofSegments.Text != "")
            {
                swhere = swhere + " and CompId = '" + txtFind_NoofSegments.Text + "'";
            }
            else
            {
                swhere = swhere + " and CompId is not null";
            }
            if (txtFind_Template.Text != "")
            {
                swhere = swhere + " and Template = '" + txtFind_Template.Text + "'";
            }
            else
            {
                swhere = swhere + " and Template is not null";
            }

            if (ChkFInd_Active.Checked == true)
            {
                swhere = swhere + " and Active = '1'";
            }
            else if (ChkFInd_Active.Checked == false)
            {
                swhere = swhere + " and Active = '0'";
            }
            else
            {
                swhere = swhere + " and Active is not null";
            }
            squer = squer + " where " + swhere + " and Record_Deleted = 0 and gocid = " + gocidinsert + " and CompId = " + compidinsert + ""; 

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

        }
    }

    protected void btn_delete_Click(object sender, EventArgs e)
    {
        Deletebox.Visible = true;
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
            formatsegment();
            string squer = "select * from SET_COA_Master ";
            string swhere;

            if (txtDelete_GOCName.SelectedIndex != 0)
            {
                swhere = "GocId = " + txtDelete_GOCName.SelectedItem.Value;
            }
            else
            {
                swhere = "GocId is not null";
            }
            if (txtDelete_CompName.Text != "")
            {
                swhere = swhere + " and CompId = '" + txtDelete_CompName.Text + "'";
            }
            else
            {
                swhere = swhere + " and CompId is not null";
            }
            if (txtDelete_Template.Text != "")
            {
                swhere = swhere + " and Template = '" + txtDelete_Template.Text + "'";
            }
            else
            {
                swhere = swhere + " and Template is not null";
            }

            if (chkDelete_Active.Checked == true)
            {
                swhere = swhere + " and Active = '1'";
            }
            else if (chkDelete_Active.Checked == false)
            {
                swhere = swhere + " and Active = '0'";
            }
            else
            {
                swhere = swhere + " and Active is not null";
            }
            squer = squer + " where " + swhere + " and Record_Deleted = 0 and gocid = " + gocidinsert + " and CompId = " + compidinsert + ""; 

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

    public void textClear()
    {
        txtNoOFSEG.Text = "";
        txtEntry_Date.Text = "";
        txtNarration.Text = "";
        lblCoaMAsterSerial.Text = "";
        lblCOA_Template.Text = "";
        lblSystem_Date.Text = "";
        rdbCompanny.Checked = false;
        rdbGoc.Checked = false;
        Label1.Text = "";
        txtFS_DateCOA.Text = "";
        txtFEnd_DateCOA.Text = "";
        lblUSerNAme.Text = "";
        chkActive.Checked = false;
        txtFS_DateCOA.Enabled = false;
        txtFEnd_DateCOA.Enabled = false;
        rdbCompanny.Enabled = false;
        rdbGoc.Enabled = false;
        //ddlGOC.Enabled = false;
        lblUSerNAme.Enabled = false;
        txtNoOFSEG.Enabled = false;
        txtEntry_Date.Enabled = false;
        txtNarration.Enabled = false;
        lblCoaMAsterSerial.Enabled = false;
        lblCOA_Template.Enabled = false;
        lblSystem_Date.Enabled = false;
        chkActive.Enabled = false;
        imgPopup.Enabled = false;
        ImageButton1.Enabled = false;
        ImageButton2.Enabled = false;
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

        rdbCompanny.Enabled = false;
        rdbGoc.Enabled = false;
        lblUSerNAme.Enabled = true;
        txtNoOFSEG.Enabled = false;
        txtEntry_Date.Enabled = true;
        txtNarration.Enabled = true;
        lblCoaMAsterSerial.Enabled = true;
        lblCOA_Template.Enabled = true;
        lblSystem_Date.Enabled = true;
        chkActive.Enabled = true;
        txtFEnd_DateCOA.Enabled = true;
        txtFS_DateCOA.Enabled = true;
        imgPopup.Enabled = true;
        ImageButton1.Enabled = true;
        ImageButton2.Enabled = true;


        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;

        try
        {
            lblCoaMAsterSerial.Text = GridView3.SelectedRow.Cells[1].Text;
            DataSet ds = dml.Find("select * from SET_COA_Master where COA_M_ID = " + lblCoaMAsterSerial.Text + " and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtEntry_Date.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtNoOFSEG.Text = ds.Tables[0].Rows[0]["NoofSegments"].ToString();
                lblCOA_Template.Text = ds.Tables[0].Rows[0]["Template"].ToString();
                DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["SysDate"].ToString());
                lblUSerNAme.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["User"].ToString());
                txtNarration.Text = ds.Tables[0].Rows[0]["Narration"].ToString();
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["Active"].ToString());
                bool g_c = bool.Parse(ds.Tables[0].Rows[0]["Goc_Comp_Coa"].ToString());
                dml.dateConvert(txtEntry_Date);
                lblSystem_Date.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");


                txtFEnd_DateCOA.Text = ds.Tables[0].Rows[0]["Fiscal_End_Dt_for_COA"].ToString();
                txtFS_DateCOA.Text = ds.Tables[0].Rows[0]["Fiscal_Start_Dt_for_COA"].ToString();

                dml.dateConvert(txtFS_DateCOA);
                dml.dateConvert(txtFEnd_DateCOA);


             //   F_SatrtDateForCoA();
             //   F_EndDateForCoA();

                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (g_c == true)
                {
                    rdbCompanny.Checked = true;
                }
                else
                {
                    rdbGoc.Checked = true;
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

    public void formatsegment()
    {
        userid = Request.QueryString["UserID"];
        string compid = "";
        string gocid = "";
        DataSet ds = dml.Find("select compid, GOCId,UseCompanyCOA from SET_Company where CompName='" + Request.Cookies["compNAme"].Value + "'");
        if (ds.Tables[0].Rows.Count > 0)
        {
             compid = ds.Tables[0].Rows[0]["compid"].ToString();
             gocid = ds.Tables[0].Rows[0]["GOCId"].ToString();

            compidinsert = compid;
            gocidinsert = gocid;
        }
        DataSet ds_goc = dml.Find("select (select CoaFormat from SET_GOC where GocId = "+gocid+") as CoaFormat , (select NoofCoaSegments from SET_GOC where GocId = " + gocid + ") as noofseg , (select DiffCoaForBranches from SET_GOC where GocId = " + gocid + ") as diff from  SET_User_Permission_CompBrYear where userid = convert(uniqueidentifier, '"+ userid + "')");
        DataSet ds_comp = dml.Find("select (select UseCompanyCOA from SET_Company where CompId = " + compid + ") as compcoa ,(select Noof_COA_Segments from SET_Company where CompId = " + compid + ") as noofsegcomp ,(select CoaFormat from SET_Company where CompId = "+compid+") as segformat  from  SET_User_Permission_CompBrYear where userid = convert(uniqueidentifier, '"+ userid + "')");

        string diffcoa = ds_goc.Tables[0].Rows[0]["diff"].ToString();
        string usecompcoa = ds_comp.Tables[0].Rows[0]["compcoa"].ToString();

        if(diffcoa == "N" && usecompcoa == "N")
        {
            txtNoOFSEG.Text = ds_goc.Tables[0].Rows[0]["noofseg"].ToString();
            lblCOA_Template.Text = ds_goc.Tables[0].Rows[0]["CoaFormat"].ToString();
            rdbGoc.Checked = true;
        }
        else if (diffcoa == "Y" && usecompcoa == "Y")
        {
            txtNoOFSEG.Text = ds_comp.Tables[0].Rows[0]["noofsegcomp"].ToString();
            lblCOA_Template.Text = ds_comp.Tables[0].Rows[0]["segformat"].ToString();
            rdbCompanny.Checked = true;
            rdbGoc.Enabled = true;
            rdbCompanny.Enabled = true;
        }
        else if (diffcoa == "Y" && usecompcoa == "N")
        {
            txtNoOFSEG.Text = ds_goc.Tables[0].Rows[0]["noofseg"].ToString();
            lblCOA_Template.Text = ds_goc.Tables[0].Rows[0]["CoaFormat"].ToString();
            rdbGoc.Checked = true;
        }
        else if (diffcoa == "N" && usecompcoa == "Y")
        {
            txtNoOFSEG.Text = ds_goc.Tables[0].Rows[0]["noofseg"].ToString();
            lblCOA_Template.Text = ds_goc.Tables[0].Rows[0]["CoaFormat"].ToString();
            rdbGoc.Checked = true;
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
        textClear();

        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {
           
            lblCoaMAsterSerial.Text = GridView1.SelectedRow.Cells[1].Text;
            DataSet ds = dml.Find("select * from SET_COA_Master where COA_M_ID = "+ lblCoaMAsterSerial.Text + "  and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtEntry_Date.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtNoOFSEG.Text = ds.Tables[0].Rows[0]["NoofSegments"].ToString();
                lblCOA_Template.Text = ds.Tables[0].Rows[0]["Template"].ToString();
                DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["SysDate"].ToString());
                lblUSerNAme.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["User"].ToString());
                txtNarration.Text = ds.Tables[0].Rows[0]["Narration"].ToString();
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["Active"].ToString());
                bool g_c = bool.Parse(ds.Tables[0].Rows[0]["Goc_Comp_Coa"].ToString());
                dml.dateConvert(txtEntry_Date);
                lblSystem_Date.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                //Fiscal_End_Dt_for_COA

                txtFEnd_DateCOA.Text = ds.Tables[0].Rows[0]["Fiscal_End_Dt_for_COA"].ToString();
                txtFS_DateCOA.Text = ds.Tables[0].Rows[0]["Fiscal_Start_Dt_for_COA"].ToString();

                dml.dateConvert(txtFS_DateCOA);
                dml.dateConvert(txtFEnd_DateCOA);
             //   F_SatrtDateForCoA();
             //   F_EndDateForCoA();
                if( chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (g_c == true)
                {
                    rdbCompanny.Checked = true;
                }
                else
                {
                    rdbGoc.Checked = true;
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
        textClear();

        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        Deletebox.Visible = false;
        try
        {

            lblCoaMAsterSerial.Text = GridView2.SelectedRow.Cells[1].Text;
            DataSet ds = dml.Find("select * from SET_COA_Master where COA_M_ID = " + lblCoaMAsterSerial.Text + "  and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtEntry_Date.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtNoOFSEG.Text = ds.Tables[0].Rows[0]["NoofSegments"].ToString();
                lblCOA_Template.Text = ds.Tables[0].Rows[0]["Template"].ToString();
                DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["SysDate"].ToString());
                lblUSerNAme.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["User"].ToString());
                txtNarration.Text = ds.Tables[0].Rows[0]["Narration"].ToString();
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["Active"].ToString());
                bool g_c = bool.Parse(ds.Tables[0].Rows[0]["Goc_Comp_Coa"].ToString());
                dml.dateConvert(txtEntry_Date);
                lblSystem_Date.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");



                txtFEnd_DateCOA.Text = ds.Tables[0].Rows[0]["Fiscal_End_Dt_for_COA"].ToString();
                txtFS_DateCOA.Text = ds.Tables[0].Rows[0]["Fiscal_Start_Dt_for_COA"].ToString();

                dml.dateConvert(txtFS_DateCOA);
                dml.dateConvert(txtFEnd_DateCOA);

               // F_SatrtDateForCoA();
              //  F_EndDateForCoA();
                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (g_c == true)
                {
                    rdbCompanny.Checked = true;
                }
                else
                {
                    rdbGoc.Checked = true;
                }
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

    public bool COmp_GOC_ID()
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
        DataSet ds_goc = dml.Find("select (select CoaFormat from SET_GOC where GocId = " + gocid + ") as CoaFormat , (select NoofCoaSegments from SET_GOC where GocId = " + gocid + ") as noofseg , (select DiffCoaForBranches from SET_GOC where GocId = " + gocid + ") as diff from  SET_User_Permission_CompBrYear where userid = convert(uniqueidentifier, '" + userid + "')");
        DataSet ds_comp = dml.Find("select (select UseCompanyCOA from SET_Company where CompId = " + compid + ") as compcoa ,(select Noof_COA_Segments from SET_Company where CompId = " + compid + ") as noofsegcomp ,(select CoaFormat from SET_Company where CompId = " + compid + ") as segformat  from  SET_User_Permission_CompBrYear where userid = convert(uniqueidentifier, '" + userid + "')");

        string diffcoa = ds_goc.Tables[0].Rows[0]["diff"].ToString();
        string usecompcoa = ds_comp.Tables[0].Rows[0]["compcoa"].ToString();

        if (diffcoa == "N" && usecompcoa == "N")
        {
            if(rdbGoc.Checked == true)
            {
                             
                return true;
            }
            else
            {
                
                return false;
            }

        }
        else if (diffcoa == "Y" && usecompcoa == "Y")
        {
            if (rdbCompanny.Checked == true)
            {
              //  Label1.Text = "Update";
                return true;
            }
            else
            {
               // Label1.Text = "Wrongentry";
                return false;
            }
        }
        else if (diffcoa == "Y" && usecompcoa == "N")
        {
            if (rdbGoc.Checked == true)
            {
                //  Label1.Text = "Update";
                return true;
            }
            else
            {
               return false;
            }
        }
        else if(diffcoa == "N" && usecompcoa == "Y")
        {
            if (rdbCompanny.Checked == true)
            {
                //  Label1.Text = "Update";
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            // Label1.Text = "Wrong Entry";
             return false;

        }

    }

    public bool validCompid()
    {
        
        int Comp_Goc = 0;
        userid = Request.QueryString["UserID"];
        string compid = "";
        string gocid = "";
        DataSet ds = dml.Find("select compid, GOCId , UseCompanyCOA from SET_Company where CompName='" + Request.Cookies["compNAme"].Value + "'");
        if (ds.Tables[0].Rows.Count > 0)
        {
            compid = ds.Tables[0].Rows[0]["compid"].ToString();
            gocid = ds.Tables[0].Rows[0]["GOCId"].ToString();

            if(COmp_GOC_ID() == true)
            {
                Comp_Goc = 1;
            }
            DataSet ds1 = dml.Find("SELECT * FROM SET_COA_Master WHERE CompId = "+compid+" AND  GocId = "+gocid+" AND Goc_Comp_Coa = "+ Comp_Goc + " and Fiscal_Start_Dt_for_COA = '"+txtFS_DateCOA.Text+"'");

            if (ds1.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        else
        {
            return false;
        }
       

        
    }

   public void entrydaterange()
    {
        string st = Request.QueryString.Get("fiscaly");
        DataSet dds = dml.Find("select StartDate, EndDate from SET_Fiscal_Year where Description = '" + st+"'");

        string startdate = dds.Tables[0].Rows[0]["StartDate"].ToString();
        string Enddate = dds.Tables[0].Rows[0]["EndDate"].ToString();

        CalendarExtender2.StartDate = DateTime.Parse(startdate);
        CalendarExtender2.EndDate = DateTime.Parse(Enddate);

      
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
    public void F_SatrtDateForCoA()
    {
        string st = Request.QueryString.Get("fiscaly");
        DataSet dds = dml.Find("Select Fiscal_Start_Dt_for_COA from SET_COA_Master where CompId = " + compid() + " and gocid = " + gocid() + " ");
        if (dds.Tables[0].Rows.Count > 0)
        {
            string startdate = dds.Tables[0].Rows[0]["Fiscal_Start_Dt_for_COA"].ToString();
            txtFS_DateCOA.Text = dml.dateConvert(startdate);
        }
    }
    public void F_EndDateForCoA()
    {
        string st = Request.QueryString.Get("fiscaly");
        DataSet dds = dml.Find("Select Fiscal_End_Dt_for_COA from SET_COA_Master where CompId = " + compid() + " and gocid = " + gocid() +" and Goc_Comp_Coa = 1");
        if (dds.Tables[0].Rows.Count > 0)
        {
            string Enddate = dds.Tables[0].Rows[0]["Fiscal_End_Dt_for_COA"].ToString();
            if (Enddate != "")
            {
                txtFEnd_DateCOA.Text = dml.dateConvert(Enddate);
            }
        }
    }

    protected int FindDate(string startdate11, string enddate11)
    {
        int count = 0;
        string StartdateFY = "", EndDateFY = "",fiscalid;
        string StartdateMaster = "", EndDateMaster = "";

        string fiscal = Request.QueryString.Get("fiscaly");
        DataSet ds = dml.Find("select FiscalYearID,StartDate,EndDate from SET_Fiscal_Year where Description = '" + fiscal + "'");
        if (ds.Tables[0].Rows.Count > 0)
        {
            // select Fiscal_Start_Dt_for_COA,Fiscal_End_Dt_for_COA,Goc_Comp_Coa from SET_COA_Master where FiscalYearID = '"++"' and gocid='" + gocid() + "' and compid = '" + compid() + "'
           
            StartdateFY = dml.dateconvertString(ds.Tables[0].Rows[0]["StartDate"].ToString());
            EndDateFY = dml.dateconvertString(ds.Tables[0].Rows[0]["EndDate"].ToString());
            fiscalid = ds.Tables[0].Rows[0]["FiscalYearID"].ToString();

           
           
                DateTime time = DateTime.Parse(startdate11);
            DateTime time_2 = DateTime.Parse(enddate11);
            DateTime startDate = DateTime.Parse( StartdateFY);
                DateTime endDate = DateTime.Parse(EndDateFY);

                int time1 = int.Parse(time.Year.ToString());
                int time2 = int.Parse(time_2.Year.ToString());
            int startyear = int.Parse(startDate.Year.ToString());
                int endyear = int.Parse(endDate.Year.ToString());

                if ( time1 >= startyear && time2 <= endyear )
                {
                    count = count + 1;
                   // ViewState["comp_Goc"] = dsCoa.Tables[0].Rows[i]["Goc_Comp_Coa"].ToString();
                }
                else
                {
                    count = 0;
                }
            }
        
        return count;
        
    }


   
}