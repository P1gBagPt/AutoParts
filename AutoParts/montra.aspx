<%@ Page Title="" Language="C#" MasterPageFile="~/main_master.Master" AutoEventWireup="true" CodeBehind="montra.aspx.cs" Inherits="AutoParts.montra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="shopify-grid padding-large">
        <div class="container">
            <div class="row">

                <section id="selling-products" class="col-md-9 product-store">
                    <div class="container">
                        <ul class="tabs list-unstyled">
                            <li data-tab-target="#" class="active tab">
                                Tudo
                            </li>
                            <asp:Repeater ID="Repeater3" runat="server">
                                <ItemTemplate>
                                    <li data-tab-target="#" class="tab">
                                        <%# Eval("categoria") %>
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
                                                    <asp:Image ID="img_produto" runat="server" class="product-image" Style="width: 100%; height: 100%;" />
                                                </div>

                                                <div class="product-detail">
                                                    <h3 class="product-title">
                                                        <a href='<%# "produto.aspx?productId=" + Eval("id_produto") %>'><%# Eval("nome") %></a>
                                                    </h3>
                                                    <div class="item-price text-primary"><%# Eval("preco")%> €</div>
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
                                    <button class="btn btn-dark"><i class="icon icon-search"></i></button>
                                </div>
                            </div>
                        </div>
                        <div class="widgets widget-product-tags">
                            <h5 class="widget-title">Marcas</h5>
                            <ul class="product-tags sidebar-list list-unstyled">
                               <asp:Repeater ID="Repeater2" runat="server">
                                    <ItemTemplate>
                                        <li class="tags-item">
                                            <a href=""><%# Eval("nome") %></a>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                   
                        
                    </div>
                </aside>

            </div>
        </div>
    </div>
</asp:Content>
