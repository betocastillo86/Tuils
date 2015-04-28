define(['underscore', 'baseView', 'tuils/views/vendor/vendorHeaderView'],
    function (_, BaseView, VendorHeaderView) {

        var VendorDetailView = BaseView.extend({
            id: 0,
            allowEdit : false,
            viewHeader : undefined,
            initialize: function (args)
            {
                this.id = parseInt(this.$("#VendorId").val());
                this.allowEdit = this.$("#AllowEdit").val() == 'true';
                this.loadControls();
            },
            loadControls : function()
            {
                this.loadHeader({ id: this.id, allowEdit: this.allowEdit });
            },
            loadHeader: function () {
                this.viewHeader = new VendorHeaderView({ el: "#divHeaderVendor" });
            }
        });

        return VendorDetailView;
});

