using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class alert : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Write("<script> window.setTimeout(function () { $('.alert-success').fadeTo(500, 0).slideUp(500, function() { $(this).remove(); });}, 5000); </script> ");

    }
}