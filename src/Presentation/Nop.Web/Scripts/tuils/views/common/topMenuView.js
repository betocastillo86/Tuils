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
            var $obj = $(obj.currentTarget);

            var $container = $obj.next('ul:first');
            var isHidden = $container.is(':hidden');

            $container.css('display', isHidden ? '' : 'none');
            
            //Cambia el icono del menu
            if(isHidden)
            {
                $obj.find('span').removeClass('icon-down').addClass('icon-next');
            }
            else
            {
                $obj.find('span').addClass('icon-down').removeClass('icon-next');
            }
            
            obj.stopPropagation();
            return false;
        }
        

    });

    return TopMenuView;
});