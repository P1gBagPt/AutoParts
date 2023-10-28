<%@ Page Title="" Language="C#" MasterPageFile="~/main_master.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="AutoParts.login" %>

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
                            <label for="email_username">Email *</label>
                            <asp:TextBox ID="email_username" class="u-full-width bg-light" name="email_username" placeholder="Email ou Username" runat="server" TextMode="SingleLine" MaxLength="255"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Style="color: red;" runat="server" ErrorMessage="Email obrigatório!" ControlToValidate="email_username"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="password">Password *</label>
                            <asp:TextBox class="u-full-width bg-light" name="password" ID="password" placeholder="Password" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" runat="server" ErrorMessage="Password Obrigatória" ControlToValidate="password"></asp:RequiredFieldValidator>
                        </div>
                        <asp:Label ID="lbl_erro" runat="server" Enabled="False" Visible="False" ForeColor="red"></asp:Label>
                        <asp:LinkButton ID="lbl_erro_enviar" runat="server" Text="" CssClass="error-message" Style="font-size: 18px;" OnClick="lbl_erro_enviar_Click"></asp:LinkButton>
                        <div class="form-group">
                            <asp:LinkButton name="submit" class="btn btn-dark btn-full btn-medium" Text="Login" ID="LinkButton1" runat="server" OnClick="submit_Click" ValidationGroup="log" Style="font-size: 22.5px; padding: 21px 225px; background-color: red; cursor: pointer; color: white;"> <!-- Increase font-size and padding for a bigger button -->
                    Login
                            </asp:LinkButton>
                        </div>


                            <div class="google-btn">
                                <div class="google-icon-wrapper">
                                    <img class="google-icon" src="https://upload.wikimedia.org/wikipedia/commons/5/53/Google_%22G%22_Logo.svg" />
                                </div>
                                <asp:LinkButton ID="btn_googleLogin" runat="server" OnClick="btn_googleLogin_Click" CausesValidation="False"> <p class="btn-text"><b>Login com google</b></p> </asp:LinkButton>
                            </div>
                        <p class="loginhere">
                            Não tem conta ? <a href="register.aspx" class="loginhere-link">Registo</a>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
