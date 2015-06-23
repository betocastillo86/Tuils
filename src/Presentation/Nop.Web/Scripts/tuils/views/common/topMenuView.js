define(['jquery', 'underscore', 'backbone', 'baseView', 'mmenu'],
    function ($, _, Backbone, BaseView) {
    var TopMenuView = BaseView.extend({

        responsiveMenuOptions :{
            // options
            extensions: ["theme-dark"],
            dividers: {
                fixed: true
            },
            navbar :{
                title : ''
            },
            navbars: [
                {
                    position: 'bottom',
                    content: [$("#templateLogoutResponsiveMenu").html()]
                }
            ]
        },

        $menu : undefined,

        initialize: function () {
            this.loadControls();
            this.handleResize();
            this.on("window-resized-max", this.hideMenuResponsive, this);
            this.on("window-resized-min", this.showMenuResponsive, this);
        },
        loadControls: function () {
            this.selectDefaultMenuOption();
            this.loadResponsiveMenu();
            this.hoverMenu();
        },
        selectDefaultMenuOption: function () {
            if (!this.isMobile())
            {
                //Solo muestra estas opciones si no es mobile
                var defaultOption = this.$("#SelectedSpecificationAttribute").val();
                this.$(".nav-menu .aFirstLevel").removeClass("active");
                this.$(".nav-menu li[data-id='" + defaultOption + "'] .aFirstLevel").addClass("active");
                this.$(".nav-menu .childrenOptions").hide();
                this.$(".nav-menu li[data-id='" + defaultOption + "'] .childrenOptions").show();
            }
        },
        showMenuResponsive : function(){
            this.loadResponsiveMenu();
        },
        hideMenuResponsive: function () {
            if(this.$menu)
                $("#mainMenu").data("mmenu").close();
        },
        loadResponsiveMenu: function () {
            //Para poder crear el menu, no debe existir previamente
            if (!this.$menu && this.isMinSize())
            {
                //Se clona el menu y se carga dinámicamente
                this.$menu = this.$("#mainMenu").clone();
                this.$menu.attr("id", "my-mobile-menu");
                this.$menu.mmenu(this.responsiveMenuOptions);

                this.$("#mainMenu").mmenu(this.responsiveMenuOptions, {
                    clone: true
                });

                //agrega las funcionalidades adicionales del menú
                $(".mm-panel:first").prepend($("#templateLinksResponsiveMenu").html());
            }
        },
        hoverMenu : function()
        {
            if(!this.isMobile())
            {
                var that = this;
                //Funcionalidad de mouse over para ocultar y mostrar los hijos del menú
                that.$(".nav-menu nav > ul > li").hover(
                    function () {
                        that.$(".nav-menu .childrenOptions").hide();
                        $(this).find(".childrenOptions").show();
                    },
                    function () {
                        that.selectDefaultMenuOption();
                    }
                );
            }
        }

    });

    return TopMenuView;
});