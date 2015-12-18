﻿define(['jquery', 'underscore', 'baseView', 'tuils/views/product/filterSearchView'],
    function ($, _, BaseView, FilterSearchView) {
        var SearchView = BaseView.extend({
            viewFiler: undefined,
            events: {
                'click .product-sorting a': 'switchOrderBy',
                'click #btnFilterByMobile': 'showRespFilter'
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
            render: function () {
               this.$('div.product-list').length ? $('.fontawesome-reorder').addClass('active') : $('.fontawesome-th').addClass('active');
            }
        });

        return SearchView;
    });