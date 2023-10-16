<%@ Page Title="" Language="C#" MasterPageFile="~/admin_master.Master" AutoEventWireup="true" CodeBehind="bo_editar_user.aspx.cs" Inherits="AutoParts.bo_editar_user" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <main id="main" class="main">
        <section class="section">
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body text-center">
                            <h1 class="card-title">Editar Utilizador</h1>

                            <div class="row">
                                <div class="col-lg-4" style="text-align: left;">
                                    <p>Nome</p>
                                    <asp:TextBox ID="tb_nome" class="form-control" runat="server" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Style="color: red;" runat="server" ErrorMessage="Nome obrigatório!" ControlToValidate="tb_nome"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-4" style="text-align: left;">
                                    <p>Email</p>
                                    <asp:TextBox ID="tb_email" class="form-control" runat="server" MaxLength="255" TextMode="Email"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" runat="server" ErrorMessage="Email obrigatório!" ControlToValidate="tb_email"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-4" style="text-align: left;">
                                    <p>Username</p>
                                    <asp:TextBox ID="tb_username" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Style="color: red;" runat="server" ErrorMessage="Username obrigatório!" ControlToValidate="tb_username"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-4" style="text-align: left;">
                                    <p style="text-align: left;">Tipo Cliente</p>
                                    <asp:DropDownList ID="ddl_tipo_cliente" class="form-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_tipo_cliente_SelectedIndexChanged">
                                        <asp:ListItem Value="1">Cliente</asp:ListItem>
                                        <asp:ListItem Value="2">Revendedor</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                <div class="col-lg-4">
                                    <p style="text-align: left;">Verificado</p>
                                    <asp:Label ID="lbl_verificado" runat="server"></asp:Label>
                                </div>

                                <div class="col-lg-4">
                                    <p style="text-align: left;">Role</p>
                                     <asp:DropDownList ID="ddl_role" class="form-select" runat="server">
                                        <asp:ListItem Value="admin">Administrador</asp:ListItem>
                                        <asp:ListItem Value="cliente">Cliente</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>

                                        <div class="col-lg-4" id="panelContent" runat="server">
                                            <p style="text-align: left;">Aceitar/Negar Revendedor</p>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <asp:Button ID="btn_aceitar" runat="server" class="btn btn-success" Text="Aceitar" OnClick="btn_aceitar_Click" />
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Button ID="btn_recusar" runat="server" class="btn btn-danger" Text="Recusar" OnClick="btn_recusar_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>




                            </div>


                            <asp:Label ID="lbl_erro" runat="server"></asp:Label>

                            <asp:Button ID="btn_editar" runat="server" class="btn btn-primary" Text="Editar Utilizador" OnClick="btn_editar_Click" />


                        </div>
                    </div>
                    <!-- End Default Card -->
                </div>
            </div>
        </section>

    </main>

</asp:Content>
