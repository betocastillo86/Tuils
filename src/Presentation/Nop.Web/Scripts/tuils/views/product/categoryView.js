define(['jquery', 'underscore', 'baseView', 'tuils/views/product/filterSearchView'],
    function ($, _, BaseView, FilterSearchView) {
        var CategoryView = BaseView.extend({
            viewFilter: undefined,
            initialize: function (args) {
                this.loadControls();
            },
            loadControls : function()
            {
                this.viewFilter = new FilterSearchView({ el: '.content-filter' });
                this.$('div.product-list').length ? $('.fontawesome-reorder').addClass('active') : $('.fontawesome-th').addClass('active');
            },
            render: function () {

            }
        });

        return CategoryView;
    });