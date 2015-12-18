define(['jquery', 'underscore', 'baseView', 'tuils/views/product/filterSearchView'],
    function ($, _, BaseView, FilterSearchView) {
        var CategoryView = BaseView.extend({
            viewFilter: undefined,
            events: {
                'click .product-sorting a': 'switchOrderBy',
                'click #btnFilterByMobile': 'showRespFilter'
            },
            initialize: function (args) {
                this.loadControls();
            },
            switchOrderBy: function () {
                this.$('.product-sorting select').show();
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