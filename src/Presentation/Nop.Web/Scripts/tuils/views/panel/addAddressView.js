define(['underscore', 'util', 'baseView', 'tuils/models/address', 'handlebars', 'tuils/views/utilities/selectPointMapView'],
    function (_, TuilsUtilities, BaseView, AddressModel, Handlebars, MapView) {

        var AddAddressView = BaseView.extend({
            events: {
                "click #btnSaveAddress": "save",
                'click #btnSaveBack' : 'back'
            },
            
            vendorId: 0,

            btnSave: undefined,

            viewMap: undefined,

            template : Handlebars.compile($("#templateOfficeDetail").html()),

            bindings: {
                "#txtName": "Name",
                "#txtEmail": "Email",
                "#txtPhoneNumber": "PhoneNumber",
                "#txtFaxNumber": "FaxNumber",
                "#ddlStateProvinceId": "StateProvinceId",
                "#txtAddress": "Address",
                "#txtSchedule": "Schedule"
            },
            initialize: function (args)
            {
                this.vendorId = args.VendorId;
                this.model = new AddressModel({ 'VendorId': args.VendorId });
                this.model.on('error', this.errorSaving, this);
                this.render();
                this.loadControls();
            },
            loadControls : function()
            {
                this.btnSave = this.$("#btnSaveAddress");
                this.loadMap();
            },
            loadAddress : function(id)
            {
                this.removeErrors();
                this.model.once('sync', this.showAddress, this);
                this.model.getAddress(id);
            },
            loadMap: function () {
                //var that = this;
                //require(['tuils/views/utilities/selectPointMapView'], function (MapView) {
                    this.viewMap = new MapView({ el: "#canvasMapAddress" });
                    this.viewMap.on('set-position', this.setMapPosition, this);
                //});
            },
            newAddress : function()
            {
                this.removeErrors();
                this.model.clear();
                this.model.set('VendorId', this.vendorId);
                this.viewMap.loadMap();
            },
            deleteById : function(id)
            {
                var deletedModel = new AddressModel();
                deletedModel.on('error', this.errorSaving, this);
                deletedModel.on('sync', this.saved, this);
                deletedModel.deleteById(id);
            },
            showAddress : function()
            {
                this.$("#ddlStateProvinceId").val(this.model.get('StateProvinceId'));
                this.viewMap.loadMap({ lat: this.model.get('Latitude'), lon: this.model.get('Longitude') });
            },
            setMapPosition : function(args)
            {
                this.model.set('Latitude', args.lat);
                this.model.set('Longitude', args.lon);
            },
            render : function()
            {
                this.$el.html(this.template());
                this.bindValidation();
                this.stickThem();
                return this;
            },
            save: function (obj)
            {
                this.validateControls();
                if (this.model.isValid())
                {
                    this.model.once('sync', this.saved, this);
                    this.btnSave.attr('disabled', true);
                    this.model.insert();
                }
            },
            saved: function ()
            {
                this.trigger("saved", this.model);
                this.btnSave.attr('disabled', false);
            },
            errorSaving: function (error)
            {
                alert("Ocurrió un error, porfavor intentalo de nuevo");
                this.btnSave.attr('disabled', false);
            },
            back: function ()
            {
                this.trigger("back", 0);
            }

        });

        return AddAddressView;

});