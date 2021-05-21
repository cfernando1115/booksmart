const currentPage = +$('.currentPage').text();
const totalPages = +$('.totalPages').text();

const firstBtn = $('#first');
const lastBtn = $('#last');

const pageBtns = Array.from($('.pageBtn'));
pageBtns.forEach(btn => {
    if (+btn.dataset.page === currentPage) {
        btn.classList.add('active');
    }
})

if (currentPage === 1 && totalPages === 1) {
    firstBtn.addClass('disabled');
    lastBtn.addClass('disabled');
}

if (currentPage === 1 && totalPages > 1) {
    firstBtn.addClass('disabled');
    lastBtn.removeClass('disabled');
}

if (currentPage === totalPages && totalPages > 1) {
    lastBtn.addClass('disabled');
    firstBtn.removeClass('disabled');
}


