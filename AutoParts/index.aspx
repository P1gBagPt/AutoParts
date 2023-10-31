<%@ Page Title="" Language="C#" MasterPageFile="~/main_master.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="AutoParts.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .product-title {
            height: 60px; /* Defina a altura desejada */
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
        }

        .item-price {
            margin-top: 10px; /* Adiciona um espaçamento entre o título e o preço */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="billboard" class="overflow-hidden">


        <button class="button-prev">
            <i class="icon icon-chevron-left"></i>
        </button>
        <button class="button-next">
            <i class="icon icon-chevron-right"></i>
        </button>
        <div class="swiper main-swiper">
            <div class="swiper-wrapper">
                <div class="swiper-slide" style="background-image: url('assets/images/banner1.jpg'); background-repeat: no-repeat; background-size: cover; background-position: center;">
                    <div class="banner-content">
                        <div class="container">
                            <div class="row">
                                <div class="col-md-6">
                                    <h2 class="banner-title">Precisas de um turbo?</h2>
                                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed eu feugiat amet, libero ipsum enim pharetra hac.</p>
                                    <div class="btn-wrap">
                                        <a href="montra.aspx?categoryID=18" class="btn btn-dark d-flex align-items-center" tabindex="0">Comprar turbo <i class="icon icon-arrow-io"></i>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <section id="featured-products" class="product-store padding-large">
        <div class="container">
            <div class="section-header d-flex flex-wrap align-items-center justify-content-between">
                <h2 class="section-title">4 das nossas melhores marcas!</h2>
                <div class="btn-wrap">
                    <a href="montra.aspx" class="d-flex align-items-center">Ver todos os produtos <i class="icon icon icon-arrow-io"></i></a>
                </div>
            </div>
            <div class="swiper product-swiper overflow-hidden">
                <div class="swiper-wrapper">
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <div class="swiper-slide">
                                <div class="product-item">
                                    <div class="image-holder">
                                        <a href='<%# "produto.aspx?productId=" + Eval("id_produto") %>'>
                                            <asp:Image ID="img_produtoquatroMarcas" runat="server" CssClass="product-image" Style="width: 350px; height: 350px;" ImageUrl='<%# GetBase64Image(Eval("imagem"), Eval("contenttype")) %>' />
                                        </a>
                                    </div>

                                    <div class="product-detail">
                                        <h3 class="product-title">
                                            <a href='<%# "produto.aspx?productId=" + Eval("id_produto") %>'><%# Eval("nome") %></a>
                                        </h3>
                                        <span class="item-price text-primary"><%# Eval("preco") %> €</span>
                                        <br />
                                        <asp:LinkButton ID="lb_adicionar_quatroMarcas" runat="server" CssClass="btn btn-outline-dark" CommandName="AdicionarCarTopMarcas" CommandArgument='<%# Eval("id_produto") %>' OnCommand="lb_adicionar_quatroMarcas_Command">Adicionar ao carrinho</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </section>

    <section id="latest-collection">
        <div class="container">
            <div class="product-collection row">
                <div class="col-lg-7 col-md-12 left-content">
                    <div class="collection-item" style="color: white;">
                        <div class="products-thumb">
                            <img src="assets/images/collection-item1.png" alt="collection item" class="large-image image-rounded">
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 product-entry">
                            <div class="categories" style="color: white;">Os nossos pneus</div>
                            <h3 class="item-title" style="color: white;">Grande variedade de marcas</h3>
                            <p style="color: white;">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Dignissim massa diam elementum.</p>
                            <div class="btn-wrap">
                                <a href="montra.aspx?categoryID=15" class="d-flex align-items-center" style="color: white;">comprar pneus <i class="icon icon-arrow-io"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-5 col-md-12 right-content flex-wrap">
                    <div class="collection-item top-item">
                        <div class="products-thumb">
                            <img src="assets/images/collection-item2.jpg" alt="collection item" class="small-image image-rounded">
                        </div>
                        <div class="col-md-6 product-entry">
                            <h3 style="color: white;" class="item-title">Filtros</h3>
                            <div class="btn-wrap">
                                <a href="montra.aspx?categoryID=17" class="d-flex align-items-center">comprar filtro <i class="icon icon-arrow-io"></i>
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="collection-item bottom-item">
                        <div class="products-thumb">
                            <img src="assets/images/collection-item3.jpg" alt="collection item" class="small-image image-rounded">
                        </div>
                        <div class="col-md-6 product-entry">
                            <div class="categories" style="color: white;">Melhores peças motor</div>
                            <h3 class="item-title" style="color: white;">peças motor</h3>
                            <div class="btn-wrap">
                                <a href="montra.aspx?categoryID=18" style="color: white;" class="d-flex align-items-center">comprar peças motor <i class="icon icon-arrow-io"></i>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>



    <section id="selling-products" class="product-store bg-light-grey padding-large">
        <div class="container">
            <div class="section-header">
                <h2 class="section-title">Produtos mais vendidos</h2>
            </div>
            <div class="tab-content">
                <div id="all">
                    <div class="row d-flex flex-wrap">
                        <asp:Repeater ID="Repeater2" runat="server">
                            <ItemTemplate>
                                <div class="product-item col-lg-3 col-md-6 col-sm-6">
                                    <div class="image-holder">
                                        <a href='<%# "produto.aspx?productId=" + Eval("id_produto") %>'>
                         <asp:Image ID="img_produtoquatroMarcas" runat="server" CssClass="product-image" Style="width: 350px; height: 350px;" ImageUrl='<%# GetBase64Image(Eval("imagem"), Eval("contenttype")) %>' />
                     </a>
                                    </div>
                                    
                                    <div class="product-detail">
                                        <h3 class="product-title">
                         <a href='<%# "produto.aspx?productId=" + Eval("id_produto") %>'><%# Eval("nome") %></a>
                     </h3>
                                        <div class="item-price text-primary"><%# Eval("preco") %> €</div>

                                        <br />
                     <asp:LinkButton ID="lb_adicionar_quatroMarcas" runat="server" CssClass="btn btn-outline-dark" CommandName="AdicionarCarTopMarcas" CommandArgument='<%# Eval("id_produto") %>' OnCommand="lb_adicionar_quatroMarcas_Command">Adicionar ao carrinho</asp:LinkButton>
                 
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <section id="testimonials" class="padding-large no-padding-bottom">
        <div class="container">
            <div class="reviews-content">
                <div class="row d-flex flex-wrap">
                    <div class="col-md-2">
                        <div class="review-icon">
                            <i class="icon icon-right-quote"></i>
                        </div>
                    </div>
                    <div class="col-md-8">
                        <div class="swiper testimonial-swiper overflow-hidden">
                            <div class="swiper-wrapper">
                                <div class="swiper-slide">
                                    <div class="testimonial-detail">
                                        <p>“Dignissim massa diam elementum habitant fames. Id nullam pellentesque nisi, eget cursus dictumst pharetra, sit. Pulvinar laoreet id porttitor egestas dui urna. Porttitor nibh magna dolor ultrices iaculis sit iaculis.”</p>
                                        <div class="author-detail">
                                            <div class="name">By Maggie Rio</div>
                                        </div>
                                    </div>
                                </div>
                                <div class="swiper-slide">
                                    <div class="testimonial-detail">
                                        <p>“Dignissim massa diam elementum habitant fames. Id nullam pellentesque nisi, eget cursus dictumst pharetra, sit. Pulvinar laoreet id porttitor egestas dui urna. Porttitor nibh magna dolor ultrices iaculis sit iaculis.”</p>
                                        <div class="author-detail">
                                            <div class="name">By John Smith</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="swiper-arrows">
                            <button class="prev-button">
                                <i class="icon icon-arrow-left"></i>
                            </button>
                            <button class="next-button">
                                <i class="icon icon-arrow-right"></i>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>





    <br />




</asp:Content>
