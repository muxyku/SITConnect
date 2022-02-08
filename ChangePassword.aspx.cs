using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Data;

namespace SITConnect
{


    public partial class ChangePassword : System.Web.UI.Page
    {

        string SITConnectDBConnectionString =
        System.Configuration.ConfigurationManager.ConnectionStrings["SITConnectDBConnection"].ConnectionString;
        /*string SITConnectDBConnectionString =
         System.Configuration.ConfigurationManager.ConnectionStrings["SITConnectDBConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;*/
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

        protected void OnChangingPassword(object sender, LoginCancelEventArgs e)
        {
            if (!ChangePassword1.CurrentPassword.Equals(ChangePassword1.NewPassword, StringComparison.CurrentCultureIgnoreCase))
            {
                int rowsAffected = 0;
                string query = "UPDATE [Account] SET [Password] = @NewPassword WHERE [Email] = @Username AND [Password] = @CurrentPassword";
                string SITConnectDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SITConnectDBConnection"].ConnectionString;

                using (SqlConnection con = new SqlConnection(SITConnectDBConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Parameters.AddWithValue("@Username", Session["LoggedIn"].ToString());
                            cmd.Parameters.AddWithValue("@CurrentPassword", ChangePassword1.CurrentPassword);
                            cmd.Parameters.AddWithValue("@NewPassword", ChangePassword1.NewPassword);

                            cmd.Connection = con;
                            con.Open();
                            rowsAffected = cmd.ExecuteNonQuery();
                            con.Close();
                            //updatePassHashSalt(Session["LoggedIn"].ToString());


                        }
                    }
                    if (rowsAffected > 0)
                    {
                        changePasswordLog();
                        lblMessage.ForeColor = Color.Green;
                        lblMessage.Text = "Password has been changed successfully.";

                    }
                    else
                    {
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Password does not match with our database records.";
                    }
                }
            }
            else
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Old Password and New Password must not be equal.";
            }

            e.Cancel = true;
        }

        //TRY UPDATING SALT AND HASH PASSWORD
        /*protected void updatePassHashSalt(string userid)
        {
            using (SqlConnection con = new SqlConnection(SITConnectDBConnectionString))
            {
                string query = "UPDATE [Account] SET [PasswordHash] = @PasswordHash AND [PasswordSalt] = @PasswordSalt FROM Account WHERE Email = @USERID";

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@PasswordHash", finalHash);
                        cmd.Parameters.AddWithValue("@PasswordSalt", salt);

                        //Hashing and salting
                        string pwd = ChangePassword1.ToString().Trim();
                        //Generate random "salt"
                        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                        byte[] saltByte = new byte[8];
                        //Fills array of bytes with a cryptographically strong sequence of random values.
                        rng.GetBytes(saltByte);
                        salt = Convert.ToBase64String(saltByte);
                        SHA512Managed hashing = new SHA512Managed();
                        string pwdWithSalt = pwd + salt;
                        byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwd));
                        byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                        finalHash = Convert.ToBase64String(hashWithSalt);
                        RijndaelManaged cipher = new RijndaelManaged();
                        cipher.GenerateKey();
                        Key = cipher.Key;
                        IV = cipher.IV;

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
        }*/

        protected void changePasswordLog()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SITConnectDBConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO AuditLogs VALUES(@DateTime, @User, @Action)"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@DateTime", DateTime.Now);
                            cmd.Parameters.AddWithValue("@User", Session["LoggedIn"].ToString());
                            cmd.Parameters.AddWithValue("@Action", "Successfully changed password");


                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}