//Objeto que accede a algunos datos almacenados en el LocalStorage del navegador
define(['categoryCollection'], function (CategoryCollection) {
    var TuilsStorage = {

        bikeReferences: undefined,

        //Carga las referencias de las motocicletas en la propiedad 
        loadBikeReferences: function () {
            var key = 'tuils_bikeReferences';
            if (!localStorage.getItem(key)) {
                var categories = new CategoryCollection();
                categories.on("sync", function (response) {
                    localStorage.setItem(key, JSON.stringify(response.toJSON()))
                    TuilsStorage.bikeReferences = response.toJSON();
                }, this);

                categories.getBikeReferences();
            }
            else {
                this.bikeReferences = JSON.parse(localStorage.getItem(key));
            }
        },

    }

    return TuilsStorage;
});
