let routeURL = location.protocol + '//' + location.host;

function removeBookFromBag(bookId) {
    try {
        $.ajax({
            url: `${routeURL}/api/Member/RemoveFromBag?bookId=${bookId}`,
            type: 'POST',
            success: function (response) {
                if (response.status === 0) {
                    $.notify(response.message, 'success');
                }
                else {
                    $.notify(response.message, 'success');
                    $(`#${bookId}`).remove();
                }
            },
            failure: function (response) {
                console.log(response);
                $.notify(response.message, 'error');
            }
        });
    } catch (ex) {
        $.notify(response.message, 'error');
    }

}