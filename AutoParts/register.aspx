<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="AutoParts.register" %>

<%@ MasterType VirtualPath="~/main_master.master" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AutoParts Registo</title>
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@100;200;300;400;500;600;700;800;900&display=swap" rel="stylesheet" />
    <link href="assets/css/formsRLAPass.css" rel="stylesheet" />

    <style>
        .home a {
            text-decoration: none;
        }

            .home a:visited {
                text-decoration: none;
            }

            .home a:hover {
                text-decoration: none;
                color: black;
            }

            .home a:focus {
                text-decoration: none;
                color: black;
            }

            .home a:active {
                text-decoration: none;
                color: black;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main">

            <section class="signup">
                <!-- <img src="images/signup-bg.jpg" alt=""> -->
                <div class="container">
                    <div class="signup-content">
                        <div class="home">
                            <h3>
                                <a href="index.aspx" class="form-title">Início</a>
                            </h3>
                        </div>
                        <div id="signup-form" class="signup-form">
                            <h2 class="form-title">Criar Conta</h2>
                            <div class="form-group">
                                <asp:TextBox ID="tb_name" class="form-input" placeholder="Nome" runat="server" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Style="color: red;" runat="server" ErrorMessage="Nome obrigatório!" ControlToValidate="tb_name"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="email" class="form-input" name="email" placeholder="Email" runat="server" TextMode="Email" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" runat="server" ErrorMessage="Email obrigatório!" ControlToValidate="email"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <asp:TextBox class="form-input" name="username" ID="username" placeholder="Nome de Utilizador" runat="server" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Style="color: red;" runat="server" ErrorMessage="Nome de Utilizador obrigatório!" ControlToValidate="username"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <asp:TextBox class="form-input" name="password" ID="password" placeholder="Password" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Style="color: red;" runat="server" ErrorMessage="Password Obrigatória" ControlToValidate="password"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="re_password" class="form-input" name="re_password" placeholder="Password Novamente" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:Label ID="lbl_erro" runat="server" Enabled="False" Visible="False" ForeColor="red"></asp:Label>
                            </div>
                            <div class="form-group">
                                <asp:RadioButtonList class="agree-term" ID="rbl_tipoUser" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Selected="True">Cliente</asp:ListItem>
                                    <asp:ListItem Value="2">Revenda</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="form-group">
                                <asp:Button name="submit" ID="submit" class="form-submit" runat="server" Text="Registar" OnClick="submit_Click" Style="background: red; cursor: pointer; color: white;" />
                            </div>
                        </div>
                        <p class="loginhere">
                            Já tens uma conta ? <a href="login.aspx" class="loginhere-link">Login</a>
                        </p>
                    </div>
                </div>
            </section>

        </div>
    </form>
</body>
</html>
