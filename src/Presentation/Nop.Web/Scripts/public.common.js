﻿/*
** nopCommerce custom js functions
*/

define(['jquery'], function ($) {
    window.OpenWindow = function(query, w, h, scroll) {
        var l = (screen.width - w) / 2;
        var t = (screen.height - h) / 2;

        winprops = 'resizable=0, height=' + h + ',width=' + w + ',top=' + t + ',left=' + l + 'w';
        if (scroll) winprops += ',scrollbars=1';
        var f = window.open(query, "_blank", winprops);
    }

    window.setLocation = function (url) {
        window.location.href = url;
    }

    window.displayAjaxLoading = function(display) {
        if (display) {
            //$('.ajax-loading-block-window').show();
            $("#divFullLoading").addClass('loading');
        }
        else {
            //$('.ajax-loading-block-window').hide('slow');
            $("#divFullLoading").removeClass('loading');
        }
    }

    window.displayPopupNotification = function(message, messagetype, modal) {
        //types: success, error
        var container;
        if (messagetype == 'success') {
            //success
            container = $('#dialog-notifications-success');
        }
        else if (messagetype == 'error') {
            //error
            container = $('#dialog-notifications-error');
        }
        else {
            //other
            container = $('#dialog-notifications-success');
        }

        //we do not encode displayed message
        var htmlcode = '';
        if ((typeof message) == 'string') {
            htmlcode = '<p>' + message + '</p>';
        } else {
            for (var i = 0; i < message.length; i++) {
                htmlcode = htmlcode + '<p>' + message[i] + '</p>';
            }
        }

        container.html(htmlcode);

        var isModal = (modal ? true : false);
        container.dialog({ modal: isModal });
    }


    var barNotificationTimeout;
    window.displayBarNotification = function(message, messagetype, timeout) {
        clearTimeout(barNotificationTimeout);

        //types: success, error
        var cssclass = 'success';
        if (messagetype == 'success') {
            cssclass = 'success';
        }
        else if (messagetype == 'error') {
            cssclass = 'error';
        }
        //remove previous CSS classes and notifications
        $('#bar-notification')
            .removeClass('success')
            .removeClass('error');
        $('#bar-notification .content').remove();

        //we do not encode displayed message

        //add new notifications
        var htmlcode = '';
        if ((typeof message) == 'string') {
            htmlcode = '<p class="content">' + message + '</p>';
        } else {
            for (var i = 0; i < message.length; i++) {
                htmlcode = htmlcode + '<p class="content">' + message[i] + '</p>';
            }
        }
        $('#bar-notification').append(htmlcode)
            .addClass(cssclass)
            .fadeIn('slow')
            .mouseenter(function () {
                clearTimeout(barNotificationTimeout);
            });

        $('#bar-notification .close').unbind('click').click(function () {
            $('#bar-notification').fadeOut('slow');
        });

        //timeout (if set)
        if (timeout > 0) {
            barNotificationTimeout = setTimeout(function () {
                $('#bar-notification').fadeOut('slow');
            }, timeout);
        }
    }

    window.htmlEncode = function(value) {
        return $('<div/>').text(value).html();
    }

    window.htmlDecode = function (value) {
        return $('<div/>').html(value).text();
    }

    $.fn.fixedDialog = function (options) {
        this.dialog(options).html('<div align="center"><img id="divLoadingback" src="/Content/loading_2x.gif" /></div>');
        $("[role='dialog']").css('position', 'fixed');
        return this;
    }

});

