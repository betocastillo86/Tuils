define(['underscore', 'backbone', 'fileModel', 'resize'], function (_, Backbone, FileModel) {
    var AddressModel = Backbone.Model.extend({

        idAttribute: "Id",

        baseUrl: "/api/addresses",

        url: "/api/addresses",

        validation: {
            Name: {
                required: true
            },
            Email: {
                required: false,
                pattern: 'email'
            },
            PhoneNumber: {
                pattern: 'number',
                required: true
            },
            FaxNumber: {
                pattern: 'number',
                required: false
            },
            StateProvinceId: {
                pattern: 'number',
                required: true
            },
            Address: {
                required: true
            },
            Schedule: {
                required: true
            },
            Latitude: {
                required: true
            },
            Longitude: {
                required: true
            }
        },
        labels: {
            Name: 'Nombre',
            Email: 'Correo',
            PhoneNumber: 'Teléfono',
            FaxNumber: 'Teléfono 2',
            StateProvinceId: 'Ciudad',
            Address: 'Dirección',
            Schedule: 'Horario',
            Latitude: 'Latitud',
            Longitude: 'Longitud'
        },
        validateLnLg: function (value) {
            return value != undefined && value != 0;
        },
        getAddress: function (id) {
            this.url = this.baseUrl + '/' + id;
            this.fetch();
        },
        insert: function (id) {
            this.url = this.baseUrl;
            this.save();
        },
        saveImage: function (file, pictureId, sizeMini, sizeBig) {
            var that = this;
           // require(['fileModel', 'resize'], function (FileModel) {

                that.fileModel = new FileModel();
                that.fileModel.on("file-saved", that.fileUploaded, that);
                that.fileModel.on("file-error", that.fileErrorUpload, that);

                var resizer = new window.resize();
                resizer.init();
                resizer.photo(file, sizeBig, 'file', function (resizedFile) {
                    resizer.photo(resizedFile, sizeMini, 'dataURL', function (thumbnail) {
                        that.fileModel.set({ src: thumbnail, file: resizedFile });
                        that.fileModel.upload({ saveUrl: '/api/addresses/' + that.get('Id') + '/pictures' + (pictureId > 0 ? '/' + pictureId : '') });
                    });
                });
          //  });
        },
        removeImage: function (addressId) {
            this.url = this.baseUrl + '/' + addressId + '/pictures/' + this.get('Id');
            this.destroy();
        },
        fileUploaded: function (file) {
            this.trigger("file-saved", file);
        },
        fileErrorUpload: function () {
            this.trigger("file-error");
        },
        deleteById: function (id) {
            this.url = this.baseUrl + '/' + id;
            this.set('Id', id);
            this.destroy();
        }
    });

    return AddressModel;
});