using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using AngleSharp.Html.Parser;
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
                        string html = "<h1 style=\"font-family: Arial, sans-serif;\">Autoparts</h1><br/>" +
                            "<h2>Encomenda Numero " + encomenda_id + "</h2><br/><br/>" +
                            "<h3>Dados Pessoais</h3><br/>" +
                            string.Format("<p>Nome: <b>{0}</b> | Alcunha: <b>{1}</b></p><br/>", tb_nome.Text, tb_alcunha.Text) +
                            string.Format("<p>Numero de Telemóvel: <b>{0}</b></p><br/><br/>", tb_telemovel.Text) +
                            "<h3>Dados Envio</h3><br/>" +
                            string.Format("<p>Rua: <b>{0}</b> | Apartamento: <b>{1}</b></p><br/>", tb_rua.Text, tb_apartamento.Text) +
                            string.Format("<p>País: <b>{0}</b> | Cidade: <b>{1}</b></p><br/>", selectedCountry, tb_cidade.Text) +
                            string.Format("<p>Código Postal: <b>{0}</b></p><br/><br/>", tb_codigo_postal.Text) +
                            "<h3>Produtos no Carrinho</h3><br/>";

                        List<ProdutoCarrinho> produtosNoCarrinho = ObterProdutosDoCarrinho(encomenda_id, id_user);

                        foreach (var produto in produtosNoCarrinho)
                        {
                            html += $"<img src=\"data:{produto.ContentTypeImagem};base64,{Convert.ToBase64String(produto.Imagem)}\" style=\"max-width: 100px; margin-right: 10px; border: 1px solid black;\" />";

                            html += string.Format("<p>Nome do Produto: <b>{0}</b> | Marca: <b>{1}</b> | Número de Artigo: <b>{2}</b> <br/> Quantidade: <b>{3}</b> | Preço Total: <b>€{4:F2}</b></p>",
                                produto.NomeProduto, produto.Marca, produto.NumeroArtigo, produto.Quantidade, produto.PrecoTotal);

                            html += "<hr style=\"border: 1px solid #ddd;\"/>";

                        }

                        html += string.Format("<h3>Total da encomenda: €{0:F2}</h3><p> Método de Pagamento: <b>{1}</b></p>", total, selectedValue);

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

        public List<ProdutoCarrinho> ObterProdutosDoCarrinho(int encomenda_id, int id_user)
        {
            List<ProdutoCarrinho> produtos = new List<ProdutoCarrinho>();

            string connectionString = ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ToString();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = @"
    SELECT
    p.nome AS NomeProduto,
    p.preco AS PrecoArtigo,
    c.quantidade AS Quantidade,
    m.nome AS Marca,
    p.numero_artigo AS NumeroArtigo,
    c.quantidade * p.preco AS PrecoTotal,
    p.imagem AS ImagemProduto,
    p.contenttype AS ContentTypeImagem
FROM carrinho c
INNER JOIN produtos p ON c.produtoID = p.id_produto
INNER JOIN marcas m ON p.marca = m.id_marca
WHERE c.userID = @id_user
    AND c.encomenda = @encomenda_id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@encomenda_id", encomenda_id);
                    cmd.Parameters.AddWithValue("@id_user", id_user);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProdutoCarrinho produto = new ProdutoCarrinho
                            {
                                NomeProduto = reader["NomeProduto"].ToString(),
                                PrecoArtigo = Convert.ToDecimal(reader["PrecoArtigo"]),
                                Quantidade = Convert.ToInt32(reader["Quantidade"]),
                                Marca = reader["Marca"].ToString(),
                                NumeroArtigo = reader["NumeroArtigo"].ToString(),
                                PrecoTotal = Convert.ToDecimal(reader["PrecoTotal"]),
                                Imagem = reader["ImagemProduto"] as byte[],
                                ContentTypeImagem = reader["ContentTypeImagem"].ToString()
                            };
                            produtos.Add(produto);
                        }
                    }
                }

                con.Close();
            }

            return produtos;
        }

        public class ProdutoCarrinho
        {
            public string NomeProduto { get; set; }
            public decimal PrecoArtigo { get; set; }
            public int Quantidade { get; set; }
            public string Marca { get; set; }
            public string NumeroArtigo { get; set; }
            public decimal PrecoTotal { get; set; }
            public byte[] Imagem { get; set; }
            public string ContentTypeImagem { get; set; }
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
            Session["EncomendaID"] = encomenda_id;
            Response.Redirect("donecheck.aspx");
        }



    }
}