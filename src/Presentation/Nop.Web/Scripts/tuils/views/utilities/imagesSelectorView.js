﻿define(['jquery', 'underscore', 'backbone', 'fileModel', 'fileCollection', 'resize'],
    function ($, _, Backbone, FileModel, FileCollection) {
    var ImagesSelectorView = Backbone.View.extend({
        events: {
            "click .addImageGalery": "addImage",
            "change input[type=file]": "uploadImage",
            "click .btnNext": "save",
            "click .btnBack": "back"
        },
        //controls
        fileUpload: undefined,
        currentControlImage: -1,
        //controls
        attributeFile: "tuils-file",

        collection: undefined,

        urlSave : undefined,

        resizer: undefined,

        onBackRemoveImages: false,

        initialize: function (args) {
            this.loadControls();
            if (args.onBackRemoveImages)
                this.onBackRemoveImages = args.onBackRemoveImages;

            if (args.urlSave)
                this.urlSave = args.urlSave;
        },
        render: function () {
            return this;
        },
        loadControls: function () {
            this.fileUpload = this.$("input[type='file']");
            this.collection = new FileCollection();
            this.resizer = new window.resize();
            this.resizer.init();
        },
        addImage: function (obj) {
            var target = $(obj.currentTarget);
            this.currentControlImage = target;

            var allowLoad = true;

            var attrFile = target.attr(this.attributeFile);
            if (attrFile) {
                allowLoad = this.removeImage(target);
            }

            if (allowLoad)
                this.fileUpload.click();

        },
        removeImage: function (obj) {
            if (confirm("¿Deseas cambiar la imagen?")) {
                if (obj.target) obj = $(obj.target);
                //Quita la imagen de la lista y la desvincula del control
                var fileToRemove = obj.attr(this.attributeFile);
                this.collection.remove(this.collection.findWhere({ guid: fileToRemove }))
                this.switchImage();
                //Elimina la imagen del servidor
                var fileModel = new FileModel({ fileGuid: fileToRemove });
                fileModel.remove();
                return true;
            }
            else {
                return false;
            }
        },
        uploadImage: function (obj) {
            var file = obj.target.files[0];
            var that = this;
            if (file) {
                var fileModel = new FileModel();
                fileModel.on("file-saved", this.fileUploaded, this);
                fileModel.on("file-error", this.fileErrorUpload, this)
                this.resizer.photo(file, 1200, 'file', function (resizedFile) {

                    that.resizer.photo(resizedFile, 400, 'dataURL', function (thumbnail) {
                        that.switchImage(thumbnail);
                        fileModel.set({ src: thumbnail, file: resizedFile });
                        fileModel.upload({saveUrl : that.urlSave });
                    });
                });

            }

        },
        switchImage : function(urlImage)
        {
            if (urlImage) {
                this.currentControlImage.find("img").attr("src", urlImage).show();
                this.currentControlImage.find("span").hide();
            }
            else {
                this.currentControlImage.find("img").removeAttr("src").hide();
                this.currentControlImage.removeAttr(this.attributeFile);
                this.currentControlImage.find("span").show();
            }
        },
        fileUploaded: function (model) {
            var srcImage = this.currentControlImage.find("img", "src");
            var guidImage = model.get('guid');
            this.collection.add(model);
            this.currentControlImage.attr(this.attributeFile, guidImage);
        },
        fileErrorUpload: function (resp) {
            this.switchImage();
        },
        save: function () {
            if (this.collection.length > 0) {
                this.trigger("images-save", this.collection);
            }
            else {
                alert("Debe seleccionar por lo menos una imagen");
            }

        },
        back: function () {
            if (this.onBackRemoveImages)
                this.removeAllImages();

            this.trigger("images-back");
        },
        removeAllImages: function () {

        }

    });

    return ImagesSelectorView;
});
