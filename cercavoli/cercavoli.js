$("#departure_location_name").autocomplete({
    source: "cercavoli/xhr.ashx?task=departure_location_name&arrival_location_info=" + $("#arrival_location_info").val(),
    minChars: 0,
    select: function (event, ui) {
        $("#departure_location_info").val(ui.item.value);
        $(this).val(ui.item.label);
        return false;
    }
}).click(function () {
    val = $(this).val();
    if (val == "") val = "a"
    $(this).autocomplete("search", val);
});    ;
$("#arrival_location_name").autocomplete({
    source: "cercavoli/xhr.ashx?task=arrival_location_name&departure_location_info=" + $("#departure_location_info").val(),
    minChars: 0,
    select: function (event, ui) {
        $("#arrival_location_info").val(ui.item.value);
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
$("#fly_search_form").submit(function () {
    $.ajax({
        url: "cercavoli/results.aspx",
        data: $(this).serialize(),
        success: function (data) {
            $("#fly_search_results").html(data);
        }
    })
    return false;
})

