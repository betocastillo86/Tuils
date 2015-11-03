define(['jquery', 'underscore', 'baseView', 'util', 'productModel', 'tuils/models/panel/myProduct'],
    function ($, _, BaseView, TuilsUtil, ProductModel, MyProductModel) {
            
        var MyProductsView = BaseView.extend({
            events: {
                'keyup .search-pdt .searchinput': 'search',
                'click .btnUnpublish': 'remove',
                'click .btnPublishMyProducts' : 'activate'
            },
            initialize: function(args){
            
            },
            remove: function (obj) {
                if (confirm("¿Seguro desea eliminar este producto?"))
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
            productErrorRemoved: function () {
                this.alert('No fue posible deshabilitar el producto. Intenta de nuevo o comentanos tu problema a info@tuils.com');
            },
            productErrorActivated: function (resp, err) {
                if (err.responseJSON.Message) {
                    this.alert(err.responseJSON.Message);
                }
                else {
                    this.alert('No fue posible habilitar el producto. Intenta de nuevo o comentanos tu problema a info@tuils.com');
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