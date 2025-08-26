$(function () {

    $(document).on('click', '[data-confirm]', function (e) {
        var msg = $(this).attr('data-confirm') || 'Are you sure?';
        if (!confirm(msg)) { e.preventDefault(); }
    });

    $('[data-filter]').each(function () {
        var $table = $(this);
        var inputSel = $table.attr('data-filter');
        $(inputSel).on('input', function () {
            var q = $(this).val().toString().toLowerCase();
            $table.find('tbody tr').each(function () {
                var rowText = $(this).text().toLowerCase();
                $(this).toggle(rowText.indexOf(q) >= 0);
            });
        });
    });
});