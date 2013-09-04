$("#departure_location_name").autocomplete({
    source: "cercavoli/xhr.ashx?task=departure_location_name&arrival_location_info=" + $("#arrival_location_info").val(),
    minChars: 0,
    select: function (event, ui) {
        $("#departure_location_info").val(ui.item.value);
        $(this).val(ui.item.label);
        return false;
    },
    change: function (event, ui) {
        if (!ui.item) {
            $(this).val("");
            $("#departure_location_info").val("");
        }
    }
}).click(function () {
    $(this).val("");
    $(this).autocomplete("search", "a");
});        ;
$("#arrival_location_name").autocomplete({
    source: "cercavoli/xhr.ashx?task=arrival_location_name&departure_location_info=" + $("#departure_location_info").val(),
    minChars: 0,
    select: function (event, ui) {
        $("#arrival_location_info").val(ui.item.value);
        $(this).val(ui.item.label);
        return false;
    },
    change: function (event, ui) {
        if (!ui.item) {
            $(this).val("");
            $("#departure_location_info").val("");
        }
    }
}).click(function () {
    $(this).val("");
    $(this).autocomplete("search", "a");
});
options = $.datepicker.regional["it"];
options["minDate"] = "+1";
options["dateFormat"] = 'dd MM yy';
$("#departure_datetime").datepicker(options);
$("#arrival_datetime").datepicker(options).change(function () {
    if ($(this).val() != "Sola andata")
        $("#reset_oneway").show();
    else
        $("#reset_oneway").hide();
});
$("#reset_oneway").click(function () {
    $(this).hide();
    $("#arrival_datetime").val("Sola andata");
});
$("#fly_search_form").submit(function () {
    $.ajax({
        url: "cercavoli/results.aspx",
        data: $(this).serialize(),
        success: function (data) {
            $("#fly_search_results").html(data);
        }
    })
    return false;
});