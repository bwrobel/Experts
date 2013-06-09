var administration = {
    initCategoryAttributeEditor: function () {
        var onTypeChange = function () {
            var type = $('#Type').val();
            var optionsVisible = type == 'SingleSelect' || type == 'MultiSelect';
            $('#CategoryAttributeOptions').toggle(optionsVisible);
        };

        $('#Type').change(onTypeChange);

        onTypeChange();
    },

    initCategoryAttributeOptionsEditor: function () {
        var container = $('#CategoryAttributeOptions');

        container.find('form').submit(function (event) {
            event.preventDefault();

            var submit = $(this).find('input[type="submit"]');
            var ajaxLoader = $(this).find('.ajax-loader-small');

            submit.hide();
            ajaxLoader.css('display', 'inline-block');

            $.post($(this).attr('action'), $(this).serialize(), function (data) {
                container.find('form')[0].reset();
                container.find('#CategoryAttributeOptionsList').html(data);
                ajaxLoader.hide();
                submit.show();
            });
        });
    },

    initCategoryAttributeOptionsList: function (deleteConfirmationMessage) {
        var container = $('#CategoryAttributeOptions');

        container.find('.edit-option').click(function () {
            var row = $(this).parents('.div-row');
            row.find('.option-data').hide();
            row.find('form').show();
        });

        container.find('.delete-option').click(function () {
            var row = $(this).parents('.div-row');
            var confirmed = confirm(deleteConfirmationMessage);
            if (confirmed) {
                $(this).hide();
                row.find('.ajax-loader-small').css('display', 'inline-block');
                var id = row.find('input[name="Id"]').val();
                $.get('/Administration/DeleteCategoryAttributeOption/' + id, function () {
                    row.remove();
                });
            }
        });

        container.find('.option-form').submit(function (event) {
            event.preventDefault();

            $(this).find('input[type="submit"]').hide();
            $(this).find('.ajax-loader-small').css('display', 'inline-block');

            $.post($(this).attr('action'), $(this).serialize(), function (data) {
                container.find('#CategoryAttributeOptionsList').html(data);
            });

            return false;
        });
    }
};