define(['jquery', 'underscore', 'util', 'baseView', 'tuils/models/address', 'handlebars', 'tuils/views/utilities/selectPointMapView', 'tuils/collections/addresses', 'handlebarsh'],
    function ($, _, TuilsUtilities, BaseView, AddressModel, Handlebars, MapView, AddressCollection) {

        var AddAddressView = BaseView.extend({
            events: {
                "click #btnSaveAddress": "save",
                'click #btnSaveBack': 'back',
                'click .picture-uploader li': 'changeImage',
                "change input[type='file']": "saveImage",
            },
            
            vendorId: 0,

            id : 0,

            btnSave: undefined,

            viewMap: undefined,

            fileUpload: undefined,

            selectedPictureId : 0,

            pictureCollection : undefined,

            templatePictures: undefined,

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
                this.template = Handlebars.compile($("#templateOfficeDetail").html());
                this.templatePictures = Handlebars.compile($("#templatePictures").html());

                this.vendorId = args.VendorId;
                this.model = new AddressModel({ 'VendorId': args.VendorId });
                this.model.on('error', this.errorSaving, this);
                this.model.on('file-saved', this.loadPictures, this);


                this.pictureCollection = new AddressCollection();
                this.pictureCollection.on('sync', this.showPictures, this);

                this.render();
                this.loadControls();
            },
            loadControls : function()
            {
                this.btnSave = this.$("#btnSaveAddress");
                this.fileUpload = this.$("input[type='file']");
                this.loadMap();
            },
            loadAddress : function(id)
            {
                this.id = id;
                this.removeErrors();
                this.model.once('sync', this.showAddress, this);
                this.model.getAddress(id);
            },
            loadMap: function () {
                this.viewMap = new MapView({ el: "#canvasMapAddress" });
                this.viewMap.on('set-position', this.setMapPosition, this);
                this.viewMap.on('set-address', this.setAddress, this);
            },
            newAddress : function()
            {
                this.removeErrors();
                this.model.clear();
                this.model.set('VendorId', this.vendorId);
                this.viewMap.loadMap({showAddress : true });
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
                this.viewMap.loadMap({ lat: this.model.get('Latitude'), lon: this.model.get('Longitude'), showAddress: true });
                this.loadPictures();
            },
            loadPictures : function(){
                this.pictureCollection.getPictures(this.id);
            },
            showPictures: function () {
                this.$("#divPictures").html(this.templatePictures(
                    {
                        pictures: this.pictureCollection.toJSON(),
                        allowMoreImages: this.pictureCollection.toJSON().length < 6
                    }));
            },
            setMapPosition : function(args)
            {
                this.model.set('Latitude', args.lat);
                this.model.set('Longitude', args.lon);
            },
            setAddress : function(address){
                this.model.set('Address', address.address);
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
            changeImage: function (obj) {
                this.fileUpload.click();
                this.selectedPictureId = $(obj.currentTarget).attr('data-id');
            },
            saveImage: function (obj) {
                var file = obj.target.files[0];
                if (file) {
                    if (TuilsUtilities.isValidSize(obj.target)) {
                        if (TuilsUtilities.isValidExtension(obj.target, 'image')) {
                            this.model.saveImage(file, this.selectedPictureId);
                        }
                        else {
                            alert("La extensión del archivo no es valida");
                        }

                    }
                    else {
                        alert("El tamaño excede el limite");
                    }
                }
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