define(['jquery', 'underscore', 'baseView', 'tuils/views/common/backTopView'],
    function ($, _, BaseView, BackTopView) {
        var FooterView = BaseView.extend({
            viewBackTop :undefined,
            events: {
                'click .footer h3' : 'showFooter'
            },
            initialize: function () {
                this.render();
            },
            loadControls: function () {
                this.$('.footer div > h3').append('<span class="icon-mas"></span>');
                this.viewBackTop = new BackTopView();
            },
            showFooter: function (obj) {
                obj = obj.target;
                if ($(obj).find('span').attr('class') == 'icon-menos') {
                    $(obj).find('span').addClass('icon-mas').removeClass('icon-menos').parents('.footer-menu').find('ul.footer-list').slideToggle();
                }
                else {
                    $(obj).find('span').addClass('icon-menos').removeClass('icon-mas').parents('.footer-menu').find('ul.footer-list').slideToggle();
                }
            },
            render: function () {
                
                this.loadControls();
                return this;
            }
        });
        return FooterView;
    });


