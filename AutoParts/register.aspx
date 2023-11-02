<%@ Page Title="" Language="C#" MasterPageFile="~/main_master.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="AutoParts.registerWmaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        @import url("//fonts.googleapis.com/css?family=Roboto");

        *, *::before, *::after {
            box-sizing: border-box;
        }

        html, body {
            width: 100%;
            height: 100%;
        }

        .block-wrap {
            display: flex;
            justify-content: center;
            align-items: center;
            flex-wrap: wrap;
            height: 100%;
        }

            .block-wrap > div {
                width: 100%;
                text-align: center;
            }

        .btn-google, .btn-fb {
            display: inline-block;
            border-radius: 1px;
            text-decoration: none;
            box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.25);
            transition: background-color 0.218s, border-color 0.218s, box-shadow 0.218s;
        }

            .btn-google .google-content, .btn-google .fb-content, .btn-fb .google-content, .btn-fb .fb-content {
                display: flex;
                align-items: center;
                width: 300px;
                height: 50px;
            }

                .btn-google .google-content .logo, .btn-google .fb-content .logo, .btn-fb .google-content .logo, .btn-fb .fb-content .logo {
                    padding: 15px;
                    height: inherit;
                }

                .btn-google .google-content svg, .btn-google .fb-content svg, .btn-fb .google-content svg, .btn-fb .fb-content svg {
                    width: 18px;
                    height: 18px;
                }

                .btn-google .google-content p, .btn-google .fb-content p, .btn-fb .google-content p, .btn-fb .fb-content p {
                    width: 100%;
                    line-height: 1;
                    letter-spacing: 0.21px;
                    text-align: center;
                    font-weight: 500;
                    font-family: "Roboto", sans-serif;
                }

        .btn-google {
            background: #FFF;
        }

            .btn-google:hover {
                box-shadow: 0 0 3px 3px rgba(66, 133, 244, 0.3);
            }

            .btn-google:active {
                background-color: #eee;
            }

            .btn-google .google-content p {
                color: #757575;
            }

        .btn-fb {
            padding-top: 1.5px;
            background: #4267b2;
            background-color: #3b5998;
        }

            .btn-fb:hover {
                box-shadow: 0 0 3px 3px rgba(59, 89, 152, 0.3);
            }

            .btn-fb .fb-content p {
                color: rgba(255, 255, 255, 0.87);
            }

            .social-message {
    display: flex;
    justify-content: center; /* Center horizontally */
    align-items: center; /* Center vertically */
    height: 100%; /* Adjust this value if necessary to control the vertical centering. */
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="login-tabs padding-large no-padding-bottom">
        <div class="container">
            <div class="row">
                <div class="tab-content" id="nav-tabContent">
                    <div class="tab-pane fade active show" id="nav-register" role="tabpanel" aria-labelledby="nav-register-tab">
                        <div class="social-message">
                            <asp:Label ID="lbl_social" runat="server" Visible="False" Enabled="False"></asp:Label>
                        </div>
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
                        <div class="form-group">
                            <asp:Button name="submit" ID="submit" class="btn btn-dark btn-full btn-medium" runat="server" Text="Registar" OnClick="submit_Click" Style="background: red; cursor: pointer; color: white;" />
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:LinkButton ID="btn_googleLogin" runat="server" OnClick="btn_googleLogin_Click" CausesValidation="False" CssClass="btn-google">
    <div class="google-content">
        <div class="logo" style="margin-bottom: 8px;">
            <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 48 48">
                <defs>
                    <path id="a" d="M44.5 20H24v8.5h11.8C34.7 33.9 30.1 37 24 37c-7.2 0-13-5.8-13-13s5.8-13 13-13c3.1 0 5.9 1.1 8.1 2.9l6.4-6.4C34.6 4.1 29.6 2 24 2 11.8 2 2 11.8 2 24s9.8 22 22 22c11 0 21-8 21-22 0-1.3-.2-2.7-.5-4z" />
                </defs>
                <clipPath id="b">
                    <use xlink:href="#a" overflow="visible" />
                </clipPath>
                <path clip-path="url(#b)" fill="#FBBC05" d="M0 37V11l17 13z" />
                <path clip-path="url(#b)" fill="#EA4335" d="M0 11l17 13 7-6.1L48 14V0H0z" />
                <path clip-path="url(#b)" fill="#34A853" d="M0 37l30-23 7.9 1L48 0v48H0z" />
                <path clip-path="url(#b)" fill="#4285F4" d="M48 48L17 24l-4-3 35-10z" />
            </svg>
        </div>
        <p style="margin-top: 19px;">Registar com Google</p>
    </div>
                                    </asp:LinkButton>

                                </div>
                                <div class="col-md-6">
                                    <asp:LinkButton ID="btn_facebookRegisto" runat="server" OnClick="btn_facebookRegisto_Click" CausesValidation="False" CssClass="btn-fb">
    <div class="fb-content">
        <div class="logo" style="margin-bottom: 8px;">
            <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" viewBox="0 0 32 32" version="1">
                <path fill="#FFFFFF" d="M32 30a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2V2a2 2 0 0 1 2-2h28a2 2 0 0 1 2 2v28z"/>
                <path fill="#4267b2" d="M22 32V20h4l1-5h-5v-2c0-2 1.002-3 3-3h2V5h-4c-3.675 0-6 2.881-6 7v3h-4v5h4v12h5z"/>
            </svg>
        </div>
        <p style="margin-top: 19px;">Registar com Facebook</p>
    </div>
                                    </asp:LinkButton>

                                </div>
                            </div>
                        </div>
                         <p class="loginhere">
     Já tens uma conta ? <a href="login.aspx" class="loginhere-link">Login</a>
 </p>
                    </div>
                   
                </div>
            </div>
        </div>
    </section>
</asp:Content>
