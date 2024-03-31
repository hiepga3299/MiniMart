(function () {
    function initial() {
        registerEvents();
    }
    function registerEvents() {
        $(document).on('click', '#btn-loading', function () {
            const categoryId = $('#current-category').val();
            const pageIndex = parseInt($('#current-page-index').val()) + 1;
            $.ajax({
                url: `/product/getproductpagination?category=${categoryId}&pageIndex=${pageIndex}`,
                method: 'GET',
                success: function (response) {
                    if (response) {
                        let html = '';
                        response.products.forEach((product, index) => {
                            html += `<div class="col-lg-3 col-sm-6 mix all dresses bags">
                                        <div class="single-product-item">
                                            <figure>
                                                <a href="#"><img src="./images/product/${product.code}.png" alt=""></a>
                                                <div class="p-status">new</div>
                                            </figure>
                                            <div class="product-text">
                                                <h6>${product.name}</h6>
                                                <p>${(product.price).toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}</p>
                                            </div>
                                        </div>
                                    </div>`;
                        })
                        $('#product-list').append(html);
                        $('#btn-loading').attr('disabled', 'disabled');
                        $.unblockUI();
                        $('#current-page-index').val(pageIndex);
                    }
                }
            })
        })
    }

    initial()
})();