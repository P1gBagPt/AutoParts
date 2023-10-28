<%@ Page Title="" Language="C#" MasterPageFile="~/main_master.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="AutoParts.registerWmaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="login-tabs padding-large no-padding-bottom">
        <div class="container">
            <div class="row">
                <div class="tab-content" id="nav-tabContent">
                    <div class="tab-pane fade active show" id="nav-register" role="tabpanel" aria-labelledby="nav-register-tab">
                        <div class="form-group">
                            <label for="tb_name">Nome *</label>
                            <asp:TextBox ID="tb_name" class="u-full-width bg-light" placeholder="Nome" runat="server" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Style="color: red;" runat="server" ErrorMessage="Nome obrigatório!" ControlToValidate="tb_name"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="email">Email *</label>
                            <asp:TextBox ID="email" class="u-full-width bg-light" name="email" placeholder="Email" runat="server" TextMode="Email" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" runat="server" ErrorMessage="Email obrigatório!" ControlToValidate="email"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="username">Username *</label>
                            <asp:TextBox class="u-full-width bg-light" name="username" ID="username" placeholder="Nome de Utilizador" runat="server" MaxLength="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Style="color: red;" runat="server" ErrorMessage="Nome de Utilizador obrigatório!" ControlToValidate="username"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="password">Password *</label>
                            <asp:TextBox class="u-full-width bg-light" name="password" ID="password" placeholder="Password" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Style="color: red;" runat="server" ErrorMessage="Password Obrigatória" ControlToValidate="password"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="re_password" class="u-full-width bg-light" name="re_password" placeholder="Password Novamente" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:Label ID="lbl_erro" runat="server" Enabled="False" Visible="False" ForeColor="red"></asp:Label>
                        </div>
                        <div class="form-group">
                            <asp:RadioButtonList class="u-full-width bg-light" ID="rbl_tipoUser" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" Selected="True">Cliente</asp:ListItem>
                                <asp:ListItem Value="2">Revenda</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                        <asp:Button name="submit" ID="submit" class="btn btn-dark btn-full btn-medium" runat="server" Text="Registar" OnClick="submit_Click" Style="background: red; cursor: pointer; color: white;" />

                        <div class="form-group">
                            <div class="google-btn">
                                <div class="google-icon-wrapper">
                                    <img class="google-icon" src="https://upload.wikimedia.org/wikipedia/commons/5/53/Google_%22G%22_Logo.svg" />
                                </div>
                                <asp:LinkButton ID="btn_googleLogin" runat="server" OnClick="btn_googleLogin_Click" CausesValidation="False"> <p class="btn-text"><b>Register with google</b></p> </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <p class="loginhere">
                    Já tens uma conta ? <a href="login.aspx" class="loginhere-link">Login</a>
                </p>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
