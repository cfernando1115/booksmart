let pageNumber = $('.currentPage').text();
let pageLinks = $('.page-link');

let genreId = 0;
$('#genreId').change(() => genreId = $('#genreId').val());

//Set selected genre
const params = new URLSearchParams(window.location.search);
if (params.has('genreId')) {
    const genreVal = params.get('genreId');
    $(`#genreId option[value=${ genreVal }]`).attr('selected', 'selected');
    genreId = genreVal;
}

//set query on filter apply
$('#applyFilter').click((e) => {
    pageNumber = 1;
    getQuery(e);
})

//reset filter button
$('#filterReset').click((e) => {
    pageNumber = 1;
    genreId = 0;
    getQuery(e);
})

//set query on page change
pageLinks.click((e) => {
    pageNumber = +(e.target.closest('li').dataset.page);
    getQuery(e);
});

function getQuery(e) {
    e.target.setAttribute('href', `${e.target.getAttribute('href')}?pageNumber=${ pageNumber }&genreId=${ genreId }`);
}








