using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using static AutoParts.bo_categorias;

namespace AutoParts
{
    public partial class bo_produtos : System.Web.UI.Page
    {
        readonly PagedDataSource _pgsource = new PagedDataSource();
        int _firstIndex, _lastIndex;
        private int _pageSize = 10;
        private int CurrentPage
        {
            get
            {
                if (ViewState["CurrentPage"] == null)
                {
                    return 0;
                }
                return ((int)ViewState["CurrentPage"]);
            }
            set
            {
                ViewState["CurrentPage"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                BindDataIntoRepeater();
            }
            catch (Exception ex)
            {


            }
        }

        // Get data from database/repository
        static DataTable GetDataFromDb()
        {
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ToString());
            var query = "SELECT p.id_produto, p.stock, p.nome, p.numero_artigo AS codigoArtigo, p.preco, p.descricao, p.estado, c.categoria AS categoria, m.nome AS marca " +
            "FROM produtos p " +
            "LEFT JOIN categoria c ON p.categoria = c.id_categoria " +
            "LEFT JOIN marcas m ON p.marca = m.id_marca;";


            var da = new SqlDataAdapter(query, con);
            var dt = new DataTable();

            try
            {
                con.Open();
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return dt;
        }


        // Bind PagedDataSource into Repeater
        private void BindDataIntoRepeater()
        {
            var dt = GetDataFromDb();
            _pgsource.DataSource = dt.DefaultView;
            _pgsource.AllowPaging = true;
            // Number of items to be displayed in the Repeater
            _pgsource.PageSize = _pageSize;
            _pgsource.CurrentPageIndex = CurrentPage;
            // Keep the Total pages in View State
            ViewState["TotalPages"] = _pgsource.PageCount;
            // Example: "Page 1 of 10"
            //lblpage.Text = "Página " + (CurrentPage + 1) + " de " + _pgsource.PageCount;
            // Enable First, Last, Previous, Next buttons
            lbPrevious.Enabled = !_pgsource.IsFirstPage;
            lbNext.Enabled = !_pgsource.IsLastPage;
            /*lbFirst.Enabled = !_pgsource.IsFirstPage;
            lbLast.Enabled = !_pgsource.IsLastPage;*/

            // Bind data into repeater
            Repeater1.DataSource = _pgsource;
            Repeater1.DataBind();

            // Call the function to do paging
            HandlePaging();
        }

        private void HandlePaging()
        {
            var dt = new DataTable();
            dt.Columns.Add("PageIndex"); //Start from 0
            dt.Columns.Add("PageText"); //Start from 1

            _firstIndex = CurrentPage - 5;
            if (CurrentPage > 5)
                _lastIndex = CurrentPage + 5;
            else
                _lastIndex = 10;

            // Check last page is greater than total page then reduced it 
            // to total no. of page is last index
            if (_lastIndex > Convert.ToInt32(ViewState["TotalPages"]))
            {
                _lastIndex = Convert.ToInt32(ViewState["TotalPages"]);
                _firstIndex = _lastIndex - 10;
            }

            if (_firstIndex < 0)
                _firstIndex = 0;

            // Now creating page number based on above first and last page index
            for (var i = _firstIndex; i < _lastIndex; i++)
            {
                var dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }

            rptPaging.DataSource = dt;
            rptPaging.DataBind();
        }

        protected void lbFirst_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            BindDataIntoRepeater();
        }
        protected void lbLast_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            BindDataIntoRepeater();
        }
        protected void lbPrevious_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            BindDataIntoRepeater();
        }
        protected void lbNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            BindDataIntoRepeater();
        }

        protected void rptPaging_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            BindDataIntoRepeater();
        }

        protected void rptPaging_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            var lnkPage = (LinkButton)e.Item.FindControl("lbPaging");
            if (lnkPage.CommandArgument != CurrentPage.ToString()) return;
            lnkPage.Enabled = false;
            lnkPage.BackColor = Color.FromName("#0398fc");
            lnkPage.ForeColor = Color.White;
        }




        protected string GetStockColor(object stock)
        {
            int stockValue = Convert.ToInt32(stock);

            if (stockValue == 0)
            {
                return "stock-red";
            }
            else if (stockValue < 4)
            {
                return "stock-yellow";
            }
            else
            {
                return "stock-green";
            }
        }

        protected void lb_ativar_desativar_Click(object sender, CommandEventArgs e)
        {
            // Obtenha o ID do produto a partir dos dados do item atual (usando Eval, por exemplo).
            //int produtoID = Convert.ToInt32(Eval("id_produto"));
            if (e.CommandName == "AtivarDesativar")
            {
                int produtoID = Convert.ToInt32(e.CommandArgument);

                // Crie uma conexão com o banco de dados.
                using (SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["autoparts_ConnectionString"].ToString()))
                {
                    // Abra a conexão.
                    con.Open();

                    // Consulta SQL para atualizar o estado do produto.
                    string query = "UPDATE produtos SET estado = CASE WHEN estado = 1 THEN 0 ELSE 1 END WHERE id_produto = @produtoID";

                    // Crie e configure o comando SQL.
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@produtoID", produtoID);

                        // Execute a consulta.
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Verifique se a consulta foi executada com sucesso.
                        if (rowsAffected > 0)
                        {
                            // Atualize a interface do usuário para refletir a mudança no estado do produto, se necessário.
                            BindDataIntoRepeater();

                        }
                    }
                }
            }
        }

        protected void editar_produto_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int productId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"bo_editar_produto.aspx?productId={productId}");
            }
        }
    }
}