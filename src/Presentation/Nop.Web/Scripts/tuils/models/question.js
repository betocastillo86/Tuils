define(['underscore', 'backbone', '_authenticationModel'], function (_, Backbone, AuthenticationModel) {
    var QuestionModel = AuthenticationModel.extend({

        idAttribute: "Id",

        baseUrl: '/api/questions',

        url: '/api/questions',

        validation: {

            QuestionText: {
                required: true,
                maxLength: 4000
            }
        },
        labels: {
            QuestionText: 'Pregunta',
        },
        newQuestion: function ()
        {
            var that = this;
            this.url = this.baseUrl;
            this.save({}, {
                beforeSend: function (xhr)
                {
                    xhr.setRequestHeader('recaptcha_response_field', that.get('recaptcha_response_field'));
                    xhr.setRequestHeader('recaptcha_challenge_field', that.get('recaptcha_challenge_field'));
                }
            });
        }
    });

    return QuestionModel;
});