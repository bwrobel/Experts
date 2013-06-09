var tinyForm = {

    initialize: function () {
        $(function () {
            $('#question-form-tiny').find('textarea').keyup();
            $('#question-form-tiny .error-message').hide();

            tinyForm.toggleAttachmentTinyListSummary(false);
        });
    },

    toggleCategories: function () {
        $(".error-message.no-category").hide();
        var categorySelectList = $("#question-form-tiny .category-select");
        categorySelectList.toggle(200, function () {
            tinyForm.scrollToShowWholeElement(categorySelectList);
        });
    },

    scrollToShowWholeElement: function (el) {
        var elBottom = el.offset().top + el.height();
        var newScrollTop = elBottom + 30 - $(window).height();

        if ($(document.body).scrollTop() < newScrollTop)
            $(document.body).animate({ 'scrollTop': newScrollTop }, 300);
    },

    selectCategory: function (categoryName, categoryId) {
        $("#question-form-tiny .no-category-selected").hide();
        $("#question-form-tiny .category-selected").show();
        $("#question-form-tiny .category-selected strong").text(categoryName);

        $("#question-form-tiny .category-select").hide(200);
        $("#category-id").val(categoryId);
    },

    toggleAttachments: function () {
        var attachmentsList = $("#question-form-tiny .attachment-list");

        attachmentsList.toggle(200, function () {
            tinyForm.scrollToShowWholeElement(attachmentsList);
        });
    },

    showAttachments: function (temporaryAttachmentFolder, isTiny) {
        thread.GetAttachmentsTiles(temporaryAttachmentFolder, isTiny);
        var attachmentsList = $("#question-form-tiny .attachment-list");
        attachmentsList.show(200, function () {
            tinyForm.scrollToShowWholeElement(attachmentsList);
        });
    },

    hideListsAndErrors: function () {
        $('#question-form-tiny .error-message').hide();
        $("#question-form-tiny .category-select").hide(200);
        $("#question-form-tiny .attachment-list").hide(200);
    },

    validateAndSubmit: function () {
        var errors = false;

        if ($("#question-form-tiny textarea").val().trim().length == 0) {
            errors = true;
            $("#question-form-tiny .no-text").show();
        }

        if ($("#question-form-tiny #category-id").val() == "") {
            errors = true;
            $("#question-form-tiny .no-category").show();
        }

        if (!errors)
            $("#question-form-tiny form").submit();
    },

    toggleAttachmentTinyListSummary: function (show) {
        if (show == true) {
            $('#attachment-tiny-list-summary').show();
            $(".tiny-attachment-upload").css('margin-top', '-18px');
        }
        if (show == false) {
            $('#attachment-tiny-list-summary').hide();
            $(".tiny-attachment-upload").css('margin-top', '0px');
        }
    }
};