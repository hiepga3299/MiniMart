(function () {
    function initial() {
        searchEvent()
    }

    function searchEvent() {
        $(document).ready(function () {
            $('#txt-search').keypress(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    var keyword = $('#txt-search').val();
                    const categoryId = $('#current-category').val();
                    const pageIndex = parseInt($('#current-page-index').val()) + 1;
                    $.ajax({
                        url: `/product/getproductpagination?category=${categoryId}&pageIndex=${pageIndex}&keyword=${keyword}`,
                        method: 'GET',
                        success: function (response) {
                            if (response) {
                                window.location.href = '/Product/Search?keyword=' + encodeURIComponent(keyword);
                            }
                        }
                    })
                }
            })
        })
    }
    initial()
})()