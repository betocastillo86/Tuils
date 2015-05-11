//Objeto que accede a algunos datos almacenados en el LocalStorage del navegador
define(['categoryCollection'], function (CategoryCollection) {
    var TuilsStorage = {

        bikeReferences: undefined,

        //Carga las referencias de las motocicletas en la propiedad 
        loadBikeReferences: function (callback, ctx) {
            var key = 'tuils-bikeReferences';
            if (!localStorage.getItem(key)) {
                var categories = new CategoryCollection();
                categories.on("sync", function (response) {
                    localStorage.setItem(key, JSON.stringify(response.toJSON()))
                    TuilsStorage.bikeReferences = response.toJSON();
                    if(callback)
                        callback();
                }, this);

                categories.getBikeReferences();
            }
            else {
                TuilsStorage.bikeReferences = JSON.parse(localStorage.getItem(key));
                if (callback)
                    callback();
            }
        },

    }

    return TuilsStorage;
});
