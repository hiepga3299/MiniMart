// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).on('click', '.btn-add-cart', function () {
    $.blockUI();
    const code = $(this).data('code');
    console.log(code);
    $.ajax({
        url: '/cart/addcart',
        method: 'POST',
        data: { productCode: code, quantity: 1 },
        success: function (response) {
            if (response) {
                $('.cart-number').text(response);
                $.unblockUI();
            }
        }
    });
});