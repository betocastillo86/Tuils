define(['jquery', 'underscore', 'baseView', 'tuils/views/product/filterSearchView'],
    function ($, _, BaseView, FilterSearchView) {
        var SearchView = BaseView.extend({
            viewFiler: undefined,
            events: {
                'click .product-sorting a': 'switchOrderBy',
                'click #btnFilterByMobile' : 'showFilter'
            },
            initialize: function (args) {
                this.loadControls();
            },
            loadControls : function()
            {
                this.viewFiler = new FilterSearchView({ el: '.content-filter' });
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

        return SearchView;
    });