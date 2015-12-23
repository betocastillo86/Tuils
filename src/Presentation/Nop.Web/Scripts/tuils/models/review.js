define(['underscore', 'backbone', '_authenticationModel'], function (_, Backbone, AuthenticationModel) {
    var ReviewModel = AuthenticationModel.extend({

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
        },
        newVendorReview: function () {
            this.url = '/api/vendors/' + this.get('VendorId') + '/reviews';
            this.save();
        },
    });

    return ReviewModel;
});