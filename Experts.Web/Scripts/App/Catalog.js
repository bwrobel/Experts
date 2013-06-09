var catalog = {
    refreshSubAttributes: function (el, attributeId) {
        var input = $(el);
        var form = input.parents("form");

        $.post("/Catalog/ChildCategoryAttributes/",
               "attributeId=" + attributeId + "&" + form.serialize(),
               function (result) {
                   form.find(".sub-attributes[data-attribute-id=" + attributeId + "]").html(result);
               });

    }

}