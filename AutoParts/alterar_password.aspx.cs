using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace AutoParts
{
    public partial class alterar_password : System.Web.UI.Page
    {
        string email_user = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isEmailPassword"] == null)
            {
                Response.Redirect("alterar_pass_input.aspx");
            }
            else
            {
                email_user = DecryptString(Request.QueryString["m_uti"]);
            }
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (tb_password1.Text != tb_password2.Text)
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

                    if (tb_password1.Text.Length < 6)
                    {
                        situacao = false;
                    }

                    if (maiusculas.Matches(tb_password1.Text).Count < 1)
                    {
                        situacao = false;
                    }
                    if (minusculas.Matches(tb_password1.Text).Count < 1)
                    {
                        situacao = false;
                    }
                    if (numeros.Matches(tb_password1.Text).Count < 1)
                    {
                        situacao = false;
                    }
                    if (especial.Matches(tb_password1.Text).Count < 1)
                    {
                        situacao = false;
                    }
                    if (plica.Matches(tb_password1.Text).Count > 0)
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
                            myCommand.CommandText = "update_password";

                            myCommand.Connection = myConn;

                            myCommand.Parameters.AddWithValue("@email", email_user);
                            myCommand.Parameters.AddWithValue("@password_atual", EncryptString(tb_pass_atual.Text));
                            myCommand.Parameters.AddWithValue("@password_nova", EncryptString(tb_password1.Text));

                            SqlParameter valor = new SqlParameter();
                            valor.ParameterName = "@retorno";
                            valor.Direction = ParameterDirection.Output;
                            valor.SqlDbType = SqlDbType.Int;
                            myCommand.Parameters.Add(valor);

                            myConn.Open();
                            myCommand.ExecuteNonQuery();
                            int resposta = Convert.ToInt32(myCommand.Parameters["@retorno"].Value);


                            myConn.Close();

                            if(resposta == 0)
                            {
                                lbl_erro.Enabled = true;
                                lbl_erro.Visible = true;
                                lbl_erro.Text = "Password atual incorreta!";
                            }
                            else if(resposta == 1)
                            {
                                lbl_erro.Enabled = true;
                                lbl_erro.Visible = true;
                                lbl_erro.Text = "Password alterada com sucesso!";

                                Response.Redirect("login.aspx");
                            }

                            

                        }
                        catch (Exception ex)
                        {
                            lbl_erro.Enabled = true;
                            lbl_erro.Visible = true;
                            lbl_erro.Text = ex.ToString();
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                lbl_erro.Text = ex.ToString();
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