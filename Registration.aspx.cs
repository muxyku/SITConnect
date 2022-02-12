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
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;

namespace SITConnect
{
    public partial class Registration : System.Web.UI.Page
    {
        string SITConnectDBConnectionString =
        System.Configuration.ConfigurationManager.ConnectionStrings["SITConnectDBConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;
        byte[] Image;
        int loginattempt = 0;
        int passwordchangeattempt = 0;
        string dt = new DateTime().ToString("yyy/MM/dd HH:MM:ss");
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Check password
        private int checkPassword(string password)
        {
            int score = 0;

            //Check password if more than 12 characters
            if (password.Length < 12)
            {
                return 1;
            }
            else
            {
                score = 1;
            }
            //Check if contains lowercase
            if (Regex.IsMatch(password, "[a-z]"))
            {
                score++;
            }
            
            //Check if contains uppercase
            if (Regex.IsMatch(password, "[A-z]"))
            {
                score++;
            }

            //Check if contains numbers
            if (Regex.IsMatch(password, "[0-9]"))
            {
                score++;
            }

            //Check if contains special characters
            if (Regex.IsMatch(password, "[.$^{[(|)*+?!@]"))
            {
                score++;
            }


            return score;
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            //Password validation
            int scores = checkPassword(tb_userPass.Text);
            string status = "";
            switch (scores)
            {
                case 1:
                    status = "Password needs to be at least 12 characters";
                    break;
                case 2:
                    status = "Password needs to contain at least 1 lowercase letter";
                    break;
                case 3:
                    status = "Password needs to contain at least 1 uppercase letter";
                    break;
                case 4:
                    status = "Password needs to contain at least 1 number";
                    break;
                case 5:
                    status = "Password needs to contain at least 1 special character";
                    break;
                default:
                    break;
            }
            lbl_pwdchecker.Text = "Status : " + status;
            if (scores < 5)
            {
                lbl_pwdchecker.ForeColor = Color.Red;
                return;
            }
            lbl_pwdchecker.ForeColor = Color.Green;

            if (checkEmailExists(tb_userEmail.Text.ToString())){
                lbl_email.Text = "Email already exists. Please use another email.";
            }
            else {
                //Hashing and salting
                string pwd = tb_userPass.Text.ToString().Trim();
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

                createAccount();

                Response.Redirect("Login.aspx");
            }
            
        }

        protected void createAccount()
        {
           
                try
                {
                    using (SqlConnection con = new SqlConnection(SITConnectDBConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Account VALUES(@FirstName, @LastName, @CreditNum, @CreditDate, @CreditCVV, @Email, @PasswordHash, @PasswordSalt, @DateofBirth, @IV, @Key, @LoginAttempts, @LockoutTime, @PasswordHist1Hash, @PasswordHist2Hash, @PasswordChangeAttempt, @VerificationCode)"))
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                cmd.CommandType = CommandType.Text;
                                cmd.Parameters.AddWithValue("@FirstName", HttpUtility.HtmlEncode(tb_userFn.Text.Trim()));
                                cmd.Parameters.AddWithValue("@LastName", HttpUtility.HtmlEncode(tb_userLn.Text.Trim()));
                                cmd.Parameters.AddWithValue("@CreditNum", encryptData(HttpUtility.HtmlEncode(tb_userCreditNum.Text)));
                                cmd.Parameters.AddWithValue("@CreditDate", encryptData(HttpUtility.HtmlEncode(tb_userCreditDate.Text)));
                                cmd.Parameters.AddWithValue("@CreditCVV", encryptData(HttpUtility.HtmlEncode(tb_userCreditCVV.Text)));
                                cmd.Parameters.AddWithValue("@Email", HttpUtility.HtmlEncode(tb_userEmail.Text));
                                cmd.Parameters.AddWithValue("@PasswordHash", finalHash);
                                cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                                cmd.Parameters.AddWithValue("@DateofBirth", HttpUtility.HtmlEncode(tb_userDob.Text.Trim()));
                                //cmd.Parameters.AddWithValue("@Photo", Image);
                                cmd.Parameters.AddWithValue("@IV", Convert.ToBase64String(IV));
                                cmd.Parameters.AddWithValue("@Key", Convert.ToBase64String(Key));
                                cmd.Parameters.AddWithValue("@LoginAttempts", loginattempt);
                                cmd.Parameters.AddWithValue("@LockoutTime", dt);
                                cmd.Parameters.AddWithValue("@PasswordHist1Hash", finalHash);
                                cmd.Parameters.AddWithValue("@PasswordHist2Hash", "");
                                cmd.Parameters.AddWithValue("@PasswordChangeAttempt", passwordchangeattempt);
                                cmd.Parameters.AddWithValue("@VerificationCode", DBNull.Value);

                                cmd.Connection = con;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                                createAccountLog();
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
           
            
        }

        //protected bool validateFile()
        //{
        //    HttpPostedFile postedFile = file_image.PostedFile;
        //    string fileName = Path.GetFileName(postedFile.FileName);
        //    string fileExtension = Path.GetExtension(fileName);

        //    if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".bmp" || fileExtension.ToLower() == ".gif" || fileExtension.ToLower() == ".png")
        //    {
        //        Stream stream = postedFile.InputStream;
        //        BinaryReader binaryReader = new BinaryReader(stream);
        //        Image = binaryReader.ReadBytes((int)stream.Length);
        //        return true;
        //    }
        //    else
        //    {
        //        imageError.Text = "Please upload files that only include: jpg bmp gif png ";
        //        return false;
        //    }
        //}

        protected byte[] encryptData(string data)
        {
            byte[] cipherText = null;
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                //ICryptoTransform decryptTransform = cipher.CreateDecryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0,
               plainText.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
            return cipherText;
        }

        protected void createAccountLog()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SITConnectDBConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT AuditLogs VALUES(@DateTimeLog, @UserLog, @Action)"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@DateTimeLog", DateTime.Now );
                            cmd.Parameters.AddWithValue("@UserLog", HttpUtility.HtmlEncode(tb_userEmail.Text.Trim()));
                            cmd.Parameters.AddWithValue("@Action", "Registered for an account".ToString());
                            
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

        public bool checkEmailExists(string email)
        {
            SqlConnection connection = new SqlConnection(SITConnectDBConnectionString);
            string sql = "select * FROM Account WHERE Email=@EMAIL";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EMAIL", email);

            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (reader["Id"].ToString() != null)
                    {
                        return true;
                    }
                }
            }
            connection.Close();
            return false;
        }
    }
}