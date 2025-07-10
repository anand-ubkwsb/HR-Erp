using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class dynamicfrm : System.Web.UI.Page
{

    TextBox S_userid, S_LoginName, S_Displayarea;
    TextBox txtuser;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());

    DataSet ds = new DataSet();


    public enum MessageType { Success, Error, Info, Warning };
    string userid, UserGrpID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            SqlDataAdapter da = new SqlDataAdapter("Select TABLE_NAME from INFORMATION_SCHEMA.TABLES", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataBind();
            DropDownList1.DataTextField = "TABLE_NAME";
            DropDownList1.DataBind();
        }


        //myalertsuccess.Visible = false;
         
        userid = Request.QueryString["UserID"];
        UserGrpID = Request.QueryString["UsergrpID"];
        

        // CreateDynamicForm();

    }

    
    public void CreateDynamicForm()
    {
        StringBuilder Html = new StringBuilder();
        DataTable dt = new DataTable();
        dt = cusField();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                HtmlGenericControl tr = new HtmlGenericControl("tr");
                HtmlGenericControl td = new HtmlGenericControl("td");
                HtmlGenericControl td1 = new HtmlGenericControl("td");

                String FieldName = Convert.ToString(dt.Rows[i]["FieldName"]);
                String FieldType = Convert.ToString(dt.Rows[i]["FieldType"]);
                String FieldValue = Convert.ToString(dt.Rows[i]["FieldValue"]);

                Label lblcustname = new Label();
                lblcustname.ID = "lbl" + FieldName;
                lblcustname.Text = FieldName;
                lblcustname.Attributes.Add("class", "col-md-2");
                td.Attributes.Add("style", "padding-bottom:10px; text-align:right");
                td.Controls.Add(lblcustname);
                tr.Controls.Add(td);


                if (FieldType.ToLower().Trim() == "Textbox")
                {
                    TextBox txtcustBox = new TextBox();
                    txtcustBox.Attributes.Add("class", "form-control col-md-10");
                    txtcustBox.ID = "txt" + FieldName;
                    txtcustBox.Text = FieldValue;
                    td1.Controls.Add(txtcustBox);
                    td1.Attributes.Add("style", "padding-bottom:10px;padding-left:10px; text-align:left");


                }

                tr.Controls.Add(td1);



                tr = new HtmlGenericControl("tr");
                td = new HtmlGenericControl("td");
                Button btnsubmit = new Button();
                btnsubmit.ID = "btnSubmit";
                // btnsubmit.Click += btnsubmit_Click();
                btnsubmit.OnClientClick = "return ValidateForm();";
                btnsubmit.Text = "Submit";
                td.Controls.Add(btnsubmit);
                td.Attributes.Add("Colspan", "2");
                td.Attributes.Add("style", "text-align:center;");
                tr.Controls.Add(td);
                //    PlaceHolder1.Controls.Add(tr);

            }
        }
    }
    public DataTable cusField()
    {
        DataTable dt = new DataTable();
        dt = new DataTable();
        dt.Columns.Add("FieldName", typeof(string));
        dt.Columns.Add("FieldType", typeof(string));
        dt.Columns.Add("FieldValue", typeof(string));

        dt.Rows.Add("FirstName", "Textbox", "my name");
        dt.Rows.Add("LastName", "Textbox", String.Empty);
        dt.Rows.Add("City", "Textbox", String.Empty);
        dt.Rows.Add("Country", "Textbox", String.Empty);
        dt.Rows.Add("City", "Textbox", String.Empty);
        dt.Rows.Add("Country", "Textbox", String.Empty);
        dt.Rows.Add("City", "Textbox", String.Empty);
        dt.Rows.Add("Country", "Textbox", String.Empty);
        return dt;
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        // Label2.Visible = false;
    }
    protected void ShowMessage(string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
    }
    public void table_frm()
    {
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("select TABLE_NAME,COLUMN_NAME,DATA_TYPE from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME= '" + DropDownList1.SelectedItem.Text + "'", con);
        da.Fill(ds);


        int fieldvalues = ds.Tables[0].Rows.Count;


        HtmlTableRow row = new HtmlTableRow();
        HtmlTableCell cell = new HtmlTableCell();
       

        row = new HtmlTableRow();
        for (int i = 0; i < fieldvalues; i++)
        {
            if (i % 2 == 0)
            {
                row = new HtmlTableRow();

            }

            cell = new HtmlTableCell();
            cell.Attributes.Add("style", "font-weight:bold");
            string txtlabel= ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
            txtlabel.ToUpper();
            cell.InnerText = txtlabel.Replace("_", " ");
            row.Cells.Add(cell);


            cell = new HtmlTableCell();
            string field_type = ds.Tables[0].Rows[i]["DATA_TYPE"].ToString();

            //if (field_type.ToLower() == "varchar" || field_type.ToLower() == "numeric" || field_type.ToLower() == "date" || field_type.ToLower() == "datetime2")
            {
                
                txtuser = new TextBox();
                txtuser.Attributes.Add("class", "form-control");
                cell.Width = "250px;";
                cell.Attributes.Add("style", "padding-bottom:15px;");
                txtuser.ID = ds.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
                txtuser.ID.ToUpper();


                txtuser.Attributes.Add("placeholder", (txtuser.ID.Replace("_", " ")));
                cell.Controls.Add(txtuser);
                row.Cells.Add(cell);

            }

            



            cell = new HtmlTableCell();

            cell.InnerText = "";
            cell.Attributes.Add("style", "padding-bottom:15px;");
            cell.Width = "150px;";
            row.Cells.Add(cell);
            tablecontent1.Rows.Add(row);


        }


        
        fieldhide();
        da.Dispose();
        con.Close();
    }

    protected void btnInsert_Click1(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_fieldConnectionString"].ConnectionString.ToString());
        con.Open();

        SqlDataAdapter da = new SqlDataAdapter("select * from tbl_frmhide_field where Userid = '" + userid + "' and hide = 'N'", con);
        DataSet ds = new DataSet();


        da.Fill(ds);

        string insertfield = "";
        for (int a = 0; a < ds.Tables[0].Rows.Count; a++)
        {
            if (a == ds.Tables[0].Rows.Count - 1)
            {
                insertfield += ds.Tables[0].Rows[a]["field_name"].ToString();
            }
            else {
                insertfield += ds.Tables[0].Rows[a]["field_name"].ToString() + ",";
            }
        }




        string query = "insert into tbl_crudtable(" + insertfield + ") values(";

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (i == ds.Tables[0].Rows.Count - 1)
            {
                string field = ds.Tables[0].Rows[i]["field_id"].ToString();
                query += "'" + ((HtmlInputText)tablecontent1.FindControl(field)).Value + "'" + ")";

            }
            else {
                string field = ds.Tables[0].Rows[i]["field_id"].ToString();
                query += "'" + ((HtmlInputText)tablecontent1.FindControl(field)).Value + "'" + ",";
            }
        }

        SqlCommand cmd = new SqlCommand(query, con);
        cmd.ExecuteNonQuery();
        con.Close();
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {

        TextBox1.AutoPostBack = true;
        TextBox1.Text = "sdsads";
        //ddd();
    }

    public void ddp_field()
    {
        ddlFieldType.Items.Insert(0, "Please Select..");
        ddlFieldType.Items.Insert(1, "TextBox");
        ddlFieldType.Items.Insert(2, "CheckBox");
        ddlFieldType.Items.Insert(3, "Label");
        ddlFieldType.Items.Insert(4, "DropDownList");
        ddlFieldType.Items.Insert(5, "Button");
    }

    protected void btn_modal_add_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db_fieldConnectionString"].ConnectionString.ToString());
        con.Open();
        string chk = "";
        if (rbtn_Hide_Y.Checked == true)
        {
            chk = "Y";
        }
        if (rbtn_Hide_No.Checked == true)
        {
            chk = "N";
        }

        SqlCommand cmd = new SqlCommand("insert into tbl_frmhide_field values ('" + txtfieldname.Text + "','" + txtField_ID.Text + "','" + ddlFieldType.Text + "','2','1','" + chk + "')", con);

        cmd.ExecuteNonQuery();

        //lblMSg.Text = "Field Added Successfully!!..";
        Response.Redirect(Request.RawUrl.ToString());
        ShowMessage("Record submitted successfully", MessageType.Success);
        con.Close();

    }

    protected void btn_modal(object sender, EventArgs e)
    {

    }

    protected void ddlFieldType_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlFieldType.Text == "TextBox")
        {
            txtField_ID.Text = "txt" + txtfieldname.Text;
        }
        else if (ddlFieldType.Text == "CheckBox")
        {
            txtField_ID.Text = "chk" + txtfieldname.Text;
        }
        else if (ddlFieldType.Text == "Label")
        {
            txtField_ID.Text = "lbl" + txtfieldname.Text;
        }
        else if (ddlFieldType.Text == "DropDownList")
        {
            txtField_ID.Text = "ddl" + txtfieldname.Text;
        }
        else if (ddlFieldType.Text == "Button")
        {
            txtField_ID.Text = "btn" + txtfieldname.Text;
        }
        else
        {
            txtField_ID.Text = "Please select Field Type";
        }

    }

    public string ddd()
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString.ToString());
        con1.Open();
        SqlDataAdapter da = new SqlDataAdapter("select * from SET_User_Manager where UserId = convert(uniqueidentifier,'359B6016-315C-4F0B-83DF-CDBFCC4206F9') ", con1);
        DataSet ds = new DataSet();
        da.Fill(ds);

        S_LoginName.Text = ds.Tables[0].Rows[0]["user_name"].ToString();
        // txtcheck.Text 
        //Response.Redirect(Request.RawUrl.ToString());
        con1.Close();
        return S_LoginName.Text;
    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        //    TextBox txtuserid = tablecontent1.FindControl("txtSystem_Date") as TextBox;
        //    TextBox txtLoginname = tablecontent1.FindControl("txtSystem_Date") as TextBox;
        //    TextBox txtpass = tablecontent1.FindControl("txtSystem_Date") as TextBox;
        //    TextBox txtRemarks = tablecontent1.FindControl("txtSystem_Date") as TextBox;
        //    TextBox txtDepartment = tablecontent1.FindControl("txtSystem_Date") as TextBox;


        // SqlDataAdapter das = new SqlDataAdapter("select * from tbl_frmhide_field where Userid = '" + userid + "' and hide = 'N'", con);

        //das.Fill(ds);

        DataSet myds = GetData("select * from tbl_frmhide_field where Userid = '" + userid + "' and hide = 'N'");

        int fieldvalues = myds.Tables[0].Rows.Count;
        for (int i = 0; i < fieldvalues; i++)
        {
            string fielsid = ds.Tables[0].Rows[i]["field_id"].ToString();
            TextBox txtc = tablecontent1.FindControl(fielsid) as TextBox;
            Label lbl = tablecontent1.FindControl(fielsid) as Label;
            if (txtc is TextBox)
            {
             //   DataSet dsf = GetData("")
                if (fielsid == txtc.ID)
                {
                    txtc.Text = TextBox1.Text;
                }
            }
            else {
                if (lbl is Label)
                {
                    lbl.Text = TextBox1.Text;
                }
            }
        }
        btn_message("Display successfully ");
    }

    private DataSet GetData(string query)
    {
        DataSet dsa = new DataSet();
        string constr = ConfigurationManager.ConnectionStrings["db_fieldConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(dsa);
                }
            }
            return dsa;
        }
    }

    //TextBox t1 = tablecontent1.FindControl("txtSystem_Date") as TextBox;

    //t1.Text = TextBox1.Text;


    public void btn_message(string mess)
    {

        StringBuilder sb = new StringBuilder();
           sb.Append("<div class='alert alert-success' id='success-alert'>");
        sb.Append(" <button type ='button' class='close' data-dismiss='alert'>x</button>");
        sb.Append("<strong>Success! </strong>");
        sb.Append(mess);
        sb.Append("</div>");

        placeholder1.Controls.Add(new Literal { Text = sb.ToString() });

        Response.Write("<script> window.setTimeout(function() { $('.alert').fadeTo(1000, 0).slideUp(1000, function() { $(this).remove(); }); }, 2000); </script>");

    }

    protected void btn_script_Click(object sender, EventArgs e)
    {
        //Response.Write("<script> swal('Good job!', 'You clicked the button!', 'success');</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "randomtext", " alertme()", true);
        
    }

    public void fieldhide()
    {
        SqlDataAdapter da1 = new SqlDataAdapter("select field_name from SET_UserGrp_FieldHide where FormId in (select FormId from SET_UserGrp_FieldHide where UserGrpId = '"+ UserGrpID + "') and table_name = '" + DropDownList1.SelectedItem.Text + "'", con);
        DataSet ds_FHide = new DataSet();
        da1.Fill(ds_FHide);
        int f_Count = ds_FHide.Tables[0].Rows.Count;


        if (f_Count == 0)
        {
            fieldUnhide_All();
        }
        else {

            for (int y = 0; y <= f_Count - 1; y++)
            {
                string fielsid = ds_FHide.Tables[0].Rows[y]["field_name"].ToString();
                TextBox txtc = tablecontent1.FindControl(fielsid) as TextBox;
                Label lbl = tablecontent1.FindControl(fielsid) as Label;
                if (txtc is TextBox)
                {
                    //   DataSet dsf = GetData("")
                    if (fielsid == txtc.ID)
                    {
                        txtc.Attributes.Add("class", "form-control");
                        txtc.Enabled = false;
                    }

                }
                else if (lbl is Label)

                {
                    lbl.Enabled = false;
                }
                else
                {
                    txtc.Enabled = true;
                    lbl.Enabled = true;
                }
            }

            da1.Dispose();
        }
    }


    public void fieldUnhide_All()
    {
        SqlDataAdapter da1 = new SqlDataAdapter("select TABLE_NAME,COLUMN_NAME,DATA_TYPE from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME= '" + DropDownList1.SelectedItem.Text + "'", con);
        DataSet ds_FHide = new DataSet();
        da1.Fill(ds_FHide);
        int f_Count = ds_FHide.Tables[0].Rows.Count;
        for (int i = 0; i <= f_Count - 1; i++)
        {
            string fielsid = ds_FHide.Tables[0].Rows[i]["COLUMN_NAME"].ToString();
            TextBox txtc = tablecontent1.FindControl(fielsid) as TextBox;
            Label lbl = tablecontent1.FindControl(fielsid) as Label;
            if (txtc is TextBox)
            {

                txtc.Enabled = true;

            }

            else if (lbl is Label)
            {
                lbl.Enabled = true;
            }

        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

           // Label1.Text = DropDownList1.SelectedItem.Value;
            SqlDataAdapter da = new SqlDataAdapter("select TABLE_NAME,COLUMN_NAME,DATA_TYPE from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME= '" + DropDownList1.SelectedItem.Text + "' ", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DropDownList2.DataSource = dt;
            DropDownList2.DataBind();
            DropDownList2.DataTextField = "COLUMN_NAME";
            DropDownList2.DataBind();
            da.Dispose();
            string l = DropDownList1.SelectedItem.Text;
             l =  l.Replace("_", " ");
            l = l.ToUpper();
            lblFrom_Title.Text = l;
            lblFrom_Title.Text.ToUpper();
            table_frm();

        }
        catch (Exception ex)
        {

        }

    }
}
    
