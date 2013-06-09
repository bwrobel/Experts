var editors = {
    initCheckboxes: function () {
        $('input[type="checkbox"]:not(.default-checkbox)').after('<span class="custom-checkbox"></span>');
    },

    initRadio: function () {
        $('input[type="radio"]:not(.default-radio)').after('<span class="custom-radio"></span>');
    },

    initTextareas: function () {
        $('textarea').each(function () { $(this).autoResize(); });
    },

    initDecimalEditor: function (decimalSeparator) {
        var allowedChars = new RegExp("[0-9" + decimalSeparator + "]");
        var valid = new RegExp("^(" +
                               "(0|[1-9][0-9]*)?" +
                               "([" + decimalSeparator + "][0-9]{0,2})?" +
                               ")?$");
        var oldValues = {};

        $(function () {
            var isValid = function (input) {
                var value = $(input).val();
                return valid.test(value);
            };

            var isAllowed = function (event) {
                if (event.ctrlKey) {
                    return true;
                }

                if (event.charCode == 0) {
                    return true;
                }

                var character = String.fromCharCode(event.which);
                return allowedChars.test(character);
            };

            $('input.decimal').bind('keypress', function (event) {
                if (isAllowed(event) && isValid(this)) {
                    oldValues[$(this).attr('id')] = $(this).val();
                } else {
                    event.preventDefault();
                }
            });

            $('input.decimal').bind('keyup', function () {
                if (!isValid(this)) {
                    $(this).val(oldValues[$(this).attr('id')]);
                }
            });
        });
    },

    initSlider: function (selectBoxSelector, min, max) {
        var select = $(selectBoxSelector);
        select.hide();

        select.siblings('.sbHolder').remove();

        $('<div></div>').insertAfter(select).slider({
            min: min,
            max: max,
            value: select[0].selectedIndex + 1,
            slide: function (event, ui) {
                select[0].selectedIndex = ui.value - 1;
                select.change();
            },
            animate: true
        });
    }
}