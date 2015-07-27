
define(['jquery', 'underscore', 'baseView'],
function ($, _, BaseView) {
    var FilterSearchView = BaseView.extend({
        events: {
            'click #btnShowFilter': 'showFilter',
            'click .tit_fil .icon-close' : 'closeFilter'
        },
        divFilter: $(".filters-main"),

        initialize: function (args) {
            this.loadControls();
        },
        loadControls : function(){
            this.handleResize();
            this.on("window-resized-min", this.resize, this);
            this.on("window-resized-max", this.resize, this);
            this.resize();
        },
        resize: function () {
            if (this.isMinSize()) {
                this.closeFilter();
            }
            else {
                this.showFilter();
            }
        },
        showFilter: function () {
            this.divFilter.show();
            window.scrollTo(0, 0);
        },
        closeFilter : function(){
            this.divFilter.hide();
        },
        render: function () {
            this.render();
        }
    });

    return FilterSearchView;
});