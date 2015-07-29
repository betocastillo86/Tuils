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
            },
            render: function () {

            }
        });

        return CategoryView;
    });