using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;

namespace AutoParts
{
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submit_Click(object sender, EventArgs e)
        {
            if (password.Text != re_password.Text)
            {
                lbl_erro.Enabled = true;
                lbl_erro.Visible = true;
                lbl_erro.Text = "As passwords não são iguais";
            }
            else
            {
                Regex maiusculas = new Regex("[A-Z]");
                Regex minusculas = new Regex("[a-z]");
                Regex numeros = new Regex("[0-9]");
                Regex especial = new Regex("[^a-zA-Z0-9]");
                Regex plica = new Regex("'");

                bool situacao = true;

                if (password.Text.Length < 6)
                {
                    situacao = false;
                }

                if (maiusculas.Matches(password.Text).Count < 1)
                {
                    situacao = false;
                }
                if (minusculas.Matches(password.Text).Count < 1)
                {
                    situacao = false;
                }
                if (numeros.Matches(password.Text).Count < 1)
                {
                    situacao = false;
                }
                if (especial.Matches(password.Text).Count < 1)
                {
                    situacao = false;
                }
                if (plica.Matches(password.Text).Count > 0)
                {
                    situacao = false;
                }
                if (situacao == false)
                {
                    lbl_erro.Enabled = true;
                    lbl_erro.Visible = true;

                    lbl_erro.Text = "Password fraca";
                }
                else
                {
                    lbl_erro.Enabled = true;
                    lbl_erro.Visible = true;
                    lbl_erro.Text = "Password forte";

                    try
                    {
                        SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString);

                        SqlCommand myCommand = new SqlCommand();
                        myCommand.CommandType = CommandType.StoredProcedure;
                        myCommand.CommandText = "registar_user";

                        myCommand.Connection = myConn;

                        myCommand.Parameters.AddWithValue("@nome", tb_name.Text);
                        myCommand.Parameters.AddWithValue("@email", email.Text);
                        myCommand.Parameters.AddWithValue("@username", username.Text);
                        myCommand.Parameters.AddWithValue("@password", EncryptString(password.Text));
                        myCommand.Parameters.AddWithValue("@tipocliente", rbl_tipoUser.SelectedValue);

                        SqlParameter valor = new SqlParameter();
                        valor.ParameterName = "@retorno";
                        valor.Direction = ParameterDirection.Output;
                        valor.SqlDbType = SqlDbType.Int;

                        myCommand.Parameters.Add(valor);

                        myConn.Open();
                        myCommand.ExecuteNonQuery();

                        int resposta = Convert.ToInt32(myCommand.Parameters["@retorno"].Value);

                        myConn.Close();

                        if (resposta == 0)
                        {
                            lbl_erro.Text = "Email já usado tenta outro!";
                            lbl_erro.ForeColor = System.Drawing.Color.Red;
                        }
                        else if (resposta == 1)
                        {
                            lbl_erro.Text = "Username já usado tenta outro!";
                            lbl_erro.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            try
                            {
                                MailMessage mail = new MailMessage();
                                SmtpClient servidor = new SmtpClient();

                                mail.From = new MailAddress(ConfigurationManager.AppSettings["SMTP_USER"]);
                                mail.To.Add(new MailAddress(email.Text));
                                mail.Subject = "AutoParts";

                                mail.IsBodyHtml = true;
                                mail.Body = "Registo autoparts, carrega <a href='https://localhost:44335/ativacao_conta.aspx?m_uti=" + EncryptString(email.Text) + "'>AQUI</a> para ativares a tua conta.";

                                servidor.Host = ConfigurationManager.AppSettings["SMTP_HOST"];
                                servidor.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);
                                string smtpUtilizador = ConfigurationManager.AppSettings["SMTP_USER"];
                                string smtpPassword = ConfigurationManager.AppSettings["SMTP_PASS"];

                                servidor.Credentials = new NetworkCredential(smtpUtilizador, smtpPassword);
                                servidor.EnableSsl = true;

                                servidor.Send(mail);                             
                                lbl_erro.Text = "Verifica a tua caixa de email para verificares a conta!";
                                lbl_erro.ForeColor = System.Drawing.Color.Green;
                                lbl_erro.Font.Size = 25;
                                Session["ativacao_enviada_acesso"] = "yes";
                            }
                            catch(Exception ex)
                            {
                                lbl_erro.Text = ex.ToString();
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        lbl_erro.Text = ex.ToString();

                    }

                }
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