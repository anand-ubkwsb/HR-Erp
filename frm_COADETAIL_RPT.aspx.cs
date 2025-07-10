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
            
            dml.dropdownsql(ddlAcct_code, "SET_COA_detail", "Acct_Code", "COA_D_ID", "Record_Deleted", "0");
            

        }
    }

    protected void btnGenerateExcel_Click(object sender, EventArgs e)
    {
        string query = "SELECT COA_D_ID,COAM.Narration AS COA_MASTER,Acct_Code,Acct_Description,Head_detail_ID, ACCT.Acct_Type_Name,SUB.Acct_Sub_Type_Name,G.GOCName,C.CompName,COA.IsActive,Bar_Code,COA.Record_Deleted FROM SET_COA_DETAIL COA LEFT JOIN SET_Acct_Type ACCT ON COA.Acct_Type_ID = ACCT.Acct_Type_Id LEFT JOIN SET_COA_Master COAM ON COA.COA_M_ID = COAM.COA_M_ID LEFT JOIN SET_Acct_Sub_Type SUB ON COA.Acct_Sub_Type_Id = SUB.Acct_Sub_Type_Id AND COA.Acct_Type_ID = SUB.Acct_Type_Id LEFT JOIN SET_GOC G ON COA.GocId = G.GocId LEFT JOIN SET_Company C ON COA.CompId = C.CompId ";
        string type, typehead, level;
        if (ddlAcct_code.SelectedIndex == 0)
        {
            type = "0";
        }
        else
        {
            type = ddlAcct_code.SelectedItem.Text;
        }
        if (ddlHeadLevel.SelectedIndex == 0)
        {
            level = "0";
        }
        else
        {
            level = ddlHeadLevel.SelectedItem.Text;
        }

        if (!type.Equals("0"))
        {
            Library.WriteExcelFile(query + " where COA.Acct_Code='" + type + "' AND COA.RECORD_DELETED='0'", "Account_" + type + "_Report");
        }
        else if (!level.Equals("0"))
        {
            Library.WriteExcelFile(query + " WHERE COA.HEAD_DETAIL_ID='" + level + "' AND COA.RECORD_DELETED='0'", "Heading_" + level + "_Report");
        }
        else if (!type.Equals("0") && !level.Equals("0"))
        {
            Library.WriteExcelFile(query + "Where COA.HEAD_DETAIL_ID ='" + level + "' OR COA.ACCT_CODE='" + type + "' AND COA.RECORD_DELETED='0'", type + "HEADING" + level + "REPORT");
        }
        else
        {
            Library.WriteExcelFile(query + " WHERE COA.RECORD_DELETED='0'", "COADETAILCOMPLETEREPORT");
        }
        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertFileGenerated()", true);

    }

    //protected void btnGenerateExcel_Click(object sender, EventArgs e)
    //{
    //    //string query = "SELECT COA_D_ID,COAM.Narration AS COA_MASTER,Acct_Code,Acct_Description,Head_detail_ID, ACCT.Acct_Type_Name,SUB.Acct_Sub_Type_Name,G.GOCName,C.CompName,COA.IsActive,Bar_Code,COA.Record_Deleted FROM SET_COA_DETAIL COA LEFT JOIN SET_Acct_Type ACCT ON COA.Acct_Type_ID = ACCT.Acct_Type_Id LEFT JOIN SET_COA_Master COAM ON COA.COA_M_ID = COAM.COA_M_ID LEFT JOIN SET_Acct_Sub_Type SUB ON COA.Acct_Sub_Type_Id = SUB.Acct_Sub_Type_Id AND COA.Acct_Type_ID = SUB.Acct_Type_Id LEFT JOIN SET_GOC G ON COA.GocId = G.GocId LEFT JOIN SET_Company C ON COA.CompId = C.CompId ";
    //    string type, typehead, level;
    //    if (ddlAcct_code.SelectedIndex == 0)
    //    {
    //        type = "0";
    //    }
    //    else
    //    {
    //        type = ddlAcct_code.SelectedItem.Text;
    //    }
    //    if (ddlHeadLevel.SelectedIndex == 0)
    //    {
    //        level = "0";
    //    }
    //    else
    //    {
    //        level = ddlHeadLevel.SelectedItem.Text;
    //    }

    //    if (!type.Equals("0"))
    //    {
    //        Library.WriteExcelFile("SELECT * fROM SET_COA_DETAIL where COA.Acct_Code='" + type + "' AND COA.RECORD_DELETED='0'", "Account_" + type + "_Report");
    //    }
    //    else if (!level.Equals("0"))
    //    {
    //        Library.WriteExcelFile("SELECT * fROM SET_COA_DETAIL WHERE COA.HEAD_DETAIL_ID='" + level + "' AND COA.RECORD_DELETED='0'", "Heading_" + level + "_Report");
    //    }
    //    else if (!type.Equals("0") && !level.Equals("0")) {
    //        Library.WriteExcelFile("SELECT * fROM SET_COA_DETAIL Where COA.HEAD_DETAIL_ID ='" + level+"' OR COA.ACCT_CODE='" + type + "' AND COA.RECORD_DELETED='0'",type+"HEADING"+level+"REPORT");
    //    }
    //    else {
    //        Library.WriteExcelFile("SELECT * fROM SET_COA_DETAIL WHERE COA.RECORD_DELETED='0'", "COADETAILCOMPLETEREPORT");
    //    }
    //    ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertFileGenerated()", true);

    //}
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        string type, typehead, level;
        if (ddlAcct_code.SelectedIndex == 0)
        {
            type = "0";
        }
        else
        {
            type = ddlAcct_code.SelectedItem.Text;
        }
        if(ddlHeadLevel.SelectedIndex == 0)
        {
            level = "0";
        }
        else 
        {
            level = ddlHeadLevel.SelectedItem.Text;
        }
       
       Response.Redirect("~/Reportsform/CoaDeTail_rpt.aspx?Itemtype='" + Server.UrlEncode(type) + "'&headlevel='" + level + "'");



    }

   
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlAcct_code.SelectedIndex = 0;
       
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