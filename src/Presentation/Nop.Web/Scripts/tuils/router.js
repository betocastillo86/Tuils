﻿define(['jquery', 'underscore', 'backbone', 'configuration', 'storage', 'publishProductView',
  'tuils/views/panel/myAccount','tuils/views/panel/vendorServices','tuils/views/panel/questionsView','tuils/views/vendor/vendorDetailView'			
,'tuils/views/product/productDetailView','tuils/views/common/newsletterView','tuils/views/common/searcherView','tuils/views/common/topMenuView','tuils/views/common/leftFeaturedProductsView'	
, 'tuils/views/common/header', 'tuils/views/panel/offices', 'tuils/views/panel/menu', 'tuils/views/panel/myProductsView', 'tuils/views/home/homeView',

'ajaxCart', 'nopCommon'],
    function ($, _, Backbone, TuilsConfiguration, TuilsStorage, PublishProductView,
        MyAccountView,VendorServicesView ,QuestionsView ,VendorDetailView,
        ProductDetailView, NewsletterView, SearchView, TopMenuView, LeftFeaturedProductsView, HeaderView, OfficesView, MenuPanelView, MyProductsView,
        HomeView) {

        var TuilsRouter = Backbone.Router.extend({
            currentView: undefined,

            //listado de vistas comunes
            viewHeader: undefined,
            viewLeftMenu: undefined,
            viewTopMenu: undefined,
            viewSearcher: undefined,
            viewNewsletter: undefined,
            viewLeftFeatured : undefined,

            //el por defecto para las vistas
            defaultEl: "#divMainSection",

            routes: {
                "": "home",
                "quiero-vender": "sell",
                "quiero-vender/producto": "sellProduct",
                "quiero-vender/moto": "sellBike",
                "quiero-vender/servicio-especializado": "sellService",
                "mi-cuenta/datos-basicos": "myAccount",
                "ControlPanel/Offices": "myOffices",
                "ControlPanel/VendorServices": "vendorServices",
                "mi-cuenta/mis-compras(/:query)": "myOrders",
                "mi-cuenta/mis-ventas(/:query)": "myOrders",
                "mi-cuenta/mis-productos(/:query)": "myProducts",
                "ControlPanel/Questions(/:query)": "questions",
                "customer/changepassword" : "changePassword",
                "v/:query": "vendor",
                "c/:categoryName/:attribute(/:query)": "category",
                "m/:query":"manufacturer",
                "p/:query": "product",
                'entrar' : 'login',
                'buscar(/:query)' : 'search'
            },
            home : function()
            {
                this.currentView = new HomeView();
                this.loadSubViews();
            },
            sell : function()
            {
                this.loadSubViews();
            },
            sellProduct: function () {
                var that = this;
                //require(['publishProductView'], function (PublishProductView) {
                    that.loadSubViews();
                    that.currentView = new PublishProductView({ el: that.defaultEl, productType: TuilsConfiguration.productBaseTypes.product });
                    //Se cargan las referencias de las motocicletas desde el comienzo
                    //TuilsStorage.loadBikeReferences();
                //});
            },
            sellBike: function () {
                var that = this;
                //require(['publishProductView'], function (PublishProductView) {
                    that.loadSubViews();
                    that.currentView = new PublishProductView({ el: that.defaultEl, productType: TuilsConfiguration.productBaseTypes.bike });
                //});
            },
            sellService: function () {
                var that = this;
                //require(['publishProductView'], function (PublishProductView) {
                    that.loadSubViews();
                    that.currentView = new PublishProductView({ el: that.defaultEl, productType: TuilsConfiguration.productBaseTypes.service });
                    //Se cargan las referencias de las motocicletas desde el comienzo
                    //TuilsStorage.loadBikeReferences();
               // });
            },
            myAccount: function () {
                var that = this;
               // require(['tuils/views/panel/myAccount'], function (MyAccountView) {
                    that.currentView = new MyAccountView({ el: that.defaultEl });
                //});
                this.loadSubViewsPanel();
            },
            myOffices: function () {
                var that = this;
                //require(['tuils/views/panel/offices'], function (OfficesView) {
                    that.currentView = new OfficesView({ el: that.defaultEl });
                //});
                this.loadSubViewsPanel();
            },
            myOrders: function () {
                this.loadSubViewsPanel();
            },
            changePassword : function()
            {
                this.loadSubViewsPanel();
            },
            myProducts : function()
            {
                var that = this;
                //require(['tuils/views/panel/myProductsView'], function (MyProductsView) {
                    that.currentView = new MyProductsView({ el: that.defaultEl });
               // });
                this.loadSubViewsPanel();
            },
            questions: function () {
                var that = this;
               // require(['tuils/views/panel/questionsView'], function (QuestionsView) {
                    that.currentView = new QuestionsView({ el: that.defaultEl });
               // });
                this.loadSubViewsPanel();
            },
            vendorServices: function () {
                var that = this;
               // require(['tuils/views/panel/vendorServices'], function (VendorServicesView) {
                    that.currentView = new VendorServicesView({ el: that.defaultEl });
                //});
                this.loadSubViewsPanel();
            },
            vendor : function(query)
            {
                $(".master-wrapper-main").first().removeClass("master-wrapper-main");
                $(".master-wrapper-page").first().removeClass("master-wrapper-page").removeClass("container").removeClass("hd");
                var that = this;
                //require(['tuils/views/vendor/vendorDetailView'], function (VendorDetailView) {
                    that.currentView = new VendorDetailView({ el: that.defaultEl });
               // });
            },
            category : function(categoryName, specification, query)
            {
                this.loadTwoColumns();
            },
            manufacturer : function(){
                this.loadTwoColumns();
            },
            search: function () {
                this.loadTwoColumns();
            },
            login: function () {
                this.loadTwoColumns();
            },
            product: function () {
                var that = this;
                //require(['tuils/views/product/productDetailView'], function (ProductDetailView) {
                    that.currentView = new ProductDetailView({ el: that.defaultEl });
                    that.loadSubViews();
                //});
            },
            loadTwoColumns : function()
            {
                this.loadSubViews();
                this.loadLeftFeaturedProducts();
                this.loadNewsletter();
            },
            loadSubViews: function () {
                this.loadHeader();
                this.loadMenu();
                this.loadSearcher();
            },
            loadNewsletter: function () {
                var that = this;
                //require(['tuils/views/common/newsletterView'], function (NewsletterView) {
                    that.viewNewsletter = new NewsletterView();
                //});;
            },
            loadSearcher: function () {
                var that = this;
               // require(['tuils/views/common/searcherView'], function (SearchView) {
                    that.viewSearcher = new SearchView({ el: 'header' });
               // });;
            },
            loadMenu: function () {
                var that = this;
               // require(['tuils/views/common/topMenuView'], function (TopMenuView) {
                    that.viewTopMenu = new TopMenuView({ el: '.header-menu' });
                //});;
            },
            loadLeftFeaturedProducts: function () {
                var that = this;
               // require(['tuils/views/common/leftFeaturedProductsView'], function (LeftFeaturedProductsView) {
                    that.viewLeftFeatured = new LeftFeaturedProductsView({ el: '.bestsellers' });
               // });;
            },
            loadHeader : function()
            {
                var that = this;
               // require(['tuils/views/common/header'], function (HeaderView) {
                    that.viewHeader = new HeaderView();

                    if (that.currentView != undefined)
                    {
                        //se atacha al evento de solicitud de ingreso
                        that.currentView.on('unauthorized', that.viewHeader.showLogin, that.viewHeader);
                        //atacha a la vista actual al evento cuando el usuario se autenticó
                        that.viewHeader.on('user-authenticated', that.currentView.userAuthenticated, that.currentView);
                    }
                    
              //  });
            },
            
            loadSubViewsPanel: function () {
                var that = this;
                //require(['tuils/views/panel/menu'], function (MenuPanelView) {
                    that.viewLeftMenu = new MenuPanelView({ el : ".menu-panel" });
                //});
            }
        });

        return TuilsRouter;
    });

