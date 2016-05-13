define(['jquery', 'underscore', 'util', 'baseView', 'handlebars', 'tuils/collections/reviews', 'configuration','handlebarsh'],
    function ($, _, TuilsUtilities, BaseView, Handlebars, ReviewsCollection, TuilsConfiguration) {

        var VendorReviewsView = BaseView.extend({

            events: {
                'click #btnMore' : 'more',
                'click .vote' : 'voteReview'
            },

            id :  0,

            currentPage: 0,

            //template: Handlebars.compile($("#templateReview").html()),

            initialize: function (args) {
                this.template= Handlebars.compile($("#templateReview").html());
                this.id = args.id;
                this.collection = new ReviewsCollection();
                this.collection.on("sync", this.showReviews, this);
                this.loadReviews();
            },
            loadReviews: function ()
            {
                this.collection.getReviewsByVendor(this.id, this.currentPage);
            },
            showReviews: function () {
                var reviews = this.collection.toJSON();
                this.$('#divReviews').append(this.template(reviews));
                if (TuilsConfiguration.vendor.reviewsPageSize > reviews.length)
                    this.$("#btnMore").hide();
                if (this.currentPage == 0 && reviews.length == 0)
                    this.$("#divNoReviews").show();
            },
            voteReview: function (obj) {
                var that = this;
                obj = $(obj.currentTarget);
                var reviewId = obj.attr('data-id');
                var wasHelpful = obj.attr('data-id').hasClass('vote-yes');

                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "/setvendorreviewhelpfulness",
                    data: { "vendorReviewId": reviewId, "washelpful": wasHelpful },
                success: function (data) {
                    that.$("#helpfulness-vote-yes-" + reviewId).html(data.TotalYes);
                    that.$("#helpfulness-vote-no-" + reviewId).html(data.TotalNo);
                    that.$("#helpfulness-vote-result-" + reviewId).html(data.Result);
                                                  
                    that.$('#helpfulness-vote-result-" + reviewI').fadeIn("slow").delay(1000).fadeOut("slow");
                },
                error:function (xhr, ajaxOptions, thrownError){
                    this.alert('Failed to vote. Please refresh the page and try one more time.');
                }  
            });
                

            },
            more: function () {
                this.currentPage++;
                this.loadReviews();
            }
        });

        return VendorReviewsView;
    });