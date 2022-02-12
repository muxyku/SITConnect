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
        
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;
        int changeAttempt;
        int remainder;
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
                string query = "UPDATE [Account] SET [PasswordHash] = @PasswordHash, [PasswordSalt] = @PasswordSalt WHERE Email = @USERID";
                
                //Update passwordhist2 query
                string query1 = "UPDATE [Account] SET [PasswordHash] = @PasswordHash, [PasswordSalt] = @PasswordSalt, [PasswordHist2Hash] = @PasswordHash WHERE Email = @USERID";
                
                //Update passwordhist1 query
                string query2 = "UPDATE [Account] SET [PasswordHash] = @PasswordHash, [PasswordSalt] = @PasswordSalt, [PasswordHist1Hash] = @PasswordHash WHERE Email = @USERID";
                string SITConnectDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SITConnectDBConnection"].ConnectionString;

                string passdbHash = getDBHash(Session["LoggedIn"].ToString());
                string passdbSalt = getDBSalt(Session["LoggedIn"].ToString());

                string pass1dbHash = getDBHash1(Session["LoggedIn"].ToString());
                
                string pass2dbHash = getDBHash2(Session["LoggedIn"].ToString());

                string currentPassword = ChangePassword1.CurrentPassword.ToString().Trim();
                string newPassword = HttpUtility.HtmlEncode(ChangePassword1.NewPassword.ToString().Trim());
                string confirmPassword = ChangePassword1.ConfirmNewPassword.ToString().Trim();

                getPasswordChangeAttempt(Session["LoggedIn"].ToString());

                remainder = changeAttempt % 2;

                /*
                SHA512Managed hashing = new SHA512Managed();
                string passwordSalt = currentPassword + passdbSalt;
                byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(passwordSalt));
                string finalHash = Convert.ToBase64String(hashWithSalt);

                if (finalHash == passdbHash)
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Current password does not match.";
                }*/

                //Adding new password to PasswordHist2, when password2hist is empty
                if (pass2dbHash == "" && pass1dbHash != null)
                {
                    using (SqlConnection con = new SqlConnection(SITConnectDBConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand(query1))
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {

                                changeAttempt++;
                                updatePasswordChangeAttempt(Session["LoggedIn"].ToString(), changeAttempt);

                                //Hashing and salting
                                string pwd = ChangePassword1.NewPassword.ToString();
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


                                cmd.CommandType = CommandType.Text;
                                cmd.Parameters.AddWithValue("@PasswordHash", finalHash);
                                cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                                cmd.Parameters.AddWithValue("@USERID", Session["LoggedIn"].ToString());

                                cmd.Connection = con;
                                con.Open();
                                rowsAffected = cmd.ExecuteNonQuery();
                                con.Close();

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

                //Update password 1 if password2 has a password
                if (pass2dbHash != "" && pass1dbHash != null && remainder == 1)
                {
                    using (SqlConnection con = new SqlConnection(SITConnectDBConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand(query2))
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                if (checkPasswordReuse(newPassword))
                                {
                                    errorMsg.Text = "New password cannot match 2 previous passwords. Please use another password.";
                                }
                                else
                                {
                                    changeAttempt++;
                                    updatePasswordChangeAttempt(Session["LoggedIn"].ToString(), changeAttempt);

                                    //Hashing and salting
                                    string pwd = ChangePassword1.NewPassword.ToString();
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


                                    cmd.CommandType = CommandType.Text;
                                    cmd.Parameters.AddWithValue("@PasswordHash", finalHash);
                                    cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                                    cmd.Parameters.AddWithValue("@USERID", Session["LoggedIn"].ToString());

                                    cmd.Connection = con;
                                    con.Open();
                                    rowsAffected = cmd.ExecuteNonQuery();
                                    con.Close();
                                }



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

                if (pass2dbHash != "" && pass1dbHash != null && remainder == 0)
                {
                    using (SqlConnection con = new SqlConnection(SITConnectDBConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand(query1))
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                if (checkPasswordReuse(newPassword))
                                {
                                    errorMsg.Text = "New password cannot match 2 previous passwords. Please use another password.";
                                }
                                else
                                {
                                    changeAttempt++;
                                    updatePasswordChangeAttempt(Session["LoggedIn"].ToString(), changeAttempt);

                                    //Hashing and salting
                                    string pwd = ChangePassword1.NewPassword.ToString();
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


                                    cmd.CommandType = CommandType.Text;
                                    cmd.Parameters.AddWithValue("@PasswordHash", finalHash);
                                    cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                                    cmd.Parameters.AddWithValue("@USERID", Session["LoggedIn"].ToString());

                                    cmd.Connection = con;
                                    con.Open();
                                    rowsAffected = cmd.ExecuteNonQuery();
                                    con.Close();
                                }
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
                //if (passdbHash == pass1dbHash && passdbHash == pass2dbHash || passdbSalt == pass1dbSalt && passdbHash == pass2dbSalt)
                //{
                //    errorMsg.Text = "New password cannot match 2 previous passwords. Please use another password.";
                //}
            }
            else
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Old Password and New Password must not be equal.";
            }

            e.Cancel = true;
        }


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

        protected string getDBHash(string userid)
        {
            string h = null;
            SqlConnection connection = new SqlConnection(SITConnectDBConnectionString);
            string sql = "select PasswordHash FROM Account WHERE Email=@USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (reader["PasswordHash"] != null)
                        {
                            if (reader["PasswordHash"] != DBNull.Value)
                            {
                                h = reader["PasswordHash"].ToString();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return h;
        }

        protected string getDBSalt(string userid)
        {
            string s = null;
            SqlConnection connection = new SqlConnection(SITConnectDBConnectionString);
            string sql = "select PASSWORDSALT FROM ACCOUNT WHERE Email=@USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid);

            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PASSWORDSALT"] != null)
                        {
                            if (reader["PASSWORDSALT"] != DBNull.Value)
                            {
                                s = reader["PASSWORDSALT"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return s;
        }

        protected string getDBHash1(string userid)
        {
            string h = null;
            SqlConnection connection = new SqlConnection(SITConnectDBConnectionString);
            string sql = "select PasswordHist1Hash FROM Account WHERE Email=@USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (reader["PasswordHist1Hash"] != null)
                        {
                            if (reader["PasswordHist1Hash"] != DBNull.Value)
                            {
                                h = reader["PasswordHist1Hash"].ToString();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return h;
        }

        

        protected string getDBHash2(string userid)
        {
            string h = null;
            SqlConnection connection = new SqlConnection(SITConnectDBConnectionString);
            string sql = "select PasswordHist2Hash FROM Account WHERE Email=@USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (reader["PasswordHist2Hash"] != null)
                        {
                            if (reader["PasswordHist2Hash"] != DBNull.Value)
                            {
                                h = reader["PasswordHist2Hash"].ToString();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return h;
        }

        

        protected string getPasswordChangeAttempt(string userid)
        {
            string h = null;
            SqlConnection connection = new SqlConnection(SITConnectDBConnectionString);
            string sql = "select PasswordChangeAttempt FROM Account WHERE Email=@USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (reader["PasswordChangeAttempt"] != DBNull.Value)
                        {
                            changeAttempt = (int)reader["PasswordChangeAttempt"];
                        }


                    }

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return h;
        }

        protected void updatePasswordChangeAttempt(string userid, int passwordAttempt)
        {
            using (SqlConnection con = new SqlConnection(SITConnectDBConnectionString))
            {
                string query = "UPDATE [Account] SET [PasswordChangeAttempt] = @PasswordChangeAttempt WHERE [Email]= @USERID";

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@PasswordChangeAttempt", passwordAttempt);
                        cmd.Parameters.AddWithValue("@USERID", userid);

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
        }

        public bool checkPasswordReuse(string newPassword)
        {
            bool reusedPassword;
            string passwordHist1 = null;
            string passwordHist2 = null;

            SqlConnection connection = new SqlConnection(SITConnectDBConnectionString);
            string sql = "select PasswordHist1Hash, PasswordHist2Hash FROM Account WHERE Email=@USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", Session["LoggedIn"].ToString());

            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (reader["PasswordHist1Hash"] != null) {

                            if (reader["PasswordHist1Hash"] != DBNull.Value)
                            {
                                passwordHist1 = (string)reader["PasswordHist1Hash"];
                                passwordHist2 = (string)reader["PasswordHist2Hash"];
                            }
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }

            SHA512Managed hashing = new SHA512Managed();
            string storedSalt = getDBSalt(Session["LoggedIn"].ToString());

            string passwordSalt = newPassword + storedSalt;
            byte[] hash1WithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(passwordSalt));
            string userHash = Convert.ToBase64String(hash1WithSalt);

            if (userHash.Equals(passwordHist1) || userHash.Equals(passwordHist2))
            {
                reusedPassword = true;
            }
            else 
            {
                reusedPassword = false;
            }
            return reusedPassword;

        }



    }
}