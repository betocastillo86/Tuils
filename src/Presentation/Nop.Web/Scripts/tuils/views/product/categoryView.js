define(['jquery', 'underscore', 'baseView', 'tuils/views/product/filterSearchView'],
    function ($, _, BaseView, FilterSearchView) {
        var CategoryView = BaseView.extend({
            viewFilter: undefined,
            events: {
                'click .product-sorting a': 'switchOrderBy',
                'click #btnFilterByMobile': 'showFilter'
            },
            initialize: function (args) {
                this.loadControls();
            },
            switchOrderBy: function () {
                this.$('.product-sorting select').show();
            },
            showFilter: function () {
                $(".filters-main").show();
                $(".filters-main").focus();
                window.scrollTo(0, 0);
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