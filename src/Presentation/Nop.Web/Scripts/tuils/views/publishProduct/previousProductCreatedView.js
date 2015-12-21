define(['jquery', 'underscore', 'baseView', 'handlebars', 'storage'],
    function ($, _, BaseView, Handlebars, TuilsStorage) {
        var PreviousProductCreatedView = BaseView.extend({
            events: {
                'click #btnCreateNewProduct': 'createNewProduct',
                'click #btnRecoverProduct': 'recoverProduct'
            },
            images: undefined,

            createNewProduct: function () {
                //Limpia el producto en el storage y lanza el evento para mostrar los productos
                TuilsStorage.setPublishProduct(undefined);
                this.trigger('previousProduct-createNew');
            },
            recoverProduct: function () {
                this.trigger('previousProduct-recover');
            },
            initialize: function (args) {
                this.template = Handlebars.compile($("#templateRecreateProduct").html());
                this.model = args.model;
                this.render();
            },
            render: function () {
                this.$el.html(this.template({ Name: this.model.get('Name') }));
                return this;
            }
        });

        return PreviousProductCreatedView;
    });