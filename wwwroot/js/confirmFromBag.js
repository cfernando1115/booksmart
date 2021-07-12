function confirmShipment(idInput = null) {

    const id = idInput === null
        ? +($('#id').val())
        : +idInput;

    $.ajax({
        url: `${routeURL}/api/Shipment/ConfirmShipment/${id}`,
        type: 'GET',
        dataType: 'JSON',
        success: function (response) {
            if (response.status > 0) {
                $.notify(response.message, 'success');
                location.href = `${routeURL}/Member/Bag`;
                closeModal();
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