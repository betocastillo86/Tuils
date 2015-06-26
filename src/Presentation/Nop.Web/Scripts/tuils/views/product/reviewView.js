define(['jquery', 'underscore', 'baseView', 'tuils/models/review'],
    function ($, _, BaseView, ReviewModel) {

        var ReviewView = BaseView.extend({

            events: {
                'click input[name="add-review"]': 'save'
            },

            bindings: {
                "#AddProductReview_Title": "Title",
                "#AddProductReview_ReviewText": "ReviewText",
                "input[name='AddProductReview.Rating']": {
                    observe: "Rating",
                    onSet: function (value, ctx) {
                        ctx.view.model.set('Rating', parseInt(value));
                        return value;
                    }
                }
            },

            initialize: function (args) {
                this.model = new ReviewModel();
                this.render();
            },

            save: function () {
                var errors = this.validateControls();
                if (!errors)
                    this.$("form").submit();
            },

            render: function () {
                this.stickThem();
                this.bindValidation();
                return this;
            }
        });

        return ReviewView;

    });