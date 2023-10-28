using ASPSnippets.GoogleAPI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using static AutoParts.login;
using System.Security.Cryptography;

namespace AutoParts
{
    public partial class teste : System.Web.UI.Page
    {
        public static string controlo = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            GoogleConnect.ClientId = "972730968305-pnle29kt7bicu76ru49nujjnnv2g4npt.apps.googleusercontent.com";
            GoogleConnect.ClientSecret = "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";
            GoogleConnect.RedirectUri = Request.Url.AbsoluteUri.Split('?')[0];

            if (!this.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["code"]))
                {
                    string code = Request.QueryString["code"];
                    string json = GoogleConnect.Fetch("me", code);
                    GoogleProfile profile = new JavaScriptSerializer().Deserialize<GoogleProfile>(json);
                    Session["email_p"] = profile.Email;
                    Session["name_p"] = profile.Name;
                    Session["id_p"] = profile.Id;
                    controlo = "1";
                }
                if (Request.QueryString["error"] == "access_denied")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Access denied.')", true);
                }


                if (controlo == "1")
                {
                    //string username = GenerateName(7);
                    SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString);

                    SqlCommand myCommand = new SqlCommand();
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandText = "registar_user_google";

                    myCommand.Connection = myConn;

                    myCommand.Parameters.AddWithValue("@nome", Session["name_p"]);
                    myCommand.Parameters.AddWithValue("@email", Session["email_p"]);
                    myCommand.Parameters.AddWithValue("@username", "asdasd");
                    myCommand.Parameters.AddWithValue("@password", EncryptString(Session["id_p"].ToString()));
                    myCommand.Parameters.AddWithValue("@tipocliente", 1);


                    SqlParameter valor = new SqlParameter();
                    valor.ParameterName = "@retorno";
                    valor.Direction = ParameterDirection.Output;
                    valor.SqlDbType = SqlDbType.Int;
                    myCommand.Parameters.Add(valor);

                    myConn.Open();
                    myCommand.ExecuteNonQuery();

                    int respostaSP = Convert.ToInt32(myCommand.Parameters["@retorno"].Value);

                    myConn.Close();

                    if (respostaSP == 0)
                    {
                        //lbl_erro.Text = "This user already exists, please choose another.";
                    }
                    else
                    {
                        // lbl_erro.ForeColor = System.Drawing.Color.Green;
                        //lbl_erro.Text = "User Successfully Created!";
                    }
                }
            }
        }

        protected void btn_googleLogin_Click(object sender, EventArgs e)
        {
          
                GoogleConnect.Authorize("profile", "email");
           
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