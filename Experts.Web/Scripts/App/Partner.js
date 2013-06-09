function experts_question(params) {
    var content = '';

    content += '<div class="asknuts-widget">';
    content += experts_inner.get_form(params);
    content += '</div>';

    var container = document.getElementById(params.container);
    container.innerHTML = content;
}

var experts_inner = {
    experts_domain: 'https://www.asknuts.com',

    get_form: function (params) {
        var result = this.get_form_begin();
        result += this.get_header();
        result += this.get_hidden('brokerId', params.brokerId);
        result += this.get_select(this.get_categories(), 'ThreadForm.CategoryId');
        result += this.get_textarea('ThreadForm.Content');
        result += this.get_hr();
        result += this.get_buttons(params);
        result += this.get_form_end();
        return result;
    },

    get_form_begin: function () {
        var action = this.experts_domain + '/sprecyzuj-pytanie';
        return '<form class="question-form" method="post" action="' + action + '">';
    },

    get_form_end: function () {
        return '</form>';
    },

    get_header: function () {
        var img_src = this.experts_domain + '/Content/images/logo-partners.jpg';
        var result = '<div class="asknuts-logo"><img src="' + img_src + '" width="180" height="41" /></div>';
        result += '<p><strong>Zadaj pytanie najlepszym ekspertom w swoich dziedzinach!</strong></p>';
        return result;
    },

    get_hidden: function (name, value) {
        return '<input type="hidden" name="' + name + '" value="' + value + '" />';
    },

    get_select: function (array, name) {
        var result = '<select name="' + name + '">';
        for (var i in array) {
            result += '<option value="' + (i == 0 ? '' : i) + '">' + array[i] + '</option>';
        }
        result += '</select>';
        return result;
    },

    get_textarea: function (name) {
        return '<textarea name="' + name + '" placeholder="wpisz treść pytania..."></textarea>';
    },

    get_buttons: function () {
        var result = '<div class="ask">';
        result += '<input type="submit" value="Poznaj odpowiedź" />';
        result += '</div>';
        return result;
    },

    get_categories: function () {
        var result = new Array();
        result[0] = '-- wybierz kategorię --';
        result[1] = 'Prawo i podatki';
        result[2] = 'Biznes i finanse';
        result[10] = 'Miłość i relacje';
        result[11] = 'Nauka';
        result[12] = 'Komputery i internet';
        result[17] = 'Zdrowie i Medycyna';
        result[3] = 'Zwierzęta i weterynaria';
        result[4] = 'Podróże';
        result[5] = 'Rozrywka i rekreacja';
        result[6] = 'Sztuka kulinarna';
        result[7] = 'Dzieci';
        result[8] = 'Wydarzenia i ceremonie';
        result[9] = 'Ja i o mnie';
        result[13] = 'Dom';
        result[14] = 'Sprzęt i naprawa';
        result[15] = 'Ogród';
        result[16] = 'Sztuka i kultura';
        result[18] = 'Motoryzacja';
        return result;
    },

    get_hr: function () {
        return '<hr/>';
    }
};