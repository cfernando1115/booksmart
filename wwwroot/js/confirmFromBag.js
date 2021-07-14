
function confirmShipment(shipId, bookId) {
    const id = shipId === null
        ? ($('#id').val())
        : shipId;

    $.ajax({
        url: `${routeURL}/api/Shipment/ConfirmShipment/${id}`,
        type: 'GET',
        dataType: 'JSON',
        success: function (response) {
            if (response.status > 0) {
                $.notify(response.message, 'success');
                addToConfirmedShipments(bookId);
                $(`#${bookId}`).remove();
            }
            else {
                $.notify(response.message, 'error');
            }
        },
        error: function (xhr) {
            $.notify('Error', 'error');
        }
    })
}

function addToConfirmedShipments(bookId) {
    const shipmentHtml = $(`#text-${bookId}`).html();

    const confirmedHtml = `
        <div class="text-center col-sm-4 mt-5">${shipmentHtml}</div>
    `;

    $('#confirmed-shipments').append(confirmedHtml);
    $('#no-confirmed').remove();
}

if (sessionStorage.getItem('addToBagMessage') !== null) {
    $.notify(sessionStorage.getItem('addToBagMessage'), 'success');
    sessionStorage.removeItem('addToBagMessage');
}