define(['underscore', 'util', 'baseView', 'tuils/models/vendor', 'handlebars', 'tuils/views/panel/addAddressView', 'handlebarsh'],
    function (_, TuilsUtilities, BaseView, VendorModel, HandleBars, AddressView) {
    var OfficesView = BaseView.extend({

        events: {
            'click #btnEditAddress': 'loadAddress',
            'click #btnDeleteAddress': 'removeAddress',
            'click #btnNew': 'loadAddress'
        },

        vendorId: 0,

        template: HandleBars.compile($("#templateListOffices").html()),

        viewAddAddress: undefined,

        accordion : undefined,

        initialize: function (args)
        {
            this.vendorId = parseInt(this.$("#VendorId").val());
            this.loadControls();
        },
        loadControls : function()
        {
            this.loadOffices();
            this.viewAddAddress = new AddressView({ el: "#divOfficeDetail", VendorId: this.vendorId });
            this.viewAddAddress.on("saved", this.loadOffices, this);
            this.viewAddAddress.on("back", this.showStep, this);
        },
        loadOffices : function()
        {
            var vendor = new VendorModel();
            vendor.on("sync", this.showOffices, this);
            vendor.getOffices(this.vendorId);
            this.showStep(0);
        },
        loadAddress : function(obj)
        {
            var id = parseInt($(obj.target).attr('tuils-id'));
            if (!isNaN(id) && id > 0)
                this.viewAddAddress.loadAddress(id);
            else
                this.viewAddAddress.newAddress();
            this.showStep(1);
        },
        removeAddress : function(obj)
        {
            var id = parseInt($(obj.target).attr('tuils-id'));
            this.viewAddAddress.deleteById(id);
        },
        showStep : function(step){
            this.$("#officesAccordion").accordion({ active: step });
        },
        showOffices : function(offices)
        {
            this.$("#divListOffices").html(this.template(offices.toJSON()));
        },
        render: function ()
        {
            return this;
        }
    });

    return OfficesView;
});