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
    int AddDays, EditDays, DeleteDays, DateFrom;
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

            dml.dropdownsql(txtEdit_UserGrpName, "SET_UserGrp", "UserGrpName", "UserGrpId", "UserGrpName");
            dml.dropdownsql(txtFind_UserGrpName, "SET_UserGrp", "UserGrpName", "UserGrpId", "UserGrpName");
            dml.dropdownsql(txtDel_UserGrpName, "SET_UserGrp", "UserGrpName", "UserGrpId", "UserGrpName");

            btnDelete_after.Visible = false;
            Showall_Dml();
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
        try
        {
            userid = Request.QueryString["UserID"];
            UserGrpID = Request.QueryString["UsergrpID"];
            FormID = Request.QueryString["FormID"];
            string FiscalY = Request.QueryString["fiscaly"];
            int chk = 0;
            if (chkActive_Status.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }


            dml.Insert("INSERT INTO SET_UserGrp ([UserGrpName], [Description], [SysDate], [IsActive]) VALUES ('" + txtUserGrpName.Text + "', '" + txtdescription.Text + "', '" + DateTime.Now + "', '" + chk + "');", "");
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "alertme()", true);

            btnInsert.Visible = true;
            btnDelete.Visible = true;
            btnUpdate.Visible = false;
            btnEdit.Visible = true;
            btnFind.Visible = true;
            btnSave.Visible = false;
            btnDelete_after.Visible = false;
            btnInsert_Click(sender, e);
            //Response.Redirect("frm_SET_UserGRP.aspx?UserID="+userid+"&UsergrpID="+UserGrpID+"&fiscaly="+ FiscalY + "&FormID="+FormID+"");


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

        chkActive_Status.Checked = true;
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Deletebox.Visible = false;
        Editbox.Visible = false;
        chkActive_Status.Checked = true;

        txtUserGrpName.Enabled = true;
        txtdescription.Enabled = true;
        lblSystemDate.Enabled = true;
        chkActive_Status.Enabled = true;
        lblSystemDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff ");

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
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

            DataSet ds_up = dml.Find("select * from SET_UserGrp WHERE ([Sno]='" + ViewState["Sno"].ToString() + "') AND ([UserGrpName]='" + txtUserGrpName.Text + "') AND ([Description]='" + txtdescription.Text + "') AND ([IsActive]='" + chk + "')");

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


                dml.Update("UPDATE SET_UserGrp SET [UserGrpName]='" + txtUserGrpName.Text + "', [Description]='" + txtdescription.Text + "', [SysDate]='" + DateTime.Now + "', [IsActive]='" + chk + "' where Sno = " + ViewState["Sno"].ToString() + "", "");
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
            dml.Delete("update SET_UserGrp set Record_Deleted = 1 where Sno =  " + ViewState["Sno"].ToString() + "", "");
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

        UserGrpID = Request.QueryString["UsergrpID"];
        FormID = Request.QueryString["FormID"];
        Showall_Dml();
    }

    protected void btnSearchEdit_Click(object sender, EventArgs e)
    {
        btnSave.Visible = false;
        btnEdit.Visible = true;
        btnFind.Visible = false;
        btnCancel.Visible = true;
        btnDelete.Visible = false;
        btnInsert.Visible = false;
        btnUpdate.Visible = false;
        try
        {
            string swhere;
            UserGrpID = Request.QueryString["UsergrpID"];

            string squer = "select * from SET_UserGrp";

            if (txtEdit_UserGrpName.SelectedIndex != 0)
            {
                swhere = "UserGrpName = '" + txtEdit_UserGrpName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "UserGrpName is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY Sno";

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
        btnSave.Visible = false;
        btnEdit.Visible = false;
        btnFind.Visible = true;
        btnCancel.Visible = true;
        btnDelete.Visible = false;
        btnInsert.Visible = false;
        btnUpdate.Visible = false;
        try
        {
            string swhere;
            UserGrpID = Request.QueryString["UsergrpID"];

            string squer = "select * from SET_UserGrp";

            if (txtFind_UserGrpName.SelectedIndex != 0)
            {
                swhere = "UserGrpName = '" + txtFind_UserGrpName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "UserGrpName is not null";
            }
            if (ChkFind_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (ChkFind_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }
            else
            {
                swhere = swhere + " and IsActive is not null";
            }
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY Sno";

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
        try
        {
            string swhere;
            UserGrpID = Request.QueryString["UsergrpID"];

            string squer = "select * from SET_UserGrp";

            if (txtDel_UserGrpName.SelectedIndex != 0)
            {
                swhere = "UserGrpName = '" + txtDel_UserGrpName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "UserGrpName is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY Sno";

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

        Label1.Text = "";
        txtdescription.Text = "";
        lblSystemDate.Text = "";
        txtUserGrpName.Text = "";

        chkActive_Status.Checked = false;
        txtUserGrpName.Enabled = false;
        txtdescription.Enabled = false;
        lblSystemDate.Enabled = false;
        chkActive_Status.Enabled = false;
    }


    public string show_username()
    {
        userid = Request.QueryString["UserID"];
        DataSet ds_autoUserName = dml.Find("select user_name from SET_User_Manager where UserId='" + userid + "'");
        return ds_autoUserName.Tables[0].Rows[0]["user_name"].ToString();

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
            ViewState["Sno"] = serialno;
            DataSet ds = dml.Find("Select * from SET_UserGrp where Sno = " + serialno);
            //
            if (ds.Tables[0].Rows.Count > 0)

            {

                txtUserGrpName.Text = ds.Tables[0].Rows[0]["UserGrpName"].ToString();
                txtdescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();

                DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["SysDate"].ToString());

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());

                lblSystemDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

                if (chk == true)
                {
                    chkActive_Status.Checked = true;
                }
                else
                {
                    chkActive_Status.Checked = false;
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
            string serialno = GridView2.SelectedRow.Cells[1].Text;
            ViewState["Sno"] = serialno;
            DataSet ds = dml.Find("Select * from SET_UserGrp where Sno = " + serialno);
            //
            if (ds.Tables[0].Rows.Count > 0)

            {

                txtUserGrpName.Text = ds.Tables[0].Rows[0]["UserGrpName"].ToString();
                txtdescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();

                DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["SysDate"].ToString());

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());

                lblSystemDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

                if (chk == true)
                {
                    chkActive_Status.Checked = true;
                }
                else
                {
                    chkActive_Status.Checked = false;
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

        chkActive_Status.Enabled = true;
        chkActive_Status.Checked = true;
        txtUserGrpName.Enabled = true;
        txtdescription.Enabled = true;
        lblSystemDate.Enabled = true;

        lblSystemDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff ");

        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {
            string serialno = GridView3.SelectedRow.Cells[1].Text;
            ViewState["Sno"] = serialno;
            DataSet ds = dml.Find("Select * from SET_UserGrp where Sno = " + serialno);
            //
            if (ds.Tables[0].Rows.Count > 0)

            {

                txtUserGrpName.Text = ds.Tables[0].Rows[0]["UserGrpName"].ToString();
                txtdescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();

                DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["SysDate"].ToString());

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());

                lblSystemDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

                if (chk == true)
                {
                    chkActive_Status.Checked = true;
                }
                else
                {
                    chkActive_Status.Checked = false;
                }

            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }

    }

    public void daterangeforfiscal(AjaxControlToolkit.CalendarExtender cal1)
    {
        DataSet ds = dml.Find("select StartDate, EndDate from SET_Fiscal_Year where Description = '" + Request.Cookies["fiscalYear"].Value + "'");
        if (ds.Tables[0].Rows.Count > 0)
        {
            string startdate1 = ds.Tables[0].Rows[0]["StartDate"].ToString();
            string enddate1 = ds.Tables[0].Rows[0]["EndDate"].ToString();

            DateTime startdate = DateTime.Parse(startdate1);
            DateTime enddate = DateTime.Parse(enddate1);

            cal1.StartDate = startdate;
            cal1.EndDate = enddate;


        }
    }


    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string id = e.Row.Cells[1].Text;
        bool flag = dml.isNumber(id);


        if (flag == true)
        {

            DataSet ds = dml.Find("select Sno ,MLD from SET_UserGrp where Sno = '" + id + "'");
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

            DataSet ds = dml.Find("select Sno,MLD from SET_UserGrp where Sno = '" + id + "'");
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