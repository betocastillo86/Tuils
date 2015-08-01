define(['jquery', 'underscore', 'util', 'baseView', 'tuils/collections/addresses', 'handlebars', 'tuils/views/panel/addAddressView',
    'imageSelectorView', 'confirm','handlebarsh'],
    function ($, _, TuilsUtilities, BaseView, AddressCollection, HandleBars, AddressView,
        ImageSelectorView, ConfirmMessageView) {
    var OfficesView = BaseView.extend({

        events: {
            'click .headerAccordion' : 'loadAddress',
            'click #btnDeleteAddress': 'removeAddress',
            'click #btnNewAddress' : 'addAdress'
        },

        vendorId: 0,

        viewAddAddress: undefined,

        viewImages : undefined,

        accordion: undefined,

        viewAddressesCollection : undefined,

        initialize: function (args)
        {
            this.template= HandleBars.compile($("#templateListOffices").html());
            this.vendorId = parseInt(this.$("#VendorId").val());
            this.loadControls();
        },
        loadControls : function()
        {
            this.loadOffices();
            this.accordion = this.$("#divListOffices");
            this.viewAddressesCollection = new Array();
        },
        addAddress: function () {
            this.accordion.accordion({ active: this.$('.headerAccordion').length - 1 });
        },
        loadOffices : function()
        {
            this.collection = new AddressCollection();
            this.collection.on("sync", this.showOffices, this);
            this.collection.getOfficesByVendor(this.vendorId);
        },
        loadAddress : function(obj)
        {
            var id = parseInt($(obj.currentTarget).attr('tuils-id'));
            
            if (this.viewAddAddress)
            {
                this.viewAddAddress.undelegateEvents();
                //this.viewAddAddress.remove();
            }

            //this.viewAddressesCollection.push({ id: id, view : this.viewAddAddress });

            this.viewAddAddress = new AddressView({ el: isNaN(id) ? "#divNewOffice" : "#divDetail" + id , VendorId: this.vendorId });
            this.viewAddAddress.on("saved", this.loadOffices, this);
            this.viewAddAddress.on("back", this.closeAccordion, this);

            this.$('#divNewOffice').css('display', isNaN(id) ? 'block' : 'none');

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
                var id = parseInt($(obj.currentTarget).attr('tuils-id'));
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
                    icons:false,
                    activate: function (event, ui)
                    {
                        ui.newPanel.attr('class', '');
                    }
                });

            this.accordion.accordion('refresh');
            //Remueve los estilos por defecto de 
            this.accordion.attr("class", '');
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