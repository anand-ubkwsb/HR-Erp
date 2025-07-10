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
    ExcelLibrary Library = new ExcelLibrary();
    string userid, UserGrpID, FormID, fiscal;
    DmlOperation dml = new DmlOperation();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
    FieldHide fd = new FieldHide();
    Report rpt = new Report();
    protected void Page_Load(object sender, EventArgs e) 
    {
      
        if (Page.IsPostBack == false)
        {
            
            dml.dropdownsql(ddlBusiness, "SET_BusinessPartner", "BPartnerName", "BPartnerId", "Record_Deleted", "0");
       
        }
    }


    protected void btnGenerateExcel_Click(object sender, EventArgs e)
    {
        string type, typehead, typesubhead, localimp;
        if (ddlBusiness.SelectedIndex == 0)
        {
            type = "0";
        }
        else
        {
            type = ddlBusiness.SelectedItem.Text;
        }
        if (txtCnic.Text == "")
        {
            typehead = "0";
        }
        else
        {
            typehead = txtCnic.Text;
        }
        if (txtNTN.Text == "")
        {
            typesubhead = "0";
        }
        else
        {
            typesubhead = txtNTN.Text;
        }

        if (type.Equals("0") && typehead.Equals("0") && typesubhead.Equals("0"))
        {
            Library.WriteExcelFile("Select * From Rpt_BusinessPartner", "All_BusinessReport");
        }
        else if (!typehead.Equals("0"))
        {
            Library.WriteExcelFile("Select * From Rpt_BusinessPartner Where CNICNO = '" + typehead + "'", typehead + " Business_Report");
        }
        else if (!typesubhead.Equals("0"))
        {
            Library.WriteExcelFile("Select * From Rpt_BusinessPartner Where NTNNO = '" + typesubhead + "' ", typesubhead + " Business_Report");
        }
        else if (!type.Equals("0")) {
            Library.WriteExcelFile("Select * From Rpt_BusinessPartner Where BPartnerName='" + type + "'", "BusinessPartner_" + type);
        }
        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertFileGenerated()", true);
        //Response.Redirect("~/Reportsform/BusinessPartner_rpt.aspx?Itemtype='" + type + "'&ItemHead='" + typehead + "'&typesubhead='" + typesubhead + "'");


    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        string type, typehead, typesubhead , localimp;
        if (ddlBusiness.SelectedIndex == 0)
        {
            type = "0";
        }
        else
        {
            type = ddlBusiness.SelectedItem.Text;
        }
        if (txtCnic.Text == "")
        {
            typehead = "0";
        }
        else
        {
            typehead = txtCnic.Text;
        }
        if (txtNTN.Text == "")
        {
            typesubhead = "0";
        }
        else
        {
            typesubhead = txtNTN.Text;
        }
       
        Response.Redirect("~/Reportsform/BusinessPartner_rpt.aspx?Itemtype='" + type + "'&ItemHead='" + typehead + "'&typesubhead='" + typesubhead + "'");
         


    }
   
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlBusiness.SelectedIndex = 0;
        txtCnic.Text = "";
        txtNTN.Text = "";
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
   
  
}