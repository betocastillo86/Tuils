﻿define(['underscore', 'backbone', 'router', 'validations'], function (_, Backbone, TuilsRouter) {
    var TuilsApp = {
        router: undefined,

        init: function () {
            TuilsApp.router = new TuilsRouter();
            Backbone.history.start({ pushState: true });
            Backbone.Validation.configure({ labelFormatter: 'label' });

            $(document).ajaxError(this.navigateFastLogin);
        }
    }

    _.extend(Backbone.Validation.messages, {
        required: 'Campo {0} es obligatorio',
        acceptance: '{0} must be accepted',
        min: 'Campo {0} debe ser mayor o igual a {1}',
        max: 'Campo {0} debe ser menor o igual a {1}',
        range: '{0} de ser entre {1} y {2}',
        length: '{0} debe tener {1} caracteres',
        minLength: '{0} debe tener por lo menos {1} caracteres',
        maxLength: '{0} debe tener máximo {1} caracteres',
        rangeLength: '{0} debe estar entre {1} y {2} caracteres',
        oneOf: '{0} debe ser uno de: {1}',
        equalTo: '{0} must be the same as {1}',
        digits: '{0} solo puede tener números',
        number: '{0} debe ser un número',
        email: '{0} debe ser un correo valido',
        url: '{0} debe ser una url',
        inlinePattern: '{0} no es valido'
    });
    
    return TuilsApp;
});


