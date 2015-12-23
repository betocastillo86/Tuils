define(['jquery', 'underscore', 'baseView', 'tuils/models/review'],
    function ($, _, BaseView, ReviewModel) {

        var VendorReviewView = BaseView.extend({

            vendorId : undefined,

            events: {
                'click input[name="add-review"]': 'save',
                'click .vote': 'voteReview',
                'click .no-result .button-1' :'showReviewForm'
            },

            bindings: {
                "#AddVendorReview_Title": "Title",
                "#AddVendorReview_ReviewText": "ReviewText",
                "input[name='AddVendorReview.Rating']": {
                    observe: "Rating",
                    onSet: function (value, ctx) {
                        ctx.view.model.set('Rating', parseInt(value));
                        return value;
                    }
                }
            },

            initialize: function (args) {
                this.model = new ReviewModel();
                this.model.on('sync', this.reviewSaved, this);
                this.model.on('error', this.showErrorSave, this);
                this.model.set('VendorId', args.vendorId);
                this.on("user-authenticated", this.save, this);
                this.render();
            },
            save: function () {
                var errors = this.validateControls();
                if (this.model.isValid())
                {
                    this.validateAuthorization();
                    this.model.newVendorReview();
                }
                    
            },
            reviewSaved: function () {
                var that = this;
                this.$(".write-review").fadeOut({
                    complete: function () {
                        that.$("#divReviewPublished").show();
                        that.$(".no-result").hide();
                    }
                });
            },
            showErrorSave: function (model, response) {
                this.handleAlertError(response);
            },
            voteReview: function (obj) {
                var that = this;
                obj = $(obj.currentTarget);
                var reviewId = obj.attr('data-id');
                var wasHelpful = obj.hasClass('vote-yes');

                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "/setvendorreviewhelpfulness",
                    data: { "vendorReviewId": reviewId, "washelpful": wasHelpful },
                    success: function (data) {
                        that.$("#helpfulness-vote-yes-" + reviewId).html(data.TotalYes);
                        that.$("#helpfulness-vote-no-" + reviewId).html(data.TotalNo);
                        that.$("#helpfulness-vote-result-" + reviewId).html(data.Result);
                                                  
                        that.$('#helpfulness-vote-result-' + reviewId).fadeIn("slow").delay(1000).fadeOut("slow");
                    },
                    error:function (xhr, ajaxOptions, thrownError){
                        alert('Failed to vote. Please refresh the page and try one more time.');
                    }  
                });
            },
            showReviewForm: function () {
                this.$('#review-form').show();
            },
            render: function () {
                this.stickThem();
                this.bindValidation();
                return this;
            }
        });

        return VendorReviewView;

    });