define(['jquery', 'underscore', 'util', 'baseView', 'handlebars', 'tuils/collections/reviews', 'configuration','handlebarsh'],
    function ($, _, TuilsUtilities, BaseView, Handlebars, ReviewsCollection, TuilsConfiguration) {

        var VendorReviewsView = BaseView.extend({

            events: {
                'click #btnMore' : 'more'
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
            more: function () {
                this.currentPage++;
                this.loadReviews();
            }
        });

        return VendorReviewsView;
    });