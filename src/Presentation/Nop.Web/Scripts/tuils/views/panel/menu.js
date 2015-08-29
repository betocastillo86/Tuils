define(['jquery', 'underscore', 'baseView'], function ($, _, BaseView) {
    var MenuPanelView = BaseView.extend({

        viewLogin: undefined,
        viewRegister: undefined,
        //responsiveMenuOptions :{
        //    // options
        //    extensions: ["theme-dark"],
        //    dividers: {
        //        fixed: true
        //    },
        //    navbar :{
        //        title : 'Panel de Control'
        //    },
        //    navbars: [
        //        {
        //            position: 'bottom',
        //            content: [$("#templateLogoutResponsiveMenu").html()]
        //        }
        //    ]
        //},
        $menu : undefined,
        //events: {
        //    'click .nav-with-sub': 'openMenu'
        //},
        initialize: function (args) {
            //this.handleResize();
            //this.on("window-resized-max", this.hideMenuResponsive, this);
            //this.on("window-resized-min", this.showMenuResponsive, this);
            //this.loadResponsiveMenu();
        },
        //openMenu : function(ev)
        //{
        //    $(ev.target).parent().find("ul").show();
        //},
        showMenuResponsive: function () {
            this.loadResponsiveMenu();
        },
        hideMenuResponsive: function () {
            //if (this.$menu)
            //    $("#mainMenu").data("mmenu").close();
        },
        loadResponsiveMenu: function () {
            ////Para poder crear el menu, no debe existir previamente
            //if (!this.$menu && this.isMinSize()) {
            //    ////Se clona el menu y se carga dinámicamente
            //    this.$menu = this.$("#mainMenu").clone();
            //    this.$menu.attr("id", "my-mobile-menu");
            //    this.$menu.mmenu(this.responsiveMenuOptions);

            //    this.$("#mainMenu").mmenu(this.responsiveMenuOptions, {
            //        clone: true
            //    });

            //    //Deja activas todas las opciones de menu
            //    $(".mm-panel .sub-menu-panel").show();
            //}
        },
        render: function () {
            return this;
        }
    });
    return MenuPanelView;
});