$(document).delegate('#pageContainer', 'pageinit', function () {
    $.mobile.changePage("Pages/Start.html", { showLoadMsg: false, transition: "none" });
});