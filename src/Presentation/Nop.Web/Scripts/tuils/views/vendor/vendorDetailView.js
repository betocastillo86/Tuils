define(['underscore', 'baseView', 'tuils/views/vendor/vendorOfficesView', 'tuils/views/vendor/vendorReviewsView'],
    function (_, BaseView, VendorOfficesView, VendorReviewsView) {

        var VendorDetailView = BaseView.extend({
            events: {
                'keyup .search-pdt .searchinput': 'search'
            },
            id: 0,
            allowEdit : false,
            viewHeader: undefined,
            viewOffices: undefined,
            viewReviews: undefined,
            initialize: function (args)
            {
                this.id = parseInt(this.$("#VendorId").val());
                this.allowEdit = this.$("#AllowEdit").val() == 'True';
                this.loadControls();
            },
            loadControls : function()
            {
                this.loadHeader();
                this.loadOffices();
                this.loadReviews();
            },
            loadHeader: function () {
                if (this.allowEdit)
                {
                    var that = this;
                    require(['tuils/views/vendor/vendorHeaderView'], function (VendorHeaderView) {
                        that.viewHeader = new VendorHeaderView({ el: "#divHeaderVendor", id: that.id, allowEdit: that.allowEdit });
                    });
                }
            },
            search: function (obj) {
                if (obj.keyCode != 13) {
                    var value = $(obj.target).val();
                    this.$('.btn_search').attr("href", "?q=" + value);
                }
                else {
                    document.location.href =  this.$('.search-pdt a').attr("href");
                }
            },
            loadOffices: function () {
                this.view = new VendorOfficesView({ el : "#divOffices", id : this.id });
            },
            loadReviews: function () {
                this.viewReviews = new VendorReviewsView({ el: '.conte-comment', id: this.id });
            }
        });

        return VendorDetailView;
});

