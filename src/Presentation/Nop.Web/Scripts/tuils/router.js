﻿define(['jquery', 'underscore', 'backbone', 'configuration', 'storage'],
    function ($, _, Backbone, TuilsConfiguration, TuilsStorage, PublishProductView) {

        var TuilsRouter = Backbone.Router.extend({
            currentView: undefined,

            //listado de vistas comunes
            viewHeader: undefined,

            //el por defecto para las vistas
            defaultEl: "#divMainSection",

            routes: {
                "quiero-vender/producto": "sellProduct",
                "quiero-vender/moto": "sellBike",
                "quiero-vender/servicio-especializado": "sellService",
                "datos-basicos": "myAccount"
            },

            sellProduct: function () {
                var that = this;
                require(['publishProductView'], function (PublishProductView) {
                    that.loadSubViews();
                    that.currentView = new PublishProductView({ el: that.defaultEl, productType: TuilsConfiguration.productBaseTypes.product });
                    //Se cargan las referencias de las motocicletas desde el comienzo
                    TuilsStorage.loadBikeReferences();
                });
            },
            sellBike: function () {
                var that = this;
                require(['publishProductView'], function (PublishProductView) {
                    that.loadSubViews();
                    that.currentView = new PublishProductView({ el: that.defaultEl, productType: TuilsConfiguration.productBaseTypes.bike });
                });
            },
            sellService: function () {
                var that = this;
                require(['publishProductView'], function (PublishProductView) {
                    that.loadSubViews();
                    that.currentView = new PublishProductView({ el: that.defaultEl, productType: TuilsConfiguration.productBaseTypes.service });
                    //Se cargan las referencias de las motocicletas desde el comienzo
                    TuilsStorage.loadBikeReferences();
                });
            },
            myAccount: function () {
                var that = this;
                require(['tuils/views/panel/myAccount'], function (MyAccountView) {
                    that.currentView = new MyAccountView({ el: that.defaultEl });
                });
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

