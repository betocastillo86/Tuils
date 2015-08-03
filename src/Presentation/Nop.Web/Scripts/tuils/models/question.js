define(['underscore', 'backbone', '_authenticationModel', 'resources'],
    function (_, Backbone, AuthenticationModel, TuilsResources) {
    var QuestionModel = AuthenticationModel.extend({

        idAttribute: "Id",

        baseUrl: '/api/questions',

        url: '/api/questions',

        validation: {

            QuestionText: {
                required: true,
                maxLength: 4000,
                minLength: 20
            }
        },
        labels: {
            QuestionText: 'Pregunta',
        },
        newQuestion: function ()
        {
            var that = this;
            this.set('message_login', TuilsResources.loginMessages.askQuestion);
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