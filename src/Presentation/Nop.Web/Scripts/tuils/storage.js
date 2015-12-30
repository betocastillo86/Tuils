//Objeto que accede a algunos datos almacenados en el LocalStorage del navegador
define(['jquery', 'categoryCollection', 'configuration'], function ($, CategoryCollection, TuilsConfiguration) {
    var TuilsStorage = {

        bikeReferences: undefined,
        productCategories: undefined,
        serviceCategories: undefined,
        bikeReferencesSameLevel: undefined,
        //Variable en el storage que almacena un producto que se está creando para retomar una próxima vez
        
        keyPublishProduct: 'tuils-publishProduct',
        keyReloadBikeReferences: 'tuils-keyReloadReferences',
        keyReloadServices: 'tuils-keyReloadServices',
        keyReloadProducts: 'tuils-keyReloadProducts',
        keyReloadBikeReferencesSameLevel: 'tuils-keyReloadReferencesSameLevel',
        //Listado deCategorias de productos
        keyProductCategories: 'tuils-productCategories',
        keyServiceCategories: 'tuils-serviceCategories',
        keyBikeReferencesSameLevel: 'tuils-bikeReferencesSameLevel',

        //Carga las referencias de las motocicletas en la propiedad 
        loadBikeReferences: function (callback, ctx) {
            var key = 'tuils-bikeReferences';
            ctx = ctx ? ctx : this;
            if (TuilsStorage.hasToReloadReferences(TuilsStorage.keyReloadBikeReferences)) {
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

        loadProductCategories: function (callback, ctx) {
            var key = TuilsStorage.keyProductCategories;
            ctx = ctx ? ctx : this;
            //Usa la misma llave de bikereferences
            if (TuilsStorage.hasToReloadReferences(TuilsStorage.keyReloadProducts)) {
                var categories = new CategoryCollection();
                categories.on("sync", function (response) {
                    localStorage.setItem(key, JSON.stringify(response.toJSON()))
                    TuilsStorage.productCategories = response.toJSON();

                    if (callback)
                        callback.call(ctx, ctx);
                }, this);

                categories.getProductCategories();
            }
            else {
                TuilsStorage.productCategories = JSON.parse(localStorage.getItem(key));
                if (callback)
                    callback.call(ctx, ctx);
            }
        },
        loadServiceCategories: function (callback, ctx) {
            var key = TuilsStorage.keyServiceCategories;
            ctx = ctx ? ctx : this;
            //Usa la misma llave de bikereferences
            if (TuilsStorage.hasToReloadReferences(TuilsStorage.keyReloadServices)) {
                var categories = new CategoryCollection();
                categories.on("sync", function (response) {
                    localStorage.setItem(key, JSON.stringify(response.toJSON()))
                    TuilsStorage.serviceCategories = response.toJSON();

                    if (callback)
                        callback.call(ctx, ctx);
                }, this);

                categories.getServices();
            }
            else {
                TuilsStorage.serviceCategories = JSON.parse(localStorage.getItem(key));
                if (callback)
                    callback.call(ctx, ctx);
            }
        },
        loadBikeReferencesSameLevel: function (callback, ctx) {
            var key = TuilsStorage.keyBikeReferencesSameLevel;
            ctx = ctx ? ctx : this;
            //Usa la misma llave de bikereferences
            if (TuilsStorage.hasToReloadReferences(TuilsStorage.keyReloadBikeReferencesSameLevel)) {
                var categories = new CategoryCollection();
                categories.on("sync", function (response) {
                    localStorage.setItem(key, JSON.stringify(response.toJSON()))
                    TuilsStorage.bikeReferencesSameLevel = response.toJSON();

                    if (callback)
                        callback.call(ctx, ctx);
                }, this);

                categories.getBikeReferencesSameLevel();
            }
            else {
                TuilsStorage.bikeReferencesSameLevel = JSON.parse(localStorage.getItem(key));
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
        hasToReloadReferences: function (key) {
            var oldKey = localStorage.getItem(key);
            if (oldKey == TuilsConfiguration.expirationBikeReferencesKey && localStorage.getItem('tuils-bikeReferences'))
                return false;
            else
            {
                localStorage.setItem(key, TuilsConfiguration.expirationBikeReferencesKey);
                return true;
            }
        }
    }

    return TuilsStorage;
});
