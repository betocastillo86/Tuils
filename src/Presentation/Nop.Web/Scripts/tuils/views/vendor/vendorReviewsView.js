define(['underscore', 'util', 'baseView', 'handlebars', 'tuils/collections/reviews', 'handlebarsh'],
    function (_, TuilsUtilities, BaseView, Handlebars, ReviewsCollection) {

        var VendorReviewsView = BaseView.extend({

            events: {
                'click #btnMore' : 'more'
            },

            id :  0,

            currentPage: 0,

            template: Handlebars.compile($("#templateReview").html()),

            initialize: function (args) {
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
                this.$('#divReviews').append(this.template(this.collection.toJSON()));
            },
            more: function () {
                this.currentPage++;
                this.loadReviews();
            }
        });

        return VendorReviewsView;
    });