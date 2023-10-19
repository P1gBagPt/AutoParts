<%@ Page Title="" Language="C#" MasterPageFile="~/admin_master.Master" AutoEventWireup="true" CodeBehind="bo_produtos.aspx.cs" Inherits="AutoParts.bo_produtos" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .stock-red {
            color: red !important;
        }

        .stock-yellow {
            color: #ebc334 !important;
        }

        .stock-green {
            color: green !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <main id="main" class="main">

        <div class="pagetitle">
            <h1>Produtos</h1>
            <nav>
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="index_admin.aspx">Inicio</a></li>
                    <li class="breadcrumb-item active">Produtos</li>
                </ol>
            </nav>
        </div>
        <!-- End Page Title -->

        <section class="section">
            <div class="row">
                <div class="col-lg-12">

                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Produtos</h5>

                            <a href="bo_adicionar_produto.aspx">Adicionar produto</a>



                            <!-- Primary Color Bordered Table -->
                            <table class="table table-bordered border-primary">
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <HeaderTemplate>
                                        <thead>
                                            <tr>
                                                <th scope="col">Stock</th>
                                                <th scope="col">Marca</th>
                                                <th scope="col">Nome</th>                                               
                                                <th scope="col">Código</th>
                                                <th scope="col">Preço</th>
                                                <th scope="col">Descrição</th>
                                                <th scope="col">Categoria</th>
                                                <th scope="col">Editar</th>
                                                <th scope="col">Estado</th>
                                                <th scope="col">Ativar/Desativar</th>
                                            </tr>
                                        </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tbody>
                                            <tr>
                                                <td scope="row" class='<%# GetStockColor(Eval("stock")) %>'><%# Eval("stock")%></td>
                                                <td scope="row"><%# Eval("marca")%></td>
                                                <td scope="row"><%# Eval("nome")%></td>
                                                <td scope="row"><%# Eval("codigoArtigo")%></td>
                                                <td scope="row"><%# Eval("preco")%> €</td>
                                                <td scope="row"><%# Eval("descricao")%></td>
                                                <td scope="row"><%# Eval("categoria")%></td>
                                                <td scope="row">
                                                    <asp:LinkButton ID="editar_produto" runat="server" OnCommand="editar_produto_Command" CommandName="Edit" CommandArgument='<%# Eval("id_produto") %>'>
                                                        <img src="admin_assets/img/editar.png" alt="Editar" />
                                                    </asp:LinkButton>
                                                </td>
                                                <td scope="row">
                                                    <asp:Image ID="imgEstado" runat="server" ImageUrl='<%# Convert.ToBoolean(Eval("estado")) ? "admin_assets/img/sim.png" : "admin_assets/img/nao.png" %>' />
                                                </td>
                                                <td scope="row">
                                                    <asp:LinkButton ID="lb_ativar_desativar" runat="server" CssClass='<%# Convert.ToBoolean(Eval("estado")) ? "btn btn-danger" : "btn btn-success" %>' CommandArgument='<%# Eval("id_produto") %>' OnCommand="lb_ativar_desativar_Click" CommandName="AtivarDesativar"><%# Convert.ToBoolean(Eval("estado")) ? "Desativar" : "Ativar" %></asp:LinkButton>
                                                </td>
  
                                            </tr>
                                        </tbody>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </table>
                                <nav aria-label="...">
                                    <ul class="pagination">
                                        <li class="page-item">
                                            <asp:LinkButton ID="lbPrevious" runat="server" OnClick="lbPrevious_Click" CssClass="page-link" Text="Previous"></asp:LinkButton>
                                        </li>
                                        <asp:DataList ID="rptPaging" runat="server" OnItemCommand="rptPaging_ItemCommand" OnItemDataBound="rptPaging_ItemDataBound" RepeatDirection="Horizontal">
                                            <ItemTemplate>
                                                <li class="page-item">
                                                    <asp:LinkButton ID="lbPaging" runat="server" CommandArgument='<%# Eval("PageIndex") %>' CommandName="newPage" CssClass="page-link"><%# Eval("PageText") %></asp:LinkButton>
                                                </li>
                                            </ItemTemplate>
                                        </asp:DataList>
                                        <li class="page-item">
                                            <asp:LinkButton ID="lbNext" runat="server" OnClick="lbNext_Click" CssClass="page-link" Text="Next"></asp:LinkButton>
                                        </li>
                                    </ul>
                                </nav>


                            <!-- End Primary Color Bordered Table -->
                        </div>
                    </div>

                </div>
            </div>
        </section>

    </main>
    <!-- End #main -->
</asp:Content>
