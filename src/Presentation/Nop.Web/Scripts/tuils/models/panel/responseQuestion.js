define(['underscore', 'backbone'], function (_, Backbone) {
    var ResponseQuestionModel = Backbone.Model.extend({

        idAttribute: "Id",

        baseUrl: "/api/questions",

        url: "/api/questions",

        validation: {
            AnswerText: {
                required: true
            }
        },
        labels: {
            AnswerText: 'Respuesta'
        },
        answer: function () {
            this.url = this.baseUrl;
            this.save();
        }
    });

    return ResponseQuestionModel;
});