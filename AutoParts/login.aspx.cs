using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;


namespace AutoParts
{
    public partial class login : System.Web.UI.Page
    {
        public class GoogleProfile
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Picture { get; set; }
            public string Email { get; set; }
            public string Verified_Email { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public static string respostaUsername, respostaEmail, respostaPerfil;
        public static int respostaVerificado, respostaId, respostaTipoCliente;

        protected void lbl_erro_enviar_Click(object sender, EventArgs e)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient servidor = new SmtpClient();

                mail.From = new MailAddress(ConfigurationManager.AppSettings["SMTP_USER"]);
                mail.To.Add(new MailAddress(email_username.Text));
                mail.Subject = "Ativar Conta AutoParts";

                mail.IsBodyHtml = true;
                mail.Body = "Registo autoparts, carrega <a href='https://localhost:44335/ativacao_conta.aspx?m_uti=" + EncryptString(email_username.Text) + "'>AQUI</a> para ativares a tua conta.";

                servidor.Host = ConfigurationManager.AppSettings["SMTP_HOST"];
                servidor.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);
                string smtpUtilizador = ConfigurationManager.AppSettings["SMTP_USER"];
                string smtpPassword = ConfigurationManager.AppSettings["SMTP_PASS"];

                servidor.Credentials = new NetworkCredential(smtpUtilizador, smtpPassword);
                servidor.EnableSsl = true;

                servidor.Send(mail);
                lbl_erro_enviar.Enabled = false;
                lbl_erro_enviar.Visible = false;

                lbl_erro.Text = "Verifica a tua caixa de email para verificares a conta!";
                lbl_erro.ForeColor = System.Drawing.Color.Green;
                lbl_erro.Font.Size = 25;
                Session["ativacao_enviada_acesso"] = "yes";
            }
            catch (Exception ex)
            {
                lbl_erro.Text = ex.Message;
                lbl_erro.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString);

                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "login_user";

                cmd.Connection = myConn;

                cmd.Parameters.AddWithValue("@email_user", email_username.Text);
                cmd.Parameters.AddWithValue("@password", EncryptString(password.Text));

                SqlParameter retorno = new SqlParameter("@retorno", SqlDbType.Int);
                retorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retorno);

                SqlParameter retorno_username = new SqlParameter("@retorno_username", SqlDbType.VarChar, 20);
                retorno_username.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retorno_username);

                SqlParameter retorno_email = new SqlParameter("@retorno_email", SqlDbType.VarChar, 255);
                retorno_email.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retorno_email);

                SqlParameter retorno_verificado = new SqlParameter("@retorno_verificado", SqlDbType.Bit);
                retorno_verificado.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retorno_verificado);

                SqlParameter retorno_id = new SqlParameter("@retorno_id", SqlDbType.Int);
                retorno_id.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retorno_id);

                SqlParameter retorno_tipoCliente = new SqlParameter("@retorno_tipoCliente", SqlDbType.Int);
                retorno_tipoCliente.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retorno_tipoCliente);

                SqlParameter retorno_perfil = new SqlParameter("@retorno_perfil", SqlDbType.VarChar, 10);
                retorno_perfil.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retorno_perfil);

                myConn.Open();
                cmd.ExecuteNonQuery();

                int respostaSP = Convert.ToInt32(cmd.Parameters["@retorno"].Value);

                if (respostaSP == 0)
                {
                    respostaUsername = cmd.Parameters["@retorno_username"].Value.ToString();
                    respostaEmail = cmd.Parameters["@retorno_email"].Value.ToString();
                    respostaVerificado = Convert.ToInt32(cmd.Parameters["@retorno_verificado"].Value);
                    respostaId = Convert.ToInt32(cmd.Parameters["@retorno_id"].Value);
                    respostaTipoCliente = Convert.ToInt32(cmd.Parameters["@retorno_tipoCliente"].Value);
                    respostaPerfil = cmd.Parameters["@retorno_perfil"].Value.ToString();
                }


                myConn.Close();

                string respostaemail = email_username.Text;

                if (respostaSP == 0)
                {
                    if (respostaVerificado == 0)
                    {
                        lbl_erro.Enabled = true;
                        lbl_erro.Visible = true;
                        lbl_erro.Text = "Conta não ativada, para ativar carrega";
                        lbl_erro_enviar.Text = " aqui";
                    }
                    else
                    {
                        lbl_erro.Enabled = true;
                        lbl_erro.Visible = true;
                        lbl_erro.Text = "Welcome";

                        Session["isLogged"] = "yes";
                        Session["perfil"] = respostaPerfil;
                        Session["userId"] = respostaId;
                        Session["username"] = respostaUsername;
                        Session["user_email"] = respostaEmail;
                        Session["tipo_cliente"] = respostaTipoCliente;

                        Response.Redirect("index.aspx");
                    }

                }
                else
                {
                    lbl_erro.Enabled = true;
                    lbl_erro.Visible = true;
                    lbl_erro.Text = "Email ou password errados!";
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                lbl_erro.Enabled = true;
                lbl_erro.Visible = true;
                //lbl_erro.Text = "Ocorreu um erro durante o processo de login. Por favor, tente novamente mais tarde.";
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