$(function () {
    $("button, input:submit, input:button, .button").button();
    SetupDatePicker();
    SetupLoadingDiv();
});

function SetupLoadingDiv() {
    $("#loader").bind("ajaxSend", function () {
        var rightMargin = ($(window).width() - $(this).width()) / 2;        //set the loader in center of window
        $(this).css('margin-right', rightMargin + 'px');
        $(this).show();
    }).bind("ajaxComplete", function () {
        $(this).hide();     //hide once ajax request is completed
    });

    $(window).scroll(function () {
        $("#loader")
              .stop()
              .animate({ "marginTop": ($(window).scrollTop()) + "px" }, "fast");
    });
}

function SetupDatePicker() {
    $('.date').datepicker({ dateFormat: 'dd M yy' });
}
