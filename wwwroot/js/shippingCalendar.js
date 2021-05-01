

let routeURL = location.protocol + '//' + location.host;

$(document).ready(function () {
    initializeCalendar();

    $('#shipDate').kendoDatePicker({
        value: new Date(),
        dateInput: false
    });
});

let events = [];

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
                validRange: function (nowDate) {
                    return {
                        start: nowDate,
                        end: Date.parse($('#membershipExpiration').val())
                    };
                },
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
                    isEdit = true;
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
let requestData;
    if (checkValidation()) {
        requestData = {
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

function deleteShipment() {
    const id = +($('#id').val());
    $.ajax({
        url: `${routeURL}/api/Shipment/DeleteShipment/${id}`,
        type: 'GET',
        dataType: 'JSON',
        success: function (response) {
            if (response.status === 1) {
                $.notify(response.message, 'success');
                calendar.refetchEvents();
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

function confirmShipment() {
    const id = +($('#id').val());
    $.ajax({
        url: `${routeURL}/api/Shipment/ConfirmShipment/${id}`,
        type: 'GET',
        dataType: 'JSON',
        success: function (response) {
            if (response.status > 0) {
                $.notify(response.message, 'success');
                calendar.refetchEvents();
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

function checkValidation() {
    let isValid = true;
    if ($('#shipDate').val() === undefined || $('#shipDate').val() === '') {
        isValid = false;
        $('#shipDate').addClass('error');
        $('#date-error').text('Please select a valid ship date.');
        $('#date-error').removeClass('d-none');
    }
    else {
        if (events.some((e) => e.bookId === $('#bookId').val()) && +$('#id').val() === 0) {
            isValid = false;
            $('#bookId').addClass('error');
            $('#book-error').text('Shipment already scheduled. To update, please edit existing shipment');
            $('#book-error').removeClass('d-none');
        }
        else {
            $('#shipDate').removeClass('error');
            $('#bookId').removeClass('error');
            $('#date-error').addClass('d-none');
            $('#book-error').addClass('d-none');
        }
    }
    return isValid;
}

function openModal(data, isDetail = false) {
    if (isDetail) {
        $('#shipDate').val(data.shipDate);
        $('#bookId').val(data.bookId);
        $('#id').val(data.id);
        $('#bookId').addClass('d-none');
        $('#bookLabel').removeClass('d-none');
        $('#bookLabel').val(data.bookName);
        if (data.isConfirmed) {
            $('#btnConfirm').addClass('d-none');
            $('#btnDelete').addClass('d-none');
            $('#btnSubmit').addClass('d-none');
            $('#shipDate').addClass('d-none');
            $('.k-datepicker').addClass('d-none');
            $('#shipDateLabel').removeClass('d-none');
            $('#shipDateLabel').val(data.shipDate);
        }
        else {
            $('#btnConfirm').removeClass('d-none');
            $('#btnDelete').removeClass('d-none');
            $('#btnSubmit').removeClass('d-none');
            $('#shipDate').removeClass('d-none');
            $('.k-datepicker').removeClass('d-none');
            $('#shipDateLabel').addClass('d-none');
            $('#shipDateLabel').val(data.shipDate);
        }
    }
    else {
        $('#shipDate').val(data.startStr);
        $('#id').val(0);
        $('#bookId').removeClass('d-none');
        $('#bookLabel').addClass('d-none');
        $('#btnConfirm').addClass('d-none');
        $('#btnDelete').addClass('d-none');
        $('#btnSubmit').removeClass('d-none');
        $('#shipDate').removeClass('d-none');
        $('.k-datepicker').removeClass('d-none');
        $('#shipDateLabel').addClass('d-none');
        $('#shipDateLabel').val(data.shipDate);
    }
    $('#shippingInput').modal('show');
}

function closeModal() {
    $('#shipmentForm')[0].reset();
    $('#shipDate').removeClass('error');
    $('#bookId').removeClass('error');
    $('#date-error').addClass('d-none');
    $('#book-error').addClass('d-none');
    $('#shippingInput').modal('hide');
}