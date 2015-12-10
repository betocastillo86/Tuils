define(['jquery', 'underscore', 'util', 'baseView', 'tuils/collections/addresses', 'handlebars', 'tuils/views/panel/addAddressView','tuils/models/address',
    'imageSelectorView', 'confirm','handlebarsh'],
    function ($, _, TuilsUtilities, BaseView, AddressCollection, HandleBars, AddressView, AddressModel,
        ImageSelectorView, ConfirmMessageView) {
    var OfficesView = BaseView.extend({

        events: {
            'click #btnEditAddress': 'loadAddress',
            'click #btnDeleteAddress': 'removeAddress',
            'click #btnNewAddress': 'loadAddress'
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
            //Carga el script de weekline para escoger fechas
            //http://codebits.weebly.com/plugins/weekline-a-jquery-week-day-picker#5
            require(['jquery.weekline.min']);
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
        //Cuando la sede fue creada agrega un evento de sincronización a la carga de las oficinas
        //para autoseleccionar la dirección creada
        loadOfficesFromNew: function (newId) {
            var that = this;
            this.loadOffices();
            this.collection.on("sync", function () {
                //that.loadAddressById(newId);
                that.$("#btnEditAddress[tuils-id='" + newId + "']").click();
            }, this);
        },
        loadAddress : function(obj)
        {
            var id = parseInt($(obj.currentTarget).attr('tuils-id'));
            this.loadAddressById(id);
        },
        loadAddressById: function (id) {
            if (this.viewAddAddress) {
                this.viewAddAddress.undelegateEvents();
                //this.viewAddAddress.remove();
            }

            //this.viewAddressesCollection.push({ id: id, view : this.viewAddAddress });

            this.viewAddAddress = new AddressView({ el: isNaN(id) ? "#divNewOffice" : "#divDetail" + id, VendorId: this.vendorId });
            this.viewAddAddress.on("saved", this.loadOffices, this);
            this.viewAddAddress.on('saved-new', this.loadOfficesFromNew, this);
            this.viewAddAddress.on("back", this.closeAccordion, this);
            this.$('#btnNewAddress').show();
            this.hideNewOffice(!isNaN(id));


            if (!isNaN(id) && id > 0)
                this.viewAddAddress.loadAddress(id);
            else {
                var that = this;
                this.accordion.accordion({ active: this.$('.headerAccordion').length - 1, activate: function () { that.scrollFocusObject('#divNewOffice'); } });
                this.$('#btnNewAddress').hide();
                this.viewAddAddress.newAddress();
            }
        },
        hideNewOffice : function(hide)
        {
            this.$('#divNewOffice').css('display', !hide ? 'block' : 'none');
            this.$('.headerAccordion:last').css('display', !hide ? 'block' : 'none');
        },
        removeAddress : function(obj)
        {
            if (confirm("¿Seguro deseas eliminar esta sede?"))
            {
                var id = parseInt($(obj.currentTarget).attr('tuils-id'));
                var addressModel = new AddressModel();
                addressModel.on('sync', this.loadOffices, this);
                addressModel.deleteById(id);
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
            this.$('.headerAccordion:last').hide();
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