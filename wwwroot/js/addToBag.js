const routeURL = location.protocol + '//' + location.host;

function addBookToBag(bookId) {
    try {
        $.ajax({
            url: `${routeURL}/api/Member/AddToBag?bookId=${bookId}`,
            type: 'POST',
            success: function (response) {
                if (response.status === 0) {
                    $.notify(response.message, 'success');
                }
                else {
                    $.notify(response.message, 'success');
                    location.href = `${routeURL}/Member/Bag`;
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

