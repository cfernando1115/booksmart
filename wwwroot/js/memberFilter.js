let pageNumber = $('.currentPage').text();
let pageLinks = $('.page-link');

let memberFilter = 'none';
$('#memberFilter').change(() => memberFilter = $('#memberFilter').val());

//set selected genre
const params = new URLSearchParams(window.location.search);
if (params.has('memberFilter')) {
    const memberVal = params.get('memberFilter');
    $(`#memberFilter option[value=${memberVal}]`).attr('selected', 'selected');
    memberFilter = memberVal;
}

//set query on filter apply
$('#applyFilter').click((e) => {
    pageNumber = 1;
    getQuery(e);
})

//reset filter button
$('#filterReset').click((e) => {
    pageNumber = 1;
    memberFilter = 'none';
    getQuery(e);
})

//set query on page change
pageLinks.click((e) => {
    pageNumber = +(e.target.closest('li').dataset.page);
    getQuery(e);
});

function getQuery(e) {
    e.target.setAttribute('href', `${e.target.getAttribute('href')}?pageNumber=${pageNumber}&memberFilter=${memberFilter}`);
}