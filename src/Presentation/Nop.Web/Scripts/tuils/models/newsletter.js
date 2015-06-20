define(['underscore', 'backbone'], function (_, Backbone) {
    var NewsletterModel = Backbone.Model.extend({

        idAttribute: "Id",

        url: "/subscribenewsletter",

        validation: {
            Email: {
                required: true,
                pattern: 'email'
            }
        },
        labels: {
            Email: 'Correo'
        },
        suscribe: function () {
            return this.save();
        }
    });

    return NewsletterModel;
});