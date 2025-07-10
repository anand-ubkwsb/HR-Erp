using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class htmlpa : System.Web.UI.Page
{
    public enum MessageType { Success, Error, Info, Warning };
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ShowMessage(string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
    }
    protected void btnSuccess_Click(object sender, EventArgs e)
    {
        ShowMessage("Record submitted successfully", MessageType.Success);
    }
    protected void btnDanger_Click(object sender, EventArgs e)
    {
        ShowMessage("A problem has occurred while submitting data", MessageType.Error);
    }
    protected void btnWarning_Click(object sender, EventArgs e)
    {
        ShowMessage("There was a problem with your internet connection", MessageType.Warning);
    }
    protected void btnInfo_Click(object sender, EventArgs e)
    {
        ShowMessage("Please verify your data before submitting", MessageType.Info);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //Response.Write("<script> swal('Good job!', 'You clicked the button!', 'success');</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", " alertme()", true);

    }
    public void clicl_a()
    {
        ShowMessage("There was a problem with your internet connection", MessageType.Warning);
    }




    
protected void btn_time_Click(object sender, EventArgs e)
    {
        System.DateTime firstDate = new System.DateTime(2016, 07, 1);
        System.DateTime secondDate = DateTime.Now.Date;

        DateTime datetocheck = new DateTime(2019, 06, 26);
        if (datetocheck >= firstDate && datetocheck <= secondDate)
        {
            lbl_time.Text = "date exist";
        }
        else
        {
            lbl_time.Text = "date not exist";
        }
    }
}