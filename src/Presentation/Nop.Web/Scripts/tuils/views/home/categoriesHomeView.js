define(['jquery', 'underscore', 'baseView', 'jqueryui'],
    function ($, _, BaseView) {
    
        var CategoriesHomeView = BaseView.extend({
            events: {
                'click .content-category-menu': 'showCategories'
            },
            initialize: function (args) {
            },
            showCategories: function (obj) {
                var parent = $(obj.currentTarget);
                obj = $(obj.target);
                
                var show = !parent.next().is(':visible');
                //obj.siblings('.tit_category_m.active').removeClass('active');
                parent.siblings('.content-category-menu').find('.tit_category_m.active')
                obj.toggleClass('active', show);
                $('.box_category_m').slideUp('normal');
                if (show)
                    parent.next().slideDown('normal');
            }
        });

        return CategoriesHomeView;
});