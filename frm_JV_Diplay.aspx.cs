using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

public partial class frm_JV_Diplay : System.Web.UI.Page
{
    DmlOperation dml = new DmlOperation();
    float TotalDR = 0;
    float TotalCR = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        bindgrid();
    }
    public void sum()
    {
    //     decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("Price"));
    //GridView1.FooterRow.Cells[1].Text = "Total";
    //                GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
    //                GridView1.FooterRow.Cells[2].Text = total.ToString("N2");
    }
    public void bindgrid()
    {
        string vno = Request.QueryString["VoucherNo"];
        if (vno != "")
        {

            string Query = "SELECT [AccountCode], [Acct_Description], [VoucherNo], [DrAmount], [CrAmount] FROM [View_JV_Display_Sum] WHERE [VoucherNo] = '" + vno + "'  order by Tran_Type DESC, Acct_Description ";
            DataSet ds = dml.Find(Query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                grdviewdisplay.DataSource = ds.Tables[0];
                grdviewdisplay.DataBind();
            }
        }
    }

    protected void grdviewdisplay_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string a = "";
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.FindControl("lblDrAmount") as Label).Text != "")
            {
                TotalDR += float.Parse((e.Row.FindControl("lblDrAmount") as Label).Text);
            }
            if ((e.Row.FindControl("lblCrAmount") as Label).Text != "")
            {
                TotalCR += float.Parse((e.Row.FindControl("lblCrAmount") as Label).Text);
            }

        }
       else if (e.Row.RowType == DataControlRowType.Footer)
        {
            
               (e.Row.FindControl("lblTotalDrAmount") as Label).Text = string.Format("{0:0.00 }", TotalDR.ToString("0.00"));
            (e.Row.FindControl("lblTotalCrAmount") as Label).Text =  string.Format("{0:0.00 }", TotalCR.ToString("0.00"));
        }

    }

    protected void btnConvert_Click(object sender, EventArgs e)
    {
        ExportGridToPDF();

    }
   
    private void ExportGridToPDF()
    {

        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=JournolVouchor.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        grdviewdisplay.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
        grdviewdisplay.AllowPaging = true;
        grdviewdisplay.DataBind();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
}