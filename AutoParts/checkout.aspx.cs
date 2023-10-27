using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.IO;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace AutoParts
{
    public partial class checkout : System.Web.UI.Page
    {

        public static int id_user;
        public static decimal total = 0;
        public static int encomenda_id = 0;
        public static string email_user = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["userId"] = 7;
            Session["user_email"] = "marcarpremio@gmail.com";

            id_user = Convert.ToInt32(Session["userId"].ToString());
            email_user = Session["user_email"].ToString();

            try
            {
                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString);

                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "total_carrinho";

                cmd.Connection = myConn;

                cmd.Parameters.AddWithValue("@userId", id_user);

                SqlParameter totalRetorno = new SqlParameter("@total", SqlDbType.Float);
                totalRetorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(totalRetorno);

                myConn.Open();
                cmd.ExecuteNonQuery();

                // Verifique se o valor de retorno não é DBNull antes de convertê-lo
                total = (cmd.Parameters["@total"].Value != DBNull.Value) ? Convert.ToDecimal(cmd.Parameters["@total"].Value) : 0;

                if (total == 0)
                {
                    total = 0;
                    ltTotal.Text = total.ToString();
                    btn_submeter.Visible = false;
                    btn_submeter.Enabled = false;
                }
                else
                {
                    ltTotal.Text = total.ToString();
                }

                myConn.Close();
            }
            catch (Exception ex)
            {
                ltTotal.Text = ex.Message;
            }


        }




        protected void btn_submeter_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int rand_num = rnd.Next(1, 999999);



            try
            {

                string selectedValue = listGroupRadios.SelectedValue;


                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString);

                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "encomenda_pro";

                cmd.Connection = myConn;

                cmd.Parameters.AddWithValue("@userId", id_user);
                cmd.Parameters.AddWithValue("@total", total);
                cmd.Parameters.AddWithValue("@data_encomenda", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@pagamento", selectedValue);
                cmd.Parameters.AddWithValue("@numeroEncomenda", rand_num);


                SqlParameter retorno = new SqlParameter("@retorno", SqlDbType.Int);
                retorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retorno);



                myConn.Open();
                cmd.ExecuteNonQuery();
                encomenda_id = Convert.ToInt32(cmd.Parameters["@retorno"].Value);
                Session["EncomendaID"] = encomenda_id;
                myConn.Close();

                try
                {
                    string selectedCountry = ddl_pais.SelectedValue;

                    SqlConnection myConn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ConnectionString);


                    SqlCommand cmd2 = new SqlCommand();

                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.CommandText = "faturacao_encomenda";

                    cmd2.Connection = myConn2;

                    cmd2.Parameters.AddWithValue("@userId", id_user);
                    cmd2.Parameters.AddWithValue("@encomenda_id", encomenda_id);
                    cmd2.Parameters.AddWithValue("@nome", tb_nome.Text);
                    cmd2.Parameters.AddWithValue("@alcunha", tb_alcunha.Text);
                    cmd2.Parameters.AddWithValue("@rua", tb_rua.Text);
                    cmd2.Parameters.AddWithValue("@apartamento", tb_apartamento.Text);
                    cmd2.Parameters.AddWithValue("@pais", selectedCountry);
                    cmd2.Parameters.AddWithValue("@cidade", tb_cidade.Text);
                    cmd2.Parameters.AddWithValue("@codigoPostal", tb_codigo_postal.Text);
                    cmd2.Parameters.AddWithValue("@telemovel", tb_telemovel.Text);

                    SqlParameter retorno2 = new SqlParameter("@retorno", SqlDbType.Int);
                    retorno2.Direction = ParameterDirection.Output;
                    cmd2.Parameters.Add(retorno2);



                    myConn2.Open();
                    cmd2.ExecuteNonQuery();

                    myConn2.Close();


                    try
                    {
                        string html = "<h1>Autoparts</h1><br/>" +
    "<h2>Encomenda Numero " + encomenda_id + "</h2><br/><br/>" +
    "<h3>Dados Pessoais</h3><br/>" +
    "<p>Nome: <b>" + tb_nome.Text + "</b> | Alcunha: <b>" + tb_alcunha.Text + "</b></p><br/>" +
    "<p>Numero de Telemóvel: <b>" + tb_telemovel.Text + " </b></p><br/><br/>" +
    "<h3>Dados Envio</h3><br/>" +
    "<p>Rua: <b>" + tb_rua.Text + "</b> | Apartamento: <b>" + tb_apartamento.Text + "</b></p><br/>" +
    "<p>País: <b>" + selectedCountry + "</b> | Cidade: <b>" + tb_cidade.Text + "</b></p><br/>" +
    "<p>Código Postal: <b>" + tb_codigo_postal.Text + "</b></p><br/><br/>" +
    "<h3>Total da encomenda: " + total + "</h3><p> Método de Pagamento: <b>" + selectedValue + "</b></p>";

                        GerarPdfEnviarEmail(html, encomenda_id, email_user);

                    }
                    catch (Exception ex)
                    {
                        lbl_erro.Text = ex.Message;

                    }

                }
                catch (Exception ex)
                {
                    lbl_erro.Text = ex.Message;
                }


            }
            catch (Exception ex)
            {
                lbl_erro.Text = ex.Message;
            }

        }

        public void GerarPdfEnviarEmail(string html, int encomenda_id, string email_user)
        {
            // Gere o PDF
            var pdfDocument = PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);
            string caminhoDaPastaPDFs = Server.MapPath("~/PDFS");
            string nomeDoArquivoPDF = encomenda_id + ".pdf"; // Use o número da encomenda como nome do arquivo
            string caminhoDoPDF = Path.Combine(caminhoDaPastaPDFs, nomeDoArquivoPDF);
            pdfDocument.Save(caminhoDoPDF);

            // Envie o PDF por email
            MailMessage mail = new MailMessage();
            SmtpClient servidor = new SmtpClient();

            mail.From = new MailAddress(ConfigurationManager.AppSettings["SMTP_USER"]);
            mail.To.Add(new MailAddress(email_user));
            mail.Subject = "Encomenda numero " + encomenda_id;
            mail.IsBodyHtml = true;

            // Anexe o PDF ao email
            MemoryStream pdfStream = new MemoryStream(File.ReadAllBytes(caminhoDoPDF));
            Attachment anexo = new Attachment(pdfStream, nomeDoArquivoPDF, "application/pdf");
            mail.Attachments.Add(anexo);

            // Corpo do email
            mail.Body = "<h1>Autoparts</h1><br/>" +
                "<h2>Encomenda Numero " + encomenda_id + "</h2>";

            servidor.Host = ConfigurationManager.AppSettings["SMTP_HOST"];
            servidor.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);

            string smtpUtilizador = ConfigurationManager.AppSettings["SMTP_USER"];
            string smtpPassword = ConfigurationManager.AppSettings["SMTP_PASS"];

            servidor.Credentials = new NetworkCredential(smtpUtilizador, smtpPassword);
            servidor.EnableSsl = true;

            servidor.Send(mail);

            Response.Redirect("donecheck.aspx");
        }



    }
}