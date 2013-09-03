$("#fly_from").autocomplete({
    source: "cercavoli/xhr.ashx?task=fly_from&fly_to_val=" + $("#fly_to_code").val(),
    minChars: 0,
    select: function (event, ui) {
        $("#fly_from_code").val(ui.item.value);
        $(this).val(ui.item.label);
        return false;
    }
}).click(function () {
    val = $(this).val();
    if (val == "") val = "a"
    $(this).autocomplete("search", val);
});    ;
$("#fly_to").autocomplete({
    source: "cercavoli/xhr.ashx?task=fly_to&fly_from_val=" + $("#fly_from_code").val(),
    minChars: 0,
    select: function (event, ui) {
        $("#fly_to_code").val(ui.item.value);
        $(this).val(ui.item.label);
        return false;
    }
}).click(function () {
    val = $(this).val();
    if (val == "") val = "a"
    $(this).autocomplete("search", val);
});  ;
options = $.datepicker.regional["it"];
options["minDate"] = "+1";
options["dateFormat"] = 'dd MM yy';
$("#fly_outward").datepicker(options);
$("#fly_return").datepicker(options);

