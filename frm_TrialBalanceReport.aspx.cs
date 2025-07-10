using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frm_TrialBalanceReport : System.Web.UI.Page
{

    int GocId, CompId, BranchId, FiscalYearId, ddlComp, ddlBr, ddlFiscal;
    SqlDataAdapter QueryAdapter;
    DataSet QueryDataSet;
    string UserId, UserGrpID, FormID, fiscal, HeadingLevel;
    DmlOperation dm = new DmlOperation();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
    FieldHide fd = new FieldHide();
    Report rpt = new Report();

    protected void Page_Load(object sender, EventArgs e)
    {
        Error_Message.Visible = false;
        Error_Label.Visible = false;
        Error_Label.Text = "";
        UserId = Request.Cookies["userid"].Value;
        GocId = GetFromCookie("GocId");
        CompId = GetFromCookie("CompId");
        BranchId = GetFromCookie("BranchId");
        FiscalYearId = GetFromCookie("FiscalYearId");

        modal_TrialBalanceDiv.Visible = false;
        QueryDataSet = dm.Find("Select GocName From Set_Goc where GocId=" + GocId);
        GroupOfCompaniesText.Text = QueryDataSet.Tables[0].Rows[0]["GocName"].ToString();
        if (!IsPostBack)
        {
            GetNecessaryData(UserId, GocId);

        }
        BranchText.Visible = false;
    }

    protected void GetNecessaryData(string UserId, int GocId)
    {
        GetCompanyForDropDown(UserId, GocId);
        GetFiscalYear(UserId, GocId, 0, 0);
        //dm.DropDownSqlWithCondition(ddlCompany, "Set_Company", "CompName", "CompId", "GocId", GocId.ToString());
        //dm.DropDownSqlWithCondition(ddlFiscalYear, "Set_Fiscal_Year", "Description", "FiscalYearId", "GocId", GocId.ToString());
        //ddlBranch.Enabled = false;
        QueryAdapter = new SqlDataAdapter("select NoofCoaSegments From Set_Goc where GocId =" + GocId, con);
        QueryDataSet = new DataSet();
        QueryAdapter.Fill(QueryDataSet);

        dm.FillHeadingDropDown(ddlHeadingLevel, Convert.ToInt32(QueryDataSet.Tables[0].Rows[0]["NoofCoaSegments"].ToString()));
    }
    protected void btnGenerateReport_Click(object sender, EventArgs e)
    {

        if (ddlHeadingLevel.SelectedItem.Value != "Please select...")
        {
            HeadingLevel = ddlHeadingLevel.SelectedItem.Value.ToString();
            Error_Message.Visible = false;
            Error_Label.Visible = false;
            Error_Label.Text = "";

            if (ddlCompany.SelectedItem.Value != "Please select...")
            {
                ddlComp = Convert.ToInt32(ddlCompany.SelectedItem.Value);
            }
            if (ddlComp > 0)
            {
                if (ddlBranch.SelectedItem.Value != "Please select...")
                {
                    ddlBr = Convert.ToInt32(ddlBranch.SelectedItem.Value);
                }
            }

            if (ddlFiscalYear.SelectedItem.Value != "Please select...")
            {
                ddlFiscal = Convert.ToInt32(ddlFiscalYear.SelectedItem.Value);
            }
            else
            {
                ddlFiscal = GetFromCookie("FiscalYearId");
            }

            if (ddlComp > 0)
            {

                if (ddlBr > 0)
                {
                    Response.Redirect("~/Reportsform/TrialBalance_rpt.aspx?GocId='" + GocId + "'&HeadingLevel=" + HeadingLevel + "&Fiscal=" + ddlFiscal + "&CompId=" + ddlComp + "&BranchId=" + ddlBr);

                }
                else
                {
                    Response.Redirect("~/Reportsform/TrialBalance_rpt.aspx?GocId='" + GocId + "'&HeadingLevel=" + HeadingLevel + "&Fiscal=" + ddlFiscal + "&CompId=" + ddlComp);
                }

            }
            else
            {

                Response.Redirect("~/Reportsform/TrialBalance_rpt.aspx?GocId='" + GocId + "'&HeadingLevel=" + HeadingLevel + "&Fiscal=" + ddlFiscal);

            }

        }
        else
        {
            Error_Message.Visible = true;
            Error_Label.Visible = true;
            Error_Label.Text = "Heading Level is Required";

        }

    }

    protected void GetCompanyForDropDown(string UserId, int GocId)
    {

        con.Close();
        con.Open();
        string Query = "select * From Set_User_Permission_CompBrYear where UserId='" + UserId + "' And GocId=" + GocId + " And CompAll='Y' And Record_Deleted='0'";
        QueryAdapter = new SqlDataAdapter(Query, con);
        QueryDataSet = new DataSet();
        QueryAdapter.Fill(QueryDataSet);
        //Checking For Compall='Y'
        if (QueryDataSet.Tables[0].Rows.Count > 0)
        {

            Query = "Select * From Set_Company where Record_deleted='0' And GocId=" + GocId;

            //SqlDataAdapter ds = new SqlDataAdapter(Query, con);
            QueryAdapter = new SqlDataAdapter(Query, con);
            QueryDataSet = new DataSet();
            QueryAdapter.Fill(QueryDataSet);
            //ds.Fill(QueryDataSet);
            if (QueryDataSet.Tables[0].Rows.Count > 1)
            {
                dm.dropdownsqlwithquery(ddlCompany, Query, "CompName", "CompId");
                CompanyText.Visible = false;
                ddlCompany.Visible = true;

            }
            else if (QueryDataSet.Tables[0].Rows.Count == 0)
            {
                ConditionForCountZero(ddlCompany, CompanyText, "No Company");

            }
            else
            {
                ConditionForCountOne(QueryDataSet, "CompName", "CompId", ddlCompany, CompanyText, "Set_Company", ddlComp);
                GetBranchForDropDOwn(UserId, GocId, ddlComp);
            }
        }
        else
        {
            Query = "Select * From Set_Company where CompId In (select CompId From Set_User_Permission_CompBrYear where UserId='" + UserId + "' And GocId=" + GocId + " And CompAll='N' And Record_Deleted='0') And Record_Deleted='0'";
            QueryAdapter = new SqlDataAdapter(Query, con);
            QueryDataSet = new DataSet();
            QueryAdapter.Fill(QueryDataSet);
            if (QueryDataSet.Tables[0].Rows.Count > 1)
            {
                dm.dropdownsqlwithquery(ddlCompany, Query, "CompName", "CompId");
                ddlCompany.Visible = true;
                CompanyText.Visible = false;
            }
            else if (QueryDataSet.Tables[0].Rows.Count == 0)
            {
                ConditionForCountZero(ddlCompany, CompanyText, "No Company");
            }
            else
            {
                ConditionForCountOne(QueryDataSet, "CompName", "CompId", ddlCompany, CompanyText,"Set_Company",ddlComp);
                GetBranchForDropDOwn(UserId, GocId, ddlComp);
            }
        }

        con.Close();
    }


    protected void GetBranchForDropDOwn(string UserId, int GocId, int CompId)
    {

        con.Close();
        con.Open();
        string Query = "Select * From Set_Branch where BranchId In (select BranchId From Set_User_Permission_CompBrYear where UserId='" + UserId + "' And GocId=" + GocId + " And CompId=" + CompId + " And BranchAll='Y' And Record_Deleted='0') And Record_Deleted='0'";
        QueryAdapter = new SqlDataAdapter(Query, con);
        QueryDataSet = new DataSet();
        QueryAdapter.Fill(QueryDataSet);
        if (QueryDataSet.Tables[0].Rows.Count > 0)
        {
            Query = "Select * From Set_Branch where CompId=" + CompId + " And Record_Deleted='0'";
            QueryAdapter = new SqlDataAdapter(Query, con);
            QueryDataSet = new DataSet();
            QueryAdapter.Fill(QueryDataSet);
            if (QueryDataSet.Tables[0].Rows.Count > 1)
            {
                dm.dropdownsqlwithquery(ddlBranch, Query, "BranchName", "BranchId");
                ddlBranch.Visible = true;
                BranchText.Visible = false;
            }
            else if (QueryDataSet.Tables[0].Rows.Count == 0)
            {
                ConditionForCountZero(ddlBranch, BranchText, "No Branch");
            }
            else
            {
                ConditionForCountOne(QueryDataSet, "BranchName", "BranchId", ddlBranch, BranchText, "Set_Branch", ddlBr);
                GetFiscalYear(UserId, GocId, Convert.ToInt32(ViewState["CompId"]), ddlBr);
            }


        }

        else
        {
            Query = "Select * From Set_Branch where BranchId In (select BranchId From Set_User_Permission_CompBrYear where UserId='" + UserId + "' And GocId=" + GocId + " And CompId=" + CompId + " And BranchAll='N' And Record_Deleted='0') And Record_Deleted='0'";
            QueryAdapter = new SqlDataAdapter(Query, con);
            QueryDataSet = new DataSet();
            QueryAdapter.Fill(QueryDataSet);
            if (QueryDataSet.Tables[0].Rows.Count > 1)
            {
                dm.dropdownsqlwithquery(ddlBranch, Query, "BranchName", "BranchId");
                ddlBranch.Visible = true;
                BranchText.Visible = false;
            }
            else if (QueryDataSet.Tables[0].Rows.Count == 0)
            {
                ConditionForCountZero(ddlBranch, BranchText, "No Branch");
            }
            else
            {
                ConditionForCountOne(QueryDataSet, "BranchName", "BranchId", ddlBranch, BranchText,"Set_Branch",ddlBr);
                GetFiscalYear(UserId, GocId, Convert.ToInt32(ViewState["CompId"]), ddlBr);
            }


        }
        con.Close();
    }

    //CompanyText.Text = QueryDataSet.Tables[0].Rows[0]["CompName"].ToString();
    //ViewState["CompId"] = QueryDataSet.Tables[0].Rows[0]["CompId"].ToString();
    //ddlCompany.Visible = false;
    //            CompanyText.Visible = true;
    //            ddlComp = Convert.ToInt32(QueryDataSet.Tables[0].Rows[0]["CompId"].ToString());


    protected void ConditionForCountOne(DataSet Ds, string FieldName, string FieldId, DropDownList ddl, Label label,string TableName,int Id)
    {
        label.Text = Ds.Tables[0].Rows[0][FieldName].ToString();
        ViewState[FieldId] = Ds.Tables[0].Rows[0][FieldId].ToString();
        Id = Convert.ToInt32(Ds.Tables[0].Rows[0][FieldId].ToString());
        string Query = "Select * From " + TableName + " Where " + FieldId + "=" + Id;
        dm.dropdownsqlwithquery(ddl, Query, FieldName, FieldId);
        ddl.SelectedIndex = 1;
        ddl.Visible = true;

    }


    protected void ConditionForCountZero(DropDownList ddl, Label label, string message)
    {
        ddl.Visible = false;
        label.Text = message;
        label.Visible = true;
    }


    public int GetFromCookie(string name)
    {
        return Convert.ToInt32(Request.Cookies[name].Value);
    }
    protected int GetFromQueryStringInInt(string name)
    {
        if (Request.QueryString.Get(name) != null)
        {
            return Convert.ToInt32(Request.QueryString.Get(name));
        }
        return 0;
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Enabled = true;
        if (ddlCompany.SelectedItem.Value != "Please select...")
        {
            ddlComp = Convert.ToInt32(ddlCompany.SelectedItem.Value);
        }
        else
        {
            BranchText.Visible = false;
            ddlComp = 0;
        }
        dm.DropDownSqlWithCondition(ddlBranch, "Set_Branch", "BranchName", "BranchId", "CompId", ddlComp.ToString());
        ddlFiscalYear.Items.Clear();
        GetFiscalYear(UserId, GocId, ddlComp, 0);
        ViewState["CompId"] = ddlComp.ToString();
    }

    protected void GetFiscalYear(string UserId, int GocId, int CompId, int BranchId)
    {
        string Query;

        con.Close();
        con.Open();
        if (CompId > 0)
        {
            if (BranchId > 0)
            {
                Query = "select * From Set_User_Permission_CompBrYear where UserId = '" + UserId + "' And FiscalYearAll ='Y' And Record_Deleted ='0' And GocId =" + GocId + " And CompId =" + CompId + " And BranchId =" + BranchId;
            }
            else
            {
                Query = "select * From Set_User_Permission_CompBrYear where UserId = '" + UserId + "' And FiscalYearAll ='Y' And Record_Deleted ='0' And GocId =" + GocId + " And CompId =" + CompId;
            }

        }
        else
        {
            Query = "select * From Set_User_Permission_CompBrYear where UserId = '" + UserId + "' And FiscalYearAll ='Y' And Record_Deleted ='0' And GocId =" + GocId;
        }

        QueryAdapter = new SqlDataAdapter(Query, con);
        QueryDataSet = new DataSet();
        QueryAdapter.Fill(QueryDataSet);

        if (QueryDataSet.Tables[0].Rows.Count > 0)
        {
            if (CompId > 0)
            {
                if (BranchId > 0)
                {
                    Query = "Select * From Set_Fiscal_Year where Record_deleted='0' And FiscalYearId in (Select FiscalYearsId From Set_User_Permission_CompBrYear Where Record_Deleted='0' And UserId='" + UserId + "' And GocId=" + GocId + " And CompId=" + CompId + " And BranchId=" + BranchId + ")";
                }
                else
                {
                    Query = "Select * From Set_Fiscal_Year where Record_deleted='0' And FiscalYearId in (Select FiscalYearsId From Set_User_Permission_CompBrYear Where Record_Deleted='0' And UserId='" + UserId + "' And GocId=" + GocId + " And CompId=" + CompId + ")";
                }

            }
            else
            {
                Query = "Select * From Set_Fiscal_Year where Record_deleted='0' And FiscalYearId in (Select FiscalYearsId From Set_User_Permission_CompBrYear Where Record_Deleted='0' And UserId='" + UserId + "' And GocId=" + GocId + ")";
            }

            QueryAdapter = new SqlDataAdapter(Query, con);
            QueryDataSet = new DataSet();
            QueryAdapter.Fill(QueryDataSet);
            if (QueryDataSet.Tables[0].Rows.Count > 1)
            {
                int[] arr = new int[QueryDataSet.Tables[0].Rows.Count];
                for (int i = 0; i < QueryDataSet.Tables[0].Rows.Count; i++)
                {
                    arr[i] = Convert.ToInt32(QueryDataSet.Tables[0].Rows[i]["FiscalYearId"].ToString());
                }
                ddlFiscalYear.Items.Clear();
                dm.dropdownsqlwithquery(ddlFiscalYear, Query, "Description", "FiscalYearId");
                ddlFiscalYear.Visible = true;
                FiscalYearText.Visible = false;

            }
            else if (QueryDataSet.Tables[0].Rows.Count == 0)
            {
                ConditionForCountZero(ddlFiscalYear, FiscalYearText, "No FiscalYear");
            }
            else
            {
                ConditionForCountOne(QueryDataSet, "Description", "FiscalYearId", ddlFiscalYear, FiscalYearText,"Set_Fiscal_Year",ddlFiscal);
            }
        }
        else
        {
            if (CompId > 0)
            {
                if (BranchId > 0)
                {
                    Query = "Select * From Set_Fiscal_Year Where Record_Deleted='0' And FiscalYearId in (select FiscalYearsId From Set_User_Permission_CompBrYear where UserId = '" + UserId + "' And FiscalYearAll ='N' And Record_Deleted ='0' And GocId =" + GocId + " And CompId =" + CompId + " And BranchId =" + BranchId + ")";
                }
                else
                {
                    Query = "Select * From Set_Fiscal_Year Where Record_Deleted='0' And FiscalYearId in (select FiscalYearsId From Set_User_Permission_CompBrYear where UserId = '" + UserId + "' And FiscalYearAll ='N' And Record_Deleted ='0' And GocId =" + GocId + " And CompId =" + CompId + ")";
                }

            }
            else
            {
                Query = "Select * From Set_Fiscal_Year Where Record_Deleted='0' And FiscalYearId in (select FiscalYearsId From Set_User_Permission_CompBrYear where UserId = '" + UserId + "' And FiscalYearAll ='N' And Record_Deleted ='0' And GocId =" + GocId + ")";
            }

            QueryAdapter = new SqlDataAdapter(Query, con);
            QueryDataSet = new DataSet();
            QueryAdapter.Fill(QueryDataSet);
            if (QueryDataSet.Tables[0].Rows.Count > 1)
            {
                ddlFiscalYear.Items.Clear();
                dm.dropdownsqlwithquery(ddlFiscalYear, Query, "Description", "FiscalYearId");
                ddlFiscalYear.Visible = true;
                FiscalYearText.Visible = false;
            }
            else if (QueryDataSet.Tables[0].Rows.Count == 0)
            {
                ConditionForCountZero(ddlFiscalYear, FiscalYearText, "No Fiscal Year");
            }
            else
            {

                ConditionForCountOne(QueryDataSet, "Description", "FiscalYearId", ddlFiscalYear, FiscalYearText, "Set_Fiscal_Year", ddlFiscal);
            }



        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedItem.Value != "Please select...")
        {
            ddlBr = Convert.ToInt32(ddlBranch.SelectedItem.Value);
        }
        else
        {
            ddlBr = 0;
        }
        ViewState["BranchId"] = ddlBr.ToString();
        GetFiscalYear(UserId, GocId, Convert.ToInt32(ViewState["CompId"]), ddlBr);
        //Response.Cookies["ddlBranch"].Value = ddlBranch.SelectedItem.Value;
        //Response.Cookies["ddlBranch"].Expires = DateTime.Now.AddMinutes(20);

    }

    protected void ddlHeadingLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["HeadingLevel"] = ddlHeadingLevel.SelectedIndex.ToString();
    }

    protected void ddlFiscal_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFiscalYear.SelectedItem.Value != "Please select...")
        {
            ddlFiscal = Convert.ToInt32(ddlFiscalYear.SelectedItem.Value);
        }
        else
        {
            ddlFiscal = 0;
        }
        ViewState["FiscalYearId"] = ddlFiscal.ToString();
        //Response.Cookies["ddlFiscal"].Value = ddlFiscalYear.SelectedItem.Value;
        //Response.Cookies["ddlFiscal"].Expires = DateTime.Now.AddMinutes(20);
    }



    protected void btnInsert_Click(object sender, EventArgs e)
    {
        //string type, typehead, typesubhead, localimp;
        //if (ddlBusiness.SelectedIndex == 0)
        //{
        //    type = "0";
        //}
        //else
        //{
        //    type = ddlBusiness.SelectedItem.Text;
        //}
        //if (txtCnic.Text == "")
        //{
        //    typehead = "0";
        //}
        //else
        //{
        //    typehead = txtCnic.Text;
        //}
        //if (txtNTN.Text == "")
        //{
        //    typesubhead = "0";
        //}
        //else
        //{
        //    typesubhead = txtNTN.Text;
        //}



    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //ddlBusiness.SelectedIndex = 0;
        //txtCnic.Text = "";
        //txtNTN.Text = "";
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