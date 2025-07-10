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
    ExcelLibrary library = new ExcelLibrary();
    int DateFrom, DeleteDays, EditDays, AddDays;
    string userid, UserGrpID, FormID, fiscal;
    DmlOperation dml = new DmlOperation();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
    FieldHide fd = new FieldHide();
    Report rpt = new Report();
    radcomboxclass cmb = new radcomboxclass();
    protected void Page_Load(object sender, EventArgs e) 
    {
      
        if (Page.IsPostBack == false)
        {
            rdbpur.Checked = true;
            //select BPartnerId,BPartnerName from SET_BusinessPartner
            dml.dropdownsql(ddlBusinessPartner, "SET_BusinessPartner", "BPartnerName", "BPartnerId", "Record_Deleted", "0");
            

        }
    }

    protected void btnGenerateExcel_Click(object sender, EventArgs e)
    {
        string code, buspart, typesubhead, pursale;
        if (RadComboItem_Code.Text == "")
        {
            code = "";
        }
        else
        {
            code = RadComboItem_Code.Text;
        }
        if (ddlBusinessPartner.SelectedIndex == 0)
        {
            buspart = "0";
        }
        else
        {
            buspart = ddlBusinessPartner.SelectedItem.Text;
        }

        if (rdbpur.Checked == true)
        {
            pursale = "0";
        }
        else
        {
            pursale = "1";
        }
        if (rdbsale.Checked == true)
        {
            pursale = "1";
        }
        else
        {
            pursale = "0";
        }
        if (!string.IsNullOrEmpty(code) && !buspart.Equals("0"))
        {
            library.WriteExcelFile("select * from view_rptItemMasterOpening where ItemCode='" + code + "' AND BPartnerName='" + buspart + "' AND RECORD_DELETED='0'", buspart + " Report");
        }
        else if (!string.IsNullOrEmpty(code)) {
            library.WriteExcelFile("select * from view_rptItemMasterOpening where ItemCode='" + code + "'AND RECORD_DELETED='0'", code+ " Report");
        }
        else {
            library.WriteExcelFile("select * from view_rptItemMasterOpening where RECORD_DELETED='0'", "ALL_BUSINESS_PARTNER_Report");
        }
        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertFileGenerated()", true);
    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        string code, buspart, typesubhead , pursale;
        if (RadComboItem_Code.Text == "")
        {
            code = "";
        }
        else
        {
            code = RadComboItem_Code.Text;
        }
        if (ddlBusinessPartner.SelectedIndex == 0)
        {
            buspart = "0";
        }
        else
        {
            buspart = ddlBusinessPartner.SelectedItem.Text;
        }
        
        if(rdbpur.Checked == true)
        {
            pursale = "0";
        }
        else
        {
            pursale = "1";
        }
        if (rdbsale.Checked == true)
        {
            pursale = "1";
        }
        else
        {
            pursale = "0";
        }
        //if(txtGLDate.Text != "")
        //{
        //    string ggdate = dml.dateconvertforinsert(txtGLDate);
        //    Response.Redirect("~/Reportsform/itemreportMasterOpenning.aspx?ItemCode='" + code + "'&BusPart='" + buspart + "'&pursale='" + pursale + "'&Gldate='" + ggdate + "'");
        //}
        //else
        //{
            Response.Redirect("~/Reportsform/itemreportMasterOpenning.aspx?ItemCode='" + code + "'&BusPart='" + buspart + "'&pursale='" + pursale + "'");
       // }


    }
   
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        RadComboItem_Code.Text = "";
        ddlBusinessPartner.SelectedIndex = 0;
        
    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {
       


    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
       
    }
    protected void btnSearchEdit_Click(object sender, EventArgs e)
    {
        
    }
    public void textClear()
    {
       

    }
    public void Showall_Dml()
    {
        userid = Request.QueryString["UserID"];
        FormID = Request.QueryString["FormID"];
        DataSet FiscalStatus;
        con.Open();
        string Query = "SELECT * FROM SET_FISCAL_YEAR WHERE FISCALYEARID=" + Convert.ToInt32(Request.Cookies["FiscalYearId"].Value);
        DataSet Fiscal = dml.Find(Query);
        string FiscalStatusQuery = "SELECT * FROM SET_FISCAL_YEAR_STATUS WHERE FISCALYEARSTATUSID=(SELECT FISCALYEARSTATUSID FROM SET_FISCAL_YEAR WHERE FISCALYEARID=" + Request.Cookies["FiscalYearId"].Value + ") AND RECORD_DELETED='0'";
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
                    //btnEdit.Visible = true;
                }
                else {
                    //btnEdit.Visible = false;
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
                    //btnDelete.Visible = true;
                }
                else {
                    //btnDelete.Visible = false;
                }

            }
            else
            {
                //btnInsert.Visible = false;
                //btnEdit.Visible = false;
                //btnDelete.Visible = false;
                //btnFind.Visible = true;
                //btnCancel.Visible = true;
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
            //btnInsert.Visible = false;
            //btnEdit.Visible = false;
            //btnFind.Visible = true;
            //btnDelete.Visible = false;
            //btnCancel.Visible = false;

        }
        con.Close();
    }
    public int gocid()
    {
        string gocid = Request.Cookies["GocId"].Value;
        return Convert.ToInt32(gocid);
    }



    protected void RadComboItem_Code_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        string where = "GocID = '" + gocid() + "' and Record_Deleted = '0'";

        cmb.serachcombo3(RadComboItem_Code, e.Text, "ItemCode", "ItemID", "Description", "Local_Import", "SET_ItemMaster", where, "ItemCode");

    }
}