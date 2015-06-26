define(['jquery', 'underscore', 'baseView', 'tuils/views/panel/responseQuestionView'],
    function ($,_, BaseView, ResponseQuestionView) {

        var QuestionsView = BaseView.extend({
            events: {
                
            },
            viewsQuestions : undefined,
            initialize: function (args) {
                
                this.loadQuestions();
            },
            loadQuestions: function () {
                this.viewsQuestions = new Array();
                var that = this;
                _.each(this.$(".divQuestion"), function (element, index) {
                    var dom = $(element);
                    that.viewsQuestions.push(new ResponseQuestionView({ el: "#" + dom.attr("id"), id: dom.attr("data-id") }));
                });
            }         
        });

        return QuestionsView;
    });