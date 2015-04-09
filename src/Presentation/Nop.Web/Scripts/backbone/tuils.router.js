var TuilsRouter = Backbone.Router.extend({
    currentView: undefined,

    //el por defecto para las vistas
    defaultEl: "#divMainSection",

    routes: {
        "quiero-vender/producto" : "sellProduct"
    },

    sellProduct: function ()
    {
        this.currentView = new PublishProductView({ el: this.defaultEl, productType: TuilsApp.configuration.productBaseTypes.product });
        //Se cargan las referencias de las motocicletas desde el comienzo
        TuilsStorage.loadBikeReferences();
    },
    fastLogin: function ()
    {
        debugger;
    }
});