$("#fly_from").autocomplete({

    source: "cercavoli_xhr.ashx?task=fly_from",
    select: function (event, ui) {
        console.log(ui.item);
        $("#fly_from_code").val(ui.item.value);
        $(this).val(ui.item.label);
        return false;
    }

});

