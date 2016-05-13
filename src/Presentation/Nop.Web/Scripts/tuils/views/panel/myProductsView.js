define(['jquery', 'underscore', 'baseView', 'util', 'productModel', 'tuils/models/panel/myProduct', 'resources'],
    function ($, _, BaseView, TuilsUtil, ProductModel, MyProductModel, TuilsResources) {
            
        var MyProductsView = BaseView.extend({
            events: {
                'keyup .search-pdt .searchinput': 'search',
                'click .btnUnpublish': 'remove',
                'click .btnPublishMyProducts': 'activate',
                'click .HasReachedLimitOfFeature' : 'showLimitMessage'
            },
            initialize: function(args){
                this.hidePublishButton();
            },
            remove: function (obj) {
                if (confirm("¿Seguro desea desactivar este producto?"))
                {
                    var id = parseInt($(obj.target).attr('data-id'));
                    var product = new ProductModel({ Id: id });
                    product.on('sync', this.productRemoved, this);
                    product.on('error', this.productErrorRemoved, this);
                    product.remove();
                }
            },
            activate: function (obj) {
                if (confirm("¿Seguro deseas reactivar este producto?")) {
                    var id = parseInt($(obj.target).attr('data-id'));
                    var product = new MyProductModel({ Id: id });
                    product.on('sync', this.productActivated, this);
                    product.on('error', this.productErrorActivated, this);
                    product.enable();
                }
            },
            productRemoved : function(model){
                this.$('.product-detail[data-id="' + model.get('Id') + '"]').fadeOut();
                this.alert('El producto fue deshabilitado correctamente');
            },
            productActivated: function (model) {
                this.$('.product-detail[data-id="' + model.get('Id') + '"]').fadeOut();
                this.alert('El producto fue habilitado correctamente');
            },
            showLimitMessage: function () {
                this.alertError({ message: TuilsResources.products.hasReachedLimitFeaturedAlert , duration : 10000});
            },
            productErrorRemoved: function () {
                this.alertError('No fue posible deshabilitar el producto. Intenta de nuevo o comentanos tu problema a info@tuils.com');
            },
            productErrorActivated: function (resp, err) {
                if (err.responseJSON.Message) {
                    this.alertError(err.responseJSON.Message);
                }
                else {
                    this.alertError('No fue posible habilitar el producto. Intenta de nuevo o comentanos tu problema a nuestra pagina de Facebook');
                }
            },
            search: function (obj) {
                if (obj.keyCode != 13) {
                    var value = $(obj.target).val();
                    //this.$('.btn_search').attr("href", "?q=" + value);
                    this.$('.btn_search').attr("href", TuilsUtil.updateQueryStringParameter(document.location.href, 'q', value));
                }
                else {
                    document.location.href = this.$('.search-pdt a').attr("href");
                }
            },
        });

        return MyProductsView; 
});