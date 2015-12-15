define(['jquery', 'underscore', 'util', 'baseView', 'tuils/models/address', 'handlebars', 'tuils/views/utilities/selectPointMapView',
    'tuils/collections/addresses', 'handlebarsh'],
    function ($, _, TuilsUtilities, BaseView, AddressModel, Handlebars, MapView,
        AddressCollection) {

        var AddAddressView = BaseView.extend({
            events: {
                "click #btnSaveAddress": "save",
                'click #btnSaveBack': 'back',
                'click .picture-uploader li': 'changeImage',
                "change input[type='file']": "saveImage",
                "click .icon-delete": "removeImage",
                'change .timeHourSchedule': 'updateSchedule'
            },
            
            vendorId: 0,

            id : 0,

            btnSave: undefined,

            viewMap: undefined,

            fileUpload: undefined,

            dayLabels : ["Lun", "Mar", "Mie", "Jue", "Vie", "Sab", "Dom"],

            selectedPictureId : 0,

            pictureCollection : undefined,

            templatePictures: undefined,
            weekSelector : undefined,

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
                this.model.on('file-error', this.errorPicture, this);

                this.pictureCollection = new AddressCollection();
                this.pictureCollection.on('sync', this.showPictures, this);
                this.pictureCollection.on('remove', this.showPictures, this);

                this.render();
                this.loadControls();
            },
            loadControls : function()
            {
                this.btnSave = this.$("#btnSaveAddress");
                this.fileUpload = this.$("input[type='file']");
                this.loadMap();
                this.loadSchedule();
            },
            loadAddress : function(id)
            {
                this.id = id;
                this.removeErrors();
                this.model.once('sync', this.showAddress, this);
                this.model.getAddress(id);
            },
            loadSchedule: function () {
                var that = this;
                this.weekSelector = this.$('.spanSchedule').weekLine({
                    dayLabels: that.dayLabels,
                    onChange: function () {
                        var days = $(this).weekLine('getSelected', 'descriptive');
                        that.model.set('Days', days);
                        that.updateSchedule();
                        //days = days.replace('Lun', 'Mo').replace('Mar', 'Tu').replace('Mier', 'We').replace('Jue', 'Th').replace('Vie', 'Fr').replace('Sab', 'Sa').replace('Dom', 'Su');
                        //that.model.set('Schedule',  days + ' ' + that.$('#txtOpenHour').val() + '-' + that.$('#txtCloseHour').val());
                    }
                });
            },
            updateSchedule: function () {
                //Actualiza el schedule en el modelo con la hora
                if (this.model.get('Days'))
                    this.model.set('Schedule', this.model.get('Days') + ' ' + this.$('#txtOpenHour').val() + '-' + this.$('#txtCloseHour').val());
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
                this.viewMap.loadMap({showAddress : true, draggable:true });
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
                this.viewMap.loadMap({ lat: this.model.get('Latitude'), lon: this.model.get('Longitude'), showAddress: true, draggable:true });

                //Carga el horario
                this.setSchedule();

                this.loadPictures();
            },
            setSchedule: function () {
                var scheduleParts = this.model.get('Schedule').replace(/,\s/g,',').split(' ');
                var days = scheduleParts[0];
                var openHour = scheduleParts[1].split('-')[0];
                var closeHour = scheduleParts[1].split('-')[1];
                var that = this;


                //Toma varios dias unidos y los convierte a dias separados
                function getDaysByComma(jointDays)
                {
                    //Busca dias unidos
                    var splitDays = jointDays.split('-');
                    if(splitDays.length > 1)
                    {
                        //Toma el primer y ultimo dia para calcular los que están implicitos
                        var firstDay = splitDays[0];
                        var lastDay = splitDays[1];
                        var days = '';
                        var found = false;
                        _.each(that.dayLabels, function(element, index){
                            if(element == firstDay)
                            {
                                days = element;
                                found = true;
                            }
                            else if(found && element == lastDay)
                            {
                                days += ','+element;
                                found = false;
                            }
                            else if(found)
                            {
                                days += ','+element;
                            }
                        });

                        return days;
                    }
                    else
                    {
                        return jointDays;
                    }
                }

                var daysComma = '';
                _.each(days.split(','), function (element, index) {
                    var append = getDaysByComma(element);
                    if (append != '')
                    {
                        if (daysComma.length > 0)
                            daysComma += ',';
                        daysComma += append;
                    }
                });


                this.weekSelector.weekLine('setSelected', daysComma);
                this.model.set('Days', this.weekSelector.weekLine('getSelected', 'descriptive'));
                this.$('#txtOpenHour').val(openHour);
                this.$('#txtCloseHour').val(closeHour);
            },
            loadPictures : function(){
                this.pictureCollection.getPictures(this.id);
            },
            errorPicture: function (error) {
                this.alert(error.Message);
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
                var selectedCity = this.$("#ddlStateProvinceId option").filter(function () {
                    return this.text == address.cityName;
                });

                if(selectedCity.length)
                    this.model.set('StateProvinceId', selectedCity.val());
                
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
                var errors = this.validateControls();
                if (this.model.isValid()) {
                    this.model.once('sync', this.saved, this);
                    this.btnSave.attr('disabled', true);
                    this.model.insert();
                }
                
                this.$('#errorSelectMap').css('display', (errors && (errors.Latitude || errors.Longitude)) ? 'block' : 'none');
            },
            saved: function ()
            {
                this.btnSave.attr('disabled', false);
                if (this.id > 0) {
                    this.trigger("saved", this.model);
                    this.alert('La sede fue actualizada con exito');
                }
                else {
                    this.alert({ message: 'La sede fue creada con exito. Carga las imagenes de la sede', duration: 7000 });
                    this.trigger('saved-new', this.model.get('Id'));
                }

            },
            changeImage: function (obj) {
                if ($(obj.target).is(".icon-delete")) return false;
                obj.preventDefault();
                this.fileUpload.click();
                this.selectedPictureId = $(obj.currentTarget).attr('data-id');
            },
            saveImage: function (obj) {
                var file = obj.target.files[0];
                if (file) {
                    if (TuilsUtilities.isValidSize(obj.target)) {
                        if (TuilsUtilities.isValidExtension(obj.target, 'image')) {
                            this.showLoadingBack(this.model, this.$(this.selectedPictureId ? '.liOfficeImage[data-id="' + this.selectedPictureId + '"]' : '.liNewOfficeImage'));
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
            removeImage: function (obj) {
                var pictureId = parseInt($(obj.target).attr("data-id"));
                var model = this.pictureCollection.findWhere({ Id: pictureId });
                model.removeImage(this.id);
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