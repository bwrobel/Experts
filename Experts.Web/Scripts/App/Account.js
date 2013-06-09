var account = {
    refreshSubAttributes: function (el, attributeId) {
        var input = $(el);
        var form = input.parents("form");

        $.post("/Account/ChildCategoryAttributesPost/",
               "attributeId=" + attributeId + "&" + form.serialize(),
               function (result) {
                   form.find(".sub-attributes[data-attribute-id=" + attributeId + "]").html(result);
               });

    },

    initQuickSignIn: function () {
        $('#quick-sign-in form').submit(function () {
            $(this).find('input[type="submit"]').hide();
            $(this).find('.ajax-loader-small').css("display", "inline-block");

            $.ajax('/Account/QuickSignInForm', {
                data: $(this).serialize(),
                success: function (result) {
                    if (result.indexOf('top-menu') != -1) {
                        $('#top-menu').replaceWith(result);
                        if (account.reloadOnSignIn) {
                            window.location.reload(true);
                        }
                    } else {
                        $('#quick-sign-in').replaceWith(result);
                    }
                },
                type: 'POST',
                xhrFields: {
                    withCredentials: true
                }
            });

            return false;
        });
    },

    reloadOnSignIn: false,

    setReloadOnSignIn: function () {
        account.reloadOnSignIn = true;
    },

    initCategorySelect: function () {
        $('.category-select label').click(function () {
            var input = $(this).parent().find('input');

            var selected = !input.is(':checked');

            input.attr('checked', selected);
            $(this).parent().toggleClass('selected', selected);
        });
    }
}