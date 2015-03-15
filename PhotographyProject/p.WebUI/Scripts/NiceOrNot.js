

$(document).on('mouseleave', '.notLink', function () {
    $(this).parent().find('.niceOrNot').find('.after').text('');
    $(this).parent().find('.niceOrNot').find('.before').show();
});

$(document).on('mouseover', '.notLink', function () {
    $(this).parent().find('.niceOrNot').find('.after').text('ot');
    $(this).parent().find('.niceOrNot').find('.before').hide();
});

$(document).on('mouseleave', '.niceLink', function () {
    $(this).parent().find('.niceOrNot').find('.after').text('');
    $(this).parent().find('.niceOrNot').find('.before').show();
});

$(document).on('mouseover', '.niceLink', function () {
    $(this).parent().find('.niceOrNot').find('.after').text('ice');
    $(this).parent().find('.niceOrNot').find('.before').hide();
});

$(document).on('click', '.niceLink', function () {
    $(this).parent().find('.niceOrNot').find('.before').text('ice');
});
$(document).on('click', '.notLink', function () {
    $(this).parent().find('.niceOrNot').find('.before').text('ot');
});