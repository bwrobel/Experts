var paging = {
    onPageChange: [],

    changePage: function (page) {
        paging.onPageChange.forEach(function (callback) {
            callback(page);
        });
    },

    initAjaxPaging: function (pagingAreaSelector, targetSelector) {
        $(pagingAreaSelector + ' a').click(function () {
            var href = $(this).attr('href');
            var pageMatch = /.*page=([\d]+)/.exec(href);
            var page = pageMatch ? pageMatch[1] : 1;

            $(targetSelector).load(href, function () {
                paging.changePage(page);
            });

            return false;
        });
    }
}