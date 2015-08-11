define(['jquery', 'underscore', 'baseView'],
    function ($, _, BaseView) {
        var FooterView = BaseView.extend({

            events: {
                'click .footer h3' : 'showFooter'
            },
            initialize: function () {
                this.render();
            },
            loadControls: function () {
                this.$('.footer div > h3').append('<span class="fontawesome-plus"></span>');
            },
            showFooter: function (obj) {
                obj = obj.target;
                if ($(obj).find('span').attr('class') == 'fontawesome-minus') {
                    $(obj).find('span').addClass('fontawesome-plus').removeClass('fontawesome-minus').parents('.footer-menu').find('ul.footer-list').slideToggle();
                }
                else {
                    $(obj).find('span').addClass('fontawesome-minus').removeClass('fontawesome-plus').parents('.footer-menu').find('ul.footer-list').slideToggle();
                }
            },
            render: function () {
                this.loadControls();
                return this;
            }
        });
        return FooterView;
    });


