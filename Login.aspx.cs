using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace SITConnect
{
    public partial class Login : System.Web.UI.Page
    {
        public string success { get; set; }
        public List<string> ErrorMessage { get; set; }
        string SITConnectDBConnectionString =
        System.Configuration.ConfigurationManager.ConnectionStrings["SITConnectDBConnection"].ConnectionString;
        int LoginAttempts = 0;
        DateTime lockoutTimenow = DateTime.Now;
        DateTime localLockTime;

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if (validateCaptcha()) 
            {
                string pwd = tb_userPass.Text.ToString().Trim();
                string userEmail = tb_userEmail.Text.ToString().Trim();
                SHA512Managed hashing = new SHA512Managed();
                string dbHash = getDBHash(userEmail);
                string dbSalt = getDBSalt(userEmail);
                getLoginAttempt(userEmail);
                getLockoutTime(userEmail);
                
                //Time
                TimeSpan ts = lockoutTimenow.Subtract(localLockTime);
                Int32 minutesLocked = Convert.ToInt32(ts.TotalMinutes);
                Int32 pendingMinutes = 10 - minutesLocked;

                //CHECK IF EMAIL EXIST OR NOT

                try
                {
                    if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                    {
                        string pwdWithSalt = pwd + dbSalt;
                        byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                        string userHash = Convert.ToBase64String(hashWithSalt);
                        
                        if (userHash.Equals(dbHash) && pendingMinutes <= 0)
                        {
                            Session["LoggedIn"] = tb_userEmail.Text.Trim();
                            string guid = Guid.NewGuid().ToString();
                            Session["AuthToken"] = guid;
                            Response.Cookies.Add(new HttpCookie("AuthToken", guid));
                            Response.Redirect("AccountPage.aspx", false);
                            setLoginAttempt(userEmail);
                            resetLockoutTime(userEmail);
                            LoginLog();
                        }
                        else
                        {
                            LoginAttempts++;
                            addLA.Text = LoginAttempts.ToString();
                            updateLoginAttempt(userEmail, LoginAttempts);

                            if (LoginAttempts == 1)
                            {
                                errorText.Text = "User email or password is incorrect! Please try again! 2 attempts remaining.";
                            }
                            else if (LoginAttempts == 2)
                            {
                                errorText.Text = "User email or password is incorrect! Please try again! 1 attempts remaining.";
                            }
                            else if (pendingMinutes > 0) { 
                                errorText.Text = " Your account has been locked! Try again later.";

                            }
                            else if (LoginAttempts == 3)
                            {
                                failedLoginLog();
                                setDBLockoutTime(userEmail, lockoutTimenow);
                                errorText.Text = "Too many tries! Your account has been locked! Try again later.";  
                            }
                            
                        }


                        }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally { }
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

        public bool validateCaptcha() {
            bool result = true;

            string captchaResponse = Request.Form["g-recaptcha-response"];

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://www.google.com/recaptcha/api/siteverify?secret=6LdnEVYeAAAAABfWohDTrWO3uJnT2tPqoPbtG2J_ &response=" + captchaResponse);

            try
            {
                using (WebResponse wResponse = req.GetResponse())
                {
                    using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {
                        string jsonResponse = readStream.ReadToEnd();
                        gScore.Text = jsonResponse;
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        Login jsonObject = js.Deserialize<Login>(jsonResponse);

                        result = Convert.ToBoolean(jsonObject.success);
                    }
                }
                return result;
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }


        protected void setDBLockoutTime(string userid, DateTime lockoutTime)
        {
            using (SqlConnection con = new SqlConnection(SITConnectDBConnectionString))
            {
                string query = "UPDATE [Account] SET [LockoutTime] = @LockoutTime WHERE [Email]= @USERID";

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@LockoutTime", lockoutTime);
                        cmd.Parameters.AddWithValue("@USERID", userid);

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
        }
        protected string getLockoutTime(string userid)
        {
            string h = null;
            SqlConnection connection = new SqlConnection(SITConnectDBConnectionString);
            string sql = "select LockoutTime FROM Account WHERE Email=@USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (reader["LockoutTime"] != DBNull.Value)
                        {
                            localLockTime = (DateTime)reader["LockoutTime"];
                           

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


        protected void resetLockoutTime(string userid)
        {
            using (SqlConnection con = new SqlConnection(SITConnectDBConnectionString))
            {
                string query = "UPDATE [Account] SET [LockoutTime] = @LockoutTime WHERE [Email]= @USERID";

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@LockoutTime", "2001 - 01 - 21 00:00:00");
                        cmd.Parameters.AddWithValue("@USERID", userid);

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
        }

        protected void updateLoginAttempt(string userid, int loginAttempt)
        {
            using (SqlConnection con = new SqlConnection(SITConnectDBConnectionString))
            {
                string query = "UPDATE [Account] SET [LoginAttempts] = @LoginAttempts WHERE [Email]= @USERID";

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@LoginAttempts", loginAttempt);
                        cmd.Parameters.AddWithValue("@USERID", userid);

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
        }


        protected string getLoginAttempt(string userid)
        {
            string h = null;
            SqlConnection connection = new SqlConnection(SITConnectDBConnectionString);
            string sql = "select LoginAttempts FROM Account WHERE Email=@USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (reader["LoginAttempts"] != DBNull.Value)
                        {
                            LoginAttempts = (int)reader["LoginAttempts"];
                            LA.Text = reader["LoginAttempts"].ToString();

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

        protected void setLoginAttempt(string userid)
        {
            using (SqlConnection con = new SqlConnection(SITConnectDBConnectionString))
            {
                string query = "UPDATE [Account] SET [LoginAttempts] = @LoginAttempt WHERE [Email] = @USERID";

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@LoginAttempt", 0);
                        cmd.Parameters.AddWithValue("@USERID", userid);

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
        }

        protected void LoginLog()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SITConnectDBConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO AuditLogs VALUES(@DateTimeLog, @UserLog, @Action)"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@DateTimeLog", DateTime.Now);
                            cmd.Parameters.AddWithValue("@UserLog", tb_userEmail.Text.Trim());
                            cmd.Parameters.AddWithValue("@Action", "Successfully logged into account".ToString());

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

        protected void failedLoginLog()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SITConnectDBConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO AuditLogs(DateTimeLog,UserLog,Action) VALUES(@DateTimeLog, @UserLog, @Action)"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@DateTimeLog", DateTime.Now);
                            cmd.Parameters.AddWithValue("@UserLog", tb_userEmail.Text.Trim());
                            cmd.Parameters.AddWithValue("@Action", "Failed to login and account has been locked for 10 minutes".ToString());


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