$(document).ready(function () {
    $('#productName').autocomplete({
        source: '/user/SearchAutocomplete',
        delay: 500
    });
});