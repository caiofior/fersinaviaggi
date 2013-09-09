$("#fly_search_results div span,#fly_container h2,#fly_container #request_summary").corner();
function checkUpdates() {
    setTimeout(function () {
        $.ajax({
            dataType: "json",
            url: "xhr/xhr.ashx",
            data: {
                task:"background_search",
                request_id: request_id
            }
        });
    }, 5000);
};
checkUpdates();