define(['underscore', 'backbone', 'fileModel','configuration', 'resize'], function (_, Backbone, FileModel, TuilsConfiguration) {
    var VendorHeaderModel = Backbone.Model.extend({

        idAttribute : 'Id',

        baseUrl: "/api/vendors/header",

        url: "/api/vendors/header",

        validation : {
            Name: {
                required: true
            },
            Description: {
                required: true
            }
        },

        fileModel: undefined,

        thumbnail: undefined,

        resizer : undefined,

        initialize : function(args)
        {
            this.set({id : args.id});

            this.fileModel = new FileModel();
            this.fileModel.on("file-saved", this.fileUploaded, this);
            this.fileModel.on("file-error", this.fileErrorUpload, this);
            this.resizer = new window.resize();
            this.resizer.init();
        },

        saveHeader: function () {
            this.save();
        },
        saveBackground: function (file)
        {
            this.saveImage(file, 'backgroundPicture', TuilsConfiguration.media.vendorBackgroundThumbPictureSize, TuilsConfiguration.media.coverImageMaxSizeResize, false);
        },
        saveLogo: function (file) {
            this.saveImage(file, 'picture', 800, TuilsConfiguration.media.logoImageMaxSizeResize, true);
        },
        saveImage: function (file, type, sizeMini, sizeBig, crop)
        {
            var that = this;
            this.resizer[crop ? 'photoCrop' : 'photo'](file, sizeBig, 'file', function (resizedFile) {
                that.resizer.photo(resizedFile, sizeMini, 'dataURL', function (thumbnail) {
                    that.fileModel.set({ src: thumbnail, file: resizedFile });
                    that.fileModel.upload({ saveUrl: '/api/vendors/' + that.get('Id') + '/' + type });
                });
            });
        },
        fileUploaded: function (file)
        {
            this.trigger("file-saved", file);
        },
        fileErrorUpload: function ()
        {
            this.trigger("file-error");
        },

        saveCoverPosition: function ()
        {
            this.url = '/api/vendors/' + this.get('Id') + '/header/background';
            this.save();
        }

    });

    return VendorHeaderModel;
});