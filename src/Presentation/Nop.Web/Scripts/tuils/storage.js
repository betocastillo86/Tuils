//Objeto que accede a algunos datos almacenados en el LocalStorage del navegador
define(['jquery', 'categoryCollection', 'configuration'], function ($, CategoryCollection, TuilsConfiguration) {
    var TuilsStorage = {

        bikeReferences: undefined,
        //Variable en el storage que almacena un producto que se está creando para retomar una próxima vez
        
        keyPublishProduct: 'tuils-publishProduct',
        keyReloadBikeReferences: 'tuils-keyReloadReferences',

        //Carga las referencias de las motocicletas en la propiedad 
        loadBikeReferences: function (callback, ctx) {
            var key = 'tuils-bikeReferences';
            ctx = ctx ? ctx : this;
            if (TuilsStorage.hasToReloadReferences()) {
                var categories = new CategoryCollection();
                categories.on("sync", function (response) {
                    localStorage.setItem(key, JSON.stringify(response.toJSON()))
                    TuilsStorage.bikeReferences = response.toJSON();

                    if(callback)
                        callback.call(ctx, ctx);
                }, this);

                categories.getBikeReferences();
            }
            else {
                TuilsStorage.bikeReferences = JSON.parse(localStorage.getItem(key));
                if (callback)
                    callback.call(ctx, ctx);
            }
        },

        setPublishProduct: function (product) {
            if (product)
                localStorage.setItem(TuilsStorage.keyPublishProduct, JSON.stringify(product.toJSON()));
            else
                localStorage.removeItem(TuilsStorage.keyPublishProduct);
        },
        getPublishProduct: function () {
            if (localStorage.getItem(TuilsStorage.keyPublishProduct))
                return JSON.parse(localStorage.getItem(TuilsStorage.keyPublishProduct));
        },
        //Valida si la llave de recarga corresponde con la nueva
        hasToReloadReferences: function () {
            var oldKey = localStorage.getItem(TuilsStorage.keyReloadBikeReferences);
            if (oldKey == TuilsConfiguration.expirationBikeReferencesKey && localStorage.getItem('tuils-bikeReferences'))
                return false;
            else
            {
                localStorage.setItem(TuilsStorage.keyReloadBikeReferences, TuilsConfiguration.expirationBikeReferencesKey);
                return true;
            }
        }
    }

    return TuilsStorage;
});
