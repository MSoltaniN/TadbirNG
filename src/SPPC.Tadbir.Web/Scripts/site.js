// Initialize all client-side logic using jQuery...
$(function () {
    $('[href*="/delete"]').click(confirmDelete);
    $('a[id*="action"]').click(promptForParaph);
    $('form[name="selectForm"]').submit(handleGroupParaph);

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
 * promptForParaph
 *   Prompts user to enter an optional paraph (comment/remark) for a workflow action.
 *   The prompt mechanism is the browser input dialog ('window.prompt()' or just 'prompt()')
 *   and it redirects browser to proper page to complete the action. The paraph value entered
 *   by the user will be appended to request URL as a query string parameter.
 * Argument(s)
 *   e : Javascript event object passed to the event handler.
 */
function promptForParaph(e) {
    e.preventDefault();
    var paraph = prompt("لطفا توضیحات یا پاراف مورد نظر خود را وارد کنید (اختیاری) :");
    if(paraph != null) {
        var targetUrl = $(this).attr('href');
        if(paraph != "") {
            targetUrl = targetUrl.indexOf('?') != -1
                ? (targetUrl + '&paraph=' + paraph)
                : (targetUrl + '?paraph=' + paraph);
            targetUrl = encodeURI(targetUrl);
        }
        
        window.location.href = targetUrl;
    } 
}

/*
 * handleGroupParaph
 *   Usage : Intended to be used when item selection is enabled in a data list.
 *   Prompts user to enter an optional paraph (comment/remark) for a workflow group action.
 *   The prompt mechanism is the browser input dialog ('window.prompt()' or just 'prompt()')
 *   and it redirects browser to proper page to complete the action. The paraph value entered
 *   by the user will be set in a hidden form field reserved for paraph.
 * Argument(s)
 *   e : Javascript event object passed to the event handler.
 */
function handleGroupParaph(e) {
    var selectedCount = $('input[type="checkbox"][name*="IsSelected"]:checked').length;
    if (selectedCount == 0) {
        alert("سندی انتخاب نشده است.");
        e.preventDefault();
        return;
    }

    var paraph = prompt("لطفا توضیحات یا پاراف مورد نظر خود را وارد کنید (اختیاری) :");
    if(paraph != null) {
        var form = this;
        form.elements.namedItem('paraph').value = paraph;
        form.submit();
    } else {
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
    var dates = $('[id*=Date]');
    if (dates.length > 0) {
        dates.MdPersianDateTimePicker({
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
