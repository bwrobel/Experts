var chat = {
    onNewMessage: [],
    onChatChange: [],
    onNewChat: [],
    onRefreshChatList: [],
    onEmptyList: [],
    currentChatId: 0,
    initialized: false,
    checkMessageRunning: false,

    initChat: function (username, dateFormat, lastMessageTimestamp, isModerator, isViewOnly, moderatorCloseMessage, isOwner, checkForMessages) {
        if (chat.initialized) {
            return;
        }

        chat.initialized = true;

        var chatRefreshInterval = 5000;

        var chatDiv = $('#chat');
        var history = chatDiv.find('.history');
        var newMessageTextarea = chatDiv.find('.new-message textarea');
        var sendButton = $('#send-message');
        var finishButton = $('#finish-chat');
        var closeButtons = $('.chat-close-buttons');
        var closeButton = $('#close-chat');
        var statusLabel = chatDiv.find('.label');
        var closeWithoutConfirmationButton = $('#close-chat-without-confirmation');
        var currentChatButtons = $('.current-chat-buttons');
        var openCurrentButton = $('#open-current');

        var isFinished = isViewOnly;

        var scrollDown = function () {
            history.prop({ scrollTop: history.prop("scrollHeight") });
        };

        var addMessageToHistory = function (name, text, date, isUnread, context) {
            if (!date) {
                date = moment().format(dateFormat.replace(/y/g, 'Y').replace(/d/g, 'D'));
            }

            var template = history.find('.message-template').clone();

            template.find('h5').html(name);
            template.find('i').html(date);
            template.find('p').html(text);

            if (isUnread) {
                template.find('p').addClass('unread');
            }

            template.removeClass('message-template');
            template.addClass('message');

            if (isModerator) {
                template.popover({ content: context, placement: 'right' });
            }

            history.find('.finish-message').before(template);

            scrollDown();
        };

        var clearHistory = function () {
            history.find('.message').remove();
        };

        var markChatMessagesRead = function () {
            if (!isOwner) {
                return;
            }

            var url = '/Chat/MarkChatMessagesRead';
            if (chat.currentChatId) {
                url += '/' + chat.currentChatId;
            }

            $.get(url);

            history.find('.unread').removeClass('unread');
        };

        var sendMessage = function (message) {

            if (!chat.checkMessageRunning)
                getRecentMessages(false, false);

            var data = { text: message };
            if (isModerator && chat.currentChatId)
                data.chatId = chat.currentChatId;

            $.post('/Chat/SendMessage', data, function () {
                chat.refreshChatList(false);
            }).error(function () {
                sendMessage(message);
            });
        };

        var getRecentMessages = function (singleCall, includeOwnMessages) {
            if (isFinished) {
                return;
            }

            if (!singleCall)
                chat.checkMessageRunning = true;

            var data = {};

            if (includeOwnMessages) {
                data.includeOwnMessages = true;
            }
            if (chat.currentChatId) {
                data.chatId = chat.currentChatId;
            }
            if (lastMessageTimestamp) {
                data.lastMessageTimestamp = lastMessageTimestamp;
            }

            $.get('/Chat/GetRecentMessages', data, function (messages) {
                if (messages && messages.length > 0) {
                    messages.forEach(function (message) {
                        var isRead = !isOwner || message.IsRead;
                        if (message.Timestamp > lastMessageTimestamp) {
                            lastMessageTimestamp = message.Timestamp;
                            addMessageToHistory(message.AuthorName, message.Text, message.CreationDate, !isRead, message.Context);
                        }
                    });

                    chat.onNewMessage.forEach(function (callback) {
                        callback();
                    });
                }
            }).complete(function () {
                if (!singleCall) {
                    setTimeout(getRecentMessages, chatRefreshInterval);
                }
            });
        };

        var addMessage = function () {
            var message = newMessageTextarea.val();
            if (message == '') {
                return;
            }

            if (isFinished || chat.noCurrentChat) {
                chat.refreshChatList(false, 1);

                clearHistory();
                history.find('.finish-message').hide();

                isFinished = false;
                chat.noCurrentChat = false;

                chat.setChatFrameState(true);
            }

            newMessageTextarea.val('');

            addMessageToHistory(username, message);
            sendMessage(message);
            markChatMessagesRead();

            history.find('.unread').removeClass('unread');
        };

        var finish = function () {
            if (isFinished) {
                return;
            }

            isFinished = true;

            history.find('.finish-message').show();
            scrollDown();

            chat.noCurrentChat = true;

            $.get('/Chat/FinishChat', function () {
                chat.refreshChatList(false);
            });
        };

        var close = function (withConfirmation) {
            setChatState(isModerator, true);

            var data = {
                isSummarySent: withConfirmation,
                chatId: chat.currentChatId
            };

            $.post('/Chat/CloseChat', data, function () {
                addMessageToHistory(username, moderatorCloseMessage);
                chat.refreshChatList(false);
            });
        };

        var onTextareaKeyPress = function (e) {
            if (e.which == 13) {
                addMessage();
                return false;
            }

            return true;
        };

        var setChatState = function (moderator, viewOnly) {
            var extendedClass = 'extended';

            if (viewOnly) {
                sendButton.hide();
                finishButton.hide();
                closeButtons.hide();
                newMessageTextarea.hide();
                statusLabel.hide();

                if (isModerator) {
                    currentChatButtons.hide();
                } else {
                    currentChatButtons.show();
                    openCurrentButton.toggle(!chat.noCurrentChat);
                }

                history.addClass(extendedClass);
            } else {
                sendButton.show();
                newMessageTextarea.show();
                currentChatButtons.hide();

                if (moderator) {
                    closeButtons.show();
                    finishButton.hide();
                    statusLabel.hide();
                } else {
                    finishButton.show();
                    closeButtons.hide();
                    statusLabel.show();
                }

                history.removeClass(extendedClass);
            }
        };

        if (isModerator) {
            closeButton.click(function () {
                close(true);
            });

            closeWithoutConfirmationButton.click(function () {
                close(false);
            });
        } else {
            finishButton.click(finish);
        }

        setChatState(isModerator, isViewOnly);

        newMessageTextarea.keypress(onTextareaKeyPress);
        sendButton.click(addMessage);

        newMessageTextarea.click(function () {
            markChatMessagesRead();
        });

        chat.onChatChange.push(function (newChatId, forceViewOnly) {
            chat.currentChatId = newChatId;
            lastMessageTimestamp = 0;
            clearHistory();
            isFinished = false;
            history.find('.finish-message').hide();

            getRecentMessages(true, true);

            setChatState(isModerator, isViewOnly || forceViewOnly);
        });

        chat.onNewChat.push(function () {
            chat.currentChatId = 0;
            lastMessageTimestamp = 0;
            clearHistory();
            isFinished = true;
            setChatState(false, false);

            $.get('/Chat/FinishChat');
        });

        chat.onEmptyList.push(function () {
            setChatState(isModerator, !isFinished);
        });

        if (checkForMessages)
            getRecentMessages(false, false);
    },

    setChatFrameState: function (opened, onSuccess) {
        $.get('/Chat/SetChatFrameState/' + opened, onSuccess);
    },

    initChatFrame: function (isOpen) {
        var blinkInterval = 500;

        var frame = $('#chat-frame');
        var content = frame.find('.content');
        var history = content.find('.history');
        var newMessageTextarea = content.find('.new-message textarea');
        var handle = frame.find('.handle');
        var close = content.find('.close');

        var isBlinking = false;

        var scrollDown = function () {
            history.prop({ scrollTop: history.prop("scrollHeight") });
        };

        var openChatFrame = function () {
            isBlinking = false;
            handle.hide();

            frame.addClass('width');

            content.show('slide', function () {
                newMessageTextarea.focus();
                scrollDown();
                chat.setChatFrameState(true);
            });
        };

        var blinkHandle = function () {
            if (!handle.is(":visible") || isBlinking) {
                return;
            }

            isBlinking = true;

            var id = setInterval(function () {
                handle.toggleClass("blink");
                if (!isBlinking) {
                    handle.removeClass("blink");
                    clearInterval(id);
                }
            }, blinkInterval);
        };

        var closeChatFrame = function () {
            content.hide('slide', function () {
                handle.show('drop');
            });

            chat.onNewMessage = [blinkHandle];

            chat.setChatFrameState(false);

            frame.removeClass('width');
        };

        handle.click(openChatFrame);
        close.click(closeChatFrame);

        if (isOpen) {
            openChatFrame();
        }
    },

    changeChat: function (chatId, forceViewOnly) {
        chat.onChatChange.forEach(function (callback) {
            callback(chatId, forceViewOnly);
        });
    },

    startNewChat: function () {
        chat.onNewChat.forEach(function (callback) {
            callback();
        });
    },

    refreshChatList: function (loadFirst, forcedPage) {
        chat.onRefreshChatList.forEach(function (callback) {
            callback(loadFirst, forcedPage);
        });
    },

    emptyList: function () {
        chat.onEmptyList.forEach(function (callback) {
            callback();
        });
    },

    initChatList: function (filter, page, loadFirst, initRefresh, isModerator, noCurrentChat) {
        var chatListRefreshInterval = 5000;
        var selectedClass = "selected";

        chat.noCurrentChat = noCurrentChat;

        var rows = $('#chat-list tbody tr').click(function () {
            if ($(this).hasClass(selectedClass)) {
                return false;
            }

            var chatId = $(this).data('chatid');

            if (!chatId) {
                return false;
            }

            $(this).siblings().removeClass(selectedClass);
            $(this).addClass(selectedClass);

            var isClosed = $(this).data('isclosed');
            chat.changeChat(chatId, isClosed == "True", chat.noCurrentChat);

            return false;
        });

        if (rows.length == 1 && !$(rows[0]).data('chatid')) {
            chat.emptyList();
        }

        if (loadFirst) {
            rows.first().click();
        } else {
            $('#chat-list tbody tr[data-chatid="' + chat.currentChatId + '"]').addClass(selectedClass);
        }

        var reinit = function (loadFirst) {
            chat.initChatList(filter, page, loadFirst, false, isModerator, chat.noCurrentChat);
            paging.initAjaxPaging('#chat-list .pagination', '#chat-list');
        };

        var refreshList = function (loadFirst, forcedPage) {
            if (forcedPage) {
                page = forcedPage;
            }

            var url = isModerator ? '/Chat/ModeratorChatList/' + filter + '?page=' + page : '/Chat/UserChatList?page=' + page;
            $.get(url, function (result) {
                $('#chat-list').html(result);
                reinit(loadFirst);
            });
        };

        if (initRefresh) {
            paging.onPageChange.push(function (newPage) {
                page = newPage;
                reinit(true);
            });

            chat.onRefreshChatList.push(refreshList);
            setInterval(refreshList, chatListRefreshInterval);
            chat.refreshChatList(true);
        }

        if (!isModerator) {
            var openCurrentButton = $('#open-current');
            var startNewButton = $('#start-new');

            openCurrentButton.click(function () {
                if (page == 1) {
                    rows.first().click();
                } else {
                    chat.refreshChatList(true, 1);
                }
            });

            startNewButton.click(function () {
                rows.removeClass(selectedClass);
                chat.startNewChat();
                chat.refreshChatList(false);
            });
        }
    },

    initEmailForm: function () {
        $('#chat').hide();

        $('#sign-in').click(function () {
            var href = $(this).attr('href');
            chat.setChatFrameState(false, function () {
                window.location.href = href;
            });

            return false;
        });

        $('#start-chat').click(function () {
            $('#email-form').hide();
            $('#chat').show();

            var email = $('#email-form #email').val();
            if (email) {
                $.get('/Chat/SetChatEmail/' + email);
            }
        });
    }
}