define(['jquery', 'underscore', 'backbone', 'baseView', 'configuration', 'jqueryui'],
    function ($, _, Backbone, BaseView, TuilsConfiguration) {
    var SearchView = BaseView.extend({
        events: {
            'click .icon-lupa': 'showSearchBox',
            'click .closeSearchResponsive': 'hideResponsiveSearch',
            'submit form' : 'search',
            'keyup #small-searchterms' : 'refreshSubmitForm'
        },

        autocompleteUrl: undefined,
        minLengthAutocomplete : 2,
        searchBox : undefined,

        initialize: function () {
            this.render();
        },
        loadControls: function () {
            this.searchBox = this.$("#small-searchterms");
            this.handleResize();
            this.on("window-resized-max", this.hideResponsiveSearch, this);
            this.autocompleteUrl = this.searchBox.attr("data-url");
            this.minLengthAutocomplete = this.searchBox.attr("data-minlength");
            this.loadAutoComplete();
        },
        refreshSubmitForm: function () {
            this.$("form").attr('action', '/buscar/' + this.searchBox.val());
        },
        search: function () {
            if (this.searchBox.val() == "") {
                alert(this.$("#Search_EnterSearchTerms").val());
                this.searchBox.focus();
                return false;
            }
            return true;
        },
        showSearchBox: function (obj) {
            //Si es pequeño muestra busqueda responsive, si no muestra la otra
            if (this.isMinSize())
                this.switchResponsiveSearch(true);
            else
                this.$("form").submit();
        },
        hideResponsiveSearch: function () {
            this.switchResponsiveSearch(false);
        },
        switchResponsiveSearch: function (showSearch) {
            //Agrega o quita la clase responsive del buscador
            if (showSearch) {
                this.$("#divSearchText").addClass("divSearchTextResponsive");
            }
            else {
                this.$("#divSearchText").removeClass("divSearchTextResponsive");
                $(".ui-autocomplete").hide();
            }

            $(".header-logo").css('display', !showSearch ? 'block' : 'none');
            this.$("#divSearchText input").css('display', showSearch ? 'block' : '');
            this.$("#divSearchText a").css('display', showSearch ? 'block' : '');
            this.$("#small-searchterms").focus();
        },
        loadAutoComplete: function () {
            var that = this;
            this.searchBox.autocomplete({
                delay: 200,
                minLength: this.minLengthAutocomplete,
                source: this.autocompleteUrl,
                select: function (event, ui) {
                    that.$("#small-searchterms").val(ui.item.value);
                    this.form.submit();
                }
            })
            .data("ui-autocomplete")._renderItem = function(ul, item) {
                var t = item.label;
                //html encode
                t = htmlEncode(t);
                return $("<li></li>")
                    .data("item.autocomplete", item)
                    .append("<a>" + t + "</a>")
                .appendTo(ul);
            };
        },
        render: function () {
            this.loadControls();
            return this;
        }
    });

    return SearchView;
});