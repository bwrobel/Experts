$(function () {
    $("select").selectbox("attach");

    $(".sbHolder").each(function () {
        $(this).width($(this).prev().width());
    });

    $(".sbToggle").addClass("icon-caret-down");
});