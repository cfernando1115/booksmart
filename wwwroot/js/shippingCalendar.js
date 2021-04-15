let routeURL = location.protocol + '//' + location.host;

$(document).ready(function () {
    initializeCalendar();

    $('#shipDate').kendoDatePicker({
        value: new Date(),
        dateInput: false
    });

});

let events = []

let calendar;

function initializeCalendar() {
    try {
        const calendarEl = document.getElementById('calendar');

        if (calendarEl !== null) {

            calendar = new FullCalendar.Calendar(calendarEl, {
                initialView: 'dayGridMonth',
                headerToolbar: {
                    left: 'prev, next, today',
                    center: 'title',
                    right: 'dayGridMonth, timeGridWeek, timeGridDay'
                },
                selectable: true,
                editable: false,
                select: function (event) {
                    openModal(event, null);
                },
                eventDisplay: 'block',
                events: function (fetchInfo, successCallback, failureCallback) {
                    $.ajax({
                        url: `${routeURL}/api/Shipment/GetShipmentDataByMember?memberId=${+$('#memberId').val()}`,
                        type: 'GET',
                        dataType: 'JSON',
                        success: function (response) {
                            events = [];
                            if (response.status === 1) {
                                $.each(response.data, function (i, data) {
                                    events.push({
                                        title: data.bookName,
                                        start: data.shipDate,
                                        bookId: data.bookId,
                                        shipDate: data.shipDate,
                                        backgroundColor: data.isConfirmed
                                            ? '#28a745'
                                            : '#dc3545',
                                        textColor: 'White',
                                        id: data.id,
                                        borderColor: 'White'
                                    });
                                });
                            }
                            successCallback(events);
                        },
                        error: function (xhr) {
                            $.notify('Error', 'error');
                        }
                    });
                },
                eventClick: function (info) {
                    getShipmentDetailsByEventId(info.event);
                }
            });
            calendar.render();
        }
    }
    catch (e) {
        alert(e);
    }
}

function submitForm() {
    if (checkValidation()) {
        const requestData = {
            Id: parseInt($('#id').val()),
            BookId: $('#bookId').val(),
            ShipDate: $('#shipDate').val(),
            MemberId: $('#memberId').val()
        };

        $.ajax({
            url: `${routeURL}/api/Shipment/SaveShipment`,
            type: 'POST',
            data: JSON.stringify(requestData),
            contentType: 'application/json',
            success: function (response) {
                if (response.status === 1 || response.status === 2) {
                    $.notify(response.message, 'success');
                    closeModal();
                    calendar.refetchEvents();
                }
                else {
                    $.notify(response.message, 'error');
                }
            },
            error: function (xhr) {
                $.notify('Error', 'error');
            }
        });
    }
}

function getShipmentDetailsByEventId(info) {
    $.ajax({
        url: `${routeURL}/api/Shipment/GetShipmentById/${info.id}`,
        type: 'GET',
        dataType: 'JSON',
        success: function (response) {
            if (response.status === 1 && response.data !== undefined) {
                openModal(response.data, true);
            }
        },
        error: function (xhr) {
            $.notify('Error', 'error');
        }
    });
}

function checkValidation() {
    let isValid = true;
    if ($('#shipDate').val() === undefined || $('#shipDate').val() === '') {
        isValid = false;
        $('#shipDate').addClass('error');
    }
    if (events.some((e) => e.bookId === $('#bookId').val())) {
        isValid = false;
        $('#bookId').addClass('error');
        $.notify('Shipment has already been scheduled for that book', 'error');
    }
    else {
        $('#shipDate').removeClass('error');
        $('#bookId').removeClass('error');
    }
    return isValid;
}

function openModal(data, isDetail = false) {
    if (isDetail) {
        $('#shipDate').val(data.shipDate);
        $('#bookId').val(data.bookId);
    }
    else {
        $('#shipDate').val(data.startStr);
        $('#id').val(0);
    }
    $('#shippingInput').modal('show');
}

function closeModal() {
    $('#shipmentForm')[0].reset();
    $('#shipDate').removeClass('error');
    $('#bookId').removeClass('error');
    $('#shippingInput').modal('hide');
}