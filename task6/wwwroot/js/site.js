$(document).ready(function () {
    $('#f-recipient').autocomplete({
        source: '/user/SearchAutocomplete',
        delay: 500
    });
});