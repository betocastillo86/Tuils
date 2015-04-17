﻿define(['jquery', 'underscore', 'backbone', 'configuration', 'storage', 'publishProductView'],
    function ($, _, Backbone, TuilsConfiguration, TuilsStorage, PublishProductView) {

    var TuilsRouter = Backbone.Router.extend({
        currentView: undefined,

        //el por defecto para las vistas
        defaultEl: "#divMainSection",

        routes: {
            "quiero-vender/producto": "sellProduct",
            "quiero-vender/moto": "sellBike",
            "quiero-vender/servicio-especializado" : "sellService"
        },

        sellProduct: function () {
            this.currentView = new PublishProductView({ el: this.defaultEl, productType: TuilsConfiguration.productBaseTypes.product });
            //Se cargan las referencias de las motocicletas desde el comienzo
            TuilsStorage.loadBikeReferences();
        },
        sellBike: function () {
            this.currentView = new PublishProductView({ el: this.defaultEl, productType: TuilsConfiguration.productBaseTypes.bike });
        },
        sellService : function(){
            this.currentView = new PublishProductView({ el: this.defaultEl, productType: TuilsConfiguration.productBaseTypes.service });
            //Se cargan las referencias de las motocicletas desde el comienzo
            TuilsStorage.loadBikeReferences();
        },
        fastLogin: function () {
            debugger;
        }
    });

    return TuilsRouter;
});

