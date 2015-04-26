define(['jquery', 'underscore'], function ($, _) {
    var TuilsUtilities = {
        //Carga un dropdownlist con los datos pasados en una lista
        //ddl: Objeto tipo select a cargar
        //list: DataSource a cargar
        //args: argumentos adicionales para cargar la lista
        //      text = Propiedad de la cual debe mostrar el texto en el ddl (Por defecto es Name)
        //      value = Propiedad de la cual debe mostrar el texto en el ddl (Por defecto es Id)
        //      defaultOptionText = Propiedad del texto por defecto (Por defecto -)
        //      defaultOptionValue = Propiedad del valor por defecto  (Por defecto 0)
        loadDropDown: function (ddl, list, args) {
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
        tagItAutocomplete: function (search, showChoices) {
            var filter = search.term.toLowerCase();
            var choices = $.grep(this.options.availableTags, function (element) {
                return (element.label.toLowerCase().indexOf(filter) > -1);
            });
            if (!this.options.allowDuplicates) {
                choices = this._subtractArray(choices, this.assignedTags());
            }
            showChoices(choices);
        },
        //Convierte una coleccion a el formato de los tags disponibles
        //se le envian las propiedades que son el label y el valor, por defecto toma name y id
        tagitAvailableTags : function(list, labelField, valueField){
            if (!labelField) labelField = 'Name';
            if (!valueField) valueField = 'Id';
            var tagReferences = [];
            _.each(list, function (element, index) {
                tagReferences.push({ label: element[labelField], value: element[valueField] });
            });
            return tagReferences;
        },
        addMinHeader: function (xhr) {
            xhr.setRequestHeader('min', true);
        },
        onlyNumbers: function (evt) {
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
        },
        navigateFastLogin: function (event, xhr) {
            if (xhr.status === 401)
                this.router.navigate("#login");
        },
        pluckPropertiesJquery: function (objsArray, property) {
            var values = new Array();
            _.each(objsArray, function (element, index) {
                values.push($(element).attr(property));
            });
            return values;
        }
    };

    return TuilsUtilities;
});