// Initialize all client-side logic using jQuery...
$(function () {
    $('[href*="/delete/"]').click(confirmDelete);
});

/*
 * confirmDelete
 *   Provides a very simple client-side mechanism for cancelling a delete operation
 *   if not confirmed by the user.
 * Argument(s) :
 *   e : Javascript event object passed to the event handler.
 */
function confirmDelete(e) {
    var confirmed = confirm('آیا در مورد حذف این سطر اطلاعاتی اطمینان دارید؟');
    if (!confirmed) {
        e.preventDefault();
    }
}
