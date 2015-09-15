define(['underscore', 'backbone', 'fileModel'],
    function (_, Backbone, FileModel) {
        var PictureModel = Backbone.Model.extend({

            idAttribute: "Id",

            baseUrl: '/api/pictures',

            url: '/api/pictures',

            removeFromProduct: function (productId) {
                this.url = '/api/products/' + productId + '/pictures/' + this.get('Id');
                this.destroy({wait : true});
            },
            savePictureToProduct: function (file, productId, sizeMini, sizeBig) {
                var uploadUrl = '/api/products/' + productId + '/pictures' + (this.get('Id') > 0 ? '/' + this.get('Id') : '');
                this.savePicture(file, uploadUrl, sizeMini, sizeBig);
            },
            savePicture: function (file, uploadUrl, sizeMini, sizeBig) {
                var that = this;
                this.fileModel = new FileModel();
                //Lanza los eventos de sincronización para estos archivos
                this.fileModel.on("file-saved", this.fileUploaded, that);
                this.fileModel.on("file-error", this.fileErrorUpload, that);

                var resizer = new window.resize();
                resizer.init();
                resizer.photo(file, sizeBig, 'file', function (resizedFile) {
                    resizer.photo(resizedFile, sizeMini, 'dataURL', function (thumbnail) {
                        that.fileModel.set({ src: thumbnail, file: resizedFile });
                        that.fileModel.upload({ saveUrl: uploadUrl });
                    });
                });
            },
            fileUploaded: function (response) {
                this.set(response.toJSON());
                this.trigger("sync", this);
            },
            fileErrorUpload: function (response) {
                this.trigger("error", this);
            }
        });

        return PictureModel;
    });