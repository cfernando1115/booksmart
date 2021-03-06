﻿
function addBookToBag(bookId) {
    try {
        $.ajax({
            url: `${routeURL}/api/Member/AddToBag?bookId=${bookId}`,
            type: 'POST',
            success: function (response) {
                if (response.status === 0) {
                    $.notify(response.message, 'info');
                }
                else {
                    sessionStorage.setItem('addToBagMessage', response.message);
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

