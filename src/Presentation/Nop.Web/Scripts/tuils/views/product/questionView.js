define(['jquery', 'underscore', 'baseView', 'tuils/models/question', 'configuration'],
    function ($, _, BaseView, QuestionModel, TuilsConfiguration) {

        var QuestionView = BaseView.extend({

            events: {
                'click input[name="add-question"]': 'save',
                'click #divLinkWriteQuestion' : 'showQuestionForm'
            },

            bindings: {
                "#NewQuestion_QuestionText": "QuestionText",
                "#recaptcha_response_field": "recaptcha_response_field"
            },

            initialize: function (args) {
                this.model = new QuestionModel();
                this.model.on("error", this.showErrorSave, this);
                this.model.on("sync", this.questionSaved, this);
                this.validateAuthorization();
                this.render();
            },
            showQuestionForm : function()
            {
                this.$("#divLinkWriteQuestion").hide();
                this.$("#question-form").show();
            },
            userAuthenticated : function()
            {
                this.save();
            },
            save: function () {
                var errors = this.validateControls();
                if (this.model.isValid()) {
                    if (TuilsConfiguration.captcha.showOnQuestions)
                        this.model.set("recaptcha_challenge_field", this.$("#recaptcha_challenge_field").val());
                    this.model.set("productId", this.$("#productId").val());
                    this.model.newQuestion();
                }
            },
            questionSaved: function () {
                var that = this;
                this.$("#question-form").fadeOut({
                    complete: function () {
                        that.$("#divQuestionPublished").show();
                    }
                });
            },
            showErrorSave: function (model, response) {
                //si hay error en el captcha muestra el mensaje
                if (response.responseJSON && response.responseJSON.ModelState && response.responseJSON.ModelState.captcha)
                {
                    alert(response.responseJSON.ModelState.message);
                    Recaptcha.reload();
                }
            },
            render: function () {
                this.stickThem();
                this.bindValidation();
                return this;
            }
        });

        return QuestionView;

    });