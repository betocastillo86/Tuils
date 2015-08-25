define(['jquery', 'underscore', 'baseView', 'jqueryui'],
    function ($, _, BaseView) {
    
        var CategoriesHomeView = BaseView.extend({
            events: {
                'click .tit_category_m' : 'showCategories'
            },
            initialize: function (args) {
            },
            showCategories: function (obj) {
                obj = $(obj.target);

                var show = !obj.next().is(':visible');
                obj.siblings('.tit_category_m.active').removeClass('active');
                obj.toggleClass('active', show);
                $('.box_category_m').slideUp('normal');
                if (show)
                    obj.next().slideDown('normal');
            }
        });

        return CategoriesHomeView;
});