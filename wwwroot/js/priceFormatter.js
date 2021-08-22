$(document).ready(function () {

    $('.bookPrice').each((_, p) => {
        p.innerHTML = parseFloat(p.innerHTML).toFixed(2);
    })

    $('#bookPrice').val(parseFloat($('#bookPrice').val()).toFixed(2));

});
