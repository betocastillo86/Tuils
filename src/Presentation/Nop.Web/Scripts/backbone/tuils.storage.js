//Objeto que accede a algunos datos almacenados en el LocalStorage del navegador
TuilsStorage = {
    
    bikeReferences: undefined,

    //Carga las referencias de las motocicletas en la propiedad 
    loadBikeReferences: function ()
    {
        var key = 'tuils_bikeReferences';
        if (!localStorage.getItem(key))
        {
            var categories = new CategoryCollection();
            categories.on("sync", function (response) { localStorage.setItem(key, JSON.stringify(response.toJSON())) }, this);
            categories.getBikeReferences();
        }
        this.bikeReferences = JSON.parse(localStorage.getItem(key));
        return this.bikeReferences;
    }
}