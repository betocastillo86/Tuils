define(['jquery', 'underscore', 'baseView', 'tuils/views/panel/editProductPicturesView', 'tuils/models/panel/updateProduct'],
    function ($, _, BaseView, EditProductPicturesView, EditProductModel) {

        var EditProductView = BaseView.extend({
            events: {
                'click #btnSave' : 'save'
            },

            bindings: {
                '#txtPrice': 'Price',
                '#ShortDescription': 'ShortDescription',
                '#Name' : 'Name'
            },

            productId : 0,

            viewPictures: undefined,

            initialize: function (args) {
                this.productId = args.productId;
                this.loadModel();
                this.loadControls();
                this.hidePublishButton();
            },
            loadModel: function () {
                this.model = new EditProductModel();
                this.model.set({
                    Id: this.productId,
                    Name: this.$("#Name").val(),
                    ShortDescription: this.$("#ShortDescription").val(),
                    Price: this.$("#txtPrice").val()
                });
                this.model.on('sync', this.saved, this);
            },
            loadControls: function () {
                this.loadPictures();
                this.render();
            },
            save: function () {
                this.validateControls();
                if (this.model.isValid()) {
                    this.model.update();
                }
            },
            saved: function () {
                this.alert({
                    message: 'Tu producto ha sido actualizado',
                    afterClose: function () {
                        document.location.href = '/mi-cuenta/mis-productos?p=true';
                    }
                });
            },
            loadPictures: function () {
                this.viewPictures = new EditProductPicturesView({ el: "#divPictures", productId : this.productId });
            },
            render: function () {
                this.stickThem();
                this.bindValidation();
            }
        });

        return EditProductView;
    });