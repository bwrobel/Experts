var refreshState = true;
var allowValueChange = true;
var recommendFormBlock = false;
var allowAttachmentScrolling = false;

var thread = {
    categoryChangeListeners: [],

    addCategoryChangeListener: function (listener) {
        this.categoryChangeListeners.push(listener);
    },

    initFormValidation: function () {

        var validateAndSubmit = function () {
            var errors = false;

            if ($("#question_form textarea").val().trim().length == 0) {
                errors = true;
                $("#question_form .no-text").show();
            }

            if ($("#question_form #ThreadForm_CategoryId").val() == "") {
                errors = true;
                $("#question_form .no-category").show();
            }

            if (!errors) {
                $("#question_form form").submit();
            }
        };

        $('#question_form .button-border a').click(function () {
            validateAndSubmit();
            return false;
        });

        $('#question_form').click(function () {
            $('#question_form .error-message').hide();
        });
    },

    refreshThreadValue: function () {
        var threadValue = $('#thread_value');

        if (allowValueChange == true) {
            threadValue.html('...');
            $.post('/Thread/GetThreadValue',
                    $('form').serialize(),
                    function (result) {
                        threadValue.html(result);
                        $('#Value').val(result);
                        thread.checkFunds(result);
                    });
        }

        $.post('Thread/GetThreadVerbocity', $('form').serialize(),
            function (result) {
                if (result == "Low") {
                    $('.slider-captions .first').addClass('purple');
                    $('.slider-captions .second').removeClass('purple');
                    $('.slider-captions .last').removeClass('purple');
                }
                if (result == "Medium") {
                    $('.slider-captions .first').removeClass('purple');
                    $('.slider-captions .second').addClass('purple');
                    $('.slider-captions .last').removeClass('purple');
                }
                if (result == "High") {
                    $('.slider-captions .first').removeClass('purple');
                    $('.slider-captions .second').removeClass('purple');
                    $('.slider-captions .last').addClass('purple');
                }
            });
    },

    initThreadValueReceiver: function () {
        $(function () {
            var priority = $('#Priority');
            var verbosity = $('#Verbosity');

            priority.change(thread.refreshThreadValue);
            verbosity.change(thread.refreshThreadValue);
        });
    },

    checkFunds: function (value) {
        var paymentForm = $('#payment-form');
        var notEnoughFundsDivs = $('.not-enough-funds');
        var enoughFundsInfo = $('#enough-funds-info');

        var funds = paymentForm.data('funds');
        var enoughFunds = parseFloat(funds) >= parseFloat(value);

        notEnoughFundsDivs.toggle(!enoughFunds);

        if (enoughFunds) {
            paymentForm.attr('disabled', 'true');
        } else {
            paymentForm.removeAttr('disabled');
        }

        enoughFundsInfo.toggle(enoughFunds);
    },

    assignUserDefinedPriceProposal: function () {
        $(document).ready(function () {
            var userDefinedPricePropsal = $("#thread_value").text();
            $("#UserDefinedPrice").val(userDefinedPricePropsal);
        });
    },

    unlockPrice: function () {
        $(function () {
            allowValueChange = true;
            $("#lockIcon").removeClass("icon-lock");
            $("#CustomValue").val(null);
            thread.refreshThreadValue();
        });
    },

    initThreadCategoryDropdown: function (baseUrl, column, direction) {
        $(function () {
            $('#SelectedCategoryId').change(function () {
                window.location.href = baseUrl + "/" + $(this).val() + "?Column=" + column + "&Direction=" + direction;
            });
        });
    },

    initThreadCategoryAcceptedAnswerDropdown: function () {
        $(function () {
            $('#SelectedCategory_Id').change(function () {
                window.location.href = "/Thread/AcceptedQuestionList/" + $(this).val();
            });
        });
    },

    initThreadSanitizationStatusDropdown: function (baseUrl, column, direction) {
        $(function () {
            $('#SelectSanitizationStatus').change(function () {
                window.location.href = baseUrl + "/" + $(this).val() + "?Column=" + column + "&Direction=" + direction;
            });
        });
    },

    initCategoryExpertsOnlineInfo: function () {
        $(function () {
            thread.addCategoryChangeListener(function (categoryId) {
                $('.field-validation-error').html('');

                var info = $('#category-experts-online-info');
                info.html('');

                if (categoryId) {
                    $.get('/Thread/CategoryExpertsOnlineInfo/' + categoryId, null, function (result) {
                        info.html(result);
                    });
                }
            });
        });
    },

    initMultiExpertBox: function (expertIds, feedbacksVisible) {
        var i = 0;
        var count = expertIds.length;

        var getExpertInfo = function () {
            $.get("/Thread/ExpertInfo/" + expertIds[i] + "/" + feedbacksVisible, null, function (result) {
                if (expertIds.length <= 1) {
                    $('.pager').hide();
                }
                else {
                    $('.pager').show();
                }
                $('#expert-overview').html(result);
            });
        };

        $(function () {
            $('#previous-expert').click(function () {
                i = i - 1;
                if (i < 0)
                    i = count - 1;

                getExpertInfo();
            });

            $('#next-expert').click(function () {
                i = (i + 1) % count;
                getExpertInfo();
            });

            thread.addCategoryChangeListener(function (categoryId) {
                $.get("/Thread/MultiExpertBoxData/" + (categoryId ? categoryId : ""), null, function (result) {
                    expertIds = result;
                    count = expertIds.length;
                    i = 0;
                    getExpertInfo();
                });
            });
        });
    },

    initOpinions: function () {
        $(function () {
            $('#opinions').carousel({
                interval: 10000
            });

            thread.addCategoryChangeListener(function (categoryId) {
                var opinionFrames = $('.opinions');
                opinionFrames.each(function (i) {
                    var frame = $(opinionFrames[i]);

                    $.get("/Profile/Opinions/" + (categoryId ? categoryId : ""), null, function (result) {
                        frame.html(result);
                    });
                });
            });
        });
    },

    initCategoryDescription: function () {
        $(function () {
            thread.addCategoryChangeListener(function (categoryId) {
                $.get("/StaticPages/CategoryDescription/" + (categoryId ? categoryId : ""), null, function (result) {
                    $('#category-description').html(result);
                });
            });
        });
    },

    initQuestionBackground: function () {
        var changeQuestionBackground = function (imageName) {
            $('.question-background').css('background-image', 'Url(/Content/images/categories/' + imageName + '.jpg)');
        };

        $(function () {
            thread.addCategoryChangeListener(function (categoryId) {
                if (categoryId) {
                    changeQuestionBackground(categoryId);
                } else {
                    changeQuestionBackground('default');
                }
            });
        });
    },

    initCategoryChangeEvent: function () {
        $(function () {
            $('#ThreadForm_CategoryId').change(function () {
                var categoryId = $(this).val();
                thread.categoryChangeListeners.forEach(function (listener) {
                    listener(categoryId);
                });
            });
        });
    },

    RemoveAttachmentTile: function (id, temporaryAttachmentFolder, fileName) {
        var tile = document.getElementById(id);

        $.post("/Thread/DeleteAttachmentTile/", { fileName: fileName, temporaryAttachmentFolder: temporaryAttachmentFolder }, function (result) { });

        tile.remove();
        thread.GetTemporaryAttachmentsCount(temporaryAttachmentFolder);
    },

    GetAttachmentsTiles: function (temporaryAttachmentFolder, isTiny) {
        $.get("/Thread/AttachmentTiles/" + temporaryAttachmentFolder + "/" + isTiny, null, function (result) {
            $('#attachment-tiny-list').html(result);
            $(".last-attachment").css('opacity', '0');
            $(".last-attachment").animate({ "opacity": "1" }, 1000);
        });
    },

    GetTemporaryAttachmentsCount: function (temporaryAttachmentFolder) {
        $.post("/Thread/GetTemporaryAttachmentsCount/", { temporaryAttachmentFolder: temporaryAttachmentFolder }, function (result) {
            if (result == 0) {
                tinyForm.toggleAttachmentTinyListSummary(false);
            }
            else {
                $('#attachment-tiny-list-count').text(result);
            }
        });
    },

    initAllPosts: function (threadId, threadLastModDate, threadIntState, isModerator) {

        $(function () {
            setInterval(checkDate, 3000);
        });

        var getAllPosts = function () {
            $.get("/Thread/GetAllPosts/" + threadId, null, function (result) {
                $('#allposts').html(result);
                if (allowAttachmentScrolling) {
                    $('html, body').animate({ scrollTop: $('#last-attachment').offset().top }, 1400, "easeOutQuint");
                    $("#last-attachment").css('opacity', '0');
                    $("#last-attachment").animate({ "opacity": "1" }, 3000);
                    allowAttachmentScrolling = false;
                }
            });
        };

        var getThreadDetailsMenu = function () {
            $.get("/Thread/GetThreadDetailsMenu/" + threadId + "/" + isModerator, null, function (result) {
                $('#menu').html(result);
            });
        };

        var getThreadDetailsByStatus = function () {
            $.post("/Thread/GetThreadDetailsByStatus/", { threadId: threadId, threadIntState: threadIntState }, function (result) {
                $('#PostForm_SubmitButton').removeAttr('disabled');
                if (result != 0) {
                    if ($('#PostForm_Content').val() == "" || $('#PostForm_Content').length == 0) {
                        window.location.reload();
                    }
                    else {
                        if ($('#releaseTimer').length > 0) {
                            $('#PostForm_SubmitButton').attr("disabled", "disabled");
                            $('#ExtendThreadLockTimeButton').hide();
                            $('#releaseTimer').hide();
                            $('#TimerInfo').append('<div><strong>Czas upłynął</strong>, ponownie przejmij pytanie by udzielić odpowiedzi.</div>');
                        }
                        getAllPosts();
                        getThreadDetailsMenu();
                    }
                }
                else {
                    getAllPosts();
                    getThreadDetailsMenu();
                    //getPriceProposalNotification();
                }
            });
        };


        var getPriceProposalNotification = function () {
            $.get("/Thread/GetPriceProposalNotification/" + threadId, null, function (result) {
                $('#priceproposal').html(result);
            });
        };

        var checkDate = function () {
            $.post("/Thread/DateDifference", { threadId: threadId, threadLastModDate: threadLastModDate }, function (result) {
                if (!result == 0 && refreshState == true) {
                    threadLastModDate = result;
                    getThreadDetailsByStatus();
                }
            });
        };
    },

    PasteTextFromPostForm: function () {
        $(function () {
            if (localStorage.answerFormText != "undefined") {
                $("#PostForm_Content").val(localStorage.answerFormText);
                localStorage.answerFormText = "";
            }
        });
    },

    CopyTextFromPostForm: function () {
        $(function () {
            localStorage.answerFormText = $("#PostForm_Content").val();
        });
    },

    ExtendOccupyLockTime: function (threadId) {
        $(function () {
            $.get("/Thread/ExtendOccupyThreadLockTime/" + threadId, null, function (result) {
                $('#releaseTimer').html(result);
            });
        });
    },

    FocusTextArea: function (option) {
        $("#PostForm_Content").focus();
        $(".sbSelector").text(option);
    },

    initAuthenticationTypeSelect: function (isAuthenticated) {
        $(function () {
            if (!isAuthenticated) {
                $('#options-form input[type=submit]').click(function () {
                    $('#issueModal').modal();
                    return false;
                });
            }
        });
    },

    postEditShowEditor: function (el) {
        refreshState = false;

        var container = $(el).parents(".post");
        var postText = container.find(".post-text");
        var postEditor = container.find(".post-editor");
        var editButton = container.find(".edit-button");

        var postHeight = postText.css('height');
        var postTextEditor = container.find(".post-text-editor");
        postTextEditor.css("height", postHeight);

        editButton.hide();
        postText.hide();
        postEditor.show();

        // remove text selection (doubleckick hack)
        if (document.selection && document.selection.empty) {
            document.selection.empty();
        } else if (window.getSelection) {
            var sel = window.getSelection();
            if (sel && sel.removeAllRanges)
                sel.removeAllRanges();
        }
    },

    postEditSave: function (el, onSave, postId) {
        var container = $(el).parents(".post");
        var postText = container.find(".post-text");
        var postEditor = container.find(".post-editor");
        var editButton = container.find(".edit-button");
        var postTextArea = postEditor.find("textarea");

        var newText = postTextArea.val();
        postText.html(newText);

        onSave(newText, container, postId);

        editButton.show();
        postText.show();
        postEditor.hide();

        refreshState = true;
    },

    sanitizationUpdatePost: function (newText, container, postId) {
        $.post("/Thread/SanitizationUpdatePost", { postId: postId, postPublicContent: newText }, function (result) {
            container.find(".post-text").html(result);
            container.find(".post-text").css("display", "initial");
        });
    },

    updatePost: function (newText, container, postId) {
        $.post("/Thread/UpdatePost", { postId: postId, postContent: newText }, function (result) {
            container.find(".modify-date").html(result);
            container.find(".modify-date-container").show();
        });
    },

    updateQuestion: function (newText) {
        $('#ThreadForm_Content').val(newText);
    },

    postEditCancel: function (el) {
        var container = $(el).parents(".post");
        var postText = container.find(".post-text");
        var postEditor = container.find(".post-editor");
        var editButton = container.find(".edit-button");
        var postTextArea = postEditor.find("textarea");

        var oldText = $.trim(postText.html());
        postTextArea.val(oldText);

        editButton.show();
        postText.show();
        postEditor.hide();

        refreshState = true;
    },

    sanitizationShowPublicOriginal: function (el, showOriginal) {
        var container = $(el).parents(".well");
        var originalContent = container.find(".original-content");
        var publicContent = container.find(".public-content");

        var originalTab = container.find(".nav-tabs .original");
        var publicTab = container.find(".nav-tabs .public");

        originalContent.css("display", showOriginal ? "block" : "none");
        publicContent.css("display", showOriginal ? "none" : "block");

        if (showOriginal) {
            originalTab.addClass("active");
            publicTab.removeClass("active");
        } else {
            publicTab.addClass("active");
            originalTab.removeClass("active");
        }
    },

    sanitzationInitializeStatusBox: function (threadId, sanitizationStatus) {

        $(function () {
            thread.sanitizationSetStatusBoxColor(sanitizationStatus);

            $(".status-box").find("select").change(function () {
                thread.sanitizationSetStatus(this, threadId);
            });
        });
    },

    sanitizationSetStatusBoxColor: function (sanitizationStatus) {

        var statusBox = $(".status-box");

        statusBox.removeClass("alert-success");
        statusBox.removeClass("alert-error");

        if (sanitizationStatus == "Sanitized") {
            statusBox.addClass("alert-success");
        } else if (sanitizationStatus == "NotForPublic") {
            statusBox.addClass("alert-error");
        }
    },

    sanitizationSetStatus: function (el, threadId) {

        var sanitizationStatus = $(el).val();

        $.post("/Thread/SanitizationUpdateThread", { threadId: threadId, sanitizationStatus: sanitizationStatus });
        this.sanitizationSetStatusBoxColor(sanitizationStatus);
    },

    sanitizationToggleVisibility: function (el, postId) {
        var container = $(el).parents(".well");
        $.post("/Thread/TogglePostVisibility", { postId: postId }, function (result) {
            container.find(".toggole-visibility").html(result.linkLabel);
            if (result.visibility)
                container.removeClass("hidden-post");
            else
                container.addClass("hidden-post");
        });
    },

    refreshSubAttributes: function (el, attributeId) {
        var input = $(el);
        var form = input.parents("form");

        $.post("/Thread/ChildCategoryAttributes/",
               "attributeId=" + attributeId + "&" + form.serialize(),
               function (result) {
                   form.find(".sub-attributes[data-attribute-id=" + attributeId + "]").html(result);
               });

    },

    initCategorySelect: function () {
        var deselectPrimary = function () {
            var previousPrimary = $('#question_form .categories a.btn-primary');
            previousPrimary.removeClass('btn-primary');
            previousPrimary.addClass('btn-inverse');
        };

        var handleClick = function () {
            deselectPrimary();

            $(this).removeClass('btn-inverse');
            $(this).addClass('btn-primary');

            $('#ThreadForm_CategoryId').val($(this).data("categoryid"));

            if ($('#question_form .categories.more').is(':visible')) {
                $('#question_form .categories.more').hide('fade');
            }

            if ($(this).parent().is('.more')) {
                var newFirst = $(this);
                var first = $('#question_form .categories a').not('.more').first();
                var main = $('#question_form .main-category');
                var mainCategories = first.parent();

                main.after(first);
                newFirst.after(main);
                mainCategories.prepend(newFirst);
            }
        };

        $('#question_form .categories a').not('#more-categories').click(handleClick);

        $('#more-categories').click(function () {
            $('.categories.more').toggle('fade');
        });
    }
}