(function () {
    const elementName = '#tbl-order';
    const urlApi = '/admin/order/getbypagination';
    const columns = [
        { data: 'fullname', name: 'fullname' },
        { data: 'code', name: 'code' },
        {
            data: 'createOn', name: 'createOn', render: function (data) {
                return `<div class="text-left">${(new Date(data)).toLocaleDateString('vi-VN', { day: '2-digit', month: '2-digit', year: 'numeric' })}</div>`
            }
        },
        {
            data: 'totalPrice', name: 'totalPrice', render: function (data) {
                return `<div class="text-left">${data.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}</div>`
            }
        },
        {
            data: 'paymentMethod', name: 'paymentMethod', width: '200px', render: function (data) {
                return `<div class="text-left">${formatPaymentMethod(data)}</div>`
            }
        },
        { data: 'status', name: 'status', width: '100px' },
        {
            data: 'id', name: 'id', render: function (key) {
                return `<span data-key="${key}">
                            <a href="/admin/order/orderdetail?orderId=${key}" class="btn btn-primary" type="button">
                                Xem
                            </a> &nbsp
                            <a href="/admin/report/ExportPdfOder?id=${key}" class="btn btn-primary" type="button" id="abc">
                                Xuất PDF <i class="far fa-file-alt"></i>
                            </a>
                         </span>`
            }
        },
    ];
    registerDataTable(elementName, columns, urlApi)

    function formatPaymentMethod(data) {
        switch (data.toLowerCase()) {
            case 'cash':
                return `<span>Tiền Mặt</span>`
            case 'paypal':
                return `<span>Thẻ</span>`
        }
    }

})();