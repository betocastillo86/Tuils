define(['jquery', 'underscore', 'baseView', 'tuils/models/panel/responseQuestion'],
    function ($, _, BaseView, ResponseQuestionModel) {

        var ResponseQuestionsView = BaseView.extend({

            //id: 0,
            
            events: {
                'click a' : 'answer'
            },

            bindings: {
                'textarea': 'AnswerText'
            },
            initialize: function (args) {
                //this.id = args.id;
                this.model = new ResponseQuestionModel();
                this.model.set("Id", args.id);
                this.model.on('sync', this.saved, this);
                this.render();
            },
            render: function () {
                this.stickThem();
                this.bindValidation();
            },
            answer: function () {
                this.validateControls();
                if (this.model.isValid() && confirm("¿Está seguro de que esta es la respuesta final?"))
                {
                    this.model.answer();
                }
            },
            saved: function () {
                var that = this;
                this.$el.fadeOut({
                    complete: function ()
                    {
                        that.undelegateEvents();
                        that.remove();
                    }
                });
                this.alert('La pregunta ha sido respondida correctamente');
                
            }

        });

        return ResponseQuestionsView;
    });