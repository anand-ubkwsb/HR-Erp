using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class checklist_Search : System.Web.UI.Page
{
    DmlOperation dml = new DmlOperation();
    protected void Page_Load(object sender, EventArgs e)
    {
        dml.dropdownsql(DropDownList1, "SET_BusinessPartner", "BPartnerName", "BPartnerId");
        //  dml.dropdownsql(DropDownList2, "SET_BusinessPartner", "BPartnerName", "BPartnerId");
        dml.dropdownsql(DropDownList3, "SET_BusinessPartner", "BPartnerName", "BPartnerId");
       
   //     DropDownList3.Attributes.Add("title", "anncc");


    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
    //    if (DropDownList1.SelectedIndex != 0)
      //  {
      //      DataSet ds = dml.Find("select  COA_D_ID , Acct_Code, Acct_Description from SET_COA_detail where Acct_Code = '" + DropDownList1.SelectedItem.Text + "'");
      //      DropDownList1.ToolTip = ds.Tables[0].Rows[0]["Acct_Description"].ToString();
      //  }
    }

    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            LinkButton1.Text = DropDownList3.SelectedItem.Text;
            // DropDownList3.DataBind();
        }
    }

    protected void DropDownList3_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
         LinkButton1.Text = DropDownList3.SelectedItem.Text;
          // DropDownList3.DataBind();
        }
    }
}