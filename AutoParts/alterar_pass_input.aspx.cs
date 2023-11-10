using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;

namespace AutoParts
{
    public partial class alterar_pass_input : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submit_Click(object sender, EventArgs e)
        {
            try
            {
                string emailToSearch = tb_email.Text;
                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString);

                using (SqlConnection connection = myConn)
                {
                    connection.Open();

                    string query = "SELECT TOP 1 1 FROM utilizadores WHERE email = @Email";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", emailToSearch);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            Session["email_alterar_pass"] = emailToSearch;

                            try
                            {
                                MailMessage mail = new MailMessage();
                                SmtpClient servidor = new SmtpClient();

                                mail.From = new MailAddress(ConfigurationManager.AppSettings["SMTP_USER"]);
                                mail.To.Add(new MailAddress(emailToSearch));
                                mail.Subject = "AutoParts reposição de password";

                                mail.IsBodyHtml = true;
                                mail.Body = "Mudança de password autoparts, carrega <a href='https://localhost:44335/alterar_password.aspx?m_uti=" + EncryptString(emailToSearch) + "'>AQUI</a> para mudares a tua password.";

                                servidor.Host = ConfigurationManager.AppSettings["SMTP_HOST"];
                                servidor.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);
                                string smtpUtilizador = ConfigurationManager.AppSettings["SMTP_USER"];
                                string smtpPassword = ConfigurationManager.AppSettings["SMTP_PASS"];

                                servidor.Credentials = new NetworkCredential(smtpUtilizador, smtpPassword);
                                servidor.EnableSsl = true;

                                servidor.Send(mail);
                                lbl_erro.Enabled = true;
                                lbl_erro.Visible = true;
                                lbl_erro.Text = "Verifica a tua caixa de email para mudares a tua password!";
                                lbl_erro.ForeColor = System.Drawing.Color.Green;
                                lbl_erro.Font.Size = 25;
                                Session["isEmailPassword"] = "yes";


                            }
                            catch (Exception ex)
                            {

                            }

                        }
                        else
                        {
                            lbl_erro.Enabled = true;
                            lbl_erro.Visible = true;
                            lbl_erro.Text = "Não existe nenhuma conta com o email indicado";
                        }

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                lbl_erro.Text = ex.Message;
            }


        }


        public string EncryptString(string Message)
        {
            string Passphrase = "@Aut0P@rts";
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();



            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below



            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));



            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();



            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;



            // Step 4. Convert the input string to a byte[]
            byte[] DataToEncrypt = UTF8.GetBytes(Message);



            // Step 5. Attempt to encrypt the string
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }



            // Step 6. Return the encrypted string as a base64 encoded string



            string enc = Convert.ToBase64String(Results);
            enc = enc.Replace("+", "KKK");
            enc = enc.Replace("/", "JJJ");
            enc = enc.Replace("\\", "III");
            return enc;
        }
    }
}