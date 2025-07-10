using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PasswordChange : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString);
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (txtLoginName.Text != "")
        {

            SqlDataAdapter da = new SqlDataAdapter("select last_pwd,pwd from SET_USER_MANAGER WHERE LoginName = '" + txtLoginName.Text + "'", con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            string oldp = ds.Tables[0].Rows[0]["last_pwd"].ToString();
            string oldpmatch = ds.Tables[0].Rows[0]["pwd"].ToString();


            if (oldp == txtNewPwd.Text)
            {
                lblmss.Text = "do not use old password";
            }
            else if (txtOldpwd.Text == "")
            {
                lblmss.Text = "please enter old password";
            }
            else if (txtOldpwd.Text != oldpmatch)
            {
                lblmss.Text = "Old Password doesn't match. Please enter correct password";
            }
            else {

                int g = 0;
                string a = txtOldpwd.Text;
                string b = txtNewPwd.Text;
                for (int i = 1; i <= a.Length - 1; i++)
                {
                    if (a.Substring(0, i) == b.Substring(0, i))
                    {
                        g = g + 1;
                    }
                }

                if (g >= 3)
                {
                    lblmss.ForeColor = System.Drawing.Color.Red;
                    lblmss.Text = "3 character are match please enter different password";
                    txtNewPwd.Text = "";
                    txtOldpwd.Text = "";
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        newpwd(txtLoginName.Text);
                        resetpwdvalues(txtLoginName.Text);
                    }
                    lblmss.Text = "Password has been changed!!";
                    Response.Redirect("frmlogin.aspx");
                }

            }
        }
        else
        {
            lblmss.ForeColor = System.Drawing.Color.Red;
            lblmss.Text = "Please enter the login name";
        }
        
    }
    

    public void newpwd(string loginn)
    {
        con.Close();
        con.Open();
        SqlCommand cmd1 = new SqlCommand("Update SET_User_Manager Set pwd = '"+txtNewPwd.Text+"', last_pwd='"+txtOldpwd.Text+"'  where LoginName='" + loginn + "'", con);
        cmd1.ExecuteNonQuery();
        con.Close();
    }
    public void resetpwdvalues(string loginn)
    {
        con.Close();
        con.Open();
        SqlCommand cmd1 = new SqlCommand("Update SET_User_Manager Set ForcePwdChange = 0 , PwdChangeCounter = 0 , last_pwd_date = '" + DateTime.Now.Date.ToShortDateString() +"' where LoginName='" + loginn + "'", con);
        cmd1.ExecuteNonQuery();
        con.Close();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        
    }
    public void charcount()
    {
        int g = 0;
        string a = txtOldpwd.Text;
        string b = txtNewPwd.Text;
        for (int i = 1; i <= a.Length - 1; i++)
        {
            if (a.Substring(0, i) == b.Substring(0, i))
            {
                g = g + 1;
            }
        }

        if (g >= 3)
        {
            lblmss.ForeColor = System.Drawing.Color.Red;
            lblmss.Text = "3 character are match please enter different password";
            txtNewPwd.Text = "";
            txtOldpwd.Text = "";
        }
        else
        {
            lblmss.Text = "change password";
        }
    }
}