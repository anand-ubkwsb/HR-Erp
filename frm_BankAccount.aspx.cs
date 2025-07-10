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

public partial class frm_BankAccount : System.Web.UI.Page
{
    TextBox S_userid, S_LoginName, S_Displayarea;
    TextBox txtuser;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());

    DataSet ds = new DataSet();

    string userid, UserGrpID;
    protected void Page_Load(object sender, EventArgs e)
    {
        userid = Request.QueryString["UserID"];
        UserGrpID = Request.QueryString["UsergrpID"];

        table_frm();
    }

    public void table_frm()
    {
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("select TABLE_NAME,COLUMN_NAME,DATA_TYPE from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME= 'SET_BANKACCOUNT'", con);
        da.Fill(ds);

        int fieldvalues = ds.Tables[0].Rows.Count;

        HtmlTableRow row = new HtmlTableRow();
        HtmlTableCell cell = new HtmlTableCell();

        row = new HtmlTableRow();
        for (int i = 0; i < fieldvalues; i++)
        {
            if (i % 2 == 0)
            {
                row = new HtmlTableRow();

            }

            cell = new HtmlTableCell();
            cell.Attributes.Add("style", "font-weight:bold");
            string txtlabel = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
            txtlabel.ToUpper();
            cell.InnerText = txtlabel.Replace("_", " ");
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            string field_type = ds.Tables[0].Rows[i]["DATA_TYPE"].ToString();

            txtuser = new TextBox();
            txtuser.Attributes.Add("class", "form-control");
            cell.Width = "250px;";
            cell.Attributes.Add("style", "padding-bottom:15px;");
            txtuser.ID = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
            txtuser.ID.ToUpper();


            txtuser.Attributes.Add("placeholder", (txtuser.ID.Replace("_", " ")));
            cell.Controls.Add(txtuser);
            row.Cells.Add(cell);


            cell = new HtmlTableCell();

            cell.InnerText = "";
            cell.Attributes.Add("style", "padding-bottom:15px;");
            cell.Width = "150px;";
            row.Cells.Add(cell);
            tablecontent1.Rows.Add(row);

        }
        fieldhide();
        da.Dispose();
        con.Close();
    }


    public void fieldhide()
    {
        SqlDataAdapter da1 = new SqlDataAdapter("select field_name from SET_UserGrp_FieldHide where FormId in (select FormId from SET_UserGrp_FieldHide where UserGrpId = '" + UserGrpID + "') and table_name = 'SET_BANKACCOUNT'", con);
        DataSet ds_FHide = new DataSet();
        da1.Fill(ds_FHide);
        int f_Count = ds_FHide.Tables[0].Rows.Count;


        if (f_Count == 0)
        {
            fieldUnhide_All();
        }
        else {

            for (int y = 0; y <= f_Count - 1; y++)
            {
                string fielsid = ds_FHide.Tables[0].Rows[y]["field_name"].ToString();
                TextBox txtc = tablecontent1.FindControl(fielsid) as TextBox;
                Label lbl = tablecontent1.FindControl(fielsid) as Label;
                if (txtc is TextBox)
                {
                    //   DataSet dsf = GetData("")
                    if (fielsid == txtc.ID)
                    {
                        txtc.Attributes.Add("class", "form-control");
                        txtc.Enabled = false;
                    }

                }
                else if (lbl is Label)

                {
                    lbl.Enabled = false;
                }
                else
                {
                    txtc.Enabled = true;
                    lbl.Enabled = true;
                }
            }

            da1.Dispose();
        }
    }

    public void fieldUnhide_All()
    {
        SqlDataAdapter da1 = new SqlDataAdapter("select TABLE_NAME,COLUMN_NAME,DATA_TYPE from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME= 'SET_BANKACCOUNT'", con);
        DataSet ds_FHide = new DataSet();
        da1.Fill(ds_FHide);
        int f_Count = ds_FHide.Tables[0].Rows.Count;
        for (int i = 0; i <= f_Count - 1; i++)
        {
            string fielsid = ds_FHide.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
            TextBox txtc = tablecontent1.FindControl(fielsid) as TextBox;
            Label lbl = tablecontent1.FindControl(fielsid) as Label;
            if (txtc is TextBox)
            {

                txtc.Enabled = true;

            }

            else if (lbl is Label)
            {
                lbl.Enabled = true;
            }

        }
    }
}