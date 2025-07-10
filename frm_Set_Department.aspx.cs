using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frm_Period : System.Web.UI.Page
{
    int DateFrom, EditDays, DeleteDays, AddDays;
    string userid, UserGrpID, FormID,fiscal;
    DmlOperation dml = new DmlOperation();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
    FieldHide fd = new FieldHide();
    protected void Page_Load(object sender, EventArgs e)
    {
        CalendarExtender1.EndDate = DateTime.Now;
        if (Page.IsPostBack == false)
        {
            btnUpdate.Visible = false;
            btnSave.Visible = false;
            btnDelete_after.Visible = false;
            userid = Request.QueryString["UserID"];
            UserGrpID = Request.QueryString["UsergrpID"];
            FormID = Request.QueryString["FormID"];
            fiscal = Request.QueryString["fiscaly"];

            dml.daterangeforfiscal(CalendarExtender1, Request.Cookies["fiscalyear"].Value);
            txtEntryDate.Attributes.Add("readonly", "readonly");

            dml.dropdownsql(txtEdit_DepartName, "SET_Department", "DepartmentName", "DepartmentID");
            dml.dropdownsql(txtFind_Departname, "SET_Department", "DepartmentName", "DepartmentID");
            dml.dropdownsql(txtDelete_DepartName, "SET_Department", "DepartmentName", "DepartmentID");
            
            fd.HideUSerGrpField(Page.Controls, UserGrpID, FormID, "SET_Department");
           
            Showall_Dml();
            textClear();
            Findbox.Visible = false;
            DeleteBox.Visible = false;
            Editbox.Visible = false;
        }
    }

   
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string type, typehead, level;
        
            type = "0";
            level = "0";


        Response.Redirect("~/Reportsform/country_rpt.aspx?Itemtype='" + Server.UrlEncode(type) + "'&headlevel='" + level + "'");



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
    
        CalendarExtender1.EndDate = DateTime.Now;
        txtDeparment.Enabled = true;
        txtEntryDate.Enabled = true;
        lblSysdate.Enabled = true;
        lblUserName.Enabled = true;
        lblSysdate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
        chkActive.Enabled = true;
        chkActive.Checked = true;
        chkwarehouse.Enabled = true;
        chkwarehouse.Checked = true;
        lblLocation.Enabled = true;
        txtLocationDesc.Enabled = true;
        txtLoctaionName.Enabled = true;
        imgPopup.Enabled = true;

        txtEntryDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");

        userid = Request.QueryString["UserID"];
        DataSet ds_autoUserName = dml.Find("select user_name from SET_User_Manager where UserId='" + userid + "'");
        lblUserName.Text = ds_autoUserName.Tables[0].Rows[0]["user_name"].ToString();

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        userid = Request.QueryString["UserID"];
        UserGrpID = Request.QueryString["UsergrpID"];
        FormID = Request.QueryString["FormID"];
        fiscal = Request.QueryString["fiscaly"];
        try {
            txtEntryDate.ReadOnly = false;
            int chk;
            int chkware;
            if (chkActive.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
            if (chkwarehouse.Checked == true)
            {
                chkware = 1;
            }
            else
            {
                chkware = 0;
            }
            string il1, il2;
            if(txtLoctaionName.Text != "")
            {
                il1 = "'" + txtLoctaionName.Text + "'";
            }
            else
            {
                il1 = "NULL";
            }
            if (txtLocationDesc.Text != "")
            {
                il2 = "'" + txtLocationDesc.Text + "'";
            }
            else
            {
                il2 = "NULL";
            }

            dml.Insert("INSERT INTO [SET_Department] ([DepartmentName], [IsActive],  [EntryDate], [SysDate], [User],[Record_Deleted],[IsWarehouse],[LocationName],[LocationDescription],[MLD]) VALUES ('"+txtDeparment.Text+ "', '"+chk+ "', '"+dml.dateconvertforinsert(txtEntryDate)+ "', '"+DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")+ "', '"+lblUserName.Text+"','0','"+chkware+"', "+il1+ " , "+il2+",'"+dml.Encrypt("h")+"');", "alertme()");
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);

            txtEntryDate.ReadOnly = true;
            txtLoctaionName.Text = "";
            txtLocationDesc.Text = "";
            Label1.Text = "";
            txtDeparment.Text = "";
            txtEntryDate.Text = "";
            lblSysdate.Text = "";
            lblUserName.Text = "";
            chkActive.Checked = true;
            chkwarehouse.Checked = true;
            FormID = Request.QueryString["FormID"];
            Showall_Dml();
            //Response.Redirect("frm_Set_Department.aspx?UserID="+userid+"&UsergrpID="+UserGrpID+"&fiscaly="+fiscal+"&FormID="+FormID+"");
        }
        catch(Exception ex)
        {
            Label1.Text = ex.Message;
        }

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try {
            string date = txtEntryDate.Text;
            int chk = 0;
            if (chkActive.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
            int chkware;
            if (chkwarehouse.Checked == true)
            {
                chkware = 1;
            }
            else
            {
                chkware = 0;
            }

            string l1, l2,ul1,ul2;
            l1 = txtLoctaionName.Text;
            l2 = txtLocationDesc.Text;
            if(txtLoctaionName.Text != "")
            {
                l1 = "([LocationName] = '" + txtLoctaionName.Text + "')";
                ul1 = "LocationName = '" + txtLoctaionName.Text + "'";

            }
            else
            {
                l1 = "([LocationName] is NULL)";
                ul1 = "LocationName = NULL";
            }
            if (txtLocationDesc.Text != "")
            {
                l2 = "([LocationDescription] = '" + txtLocationDesc.Text + "')";
                ul2 = "LocationDescription = '" + txtLocationDesc.Text + "'";


            }
            else
            {

                l2 = "([LocationDescription] is NULL)";
                ul2 = "LocationDescription = NULL";
            }

            DataSet ds_up = dml.Find("select * from SET_Department  WHERE ([DepartmentID]='"+ViewState["SNO"].ToString()+"') AND ([DepartmentName]='"+txtDeparment.Text+"') AND ([IsActive]='"+chk+"') AND ([EntryDate]='"+dml.dateconvertforinsert(txtEntryDate)+"') AND  ([IsWarehouse]='"+chkware+"') AND "+l1+" AND "+l2+";");

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
                FormID = Request.QueryString["FormID"];
                Showall_Dml();
            }
            else {

                userid = Request.QueryString["UserID"];
                dml.Update("UPDATE [SET_Department] SET [DepartmentName]='" + txtDeparment.Text + "', [IsActive]='" + chk + "', [EntryDate]='" + dml.dateconvertString(date) + "', [SysDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', [User]='" + lblUserName.Text + "',[IsWarehouse]='" + chkware + "', " + ul1 + " ," + ul2 + " WHERE ([DepartmentID]='" + ViewState["SNO"].ToString() + "')", "");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", " Editalert()", true);

                textClear();
                btnInsert.Visible = true;
                btnDelete.Visible = true;
                btnUpdate.Visible = false;
                btnEdit.Visible = true;
                btnFind.Visible = true;
                btnSave.Visible = false;
                btnDelete_after.Visible = false;
                FormID = Request.QueryString["FormID"];
                Showall_Dml();
            }
        }
        catch(Exception ex)
        {

        }
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
            GridView2.DataBind();
            string swhere;
            string squer = "Select * from SET_Department";

            if (txtDelete_DepartName.SelectedIndex != 0)
            {
                swhere = "DepartmentName = '" + txtDelete_DepartName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "DepartmentName is not null";
            }
            if (txtDelete_Location.Text != "")
            {
                swhere = swhere + " and LocationName like '" + txtDelete_Location.Text + "%'";
            }
            else
            {
                if (chkDelete_Isware.Checked == true)
                {
                    swhere = swhere + " and LocationName is not null";
                }
                else if (chkDelete_Isware.Checked == false)
                {
                    swhere = swhere + " and LocationName is null";
                }
            }
            if (chkDelete_Isware.Checked == true)
            {
                swhere = swhere + " and IsWarehouse = '1'";
            }
            else if (chkDelete_Isware.Checked == false)
            {
                swhere = swhere + " and IsWarehouse = '0'";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0  ORDER BY DepartmentName";


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
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        txtDeparment.Enabled = false;
        txtEntryDate.Enabled = false;
        lblSysdate.Enabled = false;
        lblUserName.Enabled = false;
        chkActive.Enabled = false;


        btnCancel.Visible = true;
        btnFind.Visible = true;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnInsert.Visible = false;

        GridView1.DataBind();
        string swhere;
        string squer = "Select * from SET_Department";

        if (txtFind_Departname.SelectedIndex != 0)
        {
            swhere = "DepartmentName = '" + txtFind_Departname.SelectedItem.Text + "'";
        }
        else
        {
            swhere = "DepartmentName is not null";
        }
        if (txtFind_Location.Text != "")
        {
            swhere = swhere + " and LocationName like '" + txtFind_Location.Text + "%'";
        }
        else
        {
            if (chkFind_IswareHouse.Checked == true)
            {
                swhere = swhere + " and LocationName is not null";
            }
            else if (chkFind_IswareHouse.Checked == false)
            {
                swhere = swhere + " and LocationName is null";
            }
        }
        if (chkFind_IswareHouse.Checked == true)
        {
            swhere = swhere + " and IsWarehouse = '1'";
        }
        else if (chkFind_IswareHouse.Checked == false)
        {
            swhere = swhere + " and IsWarehouse = '0'";
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
        squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY DepartmentName";

        Findbox.Visible = true;
        fieldbox.Visible = false;

        DataSet dgrid = dml.grid(squer);
        GridView1.DataSource = dgrid;
        GridView1.DataBind();





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
        btnDelete_after.Visible = false ;

        try
        {
            GridView3.DataBind();
            string swhere;
            string location_null = "LocationName is null";
            string squer = "Select * from SET_Department";

            if (txtEdit_DepartName.SelectedIndex != 0)
            {
                swhere = "DepartmentName = '" + txtEdit_DepartName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "DepartmentName is not null";
            }
            if (txtEdit_LocationName.Text != "")
            {
                swhere = swhere + " and LocationName like '" + txtEdit_LocationName.Text + "%'";
            }
            else
            {
                if (chkEdit_ISwareHouse.Checked == true)
                {
                    swhere = swhere + " and LocationName is not null";
                }
                else if (chkEdit_ISwareHouse.Checked == false)
                {
                    swhere = swhere + " and LocationName is null";
                }
            }
            if (chkEdit_ISwareHouse.Checked == true)
            {
                swhere = swhere + " and IsWarehouse = '1'";
            }
            else if (chkEdit_ISwareHouse.Checked == false)
            {
                swhere = swhere + " and IsWarehouse = '0'";
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
        
            
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY DepartmentName";

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
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", " nodatafound()", true);
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", " wrong()", true);
        }
    }
    public void textClear()
    {
        DeleteBox.Visible = false;
        Editbox.Visible = false;
        Findbox.Visible = false;
        fieldbox.Visible = true;

        txtDeparment.Enabled = false;
        txtEntryDate.Enabled = false;
        lblSysdate.Enabled = false;
        lblUserName.Enabled = false;
        chkActive.Enabled = false;
        chkwarehouse.Enabled = false;
        lblLocation.Enabled = false;
        txtLoctaionName.Enabled = false;
        txtLocationDesc.Enabled = false;
        imgPopup.Enabled = false;

        lblLocation.Text = "";
        txtLoctaionName.Text = "";
        txtLocationDesc.Text = "";
        Label1.Text = "";
        txtDeparment.Text = "";
        txtEntryDate.Text = "";
        lblSysdate.Text = "";
        lblUserName.Text = "";
        chkActive.Checked = false;
        chkwarehouse.Checked = false;
    }
    public void Showall_Dml()
    {
        userid = Request.QueryString["UserID"];
        FormID = Request.QueryString["FormID"];
        DataSet FiscalStatus;
        con.Open();
        string Query = "SELECT * FROM SET_FISCAL_YEAR WHERE FISCALYEARID="+Convert.ToInt32(Request.Cookies["FiscalYearId"].Value);
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
        //    btnCancel.Visible = true;
        //}
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            dml.Delete("update SET_Department set Record_Deleted = 1 where DepartmentID = " + ViewState["SNO"].ToString() + "", "");
            textClear();
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "alertDelete()", true);

            btnInsert.Visible = true;
            btnDelete.Visible = true;
            btnUpdate.Visible = false;
            btnEdit.Visible = true;
            btnFind.Visible = true;
            btnSave.Visible = false;
            btnDelete_after.Visible = false;
            FormID = Request.QueryString["FormID"];
            Showall_Dml();

        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDeparment.Enabled = false;
        txtEntryDate.Enabled = false;
        lblSysdate.Enabled = false;
        lblUserName.Enabled = false;
        chkActive.Enabled = false;

        
        btnUpdate.Visible = false;
        btnInsert.Visible = false;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        Label1.Text = "";
        
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {
            string serial_id;
            serial_id = GridView1.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select * from  SET_Department where DepartmentID = '"+serial_id+"' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtDeparment.Text = ds.Tables[0].Rows[0]["DepartmentName"].ToString();
                txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                CalendarExtender1.EndDate = DateTime.Now;

                DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["SysDate"].ToString());
                lblUserName.Text = ds.Tables[0].Rows[0]["User"].ToString();

                txtLoctaionName.Text = ds.Tables[0].Rows[0]["LocationName"].ToString();
                txtLocationDesc.Text = ds.Tables[0].Rows[0]["LocationDescription"].ToString();

                if (txtLoctaionName.Text == "")
                {
                    txtLoctaionName.Text = ds.Tables[0].Rows[0]["DepartmentName"].ToString();
                }
                
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                bool ware = bool.Parse(ds.Tables[0].Rows[0]["IsWarehouse"].ToString());
                dml.dateConvert(txtEntryDate);

                lblSysdate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (ware == true)
                {
                    chkwarehouse.Checked = true;
                }
                else
                {
                    chkwarehouse.Checked = false;
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
            DataSet ds = dml.Find("select  * from  SET_Department where DepartmentID = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtDeparment.Text = ds.Tables[0].Rows[0]["DepartmentName"].ToString();
                txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();

                DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["SysDate"].ToString());
                lblUserName.Text = ds.Tables[0].Rows[0]["User"].ToString();
                txtLoctaionName.Text = ds.Tables[0].Rows[0]["LocationName"].ToString();
                txtLocationDesc.Text = ds.Tables[0].Rows[0]["LocationDescription"].ToString();

                if (txtLoctaionName.Text == "")
                {
                    txtLoctaionName.Text = ds.Tables[0].Rows[0]["DepartmentName"].ToString();
                }

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                bool ware = bool.Parse(ds.Tables[0].Rows[0]["IsWarehouse"].ToString());
                dml.dateConvert(txtEntryDate);

                lblSysdate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (ware == true)
                {
                    chkwarehouse.Checked = true;
                }
                else
                {
                    chkwarehouse.Checked = false;
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

        txtDeparment.Enabled = true;
        txtEntryDate.Enabled = true;
        lblSysdate.Enabled = true;
        lblUserName.Enabled = true;
        chkActive.Enabled = true;
        chkwarehouse.Enabled = true;
        txtLocationDesc.Enabled = true;
        txtLoctaionName.Enabled = true;
        imgPopup.Enabled = true;

        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {
            string serial_id;
            serial_id = GridView3.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select  * from  SET_Department where DepartmentID = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtDeparment.Text = ds.Tables[0].Rows[0]["DepartmentName"].ToString();
                txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                CalendarExtender1.EndDate = DateTime.Now;
                DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["SysDate"].ToString());
                lblUserName.Text = ds.Tables[0].Rows[0]["User"].ToString();
                txtLoctaionName.Text = ds.Tables[0].Rows[0]["LocationName"].ToString();
                if (txtLoctaionName.Text == "")
                {
                    txtLoctaionName.Text = ds.Tables[0].Rows[0]["DepartmentName"].ToString();
                }
                txtLocationDesc.Text = ds.Tables[0].Rows[0]["LocationDescription"].ToString();
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                bool ware = bool.Parse(ds.Tables[0].Rows[0]["IsWarehouse"].ToString());
                dml.dateConvert(txtEntryDate);

                lblSysdate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (ware == true)
                {
                    chkwarehouse.Checked = true;
                }
                else
                {
                    chkwarehouse.Checked = false;
                }
                chkwarehouse_CheckedChanged(sender, e);
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }

    }


    protected void chkwarehouse_CheckedChanged(object sender, EventArgs e)
    {
        if(chkwarehouse.Checked == true)
        {
            lblLocation.Text = txtDeparment.Text;
            txtLoctaionName.Enabled = true;
            txtLocationDesc.Enabled = true;
        }
        if (chkwarehouse.Checked == false)
        {
            txtLoctaionName.Enabled = false;
            txtLocationDesc.Enabled = false;
            lblLocation.Text = "";
            txtLoctaionName.Text = "";
            txtLocationDesc.Text = "";
        }
    }

    protected void txtDeparment_TextChanged(object sender, EventArgs e)
    {
        chkwarehouse_CheckedChanged(sender, e);
    }






    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string id = e.Row.Cells[1].Text;
        bool flag = dml.isNumber(id);


        if (flag == true)
        {

            DataSet ds = dml.Find("select DepartmentID,MLD from SET_Department where DepartmentID = '" + id + "'");
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

            DataSet ds = dml.Find("select DepartmentID,MLD from SET_Department where DepartmentID = '" + id + "'");
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