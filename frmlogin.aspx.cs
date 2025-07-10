using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Net;
using System.Net.NetworkInformation;
using System.Web.Services;

public partial class frmlogin : System.Web.UI.Page
{
   
    DmlOperation dml = new DmlOperation();
    SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["db_HRERPSys"].ConnectionString);
    public string loginname_count;
    public string logina;
    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["UserId"] = "";
        if (!IsPostBack)
        {

            if (Request.Cookies["LoginName"] != null)

                txtLoginname.Value = Request.Cookies["LoginName"].Value;
                Image1.ImageUrl = picpath();

            if (Request.Cookies["pwd"] != null)

                txtPwd.Attributes.Add("value", Request.Cookies["pwd"].Value);
            if (Request.Cookies["LoginName"] != null && Request.Cookies["pwd"] != null)
            {
                chk_remember.Checked = true;
            }

            

        }

        if (Session["userid"] != null)
        {

            Response.Redirect("User_Co_Unit_Br_Fyear.aspx?UserID=" + Session["userid"].ToString() + "&UsergrpID=" + Session["usergrpid"].ToString());
        }
        else
        {

        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)

    {
       
       //string Hashpass = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPwd.Text, "sha1");
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("select * from SET_User_Manager where LoginName = '" + txtLoginname.Value.ToString() + "' and Record_Deleted = '0'", con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        
        //  SqlDataReader re = cmd.ExecuteReader();
        //ds.Tables[0].Rows[0][""].ToString();
        if (ds.Tables[0].Rows.Count > 0 )
        {
        //   while (re.Read())
            {
                string psd = ds.Tables[0].Rows[0]["pwd"].ToString();
                string id = ds.Tables[0].Rows[0]["UserId"].ToString();
                ViewState["UserId"] = id;
                string loginname = ds.Tables[0].Rows[0]["LoginName"].ToString();
                logina = loginname;
                string isactive = ds.Tables[0].Rows[0]["IsActive"].ToString();
                int wrong_attempt = Convert.ToInt32(ds.Tables[0].Rows[0]["Wrong_attempt"].ToString());
                string loginIp = ds.Tables[0].Rows[0]["login_ip"].ToString();
                int lockuser = Convert.ToInt32(ds.Tables[0].Rows[0]["Lock_user"].ToString());
                bool forcepasswordc =Convert.ToBoolean(ds.Tables[0].Rows[0]["ForcePwdChange"].ToString());
                int pwdchangedays = Convert.ToInt32(ds.Tables[0].Rows[0]["PwdChangeDays"].ToString());
                int pwdchangeCounter = Convert.ToInt32(ds.Tables[0].Rows[0]["PwdChangeCounter"].ToString());
                string last_mac = ds.Tables[0].Rows[0]["Last_Mac_add"].ToString();
                string lastpwddate = ds.Tables[0].Rows[0]["last_pwd_date"].ToString();
                string UserGrpId = ds.Tables[0].Rows[0]["UserGrpId"].ToString();
                string username = ds.Tables[0].Rows[0]["user_name"].ToString();
                if (isactive == "1")
                {
                    if (wrong_attempt < lockuser)
                    {
                        if (ds.Tables[0].Rows[0]["LoginName"].ToString() == txtLoginname.Value.ToString() && ds.Tables[0].Rows[0]["pwd"].ToString() == txtPwd.Value.ToString())
                        {
                            var ip = getip();
                            var mac = getMac();
                            if (loginIp == ip && last_mac == mac)
                            {
                                if (!forcepasswordc)
                                {
                                    if (pwdchangeCounter < pwdchangedays)
                                        {
                                            if (ds.Tables[0].Rows[0]["LoginName"].ToString() == txtLoginname.Value.ToString() && ds.Tables[0].Rows[0]["pwd"].ToString() == txtPwd.Value.ToString())
                                            {
                                                lblMsg.ForeColor = System.Drawing.Color.Black;
                                                DateTime Cdate = DateTime.Now;
                                                lastlogindate(Cdate.Date.ToShortDateString());
                                                
                                                string pwdcounter = dateupdate(lastpwddate, Cdate.Date.ToShortDateString());
                                                  pwd_Count(pwdcounter);

                                            if (chk_remember.Checked == true)
                                            {
                                                Response.Cookies["UserId"].Value = id;
                                                Response.Cookies["LoginName"].Value = txtLoginname.Value.ToString();
                                                Response.Cookies["pwd"].Value = txtPwd.Value.ToString();
                                                
                                                Response.Cookies["UserName"].Value = username.ToString();

                                                Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(15);
                                                Response.Cookies["LoginName"].Expires = DateTime.Now.AddDays(15);
                                                Response.Cookies["pwd"].Expires = DateTime.Now.AddDays(15);

                                            }

                                            else

                                            {

                                                Response.Cookies["LoginName"].Expires = DateTime.Now.AddDays(-1);

                                                Response.Cookies["pwd"].Expires = DateTime.Now.AddDays(-1);

                                            }


                                                        Session["userid"] = id;
                                                      Session["usergrpid"] = UserGrpId;
                                            dml.Update("update SET_User_Manager set Wrong_attempt = '0' where UserId = '" + id + "'", "");
                                            Response.Redirect("User_Co_Unit_Br_Fyear.aspx?UserID=" + id +"&UsergrpID="+UserGrpId);
                                                     lblMsg.Text = "Login Successfully";

                                            }
                                            else
                                            {
                                                if (ds.Tables[0].Rows[0]["pwd"].ToString() != txtPwd.Value.ToString())
                                                {
                                                    wrong_attempt = wrong_attempt + 1;
                                                    update_wrongattempt(wrong_attempt);

                                                }
                                                lblMsg.ForeColor = System.Drawing.Color.Black;
                                                lblMsg.Text = "Inalid Username and password!";
                                                // Response.Write("<script>alert('Inalid Username and password!')</script>");
                                            }
                                        }
                                        else
                                        {
                                            con.Close();
                                            con.Open();
                                            //SqlCommand cmd1 = new SqlCommand("Update SET_User_Manager Set ForcePwdChange = 1 where UserId='"+id+"'", con);
                                            //cmd1.ExecuteNonQuery();
                                            lblMsg.Text = "Your password change duration has been expired \n Please change yuour password ";
                                            con.Close();
                                        }
                                    
                                    

                                }
                                else
                                {
                                    lblMsg.ForeColor = System.Drawing.Color.Red;
                                    lblMsg.Text = "please change your password";
                                }
                            }
                            else
                            {
                                if (loginIp == "" || last_mac == "")
                                {

                                    if (!forcepasswordc)
                                    {
                                        if (pwdchangeCounter < pwdchangedays)
                                        {


                                            if (ds.Tables[0].Rows[0]["LoginName"].ToString() == txtLoginname.Value.ToString() && ds.Tables[0].Rows[0]["pwd"].ToString() == txtPwd.Value.ToString())
                                            {
                                                con.Close();
                                                //con.Open();
                                                //SqlCommand cmd1 = new SqlCommand("Update SET_User_Manager Set Last_Mac_add='" + getMac() + "' where LoginName='" + txtLoginname.Text + "'", con);
                                                //cmd1.ExecuteNonQuery();
                                                //con.Close();
                                                DateTime Cdate = DateTime.Now;
                                                lastlogindate(Cdate.Date.ToShortDateString());

                                                string pwdcounter = dateupdate(lastpwddate, Cdate.Date.ToShortDateString());
                                                pwd_Count(pwdcounter);

                                                if (chk_remember.Checked == true)
                                                {
                                                    Response.Cookies["LoginName"].Value = txtLoginname.Value.ToString();
                                                    Response.Cookies["pwd"].Value = txtPwd.Value.ToString();
                                                    Response.Cookies["UserName"].Value = username.ToString();

                                                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(15);
                                                    Response.Cookies["LoginName"].Expires = DateTime.Now.AddDays(15);
                                                    Response.Cookies["pwd"].Expires = DateTime.Now.AddDays(15);
                                                }

                                                else

                                                {

                                                    Response.Cookies["LoginName"].Expires = DateTime.Now.AddDays(-1);
                                                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                                                    Response.Cookies["pwd"].Expires = DateTime.Now.AddDays(-1);

                                                }


                                                dml.Update("update SET_User_Manager set Wrong_attempt = '0' where UserId = '" + id + "'", "");
                                                Response.Redirect("User_Co_Unit_Br_Fyear.aspx?UserID=" + id + "&UsergrpID=" + UserGrpId);
                                                Response.Write("<script> alert('login successfully!')</script>");
                                            }
                                        }

                                        else
                                        {
                                            con.Close();
                                            con.Open();
                                            SqlCommand cmd1 = new SqlCommand("Update SET_User_Manager Set ForcePwdChange = 1 where UserId='"+ViewState["UserId"]+"'", con);
                                            cmd1.ExecuteNonQuery();
                                            lblMsg.Text = "Your password change duration has been expired \n Please change your password ";
                                            con.Close();
                                        }

                                   }
                                        else
                                        {
                                            lblMsg.ForeColor = System.Drawing.Color.Red;
                                            lblMsg.Text = "please change your password";
                                        }


                                    }
                                else {
                                    lblMsg.ForeColor = System.Drawing.Color.Red;
                                    lblMsg.Text = "IP does not match with your computer";
                                }


                            }

                        }
                        else
                        {
                            wrong_attempt = wrong_attempt + 1;
                            update_wrongattempt(wrong_attempt);
                            lblMsg.Text = "Invalid Login Name and Password ";
                        }
                        //asdasdasdas
                    }
                    else if (wrong_attempt >= lockuser)
                    {
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        lblMsg.Text = "Contact Administrator";
                    }
                }
                else
                {
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = "Not valid User";
                }
                
            }
        }
        else
        {
            lblMsg.Text = "no User found";
        }

        con.Close();

        
    }

    public string getip()
    {
        string host = Dns.GetHostName();
        IPHostEntry ipentry = Dns.GetHostEntry(host);

        foreach (IPAddress ipadd in ipentry.AddressList)
        {
            if (ipadd.AddressFamily.ToString() == "InterNetwork")
            {
                return ipadd.ToString();
            }
        }
        return ".";
    }
    public string getMac()
    {
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        String sMACAdd = string.Empty;

       
        foreach (NetworkInterface adapter in nics)
        {
            if (sMACAdd == string.Empty)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                sMACAdd = adapter.GetPhysicalAddress().ToString();
            }
        }
        sMACAdd = sMACAdd.Replace(":", "");
        return sMACAdd;
    }
    public void update_wrongattempt(int wa)
    {
        con.Close();
        con.Open();
        SqlCommand cmd1 = new SqlCommand("Update SET_User_Manager Set Wrong_attempt='" + wa + "'  where UserId='" + ViewState["UserId"] + "'", con);
        cmd1.ExecuteNonQuery();
        con.Close();

    }

    protected void btnMac_Click(object sender, EventArgs e)
    {
        DateTime now = DateTime.Now;
        
        lblMsg.Text = now.Date.ToShortDateString();
        sendval();
    }

    public void lastlogindate(string date)
    {
        con.Close();
        con.Open();
        SqlCommand cmd1 = new SqlCommand("Update SET_User_Manager Set last_Login='" + date + "' , Last_Ip = '" + getip() + "'  where UserId='" + ViewState["UserId"] + "'", con);
        cmd1.ExecuteNonQuery();
        con.Close();
    }
    protected void sendval()
    {
        HttpCookie Cookie = new HttpCookie("login_name");
        Cookie.Value = txtLoginname.Value.ToString();
        Cookie.Expires = DateTime.Now.AddMinutes(1);
        Response.Cookies.Add(Cookie);
        Response.Redirect("PasswordChange.aspx");

    }

    public string dateupdate(string lstpwddate , string currentdate)
    {
        con.Close();
        con.Open();
        SqlDataAdapter cmd1 = new SqlDataAdapter("SELECT DATEDIFF(day, '"+ lstpwddate + "', '"+ currentdate + "') AS DateDiff", con);
        DataSet ds = new DataSet();
        cmd1.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string finaldate = ds.Tables[0].Rows[0]["DateDiff"].ToString();
            return finaldate;
        }
        return "";
    }


    public void pwd_Count(string fd)
    {
        con.Close();
        con.Open();
        SqlCommand cmd1 = new SqlCommand("Update SET_User_Manager Set PwdChangeCounter='" + fd + "'  where UserId='" + ViewState["UserId"] + "'", con);
        cmd1.ExecuteNonQuery();
        con.Close();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
       
    }

    protected void txtLoginname_TextChanged(object sender, EventArgs e)
    {

        Image1.ImageUrl = picpath();
    }

    public string picpath()
    {
        string path = "";
        con.Open();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT pic_path from SET_User_Manager where UserId='" + ViewState["UserId"] + "'", con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                path = ds.Tables[0].Rows[0]["pic_path"].ToString();
            }

        }
        catch (Exception ex)
        {
            ex.GetBaseException();
        }
        con.Close();
        return path;
    }


   
}