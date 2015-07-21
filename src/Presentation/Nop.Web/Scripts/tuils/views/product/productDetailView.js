﻿define(['jquery', 'underscore', 'baseView', 'tuils/models/order', 'resources','tuils/models/review', 'tuils/views/product/reviewView','tuils/views/product/questionView', 'jpopup', 'jtabs'],
    function ($, _, BaseView, OrderModel, Resources, ReviewModel, ReviewView, QuestionView) {

        var ProductDetailView = BaseView.extend({

            productId: 0,

            vendorUrl: undefined,

            viewReviews: undefined,

            viewQuestions: undefined,

            events: {
                'click #btnShowVendor': 'confirmShowVendor'
            },

            initialize: function (args) {

                this.loadControls();

                this.model = new OrderModel();
                this.model.on('sync', this.redirectToVendor, this);
                this.model.set('ProductId', this.productId);

                this.validateAuthorization();
            },
            loadControls: function () {
                this.productId = parseInt($("#productId").val());
                this.loadGallery();
                this.loadTabs();
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
            loadTabs : function(){
                var that = this;
                $('#tab-container').easytabs();
                $('#tab-container').on('easytabs:ajax:complete', function (a,b) {
                    if (b.attr("data-target") == "#product-reviews-page")
                        that.viewReviews = new ReviewView({ el: '#product-reviews-page' });
                });
            },
            loadReviews : function(){
                this.viewReviews = new ReviewView({ el: '#product-reviews-page' });
            },
            loadComments: function () {
                this.viewQuestions = new QuestionView({ el: '#product-questions' });
                var that = this;
                //this.viewQuestions.on('unauthorized', function () { that.trigger('unauthorized'); });
                //agrega la vista de preguntas como una de las que requiere autenticacion
                this.requiredViewsWithAuthentication.push(this.viewQuestions);
            },
            confirmShowVendor: function (obj) {
                obj = $(obj.target);
                this.vendorUrl = obj.attr('data-vendorUrl');
                this.createOrder();
            },
            createOrder: function () {
                this.model.newOrder();
            },
            userAuthenticated: function () {
                this.createOrder();
            },
            redirectToVendor: function () {
                document.location.href = this.vendorUrl;
            }
        });

        return ProductDetailView;
    });