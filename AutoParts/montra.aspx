<%@ Page Title="" Language="C#" MasterPageFile="~/main_master.Master" AutoEventWireup="true" CodeBehind="montra.aspx.cs" Inherits="AutoParts.montra" %>

<%@ MasterType VirtualPath="~/main_master.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tabs a {
            margin-right: 0px !important;
            color: black;
            color: var(--accent-color);
        }

            .tabs a:hover {
                color: var(--primary-color);
            }



        .marca a {
            margin-right: 0px !important;
            color: black;
            color: var(--accent-color);
        }

            .marca a:hover {
                color: var(--primary-color);
            }

        .ativo_marca a {
            color: var(--primary-color);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="shopify-grid padding-large">
        <div class="container">
            <div class="row">
                <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <section id="selling-products" class="col-md-9 product-store">
                            <div class="container">
                                <ul class="tabs list-unstyled">
                                    <li data-tab-target="#" class="tab">
                                        <asp:LinkButton ID="lb_categoria_filtro_tudo" runat="server" Text="Tudo" OnCommand="lb_categoria_filtro_tudo_Command" CommandName="CategoriaTudo"></asp:LinkButton>
                                    </li>
                                    <asp:Repeater ID="Repeater3" runat="server">
                                        <ItemTemplate>
                                            <li data-tab-target="#" class="tab">
                                                <asp:LinkButton ID="lb_categoria_filtro" runat="server" Text='<%# Eval("categoria") %>' OnCommand="lb_categoria_filtro_Command" CommandName="Categoria" CommandArgument='<%# Eval("id_categoria") %>'></asp:LinkButton>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>


                                <div class="tab-content">
                                    <div id="all" class="active">
                                        <div class="row d-flex flex-wrap">
                                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                                <ItemTemplate>
                                                    <div class="product-item col-lg-3 col-md-3 col-sm-4">
                                                        <div class="image-holder" style="width: 250px; height: 250px; text-align: center; overflow: hidden;">
                                                            <!-- Image -->
                                                            <a href='<%# "produto.aspx?productId=" + Eval("id_produto") %>'>
                                                                <asp:Image ID="img_produto" runat="server" class="product-image" Style="width: 100%; height: 100%;" />
                                                            </a>
                                                        </div>

                                                        <div class="product-detail">
                                                            <h3 class="product-title">
                                                                <a href='<%# "produto.aspx?productId=" + Eval("id_produto") %>'><%# Eval("nome") %></a>
                                                            </h3>
                                                            <div class="item-price text-primary">
                                                                <%# GetFormattedPrice(Eval("preco"), Convert.ToBoolean(Session["revenda"])) %>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                        </div>
                                    </div>
                                </div>


                                <nav class="navigation paging-navigation text-center padding-medium" role="navigation">
                                    <div class="pagination loop-pagination d-flex justify-content-center">

                                        <div class="pagination-arrow d-flex align-items-center">
                                            <asp:LinkButton ID="lbPrevious" runat="server" OnClick="lbPrevious_Click" CssClass="page-link"><i class="icon icon-arrow-left"></i></asp:LinkButton>
                                        </div>

                                        <asp:DataList ID="rptPaging" runat="server" OnItemCommand="rptPaging_ItemCommand" OnItemDataBound="rptPaging_ItemDataBound" RepeatDirection="Horizontal">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbPaging" runat="server" CommandArgument='<%# Eval("PageIndex") %>' CommandName="newPage" CssClass="page-link">
            <%# Eval("PageText") %>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:DataList>

                                        <div class="pagination-arrow d-flex align-items-center">
                                            <asp:LinkButton ID="lbNext" runat="server" OnClick="lbNext_Click" CssClass="page-link"><i class="icon icon-arrow-right"></i></asp:LinkButton>
                                        </div>


                                    </div>
                                </nav>




                            </div>
                        </section>

                        <aside class="col-md-3">
                            <div class="sidebar">
                                <div class="widgets widget-menu">
                                    <div class="widget-search-bar">
                                        <div role="search" class="d-flex">
                                            <asp:TextBox ID="tb_procurar" class="search-field" placeholder="Procurar Produto" runat="server"></asp:TextBox>
                                            <asp:LinkButton ID="btn_pesquisar" runat="server" CssClass="btn btn-dark" OnCommand="btn_pesquisar_Command" CommandName="Procurar">
    <i class="icon icon-search"></i>
                                            </asp:LinkButton>






                                        </div>
                                        <div class="d-flex" style="margin-top: 20px;">
                                            <asp:DropDownList ID="ddl_ordenar" class="u-full-width" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_ordenar_SelectedIndexChanged">
                                                <asp:ListItem Text="Nome Ascendente" Value="nome_asc" />
                                                <asp:ListItem Text="Nome Descendente" Value="nome_desc" />
                                                <asp:ListItem Text="Preço Ascendente" Value="preco_asc" />
                                                <asp:ListItem Text="Preço Descendente" Value="preco_desc" />
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                </div>
                                <div class="widgets widget-product-tags">
                                    <h5 class="widget-title">Marcas</h5>
                                    <ul class="product-tags sidebar-list list-unstyled">

                                        <li class="tag marca">
                                            <asp:LinkButton ID="lb_marcas_tudo" runat="server" Text="Todas as Marcas" OnCommand="lb_marcas_tudo_Command" CommandName="MarcasTudo"></asp:LinkButton>
                                        </li>
                                        <asp:Repeater ID="Repeater2" runat="server">
                                            <ItemTemplate>
                                                <li class="tag marca">
                                                    <asp:LinkButton ID="lb_marca" runat="server" CssClass="active_marca" Text='<%# Eval("nome") %>' OnCommand="lb_marca_Command" CommandName="Marca" CommandArgument='<%# Eval("id_marca") %>'></asp:LinkButton>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>


                            </div>
                        </aside>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


</asp:Content>
