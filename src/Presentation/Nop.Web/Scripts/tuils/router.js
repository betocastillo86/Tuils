define(['jquery', 'underscore', 'backbone', 'configuration', 'storage', 'publishProductView',
  'tuils/views/panel/myAccount','tuils/views/panel/vendorServices','tuils/views/panel/questionsView','tuils/views/vendor/vendorDetailView'			
,'tuils/views/product/productDetailView','tuils/views/common/newsletterView','tuils/views/common/searcherView','tuils/views/common/leftFeaturedProductsView'	
, 'tuils/views/common/header', 'tuils/views/panel/offices', 'tuils/views/panel/menu', 'tuils/views/panel/myProductsView', 'tuils/views/home/homeView',
'tuils/views/product/searchView', 'tuils/views/product/categoryView', 'tuils/views/product/manufacturerView', 'tuils/views/publishProduct/publishView',
'tuils/views/common/footerView', 'tuils/views/panel/editProductView', 'tuils/views/publishProduct/selectPlanView', 'tuils/views/panel/myOrdersView',
'tuils/views/publishProduct/showPlansView', 'tuils/views/login/staticLoginView', 'tuils/views/vendor/searchVendorsView',
'ajaxCart', 'nopCommon'],
    function ($, _, Backbone, TuilsConfiguration, TuilsStorage, PublishProductView,
        MyAccountView,VendorServicesView ,QuestionsView ,VendorDetailView,
        ProductDetailView, NewsletterView, SearcherView, LeftFeaturedProductsView, HeaderView, OfficesView, MenuPanelView, MyProductsView,
        HomeView, SearchView, CategoryView, ManufacturerView, PublishView, FooterView, EditProductView, SelectPlanView, MyOrdersView, 
        ShowPlansView, StaticLoginView, SearchVendorsView) {

        var TuilsRouter = Backbone.Router.extend({
            currentView: undefined,

            //listado de vistas comunes
            viewHeader: undefined,
            viewLeftMenu: undefined,
            viewSearcher: undefined,
            viewNewsletter: undefined,
            viewLeftFeatured: undefined,
            viewFooter : undefined,

            //el por defecto para las vistas
            defaultEl: "#divMainSection",

            routes: {
                "": "home",
                "quiero-vender": "sell",
                "quiero-vender/producto(/:step)": "sellProduct",
                "quiero-vender/moto(/:step)": "sellBike",
                "quiero-vender/servicio-especializado(/:step)": "sellService",
                "mi-cuenta/datos-basicos": "myAccount",
                "mi-cuenta/sedes": "myOffices",
                "mi-tienda/motos-y-servicios": "vendorServices",
                "mi-cuenta/mis-compras(/:query)": "myOrders",
                "mi-cuenta/mis-ventas(/:query)": "myOrders",
                "mi-cuenta/mis-productos(/:query)": "myProducts",
                "mis-productos/seleccionar-plan(/:id)": "selectPlan",
                "mi-cuenta/preguntas-pendientes(/:query)": "questions",
                "ControlPanel/EditProduct(/:id)": "editProduct",
                "mis-deseos(/:customerGuid)": 'wishlist',
                "comparar": 'compare',
                "customer/changepassword" : "changePassword",
                //"v/:query": "vendor",
                "c/:categoryName(/*query)": "category",
                "m/:manufacturerName(/*query)": "manufacturer",
                "p/:query": "product",
                'entrar' : 'login',
                'buscar(/*query)': 'search',
                'recordar-clave': 'passwordRecovery',
                'mapa-del-sitio': 'sitemap',
                'quiero-vender/confirmacion/:id':'loadSubViews',
                'condiciones-de-uso': 'useConditions',
                'contacto': 'contactUs',
                'quienes-somos': 'aboutUs',
                'tarifas-y-precios': 'aboutUs',
                'passwordrecovery/confirm(/*query)' : 'passwordRecovery',
                'planes(/:tab)': 'plans',
                'buscar-negocios(/:stateProvId)(/:categoryId)(/:vendorId)(/:subTypeId)(/:lat)(/:lon)(/:zoon)': 'searchVendors',
                '*path' : 'defaultRoute'
            },
            defaultRoute: function (path) {
                //Lo usa para rutas no configuradas en el router, principalmente vendor
                //Valida si existe el campo del vendor
                if ($("#vendorPageBB").length)
                    this.vendor(path);
                else
                    throw "Ruta sin configurar: " + document.location.href;
            },
            home : function()
            {
                this.currentView = new HomeView();
                this.loadSubViews();
            },
            sell : function()
            {
                this.loadSubViews();
                this.currentView = new PublishView({ el : this.defaultEl });
            },
            sellProduct: function (step) {
                this.sellSwitch(TuilsConfiguration.productBaseTypes.product, step);
            },
            sellBike: function (step) {
                this.sellSwitch(TuilsConfiguration.productBaseTypes.bike, step);
            },
            sellService: function (step) {
                this.sellSwitch(TuilsConfiguration.productBaseTypes.service, step);
            },
            selectPlan: function (id) {
                this.currentView = new SelectPlanView({ el: this.defaultEl, id: parseInt(id) });
                this.loadSubViews();
            },
            sellSwitch: function (type, step) {
                if (!this.currentView) {
                    this.currentView = new PublishProductView({ el: this.defaultEl, productType: type });
                }
                else {
                    this.currentView.currentStep = step;
                    this.currentView.showStep();
                }

                this.loadSubViews();
            },
            myAccount: function () {
                this.currentView = new MyAccountView({ el: this.defaultEl });
                this.loadSubViewsPanel();
            },
            myOffices: function () {
                this.currentView = new OfficesView({ el: this.defaultEl });
                this.loadSubViewsPanel();
            },
            myOrders: function () {
                this.currentView = new MyOrdersView({ el: this.defaultEl });
                this.loadSubViewsPanel();
            },
            plans : function(tab){
                this.currentView = new ShowPlansView({ el : this.defaultEl, tab : tab });
                this.loadSubViews();
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
            editProduct: function (id) {
                this.currentView = new EditProductView({ el: this.defaultEl, productId: parseInt(id) });
                this.loadSubViewsPanel();
            },
            vendorServices: function () {
                this.currentView = new VendorServicesView({ el: this.defaultEl });
                this.loadSubViewsPanel();
            },
            vendor : function(query)
            {
                this.currentView = new VendorDetailView({ el: this.defaultEl });
                this.loadSubViews();
            },
            searchVendors: function (stateProvId, categoryId, vendorId, subTypeId, lat, lon, zoom) {
                this.currentView = new SearchVendorsView(
                    {
                        el: this.defaultEl,
                        stateProvinceId: stateProvId == "undefined" ? undefined : stateProvId,
                        categoryId: categoryId == "undefined" ? undefined : categoryId,
                        vendorId: vendorId == "undefined" ? undefined : vendorId,
                        subTypeId: subTypeId == "undefined" ? undefined : subTypeId,
                        lat: lat == "undefined" ? undefined : lat,
                        lon: lon == "undefined" ? undefined : lon,
                        zoom: zoom == "undefined" ? undefined : zoom
                    });
                this.loadSubViews();
            },
            sitemap: function () {
                this.loadSubViews();
            },
            category : function(categoryName, specification, query)
            {
                this.currentView = new CategoryView({ el: this.defaultEl });
                this.loadTwoColumns();
            },
            manufacturer: function () {
                this.currentView = new ManufacturerView({ el: this.defaultEl });
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
            passwordRecovery: function () {
                this.loadTwoColumns();
            },
            login: function () {
                this.currentView = new StaticLoginView({ el: this.defaultEl });
                this.loadTwoColumns();
            },
            product: function () {
                this.currentView = new ProductDetailView({ el: this.defaultEl });
                this.loadSubViews();
            },
            loadTwoColumns : function()
            {
                this.loadSubViews();
                this.loadLeftFeaturedProducts();
                this.loadNewsletter();
            },
            loadSubViews: function () {
                this.loadHeader();
                this.loadFooter();
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
                    //se atacha al evento de solicitud de registro
                    that.currentView.on('unauthorized-register', that.viewHeader.showRegister, that.viewHeader);
                    //evento para cerrar el menu responsive
                    that.currentView.on('close-menu-responsive', that.viewHeader.closeMenuResponsive, that.viewHeader);
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

            loadFooter: function () {
                this.viewFooter = new FooterView({ el: 'footer' });
            },
            loadSubViewsPanel: function () {
                var that = this;
                that.viewLeftMenu = new MenuPanelView({ el: "#divPanelMenu" });
            }
        });

        return TuilsRouter;
    });

