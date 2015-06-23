
define(['jquery', 'underscore', 'baseView','jqueryui', 'iView'],
    function ($, _, BaseView) {

    var HomeView = BaseView.extend({
        initialize: function (args) {
            this.loadSlider();
        },
        loadSlider : function()
        {
            $('#iview').iView({
                timerDiameter: 25, // Timer diameter
                animationSpeed: 500, // Slide transition speed
                pauseTime: 7000,
                pauseOnHover: true,
                keyboardNav: true, // Use left & right arrows
                touchNav: true // Use Touch swipe to change slides
            });
        },
        render: function ()
        {
            return this;
        }
    });

    return HomeView;
});



