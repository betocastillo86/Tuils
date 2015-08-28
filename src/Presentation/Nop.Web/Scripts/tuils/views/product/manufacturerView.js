define(['jquery', 'underscore', 'baseView', 'tuils/views/product/filterSearchView'],
    function ($, _, BaseView, FilterSearchView) {
        var ManufacturerView = BaseView.extend({
            viewFilter: undefined,
            events: {
                'click .product-sorting a': 'switchOrderBy',
                'click #btnFilterByMobile': 'showFilter'
            },
            initialize: function (args) {
                this.loadControls();
            },
            loadControls : function()
            {
                this.viewFilter = new FilterSearchView({ el: '.content-filter' });
                this.$('div.product-list').length ? $('.fontawesome-reorder').addClass('active') : $('.fontawesome-th').addClass('active');
            },
            switchOrderBy: function () {
                this.$('.product-sorting select').show();
            },
            showFilter: function () {
                $(".filters-main").show();
                $(".filters-main").focus();
                window.scrollTo(0, 0);
            },
            render: function () {

            }
        });

        return ManufacturerView;
    });