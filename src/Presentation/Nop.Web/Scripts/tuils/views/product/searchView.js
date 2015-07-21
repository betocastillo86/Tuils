﻿define(['jquery', 'underscore', 'baseView', 'tuils/views/product/filterSearchView'],
    function ($, _, BaseView, FilterSearchView) {
        var SearchView = BaseView.extend({
            viewFiler : undefined,
            initialize: function (args) {
                this.loadControls();
            },
            loadControls : function()
            {
                this.viewFiler = new FilterSearchView({ el: '.product-filters-wrapper' });
            },
            render: function () {

            }
        });

        return SearchView;
    });