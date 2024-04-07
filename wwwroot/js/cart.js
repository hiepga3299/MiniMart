(function () {
    function initial() {
        regiterEvent()
    };

    function regiterEvent() {

        function caculateCartTotal() {
            let totalCart = 0;
            const elementTrs = $('#tbody-cart tr');
            for (var i = 0; i < elementTrs.length; i++) {
                const total = parseInt($(elementTrs[i]).find('.txt-total').text().replaceAll('đ', '').replaceAll('.', ''));
                totalCart += total;
            }

            $('#txt-total-cart').text(totalCart.toLocaleString('vi-VN', {
                style: 'currency',
                currency: 'VND'
            }));
        }

        $(document).on('click', '.qtybtn', function () {
            const self = $(this);
            const parentTr = self.closest('tr');
            const price = parseInt(parentTr.find('.txt-price').text().replaceAll('đ', '').replaceAll('.', ''));
            const quantity = parseInt(parentTr.find('.txt-quantity').val());

            const total = price * quantity;
            parentTr.find('.txt-total').text(total.toLocaleString('vi-VN', {
                style: 'currency',
                currency: 'VND'
            }));
            caculateCartTotal()
        });

        $(document).on('click', '.qtybtn', function () {
            const elementTrs = $('#tbody-cart tr');
            let products = [];
            for (var i = 0; i < elementTrs.length; i++) {
                const quantity = parseInt($(elementTrs[i]).find('.txt-quantity').val());
                const code = $(elementTrs[i]).data('code');
                products.push({ productCode: code, quantity });
            }
            $.ajax({
                url: '/cart/update',
                method: 'POST',
                data: JSON.stringify(products),
                contentType: 'application/json',
                success: function (res) {

                }
            })
        });

        $(document).on('click', '.btn-delete-cart', function () {

            const seft = $(this);
            const code = seft.closest('tr').data('code');
            $.ajax({
                url: `/cart/delete?code=${code}`,
                method: 'POST',
                success: function (res) {
                    seft.closest('tr').remove();
                    caculateCartTotal();
                    $('.cart-number').text($('#tbody-cart tr').length);
                }
            });
        });
    };

    initial();
})();