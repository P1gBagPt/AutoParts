<%@ Page Title="" Language="C#" MasterPageFile="~/main_master.Master" AutoEventWireup="true" CodeBehind="carrinho.aspx.cs" Inherits="AutoParts.carrinho" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="shopify-cart padding-large">
        <div class="container">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="cart-table">
                        <div class="cart-header border-bottom ">
                            <div class="row d-flex">
                                <h3 class="cart-title col-lg-3 text-center">Produto</h3>
                                <h3 class="cart-title col-lg-3 text-center">Quantidade</h3>
                                <h3 class="cart-title col-lg-3 text-center">Subtotal</h3>
                                <h3 class="cart-title col-lg-3 text-center">Remover</h3>
                                <h3 class="cart-title col-lg-3 text-center">Atualizar</h3>
                            </div>
                        </div>
                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound1" OnItemCommand="Repeater1_ItemCommand">
                            <ItemTemplate>
                                <div class="cart-item border-bottom padding-small">

                                    <div class="row">
                                        <div class="col-lg-4 col-md-3">
                                            <div class="row cart-info d-flex flex-wrap">
                                                <div class="col-lg-5">
                                                    <div class="card-image">
                                                        <a href='<%# "produto.aspx?productId=" + Eval("id_produto") %>'>
                                                            <img src='data:<%# Eval("contenttype") %>;base64,<%# Convert.ToBase64String((byte[])Eval("imagem")) %>' alt='<%# Eval("nome") %>' class="img-fluid">
                                                        </a>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4">
                                                    <div class="card-detail">
                                                        <h3 class="card-title">
                                                            <a href='<%# "produto.aspx?productId=" + Eval("id_produto") %>'><%# Eval("nome") %></a>
                                                        </h3>
                                                        <div class="card-price">
                                                            <span class="money text-primary"><%# Eval("preco") %> €</span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-7">
                                            <div class="row d-flex">
                                                <div class="col-md-6">
                                                    <div class="qty-number d-flex align-items-center justify-content-start">
                                                        <asp:TextBox ID="tb_quantidade" runat="server" TextMode="Number" CssClass="spin-number-output" Value='<%# Eval("quantidade") %>' max='<%# Eval("stock") %>' min="1" onkeydown="return event.keyCode === 38 || event.keyCode === 40;"></asp:TextBox>
                                                    </div>

                                                </div>
                                                <div class="col-md-4">
                                                    <div class="total-price">
                                                        <span class="money text-primary">
                                                            <asp:Label ID="lbl_subtotal" runat="server"></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-1 col-md-2">
                                            <div class="cart-remove">
                                                <asp:LinkButton ID="lb_remover" runat="server" CommandArgument='<%# Eval("id_carrinho") %>' OnCommand="lb_remover_Command" CommandName="Remover"><i class="icon icon-close"></i></asp:LinkButton>

                                            </div>
                                        </div>
                                        <div class="col-lg-1 col-md-2">
                                            <div class="cart-remove">
                                                <asp:LinkButton ID="lb_atualizar" runat="server" CommandArgument='<%# Eval("id_carrinho") %>' CommandName="Atualizar">
                                                    <i class="icon icon-close"></i>
                                                </asp:LinkButton>



                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="cart-totals">
                        <h2 class="section-title">Total do carrinho</h2>
                        <div class="total-price">
                            <table cellspacing="0" class="table">
                                <tbody>
                                    <tr class="order-total">
                                        <h2 class="section-title"><asp:Label ID="lbl_vazio" runat="server" Visible="False" Enabled="False"></asp:Label></h2>

                                        <th>Total:</th>
                                        <td data-title="Total">
                                            <span class="price-amount amount text-primary">
                                                <bdi>
                                                    <span class="price-currency-symbol"></span>
                                                    <bdi>
                                                        <asp:Literal ID="ltTotal" runat="server"></asp:Literal>
                                                        €</bdi>
                                            </span>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="button-wrap">
                            <a>
                                <asp:Button ID="btn_continuar_comprar" runat="server" class="btn btn-dark btn-medium" Text="Continuar a comprar" OnClick="btn_continuar_comprar_Click" /></a>
                            <a>
                                <asp:Button ID="btn_checkout" runat="server" class="btn btn-dark btn-medium" Text="Proceder para o Checkout" OnClick="btn_checkout_Click"/></a>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </section>




</asp:Content>
