using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class _2018_2019 : System.Web.UI.Page
{
    DmlOperation dml = new DmlOperation();
    string userid,FiscalYearId;
    protected void Page_Load(object sender, EventArgs e)
    {

        FiscalYearId = Request.QueryString.Get("Fiscal");
        Response.Cookies["FiscalYearId"].Value = FiscalYearId;
        Response.Cookies["FiscalYearId"].Expires = DateTime.Now.AddDays(1);
        userid = Request.QueryString["UserID"];
        DataSet ds = dml.Find("select * from SET_User_Permission_CompBrYear where gocid = "+gocid()+" and CompId = "+compid()+" and CompAll = 'Y' and Record_Deleted = '0' and UserId= '"+userid+"'");
        if (ds.Tables[0].Rows.Count > 0)
        {
            dml.Update("update SET_User_Permission_CompBrYear set InUse = 'Y' where GocId='" + gocid() + "' AND CompId='" + compid() + "' and Record_Deleted = '0' and UserId= '" + userid + "'", "");
        }
        else
        {
        
            dml.Update("update SET_User_Permission_CompBrYear set InUse = 'Y' where GocId=" + gocid() + " AND CompId=" + compid() + " AND BranchId=" + branchId() + " AND FiscalYearsID=" + FiscalYear() , "");
        }
    }

    public int gocid()
    {
        return Convert.ToInt32(Request.Cookies["GocId"].Value);
    }
    public int compid()
    {
        return Convert.ToInt32(Request.Cookies["CompId"].Value);
    }
    public int branchId()
    {
        return Convert.ToInt32(Request.Cookies["BranchId"].Value);
    }
    public int FiscalYear()
    {
        return Convert.ToInt32(Request.Cookies["FiscalYearId"].Value);   
    }



}