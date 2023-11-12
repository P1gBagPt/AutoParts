<%@ Page Title="" Language="C#" MasterPageFile="~/main_master.Master" AutoEventWireup="true" CodeBehind="checkout.aspx.cs" Inherits="AutoParts.checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="shopify-cart checkout-wrap padding-large">
        <div class="container">
            <div class="form-group">
                <div class="row d-flex flex-wrap">
                    <div class="col-lg-6">
                        <h2 class="section-title">Detalhes de Faturamento</h2>
                        <div class="billing-details">
                            <div class="row">
                                <div class="col-md-6">
                                    <label for="tb_nome">Primeiro Nome*</label>
                                    <asp:TextBox ID="tb_nome" runat="server" name="firstname" class="form-control" MaxLength="50"></asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Nome Obrigatório *" ControlToValidate="tb_nome"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-6">
                                    <label for="tb_alcunha">Ultimo Nome*</label>
                                    <asp:TextBox ID="tb_alcunha" runat="server" name="lastname" class="form-control" MaxLength="50"></asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Alcunha Obrigatório *" ControlToValidate="tb_alcunha"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <label for="tb_rua">Nome e numero da Rua*</label>
                                    <asp:TextBox ID="tb_rua" runat="server" name="address" placeholder="Nome e numero da Rua" class="form-control" MaxLength="255"></asp:TextBox>
                                    <asp:TextBox ID="tb_apartamento" runat="server" name="address" placeholder="Apartamento, suite, etc." class="form-control" MaxLength="50"></asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Rua Obrigatória *" ControlToValidate="tb_rua"></asp:RequiredFieldValidator>

                                </div>
                                

                                <div class="col-md-6">
                                    <label for="cname">País *</label>

                                    <asp:DropDownList ID="ddl_pais" runat="server" class="form-select form-control">
                                        <asp:ListItem Text="Portugal" Value="Portugal" />
                                        <asp:ListItem Text="Australia" Value="Australia" />
                                        <asp:ListItem Text="Canada" Value="Canada" />
                                        <asp:ListItem Text="Alemanha" Value="Alemanha" />
                                        <asp:ListItem Text="França" Value="França" />
                                        <asp:ListItem Text="Espanha" Value="Espanha" />
                                        <asp:ListItem Text="Itália" Value="Itália" />
                                    </asp:DropDownList>

                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <label for="tb_cidade">Cidade *</label>
                                    <asp:TextBox ID="tb_cidade" runat="server" name="city" class="form-control" MaxLength="50"></asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Cidade Obrigatória *" ControlToValidate="tb_cidade"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-md-6">
                                    <label for="tb_codigo_postal">Código Postal *</label>
                                    <asp:TextBox ID="tb_codigo_postal" runat="server" name="zip" class="form-control" MaxLength="50"></asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Código Postal Obrigatório *" ControlToValidate="tb_codigo_postal"></asp:RequiredFieldValidator>

                                </div>
                            </div>

                            <label for="tb_telemovel">Telemóvel *</label>
                            <asp:TextBox ID="tb_telemovel" runat="server" name="phone" class="form-control" MaxLength="50"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Telemóvel Obrigatório *" ControlToValidate="tb_telemovel"></asp:RequiredFieldValidator>

                        </div>
                    </div>
                    <div class="col-lg-6">

                        <div class="your-order">
                            <h2 class="section-title">Total do Carrinho</h2>
                            <div class="total-price">
                                <table cellspacing="0" class="table">
                                    <tbody>
                                        <tr class="order-total">
                                            <th>Total:</th>
                                            <td data-title="Total">

                                                <span class="price-amount amount text-primary">
                                                    <bdi>

                                                        <asp:Literal ID="ltTotal" runat="server"></asp:Literal>
                                                        <span class="price-currency-symbol">€</span>
                                                    </bdi>
                                                </span>

                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <div class="list-group mt-5 mb-3">
                                    <label class="list-group-item d-flex">
                                        <asp:RadioButtonList ID="listGroupRadios" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Cartão de Crédito" Value="CartaoCredito" Selected="True" />
                                            <asp:ListItem Text="Paypal" Value="Paypal" />
                                        </asp:RadioButtonList>
                                    </label>
                                </div>
                                <asp:Button ID="btn_submeter" runat="server" name="submit" class="btn btn-dark btn-full btn-medium" Text="Submeter encomenda" OnClick="btn_submeter_Click" Visible="True" />
                                <asp:Label ID="lbl_erro" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>
