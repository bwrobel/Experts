var global = {
    initMeetAsknutsHint: function () {
        $('.meet-asknuts-hint .close').click(function () {
            $('.meet-asknuts-hint').hide();
            $.cookie('meet-asknuts-hint-closed', 1, { expires: 10000 });
        });
    },

    initLinks: function (confirmationMessage) {
        $('a.confirmable').click(function () {
            return confirm(confirmationMessage);
        });
    }
}