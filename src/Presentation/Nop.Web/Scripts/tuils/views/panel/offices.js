define(['underscore', 'util', 'baseView', 'tuils/collections/addresses', 'handlebars', 'tuils/views/panel/addAddressView', 'imageSelectorView','handlebarsh'],
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
            this.accordion = this.$("#divListOffices");
        },
        loadOffices : function()
        {
            this.collection = new AddressCollection();
            this.collection.on("sync", this.showOffices, this);
            this.collection.getOfficesByVendor(this.vendorId);
        },
        loadAddress : function(obj)
        {
            var id = parseInt($(obj.target).attr('tuils-id'));
            
            if (this.viewAddAddress)
            {
                this.viewAddAddress.undelegateEvents();
                this.viewAddAddress.remove();
            }

            this.viewAddAddress = new AddressView({ el: isNaN(id) ? "#divNewOffice" : "#divDetail"+id, VendorId: this.vendorId });
            this.viewAddAddress.on("saved", this.loadOffices, this);
            this.viewAddAddress.on("back", this.closeAccordion, this);
            this.$('#divNewOffice').css('display', isNaN(id) ? 'block': 'none');

            if (!isNaN(id) && id > 0)
                this.viewAddAddress.loadAddress(id);
            else
            {
                this.accordion.accordion({ active: this.$('.headerAccordion').length - 1 });
                this.viewAddAddress.newAddress();
            }

        },
        removeAddress : function(obj)
        {
            if (confirm("¿Seguro deseas eliminar esta sede?"))
            {
                var id = parseInt($(obj.target).attr('tuils-id'));
                this.viewAddAddress.deleteById(id);
            }
        },
        showOffices : function(offices)
        {
            this.accordion.html(this.template(offices.toJSON()));

            this.accordion.accordion(
                {
                    header: '.headerAccordion',
                    heightStyle: 'content',
                    collapsible: true,
                    active: false,
                    beforeActivate: function (event, ui) {
                        //Solo en el caso de darle editar puede abrir puede abrir
                        if (event.originalEvent)
                            return event.originalEvent.target.id == "btnEditAddress";
                        else
                            return true;
                    }
                });

            this.accordion.accordion('refresh');
        },
        closeAccordion: function () {
            this.accordion.accordion({ active: false })
        },
        render: function ()
        {
            return this;
        }
    });

    return OfficesView;
});