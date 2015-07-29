define(['underscore', 'backbone'], function (_, Backbone) {
    var ReviewModel = Backbone.Model.extend({

        idAttribute: "Id",

        validation: {
            Title: {
                required: true,
                maxLength:100
            },
            ReviewText: {
                required: true,
                maxLength:4000
            },
            Rating: {
                required: true,
                pattern: 'number'
            }
        },
        labels: {
            Title: 'Titulo',
            ReviewText: 'Comentarios',
            Rating: 'Calificación',
        }
    });

    return ReviewModel;
});