using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class frm_TrailBalance : System.Web.UI.Page
{
    List<TrialBalance> Balance;
    string UserId;
    int ddlComp, ddlBr, ddlFiscal;
    DmlOperation dm = new DmlOperation();
    Double Assets, Liablity, Capital, Revenue, Expenses, Adjustment;
    int GocId, CompId, BranchId, FiscalYearId, count;
    SqlDataAdapter QueryAdapter;
    DataSet QueryDataSet = new DataSet();
    List<Double> OpeningValue, DebitValue, CreditValue, TotalValue;
    //Double[] OpeningValue, DebitValue, CreditValue, TotalValue;
    string HeadingLevel;
    TrialExcel lib = new TrialExcel();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());

    public string GetOpeningValueQuery(int GocId, int CompId, int BranchId, int FiscalYearId, string AccountCode)
    {
        string query = "Select " + AccountCode + " as AccountCode,SUM(OpeningValue) as OpeningValues From Set_Coa_Detail_Opening where Acct_Code like '" + AccountCode + "%' AND Gocid = " + GocId + " And CompId = " + CompId + " And BranchId = " + BranchId + " And Record_Deleted = '0' AND FiscalYearId = " + FiscalYearId;
        return query;
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        Error_Message.Visible = false;
        Error_Label.Visible = false;
        Error_Label.Text = "";
        //OpeningValue = new double[];

        UserId = Request.Cookies["userid"].Value;
        GocId = GetFromCookie("GocId");
        CompId = GetFromCookie("CompId");
        BranchId = GetFromCookie("BranchId");
        FiscalYearId = GetFromCookie("FiscalYearId");
        modal_TrialBalanceDiv.Visible = false;
        QueryDataSet = dm.Find("Select GocName From Set_Goc where GocId=" + GocId);
        GroupOfCompaniesText.Text = QueryDataSet.Tables[0].Rows[0]["GocName"].ToString();
        BranchText.Visible = false;
        btnBack.Visible = false;
        SaveInDataBase.Visible = false;
        //ddlComp = GetFromQueryStringInInt("ddlComp");
        //ddlBr= GetFromQueryStringInInt("ddlBr");
        //ddlFiscal = GetFromQueryStringInInt("ddlFiscalYear");

        if (!IsPostBack)
        {
            GetNecessaryData(UserId, GocId);

        }

        //ddlComp = GetFromQueryStringInInt("ddlComp");
        //ddlBr = GetFromQueryStringInInt("ddlBr");
        //ddlFiscal = GetFromQueryStringInInt("ddlFiscalYear");
        //if (Request.QueryString.Get("HeadingLevel") != null)
        //{
        //    HeadingLevel = Request.QueryString.Get("HeadingLevel");
        //}
        //else {
        //    HeadingLevel = null;
        //}

        //if (!string.IsNullOrEmpty(HeadingLevel)) {
        //    if (ddlComp > 0)
        //    {
        //        ddlCompany.SelectedItem.Value = ddlComp.ToString();
        //        if (ddlBr > 0)
        //        {
        //            ddlBranch.SelectedItem.Value = ddlBr.ToString();
        //        }
        //    }

        //    if (ddlFiscal > 0)
        //    {
        //        ddlFiscalYear.SelectedItem.Value = ddlFiscal.ToString();

        //    }
        //    modal_DataDiv.Visible = false; 
        //    modal_TrialBalanceDiv.Visible = true;
        //    btnBack.Visible = true;
        //    btnGenerate.Visible = false;
        //    SaveInDataBase.Visible = true;
        //    SaveInExcel.Visible = true;
        //    ddlHeadingLevel.SelectedItem.Value = HeadingLevel.ToString();
        //    GenerateGeneralTrialBalance();
        //}


    }

    protected int GetFromQueryStringInInt(string name)
    {
        if (Request.QueryString.Get(name) != null)
        {
            return Convert.ToInt32(Request.QueryString.Get(name));
        }
        return 0;
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
                ConditionForCountOne(QueryDataSet, "Description", "FiscalYearId", ddlFiscalYear, FiscalYearText, "Set_Fiscal_Year",  ddlFiscal);
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

                ConditionForCountOne(QueryDataSet, "Description", "FiscalYearId", ddlFiscalYear, FiscalYearText,"Set_Fiscal_Year",ddlFiscal);
            }



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
                ConditionForCountOne(QueryDataSet, "CompName", "CompId", ddlCompany, CompanyText, "Set_Company", ddlComp);
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


    protected void ConditionForCountOne(DataSet Ds, string FieldName, string FieldId, DropDownList ddl, Label label, string TableName,int Id)
    {
        label.Text = Ds.Tables[0].Rows[0][FieldName].ToString();
        ViewState[FieldId] = Ds.Tables[0].Rows[0][FieldId].ToString();
        Id = Convert.ToInt32(Ds.Tables[0].Rows[0][FieldId].ToString());
        string Query = "Select * From " + TableName + " Where " + FieldId + "=" + Id;
        dm.dropdownsqlwithquery(ddl, Query,FieldName,FieldId);
        ddl.SelectedIndex = 1;
        ddl.Visible = true;
    }


    protected void ConditionForCountZero(DropDownList ddl, Label label, string message)
    {
        ddl.Visible = false;
        label.Text = message;
        label.Visible = true;
    }

    protected void GenerateGeneralTrialBalance()
    {
        con.Close();
        con.Open();
        var HeadingLevel = ddlHeadingLevel.SelectedValue;
        QueryAdapter = new SqlDataAdapter("select Acct_Code,Acct_Description From Set_Coa_detail where Acct_Code in (Select Acct_Code From Set_Coa_Detail where Head_detail_ID = '" + HeadingLevel + "')", con);
        QueryDataSet = new DataSet();
        QueryAdapter.Fill(QueryDataSet);

        GenerateTrailBalance(QueryDataSet, false);
        modal_DataDiv.Visible = false;
        modal_TrialBalanceDiv.Visible = true;
        btnBack.Visible = true;
        btnGenerate.Visible = false;
        SaveInDataBase.Visible = true;
        con.Close();
    }





    protected void GenerateTrailBalance(DataSet ds, bool flag)
    {
        Balance = new List<TrialBalance>();
        count = ds.Tables[0].Rows.Count;
        OpeningValue = new List<Double>();
        CreditValue = new List<Double>();
        DebitValue = new List<Double>();
        TotalValue = new List<Double>();
        DataSet Temp = new DataSet();
        GetSumCrDrOpening(ds, "", count, OpeningValue, Temp);
        GetSumCrDrOpening(ds, "CreditAmount", count, CreditValue, Temp);
        GetSumCrDrOpening(ds, "DebitAmount", count, DebitValue, Temp);
        for (int i = 0; i < count; i++)
        {
            TotalValue.Add(0);
            var Acct_Code = ds.Tables[0].Rows[i]["Acct_Code"].ToString();
            if (Acct_Code[0].Equals("1") || Acct_Code[0].Equals("5") || Acct_Code[0].Equals("6"))
            {
                TotalValue[i] = OpeningValue[i] + DebitValue[i] - CreditValue[i];
            }
            else
            {
                TotalValue[i] = OpeningValue[i] + CreditValue[i] - DebitValue[i];
            }
        }

        //for (int i = 0; i < TotalValue.Count; i++)
        //{
        //    if (TotalValue[i] == 0 && OpeningValue[i] == 0 && CreditValue[i] == 0 && DebitValue[i] == 0) {
        //        ds.Tables[0].Rows[i].Delete();
        //        ds.Tables[0].AcceptChanges();
        //        TotalValue.RemoveAt(i);
        //        OpeningValue.RemoveAt(i);
        //        CreditValue.RemoveAt(i);
        //        DebitValue.RemoveAt(i);
        //    }
        //}

        StringBuilder html = new StringBuilder();
        html.Append("<div class='container-fluid table-flex'>");

        html.Append("<div class='container-table'>");

        html.Append("<table  class='table table-bordered table-responsive table_height'>");

        html.Append("<thead class='thead_light'>");

        html.Append("<tr>");


        html.Append("<th>");
        html.Append("Name</th>");

        html.Append("<th>");
        html.Append("OPENING VALUE</th>");

        html.Append("<th>");
        html.Append("DEBIT VALUE</th>");

        html.Append("<th>");
        html.Append("CREDIT VALUE</th>");

        html.Append("<th>");
        html.Append("TOTAL VALUE</th>");
        if (ddlHeadingLevel.SelectedItem.Value != "D1")
        {
            html.Append("<th>");
            html.Append("Actions</th>");
        }
        html.Append("</tr>");

        html.Append("</thead>");

        html.Append("<tbody>");

        //string test = "/frm_Trialbalance.aspx?UserId=" + UserId + "&ddlComp=" + ddlComp + "&ddlBr=" + ddlBr + "&ddlFiscalYear=" + ddlFiscal + "&fiscaly=" + Request.Cookies["FiscalYearId"].Value + "&GocId = " + GocId + "&CompId = " + Request.Cookies["CompId"].Value + "&BranchId = " + Request.Cookies["BranchId"].Value + "&Fiscal=" + Request.Cookies["FiscalYearId"].Value + "&UsergrpID=" + Request.Cookies["UserGrpID"].Value + "&HeadingLevel=" + ddlHeadingLevel.SelectedItem.Value;
        for (int i = 0; i < count; i++)
        {
            if (OpeningValue[i] != 0 || DebitValue[i] != 0 || CreditValue[i] != 0)
            {
                var code = ds.Tables[0].Rows[i]["Acct_Code"].ToString();
                string form = "frm_trialBalanceAccountDetail.aspx";
                string url =form+"?Acct_Code="+code+"&UserID="+UserId;
                html.Append("<tr style='height:30px'>");
                //html.Append("<td style='height:30px'><b>" + ds.Tables[0].Rows[i]["Acct_Code"].ToString() + "</b></td>");
                html.Append("<td style='height:30px;cursor:pointer;'><a href='"+url+"' runat='server'  AutoPostBack='True' style='color:black' >" + ds.Tables[0].Rows[i]["Acct_Description"].ToString() + " </a> </td>");
                html.Append("<td style='height:30px'><b>" + OpeningValue[i] + "</b></td>");
                html.Append("<td style='height:30px'><b>" + DebitValue[i] + "</b></td>");
                html.Append("<td style='height:30px'><b>" + CreditValue[i] + "</b></td>");
                html.Append("<td style='height:30px'><b>" + TotalValue[i] + "</b></td>");
                if (ddlHeadingLevel.SelectedItem.Value != "D1")
                {
                    html.Append("<td><a href='frm_TrialBalanceAccountDetail.aspx?Acct_Code=" + code + "&UserId=" + UserId + "&HeadingLevel=" + ddlHeadingLevel.SelectedItem.Value + "' class='btn btn-info'>Detail</a></td>");
                }
                
                html.Append("</tr>");
                var Code = ds.Tables[0].Rows[i]["Acct_Code"].ToString();
                var MainCode = Code.Substring(0, 1);

                if (MainCode.Equals("1") || MainCode.Equals("5") || MainCode.Equals("6"))
                {
                    if (TotalValue[i] < 0)
                    {
                        if (ddlComp > 0)
                        {
                            if (ddlBr > 0)
                            {
                                if (ddlFiscal > 0)
                                {
                                    Balance.Add(new TrialBalance(0, ds.Tables[0].Rows[i]["Acct_Code"].ToString(), ds.Tables[0].Rows[i]["Acct_Description"].ToString(), TotalValue[i], 0, TotalValue[i], DateTime.UtcNow, GocId, ddlComp, ddlBr, ddlFiscal, ddlHeadingLevel.SelectedItem.Value, UserId));
                                }
                                else
                                {
                                    Balance.Add(new TrialBalance(0, ds.Tables[0].Rows[i]["Acct_Code"].ToString(), ds.Tables[0].Rows[i]["Acct_Description"].ToString(), TotalValue[i], 0, TotalValue[i], DateTime.UtcNow, GocId, ddlComp, ddlBr, FiscalYearId, ddlHeadingLevel.SelectedItem.Value, UserId));
                                }

                            }
                            else
                            {
                                if (ddlFiscal > 0)
                                {
                                    Balance.Add(new TrialBalance(0, ds.Tables[0].Rows[i]["Acct_Code"].ToString(), ds.Tables[0].Rows[i]["Acct_Description"].ToString(), TotalValue[i], 0, TotalValue[i], DateTime.UtcNow, GocId, ddlComp, 0, ddlFiscal, ddlHeadingLevel.SelectedItem.Value, UserId));
                                }
                                else
                                {
                                    Balance.Add(new TrialBalance(0, ds.Tables[0].Rows[i]["Acct_Code"].ToString(), ds.Tables[0].Rows[i]["Acct_Description"].ToString(), TotalValue[i], 0, TotalValue[i], DateTime.UtcNow, GocId, ddlComp, 0, FiscalYearId, ddlHeadingLevel.SelectedItem.Value, UserId));
                                }
                            }

                        }
                        else
                        {
                            if (ddlFiscal > 0)
                            {
                                Balance.Add(new TrialBalance(0, ds.Tables[0].Rows[i]["Acct_Code"].ToString(), ds.Tables[0].Rows[i]["Acct_Description"].ToString(), TotalValue[i], 0, TotalValue[i], DateTime.UtcNow, GocId, 0, 0, ddlFiscal, ddlHeadingLevel.SelectedItem.Value, UserId));
                            }
                            else
                            {
                                Balance.Add(new TrialBalance(0, ds.Tables[0].Rows[i]["Acct_Code"].ToString(), ds.Tables[0].Rows[i]["Acct_Description"].ToString(), TotalValue[i], 0, TotalValue[i], DateTime.UtcNow, GocId, 0, 0, FiscalYearId, ddlHeadingLevel.SelectedItem.Value, UserId));
                            }

                        }


                    }
                    else
                    {
                        if (ddlComp > 0)
                        {
                            if (ddlBr > 0)
                            {
                                if (ddlFiscal > 0)
                                {
                                    Balance.Add(new TrialBalance(0, ds.Tables[0].Rows[i]["Acct_Code"].ToString(), ds.Tables[0].Rows[i]["Acct_Description"].ToString(), TotalValue[i], TotalValue[i], 0, DateTime.UtcNow, GocId, ddlComp, ddlBr, ddlFiscal, ddlHeadingLevel.SelectedItem.Value, UserId));
                                }
                                else
                                {
                                    Balance.Add(new TrialBalance(0, ds.Tables[0].Rows[i]["Acct_Code"].ToString(), ds.Tables[0].Rows[i]["Acct_Description"].ToString(), TotalValue[i], TotalValue[i], 0, DateTime.UtcNow, GocId, ddlComp, ddlBr, FiscalYearId, ddlHeadingLevel.SelectedItem.Value, UserId));
                                }

                            }
                            else
                            {
                                if (ddlFiscal > 0)
                                {
                                    Balance.Add(new TrialBalance(0, ds.Tables[0].Rows[i]["Acct_Code"].ToString(), ds.Tables[0].Rows[i]["Acct_Description"].ToString(), TotalValue[i], TotalValue[i], 0, DateTime.UtcNow, GocId, ddlComp, 0, ddlFiscal, ddlHeadingLevel.SelectedItem.Value, UserId));
                                }
                                else
                                {
                                    Balance.Add(new TrialBalance(0, ds.Tables[0].Rows[i]["Acct_Code"].ToString(), ds.Tables[0].Rows[i]["Acct_Description"].ToString(), TotalValue[i], TotalValue[i], 0, DateTime.UtcNow, GocId, ddlComp, 0, FiscalYearId, ddlHeadingLevel.SelectedItem.Value, UserId));
                                }
                            }

                        }
                        else
                        {
                            if (ddlFiscal > 0)
                            {
                                Balance.Add(new TrialBalance(0, ds.Tables[0].Rows[i]["Acct_Code"].ToString(), ds.Tables[0].Rows[i]["Acct_Description"].ToString(), TotalValue[i], TotalValue[i], 0, DateTime.UtcNow, GocId, 0, 0, ddlFiscal, ddlHeadingLevel.SelectedItem.Value, UserId));
                            }
                            else
                            {
                                Balance.Add(new TrialBalance(0, ds.Tables[0].Rows[i]["Acct_Code"].ToString(), ds.Tables[0].Rows[i]["Acct_Description"].ToString(), TotalValue[i], TotalValue[i], 0, DateTime.UtcNow, GocId, 0, 0, FiscalYearId, ddlHeadingLevel.SelectedItem.Value, UserId));
                            }

                        }
                    }

                }
                else
                {
                    if (TotalValue[i] < 0)
                    {
                        if (ddlComp > 0)
                        {
                            if (ddlBr > 0)
                            {
                                if (ddlFiscal > 0)
                                {
                                    Balance.Add(new TrialBalance(0, ds.Tables[0].Rows[i]["Acct_Code"].ToString(), ds.Tables[0].Rows[i]["Acct_Description"].ToString(), TotalValue[i], TotalValue[i], 0, DateTime.UtcNow, GocId, ddlComp, ddlBr, ddlFiscal, ddlHeadingLevel.SelectedItem.Value, UserId));
                                }
                                else
                                {
                                    Balance.Add(new TrialBalance(0, ds.Tables[0].Rows[i]["Acct_Code"].ToString(), ds.Tables[0].Rows[i]["Acct_Description"].ToString(), TotalValue[i], TotalValue[i], 0, DateTime.UtcNow, GocId, ddlComp, ddlBr, FiscalYearId, ddlHeadingLevel.SelectedItem.Value, UserId));
                                }

                            }
                            else
                            {
                                if (ddlFiscal > 0)
                                {
                                    Balance.Add(new TrialBalance(0, ds.Tables[0].Rows[i]["Acct_Code"].ToString(), ds.Tables[0].Rows[i]["Acct_Description"].ToString(), TotalValue[i], TotalValue[i], 0, DateTime.UtcNow, GocId, ddlComp, 0, ddlFiscal, ddlHeadingLevel.SelectedItem.Value, UserId));
                                }
                                else
                                {
                                    Balance.Add(new TrialBalance(0, ds.Tables[0].Rows[i]["Acct_Code"].ToString(), ds.Tables[0].Rows[i]["Acct_Description"].ToString(), TotalValue[i], TotalValue[i], 0, DateTime.UtcNow, GocId, ddlComp, 0, FiscalYearId, ddlHeadingLevel.SelectedItem.Value, UserId));
                                }
                            }

                        }
                        else
                        {
                            if (ddlFiscal > 0)
                            {
                                Balance.Add(new TrialBalance(0, ds.Tables[0].Rows[i]["Acct_Code"].ToString(), ds.Tables[0].Rows[i]["Acct_Description"].ToString(), TotalValue[i], TotalValue[i], 0, DateTime.UtcNow, GocId, 0, 0, ddlFiscal, ddlHeadingLevel.SelectedItem.Value, UserId));
                            }
                            else
                            {
                                Balance.Add(new TrialBalance(0, ds.Tables[0].Rows[i]["Acct_Code"].ToString(), ds.Tables[0].Rows[i]["Acct_Description"].ToString(), TotalValue[i], TotalValue[i], 0, DateTime.UtcNow, GocId, 0, 0, FiscalYearId, ddlHeadingLevel.SelectedItem.Value, UserId));
                            }

                        }


                    }
                    else
                    {
                        if (ddlComp > 0)
                        {
                            if (ddlBr > 0)
                            {
                                if (ddlFiscal > 0)
                                {
                                    Balance.Add(new TrialBalance(0, ds.Tables[0].Rows[i]["Acct_Code"].ToString(), ds.Tables[0].Rows[i]["Acct_Description"].ToString(), TotalValue[i], 0, TotalValue[i], DateTime.UtcNow, GocId, ddlComp, ddlBr, ddlFiscal, ddlHeadingLevel.SelectedItem.Value, UserId));
                                }
                                else
                                {
                                    Balance.Add(new TrialBalance(0, ds.Tables[0].Rows[i]["Acct_Code"].ToString(), ds.Tables[0].Rows[i]["Acct_Description"].ToString(), TotalValue[i], 0, TotalValue[i], DateTime.UtcNow, GocId, ddlComp, ddlBr, FiscalYearId, ddlHeadingLevel.SelectedItem.Value, UserId));
                                }

                            }
                            else
                            {
                                if (ddlFiscal > 0)
                                {
                                    Balance.Add(new TrialBalance(0, ds.Tables[0].Rows[i]["Acct_Code"].ToString(), ds.Tables[0].Rows[i]["Acct_Description"].ToString(), TotalValue[i], 0, TotalValue[i], DateTime.UtcNow, GocId, ddlComp, 0, ddlFiscal, ddlHeadingLevel.SelectedItem.Value, UserId));
                                }
                                else
                                {
                                    Balance.Add(new TrialBalance(0, ds.Tables[0].Rows[i]["Acct_Code"].ToString(), ds.Tables[0].Rows[i]["Acct_Description"].ToString(), TotalValue[i], 0, TotalValue[i], DateTime.UtcNow, GocId, ddlComp, 0, FiscalYearId, ddlHeadingLevel.SelectedItem.Value, UserId));
                                }
                            }

                        }
                        else
                        {
                            if (ddlFiscal > 0)
                            {
                                Balance.Add(new TrialBalance(0, ds.Tables[0].Rows[i]["Acct_Code"].ToString(), ds.Tables[0].Rows[0]["Acct_Description"].ToString(), TotalValue[i], 0, TotalValue[i], DateTime.UtcNow, GocId, 0, 0, ddlFiscal, ddlHeadingLevel.SelectedItem.Value, UserId));
                            }
                            else
                            {
                                Balance.Add(new TrialBalance(0, ds.Tables[0].Rows[i]["Acct_Code"].ToString(), ds.Tables[0].Rows[0]["Acct_Description"].ToString(), TotalValue[i], 0, TotalValue[i], DateTime.UtcNow, GocId, 0, 0, FiscalYearId, ddlHeadingLevel.SelectedItem.Value, UserId));
                            }

                        }
                    }
                }

            }
            //html.Append("<tr>");
            //html.Append("<td><asp:Button ID='Save_"+i+"_Data' runat='server' Text='Button' /></td>");
            //html.Append("</tr>");
        }
        html.Append("</tbody>");
        html.Append("</table>");
        html.Append("</div");
        html.Append("</div>");
        Table_Div.Controls.Add(new Literal { Text = html.ToString() });
        //AssetOpeningText.Text = OpeningValue[0].ToString();
        //LiabilityOpeningText.Text = OpeningValue[1].ToString();
        //CapitalOpeningText.Text = OpeningValue[2].ToString();
        //RevenueOpeningText.Text = OpeningValue[3].ToString();
        //ExpenseOpeningText.Text = OpeningValue[4].ToString();
        //AdjustmentOpeningText.Text = OpeningValue[5].ToString();

        //AssetDebitText.Text = DebitValue[0].ToString();
        //LiabilityDebitText.Text = DebitValue[1].ToString();
        //CapitalDebitText.Text = DebitValue[2].ToString();
        //RevenueDebitText.Text = DebitValue[3].ToString();
        //ExpenseDebitText.Text = DebitValue[4].ToString();
        //AdjustmentDebitText.Text = DebitValue[5].ToString();

        //AssetCreditText.Text = CreditValue[0].ToString();
        //LiabilityCreditText.Text = CreditValue[1 ].ToString();
        //CapitalCreditText.Text = CreditValue[2].ToString();
        //RevenueCreditText.Text = CreditValue[3].ToString();
        //ExpenseCreditText.Text = CreditValue[4].ToString();
        //AdjustmentCreditText.Text = CreditValue[5].ToString();

        //AssetTotalText.Text = TotalValue[0].ToString();
        //LiabilityTotalText.Text = TotalValue[1].ToString();
        //CapitalTotalText.Text = TotalValue[2].ToString();
        //RevenueTotalText.Text = TotalValue[3].ToString();
        //ExpenseTotalText.Text = TotalValue[4].ToString();
        //AdjustmentTotalText.Text = TotalValue[5].ToString();
        modal_DataDiv.Visible = false;
        modal_TrialBalanceDiv.Visible = true;
        if (flag)
        {

            if (ddlFiscalYear.SelectedItem.Value.Equals("Please select..."))
            {
                ddlFiscal = 0;
            }
            else
            {
                ddlFiscal = Convert.ToInt32(ddlFiscalYear.SelectedItem.Value);
            }
            if (ddlCompany.SelectedItem.Value.Equals("Please select..."))
            {
                ddlComp = 0;
            }
            else
            {
                ddlComp = Convert.ToInt32(ddlCompany.SelectedItem.Value);
            }
            if (ddlComp > 0)
            {

                if (ddlBranch.SelectedItem.Value.Equals("Please select..."))
                {
                    ddlBr = 0;
                }
                else
                {
                    ddlBr = Convert.ToInt32(ddlBranch.SelectedItem.Value);
                }
                if (ddlBr > 0)
                {
                    if (ddlFiscal > 0)
                    {
                        SaveinDataBase(ds, Balance, count, TotalValue, GocId, ddlComp, ddlBr, ddlFiscal);
                    }
                    else
                    {
                        SaveinDataBase(ds, Balance, count, TotalValue, GocId, ddlComp, ddlBr, FiscalYearId);
                    }
                }
                else
                {
                    if (ddlFiscal > 0)
                    {
                        SaveinDataBase(ds, Balance, count, TotalValue, GocId, ddlComp, 0, ddlFiscal);
                    }
                    else
                    {
                        SaveinDataBase(ds, Balance, count, TotalValue, GocId, ddlComp, 0, FiscalYearId);
                    }
                }

            }
            else
            {
                if (ddlFiscal > 0)
                {
                    SaveinDataBase(ds, Balance, count, TotalValue, GocId, 0, 0, ddlFiscal);
                }
                else
                {
                    SaveinDataBase(ds, Balance, count, TotalValue, GocId, 0, 0, FiscalYearId);
                }
            }
        }

    }

    public void AddLinkButton(StringBuilder html) { 
    }
    public void btnSave_Click(object sender, EventArgs e)
    {
        string Query = "select Acct_Code,Acct_Description From Set_Coa_detail where Acct_Code in (Select Acct_Code From Set_Coa_Detail where Head_detail_ID = '" + ddlHeadingLevel.SelectedItem.Value + "')";
        QueryAdapter = new SqlDataAdapter(Query, con);
        QueryDataSet = new DataSet();
        QueryAdapter.Fill(QueryDataSet);
        GenerateTrailBalance(QueryDataSet, true);
    }



    protected void btnSaveInExcel_Click(object sender, EventArgs e)
    {

    }
    protected void SaveinDataBase(DataSet ds, List<TrialBalance> Balance, int count, List<Double> TotalValue, int GocId, int CompId, int BranchId, int FiscalYearId)
    {
        string Query;

        con.Close();
        con.Open();
        if (CompId > 0)
        {
            if (BranchId > 0)
            {
                Query = "SELECT * fROM SET_TRIALBALANCE WHERE GOCID = " + GocId + " AND COMPID = " + CompId + " AND BRANCHID = " + BranchId + " AND FISCALYEARID = " + FiscalYearId + " AND HEADINGLEVEL = '" + ddlHeadingLevel.SelectedValue + "' AND convert(date, Tb_Date) = '" + DateTime.UtcNow.Date + "' ";

            }
            else
            {
                Query = "SELECT * fROM SET_TRIALBALANCE WHERE GOCID = " + GocId + " AND COMPID = " + CompId + " AND BranchId is NULL And FISCALYEARID = " + FiscalYearId + " AND HEADINGLEVEL = '" + ddlHeadingLevel.SelectedValue + "' AND convert(date, Tb_Date) = '" + DateTime.UtcNow.Date + "'";
            }
        }
        else
        {
            Query = "SELECT * fROM SET_TRIALBALANCE WHERE GOCID = " + GocId + " AND CompId is NULL And BranchId is NUll And FISCALYEARID = " + FiscalYearId + " AND HEADINGLEVEL = '" + ddlHeadingLevel.SelectedValue + "' AND convert(date, Tb_Date)='" + DateTime.UtcNow.Date.ToString() + "'";
        }

        QueryAdapter = new SqlDataAdapter(Query, con);
        QueryDataSet = new DataSet();
        //DataSet temp = new DataSet();
        QueryAdapter.Fill(QueryDataSet);
        if (QueryDataSet.Tables[0].Rows.Count > 0)
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "ConfirmForUpdate()", true);
            UpdateTrialBalanceQuery(ds, Balance, count, TotalValue, con, GocId, CompId, BranchId, FiscalYearId);
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "Editalert()", true);
            lib.WriteExcelFile(Query,"Update", "TrialBalance-" + ddlHeadingLevel.SelectedValue + "-" + DateTime.UtcNow.ToShortDateString(), Balance, count, GocId, CompId, BranchId, FiscalYearId);
        }// For Update 
        else
        {// For Insert
            InsertTrialBalanceQuery(ds, Balance, count, TotalValue, con, GocId, CompId, BranchId, FiscalYearId);
            lib.WriteExcelFile(Query,"Insert", "TrialBalance-" + ddlHeadingLevel.SelectedValue + "-" + DateTime.UtcNow.ToShortDateString(), Balance, count, GocId, CompId, BranchId, FiscalYearId);
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "alertme()", true);
        }
        con.Close();
        SaveInDataBase.Visible = true;
        Response.Redirect("/frm_Trialbalance.aspx?UserId=" + UserId + "&ddlComp=" + ddlComp + "&ddlBr=" + ddlBr + "&ddlFiscalYear=" + ddlFiscal + "&fiscaly=" + Request.Cookies["FiscalYearId"].Value + "&GocId = " + GocId + "&CompId = " + Request.Cookies["CompId"].Value + "&BranchId = " + Request.Cookies["BranchId"].Value + "&Fiscal=" + Request.Cookies["FiscalYearId"].Value + "&UsergrpID=" + Request.Cookies["UserGrpID"].Value + "&HeadingLevel=" + ddlHeadingLevel.SelectedItem.Value);
    }

    protected void InsertTrialBalanceQuery(DataSet ds, List<TrialBalance> Balance, int count, List<Double> TotalValue, SqlConnection con, int GocId, int CompId, int BranchId, int FiscalYearId)
    {
        String Query = "";
        for (int i = 0; i < Balance.Count; i++)
        {
            //var Acct_Code = ds.Tables[0].Rows[i]["Acct_Code"].ToString();
            var MainAccountCode = Balance[i].Tb_Acct_Code.Substring(0, 1);
            if (MainAccountCode.Equals("1") || MainAccountCode.Equals("5") || MainAccountCode.Equals("6"))
            {
                if (Balance[i].TotalValue < 0)
                {
                    if (CompId > 0)
                    {
                        if (BranchId > 0)
                        {
                            Query = "INSERT INTO Set_TrialBalance ([Tb_Acct_Code],[Tb_DrAmount],[Tb_CrAmount],[Tb_Date],[GocId],[CompId],[BranchId],[FiscalYearId],[HeadingLevel],[CreatedBy]) VALUES " + "('" + Balance[i].Tb_Acct_Code + "'," + Balance[i].Tb_DrAmount + "," + (Balance[i].Tb_CrAmount*-1) + ",'" + DateTime.UtcNow + "'," + GocId + "," + CompId + "," + BranchId + "," + FiscalYearId + ",'" + ddlHeadingLevel.SelectedValue + "','" + UserId + "')";
                            dm.InsertWithoutMessage(Query, con);
                        }
                        else
                        {
                            Query = "INSERT INTO Set_TrialBalance ([Tb_Acct_Code],[Tb_DrAmount],[Tb_CrAmount],[Tb_Date],[GocId],[CompId],[FiscalYearId],[HeadingLevel],[CreatedBy]) VALUES " + "('" + Balance[i].Tb_Acct_Code + "'," + Balance[i].Tb_DrAmount + "," + (Balance[i].Tb_CrAmount * -1) + ",'" + DateTime.UtcNow + "'," + GocId + "," + CompId + "," + FiscalYearId + ",'" + ddlHeadingLevel.SelectedValue + "','" + UserId + "')";
                            dm.InsertWithoutMessage(Query, con);

                        }
                    }
                    else
                    {
                        Query = "INSERT INTO Set_TrialBalance ([Tb_Acct_Code],[Tb_DrAmount],[Tb_CrAmount],[Tb_Date],[GocId],[FiscalYearId],[HeadingLevel],[CreatedBy]) VALUES " + "('" + Balance[i].Tb_Acct_Code + "'," + Balance[i].Tb_DrAmount + "," + (Balance[i].Tb_CrAmount * -1) + ",'" + DateTime.UtcNow + "'," + GocId + "," + FiscalYearId + ",'" + ddlHeadingLevel.SelectedValue + "','" + UserId + "')";
                        dm.InsertWithoutMessage(Query, con);
                    }

                }
                else
                {
                    if (CompId > 0)
                    {
                        if (BranchId > 0)
                        {
                            Query = "INSERT INTO Set_TrialBalance ([Tb_Acct_Code],[Tb_DrAmount],[Tb_CrAmount],[Tb_Date],[GocId],[CompId],[BranchId],[FiscalYearId],[HeadingLevel],[CreatedBy]) VALUES " + "('" + Balance[i].Tb_Acct_Code + "'," + Balance[i].Tb_DrAmount + "," + Balance[i].Tb_CrAmount + ",'" + DateTime.UtcNow + "'," + GocId + "," + CompId + "," + BranchId + "," + FiscalYearId + ",'" + ddlHeadingLevel.SelectedValue + "','" + UserId + "')";
                            dm.InsertWithoutMessage(Query, con);
                        }
                        else
                        {
                            Query = "INSERT INTO Set_TrialBalance ([Tb_Acct_Code],[Tb_DrAmount],[Tb_CrAmount],[Tb_Date],[GocId],[CompId],[FiscalYearId],[HeadingLevel],[CreatedBy]) VALUES " + "('" + Balance[i].Tb_Acct_Code + "'," + Balance[i].Tb_DrAmount + "," + (Balance[i].Tb_CrAmount * -1) + ",'" + DateTime.UtcNow + "'," + GocId + "," + CompId + "," + FiscalYearId + ",'" + ddlHeadingLevel.SelectedValue + "','" + UserId + "')";
                            dm.InsertWithoutMessage(Query, con);

                        }
                    }
                    else
                    {

                        Query = "INSERT INTO Set_TrialBalance ([Tb_Acct_Code],[Tb_DrAmount],[Tb_CrAmount],[Tb_Date],[GocId],[FiscalYearId],[HeadingLevel],[CreatedBy]) VALUES " + "('" + Balance[i].Tb_Acct_Code + "'," + Balance[i].Tb_DrAmount + "," + Balance[i].Tb_CrAmount + ",'" + DateTime.UtcNow + "'," + GocId + "," + FiscalYearId + ",'" + ddlHeadingLevel.SelectedValue + "','" + UserId + "')";
                        dm.InsertWithoutMessage(Query, con);
                    }
                }
            }
            else
            {
                if (Balance[i].TotalValue < 0)
                {
                    if (CompId > 0)
                    {
                        if (BranchId > 0)
                        {
                            Query = "INSERT INTO Set_TrialBalance ([Tb_Acct_Code],[Tb_DrAmount],[Tb_CrAmount],[Tb_Date],[GocId],[CompId],[BranchId],[FiscalYearId],[HeadingLevel],[CreatedBy]) VALUES " + "('" + Balance[i].Tb_Acct_Code + "'," + (Balance[i].Tb_DrAmount*-1) + "," + Balance[i].Tb_CrAmount + ",'" + DateTime.UtcNow + "'," + GocId + "," + CompId + "," + BranchId + "," + FiscalYearId + ",'" + ddlHeadingLevel.SelectedValue + "','" + UserId + "')";
                            dm.InsertWithoutMessage(Query, con);
                        }
                        else
                        {
                            Query = "INSERT INTO Set_TrialBalance ([Tb_Acct_Code],[Tb_DrAmount],[Tb_CrAmount],[Tb_Date],[GocId],[CompId],[FiscalYearId],[HeadingLevel],[CreatedBy]) VALUES " + "('" + Balance[i].Tb_Acct_Code + "'," + (Balance[i].Tb_DrAmount * -1) + "," + Balance[i].Tb_CrAmount + ",'" + DateTime.UtcNow + "'," + GocId + "," + CompId + "," + FiscalYearId + ",'" + ddlHeadingLevel.SelectedValue + "','" + UserId + "')";
                            dm.InsertWithoutMessage(Query, con);
                        }
                    }
                    else
                    {
                        Query = "INSERT INTO Set_TrialBalance ([Tb_Acct_Code],[Tb_DrAmount],[Tb_CrAmount],[Tb_Date],[GocId],[FiscalYearId],[HeadingLevel],[CreatedBy]) VALUES " + "('" + Balance[i].Tb_Acct_Code + "'," + (Balance[i].Tb_DrAmount * -1) + "," + Balance[i].Tb_CrAmount + ",'" + DateTime.UtcNow + "'," + GocId + "," + FiscalYearId + ",'" + ddlHeadingLevel.SelectedValue + "','" + UserId + "')";
                        dm.InsertWithoutMessage(Query, con);
                    }


                }
                else
                {
                    if (CompId > 0)
                    {
                        if (BranchId > 0)
                        {
                            Query = "INSERT INTO Set_TrialBalance ([Tb_Acct_Code],[Tb_DrAmount],[Tb_CrAmount],[Tb_Date],[GocId],[CompId],[BranchId],[FiscalYearId],[HeadingLevel],[CreatedBy]) VALUES " + "('" + Balance[i].Tb_Acct_Code + "'," + Balance[i].Tb_DrAmount + "," + Balance[i].Tb_CrAmount + ",'" + DateTime.UtcNow + "'," + GocId + "," + CompId + "," + BranchId + "," + FiscalYearId + ",'" + ddlHeadingLevel.SelectedValue + "','" + UserId + "')";
                            dm.InsertWithoutMessage(Query, con);
                        }
                        else
                        {
                            Query = "INSERT INTO Set_TrialBalance ([Tb_Acct_Code],[Tb_DrAmount],[Tb_CrAmount],[Tb_Date],[GocId],[CompId],[FiscalYearId],[HeadingLevel],[CreatedBy]) VALUES " + "('" + Balance[i].Tb_Acct_Code + "'," + Balance[i].Tb_DrAmount + "," + Balance[i].Tb_CrAmount + ",'" + DateTime.UtcNow + "'," + GocId + "," + CompId + "," + FiscalYearId + ",'" + ddlHeadingLevel.SelectedValue + "','" + UserId + "')";
                            dm.InsertWithoutMessage(Query, con);
                        }
                    }
                    else
                    {
                        Query = "INSERT INTO Set_TrialBalance ([Tb_Acct_Code],[Tb_DrAmount],[Tb_CrAmount],[Tb_Date],[GocId],[FiscalYearId],[HeadingLevel],[CreatedBy]) VALUES " + "('" + Balance[i].Tb_Acct_Code + "'," + Balance[i].Tb_DrAmount + "," + Balance[i].Tb_CrAmount + ",'" + DateTime.UtcNow + "'," + GocId + "," + FiscalYearId + ",'" + ddlHeadingLevel.SelectedValue + "','" + UserId + "')";
                        dm.InsertWithoutMessage(Query, con);
                    }

                }
            }

        }


        //for (int i = 0; i < count; i++)
        //{
        //    var Acct_Code = ds.Tables[0].Rows[i]["Acct_Code"].ToString();
        //    var MainAccountCode = Acct_Code.Substring(0, 1);
        //    if (MainAccountCode.Equals("1") || MainAccountCode.Equals("5") || MainAccountCode.Equals("6"))
        //    {
        //        if (TotalValue[i] < 0)
        //        {
        //            if (CompId > 0)
        //            {
        //                if (BranchId > 0)
        //                {
        //                    Query = "INSERT INTO Set_TrialBalance ([Tb_Acct_Code],[Tb_DrAmount],[Tb_CrAmount],[Tb_Date],[GocId],[CompId],[BranchId],[FiscalYearId],[HeadingLevel],[CreatedBy]) VALUES " + "('" + Acct_Code + "'," + 0 + "," + TotalValue[i] + ",'" + DateTime.UtcNow + "'," + GocId + "," + CompId + "," + BranchId + "," + FiscalYearId + ",'" + ddlHeadingLevel.SelectedValue + "','" + UserId + "')";
        //                    dm.InsertWithoutMessage(Query, con);
        //                }
        //                else
        //                {
        //                    Query = "INSERT INTO Set_TrialBalance ([Tb_Acct_Code],[Tb_DrAmount],[Tb_CrAmount],[Tb_Date],[GocId],[CompId],[FiscalYearId],[HeadingLevel],[CreatedBy]) VALUES " + "('" + Acct_Code + "'," + 0 + "," + TotalValue[i] + ",'" + DateTime.UtcNow + "'," + GocId + "," + CompId + "," + FiscalYearId + ",'" + ddlHeadingLevel.SelectedValue + "','" + UserId + "')";
        //                    dm.InsertWithoutMessage(Query, con);

        //                }
        //            }
        //            else
        //            {
        //                Query = "INSERT INTO Set_TrialBalance ([Tb_Acct_Code],[Tb_DrAmount],[Tb_CrAmount],[Tb_Date],[GocId],[FiscalYearId],[HeadingLevel],[CreatedBy]) VALUES " + "('" + Acct_Code + "'," + 0 + "," + TotalValue[i] + ",'" + DateTime.UtcNow + "'," + GocId + "," + FiscalYearId + ",'" + ddlHeadingLevel.SelectedValue + "','" + UserId + "')";
        //                dm.InsertWithoutMessage(Query, con);
        //            }

        //        }
        //        else
        //        {
        //            if (CompId > 0)
        //            {
        //                if (BranchId > 0)
        //                {
        //                    Query = "INSERT INTO Set_TrialBalance ([Tb_Acct_Code],[Tb_DrAmount],[Tb_CrAmount],[Tb_Date],[GocId],[CompId],[BranchId],[FiscalYearId],[HeadingLevel],[CreatedBy]) VALUES " + "('" + Acct_Code + "'," + TotalValue[i] + "," + 0 + ",'" + DateTime.UtcNow + "'," + GocId + "," + CompId + "," + BranchId + "," + FiscalYearId + ",'" + ddlHeadingLevel.SelectedValue + "','" + UserId + "')";
        //                    dm.InsertWithoutMessage(Query, con);
        //                }
        //                else
        //                {
        //                    Query = "INSERT INTO Set_TrialBalance ([Tb_Acct_Code],[Tb_DrAmount],[Tb_CrAmount],[Tb_Date],[GocId],[CompId],[FiscalYearId],[HeadingLevel],[CreatedBy]) VALUES " + "('" + Acct_Code + "'," + TotalValue[i] + "," + 0 + ",'" + DateTime.UtcNow + "'," + GocId + "," + CompId + "," + FiscalYearId + ",'" + ddlHeadingLevel.SelectedValue + "','" + UserId + "')";
        //                    dm.InsertWithoutMessage(Query, con);

        //                }
        //            }
        //            else
        //            {

        //                Query = "INSERT INTO Set_TrialBalance ([Tb_Acct_Code],[Tb_DrAmount],[Tb_CrAmount],[Tb_Date],[GocId],[FiscalYearId],[HeadingLevel],[CreatedBy]) VALUES " + "('" + Acct_Code + "'," + TotalValue[i] + "," + 0 + ",'" + DateTime.UtcNow + "'," + GocId + "," + FiscalYearId + ",'" + ddlHeadingLevel.SelectedValue + "','" + UserId + "')";
        //                dm.InsertWithoutMessage(Query, con);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (TotalValue[i] < 0)
        //        {
        //            if (CompId > 0)
        //            {
        //                if (BranchId > 0)
        //                {
        //                    Query = "INSERT INTO Set_TrialBalance ([Tb_Acct_Code],[Tb_DrAmount],[Tb_CrAmount],[Tb_Date],[GocId],[CompId],[BranchId],[FiscalYearId],[HeadingLevel],[CreatedBy]) VALUES " + "('" + Acct_Code + "'," + TotalValue[i] + "," + 0 + ",'" + DateTime.UtcNow + "'," + GocId + "," + CompId + "," + BranchId + "," + FiscalYearId + ",'" + ddlHeadingLevel.SelectedValue + "','" + UserId + "')";
        //                    dm.InsertWithoutMessage(Query, con);
        //                }
        //                else
        //                {
        //                    Query = "INSERT INTO Set_TrialBalance ([Tb_Acct_Code],[Tb_DrAmount],[Tb_CrAmount],[Tb_Date],[GocId],[CompId],[FiscalYearId],[HeadingLevel],[CreatedBy]) VALUES " + "('" + Acct_Code + "'," + TotalValue[i] + "," + 0 + ",'" + DateTime.UtcNow + "'," + GocId + "," + CompId + "," + FiscalYearId + ",'" + ddlHeadingLevel.SelectedValue + "','" + UserId + "')";
        //                    dm.InsertWithoutMessage(Query, con);
        //                }
        //            }
        //            else
        //            {
        //                Query = "INSERT INTO Set_TrialBalance ([Tb_Acct_Code],[Tb_DrAmount],[Tb_CrAmount],[Tb_Date],[GocId],[FiscalYearId],[HeadingLevel],[CreatedBy]) VALUES " + "('" + Acct_Code + "'," + TotalValue[i] + "," + 0 + ",'" + DateTime.UtcNow + "'," + GocId + "," + FiscalYearId + ",'" + ddlHeadingLevel.SelectedValue + "','" + UserId + "')";
        //                dm.InsertWithoutMessage(Query, con);
        //            }


        //        }
        //        else
        //        {
        //            if (CompId > 0)
        //            {
        //                if (BranchId > 0)
        //                {
        //                    Query = "INSERT INTO Set_TrialBalance ([Tb_Acct_Code],[Tb_DrAmount],[Tb_CrAmount],[Tb_Date],[GocId],[CompId],[BranchId],[FiscalYearId],[HeadingLevel],[CreatedBy]) VALUES " + "('" + Acct_Code + "'," + 0 + "," + TotalValue[i] + ",'" + DateTime.UtcNow + "'," + GocId + "," + CompId + "," + BranchId + "," + FiscalYearId + ",'" + ddlHeadingLevel.SelectedValue + "','" + UserId + "')";
        //                    dm.InsertWithoutMessage(Query, con);
        //                }
        //                else
        //                {
        //                    Query = "INSERT INTO Set_TrialBalance ([Tb_Acct_Code],[Tb_DrAmount],[Tb_CrAmount],[Tb_Date],[GocId],[CompId],[FiscalYearId],[HeadingLevel],[CreatedBy]) VALUES " + "('" + Acct_Code + "'," + 0 + "," + TotalValue[i] + ",'" + DateTime.UtcNow + "'," + GocId + "," + CompId + "," + FiscalYearId + ",'" + ddlHeadingLevel.SelectedValue + "','" + UserId + "')";
        //                    dm.InsertWithoutMessage(Query, con);
        //                }
        //            }
        //            else
        //            {
        //                Query = "INSERT INTO Set_TrialBalance ([Tb_Acct_Code],[Tb_DrAmount],[Tb_CrAmount],[Tb_Date],[GocId],[FiscalYearId],[HeadingLevel],[CreatedBy]) VALUES " + "('" + Acct_Code + "'," + 0 + "," + TotalValue[i] + ",'" + DateTime.UtcNow + "'," + GocId + "," + FiscalYearId + ",'" + ddlHeadingLevel.SelectedValue + "','" + UserId + "')";
        //                dm.InsertWithoutMessage(Query, con);
        //            }

        //        }
        //    }

        //}


    }

    protected void UpdateTrialBalanceQuery(DataSet ds, List<TrialBalance> Balance, int count, List<Double> TotalValue, SqlConnection con, int GocId, int CompId, int BranchId, int FiscalYearId)
    {
        string Query = "";
        for (int i = 0; i < Balance.Count; i++)
        {

            var MainAccountCode = Balance[i].Tb_Acct_Code.Substring(0, 1);
            if (MainAccountCode.Equals("1") || MainAccountCode.Equals("5") || MainAccountCode.Equals("6"))
            {// for debit accounts
                if (Balance[i].TotalValue < 0)
                {
                    if (CompId > 0)
                    {
                        if (BranchId > 0)
                        {
                            Query = "Update Set_TrialBalance Set Tb_DrAmount=" + Balance[i].Tb_DrAmount + ",Tb_CrAmount = " + (Balance[i].Tb_CrAmount*-1) + " Where Tb_Acct_Code='" + Balance[i].Tb_Acct_Code + "' And GocId=" + GocId + " And CompId=" + CompId + " And BranchId=" + BranchId + " And FiscalYearId=" + FiscalYearId + " And convert(date, Tb_Date)='" + DateTime.UtcNow.Date + "'";
                            dm.UpdateWithoutMessage(Query, con);

                        }
                        else
                        {
                            Query = "Update Set_TrialBalance Set Tb_DrAmount=" + Balance[i].Tb_DrAmount + ",Tb_CrAmount=" + (Balance[i].Tb_CrAmount * -1) + " Where Tb_Acct_Code='" + Balance[i].Tb_Acct_Code + "' And GocId=" + GocId + " And CompId=" + CompId + " And FiscalYearId=" + FiscalYearId + ", convert(date, Tb_Date)='" + DateTime.UtcNow.Date + "'";
                            dm.UpdateWithoutMessage(Query, con);
                        }
                    }
                    else
                    {
                        Query = "Update Set_TrialBalance Set Tb_DrAmount=" + Balance[i].Tb_DrAmount + ", Tb_CrAmount=" + (Balance[i].Tb_CrAmount * -1) + " Where Tb_Acct_Code='" + Balance[i].Tb_Acct_Code + "' And GocId=" + GocId + " And FiscalYearId=" + FiscalYearId + " And convert(date, Tb_Date)='" + DateTime.UtcNow.Date + "'";
                        dm.UpdateWithoutMessage(Query, con);

                    }

                }
                else
                {

                    if (CompId > 0)
                    {
                        if (BranchId > 0)
                        {
                            Query = "Update Set_TrialBalance Set Tb_DrAmount=" + Balance[i].Tb_DrAmount + ", Tb_CrAmount=" + Balance[i].Tb_CrAmount + " Where Tb_Acct_Code='" + Balance[i].Tb_Acct_Code + "' And GocId=" + GocId + " And CompId=" + CompId + " And BranchId=" + BranchId + " And FiscalYearId=" + FiscalYearId + " And convert(date, Tb_Date)='" + DateTime.UtcNow.Date + "'";
                            dm.UpdateWithoutMessage(Query, con);
                        }
                        else
                        {
                            Query = "Update Set_TrialBalance Set Tb_DrAmount=" + Balance[i].Tb_DrAmount + ", Tb_CrAmount=" + Balance[i].Tb_CrAmount + " Where Tb_Acct_Code='" + Balance[i].Tb_Acct_Code + "' And GocId=" + GocId + " And CompId=" + CompId + " And FiscalYearId=" + FiscalYearId + " And convert(date, Tb_Date)='" + DateTime.UtcNow.Date + "'";
                            dm.UpdateWithoutMessage(Query, con);
                        }
                    }
                    else
                    {
                        Query = "Update Set_TrialBalance Set Tb_DrAmount=" + Balance[i].Tb_DrAmount + ",Tb_CrAmount=" + Balance[i].Tb_CrAmount + " Where Tb_Acct_Code='" + Balance[i].Tb_Acct_Code + "' And GocId=" + GocId + " And FiscalYearId=" + FiscalYearId + " And convert(date, Tb_Date)='" + DateTime.UtcNow.Date + "'";
                        dm.UpdateWithoutMessage(Query, con);
                    }
                }

            }//for credit accounts
            else
            {
                if (Balance[i].TotalValue < 0)
                {
                    if (CompId > 0)
                    {
                        if (BranchId > 0)
                        {
                            Query = "Update Set_TrialBalance Set Tb_DrAmount=" + (Balance[i].Tb_DrAmount*-1) + " ,Tb_CrAmount=" + Balance[i].Tb_CrAmount + " Where Tb_Acct_Code='" + Balance[i].Tb_Acct_Code + "' And GocId=" + GocId + " And CompId=" + CompId + " And BranchId=" + BranchId + " And FiscalYearId=" + FiscalYearId + " And convert(date, Tb_Date)=" + DateTime.UtcNow.Date + "'";
                            dm.UpdateWithoutMessage(Query, con);
                        }
                        else
                        {
                            Query = "Update Set_TrialBalance Set Tb_DrAmount=" + (Balance[i].Tb_DrAmount * -1) + " ,Tb_CrAmount=" + Balance[i].Tb_CrAmount + " Where Tb_Acct_Code='" + Balance[i].Tb_Acct_Code + "' And GocId=" + GocId + " And CompId=" + CompId + " And FiscalYearId=" + FiscalYearId + " And convert(date, Tb_Date)='" + DateTime.UtcNow.Date + "'";
                            dm.UpdateWithoutMessage(Query, con);
                        }
                    }
                    else
                    {
                        Query = "Update Set_TrialBalance Set Tb_DrAmount=" + (Balance[i].Tb_DrAmount * -1) + ",Tb_CrAmount=" + Balance[i].Tb_CrAmount + " Where Tb_Acct_Code='" + Balance[i].Tb_Acct_Code + "' And GocId=" + GocId + " And FiscalYearId=" + FiscalYearId + " And convert(date, Tb_Date)='" + DateTime.UtcNow.Date + "'";
                        dm.UpdateWithoutMessage(Query, con);
                    }
                }
                else
                {
                    if (CompId > 0)
                    {
                        if (BranchId > 0)
                        {
                            Query = "Update Set_TrialBalance Set Tb_DrAmount=" + Balance[i].Tb_DrAmount + ",Tb_CrAmount=" + Balance[i].Tb_CrAmount + " Where Tb_Acct_Code='" + Balance[i].Tb_Acct_Code + "' And GocId=" + GocId + " And CompId=" + CompId + " And BranchId=" + BranchId + " And FiscalYearId=" + FiscalYearId + " And convert(date, Tb_Date)='" + DateTime.UtcNow.Date + "'";
                            dm.UpdateWithoutMessage(Query, con);

                        }
                        else
                        {
                            Query = "Update Set_TrialBalance Set Tb_DrAmount=" + Balance[i].Tb_DrAmount + ",Tb_CrAmount=" + Balance[i].Tb_CrAmount + " Where Tb_Acct_Code='" + Balance[i].Tb_Acct_Code + "' And GocId=" + GocId + " And CompId=" + CompId + " And FiscalYearId=" + FiscalYearId + " And convert(date, Tb_Date)='" + DateTime.UtcNow.Date + "'";
                            dm.UpdateWithoutMessage(Query, con);
                        }
                    }
                    else
                    {
                        Query = "Update Set_TrialBalance Set Tb_DrAmount=" + Balance[i].Tb_DrAmount + ",Tb_CrAmount=" + Balance[i].Tb_CrAmount + " Where Tb_Acct_Code='" + Balance[i].Tb_Acct_Code + "' And GocId=" + GocId + " And FiscalYearId=" + FiscalYearId + " And convert(date, Tb_Date)='" + DateTime.UtcNow.Date + "'";
                        dm.UpdateWithoutMessage(Query, con);
                    }
                }
            }
        }


        //for (int i = 0; i < count; i++)
        //{
        //    var Acct_Code = ds.Tables[0].Rows[i]["Acct_Code"].ToString();
        //    var MainAccountCode = Acct_Code.Substring(0, 1);
        //    if (MainAccountCode.Equals("1") || MainAccountCode.Equals("5") || MainAccountCode.Equals("6"))
        //    {// for debit accounts
        //        if (TotalValue[i] < 0)
        //        {
        //            if (CompId > 0)
        //            {
        //                if (BranchId > 0)
        //                {
        //                    Query = "Update Set_TrialBalance Set Tb_DrAmount=" + 0 + ",Tb_CrAmount = " + TotalValue[i] + " Where Tb_Acct_Code='" + Acct_Code + "' And GocId=" + GocId + " And CompId=" + CompId + " And BranchId=" + BranchId + " And FiscalYearId=" + FiscalYearId + " And convert(date, Tb_Date)='" + DateTime.UtcNow.Date + "'";
        //                    dm.UpdateWithoutMessage(Query, con);

        //                }
        //                else
        //                {
        //                    Query = "Update Set_TrialBalance Set Tb_DrAmount=" + 0 + ",Tb_CrAmount=" + TotalValue[i] + " Where Tb_Acct_Code='" + Acct_Code + "' And GocId=" + GocId + " And CompId=" + CompId + " And FiscalYearId=" + FiscalYearId + ", convert(date, Tb_Date)='" + DateTime.UtcNow.Date + "'";
        //                    dm.UpdateWithoutMessage(Query, con);
        //                }
        //            }
        //            else
        //            {
        //                Query = "Update Set_TrialBalance Set Tb_DrAmount=" + 0 + ", Tb_CrAmount=" + TotalValue[i] + " Where Tb_Acct_Code='" + Acct_Code + "' And GocId=" + GocId + " And FiscalYearId=" + FiscalYearId + " And convert(date, Tb_Date)='" + DateTime.UtcNow.Date + "'";
        //                dm.UpdateWithoutMessage(Query, con);

        //            }

        //        }
        //        else
        //        {

        //            if (CompId > 0)
        //            {
        //                if (BranchId > 0)
        //                {
        //                    Query = "Update Set_TrialBalance Set Tb_DrAmount=" + TotalValue[i] + ", Tb_CrAmount=" + 0 + " Where Tb_Acct_Code='" + Acct_Code + "' And GocId=" + GocId + " And CompId=" + CompId + " And BranchId=" + BranchId + " And FiscalYearId=" + FiscalYearId + " And convert(date, Tb_Date)='" + DateTime.UtcNow.Date + "'";
        //                    dm.UpdateWithoutMessage(Query, con);
        //                }
        //                else
        //                {
        //                    Query = "Update Set_TrialBalance Set Tb_DrAmount=" + TotalValue[i] + ", Tb_CrAmount=" + 0 + " Where Tb_Acct_Code='" + Acct_Code + "' And GocId=" + GocId + " And CompId=" + CompId + " And FiscalYearId=" + FiscalYearId + " And convert(date, Tb_Date)='" + DateTime.UtcNow.Date + "'";
        //                    dm.UpdateWithoutMessage(Query, con);
        //                }
        //            }
        //            else
        //            {
        //                Query = "Update Set_TrialBalance Set Tb_DrAmount=" + TotalValue[i] + ",Tb_CrAmount=" + 0 + " Where Tb_Acct_Code='" + Acct_Code + "' And GocId=" + GocId + " And FiscalYearId=" + FiscalYearId + " And convert(date, Tb_Date)='" + DateTime.UtcNow.Date + "'";
        //                dm.UpdateWithoutMessage(Query, con);
        //            }
        //        }

        //    }//for credit accounts
        //    else
        //    {
        //        if (TotalValue[i] < 0)
        //        {
        //            if (CompId > 0)
        //            {
        //                if (BranchId > 0)
        //                {
        //                    Query = "Update Set_TrialBalance Set Tb_DrAmount=" + TotalValue[i] + " ,Tb_CrAmount=" + 0 + " Where Tb_Acct_Code='" + Acct_Code + "' And GocId=" + GocId + " And CompId=" + CompId + " And BranchId=" + BranchId + " And FiscalYearId=" + FiscalYearId + " And convert(date, Tb_Date)=" + DateTime.UtcNow.Date + "'";
        //                    dm.UpdateWithoutMessage(Query, con);
        //                }
        //                else
        //                {
        //                    Query = "Update Set_TrialBalance Set Tb_DrAmount=" + TotalValue[i] + " ,Tb_CrAmount=" + 0 + " Where Tb_Acct_Code='" + Acct_Code + "' And GocId=" + GocId + " And CompId=" + CompId + " And FiscalYearId=" + FiscalYearId + " And convert(date, Tb_Date)='" + DateTime.UtcNow.Date + "'";
        //                    dm.UpdateWithoutMessage(Query, con);
        //                }
        //            }
        //            else
        //            {
        //                Query = "Update Set_TrialBalance Set Tb_DrAmount=" + TotalValue[i] + ",Tb_CrAmount=" + 0 + " Where Tb_Acct_Code='" + Acct_Code + "' And GocId=" + GocId + " And FiscalYearId=" + FiscalYearId + " And convert(date, Tb_Date)='" + DateTime.UtcNow.Date + "'";
        //                dm.UpdateWithoutMessage(Query, con);
        //            }
        //        }
        //        else
        //        {
        //            if (CompId > 0)
        //            {
        //                if (BranchId > 0)
        //                {
        //                    Query = "Update Set_TrialBalance Set Tb_DrAmount=" + 0 + ",Tb_CrAmount=" + TotalValue[i] + " Where Tb_Acct_Code='" + Acct_Code + "' And GocId=" + GocId + " And CompId=" + CompId + " And BranchId=" + BranchId + " And FiscalYearId=" + FiscalYearId + " And convert(date, Tb_Date)='" + DateTime.UtcNow.Date + "'";
        //                    dm.UpdateWithoutMessage(Query, con);

        //                }
        //                else
        //                {
        //                    Query = "Update Set_TrialBalance Set Tb_DrAmount=" + 0 + ",Tb_CrAmount=" + TotalValue[i] + " Where Tb_Acct_Code='" + Acct_Code + "' And GocId=" + GocId + " And CompId=" + CompId + " And FiscalYearId=" + FiscalYearId + " And convert(date, Tb_Date)='" + DateTime.UtcNow.Date + "'";
        //                    dm.UpdateWithoutMessage(Query, con);
        //                }
        //            }
        //            else
        //            {
        //                Query = "Update Set_TrialBalance Set Tb_DrAmount=" + 0 + ",Tb_CrAmount=" + TotalValue[i] + " Where Tb_Acct_Code='" + Acct_Code + "' And GocId=" + GocId + " And FiscalYearId=" + FiscalYearId + " And convert(date, Tb_Date)='" + DateTime.UtcNow.Date + "'";
        //                dm.UpdateWithoutMessage(Query, con);
        //            }
        //        }
        //    }
        //}

    }


    protected void GetSumCrDr(DataSet ds, string TranName, int count, List<Double> Values, DataSet Temp)
    {
        ddlComp = dm.ReturnSelectedDdlValueInInt(ddlCompany);
        if (ddlComp != 0)
        {

            ddlBr = dm.ReturnSelectedDdlValueInInt(ddlBranch);
        }
        else
        {
            ddlBr = 0;
        }
        ddlFiscal = dm.ReturnSelectedDdlValueInInt(ddlFiscalYear);

        string SqlQuery;
        for (int i = 0; i < count; i++)
        {
            var code = ds.Tables[0].Rows[i]["Acct_Code"].ToString();
            if (!string.IsNullOrEmpty(TranName))
            {

                if (ddlComp > 0)
                {
                    if (ddlBr > 0)
                    {
                        if (ddlFiscal > 0)
                        {
                            SqlQuery = "Select Sum(DrAmount) as DebitAmount,Sum(CrAmount) as CreditAmount From Act_Gl_Detail Where AccountCode like '" + code + "%' And GocId=" + GocId + " And CompId=" + ddlComp + " And BranchId=" + ddlBr + " And FiscalYearId =" + ddlFiscal + " And Record_Deleted='0'";
                        }
                        else
                        {
                            SqlQuery = "Select Sum(DrAmount) as DebitAmount,Sum(CrAmount) as CreditAmount From Act_Gl_Detail Where AccountCode like '" + code + "%' And GocId=" + GocId + " And CompId=" + Convert.ToInt32(ViewState["CompId"]) + " And BranchId=" + Convert.ToInt32(ViewState["BranchId"]) + " And FiscalYearId =" + FiscalYearId + " And Record_Deleted='0'";
                        }
                    }
                    else
                    {
                        if (ddlFiscal > 0)
                        {
                            SqlQuery = "Select Sum(DrAmount) as DebitAmount,Sum(CrAmount) as CreditAmount From Act_Gl_Detail Where AccountCode like '" + code + "%' And GocId=" + GocId + " And CompId=" + Convert.ToInt32(ViewState["CompId"]) + "  And FiscalYearId =" + Convert.ToInt32(ViewState["FicalYearId"]) + " And Record_Deleted='0'";
                        }
                        else
                        {
                            SqlQuery = "Select Sum(DrAmount) as DebitAmount,Sum(CrAmount) as CreditAmount From Act_Gl_Detail Where AccountCode like '" + code + "%' And GocId=" + GocId + " And CompId=" + Convert.ToInt32(ViewState["CompId"]) + " And FiscalYearId =" + FiscalYearId + " And Record_Deleted='0'";
                        }
                    }
                }
                else
                {

                    if (ddlFiscal > 0)
                    {
                        SqlQuery = "Select Sum(DrAmount) as DebitAmount,Sum(CrAmount) as CreditAmount From Act_Gl_Detail Where AccountCode like '" + code + "%' And GocId=" + GocId + " And FiscalYearId =" + Convert.ToInt32(ViewState["FicalYearId"]) + " And Record_Deleted='0'";
                    }
                    else
                    {
                        SqlQuery = "Select Sum(DrAmount) as DebitAmount,Sum(CrAmount) as CreditAmount From Act_Gl_Detail Where AccountCode like '" + code + "%' And GocId=" + GocId + " And FiscalYearId =" + FiscalYearId + " And Record_Deleted='0'";

                    }

                }

                Temp = dm.Find(SqlQuery);
                var CrDrSum = Temp.Tables[0].Rows[0][TranName].ToString();
                if (string.IsNullOrEmpty(CrDrSum))
                {
                    Values.Add(0);
                }
                else
                {
                    Values.Add(Convert.ToDouble(CrDrSum));
                }
            }
        }
    }
    protected void GetSumOpening(DataSet ds, string query, int count, List<Double> Values, DataSet Temp)
    {
        //ddlComp = Convert.ToInt32(ViewState["ddlComp"].ToString());
        //ddlBr= Convert.ToInt32(ViewState["ddlBr"].ToString());
        //ddlFiscal= Convert.ToInt32(ViewState["ddlFiscal"].ToString());
        ddlComp = dm.ReturnSelectedDdlValueInInt(ddlCompany);
        if (ddlComp != 0)
        {

            ddlBr = dm.ReturnSelectedDdlValueInInt(ddlBranch);
        }
        else
        {
            ddlBr = 0;
        }
        ddlFiscal = dm.ReturnSelectedDdlValueInInt(ddlFiscalYear);

        string SqlQuery;
        for (int i = 0; i < count; i++)
        {
            var code = ds.Tables[0].Rows[i]["Acct_Code"].ToString();


            if (ddlComp > 0)
            {
                if (ddlBr > 0)
                {
                    if (ddlFiscal > 0)
                    {
                        SqlQuery = "select OpeningValue,Tran_Type From SET_COA_Detail_Opening where GocID=" + GocId + " And CompId=" + ddlComp + " And BranchId=" + ddlBr + " And FiscalYearID=" + ddlFiscal + " And Acct_Code like '" + code + "%'";
                    }
                    else
                    {
                        SqlQuery = "select OpeningValue,Tran_Type From SET_COA_Detail_Opening where GocID=" + GocId + " And CompId=" + ddlComp + " And BranchId=" + ddlBr + " And FiscalYearID=" + FiscalYearId + " And Acct_Code like '" + code + "%'";
                    }
                }
                else
                {
                    if (ddlFiscal > 0)
                    {
                        SqlQuery = "select OpeningValue,Tran_Type From SET_COA_Detail_Opening where GocID=" + GocId + " And CompId=" + ddlComp + " And FiscalYearID=" + ddlFiscal + " And Acct_Code like '" + code + "%'";

                    }
                    else
                    {
                        SqlQuery = "select OpeningValue,Tran_Type From SET_COA_Detail_Opening where GocID=" + GocId + " And CompId=" + ddlComp + " And FiscalYearID=" + FiscalYearId + " And Acct_Code like '" + code + "%'";
                    }
                }
            }
            else
            {
                if (ddlFiscal > 0)
                {
                    SqlQuery = "select OpeningValue,Tran_Type From SET_COA_Detail_Opening where GocID=" + GocId + "  And FiscalYearID=" + ddlFiscal + " And Acct_Code like '" + code + "%'";

                }
                else
                {
                    SqlQuery = "select OpeningValue,Tran_Type From SET_COA_Detail_Opening where GocID=" + GocId + " And FiscalYearID=" + FiscalYearId + " And Acct_Code like '" + code + "%'";
                }
            }
            Values.Add(0);
            Temp = dm.Find(SqlQuery);
            var TempCount = Temp.Tables[0].Rows.Count;
            for (int j = 0; j < TempCount; j++)
            {
                var amount = Temp.Tables[0].Rows[j]["OpeningValue"].ToString();
                if (string.IsNullOrEmpty(amount))
                {
                    Values[i] += 0;
                }
                else
                {
                    if (string.Equals("Credit", Temp.Tables[0].Rows[j]["Tran_Type"].ToString()))
                    {
                        Values[i] -= Convert.ToDouble(amount);
                    }
                    else
                    {
                        Values[i] += Convert.ToDouble(amount);
                    }
                }

            }
            //Temp = dm.Find(GetOpeningValueQuery(GocId, CompId, BranchId, FiscalYearId, code));
            //var sum = Temp.Tables[0].Rows[0]["OpeningValues"].ToString();
            //if (string.IsNullOrEmpty(sum))
            //{
            //    Values[i] = 0;
            //}
            //else
            //{
            //    Values[i] = Convert.ToDouble(sum);
            //}
        }

    }
    protected void GetSumCrDrOpening(DataSet ds, string query, int count, List<Double> Values, DataSet Temp)
    {
        //ddlComp = Convert.ToInt32(ViewState["ddlComp"].ToString());
        //ddlBr= Convert.ToInt32(ViewState["ddlBr"].ToString());
        //ddlFiscal= Convert.ToInt32(ViewState["ddlFiscal"].ToString());

        ddlComp = dm.ReturnSelectedDdlValueInInt(ddlCompany);

        if (ddlComp != 0)
        {
           
            ddlBr = dm.ReturnSelectedDdlValueInInt(ddlBranch);
        }
        else
        {
            ddlBr = 0;
        }

            ddlFiscal = dm.ReturnSelectedDdlValueInInt(ddlFiscalYear);
        string SqlQuery;
        for (int i = 0; i < count; i++)
        {
            var code = ds.Tables[0].Rows[i]["Acct_Code"].ToString();
            if (!string.IsNullOrEmpty(query))
            {

                if (ddlComp > 0)
                {
                    if (ddlBr > 0)
                    {
                        if (ddlFiscal > 0)
                        {
                            SqlQuery = "Select Sum(DrAmount) as DebitAmount,Sum(CrAmount) as CreditAmount From Act_Gl_Detail Where AccountCode like '" + code + "%' And GocId=" + GocId + " And CompId=" + ddlComp + " And BranchId=" + ddlBr + " And FiscalYearId =" + ddlFiscal + " And Record_Deleted='0'";
                        }
                        else
                        {
                            SqlQuery = "Select Sum(DrAmount) as DebitAmount,Sum(CrAmount) as CreditAmount From Act_Gl_Detail Where AccountCode like '" + code + "%' And GocId=" + GocId + " And CompId=" + Convert.ToInt32(ViewState["CompId"]) + " And BranchId=" + Convert.ToInt32(ViewState["BranchId"]) + " And FiscalYearId =" + FiscalYearId + " And Record_Deleted='0'";
                        }
                    }
                    else
                    {
                        if (ddlFiscal > 0)
                        {
                            SqlQuery = "Select Sum(DrAmount) as DebitAmount,Sum(CrAmount) as CreditAmount From Act_Gl_Detail Where AccountCode like '" + code + "%' And GocId=" + GocId + " And CompId=" + Convert.ToInt32(ViewState["CompId"]) + "  And FiscalYearId =" + Convert.ToInt32(ViewState["FicalYearId"]) + " And Record_Deleted='0'";
                        }
                        else
                        {
                            SqlQuery = "Select Sum(DrAmount) as DebitAmount,Sum(CrAmount) as CreditAmount From Act_Gl_Detail Where AccountCode like '" + code + "%' And GocId=" + GocId + " And CompId=" + Convert.ToInt32(ViewState["CompId"]) + " And FiscalYearId =" + FiscalYearId + " And Record_Deleted='0'";
                        }
                    }
                }
                else
                {

                    if (ddlFiscal > 0)
                    {
                        SqlQuery = "Select Sum(DrAmount) as DebitAmount,Sum(CrAmount) as CreditAmount From Act_Gl_Detail Where AccountCode like '" + code + "%' And GocId=" + GocId + " And FiscalYearId =" + Convert.ToInt32(ViewState["FicalYearId"]) + " And Record_Deleted='0'";
                    }
                    else
                    {
                        SqlQuery = "Select Sum(DrAmount) as DebitAmount,Sum(CrAmount) as CreditAmount From Act_Gl_Detail Where AccountCode like '" + code + "%' And GocId=" + GocId + " And FiscalYearId =" + FiscalYearId + " And Record_Deleted='0'";

                    }

                }

                Temp = dm.Find(SqlQuery);
                var CrDrSum = Temp.Tables[0].Rows[0][query].ToString();
                if (string.IsNullOrEmpty(CrDrSum))
                {
                    Values.Add(0);
                }
                else
                {
                    Values.Add(Convert.ToDouble(CrDrSum));
                }
            }
            else
            {
                Values.Add(0);
                if (ddlComp > 0)
                {
                    if (ddlBr > 0)
                    {
                        if (ddlFiscal > 0)
                        {
                            SqlQuery = "select OpeningValue,Tran_Type From SET_COA_Detail_Opening where GocID=" + GocId + " And CompId=" + ddlComp + " And BranchId=" + ddlBr + " And FiscalYearID=" + ddlFiscal + " And Acct_Code like '" + code + "%'";
                        }
                        else
                        {
                            SqlQuery = "select OpeningValue,Tran_Type From SET_COA_Detail_Opening where GocID=" + GocId + " And CompId=" + ddlComp + " And BranchId=" + ddlBr + " And FiscalYearID=" + FiscalYearId + " And Acct_Code like '" + code + "%'";
                        }
                    }
                    else
                    {
                        if (ddlFiscal > 0)
                        {
                            SqlQuery = "select OpeningValue,Tran_Type From SET_COA_Detail_Opening where GocID=" + GocId + " And CompId=" + ddlComp + " And FiscalYearID=" + ddlFiscal + " And Acct_Code like '" + code + "%'";

                        }
                        else
                        {
                            SqlQuery = "select OpeningValue,Tran_Type From SET_COA_Detail_Opening where GocID=" + GocId + " And CompId=" + ddlComp + " And FiscalYearID=" + FiscalYearId + " And Acct_Code like '" + code + "%'";
                        }
                    }
                }
                else
                {
                    if (ddlFiscal > 0)
                    {
                        SqlQuery = "select OpeningValue,Tran_Type From SET_COA_Detail_Opening where GocID=" + GocId + "  And FiscalYearID=" + ddlFiscal + " And Acct_Code like '" + code + "%'";

                    }
                    else
                    {
                        SqlQuery = "select OpeningValue,Tran_Type From SET_COA_Detail_Opening where GocID=" + GocId + " And FiscalYearID=" + FiscalYearId + " And Acct_Code like '" + code + "%'";
                    }
                }





                Temp = dm.Find(SqlQuery);
                var TempCount = Temp.Tables[0].Rows.Count;
                for (int j = 0; j < TempCount; j++)
                {
                    var amount = Temp.Tables[0].Rows[j]["OpeningValue"].ToString();
                    if (string.IsNullOrEmpty(amount))
                    {
                        Values[i] += 0;
                    }
                    else
                    {
                        if (string.Equals("Credit", Temp.Tables[0].Rows[j]["Tran_Type"].ToString()))
                        {
                            Values[i] -= Convert.ToDouble(amount);
                        }
                        else
                        {
                            Values[i] += Convert.ToDouble(amount);
                        }
                    }

                }
                //Temp = dm.Find(GetOpeningValueQuery(GocId, CompId, BranchId, FiscalYearId, code));
                //var sum = Temp.Tables[0].Rows[0]["OpeningValues"].ToString();
                //if (string.IsNullOrEmpty(sum))
                //{
                //    Values[i] = 0;
                //}
                //else
                //{
                //    Values[i] = Convert.ToDouble(sum);
                //}
            }
        }

    }

    public int GetFromCookie(string name)
    {
        return Convert.ToInt32(Request.Cookies[name].Value);
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        //string query = "SELECT COA_D_ID,COAM.Narration AS COA_MASTER,Acct_Code,Acct_Description,Head_detail_ID,ACCT.Acct_Type_Name,SUB.Acct_Sub_Type_Name,G.GOCName,C.CompName,COA.IsActive,Bar_Code,COA.Record_Deleted FROM SET_COA_DETAIL COA LEFT JOIN SET_Acct_Type ACCT ON COA.Acct_Type_ID = ACCT.Acct_Type_Id LEFT JOIN SET_COA_Master COAM ON COA.COA_M_ID = COAM.COA_M_ID LEFT JOIN SET_Acct_Sub_Type SUB ON COA.Acct_Sub_Type_Id = SUB.Acct_Sub_Type_Id AND COA.Acct_Type_ID = SUB.Acct_Type_Id LEFT JOIN SET_GOC G ON COA.GocId = G.GocId LEFT JOIN SET_Company C ON COA.CompId = C.CompId WHERE COA.COA_D_ID = 1";

        SqlDataAdapter Adapter = new SqlDataAdapter();
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
            ddlComp = 0;
        }
        dm.DropDownSqlWithCondition(ddlBranch, "Set_Branch", "BranchName", "BranchId", "CompId", ddlComp.ToString());
        ddlFiscalYear.Items.Clear();
        GetFiscalYear(UserId, GocId, ddlComp, 0);
        ViewState["CompId"] = ddlComp.ToString();
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
        if (ddlFiscalYear.SelectedItem.Value != "Please Select...")
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


    protected void btnBack_Click(object sender, EventArgs e)
    {
        ResetAll();
    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        if (ddlHeadingLevel.SelectedItem.Value != "Please select...")
        {
            btnGenerate.Visible = false;
            btnBack.Visible = true;
            GenerateGeneralTrialBalance();
            Error_Message.Visible = false;
            Error_Label.Visible = false;
            Error_Label.Text = "";
        }
        else {
            Error_Message.Visible = true;
            Error_Label.Visible = true;
            Error_Label.Text = "Heading Level is required";
        }
    }

    protected void ResetAll()
    {
        Error_Message.Visible = false;
        Error_Label.Visible = false;
        Table_Div.Controls.Clear();
        btnGenerate.Visible = true;
        btnBack.Visible = false;
        ddlCompany.SelectedValue = "Please select...";
        ddlComp = 0;
        ddlBranch.SelectedValue = "Please select...";
        ddlBr = 0;
        ddlFiscalYear.SelectedValue = "Please select...";
        ddlFiscal = 0;
        ddlHeadingLevel.SelectedValue = "Please select...";
        ddlBranch.Enabled = false;
        modal_DataDiv.Visible = true;
        SaveInDataBase.Visible = false;
    }

}


