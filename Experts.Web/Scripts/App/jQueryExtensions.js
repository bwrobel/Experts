jQuery.showLoader = jQuery.fn.showLoader = function () {
    $(this).html("<div class='ajax-loader'></div>");
    return this;
};