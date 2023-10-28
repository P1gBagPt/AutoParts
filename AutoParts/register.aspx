<%@ Page Title="" Language="C#" MasterPageFile="~/main_master.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="AutoParts.registerWmaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        @import url(https://fonts.googleapis.com/css?family=Roboto:500);

        .google-btn {
            width: 184px;
            height: 42px;
            background-color: #4285f4;
            border-radius: 2px;
            box-shadow: 0 3px 4px 0 rgba(0, 0, 0, 0.25);
        }

            .google-btn .google-icon-wrapper {
                position: absolute;
                margin-top: 1px;
                margin-left: 1px;
                width: 40px;
                height: 40px;
                border-radius: 2px;
                background-color: #fff;
            }

            .google-btn .google-icon {
                position: absolute;
                margin-top: 11px;
                margin-left: 11px;
                width: 18px;
                height: 18px;
            }

            .google-btn .btn-text {
                float: right;
                margin: 11px 11px 0 0;
                color: #fff;
                font-size: 14px;
                letter-spacing: 0.2px;
                font-family: "Roboto";
            }

            .google-btn:hover {
                box-shadow: 0 0 6px #4285f4;
            }

            .google-btn:active {
                background: #1669f2;
            }
    </style>
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

                        <div class="google-btn">
                            <div class="google-icon-wrapper">
                                <img class="google-icon" src="https://upload.wikimedia.org/wikipedia/commons/5/53/Google_%22G%22_Logo.svg" />
                            </div>
                            <asp:LinkButton ID="btn_googleLogin" runat="server" OnClick="btn_googleLogin_Click" CausesValidation="False"> <p class="btn-text"><b>Registo com google</b></p> </asp:LinkButton>
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
