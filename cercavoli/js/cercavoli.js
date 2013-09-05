$("#departure_location_name").autocomplete({
    source: "xhr/xhr.ashx?task=departure_location_name&arrival_location_info=" + $("#arrival_location_info").val(),
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
    source: "xhr/xhr.ashx?task=arrival_location_name&departure_location_info=" + $("#departure_location_info").val(),
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
    $("#fly_search_form span.error").hide();
    var status = true;
    if ($("#departure_location_info").val() == "") {
        $(".departure_location_name_error").show();
        status = false;
    }
    if ($("#arrival_location_info").val() == "") {
        $(".arrival_location_name_error").show();
        status = false;
    }
    if ($("#departure_datetime").val() == "") {
        $(".departure_datetime_error").show();
        status = false;
    }
    return status;
});
$("#fly_search,#fly_search .error").corner();
$("#fly_search_results div span,#fly_container h2,#fly_container #request_summary").corner();