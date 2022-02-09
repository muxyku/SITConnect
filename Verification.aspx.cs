using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SITConnect
{
    public partial class Verification : System.Web.UI.Page
    {

        string SITConnectDBConnectionString =
        System.Configuration.ConfigurationManager.ConnectionStrings["SITConnectDBConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedIn"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
            {
                if (Session["LoggedIn"] != null)
                {

                }


                if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
                {
                    Response.Redirect("Login.aspx", false);
                }
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }

        }

        protected string getOTPCode(string userid)
        {
            string otp = null;
            
            SqlConnection con = new SqlConnection(SITConnectDBConnectionString);
            string sql = "select VerificationCode from Account where Email = @USERID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@USERID", userid);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        otp = reader["VerificationCode"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return otp;
        }

        protected void verifyOTP(object sender, EventArgs e)
        {
            if (code.Text.ToString() == getOTPCode(Session["LoggedIn"].ToString()))
            {
                Response.Redirect("AccountPage.aspx", false);
            }
            else
            {
                errorMsg.Text = "Verification code is wrong, please re-enter.";
            }
        }
    }
}