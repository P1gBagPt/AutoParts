<%@ Page Title="" Language="C#" MasterPageFile="~/admin_master.Master" AutoEventWireup="true" CodeBehind="bo_users.aspx.cs" Inherits="AutoParts.bo_users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <main id="main" class="main">

        <div class="pagetitle">
            <h1>Utilizadores</h1>
            <nav>
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="index_admin.aspx">Inicio</a></li>
                    <li class="breadcrumb-item active">Utilizadores</li>
                </ol>
            </nav>
        </div>
        <!-- End Page Title -->

        <section class="section">
            <div class="row">
                <div class="col-lg-12">

                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Utilizadores</h5>

                            <!--<a href="bo_adicionar_produto.aspx">Adicionar produto</a>-->

                            <!-- Primary Color Bordered Table -->
                            <table class="table table-bordered border-primary">
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <HeaderTemplate>
                                        <thead>
                                            <tr>
                                                <th scope="col">Nome</th>
                                                <th scope="col">Email</th>
                                                <th scope="col">Username</th>
                                                <th scope="col">Tipo de Cliente</th>
                                                <th scope="col">Verificado</th>
                                                <th scope="col">Revenda</th>
                                                <th scope="col">Role</th>
                                                <th scope="col">Editar</th>
                                            </tr>
                                        </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tbody>
                                            <tr>
                                                <th scope="row"><%# Eval("nome")%></th>
                                                <td scope="row"><%# Eval("email")%></td>
                                                <td scope="row"><%# Eval("username")%></td>
                                                <td scope="row">
                                                    <%# Eval("tipoCliente").ToString() == "1" ? "Cliente" : Eval("tipoCliente").ToString() == "2" ? "Revendedor" : "Other" %>
                                                </td>
                                                <td scope="row"><%# Convert.ToBoolean(Eval("verificado")) ? "Sim" : "Não" %></td>
                                                <td scope="row">
                                                    <span style='<%# Eval("tipoCliente").ToString() == "2" ? "color: green;": "" %>'>
                                                        <%# Eval("tipoCliente").ToString() == "2" ? "Sim" : Eval("tipoCliente").ToString() == "1" ? "-------------" : "Nao" %>
                                                    </span>
                                                </td>
                                                <td scope="row"><%# Eval("role")%></td>

                                                <td scope="row">
                                                    <asp:LinkButton ID="editar_produto" runat="server" OnCommand="editar_produto_Command" CommandName="Edit" CommandArgument='<%# Eval("userId") %>'>
                                                    <img src="admin_assets/img/editar.png" alt="Editar" />
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <!-- End Primary Color Bordered Table -->
                        </div>
                    </div>
                </div>
            </div>
        </section>


    </main>
</asp:Content>
