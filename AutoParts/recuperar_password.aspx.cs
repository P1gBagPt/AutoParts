using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;

namespace AutoParts
{
    public partial class recuperar_password : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string respostaPassword;

        protected void submit_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString);

                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "recuperar_password";

                myCommand.Connection = myConn;

                myCommand.Parameters.AddWithValue("@email", email.Text);

                SqlParameter valor = new SqlParameter();
                valor.ParameterName = "@retorno";
                valor.Direction = ParameterDirection.Output;
                valor.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(valor);

                SqlParameter retorno_password = new SqlParameter();
                retorno_password.ParameterName = "@retorno_password";
                retorno_password.Direction = ParameterDirection.Output;
                retorno_password.SqlDbType = SqlDbType.VarChar;
                retorno_password.Size = 255;
                myCommand.Parameters.Add(retorno_password);

                myConn.Open();
                myCommand.ExecuteNonQuery();

                int resposta = Convert.ToInt32(myCommand.Parameters["@retorno"].Value);
                respostaPassword = myCommand.Parameters["@retorno_password"].Value.ToString();

                myConn.Close();

                if (resposta == 0)
                {
                    /*lbl_erro.Text = "Email já usado tenta outro!";
                    lbl_erro.ForeColor = System.Drawing.Color.Red;*/
                    respostaPassword = DecryptString(respostaPassword);

                    try
                    {
                        MailMessage mail = new MailMessage();
                        SmtpClient servidor = new SmtpClient();

                        mail.From = new MailAddress(ConfigurationManager.AppSettings["SMTP_USER"]);
                        mail.To.Add(new MailAddress(email.Text));
                        mail.Subject = "AutoParts Recuperação de Password";

                        mail.IsBodyHtml = true;
                        mail.Body = "Recuperação de password da conta " + email.Text + " </br> Password: <b>" + respostaPassword +"</b>";

                        servidor.Host = ConfigurationManager.AppSettings["SMTP_HOST"];
                        servidor.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);
                        string smtpUtilizador = ConfigurationManager.AppSettings["SMTP_USER"];
                        string smtpPassword = ConfigurationManager.AppSettings["SMTP_PASS"];

                        servidor.Credentials = new NetworkCredential(smtpUtilizador, smtpPassword);
                        servidor.EnableSsl = true;

                        servidor.Send(mail);
                        lbl_erro.Enabled = true;
                        lbl_erro.Visible = true;
                        lbl_erro.Text = "Verifica a tua caixa de email!";
                        lbl_erro.ForeColor = System.Drawing.Color.Green;
                        lbl_erro.Font.Size = 25;
                    }
                    catch (Exception ex)
                    {
                        lbl_erro.Enabled = true;
                        lbl_erro.Visible = true;
                        lbl_erro.Text = ex.ToString();
                    }
                }
                else if (resposta == 1)
                {
                    lbl_erro.Enabled = true;
                    lbl_erro.Visible = true;
                    lbl_erro.Text = "Não existe nenhuma conta com o email indicado";
                    lbl_erro.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lbl_erro.Enabled = true;
                lbl_erro.Visible = true;
                lbl_erro.Text = ex.ToString();
            }
        }

        public string DecryptString(string Message)
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



            // Step 3. Setup the decoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;



            // Step 4. Convert the input string to a byte[]



            Message = Message.Replace("KKK", "+");
            Message = Message.Replace("JJJ", "/");
            Message = Message.Replace("III", "\\");




            byte[] DataToDecrypt = Convert.FromBase64String(Message);



            // Step 5. Attempt to decrypt the string
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }



            // Step 6. Return the decrypted string in UTF8 format
            return UTF8.GetString(Results);
        }
    }
}