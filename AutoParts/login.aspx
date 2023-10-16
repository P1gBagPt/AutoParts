<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="AutoParts.login" %>

<%@ MasterType VirtualPath="~/main_master.master" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Autoparts Login</title>
    <link href="assets/css/formsRLAPass.css" rel="stylesheet" />

    <style>
        .home a{text-decoration: none;}
        .home a:visited { text-decoration: none; }
        .home a:hover { text-decoration: none; color: black;}
        .home a:focus { text-decoration: none;color: black;}
        .home a:active { text-decoration: none;color: black;}
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
                            <h2 class="form-title">Login</h2>
                            <div class="form-group">
                                <asp:TextBox ID="email_username" class="form-input" name="email_username" placeholder="Email ou Username" runat="server" TextMode="SingleLine" MaxLength="255"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Style="color: red;" runat="server" ErrorMessage="Email obrigatório!" ControlToValidate="email_username"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <asp:TextBox class="form-input" name="password" ID="password" placeholder="Password" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" runat="server" ErrorMessage="Password Obrigatória" ControlToValidate="password"></asp:RequiredFieldValidator>
                            </div>
                            <asp:Label ID="lbl_erro" runat="server" Enabled="False" Visible="False" ForeColor="red"></asp:Label>
                            <asp:LinkButton ID="lbl_erro_enviar" runat="server" Text="" CssClass="error-message" Style="font-size: 18px;" OnClick="lbl_erro_enviar_Click"></asp:LinkButton>
                            <div class="form-group">
                                <asp:LinkButton name="submit" class="login-butao" Text="Login" ID="submit" runat="server" OnClick="submit_Click" ValidationGroup="log" Style="font-size: 22.5px; padding: 21px 225px; background-color: red; cursor: pointer; color: white;"> <!-- Increase font-size and padding for a bigger button -->
                            Login
                                </asp:LinkButton>
                            </div>
                        </div>
                        <p class="loginhere">
                            <a href="recuperar_password.aspx" class="loginhere-link">Recuperar Password</a> / <a href="alterar_pass_input.aspx" class="loginhere-link">Alterar Password</a>
                        </p>
                        <p class="loginhere">
                            Não tem conta ? <a href="register.aspx" class="loginhere-link">Registo</a>
                        </p>
                    </div>
                </div>
            </section>

        </div>
    </form>
</body>
</html>
