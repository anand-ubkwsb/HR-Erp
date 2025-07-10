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
    DmlOperation dml = new DmlOperation();
    FieldHide fd = new FieldHide();
    TextBox S_userid, S_LoginName, S_Displayarea;
    TextBox txtuser;
    int DateFrom, AddDays, EditDays, DeleteDays;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
    DataSet ds = new DataSet();
    string userid, UserGrpID, FormID,fiscal;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            CalendarExtender1.EndDate = DateTime.Now;
            btnUpdate.Visible = false;
            btnSave.Visible = false;
            btnDelete_after.Visible = false;
            userid = Request.QueryString["UserID"];
            UserGrpID = Request.QueryString["UsergrpID"];
            FormID = Request.QueryString["FormID"];
            fiscal = Request.QueryString["fiscaly"];
            ViewState["FormId"] = FormID;
            ViewState["userid"] = userid;
          //  fd.HideUSerGrpField(Page.Controls, UserGrpID, FormID, "SET_Department");
            dml.dropdownsql(ddlCountry, "SET_Country", "CountryName", "CountryID");
            dml.dropdownsql(ddlCity, "SET_City", "CityName", "CityID");
            //modal
            dml.daterangeforfiscal(CalendarExtender1, Request.Cookies["fiscalyear"].Value);
            txtNextCreditReview.Attributes.Add("readonly", "readonly");


            dml.dropdownsql(ddlFindBusinessPartnerName, "SET_BusinessPartner", "BPartnerName", "BPartnerId", "BPartnerName");
            dml.dropdownsql(ddlDel_BusinessPartnerName, "SET_BusinessPartner", "BPartnerName", "BPartnerId", "BPartnerName");
            dml.dropdownsql(ddlEdit_BusinessPartnerName, "SET_BusinessPartner", "BPartnerName", "BPartnerId", "BPartnerName");
            Showall_Dml();
            textClear();
            Findbox.Visible = false;
            DeleteBox.Visible = false;
            Editbox.Visible = false;
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

        txtCreatedDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

        userid = Request.QueryString["UserID"];
        DataSet ds_autoUserName = dml.Find("select user_name from SET_User_Manager where UserId ='" + userid + "'");
        txtCreatedBy.Text = ds_autoUserName.Tables[0].Rows[0]["user_name"].ToString();
     
       
        GridView4.Enabled = true;
        lblGOC.Enabled = true;
        txtB_PartnerName.Enabled = true;
        txtAdd1.Enabled = true;
        txtAdd2.Enabled = true;
        txtAdd3.Enabled = true;
        txtAdd4.Enabled = true;
        imgPopup.Enabled = true;
        txtArea.Enabled = true;
        txtDistrict.Enabled = true;
        txtProvince.Enabled = true;
        txtZipCode.Enabled = true;
        txtPhoneNo.Enabled = true;
        txtContactPerName.Enabled = true;
        txtContactNo.Enabled = true;
        txtEmailAdd.Enabled = true;
        txtCNIC.Enabled = true;
        txtNTNno.Enabled = true;
        txtSaleTaxRegNo.Enabled = true;
        txtContactPerson.Enabled = true;
        txtCreditAnalyst.Enabled = true;
        txtCreditRating.Enabled = true;
        txtNextCreditReview.Enabled = true;
        txtTolerancePercent.Enabled = true;
        txtInterestPeriodDays.Enabled = true;
        txtPaymentGraceD.Enabled = true;
        txtDiscountGraceDay.Enabled = true;
        txtClearingDays.Enabled = true;
        txtExemptedAmount.Enabled = true;
        txtFixedDiscount.Enabled = true;
        txtPreferredpaymentType.Enabled = true;
        txtRemarks.Enabled = true;
        txtOSCode.Enabled = true;
        txtSecuritydeposit.Enabled = true;
        txtSpecialDeal.Enabled = true;
        txtCreatedDate.Enabled = false;
        txtCreatedBy.Enabled = false;
        txtUpdatedDate.Enabled = false;
        txtUpdateBy.Enabled = false;
        ddlCountry.Enabled = true;
        ddlCity.Enabled = false;


        chkFiler.Enabled = true;
        chkRegister.Enabled = true;
        chkCreditChecking.Enabled = true;
        chkInterestCharge.Enabled = true;
        chkTaxExempted.Enabled = true;
        chkAdditionalTax.Enabled = true;
        chkIsStopped.Enabled = true;
        chkActive.Enabled = true;

        chkFiler.Checked= true;
        chkRegister.Checked= true;
        chkCreditChecking.Checked= true;
        chkInterestCharge.Checked= true;
        chkTaxExempted.Checked= true;
        chkAdditionalTax.Checked= true;
        chkIsStopped.Checked= true;
        chkActive.Checked= true;

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        userid = Request.QueryString["UserID"];
        UserGrpID = Request.QueryString["UsergrpID"];
        FormID = Request.QueryString["FormID"];
        fiscal = Request.QueryString["fiscaly"];
        try
        {
            int Filer, Register, CreditChecking, InterestCharge, TaxExempted, AdditionalTax, IsStopped, Active, gstSer_Pro;
            string stnext;
            if(txtNextCreditReview.Text == "")
            {
                stnext = "NULL";
            }
            else
            {
                stnext = txtNextCreditReview.Text;
            }


            if (chkActive.Checked == true)
            {
                Active = 1;
            }
            else
            {
                Active = 0;
            }

            if (chkFiler.Checked == true)
            {
                Filer = 1;
            }
            else
            {
                Filer = 0;
            }

            if (chkRegister.Checked == true)
            {
                Register = 1;
            }
            else
            {
                Register = 0;
            }

            if (chkCreditChecking.Checked == true)
            {
                CreditChecking = 1;
            }
            else
            {
                CreditChecking = 0;
            }
            if (chkInterestCharge.Checked == true)
            {
                InterestCharge = 1;
            }
            else
            {
                InterestCharge = 0;
            }

          
            if (chkTaxExempted.Checked == true)
            {
                TaxExempted = 1;
            }
            else
            {
                TaxExempted = 0;
            }
            if (chkAdditionalTax.Checked == true)
            {
                AdditionalTax = 1;
            }
            else
            {
                AdditionalTax = 0;
            }
            if (chkIsStopped.Checked == true)
            {
                IsStopped = 1;
            }
            else
            {
                IsStopped = 0;
            }
           
            if (chkGstSER_Pro.Checked == true)
            {
                gstSer_Pro = 1;
            }
            else
            {
                gstSer_Pro = 0;
            }
            if (txtCNIC.Text.Trim() != "")
            {
                dml.Insert("INSERT INTO [SET_BusinessPartner] ([GocID],[BPartnerName], [AddressLine1],[AddressLine2], [AddressLine3], [AddressLine4], [AreaID], [ZipCode], [Districtid], [Provinceid], [CityID], [CountryID], [PhoneNumber], [ContactPersonName], [ContactNumber],[EmailAddress], [CNICNo], [NTNNo], [IsFiler], [IsRegistered], [SalesTaxRegNo], [ContactPerson],[CreditChecking], [CreditAnalyst], [CreditRating],[NextCreditReviewDate], [TolerancePercent],[InterestCharges], [InterestPeriodDays],[PaymentGraceDays], [DiscountGraceDays],[ClearingDays], [TaxExempted], [ExemptedAmount], [AdditionalDeductionTaxExempted],[IsStopPayment], [FixedDiscount], [PreferredPaymentType], [Remarks], [OSCode], [SecurityDeposite], [SpecialDeal], [IsActive], [CreateDate], [CreatedBy],[Record_Deleted],[GstServiceProvider])VALUES ('1', '" + txtB_PartnerName.Text + "', '" + txtAdd1.Text + "', '" + txtAdd2.Text + "', '" + txtAdd3.Text + "', '" + txtAdd4.Text + "', " + txtArea.Text + ", '" + txtZipCode.Text + "', " + txtDistrict.Text + ", '" + txtProvince.Text + "', " + ddlCity.SelectedItem.Value + ", " + ddlCountry.SelectedItem.Value + ", '" + txtPhoneNo.Text + "', '" + txtContactPerName.Text + "', '" + txtContactNo.Text + "','" + txtEmailAdd.Text + "', '" + txtCNIC.Text + "', '" + txtNTNno.Text + "', '" + Filer + "', '" + Register + "', '" + txtSaleTaxRegNo.Text + "', '" + txtContactPerson.Text + "', '" + CreditChecking + "','" + txtCreditAnalyst.Text + "', '" + txtCreditRating.Text + "', '" + dml.dateconvertforinsert(txtNextCreditReview) + "', '" + txtTolerancePercent.Text + "', '" + InterestCharge + "', '" + txtInterestPeriodDays.Text + "', '" + txtPaymentGraceD.Text + "', '" + txtDiscountGraceDay.Text + "', " + txtClearingDays.Text + ", '" + TaxExempted + "', '" + txtExemptedAmount.Text + "', '" + AdditionalTax + "', '" + IsStopped + "','" + txtFixedDiscount.Text + "', '" + txtPreferredpaymentType.Text + "', '" + txtRemarks.Text + "', '" + txtOSCode.Text + "', '" + txtSecuritydeposit.Text + "', '" + txtSpecialDeal.Text + "', '" + Active + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffff") + "', '" + txtCreatedBy.Text + "','0','"+ gstSer_Pro + "');", "alertme()");
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);
                FormID = Request.QueryString["FormID"];
                Showall_Dml();
            }
            else
            {
                Label1.Text = "Please Input CNIC No";
            }
            if (txtCNIC.Text != "")
            {
                DataSet ds = dml.Find("select BPartnerId,CNICNo from SET_BusinessPartner where CNICNo = '" + txtCNIC.Text + "'");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["BPID"] = ds.Tables[0].Rows[0]["BPartnerId"].ToString();
                }
            }
            Label1.Text = "";
            checkboxselect(ViewState["BPID"].ToString());
            FormID = Request.QueryString["FormID"];
            Showall_Dml();


        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        userid = Request.QueryString["UserID"];
        UserGrpID = Request.QueryString["UsergrpID"];
        FormID = Request.QueryString["FormID"];
        fiscal = Request.QueryString["fiscaly"];
        txtUpdateBy.Text = show_username();
        try
        {
            int Filer, Register, CreditChecking, InterestCharge, TaxExempted, AdditionalTax, IsStopped, Active, gstSer_Pro;

            if (chkActive.Checked == true)
            {
                Active = 1;
            }
            else
            {
                Active = 0;
            }

            if (chkFiler.Checked == true)
            {
                Filer = 1;
            }
            else
            {
                Filer = 0;
            }

            if (chkRegister.Checked == true)
            {
                Register = 1;
            }
            else
            {
                Register = 0;
            }

            if (chkCreditChecking.Checked == true)
            {
                CreditChecking = 1;
            }
            else
            {
                CreditChecking = 0;
            }
            if (chkInterestCharge.Checked == true)
            {
                InterestCharge = 1;
            }
            else
            {
                InterestCharge = 0;
            }


            if (chkTaxExempted.Checked == true)
            {
                TaxExempted = 1;
            }
            else
            {
                TaxExempted = 0;
            }
            if (chkAdditionalTax.Checked == true)
            {
                AdditionalTax = 1;
            }
            else
            {
                AdditionalTax = 0;
            }
            if (chkIsStopped.Checked == true)
            {
                IsStopped = 1;
            }
            else
            {
                IsStopped = 0;
            }
            if (chkGstSER_Pro.Checked == true)
            {
                gstSer_Pro = 1;
            }
            else
            {
                gstSer_Pro = 0;
            }

            string str_area, str_zipcode, str_PHNO, str_CpersonNo, str_ConNo, str_email, str_salereg, str_contper, str_creditrating, str_invesperiod, str_discountGdays, str_clearday, str_fixeddisc, str_preffPayment, str_remarks, str_OScode, str_SecDeposte, str_Specialdeal;
            if (txtArea.Text == "")
            {
                str_area = "([AreaID] IS NULL)";
            }
            else
            {
                str_area = "([AreaID] = '" + txtArea.Text + "')";
            }
            if (txtPhoneNo.Text == "")
            {
                str_PHNO = "([PhoneNumber] IS NULL)";
            }
            else
            {
                str_PHNO = "([PhoneNumber] = '" + txtPhoneNo.Text + "')";
            }

            if (txtContactPerName.Text == "")
            {
                str_CpersonNo = "([ContactPersonName] IS NULL)";
            }
            else
            {
                str_CpersonNo = "([ContactPersonName] = '" + txtContactPerName.Text + "')";
            }

            if (txtContactNo.Text == "")
            {
                str_ConNo = "([ContactNumber] IS NULL)";
            }
            else
            {
                str_ConNo = "([ContactNumber] = '" + txtContactNo.Text + "')";
            }

            if (txtEmailAdd.Text == "")
            {
                str_email = "([EmailAddress] IS NULL)";
            }
            else
            {
                str_email = "([EmailAddress] = '" + txtEmailAdd.Text + "')";
            }

            if (txtSaleTaxRegNo.Text == "")
            {
                str_salereg = "([SalesTaxRegNo] IS NULL)";
            }
            else
            {
                str_salereg = "([SalesTaxRegNo] = '" + txtSaleTaxRegNo.Text + "')";
            }


            if (txtContactPerson.Text == "")
            {
                str_contper = "([ContactPerson] IS NULL)";
            }
            else
            {
                str_contper = "([ContactPerson] = '" + txtContactPerson.Text + "')";
            }

            if (txtCreditRating.Text == "")
            {
               str_creditrating = "([CreditRating] IS NULL)";
            }
            else
            {
                str_creditrating = "([CreditRating] = '" + txtCreditRating.Text + "')";
            }


            if (txtInterestPeriodDays.Text == "")
            {
                str_invesperiod = "([InterestPeriodDays] IS NULL)";
            }
            else
            {
                str_invesperiod = "([InterestPeriodDays] = '" + txtInterestPeriodDays.Text + "')";
            }

            if (txtDiscountGraceDay.Text == "")
            {
                str_discountGdays = "([DiscountGraceDays] IS NULL)";
            }
            else
            {
                str_discountGdays = "([DiscountGraceDays] = '" + txtDiscountGraceDay.Text + "')";
            }

            if (txtClearingDays.Text == "")
            {
                str_clearday = "([ClearingDays] IS NULL)";
            }
            else
            {
                str_clearday = "([ClearingDays] = '" + txtClearingDays.Text + "')";
            }

            if (txtFixedDiscount.Text == "")
            {
                str_fixeddisc = "([FixedDiscount] IS NULL)";
            }
            else
            {
                str_fixeddisc = "([FixedDiscount] = '" + txtFixedDiscount.Text + "')";
            }


            if (txtPreferredpaymentType.Text == "")
            {
                str_preffPayment = "([PreferredPaymentType] IS NULL)";
            }
            else
            {
                str_preffPayment = "([PreferredPaymentType] = '" + txtPreferredpaymentType.Text + "')";
            }

            if (txtRemarks.Text == "")
            {
                str_remarks = "([Remarks] IS NULL)";
            }
            else
            {
                str_remarks = "([Remarks] = '" + txtRemarks.Text + "')";
            }


            if (txtOSCode.Text == "")
            {
                str_OScode = "([OSCode] IS NULL)";
            }
            else
            {
                str_OScode = "([OSCode] = '" + txtOSCode.Text + "')";
            }

            if (txtSecuritydeposit.Text == "")
            {
                str_SecDeposte = "([SecurityDeposite] IS NULL)";
            }
            else
            {
                str_SecDeposte = "([SecurityDeposite] = '" + txtSecuritydeposit.Text + "')";
            }

            if (txtSpecialDeal.Text == "")
            {
                str_Specialdeal = "([SpecialDeal] IS NULL)";
            }
            else
            {
                str_Specialdeal = "([SpecialDeal] = '" + txtSpecialDeal.Text + "')";
            }

            if (txtZipCode.Text == "")
            {
                str_zipcode = "([ZipCode] IS NULL)";
            }
            else
            {
                str_zipcode = "([ZipCode] = '" + txtZipCode.Text + "')";
            }
            string str_Paymengrace,str_creditAnaylst;
            if (txtPaymentGraceD.Text == "")
            {
                str_Paymengrace = "([PaymentGraceDays] IS NULL)";
            }
            else
            {
                str_Paymengrace = "([PaymentGraceDays] = '" + txtPaymentGraceD.Text + "')";
            }
            if (txtPaymentGraceD.Text == "")
            {
                str_creditAnaylst = "([CreditAnalyst] IS NULL)";
            }
            else
            {
                str_creditAnaylst = "([CreditAnalyst] = '" + txtPaymentGraceD.Text + "')";
            }
            string city_str = ddlCity.SelectedItem.Value;
            string country_str = ddlCountry.SelectedItem.Value;

            DataSet ds_up = dml.Find("select * from SET_BusinessPartner WHERE ([BPartnerId] ='" + ViewState["BPid"].ToString() + "')  AND ([BPartnerName]='" + txtB_PartnerName.Text+"') AND ([AddressLine1]='"+txtAdd1.Text+ "') AND ([AddressLine2]='" +txtAdd2.Text + "') AND ([AddressLine3]='" +txtAdd3.Text + "') AND ([AddressLine4]='" +txtAdd4.Text + "') AND "+str_area+" AND "+str_zipcode+" AND ([Districtid]='"+txtDistrict.Text+"') AND ([Provinceid]='"+txtProvince.Text+"') AND ([CityID]='"+city_str+"') AND ([CountryID]='"+country_str+"') AND "+str_PHNO+" AND "+str_contper+" AND "+str_ConNo+" AND "+str_email+" AND ([CNICNo]='"+txtCNIC.Text+"') AND ([NTNNo]='"+txtNTNno.Text+"') AND ([IsFiler]='"+Filer+"') AND ([IsRegistered]='"+Register+"') AND "+str_salereg+" AND "+str_contper+" AND ([CreditChecking]='"+CreditChecking+"') AND "+ str_creditAnaylst + " AND "+str_creditrating+" AND ([TolerancePercent]='"+txtTolerancePercent.Text+"') AND ([InterestCharges]='"+InterestCharge+"') AND "+str_invesperiod+" AND "+ str_Paymengrace + " AND "+str_discountGdays+" AND "+str_clearday+" AND ([TaxExempted]='"+TaxExempted+"') AND ([ExemptedAmount]='"+txtExemptedAmount.Text+"') AND ([AdditionalDeductionTaxExempted]='"+AdditionalTax+"') AND ([IsStopPayment]='"+IsStopped+"') AND "+str_fixeddisc+" AND "+str_preffPayment+" AND "+str_remarks+" AND "+str_OScode+" AND "+str_SecDeposte+" AND "+str_Specialdeal+" AND ([IsActive]='"+Active+"') AND ([Record_Deleted]='0');");

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
                FormID = Request.QueryString["FormID"];
                Showall_Dml();
            }
            else {


                dml.Update("UPDATE [SET_BusinessPartner] SET [BPartnerName]='" + txtB_PartnerName.Text + "', [AddressLine1]='" + txtAdd1.Text + "', [AddressLine2]='" + txtAdd2.Text + "', [AddressLine3]='" + txtAdd3.Text + "', [AddressLine4]='" + txtAdd4.Text + "', [AreaID]='" + txtArea.Text + "', [ZipCode]='" + txtZipCode.Text + "', [Districtid]='" + txtDistrict.Text + "', [Provinceid]='" + txtProvince.Text + "', [CityID]='" + ddlCity.SelectedItem.Value + "', [CountryID]='" + ddlCountry.SelectedItem.Value + "', [PhoneNumber]='" + txtPhoneNo.Text + "', [ContactPersonName]='" + txtContactPerName.Text + "', [ContactNumber]='" + txtContactNo.Text + "', [EmailAddress]='" + txtEmailAdd.Text + "', [CNICNo]='" + txtCNIC.Text + "', [NTNNo]='" + txtNTNno.Text + "', [IsFiler]='" + Filer + "', [IsRegistered]='" + Register + "', [GstServiceProvider]='" + gstSer_Pro + "', [SalesTaxRegNo]='" + txtSaleTaxRegNo.Text + "', [ContactPerson]='" + txtContactPerson.Text + "', [CreditChecking]='" + CreditChecking + "', [CreditAnalyst]='" + txtCreditAnalyst.Text + "', [CreditRating]='" + txtCreditRating.Text + "', [NextCreditReviewDate]='" + dml.dateconvertforinsert(txtNextCreditReview) + "', [TolerancePercent]='" + txtTolerancePercent.Text + "', [InterestCharges]='" + InterestCharge + "', [InterestPeriodDays]='" + txtInterestPeriodDays.Text + "', [PaymentGraceDays]='" + txtPaymentGraceD.Text + "', [DiscountGraceDays]='" + txtDiscountGraceDay.Text + "', [ClearingDays]='" + txtClearingDays.Text + "', [TaxExempted]='" + TaxExempted + "', [ExemptedAmount]='" + txtExemptedAmount.Text + "', [AdditionalDeductionTaxExempted]='" + AdditionalTax + "', [IsStopPayment]='" + IsStopped + "', [FixedDiscount]='" + txtFixedDiscount.Text + "', [PreferredPaymentType]='" + txtPreferredpaymentType.Text + "', [Remarks]='" + txtRemarks.Text + "', [OSCode]='" + txtOSCode.Text + "', [SecurityDeposite]='" + txtSecuritydeposit.Text + "', [SpecialDeal]='" + txtSpecialDeal.Text + "', [IsActive]='" + Active + "', [UpdateDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', [UpdatedBy]='" + txtUpdateBy.Text + "' where BPartnerId ='" + ViewState["BPid"].ToString() + "'", "");

                Updatecheckboxselect(ViewState["BPid"].ToString());
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", " Editalert()", true);
                textClear();
                Clearcheckboxselect();
                btnInsert.Visible = true;
                btnDelete.Visible = true;
                btnUpdate.Visible = false;
                btnEdit.Visible = true;
                btnFind.Visible = true;
                btnSave.Visible = false;
                btnDelete_after.Visible = false;
                FormID = Request.QueryString["FormID"];
                Showall_Dml();
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        textClear();
        Clearcheckboxselect();
        btnInsert.Visible = true;
        btnDelete.Visible = true;
        btnUpdate.Visible = false;
        btnEdit.Visible = true;
        btnFind.Visible = true;
        btnSave.Visible = false;
     
        btnDelete_after.Visible = false;
        Label1.Text = "";
        DeleteBox.Visible = false;
        Editbox.Visible = false;
        Findbox.Visible = false;
        fieldbox.Visible = true;
        FormID = Request.QueryString["FormID"];
        Showall_Dml();
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
            GridView2.DataBind();
            string swhere;
            string squer = "select * from SET_BusinessPartner";

            if (ddlDel_BusinessPartnerName.SelectedIndex != 0)
            {
                swhere = "BPartnerName like '" + ddlDel_BusinessPartnerName.SelectedItem.Text + "%'";
            }
            else
            {
                swhere = "BPartnerName is not null";
            }
           if (txtDelete_CNIC.Text != "")
            {
                swhere = swhere + " and CNICNo like '" + txtDelete_CNIC.Text + "%'";
            }
            else
            {
                swhere = swhere + " and CNICNo is not null";
            }
            if (txtDelete_NTN.Text != "")
            {
                swhere = swhere + " and NTNNo like '" + txtDelete_NTN.Text + "%'";
            }
            else
            {
                swhere = swhere + " and NTNNo is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY BPartnerName";

            Findbox.Visible = false;
            fieldbox.Visible = false;
            Editbox.Visible = false;

            DataSet dgrid = dml.grid(squer);
            GridView2.DataSource = dgrid;
            GridView2.DataBind();
        }
        catch (Exception ex)
        {

        }
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        textClear();


        btnCancel.Visible = true;
        btnFind.Visible = true;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnInsert.Visible = false;
        try
        {
            GridView1.DataBind();
            string swhere;
            string squer = "select * from SET_BusinessPartner";

            if (ddlFindBusinessPartnerName.SelectedIndex != 0)
            {
                swhere = "BPartnerName like '" + ddlFindBusinessPartnerName.SelectedItem.Text + "%'";
            }
            else
            {
                swhere = "BPartnerName is not null";
            }
           
            if (txtFind_CNIC.Text != "")
            {
                swhere = swhere + " and CNICNo like '" + txtFind_CNIC.Text + "%'";
            }
            else
            {
                swhere = swhere + " and CNICNo is not null";
            }
            if (txtFind_NTNNo.Text != "")
            {
                swhere = swhere + " and NTNNo like '" + txtFind_NTNNo.Text + "%'";
            }
            else
            {
                swhere = swhere + " and NTNNo is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY BPartnerName";

            Findbox.Visible = true;
            fieldbox.Visible = false;

            DataSet dgrid = dml.grid(squer);
            GridView1.DataSource = dgrid;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
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
            GridView3.DataBind();
            string swhere;
            string squer = "select * from SET_BusinessPartner";

            if (ddlEdit_BusinessPartnerName.SelectedIndex != 0)
            {
                swhere = "BPartnerName like '" + ddlEdit_BusinessPartnerName.SelectedItem.Text + "%'";
            }
            else
            {
                swhere = "BPartnerName is not null";
            }
           
            if (txtEdit_CNIC.Text != "")
            {
                swhere = swhere + " and CNICNo like '" + txtEdit_CNIC.Text + "%'";
            }
            else
            {
                swhere = swhere + " and CNICNo is not null";
            }
            if (txtEdit_NtNno.Text != "")
            {
                swhere = swhere + " and NTNNo like '" + txtEdit_NtNno.Text + "%'";
            }
            else
            {
                swhere = swhere + " and NTNNo is not null";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY BPartnerName";
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
                ClientScript.RegisterStartupScript(this.GetType(), "randomtext", " nodatafound()", true);
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", " wrong()", true);
        }
    }
    public void textClear()
    {
       
        lblGOC.Text = "";
        txtB_PartnerName.Text = "";
        txtAdd1.Text = "";
        txtAdd2.Text = "";
        txtAdd3.Text = "";
        txtAdd4.Text = "";
        txtArea.Text = "";
        txtDistrict.Text = "";
        txtProvince.Text = "";
        txtZipCode.Text = "";
        txtPhoneNo.Text = "";
        txtContactPerName.Text = "";
        txtContactNo.Text = "";
        txtEmailAdd.Text = "";
        txtCNIC.Text = "";
        txtNTNno.Text = "";
        txtSaleTaxRegNo.Text = "";
        txtContactPerson.Text = "";
        txtCreditAnalyst.Text = "";
        txtCreditRating.Text = "";
        txtNextCreditReview.Text = "";
        txtTolerancePercent.Text = "";
        txtInterestPeriodDays.Text = "";
        txtPaymentGraceD.Text = "";
        txtDiscountGraceDay.Text = "";
        txtClearingDays.Text = "";
        txtExemptedAmount.Text = "";
        txtFixedDiscount.Text = "";
        txtPreferredpaymentType.Text = "";
        txtRemarks.Text = "";
        txtOSCode.Text = "";
        txtSecuritydeposit.Text = "";
        txtSpecialDeal.Text = "";
        txtCreatedDate.Text = "";
        txtCreatedBy.Text = "";
        txtUpdatedDate.Text = "";
        txtUpdateBy.Text = "";

        chkFiler.Checked = false;
        chkRegister.Checked = false;
        chkCreditChecking.Checked = false;
        chkInterestCharge.Checked = false;
        chkTaxExempted.Checked = false;
        chkAdditionalTax.Checked = false;
        chkIsStopped.Checked = false;
        chkActive.Checked = false;

        dml.dropdownsql(ddlCountry, "SET_Country", "CountryName", "CountryID");
        dml.dropdownsql(ddlCity, "SET_City", "CityName", "CityID");
        //modal

        dml.dropdownsql(ddlFindBusinessPartnerName, "SET_BusinessPartner", "BPartnerName", "BPartnerId", "BPartnerName");
        dml.dropdownsql(ddlDel_BusinessPartnerName, "SET_BusinessPartner", "BPartnerName", "BPartnerId", "BPartnerName");
        dml.dropdownsql(ddlEdit_BusinessPartnerName, "SET_BusinessPartner", "BPartnerName", "BPartnerId", "BPartnerName");

        ddlCountry.SelectedIndex = 0;
        ddlCity.SelectedIndex = 0;


        //Enable fields
        GridView4.Enabled = false;
        lblGOC.Enabled = false;
        txtB_PartnerName.Enabled = false;
        txtAdd1.Enabled = false;
        txtAdd2.Enabled = false;
        txtAdd3.Enabled = false;
        txtAdd4.Enabled = false;
        txtArea.Enabled = false;
        txtDistrict.Enabled = false;
        txtProvince.Enabled = false;
        txtZipCode.Enabled = false;
        txtPhoneNo.Enabled = false;
        txtContactPerName.Enabled = false;
        txtContactNo.Enabled = false;
        txtEmailAdd.Enabled = false;
        txtCNIC.Enabled = false;
        txtNTNno.Enabled = false;
        txtSaleTaxRegNo.Enabled = false;
        txtContactPerson.Enabled = false;
        txtCreditAnalyst.Enabled = false;
        txtCreditRating.Enabled = false;
        txtNextCreditReview.Enabled = false;
        txtTolerancePercent.Enabled = false;
        txtInterestPeriodDays.Enabled = false;
        txtPaymentGraceD.Enabled = false;
        txtDiscountGraceDay.Enabled = false;
        txtClearingDays.Enabled = false;
        txtExemptedAmount.Enabled = false;
        txtFixedDiscount.Enabled = false;
        txtPreferredpaymentType.Enabled = false;
        txtRemarks.Enabled = false;
        txtOSCode.Enabled = false;
        txtSecuritydeposit.Enabled = false;
        txtSpecialDeal.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtCreatedBy.Enabled = false;
        txtUpdatedDate.Enabled = false;
        txtUpdateBy.Enabled = false;
        ddlCountry.Enabled = false;
        ddlCity.Enabled = false;
        imgPopup.Enabled = false;

        chkFiler.Enabled = false;
        chkRegister.Enabled = false;
        chkCreditChecking.Enabled = false;
        chkInterestCharge.Enabled = false;
        chkTaxExempted.Enabled = false;
        chkAdditionalTax.Enabled = false;
        chkIsStopped.Enabled = false;
        chkActive.Enabled = false;
    }
    public void Showall_Dml()
    {
        userid = Request.QueryString["UserID"];
        FormID = Request.QueryString["FormID"];
        DataSet FiscalStatus;
        con.Open();
        string Query = "SELECT * FROM SET_FISCAL_YEAR WHERE FISCALYEARID="+Convert.ToInt32(Request.Cookies["FiscalYearId"].Value);
        DataSet Fiscal = dml.Find(Query);
        string FiscalStatusQuery = "SELECT * FROM SET_FISCAL_YEAR_STATUS WHERE FISCALYEARSTATUSID=(SELECT FISCALYEARSTATUSID FROM SET_FISCAL_YEAR WHERE FISCALYEARID=" +Convert.ToInt32(Request.Cookies["FiscalYearId"].Value)+ ") AND RECORD_DELETED='0'";
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
        //    if (ds.Tables[0].Rows[0]["Find"].ToString() == "N")
        //    {
        //        btnFind.Visible = false;
        //    }
        //}
        //else
        //{
        //    btnInsert.Visible = false;
        //    btnEdit.Visible = false;
        //    btnDelete.Visible = false;
        //    btnFind.Visible = true;
        //    btnCancel.Visible = true;
        //}

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            dml.Delete("update SET_BusinessPartner set Record_Deleted = 1 where BPartnerId = " + ViewState["BPid"].ToString() + "", "");
            textClear();
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext()", "alertDelete()", true);

            btnInsert.Visible = true;
            btnDelete.Visible = true;
            btnUpdate.Visible = false;
            btnEdit.Visible = true;
            btnFind.Visible = true;
            btnSave.Visible = false;
            btnDelete_after.Visible = false;
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
        btnInsert.Visible = false;
        btnCancel.Visible = true;
        btnUpdate.Visible = false;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnFind.Visible = true;

        textClear();

        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;


        string BP_Id;
        try
        {
            BP_Id = GridView1.SelectedRow.Cells[1].Text;

            DataSet ds = dml.Find("select * from SET_BusinessPartner where BPartnerId = '" + BP_Id + "'  and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {

                lblGOC.Text = ds.Tables[0].Rows[0]["BPartnerId"].ToString();
                txtB_PartnerName.Text = ds.Tables[0].Rows[0]["BPartnerName"].ToString();
                txtAdd1.Text = ds.Tables[0].Rows[0]["AddressLine1"].ToString();
                txtAdd2.Text = ds.Tables[0].Rows[0]["AddressLine2"].ToString();
                txtAdd3.Text = ds.Tables[0].Rows[0]["AddressLine3"].ToString();
                txtAdd4.Text = ds.Tables[0].Rows[0]["AddressLine4"].ToString();
                txtArea.Text = ds.Tables[0].Rows[0]["AreaID"].ToString();
                txtZipCode.Text = ds.Tables[0].Rows[0]["ZipCode"].ToString();
                txtDistrict.Text = ds.Tables[0].Rows[0]["Districtid"].ToString();
                txtProvince.Text = ds.Tables[0].Rows[0]["Provinceid"].ToString();
                txtPhoneNo.Text = ds.Tables[0].Rows[0]["PhoneNumber"].ToString();
                txtContactPerName.Text = ds.Tables[0].Rows[0]["ContactPersonName"].ToString();
                txtContactNo.Text = ds.Tables[0].Rows[0]["ContactNumber"].ToString();
                txtEmailAdd.Text = ds.Tables[0].Rows[0]["EmailAddress"].ToString();
                txtCNIC.Text = ds.Tables[0].Rows[0]["CNICNo"].ToString();
                txtNTNno.Text = ds.Tables[0].Rows[0]["NTNNo"].ToString();

                txtSaleTaxRegNo.Text = ds.Tables[0].Rows[0]["SalesTaxRegNo"].ToString();
                txtContactPerson.Text = ds.Tables[0].Rows[0]["ContactPerson"].ToString();
                txtCreditAnalyst.Text = ds.Tables[0].Rows[0]["CreditAnalyst"].ToString();
                txtCreditRating.Text = ds.Tables[0].Rows[0]["CreditRating"].ToString();
                txtNextCreditReview.Text = ds.Tables[0].Rows[0]["NextCreditReviewDate"].ToString();
                txtTolerancePercent.Text = ds.Tables[0].Rows[0]["TolerancePercent"].ToString();
                txtInterestPeriodDays.Text = ds.Tables[0].Rows[0]["InterestPeriodDays"].ToString();
               
                txtPaymentGraceD.Text = ds.Tables[0].Rows[0]["PaymentGraceDays"].ToString();
                txtDiscountGraceDay.Text = ds.Tables[0].Rows[0]["DiscountGraceDays"].ToString();
                txtClearingDays.Text = ds.Tables[0].Rows[0]["ClearingDays"].ToString();
                txtExemptedAmount.Text = ds.Tables[0].Rows[0]["ExemptedAmount"].ToString();
                txtFixedDiscount.Text = ds.Tables[0].Rows[0]["FixedDiscount"].ToString();
                txtPreferredpaymentType.Text = ds.Tables[0].Rows[0]["PreferredPaymentType"].ToString();
             
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                txtOSCode.Text = ds.Tables[0].Rows[0]["OSCode"].ToString();
                txtSecuritydeposit.Text = ds.Tables[0].Rows[0]["SecurityDeposite"].ToString();
                txtSpecialDeal.Text = ds.Tables[0].Rows[0]["SpecialDeal"].ToString();
                txtCreatedDate.Text = ds.Tables[0].Rows[0]["CreateDate"].ToString();
                txtCreatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdateBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());
                txtUpdatedDate.Text = ds.Tables[0].Rows[0]["UpdateDate"].ToString();
                


                ddlCity.ClearSelection();
                ddlCountry.ClearSelection();
                ddlCity.Items.FindByValue(ds.Tables[0].Rows[0]["CityID"].ToString()).Selected = true;
                ddlCountry.Items.FindByValue(ds.Tables[0].Rows[0]["CountryID"].ToString()).Selected = true;
                dml.dateConvert(txtNextCreditReview);
                //  DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["SysDate"].ToString());

                bool filer = bool.Parse(ds.Tables[0].Rows[0]["IsFiler"].ToString());
                bool register = bool.Parse(ds.Tables[0].Rows[0]["IsRegistered"].ToString());
                bool CreditCheck = bool.Parse(ds.Tables[0].Rows[0]["CreditChecking"].ToString());
                bool InterestCharge = bool.Parse(ds.Tables[0].Rows[0]["InterestCharges"].ToString());
                bool TaxExempted = bool.Parse(ds.Tables[0].Rows[0]["TaxExempted"].ToString());
                bool Additional = bool.Parse(ds.Tables[0].Rows[0]["AdditionalDeductionTaxExempted"].ToString());
                bool StopPayment = bool.Parse(ds.Tables[0].Rows[0]["IsStopPayment"].ToString());
                bool Active = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                bool gstser = bool.Parse(ds.Tables[0].Rows[0]["GstServiceProvider"].ToString());

                if (gstser == true)
                {
                    chkGstSER_Pro.Checked = true;
                }
                else
                {
                    chkGstSER_Pro.Checked = false;
                }

                if (filer == true)
                {
                    chkFiler.Checked = true;
                }
                else
                {
                    chkFiler.Checked = false;
                }

                if (register == true)
                {
                    chkRegister.Checked = true;
                }
                else
                {
                    chkRegister.Checked = false;
                }

                if (CreditCheck == true)
                {
                    chkCreditChecking.Checked = true;
                }
                else
                {
                    chkCreditChecking.Checked = false;
                }

                if (InterestCharge == true)
                {
                    chkInterestCharge.Checked = true;
                }
                else
                {
                    chkInterestCharge.Checked = false;
                }

                if (TaxExempted == true)
                {
                    chkTaxExempted.Checked = true;
                }
                else
                {
                    chkTaxExempted.Checked = false;
                }

                if (Additional == true)
                {
                    chkAdditionalTax.Checked = true;
                }
                else
                {
                    chkAdditionalTax.Checked = false;
                }

                if (StopPayment == true)
                {
                    chkIsStopped.Checked = true;
                }
                else
                {
                    chkIsStopped.Checked = false;
                }

                if (Active == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }

            }
            Findcheckboxselect(BP_Id);
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
        chkActive.Enabled = false;
        textClear();

        fieldbox.Visible = true;
      
        Findbox.Visible = false;
        Editbox.Visible = false;
        DeleteBox.Visible = false;

        string BP_Id;
        try
        {
            BP_Id = GridView2.SelectedRow.Cells[1].Text;
            ViewState["BPid"] = BP_Id;
            DataSet ds = dml.Find("select * from SET_BusinessPartner where BPartnerId = '" + BP_Id + "'  and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblGOC.Text = ds.Tables[0].Rows[0]["BPartnerId"].ToString();
                txtB_PartnerName.Text = ds.Tables[0].Rows[0]["BPartnerName"].ToString();
                txtAdd1.Text = ds.Tables[0].Rows[0]["AddressLine1"].ToString();
                txtAdd2.Text = ds.Tables[0].Rows[0]["AddressLine2"].ToString();
                txtAdd3.Text = ds.Tables[0].Rows[0]["AddressLine3"].ToString();
                txtAdd4.Text = ds.Tables[0].Rows[0]["AddressLine4"].ToString();
                txtArea.Text = ds.Tables[0].Rows[0]["AreaID"].ToString();
                txtZipCode.Text = ds.Tables[0].Rows[0]["ZipCode"].ToString();
                txtDistrict.Text = ds.Tables[0].Rows[0]["Districtid"].ToString();
                txtProvince.Text = ds.Tables[0].Rows[0]["Provinceid"].ToString();
                txtPhoneNo.Text = ds.Tables[0].Rows[0]["PhoneNumber"].ToString();
                txtContactPerName.Text = ds.Tables[0].Rows[0]["ContactPersonName"].ToString();
                txtContactNo.Text = ds.Tables[0].Rows[0]["ContactNumber"].ToString();
                txtEmailAdd.Text = ds.Tables[0].Rows[0]["EmailAddress"].ToString();
                txtCNIC.Text = ds.Tables[0].Rows[0]["CNICNo"].ToString();
                txtNTNno.Text = ds.Tables[0].Rows[0]["NTNNo"].ToString();

                txtSaleTaxRegNo.Text = ds.Tables[0].Rows[0]["SalesTaxRegNo"].ToString();
                txtContactPerson.Text = ds.Tables[0].Rows[0]["ContactPerson"].ToString();
                txtCreditAnalyst.Text = ds.Tables[0].Rows[0]["CreditAnalyst"].ToString();
                txtCreditRating.Text = ds.Tables[0].Rows[0]["CreditRating"].ToString();
                txtNextCreditReview.Text = ds.Tables[0].Rows[0]["NextCreditReviewDate"].ToString();
                txtTolerancePercent.Text = ds.Tables[0].Rows[0]["TolerancePercent"].ToString();
                txtInterestPeriodDays.Text = ds.Tables[0].Rows[0]["InterestPeriodDays"].ToString();

                txtPaymentGraceD.Text = ds.Tables[0].Rows[0]["PaymentGraceDays"].ToString();
                txtDiscountGraceDay.Text = ds.Tables[0].Rows[0]["DiscountGraceDays"].ToString();
                txtClearingDays.Text = ds.Tables[0].Rows[0]["ClearingDays"].ToString();
                txtExemptedAmount.Text = ds.Tables[0].Rows[0]["ExemptedAmount"].ToString();
                txtFixedDiscount.Text = ds.Tables[0].Rows[0]["FixedDiscount"].ToString();
                txtPreferredpaymentType.Text = ds.Tables[0].Rows[0]["PreferredPaymentType"].ToString();

                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                txtOSCode.Text = ds.Tables[0].Rows[0]["OSCode"].ToString();
                txtSecuritydeposit.Text = ds.Tables[0].Rows[0]["SecurityDeposite"].ToString();
                txtSpecialDeal.Text = ds.Tables[0].Rows[0]["SpecialDeal"].ToString();
                txtCreatedDate.Text = ds.Tables[0].Rows[0]["CreateDate"].ToString();
                txtCreatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdateBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());
                txtUpdatedDate.Text = ds.Tables[0].Rows[0]["UpdateDate"].ToString();
               

                ddlCity.ClearSelection();
                ddlCountry.ClearSelection();
                ddlCity.Items.FindByValue(ds.Tables[0].Rows[0]["CityID"].ToString()).Selected = true;
                ddlCountry.Items.FindByValue(ds.Tables[0].Rows[0]["CountryID"].ToString()).Selected = true;
                dml.dateConvert(txtNextCreditReview);
                //  DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["SysDate"].ToString());

                bool filer = bool.Parse(ds.Tables[0].Rows[0]["IsFiler"].ToString());
                bool register = bool.Parse(ds.Tables[0].Rows[0]["IsRegistered"].ToString());
                bool CreditCheck = bool.Parse(ds.Tables[0].Rows[0]["CreditChecking"].ToString());
                bool InterestCharge = bool.Parse(ds.Tables[0].Rows[0]["InterestCharges"].ToString());
                bool TaxExempted = bool.Parse(ds.Tables[0].Rows[0]["TaxExempted"].ToString());
                bool Additional = bool.Parse(ds.Tables[0].Rows[0]["AdditionalDeductionTaxExempted"].ToString());
                bool StopPayment = bool.Parse(ds.Tables[0].Rows[0]["IsStopPayment"].ToString());
                bool Active = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                bool gstser = bool.Parse(ds.Tables[0].Rows[0]["GstServiceProvider"].ToString());

                if (gstser == true)
                {
                    chkGstSER_Pro.Checked = true;
                }
                else
                {
                    chkGstSER_Pro.Checked = false;
                }

                if (filer == true)
                {
                    chkFiler.Checked = true;
                }
                else
                {
                    chkFiler.Checked = false;
                }

                if (register == true)
                {
                    chkRegister.Checked = true;
                }
                else
                {
                    chkRegister.Checked = false;
                }

                if (CreditCheck == true)
                {
                    chkCreditChecking.Checked = true;
                }
                else
                {
                    chkCreditChecking.Checked = false;
                }

                if (InterestCharge == true)
                {
                    chkInterestCharge.Checked = true;
                }
                else
                {
                    chkInterestCharge.Checked = false;
                }

                if (TaxExempted == true)
                {
                    chkTaxExempted.Checked = true;
                }
                else
                {
                    chkTaxExempted.Checked = false;
                }

                if (Additional == true)
                {
                    chkAdditionalTax.Checked = true;
                }
                else
                {
                    chkAdditionalTax.Checked = false;
                }

                if (StopPayment == true)
                {
                    chkIsStopped.Checked = true;
                }
                else
                {
                    chkIsStopped.Checked = false;
                }

                if (Active == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }

            }
            Findcheckboxselect(BP_Id);
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }

    }
    protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
    {
        textClear();
       
        btnInsert.Visible = false;
        btnCancel.Visible = true;
        btnUpdate.Visible = true;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        btnFind.Visible = false;


        GridView4.Enabled = true;
        lblGOC.Enabled = true;
        txtB_PartnerName.Enabled = true;
        txtAdd1.Enabled = true;
        txtAdd2.Enabled = true;
        txtAdd3.Enabled = true;
        txtAdd4.Enabled = true;
        txtArea.Enabled = true;
        txtDistrict.Enabled = true;
        txtProvince.Enabled = true;
        txtZipCode.Enabled = true;
        txtPhoneNo.Enabled = true;
        txtContactPerName.Enabled = true;
        txtContactNo.Enabled = true;
        txtEmailAdd.Enabled = true;
        txtCNIC.Enabled = true;
        txtNTNno.Enabled = true;
        txtSaleTaxRegNo.Enabled = true;
        txtContactPerson.Enabled = true;
        txtCreditAnalyst.Enabled = true;
        txtCreditRating.Enabled = true;
        txtNextCreditReview.Enabled = true;
        txtTolerancePercent.Enabled = true;
        txtInterestPeriodDays.Enabled = true;
        txtPaymentGraceD.Enabled = true;
        txtDiscountGraceDay.Enabled = true;
        txtClearingDays.Enabled = true;
        txtExemptedAmount.Enabled = true;
        txtFixedDiscount.Enabled = true;
        txtPreferredpaymentType.Enabled = true;
        txtRemarks.Enabled = true;
        txtOSCode.Enabled = true;
        txtSecuritydeposit.Enabled = true;
        txtSpecialDeal.Enabled = true;
        txtCreatedDate.Enabled = false;
        txtCreatedBy.Enabled = false;
        txtUpdatedDate.Enabled = false;
        txtUpdateBy.Enabled = false;
        ddlCountry.Enabled = true;
        ddlCity.Enabled = true;
        imgPopup.Enabled = true;

        chkFiler.Enabled = true;
        chkRegister.Enabled = true;
        chkCreditChecking.Enabled = true;
        chkInterestCharge.Enabled = true;
        chkTaxExempted.Enabled = true;
        chkAdditionalTax.Enabled = true;
        chkIsStopped.Enabled = true;
        chkActive.Enabled = true;



        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;

        GridView4.DataBind();
        string BP_Id;
        try
        {
            BP_Id = GridView3.SelectedRow.Cells[1].Text;
            ViewState["BPid"] = BP_Id;

            DataSet ds = dml.Find("select * from SET_BusinessPartner where BPartnerId = '" + BP_Id + "'  and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblGOC.Text = ds.Tables[0].Rows[0]["BPartnerId"].ToString();
                txtB_PartnerName.Text = ds.Tables[0].Rows[0]["BPartnerName"].ToString();
                txtAdd1.Text = ds.Tables[0].Rows[0]["AddressLine1"].ToString();
                txtAdd2.Text = ds.Tables[0].Rows[0]["AddressLine2"].ToString();
                txtAdd3.Text = ds.Tables[0].Rows[0]["AddressLine3"].ToString();
                txtAdd4.Text = ds.Tables[0].Rows[0]["AddressLine4"].ToString();
                txtArea.Text = ds.Tables[0].Rows[0]["AreaID"].ToString();
                txtZipCode.Text = ds.Tables[0].Rows[0]["ZipCode"].ToString();
                txtDistrict.Text = ds.Tables[0].Rows[0]["Districtid"].ToString();
                txtProvince.Text = ds.Tables[0].Rows[0]["Provinceid"].ToString();
                txtPhoneNo.Text = ds.Tables[0].Rows[0]["PhoneNumber"].ToString();
                txtContactPerName.Text = ds.Tables[0].Rows[0]["ContactPersonName"].ToString();
                txtContactNo.Text = ds.Tables[0].Rows[0]["ContactNumber"].ToString();
                txtEmailAdd.Text = ds.Tables[0].Rows[0]["EmailAddress"].ToString();
                txtCNIC.Text = ds.Tables[0].Rows[0]["CNICNo"].ToString();
                txtNTNno.Text = ds.Tables[0].Rows[0]["NTNNo"].ToString();

                txtSaleTaxRegNo.Text = ds.Tables[0].Rows[0]["SalesTaxRegNo"].ToString();
                txtContactPerson.Text = ds.Tables[0].Rows[0]["ContactPerson"].ToString();
                txtCreditAnalyst.Text = ds.Tables[0].Rows[0]["CreditAnalyst"].ToString();
                txtCreditRating.Text = ds.Tables[0].Rows[0]["CreditRating"].ToString();
                txtNextCreditReview.Text = ds.Tables[0].Rows[0]["NextCreditReviewDate"].ToString();
                txtTolerancePercent.Text = ds.Tables[0].Rows[0]["TolerancePercent"].ToString();
                txtInterestPeriodDays.Text = ds.Tables[0].Rows[0]["InterestPeriodDays"].ToString();

                txtPaymentGraceD.Text = ds.Tables[0].Rows[0]["PaymentGraceDays"].ToString();
                txtDiscountGraceDay.Text = ds.Tables[0].Rows[0]["DiscountGraceDays"].ToString();
                txtClearingDays.Text = ds.Tables[0].Rows[0]["ClearingDays"].ToString();
                txtExemptedAmount.Text = ds.Tables[0].Rows[0]["ExemptedAmount"].ToString();
                txtFixedDiscount.Text = ds.Tables[0].Rows[0]["FixedDiscount"].ToString();
                txtPreferredpaymentType.Text = ds.Tables[0].Rows[0]["PreferredPaymentType"].ToString();

                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                txtOSCode.Text = ds.Tables[0].Rows[0]["OSCode"].ToString();
                txtSecuritydeposit.Text = ds.Tables[0].Rows[0]["SecurityDeposite"].ToString();
                txtSpecialDeal.Text = ds.Tables[0].Rows[0]["SpecialDeal"].ToString();
                txtCreatedDate.Text = ds.Tables[0].Rows[0]["CreateDate"].ToString();
                txtCreatedBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                txtUpdateBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());
                txtUpdatedDate.Text = ds.Tables[0].Rows[0]["UpdateDate"].ToString();
                


                ddlCity.ClearSelection();
                ddlCountry.ClearSelection();
                ddlCity.Items.FindByValue(ds.Tables[0].Rows[0]["CityID"].ToString()).Selected = true;
                ddlCountry.Items.FindByValue(ds.Tables[0].Rows[0]["CountryID"].ToString()).Selected = true;


                dml.dateConvert(txtNextCreditReview);
                //  DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["SysDate"].ToString());

                bool filer = bool.Parse(ds.Tables[0].Rows[0]["IsFiler"].ToString());
                bool register = bool.Parse(ds.Tables[0].Rows[0]["IsRegistered"].ToString());
                bool CreditCheck = bool.Parse(ds.Tables[0].Rows[0]["CreditChecking"].ToString());
                bool InterestCharge = bool.Parse(ds.Tables[0].Rows[0]["InterestCharges"].ToString());
                bool TaxExempted = bool.Parse(ds.Tables[0].Rows[0]["TaxExempted"].ToString());
                bool Additional = bool.Parse(ds.Tables[0].Rows[0]["AdditionalDeductionTaxExempted"].ToString());
                bool StopPayment = bool.Parse(ds.Tables[0].Rows[0]["IsStopPayment"].ToString());
                bool Active = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                bool gstser = bool.Parse(ds.Tables[0].Rows[0]["GstServiceProvider"].ToString());

                if (gstser == true)
                {
                    chkGstSER_Pro.Checked = true;
                }
                else
                {
                    chkGstSER_Pro.Checked = false;
                }
                if (filer == true)
                {
                    chkFiler.Checked = true;
                }
                else
                {
                    chkFiler.Checked = false;
                }

                if (register == true)
                {
                    chkRegister.Checked = true;
                }
                else
                {
                    chkRegister.Checked = false;
                }

                if (CreditCheck == true)
                {
                    chkCreditChecking.Checked = true;
                }
                else
                {
                    chkCreditChecking.Checked = false;
                }

                if (InterestCharge == true)
                {
                    chkInterestCharge.Checked = true;
                }
                else
                {
                    chkInterestCharge.Checked = false;
                }

                if (TaxExempted == true)
                {
                    chkTaxExempted.Checked = true;
                }
                else
                {
                    chkTaxExempted.Checked = false;
                }

                if (Additional == true)
                {
                    chkAdditionalTax.Checked = true;
                }
                else
                {
                    chkAdditionalTax.Checked = false;
                }

                if (StopPayment == true)
                {
                    chkIsStopped.Checked = true;
                }
                else
                {
                    chkIsStopped.Checked = false;
                }

                if (Active == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
                

            }
            Findcheckboxselect(BP_Id);
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }


    }
    public string show_username()
    {
        userid = Request.QueryString["UserID"];
        return userid;
    }
    public int GOcName()
    {
        string gocid = "";
        try
        {
            DataSet ds = dml.Find("select * from SET_Company where CompName = '"+Session["CompName"]+"'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                string id = gocid = ds.Tables[0].Rows[0]["GOCId"].ToString();
                return int.Parse(id);
            }
            else
            {
                return 0;
            }

        }
        catch (Exception ex)
        {
            return 0;
            Label1.Text = ex.Message;
        }
    }
    public string gocid(string gid)
    {
        string gocid = "";
        try
        {
            DataSet ds = dml.Find("select GOCName from SET_GOC where GocId ='"+ gid + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                string id = gocid = ds.Tables[0].Rows[0]["GOCName"].ToString();
                return id;
            }
            else
            {
                return "";
            }
           
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;

            return "";
        }
    }
    public void checkboxselect(string bp)
    {
        try
        {
            foreach (GridViewRow grow in GridView4.Rows)
            {
                CheckBox chk_del = (CheckBox)grow.FindControl("chkSelect");
                Label lblID = (Label)grow.FindControl("lblNatureID");

                if (chk_del.Checked)
                {
                    dml.Insert("INSERT INTO [SET_BPartnerType] ([GocID], [BPartnerID], [BPNatureID], [SysDate]) VALUES  ('1', '" +bp + "', '"+ lblID.Text + "', '"+DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff")+"');", "");
                }
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    public void Findcheckboxselect(string bp)
    {
        try
        {
            DataSet ds = dml.Find("select BPNatureID  from SET_BPartnerType where BPartnerID = '" + bp+"'");
            int countrow = ds.Tables[0].Rows.Count;
            if (countrow > 0)
            {
                for (int i = 0; i <= countrow - 1; i++)
                {
                    string val = ds.Tables[0].Rows[i]["BPNatureID"].ToString();
                    foreach (GridViewRow grow in GridView4.Rows)
                    {
                        CheckBox chk_del = (CheckBox)grow.FindControl("chkSelect");
                        Label lblID = (Label)grow.FindControl("lblNatureID");

                        if (lblID.Text == val)
                        {
                            // dml.Insert("INSERT INTO [SET_BPartnerType] ([GocID], [BPartnerID], [BPNatureID], [SysDate]) VALUES  ('1', '" + ViewState["BPID"].ToString() + "', '" + lblID.Text + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "');", "");
                            chk_del.Checked = true;
                            grow.BackColor = System.Drawing.Color.LightBlue;
                        }
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    public void Updatecheckboxselect(string bp)
    {
        try
        {
            dml.Delete("DELETE from SET_BPartnerType where BPartnerID = '" + bp + "'", "");
                foreach (GridViewRow grow in GridView4.Rows)
                {
                    CheckBox chk_del = (CheckBox)grow.FindControl("chkSelect");
                    Label lblID = (Label)grow.FindControl("lblNatureID");
                    if (chk_del.Checked == true)
                    {
                        dml.Insert("INSERT INTO [SET_BPartnerType] ([GocID], [BPartnerID], [BPNatureID], [SysDate]) VALUES  ('1', '" + bp + "', '" + lblID.Text + "', '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "');", "");
                    }
                }
           }
   
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    public void Clearcheckboxselect()
    {
        try
        {
            foreach (GridViewRow grow in GridView4.Rows)
            {
                CheckBox chk_del = (CheckBox)grow.FindControl("chkSelect");
                Label lblID = (Label)grow.FindControl("lblNatureID");

                chk_del.Checked = false;
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountry.SelectedItem.Value != "0")
        {
            if (ddlCountry.SelectedItem.Value == "Please select...")
            {
                ddlCity.Enabled = false;
                dml.dropdownsql(ddlCity, "SET_City", "CityName", "CityID", "CountryID","1");
            }
            else
            {
                ddlCity.Enabled = true;
                dml.dropdownsql(ddlCity, "SET_City", "CityName", "CityID", "CountryID", ddlCountry.SelectedItem.Value);
            }
        }
        else
        {
            ddlCity.Enabled = false;
            dml.dropdownsql(ddlCity, "SET_City", "CityName", "CityID");
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string id = e.Row.Cells[1].Text;
        bool flag = dml.isNumber(id);


        if (flag == true)
        {

            DataSet ds = dml.Find("select BPartnerId,MLD from SET_BusinessPartner where BPartnerId = '" + id + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string value = ds.Tables[0].Rows[i]["MLD"].ToString();
                    if (dml.Decrypt(value) == "q")
                    {
                        e.Row.Enabled = false;
                        e.Row.Cells[0].ToolTip = "Cannot be deleteable, work in find mode only";
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

            DataSet ds = dml.Find("select BPartnerId,MLD from SET_BusinessPartner where BPartnerId = '" + id + "'");
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



}