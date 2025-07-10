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
    int DeleteDays, EditDays, AddDays, DateFrom;
    string userid, UserGrpID, FormID, fiscal;
    DmlOperation dml = new DmlOperation();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
    FieldHide fd = new FieldHide();
    Report rpt = new Report();
    protected void Page_Load(object sender, EventArgs e) 
    {
      
        if (Page.IsPostBack == false)
        {
            rdbMonthly.Checked = true;
            dml.dropdownsql(ddlItemTypes, "SET_Documents", "DocDescription", "DocID", "Record_Deleted", "0");
            

        }
    }



    protected void btnGenerateExcel_Click(object sender, EventArgs e) {

        string type, typehead, typesubhead, mon, year, m, y;
        if (ddlItemTypes.SelectedIndex == 0)
        {
            type = "0";
        }
        else
        {
            type = ddlItemTypes.SelectedItem.Text;
        }

        if (rdbMonthly.Checked == true)
        {
            mon = "1";
            m = "m";
            if (type.Equals("0")) {
                library.WriteExcelFile("Select * From Document_rpt where RECORD_DELETED='0'", "Document Reports");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertFileGenerated()", true);
            }
            else
            {
                library.WriteExcelFile("select * from Document_rpt where DocDescription = '" + type + "' and  MonthlyBase = '" + mon+ "' AND RECORD_DELETED='0'", type+" Report");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertFileGenerated()", true);
            }
        }
        //else
        //{
        //    m = "m";
        //    mon = "0";
        //    Response.Redirect("~/Reportsform/docreport.aspx?Itemtype='" + Server.UrlEncode(type) + "'&localimp='" + mon + "'&mon='" + m + "'");
        //}

        if (rdbYearly.Checked == true)
        {
            y = "y";
            year = "1";
            if (type.Equals("0")) {
                library.WriteExcelFile("select * from Document_rpt ", "Document Reports");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertFileGenerated()", true);
            }
            else
            {
                library.WriteExcelFile("select * from Document_rpt where DocDescription = '" + type + "' and  MonthlyBase = '" + year+ "' AND RECORD_DELETED='0'", type+" Report");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertFileGenerated()", true);
            }
        }

    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        string type, typehead, typesubhead, mon, year, m, y ;
        if (ddlItemTypes.SelectedIndex == 0)
        {
            type = "0";
        }
        else
        {
            type = ddlItemTypes.SelectedItem.Text;
        }
       
        if(rdbMonthly.Checked == true)
        {
            mon = "1";
            m = "m";
            Response.Redirect("~/Reportsform/docreport.aspx?Itemtype='" + Server.UrlEncode(type) + "'&localimp='" + mon + "'&mon='"+m+"'");
        }
        //else
        //{
        //    m = "m";
        //    mon = "0";
        //    Response.Redirect("~/Reportsform/docreport.aspx?Itemtype='" + Server.UrlEncode(type) + "'&localimp='" + mon + "'&mon='" + m + "'");
        //}

        if (rdbYearly.Checked == true)
        {
            y = "y";
            year = "1";
            Response.Redirect("~/Reportsform/docreport.aspx?Itemtype='" + Server.UrlEncode(type) + "'&localimp='" + year + "'&mon='" + y + "'");
        }
        //else
        //{
        //    y = "y";
        //    year = "0";
        //    Response.Redirect("~/Reportsform/docreport.aspx?Itemtype='" + Server.UrlEncode(type) + "'&localimp='" + year + "'&mon='" + y + "'");
        //}
      



    }
   
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlItemTypes.SelectedIndex = 0;
       
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
       
    }
    
  
  
       
    



    protected void ddlItemTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
   //     dml.dropdownsql(ddlItemsHead, "SET_ItemHead", "ItemHeadName", "ItemHeadID", "ItemTypeID", ""+ddlItemTypes.SelectedItem.Value+"");
    }

    protected void ddlItemsHead_SelectedIndexChanged(object sender, EventArgs e)
    {
    //    dml.dropdownsql(ddlItemSubHead, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID", "ItemHeadID", ddlItemsHead.SelectedItem.Value);
    }

  
}