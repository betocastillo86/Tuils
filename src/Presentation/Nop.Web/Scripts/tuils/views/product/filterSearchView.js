
define(['jquery', 'underscore', 'baseView'],
function ($, _, BaseView) {
    var FilterSearchView = BaseView.extend({
        events: {
            'click #btnShowFilter': 'showFilter',
            'click .tit_fil .icon-close': 'closeFilter',
            'focusout .priceFilter input[type="text"]': 'loadLinkPriceFilter',
            'click .qcat-more-filters' : 'showMoreOptions'
        },
        divFilter: $(".filters-main"),

        initialize: function (args) {
            this.loadControls();
        },
        loadControls: function () {
            this.basicValidations();
            this.handleResize();
            this.on("window-resized-min", this.resize, this);
            this.on("window-resized-max", this.resize, this);
            this.resize();
        },
        showMoreOptions: function (obj) {
            var button = $(obj.currentTarget);
            //Muestra todas las opciones
            button.parent().find('li').show();
            //Oculta el botón
            button.hide();
        },
        resize: function () {
            if (this.isMinSize()) {
                this.closeFilter();
            }
            else {
                this.showFilter();
            }
        },
        loadLinkPriceFilter: function () {
            var from = parseInt(this.$("#fromFilter").val());
            var to = parseInt(this.$("#toFilter").val());

            //Si alguno de los datos esta ingresado carga el vinculo
            if (!isNaN(from) || !isNaN(to)) {
                //Debe validar que los filtros sean validos
                var allowFilterByPrice = false;
                from = !isNaN(from) ? from : 0;
                to = !isNaN(to) ? to : "";

                this.$("#aCustomFilterPrice").attr("href", this.$("#aCustomFilterPrice").attr("data-url").replace("{from}", from).replace("{to}", to));
            }
            else {
                this.$("#aCustomFilterPrice").attr("href", "");
            }
            
        },
        showFilter: function () {
            this.divFilter.show();
            window.scrollTo(0, 0);
        },
        closeFilter : function(){
            this.divFilter.hide();
            $('body').removeClass('body-noscroll');
        },
        render: function () {
            this.render();
        }
    });

    return FilterSearchView;
});