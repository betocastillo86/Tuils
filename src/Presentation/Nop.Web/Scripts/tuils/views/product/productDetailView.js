define(['jquery', 'underscore', 'baseView', 'productModel', 'resources',/*'tuils/models/review', 'tuils/views/product/reviewView',*/'tuils/views/product/questionView', 'jpopup'/*, 'jtabs'*/],
    function ($, _, BaseView, ProductModel, Resources, /*ReviewModel, ReviewView,*/ QuestionView) {

        var ProductDetailView = BaseView.extend({

            productId: 0,

            vendorUrl: undefined,

            viewReviews: undefined,

            viewQuestions: undefined,

            alreadyBougth : false,

            //Bandera que valida si el usuario efectivamente quería ver el vendedor
            //Esto ayuda a controlar que si el usuario se autentica no cargue información que no debe
           // wantedToShowVendor : false,

            events: {
                'click #btnShowVendor': 'createOrder',
                //'click #btnShowVendorResponsive': 'createOrder',
                'click #divVendorInfoResponsive': 'cancelMoreInfoResp',
                'click .rating a' : 'showReviews',
            },

            initialize: function (args) {
                

                this.loadControls();
                this.model = new ProductModel();
                this.model.on('sync', this.redirectToVendor, this);
                this.model.set('Id', this.productId);
                //this.on("user-authenticated", this.createOrder, this);

            },
            loadControls: function () {
                this.productId = parseInt($("#productId").val());
                this.loadGallery();
                //this.loadTabs();
                this.loadComments();
            },
            loadGallery: function () {

                $('#main-product-img-lightbox-anchor-'+this.productId).magnificPopup(
                   {
                       type: 'image',
                       removalDelay: 300,
                       gallery: {
                           enabled: true
                       }
                   });

                $('.thumb-popup-link').magnificPopup(
                {
                    type: 'image',
                    removalDelay: 300,
                    gallery: {
                        enabled: true
                    }
                });
                
            },
            showReviews: function () {
                this.$('.tab[data-name="reviews"] a').click();
            },
            /*loadTabs : function(){
                var that = this;
                $('#tab-container').easytabs();
                $('#tab-container').on('easytabs:ajax:complete', function (a,b) {
                    if (b.attr("data-target") == "#product-reviews-page")
                        that.viewReviews = new ReviewView({ el: '#product-reviews-page' });
                });
            },
            loadReviews : function(){
                this.viewReviews = new ReviewView({ el: '#product-reviews-page' });
            },*/
            loadComments: function () {
                this.viewQuestions = new QuestionView({ el: '#product-questions' });
                //var that = this;
                //this.viewQuestions.on('unauthorized', function () { that.trigger('unauthorized'); });
                //agrega la vista de preguntas como una de las que requiere autenticacion
                this.requiredViewsWithAuthentication.push(this.viewQuestions);
            },
            createOrder: function (e) {
                if (this.alreadyBougth)
                {
                    this.redirectToVendor();
                    return;
                }
                    
                if (e && e.target)
                {
                    var obj = $(e.target);
                    this.disableButtonForSeconds(obj);
                    //this.vendorUrl = obj.attr('data-vendorUrl');
                    //Traquea que un usuario a intentado comprar un producto
                    //this.trackGAEvent('Compra', 'Intento');
                }

                //this.validateAuthorization();
                this.showLoadingAll();
                this.model.moreInfo();
            },
            cancelMoreInfoResp: function () {
                this.$('#divVendorInfoResponsive').fadeOut();
                //Quita el evitar hacer scroll
                $('body').removeClass('body-noscroll').removeAttr('style');
            },
            //userAuthenticated: function () {
            //    //Si quería comprar el producto, despues de aautenticarse realiza de nuevo un intento
            //    //if (this.wantedToShowVendor)
            //    //{
            //        //this.wantedToShowVendor = false;
            //        this.createOrder();
            //   // }
            //},
            redirectToVendor: function (model) {

                if (this.isMinSize())
                {
                    this.$('#divVendorInfoResponsive').show();
                    $('body').addClass('body-noscroll');
                }
                else
                {
                    //Traquea que un usuario a intentado comprar un producto
                    //this.trackGAEvent('Compra', 'Exitosa');
                    /*if (this.vendorUrl) {
                        displayAjaxLoading(true);
                        document.location.href = this.vendorUrl;
                    }
                    else {
                        this.$('#btnShowVendor').hide();
                        this.$('.product-vendor').show();
                        this.scrollFocusObject('.product-vendor', -50);
                    }*/
                    this.$('#btnShowVendor').hide();
                    this.$('.product-vendor').show();
                    
                    
                    //this.scrollFocusObject('.product-vendor', -50);
                }

                //$('body').css({ 'top': $(window).scrollTop() * -1, height: $('body').height() < $(document).height() ? $(document).height() : $('body').height() }).addClass('body-noscroll');

                this.$('#phoneHashed').hide();
                this.alreadyBougth = true;

                if(model)
                    this.trackGAEvent('Compra', 'Exitosa');
            }
        });

        return ProductDetailView;
    });