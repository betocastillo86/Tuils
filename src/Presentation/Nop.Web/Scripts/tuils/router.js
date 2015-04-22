define(['jquery', 'underscore', 'backbone', 'configuration', 'storage', 'publishProductView'],
    function ($, _, Backbone, TuilsConfiguration, TuilsStorage, PublishProductView) {

    var TuilsRouter = Backbone.Router.extend({
        currentView: undefined,

        //listado de vistas comunes
        viewHeader : undefined,

        //el por defecto para las vistas
        defaultEl: "#divMainSection",

        routes: {
            "quiero-vender/producto": "sellProduct",
            "quiero-vender/moto": "sellBike",
            "quiero-vender/servicio-especializado" : "sellService"
        },

        sellProduct: function () {
            this.loadSubViews();
            this.currentView = new PublishProductView({ el: this.defaultEl, productType: TuilsConfiguration.productBaseTypes.product });
            //Se cargan las referencias de las motocicletas desde el comienzo
            TuilsStorage.loadBikeReferences();
        },
        sellBike: function () {
            this.loadSubViews();
            this.currentView = new PublishProductView({ el: this.defaultEl, productType: TuilsConfiguration.productBaseTypes.bike });
        },
        sellService: function () {
            this.loadSubViews();
            this.currentView = new PublishProductView({ el: this.defaultEl, productType: TuilsConfiguration.productBaseTypes.service });
            //Se cargan las referencias de las motocicletas desde el comienzo
            TuilsStorage.loadBikeReferences();
        },
        fastLogin: function () {
            debugger;
        },

        loadSubViews: function () {
            var that = this;
            require(['tuils/views/common/header'], function (HeaderView) {
                that.viewHeader = new HeaderView();
                //se atacha al evento de solicitud de ingreso
                that.currentView.on('unauthorized', that.viewHeader.showLogin, that.viewHeader);
                //atacha a la vista actual al evento cuando el usuario se autenticó
                that.viewHeader.on('user-authenticated', that.currentView.userAuthenticated, that.currentView);
            });
        }
    });

    return TuilsRouter;
});

