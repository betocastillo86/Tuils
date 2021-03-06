﻿define(['jquery', 'underscore', 'configuration'],
    function ($, _, TuilsConfiguration) {
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
        tagitAvailableTags: function (list, labelField, valueField) {
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
            var charCode = (evt.which) ? evt.which : evt.keyCode;
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
        },
        isValidSize: function (obj) {
            if (obj.files[0].size > TuilsConfiguration.maxFileUploadSize) {
                obj.files[0] = null;
                obj.value = "";
                return false;
            }
            else
                return true;
        },
        isValidExtension: function (obj, typeFile) {
            obj = $(obj);

            var validExtensions;
            if (typeFile == 'image')
                validExtensions = /(jpg|JPG|jpeg|JPEG|gif|GIF|png|PNG)/

            if (!validExtensions.test(obj.val())) {
                obj.val("");
                return false;
            }
            else {
                return true;
            }
        },
        
        //Convierte los ticks de .NET a ticks en JS
        //ticks are recorded from 1/1/1; get microtime difference from 1/1/1/ to 1/1/1970
        ticksToJs: function (ticks) {
            ticks = ticks / 1000;
            return ticks - 2208988800000;
        },
        //tomado de http://stackoverflow.com/questions/5999118/add-or-update-query-string-parameter
        updateQueryStringParameter: function (uri, key, value) {
            var re = new RegExp("([?&])" + key + "=.*?(&|$)", "i");
            var separator = uri.indexOf('?') !== -1 ? "&" : "?";
            if (uri.match(re)) {
                return uri.replace(re, '$1' + key + "=" + value + '$2');
            }
            else {
                return uri + separator + key + "=" + value;
            }
        },
        //Retorna el valor del querystring
        //http://stackoverflow.com/questions/901115/how-can-i-get-query-string-values-in-javascript
        getParameterByName: function (name, url) {
            if (!url) url = window.location.href;
            name = name.replace(/[\[\]]/g, "\\$&");
            var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, " "));
        },

    };

    return TuilsUtilities;
});