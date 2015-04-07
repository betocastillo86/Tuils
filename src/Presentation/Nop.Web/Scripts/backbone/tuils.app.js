var TuilsApp = {
	router : undefined,

	init: function ()
	{
	    TuilsApp.router = new TuilsRouter();
		Backbone.history.start({ pushState: true });
	},
    //Carga un dropdownlist con los datos pasados en una lista
    //ddl: Objeto tipo select a cargar
    //list: DataSource a cargar
    //args: argumentos adicionales para cargar la lista
    //      text = Propiedad de la cual debe mostrar el texto en el ddl (Por defecto es Name)
    //      value = Propiedad de la cual debe mostrar el texto en el ddl (Por defecto es Id)
    //      defaultOptionText = Propiedad del texto por defecto (Por defecto -)
    //      defaultOptionValue = Propiedad del valor por defecto  (Por defecto 0)
	loadDropDown: function (ddl, list, args)
	{
	    args = args != undefined ? args : {};
	    var text = args.text ? args.text : "Name";
	    var value = args.value ? args.value : "Id";

	    ddl.empty();
	    ddl.append("<option value='" + (args.defaultOptionValue ? args.defaultOptionValue : "0") + "'>" + (args.defaultOptionText ? args.defaultOptionText : "-") + "</option>")

	    _.each(list, function (element, index, list) {
	        ddl.append("<option value='" + element[value] + "'>" + element[text] + "</option>")
	    });
	},
    //Función que extiende la funcionalidad de Tag-it y sirve para filtrar los contenidos desde un array label, value
    //En este caso se filtran todos los que contengan la palabra ingresada
	tagItAutocomplete: function (search, showChoices)
	{
	    var filter = search.term.toLowerCase();
	    var choices = $.grep(this.options.availableTags, function (element) {
	        return (element.label.toLowerCase().indexOf(filter) > -1);
	    });
	    if (!this.options.allowDuplicates) {
	        choices = this._subtractArray(choices, this.assignedTags());
	    }
	    showChoices(choices);
	},
	addMinHeader: function (xhr)
	{
	    xhr.setRequestHeader('min', true);
	},
	onlyNumbers: function (evt)
	{
	    var charCode = (evt.which) ? evt.which : event.keyCode
	    if (charCode > 31 && (charCode < 48 || charCode > 57))
	        return false;
	    return true;
	},
	toStringWithSeparator: function (array, separator) {
	    var toString;
	    _.each(array, function (element, index) {
	        toString = toString ? toString + separator + element : element;
	    });
	    return toString ? toString : "";
	}

}

$(document).on("ready", TuilsApp.init);


//metodos de extensión de backbone
_.extend(Backbone.View.prototype, {
    stickThem : function()
    {
        this.stickit();
        //agrega las caracteristicas de tipos de datos a los combos
        this.$("input[tuils-val='int']").on("keypress", TuilsApp.onlyNumbers);
    },
    validateControls: function (model) {
        this.removeErrors();

        if (!model)
            model = this.model;

        var errors = model.validate();

        //Si notiene bindings no valida los campos
        if (this.bindings)
        {
            var that = this;
            
            //invierte los bindings para obtener todos los campos y objetos del formulario
            var fieldsToMark = new Object();
            _.each(that.bindings, function (element, index) {
                //Si es un objeto busca la propiedad en el campo observe
                if (_.isObject(element)) {
                    fieldsToMark[element['observe']] = element['controlToMark'] ? element['controlToMark'] : index;
                }
                else {
                    fieldsToMark[element] = index;
                }
            });

            _.each(errors, function (errorField, index) {
                //recorre los errores y marca solo los que tienen objeto DOM
                var domObj = that.$(fieldsToMark[index]);
                if(domObj)
                    domObj.addClass("invalid-field");
            });
        }
    },
    removeErrors: function ()
    {
        this.$el.find(".invalid-field").removeClass("invalid-field");
    }
});

//metodos de extension de underscore
//_.extend(_.prototype, {
//    toStringWithSeparator: function (array, separator)
//    {
//        var toString;
//        this.each(arry, function (element, index) {
//            toString = toString ? separator + toString : toString;
//        });
//        return toString ? toString : "";
//    }
//});