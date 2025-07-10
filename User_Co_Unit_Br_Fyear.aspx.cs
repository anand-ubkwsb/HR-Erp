using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Configuration;
using System.Web.Configuration;
using System.Web.UI.WebControls;

public partial class User_Co_Unit_Br_Fyear : System.Web.UI.Page
{
    int GId, CId, BId;
    bool fiscalChecker = true;
    DataSet TemporaryDataSet;
    DmlOperation dml = new DmlOperation();
    public string fiscaldes = "";
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString);
    string userid = "", UgrpID = "";
    //string ad_cmp, ad_Bra, ad_goc;
    string GocID, CompId, CompAll, BranchId, BranchAll, FiscalYearsID, FisacalYearAll;
    string namecomp;
    //string branchesflag = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        TreeView1.ExpandDepth = 0;
        userid = Request.QueryString.Get("UserID");
        UgrpID = Request.QueryString.Get("UsergrpID");

        Response.Cookies["userid"].Value = userid;
        Response.Cookies["userid"].Expires = DateTime.Now.AddDays(1);
        Response.Cookies["UsergrpID"].Value = UgrpID;
        Response.Cookies["UsergrpID"].Expires = DateTime.Now.AddDays(1);
        con.Open();
        string query = "select * from SET_User_Permission_CompBrYear where userid = convert(uniqueidentifier,'" + userid.ToString() + "') and Record_Deleted = '0' and active='1'";
        SqlDataAdapter da = new SqlDataAdapter(query, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GocID = ds.Tables[0].Rows[0]["Gocid"].ToString();
            CompId = ds.Tables[0].Rows[0]["CompId"].ToString();
            CompAll = ds.Tables[0].Rows[0]["CompAll"].ToString();
            BranchId = ds.Tables[0].Rows[0]["BranchId"].ToString();
            BranchAll = ds.Tables[0].Rows[0]["BranchAll"].ToString();
            FiscalYearsID = ds.Tables[0].Rows[0]["FiscalYearsID"].ToString();
            FisacalYearAll = ds.Tables[0].Rows[0]["FiscalYearAll"].ToString();
            con.Close();
            if (!this.IsPostBack)
            {
                //Group of company name display
                GOC(userid);
                SetCompany(userid);
                //Company(userid, CompAll);
            }
        }
        else
        {
        }
        //if (Object.ReferenceEquals(CheckDataSet, TemporaryDataSet) && Object.Equals(CheckDataSet, TemporaryDataSet))
        if (IsPostBack)
        {
            TreeView1_SelectedNodeChanged(sender, e);
            fiscalChecker = false;
        }
    }


    public void SetCompany(string UserId)
    {

        con.Open();
        string Query = "SELECT GocId FROM SET_USER_PERMISSION_COMPBRYEAR WHERE USERID = '" + UserId.ToString() + "' AND RECORD_DELETED='0' AND ACTIVE='1' Group by GocId ";
        SqlDataAdapter da = new SqlDataAdapter(Query, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        int row = ds.Tables[0].Rows.Count;


        for (int i = 0; i < row; i++)
        {
            var test = ds.Tables[0].Rows[i]["GociD"].ToString();

            SqlDataAdapter sda = new SqlDataAdapter("select * from SET_GOC where GocID = " + Convert.ToInt32(test) + "  and Record_Deleted = 0", con);
            DataSet ads = new DataSet();
            sda.Fill(ads);

            DataTable dt = this.GetData("select GocID,GocName from SET_GOC where GocID = " + Convert.ToInt32(test) + " and Record_Deleted = 0");

            InsertGocInTree(dt);


            con.Close();
        }

    }

    public static bool AreTablesTheSame(DataTable tbl1, DataTable tbl2)
    {
        if (tbl1.Rows.Count != tbl2.Rows.Count || tbl1.Columns.Count != tbl2.Columns.Count)
            return false;


        for (int i = 0; i < tbl1.Rows.Count; i++)
        {
            for (int c = 0; c < tbl1.Columns.Count; c++)
            {
                if (!Equals(tbl1.Rows[i][c], tbl2.Rows[i][c]))
                    return false;
            }
        }
        return true;
    }


    public void GOC(string gocid)
    {
        con.Open();
        string query = "select (select GOCName from SET_GOC where GocId = " + GocID + ")as Goc_name from  SET_User_Permission_CompBrYear where userid = convert(uniqueidentifier,'" + gocid.ToString() + "') and Record_Deleted = '0' and active='1'";
        SqlDataAdapter da = new SqlDataAdapter(query, con);
        DataSet ds = new DataSet();
        da.Fill(ds);

        lblGoCompany.Text = ds.Tables[0].Rows[0]["Goc_name"].ToString();
        con.Close();
    }

    protected void InsertGocInTree(DataTable dt)
    {
        int index = 0;
        var Gocdata = Convert.ToInt32(dt.Rows[0]["GocID"].ToString());
        var Gocquery = "Select * From Set_Goc Where GocId=" + Gocdata + "";
        SqlDataAdapter GocData = new SqlDataAdapter(Gocquery, con);
        DataSet GocDataSet = new DataSet();
        GocData.Fill(GocDataSet);

        TreeNode Goc = new TreeNode
        {
            Text = GocDataSet.Tables[0].Rows[0]["GocName"].ToString(),
            Value = GocDataSet.Tables[0].Rows[0]["GocId"].ToString()
        };
        Goc.SelectAction = TreeNodeSelectAction.Expand;
        TreeView1.Nodes.Add(Goc);
        InsertCompanyInTree(Goc);
        index = -1;
        foreach (TreeNode node in Goc.ChildNodes)
        {
            if (node.ChildNodes.Count < 1)
            {
                index = node.Parent.ChildNodes.IndexOf(node);
            }
        }
        if (!(index <= -1))
        {
            Goc.ChildNodes.RemoveAt(index);
        }
    }


    protected void InsertCompanyInTree(TreeNode Goc)
    {
        var GocId = Convert.ToInt32(Goc.Value);

        var CompAllY = "Select CompAll FROM SET_USER_PERMISSION_COMPBRYEAR WHERE USERID='" + userid.ToString() + "' AND RECORD_DELETED='0' AND ACTIVE='1' AND GOCID=" + GocId + " AND COMPALL='Y'";
        SqlDataAdapter CompAllYDataAdapter = new SqlDataAdapter(CompAllY, con);
        DataSet CompAllYDataSet = new DataSet();
        CompAllYDataAdapter.Fill(CompAllYDataSet);
        var CompY = CompAllYDataSet.Tables[0].Rows.Count;
        if (CompY > 0)
        {
            var Companies = "SELECT * FROM Set_Company WHERE RECORD_DELETED='0' AND GOCID=" + GocId + "";
            SqlDataAdapter CompaniesDataAdapter = new SqlDataAdapter(Companies, con);
            DataSet CompanyDataSet = new DataSet();
            CompaniesDataAdapter.Fill(CompanyDataSet);
            var CompanyCounts = CompanyDataSet.Tables[0].Rows.Count;

            for (int i = 0; i < CompanyCounts; i++)
            {


                TreeNode Company = new TreeNode
                {
                    Text = CompanyDataSet.Tables[0].Rows[i]["CompName"].ToString(),
                    Value = CompanyDataSet.Tables[0].Rows[i]["CompId"].ToString()
                };
                Company.SelectAction = TreeNodeSelectAction.Expand;
                Goc.ChildNodes.Add(Company);

                InsertBranchIntoTree(Company, Goc);


            }


        }
        else
        {
            var Companies = "SELECT CompId FROM SET_USER_PERMISSION_COMPBRYEAR WHERE USERID='" + userid.ToString() + "' AND RECORD_DELETED='0' AND ACTIVE = '1' AND GOCID=" + GocId + " Group by CompId";
            SqlDataAdapter CompaniesDataAdapter = new SqlDataAdapter(Companies, con);
            DataSet CompanyDataSet = new DataSet();
            CompaniesDataAdapter.Fill(CompanyDataSet);
            var CompanyCounts = CompanyDataSet.Tables[0].Rows.Count;

            for (int i = 0; i < CompanyCounts; i++)
            {
                var CompanyId = Convert.ToInt32(CompanyDataSet.Tables[0].Rows[i]["CompId"].ToString());
                var Companyquery = "Select * From Set_Company Where CompId=" + CompanyId + "";
                SqlDataAdapter CompanyDataAd = new SqlDataAdapter(Companyquery, con);
                DataSet CompanyDs = new DataSet();
                CompanyDataAd.Fill(CompanyDs);


                TreeNode Company = new TreeNode
                {
                    Text = CompanyDs.Tables[0].Rows[0]["CompName"].ToString(),
                    Value = CompanyDs.Tables[0].Rows[0]["CompId"].ToString()
                };
                Company.SelectAction = TreeNodeSelectAction.Expand;
                Goc.ChildNodes.Add(Company);

                InsertBranchIntoTree(Company, Goc);
            }

        }
    }

    protected void InsertBranchIntoTree(TreeNode Company, TreeNode Goc)
    {
        var GocId = Convert.ToInt32(Goc.Value);
        var CompId = Convert.ToInt32(Company.Value);

        var BranchAllY = "SELECT BRANCHALL FROM SET_USER_PERMISSION_COMPBRYEAR WHERE USERID='" + userid.ToString() + "' AND RECORD_DELETED='0' AND ACTIVE='1' AND GOCID=" + GocId + " AND COMPID=" + CompId + " AND BRANCHALL='Y'";
        SqlDataAdapter BranchAllYDataAdapter = new SqlDataAdapter(BranchAllY, con);
        DataSet BranchAllYDataSet = new DataSet();
        BranchAllYDataAdapter.Fill(BranchAllYDataSet);
        var BranchY = BranchAllYDataSet.Tables[0].Rows.Count;
        if (BranchY > 0)
        {
            var Branches = "SELECT * FROM Set_Branch WHERE RECORD_DELETED='0' AND CompId=" + CompId + "";
            SqlDataAdapter BranchesDataAdapter = new SqlDataAdapter(Branches, con);
            DataSet BranchDataSet = new DataSet();
            BranchesDataAdapter.Fill(BranchDataSet);
            var BranchCounts = BranchDataSet.Tables[0].Rows.Count;

            for (int i = 0; i < BranchCounts; i++)
            {


                TreeNode Branch = new TreeNode
                {
                    Text = BranchDataSet.Tables[0].Rows[i]["BranchName"].ToString(),
                    Value = BranchDataSet.Tables[0].Rows[i]["BranchId"].ToString()
                };
                Company.SelectAction = TreeNodeSelectAction.Expand;
                Company.ChildNodes.Add(Branch);

            }


        }
        else
        {
            var Branches = "SELECT BranchId FROM SET_USER_PERMISSION_COMPBRYEAR WHERE USERID='" + userid.ToString() + "' AND RECORD_DELETED='0' AND ACTIVE = '1' AND GOCID=" + GocId + " And CompId=" + CompId + " Group by BranchId";
            SqlDataAdapter BranchesDataAdapter = new SqlDataAdapter(Branches, con);
            DataSet BranchDataSet = new DataSet();
            BranchesDataAdapter.Fill(BranchDataSet);
            var BranchCounts = BranchDataSet.Tables[0].Rows.Count;

            for (int i = 0; i < BranchCounts; i++)
            {
                var BranchId = Convert.ToInt32(BranchDataSet.Tables[0].Rows[i]["BranchId"].ToString());
                var Branchquery = "Select * From Set_Branch Where BranchId=" + BranchId + "";
                SqlDataAdapter BranchDataAd = new SqlDataAdapter(Branchquery, con);
                DataSet BranchDs = new DataSet();
                BranchDataAd.Fill(BranchDs);


                TreeNode Branch = new TreeNode
                {
                    Text = BranchDs.Tables[0].Rows[0]["BranchName"].ToString(),
                    Value = BranchDs.Tables[0].Rows[0]["BranchId"].ToString()
                };
                Company.ChildNodes.Add(Branch);


            }
        }

    }




    protected bool CheckDate(DateTime DateOfCorporation, DateTime StartDate, DateTime EndDate)
    {


        if (StartDate <= DateOfCorporation && EndDate >= DateOfCorporation)
            return true;

        return false;
    }

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        int help = 0;
        if (fiscalChecker)
        {

            var PrintTable = true;
            var GroupOfCompanyId = Convert.ToInt32(TreeView1.SelectedNode.Parent.Parent.Value.ToString());
            var CompId = Convert.ToInt32(TreeView1.SelectedNode.Parent.Value.ToString());
            var BranchId = Convert.ToInt32(TreeView1.SelectedNode.Value.ToString());
            var FiscalYearId = "";

            namecomp = TreeView1.SelectedNode.Parent.Text;
            if (TreeView1.SelectedNode.Selected == true)
            {

                Div1.Visible = true;

            }
            else
            {
                Div1.Visible = false;
            }



            con.Open();
            try
            {

                Response.Cookies["fahad"].Value = "fahad siddiqui";
                Response.Cookies["fahad"].Expires = DateTime.Now.AddHours(1);
                // Session["compNAme"] = TreeView1.SelectedNode.Parent.Text.ToString();

                Response.Cookies["GocId"].Value = TreeView1.SelectedNode.Parent.Parent.Value.ToString();
                Response.Cookies["GocId"].Expires = DateTime.Now.AddDays(1);
                Response.Cookies["GocName"].Value = TreeView1.SelectedNode.Parent.Parent.Text.ToString();
                Response.Cookies["GocName"].Expires = DateTime.Now.AddDays(1);

                Response.Cookies["compNAme"].Value = TreeView1.SelectedNode.Parent.Text.ToString();
                Response.Cookies["compNAme"].Expires = DateTime.Now.AddDays(1);
                Response.Cookies["CompId"].Value = TreeView1.SelectedNode.Parent.Value.ToString();
                Response.Cookies["CompId"].Expires = DateTime.Now.AddDays(1);

                Response.Cookies["branchNAme"].Value = TreeView1.SelectedNode.Text;
                Response.Cookies["branchNAme"].Expires = DateTime.Now.AddDays(1);
                Response.Cookies["BranchId"].Value = TreeView1.SelectedNode.Value.ToString();
                Response.Cookies["BranchId"].Expires = DateTime.Now.AddDays(1);
                // Session["branchNAme"] = TreeView1.SelectedNode.Text;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            string FiscalQuery = "Select FiscalYearAll From Set_User_Permission_CompBrYear Where UserId='" + userid.ToString() + "' AND RECORD_DELETED='0' AND ACTIVE='1' AND GocId=" + GroupOfCompanyId + " AND FISCALYEARALL='Y' ";
            SqlDataAdapter FiscalAllYearDataAdaptar = new SqlDataAdapter(FiscalQuery, con);
            DataSet fiscalAllYearDataSet = new DataSet();
            FiscalAllYearDataAdaptar.Fill(fiscalAllYearDataSet);
            var FiscalAllYearCount = fiscalAllYearDataSet.Tables[0].Rows.Count;

            if (FiscalAllYearCount > 0)
            {
                string FiscalYearQueries = "SELECT SortOrder,FiscalYearID, Description, StartDate, EndDate,(Select Description from SET_Period where PeriodID = SET_Fiscal_Year.PeriodID ) as PeriodDescription from Set_Fiscal_Year where record_deleted = '0' And GOCid = " + GroupOfCompanyId + "";
                SqlDataAdapter FiscalYearQuriesDataAdaptar = new SqlDataAdapter(FiscalYearQueries, con);
                DataSet fiscalYearQueryDataSet = new DataSet();
                FiscalYearQuriesDataAdaptar.Fill(fiscalYearQueryDataSet);
                if (!(fiscalYearQueryDataSet.Tables[0].Rows.Count > 0))
                {
                    help++;
                    PrintNoData();
                    PrintTable = false;
                }
                else
                {
                    PrintTable = true;
                    FiscalYearId = fiscalYearQueryDataSet.Tables[0].Rows[0]["FiscalYearId"].ToString();
                    string Branch = "SELECT * FROM SET_BRANCH WHERE BranchId= " + BranchId + " AND RECORD_DELETED='0'";
                    SqlDataAdapter BranchDataAdapter = new SqlDataAdapter(Branch, con);
                    DataSet BranchDataSet = new DataSet();
                    BranchDataAdapter.Fill(BranchDataSet);
                    var BranchData = BranchDataSet.Tables[0].Rows.Count;
                    DateTime[] DateOfIncorporation = new DateTime[BranchData];
                    string FiscalYearForBranch = "SELECT FISCALYEARALL,FiscalYearsId FROM Set_User_Permission_CompBrYear WHERE USERID='" + userid.ToString() + "' AND RECORD_DELETED='0' AND ACTIVE='1' AND GOCID=" + GroupOfCompanyId + " AND COMPID=" + CompId + " AND BRANCHID=" + BranchId + "";
                    SqlDataAdapter FiscalYearForBranchDataAdapter = new SqlDataAdapter(FiscalYearForBranch, con);
                    DataSet FiscalYearForBranchDataSet = new DataSet();
                    FiscalYearForBranchDataAdapter.Fill(FiscalYearForBranchDataSet);


                    for (int i = 0; i < BranchData; i++)
                    {
                        DateOfIncorporation[i] = Convert.ToDateTime(BranchDataSet.Tables[0].Rows[i]["DateOfIncorporation"].ToString());
                    }

                    if (FiscalYearForBranchDataSet.Tables[0].Rows[0]["FiscalYearAll"].ToString() == "Y")
                    {
                        for (int i = 0; i < fiscalYearQueryDataSet.Tables[0].Rows.Count; i++)
                        {
                            for (int j = 0; j < BranchData; j++)
                            {
                                if ((Convert.ToDateTime(fiscalYearQueryDataSet.Tables[0].Rows[i]["StartDate"].ToString()).Year) <= DateOfIncorporation[j].Year)
                                {
                                    if (!CheckDate(DateOfIncorporation[j], Convert.ToDateTime(fiscalYearQueryDataSet.Tables[0].Rows[i]["StartDate"].ToString()), Convert.ToDateTime(fiscalYearQueryDataSet.Tables[0].Rows[i]["EndDate"].ToString())))
                                    {
                                        fiscalYearQueryDataSet.Tables[0].Rows[i].Delete();
                                    }
                                }
                            }
                        }
                        fiscalYearQueryDataSet.Tables[0].AcceptChanges();

                    }
                    else
                    {
                        FiscalQuery = "Select FiscalYearsId From Set_User_Permission_CompBrYear Where UserId='" + userid.ToString() + "' AND RECORD_DELETED='0' AND ACTIVE='1' AND GocId=" + GroupOfCompanyId + " AND CompId=" + CompId + " And BranchId=" + BranchId + " ";
                        FiscalAllYearDataAdaptar = new SqlDataAdapter(FiscalQuery, con);
                        fiscalAllYearDataSet = new DataSet();
                        FiscalAllYearDataAdaptar.Fill(fiscalAllYearDataSet);
                        FiscalAllYearCount = fiscalAllYearDataSet.Tables[0].Rows.Count;

                        FiscalYearQueries = "SELECT SortOrder,FiscalYearID, Description, StartDate, EndDate,(Select Description from SET_Period where PeriodID = SET_Fiscal_Year.PeriodID ) as PeriodDescription from Set_Fiscal_Year where record_deleted = '0' And FiscalYearId In(Select FiscalYearsID From Set_User_Permission_CompBrYear Where UserId='" + userid.ToString() + "' AND RECORD_DELETED='0' AND ACTIVE='1' AND GocId=" + GroupOfCompanyId + " AND COMPId=" + CompId + " AND BranchId=" + BranchId + ")";
                        FiscalYearQuriesDataAdaptar = new SqlDataAdapter(FiscalYearQueries, con);
                        fiscalYearQueryDataSet = new DataSet();
                        FiscalYearQuriesDataAdaptar.Fill(fiscalYearQueryDataSet);

                        for (int i = 0; i < fiscalYearQueryDataSet.Tables[0].Rows.Count; i++)
                        {
                            for (int j = 0; j < BranchData; j++)
                            {
                                if ((Convert.ToDateTime(fiscalYearQueryDataSet.Tables[0].Rows[i]["StartDate"].ToString()).Year) < DateOfIncorporation[j].Year)
                                {
                                    if (!CheckDate(DateOfIncorporation[j], Convert.ToDateTime(fiscalYearQueryDataSet.Tables[0].Rows[i]["StartDate"].ToString()), Convert.ToDateTime(fiscalYearQueryDataSet.Tables[0].Rows[i]["EndDate"].ToString())))
                                    {
                                        fiscalYearQueryDataSet.Tables[0].Rows[i].Delete();
                                    }
                                }
                            }
                        }
                        fiscalYearQueryDataSet.Tables[0].AcceptChanges();

                    }
                }
                if (fiscalYearQueryDataSet.Tables[0].Rows.Count > 0)
                {
                    PrintFiscalTable(fiscalYearQueryDataSet, PrintTable, GroupOfCompanyId, CompId, BranchId);
                }
                else
                {
                    if (help < 1)
                    {
                        PrintNoData();
                        PrintTable = false;
                    }
                }
            }// for fiscalall year N
            else
            {

                string FiscalQueryForN = "Select FiscalYearsID From Set_User_Permission_CompBrYear Where UserId='" + userid.ToString() + "' AND RECORD_DELETED='0' AND ACTIVE='1' AND GocId=" + GroupOfCompanyId + " AND COMPId=" + CompId + " AND BranchId=" + BranchId + "";
                SqlDataAdapter FiscalYearForNAdaptar = new SqlDataAdapter(FiscalQueryForN, con);
                DataSet fiscalYearForNDataSet = new DataSet();
                FiscalYearForNAdaptar.Fill(fiscalYearForNDataSet);
                var FiscalRowCount = fiscalYearForNDataSet.Tables[0].Rows.Count;
                if (FiscalRowCount > 0)
                {
                    string Fiscal = "Select FiscalYearsId,FiscalYearAll From Set_User_Permission_CompBrYear Where UserId='" + userid.ToString() + "' AND RECORD_DELETED='0' AND ACTIVE='1' AND GocId=" + GroupOfCompanyId + " AND CompId=" + CompId + " AND BranchId=" + BranchId + " ";
                    SqlDataAdapter FiscalDataAdaptar = new SqlDataAdapter(Fiscal, con);
                    DataSet fiscalDataSet = new DataSet();
                    FiscalDataAdaptar.Fill(fiscalDataSet);
                    var FiscalCount = fiscalDataSet.Tables[0].Rows.Count;
                    if (FiscalCount > 0)
                    {
                        string FiscalDataQueries = "select  FiscalYearID,Description,StartDate,EndDate,(Select Description from SET_Period where PeriodID=SET_Fiscal_Year.PeriodID ) as PeriodDescription from Set_Fiscal_Year where record_deleted='0' And GocId=" + GroupOfCompanyId + " And FiscalYearID In (Select FiscalYearsID From Set_User_Permission_CompBrYear Where UserId='" + userid + "' AND RECORD_DELETED='0' AND ACTIVE='1' AND GocId=" + GroupOfCompanyId + " And CompId=" + CompId + " AND BranchId=" + BranchId + ")";
                        SqlDataAdapter FiscalDataQuriesDataAdaptar = new SqlDataAdapter(FiscalDataQueries, con);
                        DataSet fiscalDataQueryDataSet = new DataSet();
                        FiscalDataQuriesDataAdaptar.Fill(fiscalDataQueryDataSet);
                        if (!(fiscalDataQueryDataSet.Tables[0].Rows.Count > 0))
                        {
                                help++;
                                PrintNoData();
                                PrintTable = false;
                        }
                        else
                        {
                            PrintTable = true;
                            FiscalYearId = fiscalDataQueryDataSet.Tables[0].Rows[0]["FiscalYearId"].ToString();
                            string Branch = "SELECT * FROM SET_BRANCH WHERE BranchId= " + BranchId + " AND RECORD_DELETED='0'";
                            SqlDataAdapter BranchDataAdapter = new SqlDataAdapter(Branch, con);
                            DataSet BranchDataSet = new DataSet();
                            BranchDataAdapter.Fill(BranchDataSet);
                            var BranchData = BranchDataSet.Tables[0].Rows.Count;
                            DateTime[] DateOfIncorporation = new DateTime[BranchData];

                            for (int i = 0; i < BranchData; i++)
                            {
                                DateOfIncorporation[i] = Convert.ToDateTime(BranchDataSet.Tables[0].Rows[i]["DateOfIncorporation"].ToString());

                            }

                            for (int i = 0; i < fiscalDataQueryDataSet.Tables[0].Rows.Count; i++)
                            {
                                for (int j = 0; j < BranchData; j++)
                                {
                                    if ((Convert.ToDateTime(fiscalDataQueryDataSet.Tables[0].Rows[i]["StartDate"].ToString()).Year) <= DateOfIncorporation[j].Year)
                                    {
                                        if (!CheckDate(DateOfIncorporation[i], Convert.ToDateTime(fiscalDataQueryDataSet.Tables[0].Rows[i]["StartDate"].ToString()), Convert.ToDateTime(fiscalDataQueryDataSet.Tables[0].Rows[i]["EndDate"].ToString())))
                                        {
                                            fiscalDataQueryDataSet.Tables[0].Rows[i].Delete();
                                        }
                                    }
                                }
                            }
                            fiscalDataQueryDataSet.Tables[0].AcceptChanges();

                        }
                        if (fiscalDataQueryDataSet.Tables[0].Rows.Count > 0)
                        {
                            ViewState["TemporaryDataSet"] = fiscalDataQueryDataSet;
                            PrintFiscalTable(fiscalDataQueryDataSet, PrintTable, GroupOfCompanyId, CompId, BranchId);
                        }
                        else
                        {
                            if (help < 1)
                            {
                                PrintNoData();
                                PrintTable = false;
                            }
                        }
                    }

                }
                else
                {
                    if (help < 1)
                    {
                        PrintNoData();
                        PrintTable = false;
                    }
                }
            }
            con.Close();
        }
    }


    protected void PrintNoData()
    {
        StringBuilder html = new StringBuilder();
        html.Append("<p class='d-flex justify-content-center text-center shadow '>");
        html.Append("<b>No Fiscal Year</b>");
        html.Append("</p>");
        PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });

    }

    protected void PrintFiscalTable(DataSet dtSet, bool PrintTable, int? GocId, int? CompanyId, int? BranchId)
    {
        string[] arr = new string[dtSet.Tables[0].Rows.Count];

        for (int i = 0; i < dtSet.Tables[0].Rows.Count; i++)
        {
            arr[i] = dtSet.Tables[0].Rows[i]["FiscalYearId"].ToString();
        }

        string id = dtSet.Tables[0].Rows[0]["FiscalYearId"].ToString();
        bool flag = true;
        if (PrintTable)
        {
            if (this.IsPostBack)
            {
                //Populating a DataTable from database.
                DataTable dt = dtSet.Tables[0];
                dt.Columns[0].ColumnName = "SNo";
                dt.Columns.Remove("PeriodDescription");
                //dt.Columns.Remove("FiscalYearId");
                //dt.Columns.Remove("SortOrder");
                dt.Columns.Remove("StartDate");
                dt.Columns.Remove("EndDate");
                //Building an HTML string.
                StringBuilder html = new StringBuilder();

                //Table start.
                html.Append("<table class='table-responsive-sm table table-striped w-auto'>");
                if (flag == true)
                {
                    //Building the Header row.
                    html.Append("<tr>");
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ColumnName != "FiscalYearID")
                        {

                            html.Append("<th class='bg-navy' style='border: 1px solid #000'>");
                            html.Append(column.ColumnName);
                            html.Append("</th>");
                        }
                    }
                    html.Append("</tr>");
                    flag = false;
                }
                int j = 0;
                int i = 1;
                //Building the Data rows.
                foreach (DataRow row in dt.Rows)
                {
                   
                    html.Append("<tr>");
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ColumnName != "FiscalYearID")
                        {
                            html.Append("<td style='width:100px; color:#000; font-size:16px; border: 1px solid #000'>");

                            if (column.ColumnName == "Description")
                            {

                                string str = "2018-2019.aspx";
                                string url = str + "?UserID=" + userid + "&UsergrpID=" + UgrpID + "&fiscaly=" + row[column.ColumnName].ToString() + "&Fiscal=" + arr[j] + "&GocId=" + GocId + "&CompId=" + CompId + "&BranchId=" + BranchId;
                                html.Append("<a id=" + row[column.ColumnName].ToString() + " runat='server'  AutoPostBack='True' Onclick='UploadDataInCookie(" + GocId + "," + CompId + ",+" + BranchId + "," + arr[j] + ")'    href='" + url + "' style='color:#0026ff;'>");
                                j++;
                                html.Append(row[column.ColumnName]);
                                html.Append("</a>");
                                //Application["fiscalyear"] = row[column.ColumnName].ToString();
                            }
                            else if (column.ColumnName == "SNo")
                            {
                                html.Append("<a href='#' style='color:#0026ff;'>");

                                html.Append(i.ToString());
                                html.Append("</a>");

                            }

                            //else if (column.ColumnName == "StartDate")
                            //{

                            //    string sdate = row[column.ColumnName].ToString();

                            //    html.Append(dml.dateConvert(sdate));
                            //}
                            //else if (column.ColumnName == "EndDate")
                            //{

                            //    string edate = row[column.ColumnName].ToString();

                            //    html.Append(dml.dateConvert(edate));
                            //}
                            //else
                            //{
                            //    html.Append(row[column.ColumnName]);
                            //}

                            html.Append("</td>");
                        }
                    }
                    html.Append("</tr>");
                    i++;
                }
                i = 1;
                //Table end.
                html.Append("</table>");
                //Append the HTML string to Placeholder.
                j = 0;
                PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
            }
        }
    }

















    private DataTable GetData(string query)
    {
        DataTable dt = new DataTable();
        string constr = ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                }
            }
            return dt;
        }
    }
    private DataTable GettblData_forFiscal_Y()
    {
        string constr = ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            string date;
            string compname = Request.Cookies["compNAme"].Value;
            DataSet ds = dml.Find("select  ErpStartDate from SET_Company where CompName='" + namecomp + "'  and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                date = ds.Tables[0].Rows[0]["ErpStartDate"].ToString();
            }
            else
            {
                date = "1990-01-01";
            }
            string date1 = dml.dateconvertString(date);


            string curdate = DateTime.Now.Year + "-06-30";
            string curda = dml.dateconvertString(curdate);

            using (SqlCommand cmd = new SqlCommand("select SortOrder, FiscalYearID , Description,StartDate,EndDate,(select Description from SET_Period where PeriodID = SET_Fiscal_Year.PeriodID)as period_description from  SET_Fiscal_Year where StartDate  between '" + date1 + "' and getdate() and  Record_Deleted = 0 ORDER BY SortOrder"))
            //using (SqlCommand cmd = new SqlCommand("select SortOrder, FiscalYearID , Description,StartDate,EndDate,(select Description from SET_Period where PeriodID = SET_Fiscal_Year.PeriodID)as period_description from  SET_Fiscal_Year where enddate > '"+date1+ "' and  Record_Deleted=0 ORDER BY SortOrder"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }


    public void UploadDataInCookie(object sender, EventArgs e, int GocId, int CompId, int BranchId, string FiscalYearId)
    {

        Response.Cookies["GocId"].Value = Convert.ToString(GocId);
        Response.Cookies["GocId"].Expires = DateTime.Now.AddDays(1);

        Response.Cookies["CompId"].Value = Convert.ToString(CompId);
        Response.Cookies["CompId"].Expires = DateTime.Now.AddDays(1);

        Response.Cookies["BranchId"].Value = Convert.ToString(BranchId);
        Response.Cookies["BranchId"].Expires = DateTime.Now.AddDays(1);

        Response.Cookies["FiscalYearId"].Value = FiscalYearId;
        Response.Cookies["FiscalYearId"].Expires = DateTime.Now.AddDays(1);
    }


    private DataTable GettblData_forFiscal_N()
    {
        string constr = ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            string date;
            string compname = Request.Cookies["compNAme"].Value;
            DataSet ds = dml.Find("select  ErpStartDate from SET_Company where CompName='" + compname + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                date = ds.Tables[0].Rows[0]["ErpStartDate"].ToString();
            }
            else
            {
                date = "1990-01-01";
            }
            string date1 = dml.dateconvertString(date);

            using (SqlCommand cmd = new SqlCommand("select SortOrder, FiscalYearID , Description, StartDate,EndDate,(select Description from SET_Period where PeriodID = SET_Fiscal_Year.PeriodID) as period_id from SET_Fiscal_Year Where FiscalYearID = " + FiscalYearsID + " and  enddate > '" + date1 + "' and  Record_Deleted=0 ORDER BY SortOrder"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }
    protected void valuepass_ServerClick(object sender, EventArgs e)
    {
        Application["Year"] = "asdsad";
    }
    public void valuepass(string app)
    {

    }
}
