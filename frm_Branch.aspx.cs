using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frm_Branch : System.Web.UI.Page
{

    int AddDays, DateFrom, EditDays, DeleteDays;
DmlOperation dml = new DmlOperation();
    FieldHide fd = new FieldHide();
    TextBox S_userid, S_LoginName, S_Displayarea;
    TextBox txtuser;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
    DataSet ds = new DataSet();
    string userid, UserGrpID, FormID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {

            Findbox.Visible = false;
            btnUpdate.Visible = false;
            btnSave.Visible = false;
            Deletebox.Visible = false;
            Editbox.Visible = false;
            btnDelete_after.Visible = false;
            UserGrpID = Request.QueryString["UsergrpID"];
            userid = Request.QueryString["UserID"];
            FormID = Request.QueryString["FormID"];

            ViewState["FormId"] = FormID;
            ViewState["userid"] = userid;

            txtIncorporationDate.Attributes.Add("readonly", "readonly");
            txtEntryDate.Attributes.Add("readonly", "readonly");

            dml.dropdownsql(ddlMainBranchID, "SET_Branch", "BranchName", "BranchId");
            dml.dropdownsql(ddlCountry, "SET_Country", "CountryName", "CountryID");
            dml.dropdownsql(ddlComapnyName, "SET_Company", "CompName", "CompId");

          
            dml.dropdownsql(txtFinanialRodOff, "SET_Round_off", "Description", "Sno");
            dml.dropdownsql(txtSalaryRodOff, "SET_Round_off", "Description", "Sno");

            dml.dropdownsql(ddlEdit_branchname, "SET_Branch", "BranchName", "BranchId");
            dml.dropdownsql(ddlDelete_Branchname, "SET_Branch", "BranchName", "BranchId");
            dml.dropdownsql(ddlFind_BranchNAme, "SET_Branch", "BranchName", "BranchId");




            dml.dropdownsql(ddlEdit_Compname, "SET_Company", "CompName", "CompId");
            dml.dropdownsql(ddlFind_CompNAme, "SET_Company", "CompName", "CompId");
            dml.dropdownsql(ddlDelete_CompNAme, "SET_Company", "CompName", "CompId");
            //   dml.dropdownsql(ddlNatureOFBusiness, "SET_BusinessNature", "BusinessNature", "BusinessNatueID");
            dml.dropdownsql(ddlCurrency, "SET_Currency", "CurrencyName", "CurrencyID");
            fd.HideUSerGrpField(Page.Controls, UserGrpID, FormID, "SET_Branch");
             textClear();

             Showall_Dml();
        }
    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        textClear();
        btnSave.Visible = true;
        btnEdit.Visible = false;
        btnFind.Visible = false;
        btnCancel.Visible = true;
        btnDelete.Visible = false;
        btnInsert.Visible = false;
        btnUpdate.Visible = false;

        ddlComapnyName.Enabled = true;
        lblSerialNo.Enabled = true;
        txtBranchName.Enabled = true;
        rdbMain.Enabled = true;
        rdbSub.Enabled = true;

        ddlMainBranchID.Enabled = true;
        txtAddress1.Enabled = true;
        txtAddress2.Enabled = true;
        txtAddress3.Enabled = true;
        ddlCity.Enabled = false;
        txtZipCode.Enabled = true;
        ddlCountry.Enabled = true;
        txtCountryOfOrigin.Enabled = true;
        txtPhoneNo.Enabled = true;
        txtCellNo.Enabled = true;
        txtFaxNumber.Enabled = true;
        txtEmail.Enabled = true;
        txtIncorporationDate.Enabled = true;
        ddlCurrency.Enabled = true;
        rdb_Mullticurrency_N.Enabled = true;
        rdb_Mullticurrency_Y.Enabled = true;
        txtNTNNo.Enabled = true;
        txtGSTNo.Enabled = true;
        txtSortOrder.Enabled = true;
        txtEntryDate.Enabled = true;
        txtSystem_Date.Enabled = true;
        txtUserName.Enabled = true;
        chkActive.Enabled = true;
        rdbMain.Checked = true;
        rdb_Mullticurrency_Y.Checked = true;
        imgPopup.Enabled = true;
        ImageButton1.Enabled = true;
        txtSalaryRodOff.Enabled = true;
        txtFinanialRodOff.Enabled = true;
        txtDisplayDigit.Enabled = true;

        chkActive.Checked = true;
        txtUserName.Enabled = false ;
        txtUserName.Text = show_username();
        txtSystem_Date.Enabled = false;
        txtSystem_Date.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff ");


    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try {
            int chk = 0;
            int rdb = 0;
            string mainsub;
            if (chkActive.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
            if (rdb_Mullticurrency_Y.Checked == true)
            {
                rdb = 1;
            }
            else
            {
                rdb = 0;
            }
            if (rdbMain.Checked == true)
            {
                mainsub = "M";
            }
            else
            {
                mainsub = "S";
            }
            //                                                                                                                                                                                                                                                                                                                                                                                                                                      BranchId, BranchName    ,CompId ,MainBranchSubBranch,	MainBranchId,	AddressLine1,	AddressLine2,	AddressLine3,	                                                                                        CityID,	ZipCode,	CountryID,	CountryOfOriginID,	                                                        PhoneNumber,	CellNumber,	FaxNumber,	EmailAddress,	                                                    DateOfIncorporation ,BaseCurrencyID,	MultiCurrency,	NTNNumber,	SalesTaxNumber  ,SortOrder,                                                                                 	EntryDate   ,SysDate,	UserId  ,LoginName,	IsActive, guid
            dml.Insert("Insert into SET_Branch(BranchName,CompId ,MainBranchSubBranch,	MainBranchId,	AddressLine1,	AddressLine2,	AddressLine3,	CityID,	ZipCode,	CountryID,	CountryOfOriginID,	PhoneNumber,	CellNumber,	FaxNumber,	EmailAddress,	DateOfIncorporation	,BaseCurrencyID,	MultiCurrency,	NTNNumber,	SalesTaxNumber	,SortOrder,	EntryDate	,SysDate,	UserId	,LoginName,	IsActive, guid, Record_Deleted,FinancialRoundOff,SalaryRoundOff,DisplayDigit,MLD) values('" + txtBranchName.Text + "', " + ddlComapnyName.SelectedItem.Value + ",	'" + mainsub + "'," + ddlMainBranchID.SelectedIndex + "	,'" + txtAddress1.Text + "','" + txtAddress2.Text + "','" + txtAddress3.Text + "'," + ddlCity.SelectedItem.Value + ",'" + txtZipCode.Text + "','" + ddlCountry.SelectedItem.Value + "'," + txtCountryOfOrigin.Text + ",'" + txtPhoneNo.Text + "','" + txtCellNo.Text + "','" + txtFaxNumber.Text + "','" + txtEmail.Text + "','" + txtIncorporationDate.Text + "'," + ddlCurrency.SelectedItem.Value + "," + rdb + ",'" + txtNTNNo.Text + "','" + txtGSTNo.Text + "',	" + txtSortOrder.Text + ",	'" + txtEntryDate.Text + "','" + txtSystem_Date.Text + "',2,'" + txtUserName.Text + "'," + chk + ", '41A28783-6BAF-4C17-ACFD-AB49BEA99045' , '0','"+txtFinanialRodOff.SelectedItem.Value+"','"+txtSalaryRodOff.SelectedItem.Value+"','"+txtDisplayDigit.Text+"','"+dml.Encrypt("h")+"')", "");
            dml.Update("update Set_Company set MLD = '" + dml.Encrypt("q") + "' where CompId = '" + ddlComapnyName.SelectedItem.Value + "'", "");
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", " alertme()", true);

            textClear();
            btnInsert_Click(sender, e);
            txtBranchName.Focus();
        }
        catch(Exception ex)
        {
            Label1.Text = ex.Message;
        }
        //con.Close();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try {
            int chk = 0;
            int rdb = 0;
            string mainsub;
            if (chkActive.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
            if (rdb_Mullticurrency_Y.Checked == true)
            {
                rdb = 1;
            }
            else
            {
                rdb = 0;
            }
            if (rdbMain.Checked == true)
            {
                mainsub = "M";
            }
            else
            {
                mainsub = "S";
            }
          
            DataSet ds_up = dml.Find("select * from SET_Branch WHERE ([BranchId]='" + ViewState["SNO"].ToString()+"') AND ([BranchName]='"+txtBranchName.Text+"') AND ([CompId]='"+ddlComapnyName.SelectedItem.Value+"') AND ([MainBranchSubBranch]='"+mainsub+"') AND ([MainBranchId]='"+ddlMainBranchID.SelectedItem.Value+"') AND ([AddressLine1]='"+txtAddress1.Text+"') AND ([AddressLine2]='"+txtAddress2.Text+"') AND ([AddressLine3]='"+txtAddress3.Text+"') AND ([CityID]='"+ddlCity.SelectedItem.Value+"') AND ([ZipCode]='"+txtZipCode.Text+"') AND ([CountryID]='"+ddlCountry.SelectedItem.Value+"') AND ([CountryOfOriginID]='"+txtCountryOfOrigin.Text+"') AND ([PhoneNumber]='"+txtPhoneNo.Text+"') AND ([CellNumber]='"+txtCellNo.Text+"') AND ([FaxNumber]='"+txtFaxNumber.Text+"') AND ([EmailAddress]='"+ txtEmail.Text+"') AND ([DateOfIncorporation]='"+dml.dateconvertforinsert(txtIncorporationDate)+"') AND ([BaseCurrencyID]='"+ddlCurrency.SelectedItem.Value+"') AND ([MultiCurrency]='"+rdb+"') AND ([NTNNumber]='"+txtNTNNo.Text+"') AND ([SalesTaxNumber]='"+txtGSTNo.Text+"') AND ([SortOrder]='"+txtSortOrder.Text+"') AND  ([EntryDate]='"+dml.dateconvertString(txtEntryDate.Text)+"') AND ([IsActive]='"+chk+ "') AND ([Record_Deleted]='0') AND ([FinancialRoundOff] = '"+txtFinanialRodOff.SelectedItem.Value+ "') AND ([SalaryRoundOff] = '" + txtSalaryRodOff.SelectedItem.Value+ "') AND ([DisplayDigit] = '" + txtDisplayDigit.Text + "')");

            if (ds_up.Tables[0].Rows.Count > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", " noupdate()", true);

                textClear();
                btnInsert.Visible = true;
                btnDelete.Visible = true;
                btnUpdate.Visible = false;
                btnEdit.Visible = true;
                btnFind.Visible = true;
                btnSave.Visible = false;
                btnDelete_after.Visible = false;
                UserGrpID = Request.QueryString["UsergrpID"];
                FormID = Request.QueryString["FormID"];
                Showall_Dml();
            }
            else {
                string sysdate = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss.ffff");
                userid = Request.QueryString["UserID"];
                dml.Update("Update SET_Branch set 	BranchName = '" + txtBranchName.Text + "',	CompId = '" + ddlComapnyName.SelectedItem.Value + "',	MainBranchSubBranch = '" + mainsub + "',	MainBranchId = '" + ddlMainBranchID.SelectedIndex + "',	AddressLine1 = '" + txtAddress1.Text + "',	AddressLine2 = '" + txtAddress2.Text + "',	AddressLine3 = '" + txtAddress3.Text + "',	CityID = '" + ddlCity.SelectedItem.Value + "',	ZipCode = '" + txtZipCode.Text + "',	CountryID = '" + ddlCountry.SelectedItem.Value + "',	CountryOfOriginID = '" + txtCountryOfOrigin.Text + "',	PhoneNumber = '" + txtPhoneNo.Text + "',	CellNumber = '" + txtCellNo.Text + "',	FaxNumber = '" + txtFaxNumber.Text + "',	EmailAddress = '" + txtEmail.Text + "',	DateOfIncorporation = '" + txtIncorporationDate.Text + "',	BaseCurrencyID = '" + ddlCurrency.SelectedItem.Value + "',	MultiCurrency = '" + rdb + "',	NTNNumber = '" + txtNTNNo.Text + "',	SalesTaxNumber = '" + txtGSTNo.Text + "',	SortOrder = '" + txtSortOrder.Text + "',	EntryDate = '" + txtEntryDate.Text + "',	SysDate = '" + txtSystem_Date.Text + "',	UserId = '2',	LoginName = '" + txtUserName.Text + "',	IsActive = '" + chk + "' , FinancialRoundOff='"+txtFinanialRodOff.SelectedItem.Value+ "',SalaryRoundOff='"+txtSalaryRodOff.SelectedItem.Value+ "', DisplayDigit='"+ txtDisplayDigit.Text+ "' where BranchId = '" + ViewState["SNO"].ToString()+ "'", "");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", " Editalert()", true);
                textClear();
                btnInsert.Visible = true;
                btnDelete.Visible = true;
                btnUpdate.Visible = false;
                btnEdit.Visible = true;
                btnFind.Visible = true;
                btnSave.Visible = false;
                btnDelete_after.Visible = false;
            }
        }
        catch(Exception ex)
        {
            Label1.Text = ex.Message;
        }

    }
    public string show_username()
    {
        userid = Request.QueryString["UserID"];
        con.Open();
        SqlDataAdapter user = new SqlDataAdapter("SELECT USER_NAME FROM SET_USER_MANAGER WHERE USERID='" + userid + "'", con);
        DataSet userDataSet = new DataSet();
        user.Fill(userDataSet);
        con.Close();
        return userDataSet.Tables[0].Rows[0]["user_name"].ToString();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        textClear();
        btnInsert.Visible = true;
        btnDelete.Visible = true;
        btnUpdate.Visible = false;
        btnEdit.Visible = true;
        btnFind.Visible = true;
        btnSave.Visible = false;
        btnDelete_after.Visible = false;

        btnDelete_after.Visible = false;

        Deletebox.Visible = false;
        Editbox.Visible = false;
        Findbox.Visible = false;
        fieldbox.Visible = true;
    }

    protected void btnSearchEdit_Click(object sender, EventArgs e)
    {
        Editbox.Visible = true;
        btnFind.Visible = false;
        btnUpdate.Visible = false;
        btnSave.Visible = false;
        btnDelete.Visible = false;
        btnInsert.Visible = false;
        btnCancel.Visible = true;
        btnEdit.Visible = true;
        btnDelete_after.Visible = false;

        ddlComapnyName.Enabled = true;
        lblSerialNo.Enabled = true;
        txtBranchName.Enabled = true;
        rdbMain.Enabled = true;
        rdbSub.Enabled = true;
        ddlMainBranchID.Enabled = true;
        txtAddress1.Enabled = true;
        txtAddress2.Enabled = true;
        txtAddress3.Enabled = true;
        ddlCity.Enabled = true;
        txtZipCode.Enabled = true;
        ddlCountry.Enabled = true;
        txtCountryOfOrigin.Enabled = true;
        txtPhoneNo.Enabled = true;
        txtCellNo.Enabled = true;
        txtFaxNumber.Enabled = true;
        txtEmail.Enabled = true;
        txtIncorporationDate.Enabled = true;
        ddlCurrency.Enabled = true;
        rdb_Mullticurrency_N.Enabled = true;
        rdb_Mullticurrency_Y.Enabled = true;
        txtNTNNo.Enabled = true;
        txtGSTNo.Enabled = true;
        txtSortOrder.Enabled = true;
        txtEntryDate.Enabled = true;
        txtSystem_Date.Enabled = false;
        txtUserName.Enabled = false;
        chkActive.Enabled = true;

        Findbox.Visible = false;
        Deletebox.Visible = false;
        Editbox.Visible = true;
        fieldbox.Visible = false;

        Label1.Text = "";
        try {
            string squer = "select SET_GOC.GOCName,SET_Company.CompName,* from SET_Branch  LEFT JOIN SET_Company on SET_Branch.CompId = SET_Company.CompId LEFT JOIN SET_GOC on SET_Company.GOCId = SET_GOC.GocId";
            string swhere;

            if (ddlEdit_branchname.SelectedIndex != 0)
            {
                swhere = "SET_Branch.BranchId = " + ddlEdit_branchname.SelectedItem.Value;
            }
            else
            {
                swhere = "SET_Branch.BranchId is not null";
            }
            if (ddlEdit_Compname.SelectedIndex != 0)
            {
                swhere = swhere + " and SET_Branch.CompId = '" + ddlEdit_Compname.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and SET_Branch.CompId is not null";
            }
            if (txtEdit_NTN.Text != "")
            {
                swhere = swhere + " and SET_Branch.NTNNumber = '" + txtEdit_NTN.Text + "'";
            }
            else
            {
                swhere = swhere + " and SET_Branch.NTNNumber is not null";
            }
            if (txtEdit_GST.Text != "")
            {
                swhere = swhere + " and SET_Branch.SalesTaxNumber = '" + txtEdit_GST.Text + "'";
            }
            else
            {
                swhere = swhere + " and SET_Branch.SalesTaxNumber is not null";
            }
            if (chkEdit_Active.Checked == true)
            {
                swhere = swhere + " and SET_Branch.IsActive = '1'";
            }
            else if (chkEdit_Active.Checked == false)
            {
                swhere = swhere + " and SET_Branch.IsActive = '0'";
            }
            else
            {
                swhere = swhere + " and SET_Branch.IsActive is not null";
            }

            
            squer = squer + " where " + swhere + "and SET_Branch.Record_Deleted = 0 and SET_Branch.CompId = '" + compid() + "' and SET_Branch.BranchId = '" + branchId() + "' ORDER BY branchName";


            DataSet ds = dml.Find(squer);

            GridView3.DataSource = ds;
            GridView3.DataBind();
        }
        catch(Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        btnCancel.Visible = true;
        btnFind.Visible = true;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnInsert.Visible = false;

        Label1.Text = "";
        string squer = "select SET_GOC.GOCName,SET_Company.CompName,* from SET_Branch  LEFT JOIN SET_Company on SET_Branch.CompId = SET_Company.CompId LEFT JOIN SET_GOC on SET_Company.GOCId = SET_GOC.GocId";
        string swhere;
        
        if (ddlFind_BranchNAme.SelectedIndex != 0)
        {
            swhere = "SET_Branch.BranchId = " + ddlFind_BranchNAme.SelectedItem.Value;
        }
        else
        {
            swhere = "SET_Branch.BranchId is not null";
        }
        if (ddlFind_CompNAme.SelectedIndex != 0)
        {
            swhere = swhere + " and SET_Branch.CompId = '" + ddlFind_CompNAme.SelectedItem.Value + "'";
        }
        else
        {
            swhere = swhere + " and SET_Branch.CompId is not null";
        }
        if (txtFind_NTN.Text != "")
        {
            swhere = swhere + " and SET_Branch.NTNNumber = '" + txtFind_NTN.Text + "'";
        }
        else
        {
            swhere = swhere + " and SET_Branch.NTNNumber is not null";
        }
        if (txtFind_GST.Text != "")
        {
            swhere = swhere + " and SET_Branch.SalesTaxNumber = '" + txtFind_GST.Text + "'";
        }
        else
        {
            swhere = swhere + " and SET_Branch.SalesTaxNumber is not null";
        }
        if (chkFind_Active.Checked == true)
        {
            swhere = swhere + " and SET_Branch.IsActive = '1'";
        }
        else if (chkFind_Active.Checked == false)
        {
            swhere = swhere + " and SET_Branch.IsActive = '0'";
        }
        else
        {
            swhere = swhere + " and IsActive is not null";
        }
        squer = squer + " where " + swhere + "and SET_Branch.Record_Deleted = 0 and SET_Branch.CompId = '" + compid() + "' and SET_Branch.BranchId = '" + branchId() + "'  ORDER BY branchName";

        Findbox.Visible = true;
        fieldbox.Visible = false;

        DataSet dgrid = dml.grid(squer);
        GridView1.DataSource = dgrid;
        GridView1.DataBind();

      //  DataSet ds = dml.Find(squer);
      //  if (ds.Tables[0].Rows.Count > 0)
      //  {
      //   //   DataSet dscomp = dml.Find("select CompName from SET_Company where CompId = " + ds.Tables[0].Rows[0]["Compid"].ToString() + "");
      //    //  DataSet dsUsername = dml.Find("select USER_NAME from SET_User_Manager where UserId='" + ds.Tables[0].Rows[0]["EntryUserID"].ToString() + "'");
      //          //			MultiCurrency				GUID				LoginName	IsActive
      //      ddlComapnyName.SelectedValue = ds.Tables[0].Rows[0]["CompId"].ToString();
      //      lblSerialNo.Text = ds.Tables[0].Rows[0]["BranchId"].ToString();
      //      txtBranchName.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
      ////      txtMain_SUb.Text = ds.Tables[0].Rows[0]["MainBranchSubBranch"].ToString();
      //      ddlMainBranchID.SelectedValue = ds.Tables[0].Rows[0]["MainBranchId"].ToString();
      //      txtAddress1.Text= ds.Tables[0].Rows[0]["AddressLine1"].ToString();
      //      txtAddress2.Text= ds.Tables[0].Rows[0]["AddressLine2"].ToString();
      //      txtAddress3.Text= ds.Tables[0].Rows[0]["AddressLine3"].ToString();
            
      //     // ddlCity.SelectedValue = ds.Tables[0].Rows[0]["CityID"].ToString();
      //      txtZipCode.Text= ds.Tables[0].Rows[0]["ZipCode"].ToString();
      //      ddlCountry.SelectedValue = ds.Tables[0].Rows[0]["CountryID"].ToString();
      //      txtCountryOfOrigin.Text= ds.Tables[0].Rows[0]["CountryOfOriginID"].ToString();
      //      txtPhoneNo.Text= ds.Tables[0].Rows[0]["PhoneNumber"].ToString();
      //      txtCellNo.Text= ds.Tables[0].Rows[0]["CellNumber"].ToString();
      //      txtFaxNumber.Text= ds.Tables[0].Rows[0]["FaxNumber"].ToString();
      //      txtEmail.Text= ds.Tables[0].Rows[0]["EmailAddress"].ToString();
      //      txtIncorporationDate.Text= ds.Tables[0].Rows[0]["DateOfIncorporation"].ToString();
      //      ddlCurrency.SelectedValue = ds.Tables[0].Rows[0]["BaseCurrencyID"].ToString();
      //      txtNTNNo.Text= ds.Tables[0].Rows[0]["NTNNumber"].ToString();
      //      txtGSTNo.Text= ds.Tables[0].Rows[0]["SalesTaxNumber"].ToString();
      //      txtSortOrder.Text= ds.Tables[0].Rows[0]["SortOrder"].ToString();
      //      txtEntryDate.Text= ds.Tables[0].Rows[0]["EntryDate"].ToString();
      //      txtSystem_Date.Text= ds.Tables[0].Rows[0]["SysDate"].ToString();
      //      txtUserName.Text= ds.Tables[0].Rows[0]["UserId"].ToString();
            
      //      if (ds.Tables[0].Rows[0]["MultiCurrency"].ToString() == "True")
      //      {
      //          rdb_Mullticurrency_Y.Checked = true;
      //          rdb_Mullticurrency_N.Checked = false;
      //      }
      //      else
      //      {
      //          rdb_Mullticurrency_Y.Checked = false;
      //          rdb_Mullticurrency_N.Checked = true;
      //      }



      //      dml.dateConvert(txtIncorporationDate);
      //      dml.dateConvert(txtEntryDate);
      //      dml.dateConvert(txtSystem_Date);
    

      //      if (ds.Tables[0].Rows[0]["IsActive"].ToString() == "True")
      //      {
      //          chkActive.Checked = true;
      //      }
      //      else
      //      {
      //          chkActive.Checked = false;
      //      }
      //  }
      //  else
      //  {
      //      textClear();
      //      Label1.ForeColor = System.Drawing.Color.Red;
      //      Label1.Text = "No data found";
      //  }
    }

    protected void btn_delete_Click(object sender, EventArgs e)
    {
        Deletebox.Visible = true;
        btnFind.Visible = false;
        btnUpdate.Visible = false;
        btnSave.Visible = false;
        btnDelete.Visible = true;
        btnInsert.Visible = false;
        btnCancel.Visible = true;
        btnEdit.Visible = false;
        btnDelete_after.Visible = false;
        Label1.Text = "";
        try {
            string squer = "select SET_GOC.GOCName,SET_Company.CompName,* from SET_Branch  LEFT JOIN SET_Company on SET_Branch.CompId = SET_Company.CompId LEFT JOIN SET_GOC on SET_Company.GOCId = SET_GOC.GocId";
            string swhere;

            if (ddlDelete_Branchname.SelectedIndex != 0)
            {
                swhere = "SET_Branch.BranchId = " + ddlDelete_Branchname.SelectedItem.Value;
            }
            else
            {
                swhere = "SET_Branch.BranchId is not null";
            }
            if (ddlDelete_CompNAme.SelectedIndex != 0)
            {
                swhere = swhere + " and SET_Branch.CompId = '" + ddlDelete_CompNAme.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and SET_Branch.CompId is not null";
            }
            if (txtDelete_Ntn.Text != "")
            {
                swhere = swhere + " and SET_Branch.NTNNumber = '" + txtDelete_Ntn.Text + "'";
            }
            else
            {
                swhere = swhere + " and SET_Branch.NTNNumber is not null";
            }
            if (txtDelete_Gst.Text != "")
            {
                swhere = swhere + " and SET_Branch.SalesTaxNumber = '" + txtDelete_Gst.Text + "'";
            }
            else
            {
                swhere = swhere + " and SET_Branch.SalesTaxNumber is not null";
            }
            if (chkDelete_Active.Checked == true)
            {
                swhere = swhere + " and SET_Branch.IsActive = '1'";
            }
            else if (chkDelete_Active.Checked == false)
            {
                swhere = swhere + " and SET_Branch.IsActive = '0'";
            }
            else
            {
                swhere = swhere + " and SET_Branch.IsActive is not null";
            }
            squer = squer + " where " + swhere + "and SET_Branch.Record_Deleted = 0 and SET_Branch.CompId = '" + compid()+ "' and SET_Branch.BranchId = '" + branchId()+"'  ORDER BY branchName";

            Deletebox.Visible = true;
            fieldbox.Visible = false;

            DataSet dgrid = dml.grid(squer);
            GridView2.DataSource = dgrid;
            GridView2.DataBind();
        }
        catch(Exception ex)
        {

        }
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCity.Enabled = true;
        ddlCityFun();


    }
    public void ddlCityFun()
    {

        dml.dropdownsql(ddlCity, "SET_City", "CityName", "CityID", "CountryID", ddlCountry.SelectedValue);

    }
    public void textClear()
    {
        Findbox.Visible = false;
        fieldbox.Visible = true;
        ddlComapnyName.SelectedIndex = 0;
        lblSerialNo.Text = "";
        txtBranchName.Text = "";
        ddlMainBranchID.SelectedIndex = 0;
        txtAddress1.Text = "";
        txtAddress2.Text = "";
        txtAddress3.Text = "";
        rdbMain.Enabled = false;
        rdbSub.Enabled = false;
        txtZipCode.Text = "";
        ddlCountry.SelectedIndex = 0;
        txtCountryOfOrigin.Text = "";
        txtPhoneNo.Text = "";
        txtCellNo.Text = "";
        txtFaxNumber.Text = "";
        txtEmail.Text = "";
        txtIncorporationDate.Text = "";
        ddlCurrency.SelectedIndex = 0;
        rdb_Mullticurrency_N.Checked = false;
        rdb_Mullticurrency_Y.Checked = false;
        txtNTNNo.Text = "";
        txtGSTNo.Text = "";
        txtSortOrder.Text = "";
        txtEntryDate.Text = "";
        txtSystem_Date.Text = "";
        txtUserName.Text = "";
        chkActive.Checked = false;
        txtSalaryRodOff.SelectedIndex = 0;
        txtFinanialRodOff.SelectedIndex = 0;
        txtDisplayDigit.Text = "";



        txtSalaryRodOff.Enabled  = false;
        txtFinanialRodOff.Enabled  = false;
        txtDisplayDigit.Enabled  = false;
        ddlComapnyName.Enabled = false;
        lblSerialNo.Enabled = false;
        txtBranchName.Enabled = false; 
        ddlMainBranchID.Enabled = false;
        txtAddress1.Enabled = false;
        txtAddress2.Enabled = false;
        txtAddress3.Enabled = false;
        ddlCity.Enabled = false;
        txtZipCode.Enabled = false;
        ddlCountry.Enabled = false;
        txtCountryOfOrigin.Enabled = false;
        txtPhoneNo.Enabled = false;
        txtCellNo.Enabled = false;
        txtFaxNumber.Enabled = false;
        txtEmail.Enabled = false;
        txtIncorporationDate.Enabled = false;
        ddlCurrency.Enabled = false;
        rdb_Mullticurrency_N.Enabled = false;
        rdb_Mullticurrency_Y.Enabled = false;
        txtNTNNo.Enabled = false;
        txtGSTNo.Enabled = false;
        txtSortOrder.Enabled = false;
        txtEntryDate.Enabled = false;
        txtSystem_Date.Enabled = false;
        txtUserName.Enabled = false;
        chkActive.Enabled = false;
        Label1.Text = "";
        imgPopup.Enabled = false;
        ImageButton1.Enabled = false;

    }
    
    public void Showall_Dml()
    {
        userid = Request.QueryString["UserID"];
        FormID = Request.QueryString["FormID"];
        DataSet FiscalStatus;
        con.Open();
        string Query = "SELECT * FROM SET_FISCAL_YEAR WHERE FISCALYEARID=" + FiscalYear();
        DataSet Fiscal = dml.Find(Query);
        string FiscalStatusQuery = "SELECT * FROM SET_FISCAL_YEAR_STATUS WHERE FISCALYEARSTATUSID=(SELECT FISCALYEARSTATUSID FROM SET_FISCAL_YEAR WHERE FISCALYEARID=" + FiscalYear() + ") AND RECORD_DELETED='0'";
        FiscalStatus = dml.Find(FiscalStatusQuery);
        
        var FiscalId = Request.Cookies["FiscalYearId"].Value;
        SqlDataAdapter da = new SqlDataAdapter("select * from SET_UserGrp_Form where FormId='" + FormID + "' and DmlAllowed = 'Y' AND UserGrpId in (Select UserGrpId from SET_Assign_UserGrp where UserId='" + userid + "' AND Record_Deleted='0') ", con);
        DataSet UserGroupFormDataSet = new DataSet();
        da.Fill(UserGroupFormDataSet);
        int FiscalStatusId = Convert.ToInt32(FiscalStatus.Tables[0].Rows[0]["FiscalYearStatusId"].ToString());
        if (FiscalStatusId == 1)
        {
            var count = UserGroupFormDataSet.Tables[0].Rows.Count;
            bool[] UserGroupBool = new bool[count];
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (UserGroupFormDataSet.Tables[0].Rows[i]["Add"].ToString() == "N")
                    {
                        UserGroupBool[i] = false;
                    }
                    else {
                        UserGroupBool[i] = true;
                    }
                }
                if (UserGroupBool.Contains(true))
                {
                    btnInsert.Visible = true;
                }
                else {
                    btnInsert.Visible = false;
                }



                for (int i = 0; i < count; i++)
                {
                    if (UserGroupFormDataSet.Tables[0].Rows[i]["Edit"].ToString() == "N")
                    {
                        UserGroupBool[i] = false;
                    }
                    else {
                        UserGroupBool[i] = true;
                    }
                }
                if (UserGroupBool.Contains(true))
                {
                    btnEdit.Visible = true;
                }
                else {
                    btnEdit.Visible = false;
                }

                for (int i = 0; i < count; i++)
                {
                    if (UserGroupFormDataSet.Tables[0].Rows[i]["DELETE"].ToString() == "N")
                    {
                        UserGroupBool[i] = false;
                    }
                    else {
                        UserGroupBool[i] = true;
                    }
                }
                if (UserGroupBool.Contains(true))
                {
                    btnDelete.Visible = true;
                }
                else {
                    btnDelete.Visible = false;
                }

            }
            else
            {
                btnInsert.Visible = false;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                btnFind.Visible = true;
                btnCancel.Visible = true;
            }




            for (int i = 0; i < count; i++)
            {
                if (i != 0)
                {
                    if (UserGroupFormDataSet.Tables[0].Rows[i]["DmlAllowed"].ToString() == "Y")
                    {
                        if (Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i]["DateFrom"].ToString()) > Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i - 1]["DateFrom"].ToString()))
                        {
                            DateFrom = Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i]["DateFrom"].ToString());
                        }
                    }
                }
                else {
                    if (UserGroupFormDataSet.Tables[0].Rows[i]["DmlAllowed"].ToString() == "Y")
                    {
                        DateFrom = Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i]["DateFrom"].ToString());
                    }
                }
            }


            for (int i = 0; i < count; i++)
            {
                if (i != 0)
                {
                    if (UserGroupFormDataSet.Tables[0].Rows[i]["Add"].ToString() == "Y")
                    {
                        if (Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i]["AddDays"].ToString()) > Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i - 1]["AddDays"].ToString()))
                        {
                            AddDays = Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i]["AddDays"].ToString());
                        }
                    }
                }
                else {
                    if (UserGroupFormDataSet.Tables[0].Rows[i]["Add"].ToString() == "Y")
                    {
                        AddDays = Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i]["AddDays"].ToString());
                    }
                }
            }

            for (int i = 0; i < count; i++)
            {
                if (i != 0)
                {
                    if (UserGroupFormDataSet.Tables[0].Rows[i]["Edit"].ToString() == "Y")
                    {
                        if (Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i]["EditDays"].ToString()) > Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i - 1]["EditDays"].ToString()))
                        {
                            EditDays = Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i]["EditDays"].ToString());
                        }
                    }
                }
                else {
                    if (UserGroupFormDataSet.Tables[0].Rows[i]["Edit"].ToString() == "Y")
                    {
                        EditDays = Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i]["EditDays"].ToString());
                    }
                }
            }


            for (int i = 0; i < count; i++)
            {
                if (i != 0)
                {
                    if (UserGroupFormDataSet.Tables[0].Rows[i]["Delete"].ToString() == "Y")
                    {
                        if (Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i]["DeleteDays"].ToString()) > Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i - 1]["EditDays"].ToString()))
                        {
                            DeleteDays = Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i]["DeleteDays"].ToString());
                        }
                    }
                }
                else {
                    if (UserGroupFormDataSet.Tables[0].Rows[i]["Delete"].ToString() == "Y")
                    {
                        DeleteDays = Convert.ToInt32(UserGroupFormDataSet.Tables[0].Rows[i]["DeleteDays"].ToString());
                    }
                }
            }
        }
        else if (FiscalStatusId == 2)
        {
            btnInsert.Visible = false;
            btnEdit.Visible = false;
            btnFind.Visible = true;
            btnDelete.Visible = false;
            btnCancel.Visible = false;

        }
        con.Close();

        //con.Open();
        //SqlDataAdapter da = new SqlDataAdapter("select * from SET_UserGrp_Form where FormId='" + FormID + "' and DmlAllowed = 'Y'", con);
        //DataSet ds = new DataSet();
        //da.Fill(ds);
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    if (ds.Tables[0].Rows[0]["Add"].ToString() == "N")
        //    {
        //        btnInsert.Visible = false;
        //    }
        //    if (ds.Tables[0].Rows[0]["Edit"].ToString() == "N")
        //    {
        //        btnEdit.Visible = false;
        //    }
        //    if (ds.Tables[0].Rows[0]["Delete"].ToString() == "N")
        //    {
        //        btnDelete.Visible = false;
        //    }
        //}
        //else
        //{
        //    btnInsert.Visible = false;
        //    btnEdit.Visible = false;
        //    btnDelete.Visible = false;
        //    btnFind.Visible = true;
        //    btnCancel.Visible = false;
        //}
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
           dml.Delete("update SET_Branch set Record_Deleted = 1 where BranchId  = " + ViewState["SNO"].ToString() + "", "");
           textClear();
           ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "alertDelete()", true);
            btnInsert.Visible = true;
            btnDelete.Visible = true;
            btnUpdate.Visible = false;
            btnEdit.Visible = true;
            btnFind.Visible = true;
            btnSave.Visible = false;
            btnDelete_after.Visible = false;
            UserGrpID = Request.QueryString["UsergrpID"];
            FormID = Request.QueryString["FormID"];
            Showall_Dml();
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        textClear();
        btnInsert.Visible = false;
        btnCancel.Visible = true;
        btnUpdate.Visible = false;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnFind.Visible = true;
        Label1.Text = "";

        Findbox.Visible = false;
        Editbox.Visible = false;
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;

        try
        {

            string serial_id;
            serial_id = GridView1.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select * from SET_Branch where BranchId = '" + ViewState["SNO"].ToString() + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {


                ddlComapnyName.ClearSelection();
                ddlMainBranchID.ClearSelection();
                ddlCountry.ClearSelection();
                ddlCity.ClearSelection();
                ddlCurrency.ClearSelection();

                txtBranchName.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
                ddlComapnyName.Items.FindByValue(ds.Tables[0].Rows[0]["CompId"].ToString()).Selected = true;
                string chk_Main_Sub = ds.Tables[0].Rows[0]["MainBranchSubBranch"].ToString();
                ddlMainBranchID.Items.FindByValue(ds.Tables[0].Rows[0]["MainBranchId"].ToString()).Selected = true;
                txtAddress1.Text = ds.Tables[0].Rows[0]["AddressLine1"].ToString();
                txtAddress2.Text = ds.Tables[0].Rows[0]["AddressLine2"].ToString();
                txtAddress3.Text = ds.Tables[0].Rows[0]["AddressLine3"].ToString();
                ddlCountry.Items.FindByValue(ds.Tables[0].Rows[0]["CountryID"].ToString()).Selected = true;
                ddlCityFun();
                ddlCity.Items.FindByValue(ds.Tables[0].Rows[0]["CityID"].ToString()).Selected = true;
                txtZipCode.Text = ds.Tables[0].Rows[0]["ZipCode"].ToString(); ;
                txtCountryOfOrigin.Text = ds.Tables[0].Rows[0]["CountryOfOriginID"].ToString(); ;
                txtPhoneNo.Text = ds.Tables[0].Rows[0]["PhoneNumber"].ToString();
                txtCellNo.Text = ds.Tables[0].Rows[0]["CellNumber"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["EmailAddress"].ToString();
                txtFaxNumber.Text = ds.Tables[0].Rows[0]["FaxNumber"].ToString(); ;
                ddlCurrency.Items.FindByValue(ds.Tables[0].Rows[0]["BaseCurrencyID"].ToString()).Selected = true;
                bool chk_MultiCurr = bool.Parse(ds.Tables[0].Rows[0]["MultiCurrency"].ToString());
                txtNTNNo.Text = ds.Tables[0].Rows[0]["NTNNumber"].ToString();
                txtGSTNo.Text = ds.Tables[0].Rows[0]["SalesTaxNumber"].ToString();
                txtSortOrder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();
                txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtFinanialRodOff.ClearSelection();
                txtSalaryRodOff.ClearSelection();


                if (txtFinanialRodOff.Items.FindByValue(ds.Tables[0].Rows[0]["FinancialRoundOff"].ToString()) != null)
                {
                    txtFinanialRodOff.Items.FindByValue(ds.Tables[0].Rows[0]["FinancialRoundOff"].ToString()).Selected = true;
                }
                if (txtSalaryRodOff.Items.FindByValue(ds.Tables[0].Rows[0]["SalaryRoundOff"].ToString()) != null)
                {
                    txtSalaryRodOff.Items.FindByValue(ds.Tables[0].Rows[0]["SalaryRoundOff"].ToString()).Selected = true;

                }
                txtDisplayDigit.Text = ds.Tables[0].Rows[0]["DisplayDigit"].ToString();
                txtIncorporationDate.Text = ds.Tables[0].Rows[0]["DateOfIncorporation"].ToString();
                txtSystem_Date.Text = ds.Tables[0].Rows[0]["SysDate"].ToString();
                txtUserName.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UserId"].ToString());
                bool ChkActive = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());

                if (ds.Tables[0].Rows[0]["EntryDate"].ToString() == "")
                {
                    txtEntryDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["EntryDate"].ToString());
                    txtEntryDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }


                if (ChkActive == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (chk_MultiCurr == true)
                {
                    rdb_Mullticurrency_Y.Checked = true;
                    rdb_Mullticurrency_N.Checked = false;
                }
                else
                {
                    rdb_Mullticurrency_N.Checked = true;
                    rdb_Mullticurrency_Y.Checked = false;
                }
                if (chk_Main_Sub == "M")
                {
                    rdbMain.Checked = true;
                    rdbSub.Checked = false;

                }
                if (chk_Main_Sub == "S")
                {
                    rdbMain.Checked = false;
                    rdbSub.Checked = true;
                }

                dml.dateConvert(txtEntryDate);
                dml.dateConvert(txtIncorporationDate);
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
    {
        textClear();
        ddlComapnyName.Enabled = true;
        lblSerialNo.Enabled = true;
        txtBranchName.Enabled = true;
      //  txtMain_SUb.Enabled = true;
        ddlMainBranchID.Enabled = true;
        txtAddress1.Enabled = true;
        txtAddress2.Enabled = true;
        txtAddress3.Enabled = true;
        ddlCity.Enabled = true;
        txtZipCode.Enabled = true;
        ddlCountry.Enabled = true;
        txtCountryOfOrigin.Enabled = true;
        txtPhoneNo.Enabled = true;
        txtCellNo.Enabled = true;
        txtFaxNumber.Enabled = true;
        txtEmail.Enabled = true;
        txtIncorporationDate.Enabled = true;
        ddlCurrency.Enabled = true;
        rdb_Mullticurrency_N.Enabled = true;
        rdb_Mullticurrency_Y.Enabled = true;
        txtNTNNo.Enabled = true;
        txtGSTNo.Enabled = true;
        txtSortOrder.Enabled = true;
        txtEntryDate.Enabled = true;
        txtSystem_Date.Enabled = false;
        txtUserName.Enabled = false;
        chkActive.Enabled = true;
        imgPopup.Enabled = true;
        ImageButton1.Enabled = true;
        txtFinanialRodOff.Enabled = true;
        txtSalaryRodOff.Enabled = true;
        txtDisplayDigit.Enabled = true;

        btnInsert.Visible = false;
        btnCancel.Visible = true;
        btnUpdate.Visible = true;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnFind.Visible = false;
        Label1.Text = "";

        dml.dropdownsql(ddlMainBranchID, "SET_Branch", "BranchName", "BranchId");
       // dml.dropdownsql(ddlCountry, "SET_Country", "CountryName", "CountryID");
       // dml.dropdownsql(ddlComapnyName, "SET_Company", "CompName", "CompId");


        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try {
            string serial_id;
            serial_id = GridView3.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select * from SET_Branch where BranchId = '" + ViewState["SNO"].ToString() + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlComapnyName.ClearSelection();
                ddlMainBranchID.ClearSelection();
                ddlCountry.ClearSelection();
                ddlCity.ClearSelection();
                ddlCurrency.ClearSelection();


                txtBranchName.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
                ddlComapnyName.Items.FindByValue(ds.Tables[0].Rows[0]["CompId"].ToString()).Selected = true;
                string chk_Main_Sub = ds.Tables[0].Rows[0]["MainBranchSubBranch"].ToString();
                ddlMainBranchID.Items.FindByValue(ds.Tables[0].Rows[0]["MainBranchId"].ToString()).Selected = true;
                txtAddress1.Text = ds.Tables[0].Rows[0]["AddressLine1"].ToString();
                txtAddress2.Text = ds.Tables[0].Rows[0]["AddressLine2"].ToString();
                txtAddress3.Text = ds.Tables[0].Rows[0]["AddressLine3"].ToString();
                ddlCountry.Items.FindByValue(ds.Tables[0].Rows[0]["CountryID"].ToString()).Selected = true;
                ddlCityFun();
                ddlCity.Items.FindByValue(ds.Tables[0].Rows[0]["CityID"].ToString()).Selected = true;
                txtZipCode.Text = ds.Tables[0].Rows[0]["ZipCode"].ToString(); ;
                txtCountryOfOrigin.Text = ds.Tables[0].Rows[0]["CountryOfOriginID"].ToString(); ;
                txtPhoneNo.Text = ds.Tables[0].Rows[0]["PhoneNumber"].ToString();
                txtCellNo.Text = ds.Tables[0].Rows[0]["CellNumber"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["EmailAddress"].ToString();
                txtFaxNumber.Text = ds.Tables[0].Rows[0]["FaxNumber"].ToString(); ;
                ddlCurrency.Items.FindByValue(ds.Tables[0].Rows[0]["BaseCurrencyID"].ToString()).Selected = true;
                bool chk_MultiCurr = bool.Parse(ds.Tables[0].Rows[0]["MultiCurrency"].ToString());
                txtNTNNo.Text = ds.Tables[0].Rows[0]["NTNNumber"].ToString();
                txtGSTNo.Text = ds.Tables[0].Rows[0]["SalesTaxNumber"].ToString();
                txtSortOrder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();
                txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtFinanialRodOff.ClearSelection();
                txtSalaryRodOff.ClearSelection();


                if (txtFinanialRodOff.Items.FindByValue(ds.Tables[0].Rows[0]["FinancialRoundOff"].ToString()) != null)
                {
                    txtFinanialRodOff.Items.FindByValue(ds.Tables[0].Rows[0]["FinancialRoundOff"].ToString()).Selected = true;
                }
                if (txtSalaryRodOff.Items.FindByValue(ds.Tables[0].Rows[0]["SalaryRoundOff"].ToString()) != null)
                {
                    txtSalaryRodOff.Items.FindByValue(ds.Tables[0].Rows[0]["SalaryRoundOff"].ToString()).Selected = true;

                }
                txtDisplayDigit.Text = ds.Tables[0].Rows[0]["DisplayDigit"].ToString();
                txtIncorporationDate.Text = ds.Tables[0].Rows[0]["DateOfIncorporation"].ToString();
                txtSystem_Date.Text = ds.Tables[0].Rows[0]["SysDate"].ToString();
                txtUserName.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UserId"].ToString());
                bool ChkActive = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());

                if (ds.Tables[0].Rows[0]["EntryDate"].ToString() == "")
                {
                    txtEntryDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["EntryDate"].ToString());
                    txtEntryDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }


                if (ChkActive == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (chk_MultiCurr == true)
                {
                    rdb_Mullticurrency_Y.Checked = true;
                    rdb_Mullticurrency_N.Checked = false;
                }
                else
                {
                    rdb_Mullticurrency_N.Checked = true;
                    rdb_Mullticurrency_Y.Checked = false;
                }
                if (chk_Main_Sub == "M")
                {
                    rdbMain.Checked = true;
                    rdbSub.Checked = false;

                }
                if (chk_Main_Sub == "S")
                {
                    rdbMain.Checked = false;
                    rdbSub.Checked = true;
                }
               
                dml.dateConvert(txtEntryDate);
                dml.dateConvert(txtIncorporationDate);
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnInsert.Visible = false;
        btnCancel.Visible = true;
        btnUpdate.Visible = false;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnFind.Visible = false;
        btnDelete_after.Visible = true;
        textClear();
        
        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        Deletebox.Visible = false;
        try
        {

            string serial_id;
            serial_id = GridView2.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select * from SET_Branch where BranchId = '" + ViewState["SNO"].ToString() + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                

                ddlComapnyName.ClearSelection();
                ddlMainBranchID.ClearSelection();
                ddlCountry.ClearSelection();
                ddlCity.ClearSelection();
                ddlCurrency.ClearSelection();

                txtBranchName.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
                ddlComapnyName.Items.FindByValue(ds.Tables[0].Rows[0]["CompId"].ToString()).Selected = true;
                string chk_Main_Sub = ds.Tables[0].Rows[0]["MainBranchSubBranch"].ToString();
                ddlMainBranchID.Items.FindByValue(ds.Tables[0].Rows[0]["MainBranchId"].ToString()).Selected = true;
                txtAddress1.Text = ds.Tables[0].Rows[0]["AddressLine1"].ToString();
                txtAddress2.Text = ds.Tables[0].Rows[0]["AddressLine2"].ToString();
                txtAddress3.Text = ds.Tables[0].Rows[0]["AddressLine3"].ToString();
                ddlCountry.Items.FindByValue(ds.Tables[0].Rows[0]["CountryID"].ToString()).Selected = true;
                ddlCityFun();
                ddlCity.Items.FindByValue(ds.Tables[0].Rows[0]["CityID"].ToString()).Selected = true;
                txtZipCode.Text = ds.Tables[0].Rows[0]["ZipCode"].ToString(); ;
                txtCountryOfOrigin.Text = ds.Tables[0].Rows[0]["CountryOfOriginID"].ToString(); ;
                txtPhoneNo.Text = ds.Tables[0].Rows[0]["PhoneNumber"].ToString();
                txtCellNo.Text = ds.Tables[0].Rows[0]["CellNumber"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["EmailAddress"].ToString();
                txtFaxNumber.Text = ds.Tables[0].Rows[0]["FaxNumber"].ToString(); ;
                ddlCurrency.Items.FindByValue(ds.Tables[0].Rows[0]["BaseCurrencyID"].ToString()).Selected = true;
                bool chk_MultiCurr = bool.Parse(ds.Tables[0].Rows[0]["MultiCurrency"].ToString());
                txtNTNNo.Text = ds.Tables[0].Rows[0]["NTNNumber"].ToString();
                txtGSTNo.Text = ds.Tables[0].Rows[0]["SalesTaxNumber"].ToString();
                txtSortOrder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();
                txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();


                txtFinanialRodOff.ClearSelection();
                txtSalaryRodOff.ClearSelection();


                if (txtFinanialRodOff.Items.FindByValue(ds.Tables[0].Rows[0]["FinancialRoundOff"].ToString()) != null)
                {
                    txtFinanialRodOff.Items.FindByValue(ds.Tables[0].Rows[0]["FinancialRoundOff"].ToString()).Selected = true;
                }
                if (txtSalaryRodOff.Items.FindByValue(ds.Tables[0].Rows[0]["SalaryRoundOff"].ToString()) != null)
                {
                    txtSalaryRodOff.Items.FindByValue(ds.Tables[0].Rows[0]["SalaryRoundOff"].ToString()).Selected = true;

                }
                txtDisplayDigit.Text = ds.Tables[0].Rows[0]["DisplayDigit"].ToString();


                txtIncorporationDate.Text = ds.Tables[0].Rows[0]["DateOfIncorporation"].ToString();
                txtSystem_Date.Text = ds.Tables[0].Rows[0]["SysDate"].ToString();
                txtUserName.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["LoginName"].ToString());
                bool ChkActive = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());

                if (ds.Tables[0].Rows[0]["EntryDate"].ToString() == "")
                {
                    txtEntryDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["EntryDate"].ToString());
                    txtEntryDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }


                if (ChkActive == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (chk_MultiCurr == true)
                {
                    rdb_Mullticurrency_Y.Checked = true;
                    rdb_Mullticurrency_N.Checked = false;
                }
                else
                {
                    rdb_Mullticurrency_N.Checked = true;
                    rdb_Mullticurrency_Y.Checked = false;
                }
                if (chk_Main_Sub == "M")
                {
                    rdbMain.Checked = true;
                    rdbSub.Checked = false;

                }
                if (chk_Main_Sub == "S")
                {
                    rdbMain.Checked = false;
                    rdbSub.Checked = true;
                }

                dml.dateConvert(txtEntryDate);
                dml.dateConvert(txtIncorporationDate);
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }

    }

    protected void btnClick123(object sender, EventArgs e)
    {
        try
        {
             dml.Delete("update SET_Branch set Record_Deleted = 1 where BranchId  = " + ViewState["SNO"].ToString() + "", "");
             textClear();
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "alertDelete()", true);
            

            btnInsert.Visible = true;
            btnDelete.Visible = true;
            btnUpdate.Visible = false;
            btnEdit.Visible = true;
            btnFind.Visible = true;
            btnSave.Visible = false;
            btnDelete_after.Visible = false;
            UserGrpID = Request.QueryString["UsergrpID"];
            FormID = Request.QueryString["FormID"];
            Showall_Dml();
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string id = e.Row.Cells[1].Text;
        bool flag = dml.isNumber(id);


        if (flag == true)
        {

            DataSet ds = dml.Find("select BranchId,MLD from SET_Branch where BranchId = '" + id + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string value = ds.Tables[0].Rows[i]["MLD"].ToString();
                    if (dml.Decrypt(value) == "q")
                    {
                        e.Row.Enabled = false;
                        e.Row.Cells[0].ToolTip = "Not Edit";
                        e.Row.BackColor = System.Drawing.Color.LightGray;
                    }

                }
            }
        }
    }

    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string id = e.Row.Cells[1].Text;
        bool flag = dml.isNumber(id);


        if (flag == true)
        {

            DataSet ds = dml.Find("select BranchId,MLD from SET_Branch where BranchId = '" + id + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string value = ds.Tables[0].Rows[i]["MLD"].ToString();
                    if (dml.Decrypt(value) == "q")
                    {
                        e.Row.Enabled = false;
                        e.Row.BackColor = System.Drawing.Color.LightGray;
                    }

                }
            }
        }
    }

    protected void ddlComapnyName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //select * from SET_Branch where CompId = 20

        if(ddlComapnyName.SelectedIndex != 0)
        {
            dml.dropdownsqlwithquery(ddlMainBranchID, "select branchId, BranchName from SET_Branch where CompId = 20", "BranchName", "branchId");
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
