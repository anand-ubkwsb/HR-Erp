using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frm_Period : System.Web.UI.Page
{
    int DateFrom, AddDays, EditDays, DeleteDays;
    string userid, UserGrpID, FormID, fiscal;
    DmlOperation dml = new DmlOperation();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
    FieldHide fd = new FieldHide();

    string itemtype, itemhead, itemsubhead;
    float i;
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
            fiscal = Request.QueryString["fiscaly"];


            dml.dropdownsql(ddlItemTypes, "SET_ItemType", "Description", "ItemTypeID", "Record_Deleted", "0");
            dml.dropdownsql(ddlItemsHead, "SET_ItemHead", "ItemHeadName", "ItemHeadID", "Record_Deleted", "0");
            dml.dropdownsql(ddlItemSubHead, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID", "Record_Deleted", "0");
            dml.dropdownsql(ddlUOM1, "SET_UnitofMeasure", "UOMName", "UOMID", "Record_Deleted", "0");
            dml.dropdownsql(ddlUOM2, "SET_UnitofMeasure", "UOMName", "UOMID", "Record_Deleted", "0");

            //select Description,ItemID from SET_ItemMaster
            dml.dropdownsql(ddlDel_Description, "SET_ItemMaster", "Description", "ItemID", "Record_Deleted", "0");
            dml.dropdownsql(ddlDel_ItemType, "SET_ItemType", "Description", "ItemTypeID", "Record_Deleted", "0");
            dml.dropdownsql(ddlDel_ItemHead, "SET_ItemHead", "ItemHeadName", "ItemHeadID", "Record_Deleted", "0");
            dml.dropdownsql(ddlDel_ItemSubHead, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID", "Record_Deleted", "0");

            dml.dropdownsql(ddlEdit_Description, "SET_ItemMaster", "Description", "ItemID", "Record_Deleted", "0");
            dml.dropdownsql(ddlEdit_ItemType, "SET_ItemType", "Description", "ItemTypeID", "Record_Deleted", "0");
            dml.dropdownsql(ddlEdit_ItemHead, "SET_ItemHead", "ItemHeadName", "ItemHeadID", "Record_Deleted", "0");
            dml.dropdownsql(ddlEdit_ItemSubHead, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID", "Record_Deleted", "0");

            dml.dropdownsql(ddlFind_Description, "SET_ItemMaster", "Description", "ItemID","Record_Deleted","0");
            dml.dropdownsql(ddlFind_ItemType, "SET_ItemType", "Description", "ItemTypeID", "Record_Deleted", "0");
            dml.dropdownsql(ddlFind_ItemHead, "SET_ItemHead", "ItemHeadName", "ItemHeadID", "Record_Deleted", "0");
            dml.dropdownsql(ddlFind_ItemSubHead, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID", "Record_Deleted", "0");
            textClear();
           
            Findbox.Visible = false;
            DeleteBox.Visible = false;
            Editbox.Visible = false;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string type, typehead, level;

        type = "0";
        level = "0";

        string queryrpt = "select * from View_ItemMasterFED";
        string rptname = "rpt_ItemMASTER";

        Response.Redirect("~/Reportsform/Reportdisplay.aspx?Itemtype='" + Server.UrlEncode(type) + "'&headlevel='" + level + "'&q="+queryrpt+ "&rptname="+rptname+"");

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

        txtItemCode.Enabled = false;
        txtDesc.Enabled = true;
        txtColor.Enabled = true;
        txtLenght.Enabled = true;
        txtWidth.Enabled = true;
        ddlItemTypes.Enabled = true;
        ddlItemsHead.Enabled = false;
        ddlItemSubHead.Enabled = false;
        rdbLocal.Enabled = true;
        rdbImport.Enabled = true;
        ddlUOM1.Enabled = true;
        ddlUOM2.Enabled = true;
        txtQtyRcvPer.Enabled = true;
        txtQtyIssuePer.Enabled = true;
        rdbConsumable.Enabled = true;
        rdbExpense.Enabled = true;
        rdbAsset.Enabled = true;
        txtOldCOde.Enabled = true;
        txtRoundFactor.Enabled = true;
        chkActive.Enabled = true;
        chkActive.Checked = true;
        txtItemCode.Text = "00-000-000-000000-0";

        txtCreatedDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff");

        userid = Request.QueryString["UserID"];
        DataSet ds_autoUserName = dml.Find("select user_name from SET_User_Manager where UserId ='" + userid + "'");
        txtCreatedby.Text = ds_autoUserName.Tables[0].Rows[0]["user_name"].ToString();

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        



        try
        {
            DataSet unique = dml.Find("select ItemCode from SET_ItemMaster where ItemCode= '"+txtItemCode.Text+ "' and Record_Deleted = '0' and Description = '"+txtDesc.Text+"'");
            if (unique.Tables[0].Rows.Count > 0)
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "Item Code Already Inserted";
            }
           else
            {
                if (rdbLocal.Checked == true || rdbImport.Checked == true)
                {
                    userid = Request.QueryString["UserID"];
                    UserGrpID = Request.QueryString["UsergrpID"];
                    FormID = Request.QueryString["FormID"];
                    fiscal = Request.QueryString["fiscaly"];

                    int chk, local_imp = 0, IsConsume = 0, IsAsset = 0, IsExpense = 0;
                    string uom1 = "0", uom2 = "0", len = "0", wid = "0";

                    if (chkActive.Checked == true)
                    {
                        chk = 1;
                    }
                    else
                    {
                        chk = 0;
                    }
                    if (rdbLocal.Checked == true)
                    {
                        local_imp = 0;
                        txtItemCode.Text = itemcodelocalgenerate();
                    }
                    if (rdbImport.Checked == true)
                    {
                        local_imp = 1;
                        txtItemCode.Text = itemcodeImportgenerate();
                    }
                    if (rdbConsumable.Checked == true)
                    {
                        IsConsume = 1;
                    }
                    if (rdbExpense.Checked == true)
                    {
                        IsExpense = 1;
                    }
                    if (rdbAsset.Checked == true)
                    {
                        IsAsset = 1;
                    }
                    if(ddlUOM1.SelectedIndex != 0)
                    {
                        uom1 = ddlUOM1.SelectedItem.Value;
                    }
                    if (ddlUOM2.SelectedIndex != 0)
                    {
                        uom2 = ddlUOM2.SelectedItem.Value;
                    }
                    if(txtLenght.Text != "")
                    {
                        len = txtLenght.Text;
                    }
                    if (txtWidth.Text != "")
                    {
                        wid = txtWidth.Text;
                    }
                    if (rdbConsumable.Checked == false && rdbExpense.Checked == false && rdbAsset.Checked == false)
                    {
                        Label1.Text = "Please select Atleast one option";
                        rdbAsset.BackColor = System.Drawing.Color.Pink;
                        rdbExpense.BackColor = System.Drawing.Color.Pink;
                        rdbConsumable.BackColor = System.Drawing.Color.Pink;

                    }
                    else {

                        dml.Insert("INSERT INTO [SET_ItemMaster] ([GocID],[ItemCode], [Description], [Color], [Length], [Width], [ItemTypeID], [ItemHeadID], [ItemSubHeadID], [Local_Import], [UOMId], [UOMId2], [QtyRcvTolerancePercent], [QtyIssueTolerancePercent], [IsConsumable], [IsExpense], [IsAsset], [OldItemCode], [RoundingFactor], [IsActive], [CreatedBy], [CreateDate], [Record_Deleted],[MLD]) VALUES ('1', '" + txtItemCode.Text + "', '" + txtDesc.Text + "', '" + txtColor.Text + "', '" + len + "', '" + wid + "', '" + ddlItemTypes.SelectedItem.Value + "', '" + ddlItemsHead.SelectedItem.Value + "', '" + ddlItemSubHead.Text + "', '" + local_imp + "', '" + uom1 + "', '" + uom2 + "', '" + txtQtyRcvPer.Text + "', '" + txtQtyIssuePer.Text + "', '" + IsConsume + "', '" + IsExpense + "', '" + IsAsset + "', '" + txtOldCOde.Text + "', '" + txtRoundFactor.Text + "', '" + chk + "', '" + txtCreatedby.Text + "', '" + DateTime.Now.ToString("dd-MMM-yyy hh:mm:ss.ffff") + "', '0','"+dml.Encrypt("h")+"');", "alertme()");
                        
                        dml.Update("update SET_ItemHead set MLD = '" + dml.Encrypt("q") + "' where ItemHeadID = '" + ddlItemsHead.SelectedItem.Value + "'", "");
                        dml.Update("update SET_ItemSubHead set MLD = '" + dml.Encrypt("q") + "' where ItemSubHeadID = '" + ddlItemSubHead.SelectedItem.Value + "'", "");
                        dml.Update("update SET_ItemType set MLD = '" + dml.Encrypt("q") + "' where ItemTypeID = '" + ddlItemTypes.SelectedItem.Value + "'", "");



                        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "alertme()", true);
                        Label1.Text = "";
                        rdbAsset.BackColor = System.Drawing.Color.Transparent;
                        rdbExpense.BackColor = System.Drawing.Color.Transparent;
                        rdbConsumable.BackColor = System.Drawing.Color.Transparent;
                        textClear();

                    }
                }
                else
                {
                    lblSteriek.Text = "*";
                    Label1.Text = "Please Select atleast one option";
                }
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
       try
        {
            txtUpdateBy.Text = show_username();
            int chk, local_imp = 0, IsConsume = 0, IsAsset = 0, IsExpense = 0;

            if (chkActive.Checked == true)
            {
                chk = 1;
            }
            else
            {
                chk = 0;
            }
            if (rdbLocal.Checked == true)
            {
                local_imp = 0;
            }
            if (rdbImport.Checked == true)
            {
                local_imp = 1;
            }
            if (rdbConsumable.Checked == true)
            {
                IsConsume = 1;
            }
            if (rdbExpense.Checked == true)
            {
                IsExpense = 1;
            }
            if (rdbAsset.Checked == true)
            {
                IsAsset = 1;
            }
            int uu1, uu2;
            if(ddlUOM1.SelectedIndex == 0)
            {
                uu1 = 0;
            }
            else
            {
                uu1 =int.Parse(ddlUOM1.SelectedItem.Value);
            }
            if (ddlUOM1.SelectedIndex == 0)
            {
                uu2 = 0;
            }
            else
            {
                uu2 = int.Parse(ddlUOM2.SelectedItem.Value);
            }
            DataSet ds_up = dml.Find("select * from SET_ItemMaster WHERE ([GocID]='1') AND ([ItemID]='"+ViewState["SNO"].ToString()+"') AND ([ItemCode]='"+txtItemCode.Text+"') AND ([Description]='"+txtDesc.Text+"') AND ([Color]='"+txtColor.Text+"') AND ([Length]= '"+txtLenght.Text+"') AND ([Width]= '"+txtWidth.Text+"') AND ([ItemTypeID]='"+ddlItemTypes.SelectedItem.Value+"') AND ([ItemHeadID]='"+ddlItemsHead.SelectedItem.Value+"') AND ([ItemSubHeadID]='"+ddlItemSubHead.SelectedItem.Value+"') AND ([Local_Import]='"+local_imp+"') AND ([UOMId]='"+uu1+"') AND ([UOMId2]='"+uu2+"') AND ([QtyRcvTolerancePercent] = '"+txtQtyRcvPer.Text+"') AND ([QtyIssueTolerancePercent]='"+txtQtyIssuePer.Text+"') AND ([IsConsumable]='"+IsConsume+"') AND ([IsExpense]='"+IsExpense+"') AND ([IsAsset]='"+IsAsset+"') AND ([OldItemCode]='"+txtOldCOde.Text+"') AND ([RoundingFactor] = '"+txtRoundFactor.Text+"') AND ([IsActive]='"+chk+"')");

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
                userid = Request.QueryString["UserID"];
                dml.Update("UPDATE [SET_ItemMaster] SET [GocID]='1', [ItemCode]='" + txtItemCode.Text + "', [Description]='" + txtDesc.Text + "', [Color]='" + txtColor.Text + "', [Length]='" + txtLenght.Text + "', [Width]='" + txtWidth.Text + "', [ItemTypeID]='" + ddlItemTypes.SelectedItem.Value + "', [ItemHeadID]='" + ddlItemsHead.SelectedItem.Value + "', [ItemSubHeadID]='" + ddlItemSubHead.SelectedItem.Value + "', [Local_Import]='" + local_imp + "', [UOMId]='" + ddlUOM1.SelectedItem.Value + "', [UOMId2]='" + ddlUOM2.SelectedItem.Value + "', [QtyRcvTolerancePercent]='" + txtQtyRcvPer.Text + "', [QtyIssueTolerancePercent]='" + txtQtyIssuePer.Text + "', [IsConsumable]='" + IsConsume + "', [IsExpense]='" + IsExpense + "', [IsAsset]='" + IsAsset + "', [OldItemCode]='" + txtOldCOde.Text + "', [RoundingFactor]='" + txtRoundFactor.Text + "', [IsActive]='" + chk + "', [UpdatedBy]='" + txtUpdateBy.Text + "', [UpdateDate]='" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.ffff") + "', [Record_Deleted]='0' WHERE ([ItemID]='" + ViewState["SNO"] + "');", "");
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
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnInsert.Visible = true;
        updatecol.Visible = false;
        btnDelete.Visible = true;
        btnUpdate.Visible = false;
        btnEdit.Visible = true;
        btnFind.Visible = true;
        btnSave.Visible = false;
        btnDelete_after.Visible = false;
        lblSteriek.Text = "";
         textClear();
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
            //select Description, ItemTypeID,ItemHeadID,ItemSubHeadID,Local_Import,OldItemCode from SET_ItemMaster
            GridView2.DataBind();
            string swhere;
            string squer = "select * from View_ItemMasterFED";

            if (ddlDel_Description.SelectedIndex != 0)
            {
                swhere = "Description like '" + ddlDel_Description.SelectedItem.Text + "%'";
            }
            else
            {
                swhere = "Description is not null";
            }
            if (ddlDel_ItemType.SelectedIndex != 0)
            {
                swhere = swhere + " and ItemTypeID like '" + ddlDel_ItemType.SelectedItem.Value + "%'";
            }
            else
            {
                swhere = swhere + " and ItemTypeID is not null";
            }

            if (ddlDel_ItemHead.SelectedIndex != 0)
            {
                swhere = swhere + " and ItemHeadID like '" + ddlDel_ItemHead.SelectedItem.Value + "%'";
            }
            else
            {
                swhere = swhere + " and ItemHeadID is not null";
            }

            if (ddlDel_ItemSubHead.SelectedIndex != 0)
            {
                swhere = swhere + " and ItemSubHeadID like '" + ddlDel_ItemSubHead.SelectedItem.Value + "%'";
            }
            else
            {
                swhere = swhere + " and ItemSubHeadID is not null";
            }

            if (txtDel_OldCode.Text != "")
            {
                swhere = swhere + " and ItemCode like '" + txtDel_OldCode.Text + "%'";
            }
            else
            {
                swhere = swhere + " and ItemCode is not null";
            }

            if (rdbDel_Local.Checked == true)
            {
                swhere = swhere + " and Local_Import = '0'";
            }
            if (rdbDel_Import.Checked == true)
            {
                swhere = swhere + " and Local_Import = '1'";
            }
      

            if (chkDel_Active.Checked == true)
            {
                swhere = swhere + " and IsActive = '1'";
            }
            else if (chkDel_Active.Checked == false)
            {
                swhere = swhere + " and IsActive = '0'";
            }
            else
            {
                swhere = swhere + " and IsActive is not null";
            }
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY Description";

            Findbox.Visible = false;
            fieldbox.Visible = false;
            Editbox.Visible = false;

            DataSet dgrid = dml.grid(squer);
            GridView2.DataSource = dgrid;
            GridView2.DataBind();
        }
        catch (Exception ex)
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
        try
        {
            //select Description, ItemTypeID,ItemHeadID,ItemSubHeadID,Local_Import,OldItemCode from SET_ItemMaster
            GridView1.DataBind();
            string swhere;
            string squer = "select * from View_ItemMasterFED";

            if (ddlFind_Description.SelectedIndex != 0)
            {
                swhere = "Description like '" + ddlFind_Description.SelectedItem.Text + "%'";
            }
            else
            {
                swhere = "Description is not null";
            }
            if (ddlFind_ItemType.SelectedIndex != 0)
            {
                swhere = swhere + " and ItemTypeID like '" + ddlFind_ItemType.SelectedItem.Value + "%'";
            }
            else
            {
                swhere = swhere + " and ItemTypeID is not null";
            }

            if (ddlFind_ItemHead.SelectedIndex != 0)
            {
                swhere = swhere + " and ItemHeadID like '" + ddlFind_ItemHead.SelectedItem.Value + "%'";
            }
            else
            {
                swhere = swhere + " and ItemHeadID is not null";
            }

            if (ddlFind_ItemSubHead.SelectedIndex != 0)
            {
                swhere = swhere + " and ItemSubHeadID like '" + ddlFind_ItemSubHead.SelectedItem.Value + "%'";
            }
            else
            {
                swhere = swhere + " and ItemSubHeadID is not null";
            }

            if (txtFind_oldCode.Text != "")
            {
                swhere = swhere + " and ItemCode like '" + txtFind_oldCode.Text + "%'";
            }
            else
            {
                swhere = swhere + " and ItemCode is not null";
            }

            if (rdbFind_Local.Checked == true)
            {
                swhere = swhere + " and Local_Import = '0'";
            }
            if (rdbFind_Import.Checked == true)
            {
                swhere = swhere + " and Local_Import = '1'";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY Description";

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
            //select Description, ItemTypeID,ItemHeadID,ItemSubHeadID,Local_Import,OldItemCode from SET_ItemMaster
            GridView3.DataBind();
            string swhere;
            string squer = "select * from View_ItemMasterFED";

            if (ddlEdit_Description.SelectedIndex != 0)
            {
                swhere = "Description like '" + ddlEdit_Description.SelectedItem.Text + "%'";
            }
            else
            {
                swhere = "Description is not null";
            }
            if (ddlEdit_ItemType.SelectedIndex != 0)
            {
                swhere = swhere + " and ItemTypeID like '" + ddlEdit_ItemType.SelectedItem.Value + "%'";
            }
            else
            {
                swhere = swhere + " and ItemTypeID is not null";
            }

            if (ddlEdit_ItemHead.SelectedIndex != 0)
            {
                swhere = swhere + " and ItemHeadID like '" + ddlEdit_ItemHead.SelectedItem.Value + "%'";
            }
            else
            {
                swhere = swhere + " and ItemHeadID is not null";
            }

            if (ddlEdit_ItemSubHead.SelectedIndex != 0)
            {
                swhere = swhere + " and ItemSubHeadID like '" + ddlEdit_ItemSubHead.SelectedItem.Value + "%'";
            }
            else
            {
                swhere = swhere + " and ItemSubHeadID is not null";
            }

            if (txtEdit_OldCode.Text != "")
            {
                swhere = swhere + " and ItemCode like '" + txtEdit_OldCode.Text + "%'";
            }
            else
            {
                swhere = swhere + " and ItemCode is not null";
            }

            if (rdbedit_Local.Checked == true)
            {
                swhere = swhere + " and Local_Import = '0'";
            }
            if (rdbedit_Import.Checked == true)
            {
                swhere = swhere + " and Local_Import = '1'";
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
            squer = squer + " where " + swhere + " and Record_Deleted = 0 ORDER BY Description";

            Findbox.Visible = false;
            fieldbox.Visible = false;
            DeleteBox.Visible = false;

            DataSet dgrid = dml.grid(squer);
            GridView3.DataSource = dgrid;
            GridView3.DataBind();
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    public void textClear()
    {
        DeleteBox.Visible = false;
        Editbox.Visible = false;
        Findbox.Visible = false;
        fieldbox.Visible = true;
        updatecol.Visible = false;

        txtItemCode.Enabled = false;
        txtDesc.Enabled = false;
        txtColor.Enabled = false;
        txtLenght.Enabled = false;
        txtWidth.Enabled = false;
        ddlItemTypes.Enabled = false;
        ddlItemsHead.Enabled = false;
        ddlItemSubHead.Enabled = false;
        rdbLocal.Enabled = false;
        rdbImport.Enabled = false;
        ddlUOM1.Enabled = false;
        ddlUOM2.Enabled = false;
        txtQtyRcvPer.Enabled = false;
        txtQtyIssuePer.Enabled = false;
        rdbConsumable.Enabled = false;
        rdbExpense.Enabled = false;
        rdbAsset.Enabled = false;
        txtOldCOde.Enabled = false;
        txtRoundFactor.Enabled = false;
        chkActive.Enabled = false;
        txtCreatedby.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdateBy.Enabled = false;
        txtUpdateDate.Enabled = false;
        rdbAsset.BackColor = System.Drawing.Color.Transparent;
        rdbExpense.BackColor = System.Drawing.Color.Transparent;
        rdbConsumable.BackColor = System.Drawing.Color.Transparent;

        Label1.Text = "";
        txtItemCode.Text = "";
        txtDesc.Text = "";
        txtColor.Text = "";
        txtLenght.Text = "";
        txtWidth.Text = "";
        ddlItemTypes.SelectedIndex = 0;
      //  ddlItemsHead.SelectedIndex = 0;
       // ddlItemSubHead.SelectedIndex = 0;
        rdbLocal.Checked = false;
        rdbImport.Checked = false;
        ddlUOM1.SelectedIndex = 0;
        ddlUOM2.SelectedIndex = 0;
        txtQtyRcvPer.Text = "";
        txtQtyIssuePer.Text = "";
        rdbConsumable.Checked = false;
        rdbExpense.Checked = false;
        rdbAsset.Checked = false;
        txtOldCOde.Text = "";
        txtRoundFactor.Text = "";
        ddlItemsHead.ClearSelection();
        ddlItemTypes.ClearSelection();
        ddlItemSubHead.ClearSelection();
       
        txtCreatedby.Text = "";
        txtCreatedDate.Text = "";
        txtUpdateBy.Text = "";
        txtUpdateDate.Text = "";

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
        //    btnCancel.Visible = true;
        //}
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            dml.Delete("update SET_ItemMaster set Record_Deleted = 1 where ItemID = " + ViewState["SNO"].ToString() + "", "");
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
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtItemCode.Enabled = false;
        txtDesc.Enabled = false;
        txtColor.Enabled = false;
        txtLenght.Enabled = false;
        txtWidth.Enabled = false;
        ddlItemTypes.Enabled = false;
        ddlItemsHead.Enabled = false;
        ddlItemSubHead.Enabled = false;
        rdbLocal.Enabled = false;
        rdbImport.Enabled = false;
        ddlUOM1.Enabled = false;
        ddlUOM2.Enabled = false;
        txtQtyRcvPer.Enabled = false;
        txtQtyIssuePer.Enabled = false;
        rdbConsumable.Enabled = false;
        rdbExpense.Enabled = false;
        rdbAsset.Enabled = false;
        txtOldCOde.Enabled = false;
        txtRoundFactor.Enabled = false;
        chkActive.Enabled = false;
        txtCreatedby.Enabled = false;
        txtCreatedDate.Enabled = false;
        txtUpdateBy.Enabled = false;
        txtUpdateDate.Enabled = false;

        btnUpdate.Visible = false;
        btnInsert.Visible = false;
        btnDelete.Visible = false;
        btnEdit.Visible = false;
        Label1.Text = "";

        fieldbox.Visible = true;
        Findbox.Visible = false;
        Editbox.Visible = false;
        try
        {
            string serial_id;
            serial_id = GridView1.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select * from SET_ItemMaster where ItemID  = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlItemTypes.ClearSelection();
                ddlItemsHead.ClearSelection();
                ddlItemSubHead.ClearSelection();
                ddlUOM1.ClearSelection();
                ddlUOM2.ClearSelection();
                //FindByText(ds.Tables[0].Rows[0]["PurchasesAcct"].ToString()).Selected = true;
                txtItemCode.Text = ds.Tables[0].Rows[0]["ItemCode"].ToString();
                txtDesc.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                txtColor.Text = ds.Tables[0].Rows[0]["Color"].ToString();
                txtLenght.Text = ds.Tables[0].Rows[0]["Length"].ToString();
                txtWidth.Text = ds.Tables[0].Rows[0]["Width"].ToString();

                
                ddlItemTypes.Items.FindByValue(ds.Tables[0].Rows[0]["ItemTypeID"].ToString()).Selected = true;
                ddlItemsHead.Items.FindByValue(ds.Tables[0].Rows[0]["ItemHeadID"].ToString()).Selected = true;
                ddlItemSubHead.Items.FindByValue(ds.Tables[0].Rows[0]["ItemSubHeadID"].ToString()).Selected = true;

                bool rdbLocalchk = bool.Parse(ds.Tables[0].Rows[0]["Local_Import"].ToString());

                if (ds.Tables[0].Rows[0]["UOMId"].ToString() != "0")
                {
                    ddlUOM1.Items.FindByValue(ds.Tables[0].Rows[0]["UOMId"].ToString()).Selected = true;
                }
                if (ds.Tables[0].Rows[0]["UOMId2"].ToString() != "0")
                {
                    ddlUOM2.Items.FindByValue(ds.Tables[0].Rows[0]["UOMId2"].ToString()).Selected = true;
                }

                txtQtyRcvPer.Text = ds.Tables[0].Rows[0]["QtyRcvTolerancePercent"].ToString();
                txtQtyIssuePer.Text = ds.Tables[0].Rows[0]["QtyIssueTolerancePercent"].ToString();

                bool rdbConsumablechk = bool.Parse(ds.Tables[0].Rows[0]["IsConsumable"].ToString());
                bool rdbExpensechk = bool.Parse(ds.Tables[0].Rows[0]["IsExpense"].ToString());
                bool rdbAssetchk = bool.Parse(ds.Tables[0].Rows[0]["IsAsset"].ToString());

                if (rdbConsumablechk == true)
                {
                    rdbConsumable.Checked = true;
                }
                if (rdbExpensechk == true)
                {
                    rdbExpense.Checked = true;
                }
                if (rdbAssetchk == true)
                {
                    rdbAsset.Checked = true;
                }
                txtOldCOde.Text = ds.Tables[0].Rows[0]["OldItemCode"].ToString();
                txtRoundFactor.Text = ds.Tables[0].Rows[0]["RoundingFactor"].ToString();
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                txtCreatedby.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                txtCreatedDate.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                txtUpdateBy.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                txtUpdateDate.Text = ds.Tables[0].Rows[0]["Description"].ToString();


                // ddlCity.Items.FindByValue(ds.Tables[0].Rows[0]["CityID"].ToString()).Selected = true;


                txtCreatedby.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                // txtSystemDate.Text = ds.Tables[0].Rows[0]["CreateDate"].ToString();
                txtUpdateBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());
                //txtUpadtedDate.Text = ds.Tables[0].Rows[0]["UpdateDate"].ToString();

                //rdbLocal

                if (rdbLocalchk == true)
                {
                    rdbImport.Checked = true;
                }
                else
                {
                    rdbLocal.Checked = true;
                }
                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }


                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() == "")
                {
                    txtCreatedDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                    txtCreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdateDate"].ToString() == "")
                {
                    txtUpdateDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateDate"].ToString());
                    txtUpdateDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }

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
        txtUpdateBy.Enabled = false;
        txtUpdateDate.Enabled = false;
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
            DataSet ds = dml.Find("select * from SET_ItemMaster where ItemID  = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlItemTypes.ClearSelection();
                ddlItemsHead.ClearSelection();
                ddlItemSubHead.ClearSelection();
                ddlUOM1.ClearSelection();
                ddlUOM2.ClearSelection();
                //FindByText(ds.Tables[0].Rows[0]["PurchasesAcct"].ToString()).Selected = true;
                txtItemCode.Text = ds.Tables[0].Rows[0]["ItemCode"].ToString();
                txtDesc.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                txtColor.Text = ds.Tables[0].Rows[0]["Color"].ToString();
                txtLenght.Text = ds.Tables[0].Rows[0]["Length"].ToString();
                txtWidth.Text = ds.Tables[0].Rows[0]["Width"].ToString();


                ddlItemTypes.Items.FindByValue(ds.Tables[0].Rows[0]["ItemTypeID"].ToString()).Selected = true;
                ddlItemsHead.Items.FindByValue(ds.Tables[0].Rows[0]["ItemHeadID"].ToString()).Selected = true;
                ddlItemSubHead.Items.FindByValue(ds.Tables[0].Rows[0]["ItemSubHeadID"].ToString()).Selected = true;

                bool rdbLocalchk = bool.Parse(ds.Tables[0].Rows[0]["Local_Import"].ToString());

                if (ds.Tables[0].Rows[0]["UOMId"].ToString() != "0")
                {
                    ddlUOM1.Items.FindByValue(ds.Tables[0].Rows[0]["UOMId"].ToString()).Selected = true;
                }
                if (ds.Tables[0].Rows[0]["UOMId2"].ToString() != "0")
                {
                    ddlUOM2.Items.FindByValue(ds.Tables[0].Rows[0]["UOMId2"].ToString()).Selected = true;
                }

                txtQtyRcvPer.Text = ds.Tables[0].Rows[0]["QtyRcvTolerancePercent"].ToString();
                txtQtyIssuePer.Text = ds.Tables[0].Rows[0]["QtyIssueTolerancePercent"].ToString();
                bool rdbConsumablechk = bool.Parse(ds.Tables[0].Rows[0]["IsConsumable"].ToString());
                bool rdbExpensechk = bool.Parse(ds.Tables[0].Rows[0]["IsExpense"].ToString());
                bool rdbAssetchk = bool.Parse(ds.Tables[0].Rows[0]["IsAsset"].ToString());

                if (rdbConsumablechk == true)
                {
                    rdbConsumable.Checked = true;
                }
                if (rdbExpensechk == true)
                {
                    rdbExpense.Checked = true;
                }
                if (rdbAssetchk == true)
                {
                    rdbAsset.Checked = true;
                }
                txtOldCOde.Text = ds.Tables[0].Rows[0]["OldItemCode"].ToString();
                txtRoundFactor.Text = ds.Tables[0].Rows[0]["RoundingFactor"].ToString();
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                txtCreatedby.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                txtCreatedDate.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                txtUpdateBy.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                txtUpdateDate.Text = ds.Tables[0].Rows[0]["Description"].ToString();


                // ddlCity.Items.FindByValue(ds.Tables[0].Rows[0]["CityID"].ToString()).Selected = true;


                txtCreatedby.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                // txtSystemDate.Text = ds.Tables[0].Rows[0]["CreateDate"].ToString();
                txtUpdateBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());
                //txtUpadtedDate.Text = ds.Tables[0].Rows[0]["UpdateDate"].ToString();

                //rdbLocal

                if (rdbLocalchk == true)
                {
                    rdbImport.Checked = true;
                }
                else
                {
                    rdbLocal.Checked = true;
                }
                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }


                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() == "")
                {
                    txtCreatedDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                    txtCreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdateDate"].ToString() == "")
                {
                    txtUpdateDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateDate"].ToString());
                    txtUpdateDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
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
            textClear();
            btnInsert.Visible = false;
            btnCancel.Visible = true;
            btnUpdate.Visible = true;
            btnDelete.Visible = false;
            btnEdit.Visible = false;
            btnFind.Visible = false;
            Label1.Text = "";

            fieldbox.Visible = true;
            Findbox.Visible = false;
            Editbox.Visible = false;
        try
        {
            string serial_id;
            serial_id = GridView3.SelectedRow.Cells[1].Text;
            ViewState["SNO"] = serial_id;
            DataSet ds = dml.Find("select * from SET_ItemMaster where ItemID  = '" + serial_id + "' and Record_Deleted = 0");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlItemTypes.ClearSelection();
                ddlItemsHead.ClearSelection();
                ddlItemSubHead.ClearSelection();
                ddlUOM1.ClearSelection();
                ddlUOM2.ClearSelection();
                //FindByText(ds.Tables[0].Rows[0]["PurchasesAcct"].ToString()).Selected = true;
                txtItemCode.Text = ds.Tables[0].Rows[0]["ItemCode"].ToString();
                txtDesc.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                txtColor.Text = ds.Tables[0].Rows[0]["Color"].ToString();
                txtLenght.Text = ds.Tables[0].Rows[0]["Length"].ToString();
                txtWidth.Text = ds.Tables[0].Rows[0]["Width"].ToString();

               
                ddlItemTypes.Items.FindByValue(ds.Tables[0].Rows[0]["ItemTypeID"].ToString()).Selected = true;
                ddlItemsHead.Items.FindByValue(ds.Tables[0].Rows[0]["ItemHeadID"].ToString()).Selected = true;
                ddlItemSubHead.Items.FindByValue(ds.Tables[0].Rows[0]["ItemSubHeadID"].ToString()).Selected = true;

                bool rdbLocalchk = bool.Parse(ds.Tables[0].Rows[0]["Local_Import"].ToString());
                string str = ds.Tables[0].Rows[0]["UOMId"].ToString();
                if (ds.Tables[0].Rows[0]["UOMId"].ToString() != "0")
                {
                    ddlUOM1.Items.FindByValue(ds.Tables[0].Rows[0]["UOMId"].ToString()).Selected = true;
                }
                if (ds.Tables[0].Rows[0]["UOMId2"].ToString() != "0")
                {
                    ddlUOM2.Items.FindByValue(ds.Tables[0].Rows[0]["UOMId2"].ToString()).Selected = true;
                }

                txtQtyRcvPer.Text = ds.Tables[0].Rows[0]["QtyRcvTolerancePercent"].ToString();
                txtQtyIssuePer.Text = ds.Tables[0].Rows[0]["QtyIssueTolerancePercent"].ToString();
               
                bool rdbConsumablechk = bool.Parse(ds.Tables[0].Rows[0]["IsConsumable"].ToString());
                bool rdbExpensechk = bool.Parse(ds.Tables[0].Rows[0]["IsExpense"].ToString());
                bool rdbAssetchk = bool.Parse(ds.Tables[0].Rows[0]["IsAsset"].ToString());

                if(rdbConsumablechk == true)
                {
                    rdbConsumable.Checked = true;
                }
                if (rdbExpensechk == true)
                {
                    rdbExpense.Checked = true;
                }
                if (rdbAssetchk == true)
                {
                    rdbAsset.Checked = true;
                }


                txtOldCOde.Text = ds.Tables[0].Rows[0]["OldItemCode"].ToString();
                txtRoundFactor.Text = ds.Tables[0].Rows[0]["RoundingFactor"].ToString();
                bool chk = bool.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
                txtCreatedby.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                txtCreatedDate.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                txtUpdateBy.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                txtUpdateDate.Text = ds.Tables[0].Rows[0]["Description"].ToString();


                // ddlCity.Items.FindByValue(ds.Tables[0].Rows[0]["CityID"].ToString()).Selected = true;


                txtCreatedby.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["CreatedBy"].ToString());
                // txtSystemDate.Text = ds.Tables[0].Rows[0]["CreateDate"].ToString();
                txtUpdateBy.Text = dml.show_usernameFED(ds.Tables[0].Rows[0]["UpdatedBy"].ToString());
                //txtUpadtedDate.Text = ds.Tables[0].Rows[0]["UpdateDate"].ToString();

                //rdbLocal

                if (rdbLocalchk == true)
                {
                    rdbImport.Checked = true;
                }
                else
                {
                    rdbLocal.Checked = true;
                }
                if (chk == true)
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }


                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() == "")
                {
                    txtCreatedDate.Text = "";
                }
                else {
                    DateTime sys = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                    txtCreatedDate.Text = sys.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }
                if (ds.Tables[0].Rows[0]["UpdateDate"].ToString() == "")
                {
                    txtUpdateDate.Text = "";
                }
                else {
                    DateTime Updatedate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateDate"].ToString());
                    txtUpdateDate.Text = Updatedate.ToString("dd-MMM-yyyy hh:mm:ss.ffff");
                }

            }
            txtItemCode.Enabled = false;
            txtDesc.Enabled = true;
            txtColor.Enabled = true;
            txtLenght.Enabled = true;
            txtWidth.Enabled = true;
            ddlItemTypes.Enabled = false;
            ddlItemsHead.Enabled = false;
            ddlItemSubHead.Enabled = false;
            rdbLocal.Enabled = false;
            rdbImport.Enabled = false;
            ddlUOM1.Enabled = true;
            ddlUOM2.Enabled = true;
            txtQtyRcvPer.Enabled = true;
            txtQtyIssuePer.Enabled = true;
            rdbConsumable.Enabled = true;
            rdbExpense.Enabled = true;
            rdbAsset.Enabled = true;
            txtOldCOde.Enabled = true;
            txtRoundFactor.Enabled = true;
            chkActive.Enabled = true;
            updatecol.Visible = true;
            txtUpdateBy.Enabled = false;
            txtUpdateDate.Enabled = false;

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
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            float itemtypecode=0, itemHeadcode=0, itemSubHeadcode=0;

            if (ddlItemTypes.SelectedIndex != 0)
            {
                 itemtypecode = float.Parse(ddlItemTypes.SelectedItem.Value);
            }
            if (ddlItemsHead.SelectedIndex != 0)
            {
                 itemHeadcode = float.Parse(ddlItemsHead.SelectedItem.Value);
            }
            if (ddlItemSubHead.SelectedIndex != 0)
            {
                 itemSubHeadcode = float.Parse(ddlItemSubHead.SelectedItem.Value);
            }
                if (itemHeadcode > 0)
                {
                    string rdb_l_i = "";
                    if (rdbLocal.Checked == true)
                    {
                        rdb_l_i = "0";
                    }
                    if (rdbImport.Checked == true)
                    {
                        rdb_l_i = "1";
                    }
                    txtItemCode.Text = itemtypecode.ToString("00") + "-" + itemHeadcode.ToString("000") + "-" + itemSubHeadcode.ToString("000") + "-000001" + "-" + rdb_l_i;

                }
            }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    protected void rdbLocal_CheckedChanged(object sender, EventArgs e)
    {
        if(rdbLocal.Checked == true)
        {
            try
            {
                float itemtypecode = 0;
                string  itemHeadcode = "000", itemSubHeadcode = "000";

                if (ddlItemTypes.SelectedIndex != 0)
                {
                    itemtypecode =float.Parse(ddlItemTypes.SelectedItem.Value);
                    
                }
                if (ddlItemsHead.SelectedIndex != 0)
                {
                    //  itemHeadcode = float.Parse(ddlItemsHead.SelectedItem.Value);

                    DataSet dsitemHeadcode = dml.Find("select ItemHeadCode from SET_ItemHead where ItemHeadID= '" + ddlItemsHead.SelectedItem.Value + "';");
                    if (dsitemHeadcode.Tables[0].Rows.Count > 0)
                    {
                        itemHeadcode = dsitemHeadcode.Tables[0].Rows[0]["ItemHeadCode"].ToString();
                    }
                    else
                    {
                        itemHeadcode = "001";
                    }

                }
                if (ddlItemSubHead.SelectedIndex != 0)
                {
                    //  itemSubHeadcode = float.Parse(ddlItemSubHead.SelectedItem.Value);

                    DataSet ds1 = dml.Find("select ItemsubHeadCode from SET_ItemSubHead where ItemSubHeadID= '" + ddlItemSubHead.SelectedItem.Value + "';");
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        itemSubHeadcode = ds1.Tables[0].Rows[0]["ItemsubHeadCode"].ToString();
                    }
                    else
                    {
                        itemSubHeadcode = "001";
                    }

                }
               
                DataSet ds = dml.Find("select MAX(SUBSTRING(ItemCode,12,6)) as code from SET_ItemMaster where SUBSTRING(ItemCode, 1, 2) = '" + itemtypecode.ToString("00") + "' and substring(ItemCode,4,3)='" + itemHeadcode + "' and SUBSTRING(ItemCode,8,3)='" + itemSubHeadcode + "' and Record_Deleted ='0' and Local_Import = '0'");
                string str = ds.Tables[0].Rows[0]["code"].ToString();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["code"].ToString() != "")
                    {
                        float inc_Code = float.Parse(ds.Tables[0].Rows[0]["code"].ToString());
                       // if (itemHeadcode > 0)
                       // {
                            inc_Code = inc_Code + 1;
                            string rdb_l_i = "";
                            if (rdbLocal.Checked == true)
                            {
                                rdb_l_i = "0";
                            }
                           
                            txtItemCode.Text = itemtypecode.ToString("00") + "-" + itemHeadcode + "-" + itemSubHeadcode + "-" + inc_Code.ToString("000000") + "-" + rdb_l_i;
                       // }
                    }
                    else
                    {
                        //    if (itemHeadcode > 0)
                        //    {

                        string rdb_l_i = "";
                        if (rdbLocal.Checked == true)
                        {
                            rdb_l_i = "0";
                        }

                        txtItemCode.Text = itemtypecode.ToString("00") + "-" + itemHeadcode + "-" + itemSubHeadcode + "-000001" + "-" + rdb_l_i;
                        //    }
                    }
                }
                else
                {
                    //if (itemHeadcode > 0)
                 //   {

                        string rdb_l_i = "";
                        if (rdbLocal.Checked == true)
                        {
                            rdb_l_i = "0";
                        }
                       
                        txtItemCode.Text = itemtypecode.ToString("00") + "-" + itemHeadcode + "-" + itemSubHeadcode + "-000001" + "-" + rdb_l_i;
                   // }
                }
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }

        }
        
    }
    public string itemcodelocalgenerate()
    {
        if (rdbLocal.Checked == true)
        {
            try
            {
                float itemtypecode = 0, itemHeadcode = 0, itemSubHeadcode = 0;

                if (ddlItemTypes.SelectedIndex != 0)
                {
                    itemtypecode = float.Parse(ddlItemTypes.SelectedItem.Value);
                }
                if (ddlItemsHead.SelectedIndex != 0)
                {
                    itemHeadcode = float.Parse(ddlItemsHead.SelectedItem.Value);
                }
                if (ddlItemSubHead.SelectedIndex != 0)
                {
                    itemSubHeadcode = float.Parse(ddlItemSubHead.SelectedItem.Value);
                }
                else
                {
                    itemSubHeadcode = 0;
                }
                DataSet ds = dml.Find("select MAX(SUBSTRING(ItemCode,12,6)) as code from SET_ItemMaster where SUBSTRING(ItemCode, 1, 2) = '" + itemtypecode.ToString("00") + "' and substring(ItemCode,4,3)='" + itemHeadcode.ToString("000") + "' and SUBSTRING(ItemCode,8,3)='" + itemSubHeadcode.ToString("000") + "' and Record_Deleted ='0' and Local_Import = '0'");
                string str = ds.Tables[0].Rows[0]["code"].ToString();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["code"].ToString() != "")
                    {
                        float inc_Code = float.Parse(ds.Tables[0].Rows[0]["code"].ToString());
                        if (itemHeadcode > 0)
                        {
                            inc_Code = inc_Code + 1;
                            string rdb_l_i = "";
                            if (rdbLocal.Checked == true)
                            {
                                rdb_l_i = "0";
                            }

                            txtItemCode.Text = itemtypecode.ToString("00") + "-" + itemHeadcode.ToString("000") + "-" + itemSubHeadcode.ToString("000") + "-" + inc_Code.ToString("000000") + "-" + rdb_l_i;
                            return txtItemCode.Text;
                        }
                    }
                    else
                    {
                        if (itemHeadcode > 0)
                        {

                            string rdb_l_i = "";
                            if (rdbLocal.Checked == true)
                            {
                                rdb_l_i = "0";
                            }

                            txtItemCode.Text = itemtypecode.ToString("00") + "-" + itemHeadcode.ToString("000") + "-" + itemSubHeadcode.ToString("000") + "-000001" + "-" + rdb_l_i;
                            return txtItemCode.Text;
                        }
                    }
                }
                else
                {
                    if (itemHeadcode > 0)
                    {

                        string rdb_l_i = "";
                        if (rdbLocal.Checked == true)
                        {
                            rdb_l_i = "0";
                        }

                        txtItemCode.Text = itemtypecode.ToString("00") + "-" + itemHeadcode.ToString("000") + "-" + itemSubHeadcode.ToString("000") + "-000001" + "-" + rdb_l_i;
                        return txtItemCode.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
             return txtItemCode.Text;

        }
        return "";
    }

    public string itemcodeImportgenerate()
    {
        if (rdbImport.Checked == true)
        {
            try
            {
                float itemtypecode = 0, itemHeadcode = 0, itemSubHeadcode = 0;

                if (ddlItemTypes.SelectedIndex != 0)
                {
                    itemtypecode = float.Parse(ddlItemTypes.SelectedItem.Value);
                }
                if (ddlItemsHead.SelectedIndex != 0)
                {
                    itemHeadcode = float.Parse(ddlItemsHead.SelectedItem.Value);
                }
                if (ddlItemSubHead.SelectedIndex != 0)
                {
                    itemSubHeadcode = float.Parse(ddlItemSubHead.SelectedItem.Value);
                }
                else
                {
                    itemSubHeadcode = 0;
                }
                DataSet ds = dml.Find("select MAX(SUBSTRING(ItemCode,12,6)) as code from SET_ItemMaster where SUBSTRING(ItemCode, 1, 2) = '" + itemtypecode.ToString("00") + "' and substring(ItemCode,4,3)='" + itemHeadcode.ToString("000") + "' and SUBSTRING(ItemCode,8,3)='" + itemSubHeadcode.ToString("000") + "' and Record_Deleted ='0' and Local_Import = '1'");
                string str = ds.Tables[0].Rows[0]["code"].ToString();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["code"].ToString() != "")
                    {
                        float inc_Code = float.Parse(ds.Tables[0].Rows[0]["code"].ToString());
                        if (itemHeadcode > 0)
                        {
                            inc_Code = inc_Code + 1;
                            string rdb_l_i = "";
                            if (rdbImport.Checked == true)
                            {
                                rdb_l_i = "1";
                            }

                            txtItemCode.Text = itemtypecode.ToString("00") + "-" + itemHeadcode.ToString("000") + "-" + itemSubHeadcode.ToString("000") + "-" + inc_Code.ToString("000000") + "-" + rdb_l_i;
                            return txtItemCode.Text;
                        }
                    }
                    else
                    {
                        if (itemHeadcode > 0)
                        {

                            string rdb_l_i = "";

                            if (rdbImport.Checked == true)
                            {
                                rdb_l_i = "1";
                            }
                            txtItemCode.Text = itemtypecode.ToString("00") + "-" + itemHeadcode.ToString("000") + "-" + itemSubHeadcode.ToString("000") + "-000001" + "-" + rdb_l_i;
                            return txtItemCode.Text;
                        }
                    }
                }
                else
                {
                    if (itemHeadcode > 0)
                    {

                        string rdb_l_i = "";

                        if (rdbImport.Checked == true)
                        {
                            rdb_l_i = "1";
                        }
                        txtItemCode.Text = itemtypecode.ToString("00") + "-" + itemHeadcode.ToString("000") + "-" + itemSubHeadcode.ToString("000") + "-000001" + "-" + rdb_l_i;
                        return txtItemCode.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }

        }
        return "";
    }

    protected void rdbImport_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbImport.Checked == true)
        {
            try
            {
                float itemtypecode = 0;
                string itemHeadcode = "000", itemSubHeadcode = "000";

                if (ddlItemTypes.SelectedIndex != 0)
                {
                    itemtypecode = float.Parse(ddlItemTypes.SelectedItem.Value);

                }
                if (ddlItemsHead.SelectedIndex != 0)
                {
                    //  itemHeadcode = float.Parse(ddlItemsHead.SelectedItem.Value);

                    DataSet dsitemHeadcode = dml.Find("select ItemHeadCode from SET_ItemHead where ItemHeadID= '" + ddlItemsHead.SelectedItem.Value + "';");
                    if (dsitemHeadcode.Tables[0].Rows.Count > 0)
                    {
                        itemHeadcode = dsitemHeadcode.Tables[0].Rows[0]["ItemHeadCode"].ToString();
                    }
                    else
                    {
                        itemHeadcode = "001";
                    }

                }
                if (ddlItemSubHead.SelectedIndex != 0)
                {
                    //  itemSubHeadcode = float.Parse(ddlItemSubHead.SelectedItem.Value);

                    DataSet ds1 = dml.Find("select ItemsubHeadCode from SET_ItemSubHead where ItemSubHeadID= '" + ddlItemSubHead.SelectedItem.Value + "';");
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        itemSubHeadcode = ds1.Tables[0].Rows[0]["ItemsubHeadCode"].ToString();
                    }
                    else
                    {
                        itemSubHeadcode = "001";
                    }

                }

                DataSet ds = dml.Find("select MAX(SUBSTRING(ItemCode,12,6)) as code from SET_ItemMaster where SUBSTRING(ItemCode, 1, 2) = '" + itemtypecode.ToString("00") + "' and substring(ItemCode,4,3)='" + itemHeadcode + "' and SUBSTRING(ItemCode,8,3)='" + itemSubHeadcode + "' and Record_Deleted ='0' and Local_Import = '1'");
                string str = ds.Tables[0].Rows[0]["code"].ToString();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["code"].ToString() != "")
                    {
                        float inc_Code = float.Parse(ds.Tables[0].Rows[0]["code"].ToString());
                        // if (itemHeadcode > 0)
                        // {
                        inc_Code = inc_Code + 1;
                        string rdb_l_i = "";
                        if (rdbImport.Checked == true)
                        {
                            rdb_l_i = "1";
                        }

                        txtItemCode.Text = itemtypecode.ToString("00") + "-" + itemHeadcode + "-" + itemSubHeadcode + "-" + inc_Code.ToString("000000") + "-" + rdb_l_i;
                        // }
                    }
                    else
                    {
                        //    if (itemHeadcode > 0)
                        //    {

                        string rdb_l_i = "";
                        if (rdbImport.Checked == true)
                        {
                            rdb_l_i = "1";
                        }

                        txtItemCode.Text = itemtypecode.ToString("00") + "-" + itemHeadcode + "-" + itemSubHeadcode + "-000001" + "-" + rdb_l_i;
                        //    }
                    }
                }
                else
                {
                    //if (itemHeadcode > 0)
                    //   {

                    string rdb_l_i = "";
                    if (rdbLocal.Checked == true)
                    {
                        rdb_l_i = "1";
                    }

                    txtItemCode.Text = itemtypecode.ToString("00") + "-" + itemHeadcode + "-" + itemSubHeadcode + "-000001" + "-" + rdb_l_i;
                    // }
                }
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }


        }
    }

    protected void ddlItemTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            if (ddlItemTypes.SelectedIndex != 0)
            {

                i = float.Parse(ddlItemTypes.SelectedItem.Value);

                txtItemCode.Text = i.ToString("00") + "-000-000-000000-0";
                ddlItemsHead.Enabled = true;
                ddlItemsHead.ClearSelection();
                ddlItemSubHead.ClearSelection();
                ddlItemsHead.Enabled = true;



                dml.dropdownsql(ddlItemsHead, "SET_ItemHead", "ItemHeadName", "ItemHeadID", "Record_Deleted", "0", "ItemTypeID", ddlItemTypes.SelectedItem.Value);
            }
           
        }
    }
    protected void ddlItemsHead_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlItemsHead.SelectedIndex != 0)
        {
            string head = "0";//float.Parse(ddlItemsHead.SelectedItem.Value);
            i = float.Parse(ddlItemTypes.SelectedItem.Value);

            DataSet ds = dml.Find("select ItemHeadCode from SET_ItemHead where ItemHeadID= '" + ddlItemsHead.SelectedItem.Value + "';");
            if (ds.Tables[0].Rows.Count > 0)
            {
                head = ds.Tables[0].Rows[0]["ItemHeadCode"].ToString();
            }
            else
            {
                head = "001";
            }

            txtItemCode.Text = i.ToString("00") + "-" + head + "-000-000000-0";

            dml.dropdownsql(ddlItemSubHead, "SET_ItemSubHead", "ItemSubHeadName", "ItemSubHeadID", "Record_Deleted", "0", "ItemHeadID", ddlItemsHead.SelectedItem.Value);
            ddlItemSubHead.Enabled = true;

            if(rdbLocal.Checked == true)
            {
                rdbLocal_CheckedChanged(sender, e);
            }
            if(rdbImport.Checked == true)
            {
                rdbImport_CheckedChanged(sender, e);
            }


        }
    }
    protected void ddlItemSubHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlItemSubHead.SelectedIndex != 0)
        {
            string head = "0";// float.Parse(ddlItemsHead.SelectedItem.Value);
            i = float.Parse(ddlItemTypes.SelectedItem.Value);
            string subhead = "0"; //float.Parse(ddlItemSubHead.SelectedItem.Value);

            DataSet ds = dml.Find("select ItemHeadCode from SET_ItemHead where ItemHeadID= '" + ddlItemsHead.SelectedItem.Value + "';");
            if (ds.Tables[0].Rows.Count > 0)
            {
                head = ds.Tables[0].Rows[0]["ItemHeadCode"].ToString();
            }
            else
            {
                head = "001";
            }


            DataSet ds1 = dml.Find("select ItemsubHeadCode from SET_ItemSubHead where ItemSubHeadID= '" + ddlItemSubHead.SelectedItem.Value + "';");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                subhead = ds1.Tables[0].Rows[0]["ItemsubHeadCode"].ToString();
            }
            else
            {
                subhead = "001";
            }




            txtItemCode.Text = i.ToString("00") + "-" + head + "-" + subhead + "-000000-0";

            if (rdbLocal.Checked == true)
            {
                rdbLocal_CheckedChanged(sender, e);
            }
            if (rdbImport.Checked == true)
            {
                rdbImport_CheckedChanged(sender, e);
            }
        }
    }

    protected void rdbConsumable_CheckedChanged(object sender, EventArgs e)
    {
        if(rdbConsumable.Checked == true)
        {
            rdbAsset.BackColor = System.Drawing.Color.Transparent;
            rdbExpense.BackColor = System.Drawing.Color.Transparent;
            rdbConsumable.BackColor = System.Drawing.Color.Transparent;
            Label1.Text = "";
        }
    }

    protected void rdbExpense_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbExpense.Checked == true)
        {
            rdbAsset.BackColor = System.Drawing.Color.Transparent;
            rdbExpense.BackColor = System.Drawing.Color.Transparent;
            rdbConsumable.BackColor = System.Drawing.Color.Transparent;
            Label1.Text = "";
        }
    }

    protected void rdbAsset_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbAsset.Checked == true)
        {
            rdbAsset.BackColor = System.Drawing.Color.Transparent;
            rdbExpense.BackColor = System.Drawing.Color.Transparent;
            rdbConsumable.BackColor = System.Drawing.Color.Transparent;
            Label1.Text = "";
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string id = e.Row.Cells[1].Text;
        bool flag = dml.isNumber(id);


        if (flag == true)
        {

            DataSet ds = dml.Find("select ItemID,MLD from SET_ItemMaster where ItemID = '" + id + "'");
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

            DataSet ds = dml.Find("select ItemID,MLD from SET_ItemMaster where ItemID = '" + id + "'");
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