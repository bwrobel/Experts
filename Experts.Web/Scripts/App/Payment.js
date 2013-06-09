var payment = {
    initPaymentForm: function () {
        $(function () {
            var signUp = $('#PaymentForm_SignUp');
            var onChange = function () {
                var editor = $('#password-editor');
                if (signUp.is(':checked')) {
                    editor.html($('#password-editor-template').html());
                } else {
                    editor.html('');
                }
            };

            onChange();
            signUp.click(onChange);
        });
    },

    channelSelected: function (channel, name, image) {
        setTimeout(function () {
            $('#providerChannelModal').modal('hide');
        }, 100);

        $('input[name="PaymentForm.SelectedChannel"]').val(channel);

        $('#channel-preview').attr('src', image);
        $('#channel-preview').show();
        $('a#select-channel span').html(name);
        $('.channel-select label').addClass('space-top');
    }
}