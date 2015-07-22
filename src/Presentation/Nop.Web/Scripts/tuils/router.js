define(['jquery', 'underscore', 'backbone', 'configuration', 'storage', 'publishProductView',
  'tuils/views/panel/myAccount','tuils/views/panel/vendorServices','tuils/views/panel/questionsView','tuils/views/vendor/vendorDetailView'			
,'tuils/views/product/productDetailView','tuils/views/common/newsletterView','tuils/views/common/searcherView','tuils/views/common/leftFeaturedProductsView'	
, 'tuils/views/common/header', 'tuils/views/panel/offices', 'tuils/views/panel/menu', 'tuils/views/panel/myProductsView', 'tuils/views/home/homeView',
'tuils/views/product/searchView',
'ajaxCart', 'nopCommon'],
    function ($, _, Backbone, TuilsConfiguration, TuilsStorage, PublishProductView,
        MyAccountView,VendorServicesView ,QuestionsView ,VendorDetailView,
        ProductDetailView, NewsletterView, SearcherView, LeftFeaturedProductsView, HeaderView, OfficesView, MenuPanelView, MyProductsView,
        HomeView, SearchView) {

        var TuilsRouter = Backbone.Router.extend({
            currentView: undefined,

            //listado de vistas comunes
            viewHeader: undefined,
            viewLeftMenu: undefined,
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
                "mi-cuenta/sedes": "myOffices",
                "ControlPanel/VendorServices": "vendorServices",
                "mi-cuenta/mis-compras(/:query)": "myOrders",
                "mi-cuenta/mis-ventas(/:query)": "myOrders",
                "mi-cuenta/mis-productos(/:query)": "myProducts",
                "mi-cuenta/preguntas-pendientes(/:query)": "questions",
                "mis-deseos(/:customerGuid)": 'wishlist',
                "comparar": 'compare',
                "customer/changepassword" : "changePassword",
                "v/:query": "vendor",
                "c/:categoryName(/:attribute)(/:query)": "category",
                "m/:query":"manufacturer",
                "p/:query": "product",
                'entrar' : 'login',
                'buscar(/:query)': 'search',
                'recordar-clave': 'passwordRecovery',
                'mapa-del-sitio': 'sitemap',
                'condiciones-de-uso': 'useConditions',
                'contacto': 'contactUs',
                'acerca-de-nosotros' : 'aboutUs'

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
                that.currentView = new PublishProductView({ el: that.defaultEl, productType: TuilsConfiguration.productBaseTypes.product });
                that.loadSubViews();
            },
            sellBike: function () {
                var that = this;
                that.currentView = new PublishProductView({ el: that.defaultEl, productType: TuilsConfiguration.productBaseTypes.bike });
                that.loadSubViews();
            },
            sellService: function () {
                var that = this;
                that.currentView = new PublishProductView({ el: that.defaultEl, productType: TuilsConfiguration.productBaseTypes.service });
                that.loadSubViews();
            },
            myAccount: function () {
                var that = this;
                that.currentView = new MyAccountView({ el: that.defaultEl });
                this.loadSubViewsPanel();
            },
            myOffices: function () {
                var that = this;
                that.currentView = new OfficesView({ el: that.defaultEl });
                this.loadSubViewsPanel();
            },
            myOrders: function () {
                this.loadSubViewsPanel();
            },
            changePassword : function()
            {
                this.loadSubViewsPanel();
            },
            wishlist: function () {
                this.loadTwoColumns();
            },
            compare: function () {
                this.loadTwoColumns();
            },
            myProducts : function()
            {
                var that = this;
                that.currentView = new MyProductsView({ el: that.defaultEl });
                this.loadSubViewsPanel();
            },
            questions: function () {
                var that = this;
                that.currentView = new QuestionsView({ el: that.defaultEl });
                this.loadSubViewsPanel();
            },
            vendorServices: function () {
                var that = this;
                that.currentView = new VendorServicesView({ el: that.defaultEl });
                this.loadSubViewsPanel();
            },
            vendor : function(query)
            {
                $(".master-wrapper-main").first().removeClass("master-wrapper-main");
                $(".master-wrapper-page").first().removeClass("master-wrapper-page").removeClass("container").removeClass("hd");
                this.currentView = new VendorDetailView({ el: this.defaultEl });
                this.loadSubViews();
            },
            sitemap: function () {
                this.loadSubViews();
            },
            category : function(categoryName, specification, query)
            {
                this.loadTwoColumns();
            },
            manufacturer : function(){
                this.loadTwoColumns();
            },
            search: function () {
                this.currentView = new SearchView({ el: this.defaultEl });
                this.loadTwoColumns();
            },
            passwordRecovery: function () {
                this.loadTwoColumns();
            },
            useConditions: function () {
                this.loadTwoColumns();
            },
            contactUs: function () {
                this.loadTwoColumns();
            },
            aboutUs: function () {
                this.loadTwoColumns();
            },
            login: function () {
                this.loadTwoColumns();
            },
            product: function () {
                var that = this;
                that.currentView = new ProductDetailView({ el: that.defaultEl });
                that.loadSubViews();
            },
            loadTwoColumns : function()
            {
                this.loadSubViews();
                this.loadLeftFeaturedProducts();
                this.loadNewsletter();
            },
            loadSubViews: function () {
                this.loadHeader();
                this.loadSearcher();
            },
            loadNewsletter: function () {
                this.viewNewsletter = new NewsletterView();
            },
            loadSearcher: function () {
                var that = this;
                that.viewSearcher = new SearcherView({ el: 'header' });
            },
            loadLeftFeaturedProducts: function () {
                var that = this;
                that.viewLeftFeatured = new LeftFeaturedProductsView({ el: '.bestsellers' });
            },
            loadHeader : function()
            {
                var that = this;

                that.viewHeader = new HeaderView();

                if (that.currentView != undefined)
                {
                    //se atacha al evento de solicitud de ingreso
                    that.currentView.on('unauthorized', that.viewHeader.showLogin, that.viewHeader);
                    //atacha a la vista actual al evento cuando el usuario se autenticó
                    that.viewHeader.on('user-authenticated', that.currentView.userAuthenticated, that.currentView);

                    //Recorre todas las vistas anidadas que requieren autenticación y les agrega los eventos de autorizacion
                    //Esto se hace para controlar estos eventos en las vistas que no son de primer nivel
                    if (that.currentView.requiredViewsWithAuthentication && that.currentView.requiredViewsWithAuthentication.length > 0)
                    {
                        for (var i = 0; i < that.currentView.requiredViewsWithAuthentication.length; i++) {
                            var view = that.currentView.requiredViewsWithAuthentication[i];
                            //se atacha al evento de solicitud de ingreso
                            view.on('unauthorized', that.viewHeader.showLogin, that.viewHeader);
                            //atacha a la vista actual al evento cuando el usuario se autenticó
                            that.viewHeader.on('user-authenticated', view.userAuthenticated, view);
                        }
                    }

                }

            },
            
            loadSubViewsPanel: function () {
                var that = this;
                that.viewLeftMenu = new MenuPanelView({ el: "#divPanelMenu" });
            }
        });

        return TuilsRouter;
    });

