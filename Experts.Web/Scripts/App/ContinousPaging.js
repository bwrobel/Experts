var continousPaging = {
    requestData: {},
    currentPage: 1,
    lastPage: false,
    loading: false,
    initialize: function () {
        $(function () {
            $(window).scroll(function () {
                if (continousPaging.loading || continousPaging.lastPage)
                    return;

                if ($(window).scrollTop() + $(window).height() > $(document).height() - 300) {
                    continousPaging.loading = true;
                    continousPaging.loadPage();
                }

            });
        });
    },
    loadPage: function () {
        continousPaging.requestData.page = continousPaging.currentPage + 1;
        $.post(window.location.href, continousPaging.requestData, function (data) {
            $(".paged-list .loader").remove();
            $(".paged-list").append(data);
            continousPaging.currentPage++;
            continousPaging.loading = false;
            continousPaging.lastPage = $(".paged-list .loader").length == 0;
        });
    },
    reloadPage: function () {
        $(".paged-list").children().remove();
        continousPaging.currentPage = 0;
        continousPaging.loadPage();
    }
};