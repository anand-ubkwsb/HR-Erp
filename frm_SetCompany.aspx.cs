using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class frm_SetCompany : System.Web.UI.Page
{
    int DateFrom, EditDays, DeleteDays, AddDays;
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

            btnUpdate.Visible = false;
            btnSave.Visible = false;
            btnDelete_after.Visible = false;
            userid = Request.QueryString["UserID"];
            UserGrpID = Request.QueryString["UsergrpID"];
            FormID = Request.QueryString["FormID"];
           
         
            dml.dropdownsql(ddlFnd_GOC, "SET_GOC", "GOCName", "GocId", "GOCName");
            dml.dropdownsql(ddlEdit_GOCName, "SET_GOC", "GOCName", "GocId", "GOCName");
            dml.dropdownsql(ddlDelete_GOCName, "SET_GOC", "GOCName", "GocId", "GOCName");

            dml.dropdownsql(ddlGOCName, "SET_GOC", "GOCName", "GocId", "GOCName");
            dml.dropdownsql(ddlCity, "SET_City", "CityName", "CityID", "CityName");
            dml.dropdownsql(ddlCountry, "SET_Country", "CountryName", "CountryID", "CountryName");
            dml.dropdownsql(ddlNatureOFBusiness, "SET_BusinessNature", "BusinessNature", "BusinessNatueID", "BusinessNature");
            dml.dropdownsql(ddlMainCurrency, "SET_Currency", "CurrencyName", "CurrencyID", "CurrencyName");


            dml.dropdownsql(ddlFind_CompName, "SET_Company", "CompName", "CompId", "CompName");
            dml.dropdownsql(ddlEdit_CompName, "SET_Company", "CompName", "CompId", "CompName");
            dml.dropdownsql(ddlDel_CompName, "SET_Company", "CompName", "CompId", "CompName");

            dml.dropdownsql(txtFind_NOB, "SET_BusinessNature", "BusinessNature", "BusinessNatueID");
            dml.dropdownsql(txtDelete_NOB, "SET_BusinessNature", "BusinessNature", "BusinessNatueID");
            dml.dropdownsql(txtEdit_NOB, "SET_BusinessNature", "BusinessNature", "BusinessNatueID");

            //  dml.dropdownsql(ddlMainBusiness, "", "", "");
            //  dml.dropdownsql(ddlDescriptionMethod, "", "", "");

            CalendarExtender1.EndDate = DateTime.Now;
            txtIncorporationDate_CalendarExtender.EndDate = DateTime.Now;

           
            txtEntry_Date.Attributes.Add("readonly", "readonly");
            txtIncorporationDate.Attributes.Add("readonly", "readonly");
            
            MainView.ActiveViewIndex = 0;


            fd.HideUSerGrpField(Page.Controls, UserGrpID, FormID, "SET_Company");
            Findbox.Visible = false;
            DeleteBox.Visible = false;
            Editbox.Visible = false;
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

        txtCompanyName.Enabled = true;
        ddlNatureOFBusiness.Enabled = true;
        txtAddress1.Enabled = true;
        txtPhoneNo.Enabled = true;
        txtFaxNumber.Enabled = true;
        txtEmail.Enabled = true;
        txtCOntactPerson.Enabled = true;
        txtNTNNo.Enabled = true;
        ddlMainCurrency.Enabled = true;

        rdb_UMC_Y.Enabled = true;
        rdb_UMC_N.Enabled = true;

        ddlMainBusiness.Enabled = true;
        txtBy_Product.Enabled = true;
        txtSegment_7_Size.Enabled = true;
        txtSegment_8_Size.Enabled = true;
        txtSegment_9_Size.Enabled = true;
        lblSerialNo.Enabled = true;
        txtCOA_Format.Enabled = true;
        txtUserName_Entry.Enabled = false;
        ddlCity.Enabled = true;
        ddlCountry.Enabled = true;
        chkActive.Checked = true;
        ddlGOCName.Enabled = true;
        txtAddress2.Enabled = true;
        txtAddress3.Enabled = true;
        txtZipCode.Enabled = true;
        txtCellNo.Enabled = true;
        txtGSTNo.Enabled = true;
        ddlDescriptionMethod.Enabled = true;
        txtSegment_0_Size.Enabled = true;
        txtSegment_1_Size.Enabled = true;
        txtSegment_2_Size.Enabled = true;
        txtSegment_3_Size.Enabled = true;
        txtSegment_4_Size.Enabled = true;
        txtSegment_5_Size.Enabled = true;
        txtSegment_6_Size.Enabled = true;
        txtUserName_loginName.Enabled = false;
        txtEntry_Date.Enabled = true;
        txtSystem_Date.Enabled = false;
        txtAccountSepartor.Enabled = true;
        txtIncorporationDate.Enabled = true;
        rdb_UMC_Y.Checked = true;

        imgPopup.Enabled = true;
        ImageButton1.Enabled = true;

        rdbCOmpanyCOA_NO.Enabled = true;
        rdbCOmpanyCOA_YES.Enabled = true;
        rdbCOmpanyCOA_NO.Checked = true;
        rdbCOmpanyCOA_NO_CheckedChanged(sender, e);
        chkActive.Enabled = true;
        txtSystem_Date.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
        try {
            userid = Request.QueryString["UserID"];
            DataSet dsGrid = dml.Find("select USER_NAME, LoginName from SET_USer_Manager where Userid = '" + userid + "'");
            txtUserName_Entry.Text = dsGrid.Tables[0].Rows[0]["USER_NAME"].ToString();
            txtUserName_loginName.Text = dsGrid.Tables[0].Rows[0]["LoginName"].ToString();
        }
        catch(Exception ex)
        {
            Label1.Text = ex.Message;
        }
        


    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        textClear();
        string userid = Request.QueryString["UserID"];
        txtCompanyName.Enabled = false;
        ddlNatureOFBusiness.Enabled = false;
        txtAddress1.Enabled = false;
        txtPhoneNo.Enabled = false;
        txtFaxNumber.Enabled = false;
        txtEmail.Enabled = false;
        txtCOntactPerson.Enabled = false;
        txtNTNNo.Enabled = false;
        ddlMainCurrency.Enabled = false;
        rdb_UMC_Y.Enabled = false;
        rdb_UMC_N.Enabled = false;
        ddlMainBusiness.Enabled = false;
        txtBy_Product.Enabled = false;
        txtSegment_7_Size.Enabled = false;
        txtSegment_8_Size.Enabled = false;
        txtSegment_9_Size.Enabled = false;
        lblSerialNo.Enabled = false;
        txtCOA_Format.Enabled = false;
        txtUserName_Entry.Enabled = false;
        ddlCity.Enabled = false;
        ddlCountry.Enabled = false;
        chkActive.Checked = false;
        ddlGOCName.Enabled = false;
        txtAddress2.Enabled = false;
        txtAddress3.Enabled = false;
        txtZipCode.Enabled = false;
        txtCellNo.Enabled = false;
        txtGSTNo.Enabled = false;
        ddlDescriptionMethod.Enabled = false;
        txtSegment_0_Size.Enabled = false;
        txtSegment_1_Size.Enabled = false;
        txtSegment_2_Size.Enabled = false;
        txtSegment_3_Size.Enabled = false;
        txtSegment_4_Size.Enabled = false;
        txtSegment_5_Size.Enabled = false;
        txtSegment_6_Size.Enabled = false;
        txtUserName_loginName.Enabled = false;
        txtEntry_Date.Enabled = false;
        txtSystem_Date.Enabled = false;
        txtAccountSepartor.Enabled = false;
        txtIncorporationDate.Enabled = false;
        //txtEmployeeLeftDate.Enabled = false;

        rdbCOmpanyCOA_NO.Enabled = false;
        rdbCOmpanyCOA_YES.Enabled = false;

        Findbox.Visible = true;
        fieldbox.Visible = false;
       // tab_En_dis.Visible = false;

        btnCancel.Visible = true;
        btnFind.Visible = true;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnInsert.Visible = false;

        try {

            string squer = "select * from View_Company ";
            string swhere;

            if (ddlFind_CompName.SelectedIndex != 0)
            {
                swhere = "CompName  = '" + ddlFind_CompName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "CompName is not null";
            }
            if (ddlFnd_GOC.SelectedIndex != 0)
            {
                swhere = swhere + " and GOCId = '" + ddlFnd_GOC.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and GOCId is not null";
            }
            if (txtFind_NOB.SelectedIndex != 0)
            {
                swhere = swhere + " and BusinessNatureId = '" + txtFind_NOB.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and BusinessNatureId is not null";
            }
            if (txtFind_NTN.Text != "")
            {
                swhere = swhere + " and NTNNumber = '" + txtFind_NTN.Text + "'";
            }
            else
            {
                swhere = swhere + " and NTNNumber is not null";
            }
            if (txtFind_GST.Text != "")
            {
                swhere = swhere + " and SalesTaxNumber = '" + txtFind_GST.Text + "'";
            }
            else
            {
                swhere = swhere + " and SalesTaxNumber is not null";
            }
            if (txtFind_ED.Text != "")
            {
                swhere = swhere + " and EntryDate = '" + txtFind_ED.Text + "'";
            }
            else
            {
                swhere = swhere + " and EntryDate is not null";
            }

            if (chkFind_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkFind_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }
            else
            {
                swhere = swhere + " and IsActive is not null";
            }
            squer = squer + " where " + swhere + " and Record_Deleted = 0 and gocid= '" + gocid() + "' and Compid= '" + compid() + "' ORDER BY CompName";


            DataSet dgrid = dml.grid(squer);
           
            if (dgrid.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = dgrid;
                GridView1.DataBind();
            }
            else
            {
                textClear();
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", " nodatafound()", true);
            }

        }
        catch(Exception ex)
        {
            Label1.Text = ex.Message;
        }
       
    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        DeleteBox.Visible = true;
        btnFind.Visible = false;
        btnUpdate.Visible = false;
        btnSave.Visible = false;
        btnDelete.Visible = true;
        btnInsert.Visible = false;
        btnCancel.Visible = true;
        btnEdit.Visible = false;
        btnDelete_after.Visible = false;

        try
        {

            string squer = "select * from View_Company ";
            string swhere;

            if (ddlDel_CompName.SelectedIndex != 0)
            {
                swhere = "CompName =  '" + ddlDel_CompName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "CompName is not null";
            }
            if (ddlDelete_GOCName.SelectedIndex != 0)
            {
                swhere = swhere + " and GOCId = '" + ddlDelete_GOCName.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and GOCId is not null";
            }
            if (txtDelete_NOB.SelectedIndex != 0)
            {
                swhere = swhere + " and BusinessNatureId = '" + txtDelete_NOB.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and BusinessNatureId is not null";
            }
            if (txtDelete_NTN.Text != "")
            {
                swhere = swhere + " and NTNNumber = '" + txtDelete_NTN.Text + "'";
            }
            else
            {
                swhere = swhere + " and NTNNumber is not null";
            }
            if (txtDelete_GST.Text != "")
            {
                swhere = swhere + " and SalesTaxNumber = '" + txtDelete_GST.Text + "'";
            }
            else
            {
                swhere = swhere + " and SalesTaxNumber is not null";
            }
            if (txtDelete_ED.Text != "")
            {
                swhere = swhere + " and EntryDate = '" + txtDelete_ED.Text + "'";
            }
            else
            {
                swhere = swhere + " and EntryDate is not null";
            }

            if (chkDelete_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkDelete_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }
            else
            {
                swhere = swhere + " and IsActive is not null";
            }
            squer = squer + " where " + swhere + " and Record_Deleted = 0 and gocid= '" + gocid() + "' and Compid= '" + compid() + "' ORDER BY CompName";
            Findbox.Visible = false;
            fieldbox.Visible = false;
            Editbox.Visible = false;

            DataSet dgrid = dml.grid(squer);
            GridView2.DataSource = dgrid;
            GridView2.DataBind();
        }
        catch(Exception ex)
        {
            Label1.Text = ex.Message;
        }

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        txtCompanyName.Enabled = false;
        ddlNatureOFBusiness.Enabled = false;
        txtAddress1.Enabled = false;
        txtPhoneNo.Enabled = false;
        txtFaxNumber.Enabled = false;
        txtEmail.Enabled = false;
        txtCOntactPerson.Enabled = false;
        txtNTNNo.Enabled = false;
        ddlMainCurrency.Enabled = false;
        rdb_UMC_N.Enabled = false;
        rdb_UMC_Y.Enabled = false;
        ddlMainBusiness.Enabled = false;
        txtBy_Product.Enabled = false;
        txtSegment_7_Size.Enabled = false;
        txtSegment_8_Size.Enabled = false;
        txtSegment_9_Size.Enabled = false;
        lblSerialNo.Enabled = false;
        txtCOA_Format.Enabled = false;
        txtUserName_Entry.Enabled = false;
        ddlCity.Enabled = false;
        ddlCountry.Enabled = false;
        chkActive.Checked = false;
        ddlGOCName.Enabled = false;
        txtAddress2.Enabled = false;
        txtAddress3.Enabled = false;
        txtZipCode.Enabled = false;
        txtCellNo.Enabled = false;
        txtGSTNo.Enabled = false;
        ddlDescriptionMethod.Enabled = false;
        txtSegment_0_Size.Enabled = false;
        txtSegment_1_Size.Enabled = false;
        txtSegment_2_Size.Enabled = false;
        txtSegment_3_Size.Enabled = false;
        txtSegment_4_Size.Enabled = false;
        txtSegment_5_Size.Enabled = false;
        txtSegment_6_Size.Enabled = false;
        txtUserName_loginName.Enabled = false;
        txtEntry_Date.Enabled = false;
        txtSystem_Date.Enabled = false;
        txtAccountSepartor.Enabled = false;
        txtIncorporationDate.Enabled = false;
        //txtEmployeeLeftDate.Enabled = false;

        rdbCOmpanyCOA_NO.Enabled = false;
        rdbCOmpanyCOA_YES.Enabled = false;


       btnUpdate.Visible = true;
        btnInsert.Visible = false;
        btnDelete.Visible = false;
        btnEdit.Visible = false;

    }
    public void Update()
    {
        try {
            int chk = 0;
            if (chkActive.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }

            string rdb_val = "",rdb_UMC="";
            if (rdbCOmpanyCOA_YES.Checked == true)
            {
                rdb_val = "Y";
            }
            if (rdbCOmpanyCOA_NO.Checked == true)
            {
                rdb_val = "N";
            }

            if (rdb_UMC_Y.Checked == true)
            {
                rdb_UMC = "1";
            }
            if (rdb_UMC_N.Checked == true)
            {
                rdb_UMC = "0";
            }
            userid = Request.QueryString["UserID"];

            txtnull_segment();


            string inc_date= txtIncorporationDate.Text;
            string entry_date = txtEntry_Date.Text;
            
            DataSet ds_up = dml.Find("select  * from SET_Company WHERE ([CompId]='"+ ViewState["SNO"].ToString() + "') AND ([CompName]='"+txtCompanyName.Text+"') AND ([GOCId]='"+ddlGOCName.SelectedItem.Value+"') AND ([BusinessNatureId]='"+ddlNatureOFBusiness.SelectedItem.Value+"') AND ([AddressLine1]='"+txtAddress1.Text+ "') AND ([AddressLine2]='" +txtAddress2.Text + "') AND ([AddressLine3]='" +txtAddress3.Text + "') AND ([CityID]='" +ddlCity.SelectedItem.Value + "') AND ([ZipCode]='"+txtZipCode.Text+"') AND ([CountryID]='"+ddlCountry.SelectedItem.Value+ "') AND ([PhoneNumber]='" +txtPhoneNo.Text + "') AND ([CellNumber]='" +txtCellNo.Text + "') AND ([FaxNumber]='" +txtFaxNumber.Text + "') AND ([EmailAddress]='" +txtEmail.Text + "') AND ([DateOfIncorporation]='" +dml.dateconvertforinsert(txtIncorporationDate) + "') AND ([ContactPerson]='" +txtCOntactPerson.Text + "') AND ([NTNNumber]='"+txtNTNNo.Text+"') AND ([SalesTaxNumber]='"+txtGSTNo.Text+"') AND ([BaseCurrencyID]='"+ddlMainCurrency.SelectedItem.Value+"') AND ([MultiCurrency]='"+ rdb_UMC+"') AND ([DepreciationMethod]='') AND ([InventoryMethod]='"+txtInventoryMethod.Text+"') AND ([Byproduct]='"+txtBy_Product.Text+"') AND ([UseCompanyCOA]='"+rdb_val+"') AND ([Noof_COA_Segments]='"+txtNoOfCOASeg.Text+"') AND ([COA_Separater]='"+txtAccountSepartor.Text+"') AND ([Segment_0_Size]='"+txtSegment_0_Size.Text+ "') AND ([Segment_1_Size]='" + txtSegment_1_Size.Text + "') AND ([Segment_2_Size]='" + txtSegment_2_Size.Text + "') AND ([Segment_3_Size]='" + txtSegment_3_Size.Text + "') AND ([Segment_4_Size] IS NULL) AND ([Segment_5_Size] IS NULL) AND ([Segment_6_Size] IS NULL) AND ([Segment_7_Size] IS NULL) AND ([Segment_8_Size] IS NULL) AND ([Segment_9_Size] IS NULL) AND ([CoaFormat]='" + txtCOA_Format.Text + "') AND ([EntryDate]='"+txtEntry_Date.Text+"') AND ([IsActive]='1') AND ([Record_Deleted]='0')");

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
            }
            else {

               
                dml.Update("update SET_Company  set CompName='" + txtCompanyName.Text + "', GOCId='" + ddlGOCName.Text + "', BusinessNatureId='" + ddlNatureOFBusiness.Text + "', AddressLine1='" + txtAddress1.Text + "', AddressLine2='" + txtAddress2.Text + "', AddressLine3='" + txtAddress3.Text + "', CityID='" + ddlCity.SelectedValue + "', ZipCode='" + txtZipCode.Text + "', CountryID='" + ddlCountry.SelectedValue + "', PhoneNumber='" + txtPhoneNo.Text + "', CellNumber='" + txtCellNo.Text + "', FaxNumber='" + txtPhoneNo.Text + "', EmailAddress='" + txtEmail.Text + "', DateOfIncorporation='" + dml.dateconvertString(inc_date) + "', ContactPerson='" + txtCOntactPerson.Text + "', NTNNumber='" + txtNTNNo.Text + "', SalesTaxNumber='" + txtGSTNo.Text + "', BaseCurrencyID='" + ddlMainCurrency.Text + "', MultiCurrency='" + rdb_UMC + "',DepreciationMethod='" + ddlDescriptionMethod.Text + "', InventoryMethod='" + txtInventoryMethod.Text + "', MainProduct_Services='" + ddlMainBusiness.SelectedItem.Text + "', Byproduct='" + txtBy_Product.Text + "', UseCompanyCOA='" + rdb_val + "', Noof_COA_Segments='" + txtNoOfCOASeg.Text + "', COA_Separater='" + txtAccountSepartor.Text + "', Segment_0_Size=" + txtSegment_0_Size.Text + ", Segment_1_Size=" + txtSegment_1_Size.Text + ",Segment_2_Size =" + txtSegment_2_Size.Text + ", Segment_3_Size=" + txtSegment_3_Size.Text + ", Segment_4_Size=" + txtSegment_4_Size.Text + ", Segment_5_Size=" + txtSegment_5_Size.Text + ", Segment_6_Size=" + txtSegment_6_Size.Text + ", Segment_7_Size=" + txtSegment_7_Size.Text + ", Segment_8_Size=" + txtSegment_8_Size.Text + ", Segment_9_Size=" + txtSegment_9_Size.Text + ", CoaFormat='" + txtCOA_Format.Text + "', EntryUserId='" + userid + "',UserId='2' ,LoginName='" + txtUserName_loginName.Text + "', GUID='41A28783-6BAF-4C17-ACFD-AB49BEA99045',EntryDate='" + entry_date + "',SysDate= GETDATE(),IsActive= " + chk + "  where CompId = " + ViewState["SNO"].ToString() + " ", "");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", " Editalert()", true);
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
    public void textClear()
    {
        ddlCountry.SelectedIndex = 0;
        txtNoOfCOASeg.Enabled = false;
        ddlMainCurrency.SelectedIndex = 0;
        ddlGOCName.SelectedIndex = 0;
       
        ddlNatureOFBusiness.SelectedIndex = 0;
        chkActive.Enabled = false;
        DeleteBox.Visible = false;
        Editbox.Visible = false;
        Findbox.Visible = false;
        fieldbox.Visible = true;
       // tab_En_dis.Visible = true;
        Findbox.Visible = false;
        fieldbox.Visible = true;
        txtCompanyName.Text = "";
        //ddlNatureOFBusiness.Text = "";
        txtAddress1.Text = "";
        txtPhoneNo.Text = "";
        txtFaxNumber.Text = "";
        txtEmail.Text = "";
        txtCOntactPerson.Text = "";
        txtNTNNo.Text = "";
        //ddlMainCurrency.Text = "";
        rdb_UMC_Y.Checked= false;
        rdb_UMC_N.Checked = false;
        ddlMainBusiness.SelectedIndex = 0;
        txtBy_Product.Text = "";
        txtSegment_7_Size.Text = "";
        txtSegment_8_Size.Text = "";
        txtSegment_9_Size.Text = "";
        lblSerialNo.Text = "";
        txtCOA_Format.Text = "";
        txtUserName_Entry.Text = "";
        //ddlCity.Text = "";
        //ddlCountry.Text = "";
        txtAccountSepartor.Enabled = false;
        txtNoOfCOASeg.Text = "";
        //ddlGOCName.Text = "";
        txtAddress2.Text = "";
        txtAddress3.Text = "";
        txtZipCode.Text = "";
        txtCellNo.Text = "";
        txtGSTNo.Text = "";
        ddlDescriptionMethod.Text = "";
        txtSegment_0_Size.Text = "";
        txtSegment_1_Size.Text = ""; 
        txtSegment_2_Size.Text = "";
        txtSegment_3_Size.Text = "";
        txtSegment_4_Size.Text = "";
        txtSegment_5_Size.Text = "";
        txtSegment_6_Size.Text = "";
        txtUserName_loginName.Text = "";
        txtEntry_Date.Text = "";
        txtSystem_Date.Text = "";
        txtAccountSepartor.Text = "";
        txtIncorporationDate.Text = "";
        //txtEmployeeLeftDate.Text = "";
        rdbCOmpanyCOA_NO.Checked = false;
        rdbCOmpanyCOA_YES.Checked = false;
        txtInventoryMethod.Text = "";
        Label1.Text = "";
        imgPopup.Enabled = false;
        ImageButton1.Enabled = false;    

        txtCompanyName.Enabled = false;
        ddlNatureOFBusiness.Enabled = false;
        txtAddress1.Enabled = false;
        txtPhoneNo.Enabled = false;
        txtFaxNumber.Enabled = false;
        txtEmail.Enabled = false;
        txtCOntactPerson.Enabled = false;
        txtNTNNo.Enabled = false;
        ddlMainCurrency.Enabled = false;
        rdb_UMC_N.Enabled = false;
        rdb_UMC_Y.Enabled = false;
        ddlMainBusiness.Enabled = false;
        txtBy_Product.Enabled = false;
       
        lblSerialNo.Enabled = false;
        txtCOA_Format.Enabled = false;
        txtUserName_Entry.Enabled = false;
        ddlCity.Enabled = false;
        ddlCountry.Enabled = false;
        chkActive.Checked = false;
        ddlGOCName.Enabled = false;
        txtAddress2.Enabled = false;
        txtAddress3.Enabled = false;
        txtZipCode.Enabled = false;
        txtCellNo.Enabled = false;
        txtGSTNo.Enabled = false;
        ddlDescriptionMethod.Enabled = false;
        txtSegment_0_Size.Enabled = false;
        txtSegment_1_Size.Enabled = false;
        txtSegment_2_Size.Enabled = false;
        txtSegment_3_Size.Enabled = false;
        txtSegment_4_Size.Enabled = false;
        txtSegment_5_Size.Enabled = false;
        txtSegment_6_Size.Enabled = false;
        txtSegment_7_Size.Enabled = false;
        txtSegment_8_Size.Enabled = false;
        txtSegment_9_Size.Enabled = false;
        txtUserName_loginName.Enabled = false;
        txtEntry_Date.Enabled = false;
        txtSystem_Date.Enabled = false;
        txtAccountSepartor.Enabled = false;
        txtIncorporationDate.Enabled = false;
        // txtEmployeeLeftDate.Enabled = false;

        rdbCOmpanyCOA_NO.Enabled = false;
        rdbCOmpanyCOA_YES.Enabled = false;
        txtInventoryMethod.Enabled = false;

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
        //Tab1_Click(sender, e);


    }

    public void Showall_Dml()
    {
        userid = Request.QueryString["UserID"];
        FormID = Request.QueryString["FormID"];
        DataSet FiscalStatus;
        con.Open();
        string Query = "SELECT * FROM SET_FISCAL_YEAR WHERE FISCALYEARID=" + Convert.ToInt32(Request.Cookies["FiscalYearId"].Value);
        DataSet Fiscal = dml.Find(Query);
        string FiscalStatusQuery = "SELECT * FROM SET_FISCAL_YEAR_STATUS WHERE FISCALYEARSTATUSID=(SELECT FISCALYEARSTATUSID FROM SET_FISCAL_YEAR WHERE FISCALYEARID=" + Convert.ToInt32(Request.Cookies["FiscalYearId"].Value) + ") AND RECORD_DELETED='0'";
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
        //try {
        //    con.Open();
        //    if (FormID != "")
        //    {
        //        SqlDataAdapter da = new SqlDataAdapter("select * from SET_UserGrp_Form where FormId='" + FormID + "' and DmlAllowed = 'Y'", con);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows[0]["Add"].ToString() == "N")
        //            {
        //                btnInsert.Visible = false;
        //            }
        //            if (ds.Tables[0].Rows[0]["Edit"].ToString() == "N")
        //            {
        //                btnEdit.Visible = false;
        //            }
        //            if (ds.Tables[0].Rows[0]["Delete"].ToString() == "N")
        //            {
        //                btnDelete.Visible = false;
        //            }
        //        }
        //        else
        //        {
        //            btnInsert.Visible = false;
        //            btnEdit.Visible = false;
        //            btnDelete.Visible = false;
        //            btnFind.Visible = true;
        //            btnCancel.Visible = false;
        //        }
        //}
        //}
        //catch(Exception ex)
        //{
        //    Label1.Text = ex.Message;
        //}
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Update();
    }
    public void add_save()
    {
        try
        {
            int chk = 0;
            string rdb = "", rdb_UMC="";
            if (chkActive.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
            if (rdbCOmpanyCOA_YES.Checked == true)
            {
                rdb = "Y";
            }
            else
            {
                rdb = "N";
            }

            if (rdb_UMC_Y.Checked == true)
            {
                rdb_UMC = "1";
            }
            if (rdb_UMC_N.Checked == true)
            {
                rdb_UMC = "0";
            }
            txtnull_segment();
        userid = Request.QueryString["UserID"];
        string sQry = "";
        string ddlDescriptionValue = "";
        sQry = "INSERT INTO SET_Company ( [CompName], [GOCId], [BusinessNatureId] ,[AddressLine1], [AddressLine2], [AddressLine3], [CityID], [ZipCode], [CountryID],[PhoneNumber], [CellNumber], [FaxNumber], [EmailAddress], [DateOfIncorporation], [ContactPerson],[NTNNumber], [SalesTaxNumber], [BaseCurrencyID], [MultiCurrency],  [InventoryMethod], [Byproduct], [UseCompanyCOA], [Noof_COA_Segments], [COA_Separater], [Segment_0_Size],[Segment_1_Size], [Segment_2_Size], [Segment_3_Size], [Segment_4_Size], [Segment_5_Size], [Segment_6_Size],[Segment_7_Size], [Segment_8_Size], [Segment_9_Size], [CoaFormat], [enable_Branch_Accounting], [EntryUserId],[UserId], [LoginName], [GUID], [EntryDate], [SysDate], [IsActive],[MainProduct_Services],[Record_Deleted],[MLD])  VALUES( ";
        sQry += "'" + txtCompanyName.Text + "',";
        sQry += "'" + ddlGOCName.SelectedItem.Value + "',";
        sQry += "'" + ddlNatureOFBusiness.SelectedItem.Value + "',";
        sQry += "'" + txtAddress1.Text + "',";
        sQry += "'" + txtAddress2.Text + "',";
        sQry += "'" + txtAddress3.Text + "',";
        sQry += "'" + ddlCity.SelectedItem.Value  + "',";
        sQry += "'" + txtZipCode.Text + "',";
        sQry += "'" + ddlCountry.SelectedItem.Value + "',";
        sQry += "'" + txtPhoneNo.Text + "',";
        sQry += "'" + txtCellNo.Text + "',";
        sQry += "'" + txtFaxNumber.Text + "',";
        sQry += "'" + txtEmail.Text + "',";
        sQry += "'" + dml.dateconvertforinsert(txtIncorporationDate) + "',";
        sQry += "'" + txtCOntactPerson.Text + "',";
        sQry += "'" + txtNTNNo.Text + "',";
        sQry += "'" + txtGSTNo.Text + "',";
        sQry += "'" + ddlMainCurrency.SelectedItem.Value + "',";
        sQry += "'" + rdb_UMC + "',";
       
        sQry += "'" + txtInventoryMethod.Text + "',";
        sQry += "'" + txtBy_Product.Text + "',";
        sQry += "'"+rdb+"',";
        sQry += "'" + txtNoOfCOASeg.Text + "',";
        sQry += "'" + txtAccountSepartor.Text + "',";
        sQry +=  txtSegment_0_Size.Text + ",";
        sQry +=  txtSegment_1_Size.Text + ",";
        sQry +=  txtSegment_2_Size.Text + ",";
        sQry +=  txtSegment_3_Size.Text + ",";
        sQry +=  txtSegment_4_Size.Text + ",";
        sQry +=  txtSegment_5_Size.Text + ",";
        sQry +=  txtSegment_6_Size.Text + ",";
        sQry +=  txtSegment_7_Size.Text + ",";
        sQry +=  txtSegment_8_Size.Text + ",";
        sQry += txtSegment_9_Size.Text + ",";
        sQry  += "'" + MaskedEditExtender1.Mask+ "',";
        sQry += "'Y',";
        sQry += "'" + userid + "','2','"+txtUserName_loginName.Text +"','41A28783-6BAF-4C17-ACFD-AB49BEA99045', ";
        sQry += "'" + dml.dateconvertforinsert(txtEntry_Date) + "',";
        sQry += "'" + dml.dateconvertforinsert(txtSystem_Date) + "'," + chk + " , '"+ddlMainBusiness.SelectedItem.Text+"','0','" + dml.Encrypt("h") + "')";
        
        dml.Insert(sQry, "");
        dml.Update("update SET_GOC set MLD = '" + dml.Encrypt("q") + "' where GocId = '"+ddlGOCName.SelectedItem.Value+"'", "");
               
        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", " alertme()", true);
            textClear();
           
        }
        catch(Exception ex)
        {
            Label1.Enabled = true;
            Label1.Text = ex.Message;
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        add_save();
        btnInsert_Click(sender, e);
    }

    protected void btnSearchUser_Click(object sender, EventArgs e)
    {

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
        try
        {

            string squer = "select * from View_Company ";
            string swhere;

            if (ddlEdit_CompName.SelectedIndex != 0)
            {
                swhere = "CompName =  '" + ddlEdit_CompName.SelectedItem.Text + "'";
            }
            else
            {
                swhere = "CompName is not null";
            }
            if (ddlEdit_GOCName.SelectedIndex != 0)
            {
                swhere = swhere + " and GOCId = '" + ddlEdit_GOCName.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and GOCId is not null";
            }
            if (txtFind_NOB.SelectedIndex != 0)
            {
                swhere = swhere + " and BusinessNatureId = '" + txtFind_NOB.SelectedItem.Value + "'";
            }
            else
            {
                swhere = swhere + " and BusinessNatureId is not null";
            }
            if (txtEdit_NTN.Text != "")
            {
                swhere = swhere + " and NTNNumber = '" + txtEdit_NTN.Text + "'";
            }
            else
            {
                swhere = swhere + " and NTNNumber is not null";
            }
            if (txtEdit_GST.Text != "")
            {
                swhere = swhere + " and SalesTaxNumber = '" + txtEdit_GST.Text + "'";
            }
            else
            {
                swhere = swhere + " and SalesTaxNumber is not null";
            }
            if (txtEdit_Ed.Text != "")
            {
                swhere = swhere + " and EntryDate = '" + txtEdit_Ed.Text + "'";
            }
            else
            {
                swhere = swhere + " and EntryDate is not null";
            }

            if (chkEdit_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkEdit_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }
            else
            {
                swhere = swhere + " and IsActive is not null";
            }
            squer = squer + " where " + swhere + " and Record_Deleted = 0 and gocid= '" + gocid() + "' and Compid= '" + compid() + "' ORDER BY CompName";

            Findbox.Visible = false;
            fieldbox.Visible = false;
            DeleteBox.Visible = false;
            DataSet dgrid = dml.grid(squer);

            if (dgrid.Tables[0].Rows.Count > 0)
            {
                GridView3.DataSource = dgrid;
                GridView3.DataBind();
            }
            else
            {
                textClear();
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", " nodatafound()", true);
            }

        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
        FormID = Request.QueryString["FormID"];
        
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCityFun();

    }
    public void ddlCityFun()
    {

        dml.dropdownsql(ddlCity, "SET_City", "CityName", "CityID", "CountryID", ddlCountry.SelectedValue);
    }

    public void txtnull_segment()
    {
        try {
            if (txtSegment_0_Size.Text == "")
            {
                txtSegment_0_Size.Text = "NULL";
            }
            if (txtSegment_1_Size.Text == "")
            {
                txtSegment_1_Size.Text = "NULL";
            }
            if (txtSegment_2_Size.Text == "")
            {
                txtSegment_2_Size.Text = "NULL";
            }
            if (txtSegment_3_Size.Text == "")
            {
                txtSegment_3_Size.Text = "NULL";
            }
            if (txtSegment_4_Size.Text == "")
            {
                txtSegment_4_Size.Text = "NULL";
            }
            if (txtSegment_5_Size.Text == "")
            {
                txtSegment_5_Size.Text = "NULL";
            }
            if (txtSegment_6_Size.Text == "")
            {
                txtSegment_6_Size.Text = "NULL";
            }
            if (txtSegment_7_Size.Text == "")
            {
                txtSegment_7_Size.Text = "NULL";
            }
            if (txtSegment_8_Size.Text == "")
            {
                txtSegment_8_Size.Text = "NULL";
            }
            if (txtSegment_9_Size.Text == "")
            {
                txtSegment_9_Size.Text = "NULL";
            }
        }
        catch(Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

    protected void txtNoOfCOASeg_Load(object sender, EventArgs e)
    {
        txtSegment_0_Size.Enabled = false;
        txtSegment_1_Size.Enabled = false;
        txtSegment_2_Size.Enabled = false;
        txtSegment_3_Size.Enabled = false;
        txtSegment_4_Size.Enabled = false;
        txtSegment_5_Size.Enabled = false;
        txtSegment_6_Size.Enabled = false;
        txtSegment_7_Size.Enabled = false;
        txtSegment_8_Size.Enabled = false;
        txtSegment_9_Size.Enabled = false;
        try {
            string no = txtNoOfCOASeg.Text;
            if (no == "")
            {
            }
            else {

                for (int i = 1; i <= int.Parse(no); i++)
                {
                    switch (i)
                    {
                        case 1:
                            txtSegment_0_Size.Enabled = true;
                            break;

                        case 2:
                            txtSegment_1_Size.Enabled = true;
                            break;

                        case 3:
                            txtSegment_2_Size.Enabled = true;
                            break;

                        case 4:
                            txtSegment_3_Size.Enabled = true;
                            break;

                        case 5:
                            txtSegment_4_Size.Enabled = true;
                            break;

                        case 6:
                            txtSegment_5_Size.Enabled = true;
                            break;
                        case 7:
                            txtSegment_6_Size.Enabled = true;
                            break;
                        case 8:
                            txtSegment_7_Size.Enabled = true;
                            break;
                        case 9:
                            txtSegment_8_Size.Enabled = true;
                            break;
                        case 10:
                            txtSegment_9_Size.Enabled = true;
                            break;
                    }
                }
                MaskedEditExtender1.Mask = maskFormat(txtAccountSepartor, txtNoOfCOASeg, txtSegment_0_Size, txtSegment_1_Size, txtSegment_2_Size, txtSegment_3_Size, txtSegment_4_Size, txtSegment_5_Size, txtSegment_6_Size, txtSegment_7_Size, txtSegment_8_Size, txtSegment_9_Size);
            }
        }
        catch(Exception ex)
        {
            Label1.Text = ex.Message;
        }


    }
    public string maskFormat(TextBox seperator, TextBox NoofSegm, TextBox segm0, TextBox segm1, TextBox segm2, TextBox segm3, TextBox segm4, TextBox segm5, TextBox segm6, TextBox segm7, TextBox segm8, TextBox segm9)
    {
        string dd = "";
        try {
            // DataSet dsFormat = dml.Find("select NoofCoaSegments,CoaSegmentSeparater,PerSegmentSize0,PerSegmentSize1	,PerSegmentSize2,	PerSegmentSize3	,PerSegmentSize4,	PerSegmentSize5	,PerSegmentSize6,	PerSegmentSize7	,PerSegmentSize8,	PerSegmentSize9	,CoaFormat from SET_GOC where GocID = 1");
            string format = "";
            string noOFSEG = NoofSegm.Text;
            string separator = seperator.Text;
            string seg0 = segm0.Text;
            string seg1 = segm1.Text;
            string seg2 = segm2.Text;
            string seg3 = segm3.Text;
            string seg4 = segm4.Text;
            string seg5 = segm5.Text;
            string seg6 = segm6.Text;
            string seg7 = segm7.Text;
            string seg8 = segm8.Text;
            string seg9 = segm9.Text;
            

            if (seg0 != "" && segm0.Enabled == true)
            {

                for (int s1 = 1; s1 <= int.Parse(seg0) + 1; s1++)
                {
                    if (s1 <= int.Parse(seg0))
                    {
                        format += "0";
                    }
                    else
                    {
                        format += separator;
                    }
                }
            }

            if (seg1 != "" && segm1.Enabled == true)
            {
                for (int s2 = 1; s2 <= int.Parse(seg1) + 1; s2++)
                {
                    if (s2 <= int.Parse(seg1))
                    {
                        format += "0";
                    }
                    else
                    {
                        format += separator;
                    }
                }
            }

            if (seg2 != "" && segm2.Enabled == true)
            {
                for (int s3 = 1; s3 <= int.Parse(seg2) + 1; s3++)
                {
                    if (s3 <= int.Parse(seg2))
                    {
                        format += "0";
                    }
                    else
                    {
                        format += separator;
                    }
                }
            }

            if (seg3 != "" && segm3.Enabled == true)
            {
                for (int s4 = 1; s4 <= int.Parse(seg3) + 1; s4++)
                {
                    if (s4 <= int.Parse(seg3))
                    {
                        format += "0";
                    }
                    else
                    {
                        format += separator;
                    }
                }
            }

            if (seg4 != "" && segm4.Enabled == true)
            {
                for (int s5 = 1; s5 <= int.Parse(seg4) + 1; s5++)
                {
                    if (s5 <= int.Parse(seg4))
                    {
                        format += "0";
                    }
                    else
                    {
                        format += separator;
                    }
                }
            }

            if (seg5 != "" && segm5.Enabled == true)
            {
                for (int s6 = 1; s6 <= int.Parse(seg5) + 1; s6++)
                {
                    if (s6 <= int.Parse(seg5))
                    {
                        format += "0";
                    }
                    else
                    {
                        format += separator;
                    }
                }
            }

            if (seg6 != "" && segm6.Enabled == true)
            {
                for (int s7 = 1; s7 <= int.Parse(seg6) + 1; s7++)
                {
                    if (s7 <= int.Parse(seg6))
                    {
                        format += "0";
                    }
                    else
                    {
                        format += separator;
                    }
                }
            }
            if (seg7 != "" && segm7.Enabled == true)
            {
                for (int s8 = 1; s8 <= int.Parse(seg7) + 1; s8++)
                {
                    if (s8 <= int.Parse(seg7))
                    {
                        format += "0";
                    }
                    else
                    {
                        format += separator;
                    }
                }
            }

            if (seg8 != "" && segm8.Enabled == true)
            {
                for (int s9 = 1; s9 <= int.Parse(seg8) + 1; s9++)
                {
                    if (s9 <= int.Parse(seg8))
                    {
                        format += "0";
                    }
                    else
                    {
                        format += separator;
                    }
                }
            }

            if (seg9 != "" && segm9.Enabled == true)
            {
                for (int s10 = 1; s10 <= int.Parse(seg9) + 1; s10++)
                {
                    if (s10 <= int.Parse(seg9))
                    {
                        format += "0";
                    }
                    else
                    {
                        format += separator;
                    }
                }
            }
            //  str.Remove(str.Length - 1);
            if (format.Length > 0)
            {
                dd = format.Remove(format.Length - 1);

            }
            else
            {
                dd = "0";
            }
           
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
        return dd;
    }

    protected void rdbCOmpanyCOA_NO_CheckedChanged(object sender, EventArgs e)
    {
        txtAccountSepartor.Enabled = false;
        txtNoOfCOASeg.Enabled = false;
        txtSegment_0_Size.Enabled = false;
        txtSegment_1_Size.Enabled = false;
        txtSegment_2_Size.Enabled = false;
        txtSegment_3_Size.Enabled = false;
        txtSegment_4_Size.Enabled = false;
        txtSegment_5_Size.Enabled = false;
        txtSegment_6_Size.Enabled = false;
        txtSegment_7_Size.Enabled = false;
        txtSegment_8_Size.Enabled = false;
        txtSegment_9_Size.Enabled = false;

        txtSegment_0_Size.Text = "";
        txtSegment_1_Size.Text = "";
        txtSegment_2_Size.Text = "";
        txtSegment_3_Size.Text = "";
        txtSegment_4_Size.Text = "";
        txtSegment_5_Size.Text = "";
        txtSegment_6_Size.Text = ""; 
        txtSegment_7_Size.Text = "";
        txtSegment_8_Size.Text = "";
        txtSegment_9_Size.Text = "";

        txtCOA_Format.Enabled = false;
        try {
            string ddlGOC = "";
            if (ddlGOCName.SelectedIndex == 0)
            {
                ddlGOC = "1";
            }
            else
            {
                ddlGOC = ddlGOCName.SelectedItem.Value;
            }

            DataSet dsGrid = dml.Find("select CoaFormat from set_GOC where GOCid = " + ddlGOC + "");
            string masks = dsGrid.Tables[0].Rows[0]["CoaFormat"].ToString();
            MaskedEditExtender1.Mask = masks;
        }
        catch(Exception ex)
        {
            Label1.Text = ex.Message;
        }
        }

    protected void rdbCOmpanyCOA_YES_CheckedChanged(object sender, EventArgs e)
    {
        
        txtAccountSepartor.Enabled = true;
        txtNoOfCOASeg.Enabled = true;
        if (txtNoOfCOASeg.Text == "0")
        {
            
        }
        else if(txtNoOfCOASeg.Text == "1")
        {
            txtSegment_0_Size.Enabled = true;
            txtSegment_1_Size.Enabled = false;
            txtSegment_2_Size.Enabled = false;
            txtSegment_3_Size.Enabled = false;
            txtSegment_4_Size.Enabled = false;
            txtSegment_5_Size.Enabled = false;
            txtSegment_6_Size.Enabled = false;
            txtSegment_7_Size.Enabled = false;
            txtSegment_8_Size.Enabled = false;
            txtSegment_9_Size.Enabled = false;
            txtCOA_Format.Enabled = true;
        }
        else if (txtNoOfCOASeg.Text == "2")
        {
            txtSegment_0_Size.Enabled = true;
            txtSegment_1_Size.Enabled = true;
            txtSegment_2_Size.Enabled = false;
            txtSegment_3_Size.Enabled = false;
            txtSegment_4_Size.Enabled = false;
            txtSegment_5_Size.Enabled = false;
            txtSegment_6_Size.Enabled = false;
            txtSegment_7_Size.Enabled = false;
            txtSegment_8_Size.Enabled = false;
            txtSegment_9_Size.Enabled = false;
            txtCOA_Format.Enabled = true;

        }
        else if (txtNoOfCOASeg.Text == "3")
        {
            txtSegment_0_Size.Enabled = true;
            txtSegment_1_Size.Enabled = true;
            txtSegment_2_Size.Enabled = true;
            txtSegment_3_Size.Enabled = false;
            txtSegment_4_Size.Enabled = false;
            txtSegment_5_Size.Enabled = false;
            txtSegment_6_Size.Enabled = false;
            txtSegment_7_Size.Enabled = false;
            txtSegment_8_Size.Enabled = false;
            txtSegment_9_Size.Enabled = false;
            txtCOA_Format.Enabled = true;

        }
        else if (txtNoOfCOASeg.Text == "4")
        {
            txtSegment_0_Size.Enabled = true;
            txtSegment_1_Size.Enabled = true;
            txtSegment_2_Size.Enabled = true;
            txtSegment_3_Size.Enabled = true;
            txtSegment_4_Size.Enabled = false;
            txtSegment_5_Size.Enabled = false;
            txtSegment_6_Size.Enabled = false;
            txtSegment_7_Size.Enabled = false;
            txtSegment_8_Size.Enabled = false;
            txtSegment_9_Size.Enabled = false;
            txtCOA_Format.Enabled = true;

        }
        else if (txtNoOfCOASeg.Text == "5")
        {
            txtSegment_0_Size.Enabled = true;
            txtSegment_1_Size.Enabled = true;
            txtSegment_2_Size.Enabled = true;
            txtSegment_3_Size.Enabled = true;
            txtSegment_4_Size.Enabled = true;
            txtSegment_5_Size.Enabled = false;
            txtSegment_6_Size.Enabled = false;
            txtSegment_7_Size.Enabled = false;
            txtSegment_8_Size.Enabled = false;
            txtSegment_9_Size.Enabled = false;
            txtCOA_Format.Enabled = true;

        }
        else if (txtNoOfCOASeg.Text == "6")
        {
            txtSegment_0_Size.Enabled = true;
            txtSegment_1_Size.Enabled = true;
            txtSegment_2_Size.Enabled = true;
            txtSegment_3_Size.Enabled = true;
            txtSegment_4_Size.Enabled = true;
            txtSegment_5_Size.Enabled = true;
            txtSegment_6_Size.Enabled = false;
            txtSegment_7_Size.Enabled = false;
            txtSegment_8_Size.Enabled = false;
            txtSegment_9_Size.Enabled = false;
            txtCOA_Format.Enabled = true;
        }
        else if (txtNoOfCOASeg.Text == "7")
        {
            txtSegment_0_Size.Enabled = true;
            txtSegment_1_Size.Enabled = true;
            txtSegment_2_Size.Enabled = true;
            txtSegment_3_Size.Enabled = true;
            txtSegment_4_Size.Enabled = true;
            txtSegment_5_Size.Enabled = true;
            txtSegment_6_Size.Enabled = true;
            txtSegment_7_Size.Enabled = false;
            txtSegment_8_Size.Enabled = false;
            txtSegment_9_Size.Enabled = false;
            txtCOA_Format.Enabled = true;
        }
        else if (txtNoOfCOASeg.Text == "8")
        {
            txtSegment_0_Size.Enabled = true;
            txtSegment_1_Size.Enabled = true;
            txtSegment_2_Size.Enabled = true;
            txtSegment_3_Size.Enabled = true;
            txtSegment_4_Size.Enabled = true;
            txtSegment_5_Size.Enabled = true;
            txtSegment_6_Size.Enabled = true;
            txtSegment_7_Size.Enabled = true;
            txtSegment_8_Size.Enabled = false;
            txtSegment_9_Size.Enabled = false;
            txtCOA_Format.Enabled = true;
        }
        else if (txtNoOfCOASeg.Text == "9")
        {
            txtSegment_0_Size.Enabled = true;
            txtSegment_1_Size.Enabled = true;
            txtSegment_2_Size.Enabled = true;
            txtSegment_3_Size.Enabled = true;
            txtSegment_4_Size.Enabled = true;
            txtSegment_5_Size.Enabled = true;
            txtSegment_6_Size.Enabled = true;
            txtSegment_7_Size.Enabled = true;
            txtSegment_8_Size.Enabled = true;
            txtSegment_9_Size.Enabled = false;
            txtCOA_Format.Enabled = true;
        }
        else if (txtNoOfCOASeg.Text == "10")
        {
            txtSegment_0_Size.Enabled = true;
            txtSegment_1_Size.Enabled = true;
            txtSegment_2_Size.Enabled = true;
            txtSegment_3_Size.Enabled = true;
            txtSegment_4_Size.Enabled = true;
            txtSegment_5_Size.Enabled = true;
            txtSegment_6_Size.Enabled = true;
            txtSegment_7_Size.Enabled = true;
            txtSegment_8_Size.Enabled = true;
            txtSegment_9_Size.Enabled = true;
            txtCOA_Format.Enabled = true;
        }


        MaskedEditExtender1.Mask = "0";
    }


    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        btnUpdate.Visible = false;
        btnInsert.Visible = false;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        Label1.Text = "";


        fieldbox.Visible = true;
        Findbox.Visible = false;
        textClear();
        txtSegment_0_Size.Enabled = false;
        txtSegment_1_Size.Enabled = false;
        txtSegment_2_Size.Enabled = false;
        txtSegment_3_Size.Enabled = false;
        txtSegment_4_Size.Enabled = false;
        txtSegment_5_Size.Enabled = false;
        txtSegment_6_Size.Enabled = false;
        txtSegment_7_Size.Enabled = false;
        txtSegment_8_Size.Enabled = false;
        txtSegment_9_Size.Enabled = false;
        try {
            

            string serial_id;
            serial_id = GridView1.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select * from SET_Company where CompId = '" + ViewState["SNO"].ToString() + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {


               
                ddlNatureOFBusiness.ClearSelection();
                ddlCity.ClearSelection();
                ddlCountry.ClearSelection();
                ddlMainCurrency.ClearSelection();
                ddlMainBusiness.ClearSelection();
                ddlDescriptionMethod.ClearSelection();
                ddlGOCName.ClearSelection();

                txtCompanyName.Text = ds.Tables[0].Rows[0]["CompName"].ToString();
                ddlNatureOFBusiness.Items.FindByValue(ds.Tables[0].Rows[0]["BusinessNatureId"].ToString()).Selected = true;
                txtAddress1.Text = ds.Tables[0].Rows[0]["AddressLine1"].ToString();
                txtAddress2.Text = ds.Tables[0].Rows[0]["AddressLine2"].ToString();
                txtAddress3.Text = ds.Tables[0].Rows[0]["AddressLine3"].ToString();
                ddlCity.Items.FindByValue(ds.Tables[0].Rows[0]["CityID"].ToString()).Selected = true;
                txtZipCode.Text = ds.Tables[0].Rows[0]["ZipCode"].ToString();
                ddlCountry.Items.FindByValue(ds.Tables[0].Rows[0]["CountryID"].ToString()).Selected = true;
                txtCellNo.Text = ds.Tables[0].Rows[0]["CellNumber"].ToString();
                txtPhoneNo.Text = ds.Tables[0].Rows[0]["PhoneNumber"].ToString();
                txtFaxNumber.Text = ds.Tables[0].Rows[0]["FaxNumber"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["EmailAddress"].ToString();
                txtIncorporationDate.Text = ds.Tables[0].Rows[0]["DateOfIncorporation"].ToString();
                txtCOntactPerson.Text = ds.Tables[0].Rows[0]["ContactPerson"].ToString();
                txtNTNNo.Text = ds.Tables[0].Rows[0]["NTNNumber"].ToString();
                txtGSTNo.Text = ds.Tables[0].Rows[0]["SalesTaxNumber"].ToString();
                ddlMainCurrency.Items.FindByValue(ds.Tables[0].Rows[0]["BaseCurrencyID"].ToString()).Selected = true ;
                //txtUSeMultiCurrency.Text = ds.Tables[0].Rows[0]["MultiCurrency"].ToString();
                bool rdbUMC = bool.Parse(ds.Tables[0].Rows[0]["MultiCurrency"].ToString());
                ddlMainBusiness.Items.FindByText(ds.Tables[0].Rows[0]["MainProduct_Services"].ToString()).Selected = true;
                //ddlDescriptionMethod.Items.FindByValue(ds.Tables[0].Rows[0]["DepreciationMethod"].ToString()).Selected = true;
                string rdb = ds.Tables[0].Rows[0]["UseCompanyCOA"].ToString();
                txtBy_Product.Text = ds.Tables[0].Rows[0]["Byproduct"].ToString();
                txtNoOfCOASeg.Text = ds.Tables[0].Rows[0]["Noof_COA_Segments"].ToString();
                txtCOA_Format.Text = ds.Tables[0].Rows[0]["CoaFormat"].ToString();

                ddlGOCName.Items.FindByValue(ds.Tables[0].Rows[0]["GOCId"].ToString()).Selected = true;

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());

                txtSegment_0_Size.Text = ds.Tables[0].Rows[0]["Segment_0_Size"].ToString();
                txtSegment_1_Size.Text = ds.Tables[0].Rows[0]["Segment_1_Size"].ToString();
                txtSegment_2_Size.Text = ds.Tables[0].Rows[0]["Segment_2_Size"].ToString();
                txtSegment_3_Size.Text = ds.Tables[0].Rows[0]["Segment_3_Size"].ToString();
                txtSegment_4_Size.Text = ds.Tables[0].Rows[0]["Segment_4_Size"].ToString();
                txtSegment_5_Size.Text = ds.Tables[0].Rows[0]["Segment_5_Size"].ToString();
                txtSegment_6_Size.Text = ds.Tables[0].Rows[0]["Segment_6_Size"].ToString();
                txtSegment_7_Size.Text = ds.Tables[0].Rows[0]["Segment_7_Size"].ToString();
                txtSegment_8_Size.Text = ds.Tables[0].Rows[0]["Segment_8_Size"].ToString();
                txtSegment_9_Size.Text = ds.Tables[0].Rows[0]["Segment_9_Size"].ToString();

                txtUserName_loginName.Text = ds.Tables[0].Rows[0]["LoginName"].ToString();
                txtEntry_Date.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtSystem_Date.Text = ds.Tables[0].Rows[0]["SysDate"].ToString();
                txtAccountSepartor.Text = ds.Tables[0].Rows[0]["COA_Separater"].ToString();
               
                
                txtInventoryMethod.Text = ds.Tables[0].Rows[0]["InventoryMethod"].ToString();
                txtUserName_Entry.Text = ds.Tables[0].Rows[0]["EntryUserId"].ToString();
                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (rdbUMC == true)
                {
                    rdb_UMC_Y.Checked = true;
                    rdb_UMC_N.Checked = false;
                }
                else
                {
                    rdb_UMC_Y.Checked = false;
                    rdb_UMC_N.Checked = true;
                }

                dml.dateConvert(txtIncorporationDate);
                dml.dateConvert(txtEntry_Date);
                DataSet dsGrid = dml.Find("select USER_NAME, LoginName from SET_USer_Manager where Userid = '" + txtUserName_Entry.Text + "'");
                txtUserName_Entry.Text = dsGrid.Tables[0].Rows[0]["USER_NAME"].ToString();




                if (rdb == "Y")
                {

                    rdbCOmpanyCOA_YES.Checked = true;
                    rdbCOmpanyCOA_NO.Checked = false;

                    rdbCOmpanyCOA_YES_CheckedChanged(sender, e);
                    MaskedEditExtender1.Mask = txtCOA_Format.Text;
                    txtCOA_Format.Enabled = false;
                    txtSegment_0_Size.Enabled = false;
                    txtSegment_1_Size.Enabled = false;
                    txtSegment_2_Size.Enabled = false;
                    txtSegment_3_Size.Enabled = false;
                    txtSegment_4_Size.Enabled = false;
                    txtSegment_5_Size.Enabled = false;
                    txtSegment_6_Size.Enabled = false;
                    txtSegment_7_Size.Enabled = false;
                    txtSegment_8_Size.Enabled = false;
                    txtSegment_9_Size.Enabled = false;
                    txtNoOfCOASeg.Enabled = false;

                }
                else
                {
                    rdbCOmpanyCOA_YES.Checked = false;
                    rdbCOmpanyCOA_NO.Checked = true;
                     rdbCOmpanyCOA_NO_CheckedChanged(sender, e);
                    txtCOA_Format.Enabled = false;
                    txtSegment_0_Size.Enabled = false;
                    txtSegment_1_Size.Enabled = false;
                    txtSegment_2_Size.Enabled = false;
                    txtSegment_3_Size.Enabled = false;
                    txtSegment_4_Size.Enabled = false;
                    txtSegment_5_Size.Enabled = false;
                    txtSegment_6_Size.Enabled = false;
                    txtSegment_7_Size.Enabled = false;
                    txtSegment_8_Size.Enabled = false;
                    txtSegment_9_Size.Enabled = false;
                    txtNoOfCOASeg.Enabled = false;
                    
                }

              
              

                txtSegment_0_Size.Enabled = false;
                txtSegment_1_Size.Enabled = false;
                txtSegment_2_Size.Enabled = false;
                txtSegment_3_Size.Enabled = false;
                txtSegment_4_Size.Enabled = false;
                txtSegment_5_Size.Enabled = false;
                txtSegment_6_Size.Enabled = false;
                txtSegment_7_Size.Enabled = false;
                txtSegment_8_Size.Enabled = false;
                txtSegment_9_Size.Enabled = false;
                
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
        DeleteBox.Visible = false;
        try
        {


            string serial_id;
            serial_id = GridView2.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select * from SET_Company where CompId = '" + ViewState["SNO"].ToString() + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {


                //    ([CompId],  [MainProduct_Services], 
                //[enable_Branch_Accounting],
                // [], [UserId], [], [GUID], [], [], [IsActive],
                // [LogoImage], [Record_Deleted]) 

                ddlNatureOFBusiness.ClearSelection();
                ddlCity.ClearSelection();
                ddlCountry.ClearSelection();
                ddlMainCurrency.ClearSelection();
                ddlMainBusiness.ClearSelection();
                ddlDescriptionMethod.ClearSelection();
                ddlGOCName.ClearSelection();

                txtCompanyName.Text = ds.Tables[0].Rows[0]["CompName"].ToString();
                ddlNatureOFBusiness.Items.FindByValue(ds.Tables[0].Rows[0]["BusinessNatureId"].ToString()).Selected = true;
                txtAddress1.Text = ds.Tables[0].Rows[0]["AddressLine1"].ToString();
                txtAddress2.Text = ds.Tables[0].Rows[0]["AddressLine2"].ToString();
                txtAddress3.Text = ds.Tables[0].Rows[0]["AddressLine3"].ToString();
                ddlCity.Items.FindByValue(ds.Tables[0].Rows[0]["CityID"].ToString()).Selected = true;
                txtZipCode.Text = ds.Tables[0].Rows[0]["ZipCode"].ToString();
                ddlCountry.Items.FindByValue(ds.Tables[0].Rows[0]["CountryID"].ToString()).Selected = true;
                txtCellNo.Text = ds.Tables[0].Rows[0]["CellNumber"].ToString();
                txtPhoneNo.Text = ds.Tables[0].Rows[0]["PhoneNumber"].ToString();
                txtFaxNumber.Text = ds.Tables[0].Rows[0]["FaxNumber"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["EmailAddress"].ToString();
                txtIncorporationDate.Text = ds.Tables[0].Rows[0]["DateOfIncorporation"].ToString();
                txtCOntactPerson.Text = ds.Tables[0].Rows[0]["ContactPerson"].ToString();
                txtNTNNo.Text = ds.Tables[0].Rows[0]["NTNNumber"].ToString();
                txtGSTNo.Text = ds.Tables[0].Rows[0]["SalesTaxNumber"].ToString();
                ddlMainCurrency.Items.FindByValue(ds.Tables[0].Rows[0]["BaseCurrencyID"].ToString()).Selected = true;
                //txtUSeMultiCurrency.Text = ds.Tables[0].Rows[0]["MultiCurrency"].ToString();
                bool rdbUMC = bool.Parse(ds.Tables[0].Rows[0]["MultiCurrency"].ToString());
                ddlMainBusiness.Items.FindByText(ds.Tables[0].Rows[0]["MainProduct_Services"].ToString()).Selected = true;
                //ddlDescriptionMethod.Items.FindByValue(ds.Tables[0].Rows[0]["DepreciationMethod"].ToString()).Selected = true;
                string rdb = ds.Tables[0].Rows[0]["UseCompanyCOA"].ToString();
                txtBy_Product.Text = ds.Tables[0].Rows[0]["Byproduct"].ToString();
                txtNoOfCOASeg.Text = ds.Tables[0].Rows[0]["Noof_COA_Segments"].ToString();
                txtCOA_Format.Text = ds.Tables[0].Rows[0]["CoaFormat"].ToString();

                ddlGOCName.Items.FindByValue(ds.Tables[0].Rows[0]["GOCId"].ToString()).Selected = true;

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());

                txtSegment_0_Size.Text = ds.Tables[0].Rows[0]["Segment_0_Size"].ToString();
                txtSegment_1_Size.Text = ds.Tables[0].Rows[0]["Segment_1_Size"].ToString();
                txtSegment_2_Size.Text = ds.Tables[0].Rows[0]["Segment_2_Size"].ToString();
                txtSegment_3_Size.Text = ds.Tables[0].Rows[0]["Segment_3_Size"].ToString();
                txtSegment_4_Size.Text = ds.Tables[0].Rows[0]["Segment_4_Size"].ToString();
                txtSegment_5_Size.Text = ds.Tables[0].Rows[0]["Segment_5_Size"].ToString();
                txtSegment_6_Size.Text = ds.Tables[0].Rows[0]["Segment_6_Size"].ToString();
                txtSegment_7_Size.Text = ds.Tables[0].Rows[0]["Segment_7_Size"].ToString();
                txtSegment_8_Size.Text = ds.Tables[0].Rows[0]["Segment_8_Size"].ToString();
                txtSegment_9_Size.Text = ds.Tables[0].Rows[0]["Segment_9_Size"].ToString();

                txtUserName_loginName.Text = ds.Tables[0].Rows[0]["LoginName"].ToString();
                txtEntry_Date.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtSystem_Date.Text = ds.Tables[0].Rows[0]["SysDate"].ToString();
                txtAccountSepartor.Text = ds.Tables[0].Rows[0]["COA_Separater"].ToString();


                txtInventoryMethod.Text = ds.Tables[0].Rows[0]["InventoryMethod"].ToString();
                txtUserName_Entry.Text = ds.Tables[0].Rows[0]["EntryUserId"].ToString();
                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (rdbUMC == true)
                {
                    rdb_UMC_Y.Checked = true;
                    rdb_UMC_N.Checked = false;
                }
                else
                {
                    rdb_UMC_Y.Checked = false;
                    rdb_UMC_N.Checked = true;
                }

                dml.dateConvert(txtIncorporationDate);
                dml.dateConvert(txtEntry_Date);
                DataSet dsGrid = dml.Find("select USER_NAME, LoginName from SET_USer_Manager where Userid = '" + txtUserName_Entry.Text + "'");
                txtUserName_Entry.Text = dsGrid.Tables[0].Rows[0]["USER_NAME"].ToString();




                if (rdb == "Y")
                {

                    rdbCOmpanyCOA_YES.Checked = true;
                    rdbCOmpanyCOA_NO.Checked = false;

                    rdbCOmpanyCOA_YES_CheckedChanged(sender, e);
                    MaskedEditExtender1.Mask = txtCOA_Format.Text;
                    txtCOA_Format.Enabled = false;
                    txtSegment_0_Size.Enabled = false;
                    txtSegment_1_Size.Enabled = false;
                    txtSegment_2_Size.Enabled = false;
                    txtSegment_3_Size.Enabled = false;
                    txtSegment_4_Size.Enabled = false;
                    txtSegment_5_Size.Enabled = false;
                    txtSegment_6_Size.Enabled = false;
                    txtSegment_7_Size.Enabled = false;
                    txtSegment_8_Size.Enabled = false;
                    txtSegment_9_Size.Enabled = false;
                    txtNoOfCOASeg.Enabled = false;

                }
                else
                {
                    rdbCOmpanyCOA_NO_CheckedChanged(sender, e);
                    txtCOA_Format.Enabled = false;
                    txtSegment_0_Size.Enabled = false;
                    txtSegment_1_Size.Enabled = false;
                    txtSegment_2_Size.Enabled = false;
                    txtSegment_3_Size.Enabled = false;
                    txtSegment_4_Size.Enabled = false;
                    txtSegment_5_Size.Enabled = false;
                    txtSegment_6_Size.Enabled = false;
                    txtSegment_7_Size.Enabled = false;
                    txtSegment_8_Size.Enabled = false;
                    txtSegment_9_Size.Enabled = false;
                    txtNoOfCOASeg.Enabled = false;
                    rdbCOmpanyCOA_YES.Checked = false;
                    rdbCOmpanyCOA_NO.Checked = true;
                }

                
            }




        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }

    }
    protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        txtCompanyName.Enabled = true;
        ddlNatureOFBusiness.Enabled = true;
        txtAddress1.Enabled = true;
        txtPhoneNo.Enabled = true;
        txtFaxNumber.Enabled = true;
        txtEmail.Enabled = true;
        txtCOntactPerson.Enabled = true;
        txtNTNNo.Enabled = true;
        ddlMainCurrency.Enabled = true;
        rdb_UMC_Y.Enabled = true;
        rdb_UMC_N.Enabled = true;
        ddlMainBusiness.Enabled = true;
        txtBy_Product.Enabled = true;
        txtSegment_7_Size.Enabled = true;
        txtSegment_8_Size.Enabled = true;
        txtSegment_9_Size.Enabled = true;
        lblSerialNo.Enabled = true;
        txtCOA_Format.Enabled = true;
        txtUserName_Entry.Enabled = true;
        ddlCity.Enabled = true;
        ddlCountry.Enabled = true;
        chkActive.Checked = true;
        ddlGOCName.Enabled = true;
        txtAddress2.Enabled = true;
        txtAddress3.Enabled = true;
        txtZipCode.Enabled = true;
        txtCellNo.Enabled = true;
        txtGSTNo.Enabled = true;
        ddlDescriptionMethod.Enabled = true;
        txtSegment_0_Size.Enabled = true;
        txtSegment_1_Size.Enabled = true;
        txtSegment_2_Size.Enabled = true;
        txtSegment_3_Size.Enabled = true;
        txtSegment_4_Size.Enabled = true;
        txtSegment_5_Size.Enabled = true;
        txtSegment_6_Size.Enabled = true;
        txtUserName_loginName.Enabled = true;
        txtEntry_Date.Enabled = true;
        txtSystem_Date.Enabled = true;
        txtAccountSepartor.Enabled = true;
        txtIncorporationDate.Enabled = true;
        imgPopup.Enabled = true;
        ImageButton1.Enabled = true;
        // txtEmployeeLeftDate.Enabled = true;

        rdbCOmpanyCOA_NO.Enabled = true;
        rdbCOmpanyCOA_YES.Enabled = true;
        txtInventoryMethod.Enabled = true;

        btnUpdate.Visible = true;
        btnInsert.Visible = false;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        Label1.Text = "";


        fieldbox.Visible = true;
        Editbox.Visible = false;
        try
        {

            string serial_id;
            serial_id = GridView3.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select * from SET_Company where CompId = '" + ViewState["SNO"].ToString() + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {


                //    ([CompId],  [], 
                //[enable_Branch_Accounting],
                // [], [UserId], [], [GUID], [], [], [IsActive],
                // [LogoImage], [Record_Deleted]) 

                ddlNatureOFBusiness.ClearSelection();
                ddlCity.ClearSelection();
                ddlCountry.ClearSelection();
                ddlMainCurrency.ClearSelection();
                ddlMainBusiness.ClearSelection();
                ddlDescriptionMethod.ClearSelection();
                ddlGOCName.ClearSelection();

                txtCompanyName.Text = ds.Tables[0].Rows[0]["CompName"].ToString();
                string str = ds.Tables[0].Rows[0]["BusinessNatureId"].ToString();
                ddlNatureOFBusiness.Items.FindByValue(ds.Tables[0].Rows[0]["BusinessNatureId"].ToString()).Selected = true;
                txtAddress1.Text = ds.Tables[0].Rows[0]["AddressLine1"].ToString();
                txtAddress2.Text = ds.Tables[0].Rows[0]["AddressLine2"].ToString();
                txtAddress3.Text = ds.Tables[0].Rows[0]["AddressLine3"].ToString();
                ddlCity.Items.FindByValue(ds.Tables[0].Rows[0]["CityID"].ToString()).Selected = true;
                txtZipCode.Text = ds.Tables[0].Rows[0]["ZipCode"].ToString();
                ddlCountry.Items.FindByValue(ds.Tables[0].Rows[0]["CountryID"].ToString()).Selected = true;
                txtCellNo.Text = ds.Tables[0].Rows[0]["CellNumber"].ToString();
                txtPhoneNo.Text = ds.Tables[0].Rows[0]["PhoneNumber"].ToString();
                txtFaxNumber.Text = ds.Tables[0].Rows[0]["FaxNumber"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["EmailAddress"].ToString();
                txtIncorporationDate.Text = ds.Tables[0].Rows[0]["DateOfIncorporation"].ToString();
                txtCOntactPerson.Text = ds.Tables[0].Rows[0]["ContactPerson"].ToString();
                txtNTNNo.Text = ds.Tables[0].Rows[0]["NTNNumber"].ToString();
                txtGSTNo.Text = ds.Tables[0].Rows[0]["SalesTaxNumber"].ToString();
                ddlMainCurrency.Items.FindByValue(ds.Tables[0].Rows[0]["BaseCurrencyID"].ToString()).Selected = true;
                //txtUSeMultiCurrency.Text = ds.Tables[0].Rows[0]["MultiCurrency"].ToString();
                bool rdbUMC = bool.Parse(ds.Tables[0].Rows[0]["MultiCurrency"].ToString());
                 ddlMainBusiness.Items.FindByText(ds.Tables[0].Rows[0]["MainProduct_Services"].ToString()).Selected = true;
                // ddlDescriptionMethod.Items.FindByValue(ds.Tables[0].Rows[0]["DepreciationMethod"].ToString()).Selected = true;
                string rdb = ds.Tables[0].Rows[0]["UseCompanyCOA"].ToString();
                txtBy_Product.Text = ds.Tables[0].Rows[0]["Byproduct"].ToString();
                txtNoOfCOASeg.Text = ds.Tables[0].Rows[0]["Noof_COA_Segments"].ToString();
                txtCOA_Format.Text = ds.Tables[0].Rows[0]["CoaFormat"].ToString();

                ddlGOCName.Items.FindByValue(ds.Tables[0].Rows[0]["GOCId"].ToString()).Selected = true;

                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());

                txtSegment_0_Size.Text = ds.Tables[0].Rows[0]["Segment_0_Size"].ToString();
                txtSegment_1_Size.Text = ds.Tables[0].Rows[0]["Segment_1_Size"].ToString();
                txtSegment_2_Size.Text = ds.Tables[0].Rows[0]["Segment_2_Size"].ToString();
                txtSegment_3_Size.Text = ds.Tables[0].Rows[0]["Segment_3_Size"].ToString();
                txtSegment_4_Size.Text = ds.Tables[0].Rows[0]["Segment_4_Size"].ToString();
                txtSegment_5_Size.Text = ds.Tables[0].Rows[0]["Segment_5_Size"].ToString();
                txtSegment_6_Size.Text = ds.Tables[0].Rows[0]["Segment_6_Size"].ToString();
                txtSegment_7_Size.Text = ds.Tables[0].Rows[0]["Segment_7_Size"].ToString();
                txtSegment_8_Size.Text = ds.Tables[0].Rows[0]["Segment_8_Size"].ToString();
                txtSegment_9_Size.Text = ds.Tables[0].Rows[0]["Segment_9_Size"].ToString();

                txtUserName_loginName.Text = ds.Tables[0].Rows[0]["LoginName"].ToString();
                txtEntry_Date.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                txtSystem_Date.Text = ds.Tables[0].Rows[0]["SysDate"].ToString();
                txtAccountSepartor.Text = ds.Tables[0].Rows[0]["COA_Separater"].ToString();


                txtInventoryMethod.Text = ds.Tables[0].Rows[0]["InventoryMethod"].ToString();
                txtUserName_Entry.Text = ds.Tables[0].Rows[0]["EntryUserId"].ToString();
                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (rdbUMC == true)
                {
                    rdb_UMC_Y.Checked = true;
                    rdb_UMC_N.Checked = false;
                }
                else
                {
                    rdb_UMC_Y.Checked = false;
                    rdb_UMC_N.Checked = true;
                }
                dml.dateConvert(txtIncorporationDate);
                dml.dateConvert(txtEntry_Date);
                DataSet dsGrid = dml.Find("select USER_NAME, LoginName from SET_USer_Manager where Userid = '" + txtUserName_Entry.Text + "'");
                txtUserName_Entry.Text = dsGrid.Tables[0].Rows[0]["USER_NAME"].ToString();



                if (rdb == "Y")
                {

                    rdbCOmpanyCOA_YES.Checked = true;
                    rdbCOmpanyCOA_NO.Checked = false;

                    rdbCOmpanyCOA_YES_CheckedChanged(sender, e);
                    MaskedEditExtender1.Mask = txtCOA_Format.Text;
                    txtCOA_Format.Enabled = false;
                    txtSegment_0_Size.Enabled = false;
                    txtSegment_1_Size.Enabled = false;
                    txtSegment_2_Size.Enabled = false;
                    txtSegment_3_Size.Enabled = false;
                    txtSegment_4_Size.Enabled = false;
                    txtSegment_5_Size.Enabled = false;
                    txtSegment_6_Size.Enabled = false;
                    txtSegment_7_Size.Enabled = false;
                    txtSegment_8_Size.Enabled = false;
                    txtSegment_9_Size.Enabled = false;
                    txtNoOfCOASeg.Enabled = false;

                }
                else
                {
                    rdbCOmpanyCOA_NO_CheckedChanged(sender, e);
                    txtCOA_Format.Enabled = false;
                    txtSegment_0_Size.Enabled = false;
                    txtSegment_1_Size.Enabled = false;
                    txtSegment_2_Size.Enabled = false;
                    txtSegment_3_Size.Enabled = false;
                    txtSegment_4_Size.Enabled = false;
                    txtSegment_5_Size.Enabled = false;
                    txtSegment_6_Size.Enabled = false;
                    txtSegment_7_Size.Enabled = false;
                    txtSegment_8_Size.Enabled = false;
                    txtSegment_9_Size.Enabled = false;
                    txtNoOfCOASeg.Enabled = false;
                    rdbCOmpanyCOA_YES.Checked = false;
                    rdbCOmpanyCOA_NO.Checked = true;
                }









            }






        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }



    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            dml.Delete("update SET_Company set Record_Deleted = 1 where CompId = " + ViewState["SNO"].ToString() + "", "");
            textClear();
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "alertDelete()", true);

            btnInsert.Visible = true;
            btnDelete.Visible = true;
            btnUpdate.Visible = false;
            btnEdit.Visible = true;
            btnFind.Visible = true;
            btnSave.Visible = false;
            btnDelete_after.Visible = false;

        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }


    protected void ddlGOCName_SelectedIndexChanged(object sender, EventArgs e)
    {
        rdbCOmpanyCOA_NO_CheckedChanged(sender, e);
    }



    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string id = e.Row.Cells[1].Text;
        bool flag  = dml.isNumber(id);


        if (flag == true)
        {

            DataSet ds = dml.Find("select CompId,MLD from SET_Company where CompId = '" + id + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string value = ds.Tables[0].Rows[i]["MLD"].ToString();
                    if (dml.Decrypt(value) == "q")
                    {
                        e.Row.Enabled = false;
                        e.Row.Cells[0].ToolTip = "Cannot be deletable, work in find mode only";
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

            DataSet ds = dml.Find("select CompId,MLD from SET_Company where CompId = '" + id + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string value = ds.Tables[0].Rows[i]["MLD"].ToString();
                    if (dml.Decrypt(value) == "q")
                    {
                        e.Row.Enabled = false;
                        e.Row.Cells[0].ToolTip = "Cannot be editable, work in find mode only";
                        e.Row.BackColor = System.Drawing.Color.LightGray;
                    }

                }
            }
        }
    }

    public int gocid()
    {
        string GocId = Request.Cookies["GocId"].Value;
        return Convert.ToInt32(GocId);
    }
    public int compid()
    {
        string CompId = Request.Cookies["CompId"].Value;
        return Convert.ToInt32(CompId);
    }    
    
}