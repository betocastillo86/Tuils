﻿define(['underscore', 'util', 'baseView', 'tuils/collections/addresses', 'handlebars', 'tuils/views/panel/addAddressView', 'imageSelectorView','handlebarsh'],
    function (_, TuilsUtilities, BaseView, AddressCollection, HandleBars, AddressView, ImageSelectorView) {
    var OfficesView = BaseView.extend({

        events: {
            'click #btnEditAddress': 'loadAddress',
            'click #btnDeleteAddress': 'removeAddress',
            'click #btnNew': 'loadAddress'
        },

        vendorId: 0,

        template: HandleBars.compile($("#templateListOffices").html()),

        viewAddAddress: undefined,

        viewImages : undefined,

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
            this.collection = new AddressCollection();
            this.collection.on("sync", this.showOffices, this);
            this.collection.getOfficesByVendor(this.vendorId);
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