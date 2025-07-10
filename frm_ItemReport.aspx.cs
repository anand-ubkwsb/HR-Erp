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
    ExcelLibrary library = new ExcelLibrary();
    int DateFrom, EditDays, DeleteDays, AddDays;
    string userid, UserGrpID, FormID, fiscal;
    DmlOperation dml = new DmlOperation();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
    FieldHide fd = new FieldHide();
    Report rpt = new Report();
    protected void Page_Load(object sender, EventArgs e) 
    {
      
        if (Page.IsPostBack == false)
        {
            rdbLocal.Checked = true;
            dml.dropdownsql(ddlItemTypes, "SET_ItemType", "Description", "ItemTypeID", "Record_Deleted", "0");
            dml.dropdownsql(ddlItemsHead, "SET_ItemHead", "ItemHeadName", "ItemHeadID", "Record_Deleted", "0");
            dml.dropdownsql(ddlItemSubHead, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID", "Record_Deleted", "0");

        }
    }

    protected void btnGenerateExcel_Click(object sender, EventArgs e) {
        string type, typehead, typesubhead, localimp;
        if (ddlItemTypes.SelectedIndex == 0)
        {
            type = "0";
        }
        else
        {
            type = ddlItemTypes.SelectedItem.Text;
        }
        if (ddlItemsHead.SelectedIndex == 0)
        {
            typehead = "0";
        }
        else
        {
            typehead = ddlItemsHead.SelectedItem.Text;
        }
        if (ddlItemSubHead.SelectedIndex == 0)
        {
            typesubhead = "0";
        }
        else
        {
            typesubhead = ddlItemSubHead.SelectedItem.Text;
        }
        if (rdbLocal.Checked == true)
        {
            localimp = "0";
        }
        else
        {
            localimp = "1";
        }
        if (rdbImport.Checked == true)
        {
            localimp = "import";
        }
        else
        {
            localimp = "local";
        }
        library.WriteExcelFile("Select * From View_rpt_Item_Master where itemtype='" + type + "' and itemSubHeadName='" + typesubhead + "' and Local_Import='" + localimp + "'",type+" Report");
        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertFileGenerated()", true);
        //Response.Redirect("~/Reportsform/itemreportMaster.aspx?Itemtype='" + Server.UrlEncode(type) + "'&ItemHead='" + Server.UrlEncode(typehead) + "'&localimp='" + localimp + "'");


    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        string type, typehead, typesubhead , localimp;
        if (ddlItemTypes.SelectedIndex == 0)
        {
            type = "0";
        }
        else
        {
            type = ddlItemTypes.SelectedItem.Text;
        }
        if (ddlItemsHead.SelectedIndex == 0)
        {
            typehead = "0";
        }
        else
        {
            typehead = ddlItemsHead.SelectedItem.Text;
        }
        if (ddlItemSubHead.SelectedIndex == 0)
        {
            typesubhead = "0";
        }
        else
        {
            typesubhead = ddlItemSubHead.SelectedItem.Text;
        }
        if(rdbLocal.Checked == true)
        {
            localimp = "0";
        }
        else
        {
            localimp = "1";
        }
        if (rdbImport.Checked == true)
        {
            localimp = "import";
        }
        else
        {
            localimp = "local";
        }


        Response.Redirect("~/Reportsform/itemreportMaster.aspx?Itemtype='" + Server.UrlEncode(type) + "'&ItemHead='" + Server.UrlEncode(typehead) + "'&localimp='"+localimp+"'");
        

    }
   
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlItemTypes.SelectedIndex = 0;
        ddlItemsHead.SelectedIndex = 0;
        ddlEdit_ItemSubHead.SelectedIndex = 0;
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
    
  
  
       
    



    protected void ddlItemTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        dml.dropdownsql(ddlItemsHead, "SET_ItemHead", "ItemHeadName", "ItemHeadID", "ItemTypeID", ""+ddlItemTypes.SelectedItem.Value+"");
    }

    protected void ddlItemsHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        dml.dropdownsql(ddlItemSubHead, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID", "ItemHeadID", ddlItemsHead.SelectedItem.Value);
    }

  
}