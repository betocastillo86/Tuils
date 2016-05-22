define(['jquery', 'underscore', 'backbone', 'baseView'],
    function ($, _, Backbone, BaseView) {
    var TopMenuView = BaseView.extend({
        events: {
            'click .ico-register': 'register',
            'click .ico-login': 'login',
            'click .submenuResponsive': 'submenu'
        },

        $menu : undefined,

        initialize: function () {
        },
        register: function () {
            this.trigger('register');
        },
        login: function () {
            this.trigger('login');
        },
        submenu: function (obj) {
            if (window.innerWidth > 960)
                return true;

            var $obj = $(obj.currentTarget);

            var $container = $obj.next('ul:first');
            var isHidden = $container.is(':hidden');

            //Cambia el icono del menu
            if(isHidden)
            {
                $obj.find('span').removeClass('icon-down').addClass('icon-next');
                $container.removeClass('hideMenuOption');
            }
            else
            {
                $obj.find('span').addClass('icon-down').removeClass('icon-next');
                $container.addClass('hideMenuOption');
            }
            
            obj.stopPropagation();
            return false;
        },
        hide: function () {
            var $menu = this.$('ul:first');
            if (!$menu.hasClass('hide-nav-responsive'))
                $menu.addClass('hide-nav-responsive');
            else
                $menu.removeClass('hide-nav-responsive');
        },
        

    });

    return TopMenuView;
});