define(['jquery', 'underscore', 'backbone', 'util', 'validations'], function ($, _, Backbone, TuilsUtil) {
    
    var BaseView = Backbone.View.extend({

        viewLogin: undefined,

        viewCreateUser : undefined,

        initialize: function()
        {
            debugger;
        },
        viewLogin : undefined,

        showLogin: function (model)
        {
            this.trigger('unauthorized');
        },
        validateAuthorization: function ()
        {
            this.model.on('unauthorized', this.showLogin, this);
        },
        userAuthenticated: function () {
            //Relanza el evento que el usuario fue autenticado, para que la vista que hereda lo pueda capturar
            this.trigger("user-authenticated");
        },
        stickThem: function () {
            this.stickit();
            //agrega las caracteristicas de tipos de datos a los combos
            this.$("input[tuils-val='int']").on("keypress", TuilsUtil.onlyNumbers);
        },
        validateControls: function (model) {
            //Formatea los mensajes de respuesta contra los label
            
            this.removeErrors();

            if (!model)
                model = this.model;

            var errors = model.validate();

            //Si notiene bindings no valida los campos
            if (this.bindings) {
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
                    if (domObj)
                    {
                        domObj.addClass("input-validation-error");
                        var domMessage = that.$("span[tuils-val-for='" + index + "']");
                        if (domMessage)
                            domMessage.text(errorField);
                    }

                    
                        
                });
            }

            return errors;
        },
        removeErrors: function () {
            this.$el.find(".input-validation-error").removeClass("input-validation-error");
            this.$el.find(".field-validation-error").text("").removeClass("input-validation-error");
        }
    });

    return BaseView;
});




