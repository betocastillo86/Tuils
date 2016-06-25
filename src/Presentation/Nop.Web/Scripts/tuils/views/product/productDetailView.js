define(['jquery', 'underscore', 'baseView', 'productModel', 'resources',/*'tuils/models/review', 'tuils/views/product/reviewView',*/'tuils/views/product/questionView', 'jpopup'/*, 'jtabs'*/],
    function ($, _, BaseView, ProductModel, Resources, /*ReviewModel, ReviewView,*/ QuestionView) {

        var ProductDetailView = BaseView.extend({

            productId: 0,

            vendorUrl: undefined,

            viewReviews: undefined,

            viewQuestions: undefined,

            alreadyBougth : false,

            events: {
                'click #btnShowVendor': 'createOrder',
                'click #divVendorInfoResponsive': 'cancelMoreInfoResp',
                'click .rating a': 'showReviews',
                'click .actionVendor' : 'actionVendor'
            },

            initialize: function (args) {
                

                this.loadControls();
                this.model = new ProductModel();
                this.model.set('Id', this.productId);
            },
            loadControls: function () {
                this.productId = parseInt($("#ProductId").val());
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
            loadComments: function () {
                this.viewQuestions = new QuestionView({ el: '#product-questions' });
                //agrega la vista de preguntas como una de las que requiere autenticacion
                this.requiredViewsWithAuthentication.push(this.viewQuestions);
            },
            createOrder: function (e) {
                this.redirectToVendor();
                if (e && e.target)
                {
                    var obj = $(e.target);
                    this.disableButtonForSeconds(obj);
                }

                this.model.moreInfo();
            },
            cancelMoreInfoResp: function () {
                this.$('#divVendorInfoResponsive').fadeOut();
                //Quita el evitar hacer scroll
                $('body').removeClass('body-noscroll').removeAttr('style');
            },
            actionVendor: function (obj) {
                var targetFor = $(obj.currentTarget).attr('for');
                this.trackGAEvent('Vendor'+targetFor);
            },
            redirectToVendor: function (model) {

                if (this.isMinSize())
                {
                    this.$('#divVendorInfoResponsive').show();
                    $('body').addClass('body-noscroll');
                }
                else
                {
                    this.$('#btnShowVendor').hide();
                    this.$('.product-vendor').show();
                }
                this.$('#phoneHashed').hide();
                this.trackGAEvent('Compra', 'Exitosa');
            }
        });

        return ProductDetailView;
    });