// Initialize all client-side logic using jQuery...
$(function () {
    $('[href*="/delete/"]').click(confirmDelete);

    setupDatePickers();
    setupCaptcha();
});

/*
 * confirmDelete
 *   Provides a very simple client-side mechanism for cancelling a delete operation
 *   if not confirmed by the user.
 * Argument(s)
 *   e : Javascript event object passed to the event handler.
 */
function confirmDelete(e) {
    var confirmed = confirm('آیا در مورد حذف این سطر اطلاعاتی اطمینان دارید؟');
    if (!confirmed) {
        e.preventDefault();
    }
}

/*
 * setupDatePickers
 *   Prepares date picker components inside the page, if any.
 * Argument(s)
 *   none
 */
 function setupDatePickers() {
    $('[id*=Date]').MdPersianDateTimePicker({
        Placement: 'left',
        Trigger: 'click',
        EnableTimePicker: false,
        GroupId: '',
        ToDate: false,
        FromDate: false,
        DisableBeforeToday: false,
        Disabled: false,
        Format: 'yyyy/MM/dd',
        IsGregorian: false,
        EnglishNumber: true,
    });
}

/*
 * setupCaptcha
 *   Prepares event handlers for captcha challenge generation and initializes first challenge, if required.
 * Argument(s)
 *   none
 */
function setupCaptcha() {
    // Initialize captcha, if present...
    var captchaDiv = $("#captcha");
    if(captchaDiv)
        getNewChallenge();

    // Handle generation of a new captcha challenge...
    $('#new-captcha').on("click", null, function (event) {
        getNewChallenge();
        event.preventDefault();
    });
}

/*
 * getNewChallenge
 *   Sends an Ajax GET request to server, extracts required parameters from JSON response and replaces
 *   captcha image source and response hash value in hidden field.
 * Argument(s)
 *   none
 * Usage : Must be called whenever captcha challenge needs to be renewed.
 */
function getNewChallenge() {
    var captcha = $("#captcha");
    if (captcha.length == 1) {
        $.ajax({
            url: "https://babaksoft.com/api/bss/captcha/new",
            type: "GET",
            dataType: "jsonp",
            jsonp: "callback",
            crossDomain: true,
            success: function (json) {
                var img = $("#captcha-image");
                img.attr("src", json.imageSource);
                var inp = $("#captcha-response");
                inp.attr("value", json.response);
            },
            error: function (xhr, status) {
                alert("Ajax request failed.");
            }
        });
    }
}
